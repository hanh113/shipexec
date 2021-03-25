using ClientUtilsDll;
using ClientUtilsDll.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIP.Core;

namespace WIP
{
    public partial class fMain : PPSForm
    {
        private corebridge core;
        private ExecutionResult exeRes;
        public fMain()
        {
            core = new corebridge();
            exeRes = new ExecutionResult();
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            exeRes = core.GetAllMoInfo(dtpStart.Value, dtpEnd.Value);
            dgvMo.DataSource = exeRes.Status ? (DataTable)exeRes.Anything : null;
            ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
        }

        private void dgvMo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMo.CurrentRow.Index >= 0)
            {
                exeRes = core.GetBomByMo(dgvMo.Rows[dgvMo.CurrentRow.Index].Cells["MO_NO"].Value.ToString());
                dgvBom.DataSource = exeRes.Status ? (DataTable)exeRes.Anything : null;
                ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
            }
        }

        private void tbScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            lbQty.Text = "0";
            exeRes = core.GetSnInfo(tbScan.Text.Trim().ToUpper());
            dgvSN.DataSource = exeRes.Status ? (DataTable)exeRes.Anything : null;
            ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
            lbQty.Text = exeRes.Status ? ((DataTable)exeRes.Anything).Rows.Count.ToString() : "0";
        }

        private void btnSNExl_Click(object sender, EventArgs e)
        {
            if (dgvSN.Rows.Count > 0)
            {
                try
                {
                    TableToExcel((DataTable)dgvSN.DataSource, "SN_INFO");
                    ShowMSG("Save Excel Success.", 3);
                }
                catch (Exception ex)
                {
                    ShowMSG(ex.Message, 0);
                }
            }
        }

        /// <summary>
        /// npoi导入excel
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="sheetname">分页</param>
        /// <param name="file">文件路径</param>
        public void TableToExcel(System.Data.DataTable dt, string sheetname, string file = "")
        {
            IWorkbook workbook;
            ISheet sheet;
            System.Data.DataTable tempdt;
            string fileExt = string.Empty;
            if (string.IsNullOrEmpty(file))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "保存为Excel文件";
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName.IndexOf(":") < 0)
                {
                    return; //被点了"取消"
                }
                fileExt = Path.GetExtension(saveFileDialog.FileName).ToLower();
                file = saveFileDialog.FileName;
            }
            else
            {
                fileExt = Path.GetExtension(file).ToLower();
            }
            int SheetCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dt.Rows.Count) / 60000));

            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
            if (workbook == null) { return; }
            // ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);
            //限制每张Sheet中只插入6W笔数据 上限为65536笔
            int rowc = SheetCount == 1 ? dt.Rows.Count : 60000;
            for (int tt = 1; tt <= SheetCount; tt++)
            {
                sheet = workbook.CreateSheet(sheetname + "_" + tt);
                tempdt = new System.Data.DataTable();
                tempdt = dt.AsEnumerable().Skip((tt - 1) * rowc).Take(rowc).CopyToDataTable();
                //表头  
                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < tempdt.Columns.Count; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(tempdt.Columns[i].ColumnName);
                }
                //数据  
                for (int i = 0; i < tempdt.Rows.Count; i++)
                {
                    IRow row1 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < tempdt.Columns.Count; j++)
                    {
                        ICell cell = row1.CreateCell(j);
                        cell.SetCellValue(tempdt.Rows[i][j].ToString());
                    }
                }
            }


            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }

        }

        private void btnQueryAll_Click(object sender, EventArgs e)
        {
            lbQty.Text = "0";
            exeRes = core.GetSnInfo("STOCK_OUT");
            dgvSN.DataSource = exeRes.Status ? (DataTable)exeRes.Anything : null;
            ShowMSG(exeRes.Message, exeRes.Status ? 3 : 0);
            lbQty.Text = exeRes.Status ? ((DataTable)exeRes.Anything).Rows.Count.ToString() : "0";
        }
    }
}
