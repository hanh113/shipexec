using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using SajetClass;
using PTTWebServices.Models;
using PTTWebServices;
using System.Linq;

namespace Sentedi
{
    public partial class fMain : Form
    {
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
            dtResult.Columns.Add("出货单号", Type.GetType("System.String"));
            //dtResult.Columns.Add("出货明细行号", Type.GetType("System.String"));
            dtResult.Columns.Add("SSCC", Type.GetType("System.String"));
            dtResult.Columns.Add("MIX_FLAG", Type.GetType("System.String"));
            dtResult.Columns.Add("CQTY", Type.GetType("System.String"));
            dtResult.Columns.Add("出货数量", Type.GetType("System.String"));
            dtResult.Columns.Add("已扫描数量", Type.GetType("System.String"));
            dtResult.Columns.Add("栈板重量（kg）", Type.GetType("System.String"));
            dtResult.Columns.Add("整栈板重（kg）", Type.GetType("System.String"));
            dtResult.Columns.Add("所有箱重（kg）", Type.GetType("System.String"));
            dtResult.Columns.Add("L（cm）", Type.GetType("System.String"));
            dtResult.Columns.Add("W（cm）", Type.GetType("System.String"));
            dtResult.Columns.Add("H（cm）", Type.GetType("System.String"));
            dtResult.Columns.Add("EDI_FLAG", Type.GetType("System.String"));

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dtResult;

            dataGridView1.Columns["出货单号"].Width = 200;
            //dataGridView1.Columns["出货明细行号"].Width = 200;
            dataGridView1.Columns["SSCC"].Width = 250;
            dataGridView1.Columns["MIX_FLAG"].Width = 200;
            dataGridView1.Columns["CQTY"].Width = 200;
            dataGridView1.Columns["出货数量"].Width = 200;
            dataGridView1.Columns["已扫描数量"].Width = 200;
            dataGridView1.Columns["栈板重量（kg）"].Width = 100;
            dataGridView1.Columns["整栈板重（kg）"].Width = 100;
            dataGridView1.Columns["所有箱重（kg）"].Width = 200;
            dataGridView1.Columns["L（cm）"].Width = 100;
            dataGridView1.Columns["W（cm）"].Width = 100;
            dataGridView1.Columns["H（cm）"].Width = 100;
            dataGridView1.Columns["EDI_FLAG"].Width = 100;

        }

