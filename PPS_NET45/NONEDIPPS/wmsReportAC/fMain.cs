using CommonControl;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using wmsReportAC;

namespace wmsReportAC
{
    public partial class fMain : Form
    {

        /// <summary>
        /// 系统变量定义
        /// </summary>
        //读取当前库存信息，保存到系统表
        DataTable DtWMSList = new DataTable();
        DataTable dbStockSummary;        //库存报表，Summary Report表结构
        DataTable dbStockDetail;         //库存报表，Detail Report表结构
        
        //DataTable dbInventorySummary;    //盘点报表，Summary Report表结构
        //DataTable dbInventoryDetail;     //盘点报表，Detail Report表结构
        DataTable dbIventory;

        DataTable dbPpart;
        public fMain()
        {
            InitializeComponent();
        }
        private void fMain_Load(object sender, EventArgs e)
        {

            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.WindowState = FormWindowState.Maximized;

            //填充仓库信息
            string strSql = "select warehouse_id id,warehouse_No name from SAJET.WMS_WAREHOUSE where enabled = 'Y' ORDER BY WAREHOUSE_NO";
            fillCmb(strSql, "warehouse_No", cmbWHID);

            //填充储位信息
            //strSql = "SELECT location_id id,location_name name FROM NONEDIPPS.WMS_LOCATION WHERE enabled = 'Y' order by location_name";
            //fillCmb(strSql, "location_name", cmbLocation);
            fileViewNullStruc();
            formControl();
            string strResult = string.Empty;
            string strOutparavalue = string.Empty;
            string strResulterrmsg = string.Empty;
            WMSBLL pb = new WMSBLL();
            strResult = pb.GetDBType("DB_TYPE", out strOutparavalue, out strResulterrmsg);
            //if (strResult.Equals("OK") && strOutparavalue.Equals("TEST"))
            //{
            //    button1.Visible = true;
            //    button1.Enabled = true;

            //}
            //else
            //{
            //    button1.Visible = false;
            //    button1.Enabled = false;
            //}
        }


        /// <summary>
        /// 清空库存信息显示内容
        /// </summary>
        private void clearDgv()
        {
            this.dgvNo.DataSource = null;
        }

        /// <summary>
        /// 页面控件按条件控制显示
        /// </summary>
        public void formControl()
        {
            
            DataTable db;
            //按报表类型，控制库存报表时，开始、结束时间控件不显示
            if (cmbTYPE.Text == "库存报表")
            {
                lblStart.Visible = false;
                lblEnd.Visible = false;
                dt_start.Visible = false;
                dt_end.Visible = false;
                cmbLocation.Enabled = true;
                label4.Visible = true;
                cmbICTNO.Visible = true;
                //判断库存报表时，显示不同空表结构
                if (radSummary.Checked == true)
                { db = dbStockSummary; }
                else
                { db = dbStockDetail; }
                dgvNo.DataSource = null;

                dgvNo.DataSource = db;

                radSummary.Text = "Summary Report";
                radDetail.Text = "Detail Report";
                cmbWHID.Enabled = true;
            }
            else if (cmbTYPE.Text == "储位检查报表")
            {
                lblStart.Visible = true;
                lblEnd.Visible = true;
                dt_start.Visible = true;
                dt_end.Visible = true;
                label4.Visible = false;
                cmbICTNO.Visible = false;
                cmbLocation.Enabled = true;
                db = dbIventory;
                dgvNo.DataSource = null;

                dgvNo.DataSource = db;
                radSummary.Text = "Summary Report";
                radDetail.Text = "Detail Report";
                cmbWHID.Enabled = true;

            }
            //else if (cmbTYPE.Text == "PPart车位报表")
            //{
            //    lblStart.Visible = false;
            //    lblEnd.Visible = false;
            //    dt_start.Visible = false;
            //    dt_end.Visible = false;
            //    label4.Visible = false;
            //    cmbICTNO.Visible = false;
            //    cmbLocation.Enabled = false;
            //    db = dbPpart;
            //    dgvNo.DataSource = null;
            //    dgvNo.DataSource = db;
            //    radSummary.Text = "DN不满储位";
            //    radDetail.Text = "全部储位";
            //    cmbWHID.Enabled = false;
            //}
        }

