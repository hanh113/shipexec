using ClientUtilsDll.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wmsReport.Core;

namespace wmsReport.Forms
{
    public partial class StockTransfer : PPSForm
    {
        ExecuteResult exeRes;
        public StockTransfer()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

        private void StockTransfer_Load(object sender, EventArgs e)
        {
            exeRes = new ExecuteResult();
            exeRes = this.GetData();
            DataTable dtPallet = (DataTable)exeRes.Anything;
            var part_no = dtPallet.AsEnumerable().Select(x => x["part_no"].ToString()).Distinct().ToArray();
            var coo = dtPallet.AsEnumerable().Select(x => x["coo"].ToString()).Distinct().ToArray();
            cmbKeyPart.Items.Clear();
            cmbCoo.Items.Clear();
            cmbKeyPart.Items.AddRange(part_no);
            cmbCoo.Items.AddRange(coo);
            dgvStorage.Rows.Clear();
            dgvStorage.DataSource = dtPallet;
        }

        private ExecuteResult GetData()
        {
            string sql, sql3 = string.Empty;
            string sql1 = @"select a.location_no, a.part_no, b.coo, a.pallet_no, count(*) as qty
                                      from ppsuser.t_location a, ppsuser.t_sn_status b
                                     where a.location_no = 'SOLIDSTORE'
                                       and a.pallet_no = b.pallet_no 
                                       and a.location_no=b.location_no ";
            string sql2 = @" group by a.location_no, a.part_no, b.coo, a.pallet_no ,b.cdt order by b.cdt ";
            if (!string.IsNullOrEmpty(cmbKeyPart.Text.Trim().ToString()))
            {
                sql3 += "  and  b.part_no='" + cmbKeyPart.Text.Trim().ToString() + "' ";
            }
            if (!string.IsNullOrEmpty(cmbCoo.Text.Trim().ToString()))
            {
                sql3 += "  and  b.coo='" + cmbCoo.Text.Trim().ToString() + "' ";
            }
            sql = sql1 + sql3 + sql2;
            exeRes.Anything = ClientUtils.ExecuteSQL(sql).Tables[0];
            return exeRes;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            exeRes = this.GetData();
            DataTable dtPallet = (DataTable)exeRes.Anything;
            dgvStorage.DataSource = dtPallet;
        }

        private void dgvStorage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell dgvCheck = (DataGridViewCheckBoxCell)(this.dgvStorage.Rows[this.dgvStorage.CurrentCell.RowIndex].Cells["check"]);
            if (this.dgvStorage.CurrentCell.ColumnIndex == 0)
            {
                int iQty = 0;
                dgvCheck.Value = !Convert.ToBoolean(dgvCheck.EditedFormattedValue);
                for (int i = 0; i < this.dgvStorage.Rows.Count; i++)
                {
                    if (this.dgvStorage.Rows[i].Cells["check"].EditedFormattedValue.ToString().Equals("True"))
                    {
                        iQty += int.Parse(this.dgvStorage[5, i].Value.ToString());
                    }
                }
                lbqtyTotal.Text = iQty.ToString();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            exeRes = new ExecuteResult();
            List<string> pallets = new List<string>();
            for (int i = 0; i < this.dgvStorage.Rows.Count; i++)
            {
                if (this.dgvStorage.Rows[i].Cells["check"].EditedFormattedValue.ToString().Equals("True"))
                {
                    //循环获取选中栈板号
                    pallets.Add(this.dgvStorage[4, i].Value.ToString());
                }
            }
            if (pallets.Count > 0)
            {
                // richTextBox1.AppendText(Environment.NewLine + DateTime.Now.ToShortTimeString() + "-------Start");
                //异步，程序不假死
                this.CallApiAsync(pallets.ToArray(), "TaskID:" + Guid.NewGuid());
                // richTextBox1.AppendText(Environment.NewLine + DateTime.Now.ToShortTimeString() + "-------End");
            }
        }

        public async void CallApiAsync(string[] pallets, string guid)
        {
            richTextBox1.Clear();
            Controller core = new Controller();
            exeRes = new ExecuteResult();
            //异步执行，不等待
            exeRes = await Task.Run<ExecuteResult>(() => core.changeLocation(pallets.ToArray()));
            richTextBox1.AppendText(Environment.NewLine + DateTime.Now.ToShortTimeString() + "---" + guid + ":" + exeRes.Message);
            if (exeRes.Status)
                SuccessMSG(guid + ":" + exeRes.Message);
            else
                ErrorMSG(guid + ":" + exeRes.Message);
        }

        private  void btnAllStockOut_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT  pallet_no FROM T_LOCATION WHERE LOCATION_NO='SOLIDSTORE' and trunc(udt)>=trunc(sysdate-5) ORDER BY udt";
            var dt = ClientUtils.ExecuteSQL(qry).Tables[0];
            var lstPallets = dt.AsEnumerable().Select(x => x.Field<string>("pallet_no")).ToArray();

            //var lstClone = lstPallets.Clone();

            int nextStep = 1;
            int c = 0;
            int sleep = 200;
            for (int i = 1; i < lstPallets.Count(); i += nextStep)
            {
                var lstClone = lstPallets.Skip(i).Take(nextStep).ToArray();
                //c += lstClone.Count();
                 this.CallApiAsync(lstClone, "TaskID:" + Guid.NewGuid());
                Thread.Sleep(sleep);
            }
        }
    }
}
