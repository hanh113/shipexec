using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using PTTWebServices.Models;
using System.Collections.Generic;
using PTTWebServices;

namespace Sentdirectedi
{
    public partial class fMain : Form
    {
        public string StrInShipmentId { get; set; }

        public fMain()
        {
            InitializeComponent();
        }
        private string g_sUserID, g_sUserNo;
        private string g_sIniFile = Application.StartupPath + "\\sajet.ini";
        private string g_sExeName;
        private string g_sProgram, g_sFunction;
        private string g_sAppPath = string.Empty;
        DataTable dtResult = new DataTable();


        public DialogResult ShowMsg(string sText, int iType)
        {
            TextMsg.Text = sText;
            switch (iType)
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
                    return MessageBox.Show(sText, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    TextMsg.ForeColor = Color.Green;
                    TextMsg.BackColor = Color.White;
                    return DialogResult.None;
            }
        }

        private void InitaildtResult1()
        {

            dtResult = new DataTable();
            dtResult.Columns.Add("SHIPMENT_ID", Type.GetType("System.String"));
            dtResult.Columns.Add("出货单号", Type.GetType("System.String"));
            dtResult.Columns.Add("出货明细行号", Type.GetType("System.String"));
            dtResult.Columns.Add("栈板号", Type.GetType("System.String"));
            dtResult.Columns.Add("箱号", Type.GetType("System.String"));
            dtResult.Columns.Add("TT_FLAG", Type.GetType("System.String"));
            dtResult.Columns.Add("EDI_FLAG", Type.GetType("System.String"));
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dtResult;

            dataGridView1.Columns["SHIPMENT_ID"].Width = 200;
            dataGridView1.Columns["出货单号"].Width = 200;
            dataGridView1.Columns["出货明细行号"].Width = 250;
            dataGridView1.Columns["栈板号"].Width = 200;
            dataGridView1.Columns["箱号"].Width = 200;
            dataGridView1.Columns["TT_FLAG"].Width = 200;
            dataGridView1.Columns["EDI_FLAG"].Width = 200;

        }

        //窗体载入时初始化
        private void fMain_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(StrInShipmentId))
            {
                tbshipmentid.Text = StrInShipmentId;
            }

