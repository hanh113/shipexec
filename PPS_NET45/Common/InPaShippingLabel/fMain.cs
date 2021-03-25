using System;
using System.Data;
using System.Windows.Forms;
using HShippingLabel;
using System.IO;
using SajetClass;
using System.Data.OracleClient;
using System.Reflection;
using System.Drawing;

namespace InPaShippingLabel
{
    public partial class fMain : Form
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);
        //public PrintLabel sPrintLabel;
        public string g_sExeName;
        public string g_sProgram;
        public string g_sFunction;
        public string g_sFunctionType;
        public string g_sUserID;
        public string g_sUserNo;
        public string g_sUserName;
        public string g_sTerminalID = string.Empty;
        public string g_sProcessID = string.Empty;
        public string g_sStageID = string.Empty;
        public string g_sPDLineID = string.Empty;
        public string sPdlineName, sProcessName, sTerminalName, sStageName, sShiftName, sMessage;
        public string g_sIniFile = Application.StartupPath + "\\sajet.ini";
        public string pallet_weight = "";
        string so_no;
        public fMain()
        {
            InitializeComponent();
        }

        private void txt_sscc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                CheckSSCC(txt_shipingNO.Text.Trim());
            }

        }
        private void CheckSSCC(string sSSCC)//产生栈板号
        {
            string sSQL = "SELECT A.INVOICENO,A.HAWB "
                        + " FROM ppsuser.g_shipping_detail a WHERE A.SO_NO='" + sSSCC + "'";
            DataSet sDataSet = ClientUtils.ExecuteSQL(sSQL);
            if (sDataSet.Tables[0].Rows.Count > 0)
            {
                so_no = txt_shipingNO.Text;
                txt_Invoice.Text = sDataSet.Tables[0].Rows[0][0].ToString();
                txt_Hawb.Text = sDataSet.Tables[0].Rows[0][1].ToString();
                btnPrint.Enabled = true;
                btnPrintPacklist.Enabled = true;
                showMessage("出货单号录入成功！", 2);
            }
            else
            {
                showMessage("出货单号错误或不存在！", 1);
                txt_Invoice.Text = "";
                txt_Hawb.Text = "";
                txt_shipingNO.Focus();
                txt_shipingNO.SelectAll();
                return;
            }
        }

        private void fMain_Load(object sender, EventArgs e)
        {

            //sPrintLabel = new PrintLabel();
            // ClientUtils.url = "tcp://172.17.32.23:8085";
            g_sProgram = ClientUtils.fProgramName;
            g_sExeName = ClientUtils.fCurrentProject;
            this.BackgroundImage = ClientUtils.LoadImage("ImgMain.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.WindowState = FormWindowState.Maximized;
            this.Text = this.Text + "(" + SajetCommon.g_sFileVersion + ")";
            cmb_type.Text = "AMR"
;
        }
        private string CheckDirectory()
        {
            string sDirectory = Application.StartupPath + @"\" + g_sProgram + @"\Label";
            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);
            return sDirectory;
        }

        private void PrintRecord()
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_type.Text == "AMR")
            {
                txt_InvoiceLableName.Text = "AMR_Invoice";
                txt_PLLableName.Text = "AMR_Packing List";
            }
            else if (cmb_type.Text == "APAC")
            {
                txt_InvoiceLableName.Text = "APAC_Invoice";
                txt_PLLableName.Text = "APAC_Packing List";
            }
            else if (cmb_type.Text == "EMEA")
            {
                txt_InvoiceLableName.Text = "EMEA_Invoice";
                txt_PLLableName.Text = "EMEA_Packing List";
            }

        }

        private void ClearText()
        {
            foreach (Control sControl in this.Controls)
            {
                if (sControl is TextBox)
                {
                    sControl.Text = string.Empty;
                }
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            foreach (Control sControl in this.Controls)
            {
                if (sControl is TextBox)
                {
                    if (string.IsNullOrEmpty(sControl.Text))
                    {
                        showMessage("有项为空！", 1);
                        return;
                    }
                }
                if (so_no != txt_shipingNO.Text)
                {
                    showMessage("录入的出货单号有变更，请确认！", 1);
                    btnPrint.Enabled = false;
                    txt_shipingNO.Focus();
                    txt_shipingNO.SelectAll();
                    txt_Invoice.Text = "";
                    txt_Hawb.Text = "";
                    return;
                }
            }
            try
            {
                ListView list = new ListView();
                list.Items.Add(txt_shipingNO.Text);
                string tre = PrintInvoice(list);
                if (tre == "OK")
                {
                    showMessage("" + txt_shipingNO.Text + "打印OK!", 2);
                    btnPrint.Enabled = false;
                    txt_shipingNO.Focus();
                    txt_shipingNO.Text = "";
                    txt_Invoice.Text = "";
                    txt_Hawb.Text = "";
                }
                else
                {
                    showMessage(tre, 2);
                }

                //string sMessage = string.Empty;
                //string sTrText = "H06P0000292" + "," + "SHA86429992" + "," + "" + "|" + txt_Start.Text.Trim() + "|" + txt_End.Text.Trim() + "|" + txt_Region.Text.Trim() + "|" + txt_Carrier.Text.Trim() + "|" + txt_TotalCartons.Text.Trim() + "|" + txt_EmptyCartons.Text.Trim();
                //if (sPrintLabel.Print_Bartender_DataSource("PalletSheetlabel", CheckDirectory(), sTrText, 1, out sMessage))
                //{
                //    MessageBox.Show("打印OK", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    PrintRecord();
                //    ClearText();
                //}
                //else
                //{
                //    MessageBox.Show(sMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                showMessage(ex.Message, 2);
                //MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void showMessage(string str, int i)
        {
            if (i == 1)
            {
                txt_MSG.Text = str;
                txt_MSG.BackColor = Color.Yellow;
                txt_MSG.ForeColor = Color.Red;
            }
            if (i == 2)
            {
                txt_MSG.Text = str;
                txt_MSG.BackColor = Color.White;
                txt_MSG.ForeColor = Color.Green;
            }
            if (i == 0)
            {
                txt_MSG.Text = str;
                txt_MSG.BackColor = Color.White;
                txt_MSG.ForeColor = Color.White;
            }
        }


        private string PrintInvoice(ListView list)
        {
            try
            {
                string invoice_labelName = "";
                string packinglist_labelName = "";
                string assyPath = "";
                string linvoice_abelPath = "";
                string packinglist_abelPath = "";
                string sMessage = "";
                string type = "";
                PrintLabel_Bitland.Setup stup = new PrintLabel_Bitland.Setup();
                PrintLabel.Setup PrintLabelDll = new PrintLabel.Setup();
                System.Windows.Forms.ListBox ListParam = new ListBox();
                System.Windows.Forms.ListBox ListData = new ListBox();
                ListParam.Items.Clear();
                ListData.Items.Clear();

                //确认文件名               
                invoice_labelName = "PKL_" + txt_InvoiceLableName.Text;
                packinglist_labelName = "PKL_" + txt_PLLableName.Text;
                assyPath = Assembly.GetExecutingAssembly().Location;
                assyPath = assyPath.Substring(0, assyPath.LastIndexOf('\\'));
                linvoice_abelPath = assyPath + "\\" + invoice_labelName + ".btw";
                packinglist_abelPath = assyPath + "\\" + packinglist_labelName + ".btw";

                ListData.Items.Add(list.Items[0].Text);

                #region
                //if (!File.Exists(labelPath))
                //{
                //    labelName = "PKL_" + txt_shipingNO.Text.Trim();
                //    labelPath = assyPath + "\\" + labelName + ".btw";
                //    if (!File.Exists(labelPath))
                //    {
                //        labelName = "PKL_" + "DEFAULT";
                //        labelPath = assyPath + "\\" + labelName + ".btw";
                //        if (!File.Exists(labelPath))
                //        {
                //            return "Print File not Exist";
                //        }
                //    }
                //}


                /*
                if (!stup.GetPrintData("WC_LABLE", ref ListParam, ref ListData, out sMessage)) //获得列印数据
                {
                    return "Get Print Data Error:" + sMessage;
                }

                //stup.Open("BARTENDER");

                if (!stup.Print_Bartender_DataSource_Single(g_sExeName, "WC_LABLE", "PKL_", invoice_labelName, 1, "BARTENDER", "DATASOURCE", ListParam, ListData, out sMessage))
                {
                    return "Print error:" + sMessage;
                }
                */
                #endregion

                System.Text.StringBuilder Invoicesb = new System.Text.StringBuilder();

                string invoiceSql = string.Empty;

                if (cmb_type.Text == "AMR")
                {
                    #region AMR
                    invoiceSql = String.Format(@"select a.invoice_no,a.hawb,b.ac_pn,b.ac_pn_desc,b.ac_po,b.unit_price,b.qty,a.st_name,a.st_addr
                        ,A.sold_name,0 as cartons,A.sold_name,0 as cartons,b.ac_po_line,a.Carr_code,a.ship_per,a.poe
                        from wmuser.AC_FD_AMR_CI_HEADER@dgedi a,
                        wmuser.AC_FD_AMR_CI_LINE@dgedi b
                        where   
                        a.invoice_no = '{0}' and   
                        a.invoice_no = b.invoice_no
                        ", txt_Invoice.Text.Trim());
                    #endregion
                }
                else if (cmb_type.Text == "APAC")
                {
                    #region APAC
                    invoiceSql = string.Format(@"SELECT distinct a.invoice_no,a.st_addr,a.hawb,a.poe,a.FINAL_DEST,
                                b.ac_pn,b.ac_pn_desc,b.cust_po,b.ac_po_line,b.ac_po,b.qty,b.unit_price,b.model_no
                                FROM wmuser.AC_FD_APAC_CI_HEADER@dgedi a,
                                wmuser.AC_FD_APAC_CI_LINE@dgedi b
                                where a.invoice_no = b.invoice_no
                                and a.invoice_no = '{0}'
                                order by b.ac_po_line", txt_Invoice.Text);
                    #endregion
                }
                else if (cmb_type.Text == "EMEA")
                {
                    #region EMEIA
                    invoiceSql = string.Format(@"SELECT distinct a.invoice_no,a.hawb,a.poe_coc,a.ST_ADDR,a.SOLD_ADDR,a.ship_per,
                                                  b.ac_pn,b.ac_po,b.ac_pn_desc,sum(b.qty) qty,Sum(b.unit_price) unit_price
                                                  FROM wmuser.AC_FD_EMEIA_CI_HEADER@dgedi a,wmuser.AC_FD_EMEIA_CI_LINE@dgedi b
                                                  where   a.invoice_no = '{0}'
                                                  and   a.invoice_no = b.invoice_no
                                                  group by a.invoice_no,a.hawb,a.poe_coc,a.ST_ADDR,a.SOLD_ADDR,a.ship_per,b.ac_pn,b.ac_po,b.ac_pn_desc
                                                  order by b.ac_pn", txt_Invoice.Text);
                    #endregion
                }

                DataTable invoiceDt = ClientUtils.ExecuteSQL(invoiceSql).Tables[0];

                #region Invoice
                if (invoiceDt != null && invoiceDt.Rows.Count > 0)
                {
                    #region
                    string weightSql = string.Format(@"select a.ICTPN,a.PRODUCT_WEIGHT,a.ROUGH_WEIGHT
                            FROM ppsuser.g_ds_partinfo_t a,ppsuser.g_shipping_detail b,sajet.sys_part c
                            WHERE  c.part_id = b.part_id
                            and a.ictpn = c.part_no and b.mpn = '{0}'", invoiceDt.Rows[0]["ac_pn"].ToString());

                    DataTable weightDt = ClientUtils.ExecuteSQL(weightSql).Tables[0];

                    string PRODUCT_WEIGHT = weightDt == null ? "0" : weightDt.Rows.Count > 0 ? weightDt.Rows[0]["PRODUCT_WEIGHT"].ToString() : "0";
                    string ROUGH_WEIGHT = weightDt == null ? "0" : weightDt.Rows.Count > 0 ? weightDt.Rows[0]["ROUGH_WEIGHT"].ToString() : "0";

                    string cartonsSql = string.Format(@" select max(a.end_cartons) as cartons
                                    from ppsuser.g_shipping_detail a
                                    where a.SO_NO = '{0}'", txt_shipingNO.Text);
                    DataTable cartonsDt = ClientUtils.ExecuteSQL(cartonsSql).Tables[0];
                    int cartons = cartonsDt == null ? 0 : cartonsDt.Rows.Count > 0 ? Convert.ToInt32(cartonsDt.Rows[0]["cartons"].ToString()) : 0;

                    #endregion

                    if (cmb_type.Text == "AMR")
                    {
                        #region AMR
                        string palletSql = string.Format(@"select count(*) as pallets from (select count(a.sscc)
                                            from ppsuser.g_shipping_detail a
                                            where a.SO_NO = '{0}'
                                            group by a.sscc) a ", txt_shipingNO.Text);
                        DataTable palletDt = ClientUtils.ExecuteSQL(palletSql).Tables[0];
                        int pallets = cartonsDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;


                        string soSql = string.Format(@" select distinct t.trans_mode from wmuser.ac_tms_req_header@dgedi t,ppsuser.g_shipping_detail b
                                     where t.req_num = b.req_num and b.so_no = '{0}'", txt_shipingNO.Text);
                        DataTable soDt = ClientUtils.ExecuteSQL(soSql).Tables[0];
                        string incotenms = soDt == null ? "" : soDt.Rows.Count > 0 ? soDt.Rows[0]["trans_mode"].ToString() : "0";
                        //Invoicesb.Append(invoiceDt.Rows[0]["invoice_no"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["hawb"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["ac_pn"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["ac_pn_desc"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["ac_po"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["ac_po"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["unit_price"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["qty"].ToString() + "|"
                        //            + (Convert.ToDouble(invoiceDt.Rows[0]["unit_price"].ToString()) * Convert.ToDouble(invoiceDt.Rows[0]["qty"].ToString())).ToString("0.00") + "|"
                        //            + PRODUCT_WEIGHT + "|"
                        //            + ROUGH_WEIGHT + "|"
                        //            + invoiceDt.Rows[0]["st_name"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["st_addr"].ToString() + "|"
                        //            + invoiceDt.Rows[0]["sold_name"].ToString() + "|"
                        //            + cartons + "|"
                        //            + pallets);
                        AMR_Invoice ai = new AMR_Invoice();
                        string amr_Invoice = ai.getAMR_Invoice(invoiceDt, cartonsDt, pallets, incotenms, txt_shipingNO.Text.Trim());
                        Invoicesb.Append(amr_Invoice);
                        #endregion
                    }
                    else if (cmb_type.Text == "APAC")
                    {
                        #region APAC
                        string palletSql = string.Format(@"select count(*) as pallets from (select count(a.sscc)
                                            from ppsuser.g_shipping_detail a
                                            where a.SO_NO = '{0}'
                                            group by a.sscc) a ", txt_shipingNO.Text);
                        DataTable palletDt = ClientUtils.ExecuteSQL(palletSql).Tables[0];
                        int pallets = cartonsDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;

                        string soSql = string.Format(@" select distinct t.trans_mode from wmuser.ac_tms_req_header@dgedi t,ppsuser.g_shipping_detail b
                                     where t.req_num = b.req_num and b.so_no = '{0}'", txt_shipingNO.Text);
                        DataTable soDt = ClientUtils.ExecuteSQL(soSql).Tables[0];
                        string incotenms = soDt == null ? "" : soDt.Rows.Count > 0 ? soDt.Rows[0]["trans_mode"].ToString() : "0";

                        APAC_Invoice ai = new APAC_Invoice();
                        string aa = ai.getAPAC_Invoice(txt_shipingNO.Text, invoiceDt, cartonsDt, incotenms);
                        Invoicesb.Append(aa);
                        #endregion
                    }
                    else if (cmb_type.Text == "EMEA")
                    {
                        #region EMEA
                        string palletSql = string.Format(@"select count(*) as pallets from (select count(a.sscc)
                                            from ppsuser.g_shipping_detail a
                                            where a.SO_NO = '{0}'
                                            group by a.sscc) a ", txt_shipingNO.Text);
                        DataTable palletDt = ClientUtils.ExecuteSQL(palletSql).Tables[0];
                        int pallets = cartonsDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;

                        string soSql = string.Format(@" select distinct t.trans_mode from wmuser.ac_tms_req_header@dgedi t,ppsuser.g_shipping_detail b
                                     where t.req_num = b.req_num and b.so_no = '{0}'", txt_shipingNO.Text);
                        DataTable soDt = ClientUtils.ExecuteSQL(soSql).Tables[0];
                        string incotenms = soDt == null ? "" : soDt.Rows.Count > 0 ? soDt.Rows[0]["trans_mode"].ToString() : "0";

                        EMEA_Invoice ai = new EMEA_Invoice();
                        string aa = ai.getEMEA_Invoice(invoiceDt, cartonsDt, incotenms, txt_shipingNO.Text.Trim(), out type);
                        Invoicesb.Append(aa);
                        #endregion
                    }
                }
                #endregion

                string InvoiceLabelName = @"PKL_EMEA_Invoice";
                string labelPath = //@"D:\MES_CLIENT\Shipping\";
                    Application.StartupPath + "\\Shipping\\";

                if (cmb_type.Text == "AMR")
                {
                    InvoiceLabelName = "PKL_AMR_Invoice";
                }
                else if (cmb_type.Text == "APAC")
                {
                    InvoiceLabelName = "PKL_APAC_Invoice";
                }
                else if (cmb_type.Text == "EMEA")
                {
                    if (type == "AE")
                    {
                        InvoiceLabelName = "PKL_EMEA_InvoiceAE";
                    }
                    else
                    {
                        InvoiceLabelName = "PKL_EMEA_Invoice";
                    }

                }

                bool resultInvoice = Print_Bartender_DataSource(InvoiceLabelName, Path.GetFullPath(labelPath), Invoicesb.ToString(), 1, out sMessage);

                //System.Threading.Thread.Sleep(3 * 1000);//暂停执行3s后执行后面代码


                //if (!stup.Print_Bartender_DataSource_Single(g_sExeName, "WC_LABLE", "PKL_", packinglist_labelName, 1, "BARTENDER", "DATASOURCE", ListParam, ListData, out sMessage))
                //{
                //    return "packinglist_labelName Print error:" + sMessage;
                //}
                //Print_Bartender_DataSource(PackingListLabelName, Path.GetFullPath(labelPath), PackingListsb.ToString(), 1, out sMessage);

                //列印数据放入历史表
                //string cmd = @"insert into sajet.g_label_record(LABEL_TYPE,SN1,USER_ID)values('" + labelName + "','" + tpsn + "'," + ClientUtils.UserPara1 + ")";
                //ClientUtils.ExecuteSQL(cmd);
                if (!resultInvoice)
                {
                    throw new Exception("Invoice打印失败");
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private string PrintPacklist(ListView list)
        {
            try
            {
                string invoice_labelName = "";
                string packinglist_labelName = "";
                string assyPath = "";
                string linvoice_abelPath = "";
                string packinglist_abelPath = "";
                string sMessage = "";
                string type = "";
                PrintLabel_Bitland.Setup stup = new PrintLabel_Bitland.Setup();
                PrintLabel.Setup PrintLabelDll = new PrintLabel.Setup();
                System.Windows.Forms.ListBox ListParam = new ListBox();
                System.Windows.Forms.ListBox ListData = new ListBox();
                ListParam.Items.Clear();
                ListData.Items.Clear();

                //确认文件名               
                invoice_labelName = "PKL_" + txt_InvoiceLableName.Text;
                packinglist_labelName = "PKL_" + txt_PLLableName.Text;
                assyPath = Assembly.GetExecutingAssembly().Location;
                assyPath = assyPath.Substring(0, assyPath.LastIndexOf('\\'));
                linvoice_abelPath = assyPath + "\\" + invoice_labelName + ".btw";
                packinglist_abelPath = assyPath + "\\" + packinglist_labelName + ".btw";

                ListData.Items.Add(list.Items[0].Text);

                #region
                //if (!File.Exists(labelPath))
                //{
                //    labelName = "PKL_" + txt_shipingNO.Text.Trim();
                //    labelPath = assyPath + "\\" + labelName + ".btw";
                //    if (!File.Exists(labelPath))
                //    {
                //        labelName = "PKL_" + "DEFAULT";
                //        labelPath = assyPath + "\\" + labelName + ".btw";
                //        if (!File.Exists(labelPath))
                //        {
                //            return "Print File not Exist";
                //        }
                //    }
                //}


                /*
                if (!stup.GetPrintData("WC_LABLE", ref ListParam, ref ListData, out sMessage)) //获得列印数据
                {
                    return "Get Print Data Error:" + sMessage;
                }

                //stup.Open("BARTENDER");

                if (!stup.Print_Bartender_DataSource_Single(g_sExeName, "WC_LABLE", "PKL_", invoice_labelName, 1, "BARTENDER", "DATASOURCE", ListParam, ListData, out sMessage))
                {
                    return "Print error:" + sMessage;
                }
                */
                #endregion

                System.Text.StringBuilder PackingListsb = new System.Text.StringBuilder();

                string packlistSql = string.Empty;
                string packlistSql2 = string.Empty;
                if (cmb_type.Text == "AMR")
                {
                    #region AMR
                    //packlistSql = string.Format(@"select a.invoice_no,a.hawb,b.ac_po,b.ac_pn,b.ac_pn_desc,4.5 as unit_price,b.qty,a.st_name,0 as cartons
                    //                from wmuser.AC_FD_AMR_PL_HEADER@dgedi a,
                    //                wmuser.AC_FD_AMR_PL_LINE@dgedi b
                    //                where   
                    //                a.invoice_no = '{0}' and   
                    //                a.invoice_no = b.invoice_no", "AMR000245");

                    //此处SQL需合并
                    packlistSql2 = string.Format(@"select a.invoice_no,a.hawb,a.carr_code,a.ship_per,a.st_addr,b.ac_po,b.ac_pn,b.ac_pn_desc,4.5 as unit_price,sum(b.qty) as qty,a.st_name,0 as cartons
                                    from wmuser.AC_FD_AMR_PL_HEADER@dgedi a,
                                    wmuser.AC_FD_AMR_PL_LINE@dgedi b
                                    where   
                                    a.invoice_no = '{0}' and   
                                    a.invoice_no = b.invoice_no
                                    group by  a.invoice_no,
                                   a.hawb,
                                   a.carr_code,
                                   a.ship_per,
                                   a.st_addr,
                                   b.ac_po,
                                   b.ac_pn,
                                   b.ac_pn_desc,
                                   b.qty,
                                   a.st_name
                                    ", txt_Invoice.Text.Trim());
                    packlistSql = string.Format(@"select a.invoice_no,a.hawb,a.carr_code,a.ship_per,a.st_addr,b.ac_po,4.5 as unit_price,a.st_name,0 as cartons
                                    from wmuser.AC_FD_AMR_PL_HEADER@dgedi a,
                                    wmuser.AC_FD_AMR_PL_LINE@dgedi b
                                    where   
                                    a.invoice_no = '{0}' and   
                                    a.invoice_no = b.invoice_no
                                    group by  a.invoice_no,
                                   a.hawb,
                                   a.carr_code,
                                   a.ship_per,
                                   a.st_addr,
                                   b.ac_po,
                                  
                                
                                   a.st_name
                                    ", txt_Invoice.Text.Trim());
                    #endregion
                }
                else if (cmb_type.Text == "APAC")
                {
                    #region APAC
                    packlistSql = string.Format(@"SELECT a.invoice_no,a.st_addr,a.hawb,a.poe,a.FINAL_DEST,
                                b.ac_pn,b.ac_pn_desc,b.ac_po_line,b.ac_po,b.qty,b.model_no
                                FROM wmuser.AC_FD_APAC_PL_HEADER@dgedi a,
                                wmuser.AC_FD_APAC_PL_LINE@dgedi b
                                where a.invoice_no = b.invoice_no
                                and a.invoice_no = '{0}'
                                order by b.ac_po_line", txt_Invoice.Text);
                    #endregion
                }
                else if (cmb_type.Text == "EMEA")
                {
                    #region EMEIA
                    packlistSql = string.Format(@"SELECT distinct a.invoice_no,a.hawb,a.poe_coc,a.ST_ADDR,a.SOLD_ADDR,a.ship_per,
                             b.ac_pn,b.ac_po
                             FROM wmuser.AC_FD_EMEIA_PL_HEADER@dgedi a,
                             wmuser.AC_FD_EMEIA_PL_LINE@dgedi b
                             where a.invoice_no = b.invoice_no 
                             and a.invoice_no = '{0}'", txt_Invoice.Text);
                    #endregion
                }
                DataTable packlistDt = ClientUtils.ExecuteSQL(packlistSql).Tables[0];

                #region Packlist
                if (packlistDt != null && packlistDt.Rows.Count > 0)
                {
                    DataTable packlistDt2 = null;
                    #region
                    string weightSql = string.Empty;
                    if (cmb_type.Text == "AMR")
                    {
                        packlistDt2 = ClientUtils.ExecuteSQL(packlistSql2).Tables[0];
                        weightSql = string.Format(@"select a.ICTPN,a.PRODUCT_WEIGHT,a.ROUGH_WEIGHT
                            FROM ppsuser.g_ds_partinfo_t a,ppsuser.g_shipping_detail b,sajet.sys_part c
                            WHERE  c.part_id = b.part_id
                            and a.ictpn = c.part_no and b.mpn = '{0}'", packlistDt2.Rows[0]["ac_pn"].ToString());
                    }
                    else
                    {
                        weightSql = string.Format(@"select a.ICTPN,a.PRODUCT_WEIGHT,a.ROUGH_WEIGHT
                            FROM ppsuser.g_ds_partinfo_t a,ppsuser.g_shipping_detail b,sajet.sys_part c
                            WHERE  c.part_id = b.part_id
                            and a.ictpn = c.part_no and b.mpn = '{0}'", packlistDt.Rows[0]["ac_pn"].ToString());


                    }
                    DataTable weightDt = ClientUtils.ExecuteSQL(weightSql).Tables[0];
                    string PRODUCT_WEIGHT = weightDt == null ? "0" : weightDt.Rows.Count > 0 ? weightDt.Rows[0]["PRODUCT_WEIGHT"].ToString() : "0";
                    string ROUGH_WEIGHT = weightDt == null ? "0" : weightDt.Rows.Count > 0 ? weightDt.Rows[0]["ROUGH_WEIGHT"].ToString() : "0";

                    string cartonsSql = string.Format(@" select max(a.end_cartons) as cartons
                                    from ppsuser.g_shipping_detail a
                                    where a.SO_NO = '{0}'", txt_shipingNO.Text);
                    DataTable cartonsDt = ClientUtils.ExecuteSQL(cartonsSql).Tables[0];
                    int cartons = cartonsDt == null ? 0 : cartonsDt.Rows.Count > 0 ? Convert.ToInt32(cartonsDt.Rows[0]["cartons"].ToString()) : 0;

                    #endregion

                    if (cmb_type.Text == "AMR")
                    {
                        #region AMR
                        string palletSql = string.Format(@"select count(*) as pallets from (select count(a.sscc)
                                            from ppsuser.g_shipping_detail a
                                            where a.SO_NO = '{0}'
                                            group by a.sscc) a ", txt_shipingNO.Text);
                        DataTable palletDt = ClientUtils.ExecuteSQL(palletSql).Tables[0];
                        int pallets = cartonsDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;

                        //a.invoice_no,a.hawb,b.ac_po,b.ac_pn,b.ac_pn_desc,4.5 as unit_price,b.qty,a.st_name,0 as cartons
                        //PackingListsb.Append(packlistDt.Rows[0]["invoice_no"].ToString() + "|"
                        //            + packlistDt.Rows[0]["hawb"].ToString() + "|"
                        //            + packlistDt.Rows[0]["ac_po"].ToString() + "|"
                        //            + packlistDt.Rows[0]["ac_pn"].ToString() + "|"
                        //            + packlistDt.Rows[0]["ac_pn_desc"].ToString() + "|"
                        //            + packlistDt.Rows[0]["unit_price"].ToString() + "|"
                        //            + packlistDt.Rows[0]["qty"].ToString() + "|"
                        //            + (Convert.ToDouble(packlistDt.Rows[0]["unit_price"].ToString()) * Convert.ToDouble(packlistDt.Rows[0]["qty"].ToString())).ToString("0.00") + "|"
                        //            + PRODUCT_WEIGHT + "|"
                        //            + ROUGH_WEIGHT + "|"
                        //            + packlistDt.Rows[0]["st_name"].ToString() + "|"
                        //            + cartons + "|"
                        //            + DateTime.Now.ToString("yyyy/MM/dd") + "|"
                        //            + pallets);

                        //PackingListsb = new AMR_PackingList().GetAMP_PackinglistStr(txt_shipingNO.Text, packlistDt2, packlistDt);
                        PackingListsb = new AMR_PackingList_New().GetAMP_PackinglistStr(txt_shipingNO.Text, packlistDt2, packlistDt);
                        #endregion
                    }
                    else if (cmb_type.Text == "APAC")
                    {
                        #region APAC
                        string palletSql = string.Format(@"select count(*) as pallets from (select count(a.sscc)
                                            from ppsuser.g_shipping_detail a
                                            where a.SO_NO = '{0}'
                                            group by a.sscc) a ", txt_shipingNO.Text);
                        DataTable palletDt = ClientUtils.ExecuteSQL(palletSql).Tables[0];
                        int pallets = cartonsDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;

                        string soSql = string.Format(@" select distinct t.trans_mode from wmuser.ac_tms_req_header@dgedi t,ppsuser.g_shipping_detail b
                                     where t.req_num = b.req_num and b.so_no = '{0}'", txt_shipingNO.Text);
                        DataTable soDt = ClientUtils.ExecuteSQL(soSql).Tables[0];
                        string incotenms = soDt == null ? "" : soDt.Rows.Count > 0 ? soDt.Rows[0]["trans_mode"].ToString() : "0";

                        APAC_Packlist ai = new APAC_Packlist();
                        string aa = ai.getAPAC_Packlist(txt_shipingNO.Text, packlistDt, cartonsDt, incotenms);
                        PackingListsb.Append(aa);
                        #endregion
                    }
                    else if (cmb_type.Text == "EMEA")
                    {
                        #region EMEIA
                        string palletSql = string.Format(@"select count(*) as pallets from (select count(a.sscc)
                                            from ppsuser.g_shipping_detail a
                                            where a.SO_NO = '{0}'
                                            group by a.sscc) a ", txt_shipingNO.Text);
                        DataTable palletDt = ClientUtils.ExecuteSQL(palletSql).Tables[0];
                        int pallets = cartonsDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;

                        string soSql = string.Format(@" select distinct t.trans_mode from wmuser.ac_tms_req_header@dgedi t,ppsuser.g_shipping_detail b
                                     where t.req_num = b.req_num and b.so_no = '{0}'", txt_shipingNO.Text);
                        DataTable soDt = ClientUtils.ExecuteSQL(soSql).Tables[0];
                        string incotenms = soDt == null ? "" : soDt.Rows.Count > 0 ? soDt.Rows[0]["trans_mode"].ToString() : "0";

                        EMEA_Packlist ai = new EMEA_Packlist();
                        string aa = ai.getEMEA_Packlist(txt_shipingNO.Text, packlistDt, cartonsDt, incotenms, out type);
                        PackingListsb.Append(aa);
                        #endregion
                    }
                }
                #endregion
                string PackingListLabelName = @"PKL_EMEA_Packing List";
                string labelPath = Application.StartupPath + "\\Shipping\\";

                if (cmb_type.Text == "AMR")
                {
                    PackingListLabelName = "PKL_AMR_Packing List";
                }
                else if (cmb_type.Text == "APAC")
                {
                    PackingListLabelName = "PKL_APAC_Packing List";
                }
                else if (cmb_type.Text == "EMEA")
                {
                    if (type == "AE")
                    {
                        PackingListLabelName = "PKL_EMEA_Packing ListAE";
                    }
                    else
                    {
                        PackingListLabelName = "PKL_EMEA_Packing List";
                    }
                }
                bool resultPacklist = false;
                if (cmb_type.Text.Equals("AMR"))
                {
                    ///原来打印多个页面
                    //string[] packingList = PackingListsb.ToString().Split('&');
                    //for (int i = 0; i < packingList.Length - 1; i++)
                    //{
                    //    //bool resultPacklist = Print_Bartender_DataSource(PackingListLabelName, Path.GetFullPath(labelPath), PackingListsb.ToString(), 1, out sMessage);
                    //    resultPacklist = Print_Bartender_DataSource(PackingListLabelName, Path.GetFullPath(labelPath), packingList[i], 1, out sMessage);

                    //    System.Threading.Thread.Sleep(5 * 1000);//暂停执行3s后执行后面代码
                    //}
                    resultPacklist = Print_Bartender_DataSource(PackingListLabelName, Path.GetFullPath(labelPath), PackingListsb.ToString(), 1, out sMessage);

                    System.Threading.Thread.Sleep(3 * 1000);//暂停执行3s后执行后面代码
                }
                else
                {
                    resultPacklist = Print_Bartender_DataSource(PackingListLabelName, Path.GetFullPath(labelPath), PackingListsb.ToString(), 1, out sMessage);
                }

                //if (!stup.Print_Bartender_DataSource_Single(g_sExeName, "WC_LABLE", "PKL_", packinglist_labelName, 1, "BARTENDER", "DATASOURCE", ListParam, ListData, out sMessage))
                //{
                //    return "packinglist_labelName Print error:" + sMessage;
                //}
                //Print_Bartender_DataSource(PackingListLabelName, Path.GetFullPath(labelPath), PackingListsb.ToString(), 1, out sMessage);

                //列印数据放入历史表
                //string cmd = @"insert into sajet.g_label_record(LABEL_TYPE,SN1,USER_ID)values('" + labelName + "','" + tpsn + "'," + ClientUtils.UserPara1 + ")";
                //ClientUtils.ExecuteSQL(cmd);
                if (!resultPacklist)
                {
                    throw new Exception("Packlist打印失败");
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string getweight(string layer, string emptycontainer)
        {
            try
            {
                double a = Convert.ToDouble(layer);
                double b = Convert.ToDouble(emptycontainer);
                string allpalletweight = "";
                object[][] pa = new object[1][];
                string sql = @"SELECT A.MPN,
                           A.AREA,
                           A.TRUNKFUL_WEIGHT,
                           A.TRUNKFUL_COUNT,
                           A.EMPTYCONTAINER_WEIGHT,
                           A.EMPTYCONTAINER_COUNT,
                           A.ALLCONTAINER_WEIGHT,
                           A.ALLEMPTYCONTAINER_WEIGHT,
                           A.VERTICALANGLELAYER_WEIGHT,
                           A.LAYER_QTY,
                           A.VERTICALANGLE_WEIGHT,
                           A.UPPERCORNERPLATE_WEIGHT,
                           A.WORLDCOVER_WEIGHT,
                           A.PALLET_WEIGHT,
                           A.OTHER_WEIGHT,
                           A.ALLPALLET_WEIGHT,
                           A.TOLERANCE
                      FROM PPSUSER.G_SHIPPING_DETAIL B,
                           PPSUSER.SYS_PALLET_INFO   A,
                           SAJET.SYS_PART            C
                     WHERE A.MPN = C.PART_NO
                       AND C.PART_ID = B.PART_ID
                       AND B.SSCC=:SSCC";
                pa[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "SSCC", txt_shipingNO.Text.Trim() };
                DataSet ds = ClientUtils.ExecuteSQL(sql, pa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    double y = Convert.ToDouble(ds.Tables[0].Rows[0]["OTHER_WEIGHT"].ToString());
                    double q = Convert.ToDouble(ds.Tables[0].Rows[0]["ALLCONTAINER_WEIGHT"].ToString());
                    double v = Convert.ToDouble(ds.Tables[0].Rows[0]["UPPERCORNERPLATE_WEIGHT"].ToString());
                    double w = Convert.ToDouble(ds.Tables[0].Rows[0]["WORLDCOVER_WEIGHT"].ToString());
                    double x = Convert.ToDouble(ds.Tables[0].Rows[0]["PALLET_WEIGHT"].ToString());
                    double o = Convert.ToDouble(ds.Tables[0].Rows[0]["EMPTYCONTAINER_WEIGHT"].ToString());
                    double s = Convert.ToDouble(ds.Tables[0].Rows[0]["VERTICALANGLELAYER_WEIGHT"].ToString());

                    double u = s * a;
                    double r = o * b;

                    double z = u + r + q + v + w + x + y;
                    allpalletweight = z.ToString("0.000");
                    pallet_weight = x.ToString();
                }
                return allpalletweight;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnPrintPacklist_Click(object sender, EventArgs e)
        {
            foreach (Control sControl in this.Controls)
            {
                if (sControl is TextBox)
                {
                    if (string.IsNullOrEmpty(sControl.Text))
                    {
                        showMessage("有项为空！", 1);
                        return;
                    }
                }
                if (so_no != txt_shipingNO.Text)
                {
                    showMessage("录入的出货单号有变更，请确认！", 1);
                    btnPrintPacklist.Enabled = false;
                    txt_shipingNO.Focus();
                    txt_shipingNO.SelectAll();
                    txt_Invoice.Text = "";
                    txt_Hawb.Text = "";
                    return;
                }
            }
            try
            {
                ListView list = new ListView();
                list.Items.Add(txt_shipingNO.Text);
                string tre = PrintPacklist(list);
                if (tre == "OK")
                {
                    showMessage("" + txt_shipingNO.Text + "打印OK!", 2);
                    btnPrint.Enabled = false;
                    txt_shipingNO.Focus();
                    txt_shipingNO.Text = "";
                    txt_Invoice.Text = "";
                    txt_Hawb.Text = "";
                }
                else
                {
                    showMessage(tre, 2);
                }

                //string sMessage = string.Empty;
                //string sTrText = "H06P0000292" + "," + "SHA86429992" + "," + "" + "|" + txt_Start.Text.Trim() + "|" + txt_End.Text.Trim() + "|" + txt_Region.Text.Trim() + "|" + txt_Carrier.Text.Trim() + "|" + txt_TotalCartons.Text.Trim() + "|" + txt_EmptyCartons.Text.Trim();
                //if (sPrintLabel.Print_Bartender_DataSource("PalletSheetlabel", CheckDirectory(), sTrText, 1, out sMessage))
                //{
                //    MessageBox.Show("打印OK", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    PrintRecord();
                //    ClearText();
                //}
                //else
                //{
                //    MessageBox.Show(sMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                showMessage(ex.Message, 2);
                //MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DS_Cartons s = new DS_Cartons();
            s.DS_CartonsFunc("DSB_20180524_000001");
        }

        public bool Print_Bartender_DataSource(string sFileName, string sDirectory, string sInputText, int iPrintQty, out string sMessage)
        {
            try
            {
                sMessage = "OK";
                string startupPath = System.Windows.Forms.Application.StartupPath;
                string sSampleFile = sDirectory + @"\" + sFileName + ".btw";
                string str7 = sDirectory + sFileName + ".lst";
                string str8 = sDirectory + sFileName + ".dat";
                string sFile = startupPath + @"\PrintGo.bat";
                string str11 = startupPath + @"\PrintLabel.bat";
                string sData = this.LoadBatFile(str11, ref sMessage);
                string str9 = string.Empty;
                if (!File.Exists(sSampleFile))
                {
                    sMessage = "Sample File Not exists-" + sFileName;
                    return false;
                }
                if (!string.IsNullOrEmpty(sMessage))
                {
                    return false;
                }
                if (File.Exists(str7))
                {
                    File.Delete(str7);
                }
                if (!File.Exists(str8))
                {
                    sMessage = "Label File Not Found (.dat)" + Environment.NewLine + Environment.NewLine + str8 + Environment.NewLine + Environment.NewLine;
                    return false;
                }
                str9 = Readtxt(str8);
                this.WriteToTxt(str7, str9 + "\r\n" + sInputText);
                string path = sSampleFile;
                sData = sData.Replace("@PATH1", '"' + path + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", iPrintQty.ToString());
                this.WriteToPrintGo(sFile, sData);
                int num4 = WinExec(sFile, 0);
                sMessage = "OK";
                return true;
            }
            catch (Exception ex)
            {
                sMessage = ex.Message;
                return false;
            }
        }

        private string LoadBatFile(string sFile, ref string sMessage)
        {
            sMessage = string.Empty;
            string str = string.Empty;
            if (!File.Exists(sFile))
            {
                sMessage = "File not exist - " + sFile;
                return str;
            }
            StreamReader reader = new StreamReader(sFile);
            try
            {
                str = reader.ReadLine().Trim();
            }
            finally
            {
                reader.Close();
            }
            return str;
        }
        private void WriteToPrintGo(string sFile, string sData)
        {
            try
            {
                if (File.Exists(sFile))
                {
                    File.Delete(sFile);
                }
                File.AppendAllText(sFile, sData, System.Text.Encoding.Default);
            }
            finally
            {
            }
        }
        private string Readtxt(string sFile)
        {
            try
            {
                string sData = string.Empty;
                using (StreamReader _sr = new StreamReader(sFile))
                {
                    sData = _sr.ReadLine();
                    return sData;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {

            }
        }
        private void WriteToTxt(string sFile, string sData)
        {
            StreamWriter writer = null;
            try
            {
                using (writer = new StreamWriter(sFile, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine(sData);
                    writer.Flush();
                    writer.Close();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

    }
}