        //窗体载入时初始化
        private void fMain_Load(object sender, EventArgs e)
        {
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


        /// <summary>
        /// 获取Web Service 用户名和密码
        /// </summary>
        /// <returns></returns>
        private bool getprofitoperation()
        {
            try
            {
                string sSQL = @"SELECT A.PROFIT_CENTER, A.OPERATIONS_CENTER
                                  FROM PPSUSER.G_DN_BASE A
                                 WHERE A.DN_NO =:dnno";
                object[][] Params = new object[1][];
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "dnno", tbdn.Text };
                DataSet dt = ClientUtils.ExecuteSQL(sSQL, Params);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    lbprofit.Text = dt.Tables[0].Rows[0]["PROFIT_CENTER"].ToString();
                    lboperation.Text = dt.Tables[0].Rows[0]["OPERATIONS_CENTER"].ToString();
                    if (string.IsNullOrEmpty(lbprofit.Text) && string.IsNullOrEmpty(lboperation.Text))
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
          btnUploadTT.Enabled = false;
          btsentedi.Enabled = false;
            if (Getsoinfo())
            {
                this.ShowMsg("出货单查询OK，请确认！", 3);
            }

            if (!getprofitoperation())//利润中心和营运中心
            {
                ShowMsg(SajetCommon.SetLanguage("DN Profit or Operation Error"), 0);
                tbdn.Focus();
                tbdn.SelectAll();
                btnUploadTT.Enabled = false;
            }
            else 
            {
                btnUploadTT.Enabled = true;
              
            }
            btsentedi.Enabled = true;
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
/*
                string sSQL = @" SELECT A.SO_NO,
                                 A.SO_ITEM,
                                 A.SSCC,
                                 B.PART_NO,
                                 A.QTY,
                                 A.USEDQTY,
                                 C.PALLET_WEIGHT,
                                 C.ALLPALLET_WEIGHT,
                                 C.CARTON_WEIGHT,
                                 C.PALLET_LENGTH,
                                 C.PALLET_WIDTH,
                                 C.PALLET_HEIGHT,
                                 C.EDI_FLAG
                            FROM PPSUSER.G_SHIPPING_DETAIL_t             A,
                                 SAJET.SYS_PART                        B,
                                 PPSUSER.G_PALLETEDI_INFO C
                           WHERE A.PART_ID = B.PART_ID
                             AND A.SO_NO = C.SO_NO
                             AND A.SHIPPING_ITEM = C.SO_ITEM
                             AND A.So_No =:dn
                           GROUP BY A.SO_NO,
                                 A.SO_ITEM,
                                 A.SSCC,
                                 B.PART_NO,
                                 A.QTY,
                                 A.USEDQTY,
                                 C.PALLET_WEIGHT,
                                 C.ALLPALLET_WEIGHT,
                                 C.CARTON_WEIGHT,
                                 C.PALLET_LENGTH,
                                 C.PALLET_WIDTH,
                                 C.PALLET_HEIGHT,
                                 C.EDI_FLAG ";
 */
                string sSQL = @" SELECT DISTINCT C.SO_NO,
                  D.MIX_FLAG,
                  D.QTY,
                  D.CQTY,
                  D.USEDQTY,
                  D.SSCC,
                  C.PALLET_WEIGHT,
                  C.ALLPALLET_WEIGHT,
                  C.CARTON_WEIGHT,
                  C.PALLET_LENGTH,
                  C.PALLET_WIDTH,
                  C.PALLET_HEIGHT,
                  C.EDI_FLAG
    FROM (  SELECT AA.SSCC,
                   AA.MIX_FLAG,
                   SUM (QTY) QTY,
                   SUM (USEDQTY) USEDQTY,
                   SUM(CQTY) CQTY
              FROM PPSUSER.G_SHIPPING_DETAIL_t AA
             WHERE AA.SO_NO = :dn
          GROUP BY AA.SSCC, AA.MIX_FLAG) D,
         PPSUSER.G_PALLETEDI_INFO C
   WHERE C.SSCC = D.SSCC AND C.So_No = :dn
GROUP BY C.SO_NO,
         D.MIX_FLAG,
         D.SSCC,
         D.QTY,
         D.CQTY,
         D.USEDQTY,
         C.PALLET_WEIGHT,
         C.ALLPALLET_WEIGHT,
         C.CARTON_WEIGHT,
         C.PALLET_LENGTH,
         C.PALLET_WIDTH,
         C.PALLET_HEIGHT,
         C.EDI_FLAG
ORDER BY SSCC";
                Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "dn", tbdn.Text };
                DataSet sDataSet = ClientUtils.ExecuteSQL(sSQL, Param);
                if (sDataSet.Tables[0].Rows.Count > 0)
                {
                    dtResult.Rows.Clear();
                    for (int i = 0; i < sDataSet.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dtResult.NewRow();
                        dr["出货单号"] = sDataSet.Tables[0].Rows[i]["SO_NO"].ToString();
                        //dr["出货明细行号"] = sDataSet.Tables[0].Rows[i]["SO_ITEM"].ToString();
                        dr["SSCC"] = sDataSet.Tables[0].Rows[i]["SSCC"].ToString();
                        dr["MIX_FLAG"] = sDataSet.Tables[0].Rows[i]["MIX_FLAG"].ToString();
                        dr["CQTY"] = sDataSet.Tables[0].Rows[i]["CQTY"].ToString();
                        dr["出货数量"] = sDataSet.Tables[0].Rows[i]["QTY"].ToString();
                        dr["已扫描数量"] = sDataSet.Tables[0].Rows[i]["USEDQTY"].ToString();
                        dr["栈板重量（kg）"] = sDataSet.Tables[0].Rows[i]["PALLET_WEIGHT"].ToString();
                        dr["整栈板重（kg）"] = sDataSet.Tables[0].Rows[i]["ALLPALLET_WEIGHT"].ToString();
                        dr["所有箱重（kg）"] = sDataSet.Tables[0].Rows[i]["CARTON_WEIGHT"].ToString();
                        dr["L（cm）"] = sDataSet.Tables[0].Rows[i]["PALLET_LENGTH"].ToString();
                        dr["W（cm）"] = sDataSet.Tables[0].Rows[i]["PALLET_WIDTH"].ToString();
                        dr["H（cm）"] = sDataSet.Tables[0].Rows[i]["PALLET_HEIGHT"].ToString();
                        dr["EDI_FLAG"] = sDataSet.Tables[0].Rows[i]["EDI_FLAG"].ToString();
                        dtResult.Rows.Add(dr);
                    }
                    return true;

                }
                else
                {
                    this.ShowMsg("出货单不存在或者没过栈板打印，请确认！", 0);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMsg(ex.Message, 0);
                return false;
            }

        }

