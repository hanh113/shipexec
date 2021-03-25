using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibHelper;
using System.Net;
using System.Net.Mail;
using SajetClass;
using Oracle.ManagedDataAccess.Client;
using ClientUtilsDll;

namespace QHold
{

    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();

        }
        private string g_ServerIP = ClientUtils.url;
        private string strDBtype = string.Empty;
        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text = labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            string strResult = string.Empty;
            string strOutparavalue = string.Empty;
            string strResulterrmsg = string.Empty;
            QHoldBll pb = new QHoldBll();
            strResult = pb.GetDBType("DB_TYPE", out strOutparavalue, out strResulterrmsg);
            if (strResult.Equals("OK") && strOutparavalue.Equals("TEST"))
            {

                btnToHold.Visible = Enabled;
                btnToHold.Enabled = Enabled;
            }
            else
            {
                btnToHold.Visible = false;
                btnToHold.Enabled = false;
            }
            strDBtype = strOutparavalue;

        }
        private string g_SQL;
        //private Int32 g_curRow = -1;    //当前选中行号
        //private string g_partNo = "";   //当前选中的料号

        private string g_sUserID = ClientUtils.UserPara1;
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sProgram = ClientUtils.fProgramName;
        private string g_sFunction = ClientUtils.fFunctionName;
        private string g_sExeName = ClientUtils.fCurrentProject;
        //保存查询出来的PickList用于过滤
        DataTable DtPickList = new DataTable();
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

        private void radFD_CheckedChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
        }

        private void radDS_CheckedChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
        }
        private void BindingCombobox()
        {
            string l_shipmentType;

            if (radDS.Checked)
                l_shipmentType = radDS.Text;
            else
                l_shipmentType = radFD.Text;


            this.cmbPOE.SelectedValueChanged -= new System.EventHandler(this.cmbSmid_SelectedValueChanged);
            this.cmbCarrier.SelectedValueChanged -= new System.EventHandler(this.cmbSmid_SelectedValueChanged);

            string strSql = @"select '-ALL-' as carrier from dual 
                              union 
                              select distinct  c.CARRIER_NAME as carrier       
                              from ppsuser.T_SHIPMENT_PALLET a 
                                 inner join ppsuser.T_SHIPMENT_PALLET_PART b on a.pallet_no = b.PALLET_NO 
                                 inner join ppsuser.T_SHIPMENT_INFO c on a.SHIPMENT_ID = c.shipment_id 
                              where 1 = 1  and c.status='WP' 
                                 and c.SHIPMENT_TYPE = :shipmentType ";
            object[][] Param = new object[1][];
            Param[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentType", l_shipmentType };

            DataSet dts = ClientUtils.ExecuteSQL(strSql, Param);
            if (dts != null && dts.Tables[0].Rows.Count > 0)
            {
                List<string> carrierList = (from d in dts.Tables[0].AsEnumerable()
                                            select d.Field<string>("carrier")).ToList();
                carrierList.Sort();
                cmbCarrier.DataSource = carrierList;

            }
            else
            {
                List<string> carrierList = new List<string>();
                carrierList.Add("-ALL-");
                cmbCarrier.DataSource = carrierList;
            }

            string strSql2 = @"select '-ALL-' as poe from dual 
                              union 
                              select distinct  c.poe  as poe    
                              from ppsuser.T_SHIPMENT_PALLET a 
                                 inner join ppsuser.T_SHIPMENT_PALLET_PART b on a.pallet_no = b.PALLET_NO 
                                 inner join ppsuser.T_SHIPMENT_INFO c on a.SHIPMENT_ID = c.shipment_id 
                              where 1 = 1 and c.status='WP' 
                                 and c.SHIPMENT_TYPE = :shipmentType ";
            object[][] Param2 = new object[1][];
            Param2[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentType", l_shipmentType };
            DataSet dts2 = ClientUtils.ExecuteSQL(strSql2, Param2);
            if (dts2 != null && dts2.Tables[0].Rows.Count > 0)
            {
                List<string> poeList2 = (from d in dts2.Tables[0].AsEnumerable()
                                         select d.Field<string>("poe")).ToList();
                poeList2.Sort();
                cmbPOE.DataSource = poeList2;
            }
            else
            {
                List<string> poeList2 = new List<string>();
                poeList2.Add("-ALL-");
                cmbPOE.DataSource = poeList2;
            }

            this.cmbPOE.SelectedValueChanged += new System.EventHandler(this.cmbSmid_SelectedValueChanged);
            this.cmbCarrier.SelectedValueChanged += new System.EventHandler(this.cmbSmid_SelectedValueChanged);
        }


        private void cmbSmid_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void dgvNo_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //for (int i = 0; i < dgvNo.Rows.Count; i++)
            //{
            //    //this.dgvNo.Rows[i].HeaderCell.Value = (i + 1).ToString().Trim();
            //    ////按条件设置每一行的颜色属性
            //    //this.dgvNo.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
            //    if (dgvNo.Rows[i].Cells["集货单状态"].Value.ToString().Contains("WP"))
            //    {
            //        this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //    }
            //    else if (dgvNo.Rows[i].Cells["集货单状态"].Value.ToString().Contains("IP"))
            //    {
            //        this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
            //    }
            //    else if (dgvNo.Rows[i].Cells["集货单状态"].Value.ToString().Contains("UF"))
            //    {
            //        this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
            //    }
            //}
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int iPara = 0;                              //变量项次
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            string l_shipmentType;

            g_SQL = "Select a.Shipment_ID 集货单号,                      "
                    + "       case                                         "
                    + "         when b.status = 'WP' then                  "
                    + "          '待PACK'                                  "
                    + "         when b.status = 'IP' then                  "
                    + "          'PACK中'                                  "
                    + "         when b.status = 'FP' then                  "
                    + "          '已PACK'                                  "
                    + "         when b.status = 'LF' then                  "
                    + "          '已装车'                                  "
                    + "         when b.status = 'UF' then                  "
                    + "          '已上传'                                  "
                    + "         when b.status = 'HO' then                  "
                    + "          'HOLD'                                    "
                    + "         when b.status = 'CP' then                  "
                    + "          '已取消'                                  "
                    + "         else                                       "
                    + "          b.status                                  "
                    + "       end 集货单状态,                              "
                    + "       b.shipping_time 出货时间,                    "
                    + "       (SELECT distinct SCACCODE                             "
                    + "          FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D "
                    + "         where trim(D.carriercode) = B.carrier_code "
                    + "           and D.ShipMode = b.transport             "
                    + "           and D.region = b.region                  "
                    + "           and D.isdisabled = '0'                   "
                    + "           and D.type = 'HAWB') AS Carrier,         "
                    + "       b.POE,                                       "
                    + "       b.Region 地区,                               "
                    + "       a.Pallet_NO 栈板号,                          "
                    + "       case                                         "
                    + "         when a.PALLET_TYPE = '001' then            "
                    + "          'NO MIX'                                  "
                    + "         when a.PALLET_TYPE = '999' then            "
                    + "          'MIX'                                     "
                    + "         else                                       "
                    + "          a.PALLET_TYPE                             "
                    + "       end 栈板类型,                                "
                    + "       a.carton_qty 需求箱数,                       "
                    + "       a.pick_carton PICK箱数,                      "
                    + "       a.pack_carton PACK箱数,                      "
                    + "       a.check_result Check结果,                    "
                    + "       a.weight 重量,                               "
                    + "       case                                         "
                    + "         when a.shipment_flag = '1' then            "
                    + "          '已装车'                                  "
                    + "         else                                       "
                    + "          '未装车'                                  "
                    + "       end 装车状态,                                "
                    + "       a.truck_no 车牌                              "
                    + "  from ppsuser.T_SHIPMENT_PALLET a                  "
                    + " inner join ppsuser.T_SHIPMENT_INFO b               "
                    + "    on a.SHIPMENT_ID = b.shipment_id                "
                    + " where 1 = 1						                  ";
            //组合输入查询条件，过滤数据源
            //出货类型查询条件
            if (radDS.Checked)
                l_shipmentType = radDS.Text;
            else
                l_shipmentType = radFD.Text;

            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            g_SQL += " and b.SHIPMENT_TYPE = :shipmentType";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentType", l_shipmentType };
            iPara = iPara + 1;

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
                g_SQL += " and b.CARRIER_NAME = :carrier";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "carrier", cmbCarrier.Text };
                iPara = iPara + 1;
            }

            //港口查询条件
            if (cmbPOE.Text.Trim() != "" && cmbPOE.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and b.POE = :poe";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "poe", cmbPOE.Text };
                iPara = iPara + 1;
            }

            //状态查询条件 
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
                else if (strStatus.Contains("UF"))
                { strStatus = "UF"; }
                else if (strStatus.Contains("CP"))
                { strStatus = "CP"; }
                else if (strStatus.Contains("HO"))
                { strStatus = "HO"; }

                //"-ALL-",
                //"WP-未PACK",
                //"IP-PACK中",
                //"CP-CANCEL",
                //"HO-HOLD"
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and b.Status = :Status";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Status", strStatus };
                iPara = iPara + 1;
            }

            if (cmbSTATUS.Text.Contains("ALL") || cmbSTATUS.Text.Contains("UF-"))
            {
                //出货开始日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and b.SHIPPING_TIME >= :shipmentTime";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "shipmentTime", dt_start.Value };
                iPara = iPara + 1;

                //出货结束日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and b.SHIPPING_TIME <= :shipmentTime2";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "shipmentTime2", dt_end.Value };
                iPara = iPara + 1;
            }

            //地区查询条件
            if (cmbREGION.Text.Trim() != "" && cmbREGION.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and b.Region = :Region";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Region", cmbREGION.Text };
                iPara = iPara + 1;
            }
            //添加排序
            g_SQL += " order by b.shipping_time asc, a.pallet_no";
            DataTable db = ClientUtils.ExecuteSQL(g_SQL, sqlparams).Tables[0];
            dgvNo.DataSource = db;
            dgvNo.AutoResizeColumns();

            if (db.Rows.Count > 0)
            {
                cmbSmid.Items.Clear();
                cmbSmid.Items.Add("-ALL-");

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

                //如果shipment不是ALL 且上面
                if (cmbSmid.Text.Trim() != "" && cmbSmid.Text.Trim() != "-ALL-")
                {
                    string strSID = cmbSmid.Text.Trim();
                    showdgvHold(strSID, strDBtype);

                }
                else { ShowMsg("查询OK", 0); }



            }
            else
            {
                dgvNo.DataSource = null;
                ShowMsg("查询无资料", 0);
            }


        }
        private void showdgvHold(string sid, string strDBtype)
        {
            dgvHold.DataSource = null;
            QHoldBll rb = new QHoldBll();
            DataTable dtHold = rb.GetHoldInfoDataTable(sid, strDBtype);
            if (dtHold != null && dtHold.Rows.Count > 0)
            {
                dgvHold.DataSource = dtHold;
                dgvHold.AutoResizeColumns();
            }

        }

        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt.TP();
            switch (strType)
            {
                case 0: //Error    
                    //LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    TextMsg.ForeColor = Color.Red;
                    //TextMsg.BackColor = Color.Silver;
                    TextMsg.BackColor = Color.Blue;
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

        private void btnClsFace_Click(object sender, EventArgs e)
        {
            cmbSmid.Text = "-ALL-";
            cmbCarrier.Text = "-ALL-";
            cmbPOE.Text = "-ALL-";
            cmbREGION.Text = "-ALL-";
            cmbSTATUS.Text = "-ALL-";
            dgvNo.DataSource = null;
            dgvHold.DataSource = null;
            ShowMsg("", 0);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ShowMsg("", -1);

            string strSID = cmbSmid.Text;

            //检查dgv数据,
            if (dgvNo.RowCount == 0 || dgvHold.RowCount == 0)
            {
                ShowMsg("请先查找合适的集货单号，才能开始作业。", 0);
                return;
            }

            //检查集货单号状态是否能替换。
            //只有WP IP  FP 才能做次作业，  HO的状态为PM锁定， ZC和箱替换都停止。

            if (!(cmbSTATUS.Text.Contains("WP") || cmbSTATUS.Text.Contains("IP") || cmbSTATUS.Text.Contains("FP")))
            {
                ShowMsg("只有集货单号状态时WP/IP/FP，才能开始作业。", 0);
                return;
            }
            //可能要补上通过DB 返回状态， 而不是上面通过下拉框的值判定。


            //锁定界面，不能查询
            //cmbshipment 不能更改
            btnSearch.Enabled = false;
            btnClsFace.Enabled = false;
            cmbSmid.Enabled = false;
            cmbSTATUS.Enabled = false;
            txtHoldCarton.Enabled = true;
            txtNewCarton.Enabled = true;
            txtHoldPallet.Enabled = true;
            //如果检查通过
            txtHoldCarton.Focus();
            txtHoldCarton.SelectAll();


        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            //解锁界面，开始查询
            btnSearch.Enabled = true;
            btnClsFace.Enabled = true;
            cmbSmid.Enabled = true;
            cmbSTATUS.Enabled = true;
            txtHoldCarton.Text = "";
            txtNewCarton.Text = "";
            txtHoldPallet.Text = "";
            txtHoldCarton.Enabled = false;
            txtNewCarton.Enabled = false;
            txtHoldPallet.Enabled = false;

        }

        private void txtHoldCarton_KeyDown(object sender, KeyEventArgs e)
        {

            //检查序号如果序号是Hold 且属于SID ，则检查通过
            //锁定光标转到下面的textbox
            string strHoldCarton = txtHoldCarton.Text.Trim();

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);
            lblLocation.Text = "";
            lblLocationqty.Text = "";
            txtNewCarton.Text = "";

            if (string.IsNullOrEmpty(strHoldCarton))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的Carton不能为空！", 0);
                txtHoldCarton.Focus();
                txtHoldCarton.SelectAll();
                return;
            }

            QHoldBll qhb = new QHoldBll();
            strHoldCarton = qhb.DelPrefixCartonSN(strHoldCarton);


            //HYQ： 加一个判断，如果输入的序号不是ICT的 customerSN 或者cartonSN  直接return 
            #region
            string strSQL = string.Format("select distinct  carton_no "
                                          + "    from ppsuser.t_sn_status "
                                          + "   where customer_sn = '{0}' "
                                          + "      or carton_no = '{1}'", strHoldCarton, strHoldCarton);

            DataTable dt0 = new DataTable();
            try
            {
                dt0 = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
            }

            if (dt0.Rows.Count == 0)
            {
                ShowMsg("输入非法无效的序号或者箱号，不做统计。", 0);
                txtHoldCarton.Focus();
                txtHoldCarton.SelectAll();
                return;
            }
            //HYQ： 如果等于1 ，说明刷入的序号， single的箱号是空，继续保持序号， 如果箱号不为空， 则序号转成箱号处理。
            if (dt0.Rows.Count == 1)
            {
                string a = dt0.Rows[0]["carton_no"].ToString();
                if (!string.IsNullOrEmpty(a))
                {
                    strHoldCarton = dt0.Rows[0]["carton_no"].ToString();
                }
            }
            #endregion



            string strSID = cmbSmid.Text;

            QHoldBll qh = new QHoldBll();
            string errorMessage = string.Empty;
            string strResult = qh.CheckHoldCarton(strSID, strHoldCarton, out errorMessage);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(errorMessage, 0);
                txtHoldCarton.Focus();
                txtHoldCarton.SelectAll();
                return;
            }

            txtHoldCarton.Enabled = false;
            txtNewCarton.Enabled = true;
            txtNewCarton.Focus();
            txtNewCarton.SelectAll();
            ShowMsg("检查OK，请刷入新CartonNO", 0);

        }

        private void txtNewCarton_KeyDown(object sender, KeyEventArgs e)
        {
            //上面的序号再检查一边，备份
            //检查新序号
            //执行置换SP

            string strNewCarton = txtNewCarton.Text.Trim();

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);

            lblLocation.Text = "";
            if (string.IsNullOrEmpty(strNewCarton))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的NewCarton不能为空！", 0);
                txtNewCarton.Focus();
                txtNewCarton.SelectAll();
                return;
            }
            QHoldBll qhb = new QHoldBll();
            strNewCarton = qhb.DelPrefixCartonSN(strNewCarton);

            //HYQ： 加一个判断，如果输入的序号不是ICT的 customerSN 或者cartonSN  直接return 
            #region
            string strSQL = string.Format("select distinct carton_no "
                                          + "    from ppsuser.t_sn_status "
                                          + "   where customer_sn = '{0}' "
                                          + "      or carton_no = '{1}'", strNewCarton, strNewCarton);

            DataTable dt0 = new DataTable();
            try
            {
                dt0 = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
            }

            if (dt0.Rows.Count == 0)
            {
                ShowMsg("输入非法无效的序号或者箱号，不做统计。", 0);
                txtNewCarton.Focus();
                txtNewCarton.SelectAll();
                return;
            }
            //HYQ： 如果等于1 ，说明刷入的序号， single的箱号是空，继续保持序号， 如果箱号不为空， 则序号转成箱号处理。
            if (dt0.Rows.Count == 1)
            {
                if (!string.IsNullOrEmpty(dt0.Rows[0]["carton_no"].ToString()))
                {
                    strNewCarton = dt0.Rows[0]["carton_no"].ToString();
                }
            }
            #endregion

            //这里的时候 可能是CSN也可能是箱号 --用txtHoldCarton2
            string strHoldCarton = txtHoldCarton.Text.Trim();
            strHoldCarton = qhb.DelPrefixCartonSN(strHoldCarton);
            string strSID = cmbSmid.Text;

            QHoldBll qh = new QHoldBll();
            string errorMessage = string.Empty;
            string strlocationinfo = string.Empty;
            string strResult = qh.ReplaceHoldCarton(strSID, strNewCarton, strHoldCarton, out strlocationinfo, out errorMessage);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(errorMessage, 0);
                txtNewCarton.Focus();
                txtNewCarton.SelectAll();
                return;
            }
            if (!string.IsNullOrEmpty(strlocationinfo))
            {
                string[] locationinfo = strlocationinfo.Split('|');
                string strlocation = locationinfo[0];
                string strlocationqty = locationinfo[1];
                lblLocation.Text = "储位:" + strlocation;
                lblLocationqty.Text = "剩余箱数:" + strlocationqty;
            }


            showdgvHold(strSID, strDBtype);

            if (dgvHold.DataSource == null)
            {
                MessageBox.Show("该集货单号替换完成。".TL());
                btnSearch.Enabled = true;
                btnClsFace.Enabled = true;
                cmbSmid.Enabled = true;
                txtHoldCarton.Text = "";
                txtHoldCarton.Enabled = false;
                txtNewCarton.Text = "";
                txtNewCarton.Enabled = false;
            }
            else
            {
                txtHoldCarton.Text = "";
                txtHoldCarton.Enabled = true;
                txtNewCarton.Text = "";
                txtNewCarton.Enabled = false;
                txtHoldCarton.Focus();
                txtHoldCarton.SelectAll();
                ShowMsg("置换OK", 0);
            }



        }

        private void btnMail_Click(object sender, EventArgs e)
        {

            string HAWB = "AA";
            string REGION = "BB";
            string carrier = "CC";
            string Air = "DD";
            List<string> filePath = new List<string>();
            string diskPath = @"D:\TEST_DEVELOPMENT\TESTMAIL\a.txt";
            string diskPath1 = @"D:\TEST_DEVELOPMENT\TESTMAIL\b.txt";
            filePath.Add(diskPath);
            filePath.Add(diskPath1);

            XMLHelper xmlh = new XMLHelper();
            XMLHelper.EmailModel model = new XMLHelper.EmailModel();
            model = xmlh.PPSemail();
            //设置发件人信箱,及显示名字 
            MailAddress from = new MailAddress(model.FromEmail, model.FromPerson);
            //设置收件人信箱,及显示名字 
            MailAddress to = new MailAddress(model.ToEmail, model.ToPerson);
            //创建一个MailMessage对象 
            MailMessage oMail = new MailMessage(from, to);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            //设置收件人,可添加多个,添加方法与下面的一样
            oMail.CC.Add(model.EmailCC);
            oMail.Subject = "" + "FDORDS" + " TEST " + date + " " + HAWB + " Import Customs Clearance Doc. ICT Ship to  " + REGION + " " + Air + "  " + carrier + ""; //邮件标题 
            oMail.Body = @"<!DOCTYPE html><html><body><p> Dear " + carrier + " team，</p><br/>"
             + "<p> &nbsp; &nbsp; &nbsp; 附件是" + date + "出货目的港清关文件，请查收，谢谢！</p>"
             + "<br/><br/><br/><p> With Best regards</p>"
             + "<p> 立讯电子科技（昆山）有限公司 </p>"
             + "<p> LUXSHARE ELECTRONIC TECHNOLOGY(KUNSHAN)LTD.</p>"
             + "<p><img src='http://www.luxshare-ict.com/images/logo.png'/></p></body></html>";
            oMail.IsBodyHtml = true; //指定邮件格式,支持HTML格式 
            foreach (string EmailFile in filePath)
            {
                System.Net.Mail.Attachment mailAttach_1 = new Attachment(EmailFile);//附件
                oMail.Attachments.Add(mailAttach_1);
            }
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding(model.Encoding);//邮件采用的编码 
            oMail.Priority = MailPriority.High;//设置邮件的优先级为高 
            SmtpClient client = new SmtpClient();
            //client.Host = model.SmtpServer; //指定邮件服务器 ??
            client.Host = "10.33.22.101";
            //model.SmtpServer = "dgmail.luxshare-ict.com";
            //model.SmtpServer = "203.70.94.54";
            //192.168.20.40
            client.Port = 25;
            try
            {
                client.UseDefaultCredentials = true;
                NetworkCredential senderCredential = new NetworkCredential(model.UserName, model.PassWord);//指定服务器邮件,及密码 
                client.Credentials = senderCredential;
                client.Send(oMail);//发送邮件 
                MessageBox.Show("发送成功!");
                oMail.Dispose(); //释放资源  
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败!" + ex.Message.ToString());
            }



        }

        private void txtHoldPallet_KeyDown(object sender, KeyEventArgs e)
        {
            string strHoldPallet = txtHoldPallet.Text.Trim();

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            ShowMsg("", -1);

            if (string.IsNullOrEmpty(strHoldPallet))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的Hold 栈板号不能为空！", 0);
                txtHoldPallet.Focus();
                txtHoldPallet.SelectAll();
                return;
            }


            //HYQ： 加一个判断，如果输入的序号不是ICT的 pallet_no  直接return 
            #region
            //string strSQL = string.Format("select distinct pallet_no "
            //                              + "    from ppsuser.t_sn_status "
            //                              + "   where pallet_no = '{1}'", strHoldPallet);

            //DataTable dt0 = new DataTable();
            //try
            //{
            //    dt0 = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            //}
            //catch (Exception ex)
            //{
            //    ShowMsg(ex.ToString(), 0);
            //}

            //if (dt0.Rows.Count == 0)
            //{
            //    ShowMsg("输入非法无效pallet_no，不做统计。", 0);
            //    txtHoldPallet.SelectAll();
            //    txtHoldPallet.Focus();
            //    return;
            //}
            ////HYQ： 如果等于1 ，说明刷入pallet_no 有效。
            //if (dt0.Rows.Count == 1)
            //{
            //    if (!string.IsNullOrEmpty(dt0.Rows[0]["pallet_no"].ToString()))
            //    {
            //        strHoldPallet = dt0.Rows[0]["pallet_no"].ToString();
            //    }
            //}
            #endregion

            //可以把这个改下， palletno不在dgvHold里面，就报错。
            string isbelong = string.Empty;
            if (dgvHold.Rows.Count > 0)
            {
                for (int i = 0; i < dgvHold.Rows.Count - 1; i++)
                {
                    string aa = dgvHold.Rows[i].Cells["pallet_no"].Value.ToString();
                    if (!string.IsNullOrEmpty(aa))
                    {
                        if (aa.Contains(strHoldPallet))
                        {
                            isbelong = "YES";
                            break;
                        }
                        if (aa.Contains(strHoldPallet.Substring(2)))
                        {
                            isbelong = "YES";
                            strHoldPallet = strHoldPallet.Substring(2);
                            break;
                        }
                    }
                }

            }
            if (string.IsNullOrEmpty(isbelong))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByNg();
                this.ShowMsg("输入的栈板号不属于此集货单号！", 0);
                txtHoldPallet.Focus();
                txtHoldPallet.SelectAll();
                return;
            }

            string strSID = cmbSmid.Text;
            QHoldBll qh = new QHoldBll();
            string errorMessage = string.Empty;
            string strResult = qh.ZCHoldPallet(strSID, strHoldPallet, out errorMessage);
            if (!strResult.Equals("OK"))
            {
                ShowMsg(errorMessage, 0);
                txtHoldPallet.Focus();
                txtHoldPallet.SelectAll();
                return;
            }

            showdgvHold(strSID, strDBtype);

            if (dgvHold.DataSource == null)
            {
                MessageBox.Show("该集货单号Hold清除完成。".TL());
                btnSearch.Enabled = true;
                btnClsFace.Enabled = true;
                cmbSmid.Enabled = true;
                txtHoldCarton.Text = "";
                txtHoldCarton.Enabled = false;
                txtNewCarton.Text = "";
                txtNewCarton.Enabled = false;
                txtHoldPallet.Text = "";
                txtHoldPallet.Enabled = false;
            }
            else
            {
                txtHoldPallet.Text = "";
                txtHoldPallet.Enabled = true;
                txtHoldPallet.Focus();
                txtHoldPallet.SelectAll();
                ShowMsg("栈板还原完成", 0);
            }


        }

        private void btnToHold_Click(object sender, EventArgs e)
        {

            TrantoHoldCarton thc = new TrantoHoldCarton();
            thc.ShowDialog();
        }


    }
}
