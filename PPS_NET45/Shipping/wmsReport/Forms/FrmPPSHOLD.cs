using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace wmsReport
{
    public partial class FrmPPSHOLD : Form
    {
        public FrmPPSHOLD()
        {
            InitializeComponent();
            ClientUtils.runBackground((Form)this);
        }

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
                    MessageBox.Show("确认导出Excel有数据！");
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
                MessageBox.Show("导出Excel失败！");
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
                MessageBox.Show("导出Excel成功！");
            }
        }

        private void FrmPPSHOLD_Load(object sender, EventArgs e)
        {
            string strSQL2 = string.Format(@"
SELECT DISTINCT a.LOCATION_ID,a.LOCATION_NO,a.PALLET_NO,a.CARTON_NO,a.CUSTOMER_SN,a.PART_NO,a.WC,b.WARRANTY
FROM ppsuser.T_SN_STATUS a INNER JOIN sajet.G_SN_STATUS b ON to_char(a.SERIAL_NUMBER)=to_char(b.SERIAL_NUMBER)
WHERE a.WC<>'W5' AND b.HOLD_FLAG='Y'
ORDER BY a.LOCATION_ID,a.LOCATION_NO,a.PALLET_NO,a.CARTON_NO,a.CUSTOMER_SN
");
            DataTable dt2 = new DataTable();

            try
            {
                dt2 = ClientUtils.ExecuteSQL(strSQL2).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (dt2.Rows.Count == 0)
            {
                MessageBox.Show("查无资料");
                return;
            }
            else
            {
                this.dgvDetail.DataSource = dt2;
            }
        }
    }
}