        /// <summary>
        /// 查询业务逻辑控制
        /// </summary>
        /// <returns></returns>
        private DataTable findData()
        {

            int iPara = 0;                              //变量项次
            string strSql;                              //SQL字符串变量
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            bool isInput = false;                       //是否有输入查询条件
            DataTable dt= new DataTable();              //按查询条件，查出数据源


            string startTime = dt_start.Text;
            string endTime = dt_end.Text;

            if (cmbTYPE.Text == "库存报表")
            {
                /// <summary>
                /// HYQ：之前人写的[库存报表]的查询逻辑
                /// </summary>
                #region
                if (radSummary.Checked)
                {
                    //库存报表 - 汇总数据
                    strSql = "select part_no 料号,sum(cartonqty) 箱数 , sum(qty) 数量,sum(qhcartonqty) QHold影响的箱数, sum(qhqty) QHold数量 " +
                            " from NONEDIPPS.t_location where 1 = 1 ";
                }
                else
                {
                    //库存报表 - 明细数据
                    strSql = "select location_no  Location,pallet_no 栈板号,part_no 料号, sum(cartonqty)箱数, sum(qty) 数量, sum(qhcartonqty) QHold影响的箱数, sum(qhqty) QHold数量 " +
                             " from NONEDIPPS.t_location  where  1=1 ";
                }

                //组合输入查询条件，过滤数据源
                //仓库有输入值时，添加查询条件变量
                if (cmbWHID.Text.Trim() != "")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and warehouse_id = :warehouse";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "warehouse", cmbWHID.SelectedValue };
                    iPara = iPara + 1;
                }

                //储位有输入值时，添加查询条件变量
                if (cmbLocation.Text.Trim() != "" && cmbLocation.Text.Trim() != "-ALL-")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and location_no = :location";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "location", cmbLocation.Text.Trim() };
                    iPara = iPara + 1;
                }

                //料号有输入值时，添加查询条件变量
                if (cmbICTNO.Text.Trim() != "" && cmbICTNO.Text.Trim() != "-ALL-")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and part_no = :part";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "part", cmbICTNO.Text.Trim() };
                    iPara = iPara + 1;
                }

                //库存报表 - 汇总数据 - 查询条件组合
                if (radSummary.Checked)
                {
                    strSql += " group by part_no having sum(qty) > 0 order by part_no";
                }
                else
                {
                    strSql += " group by location_no,pallet_no, part_no having sum(qty) > 0 order by location_no,pallet_no, part_no";
                }

