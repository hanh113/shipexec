using CommonControl;
using SajetClass;
using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;
using System.Windows.Forms;
//using PTTWebServices.Models;
//using PTTWebServices;

namespace UpLoad856AC
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private string g_SQL;
        //private Int32 g_curRow = -1;    //当前选中行号
        //private string g_partNo = "";   //当前选中的料号

        private string g_sUserID = ClientUtils.UserPara1;
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sProgram = ClientUtils.fProgramName;
        private string g_sFunction = ClientUtils.fFunctionName;
        private string g_sExeName = ClientUtils.fCurrentProject;
        private string g_ServerIP = ClientUtils.url;
        public ChromaThreadLog m_log = new ChromaThreadLog();
        //保存查询出来的PickList用于过滤
        DataTable DtPickList = new DataTable();
        DataTable dtResult = new DataTable();
        string strLocalMACADDRESS = string.Empty;
        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            dt_start.Value = dateTimeNow.AddDays(-1);
            dt_end.Value = dateTimeNow.AddDays(1);
            this.WindowState = FormWindowState.Maximized;
            strLocalMACADDRESS = LibHelperAC.LocalHelper.getMacAddr_Local();

            //this.btsSentEDI.Enabled = true;
            //this.btsSentEDI.Visible = true;
            this.btnUploadTT.Enabled = true;
            this.btnUploadTT.Visible = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgvDN.DataSource = null;
            dgvNo.DataSource = null;

            int iPara = 0;                              //变量项次
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            string l_shipmentType;

            g_SQL = "  Select a.Shipment_ID 集货单号,c.shipping_time 出货时间,c.CARRIER_NAME Carrier,c.POE,c.Region 地区,a.Pallet_NO 栈板号, "
                    + "       case "
                    + "         when a.PALLET_TYPE = '001' then "
                    + "          'NO MIX' "
                    + "         when a.PALLET_TYPE = '999' then "
                    + "          'MIX' "
                    + "         else "
                    + "          a.PALLET_TYPE "
                    + "       end 栈板类型, "
                    + "     b.ICTPN 料号, b.QTY 数量, b.CARTON_QTY 箱数, b.PACK_CARTON 已Pack箱数, "
                    + "        case "
                    + "          when b.pack_status = 'WP' then "
                    + "           'WP-未PACK' "
                    + "          when b.pack_status = 'IP' then "
                    + "           'IP-PACK中' "
                    + "          when b.pack_status = 'FP' then "
                    + "           'FP-已PACK' "
                    + "          when b.pack_status = 'QH' then "
                    + "           'QH-QHold' "
                    + "       else "
                    + "           b.pack_status "
                    + "        end  Part_PACK_Status, "
                    + "        upload_edi_time,upload_erp_time,upload_emp_no "
                    + " from NONEDIPPS.T_SHIPMENT_PALLET a "
                    + " inner join NONEDIPPS.T_SHIPMENT_PALLET_PART b on a.pallet_no = b.PALLET_NO "
                    + " inner join NONEDIPPS.T_SHIPMENT_INFO c on a.SHIPMENT_ID = c.shipment_id "
                    + " where 1 = 1 ";
            //组合输入查询条件，过滤数据源
            //出货类型查询条件
            if (radDS.Checked)
                l_shipmentType = radDS.Text;
            else
                l_shipmentType = radFD.Text;

            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            g_SQL += " and c.SHIPMENT_TYPE = :shipmentType";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentType", l_shipmentType };
            iPara = iPara + 1;


            if (cmbSTATUS.Text.Contains("ALL") || cmbSTATUS.Text.Contains("UF-"))
            {
                //出货开始日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.SHIPPING_TIME >= :shipmentTime";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "shipmentTime", dt_start.Value };
                iPara = iPara + 1;

                //出货结束日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.SHIPPING_TIME <= :shipmentTime2";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "shipmentTime2", dt_end.Value };
                iPara = iPara + 1;
            }

            //集货单号查询条件
            if (cmbSmid.Text.Trim() != "" && cmbSmid.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and a.Shipment_ID = :shipmentID";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentID", cmbSmid.Text };
                iPara = iPara + 1;
            }

            //货代查询条件
            if (cmbCarrier.Text.Trim() != "" && cmbCarrier.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.CARRIER_NAME = :carrier";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "carrier", cmbCarrier.Text };
                iPara = iPara + 1;
            }

            //港口查询条件
            if (cmbPOE.Text.Trim() != "" && cmbPOE.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.POE = :poe";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "poe", cmbPOE.Text };
                iPara = iPara + 1;
            }

            //状态查询条件// 这里有个状态 查不到的 QH
            if (cmbSTATUS.Text.Trim() != "" && cmbSTATUS.Text.Trim() != "-ALL-")
            {
                string strStatus = cmbSTATUS.Text.Trim();
                if (strStatus.Contains("WP"))
                { strStatus = "WP"; }
                else if (strStatus.Contains("IP"))
                { strStatus = "IP"; }
                else if (strStatus.Contains("FP"))
                { strStatus = "FP"; }
                else if (strStatus.Contains("LF"))
                { strStatus = "LF"; }
                //else if (strStatus.Contains("UF"))
                //{ strStatus = "UF"; }
                else if (strStatus.Contains("CP"))
                { strStatus = "CP"; }
                else if (strStatus.Contains("HO"))
                { strStatus = "HO"; }

                //{"-ALL-",
                //"WP-未PACK",
                //"IP-PACK中",
                //"FP-已PACK",
                //"LF-已LOADCAR",
                //"UF-已UPLOAD",
                //"CP-CANCEL",
                //"HO-Hold"}

                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.Status = :Status";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Status", strStatus };
                iPara = iPara + 1;
            }

            //地区查询条件
            if (cmbREGION.Text.Trim() != "" && cmbREGION.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.Region = :Region";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Region", cmbREGION.Text };
                iPara = iPara + 1;
            }
            //添加排序
            g_SQL += " order by c.shipping_time asc, a.pallet_no asc , b.ictpn ";
            DataTable db = ClientUtils.ExecuteSQL(g_SQL, sqlparams).Tables[0];
            //HYQ：下面一句写完好像不能设置颜色
            dgvNo.DataSource = db;
            dgvNo.AutoResizeColumns();


            if (db.Rows.Count > 0)
            {
                cmbSmid.DataSource = null;
                cmbSmid.Items.Clear();
                cmbSmid.Items.Add("-ALL-");

                cmbREGION.DataSource = null;
                cmbREGION.Items.Clear();
                cmbREGION.Items.Add("-ALL-");


                cmbPOE.DataSource = null;
                cmbPOE.Items.Clear();
                cmbPOE.Items.Add("-ALL-");


                cmbCarrier.DataSource = null;
                cmbCarrier.Items.Clear();
                cmbCarrier.Items.Add("-ALL-");

                for (int i = 0; i < dgvNo.Rows.Count; i++)
                {
                    if (!cmbSmid.Items.Contains(dgvNo.Rows[i].Cells["集货单号"].Value.ToString()))
                    {
                        cmbSmid.Items.Add(dgvNo.Rows[i].Cells["集货单号"].Value.ToString());
                    }
                    if (!cmbREGION.Items.Contains(dgvNo.Rows[i].Cells["地区"].Value.ToString()))
                    {
                        cmbREGION.Items.Add(dgvNo.Rows[i].Cells["地区"].Value.ToString());
                    }
                    if (!cmbPOE.Items.Contains(dgvNo.Rows[i].Cells["POE"].Value.ToString()))
                    {
                        cmbPOE.Items.Add(dgvNo.Rows[i].Cells["POE"].Value.ToString());
                    }
                    if (!cmbCarrier.Items.Contains(dgvNo.Rows[i].Cells["Carrier"].Value.ToString()))
                    {
                        cmbCarrier.Items.Add(dgvNo.Rows[i].Cells["Carrier"].Value.ToString());
                    }

                }
            }
            else
            {
                dgvNo.DataSource = null;
            }

            ShowMsg("", -1);

            if (!cmbSmid.Text.Equals("-ALL-"))
            {
                string strSQL = string.Format(" select delivery_no     DeliveryNo, "
                                        + "       line_item       DNItem, "
                                        + "       mpn             MPN,  "
                                        + "       ictpn           料号, "
                                        + "       shipment_id     集货单号,  "
                                        + "       hawb, "
                                        + "       shipment_type, "
                                        + "       qty, "
                                        + "       carton_qty, "
                                        + "       transport, "
                                        + "       carrier_code, "
                                        + "       status, "
                                        + "       pack_qty        PACK数量, "
                                        + "       pack_carton_qty PACK箱数 "
                                        + "  from NONEDIPPS.t_order_info "
                                        + " where shipment_id = '{0}' ", cmbSmid.Text.Trim());
                DataTable db2 = ClientUtils.ExecuteSQL(strSQL).Tables[0];
                dgvDN.DataSource = db2;
                dgvDN.AutoResizeColumns();
            }

        }

        private void btnUploadTT_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);
            btnUploadTT.Enabled = false;
            if (cmbSmid.Text.Equals("-ALL-") || (dgvDN.RowCount == 0))
            {
                ShowMsg("先选集货单点击查询", 0);
                btnUploadTT.Enabled = true;
                return;
            }
            string shipmentid = cmbSmid.Text.Trim();
            if (string.IsNullOrEmpty(shipmentid))
            { btnUploadTT.Enabled = true; return; }

            //改成SP， 加强检查功能，如站别数量
            //private bool CheckShipmentStatus(string shipmentid, string checktype)  //TIPTOP  EDI
            string checktype = string.Empty;
            checktype = "TIPTOP";

            string strResult1 = string.Empty;
            strResult1 = CheckShipmentStatus(shipmentid, checktype);

            string strCheckerrmsg = string.Empty;
            string strTTURL = string.Empty;
            string strResult2 = string.Empty;

            UploadBll ub = new UploadBll();
            strResult2 = ub.PPSCheckWEBLOG(shipmentid, out strTTURL, out strCheckerrmsg);

            if (strResult1.StartsWith("NG"))
            {
                Show_Message(strResult1, 0);
                btnUploadTT.Enabled = true;
                return;
            }
            if (strCheckerrmsg.StartsWith("NG"))
            {
                Show_Message(strCheckerrmsg, 0);
                btnUploadTT.Enabled = true;
                return;
            }
            if (strResult1.Contains("UPLOAD TIPTOP") || strCheckerrmsg.Contains("已经传过"))
            {
                //跳对话框
                fCheck fcheck = new fCheck();
                if (fcheck.ShowDialog() != DialogResult.OK)
                {
                    ShowMsg("账号权限不正确", 0);
                    btnUploadTT.Enabled = true;
                    return;
                }
            }
            string strWebServiceRturn = string.Empty;

            // 20190628 update :IP区分改为DB维护TTURL
            strWebServiceRturn = ub.AfterEdiHttpPostWebService(g_ServerIP, strTTURL, shipmentid);


            ////KS测试库 //http://10.33.20.185/OMSBgJob/ExecuteToTTAOISOI // 
            //if (g_ServerIP.Contains("10.33.20.177"))
            //{
            //    strWebServiceRturn = ub.AfterEdiHttpPostWebService(g_ServerIP, "http://10.33.20.185/OMSBgJob/ExecuteToTTAOISOI", shipmentid);

            //}
            ////KS正式库 http://omsks.luxshare-ict.com/OMSBgJob/ExecuteToTTAOISOI
            //else if (g_ServerIP.Contains("10.38.8.107") || g_ServerIP.Contains("10.38.8.108"))
            //{
            //    strWebServiceRturn = ub.AfterEdiHttpPostWebService(g_ServerIP, "http://omsks.luxshare-ict.com/OMSBgJob/ExecuteToTTAOISOI", shipmentid);

            //}
            //else
            //{
            //    strWebServiceRturn = "";
            //    Show_Message("不用TIPTOP扣账", -1);
            //    return;
            //}
            //UpdateShipmentFlag(shipmentid, checktype);
            Show_Message(strWebServiceRturn, -1);
            btnUploadTT.Enabled = true;
        }
        private string CheckShipmentStatus(string shipmentid, string checktype)
        {
            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentid };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "checktype", checktype };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                //NONEDIPPS.SP_UPLOAD_CHECKSHIPMENTSTATUS(shipmentid in varchar2,
                //                                                  checktype  in varchar2, //checktype = 'TIPTOP' or 'EDI'
                //                                                  errmsg     out varchar2)
                dt = ClientUtils.ExecuteProc("NONEDIPPS.SP_UPLOAD_CHECKSHIPMENTSTATUS", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                Show_Message(e1.ToString(), 0);
                return "NG";
            }
            return dt.Rows[0]["errmsg"].ToString();
        }

        private void cmbSTATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            formControl();
        }

        public void formControl()
        {
            if (cmbSTATUS.Text.Contains("ALL") || cmbSTATUS.Text.Contains("UF-"))
            {
                lblStart.Visible = true;
                lblEnd.Visible = true;
                dt_start.Visible = true;
                dt_end.Visible = true;
            }
            else
            {
                lblStart.Visible = false;
                lblEnd.Visible = false;
                dt_start.Visible = false;
                dt_end.Visible = false;
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            initface();
        }

        private void initface()
        {
            cmbSmid.Text = "-ALL-";
            cmbCarrier.Text = "-ALL-";
            cmbPOE.Text = "-ALL-";
            cmbREGION.Text = "-ALL-";
            dgvDN.DataSource = null;
            dgvNo.DataSource = null;
            int iPara = 0;                              //变量项次
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            string l_shipmentType;

            g_SQL = "  Select a.Shipment_ID 集货单号,c.shipping_time 出货时间,c.CARRIER_NAME Carrier,c.POE,c.Region 地区,a.Pallet_NO 栈板号, a.PALLET_TYPE 栈板类型, " +
                    "     b.ICTPN 料号, b.QTY 数量, b.CARTON_QTY 箱数, b.PACK_CARTON 已Pack箱数, " +
                    "        case " +
                    "          when b.pack_status = 'WP' then " +
                    "           'WP-未PACK' " +
                    "          when b.pack_status = 'IP' then " +
                    "           'IP-PACK中' " +
                    "          when b.pack_status = 'FP' then " +
                    "           'FP-已PACK' " +
                    "          when b.pack_status = 'QH' then " +
                    "           'QH-QHold' " +
                    "       else " +
                    "           b.pack_status " +
                    "        end  Part_PACK_Status, " +
                    "        upload_edi_time,upload_erp_time,upload_emp_no " +
                    " from NONEDIPPS.T_SHIPMENT_PALLET a " +
                    " inner join NONEDIPPS.T_SHIPMENT_PALLET_PART b on a.pallet_no = b.PALLET_NO " +
                    " inner join NONEDIPPS.T_SHIPMENT_INFO c on a.SHIPMENT_ID = c.shipment_id " +
                    " where 1 = 1 ";
            //组合输入查询条件，过滤数据源
            //出货类型查询条件
            if (radDS.Checked)
                l_shipmentType = radDS.Text;
            else
                l_shipmentType = radFD.Text;

            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            g_SQL += " and c.SHIPMENT_TYPE = :shipmentType";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentType", l_shipmentType };
            iPara = iPara + 1;


            if (cmbSTATUS.Text.Contains("ALL") || cmbSTATUS.Text.Contains("UF-"))
            {
                //出货开始日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.SHIPPING_TIME >= :shipmentTime";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "shipmentTime", dt_start.Value };
                iPara = iPara + 1;

                //出货结束日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.SHIPPING_TIME <= :shipmentTime2";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "shipmentTime2", dt_end.Value };
                iPara = iPara + 1;
            }

            //集货单号查询条件
            if (cmbSmid.Text.Trim() != "" && cmbSmid.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and a.Shipment_ID = :shipmentID";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentID", cmbSmid.Text };
                iPara = iPara + 1;
            }

            //货代查询条件
            if (cmbCarrier.Text.Trim() != "" && cmbCarrier.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.CARRIER_NAME = :carrier";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "carrier", cmbCarrier.Text };
                iPara = iPara + 1;
            }

            //港口查询条件
            if (cmbPOE.Text.Trim() != "" && cmbPOE.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.POE = :poe";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "poe", cmbPOE.Text };
                iPara = iPara + 1;
            }

            //状态查询条件// 这里有个状态 查不到的 QH
            if (cmbSTATUS.Text.Trim() != "" && cmbSTATUS.Text.Trim() != "-ALL-")
            {
                string strStatus = cmbSTATUS.Text.Trim();
                if (strStatus.Contains("WP"))
                { strStatus = "WP"; }
                else if (strStatus.Contains("IP"))
                { strStatus = "IP"; }
                else if (strStatus.Contains("FP"))
                { strStatus = "FP"; }
                else if (strStatus.Contains("LF"))
                { strStatus = "LF"; }
                //else if (strStatus.Contains("UF"))
                //{ strStatus = "UF"; }
                else if (strStatus.Contains("CP"))
                { strStatus = "CP"; }
                else if (strStatus.Contains("HO"))
                { strStatus = "HO"; }

                //{"-ALL-",
                //"WP-未PACK",
                //"IP-PACK中",
                //"FP-已PACK",
                //"LF-已LOADCAR",
                //"UF-已UPLOAD",
                //"CP-CANCEL",
                //"HO-Hold"}

                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.Status = :Status";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Status", strStatus };
                iPara = iPara + 1;
            }

            //地区查询条件
            if (cmbREGION.Text.Trim() != "" && cmbREGION.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.Region = :Region";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Region", cmbREGION.Text };
                iPara = iPara + 1;
            }
            //添加排序
            g_SQL += " order by c.shipping_time asc, a.pallet_no, b.ictpn ";
            DataTable db = ClientUtils.ExecuteSQL(g_SQL, sqlparams).Tables[0];
            //HYQ：下面一句写完好像不能设置颜色
            dgvNo.DataSource = db;
            dgvNo.AutoResizeColumns();


            if (db.Rows.Count > 0)
            {
                cmbSmid.DataSource = null;
                cmbSmid.Items.Clear();
                cmbSmid.Items.Add("-ALL-");

                cmbREGION.DataSource = null;
                cmbREGION.Items.Clear();
                cmbREGION.Items.Add("-ALL-");


                cmbPOE.DataSource = null;
                cmbPOE.Items.Clear();
                cmbPOE.Items.Add("-ALL-");


                cmbCarrier.DataSource = null;
                cmbCarrier.Items.Clear();
                cmbCarrier.Items.Add("-ALL-");

                for (int i = 0; i < dgvNo.Rows.Count; i++)
                {
                    if (!cmbSmid.Items.Contains(dgvNo.Rows[i].Cells["集货单号"].Value.ToString()))
                    {
                        cmbSmid.Items.Add(dgvNo.Rows[i].Cells["集货单号"].Value.ToString());
                    }
                    if (!cmbREGION.Items.Contains(dgvNo.Rows[i].Cells["地区"].Value.ToString()))
                    {
                        cmbREGION.Items.Add(dgvNo.Rows[i].Cells["地区"].Value.ToString());
                    }
                    if (!cmbPOE.Items.Contains(dgvNo.Rows[i].Cells["POE"].Value.ToString()))
                    {
                        cmbPOE.Items.Add(dgvNo.Rows[i].Cells["POE"].Value.ToString());
                    }
                    if (!cmbCarrier.Items.Contains(dgvNo.Rows[i].Cells["Carrier"].Value.ToString()))
                    {
                        cmbCarrier.Items.Add(dgvNo.Rows[i].Cells["Carrier"].Value.ToString());
                    }

                }
            }
            else
            {
                dgvNo.DataSource = null;
            }

            ShowMsg("", -1);

            if (!cmbSmid.Text.Equals("-ALL-"))
            {
                string strSQL = string.Format(" select delivery_no     DeliveryNo, "
                                        + "       line_item       DNItem, "
                                        + "       mpn             MPN,  "
                                        + "       ictpn           料号, "
                                        + "       shipment_id     集货单号,  "
                                        + "       hawb, "
                                        + "       shipment_type, "
                                        + "       qty, "
                                        + "       carton_qty, "
                                        + "       transport, "
                                        + "       carrier_code, "
                                        + "       status, "
                                        + "       pack_qty        PACK数量, "
                                        + "       pack_carton_qty PACK箱数 "
                                        + "  from NONEDIPPS.t_order_info "
                                        + " where shipment_id = '{0}' ", cmbSmid.Text.Trim());
                DataTable db2 = ClientUtils.ExecuteSQL(strSQL).Tables[0];
                dgvDN.DataSource = db2;
                dgvDN.AutoResizeColumns();
            }
        }

        private void radFD_CheckedChanged(object sender, EventArgs e)
        {
            initface();
        }

        private void radDS_CheckedChanged(object sender, EventArgs e)
        {
            initface();
        }

        private void btsSentEDI_Click(object sender, EventArgs e)
        {
            //ShowMsg("", -1);
            //btsSentEDI.Enabled = false;
            //if (cmbSmid.Text.Equals("-ALL-") || (dgvDN.RowCount==0))
            //{
            //    ShowMsg("先选集货单点击查询", 0);
            //    btsSentEDI.Enabled = true;
            //    return;
            //}

            //string shipmentId = cmbSmid.Text;
            //string checktype = string.Empty;
            //checktype = "EDI";
            //string strResult1 = string.Empty;
            //strResult1 = CheckShipmentStatus(shipmentId, checktype);

            //if (strResult1.StartsWith("NG"))
            //{
            //    ShowMsg(strResult1, 0);
            //    btsSentEDI.Enabled = true;
            //    return;
            //}
            //if (strResult1.Contains("UPLOAD EDI"))
            //{
            //    //跳对话框
            //    fCheck fcheck = new fCheck();
            //    if (fcheck.ShowDialog() != DialogResult.OK)
            //    {
            //        ShowMsg("账号权限不正确", 0);
            //        btsSentEDI.Enabled = true;
            //        return;
            //    }
            //}

            ////HYQ: 20181123 全部新写， 不按照之前的来，
            ////新写的，写5张表
            //string errmsg = string.Empty;
            //string strResult = string.Empty;
            //if (radFD.Checked)
            //{ strResult = Insert856ASN(shipmentId, out errmsg); }
            //else
            //{
            //    if (strResult1.Contains("-SAWB"))
            //    { strResult = Insert856SCSAWB(shipmentId, out errmsg); }
            //    else
            //    { strResult = Insert856SC(shipmentId, out errmsg); }

            //}

            //if (!strResult.Contains("OK"))
            //{
            //    Show_Message(errmsg, 0);
            //    btsSentEDI.Enabled = true;
            //    return;
            //}
            ////if (!UpdateShipmentFlag(shipmentId, checktype))
            ////{
            ////    Show_Message("UpdateShipmentFlag", 0);
            ////    return;
            ////}

            //string strResultInsertLog = string.Empty;
            //string strResulterrmsg = string.Empty;
            //UploadBll ub = new UploadBll();
            //strResultInsertLog = ub.PPSInsertWorkLog(shipmentId, "UPLOAD", strLocalMACADDRESS, out strResulterrmsg);
            //UpdateShipmentFlag(shipmentId, checktype);
            //Show_Message("UPLOAD EDI成功", -1);

            //btsSentEDI.Enabled = true;
            #region //HYQ:以下是旧的
            //string sRes = "";

            //DataTable dta = this.GetDgvToTable();
            //int intRowIndex = 0;
            //foreach (DataRow r in dta.Rows)
            //{

            //    string dn = r["出货单号"].ToString();
            //    string shipmentid = r["SHIPMENT_ID"].ToString();
            //    string dnline = r["出货明细行号"].ToString();
            //    string cartonno = r["箱号"].ToString();
            //    string nums = GETPACKAGE_ID();//获取package_id
            //    update856data(dn);
            //    update945data(dn);
            //    bool flag1 = sentedi(shipmentid, dn, dnline, nums, cartonno, ref sRes);
            //    if (flag1)
            //    {
            //        this.ShowMsg("上传OK！", 1);
            //        dgvNo.Rows[intRowIndex].Cells["EDI_FLAG"].Value = 'Y';

            //    }
            //    else
            //    {
            //        this.ShowMsg(sRes, 0);

            //        dtResult.Rows.Clear();
            //        return;//跳出for循环
            //    }
            //    intRowIndex++;
            //}
            //object[][] Par = new object[1][];
            //string Sql = @"SELECT A.TYPE
            //                  FROM PPSUSER.G_DS_SHIMMENT_BASE_T A
            //                 WHERE A.SHIPMENT_ID =:SHIPMENTID";
            //Par[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "SHIPMENTID", cmbSmid.Text.Trim() };
            //DataSet dt = ClientUtils.ExecuteSQL(Sql, Par);
            //if (dt.Tables[0].Rows.Count > 0)
            //{
            //    string Type = dt.Tables[0].Rows[0]["TYPE"].ToString();
            //    if (Type != "Bulk")
            //    {
            //        UpdateEdiFlag(shipmentId);
            //        dtResult.Rows.Clear();
            //        return;
            //    }
            //}

            //object[][] Params = new object[1][];
            //string sql = @"SELECT A.SHIPMENT_ID, A.SSCC,B.ASN_NUM,A.PALLET_TYPE
            //                  FROM PPSUSER.G_DS_PALLET_T          A,PPSUSER.G_DS_PICK_T D,
            //                       WMUSER.AC_856SC_SHIPMENT       B,
            //                       PPSUSER.G_DS_SHIPMENT_DNLINE_T C
            //                 WHERE A.SHIPMENT_ID = C.SHIPMENT_ID
            //                   AND C.DN = B.ASN_NUM AND A.PALLET_NO=D.PALLET_NO AND D.DN=C.DN
            //                   AND A.SHIPMENT_ID =:TDN
            //                   GROUP BY A.SHIPMENT_ID, A.SSCC,B.ASN_NUM,A.PALLET_TYPE";
            //Params[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "TDN", cmbSmid.Text.Trim() };
            //DataSet dsTemp = ClientUtils.ExecuteSQL(sql, Params);
            //if (dsTemp.Tables[0].Rows.Count > 0)
            //{
            //    string id = string.Empty;
            //    string sscc = string.Empty;
            //    string asnnum = string.Empty;
            //    string pallettype = string.Empty;
            //    for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
            //    {
            //        pallettype = dsTemp.Tables[0].Rows[i]["PALLET_TYPE"].ToString();
            //        if (pallettype == "999" && id == dsTemp.Tables[0].Rows[i]["SHIPMENT_ID"].ToString() && sscc == dsTemp.Tables[0].Rows[i]["SSCC"].ToString())
            //        {
            //            continue;
            //        }

            //        id = dsTemp.Tables[0].Rows[i]["SHIPMENT_ID"].ToString();
            //        sscc = dsTemp.Tables[0].Rows[i]["SSCC"].ToString();
            //        asnnum = dsTemp.Tables[0].Rows[i]["asn_num"].ToString();

            //        //   insert945data(asnnum);//  改为procedure  中上传
            //        string ids = GETPACKAGE_ID();//获取package_id
            //        insert_t(cmbSmid.Text.Trim(), ids, sscc, asnnum, pallettype);
            //        Updatepack_p(ids, sscc, asnnum, pallettype);
            //    }

            //}
            //UpdateEdiFlag(shipmentId);

            //dtResult.Rows.Clear();

            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(ClientUtils.url);
            //UploadBll ub = new UploadBll();
            //string strWebServiceRturn = string.Empty;
            //strWebServiceRturn = ub.AfterEdiHttpPostWebService(g_ServerIP, "http://omsks.luxshare-ict.com/OMSBgJob/ExecuteToTTAOISOI", "FK1919000166");
            //MessageBox.Show(strWebServiceRturn);
        }

        private void cmbSmid_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDN.DataSource = null;
        }
        private void Show_Message(string msg, int type)
        {
            TextMsg.Text = msg;
            switch (type)
            {
                case 0: //error
                    TextMsg.ForeColor = Color.Black;
                    TextMsg.BackColor = Color.Red;
                    break;
                case 1:
                    TextMsg.ForeColor = Color.Yellow;
                    TextMsg.BackColor = Color.Blue;
                    break;
                default:
                    TextMsg.ForeColor = Color.White;
                    TextMsg.BackColor = Color.Blue;
                    break;
            }
        }

        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt;
            switch (strType)
            {
                case 0: //Error                
                    TextMsg.ForeColor = Color.Black;
                    TextMsg.BackColor = Color.Red;
                    return DialogResult.None;
                case 1: //Warning                        
                    TextMsg.ForeColor = Color.Blue;
                    TextMsg.BackColor = Color.FromArgb(255, 255, 128);
                    return DialogResult.None;
                case 2: //Confirm
                    return MessageBox.Show(strTxt, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.White;
                    TextMsg.BackColor = Color.Blue;
                    return DialogResult.None;
            }
        }
    }
}