            InitaildtResult1();
            g_sUserID = ClientUtils.UserPara1;
            g_sUserNo = ClientUtils.fLoginUser;
            g_sProgram = ClientUtils.fProgramName;
            g_sFunction = ClientUtils.fFunctionName;
            g_sExeName = ClientUtils.fCurrentProject;
            dataGridView1.AutoGenerateColumns = false;
            g_sProgram = ClientUtils.fProgramName;
            g_sAppPath = Path.GetFullPath(Application.StartupPath + @"\" + g_sProgram + @"\");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Getsoinfo())
            {
                return;
            }
            ShowMsg("出货单查询OK，请确认！", 1);
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示在HeaderCell上
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow r = this.dataGridView1.Rows[i];
                r.HeaderCell.Value = string.Format("{0}", i + 1);
            }
            this.dataGridView1.Refresh();
        }



        private bool Getsoinfo()
        {
            try
            {
                object[][] Param;
                string sSQL = @" SELECT A.SHIPMENT_ID, A.DN, A.DN_LINE, A.PALLET_NO, A.CARTON_NO,B.EDI_FLAG,B.TT_FLAG  
                                  FROM PPSUSER.G_DS_SCANDATA_DETAIL_T A,PPSUSER.G_DS_SHIMMENT_BASE_T B
                                 WHERE A.SHIPMENT_ID =:SHIPMENTID AND A.SHIPMENT_ID=B.SHIPMENT_ID
                                 GROUP BY A.SHIPMENT_ID, A.DN, A.DN_LINE, A.PALLET_NO, A.CARTON_NO ,B.EDI_FLAG,B.TT_FLAG ";
                Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIPMENTID", tbshipmentid.Text };
                DataSet sDataSet = ClientUtils.ExecuteSQL(sSQL, Param);
                if (sDataSet.Tables[0].Rows.Count > 0)
                {
                    dtResult.Rows.Clear();
                    for (int i = 0; i < sDataSet.Tables[0].Rows.Count; i++)
                    {

                        dataGridView1.Columns["SHIPMENT_ID"].Width = 200;
                        dataGridView1.Columns["出货单号"].Width = 200;
                        dataGridView1.Columns["出货明细行号"].Width = 250;
                        dataGridView1.Columns["栈板号"].Width = 200;
                        dataGridView1.Columns["箱号"].Width = 200;
                        dataGridView1.Columns["TT_FLAG"].Width = 200;
                        dataGridView1.Columns["EDI_FLAG"].Width = 200;
                        DataRow dr = dtResult.NewRow();
                        dr["SHIPMENT_ID"] = sDataSet.Tables[0].Rows[i]["SHIPMENT_ID"].ToString();
                        dr["出货单号"] = sDataSet.Tables[0].Rows[i]["DN"].ToString();
                        dr["出货明细行号"] = sDataSet.Tables[0].Rows[i]["DN_LINE"].ToString();
                        dr["栈板号"] = sDataSet.Tables[0].Rows[i]["PALLET_NO"].ToString();
                        dr["箱号"] = sDataSet.Tables[0].Rows[i]["CARTON_NO"].ToString();
                        dr["TT_FLAG"] = sDataSet.Tables[0].Rows[i]["TT_FLAG"].ToString();
                        dr["EDI_FLAG"] = sDataSet.Tables[0].Rows[i]["EDI_FLAG"].ToString();
                        dtResult.Rows.Add(dr);
                    }
                    return true;

                }
                else
                {
                    ShowMsg("出货单不存在或者没过栈板打印，请确认！", 0);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
                return false;
            }

        }

        private bool UpdateEdiFlag(string shipmentid)
        {
            try
            {
                string sqlstr = "UPDATE PPSUSER.G_DS_SHIMMENT_BASE_T SET EDI_FLAG='Y' WHERE SHIPMENT_ID=:SHIPMENT_ID";

                object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIPMENT_ID", shipmentid } };
                ClientUtils.ExecuteSQL(sqlstr, sqlparams);
                return true;
            }
            catch (Exception ex) { ShowMsg(ex.Message, 0); return false; }
        }


        public bool checkupdate(string dn)
        {

            object[][] Param = new object[1][];
            string sql = @"SELECT * FROM PPSUSER.G_PALLETEDI_INFO a
                           WHERE a.so_no=:dn";
            Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "dn", dn };
            DataSet ds = ClientUtils.ExecuteSQL(sql, Param);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string flag = ds.Tables[0].Rows[i]["edi_flag"].ToString();
                    if (flag == "Y")
                    {
                        MessageBox.Show("已结上传过了，不能保存！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("输入的DN对应信息不存在，请确认是否打印pallet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CheckDSEdiFlag(string shipmentid)
        {
            try
            {
                string sqlstr = "SELECT EDI_FLAG,TT_FLAG FROM PPSUSER.G_DS_SHIMMENT_BASE_T WHERE SHIPMENT_ID=:SHIPMENT_ID AND ROWNUM=1";
                object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIPMENT_ID", shipmentid } };
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string ediflag = dt.Rows[0]["EDI_FLAG"].ToString();
                    string ttflag = dt.Rows[0]["TT_FLAG"].ToString();
                    //if (ttflag == "N") 
                    //{
                    //    ShowMsg("Shipment ID 未上传TT,请先上传TT", 0);
                    //    return false;

                    //}
                    if (ediflag == "Y")
                    {
                        ShowMsg("Shipment ID 已上传", 0);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    ShowMsg("SHIPMENT ID ERROR", 0);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                return false;
            }
        }

        private void btsentedi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbshipmentid.Text))
                return;
            string shipmentId = tbshipmentid.Text;
            if (!CheckDSEdiFlag(shipmentId))
            {
                return;
            }
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("请先点击查询！", "Confirm", MessageBoxButtons.OKCancel);
                return;
            }
            string sRes = "";
            DataTable dta = this.GetDgvToTable();
            int intRowIndex = 0;
            foreach (DataRow r in dta.Rows)
            {

                string dn = r["出货单号"].ToString();
                string shipmentid = r["SHIPMENT_ID"].ToString();
                string dnline = r["出货明细行号"].ToString();
                string cartonno = r["箱号"].ToString();
                string nums = GETPACKAGE_ID();//获取package_id
                update856data(dn);
                update945data(dn);
                bool flag1 = sentedi(shipmentid, dn, dnline, nums, cartonno, ref sRes);
                if (flag1)
                {
                    this.ShowMsg("上传OK！", 1);
                    dataGridView1.Rows[intRowIndex].Cells["EDI_FLAG"].Value = 'Y';
                    tbshipmentid.SelectAll();
                    tbshipmentid.Focus();
                }
                else
                {
                    this.ShowMsg(sRes, 0);
                    tbshipmentid.SelectAll();
                    tbshipmentid.Focus();
                    dtResult.Rows.Clear();
                    return;//跳出for循环
                }
                intRowIndex++;
            }
            object[][] Par = new object[1][];
            string Sql = @"SELECT A.TYPE
                              FROM PPSUSER.G_DS_SHIMMENT_BASE_T A
                             WHERE A.SHIPMENT_ID =:SHIPMENTID";
            Par[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIPMENTID", tbshipmentid.Text };
            DataSet dt = ClientUtils.ExecuteSQL(Sql, Par);
            if (dt.Tables[0].Rows.Count > 0)
            {
                string Type = dt.Tables[0].Rows[0]["TYPE"].ToString();
                if (Type != "Bulk")
                {
                    UpdateEdiFlag(shipmentId);
                    dtResult.Rows.Clear();
                    return;
                }
            }

            object[][] Params = new object[1][];
            string sql = @"SELECT A.SHIPMENT_ID, A.SSCC,B.ASN_NUM,A.PALLET_TYPE
                              FROM PPSUSER.G_DS_PALLET_T          A,PPSUSER.G_DS_PICK_T D,
                                   WMUSER.AC_856SC_SHIPMENT       B,
                                   PPSUSER.G_DS_SHIPMENT_DNLINE_T C
                             WHERE A.SHIPMENT_ID = C.SHIPMENT_ID
                               AND C.DN = B.ASN_NUM AND A.PALLET_NO=D.PALLET_NO AND D.DN=C.DN
                               AND A.SHIPMENT_ID =:TDN
                               GROUP BY A.SHIPMENT_ID, A.SSCC,B.ASN_NUM,A.PALLET_TYPE";
            Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", tbshipmentid.Text };
            DataSet dsTemp = ClientUtils.ExecuteSQL(sql, Params);
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                string id = string.Empty;
                string sscc = string.Empty;
                string asnnum = string.Empty;
                string pallettype = string.Empty;
                for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
                {
                    pallettype = dsTemp.Tables[0].Rows[i]["PALLET_TYPE"].ToString();
                    if (pallettype == "999" && id == dsTemp.Tables[0].Rows[i]["SHIPMENT_ID"].ToString() && sscc == dsTemp.Tables[0].Rows[i]["SSCC"].ToString())
                    {
                        continue;
                    }

                    id = dsTemp.Tables[0].Rows[i]["SHIPMENT_ID"].ToString();
                    sscc = dsTemp.Tables[0].Rows[i]["SSCC"].ToString();
                    asnnum = dsTemp.Tables[0].Rows[i]["asn_num"].ToString();

                    //   insert945data(asnnum);//  改为procedure  中上传
                    string ids = GETPACKAGE_ID();//获取package_id
                    insert_t(tbshipmentid.Text, ids, sscc, asnnum, pallettype);
                    Updatepack_p(ids, sscc, asnnum, pallettype);
                }

            }
            UpdateEdiFlag(shipmentId);

            dtResult.Rows.Clear();
        }

        public bool Updatepack_p(string ids, string sscc, string asnnum, string pallettype)
        {
            try
            {
                object[][] Params = new object[2][];
                string sql = @"UPDATE WMUSER.AC_856SC_PACKAGE  A
                               SET A.P_PACKAGE_ID =:id,SSCC=''
                             WHERE  sscc=:sscc and hl_code='P'";
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "id", pallettype == "999" ? "" : ids };
                //Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "asnnum", asnnum };
                Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "sscc", sscc };
                DataSet dsTemp = ClientUtils.ExecuteSQL(sql, Params);

                object[][] Par = new object[2][];
                string Sql = @"UPDATE WMUSER.AC_856SC_PACKAGE  A
                                   SET A.P_PACKAGE_ID =''
                                 WHERE A.PACKAGE_ID =:id AND SSCC=:sscc and  hl_code IN ('T','M')";
                Par[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "id", ids };
                //Par[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "asnnum", asnnum };
                Par[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "sscc", sscc };
                DataSet ds = ClientUtils.ExecuteSQL(Sql, Par);

                return true;
            }
            catch (Exception )
            {
                return false;
            }

        }

        public bool insert_t(string shipmentid, string packageid, string SSCC, string asnnum, string pallettype)
        {
            try
            {
                string MSG_ID = "";
                string ASN_NUM = "";
                string TVOLUME = "";
                string TG_WEIGHT = "";
                string TN_WEIGHT = "";
                object[][] Params = new object[2][];
                string sql = @"SELECT A.MSG_ID, A.ASN_NUM
                          FROM WMUSER.AC_856SC_SHIPMENT   A
                          WHERE A.shipment_id =:shipmentid
                           AND A.ASN_NUM =:asnnum";
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentid", shipmentid };
                Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "asnnum", asnnum };
                DataSet dsTemp = ClientUtils.ExecuteSQL(sql, Params);
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    MSG_ID = dsTemp.Tables[0].Rows[0]["MSG_ID"].ToString();
                    ASN_NUM = dsTemp.Tables[0].Rows[0]["ASN_NUM"].ToString();
                }

                object[][] Para = new object[2][];
                string sql1 = @" SELECT ROUND(SUM(A.VOLUME),2) AS VOLUME,
                                    ROUND(MAX(F.WEIGHT),2) AS WHOLE_WEIGHT,
                                    ROUND(SUM(C.SN_QTY * B.PRODUCT_WEIGHT),2) AS WEIGHT
                               FROM PPSUSER.G_CARTON_INFO          A,
                                    PPSUSER.G_DS_PARTINFO_T        B,
                                    PPSUSER.G_DS_PACKINFO_T        C,
                                    PPSUSER.G_DS_SHIPMENT_DDLINE_T D,
                                    PPSUSER.G_DS_PALLET_T          E,PPSUSER.G_DS_WEIGHT_T F 
                              WHERE E.SHIPMENT_ID = D.SHIPMENT_ID
                                AND A.MPN = B.ICTPN AND E.END_PALLETNO = F.PALLET_NO
                                AND C.PACK_CODE = B.PACK_CODE
                                AND D.ICTPN = A.MPN
                                AND D.SHIPMENT_ID =:shipmentid
                                AND E.SSCC =:sscc ";
                Para[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentid", shipmentid };
                Para[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "sscc", SSCC };
                DataSet ds = ClientUtils.ExecuteSQL(sql1, Para);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TVOLUME = ds.Tables[0].Rows[0]["VOLUME"].ToString();
                    TG_WEIGHT = ds.Tables[0].Rows[0]["WHOLE_WEIGHT"].ToString();
                    TN_WEIGHT = ds.Tables[0].Rows[0]["WEIGHT"].ToString();
                }

                string hlcode = "T";
                string packcode = "4";

                if (pallettype == "999")
                {
                    hlcode = "M";
                    packcode = "4";
                }


                object[][] Par = new object[8][];
                string sql2 = @" INSERT INTO WMUSER.AC_856SC_PACKAGE   A
                        (A.MSG_ID, 
                         A.DN,

                         A.ASN_NUM,
                         A.HL_CODE, --(P)
                         A.PACKAGE_ID, --(SEQ)
                         A.PACKAGE_CODE,
                         A.UOM_WEIGHT, --(KG)
                         A.LENGTH,
                         A.WIDTH,
                         A.HEIGHT,
                         A.UOM_S, --(CM)
                         A.G_WEIGHT,
                        A.UNIT_WEIGHT,
                         A.G_VOLUME,
                         A.UOM_VOL, --(CR)
                         A.PACK_NUM_MAN,
                         A.SSCC,A.QTY
                       )
                          SELECT :TMSG_ID,
         F.DN,
         :asnnum,
         :hlcode,
         :TPACKAGE_ID,
         :packcode,
         'KG',
         ROUND (G.LENGTH, 2),
         ROUND (G.WIDTH, 2),
         ROUND (G.HEIGHT, 2),
         'CM',
         :TG_WEIGHT,
         E.EMPTY_PALLET_WEIGHT,
         ROUND (G.LENGTH*G.WIDTH*G.HEIGHT/100000, 2),
         'CR',
         G.QTY,
         G.SSCC,G.QTY
    FROM PPSUSER.G_DS_SHIPMENT_DDLINE_T A,
         PPSUSER.G_DS_PACKINFO_T E,
         PPSUSER.G_CARTON_INFO B,
         PPSUSER.G_DS_SCANDATA_T C,
         WMUSER.AC_856SC_SHIPMENT D,
         WMUSER.AC_SHIPMENT_LINE_SC F,
         PPSUSER.G_DS_PALLET_T G
   WHERE     A.ICTPN = B.MPN
         AND G.PALLET_NO = C.PALLET_NO
         AND A.MPN = C.MPN
         AND A.DN = F.AC_DN
         AND A.MPN = F.AC_PN
         AND A.PACK_CODE = E.PACK_CODE
         AND A.DN = D.ASN_NUM
         AND A.ICTPN = C.ICTPN
         AND a.shipment_id = :shipmentid
         AND G.SSCC = :sscc