        public void updatepallet()
        {
            try
            {
                object[][] Param;
                DataTable dta = this.GetDgvToTable();
                foreach (DataRow r in dta.Rows)
                {
                    string a = r["SSCC"].ToString();
                    string b = r["栈板重量（kg）"].ToString();
                    string c = r["整栈板重（kg）"].ToString();
                    string d = r["所有箱重（kg）"].ToString();
                    string e = r["L（cm）"].ToString();
                    string f = r["W（cm）"].ToString();
                    string g = r["H（cm）"].ToString();
                    string sql = @"UPDATE PPSUSER.G_PALLETEDI_INFO A
                                    SET A.PALLET_WEIGHT =:PALLET_WEIGHT,
                                        A.ALLPALLET_WEIGHT =:ALLPALLET_WEIGHT,
                                        A.CARTON_WEIGHT =:CARTON_WEIGHT,
                                        A.PALLET_LENGTH =:PALLET_LENGTH,
                                        A.PALLET_WIDTH =:PALLET_WIDTH,
                                        A.PALLET_HEIGHT =:PALLET_HEIGHT
                                  WHERE A.SSCC = :SSCC";
                    Param = new object[7][];
                    Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", a};
                    Param[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_WEIGHT",  b};
                    Param[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "ALLPALLET_WEIGHT",  c};
                    Param[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_WEIGHT",  d};
                    Param[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_LENGTH",  e};
                    Param[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_WIDTH",  f};
                    Param[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_HEIGHT",  g};
                    ClientUtils.ExecuteSQL(sql, Param);
                }

                this.ShowMsg("保存成功！", 1);
            }
            catch (Exception ex)
            {
                this.ShowMsg(ex.ToString(), 0);
                MessageBox.Show(ex.ToString());
            }
        }

        private void btupdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            //if(!checkupdate(tbdn.Text.Trim()))
            //{
            //    tbdn.SelectAll();
            //    tbdn.Focus();
            //    dtResult.Rows.Clear();
            //    return;
            //}
            //updatepallet();
            //tbdn.SelectAll();
            //tbdn.Focus();
            update945weight(tbdn.Text);
            update945carton(tbdn.Text);
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


        private DataTable GetTcPo(string dnno) 
        {
            try
            {
                string sSQL = @" SELECT DISTINCT C.SO_NO,D.PO_NO,D.MIX_FLAG,
                  D.QTY,
                  D.USEDQTY,
                  D.SSCC,
                  C.PALLET_WEIGHT,
                  C.ALLPALLET_WEIGHT,
                  C.CARTON_WEIGHT,
                  C.PALLET_LENGTH,
                  C.PALLET_WIDTH,
                  C.PALLET_HEIGHT,
                  C.EDI_FLAG
    FROM (  SELECT AA.SSCC,AA.PO_NO,AA.MIX_FLAG, SUM (QTY) QTY, SUM (USEDQTY) USEDQTY
              FROM PPSUSER.G_SHIPPING_DETAIL_t AA
             WHERE AA.SO_NO = :dn
          GROUP BY AA.SSCC,AA.PO_NO,AA.MIX_FLAG) D,
         PPSUSER.G_PALLETEDI_INFO C
   WHERE C.SSCC = D.SSCC AND C.So_No = :dn 
GROUP BY C.SO_NO,D.PO_NO,D.MIX_FLAG,
         D.SSCC,
         D.QTY,
         D.USEDQTY,
         C.PALLET_WEIGHT,
         C.ALLPALLET_WEIGHT,
         C.CARTON_WEIGHT,
         C.PALLET_LENGTH,
         C.PALLET_WIDTH,
         C.PALLET_HEIGHT,
         C.EDI_FLAG
ORDER BY SSCC";
                object[][] Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "dn", dnno };
                DataSet sDataSet = ClientUtils.ExecuteSQL(sSQL, Param);
                if (sDataSet.Tables[0].Rows.Count > 0)
                {
                    return sDataSet.Tables[0];
                }
                else
                {
                    this.ShowMsg("出货单不存在或者没过栈板打印，请确认！", 0);
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.ShowMsg(ex.Message, 0);
                return null;
            }
        }

        private string GetTpackgeid(string dn,string sscc)
        {
            try 
            {
                string sqlstr = "SELECT PACKAGE_ID  FROM WMUSER.AC_856ASN_PACKAGE where DN=:DN AND SSCC =:SSCC AND ROWNUM=1 ";
                object[][] sqlparams = new object[][]{new object[]{ParameterDirection.Input,OracleType.VarChar,"DN",dn},
                new object[]{ParameterDirection.Input,OracleType.VarChar,"SSCC",sscc}};
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
                if (dt.Rows.Count > 0) 
                {
                    return dt.Rows[0]["PACKAGE_ID"].ToString();
                }
               else
                {
                    ShowMsg("Get T packag id error",0);
                    return string.Empty;

                }
           
            }
            catch (Exception ex) { ShowMsg(ex.Message, 0); return string.Empty; }
        }


        private bool CheckContainer(string dn) 
        {
            try
            {
                string sqlstr = @"SELECT TRANS_MODE
  FROM WMUSER.AC_TMS_RES_HEADER@DGEDI
 WHERE REQ_NUM = (SELECT OGAUD01
                    FROM SZFD.OGA_FILE
                   WHERE OGA01 = :DN AND ROWNUM = 1)";
                object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "DN", dn } };
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
                if (dt.Rows.Count > 0) 
                {
                    string transmode = dt.Rows[0]["TRANS_MODE"].ToString();
                    if (transmode == "04")
                    {
                        sqlstr = "SELECT CONTAINER FROM (SELECT CONTAINER FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE SO_NO=:DN ORDER BY SHIPPING_ITEM ) WHERE ROWNUM=1";

                        string container = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0].Rows[0]["CONTAINER"].ToString();
                        if (string.IsNullOrEmpty(container))
                        {
                            ShowMsg("CONTAINER IS NULL", 0);
                            return false;
                        }
                        else 
                        {
                            return true;
                        }
                    }
                    else 
                    {
                        return true;
                    }
                }
                else
                {
                    ShowMsg("NO Found TRANS_MODE ", 0);
                    return false;
                }
            }
            catch (Exception ex) 
            {
                ShowMsg(ex.Message,0);
                return false;
            }
        }

        private void btsentedi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbdn.Text))
                return;
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("请先点击查询！", "Confirm", MessageBoxButtons.OKCancel);
                return;
            }
            if(!Checkprint())
            {
                MessageBox.Show(tbdn.Text + " 此DN已经上传过，不能重复上传！", "Confirm", MessageBoxButtons.OKCancel);
                return;
            }

            if (!CheckContainer(tbdn.Text.Trim())) 
            {
                return;
            }

            string sRes = "";
            string updateflag = "Y";//判断是否更新945和856 的flag

            btsentedi.Enabled = false;

            //传T层
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {//MIX FLAG
                if (dgvr.Cells[2].Value.ToString() == "001")
                {
                    string nums = GETPACKAGE_ID();//获取package_id
                    string sscca = dgvr.Cells[1].Value.ToString();
                    string cqty = dgvr.Cells[3].Value.ToString();
                    if (!insertpalletinfo(tbdn.Text, sscca, nums, cqty,"T"))//先insert edi的T行
                    {
                        return;
                    }
                }
                else 
                {
                    string nums = GETPACKAGE_ID();//获取package_id
                    string sscca = dgvr.Cells[1].Value.ToString();
                    string cqty = dgvr.Cells[3].Value.ToString();
                    if (!insertpalletinfo(tbdn.Text, sscca, nums, cqty,"M"))//先insert edi的T行
                    {
                        return;
                    }
                }
            }


            //传P层
            DataTable dta = GetTcPo(tbdn.Text.Trim());
            if (dta == null || dta.Rows.Count == 0) 
            {
                return;
            }
            foreach (DataRow r in dta.Rows)
            {
                string dn=r["SO_NO"].ToString();
                string sscct = r["SSCC"].ToString();
                string pono = r["PO_NO"].ToString();
                string mixflag = r["MIX_FLAG"].ToString();
                string nums = string.Empty;
                if (mixflag == "001") 
                {
                    nums = GetTpackgeid(dn, sscct);

                    if (string.IsNullOrEmpty(nums))
                    {
                        return;
                    }
                }

                string sql1 = @"SELECT DISTINCT A.SHIPPING_RECID, A.CARTON_NO,B.MPN,B.PO_NO 
                            FROM PPSUSER.G_SHIPPING_SN A, PPSUSER.G_SHIPPING_DETAIL_t B
                            WHERE A.SHIPPING_RECID = B.SSCC 
                            AND B.SO_NO =:TDN  AND A.DN_ITEM=B.SHIPPING_ITEM
                            AND B.SSCC =:SSCC AND A.PART_ID=B.PART_ID AND B.PO_NO=:PO_NO 
                            GROUP BY A.SHIPPING_RECID, A.CARTON_NO,B.MPN,B.PO_NO";
                object[][] Param = new object[3][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", tbdn.Text };
                Param[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", sscct };
                Param[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PO_NO", pono };
                DataSet ds = ClientUtils.ExecuteSQL(sql1, Param);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string nums1 = GETPACKAGE_ID();//获取package_id
                        string sscc = ds.Tables[0].Rows[i]["SHIPPING_RECID"].ToString();
                        string carton = ds.Tables[0].Rows[i]["CARTON_NO"].ToString();
                        string mpn = ds.Tables[0].Rows[i]["MPN"].ToString();
                        string po = ds.Tables[0].Rows[i]["PO_NO"].ToString();
                            bool flag1 = sentedi(tbdn.Text, nums1, sscc, carton,po, nums, mpn,ref sRes);//insert 856
                            if (flag1)
                            {
                                this.ShowMsg("上传OK！", 1);
                                tbdn.SelectAll();
                                tbdn.Focus();
                            }
                            else
                            {
                                this.ShowMsg(sRes, 0);
                                tbdn.SelectAll();
                                tbdn.Focus();
                                dtResult.Rows.Clear();
                                updateflag = "N";
                                break;//跳出for循环
                            }
                        
                    }
                    if(updateflag == "N")//跳出foreach循环
                    {
                        break;
                    }
                }
            }
            if (updateflag == "Y")//更新flag
            {
                update856data(tbdn.Text);
                update945data(tbdn.Text);
              
                insert945sn(tbdn.Text);
             

            }
            dtResult.Rows.Clear();
        }

        private bool sentedi(string dn,string package_id,string sscc, string carton,string pono,string p_package_id ,string mpn,ref string ttres)
        {
            try
            {
                object[][] Params = new object[8][];
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tdn", dn };
                Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tpackage_id", package_id };
                Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tsscc", sscc };
                Params[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tcarton_no", carton };
                Params[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tp_package_id", p_package_id };
                Params[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tmpn", mpn };
                Params[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TPONO", pono };
                Params[7] = new object[] { ParameterDirection.Output, OracleType.VarChar, "tres", "" };
                DataSet dsTemp = ClientUtils.ExecuteProc("PPSUSER.SJ_INSERT_DGEDI", Params);
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
                //
                string sql1 = @"SELECT WMUSER.AC_856ASN_PACKAGE_PID_SEQ.nextval@DGEDI AS NUMS FROM DUAL";
                DataSet ds = ClientUtils.ExecuteSQL(sql1);
                string nums = ds.Tables[0].Rows[0]["NUMS"].ToString();
                return nums;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Confirm", MessageBoxButtons.OKCancel);
                return null;
            }
        }

        public string update945weight(string dn)
        {
            try
            {
                string nums ="";
                object[][] Params = new object[1][];
                /*
                string sql1 = @"SELECT B.PO_NO, B.LINE_ITEM, SUM(A.CARTON_WEIGHT) as N_weight,SUM(a.allpallet_weight) G_weight
                                  FROM PPSUSER.G_PALLETEDI_INFO  A,
                                       PPSUSER.G_SHIPPING_DETAIL_t B,
                                       WMUSER.AC_945_LINE@DGEDI  C
                                 WHERE A.SO_NO = B.SO_NO
                                   AND A.SO_NO = C.DN
                                   AND B.PO_NO = C.PO_DN_NUM
                                   AND A.SO_NO = :tdn
                                 GROUP BY B.PO_NO, B.LINE_ITEM";
                 */
                string sql1 = @"SELECT B.PO_NO,
         C.ITEM_NUM,
        ROUND(SUM (B.QTY) * T.PRODUCT_WEIGHT,2) N_weight,
         ROUND(SUM (B.QTY) * T.ROUGH_WEIGHT,2) G_weight
    FROM PPSUSER.G_SHIPPING_DETAIL_T B,
         WMUSER.AC_945_LINE  C,
         SAJET.SYS_PART D,
         PPSUSER.G_DS_PARTINFO_T T
   WHERE     B.PART_ID = D.PART_ID
         AND D.PART_NO = T.ICTPN
         AND B.PO_NO = C.PO_DN_NUM
         AND B.LINE_ITEM = C.ITEM_NUM
         AND C.DN = B.SO_NO
         AND B.SO_NO = :TDN
GROUP BY B.PO_NO,
         C.ITEM_NUM,
         T.PRODUCT_WEIGHT,
         T.ROUGH_WEIGHT";
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", dn };
                DataSet ds = ClientUtils.ExecuteSQL(sql1, Params);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string po = ds.Tables[0].Rows[i]["PO_NO"].ToString();
                        string po_line = ds.Tables[0].Rows[i]["ITEM_NUM"].ToString();
                        string N_weight = ds.Tables[0].Rows[i]["N_weight"].ToString();
                        string G_weight = ds.Tables[0].Rows[i]["G_weight"].ToString();

                        string sql2 = @"   UPDATE WMUSER.AC_945_LINE   D
                                             SET D.G_WEIGHT     =:G_weight,
                                                 D.N_WEIGHT     =:N_weight,
                                                 D.W_UNIT       = 'K'
                                           WHERE D.PO_DN_NUM =:TPO
                                             AND D.ITEM_NUM =:TPO_LINE";
                        object[][] Par = new object[4][];
                        Par[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TPO", po };
                        Par[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TPO_LINE", po_line };
                        Par[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "N_weight", N_weight };
                        Par[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "G_weight", G_weight };
                        DataSet dss = ClientUtils.ExecuteSQL(sql2, Par);

                    }
                }
                return nums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Confirm", MessageBoxButtons.OKCancel);
                return null;
            }
        }

        public string update945carton(string dn)
        {
            try
            {
                string nums = "";
                object[][] Params = new object[1][];
                string sql1 = @" SELECT A.PO_NO, A.LINE_ITEM, COUNT(DISTINCT(B.CARTON_NO)) AS CAR_COUNT
                                   FROM PPSUSER.G_SHIPPING_DETAIL_t A, PPSUSER.G_SHIPPING_SN B
                                  WHERE A.DN_ID = B.DN_ID
                                    AND A.SHIPPING_ITEM = B.DN_ITEM
                                    AND A.SO_NO =:tdn
                                  GROUP BY A.PO_NO, A.LINE_ITEM";
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tdn", dn };
                DataSet ds = ClientUtils.ExecuteSQL(sql1, Params);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string po = ds.Tables[0].Rows[i]["PO_NO"].ToString();
                        string po_line = ds.Tables[0].Rows[i]["LINE_ITEM"].ToString();
                        string CAR_COUNT = ds.Tables[0].Rows[i]["CAR_COUNT"].ToString();

                        string sql2 = @"UPDATE WMUSER.AC_945_LINE    D
                                         SET D.CARTON_COUNT =:CAR_COUNT
                                       WHERE D.PO_DN_NUM =:TPO
                                         AND D.ITEM_NUM =:TPO_LINE ";
                        object[][] Par = new object[3][];
                        Par[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TPO", po };
                        Par[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TPO_LINE", po_line };
                        Par[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "CAR_COUNT", CAR_COUNT };
                        DataSet dss = ClientUtils.ExecuteSQL(sql2, Par);

                    }
                }
                return nums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Confirm", MessageBoxButtons.OKCancel);
                return null;
            }
        }

        public string insert945sn(string dn)
        {
            try
            {
                string nums = "";
                object[][] Params = new object[1][];
                string sql1 = @" INSERT INTO WMUSER.AC_945_SN    A --945
                                    (A.MSG_ID,
                                     A.DO_NUM,
                                     A.SEQ,
                                     A.SN,
                                     A.SSCC,
                                     A.DN,
                                     A.DN_LINE,
                                     A.CTN)
                                SELECT B.MSG_ID,
                                       B.DO_NUM,
                                       B.SEQ,
                                       A.SERIAL_NUMBER,
                                       A.SHIPPING_RECID,
                                       C.SO_NO,
                                       A.DN_ITEM,
                                       C.PO_NO
                                  FROM PPSUSER.G_SHIPPING_SN       A,
                                       WMUSER.AC_945_LINE       B,
                                       PPSUSER.G_SHIPPING_DETAIL_T C
                                 WHERE C.PO_NO = B.PO_DN_NUM
                                   AND C.SHIPPING_ID = A.SHIPPING_ID
                                AND A.PART_ID = C.PART_ID AND A.DN_ITEM=C.SHIPPING_ITEM
                                   AND C.SSCC = A.SHIPPING_RECID
                                   AND C.LINE_ITEM = B.ITEM_NUM AND  C.SO_NO=B.DN
                                   AND C.SO_NO =:tdn
                                 GROUP BY B.MSG_ID,
                                          B.DO_NUM,
                                          B.SEQ,
                                          A.SERIAL_NUMBER,
                                          A.SHIPPING_RECID,
                                          C.SO_NO,
                                          A.DN_ITEM,
                                          C.PO_NO";
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tdn", dn };
                DataSet ds = ClientUtils.ExecuteSQL(sql1, Params);

                //AAA
                object[][] Par = new object[1][];
                string sql = @"SELECT A.SHIPPING_RECID, COUNT(*) AS QTY
                                 FROM PPSUSER.G_SHIPPING_SN A, PPSUSER.G_SHIPPING_DETAIL_T B
                                WHERE A.SHIPPING_RECID = B.SSCC AND B.SHIPPING_ITEM=A.DN_ITEM
                                  AND B.SO_NO =:tdn
                                GROUP BY A.SHIPPING_RECID";
                Par[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "tdn", dn };
                DataSet dt = ClientUtils.ExecuteSQL(sql, Par);
                if (dt.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        string sscc = dt.Tables[0].Rows[i]["SHIPPING_RECID"].ToString();
                        string qty = dt.Tables[0].Rows[i]["QTY"].ToString();

                        string sql2 = @" UPDATE WMUSER.AC_945_SN    A
                                         SET A.SSCC_COUNT =:qty
                                       WHERE A.SSCC =:sscc
                                         AND A.DN =:dn ";
                        object[][] pars = new object[3][];
                        pars[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "sscc", sscc };
                        pars[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "qty", qty };
                        pars[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "dn", dn };
                        DataSet dss = ClientUtils.ExecuteSQL(sql2, pars);

                    }
                }

                update945carton(tbdn.Text);
                update945weight(tbdn.Text);
                return nums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Confirm", MessageBoxButtons.OKCancel);
                return null;
            }
        }

        private bool insertpalletinfo(string dn,string sscc,string package_id,string pack_num_man,string ttype)
        {
            try
            {
                string MSG_ID = "";
                string ASN_NUM = "";
                string sql1 = @" SELECT A.*
                                    FROM WMUSER.AC_856ASN_SHIPMENT     A
                                   WHERE A.DN =:TDN";
                object[][] Param = new object[1][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", dn };
                DataSet ds = ClientUtils.ExecuteSQL(sql1, Param);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    MSG_ID = ds.Tables[0].Rows[0]["MSG_ID"].ToString();
                    ASN_NUM = ds.Tables[0].Rows[0]["ASN_NUM"].ToString();
                }
                else
                {
                    ShowMsg("SHIPMENT没有数据或SHIPMENT数据异常，请确认！", 0);
                    return false;
                }

                string sql = string.Format(@"INSERT INTO WMUSER.AC_856ASN_PACKAGE     A
                              (A.MSG_ID,
                               A.ASN_NUM,
                               A.DN,
                               A.PACKAGE_ID,
                               A.HL_CODE,
                               A.QTY,
                               A.SSCC,
                               A.AC_PO_DATE,A.LENGTH, A.WIDTH, A.HEIGHT, A.G_WEIGHT, A.UNIT_WEIGHT,A.UOM_S,A.UOM_WEIGHT,
                               A.UOM_VOL,A.G_VOLUME,A.PACKAGE_CODE,A.PALLET_BLOCKS,A.PALLET_TIER,A.PACK_NUM_MAN)
                              SELECT DISTINCT
       '{0}',
       '{1}',
       A.SO_NO,
       '{2}',
       '{7}',
       d.QTY,
       A.SSCC,
       SYSDATE,
       C.PALLET_LENGTH,
       C.PALLET_WIDTH,
       C.PALLET_HEIGHT,
       ROUND (C.ALLPALLET_WEIGHT,2),
       ROUND (C.CARTON_WEIGHT,2),
       'CM',
       'KG',
       'CR',
       ROUND ((C.PALLET_LENGTH * C.PALLET_WIDTH * C.PALLET_HEIGHT) / 1000000,2) AS CR,
       '4',
       '15',
       C.LAYER,
       '{3}'
  FROM PPSUSER.G_SHIPPING_DETAIL_t A
       LEFT JOIN PPSUSER.G_PALLETEDI_INFO C
          ON A.SSCC = C.SSCC AND A.SO_NO = C.SO_NO
       LEFT JOIN (  SELECT AA.SSCC, SUM (QTY) QTY
                      FROM PPSUSER.G_SHIPPING_DETAIL_t AA
                     WHERE AA.SO_NO = '{4}'
                  GROUP BY AA.SSCC) D
          ON A.SSCC = D.SSCC 
 WHERE A.SO_NO = '{5}' AND A.SSCC = '{6}' ", MSG_ID, ASN_NUM, package_id, pack_num_man, dn, dn, sscc, ttype);
                //object[][] Params = new object[7][];
                //Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", dn };
                //Params[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "DN", dn };
                //Params[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TMSG_ID", MSG_ID };
                //Params[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TASN_NUM", ASN_NUM };
                //Params[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PACKAGE_ID", package_id };
                //Params[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", sscc };
                //Params[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "PACK_NUM_MAN", pack_num_man };
                DataSet dsTemp = ClientUtils.ExecuteSQL(sql);
                return true;
            }
            catch (Exception ex)
            {
                ShowMsg(ex.ToString(),0);
                return false;
            }
        }
        public bool update856data(string dn)
        {
            try
            {
                string sql = @" UPDATE WMUSER.AC_856ASN_SHIPMENT    A
                                 SET A.TRANS_FLAG = 'N', A.LAST_UPDATE = SYSDATE, A.UPDATE_BY = 'PPS',O_CONTAINER_NUM=(SELECT CONTAINER FROM (SELECT CONTAINER FROM PPSUSER.G_SHIPPING_DETAIL_T WHERE SO_NO=:SO_NO ORDER BY SHIPPING_ITEM ) WHERE ROWNUM=1)
                                 WHERE A.DN =:TDN";
                object[][] Param = new object[2][];
                Param[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "TDN", dn };
                Param[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SO_NO", dn };
                DataSet ds = ClientUtils.ExecuteSQL(sql, Param);
                return true;
            }
            catch(Exception ex)
            {
                ShowMsg(ex.ToString(), 0);
                return false;
            }
        }
        public bool update945data(string dn)
        {
            try
            {
                string sql = @" UPDATE WMUSER.AC_945_HEADER    A
                                SET A.TRANS_FLAG = 'N', A.LAST_UPDATE = SYSDATE, A.UPDATE_BY = 'PPS'
                                WHERE A.DN =:TDN";
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

        private string GetWareHoseName(string wareHouseId)
        {
          try
          {
            string sqlstr = "SELECT WAREHOUSE_NO FROM PPSUSER.WMS_WAREHOUSE WHERE WAREHOUSE_ID=:WAREHOUSE_ID AND ROWNUM=1";

            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "WAREHOUSE_ID", wareHouseId } };
            DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
            if (dt.Rows.Count > 0)
            {
              return dt.Rows[0]["WAREHOUSE_NO"].ToString();

            }
            else
            {
              return "";
            }
          }
          catch (Exception ex) 
          {
            MessageBox.Show(ex.Message);
            return "";
          }
        }
        private bool shippingtt()//传数据给TT
        {
            try
            {
                M_Doc M_Doc = new M_Doc();
                string sSQL = @"SELECT A.DN_NO,
                                   C.SHIPPING_ITEM,
                                   B.CONTAINER,
                                   B.RC_NO,
                                   C.QTY
                              FROM PPSUSER.G_SN_STATUS       B,
                                   PPSUSER.G_DN_BASE         A,
                                   PPSUSER.G_SHIPPING_DETAIL_T C
                             WHERE A.DN_ID = C.SHIPPING_ID
                               AND TO_CHAR(A.DN_ID)= B.SHIPPING_ID
                               AND A.DN_NO = :dnno
                             GROUP BY A.DN_NO,
                                      C.SHIPPING_ITEM,
                                      B.CONTAINER,
                                      B.RC_NO,
                                      C.QTY";
                object[][] Params = new object[1][];
                Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "dnno", tbdn.Text };
                DataSet dt = ClientUtils.ExecuteSQL(sSQL, Params);
                M_Doc.User = new User_Account(lboperation.Text, lbprofit.Text);
                M_Doc.M_Ships = new List<M_Ship>() { new M_Ship() };
                M_Doc.M_Ships[0].oga01 = tbdn.Text;
                M_Doc.M_Ships[0].OgcFiles = new List<U_OgcFile>();

                List<string[]> itemnamedata = new List<string[]>();
                if (dt.Tables[0].Rows.Count > 0)
                {
                  string whid = dt.Tables[0].Rows[0]["CONTAINER"].ToString();
                  for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                  {
                    string[] shippitemdata=new string[2];// = ;
                    string itemno=dt.Tables[0].Rows[i]["SHIPPING_ITEM"].ToString();
                    if (itemno.Length > 2)
                    {
                      itemno = itemno.Substring(0, 2);
                      if (itemno.EndsWith("0"))
                      {
                        itemno = itemno.Substring(0, 1);
                      }
                    }
                    shippitemdata[0]=itemno;
                    shippitemdata[1]=dt.Tables[0].Rows[i]["QTY"].ToString();
                    itemnamedata.Add(shippitemdata);

                  }
                  var itemdistinct = itemnamedata.GroupBy(q => q[0]);
                  foreach (var item in itemdistinct)
                  {
                    M_Doc.M_Ships[0].OgcFiles.Add(new U_OgcFile()
                    {
                      ogc03 = item.Key,
                      //ogc09 = dt.Tables[0].Rows[i]["RC_NO"].ToString(),
                      ogc09 = GetWareHoseName(whid),// "FNA", //dt.Tables[0].Rows[i]["CONTAINER"].ToString(),
                      ogc15 = "",
                      ogc12 = item.Sum(q=>int.Parse(q[1].ToString())).ToString()
                    });
                  }

                  #region 拆分的
                  /*
                  for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    { string shippitem=dt.Tables[0].Rows[i]["SHIPPING_ITEM"].ToString();

                      if(shippitem.Length>2)
                      {
                        shippitem=shippitem.Substring(0,2);
                        if(shippitem.EndsWith("0"))
                        {
                          shippitem=shippitem.Substring(0,1);
                        }
                      }
                        M_Doc.M_Ships[0].OgcFiles.Add(new U_OgcFile()
                        {
                            ogc03 =shippitem,
                           
                            //ogc09 = dt.Tables[0].Rows[i]["RC_NO"].ToString(),
                            ogc09 =GetWareHoseName(dt.Tables[0].Rows[i]["CONTAINER"].ToString()),// "FNA", //dt.Tables[0].Rows[i]["CONTAINER"].ToString(),
                            ogc15 = "",
                            ogc12 = dt.Tables[0].Rows[i]["QTY"].ToString()
                        });

                    }
                  */
                  #endregion
                }

                //实例化一个出货
                Shipping Shipping = new Shipping();
                //调用：单个出货方法
                var ResponseModel = Shipping.MoreShip(M_Doc);
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
          btnUploadTT.Enabled = false;
            shippingtt();
        }
    }
}
