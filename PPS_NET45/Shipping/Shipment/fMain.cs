using Reverse;
using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Windows.Forms;
using SajetClass;
using ClientUtilsDll;

namespace Shipment
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();

        }
        //DataTable dt = new DataTable();
        //DataTable dtResult = new DataTable();

        string strLocalMACADDRESS = string.Empty;
        #region Even/fMain_Load
        //窗体载入时初始化
        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.dtpStartTime.ValueChanged -= new System.EventHandler(this.dtpStartTime_ValueChanged);
            this.dtpEndTime.ValueChanged -= new System.EventHandler(this.dtpEndTime_ValueChanged);

            DateTime dateTimeNow = DateTime.Now;
            //dtpStartTime.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month-2, 1);
            dtpStartTime.Value = DateTime.Now.AddMonths(-1);
            //HYQ:以下两个第一次被赋值的时候，就会调用dtpStartTime_ValueChanged 和dtpEndTime_ValueChanged

            //dtpStartTime.Value = dateTimeNow;
            dtpEndTime.Value = DateTime.Now.AddDays(1);


            //关掉触发
            this.cboCarNo.SelectedIndexChanged -= new System.EventHandler(this.cboCarNo_SelectedIndexChanged);

            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);


            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);

            //开启触发
            this.cboCarNo.SelectedIndexChanged += new System.EventHandler(this.cboCarNo_SelectedIndexChanged);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            strLocalMACADDRESS = LibHelper.LocalHelper.getMacAddr_Local();
        }
        #endregion

        #region Even/txtPalletNo_KeyDown
        /// <summary>
        /// 栈板号的扫描输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            string StrPalletNo = txtPalletNo.Text.Trim();
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (string.IsNullOrEmpty(StrPalletNo))
            {
                txtMessage.Text = "";
                txtMessage.BackColor = Color.Blue;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtMessage.BackColor = Color.Blue;
                txtMessage.Text = "";

                try
                {
                    ShowMsg("", -1);
                    ShipmentBll shipmentBll = new ShipmentBll();


                    DataTable dtShipmentInfo = (DataTable)dgvShipmentInfo.DataSource;

                    if (dtShipmentInfo == null)
                    {
                        ShowMsg(cboCarNo.Text + "无装车信息!", 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.Focus();
                        return;
                    }
                    // HYQ：20181120
                    //添加QHold 检查
                    //ReverseBll.CheckHold(string Sno, string Type, out string errorMessage)
                    //Type有: 'SHIPMENT', 'PICKPALLETNO', 'PACKPALLETNO', 'SERIALNUMBER'
                    #region
                    string errorMessage = "";
                    bool CheckHoldOK = ReverseBll.CheckHold(StrPalletNo, "PACKPALLETNO", out errorMessage);
                    if (!CheckHoldOK)
                    {
                        ShowMsg(errorMessage, 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.Focus();
                        return;
                    }
                    #endregion

                    // HYQ：20190329 新增检查HOLD
                    #region
                    bool CheckHoldOK2 = shipmentBll.CheckShipmentIDHold(StrPalletNo, out errorMessage);
                    if (!CheckHoldOK2)
                    {
                        ShowMsg(errorMessage, 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.Focus();
                        return;
                    }
                    #endregion

                    #region 检查优先的栈板 by wenxing 2020-09-21
                    bool isPriorityPallet = shipmentBll.IsValidPalletLoadTruck(StrPalletNo, out errorMessage);
                    if (!isPriorityPallet)
                    {
                        ShowMsg(errorMessage, 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.Focus();
                        return;
                    }
                    #endregion


                    //扫描栈板号
                    //HYQ:检查栈板下所有序号的CSN的WC
                    //real_pallet_no , pallet_no
                    if (dgvShipmentInfoAlready.RowCount > 0)
                    {
                        for (int i = 0; i < dgvShipmentInfoAlready.RowCount; i++)
                        {
                            if (dgvShipmentInfoAlready.Rows[i].Cells["real_pallet_no"].Value.ToString().Contains(StrPalletNo))
                            {
                                ShowMsg("此序号已经刷入过", 0);
                                txtPalletNo.Text = "";
                                txtPalletNo.SelectAll();
                                txtPalletNo.Focus();
                                return;
                            }
                            if (dgvShipmentInfoAlready.Rows[i].Cells["pallet_no"].Value.ToString().Contains(StrPalletNo))
                            {
                                ShowMsg("此序号已经刷入过", 0);
                                txtPalletNo.Text = "";
                                txtPalletNo.SelectAll();
                                txtPalletNo.Focus();
                                return;
                            }
                        }
                    }

                    if (!checkPalletWC(StrPalletNo))
                    {
                        ShowMsg("栈板内CSN 序号站别存在非[W4]，异常。", 0);
                        txtPalletNo.Text = "";
                        txtPalletNo.SelectAll();
                        txtPalletNo.Focus();
                        return;
                    }


                    //HYQ:之前人写的检查逻辑
                    string strResultMessage = string.Empty;
                    int intResultRowNum = 0;

                    //
                    //HYQ： 都在这里做处理， 包括，shipment 结束 打印水晶报表 handover manifest
                    bool scanStatus = shipmentBll.ScanPalletNo(dtShipmentInfo, txtPalletNo.Text.Trim(), this.cboCarNo.Text.Trim(), out strResultMessage, out intResultRowNum, strLocalMACADDRESS);



                    if (scanStatus)
                    {
                        ShowMsg("装车OK", -1);
                        FillDataGridViewByShipment();
                    }
                    else
                    {
                        ShowMsg(strResultMessage, 0);
                    }
                    FillDataGridViewByShipment();
                    GetShipNumInfo();
                    txtPalletNo.Text = "";
                    txtPalletNo.SelectAll();
                    txtPalletNo.Focus();
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message, 0);
                }
            }
            else { }

        }
        #endregion

        #region Even/cboCarNo_SelectedIndexChanged
        private void cboCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FillDataGridViewByShipment();
            //ShowMsg("", -1);
            string carno = cboCarNo.Text.Trim();
            string startDate = dtpStartTime.Text.Trim();
            string endDate = dtpEndTime.Text.Trim();
            if (string.IsNullOrEmpty(carno))
            {
                cboShipmentId.Items.Clear();
                return;
            }
            GetCombineCarInfo(carno, startDate, endDate);
            getShipment(carno, startDate, endDate);
            BindToDayShipmentByCar(carno);
            cboShipmentId.SelectAll();
            cboShipmentId.Focus();
            ShowMsg("", -1);
        }
        #endregion
        private void clearForm(string type)
        {
            if ("1".Equals(type))
            {
                cboCarNo.Text = "";
                cboCarNo.Items.Clear();
            }

            cboShipmentId.Text = "";
            cboShipmentId.Items.Clear();
            dgvShipmentInfo.DataSource = null;
            dgvShipmentInfoAlready.DataSource = null;
            dgvShipmentByCar.DataSource = null;
        }
        //HYQ: 检查栈板下CSN的站别
        private bool checkPalletWC(string palletno)
        {
            string strSQL = @"select wc from ppsuser.t_sn_status where pack_pallet_no in (select distinct  pallet_no  from ppsuser.t_shipment_pallet where pallet_no ='" + palletno + "' or real_pallet_no='" + palletno + "' )";

            DataTable sDataTab = ClientUtils.ExecuteSQL(strSQL).Tables[0];

            if (sDataTab.Rows.Count > 0)
            {
                string palletStationCheck = string.Empty;
                for (int i = 0; i < sDataTab.Rows.Count; i++)
                {
                    string csnStation = sDataTab.Rows[i]["wc"].ToString();
                    csnStation = csnStation.Trim();
                    if (!csnStation.Equals("W4"))
                    {
                        palletStationCheck = "存在异常站别";
                    }

                }
                if (palletStationCheck.Equals("存在异常站别"))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        #region Even/dtpStartTime_KeyPress
        private void dtpStartTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    GetComCarNo();
                    //    FillDataGridViewByShipment();
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message.ToString(), 0);
                }
            }
        }
        #endregion

        #region Even/dtpEndTime_KeyPress
        private void dtpEndTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    GetComCarNo();
                    //    FillDataGridViewByShipment();
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message.ToString(), 0);
                }
            }
        }
        #endregion

        #region Even/cboCarNo_KeyPress
        /// <summary>
        /// 手动输入车牌号-回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboCarNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
                return;
            if (!string.IsNullOrEmpty(cboCarNo.Text))
            {
                //   FillDataGridViewByShipment();
                string carno = cboCarNo.Text.Trim();
                string startDate = dtpStartTime.Text.Trim();
                string endDate = dtpEndTime.Text.Trim();
                if (string.IsNullOrEmpty(carno))
                {
                    cboShipmentId.Items.Clear();
                    return;
                }
                GetCombineCarInfo(carno, startDate, endDate);
                getShipment(carno, startDate, endDate);
                BindToDayShipmentByCar(carno);
                cboShipmentId.SelectAll();
                cboShipmentId.Focus();
            }
            else
            {
                clearForm("2");
                GetShipNumInfo();
                ShowMsg("请扫入车牌号!", 0);
                return;
            }
        }
        #endregion


        #region Even/dtpStartTime_ValueChanged
        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetComCarNo();
                FillDataGridViewByShipment();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message.ToString(), 0);
            }
        }
        #endregion
        #region Even/dtpEndTime_ValueChanged
        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetComCarNo();
                FillDataGridViewByShipment();
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message.ToString(), 0);
            }
        }
        #endregion
        #region Function/ShowMsg 消息显示
        /// <summary>
        /// 错误/警告/确认 消息显示
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public DialogResult ShowMsg(string strText, int intType)
        {
            txtMessage.Text = strText.TP();
            switch (intType)
            {
                case 0: //Error     
                    LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    txtMessage.ForeColor = Color.Red;
                    txtMessage.BackColor = Color.Silver;
                    return DialogResult.None;
                case 1: //Warning                        
                    txtMessage.ForeColor = Color.Blue;
                    txtMessage.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    txtMessage.ForeColor = Color.White;
                    txtMessage.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }
        #endregion

        #region Function/填充dgvShipmentInfo内容
        /// <summary>
        /// 填充shipment Info Datagridview内容
        /// </summary>
        private void FillDataGridViewByShipment()
        {
            //清空dgvShipmentInfo
            ShipmentBll shipmentBll = new ShipmentBll();
            DataTable dtShipMentDataTalbe = (DataTable)dgvShipmentInfo.DataSource;
            if (dtShipMentDataTalbe != null)
            {
                dtShipMentDataTalbe.Rows.Clear();
                dgvShipmentInfo.DataSource = dtShipMentDataTalbe;
            }
            DataTable dtShipMentAlreadyDataTalbe = (DataTable)dgvShipmentInfoAlready.DataSource;
            if (dtShipMentAlreadyDataTalbe != null)
            {
                dtShipMentAlreadyDataTalbe.Rows.Clear();
                dgvShipmentInfoAlready.DataSource = dtShipMentAlreadyDataTalbe;
            }
            //重新获取数据 
            //  DataTable dataTable = shipmentBll.GetShipmentInfoDataTable(cboCarNo.Text.Trim());
            DataTable dataTable = shipmentBll.GetShipmentInfoDataTable(cboCarNo.Text.Trim(), cboShipmentId.Text.Trim());
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                dgvShipmentInfo.DataSource = dataTable;
            }
            DataTable dataTableAlready = shipmentBll.GetShipmentAlreadyInfoDataTable(cboCarNo.Text.Trim(), cboShipmentId.Text.Trim());
            if (dataTableAlready != null && dataTableAlready.Rows.Count > 0)
            {
                dgvShipmentInfoAlready.DataSource = dataTableAlready;
            }
            GetShipNumInfo();
            //txtPalletNo.SelectAll();
            //txtPalletNo.Focus();
            txtNoCarNum.Text = "";
            string strSID = cboShipmentId.Text;

            txtNoCarNum.Text = shipmentBll.GetNoCarPalletCount(strSID);
            if (!"0".Equals(txtRemainNum.Text))
            {
                txtPalletNo.SelectAll();
                txtPalletNo.Focus();
            }
        }
        #endregion

        /// <summary>
        /// 显示栈板的扫描统计信息
        /// </summary>
        private void GetShipNumInfo()
        {

            //应扫描的栈板的数目
            txtShallNum.Text = (dgvShipmentInfo.RowCount + dgvShipmentInfoAlready.RowCount).ToString();
            //已扫描的栈板的数目
            txtAlreadyNum.Text = dgvShipmentInfoAlready.RowCount.ToString();
            //未扫描的栈板的数目 
            txtRemainNum.Text = dgvShipmentInfo.RowCount.ToString();
            //}

        }

        private void GetComCarNo()
        {
            //this.dtpStartTime.ValueChanged -= new System.EventHandler(this.dtpStartTime_ValueChanged);
            //this.dtpEndTime.ValueChanged -= new System.EventHandler(this.dtpEndTime_ValueChanged);

            //DateTime dateTimeNow = DateTime.Now;
            //dtpStartTime.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            ////HYQ:以下两个第一次被赋值的时候，就会调用dtpStartTime_ValueChanged 和dtpEndTime_ValueChanged

            ////dtpStartTime.Value = dateTimeNow;
            //dtpEndTime.Value = dateTimeNow;



            //关掉触发
            this.cboCarNo.SelectedIndexChanged -= new System.EventHandler(this.cboCarNo_SelectedIndexChanged);

            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);


            //this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            //this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);

            //开启触发
            this.cboCarNo.SelectedIndexChanged += new System.EventHandler(this.cboCarNo_SelectedIndexChanged);

        }


        //HYQ:重写获得卡车信息  //会增加第3个参数只是显示未完成的卡车或是shipment
        //public void getTruckNo(string starttime, string endtime)
        //{
        //    string sql = string.Empty;
        //    if (chkAllRecord.Checked == false)
        //    {
        //        //车牌号 集货单号 间连接符由-改成# modify by Franky 2020/3/4
        //        sql = @"select distinct case
        //                                  when b.car_no = '' then
        //                                   'notrack#' || a.shipment_id
        //                                  when b.car_no is null then
        //                                   'notruck#' || a.shipment_id
        //                                  else
        //                                   b.car_no || '#' || a.shipment_id
        //                                end CAR_NO,
        //                                a.shipment_id,
        //                                to_char(a.cdt,'YYYYMMDD') strcdt 
        //                  FROM ppsuser.t_shipment_pallet a
        //                  join pptest.oms_load_car b
        //                    on a.shipment_id = b.shipment_id
        //                   and isload = 0
        //                   and (b.active = 0  or  b.active is null)
        //                 WHERE (to_date(a.cdt) >= to_date(:startDate, 'YYYY-MM-DD')
        //                   AND to_date(a.cdt) <= to_date(:endDate, 'YYYY-MM-DD')) or a.cdt is null
        //                 order by strcdt asc";
        //    }
        //    else
        //    {
        //        sql = @"select distinct case
        //                                  when b.car_no = '' then
        //                                   'notrack#' || a.shipment_id
        //                                  when b.car_no is null then
        //                                   'notruck#' || a.shipment_id
        //                                  else
        //                                   b.car_no || '#' || a.shipment_id
        //                                end CAR_NO,
        //                                a.shipment_id,
        //                                 to_char(a.cdt,'YYYYMMDD') strcdt 
        //                  FROM ppsuser.t_shipment_pallet a
        //                  join pptest.oms_load_car b
        //                    on a.shipment_id = b.shipment_id
        //                   and (b.active = 0  or  b.active is null)
        //                 WHERE (to_date(a.cdt) >= to_date(:startDate, 'YYYY-MM-DD')
        //                   AND to_date(a.cdt) <= to_date(:endDate, 'YYYY-MM-DD')) or a.cdt is null
        //                 order by strcdt asc";
        //    }

        //    object[][] parameterArray = new object[2][];
        //    parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "startDate", starttime };
        //    parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endDate", endtime };

        //    DataSet dataSet = new DataSet();
        //    try
        //    {
        //        dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
        //    }
        //    catch (Exception e)
        //    {
        //        ShowMsg("连接数据错误" + e, 1);
        //        return;
        //    }
        //    cboCarNo.Items.Clear();

        //    if (dataSet.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
        //        {
        //            cboCarNo.Items.Add(dataSet.Tables[0].Rows[i]["CAR_NO"].ToString());
        //        }
        //        return;
        //    }
        //    else
        //    {
        //        cboCarNo.Items.Add("");
        //        return;
        //    }

        //}
        public void getTruckNo(string starttime, string endtime)
        {
            string sql = string.Empty;
            if (chkAllRecord.Checked == false)
            {
                sql = @"SELECT DISTINCT T.CAR_NO
                          FROM (SELECT DISTINCT CASE
                                                  WHEN B.CAR_NO = '' THEN
                                                   'notrack'
                                                  WHEN B.CAR_NO IS NULL THEN
                                                   'notrack'
                                                  ELSE
                                                   B.CAR_NO
                                                END CAR_NO,
                                                TO_CHAR(A.SHIPPING_TIME, 'YYYYMMDD') STRCDT
                                  FROM PPSUSER.T_SHIPMENT_INFO A
                                  JOIN PPTEST.OMS_LOAD_CAR B
                                    ON A.SHIPMENT_ID = B.SHIPMENT_ID
                                   AND B.ISLOAD = 0
                                   AND (B.ACTIVE = 0 OR B.ACTIVE IS NULL)
                                 WHERE (TO_DATE(A.SHIPPING_TIME) >=
                                       TO_DATE(:STARTDATE, 'YYYY-MM-DD') AND
                                       TO_DATE(A.SHIPPING_TIME) <= TO_DATE(:ENDDATE, 'YYYY-MM-DD'))
                                 ORDER BY STRCDT ASC) T";
            }
            else
            {
                sql = @"SELECT DISTINCT T.CAR_NO
                          FROM (SELECT DISTINCT CASE
                                                  WHEN B.CAR_NO = '' THEN
                                                   'notrack'
                                                  WHEN B.CAR_NO IS NULL THEN
                                                   'notrack'
                                                  ELSE
                                                   B.CAR_NO
                                                END CAR_NO,
                                                TO_CHAR(A.SHIPPING_TIME, 'YYYYMMDD') STRCDT
                                  FROM PPSUSER.T_SHIPMENT_INFO A
                                  JOIN PPTEST.OMS_LOAD_CAR B
                                    ON A.SHIPMENT_ID = B.SHIPMENT_ID
                                    and (b.active = 0  or  b.active is null)
                                 WHERE (TO_DATE(A.SHIPPING_TIME) >=
                                       TO_DATE(:STARTDATE, 'YYYY-MM-DD') AND
                                       TO_DATE(A.SHIPPING_TIME) <= TO_DATE(:ENDDATE, 'YYYY-MM-DD'))
                                 ORDER BY STRCDT ASC) T";
            }

            object[][] parameterArray = new object[2][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "startDate", starttime };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endDate", endtime };

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
            }
            catch (Exception e)
            {
                ShowMsg("连接数据错误" + e, 1);
                return;
            }

            clearForm("1");
            GetShipNumInfo();
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    cboCarNo.Items.Add(dataSet.Tables[0].Rows[i]["CAR_NO"].ToString());
                }
                return;
            }
            else
            {
                cboCarNo.Items.Add("");
                return;
            }

        }

        //copy from jx by wenxing 2020-08-12
        /// <summary>
        /// 看是否有AC和EDI维护在同一辆车上
        /// </summary>
        private void GetCombineCarInfo(string carno, string starttime, string endtime)
        {
            object[][] parameterArray = new object[3][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CARNO", carno };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "STARTDATE", starttime };
            parameterArray[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ENDDATE", endtime };
            string strSql = string.Empty;
            if (!this.chkAllRecord.Checked)
            {
                strSql = @"SELECT M.SHIPPING_TIME,M.CAR_NO FROM 
                            (SELECT DISTINCT A1.SHIPPING_TIME,B1.CAR_NO
                            FROM PPSUSER.T_SHIPMENT_INFO A1
                            INNER JOIN PPTEST.OMS_LOAD_CAR B1 ON A1.SHIPMENT_ID = B1.SHIPMENT_ID
                            WHERE (B1.ACTIVE = 0 OR B1.ACTIVE IS NULL)
                                   AND NVL(B1.ISLOAD,0) = 0
                                   AND (TO_DATE(A1.SHIPPING_TIME) >=
                                         TO_DATE(:STARTDATE, 'YYYY-MM-DD') AND
                                         TO_DATE(A1.SHIPPING_TIME) <= TO_DATE(:ENDDATE, 'YYYY-MM-DD'))
                                   AND B1.CAR_NO=:CARNO) M
                            INNER JOIN 
                            (SELECT DISTINCT A2.SHIPPING_TIME,B2.CAR_NO
                            FROM NONEDIPPS.T_SHIPMENT_INFO A2
                            INNER JOIN NONEDIOMS.OMS_LOAD_CAR B2 ON A2.SHIPMENT_ID = B2.SHIPMENT_ID
                            WHERE (B2.ACTIVE = 0 OR B2.ACTIVE IS NULL)
                                   AND (TO_DATE(A2.SHIPPING_TIME) >=
                                         TO_DATE(:STARTDATE, 'YYYY-MM-DD') AND
                                         TO_DATE(A2.SHIPPING_TIME) <= TO_DATE(:ENDDATE, 'YYYY-MM-DD'))
                                   AND B2.CAR_NO=:CARNO) N ON M.SHIPPING_TIME=N.SHIPPING_TIME AND M.CAR_NO=N.CAR_NO";
            }
            else
            {
                strSql = @"
                            SELECT M.SHIPPING_TIME,M.CAR_NO FROM 
                            (SELECT DISTINCT A1.SHIPPING_TIME,B1.CAR_NO
                            FROM PPSUSER.T_SHIPMENT_INFO A1
                            INNER JOIN PPTEST.OMS_LOAD_CAR B1 ON A1.SHIPMENT_ID = B1.SHIPMENT_ID
                            WHERE (B1.ACTIVE = 0 OR B1.ACTIVE IS NULL)
                                   AND (TO_DATE(A1.SHIPPING_TIME) >=
                                         TO_DATE(:STARTDATE, 'YYYY-MM-DD') AND
                                         TO_DATE(A1.SHIPPING_TIME) <= TO_DATE(:ENDDATE, 'YYYY-MM-DD'))
                                   AND B1.CAR_NO=:CARNO) M
                            INNER JOIN 
                            (SELECT DISTINCT A2.SHIPPING_TIME,B2.CAR_NO
                            FROM NONEDIPPS.T_SHIPMENT_INFO A2
                            INNER JOIN NONEDIOMS.OMS_LOAD_CAR B2 ON A2.SHIPMENT_ID = B2.SHIPMENT_ID
                            WHERE (B2.ACTIVE = 0 OR B2.ACTIVE IS NULL)
                                   AND (TO_DATE(A2.SHIPPING_TIME) >=
                                         TO_DATE(:STARTDATE, 'YYYY-MM-DD') AND
                                         TO_DATE(A2.SHIPPING_TIME) <= TO_DATE(:ENDDATE, 'YYYY-MM-DD'))
                                   AND B2.CAR_NO=:CARNO) N ON M.SHIPPING_TIME=N.SHIPPING_TIME AND M.CAR_NO=N.CAR_NO
                            ";
            }
            DataTable dtTemp = ClientUtils.ExecuteSQL(strSql, parameterArray).Tables[0];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                this.labInfo.Text = string.Format("此车:{0} 同时包含EDI和AC的货，请注意装车！".TP(), carno);
                this.labInfo.Visible = true;
            }
            else
            {
                this.labInfo.Visible = false;
            }
        }
        public void getShipment(string carno, string starttime, string endtime)
        {
            string sql = string.Empty;
            if (chkAllRecord.Checked == false)
            {
                sql = @"select distinct a.shipment_id, to_char(a.shipping_time, 'YYYYMMDD') strcdt
                      from ppsuser.t_shipment_info a
                      join pptest.oms_load_car b
                        on a.shipment_id = b.shipment_id
                        and b.isload = 0
                           and (b.active = 0  or  b.active is null)
                     where b.car_no = :carNo
                       and to_date(a.shipping_time) >= to_date(:startDate, 'YYYY-MM-DD') 
                       and to_date(a.shipping_time) <= to_date(:endDate, 'YYYY-MM-DD')
                     order by strcdt asc";
            }
            else
            {
                sql = @"select distinct a.shipment_id, to_char(a.shipping_time, 'YYYYMMDD') strcdt
                      from ppsuser.t_shipment_info a
                      join pptest.oms_load_car b
                        on a.shipment_id = b.shipment_id
                     where b.car_no = :carNo
                       and (b.active = 0  or  b.active is null)
                       and to_date(a.shipping_time) >= to_date(:startDate, 'YYYY-MM-DD') 
                       and to_date(a.shipping_time) <= to_date(:endDate, 'YYYY-MM-DD')
                     order by strcdt asc";
            }


            object[][] parameterArray = new object[3][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "carNo", carno };
            parameterArray[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "startDate", starttime };
            parameterArray[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endDate", endtime };
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql, parameterArray);
            }
            catch (Exception e)
            {
                ShowMsg("连接数据错误" + e, 1);
                return;
            }
            clearForm("2");
            GetShipNumInfo();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    cboShipmentId.Items.Add(dataSet.Tables[0].Rows[i]["shipment_id"].ToString());
                }
                return;
            }
            else
            {
                cboShipmentId.Items.Add("");
                return;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "";
            txtMessage.BackColor = Color.Blue;
            fCheck fcheck = new fCheck();
            if (fcheck.ShowDialog() != DialogResult.OK)
            {
                ShowMsg("账号权限不正确", 0);
                return;
            }
            else
            {
                rePrintReport pr = new rePrintReport();
                pr.ShowDialog();
            }
        }

        private void chkAllRecord_CheckedChanged(object sender, EventArgs e)
        {
            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            getTruckNo(dtpStartTime.Text, dtpEndTime.Text);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            fShipmentConfirm tsc = new fShipmentConfirm();
            tsc.ShowDialog();
        }
        private void cboShipmentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGridViewByShipment();
            ShowMsg("", -1);
        }

        private void cboShipmentId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
                return;
            if (!string.IsNullOrEmpty(cboShipmentId.Text))
            {
                FillDataGridViewByShipment();
            }
            else
            {
                ShowMsg("请扫入集货单号!", 0);
                return;
            }
        }

        //bind to day shipment by car created by wenxing 2020-09-21
        private void BindToDayShipmentByCar(string carNo)
        {
            ShipmentBll shipmentBll = new ShipmentBll();
            var dt = shipmentBll.GetToDayShipmentByCar(carNo);
            dgvShipmentByCar.DataSource = dt;
        }
    }
}
