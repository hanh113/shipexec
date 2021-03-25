using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ClientUtilsDll;

namespace wmsReport
{
    public partial class fWMSQuery : Form
    {
        public fWMSQuery()
        {
            InitializeComponent();
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            QueryData();
        }

        private void QueryData()
        {
            ShowMsg("", -1);
            TextMsg.Text = "";
            string strSN = txtSN.Text.Trim();
            if (string.IsNullOrEmpty(strSN))
            {
                this.ShowMsg("输入的 " + lblSN.Text + " 不能为空！", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }

            dgvDetail.Rows.Clear();
            WMSBLL plb = new WMSBLL();

            string strCustIn = string.Empty;

            StringBuilder sb = new StringBuilder();
            string[] strSNArr = this.txtSN.Lines;
            if (strSNArr.Length > 1000)
            {
                this.ShowMsg("查询的序号不能超过1000行", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }
            foreach (string varSN in strSNArr)
            {
                if (!string.IsNullOrEmpty(varSN.Trim()))
                {
                    sb.AppendFormat("'{0}',", plb.DelPrefixCartonSN(varSN.Trim()).Replace("'", "''"));
                }
            }
            if (sb.Length > 0)
            {
                strCustIn = sb.ToString().Trim();
                strCustIn = strCustIn.Substring(0, strCustIn.Length - 1);
            }
            else
            {
                this.ShowMsg("输入的 " + lblSN.Text + " 不能为空！", 0);
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }

            //strSN = plb.DelPrefixCartonSN(strSN);

            //string strSQL2;
            //strSQL2 = "SELECT CUSTOMER_SN, CARTON_NO, PART_NO, LOCATION_NO, PALLET_NO  FROM PPSUSER.T_SN_STATUS " +
            //         " WHERE (CUSTOMER_SN = '" + strSN.Replace("'", "''") + "' or Carton_NO ='" + strSN.Replace("'", "''") + "'"
            //         +" or pallet_no = '" + strSN.Replace("'", "''") + "' ) and wc ='W0'";

            string strSQL2 = string.Format(@"
SELECT x.CUSTOMER_SN, x.CARTON_NO, x.PART_NO, x.LOCATION_NO, x.PALLET_NO,x.TROLLEY_LINE_NO,x.POINTNO
FROM(SELECT a.CUSTOMER_SN, a.CARTON_NO, a.PART_NO, a.LOCATION_NO, a.PALLET_NO,c.TROLLEY_LINE_NO,b.POINTNO
FROM ppsuser.T_SN_STATUS a LEFT JOIN ppsuser.T_TROLLEY_SN_STATUS b ON to_char(a.CUSTOMER_SN)=to_char(b.CUSTOM_SN)
LEFT JOIN ppsuser.T_TROLLEY_LINE_INFO c ON b.TROLLEY_NO=c.TROLLEY_NO AND 
b.SIDES_NO=c.SIDES_NO AND b.LEVEL_NO=c.LEVEL_NO AND b.SEQ_NO=c.SEQ_NO
WHERE a.CUSTOMER_SN IN
(
{0}
)
AND a.WC='W0'
UNION
SELECT aa.CUSTOMER_SN, aa.CARTON_NO, aa.PART_NO, aa.LOCATION_NO, aa.PALLET_NO,cc.TROLLEY_LINE_NO,bb.POINTNO
FROM ppsuser.T_SN_STATUS aa LEFT JOIN ppsuser.T_TROLLEY_SN_STATUS bb ON to_char(aa.CUSTOMER_SN)=to_char(bb.CUSTOM_SN)
LEFT JOIN ppsuser.T_TROLLEY_LINE_INFO cc ON bb.TROLLEY_NO=cc.TROLLEY_NO AND 
bb.SIDES_NO=cc.SIDES_NO AND bb.LEVEL_NO=cc.LEVEL_NO AND bb.SEQ_NO=cc.SEQ_NO
WHERE aa.CARTON_NO IN
(
{0}
)
AND aa.WC='W0'
UNION
SELECT aaa.CUSTOMER_SN, aaa.CARTON_NO, aaa.PART_NO, aaa.LOCATION_NO, aaa.PALLET_NO,ccc.TROLLEY_LINE_NO,bbb.POINTNO
FROM ppsuser.T_SN_STATUS aaa LEFT JOIN ppsuser.T_TROLLEY_SN_STATUS bbb ON to_char(aaa.CUSTOMER_SN)=to_char(bbb.CUSTOM_SN)
LEFT JOIN ppsuser.T_TROLLEY_LINE_INFO ccc ON bbb.TROLLEY_NO=ccc.TROLLEY_NO AND 
bbb.SIDES_NO=ccc.SIDES_NO AND bbb.LEVEL_NO=ccc.LEVEL_NO AND bbb.SEQ_NO=ccc.SEQ_NO
WHERE aaa.PALLET_NO IN
(
{0}
)AND aaa.WC='W0')x
ORDER BY x.TROLLEY_LINE_NO,x.POINTNO,x.LOCATION_NO, x.PALLET_NO,x.PART_NO,x.CARTON_NO,x.CUSTOMER_SN 
", strCustIn);

            DataTable dt2 = new DataTable();

            try
            {
                dt2 = ClientUtils.ExecuteSQL(strSQL2).Tables[0];
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
            }

            if (dt2.Rows.Count == 0)
            {
                ShowMsg("查无资料", 0);
                txtSN.Text = "";
                txtSN.Focus();
                return;
            }
            else
            {
                //dgvDetail.DataSource = dt2;

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvDetail.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dt2.Rows[i]["CUSTOMER_SN"].ToString();
                    dr.Cells[1].Value = dt2.Rows[i]["CARTON_NO"].ToString();
                    dr.Cells[2].Value = dt2.Rows[i]["PART_NO"].ToString();
                    dr.Cells[3].Value = dt2.Rows[i]["LOCATION_NO"].ToString();
                    dr.Cells[4].Value = dt2.Rows[i]["PALLET_NO"].ToString();
                    dr.Cells[5].Value = dt2.Rows[i]["TROLLEY_LINE_NO"].ToString();
                    dr.Cells[6].Value = dt2.Rows[i]["POINTNO"].ToString();
                    try
                    {
                        dgvDetail.Invoke((MethodInvoker)delegate ()
                        {
                            dgvDetail.Rows.Add(dr);

                        });
                    }
                    catch (Exception)
                    {

                        return;
                    }
                }

            }
            this.txtSN.SelectAll();
            this.txtSN.Focus();
        }

        private int snCheck(DataRow dr)
        {
            //扫描出Customer SN / CARTON NO是否已存在，并更查询汇总数据与显示内容
            //0: 该储位查询出的箱号，还未扫描
            //1: 该储位查询出的箱号，已扫描，但Customer SN 未扫描
            //2: 该储位查询出的箱号、Customer SN都已扫描
            int ds = dgvDetail.Rows.Count;

            int retValue = 0;

            for (int i = 0; i < ds; i++)
            {
                //比较箱号是否相同
                if (dgvDetail.Rows[i].Cells["cartonid"].Value.ToString() == dr["Carton_NO"].ToString())
                {
                    //比较Customer SN 是否相同
                    if (retValue == 0)
                        retValue = 1;
                }
                //比较Customer SN 是否相同
                if (dgvDetail.Rows[i].Cells["sn"].Value.ToString() == dr["CUSTOMER_SN"].ToString())
                {
                    //比较Customer SN 是否相同
                    if (retValue == 1)
                    {
                        //检测到Customer SN 与 Carton ID都相同时，退出
                        retValue = 2;
                        break;
                    }
                }
            }

            return retValue;

        }

        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
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
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void btnEXCEL3_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvDetail.Rows.Count > 0)
                {
                    ExportExcel(dgvDetail);
                }
                else
                {
                    this.ShowMsg("确认导出Excel有数据！", 0);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ExportExcel(DataGridView dt)
        {
            //获取导出路径
            string filePath = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "EXCEL 97-2007 工作簿(*.xls)|*.xls";//设置文件类型

            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string currdate = currentTime.ToString("yyyy-MM-dd-HH-mm-ss");
            //HH是24小时制,hh是12小时制

            //sfd.FileName = "wmsReport"+cmbTYPE.Text.Trim()+"_"+cmbLocation.Text.Trim()+"_"+ currdate;//设置默认文件名

            sfd.FileName = "_" + currdate;
            sfd.DefaultExt = "xlsx";//设置默认格式（可以不设）
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
            }
            else
            {
                this.ShowMsg("导出Excel失败！", 0);
            }

            IWorkbook workbook;
            string fileExt = Path.GetExtension(filePath).ToLower();
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                return;
            }
            ISheet sheet = string.IsNullOrEmpty("wmsReport") ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet("wmsReport");

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].HeaderText);
            }

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    if (dt.Rows[i].Cells[j].Value != null)
                    {
                        cell.SetCellValue(dt.Rows[i].Cells[j].Value.ToString());
                    }
                    else
                    {
                        cell.SetCellValue("");
                    }

                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
                this.ShowMsg("导出Excel成功！", 5);
            }
        }

        private void btnSN_Click(object sender, EventArgs e)
        {
            FrmSNSingle frm = new FrmSNSingle();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.txtSN.Text = "";
                this.txtSN.Text = frm.strSnArray;
                QueryData();
            }
        }

        /// <summary>
        /// PPS中未出货HOLD数据
        /// </summary>
        private void btnHOLD_Click(object sender, EventArgs e)
        {
            FrmPPSHOLD frm = new FrmPPSHOLD();
            frm.ShowDialog();
        }

        private void fWMSQuery_Load(object sender, EventArgs e)
        {
            string strLocalMACADDRESS = LibHelper.LocalHelper.getMacAddr_Local();

            string strSql = string.Format(@"
SELECT a.PARA_VALUE 
FROM PPSUSER.T_BASICPARAMETER_INFO a
WHERE a.PARA_TYPE='QHOLD_MAC' AND a.PARA_VALUE='{0}' AND a.ENABLED='Y'
", strLocalMACADDRESS);
            DataTable dtTemp= ClientUtils.ExecuteSQL(strSql).Tables[0];

            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                this.btnHOLD.Visible = true;
            }
            else
            {
                this.btnHOLD.Visible = false;
            }
        }
    }
}
