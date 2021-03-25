using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReverseAC;
using BarcodeLib;


namespace PickListAC
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }
        private string g_SQL;
        private Int32 g_curRow = -1;    //当前选中行号
        private string g_partNo = "";   //当前选中的料号

        private string g_sUserID = ClientUtils.UserPara1;
        private string g_sUserNo = ClientUtils.fLoginUser;
        private string g_sProgram = ClientUtils.fProgramName;
        private string g_sFunction = ClientUtils.fFunctionName;
        private string g_sExeName = ClientUtils.fCurrentProject;
        private string g_ServerIP = ClientUtils.url;

        //保存查询出来的PickList用于过滤
        string StrIniFile = Application.StartupPath + "\\sajet.ini";
        string StrData;
        //窗体长高
        public int H = 0;
        string strLocalMACADDRESS = string.Empty;
        
        /// <summary>
        /// 设置文本框美式键盘
        /// </summary>
        private void SeachTxt()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    c.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
                if (c is ComboBox)
                {
                    c.ImeMode = System.Windows.Forms.ImeMode.Disable;
                }
            }
        }
        
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fMain_Load(object sender, EventArgs e)
        {
            //HYQ:fLoad()  设定了panel2.Size
            fLoad();

            //HYQ:以下初始设定了//DtbPickList+DtbPick+DtbQtys 3个datagriadview的列名
            InitaildtResult();
         
            this.Text = this.Text + " (" + SajetCommon.g_sFileVersion + ")";
            labShow.Text= labShow.Text + " (" + SajetCommon.g_sFileVersion + ")";
            SeachTxt();

            DateTime dateTimeNow = DateTime.Now;
            //dt_start.Value = new DateTime(dateTimeNow.Year, dateTimeNow.Month - 2, 1);
            dt_start.Value = dateTimeNow.AddDays(-1);
            dt_end.Value = dateTimeNow.AddDays(1);
            this.WindowState = FormWindowState.Maximized;
            //SajetInifile sajetInifile1 = new SajetInifile();
            //StrData = sajetInifile1.ReadIniFile(StrIniFile, "System", "Data", "Prod").ToLower();
            strLocalMACADDRESS = LibHelperAC.LocalHelper.getMacAddr_Local();


            string strResult = string.Empty;
            string strOutparavalue = string.Empty;
            string strResulterrmsg = string.Empty;
            PickListBll pb = new PickListBll();
            strResult = pb.GetDBType("DB_TYPE", out strOutparavalue, out strResulterrmsg);
            if (strResult.Equals("OK") && strOutparavalue.Equals("TEST"))
            {
                btnGetSN.Visible = Enabled;
                btnGetSN.Enabled = Enabled;
            }
            else
            {
                btnGetSN.Visible = false;
                btnGetSN.Enabled = false;
            }
        }
        private void fLoad()
        {
            H = Screen.PrimaryScreen.Bounds.Height;
            if (H >= 1080)
            {
                H = Convert.ToInt32(H * 0.30);
            }
            else
            {
                H = Convert.ToInt32(H * 0.22);

            }
            this.panel2.Size = new System.Drawing.Size(1300, H);
        }
        /// <summary>
        /// gridview 初始化
        /// </summary>
        private void InitaildtResult()
        {
            clearTxt();
            clearCmb();
        }
       
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            object[][] sqlparams = new object[0][];     //查询条件传参数组
            string strSQL = createSQL(out sqlparams);
            DataTable db = ClientUtils.ExecuteSQL(strSQL, sqlparams).Tables[0];
            dgvNo.DataSource = null;
            dgvNo.Rows.Clear();
            refreshCmbbox();
            if (db.Rows.Count > 0)
            {
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvNo.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    //累加序号
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = db.Rows[i]["集货单号"].ToString();
                    dr.Cells[1].Value = Convert.ToDateTime(db.Rows[i]["出货时间"]).ToString("yyyy/MM/dd");
                    dr.Cells[2].Value = db.Rows[i]["CARRIER"].ToString();
                    dr.Cells[3].Value = db.Rows[i]["POE"].ToString();
                    dr.Cells[4].Value = db.Rows[i]["地区"].ToString();
                    dr.Cells[5].Value = db.Rows[i]["栈板号"].ToString();
                    dr.Cells[6].Value = db.Rows[i]["栈板类型"].ToString();
                    dr.Cells[7].Value = db.Rows[i]["MPN"].ToString();
                    dr.Cells[8].Value = db.Rows[i]["料号"].ToString();
                    dr.Cells[9].Value = db.Rows[i]["数量"].ToString();
                    dr.Cells[10].Value = db.Rows[i]["箱数"].ToString();
                    dr.Cells[11].Value = db.Rows[i]["已PICK箱数"].ToString();
                    dr.Cells[12].Value = db.Rows[i]["PART_PICK_STATUS"].ToString();
                    dr.Cells[13].Value = db.Rows[i]["PALLET_PICK_STATUS"].ToString();
                    dr.Cells[14].Value = db.Rows[i]["PACKCODEDESC"].ToString();
                    dr.Cells[15].Value = db.Rows[i]["PRIORITY"].ToString();
                    
                    try
                    {
                        dgvNo.Invoke((MethodInvoker)delegate ()
                        {
                            dgvNo.Rows.Add(dr);
                            if (!cmbSmid.Items.Contains(db.Rows[i]["集货单号"].ToString()))
                            {
                                cmbSmid.Items.Add(db.Rows[i]["集货单号"].ToString());
                            }
                            //if (!cmbRegion.Items.Contains(db.Rows[i]["地区"].ToString()) && !string.IsNullOrEmpty(db.Rows[i]["地区"].ToString()))
                            //{
                            //    cmbRegion.Items.Add(db.Rows[i]["地区"].ToString());
                            //}
                            if (!cmbPOE.Items.Contains(db.Rows[i]["POE"].ToString()) && !string.IsNullOrEmpty(db.Rows[i]["POE"].ToString()))
                            {
                                cmbPOE.Items.Add(db.Rows[i]["POE"].ToString());
                            }
                            if (!cmbCarrier.Items.Contains(db.Rows[i]["CARRIER"].ToString()) && !string.IsNullOrEmpty(db.Rows[i]["CARRIER"].ToString()))
                            {
                                cmbCarrier.Items.Add(db.Rows[i]["CARRIER"].ToString());
                            }
                            if (db.Rows[i]["PALLET_PICK_STATUS"].ToString().Contains("WP"))
                            {
                                dr.DefaultCellStyle.BackColor = Color.White;
                            }
                            else if (db.Rows[i]["PALLET_PICK_STATUS"].ToString().Contains("IP"))
                            {
                                dr.DefaultCellStyle.BackColor = Color.Yellow;
                            }
                            else if (db.Rows[i]["PALLET_PICK_STATUS"].ToString().Contains("FP"))
                            {
                                dr.DefaultCellStyle.BackColor = Color.Green;
                            }
                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                }
                ShowMsg("", -1);
                autoselectLine();
            }
            else
            {
                ShowMsg("查询无资料", 0);
            }
            btnSearch.Enabled = true;
        }

        private string createSQL(out object[][] sqlparams)
        {
            g_SQL = " 	 Select a.Shipment_ID 集货单号, "
                    + "       c.shipping_time 出货时间, "
                    + "       c.Carrier_Name AS Carrier, "
                    + "       c.POE, "
                    + "       c.Region 地区, "
                    + "       a.Pallet_NO 栈板号, "
                    + "       case "
                    + "         when a.PALLET_TYPE = '001' then "
                    + "          'NO MIX' "
                    + "         when a.PALLET_TYPE = '999' then "
                    + "          'MIX' "
                    + "         else "
                    + "          a.PALLET_TYPE "
                    + "       end 栈板类型, "
                    + "       (select distinct custpart from NONEDIOMS.oms_partmapping  where part= b.ictpn)  MPN,  "
                    + "       b.ICTPN 料号, "
                    + "       b.QTY 数量, "
                    + "       b.CARTON_QTY 箱数, "
                    + "       b.PICK_CARTON 已Pick箱数, "
                    + "       case "
                    + "         when b.pick_status = 'WP' then "
                    + "          'WP-未拣货' "
                    + "         when b.pick_status = 'IP' then "
                    + "          'IP-拣货中' "
                    + "         when b.pick_status = 'FP' then "
                    + "          'FP-已拣货' "
                    + "         when b.pick_status = 'HO' then "
                    + "          'HO-QHold' "
                    + "         else "
                    + "          b.pick_status "
                    + "       end Part_PICK_Status, "
                    + "       case "
                    + "         when a.pick_status = 'WP' then "
                    + "          'WP-未拣货' "
                    + "         when a.pick_status = 'IP' then "
                    + "          'IP-拣货中' "
                    + "         when a.pick_status = 'FP' then "
                    + "          'FP-已拣货' "
                    + "         when a.pick_status = 'HO' then "
                    + "          'HO-QHold' "
                    + "         else "
                    + "          a.pick_status "
                    + "       end Pallet_PICK_Status   ,a.pack_code ,d.remark PACKCODEDESC,c.priority PRIORITY "
                    + "  from NONEDIPPS.T_SHIPMENT_PALLET a "
                    + " inner join NONEDIPPS.T_SHIPMENT_PALLET_PART b "
                    + "    on a.pallet_no = b.PALLET_NO "
                    + " inner join NONEDIPPS.T_SHIPMENT_INFO c "
                    + "    on a.SHIPMENT_ID = c.shipment_id "
                    + " left join (select e.packcode, min(e.remark) remark "
                    + "          from(select distinct packcode, PALLETLENGTHCM || '*' || PALLETWIDTHCM as remark from NONEDIPPS.vw_mpn_info)e "
                    + "        group by packcode) d "
                    + "    on a.pack_code = d.packcode "
                    + " where c.status not in('WS','SF') and 1 = 1";
            //组合输入查询条件，过滤数据源
            //出货类型查询条件
            string strShipmentType;
            if (radDS.Checked)
                strShipmentType = radDS.Text;
            else
                strShipmentType = radFD.Text;

            sqlparams = new object[0][];
            Array.Resize(ref sqlparams, sqlparams.Length + 1);

            g_SQL += " and c.SHIPMENT_TYPE = :shipmentType";

            int iPara = 0;                              //变量项次
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentType", strShipmentType };
            iPara = iPara + 1;

            //集货单号查询条件
            if (cmbSmid.Text.Trim() != "" && cmbSmid.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and a.Shipment_ID = :shipmentID";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentID", cmbSmid.Text };
                iPara = iPara + 1;
            }

            //货代查询条件
            if (cmbCarrier.Text.Trim() != "" && cmbCarrier.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                //g_SQL += " and c.CARRIER_NAME = :carrier";
                //一个是CARRIER_NAME,一个是SCACCODE 筛选不到  Modified By KyLinQiu 20190615
                g_SQL += " AND C.CARRIER_NAME = :carrier ";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.VarChar, "carrier", cmbCarrier.Text };
                iPara = iPara + 1;
            }

            //港口查询条件
            if (cmbPOE.Text.Trim() != "" && cmbPOE.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.POE = :poe";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.VarChar, "poe", cmbPOE.Text };
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
                g_SQL += " and c.Status = :Status";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.VarChar, "Status", strStatus };
                iPara = iPara + 1;
            }

            if (cmbSTATUS.Text.Contains("ALL"))
            {
                //出货开始日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.SHIPPING_TIME >= :shipmentTime";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.DateTime, "shipmentTime", dt_start.Value };
                iPara = iPara + 1;

                //出货结束日期
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.SHIPPING_TIME <= :shipmentTime2";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.DateTime, "shipmentTime2", dt_end.Value };
                iPara = iPara + 1;
            }

            //地区查询条件
            if (cmbRegion.Text.Trim() != "" && cmbRegion.Text.Trim() != "-ALL-")
            {
                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                g_SQL += " and c.Region = :Region";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleType.VarChar, "Region", cmbRegion.Text };
                iPara = iPara + 1;
            }
            //添加排序
            g_SQL += " order by c.priority asc, a.pallet_no asc ,MPN desc, b.ictpn asc ";

            return g_SQL;
        }
        private void refreshCmbbox()
        {
            cmbSmid.Items.Clear();
            cmbSmid.Items.Add("-ALL-");

            //cmbRegion.Items.Clear();
            //cmbRegion.Items.Add("-ALL-");
            
            cmbPOE.DataSource = null;
            cmbPOE.Items.Clear();
            cmbPOE.Items.Add("-ALL-");
            
            cmbCarrier.DataSource = null;
            cmbCarrier.Items.Clear();
            cmbCarrier.Items.Add("-ALL-");
            

        }
        private void autoselectLine()
        {
            //自动跳到未完成的首笔记录
            for (int i = 0; i < dgvNo.RowCount; i++)
            {
                if (!(dgvNo.Rows[i].DefaultCellStyle.BackColor == Color.Green))
                {
                    dgvNo.Rows[i].Selected = true;
                    dgvNo.FirstDisplayedScrollingRowIndex = i;
                    //this.dgvNo.CurrentCell = this.dgvNo.Rows[i].Cells[0];
                    string setPart = dgvNo.Rows[i].Cells["料号"].Value.ToString();
                    string l_smid = dgvNo.Rows[i].Cells["集货单号"].Value.ToString();
                    string l_pallet = dgvNo.Rows[i].Cells["栈板号"].Value.ToString();
                    string l_carrier = dgvNo.Rows[i].Cells["Carrier"].Value.ToString();
                    string l_poe = dgvNo.Rows[i].Cells["POE"].Value.ToString();
                    string l_Qty = dgvNo.Rows[i].Cells["箱数"].Value.ToString();
                    string remark = dgvNo.Rows[i].Cells["PACKCODEDESC"].Value.ToString();
                    string priority = dgvNo.Rows[i].Cells["priority"].Value.ToString();

                    //2 更新作业显示内容
                    showValue(l_smid, l_pallet, l_carrier, l_poe, l_Qty, setPart, remark, priority);

                    PickListBll pb2 = new PickListBll();
                    //pb2.ShowStockInfo(setPart ,dgvStock);
                    pb2.ShowStockCarInfo(setPart, l_pallet, dgvStock);
                    break;
                }
            }
        }
        
        /// <summary>
        /// combox清空
        /// </summary>
        private void clearCmb()
        {
            //this.dt_start.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            //this.dt_end.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cmbSmid.Text = "-ALL-";
            cmbCarrier2.Text = "";
        }
        /// <summary>
        /// dgv清空
        /// </summary>
        private void clearDgv()
        {
            this.dgvNo.DataSource = null;
            this.dgvStock.DataSource = null;
            this.dgvPick.DataSource = null;
            dgvNo.Rows.Clear();
            dgvStock.Rows.Clear();
            dgvPick.Rows.Clear();
        }

        /// <summary>
        /// txt清空
        /// </summary>
        private void clearTxt()
        {
            //txtCountry.Text = string.Empty;
            txtPallet.Text = string.Empty;
            txtPoe.Text = string.Empty;
            //txtRegion.Text = string.Empty;
            txtSmId.Text = string.Empty;
            txtCarton.Text = string.Empty;
            txtPick.Text = string.Empty;
            txtSmId.ReadOnly = true;
            //txtRegion.ReadOnly = true;
            //txtCountry.ReadOnly = true;
            txtPallet.ReadOnly = true;
            //txtPoe.ReadOnly = true;
            txtSmId.BackColor = System.Drawing.Color.Silver;
            //txtRegion.BackColor = System.Drawing.Color.Silver;
            //txtCountry.BackColor = System.Drawing.Color.Silver;
            txtPallet.BackColor = System.Drawing.Color.Silver;
            //txtPoe.BackColor = System.Drawing.Color.Silver;
            txtCarton.BackColor = System.Drawing.SystemColors.Window;
            //txtLOCATION_NO.BackColor = System.Drawing.SystemColors.Window;
        }


        /// <summary>
        /// pickList类
        /// </summary>
        picklist List = new picklist();

        /// <summary>
        /// 开始pick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarton_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime dateStart = DateTime.Now;
            //可扫描序号/箱号/栈板号进行Pick
            string strCarton = txtCarton.Text.Trim();
            PickListBll plb = new PickListBll();
            strCarton = plb.DelPrefixCartonSN(strCarton); 
           
            if (e.KeyCode != Keys.Enter)
            { return; }
            ShowMsg("", -1);
            if (string.IsNullOrEmpty(strCarton))
            {
                //LibHelper.MediasHelper.PlaySoundAsyncByNg();
                //this.ShowMsg("输入的/PalletID/SN/Carton不能为空！", 0);
                txtCarton.SelectAll();
                txtCarton.Focus();
                return;
            }
            DataTable dt0 = plb.GetSNInfoDataTableBLL(strCarton);
            if (dt0 == null)
            {
                LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg("输入非法无效的序号或者箱号，不做统计。", 0);
                txtCarton.Text = "";
                txtCarton.Focus();
                return;
            }
            //HYQ： 如果等于1 ，说明刷入的序号， single的箱号是空，继续保持序号， 如果箱号不为空， 则序号转成箱号处理。
            if (dt0.Rows.Count == 1)
            {
                if (!string.IsNullOrEmpty(dt0.Rows[0]["carton_no"].ToString()))
                {
                    strCarton = dt0.Rows[0]["carton_no"].ToString();
                }
            }
            // HYQ：20181102
            //添加QHold 检查
            //ReverseBll.CheckHold(string Sno, string Type, out string errorMessage)
            //Type有: 'SHIPMENT', 'PICKPALLETNO', 'PACKPALLETNO', 'SERIALNUMBER'
            string errorMessage = "";
            bool CheckHoldOK = ReverseBll.CheckHold(strCarton, "SERIALNUMBER", out errorMessage);
            if (!CheckHoldOK)
            {
                Ng();
                ShowMsg(errorMessage, 0);
                txtCarton.Text = "";
                txtCarton.Focus();
                return;
            }
            //绑定电脑名和获取pick_pallet_no
            //HYQ:  SPname :create or replace procedure SP_PICK_INSERTPALLETPICK(pickpalletno in out  varchar2,
            //                                         palletno     in varchar2,
            //                                         snOrCartonno in varchar2,
            //                                         empno        in varchar2,
            //                                         errmsg       out varchar2,
            //                                         strlbl       out varchar2) as
            //原先这个是SP  增加一个参数 ，显示PICKCARTON/TOTALPICKCARTON
            TimeSpan ts0 = DateTime.Now.Subtract(dateStart).Duration();
            string strTest = dateStart.ToString("HHmmss.fff") + "执行insertpickpallet前" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts0.Seconds.ToString() + "*" + ts0.Milliseconds.ToString();

            string strPalletno = txtPallet.Text.Trim();
            string strPickPalletno = txtPick.Text.Trim();

            object[][] procParams = new object[7][];
            string errormsg = "";
            string strlbl = string.Empty;
            
            //procParams[0] = new object[] { ParameterDirection.InputOutput, OracleType.VarChar, "pickpalletno", strPickPalletno };
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "inpickpalletno", strPickPalletno };
            procParams[1] = new object[] { ParameterDirection.Output, OracleType.VarChar, "pickpalletno", strPickPalletno };
            procParams[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "palletno", strPalletno };
            procParams[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "snOrCartonno", strCarton };
            procParams[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "empno", g_sUserNo };
            procParams[5] = new object[] { ParameterDirection.Output, OracleType.VarChar, "errmsg", errormsg };
            procParams[6] = new object[] { ParameterDirection.Output, OracleType.VarChar, "strlbl", strlbl };

            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("NONEDIPPS.SP_PICK_INSERTPALLETPICK2", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                ShowMsg(e1.ToString(), 0);
                this.txtCarton.SelectAll();
                this.txtCarton.Focus();
                return;
            }
            txtPick.Text = dt.Rows[0]["pickpalletno"].ToString();
            TimeSpan ts1= DateTime.Now.Subtract(dateStart).Duration();
            strTest += "\r\n执行insertpickpallet后" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts1.Seconds.ToString() + "*" + ts1.Milliseconds.ToString();

            string strF = string.Empty;
            #region
            if (dt.Rows[0]["errmsg"].ToString().Contains("OK"))
            {
                this.labqty.Text = dt.Rows[0]["strlbl"].ToString();
                labqty.Refresh();
                Ok();
                ShowMsg("OK", -1);

                string strResultInsertLog = string.Empty;
                string strResulterrmsg = string.Empty;
                PickListBll pb1 = new PickListBll();
                strResultInsertLog = pb1.PPSInsertWorkLogBy(strCarton, "PICK", strLocalMACADDRESS, out strResulterrmsg);
                txtCarton.SelectAll();
                txtCarton.Focus();
                btnStart.Enabled = false;
            }
            else if (dt.Rows[0]["errmsg"].ToString().Contains("NG"))
            {
                ShowMsg(dt.Rows[0]["errmsg"].ToString(), 0);
                if (dt.Rows[0]["errmsg"].ToString().Contains("序号料号"))
                {
                    LibHelperAC.MediasHelper.PlaySoundAsyncByPartError();
                }
                else if (dt.Rows[0]["errmsg"].ToString().Contains("序号重复刷入"))
                {
                    LibHelperAC.MediasHelper.PlaySoundAsyncByRe();
                }
                else
                {
                    LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                }

                txtCarton.SelectAll();
                btnStart.Enabled = false;
                txtCarton.Focus();
                return;
            }
            else if (dt.Rows[0]["errmsg"].ToString().Contains("FINISH"))
            {
                strF = "F";
                labqty.Text = dt.Rows[0]["strlbl"].ToString();
                labqty.Refresh();

                TimeSpan ts2 = DateTime.Now.Subtract(dateStart).Duration();
                strTest += "\r\n执行打印前" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts2.Seconds.ToString() + "*" + ts2.Milliseconds.ToString();

                //HYQ： 加一个如果满了就自动打印。
                string strPickpalletno = txtPick.Text.Trim();
                if (string.IsNullOrEmpty(strPickpalletno))
                {
                    ShowMsg("PickPalletNO 不能为空", 0);
                    return;
                }
                string strResultInsertLog = string.Empty;
                string strResulterrmsg = string.Empty;
                PickListBll pb1 = new PickListBll();
                strResultInsertLog = pb1.PPSInsertWorkLogBy(strCarton, "PICK", strLocalMACADDRESS, out strResulterrmsg);


                Ok();
                PickPalletLabel ppl = new PickPalletLabel();
                #region
                if (ppl.PrintPickPalletLabel_new(strPickpalletno))
                {
                    TextMsg.Text = "打印OK";
                    endprintaction();
                }
                else
                {
                    ShowMsg("打印连接出了问题", 0);
                    //LibHelper.MediasHelper.PlaySoundAsyncByNg();
                    txtCarton.Text = "";
                    btnStart.Enabled = false;
                    return;
                }
                #endregion
                TimeSpan ts3 = DateTime.Now.Subtract(dateStart).Duration();
                strTest += "\r\n执行打印后" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts3.Seconds.ToString() + "*" + ts3.Milliseconds.ToString();

            }
            else
            {
                ShowMsg("检查SN或者CARTONNO获得特殊异常", 0);
                txtCarton.Text = "";
                return;
            }
            #endregion
            refresh_dgvPick2New(strPalletno);
            //加一段自动跳dgv index 的动作
            //如果当前pallet没有做完就不动；如果做完了就循环找下下一条没有找完的。
            if (strF.Contains("F"))
            {
                autoselectLine();
            }
            else
            {
                PickListBll pb2 = new PickListBll();
                //pb2.ShowStockInfo(strCarton, dgvStock);
                pb2.ShowStockCarInfo(strCarton, strPalletno, dgvStock);
            }

            txtCarton.Text = "";
            txtCarton.Focus();
            TimeSpan ts4 = DateTime.Now.Subtract(dateStart).Duration();
            strTest += "\r\n执行SP后界面刷新" + DateTime.Now.ToString("HHmmss.fff") + "|" + ts4.Seconds.ToString() + "*" + ts4.Milliseconds.ToString();
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\Shipping\Label";
            string str8= Path.GetFullPath(strLabelPath) + @"\" + strCarton + DateTime.Now.ToString("yyyyMMddHHmmssms") + ".txt";
            //this.WriteToPrintGo(str8, strTest);

            //MessageBox.Show(strTest);
            //TimeSpan ts = DateTime.Now.Subtract(dateStart).Duration();
            //MessageBox.Show(dateStart.ToString("HHmmss.fff")+"|"+ DateTime.Now.ToString("HHmmss.fff") +"|"+ts.Seconds.ToString() + "*" + ts.Milliseconds.ToString());

        }
        private void WriteToPrintGo(string sFile, string sData)
        {
            try
            {
                if (File.Exists(sFile))
                {
                    File.Delete(sFile);
                }
                File.AppendAllText(sFile, sData, Encoding.Default);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
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
                ShowMsg(ex.Message, 0);
            }
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dt">datatable</param>
        public void ExportExcel(DataGridView dt)
        {
            //获取导出路径
            string filePath = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "EXCEL 97-2007 工作簿(*.xls)|*.xls";//设置文件类型
            sfd.FileName = "PickList";//设置默认文件名
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
            ISheet sheet = string.IsNullOrEmpty("PickList") ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet("PickList");

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int c = 0; c < dt.Columns.Count; c++)
            {

                ICell cell = row.CreateCell(c);
                cell.SetCellValue(dt.Columns[c].HeaderText);
            }

            //数据   //如果是最后一行，加一行， 如果行数大于1 且与最后一行
            int realrow = dt.Rows.Count;
            //shipmentid 唯一值的个数
            int m = 0;
            //当前shipmetid
            string curshipmentid = string.Empty;
            string strPARCELBulk = string.Empty;

            for (int i = 0; i < realrow; i++)
            {
                if (!curshipmentid.Equals(dt.Rows[i - m].Cells[0].Value.ToString()))
                {
                    //写值
                    curshipmentid = dt.Rows[i - m].Cells[0].Value.ToString();
                    IRow row2 = sheet.CreateRow(i + 1);
                    row2.Height = 480;
                    ICell cell2 = row2.CreateCell(0);
                    PickListBll plb = new PickListBll();
                    DataTable dt0 = plb.GetShipmentTypeBll(curshipmentid);
                  
                    if (dt0 == null)
                    {
                        LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
                        return;
                    }
                    //HYQ： 如果等于1 ，说明刷入的序号， single的箱号是空，继续保持序号， 如果箱号不为空， 则序号转成箱号处理。
                    if (dt0.Rows.Count == 1)
                    {
                        if (!string.IsNullOrEmpty(dt0.Rows[0]["type"].ToString()))
                        {
                            strPARCELBulk = dt0.Rows[0]["type"].ToString();
                        }
                    }
                    //cell7.SetCellValue(strPARCELBulk);
                    //设置样式

                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    IFont font = workbook.CreateFont();
                    font.FontName = "Code 128";
                    font.FontHeightInPoints = 38;
                    //cellStyle.SetFont(font);
                    cell2.CellStyle = cellStyle;
                    //cell2.SetCellValue(curshipmentid);
                    try
                    {
                        Image image = GenerateBarCodeBitmap(curshipmentid);
                        MemoryStream ms = new MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bytes = ms.ToArray();
                        int pictureIdx = workbook.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);
                        HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                        HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, i + 1, 1, i + 2);
                        //##处理照片位置，【图片左上角为（col, row）第row+1行col+1列，右下角为（ col +1, row +1）第 col +1+1行row +1+1列，宽为100，高为50
                        HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                        pict.Resize();//这句话一定不要，这是用图片原始大小来显示

                    }
                    catch (Exception)
                    {
                        cell2.SetCellValue(curshipmentid);
                    }


                    i += 1;
                    m += 1;
                    realrow += 1;
                }

                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j <= dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    if (j < dt.Columns.Count) {
                        if (!string.IsNullOrEmpty(dt.Rows[i - m].Cells[0].Value.ToString()))
                        {
                            cell.SetCellValue(dt.Rows[i - m].Cells[j].Value.ToString());
                        }
                        else
                        {
                            cell.SetCellValue("");
                        }
                    }
                    if(j == dt.Columns.Count)
                    {
                        cell.SetCellValue(strPARCELBulk);
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
                this.ShowMsg("导出Excel成功！", -1);
                Ok();
            }
        }

        public static Image GenerateBarCodeBitmap(string content)
        {
            using (var barcode = new Barcode()
            {
                IncludeLabel = false,    //ture 的话会显示 图片会显示文字部分
                Alignment = AlignmentPositions.CENTER,
                Width = 300,
                Height = 30,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
            })
            {
                return barcode.Encode(TYPE.CODE128B, content);
            }
        }

        private void showdgvNoColor()
        {
            #region
            for (int i = 0; i < dgvNo.Rows.Count; i++)
            {
                this.dgvNo.Rows[i].HeaderCell.Value = (i + 1).ToString();
                ////按条件设置每一行的颜色属性
                //this.dgvNo.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                if (dgvNo.Rows[i].Cells["Pallet_PICK_Status"].Value.ToString().Contains("WP"))
                {
                    this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else if (dgvNo.Rows[i].Cells["Pallet_PICK_Status"].Value.ToString().Contains("IP"))
                {
                    this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (dgvNo.Rows[i].Cells["Pallet_PICK_Status"].Value.ToString().Contains("FP"))
                {
                    this.dgvNo.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
            #endregion
        }

        private void showValue(string smid, string pallet, string carrier, string poe, string qty, string IctNo,string packcode,string priority)
        {
            lblPriority.BackColor = Color.White;
            lblPriority.ForeColor = Color.Green;
          

            //更新待开始作业的显示内容
            txtSmId.Text = smid;
            txtPallet.Text = pallet;
            cmbCarrier2.Text = carrier;
            txtPoe.Text = poe;
            //labDefQty.Text = qty;
            //txtIctNo.Text = IctNo;
            txtpackcode.Text = packcode;
            if (priority.Equals("0"))
            {
                lblPriority.BackColor = Color.Red;
                lblPriority.ForeColor = Color.White;
            }
            else if (priority.Equals("0.50"))
            {
                lblPriority.BackColor = Color.Yellow;
                lblPriority.ForeColor = Color.Blue;
            }
            else if (priority.Equals("1"))
            {
                lblPriority.BackColor = Color.Green;
                lblPriority.ForeColor = Color.Yellow;
            }
            lblPriority.Text = priority;

        }


        private void dgvNo_SelectionChanged(object sender, EventArgs e)
        {
            dgvPick.DataSource = null;
            dgvStock.DataSource = null;
            //集货清单选中的事件
            //1. 更新库存清单显示逻辑
            //2. 更新需作业集货单号、栈板号等显示内容
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvNo.CurrentRow.Index;
                //rowIndex = dgvNo.CurrentCell.RowIndex;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvNo.CurrentRow.Index >= 0)
            {
                //1.1 同一行，则返回
                if (g_curRow == rowIndex)
                    return;
                g_curRow = rowIndex;
                string setPart = dgvNo.Rows[rowIndex].Cells["料号"].Value.ToString();
                string l_smid = dgvNo.Rows[rowIndex].Cells["集货单号"].Value.ToString();
                string l_pallet = dgvNo.Rows[rowIndex].Cells["栈板号"].Value.ToString();
                string l_carrier = dgvNo.Rows[rowIndex].Cells["Carrier"].Value.ToString();
                string l_poe = dgvNo.Rows[rowIndex].Cells["POE"].Value.ToString();
                string l_Qty = dgvNo.Rows[rowIndex].Cells["箱数"].Value.ToString();
                string remark = dgvNo.Rows[rowIndex].Cells["PACKCODEDESC"].Value.ToString();
                string priority = dgvNo.Rows[rowIndex].Cells["priority"].Value.ToString();
                

                //2 更新作业显示内容
                showValue(l_smid, l_pallet, l_carrier, l_poe, l_Qty, setPart, remark,priority);

                //1.2 不同行，料号相同，也不更新库存信息
                //if (g_partNo == setPart && dgvStock.Rows.Count > 0)
                //    return;
                g_partNo = setPart;


                PickListBll pb = new PickListBll();

                string strReult = string.Empty;
                string strOuterrmsg = string.Empty;
                //strReult = pb.PPartPreAssinPalletNO(l_smid, out strOuterrmsg);

                //pb.ShowStockInfo(setPart, dgvStock);
                pb.ShowStockCarInfo(setPart, l_pallet, dgvStock);

            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            TextMsg.Text = "";

            //1. 检查
            //获得电脑名
            string localHostname = "";
            try
            {
                localHostname = System.Environment.MachineName;
            }
            catch (Exception ex)
            {
                ShowMsg("获取电脑名异常" + ex.ToString(), 0);
                return;
            }
            //检查必须要选择一个集货单号
            if (string.IsNullOrEmpty(txtSmId.Text.Trim()))
            {
                ShowMsg("请选择一个需作业的集货单号。", 0);
                return;
            }

            //检查必须要货代
            if (string.IsNullOrEmpty(cmbCarrier2.Text.Trim()))
            {
                ShowMsg("请联系OPM确认货代信息", 0);
                return;
            }
            ////检查必须要POE
            //if (string.IsNullOrEmpty(txtPoe.Text.Trim()))
            //{
            //    ShowMsg("请联系OPM确认POE/COC信息。", 0);
            //    return;
            //}
            string strPalletno = txtPallet.Text.Trim();
            string strShipmentid = txtSmId.Text.Trim();

            //HYQ：20181101
            //添加QHold 检查
            //ReverseBll.CheckHold(string Sno, string Type, out string errorMessage)
            //Type有: 'SHIPMENT', 'PICKPALLETNO', 'PACKPALLETNO', 'SERIALNUMBER'

            //a.检查SHIPMENT 

            string errorMessage = "";
            bool CheckHoldOK = ReverseBll.CheckHold(strShipmentid, "SHIPMENT", out errorMessage);
            if (!CheckHoldOK)
            {
                ShowMsg(errorMessage, 0);
                //MessageBox.Show("shipment is Hold.");
                return;
            }

            PickListBll pb11 = new PickListBll();

            string strReult = string.Empty;
            string strOuterrmsg = string.Empty;
            //strReult = pb11.PPartPreAssinPalletNO(strShipmentid, out strOuterrmsg);



            //绑定电脑名和获取pick_pallet_no
            //HYQ:  SPname :SP_PICK_CHECKPALLETSTATUS(string shipmentid, string palletno,string computername,out string errormsg)
            //private string g_sUserNo = ClientUtils.fLoginUser;
            //            shipmentid in varchar2,
            //palletno in  varchar2,
            //computername in varchar2,
            //errmsg out varchar2
            //给栈板绑定电脑名，更新pick_status

            object[][] procParams = new object[4][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentid", strShipmentid };
            procParams[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "palletno", strPalletno };
            procParams[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "computername", localHostname };
            procParams[3] = new object[] { ParameterDirection.Output, OracleType.VarChar, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("NONEDIPPS.SP_PICK_CHECKPALLETSTATUS", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                ShowMsg(e1.ToString(), 0);
                return;
            }
            if (dt.Rows[0]["errmsg"].ToString().Equals("OK"))
            {
                txtCarton.Enabled = false;
                txtCarton.Focus();
            }
            else if (dt.Rows[0]["errmsg"].ToString().Contains("NG-"))
            {
                ShowMsg(dt.Rows[0]["errmsg"].ToString(), 0);
                return;
            }
            else if (dt.Rows[0]["errmsg"].ToString().Contains("WA"))
            {
                DialogResult strResult = MessageBox.Show(dt.Rows[0]["errmsg"].ToString().Substring(3) + ",是否继续作业？", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (strResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                ShowMsg("检查palletno获得特殊异常", 0);
                return;
            }

            //给dgvPick写值, 刷新界面
            refresh_dgvPickNew(strPalletno);

            /// 2.锁定程式界面上半部分的功能>> 按钮失效;datagriadview 选择index 事件失效;...
            btnSearch.Enabled = false;
            btnClsFace.Enabled = false;
            btnReprint.Enabled = false;
            this.dgvNo.SelectionChanged -= new System.EventHandler(this.dgvNo_SelectionChanged);

            txtPick.Text = "";
            txtCarton.Enabled = true;
            txtCarton.Text = "";
            txtCarton.SelectAll();
            txtCarton.Focus();
            /// 3.【结束作业】 按钮启用。
            btnStart.Enabled = false;
            btnEnd.Enabled = true;

        }
        private void refresh_dgvPick2New(string palletno)
        {
            dgvPick.DataSource = null;
            dgvPick.Rows.Clear();
            PickListBll plb = new PickListBll();
            DataTable pickdt = plb.GetDataTableBLL(palletno);
            if (pickdt.Rows.Count > 0)
            {
                for (int i = 0; i < pickdt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPick.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = pickdt.Rows[i]["PALLET_NO"].ToString();
                    dr.Cells[1].Value = pickdt.Rows[i]["ICTPN"].ToString();
                    dr.Cells[2].Value = pickdt.Rows[i]["QTY"].ToString();
                    dr.Cells[3].Value = pickdt.Rows[i]["CARTON_QTY"].ToString();
                    dr.Cells[4].Value = pickdt.Rows[i]["PICK_QTY"].ToString();
                    dr.Cells[5].Value = pickdt.Rows[i]["PICK_CARTON"].ToString();
                    dr.Cells[6].Value = pickdt.Rows[i]["PICK_STATUS"].ToString();
                    dr.Cells[7].Value = pickdt.Rows[i]["COMPUTER_NAME"].ToString();

                    if (pickdt.Rows[i]["PICK_STATUS"].ToString().Contains("WP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.White;
                    }
                    else if (pickdt.Rows[i]["PICK_STATUS"].ToString().Contains("IP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (pickdt.Rows[i]["PICK_STATUS"].ToString().Contains("FP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Green;
                    }
                    try
                    {
                        dgvPick.Invoke((MethodInvoker)delegate ()
                        {
                            dgvPick.Rows.Add(dr);
                            string dgvPickpalletpart = dr.Cells["pallet_no"].Value.ToString() + dr.Cells["ictpn"].Value.ToString();
                            dgvPickpalletpart = dgvPickpalletpart.Trim();
                            if (dgvNo.Rows.Count > 0)
                            {
                                for (int j = 0; j < dgvNo.Rows.Count; j++)
                                {
                                    //"Select  a.Shipment_ID 集货单号,c.CARRIER_NAME Carrier, c.POE, c.Region 地区, a.Pallet_NO 栈板号, a.pick_status Pallet_PICK_Status, a.PALLET_TYPE 栈板类型, b.ICTPN 料号 " +
                                    //" , b.QTY 数量, b.CARTON_QTY 箱数, b.PICK_CARTON 已Pick箱数,b.pick_status Part_PICK_Status" + //, c.SHIPMENT_TYPE, c.SHIPPING_TIME " +
                                    //"
                                    string dgvNopalletpart = dgvNo.Rows[j].Cells["栈板号"].Value.ToString() + dgvNo.Rows[j].Cells["料号"].Value.ToString(); ;
                                    dgvNopalletpart = dgvNopalletpart.Trim();
                                    if (dgvPickpalletpart.Equals(dgvNopalletpart))
                                    {
                                        dgvNo.Rows[j].Cells["已Pick箱数"].Value = dr.Cells["pick_carton"].Value;
                                        dgvNo.Rows[j].Cells["Part_PICK_Status"].Value = dr.Cells["pick_status"].Value;

                                        if (dgvNo.Rows[j].Cells["PART_PICK_STATUS"].Value.ToString().Contains("WP"))
                                        {
                                            this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.White;
                                        }
                                        else if (dgvNo.Rows[j].Cells["PART_PICK_STATUS"].Value.ToString().Contains("IP"))
                                        {
                                            this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.Yellow;
                                        }
                                        else if (dgvNo.Rows[j].Cells["PART_PICK_STATUS"].Value.ToString().Contains("FP"))
                                        {
                                            this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.Green;
                                        }
                                        dgvNo.Rows[j].Cells["PALLET_PICK_STATUS"].Value = "IP-拣货中";
                                    }
                                }
                            }
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

        private void refresh_dgvPick2(string palletno)
        {
            dgvPick.DataSource = null;
            PickListBll plb = new PickListBll();
            DataTable pickdt = plb.GetDataTableBLL(palletno);
            if (pickdt == null)
            {
                return;
            }
            dgvPick.DataSource = pickdt;
            if (pickdt.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPick.Rows.Count; i++)
                {
                    if (dgvPick.Rows[i].Cells["pick_status"].Value.ToString().Contains("WP"))
                    {
                        this.dgvPick.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    else if (dgvPick.Rows[i].Cells["pick_status"].Value.ToString().Contains("IP"))
                    {
                        this.dgvPick.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (dgvPick.Rows[i].Cells["pick_status"].Value.ToString().Contains("FP"))
                    {
                        this.dgvPick.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    }


                    string dgvPickpalletpart = dgvPick.Rows[i].Cells["pallet_no"].Value.ToString() + dgvPick.Rows[i].Cells["ictpn"].Value.ToString();
                    //select pallet_no, ictpn, qty, carton_qty, pick_qty, pick_carton, pick_status, computer_name "

                    dgvPickpalletpart = dgvPickpalletpart.Trim();
                    if (dgvNo.Rows.Count > 0)
                    {
                        for (int j = 0; j < dgvNo.Rows.Count; j++)
                        {
                            //"Select  a.Shipment_ID 集货单号,c.CARRIER_NAME Carrier, c.POE, c.Region 地区, a.Pallet_NO 栈板号, a.pick_status Pallet_PICK_Status, a.PALLET_TYPE 栈板类型, b.ICTPN 料号 " +
                            //" , b.QTY 数量, b.CARTON_QTY 箱数, b.PICK_CARTON 已Pick箱数,b.pick_status Part_PICK_Status" + //, c.SHIPMENT_TYPE, c.SHIPPING_TIME " +
                            //"
                            string dgvNopalletpart = dgvNo.Rows[j].Cells["栈板号"].Value.ToString() + dgvNo.Rows[j].Cells["料号"].Value.ToString(); ;
                            dgvNopalletpart = dgvNopalletpart.Trim();
                            if (dgvPickpalletpart.Equals(dgvNopalletpart))
                            {
                                dgvNo.Rows[j].Cells["已Pick箱数"].Value = dgvPick.Rows[i].Cells["pick_carton"].Value;
                                dgvNo.Rows[j].Cells["Part_PICK_Status"].Value = dgvPick.Rows[i].Cells["pick_status"].Value;

                                if (dgvNo.Rows[j].Cells["Part_PICK_Status"].Value.ToString().Contains("WP"))
                                {
                                    this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.White;
                                }
                                else if (dgvNo.Rows[j].Cells["Part_PICK_Status"].Value.ToString().Contains("IP"))
                                {
                                    this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.Yellow;
                                }
                                else if (dgvNo.Rows[j].Cells["Part_PICK_Status"].Value.ToString().Contains("FP"))
                                {
                                    this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.Green;
                                }
                                dgvNo.Rows[j].Cells["Pallet_PICK_Status"].Value = "IP-拣货中";
                            }

                        }
                    }
                }


            }
            else
            {
                dgvPick.DataSource = "";
            }

        }

        private void refresh_dgvPickNew(string palletno)
        {
            dgvPick.DataSource = null;
            dgvPick.Rows.Clear();
            PickListBll plb = new PickListBll();
            DataTable pickdt = plb.GetDataTableBLL(palletno);
            int sumpalletcarton = 0;
            int sumpickcarton = 0;
            if (pickdt.Rows.Count > 0)
            {
                for (int i = 0; i < pickdt.Rows.Count; i++)
                {
                    //创建行
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in dgvPick.Columns)
                    {
                        dr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dr.HeaderCell.Value = (i + 1).ToString();
                    dr.Cells[0].Value = pickdt.Rows[i]["PALLET_NO"].ToString();
                    dr.Cells[1].Value = pickdt.Rows[i]["ICTPN"].ToString();
                    dr.Cells[2].Value = pickdt.Rows[i]["QTY"].ToString();
                    dr.Cells[3].Value = pickdt.Rows[i]["CARTON_QTY"].ToString();
                    dr.Cells[4].Value = pickdt.Rows[i]["PICK_QTY"].ToString();
                    dr.Cells[5].Value = pickdt.Rows[i]["PICK_CARTON"].ToString();
                    dr.Cells[6].Value = pickdt.Rows[i]["PICK_STATUS"].ToString();
                    dr.Cells[7].Value = pickdt.Rows[i]["COMPUTER_NAME"].ToString();

                    if (pickdt.Rows[i]["PICK_STATUS"].ToString().Contains("WP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.White;
                    }
                    else if (pickdt.Rows[i]["PICK_STATUS"].ToString().Contains("IP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (pickdt.Rows[i]["PICK_STATUS"].ToString().Contains("FP"))
                    {
                        dr.DefaultCellStyle.BackColor = Color.Green;
                    }
                    try
                    {
                        dgvPick.Invoke((MethodInvoker)delegate ()
                        {
                            dgvPick.Rows.Add(dr);
                            string dgvPickpalletpart = dr.Cells["pallet_no"].Value.ToString() + dr.Cells["ictpn"].Value.ToString();
                            dgvPickpalletpart = dgvPickpalletpart.Trim();
                            if (dgvNo.Rows.Count > 0)
                            {
                                for (int j = 0; j < dgvNo.Rows.Count; j++)
                                {
                                    //"Select  a.Shipment_ID 集货单号,c.CARRIER_NAME Carrier, c.POE, c.Region 地区, a.Pallet_NO 栈板号, a.pick_status Pallet_PICK_Status, a.PALLET_TYPE 栈板类型, b.ICTPN 料号 " +
                                    //" , b.QTY 数量, b.CARTON_QTY 箱数, b.PICK_CARTON 已Pick箱数,b.pick_status Part_PICK_Status" + //, c.SHIPMENT_TYPE, c.SHIPPING_TIME " +
                                    //"
                                    string dgvNopalletpart = dgvNo.Rows[j].Cells["栈板号"].Value.ToString() + dgvNo.Rows[j].Cells["料号"].Value.ToString(); ;
                                    dgvNopalletpart = dgvNopalletpart.Trim();
                                    if (dgvPickpalletpart.Equals(dgvNopalletpart))
                                    {
                                        dgvNo.Rows[j].Cells["已Pick箱数"].Value = dr.Cells["pick_carton"].Value;
                                        dgvNo.Rows[j].Cells["Part_PICK_Status"].Value = dr.Cells["pick_status"].Value;

                                        if (dgvNo.Rows[j].Cells["PART_PICK_STATUS"].Value.ToString().Contains("WP"))
                                        {
                                            this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.White;
                                        }
                                        else if (dgvNo.Rows[j].Cells["PART_PICK_STATUS"].Value.ToString().Contains("IP"))
                                        {
                                            this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.Yellow;
                                        }
                                        else if (dgvNo.Rows[j].Cells["PART_PICK_STATUS"].Value.ToString().Contains("FP"))
                                        {
                                            this.dgvNo.Rows[j].DefaultCellStyle.BackColor = Color.Green;
                                        }
                                        dgvNo.Rows[j].Cells["PALLET_PICK_STATUS"].Value = "IP-拣货中";
                                    }
                                }
                            }
                        });
                    }
                    catch (Exception e1)
                    {
                        ShowMsg(e1.ToString(), 0);
                        return;
                    }
                    sumpalletcarton += Convert.ToInt32(dr.Cells["carton_qty"].Value);
                    sumpickcarton += Convert.ToInt32(dr.Cells["pick_carton"].Value);
                }
                labqty.Text = sumpickcarton.ToString() + "/" + sumpalletcarton.ToString();
                labqty.Refresh();
            }
            else
            {
                labqty.Text = "00/00";
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            //HYQ：20181108 
            //重新定义: 
            //如果strPickpalletno是空， 说明没有刷入过有效的序号， 直接结束,还原到未点 开始作业的状态， 解除电脑名。
            //-----//如果strPickpalletno不为空， 说明刷入过有效的序号，保留原有逻辑
            //HYQ：20181126
            //如果strPickpalletno不为空， 统计已经刷的数量：
            //如果不为零 说明刷入过有效的序号，保留原有逻辑；
            //如果为零说明没有刷入过有效的序号， 直接结束,还原到未点 开始作业的状态， 解除电脑名。
            string strShipmentid = txtSmId.Text.Trim();

            string strPalletno = txtPallet.Text.Trim();

            string strPickpalletno = txtPick.Text.Trim();

            if (string.IsNullOrEmpty(strPickpalletno))
            {
                
            }
            else
            {
                if (strPickpalletno.Substring(1, 1).Equals("9"))
                {
                    //这么写不好，再改改。
                    ShowMsg("9号pickpallet,必须拣货完成自动打印", 0);
                    return;
                }

                /// 2.解锁电脑名与栈板的绑定。 改成SP NONEDIPPS.SP_PICK_UNLOCKCOMPUTERNAME(palletno  in varchar2,timelimit in varchar2,errmsg    out varchar2)
                //--timelimit 如果是N，就是没有时间限制，栈板号来了就能解除电脑， 
                //--          如果是Y，就是有时间限制， UDT 在当前时间点 前一天 或者一小时 才能解锁电脑。

              
                PickPalletLabel ppl = new PickPalletLabel();

                if (ppl.PrintPickPalletLabel_new(strPickpalletno))
                {
                    ShowMsg("打印OK", -1);

                }
                else
                {
                    ShowMsg("打印连接出了问题；或者pick数量为0，不需要打印label", 0);
                    return;
                }
            }

            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "palletno", strPalletno };
            procParams[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "timelimit", "N" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleType.VarChar, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("NONEDIPPS.SP_PICK_UNLOCKCOMPUTERNAME", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                ShowMsg(e1.ToString(), 0);
                return;
            }
            if (dt.Rows[0]["errmsg"].ToString().Contains("OK"))
            {
                txtCarton.Enabled = false;
                txtCarton.Focus();
            }
            else if (dt.Rows[0]["errmsg"].ToString().Contains("NG"))
            {
                ShowMsg(dt.Rows[0]["errmsg"].ToString(), 0);
                return;
            }
            else
            {
                ShowMsg("处理palletno遇到特殊异常", 0);
                return;
            }
            endprintaction();
        }
        private void endprintaction()
        {
            /// 3.锁定程式界面上半部分的功能>> 按钮失效;datagriadview 选择index 事件失效;...
            btnSearch.Enabled = true;
            btnClsFace.Enabled = true;
            btnReprint.Enabled = true;
            
            this.dgvNo.SelectionChanged += new System.EventHandler(this.dgvNo_SelectionChanged);
            txtCarton.Enabled = false;
            txtCarton.Text = "";
            txtPick.Text = "";
            /// 4.【开始作业】 按钮启用。
            btnStart.Enabled = true;
            btnEnd.Enabled = false;
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            TextMsg.Text = "";
            TextMsg.BackColor = Color.Blue;
            fCheck fcheck = new fCheck();
            if (fcheck.ShowDialog() != DialogResult.OK)
            {
                ShowMsg("账号权限不正确", 0);
                return;
            }
            else
            {
                rePrintLabel pr = new rePrintLabel();
                pr.ShowDialog();
            }
        }

        private void btnClsFace_Click(object sender, EventArgs e)
        {
            btnClsFace.Enabled = false;
            try
            {
                btnStart.Enabled = true;
                clearCmb();
                clearDgv();
                clearTxt();
                labqty.Text = "00/00";
                this.ShowMsg("清空成功", -1);
                Ok();
            }
            catch (Exception ex)
            {
                Ng();
                MessageBox.Show(ex.Message);
            }
            btnClsFace.Enabled = true;
        }

        private void radFD_CheckedChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
            if (!btnEnd.Enabled)
            {
                clearCmb();
                clearDgv();
                clearTxt();
                labqty.Text = "00/00";
            }
        }

        private void radDS_CheckedChanged(object sender, EventArgs e)
        {
            dgvNo.DataSource = null;
            if (!btnEnd.Enabled)
            {
                clearCmb();
                clearDgv();
                clearTxt();
                labqty.Text = "00/00";
            }
        }

        private void cmbSTATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            formControl();
        }

        public void formControl()
        {
            if (cmbSTATUS.Text.Contains("ALL") || cmbSTATUS.Text.Contains("FP-"))
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

        private void btnGetSN_Click(object sender, EventArgs e)
        {

            TESTSN_Form tsf = new TESTSN_Form();
            tsf.ShowDialog();
        }
        
        private void dgvPick_SelectionChanged(object sender, EventArgs e)
        {
            
            Int32 rowIndex = 0;
            try
            {
                rowIndex = dgvPick.CurrentRow.Index;
            }
            catch (Exception)
            {
                return;
            }
            if (dgvPick.CurrentRow.Index >= 0)
            {
                if (g_curRow == rowIndex)
                    return;
                g_curRow = rowIndex;
                string setPart = dgvPick.Rows[rowIndex].Cells["ictpn"].Value.ToString();

                //1.2 不同行，料号相同，也不更新库存信息
               // if (g_partNo == setPart && dgvStock.Rows.Count > 0)
                //{ return; }
                g_partNo = setPart;

                PickListBll pb = new PickListBll();
                //pb.ShowStockInfo(setPart, dgvStock);
                pb.ShowStockCarInfo(setPart, txtPallet.Text, dgvStock);

                //PickListBll pb = new PickListBll();
                //DataTable dtStock = pb.GetStockInfoDataTable(setPart);
                //if (dtStock != null && dtStock.Rows.Count > 0)
                //{

                //    dgvStock.DataSource = dtStock;
                //    dgvStock.AutoResizeColumns();
                //}

            }
        }
    
        /// <summary>
        /// 错误声音
        /// </summary>
        public void Ng()
        {
            LibHelperAC.MediasHelper.PlaySoundAsyncByNg();
        }

        /// <summary>
        /// 正确声音
        /// </summary>
        public void Ok()
        {
            LibHelperAC.MediasHelper.PlaySoundAsyncByOk();
        }

        public void Re()
        {
            LibHelperAC.MediasHelper.PlaySoundAsyncByRe();
        }
        public void PartError()
        {
            LibHelperAC.MediasHelper.PlaySoundAsyncByPartError();
        }

        /// <summary>
        /// Message 信息
        /// </summary>
        public DialogResult ShowMsg(string strTxt, int strType)
        {
            TextMsg.Text = strTxt;
            switch (strType)
            {
                case 0: //Error    
                    TextMsg.ForeColor = Color.Red;
                    TextMsg.BackColor = Color.Blue;
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

