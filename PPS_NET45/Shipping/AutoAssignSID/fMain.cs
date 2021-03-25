using ClientUtilsDll;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SajetClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoAssignSID
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }
        private Int32 g_curRow = -1;    //当前选中行号
        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            //dtSID.Value = dateTimeNow.AddDays(-1);
            dtSID.Value = dateTimeNow;
            this.WindowState = FormWindowState.Maximized;


        }

        private void btnSearchSID_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnSearchSID.Enabled = false;


            showSIDList();

            btnSearchSID.Enabled = true;
        }
        private void showSIDList()
        {
            string strStartDay = dtSID.Value.ToString("yyyy-MM-dd");
            string strEndDay = dtSID.Value.AddDays(1).ToString("yyyy-MM-dd");

            dgvSID.Rows.Clear();
            dgvSID.DataSource = null;
            AutoAssignSIDBLL aasb = new AutoAssignSIDBLL();
            DataTable dtSIDList = aasb.GetSIDListDataTable(strStartDay, strEndDay);
            if (dtSIDList == null || dtSIDList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                //dgvSID.DataSource = dtSIDList;
                for (int i = 0; i < dtSIDList.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvSID.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = dtSIDList.Rows[i]["SHIPMENT_ID"].ToString();
                    dr.Cells[1].Value = dtSIDList.Rows[i]["FD_DS"].ToString();
                    dr.Cells[2].Value = dtSIDList.Rows[i]["TYPE"].ToString();
                    dr.Cells[3].Value = dtSIDList.Rows[i]["PRIORITY"].ToString();
                    dr.Cells[4].Value = dtSIDList.Rows[i]["REGION"].ToString();
                    dr.Cells[5].Value = dtSIDList.Rows[i]["CNAME"].ToString();
                    dr.Cells[6].Value = dtSIDList.Rows[i]["CARTON_QTY"].ToString();
                    dr.Cells[7].Value = dtSIDList.Rows[i]["PACK_CARTON"].ToString();

                    try
                    {
                        dgvSID.Invoke((MethodInvoker)delegate ()
                        {
                            dgvSID.Rows.Add(dr);
                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                }
            }
        }
        private DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TL();
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

        private void btnSearchLine_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnSearchLine.Enabled = false;

            showLineList();

            btnSearchLine.Enabled = true;
        }
        private void showLineList()
        {

            dgvLine.DataSource = null;
            dgvLine.Rows.Clear();
            AutoAssignSIDBLL aasb = new AutoAssignSIDBLL();
            DataTable dtlineList = aasb.GetLineListDataTable();
            if (dtlineList == null || dtlineList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvLine.DataSource = dtlineList;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnStart.Enabled = false;
            //只有日期的参数

            string strStartDay = dtSID.Value.ToString("yyyy-MM-dd");
            string strEndDay = dtSID.Value.AddDays(1).ToString("yyyy-MM-dd");

            AutoAssignSIDBLL aasb = new AutoAssignSIDBLL();
            string errorMessage = string.Empty;
            string strResult = aasb.AutoAssignSIDbydate(strStartDay, out errorMessage);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(errorMessage, 0);
                btnStart.Enabled = true;
                return;
            }

            //执行SP(1.更新t_line_assign,分配集货单号)

            if (!chkShowPallet.Checked)
            {
                showAssginLineList();
            }
            else
            {
                showAssginLineList2();
            }
            showcolor();
            //刷新界面， 获得t_line_assign
            btnStart.Enabled = true;

        }


        private void showAssginLineList()
        {

            dgvAssign.DataSource = null;
            dgvAssign.Rows.Clear();
            AutoAssignSIDBLL aasb = new AutoAssignSIDBLL();
            DataTable dtlineList = aasb.GetAssignLineListDataTable();
            if (dtlineList == null || dtlineList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvAssign.DataSource = dtlineList;
            }
        }
        private void showAssginLineList2()
        {

            dgvAssign.DataSource = null;
            dgvAssign.Rows.Clear();
            AutoAssignSIDBLL aasb = new AutoAssignSIDBLL();
            DataTable dtlineList = aasb.GetAssignLineListDataTable2();
            if (dtlineList == null || dtlineList.Rows.Count == 0)
            {
                ShowMsg("NG，查不资料！", 0);
            }
            else
            {
                dgvAssign.DataSource = dtlineList;
            }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnResult.Enabled = false;

            if (!chkShowPallet.Checked)
            {
                showAssginLineList();
            }
            else
            {
                showAssginLineList2();
            }
            showcolor();

            btnResult.Enabled = true;
        }

        public void showcolor()
        {
            for (int i = 0; i < dgvSID.RowCount; i++)
            {
                dgvSID.Rows[i].DefaultCellStyle.BackColor = Color.White;
                for (int j = 0; j < dgvAssign.RowCount; j++)
                {
                    string strsid = string.Empty;
                    string strasssid = string.Empty;
                    try { strsid = dgvSID.Rows[i].Cells[0].Value.ToString(); }
                    catch (Exception) { strsid = ""; break; }


                    try { strasssid = dgvAssign.Rows[j].Cells["SHIPMENT_ID"].Value.ToString(); }
                    catch (Exception) { strasssid = ""; break; }


                    if (strsid.Equals(strasssid) && !string.IsNullOrEmpty(strsid))
                    {
                        dgvSID.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                    }
                }
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

        private void btnEXCEL1_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvSID.Rows.Count > 1)
                {
                    ExportExcel(dgvSID);
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

        private void btnEXCEL2_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvLine.Rows.Count > 0)
                {
                    ExportExcel(dgvLine);
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

        private void btnEXCEL3_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvAssign.Rows.Count > 0)
                {
                    ExportExcel(dgvAssign);
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

        private void dgvAssign_SelectionChanged(object sender, EventArgs e)
        {
            reflashlabel();
        }

        private void reflashlabel()
        {
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvAssign.CurrentRow.Index;
                //rowIndex = dgvDN.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvAssign.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                if (g_curRow == rowIndex)
                    return;
                g_curRow = rowIndex;

                txtLine.Text = dgvAssign.Rows[rowIndex].Cells["LINENAME"].Value.ToString();
                txtSID.Text = dgvAssign.Rows[rowIndex].Cells["SHIPMENT_ID"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //检查line是否为空 

            //集货单如果是空 就是删除

            //集货单是其它值这执行SP进行更新。

            ShowMsg("", -1);
            btnUpdate.Enabled = false;

            string strLine = txtLine.Text;
            string strSID = txtSID.Text;

            string strNewSID = txtNewSID.Text;

            if (string.IsNullOrEmpty(strLine))
            {
                ShowMsg("Line不能为空", -1);

                afterbtnUpdate();
                return;
            }

            if (string.IsNullOrEmpty(strSID) && string.IsNullOrEmpty(strNewSID))
            {
                ShowMsg("新旧集货单不能都为空", -1);
                afterbtnUpdate();
                return;
            }

            if (string.IsNullOrEmpty(strNewSID))
            {

                DialogResult result = ShowMsg("确定取消已经分配的集货单", 2);
                if (result == DialogResult.No)
                {
                    afterbtnUpdate();
                    ShowMsg("", -1);
                    return;
                }

            }
            ShowMsg("", -1);

            string strStartDay = dtSID.Value.ToString("yyyy-MM-dd");

            AutoAssignSIDBLL aasb = new AutoAssignSIDBLL();
            string errorMessage = string.Empty;
            string strResult = aasb.UpdateLineSID(strLine, strSID, strNewSID, strStartDay, out errorMessage);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(errorMessage, 0);
                afterbtnUpdate();
                return;
            }

            if (!chkShowPallet.Checked)
            {
                showAssginLineList();
            }
            else
            {
                showAssginLineList2();
            }
            afterbtnUpdate();
        }
        private void afterbtnUpdate()
        {
            txtLine.Text = "";
            txtSID.Text = "";
            txtNewSID.SelectAll();
            txtNewSID.Focus();
            btnUpdate.Enabled = true;
        }

        private void chkShowPallet_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkShowPallet.Checked)
            {
                showAssginLineList();
            }
            else
            {
                showAssginLineList2();
            }
        }
    }
}