GROUP BY
         F.DN,
         G.LENGTH,
         G.WIDTH,
         G.HEIGHT,
         E.EMPTY_PALLET_WEIGHT,
         G.SSCC ,G.QTY";
                Par[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipmentid", shipmentid };
                Par[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TMSG_ID", MSG_ID };
                Par[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TPACKAGE_ID", packageid };
                Par[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TG_WEIGHT", TG_WEIGHT };
                // Par[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TVOLUME", TVOLUME };
                Par[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "sscc", SSCC };
                Par[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "asnnum", hlcode == "M" ? shipmentid : asnnum };
                Par[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "packcode", packcode };
                Par[7] = new object[] { ParameterDirection.Input, OracleType.VarChar, "hlcode", hlcode };
                DataSet ds1 = ClientUtils.ExecuteSQL(sql2, Par);
                return true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                return false;
            }


        }

        private bool sentedi(string shipmentid, string dn, string dnline, string p_package_id, string carton, ref string ttres)
        {
            try
            {
                object[][] Params = new object[6][];
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tshipmentid", shipmentid };
                Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tdn", dn };
                Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tdnline", dnline };
                Params[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tpackage_id", p_package_id };
                Params[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tcarton_no", carton };
                Params[5] = new object[] { ParameterDirection.Output, OracleType.VarChar, "tres", "" };
                DataSet dsTemp = ClientUtils.ExecuteProc("PPSUSER.SJ_INSERT_DIRECTDEI", Params);
                string sRes = dsTemp.Tables[0].Rows[0]["tres"].ToString();
                if (sRes.Substring(0, 2) == "OK")
                {
                    ttres = sRes;
                    return true;
                }
                else
                {
                    ttres = sRes;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ttres = ex.Message;
                return false;
            }
        }

        public string GETPACKAGE_ID()
        {
            try
            {
                string sql1 = @"SELECT WMUSER.AC_856ASN_PACKAGE_PID_SEQ.nextval@DGEDI AS NUMS FROM DUAL";
                DataSet ds = ClientUtils.ExecuteSQL(sql1);
                string nums = ds.Tables[0].Rows[0]["NUMS"].ToString();
                return nums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Confirm", MessageBoxButtons.OKCancel);
                return null;
            }
        }
        public bool update856data(string dn)
        {
            try
            {
                string sql = @" UPDATE WMUSER.AC_856SC_SHIPMENT  A
                                 SET A.TRANS_FLAG = 'N', A.LAST_UPDATE = SYSDATE, A.UPDATE_BY = 'PPS'
                                 WHERE A.ASN_NUM =:TDN";
                object[][] Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", dn };
                DataSet ds = ClientUtils.ExecuteSQL(sql, Param);
                return true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
                return false;
            }
        }
        public bool update945data(string dn)
        {
            try
            {
                string sql = @" UPDATE WMUSER.AC_945_HEADER  a SET A.TRANS_FLAG = 'N', A.LAST_UPDATE = SYSDATE, A.UPDATE_BY = 'PPS'
                                WHERE a.dn IN (
                                SELECT A.Dn
                                  FROM WMUSER.AC_856SC_SHIPMENT  T, WMUSER.AC_945_HEADER  A
                                 WHERE A.DN = T.DN
                                   AND T.ASN_NUM =:TDN)";
                object[][] Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", dn };
                DataSet ds = ClientUtils.ExecuteSQL(sql, Param);
                return true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
                return false;
            }
        }

        public bool insert945data(string dn)
        {
            try
            {
                string sql = @"  INSERT INTO WMUSER.AC_945_SN  A
                                (A.MSG_ID,
                                 A.DO_NUM,
                                 A.SEQ,
                                 A.SN,
                                 A.SSCC,
                                 A.DN,
                                 A.DN_LINE,
                                 A.CTN,
                                 A.SSCC_COUNT)
                                SELECT B.MSG_ID,
                                       B.DO_NUM,
                                       B.SEQ,
                                       A.SERIAL_NUMBER,
                                       C.SSCC,
                                       C.DN,
                                       C.DN_LINE,
                                       A.CARTON_NO,
                                       (SELECT COUNT(DISTINCT(D.CARTON_NO))
                                          FROM PPSUSER.G_DS_SCANDATA_DETAIL_T D
                                         WHERE D.DN = A.DN)  AS qty
                                  FROM PPSUSER.G_DS_SCANDATA_T        C,
                                       PPSUSER.G_DS_SCANDATA_DETAIL_T A,
                                       WMUSER.AC_945_LINE        B
                                 WHERE B.PO_DN_NUM = A.DN
                                   AND C.DN_LINE = A.DN_LINE
                                   AND C.DN = A.DN
                                   AND C.INPUT = A.CARTON_NO
                                   AND A.DN =:TDN";
                object[][] Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", dn };
                DataSet ds = ClientUtils.ExecuteSQL(sql, Param);
                return true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
                return false;
            }
        }
        public DataTable GetDgvToTable()
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dataGridView1.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dataGridView1.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dataGridView1.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dataGridView1.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dataGridView1.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private bool Checkprint()
        {
            string a = "";
            DataTable dta = this.GetDgvToTable();
            foreach (DataRow r in dta.Rows)
            {

                if (r["EDI_FLAG"].ToString() == "Y")
                {
                    a = "Y";
                    break;
                }

            }
            if (a == "Y")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool UpdateTTFlag(string shipmentid)
        {
            try
            {
                string sqlstr = "UPDATE PPSUSER.G_DS_SHIMMENT_BASE_T SET TT_FLAG='Y' WHERE SHIPMENT_ID=:SHIPMENT_ID";
                object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIPMENT_ID", shipmentid } };
                ClientUtils.ExecuteSQL(sqlstr, sqlparams);
                return true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                return false;
            }
        }

        private bool CheckTTFlag(string shipmentid)
        {
            try
            {
                string sqlstr = "SELECT TT_FLAG FROM PPSUSER.G_DS_SHIMMENT_BASE_T  WHERE SHIPMENT_ID=:SHIPMENT_ID AND ROWNUM=1";
                object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIPMENT_ID", shipmentid } };
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string ttflag = dt.Rows[0]["TT_FLAG"].ToString();
                    if (ttflag == "Y")
                    {
                        ShowMsg("Shippment ID 已上传TT", 0);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message, 0);
                return false;
            }
        }
        private bool shippingtt(string strshipid)//传数据给PTT
        {
            try
            {

                DM_Doc DM_Doc = new DM_Doc();
                string sSQL = @"select Qty,ict_no,ict_item,shipment_id from  ppsuser.shipment_item where shipment_id = ";
                // string Ssql2 = @"select t.ict_no from ppsuser.shipment_item t where shipment_id =  ";

                if (strshipid != "")
                {
                    sSQL += "  '" + strshipid + "'";
                    //   Ssql2 += "  '" + strshipid + "'";
                }

                // DataSet ds2 = ClientUtils.ExecuteSQL(Ssql2);


                DataSet dt = ClientUtils.ExecuteSQL(sSQL);
                DM_Doc.User = new User_Account("SZFD", "FD-LT");
                DM_Doc.DM_Ships = new List<DM_Ship>() { new DM_Ship() };
                DM_Doc.DM_Ships[0].oga01 = dt.Tables[0].Rows[0]["ICT_NO"].ToString(); //出货单号
                DM_Doc.ASNtype = "post";
                DM_Doc.EDItype = "940";
                DM_Doc.SHIPMENT_ID = dt.Tables[0].Rows[0]["ShipMent_ID"].ToString();
                //  DataTable dt2 = ds2.Tables[0];
                #region  当shipment_id对应多个单号时
                //string[] Oga = dt2.AsEnumerable().Select(d => d.Field<string>("ICT_NO")).ToArray();
                //for (int i = 0; i < Oga.Count(); i++)
                //{
                //    DM_Doc.DM_Ships[i].oga01 = Oga[i];
                //    string Ssql = @"select t.ict_item ,t.qty  from ppsuser.shipment_item t where  t.ict_no = ";
                //    Ssql += "'" + Oga[i] + "'";
                //    DataSet ds3 = ClientUtils.ExecuteSQL(Ssql);
                //    DM_Doc.DM_Ships[i].OgbFiles = new List<U_OgbFile>();
                //    if (ds3.Tables[0].Rows.Count > 0)
                //    {
                //        for (int k = 0; k < ds3.Tables[0].Rows.Count; k++)
                //        {

                //            DM_Doc.DM_Ships[i].OgbFiles.Add(new U_OgbFile()
                //            {
                //                //出货项次
                //                ogb03 = ds3.Tables[0].Rows[k]["ICT_ITEM"].ToString(),
                //                //数量
                //                ogb12 = ds3.Tables[0].Rows[k]["QTY"].ToString()
                //            });
                //        }
                //    }
                //}
                #endregion

                #region shipment_id 对应单个单号时
                DM_Doc.DM_Ships[0].OgbFiles = new List<U_OgbFile>();
                if (dt.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {

                        DM_Doc.DM_Ships[0].OgbFiles.Add(new U_OgbFile()
                        {
                            //出货项次
                            ogb03 = dt.Tables[0].Rows[i]["ICT_ITEM"].ToString(),
                            //数量
                            ogb12 = dt.Tables[0].Rows[i]["QTY"].ToString()
                        });
                    }
                }
                #endregion

                //实例化一个出货
                Shipping Shipping = new Shipping();
                //调用：单个出货方法
                var ResponseModel = Shipping.DMoreShip(DM_Doc);
                if (ResponseModel.YN)
                {
                    MessageBox.Show("抛转成功！单号为：" + ResponseModel.FromNo);
                    return true;
                }
                else
                {
                    MessageBox.Show("抛转失败！失败码" + ResponseModel.errcode + "，详细信息：" + ResponseModel.errDesc);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }


        private void btnUploadTT_Click(object sender, EventArgs e)
        {
            string shipmentid = tbshipmentid.Text.Trim();
            if (string.IsNullOrEmpty(shipmentid)) return;
            if (!CheckTTFlag(shipmentid))
            {
                return;
            }
            if (shippingtt(shipmentid))
            {
                UpdateTTFlag(shipmentid);
            }
        }
    }
}
