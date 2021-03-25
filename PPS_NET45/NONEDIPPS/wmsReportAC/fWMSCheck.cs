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

namespace wmsReportAC
{
    public partial class fWMSCheck : Form
    {
        public fWMSCheck()
        {
            InitializeComponent();
        }

        private string g_sUserID = ClientUtils.UserPara1;
        private string l_Location_ID = "";     //保存查询时Loc id
        private string l_location_NO = "";   //保存location_no 为后面查询条件
        private string l_WHID = "";            //保存查询时的WHID
        private DataTable l_T_SN_STATUS = new DataTable();       //保存按储位名称location_no取的全部记录
        private DataTable l_d1, l_d2, l_d3;

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
        private void gridInit()
        {
            dgvFindResult.DataSource = l_d1;
            dgvCheckSum.DataSource = l_d2;
            //dgvDetail.DataSource = l_d3;
        }

        private void fWMSCheck_Load(object sender, EventArgs e)
        {
            //填充仓库信息
            string strSql = "select warehouse_id id,warehouse_No name from NONEDIPPS.WMS_WAREHOUSE where enabled = 'Y' ORDER BY WAREHOUSE_NO";
            WMSBLL wb = new WMSBLL();
            wb.fillCmb(strSql, "warehouse_No", cmbWHID);

            //初始化datagridView数据源
            l_d1 = searchShow();
            l_d2 = findCheck("00000000000");
            //l_d3 = get_T_SN_STATUS("00000000000");

            gridInit();

        }

        private void cmbWHID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWHID.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
                return;
            
            //填充储位信息
            // string strSql = "SELECT location_id id,location_no name FROM NONEDIPPS.WMS_LOCATION WHERE enabled = 'Y' AND warehouse_id = " + cmbWHID.SelectedValue + " order by location_no";

            string strSql = string .Format("SELECT location_id id, location_no name "
                             + "     FROM NONEDIPPS.WMS_LOCATION "
                             + "    WHERE LOCATION_NO IN "
                             + "          (SELECT location_no FROM NONEDIPPS.t_location where qty > 0) "
                             + "      and enabled = 'Y' "
                             + "      AND warehouse_id = '{0}'"  
                             + "    order by location_no", cmbWHID.SelectedValue);
            WMSBLL wb =new WMSBLL();
            wb.fillCmb(strSql, "location_no", cmbLocation);

            TextMsg.Text = "";
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
        /// 查询业务逻辑控制
        /// </summary>
        /// <returns></returns>
        private DataTable findData()
        {
            int iPara = 0;                              //变量项次
            string strSql;                              //SQL字符串变量
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            bool isInput = false;                       //是否有输入查询条件
            DataTable dt = new DataTable();              //按查询条件，查出数据源

            strSql = "SELECT Part_NO 料号,Pack_Code 包规,Pallet_NO,CartonQTY 箱数," +
                     "qty 数量, QHCARTONQTY QHold箱数,QHQTY QHold数量 FROM NONEDIPPS.T_LOCATION where 1 = 1";
                //组合输入查询条件，过滤数据源
                //仓库有输入值时，添加查询条件变量
                if (cmbWHID.Text.Trim() != "")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and warehouse_id = :warehouse";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input,OracleDbType.Varchar2, "warehouse", cmbWHID.SelectedValue };
                    iPara = iPara + 1;
                }

                //储位有输入值时，添加查询条件变量
                if (cmbLocation.Text.Trim() != "")
                {
                    isInput = true;
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    strSql += " and location_no = :location";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input,OracleDbType.Varchar2, "location", cmbLocation.Text.Trim() };
                    iPara = iPara + 1;
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

            if ((dt.Rows.Count > 0))
            {
                this.ShowMsg("查询条件下，资料获取成功!", 5);
                Ok();
            }
            else
            {
                this.ShowMsg("查询条件下，资料获取不到资料，请确认!", 0);
                Ng();
            }
            return dt;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dgvFindResult.Columns.Clear();
                dgvFindResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvFindResult.DataSource = findData();

