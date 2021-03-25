using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace CreatePPSDataPpart
{
    public partial class MainForm : Form
    {
        DataTable PARTList = new DataTable();
        int ID = 1;
        string DNNAME;
        string DNLINEITEM;
        string SHIPMENTID;
        string PARTNO;

        string CARLINENO;

        String CarNo;
        string GroupCode;

        public MainForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ClientUtils.ServerUrl = string.Format("http://{0}:8090/WCF_RemoteObject", ConfigurationManager.AppSettings["AP"]);
            //  ClientUtils.ServerUrl = "http://10.171.16.201:8090/WCF_RemoteObject";
            // KS ap server:
            //ClientUtils.ServerUrl = "http://10.33.20.185:8090/WCF_RemoteObject";
            //ClientUtils.ServerUrl = "http://172.23.28.52:8090/WCF_RemoteObject";
            //ClientUtils.url = "TCP://10.33.20.177:8085";
            ///ClientUtils.url = "TCP://10.103.16.59:8085";
            initComboBox();

            _printlabel = new HShippingLabel.PrintLabel();
            //cmbCartonQty.SelectedIndex = 9;

            PARTList.Columns.Add("ID", System.Type.GetType("System.String"));
            PARTList.Columns.Add("LOCATIONNO", System.Type.GetType("System.String"));
            PARTList.Columns.Add("PARTNO", System.Type.GetType("System.String"));
            PARTList.Columns.Add("CARTONCOUNT", System.Type.GetType("System.String"));
            PARTList.Columns.Add("CSNTYPE", System.Type.GetType("System.String"));
            PARTList.Columns.Add("CARTONTYPE", System.Type.GetType("System.String"));

        }
        geneCSNfunc gcf = new geneCSNfunc();
        CommClass cc = new CommClass();


        private void initComboBox()
        {

            //DN
            cmbDN.DataSource = null;
            cmbDN.Items.Clear();
            string strSqlDN = @" select  toi.delivery_no || '*' || toi.line_item || '*' ||                 toi.shipment_id || '*' || toi.ictpn || '*' || (TOI.QTY-NVL(b.QTY,0)) || '*' || (TOI.CARTON_QTY-NVL(b.CartonQty,0))
  ||'*' || NVL(VPSG.GROUP_CODE,'XXXX')
  
  as DN,                 toi.shipment_id,                 toi.delivery_no   
  FROM PPSUSER.T_ORDER_INFO toi

  INNER JOIN PPSUSER.T_SHIPMENT_INFO tsi ON toi.SHIPMENT_ID=tsi.SHIPMENT_ID

  LEFT JOIN PPSUSER.VW_PARKING_SEATING_GROUP vpsg ON TSI.CARRIER_CODE=vpsg.CARRIER
  AND TSI.POE=vpsg.POE AND NVL(TSI.SERVICE_LEVEL,'NA') = NVL(vpsg.SERVICELEVEL,'NA')
  AND TOI.ICTPN=vpsg.ICT_PARTNO

  LEFT JOIN (
    SELECT TTSS.DELIVERY_NO,TTSS.LINE_ITEM,TSS.PART_NO,COUNT(*) AS QTY,count(DISTINCT tss.CARTON_NO) AS CartonQty FROM PPSUSER.T_TROLLEY_SN_STATUS ttss INNER JOIN PPSUSER.T_SN_STATUS tss
      ON ttss.CUSTOM_SN=TSS.CUSTOMER_SN
      GROUP BY TTSS.DELIVERY_NO,TTSS.LINE_ITEM,TSS.PART_NO)B ON TOI.DELIVERY_NO=B.DELIVERY_NO AND TOI.LINE_ITEM=B.LINE_ITEM
  AND TOI.ICTPN=B.PART_NO 
  WHERE toi.PERSON_FLAG='Y' AND toi.STATUS IN ('WP','IP') AND tsi.STATUS  IN ('WP','IP') AND TOI.QTY>NVL(B.QTY,0) ORDER BY shipment_id desc, delivery_no asc";

            DataSet dtsdn = ClientUtils.ExecuteSQL(strSqlDN);
            if (dtsdn != null && dtsdn.Tables[0].Rows.Count > 0)
            {


                List<string> carrierListdn = (from d in dtsdn.Tables[0].AsEnumerable()
                                              select d.Field<string>("DN")).ToList();
                //carrierList.Sort();
                cmbDN.DataSource = carrierListdn;

            }
            else
            {
                cmbDN.DataSource = null;
            }
        }

        private void initcmbCAR(String GroupCode)
        {

            //CAR
            cmbCAR.Text = "";
            cmbCAR.DataSource = null;
            cmbCAR.Items.Clear();
            string strSqlDN = @"
    SELECT  trolley_line_no ||  '*' || UsedQty ||  '*' || MAXQTY  CAR FROM ppsuser.T_TROLLEY_LINE_INFO ttli WHERE ttli.UsedQty<MaxQty AND (NVL(GROUP_CODE,'NA')='NA' OR ttli.GROUP_CODE='" + GroupCode + "' )   order by trolley_line_no asc";

            DataSet dtscar = ClientUtils.ExecuteSQL(strSqlDN);
            if (dtscar != null && dtscar.Tables[0].Rows.Count > 0)
            {


                List<string> carlist = (from d in dtscar.Tables[0].AsEnumerable()
                                        select d.Field<string>("CAR")).ToList();
                //carrierList.Sort();
                cmbCAR.DataSource = carlist;
            }
            else
            {
                cmbCAR.DataSource = null;
            }


        }
        private void initcmbpart(string partno)
        {
            //MPN
            cmbPartno.DataSource = null;
            cmbPartno.Items.Clear();
            string strSql = string.Format("select trim(ICTPARTNO || '*' || MPN || '*' || PACKUNIT || '*' || TOTALQTY) as MPN1 "
                                         + " from ppsuser.vw_mpn_info "
                                         + " where (packcode <> subpackcode "
                                         + "   or subpackcode is null)  "
                                         + "  and ICTPARTNO = '{0}' "
                                         + " order by MPN1 ", partno);

            DataSet dts = ClientUtils.ExecuteSQL(strSql);
            if (dts != null && dts.Tables[0].Rows.Count > 0)
            {


                List<string> carrierList = (from d in dts.Tables[0].AsEnumerable()
                                            select d.Field<string>("MPN1")).ToList();
                carrierList.Sort();
                cmbPartno.DataSource = carrierList;

            }
            else
            {
                cmbPartno.DataSource = null;
            }
        }
        HShippingLabel.PrintLabel _printlabel;


        string partID = string.Empty;
        string locationID = string.Empty;
        private void btnCreateCarton_Click(object sender, EventArgs e)
        {
            //string DNNAME;
            //string DNLINEITEM;
            //string SHIPMENTID;
            //string PARTNO;

            //string CARLINENO;

            if (int.Parse(lblMaxQty.Text) - int.Parse(lblCurrCarqty.Text) < nudCartonCount.Value)
            {
                MessageBox.Show("车行号放不下这么多");
                return;
            }

            if (int.Parse(lblDNcaton.Text) < nudCartonCount.Value)
            {
                MessageBox.Show("DN 不需要这么多");
                return;
            }

            btnCreateCarton.Enabled = false;

            string strpartno = PARTNO;
            string strpackunit = cmbCartonQty.Text;
            string LocationId, LocationNo;
            string strCoo = cmbCoo.Text;

            if (string.IsNullOrEmpty(strpartno))
            {
                btnCreateCarton.Enabled = true;
                return;
            }

            txtCartons.Clear();
            if (!cc.CheckPartNo(strpartno, out partID))
            {
                MessageBox.Show("料号 错误");
                btnCreateCarton.Enabled = true;
                return;
            }
            //LocationNo = cmbLocation.Text;
            //LocationId = cmbLocation.SelectedValue.ToString();

            LocationNo = "SYS";
            LocationId = "10000005";


            string strCARTONCOUNT = nudCartonCount.Value.ToString();
            string strCSNTYPE = getCSNType().ToString();
            string strCARTONTYPE = getCartonType().ToString();
            string strResult = CreateCSN2(strpartno, strCARTONCOUNT, strCSNTYPE, strCARTONTYPE, DNNAME, DNLINEITEM, CARLINENO, LocationNo, LocationId, lblCurrCarqty.Text, strCoo);


            initComboBox();
            grpList.Text = "产生序号库位 List0:";
            btnCreateCarton.Enabled = true;
        }
        private void Carton_all_generate(string strCARTONCOUNT)
        {
            //string DNNAME;
            //string DNLINEITEM;
            //string SHIPMENTID;
            //string PARTNO;

            //string CARLINENO;

            if (int.Parse(lblMaxQty.Text) - int.Parse(lblCurrCarqty.Text) < nudCartonCount.Value)
            {
                MessageBox.Show("车行号放不下这么多");
                return;
            }

            if (int.Parse(lblDNcaton.Text) < nudCartonCount.Value)
            {
                MessageBox.Show("DN 不需要这么多");
                return;
            }

            btnCreateCarton.Enabled = false;

            string strpartno = PARTNO;
            string strpackunit = cmbCartonQty.Text;
            string LocationId, LocationNo;
            string strCoo = cmbCoo.Text;

            if (string.IsNullOrEmpty(strpartno))
            {
                btnCreateCarton.Enabled = true;
                return;
            }

            txtCartons.Clear();
            if (!cc.CheckPartNo(strpartno, out partID))
            {
                MessageBox.Show("料号 错误");
                btnCreateCarton.Enabled = true;
                return;
            }
            //LocationNo = cmbLocation.Text;
            //LocationId = cmbLocation.SelectedValue.ToString();

            LocationNo = "SYS";
            LocationId = "10000005";

            //string strCARTONCOUNT = nudCartonCount.Value.ToString();
            string strCSNTYPE = getCSNType().ToString();
            string strCARTONTYPE = getCartonType().ToString();
            string strResult = CreateCSN3(strpartno, strCARTONCOUNT, strCSNTYPE, strCARTONTYPE, DNNAME, DNLINEITEM, CARLINENO, LocationNo, LocationId, lblCurrCarqty.Text, strCoo);


            //initComboBox();
            grpList.Text = "产生序号库位 List0:";
            btnCreateCarton.Enabled = true;
        }
        private void Carton_all_generate_print(string strCARTONCOUNT)
        {
            //string DNNAME;
            //string DNLINEITEM;
            //string SHIPMENTID;
            //string PARTNO;

            //string CARLINENO;

            if (int.Parse(lblMaxQty.Text) - int.Parse(lblCurrCarqty.Text) < nudCartonCount.Value)
            {
                MessageBox.Show("车行号放不下这么多");
                return;
            }

            if (int.Parse(lblDNcaton.Text) < nudCartonCount.Value)
            {
                MessageBox.Show("DN 不需要这么多");
                return;
            }

            btnCreateCarton.Enabled = false;

            string strpartno = PARTNO;
            string strpackunit = cmbCartonQty.Text;
            string LocationId, LocationNo;
            string strCoo = cmbCoo.Text;

            if (string.IsNullOrEmpty(strpartno))
            {
                btnCreateCarton.Enabled = true;
                return;
            }

            txtCartons.Clear();
            if (!cc.CheckPartNo(strpartno, out partID))
            {
                MessageBox.Show("料号 错误");
                btnCreateCarton.Enabled = true;
                return;
            }
            //LocationNo = cmbLocation.Text;
            //LocationId = cmbLocation.SelectedValue.ToString();

            LocationNo = "SYS";
            LocationId = "10000005";


            //string strCARTONCOUNT = nudCartonCount.Value.ToString();
            string strCSNTYPE = getCSNType().ToString();
            string strCARTONTYPE = getCartonType().ToString();
            string strResult = CreateCSN2(strpartno, strCARTONCOUNT, strCSNTYPE, strCARTONTYPE, DNNAME, DNLINEITEM, CARLINENO, LocationNo, LocationId, lblCurrCarqty.Text, strCoo);


            //initComboBox();
            grpList.Text = "产生序号库位 List0:";
            btnCreateCarton.Enabled = true;
        }

        private void btnPrintCarton_Click(object sender, EventArgs e)
        {

            cc.Print_DNLabel(txtCartons.Lines);
        }

        private void btnClearLocation_Click(object sender, EventArgs e)
        {
            if (!cc.CheckLocationId(cmbLocation.Text.Trim(), out locationID))
            {
                MessageBox.Show("储位 错误");
                return;
            }
            if (cc.ClearLocation(locationID))
            {
                MessageBox.Show("储位已清空");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateCSN gc = new GenerateCSN();
            gc.ShowDialog();
        }

        private void cmbDN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbDN.Text))
            {
                //a.delivery_no || '*' || a.line_item || '*' ||
                //a.shipment_id || '*' || a.ictpn || '*' || a.assign_qty || '*' ||
                //a.assign_carton as DN,
                string strdnlist = cmbDN.Text;
                string[] strdn = strdnlist.Split('*');
                DNNAME = strdn[0];
                DNLINEITEM = strdn[1];
                SHIPMENTID = strdn[2];
                PARTNO = strdn[3];
                lblDNqty.Text = strdn[4];
                lblDNcaton.Text = strdn[5];
                GroupCode = strdn[6];
                GetCooByDN(DNNAME, DNLINEITEM, SHIPMENTID, PARTNO);
                initcmbpart(PARTNO);
                initcmbCAR(GroupCode);
            }

        }

        private void GetCooByDN(string dNNAME, string dNLINEITEM, string sHIPMENTID, string pARTNO)
        {
            cmbCoo.DataSource = null;
            string strSql = string.Format("select distinct coo from ppsuser.t_pallet_order where " +
                "shipment_id ='{0}' and delivery_no='{1}' and line_item='{2}' and ictpn='{3}' ", sHIPMENTID, dNNAME, dNLINEITEM, pARTNO);
            string strSql1 = string.Format("select ppsuser.getcoobykp('{0}',0) as coo from dual", pARTNO);
            DataSet dts2 = new DataSet();
            DataSet dts = ClientUtils.ExecuteSQL(strSql);
            if (dts != null && dts.Tables[0].Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dts.Tables[0].Rows[0]["coo"].ToString()))
                {
                    dts2 = ClientUtils.ExecuteSQL(strSql1);
                    cmbCoo.DataSource = dts2.Tables[0];
                    cmbCoo.DisplayMember = "coo";
                    cmbCoo.ValueMember = "coo";
                }
                else
                {
                    cmbCoo.DataSource = dts.Tables[0];
                    cmbCoo.DisplayMember = "coo";
                    cmbCoo.ValueMember = "coo";
                }
            }
            else
            {
                dts2 = ClientUtils.ExecuteSQL(strSql1);
                cmbCoo.DataSource = dts2.Tables[0];
                cmbCoo.DisplayMember = "coo";
                cmbCoo.ValueMember = "coo";
            }
        }

        private void cmbCAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbCAR.Text))
            {
                // a.trolley_line_no id// count(b.custom_sn)
                string strcarlist = cmbCAR.Text;
                string[] strcar = strcarlist.Split('*');
                string Sql = "";
                CARLINENO = strcar[0];
                lblCurrCarqty.Text = strcar[1];
                lblMaxQty.Text = strcar[2];

                CarNo = strcarlist.Substring(0, strcarlist.LastIndexOf("-"));

                Sql = "SELECT tlt.LOCATION_ID FROM PPSUSER.T_LOCATION_TROLLEY tlt WHERE tlt.TROLLEY_NO='" + CarNo + "'";
                DataSet dts = ClientUtils.ExecuteSQL(Sql);

                //cmbLocation.Items.Clear();

                Sql = "   SELECT * FROM ppsuser.wms_location WL WHERE WL.WAREHOUSE_ID='1' AND LENGTH(WL.LOCATION_NO)<10   AND WL.ENABLED = 'Y'";


                if (dts.Tables[0].Rows.Count > 0)
                {
                    //;
                    Sql = Sql + " and Location_id='" + dts.Tables[0].Rows[0][0].ToString() + "'";

                    //   cmbLocation.Enabled = false;
                }
                else
                {
                    cmbLocation.Enabled = true;
                }
                ;
                Sql = Sql + "ORDER BY WL.LOCATION_NO";
                dts = ClientUtils.ExecuteSQL(Sql);
                DataTable cl = dts.Tables[0];
                cmbLocation.Text = "";
                cmbLocation.DataSource = cl;
                cmbLocation.DisplayMember = "Location_No";
                cmbLocation.ValueMember = "Location_id";
                cmbLocation.Refresh();






            }
        }
        private void cmbPartno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPartno.Text))
            {
                string ictpartnompn = cmbPartno.Text;
                string[] partmpn = ictpartnompn.Split('*');
                string strpartno = partmpn[0];
                string strmpn = partmpn[1];
                string strpackunit = partmpn[2];
                cmbCartonQty.Text = strpackunit;
                //通过料号查upccode 
                string strUPC = string.Empty;
                string strJAN = string.Empty;
                string strCustModel = string.Empty;
                txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
                lblJAN.Text = strJAN;
                lblUPC.Text = strUPC;
                lblModel.Text = strCustModel;
                if (int.Parse(strpackunit) > 1)
                {
                    rdoSCCarton.Checked = true; ;
                }
                else
                {
                    rdoNCarton.Checked = true;
                }
                //if (strmpn.Substring(0, 3).Equals("MME"))
                //{ lblModel.Text = "B188"; }
                //else if (strmpn.Substring(0, 3).Equals("MUF"))
                //{ lblModel.Text = "B501"; }
                //else if (strmpn.Substring(0, 3).Equals("MLL"))
                //{ lblModel.Text = "A145"; }

            }
        }


        private void txtCartonRePrint_KeyDown(object sender, KeyEventArgs e)
        {

            string strCarton = txtCartonRePrint.Text.Trim();
            if (strCarton.Length == 20 && strCarton.Substring(0, 2).Equals("00"))
            { strCarton = strCarton.Substring(2); }

            if (e.KeyCode != Keys.Enter)
            { return; }
            if (string.IsNullOrEmpty(strCarton))
            {
                MessageBox.Show("输入的Carton不能为空！");
                txtCartonRePrint.SelectAll();
                return;
            }

            string sql = string.Empty;
            sql = string.Format(@"select a.ictpartno,
                                                   a.upc_code,
                                                   a.jan_code,
                                                   a.custmodel,
                                                   a.MPN,
                                                   a.packunit,
                                                   a.TOTALQTY,
                                                   case
                                                     when a.custmodel = 'B188' then
                                                      decode(a.jan_code, '', a.upc_code, a.jan_code)
                                                     when a.custmodel = 'B501' then
                                                      decode(a.upc_code, '', a.jan_code, a.upc_code)
                                                     else
                                                      a.upc_code
                                                   end GTIN,
                                                   b.customer_sn,
                                                   b.coo
                                              from ppsuser.t_sn_status b
                                              join ppsuser.vw_mpn_info a
                                                on b.part_no = a.ICTPARTNO
                                             where (a.packcode <> subpackcode or a.subpackcode is null)
                                               and b.carton_no = '{0}'", strCarton);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e1)
            {
                System.Windows.Forms.MessageBox.Show(e1.ToString());
                return;
            }
            if (dataSet.Tables[0].Rows.Count < 1)
            {
                return;
            }
            string strmpn = dataSet.Tables[0].Rows[0]["MPN"].ToString();
            string strpartno = dataSet.Tables[0].Rows[0]["ictpartno"].ToString();
            string strpackunit = dataSet.Tables[0].Rows[0]["packunit"].ToString();
            string strCoo = dataSet.Tables[0].Rows[0]["coo"].ToString();
            //通过料号查upccode 
            string strUPC = string.Empty;
            string strJAN = string.Empty;
            string strCustModel = string.Empty;
            txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
            string strmodel = string.Empty;
            strmodel = strCustModel;
            //if (strmpn.Substring(0, 3).Equals("MME"))
            //{ strmodel = "B188"; }
            //else if (strmpn.Substring(0, 3).Equals("MUF"))
            //{ strmodel = "B501"; }
            //else if (strmpn.Substring(0, 3).Equals("MLL"))
            //{ strmodel = "A145"; }


            string CSN = string.Empty;
            //定义一个序号的 起始变量， 不因程序初始而变动， 每天变化。

            int qty = 0;
            qty = int.Parse(strpackunit);


            //定义一个箱号的 起始变量，一直用这个文件。
            //000 88590 99870 25102
            //0885909 987025102~987030102

            string strGS1snlist = string.Empty;
            strGS1snlist = "SSCC|CSN1|CSN2|CSN3|CSN4|CSN5|CSN6|CSN7|CSN8|CSN9|CSN10|COO|" + "\r\n";

            string strSN2D = string.Empty;
            string strGTIN = string.Empty;
            strGTIN = txtGTIN.Text.PadLeft(14, '0');
            string strGS1cartonlist = string.Empty;
            strGS1cartonlist = "SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN|COO1|COO2|COO3|" + "\r\n";

            string snlist = string.Empty;
            string ssnlist = string.Empty;

            for (int m = 0; m < dataSet.Tables[0].Rows.Count; m++)
            {
                CSN = dataSet.Tables[0].Rows[m]["customer_sn"].ToString();
                snlist += CSN + "|";
                ssnlist += "S" + CSN + ",";
            }

            strGS1snlist += strCarton + "|" + snlist + cc.GetCoo(strCoo, 4) + "|" + "\r\n";
            string strQTY = qty.ToString().PadLeft(2, '0');
            //SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN
            strSN2D = "V3," + "SSCC" + strCarton + ",GTIN" + strGTIN + ",MPN" + strmpn + ",QTY" + strQTY + "," + ssnlist;
            strGS1cartonlist += strCarton + "|" + strmpn + "|" + strSN2D + "|" + strGTIN + "|" + qty + "|" + strGTIN + strQTY + "|" + strUPC + "|" + strJAN + "|" + cc.GetCoo(strCoo, 4) + "|" + cc.GetCoo(strCoo, 6) + "|" + cc.GetCoo(strCoo, 5) + "\r\n";


            //产生2个lst文件，然后打印
            string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";

            string strBTWname = string.Empty;
            if (strmodel.Equals("B188"))
            {
                strBTWname = "B188_GS1_MAIN";
            }
            else if (strmodel.Equals("B288") || strmodel.Equals("B298"))
            {
                strBTWname = "B288_GS1_MAIN";
            }
            else if (strmodel.Equals("A145"))
            {
                if (strmpn.Contains("MLL82AM"))
                { strBTWname = "A145_GS1_MAIN_A"; }
                else if (strmpn.Contains("MLL82FE"))
                { strBTWname = "A145_GS1_MAIN_F"; }
                else
                { strBTWname = "A145_GS1_MAIN"; }

            }
            else if (strmodel.Equals("B501"))
            {
                if (strmpn.Contains("MUFM2J"))
                { strBTWname = "B501_GS1_MAIN_J"; }
                else
                { strBTWname = "B501_GS1_MAIN"; }
            }

            string str7 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_MAIN" + ".lst";
            string str8 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN" + ".lst";
            cc.WriteToPrintGo(str7, strGS1cartonlist);
            cc.WriteToPrintGo(str8, strGS1snlist);
            using (Process p = new Process())
            {
                string strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + strBTWname + ".btw";
                if (!File.Exists(strSampleFile))
                {
                    MessageBox.Show("Sample File Not exists-" + strSampleFile);
                    return;
                }
                p.StartInfo.FileName = "bartend.exe";
                string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", "1");
                p.StartInfo.Arguments = sArguments;
                p.Start();
                p.WaitForExit();
            }
            if (strmodel.Equals("B501") || strmodel.Equals("B188") || strmodel.Equals("B288") || strmodel.Equals("B298"))
            {
                using (Process p = new Process())
                {
                    string strSampleFile = string.Empty;
                    if (qty == 10)
                    {
                        strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN10" + ".btw";
                    }
                    else if (qty == 4)
                    {
                        strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN4" + ".btw";
                    }
                    else
                    {
                        return;
                    }
                    if (!File.Exists(strSampleFile))
                    {
                        MessageBox.Show("Sample File Not exists-" + strSampleFile);
                        return;
                    }
                    p.StartInfo.FileName = "bartend.exe";
                    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str8 + '"').Replace("@QTY", "1");
                    p.StartInfo.Arguments = sArguments;
                    p.Start();
                    p.WaitForExit();
                }
            }
            txtCartonRePrint.SelectAll();

        }


        private void rdoOneLocation_CheckedChanged(object sender, EventArgs e)
        {
            //dgvPART = null;
            if (rdoOneLocation.Checked)
            {
                btnCreateCarton.Enabled = true;

                dgvPART.Enabled = false;
                btnADD.Enabled = false;
                btnCreateCarton2.Enabled = false;
            }
            else
            {
                btnCreateCarton.Enabled = false;

                dgvPART.Enabled = true;
                btnADD.Enabled = true;
                btnCreateCarton2.Enabled = true;
            }
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string ictpartnompn = cmbPartno.Text;

            string[] partmpn = ictpartnompn.Split('*');
            string strpartno = partmpn[0];
            string strmpn = partmpn[1];
            string strpackunit = partmpn[2];
            string strPALLETQTY = partmpn[3];

            if (string.IsNullOrEmpty(strpartno)) return;

            if (grpList.Text.Contains("0"))
            {
                int count = dgvPART.Rows.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    dgvPART.Rows.RemoveAt(i);
                }
                ID = 1;

                grpList.Text = "产生序号库位 List:";
            }

            //DataTable PARTList = new DataTable();
            //int ID = 1;
            //PARTList.Columns.Add("ID", System.Type.GetType("System.String"));
            //PARTList.Columns.Add("LOCATIONNO", System.Type.GetType("System.String"));
            //PARTList.Columns.Add("PARTNO", System.Type.GetType("System.String"));
            //PARTList.Columns.Add("CARTONCOUNT", System.Type.GetType("System.String"));
            //PARTList.Columns.Add("CSNTYPE", System.Type.GetType("System.String"));
            //PARTList.Columns.Add("CARTONTYPE", System.Type.GetType("System.String"));

            DataRow dr = PARTList.NewRow();
            dr[0] = ID.ToString();
            dr[1] = cmbLocation.Text.ToString();
            dr[2] = strpartno.ToString();
            dr[3] = nudCartonCount.Value.ToString();
            dr[4] = getCSNType().ToString();
            dr[5] = getCartonType().ToString();

            int isexist = 0;
            for (int i = 0; i < PARTList.Rows.Count; i++)
            {
                if (PARTList.Rows[i]["LOCATIONNO"].Equals(dr[1]))
                {
                    PARTList.Rows[i]["PARTNO"] = dr[2];
                    PARTList.Rows[i]["CARTONCOUNT"] = dr[3];
                    PARTList.Rows[i]["CSNTYPE"] = dr[4];
                    PARTList.Rows[i]["CARTONTYPE"] = dr[5];
                    isexist = 1;
                }
            }
            if (isexist == 0)
            {
                PARTList.Rows.Add(dr);
                ID++;
            }
            dgvPART.DataSource = PARTList;
            this.dgvPART.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPART.Refresh();


        }

        private void btnCreateCarton2_Click(object sender, EventArgs e)
        {
            btnCreateCarton2.Enabled = false;
            if (dgvPART.Rows.Count == 0)
            {
                MessageBox.Show("请先添加产生序号清单");
                btnCreateCarton2.Enabled = true;
                return;
            }
            //造G_SN_STATUS 资料
            for (int i = 0; i < dgvPART.Rows.Count - 1; i++)
            {
                string strLOCATIONNO = dgvPART.Rows[i].Cells["LOCATIONNO"].Value.ToString();
                string strPARTNO = dgvPART.Rows[i].Cells["PARTNO"].Value.ToString();
                string strCARTONCOUNT = dgvPART.Rows[i].Cells["CARTONCOUNT"].Value.ToString();
                string strCSNTYPE = dgvPART.Rows[i].Cells["CSNTYPE"].Value.ToString();
                string strCARTONTYPE = dgvPART.Rows[i].Cells["CARTONTYPE"].Value.ToString();

                string strResult = CreateCSN(strLOCATIONNO, strPARTNO, strCARTONCOUNT, strCSNTYPE, strCARTONTYPE);

                if (strResult.Equals("NG"))
                {
                    this.dgvPART.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            //转储位 入库 和转G_SN_STATUS-->T_SN_STATUS
            object[][] procParams = new object[0][];
            DataTable dt2 = new DataTable();
            try
            {
                dt2 = ClientUtils.ExecuteProc("ppsuser.T_TRAN_WMS_DATA", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                grpList.Text = "产生序号库位 List0:";
                btnCreateCarton2.Enabled = true;
                return;
            }
            initComboBox();
            grpList.Text = "产生序号库位 List0:";
            btnCreateCarton2.Enabled = true;
        }

        private string getCSNType()
        {
            if (rdoCSN.Checked)
            {
                return "CSN12";
            }
            else
            {
                return "CSN17";
            }
        }

        private string getCartonType()
        {
            if (rdoNCarton.Checked)
            {
                return "NCARTON";
            }
            else if (rdoSCCarton.Checked)
            {
                return "SCCARTON";
            }
            else
            {
                return "SCCARTONA";
            }
        }


        //PARTList.Columns.Add("LOCATIONNO", System.Type.GetType("System.String"));
        //PARTList.Columns.Add("PARTNO", System.Type.GetType("System.String"));
        //PARTList.Columns.Add("CARTONCOUNT", System.Type.GetType("System.String"));
        //PARTList.Columns.Add("CSNTYPE", System.Type.GetType("System.String"));
        //PARTList.Columns.Add("CARTONTYPE", System.Type.GetType("System.String"));
        public string CreateCSN2(string partno, string cartoncount, string csntype, string cartontype, string strdn, string strdnline, string strcarlineno, String strLocationNo, String StrLocationID, String StrCurrentQty, string strCoo)
        {
            string sqlstr;
            if (string.IsNullOrEmpty(partno))
            {
                MessageBox.Show("料号为空");
                return "NG";
            }

            string ictpartnompn = cc.getMPNInfo(partno);
            if (ictpartnompn.Equals("NG"))
            {
                MessageBox.Show(partno + "不在OMS系统");
                return "NG";
            }
            if (string.IsNullOrEmpty(strCoo))
            {
                MessageBox.Show("Coo为空");
                return "NG";
            }
            string[] partmpn = ictpartnompn.Split('*');
            string strpartno = partno;
            string strmpn = partmpn[1];

            string strCustModel = string.Empty;

            string strpackunit = partmpn[2];
            string strPALLETQTY = partmpn[3];
            txtCartons.Clear();

            if (!cc.CheckPartNo(strpartno, out partID))
            {
                MessageBox.Show("料号 错误");
                return "NG";
            }
            int cartonscount = Convert.ToInt32(cartoncount);
            //ppp code 取值逻辑 VN 版 根据oms part mapping维护进行判断
            string PPP = cc.GetCoo(strCoo, 7);
            if (string.IsNullOrEmpty(PPP))
            {
                MessageBox.Show("获取PPP Code失败,料号未维护或不存在");
            }
            string YW = gcf.GetYW(DateTime.Now.ToString("yyyy-MM-dd"));
            if (string.IsNullOrEmpty(YW))
            {
                MessageBox.Show("YW获取异常");
                return "NG";
            }
            //取年最后一码
            string BU = DateTime.Now.Year.ToString().Substring(3, 1);
            //取月份2码
            string CC = DateTime.Now.Month.ToString();
            CC = CC.PadLeft(2, '0');

            //取日的31进制
            string B1 = gcf.GetNumtoWvV(DateTime.Now.Day.ToString(), "A", "-");

            string EEEE = (strpartno).ToUpper().Trim().Substring(0, 4);
            //csn17 vserion
            string E1 = "A";
            string SSS;
            string SSSS;
            string aa = "";
            string CCCC = (strpartno).ToUpper().Trim().Substring(0, 3);

            string CSN = string.Empty;

            int qty = int.Parse(strpackunit);
            int outqty = 0;
            string strResultGetSN = string.Empty;
            if (csntype.Equals("CSN12"))
            {
                //"CSN12"
                strResultGetSN = cc.GetSNRangeByProcedure(csntype, PPP + YW, cartonscount * qty, out outqty);
            }
            else
            {
                //"CSN17"
                strResultGetSN = cc.GetSNRangeByProcedure(csntype, BU + CC + B1, cartonscount * qty, out outqty);
            }
            int startnum = outqty + 1;

            //000 88590 99870 25102
            //0885909 987025102~987030102
            int outqtyCarton = 0;
            string strResultGetSNCarton = string.Empty;
            if (cartontype.Equals("SCCARTON"))
            {
                //"SCCARTON"
                strResultGetSNCarton = cc.GetSNRangeByProcedure(cartontype, "888462", cartonscount, out outqtyCarton);
            }
            if (cartontype.Equals("SCCARTONA"))
            {
                //"SCCARTONA"
                strResultGetSNCarton = cc.GetSNRangeByProcedure(cartontype, "888462", cartonscount, out outqtyCarton);
            }
            int startCartonnum = outqtyCarton;


            string strGS1snlist = string.Empty;
            strGS1snlist = "SSCC|CSN1|CSN2|CSN3|CSN4|CSN5|CSN6|CSN7|CSN8|CSN9|CSN10|COO|" + "\r\n";

            string strSN2D = string.Empty;
            string strGTIN = string.Empty;
            string strUPC = string.Empty;
            string strJAN = string.Empty;
            //txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
            strGTIN = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
            strGTIN = strGTIN.PadLeft(14, '0');

            string strGS1cartonlist = string.Empty;
            strGS1cartonlist = "SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN|COO1|COO2|COO3|" + "\r\n";



            for (int i = 1; i <= cartonscount; i++)
            {

                string cartonno = string.Empty;
                string cartonwo = string.Empty;
                if (cartontype.Equals("SCCARTONA") || cartontype.Equals("SCCARTON"))
                {
                    cartonno = "00888462" + (startCartonnum + i).ToString().PadLeft(9, '0');
                    cartonno = cartonno + cc.CheckSSCCSum(cartonno);
                }
                //else
                //{
                cartonwo = strpartno.Substring(0, 3) + DateTime.Now.ToString("yyMMddHHmmssfff") + i.ToString().PadLeft(4, '0');

                //  }

                //string sqlstr = "SELECT WORK_ORDER,SERIAL_NUMBER FROM PPSUSER.G_SN_STATUS WHERE CARTON_NO=:CARTON_NO AND ROWNUM=1";
                //object[][] sqlparamstemp = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", cartonno } };
                //DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparamstemp).Tables[0];
                //if (dt.Rows.Count > 0)
                //{
                //    MessageBox.Show("箱号已存在，请变更固定码重新生成");
                //    return "NG";
                //}
                string snlist = string.Empty;
                string ssnlist = string.Empty;

                for (int j = 1; j <= qty; j++)
                {
                    //每次取一个序号

                    if (csntype.Equals("CSN12"))
                    {
                        //"CSN12"
                        aa = "0000" + gcf.GetNumtoWvV((startnum).ToString(), "A", "-");
                        SSS = aa.Substring(aa.Length - 4, 4);
                        CSN = PPP + YW + SSS + CCCC;
                    }
                    else
                    {
                        //"CSN17"
                        aa = "000" + gcf.GetNumtoWvV((startnum).ToString(), "A", "-");
                        SSSS = aa.Substring(aa.Length - 4, 4);
                        CSN = PPP + BU + CC + B1 + SSSS + EEEE + E1;
                        CSN = CSN + gcf.CheckSum(CSN);
                    }
                    //private bool SN_InsertWo(string sn, string csn, string wo, string partid, string cartonno, string locationid, string PALLET_NO)
                    if (qty == 1)
                    {
                        cartonno = CSN;
                    }
                    //cartonno =  CSN;
                    string strresult = cc.SN_InsertWo2(cartonno + j.ToString().PadLeft(2, '0'), CSN, cartonwo.Substring(9), partID, cartonno, StrLocationID, strLocationNo, "SYS", strdn, strdnline, strcarlineno, (int.Parse(StrCurrentQty) + i).ToString(), strCoo);
                    if (!strresult.Contains("OK"))
                    {
                        MessageBox.Show(strresult);
                        return "NG";
                    }

                    if (qty == 1)
                    {
                        string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";
                        string strPrintCSNinfo = "DNNAME, DNLINEITEM,SHIPMENTID,PARTNO,CARLINENO,CSN" + "\r\n";
                        strPrintCSNinfo += DNNAME + "," + DNLINEITEM + "," + SHIPMENTID + "," + PARTNO + "," + CARLINENO + "," + cartonno;

                        using (Process p = new Process())
                        {
                            string strSampleFile = string.Empty;
                            strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "PPART_SN" + ".btw";

                            if (!File.Exists(strSampleFile))
                            {
                                MessageBox.Show("Sample File Not exists-" + strSampleFile);
                                return "NG";
                            }
                            string str9 = Path.GetFullPath(strStartupPath) + @"\" + "PPART_SN" + CSN + ".lst";
                            cc.WriteToPrintGo(str9, strPrintCSNinfo);
                            p.StartInfo.FileName = "bartend.exe";
                            string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                            sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str9 + '"').Replace("@QTY", "1");
                            p.StartInfo.Arguments = sArguments;
                            p.Start();
                            p.WaitForExit();
                        }

                    }

                    snlist += CSN + "|";
                    ssnlist += "S" + CSN + ",";
                    startnum++;
                };
                sqlstr = @"update ppsuser.t_trolley_line_info SET USEDQTY=USEDQTY+1 ,GROUP_CODE='" + GroupCode + "' WHERE TROLLEY_LINE_NO = '" + strcarlineno + "'";

                DataSet dts = ClientUtils.ExecuteSQL(sqlstr);


                strGS1snlist += cartonno + "|" + snlist + cc.GetCoo(strCoo, 4) + "|" + "\r\n";
                string strQTY = qty.ToString().PadLeft(2, '0');
                //SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN
                strSN2D = "V3," + "SSCC" + cartonno + ",GTIN" + strGTIN + ",MPN" + strmpn + ",QTY" + strQTY + "," + ssnlist;
                strGS1cartonlist += cartonno + "|" + strmpn + "|" + strSN2D + "|" + strGTIN + "|" + qty + "|" + strGTIN + strQTY + "|" + strUPC + "|" + strJAN + "|" + cc.GetCoo(strCoo, 4) + "|" + cc.GetCoo(strCoo, 6) + "|" + cc.GetCoo(strCoo, 5) + "\r\n";
                txtCartons.AppendText(cartonno + Environment.NewLine);
            }
            cc.SN_Trolley(strcarlineno);
            if (cartontype.Equals("SCCARTONA") || cartontype.Equals("SCCARTON"))
            {
                //如果是Multi 打印
                //产生2个lst文件，然后打印
                string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";

                string strBTWname = string.Empty;
                if (strCustModel.Equals("B188"))
                {
                    strBTWname = "B188_GS1_MAIN";
                }
                else if (strCustModel.Equals("B288") || strCustModel.Equals("B298"))
                {
                    strBTWname = "B288_GS1_MAIN";
                }
                else if (strCustModel.Equals("A145"))
                {
                    if (strmpn.Contains("MLL82AM"))
                    { strBTWname = "A145_GS1_MAIN_A"; }
                    else if (strmpn.Contains("MLL82FE"))
                    { strBTWname = "A145_GS1_MAIN_F"; }
                    else
                    { strBTWname = "A145_GS1_MAIN"; }

                }
                else if (strCustModel.Equals("B501"))
                {
                    if (strmpn.Contains("MUFM2J"))
                    { strBTWname = "B501_GS1_MAIN_J"; }
                    else
                    { strBTWname = "B501_GS1_MAIN"; }
                }
                //dgvPART 有几行就要打印几次， dgvPart 每行就Locaion_no不一样
                string str7 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_MAIN" + strLocationNo + ".lst";
                string str8 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN" + strLocationNo + ".lst";
                cc.WriteToPrintGo(str7, strGS1cartonlist);
                cc.WriteToPrintGo(str8, strGS1snlist);
                if (chkGS1Label.Checked)
                {
                    using (Process p = new Process())
                    {
                        string strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + strBTWname + ".btw";
                        if (!File.Exists(strSampleFile))
                        {
                            MessageBox.Show("Sample File Not exists-" + strSampleFile);
                            return "NG";
                        }
                        p.StartInfo.FileName = "bartend.exe";
                        string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", "1");
                        p.StartInfo.Arguments = sArguments;
                        p.Start();
                        p.WaitForExit();
                    }
                }
                if ((strCustModel.Equals("B501") || strCustModel.Equals("B188") || strCustModel.Equals("B288") || strCustModel.Equals("B298")) && (chkCSNListLabel.Checked))
                {
                    using (Process p = new Process())
                    {
                        string strSampleFile = string.Empty;
                        if (qty == 10)
                        {
                            strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN10" + ".btw";
                        }
                        if (qty == 4)
                        {
                            strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN4" + ".btw";
                        }
                        if (qty == 1)
                        {
                            return "";
                        }

                        if (!File.Exists(strSampleFile))
                        {
                            MessageBox.Show("Sample File Not exists-" + strSampleFile);
                            return "NG";
                        }
                        p.StartInfo.FileName = "bartend.exe";
                        string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str8 + '"').Replace("@QTY", "1");
                        p.StartInfo.Arguments = sArguments;
                        p.Start();
                        p.WaitForExit();
                    }
                }
            }
            else
            {
                //如果是Single 打印

            }

            return "OK";

        }
        public string CreateCSN3(string partno, string cartoncount, string csntype, string cartontype, string strdn, string strdnline, string strcarlineno, String strLocationNo, String StrLocationID, String StrCurrentQty, string strCoo)
        {
            string sqlstr;
            if (string.IsNullOrEmpty(partno))
            {
                MessageBox.Show("料号为空");
                return "NG";
            }

            string ictpartnompn = cc.getMPNInfo(partno);
            if (ictpartnompn.Equals("NG"))
            {
                MessageBox.Show(partno + "不在OMS系统");
                return "NG";
            }
            if (string.IsNullOrEmpty(strCoo))
            {
                MessageBox.Show("Coo为空");
                return "NG";
            }
            string[] partmpn = ictpartnompn.Split('*');
            string strpartno = partno;
            string strmpn = partmpn[1];

            string strCustModel = string.Empty;

            string strpackunit = partmpn[2];
            string strPALLETQTY = partmpn[3];
            txtCartons.Clear();

            if (!cc.CheckPartNo(strpartno, out partID))
            {
                MessageBox.Show("料号 错误");
                return "NG";
            }
            int cartonscount = Convert.ToInt32(cartoncount);
            //ppp code 取值逻辑 VN 版 根据oms part mapping维护进行判断
            string PPP = cc.GetCoo(strCoo, 7);
            if (string.IsNullOrEmpty(PPP))
            {
                MessageBox.Show("获取PPP Code失败,料号未维护或不存在");
            }
            string YW = gcf.GetYW(DateTime.Now.ToString("yyyy-MM-dd"));
            if (string.IsNullOrEmpty(YW))
            {
                MessageBox.Show("YW获取异常");
                return "NG";
            }
            //取年最后一码
            string BU = DateTime.Now.Year.ToString().Substring(3, 1);
            //取月份2码
            string CC = DateTime.Now.Month.ToString();
            CC = CC.PadLeft(2, '0');

            //取日的31进制
            string B1 = gcf.GetNumtoWvV(DateTime.Now.Day.ToString(), "A", "-");

            string EEEE = (strpartno).ToUpper().Trim().Substring(0, 4);
            //csn17 vserion
            string E1 = "A";
            string SSS;
            string SSSS;
            string aa = "";
            string CCCC = (strpartno).ToUpper().Trim().Substring(0, 3);

            string CSN = string.Empty;

            int qty = int.Parse(strpackunit);
            int outqty = 0;
            string strResultGetSN = string.Empty;
            if (csntype.Equals("CSN12"))
            {
                //"CSN12"
                strResultGetSN = cc.GetSNRangeByProcedure(csntype, PPP + YW, cartonscount * qty, out outqty);
            }
            else
            {
                //"CSN17"
                strResultGetSN = cc.GetSNRangeByProcedure(csntype, BU + CC + B1, cartonscount * qty, out outqty);
            }
            int startnum = outqty + 1;

            //000 88590 99870 25102
            //0885909 987025102~987030102
            int outqtyCarton = 0;
            string strResultGetSNCarton = string.Empty;
            if (cartontype.Equals("SCCARTON"))
            {
                //"SCCARTON"
                strResultGetSNCarton = cc.GetSNRangeByProcedure(cartontype, "888462", cartonscount, out outqtyCarton);
            }
            if (cartontype.Equals("SCCARTONA"))
            {
                //"SCCARTONA"
                strResultGetSNCarton = cc.GetSNRangeByProcedure(cartontype, "888462", cartonscount, out outqtyCarton);
            }
            int startCartonnum = outqtyCarton;


            string strGS1snlist = string.Empty;
            strGS1snlist = "SSCC|CSN1|CSN2|CSN3|CSN4|CSN5|CSN6|CSN7|CSN8|CSN9|CSN10|COO|" + "\r\n";

            string strSN2D = string.Empty;
            string strGTIN = string.Empty;
            string strUPC = string.Empty;
            string strJAN = string.Empty;
            //txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
            strGTIN = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
            strGTIN = strGTIN.PadLeft(14, '0');

            string strGS1cartonlist = string.Empty;
            strGS1cartonlist = "SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN|COO1|COO2|COO3|" + "\r\n";



            for (int i = 1; i <= cartonscount; i++)
            {

                string cartonno = string.Empty;
                string cartonwo = string.Empty;
                if (cartontype.Equals("SCCARTONA") || cartontype.Equals("SCCARTON"))
                {
                    cartonno = "00888462" + (startCartonnum + i).ToString().PadLeft(9, '0');
                    cartonno = cartonno + cc.CheckSSCCSum(cartonno);
                }
                //else
                //{
                cartonwo = strpartno.Substring(0, 3) + DateTime.Now.ToString("yyMMddHHmmssfff") + i.ToString().PadLeft(4, '0');

                //  }

                //string sqlstr = "SELECT WORK_ORDER,SERIAL_NUMBER FROM PPSUSER.G_SN_STATUS WHERE CARTON_NO=:CARTON_NO AND ROWNUM=1";
                //object[][] sqlparamstemp = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", cartonno } };
                //DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparamstemp).Tables[0];
                //if (dt.Rows.Count > 0)
                //{
                //    MessageBox.Show("箱号已存在，请变更固定码重新生成");
                //    return "NG";
                //}
                string snlist = string.Empty;
                string ssnlist = string.Empty;

                for (int j = 1; j <= qty; j++)
                {
                    //每次取一个序号

                    if (csntype.Equals("CSN12"))
                    {
                        //"CSN12"
                        aa = "0000" + gcf.GetNumtoWvV((startnum).ToString(), "A", "-");
                        SSS = aa.Substring(aa.Length - 4, 4);
                        CSN = PPP + YW + SSS + CCCC;
                    }
                    else
                    {
                        //"CSN17"
                        aa = "000" + gcf.GetNumtoWvV((startnum).ToString(), "A", "-");
                        SSSS = aa.Substring(aa.Length - 4, 4);
                        CSN = PPP + BU + CC + B1 + SSSS + EEEE + E1;
                        CSN = CSN + gcf.CheckSum(CSN);
                    }
                    //private bool SN_InsertWo(string sn, string csn, string wo, string partid, string cartonno, string locationid, string PALLET_NO)
                    if (qty == 1)
                    {
                        cartonno = CSN;
                    }
                    //cartonno =  CSN;
                    string strresult = cc.SN_InsertWo2(cartonno + j.ToString().PadLeft(2, '0'), CSN, cartonwo.Substring(9), partID, cartonno, StrLocationID, strLocationNo, "SYS", strdn, strdnline, strcarlineno, (int.Parse(StrCurrentQty) + i).ToString(), strCoo);
                    if (!strresult.Contains("OK"))
                    {
                        MessageBox.Show(strresult);
                        return "NG";
                    }

                    if (qty == 1)
                    {
                        string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";
                        string strPrintCSNinfo = "DNNAME, DNLINEITEM,SHIPMENTID,PARTNO,CARLINENO,CSN" + "\r\n";
                        strPrintCSNinfo += DNNAME + "," + DNLINEITEM + "," + SHIPMENTID + "," + PARTNO + "," + CARLINENO + "," + cartonno;

                        //using (Process p = new Process())
                        //{
                        //    string strSampleFile = string.Empty;
                        //    strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "PPART_SN" + ".btw";

                        //    if (!File.Exists(strSampleFile))
                        //    {
                        //        MessageBox.Show("Sample File Not exists-" + strSampleFile);
                        //        return "NG";
                        //    }
                        //    string str9 = Path.GetFullPath(strStartupPath) + @"\" + "PPART_SN" + CSN + ".lst";
                        //    cc.WriteToPrintGo(str9, strPrintCSNinfo);
                        //    p.StartInfo.FileName = "bartend.exe";
                        //    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        //    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str9 + '"').Replace("@QTY", "1");
                        //    p.StartInfo.Arguments = sArguments;
                        //    p.Start();
                        //    p.WaitForExit();
                        //}

                    }

                    snlist += CSN + "|";
                    ssnlist += "S" + CSN + ",";
                    startnum++;
                };
                sqlstr = @"update ppsuser.t_trolley_line_info SET USEDQTY=USEDQTY+1 ,GROUP_CODE='" + GroupCode + "' WHERE TROLLEY_LINE_NO = '" + strcarlineno + "'";

                DataSet dts = ClientUtils.ExecuteSQL(sqlstr);


                strGS1snlist += cartonno + "|" + snlist + cc.GetCoo(strCoo, 4) + "|" + "\r\n";
                string strQTY = qty.ToString().PadLeft(2, '0');
                //SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN
                strSN2D = "V3," + "SSCC" + cartonno + ",GTIN" + strGTIN + ",MPN" + strmpn + ",QTY" + strQTY + "," + ssnlist;
                strGS1cartonlist += cartonno + "|" + strmpn + "|" + strSN2D + "|" + strGTIN + "|" + qty + "|" + strGTIN + strQTY + "|" + strUPC + "|" + strJAN + "|" + cc.GetCoo(strCoo, 4) + "|" + cc.GetCoo(strCoo, 6) + "|" + cc.GetCoo(strCoo, 5) + "\r\n";
                txtCartons.AppendText(cartonno + Environment.NewLine);
            }
            cc.SN_Trolley(strcarlineno);
            if (cartontype.Equals("SCCARTONA") || cartontype.Equals("SCCARTON"))
            {
                //如果是Multi 打印
                //产生2个lst文件，然后打印
                string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";

                string strBTWname = string.Empty;
                if (strCustModel.Equals("B188"))
                {
                    strBTWname = "B188_GS1_MAIN";
                }
                else if (strCustModel.Equals("B288") || strCustModel.Equals("B298"))
                {
                    strBTWname = "B288_GS1_MAIN";
                }
                else if (strCustModel.Equals("A145"))
                {
                    if (strmpn.Contains("MLL82AM"))
                    { strBTWname = "A145_GS1_MAIN_A"; }
                    else if (strmpn.Contains("MLL82FE"))
                    { strBTWname = "A145_GS1_MAIN_F"; }
                    else
                    { strBTWname = "A145_GS1_MAIN"; }

                }
                else if (strCustModel.Equals("B501"))
                {
                    if (strmpn.Contains("MUFM2J"))
                    { strBTWname = "B501_GS1_MAIN_J"; }
                    else
                    { strBTWname = "B501_GS1_MAIN"; }
                }
                //dgvPART 有几行就要打印几次， dgvPart 每行就Locaion_no不一样
                string str7 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_MAIN" + strLocationNo + ".lst";
                string str8 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN" + strLocationNo + ".lst";
                cc.WriteToPrintGo(str7, strGS1cartonlist);
                cc.WriteToPrintGo(str8, strGS1snlist);
                if (chkGS1Label.Checked)
                {
                    //using (Process p = new Process())
                    //{
                    //    string strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + strBTWname + ".btw";
                    //    if (!File.Exists(strSampleFile))
                    //    {
                    //        MessageBox.Show("Sample File Not exists-" + strSampleFile);
                    //        return "NG";
                    //    }
                    //    p.StartInfo.FileName = "bartend.exe";
                    //    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    //    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", "1");
                    //    p.StartInfo.Arguments = sArguments;
                    //    p.Start();
                    //    p.WaitForExit();
                    //}
                }
                if ((strCustModel.Equals("B501") || strCustModel.Equals("B188") || strCustModel.Equals("B288") || strCustModel.Equals("B298")) && (chkCSNListLabel.Checked))
                {
                    //using (Process p = new Process())
                    //{
                    //    string strSampleFile = string.Empty;
                    //    if (qty == 10)
                    //    {
                    //        strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN10" + ".btw";
                    //    }
                    //    if (qty == 4)
                    //    {
                    //        strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN4" + ".btw";
                    //    }
                    //    if (qty == 1)
                    //    {
                    //        return "";
                    //    }

                    //    if (!File.Exists(strSampleFile))
                    //    {
                    //        MessageBox.Show("Sample File Not exists-" + strSampleFile);
                    //        return "NG";
                    //    }
                    //    p.StartInfo.FileName = "bartend.exe";
                    //    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    //    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str8 + '"').Replace("@QTY", "1");
                    //    p.StartInfo.Arguments = sArguments;
                    //    p.Start();
                    //    p.WaitForExit();
                    //}
                }
            }
            else
            {
                //如果是Single 打印

            }

            return "OK";

        }

        public string CreateCSN(string locationno, string partno, string cartoncount, string csntype, string cartontype)
        {
            if (string.IsNullOrEmpty(partno))
            {
                MessageBox.Show("料号为空");
                return "NG";
            }
            string ictpartnompn = cc.getMPNInfo(partno);
            if (ictpartnompn.Equals("NG"))
            {
                MessageBox.Show(partno + "不在OMS系统");
                return "NG";
            }

            string[] partmpn = ictpartnompn.Split('*');
            string strpartno = partno;
            string strmpn = partmpn[1];

            string strCustModel = string.Empty;

            string strpackunit = partmpn[2];
            string strPALLETQTY = partmpn[3];
            var palletNo = DateTime.Now.ToString("yyyyMMddHHmmssms");
            txtCartons.Clear();

            if (!cc.CheckPartNo(strpartno, out partID))
            {
                MessageBox.Show("料号 错误");
                return "NG";
            }
            if (!cc.CheckLocationId(locationno, out locationID))
            {
                MessageBox.Show("储位 错误");
                return "NG";
            }
            if (cc.checkLocation(locationno).Equals("NG"))
            {
                MessageBox.Show(locationno + "储位已经使用");
                return "NG";
            }

            int cartonscount = Convert.ToInt32(cartoncount);

            string PPP = "ASD";
            string YW = gcf.GetYW(DateTime.Now.ToString("yyyy-MM-dd"));
            if (string.IsNullOrEmpty(YW))
            {
                MessageBox.Show("YW获取异常");
                return "NG";
            }
            //取年最后一码
            string BU = DateTime.Now.Year.ToString().Substring(3, 1);
            //取月份2码
            string CC = DateTime.Now.Month.ToString();
            CC = CC.PadLeft(2, '0');

            //取日的31进制
            string B1 = gcf.GetNumtoWvV(DateTime.Now.Day.ToString(), "A", "-");

            string EEEE = (strpartno).ToUpper().Trim().Substring(0, 4);
            //csn17 vserion
            string E1 = "A";
            string SSS;
            string SSSS;
            string aa = "";
            string CCCC = (strpartno).ToUpper().Trim().Substring(0, 4);

            string CSN = string.Empty;

            int qty = int.Parse(strpackunit);
            int outqty = 0;
            string strResultGetSN = string.Empty;
            if (csntype.Equals("CSN12"))
            {
                //"CSN12"
                strResultGetSN = cc.GetSNRangeByProcedure(csntype, PPP + YW, cartonscount * qty, out outqty);
            }
            else
            {
                //"CSN17"
                strResultGetSN = cc.GetSNRangeByProcedure(csntype, BU + CC + B1, cartonscount * qty, out outqty);
            }
            int startnum = outqty + 1;

            //000 88590 99870 25102
            //0885909 987025102~987030102
            int outqtyCarton = 0;
            string strResultGetSNCarton = string.Empty;
            if (cartontype.Equals("SCCARTON"))
            {
                //"SCCARTON"
                strResultGetSNCarton = cc.GetSNRangeByProcedure(cartontype, "N00885909", cartonscount, out outqtyCarton);
            }
            if (cartontype.Equals("SCCARTONA"))
            {
                //"SCCARTONA"
                strResultGetSNCarton = cc.GetSNRangeByProcedure(cartontype, "A00885909", cartonscount, out outqtyCarton);
            }
            int startCartonnum = outqtyCarton;


            string strGS1snlist = string.Empty;
            strGS1snlist = "SSCC|CSN1|CSN2|CSN3|CSN4|CSN5|CSN6|CSN7|CSN8|CSN9|CSN10|COO|" + "\r\n";

            string strSN2D = string.Empty;
            string strGTIN = string.Empty;
            string strUPC = string.Empty;
            string strJAN = string.Empty;
            //txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
            strGTIN = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
            strGTIN = strGTIN.PadLeft(14, '0');

            string strGS1cartonlist = string.Empty;
            strGS1cartonlist = "SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN|COO1|COO2|COO3|" + "\r\n";

            for (int i = 1; i <= cartonscount; i++)
            {

                string cartonno = string.Empty;
                if (cartontype.Equals("SCCARTONA") || cartontype.Equals("SCCARTON"))
                {
                    cartonno = "00885909" + (startCartonnum + i).ToString().PadLeft(9, '0');
                    cartonno = cartonno + cc.CheckSSCCSum(cartonno);
                }
                else
                {
                    //cartonno = (strpartno.StartsWith("L1S") ? strpartno.Substring(strpartno.Length - 3, 3) : strpartno.Substring(0, 3)) + txtCartonFix.Text.Trim() + DateTime.Now.ToString("MMddHH") + i.ToString().PadLeft(4, '0');
                    cartonno = strpartno.Substring(0, 3) + DateTime.Now.ToString("yyMMddHHmmssfff") + i.ToString().PadLeft(4, '0');

                }

                string sqlstr = "SELECT WORK_ORDER,SERIAL_NUMBER FROM PPSUSER.G_SN_STATUS WHERE CARTON_NO=:CARTON_NO AND ROWNUM=1";
                object[][] sqlparamstemp = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", cartonno } };
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparamstemp).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("箱号已存在，请变更固定码重新生成");
                    return "NG";
                }
                string snlist = string.Empty;
                string ssnlist = string.Empty;

                for (int j = 1; j <= qty; j++)
                {
                    //每次取一个序号

                    if (csntype.Equals("CSN12"))
                    {
                        //"CSN12"
                        aa = "000" + gcf.GetNumtoWvV((startnum).ToString(), "A", "-");
                        SSS = aa.Substring(aa.Length - 3, 3);
                        CSN = PPP + YW + SSS + CCCC;
                    }
                    else
                    {
                        //"CSN17"
                        aa = "000" + gcf.GetNumtoWvV((startnum).ToString(), "A", "-");
                        SSSS = aa.Substring(aa.Length - 4, 4);
                        CSN = PPP + BU + CC + B1 + SSSS + EEEE + E1;
                        CSN = CSN + gcf.CheckSum(CSN);
                    }
                    //private bool SN_InsertWo(string sn, string csn, string wo, string partid, string cartonno, string locationid, string PALLET_NO)


                    if (!cc.SN_InsertWo(cartonno.Substring(3) + j.ToString().PadLeft(3, '0'), CSN, cartonno.Substring(9), partID, cartonno, locationID, palletNo))
                    {
                        return "NG";
                    }
                    snlist += CSN + "|";
                    ssnlist += "S" + CSN + ",";
                    startnum++;
                }
                strGS1snlist += cartonno + "|" + snlist + cc.GetCoo(strpartno, 2) + "|" + "\r\n";
                string strQTY = qty.ToString().PadLeft(2, '0');
                //SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN
                strSN2D = "V3," + "SSCC" + cartonno + ",GTIN" + strGTIN + ",MPN" + strmpn + ",QTY" + strQTY + "," + ssnlist;
                strGS1cartonlist += cartonno + "|" + strmpn + "|" + strSN2D + "|" + strGTIN + "|" + qty + "|" + strGTIN + strQTY + "|" + strUPC + "|" + strJAN + "|" + cc.GetCoo(strpartno, 2) + "|" + cc.GetCoo(strpartno, 4) + "|" + cc.GetCoo(strpartno, 3) + "\r\n";
                txtCartons.AppendText(cartonno + Environment.NewLine);
            }



            if (cartontype.Equals("SCCARTONA") || cartontype.Equals("SCCARTON"))
            {
                //产生2个lst文件，然后打印
                string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";

                string strBTWname = string.Empty;
                if (strCustModel.Equals("B188"))
                {
                    strBTWname = "B188_GS1_MAIN";
                }
                else if (strCustModel.Equals("B288") || strCustModel.Equals("B298"))
                {
                    strBTWname = "B288_GS1_MAIN";
                }
                else if (strCustModel.Equals("A145"))
                {
                    if (strmpn.Contains("MLL82AM"))
                    { strBTWname = "A145_GS1_MAIN_A"; }
                    else if (strmpn.Contains("MLL82FE"))
                    { strBTWname = "A145_GS1_MAIN_F"; }
                    else
                    { strBTWname = "A145_GS1_MAIN"; }

                }
                else if (strCustModel.Equals("B501"))
                {
                    if (strmpn.Contains("MUFM2J"))
                    { strBTWname = "B501_GS1_MAIN_J"; }
                    else
                    { strBTWname = "B501_GS1_MAIN"; }
                }
                //dgvPART 有几行就要打印几次， dgvPart 每行就Locaion_no不一样
                string str7 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_MAIN" + locationno + ".lst";
                string str8 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN" + locationno + ".lst";
                cc.WriteToPrintGo(str7, strGS1cartonlist);
                cc.WriteToPrintGo(str8, strGS1snlist);
                if (chkGS1Label.Checked)
                {
                    using (Process p = new Process())
                    {
                        string strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + strBTWname + ".btw";
                        if (!File.Exists(strSampleFile))
                        {
                            MessageBox.Show("Sample File Not exists-" + strSampleFile);
                            return "NG";
                        }
                        p.StartInfo.FileName = "bartend.exe";
                        string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", "1");
                        p.StartInfo.Arguments = sArguments;
                        p.Start();
                        p.WaitForExit();
                    }
                }
                if ((strCustModel.Equals("B501") || strCustModel.Equals("B188") || strCustModel.Equals("B288") || strCustModel.Equals("B298")) && (chkCSNListLabel.Checked))
                {
                    using (Process p = new Process())
                    {
                        string strSampleFile = string.Empty;
                        if (qty == 10)
                        {
                            strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN10" + ".btw";
                        }
                        if (qty == 4)
                        {
                            strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN4" + ".btw";
                        }


                        if (!File.Exists(strSampleFile))
                        {
                            MessageBox.Show("Sample File Not exists-" + strSampleFile);
                            return "NG";
                        }
                        p.StartInfo.FileName = "bartend.exe";
                        string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str8 + '"').Replace("@QTY", "1");
                        p.StartInfo.Arguments = sArguments;
                        p.Start();
                        p.WaitForExit();
                    }
                }
            }
            return "OK";

        }


        public string CreateCSN_backup(string locationno, string partno, string cartoncount, string csntype, string cartontype)
        {
            if (string.IsNullOrEmpty(partno))
            {
                MessageBox.Show("料号为空");
                return "NG";
            }
            string ictpartnompn = cc.getMPNInfo(partno);
            if (ictpartnompn.Equals("NG"))
            {
                MessageBox.Show(partno + "不在OMS系统");
                return "NG";
            }

            string[] partmpn = ictpartnompn.Split('*');
            string strpartno = partno;
            string strmpn = partmpn[1];
            string strpackunit = partmpn[2];
            string strPALLETQTY = partmpn[3];
            var palletNo = DateTime.Now.ToString("yyyyMMddHHmmssms");
            txtCartons.Clear();

            if (!cc.CheckPartNo(strpartno, out partID))
            {
                MessageBox.Show("料号 错误");
                return "NG";
            }
            if (!cc.CheckLocationId(locationno, out locationID))
            {
                MessageBox.Show("储位 错误");
                return "NG";
            }
            if (cc.checkLocation(locationno).Equals("NG"))
            {
                MessageBox.Show(locationno + "储位已经使用");
                return "NG";
            }

            //create or replace procedure ppsuser.SP_TEST_GETSNRANGE(strsntype in varchar2,
            //                                           strprefix in varchar2,
            //                                           qty       in number,
            //                                           outqty    out number,
            //                                           errmsg    out varchar2) as
            //  --qty是外面程序的需求数
            //  --outqty是当前用到数量值，外面使用需加1.


            int cartonscount = Convert.ToInt32(cartoncount);

            //C01C1ABCVWXY  PPPYWSSSCCCC

            string PPP = "ASD";
            string YW = "Y9";

            YW = gcf.GetYW(DateTime.Now.ToString("yyyy-MM-dd"));
            if (string.IsNullOrEmpty(YW))
            {
                MessageBox.Show("YW获取异常");
                return "NG";
            }
            string SSS;
            string aa = "";
            string CCCC = "";
            CCCC = (strpartno).ToUpper().Trim().Substring(0, 4);


            string CSN = string.Empty;
            //定义一个序号的 起始变量， 不因程序初始而变动， 每天变化。
            int startnum = 0;

            string sFile = string.Empty;
            sFile = @"D:\TEST_DEVELOPMENT\CSN12\" + PPP + YW + ".txt";
            int qty = 0;
            //qty = int.Parse(cmbCartonQty.SelectedItem.ToString());
            //qty = int.Parse(cmbCartonQty.Text);
            qty = int.Parse(strpackunit);

            #region  //抓序号流水号
            //读
            string sData = string.Empty;
            if (!File.Exists(sFile))
            {
                startnum = 1;
            }
            else
            {
                try
                {
                    using (StreamReader _sr = new StreamReader(sFile))
                    {
                        sData = _sr.ReadLine();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return "NG";
                }
                startnum = Convert.ToInt32(sData) + 1;
            }


            //回写
            StreamWriter writer = null;
            try
            {
                using (writer = new StreamWriter(sFile, false, Encoding.UTF8))
                {
                    writer.WriteLine((startnum + cartonscount * qty).ToString());
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
            #endregion

            //000 88590 99870 25102
            //0885909 987025102~987030102
            int startCartonnum = 0;
            string sFileCarton = string.Empty;
            sFileCarton = @"D:\TEST_DEVELOPMENT\Carton\" + "Carton.txt";
            #region //抓箱号流水号
            //读
            string sDataCarton = string.Empty;
            if (!File.Exists(sFileCarton))
            {
                //startCartonnum = 1;
                MessageBox.Show("Carton文件不存在，不能作业。");
            }
            else
            {
                try
                {
                    using (StreamReader _sr2 = new StreamReader(sFileCarton))
                    {
                        sDataCarton = _sr2.ReadLine();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return "NG";
                }
                startCartonnum = Convert.ToInt32(sDataCarton);
            }


            //回写
            StreamWriter writerCarton = null;
            try
            {
                using (writerCarton = new StreamWriter(sFileCarton, false, Encoding.UTF8))
                {
                    writerCarton.WriteLine((startCartonnum + cartonscount).ToString());
                    writerCarton.Flush();
                    writerCarton.Close();
                }
            }
            finally
            {
                if (writerCarton != null)
                {
                    writerCarton.Close();
                }
            }
            //--
            #endregion


            string strGS1snlist = string.Empty;
            strGS1snlist = "SSCC|CSN1|CSN2|CSN3|CSN4|CSN5|CSN6|CSN7|CSN8|CSN9|CSN10|COO|" + "\r\n";

            string strSN2D = string.Empty;
            string strGTIN = string.Empty;
            strGTIN = txtGTIN.Text.PadLeft(14, '0');
            string strUPC = string.Empty;
            string strJAN = string.Empty;
            strUPC = lblUPC.Text;
            strJAN = lblJAN.Text;

            string strGS1cartonlist = string.Empty;
            strGS1cartonlist = "SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN|COO1|COO2|COO3|" + "\r\n";

            for (int i = 1; i <= cartonscount; i++)
            {
                //string oldcartonno = (strpartno.StartsWith("L1S") ? strpartno.Substring(strpartno.Length - 3, 3) : strpartno.Substring(0,3)) +txtCartonFix.Text.Trim()+ DateTime.Now.ToString("MMddHH") + i.ToString().PadLeft(4, '0');
                string cartonno = "00885909" + (startCartonnum + i).ToString().PadLeft(9, '0');
                cartonno = cartonno + cc.CheckSSCCSum(cartonno);
                string sqlstr = "SELECT WORK_ORDER,SERIAL_NUMBER FROM PPSUSER.G_SN_STATUS WHERE CARTON_NO=:CARTON_NO AND ROWNUM=1";
                object[][] sqlparamstemp = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", cartonno } };
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparamstemp).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("箱号已存在，请变更固定码重新生成");
                    return "NG";
                }
                string snlist = string.Empty;
                string ssnlist = string.Empty;

                for (int j = 1; j <= qty; j++)
                {
                    //每次取一个序号
                    aa = "000" + gcf.GetNumtoWvV((startnum).ToString(), "A", "-");
                    SSS = aa.Substring(aa.Length - 3, 3);
                    CSN = PPP + YW + SSS + CCCC;
                    if (!cc.SN_InsertWo(cartonno.Substring(3, 15) + j.ToString().PadLeft(5, '0'), CSN, cartonno.Substring(9), partID, cartonno, locationID, palletNo))
                    {
                        return "NG";
                    }
                    snlist += CSN + "|";
                    ssnlist += "S" + CSN + ",";
                    startnum++;
                }
                strGS1snlist += cartonno + "|" + snlist + cc.GetCoo(strpartno, 2) + "|" + "\r\n";
                string strQTY = qty.ToString().PadLeft(2, '0');
                //SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN
                strSN2D = "V3," + "SSCC" + cartonno + ",GTIN" + strGTIN + ",MPN" + strmpn + ",QTY" + strQTY + "," + ssnlist;
                strGS1cartonlist += cartonno + "|" + strmpn + "|" + strSN2D + "|" + strGTIN + "|" + qty + "|" + strGTIN + strQTY + "|" + strUPC + "|" + strJAN + "|" + cc.GetCoo(strpartno, 2) + "|" + cc.GetCoo(strpartno, 4) + "|" + cc.GetCoo(strpartno, 3) + "\r\n";


                txtCartons.AppendText(cartonno + Environment.NewLine);
            }

            //产生2个lst文件，然后打印
            string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";

            string strBTWname = string.Empty;
            if (lblModel.Text.Equals("B188"))
            {
                strBTWname = "B188_GS1_MAIN";
            }
            else if (lblModel.Text.Equals("B288") || lblModel.Text.Equals("B298"))
            {
                strBTWname = "B288_GS1_MAIN";
            }
            else if (lblModel.Text.Equals("A145"))
            {
                if (strmpn.Contains("MLL82AM"))
                { strBTWname = "A145_GS1_MAIN_A"; }
                else if (strmpn.Contains("MLL82FE"))
                { strBTWname = "A145_GS1_MAIN_F"; }
                else
                { strBTWname = "A145_GS1_MAIN"; }

            }
            else if (lblModel.Text.Equals("B501"))
            {
                if (strmpn.Contains("MUFM2J"))
                { strBTWname = "B501_GS1_MAIN_J"; }
                else
                { strBTWname = "B501_GS1_MAIN"; }
            }

            string str7 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_MAIN" + ".lst";
            string str8 = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN" + ".lst";
            cc.WriteToPrintGo(str7, strGS1cartonlist);
            cc.WriteToPrintGo(str8, strGS1snlist);
            using (Process p = new Process())
            {
                string strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + strBTWname + ".btw";
                if (!File.Exists(strSampleFile))
                {
                    MessageBox.Show("Sample File Not exists-" + strSampleFile);
                    return "NG";
                }
                p.StartInfo.FileName = "bartend.exe";
                string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str7 + '"').Replace("@QTY", "1");
                p.StartInfo.Arguments = sArguments;
                p.Start();
                p.WaitForExit();
            }
            if (lblModel.Text.Equals("B501") || lblModel.Text.Equals("B188"))
            {
                using (Process p = new Process())
                {
                    string strSampleFile = string.Empty;
                    if (qty == 10)
                    {
                        strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN10" + ".btw";
                    }
                    if (qty == 4)
                    {
                        strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "GS1_SN4" + ".btw";
                    }


                    if (!File.Exists(strSampleFile))
                    {
                        MessageBox.Show("Sample File Not exists-" + strSampleFile);
                        return "NG";
                    }
                    p.StartInfo.FileName = "bartend.exe";
                    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str8 + '"').Replace("@QTY", "1");
                    p.StartInfo.Arguments = sArguments;
                    p.Start();
                    p.WaitForExit();
                }
            }

            object[][] procParams = new object[0][];
            DataTable dt2 = new DataTable();
            try
            {
                dt2 = ClientUtils.ExecuteProc("ppsuser.T_TRAN_WMS_DATA", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
                return "NG";

            }
            return "OK";
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        public bool SearchCarton()
        {
            string strSqlDN = @" select distinct  toi.delivery_no || '*' || toi.line_item || '*' ||                 toi.shipment_id || '*' || toi.ictpn || '*' || (tsp.ASSIGN_QTY-NVL(b.QTY,0)) || '*' || (tsp.ASSIGN_CARTON-NVL(b.CartonQty,0))
                      ||'*' || NVL(VPSG.GROUP_CODE,'XXXX')
  
                      as DN,                 toi.shipment_id,                 toi.delivery_no  , tsp.PALLET_NO 
                      FROM PPSUSER.T_ORDER_INFO toi

                      INNER JOIN PPSUSER.T_SHIPMENT_INFO tsi ON toi.SHIPMENT_ID=tsi.SHIPMENT_ID
                      inner join PPSUSER.T_pallet_order tsp on tsi.SHIPMENT_ID=tsp.SHIPMENT_ID and tsp.DELIVERY_NO=toi.DELIVERY_NO and tsp.LINE_ITEM=toi.LINE_ITEM 

                      LEFT JOIN PPSUSER.VW_PARKING_SEATING_GROUP vpsg ON TSI.CARRIER_CODE=vpsg.CARRIER
                      AND TSI.POE=vpsg.POE AND NVL(TSI.SERVICE_LEVEL,'NA') = NVL(vpsg.SERVICELEVEL,'NA')
                      AND TOI.ICTPN=vpsg.ICT_PARTNO

                      LEFT JOIN (
                        SELECT TTSS.DELIVERY_NO,TTSS.LINE_ITEM,TSS.PART_NO,COUNT(*) AS QTY,count(DISTINCT tss.CARTON_NO) AS CartonQty FROM PPSUSER.T_TROLLEY_SN_STATUS ttss INNER JOIN PPSUSER.T_SN_STATUS tss
                          ON ttss.CUSTOM_SN=TSS.CUSTOMER_SN
                          GROUP BY TTSS.DELIVERY_NO,TTSS.LINE_ITEM,TSS.PART_NO)B ON TOI.DELIVERY_NO=B.DELIVERY_NO AND TOI.LINE_ITEM=B.LINE_ITEM
                      AND TOI.ICTPN=B.PART_NO 
                      WHERE toi.PERSON_FLAG='Y' AND toi.STATUS IN ('WP','IP') AND tsi.STATUS  IN ('WP','IP') AND tsp.ASSIGN_QTY>NVL(B.QTY,0) and toi.shipment_id=:shipment_id ORDER BY  tsp.PALLET_NO  asc, 
                      delivery_no asc";

            object[][] Params = new object[1][];
            Params[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "shipment_id", textBox1.Text };
            DataSet dtsdn = ClientUtils.ExecuteSQL(strSqlDN, Params);
            if (dtsdn != null && dtsdn.Tables[0].Rows.Count > 0)
            {


                List<string> carrierListdn = (from d in dtsdn.Tables[0].AsEnumerable()
                                              select d.Field<string>("DN")).ToList();
                //carrierList.Sort();
                cmbDN.DataSource = carrierListdn;

            }
            else
            {
                //cmbDN.DataSource = null;
                initComboBox();
                MessageBox.Show("shipment ID not exist!");
            }
            return dtsdn.Tables[0].Rows.Count > 0;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //cmbDN.DataSource = null;
            //cmbDN.Items.Clear();
            SearchCarton();


        }

        private void btn_GetAll_Click(object sender, EventArgs e)
        {

            //btn_search_Click(sender, e);
            var res = SearchCarton();
            if (!res)
                return;
            int n = cmbDN.Items.Count;
            for (int i = 0; i < n; i++)
            {
                cmbDN.SelectedIndex = i;
                string carton_Qty = lblDNcaton.Text;
                //btn_search_Click(sender, e);
                //string str = cmbCAR.SelectedItem.ToString();
                Carton_all_generate(carton_Qty);
                richTextBox1.AppendText(txtCartons.Text);
                Thread.Sleep(100);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //btn_search_Click(sender, e);
            var res = SearchCarton();
            if (!res)
                return;
            int n = cmbDN.Items.Count;
            for (int i = 0; i < n; i++)
            {
                cmbDN.SelectedIndex = i;
                string carton_Qty = lblDNcaton.Text;
                //btn_search_Click(sender, e);
                //string str = cmbCAR.SelectedItem.ToString();
                //string tnt = cmbCAR.SelectedItem.ToString();
                Carton_all_generate_print(carton_Qty);
                richTextBox1.AppendText(txtCartons.Text);
                Thread.Sleep(100);
            }
        }
    }
}