                //判断是否有输入查询条件，调用不同的类型
                if (isInput)
                {
                    dt = ClientUtils.ExecuteSQL(strSql, sqlparams).Tables[0];
                }
                else
                {
                    dt = ClientUtils.ExecuteSQL(strSql).Tables[0];
                }
                #endregion
            }


            if (cmbTYPE.Text == "储位检查报表")
            {
                /// <summary>
                /// HYQ：[储位检查报表]的查询逻辑
                /// </summary>
                #region

                dt = new DataTable();
                strSql = string.Format("select daycode,checktime, location_NO,pallet_no ,cartonqty,  "
                                    +" passcartonqty, errorcartonqty, result, cdt, emp_id, udt "
                                    +" from NONEDIPPS.t_location_check "
                                    +" where cdt > = to_date('{0}', 'yyyy-mm-dd hh24:mi:ss') and cdt < = to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') "
                                    , startTime, endTime);

                //组合输入查询条件，过滤数据源
                //仓库有输入值时，添加查询条件变量
                if (cmbWHID.Text.Trim() != "")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and location_name in (select location_name  from NONEDIPPS.t_location where warehouse_id = :warehouse " + ")";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "warehouse", cmbWHID.SelectedValue };
                    iPara = iPara + 1;
                }

                //储位有输入值时，添加查询条件变量
                if ((cmbLocation.Text.Trim() != "") && (cmbLocation.Text.Trim()!="-ALL-"))
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and location_name = :location";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "location", cmbLocation.Text.Trim() };
                    iPara = iPara + 1;
                }

                //料号有输入值时，添加查询条件变量
                if ((cmbICTNO.Text.Trim() != "")  && cmbICTNO.Text.Trim() != "-ALL-")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and part_no = :part";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "part", cmbICTNO.Text.Trim() };
                    iPara = iPara + 1;
                }

                strSql += " order by cdt desc ";


                //判断是否有输入查询条件，调用不同的类型
                if (isInput)
                {
                    dt = ClientUtils.ExecuteSQL(strSql, sqlparams).Tables[0];
                }
                else
                {
                    dt = ClientUtils.ExecuteSQL(strSql).Tables[0];
                }




                #endregion
            }
            //if (cmbTYPE.Text == "PPart车位报表")
            //{
            //    /// <summary>
            //    /// HYQ：[PPart车位报表]的查询逻辑
            //    /// </summary>
            //    #region

            //    dt = new DataTable();
            //    if (radSummary.Checked)
            //    {
            //        strSql = string.Format(@" select rl.*, NONEDIPPS.t_get_dn_trollery(delivery_no) location
            //                                   from (select delivery_no,
            //                                                (select sum(c.pcs_qty)
            //                                                   from NONEDIOMS.oms_ppart_dn_info c
            //                                                  where c.dn_no = a.delivery_no) as TotalQty,
            //                                                (select count(*)
            //                                                   from sajet.g_ppart_laser_detail b
            //                                                  where b.dn_no = a.delivery_no) as LaserQty,
            //                                                (select count(*)
            //                                                   from NONEDIPPS.T_TROLLEY_SN_STATUS d
            //                                                  where d.DELIVERY_NO = a.delivery_no) as StockinQty
            //                                           from (select DISTINCT a.delivery_no
            //                                                   from NONEDIPPS.T_TROLLEY_SN_STATUS A
            //                                                   LEFT JOIN NONEDIPPS.T_ORDER_INFO B
            //                                                     ON A.DELIVERY_NO = B.DELIVERY_NO
            //                                                  WHERE A.trolley_no <> 'ICT-00-00-000'
            //                                                    and NVL(B.DELIVERY_NO, 'NA') = 'NA') a) rl
            //                                  where rl.totalqty > stockinqty");
            //    }
            //    else
            //    {
            //        strSql = string.Format(@" select delivery_no,
            //                                    (select sum(c.pcs_qty)
            //                                       from NONEDIOMS.oms_ppart_dn_info c
            //                                      where c.dn_no = a.delivery_no) as totalqty,
            //                                    (select count(*)
            //                                       from sajet.g_ppart_laser_detail b
            //                                      where b.dn_no = a.delivery_no) as laserqty,
            //                                    (select count(*)
            //                                       from NONEDIPPS.t_trolley_sn_status d
            //                                      where d.delivery_no = a.delivery_no) as stockinqty,
            //                                    NONEDIPPS.t_get_dn_trollery(delivery_no) location
            //                               from (select distinct delivery_no
            //                                       from NONEDIPPS.t_trolley_sn_status
            //                                      where trolley_no <> 'ICT-00-00-000') a");
            //        //strSql = string.Format(@" select aaa.group_code,
            //        //                   aaa.delivery_no,
            //        //                   aaa.needqty,
            //        //                   aaa.shipment_id,
            //        //                   aaa.stockinqty,
            //        //                   aaa.trollerybin,
            //        //                   decode(remark, 0, '正常', 1， 'DN分离', 2, '一车多SID', remark) remark
            //        //              from (select aa.group_code,
            //        //                           aa.delivery_no,
            //        //                           (select sum(qty)
            //        //                              from NONEDIPPS.t_order_info c
            //        //                             where c.delivery_no = aa.delivery_no
            //        //                               and c.shipment_id not in
            //        //                                   (select f.shipment_id from NONEDIPPS.t_shipment_sawb f)
            //        //                               and c.person_flag = 'Y') as needqty,
            //        //                           (select d.shipment_id
            //        //                              from NONEDIPPS.t_order_info d
            //        //                             where d.delivery_no = aa.delivery_no
            //        //                               and d.shipment_id not in
            //        //                                   (select e.shipment_id from NONEDIPPS.t_shipment_sawb e)
            //        //                               and rownum <= 1) as shipment_id,
            //        //                           count(*) as stockinqty,
            //        //                           NONEDIPPS.t_get_dn_trollery(aa.delivery_no) as trollerybin,
            //        //                           NONEDIPPS.t_get_dn_trollery_remark(aa.delivery_no) as remark
            //        //                      from NONEDIPPS.t_trolley_sn_status aa
            //        //                     where aa.trolley_no <> 'ICT-00-00-000'
            //        //                      /* and aa.cdt > =
            //        //                           to_date('2019-07-01 00:00:00', 'yyyy-mm-dd hh24:mi:ss')
            //        //                       and aa.cdt < =
            //        //                           to_date('2019-07-03 00:00:00', 'yyyy-mm-dd hh24:mi:ss')*/
            //        //                     group by aa.group_code, aa.delivery_no) aaa
            //        //              order by aaa.delivery_no");

            //                }
            //        dt = ClientUtils.ExecuteSQL(strSql).Tables[0];
                
            //    #endregion
            //}

            if ((dt.Rows.Count > 0))
            {
                this.ShowMsg("查询成功", 5);
                Ok();
            }
            else
            {
                if (cmbTYPE.Text == "库存报表")
                {
                    this.ShowMsg("该仓库在查询条件下无资料，请确认", 0);
                }
                else
                {
                    this.ShowMsg("该查询条件下无资料，请确认", 0);
                }
                Ng();
            }
            return dt;
        }

        /// <summary>
        /// 库存信息查询
        /// 查询系统表：T_SN_STATUS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;
                dgvNo.DataSource = null;
                dgvNo.DataSource = findData();
                lblstrFile.Text = "wmsReport" + cmbTYPE.Text.Trim() + "_" + cmbLocation.Text.Trim();
                btnSearch.Enabled = true;
            }
            catch (Exception ex)
            {
                this.ShowMsg("未获取到数据" + ex.Message, 1);
                Ng();
            }

        }


        private void cmbTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            formControl();
        }

        private void radSummary_CheckedChanged(object sender, EventArgs e)
        {
           // formControl();
        }

        private void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            if (dts.Tables[0].Rows.Count > 0)
            {

                //return DtbPickList;
                //绑定单据号
                //List<string> shipmentList = (from d in dts.Tables[0].AsEnumerable()
                //                             select d.Field<string>(colName)
                //                           ).Distinct()
                //                           .ToList();
                //shipmentList.Add("");
                //shipmentList.Sort();
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";
            }
            else
            {
                cmb.Items.Clear();
            }
        }

        /// <summary>
        /// 填充DATAgridview空表时，显示结构
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileViewNullStruc()
        {

            string strSQL1 = "select part_no 料号,cartonqty 箱数 , qty 数量,qhcartonqty QHold影响的箱数, qhqty QHold数量 from NONEDIPPS.t_location WHERE 1 = 0";
            //库存报表,Summary Report报表空结构
            dbStockSummary = ClientUtils.ExecuteSQL(strSQL1).Tables[0];

            //库存报表，Detail Report表结构
            string strSQL2 = "select location_no Location,pallet_no 栈板号,part_no 料号, cartonqty 箱数, qty 数量, qhcartonqty QHold影响的箱数, qhqty QHold数量 from NONEDIPPS.t_location WHERE 1 = 0";
            dbStockDetail = ClientUtils.ExecuteSQL(strSQL2).Tables[0];

            string strSql3 = "select daycode,checktime, location_name,pallet_no ,cartonqty, passcartonqty, errorcartonqty, result, cdt, emp_id, udt from NONEDIPPS.t_location_check where 1=0 ";
            dbIventory = ClientUtils.ExecuteSQL(strSql3).Tables[0];
            
            string strSql4 = "  select null delivery_no,null totalqty,null laserqty,null stockinqty ,null location from dual where 1 = 0 ";
            dbPpart = ClientUtils.ExecuteSQL(strSql4).Tables[0];



            //盘点报表，Summary Report表结构
            //DataTable dbInventorySummary;
            //盘点报表，Detail Report表结构   
            //DataTable dbInventoryDetail;     
        }

       

        private void btnExport_Click(object sender, EventArgs e)
        {
            //导出Excel文件
            try
            {
                if (dgvNo.Rows.Count > 0)
                {
                    ExportExcel(dgvNo);
                }
                else
                {
                    this.ShowMsg("确认导出Excel有数据！", 0);
                    Ng();
                }
            }
            catch (Exception ex)
            {
                Ng();
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
            
            sfd.FileName =lblstrFile.Text + "_" + currdate;
            sfd.DefaultExt = "xlsx";//设置默认格式（可以不设）
            sfd.AddExtension = true;//设置自动在文件名中添加扩展名
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filePath = sfd.FileName;
            }
            else
            {
                this.ShowMsg("导出Excel失败！", 0);
                Ng();
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
                Ok();
            }
        }

        private void dgvNo_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //for (int i = 0; i < dgvNo.Rows.Count; i++)
            //{
            //    this.dgvNo.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            //    if (cmbTYPE.Text == "PPart车位报表")
            //    {
            //        //0'正常', 1， 'DN分离', 2, '一车多SID',
            //        if (dgvNo.Rows[i].Cells["remark"].Value.ToString().Equals("一车多SID"))
            //        {
            //            this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            //        }
            //        else if (dgvNo.Rows[i].Cells["remark"].Value.ToString().Equals("DN分离"))
            //        {
            //            this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
            //        }
            //        else
            //        {
            //            this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //        }
            //    }
            //}
        }
        

        private void cmbWHID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWHID.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
                return;

            //填充储位信息
            //string strSql = "SELECT location_id id,location_name name FROM NONEDIPPS.WMS_LOCATION WHERE enabled = 'Y' AND warehouse_id = " + cmbWHID.SelectedValue + " order by location_name";
            //string strSql = string.Format("select to_number('10000000') as ID, '-ALL-' as NAME from dual "
            //                              + " union "
            //                              + " SELECT location_id id, location_name name "
            //                              + "   FROM NONEDIPPS.WMS_LOCATION "
            //                              + "   WHERE enabled = 'Y' "
            //                              + "   AND warehouse_id = '{0}'"
            //                              + "   and location_name is not null"
            //                              + "   order by name", cmbWHID.SelectedValue);
          
            string strSql = string.Format("select to_number('10000000') as ID, '-ALL-' as NAME from dual "
                                       + " union "
                                       + " select  id,  name from ( "
                                       + " SELECT distinct location_id id, location_NO name "
                                       + "     FROM NONEDIPPS.WMS_LOCATION "
                                       + "    WHERE LOCATION_NO IN "
                                       + "          (SELECT location_NO FROM NONEDIPPS.t_location where qty > 0) "
                                       + "      and enabled = 'Y' "
                                       + "      AND warehouse_id = '{0}'"
                                       + "  ) order by name ", cmbWHID.SelectedValue);

            fillCmb(strSql, "location_name", cmbLocation);

            string strSql2 = string.Format("select to_number('0') as ID, '-ALL-' as NAME "
                                         + "       from dual "
                                         + "     union "
                                         + "     select rownum as ID, part_no as NAME from( "
                                         + "     select  distinct  part_no "
                                         + "       from NONEDIPPS.t_location "
                                         + "      where warehouse_id = '{0}' and qty > 0 "
                                         + "      order by part_no)", cmbWHID.SelectedValue);

            fillCmb(strSql2, "part_no", cmbICTNO);
            
        }

        private void dgvNo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dgvNo.DataSource = null;
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            Transfer transfer = new Transfer();
            transfer.Show();
        }

        private void reportS_BTN_Click(object sender, EventArgs e)
        {
            ReportQuery rq = new ReportQuery();
            rq.Show();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            fWMSQuery fq = new fWMSQuery();
            fq.ShowDialog();
        }

        private void ZF_BTN_Click(object sender, EventArgs e)
        {
            //ZF_CheckOut zfCheckOut = new ZF_CheckOut();
            //zfCheckOut.ShowDialog();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            fWMSCheck fc = new fWMSCheck();
            fc.ShowDialog();
        }

        private void btnPpartCheck_Click(object sender, EventArgs e)
        {
            //fWMSPpartCheck fc = new fWMSPpartCheck();
            //fc.ShowDialog();

        }

        private void btnPpartTrans_Click(object sender, EventArgs e)
        {
            //fPpartTransfer fpt = new fPpartTransfer();
            //fpt.ShowDialog();
        }


        /// <summary>
        /// 执行成功后，调用OK声音文件
        /// </summary>
        public void Ok()
        {
            LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
        }

        /// <summary>
        /// 执行失败后，调用Ng声音文件
        /// </summary>
        public void Ng()
        {
            LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
        }


        /// <summary>
        /// 程式执行信息显示
        /// </summary>
        /// <param name="strTxt"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public DialogResult ShowMsg(string strTxt, int strType)
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
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }

        /// <summary>
        /// LabelCheck  Add By KyLinQiu 20190624
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            //VerificationDN.fMain frm = new VerificationDN.fMain();
            //frm.Show();
        }

        /// <summary>
        /// 检查Tran.in File  Add By KyLinQiu 20190625
        /// </summary>
        private void btnCheckTranFile_Click(object sender, EventArgs e)
        {
            //VerificationTranInFile.fMain frm = new VerificationTranInFile.fMain();
            //frm.Show();
        }

        private void createDnPdf_BTN_Click(object sender, EventArgs e)
        {
            //CreateDeliveryNoteForm cDNform = new CreateDeliveryNoteForm();
            //cDNform.ShowDialog();
        }

        private void Fast_search_BTN_Click(object sender, EventArgs e)
        {
            //FastSearchForm fastSearchF = new FastSearchForm();
            //fastSearchF.ShowDialog();
        }

        /// <summary>
        /// PPS出货
        /// </summary>
        private void btnPPSOUT_Click(object sender, EventArgs e)
        {
            FrmPPSOUT frm = new FrmPPSOUT();
            frm.ShowDialog();
        }

        private void btnTrolleyMove_Click(object sender, EventArgs e)
        {
            //formTrolleyMove ftm = new formTrolleyMove();
            //ftm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            FrmZCCheck frm = new FrmZCCheck();
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                ZCshipmentid zcs = new ZCshipmentid();
                zcs.ShowDialog();
            }
            else
            {
                ShowMsg("验证密码错误!", 0);
            }
        }

        private void btnCancelshipment_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            FrmZCCheck frm = new FrmZCCheck();
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                CancelSID zcs = new CancelSID();
                zcs.ShowDialog();
            }
            else
            {
                ShowMsg("验证密码错误!", 0);
            }
        }
    }
}