                l_Location_ID = cmbLocation.SelectedValue.ToString();
                l_location_NO = cmbLocation.Text.Trim();
                l_WHID = cmbWHID.SelectedValue.ToString();

                if (dgvFindResult.Rows.Count > 0)
                {
                    //查询到数据，开启检验按纽功能
                    butStart.Enabled = true;
                    butEnd.Enabled = false;
                    txtSN.ReadOnly = true;
                    btnSearch.Enabled = true;
                }
                else
                {
                    //查询没有记录时，禁用开始作业功能
                    butStart.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMsg("未获取到数据" + ex.Message, 1);
                Ng();
            }
        }

        private void dgvFindResult_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvFindResult.Rows.Count; i++)
            {
                this.dgvFindResult.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            }
        }

        private void insertCheck()
        {
            string strSQL;

            string l_checkTime, l_location_id, l_location_no, l_pallet_no, l_cartonqty;

            l_checkTime = get_CheckTime(l_location_NO);
            l_location_id = l_Location_ID;
            l_location_no = l_location_NO;
            l_pallet_no = dgvFindResult.Rows[0].Cells["PALLET_NO"].Value.ToString();
            l_cartonqty = get_cartonQTYsum();


            //HYQ: 之前没有检查 上一笔记录是否是异常退出， pass and errorqty 是0 的情况。
            if (l_checkTime.Contains("NEW"))
            {
                l_checkTime = l_checkTime.Replace("NEW", "").Trim();
                strSQL = "Insert into NONEDIPPS.T_LOCATION_Check(daycode, checktime, location_id, location_no, pallet_no," +
                         "cartonqty, passcartonqty, errorcartonqty, result, cdt, emp_id) " +
                         "values(to_char(sysdate, 'yyyy-mm-dd'), " + l_checkTime + "," + l_location_id + ",'" + l_location_no.Replace("'", "''") +
                         "','" + l_pallet_no.Replace("'", "''") + "'," + l_cartonqty + ",0,0,'IN PROCESS PASS',sysdate," + g_sUserID + ")";
                DataSet sDataSet = ClientUtils.ExecuteSQL(strSQL);
            }
            else if (l_checkTime.Contains("OLD"))
            {
                l_checkTime = l_checkTime.Replace("OLD", "").Trim();
            }
            else
            {
                ShowMsg("获取库位最大检查次数异常",0);
                return;
            }
            lalFirst.Text = "N";

        }

        private string get_cartonQTYsum()
        {
            //汇总查询结果中箱数显示
            int iRows = dgvFindResult.Rows.Count;
            int iQTY = 0;

            for (int i = 0; i < iRows; i++)
            {
                iQTY += Convert.ToInt16(dgvFindResult.Rows[i].Cells["箱数"].Value);
            }
            return iQTY.ToString();
        }

        private DataTable searchShow()
        {
            string strSql = "SELECT Part_NO 料号,Pack_Code 包规,Pallet_NO,CartonQTY 箱数," +
                     "qty 数量, QHCARTONQTY QHold箱数,QHQTY QHold数量 FROM NONEDIPPS.T_LOCATION where 1 = 0";
            return ClientUtils.ExecuteSQL(strSql).Tables[0];

        }

        private DataTable findCheck(string locationNo)
        {
            string strSQL = "SELECT DAYCODE 日期,Checktime 检查次数, CARTONQTY 箱数,PASSCARTONQTY 匹配箱数, errorcartonqty ERROR箱数,RESULT 结果 " +
                            " FROM NONEDIPPS.T_LOCATION_Check " +
                            " where daycode = to_char(sysdate, 'yyyy-mm-dd') and location_no = '" + locationNo.Replace("'","''")+"'";
                   strSQL += " order by checktime desc";
             return ClientUtils.ExecuteSQL(strSQL).Tables[0];

        }

        private string get_CheckTime(string locationNo)
        {
            string strSQL = string.Empty;
            //按日期与当天检查次数，递增检查次数  //之前人写的。
            //strSQL = "select nvl(max(checktime) + 1, 1) from NONEDIPPS.T_LOCATION_Check " +
            //                "where daycode = to_char(sysdate, 'yyyy-mm-dd') and location_no = '" + locationNo.Replace("'","''")+"'";
            
            //HYQ: 更新下如果上次check是异常退出的且检查数量是0 0 ， checktime 还是保持原来的最大值。

            strSQL = string.Format("select case when passcartonqty=0 and errorcartonqty=0  then checktime "
                                  + "    else checktime + 1 end as checktime2, "
                                  + "    case when passcartonqty = 0 and errorcartonqty = 0  then 'OLD' "
                                  + "    else 'NEW' end as checktimedesc "
                                  + "    from NONEDIPPS.T_LOCATION_Check "
                                  + "   where checktime in (select max(checktime) checktime "
                                  + "                         from NONEDIPPS.T_LOCATION_Check "
                                  + "                        where daycode = to_char(sysdate, 'yyyy-mm-dd') "
                                  + "                          and location_no = '{0}') "
                                  + "     and daycode = to_char(sysdate, 'yyyy-mm-dd') "
                                  + "     and location_no = '{1}'", locationNo, locationNo);
            if (ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows.Count > 0)
            {
                return ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows[0]["checktime2"].ToString() + ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows[0]["checktimedesc"].ToString();
            }
            else
            {
                return "0NEW";
            }
        
        }

        /// <summary>
        /// 按储位名称，取出全部明细记录
        /// </summary>
        private DataTable get_T_SN_STATUS(string location_no)
        {
            string strSQL;
            //HYQ: 加了一个限制， WC=W0
            strSQL = "SELECT CUSTOMER_SN, Carton_NO, PART_NO, LOCATION_NO, PALLET_NO,SERIAL_NUMBER FROM NONEDIPPS.T_SN_STATUS " +
                     " WHERE WC='W0' and  Location_NO = '" + location_no.Replace("'", "''") + "'";
            return ClientUtils.ExecuteSQL(strSQL).Tables[0];
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            //HYQ： 补了前人写的一个BUG //insertcheck() 转到txtsn_keydown里面了
            //insertCheck();

            //开始按纽开始
            dgvCheckSum.DataSource  = findCheck(l_location_NO);

            //按储位名称，取出全部location_no
            //HYQ: 加了一个限制， WC=W0
            l_T_SN_STATUS = get_T_SN_STATUS(l_location_NO);


            lalFirst.Text = "Y";

            butStart.Enabled = false;
            butEnd.Enabled = true;
            txtSN.ReadOnly = false;


        }

        private void dgvCheckSum_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < dgvCheckSum.Rows.Count; i++)
            {
                this.dgvCheckSum.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            }
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            string strSN = txtSN.Text.Trim();
            TextMsg.Text = "";

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strSN))
            {
                this.ShowMsg("输入的 " + lblSN.Text + " 不能为空！", 0);
                Ng();
                txtSN.SelectAll();
                txtSN.Focus();
                return;
            }
            WMSBLL plb = new WMSBLL();
            strSN = plb.DelPrefixCartonSN(strSN);

            //HYQ： 加一个判断，如果输入的序号不是ICT的 customerSN 或者cartonSN  直接return 不用计数 ，检查站别是否为W0
            #region
            string strSQL = string.Format("select customer_sn ,carton_no,wc "
                                          +"    from NONEDIPPS.t_sn_status "
                                          + "   where customer_sn = '{0}' "
                                          + "      or carton_no = '{1}'", strSN, strSN);

            DataTable dt = new DataTable();

            try
            {
                dt = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            }
            catch  (Exception ex )
            {
                ShowMsg(ex.ToString(), 0);
            }

            if (dt.Rows.Count == 0)
            {
                ShowMsg("输入非法无效的序号或者箱号，不做统计。",0);
                txtSN.Text="";
                txtSN.Focus();
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //string snwc = ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows[i]["wc"].ToString().Trim();
                string snwc = dt.Rows[i]["wc"].ToString().Trim();
                if (!snwc.Equals("W0"))
                {
                    ShowMsg("输入的序号或者箱号，站别异常，不应该在库位，不做统计。", 0);
                    txtSN.Text = "";
                    txtSN.Focus();
                    return;
                }
            }

            //转成箱号处理
            //strSN = ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows[0]["carton_no"].ToString();
            strSN = dt.Rows[0]["carton_no"].ToString();

            #endregion

            // HYQ：20181105
            //添加QHold 检查
            //ReverseBll.CheckHold(string Sno, string Type, out string errorMessage)
            //Type有: 'SHIPMENT', 'PICKPALLETNO', 'PACKPALLETNO', 'SERIALNUMBER'

            string errorMessage = "";
            bool CheckHoldOK = ReverseAC.ReverseBll.CheckHold(strSN, "SERIALNUMBER", out errorMessage);
            if (!CheckHoldOK)
            {
                ShowMsg(errorMessage, 0);
                txtSN.Text = "";
                txtSN.Focus();
                return;
            }


            if (lalFirst.Text.Contains("Y"))
            {
                //HYQ:确保一定会序号是对的情况下，（确保一定会是PASS 还是ERROR ）一定会写T_LOCATION_CHECK,才做下面一步。
                dgvCheckSum.DataSource = null;
                //HYQ： 补了前人写的一个BUG
                insertCheck();
            }

            //开始按纽开始
            dgvCheckSum.DataSource = findCheck(l_location_NO);

            var dr = (from d in l_T_SN_STATUS.AsEnumerable()
                     .Where(p => p.Field<string>("CUSTOMER_SN") == strSN
                     || p.Field<string>("CARTON_NO") == strSN
                     )
                     select d);

            System.Data.EnumerableRowCollection drs = (System.Data.EnumerableRowCollection)dr;

            bool isFind = false;
            string errorValue = "Customer SN / Carton ID 查询数据不存在，请确认!";
            //扫描Customer SN时，只添加一行，扫描Carton NO时，添加箱中全部数据显示
            int isCheckResult =0;
            foreach (DataRow de in drs)
            {
                isCheckResult =  snCheck(de);
                if (isCheckResult == 0)
                    upType();               //更新箱数汇总数量

                

                if (isCheckResult == 2)
                {
                    errorValue = "Customer SN / Carton ID 数据已扫描，请确认!";
                }
                else
                {
                    if (isCheckResult == 1)
                        errorValue = "Customer SN数据未扫描，Carton ID 数据已扫描，请确认!";
                    object[] ds = de.ItemArray;
                    dgvDetail.Rows.Insert(0, ds);
                    dgvDetail.CurrentCell = dgvDetail.Rows[0].Cells[0];
                }
                isFind = true;
            }

            if (isFind)
            {
                if (isCheckResult == 0 || isCheckResult == 1)
                    ShowMsg("匹配查询，资料获取成功!", 5);
                else
                    ShowMsg(errorValue, 5);
            }
            else
            {
                if (errorDisp(strSN) != -1)
                    errorValue = "Customer SN / Carton ID 不属于此储位，请确认!";       
                ShowMsg(errorValue, 0);     //显示错误信息
                upErrorQty();               //更新扫描错误汇总数据 K1350
            }

            //update 表
            saveData();
            txtSN.SelectAll();
            txtSN.Focus();

        }

        //检查错误后，需查出正确资料显示，并提示此行信息不属于此储位
        private int errorDisp(string inputValue)
        {
            string strSQL;
            strSQL = "SELECT CUSTOMER_SN, Carton_NO, PART_NO, LOCATION_NO, PALLET_NO,SERIAL_NUMBER FROM NONEDIPPS.T_SN_STATUS " +
                     " WHERE CUSTOMER_SN = '" + inputValue.Replace("'", "''") + "' or Carton_NO ='" + inputValue.Replace("'", "''") + "'";

            int retValue = -1;

            foreach (DataRow dr in ClientUtils.ExecuteSQL(strSQL).Tables[0].Rows)
            {
                //检测是否有重复
                retValue = snCheck(dr);

                //两个都重复，就返回
                if (retValue == 2)
                    continue;

                //返回值为0或1时，新增一行
                object[] ds = dr.ItemArray;
                dgvDetail.Rows.Insert(0, ds);
                dgvDetail.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                dgvDetail.Rows[0].DefaultCellStyle.Font = new Font("宋体", 10, FontStyle.Italic);
                dgvDetail.CurrentCell = dgvDetail.Rows[0].Cells[0];
            }

            return retValue;

        } 

        private void upType()
        {
            int i = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["匹配箱数"].Value);
            dgvCheckSum.Rows[0].Cells["匹配箱数"].Value = i + 1;
            dgvCheckSum.Rows[0].Cells["匹配箱数"].Style.ForeColor = Color.Blue;
            dgvCheckSum.Rows[0].Cells["匹配箱数"].Style.Font = new Font("宋体", 12,FontStyle.Bold);
        }

        private void upErrorQty()
        {
            int i = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value);
            dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value = i + 1;
            dgvCheckSum.Rows[0].Cells["ERROR箱数"].Style.ForeColor = Color.Red;
            dgvCheckSum.Rows[0].Cells["ERROR箱数"].Style.Font = new Font("宋体", 12, FontStyle.Bold);
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


        private void dgvDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rs = dgvDetail.Rows.Count;

            for (int i = 0; i < rs; i++)
            {
                this.dgvDetail.Rows[i].HeaderCell.Value = (rs-i).ToString().Trim();
            }
        }

        private void butEnd_Click(object sender, EventArgs e)
        {
            if (dgvCheckSum.Rows.Count>0)
            {
                //如果点了开始 什么都没有做，点结束的话， 删除 这笔 pass0 error0的记录

                string iPassQty = dgvCheckSum.Rows[0].Cells["匹配箱数"].Value.ToString();
                string iErrorQty = dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value.ToString();
                string sDayCode = dgvCheckSum.Rows[0].Cells["日期"].Value.ToString();
                string sCheckTime = dgvCheckSum.Rows[0].Cells["检查次数"].Value.ToString();

                //HYQ: 正式上线后没有DELETE权限。  这段注释

                if (iPassQty.Equals("0") && iErrorQty.Equals("0"))
                {
                    string strSQL = string.Format("delete from NONEDIPPS.T_LOCATION_Check "
                                                  + "   where daycode = to_char(sysdate, 'yyyy-mm-dd') "
                                                  + "     and location_no = '{0}' "
                                                  + "     and checktime = '{1}' "
                                                  + "     and passcartonqty = '0' "
                                                  + "     and errorcartonqty = '0'", cmbLocation.Text.ToString(), sCheckTime);


                    //string strSQL = "update NONEDIPPS.T_LOCATION_Check SET  passcartonqty =" + iPassQty + ",errorcartonqty = " + iErrorQty +
                    //         " ,UDT = sysdate where daycode = '" + sDayCode + "' and checktime =" + sCheckTime;
                    ClientUtils.ExecuteSQL(strSQL);
                }
            }

            lalFirst.Text = "Y";

            //
            btnSearch.Enabled = true;
            butStart.Enabled = false;
            butEnd.Enabled = false;
            txtSN.ReadOnly = true;
            txtSN.Text = "";
            dgvDetail.Rows.Clear();
            gridInit();
            btnSearch.Focus();

        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextMsg.Text = "";
        }

       
        private bool saveData()
        {
            //保存数据
            string iPassQty = dgvCheckSum.Rows[0].Cells["匹配箱数"].Value.ToString();
            string iErrorQty = dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value.ToString();
            string sDayCode = dgvCheckSum.Rows[0].Cells["日期"].Value.ToString();
            string sCheckTime = dgvCheckSum.Rows[0].Cells["检查次数"].Value.ToString();
            //HYQ:补上result的逻辑
            //i)	ErrorCartonQty = 0,CARTONQTY = PASSCARTONQTY 为PASS
            //ii)	ErrorCartonQty != 0,CARTONQTY = PASSCARTONQTY 为PASS_HASCHECKFAIL
            //iii)	ErrorCartonQty = 0,CARTONQTY > PASSCARTONQTY 为INPROCESSPASS
            //iv)	ErrorCartonQty != 0,CARTONQTY > PASSCARTONQTY 为INPROCESSFAIL

            //保存数据
            Int32 valiPassQty = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["匹配箱数"].Value);
            Int32 valiErrorQty = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["ERROR箱数"].Value.ToString());
            Int32 valiCartonQty = Convert.ToInt32(dgvCheckSum.Rows[0].Cells["箱数"].Value.ToString());
            string dgvchecksumResult = "";
            if (valiErrorQty == 0 && valiCartonQty == valiPassQty) { dgvchecksumResult = "PASS"; }
            else if (valiErrorQty != 0 && valiCartonQty == valiPassQty) { dgvchecksumResult = "PASS_HASCHECKFAIL"; }
            else if (valiErrorQty == 0 && valiCartonQty > valiPassQty) { dgvchecksumResult = "INPROCESSPASS"; }
            else if (valiErrorQty != 0 && valiCartonQty > valiPassQty) { dgvchecksumResult = "INPROCESSFAIL"; }
            else { dgvchecksumResult = "INPROCESS"; }

            dgvCheckSum.Rows[0].Cells["结果"].Value = dgvchecksumResult;


            //HYQ: 原来人写的有问题， 少了location_no 
            string strSQL = string.Format("update NONEDIPPS.T_LOCATION_Check "
                                          + "    SET passcartonqty = '{0}', errorcartonqty = '{1}',result='{5}', udt = sysdate "
                                          + "   where daycode = '{2}' "
                                          + "     and location_no = '{3}' "
                                          + "     and checktime = '{4}'", iPassQty, iErrorQty, sDayCode,cmbLocation.Text.Trim(), sCheckTime, dgvchecksumResult);

            //string strSQL = "update NONEDIPPS.T_LOCATION_Check SET  passcartonqty =" + iPassQty + ",errorcartonqty = " + iErrorQty +
            //                " ,UDT = sysdate where daycode = '" + sDayCode + "' and checktime =" + sCheckTime;
            ClientUtils.ExecuteSQL(strSQL);
            string sPallet_no, serial_number, customer_sn, carton_no, snolocation_id, snolocation_no,result;

            foreach (DataGridViewRow dr in dgvDetail.Rows)
            {
                sPallet_no = dr.Cells["palletno"].Value.ToString();
                serial_number = dr.Cells["SERIAL_NUMBER"].Value.ToString();
                customer_sn = dr.Cells["sn"].Value.ToString();
                carton_no = dr.Cells["cartonid"].Value.ToString();
                snolocation_id = "";
                snolocation_no = dr.Cells["location"].Value.ToString();

                if (l_location_NO == snolocation_no)
                    result = "OK";
                else
                    result = "NG";

                strSQL = "Insert into NONEDIPPS.t_location_check_sno_log(daycode, checktime, location_id, location_no, pallet_no," +
                                  "serial_number, customer_sn, carton_no,snolocation_id,snolocation_name,result, cdt, emp_id) " +
                                  "values('" + sDayCode + "'," + sCheckTime + "," + l_Location_ID.Replace("'", "''") + ",'" + l_location_NO.Replace("'", "''") +
                                  "','" + sPallet_no + "','" + serial_number + "','" + customer_sn + "','" + carton_no + "','" + snolocation_id +
                                  "','" + snolocation_no +"','" + result + "',sysdate," + g_sUserID + ")";
              ClientUtils.ExecuteSQL(strSQL);
            }
            return true;
        }

        //需要结束作业时，
        private bool checkSave()
        {
            int checkSum = dgvCheckSum.Rows.Count;
            int checkDetail = dgvDetail.Rows.Count;
            return true;

        }


    }
}
