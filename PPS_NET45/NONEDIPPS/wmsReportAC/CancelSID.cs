using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace wmsReportAC
{
    public partial class CancelSID : Form
    {
        public CancelSID()
        {
            InitializeComponent();
        }
        private Int32 g_curRow = -1;    //当前选中行号

        private void CancelSID_Load(object sender, EventArgs e)
        {
            DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            dt_start.Value = dateTimeNow.AddDays(-1);
            dt_end.Value = dateTimeNow.AddDays(1);


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnSearch.Enabled = false;

            showSIDList();

            btnSearch.Enabled = true;

        }
        private void showSIDList()
        {
            string strStartDay = dt_start.Value.ToString("yyyy-MM-dd");
            string strEndDay = dt_end.Value.ToString("yyyy-MM-dd");
            dgvSID.DataSource = null;
            WMSBLL rb = new WMSBLL();
            DataTable dtDNList = rb.GetZCSIDListDataTable(strStartDay, strEndDay);
            if (dtDNList == null || dtDNList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvSID.DataSource = dtDNList;
            }
        }
        private DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt;
            switch (strType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Silver;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Green;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }

        private void dgvSID_SelectionChanged(object sender, EventArgs e)
        {

            reflashlabel();
        }
        private void reflashlabel()
        {
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvSID.CurrentRow.Index;
                //rowIndex = dgvSID.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvSID.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                if (g_curRow == rowIndex)
                    return;
                g_curRow = rowIndex;

                txtSmId.Text = dgvSID.Rows[rowIndex].Cells["SHIPMENT_ID"].Value.ToString();
            }
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtSmId.Text) )
            {
                ShowMsg("NG，请选择合适的集货单再取消集货单", 0);
                return;
            }
            ShowMsg("", -1);
            string strSID = txtSmId.Text;

            btnRollback.Enabled = false;


            string strResult = string.Empty;
            string strResulterrmsg = string.Empty;
            WMSBLL pb = new WMSBLL();
            strResult = pb.RBShipmentID2(strSID, out strResulterrmsg);
            if (strResult.Equals("NG"))
            {
                ShowMsg(strResulterrmsg, 0);
                btnRollback.Enabled = true;
                return;
            }
            //刷新dgvSID list
            showSIDList();
            //选择groupcode 最后一个
            if (dgvSID.RowCount == 0)
            {
                ShowMsg("NG，查不资料！", 0);
                return;
            }
            else
            {
                bool isfirst = true;
                for (int i = 0; i < dgvSID.RowCount - 1; i++)
                {
                    if (dgvSID.Rows[i].Cells["SHIPMENT_ID"].Value.ToString().Equals(strSID))
                    {
                        dgvSID.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        if (isfirst)
                        {
                            dgvSID.Rows[i].Selected = true;
                            dgvSID.FirstDisplayedScrollingRowIndex = i;
                            txtSmId.Text = dgvSID.Rows[i].Cells["SHIPMENT_ID"].Value.ToString();
                            isfirst = false;
                        }
                    }
                }
            }
            btnRollback.Enabled = true;
        }
    }
}
