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
using System.Windows.Forms;


namespace CreatePPSDataAC
{
    public partial class MainForm3 : Form
    {
        DataTable PARTList = new DataTable();
        int ID = 1;
        BLL bll = new BLL();
        public MainForm3()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ClientUtils.ServerUrl = string.Format("http://{0}:8090/WCF_RemoteObject", ConfigurationManager.AppSettings["AP"]);
            //VN
            //ClientUtils.ServerUrl = "http://10.171.16.201:8090/WCF_RemoteObject";
            //JS
            //ClientUtils.ServerUrl = "http://10.54.10.14:8090/WCF_RemoteObject";
            //  ClientUtils.url = "http://172.23.28.52:8090";
            //江西测试AP  不同site需设置成不同对应的测试
            //       ClientUtils.url = "TCP://10.54.10.14:8085";
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

            //MPN
            cmbPartno.DataSource = null;
            cmbPartno.Items.Clear();
            string strSql = @"select trim(ICTPARTNO||'*'||MPN||'*'||PACKUNIT||'*'||max(TOTALQTY))  as MPN1  "
                            + " from NONEDIPPS.vw_mpn_info "
                            //where packcode <> subpackcode or subpackcode is null"
                            //+ " where  MPN like 'M%' and MPN like '%A' "
                            + " group by   ICTPARTNO ,MPN , PACKUNIT  "
                            + " order by MPN1 ";

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


            //Store
            cmbLocation.DataSource = null;
            cmbLocation.Items.Clear();
            //string strSql2 = @"select location_name "
            //                  + "    from (SELECT location_name "
            //                  + "            FROM PPSUSER.WMS_LOCATION "
            //                  + "           where LOCATION_ID not IN "
            //                  + "                 (SELECT LOCATION_ID FROM ppsuser.t_location a where a.qty = 0 and a.cartonqty=0  ) "
            //                  + "             and enabled = 'Y' "
            //                  + "             and location_name like 'A8%' "
            //                  + "           order by location_name) "
            //                  + "   where 1=1";

            string strSql2 = @"SELECT distinct a.location_name "
                              + "    FROM NONEDIPPS.WMS_LOCATION a "
                              + "   where LOCATION_ID not IN (select distinct location_ID "
                              + "                               from nonedipps.t_location ) "
                              + "     and enabled = 'Y' "
                              //昆山测试排除,江西不排除
                              //    + "     and (location_name like 'B1FG%') "
                              + "   order by a.location_name ";


            DataSet dts2 = ClientUtils.ExecuteSQL(strSql2);
            if (dts2 != null && dts2.Tables[0].Rows.Count > 0)
            {
                //List<string> List2 = (from e in dts2.Tables[0].AsEnumerable()
                //                      select e.Field<string>("location_name")).ToList();
                //List2.Sort();
                //cmbLocation.DataSource = List2;

                cmbLocation.DataSource = dts2.Tables[0];
                cmbLocation.DisplayMember = "location_name";
                cmbLocation.ValueMember = "location_name";
            }
            else
            {
                cmbLocation.DataSource = null;
            }
        }

        HShippingLabel.PrintLabel _printlabel;


        string partID = string.Empty;
        string locationID = string.Empty;
        private void btnCreateCarton_Click(object sender, EventArgs e)
        {
            btnCreateCarton.Enabled = false;
            string ictpartnompn = cmbPartno.Text;
            string[] partmpn = ictpartnompn.Split('*');
            string strpartno = partmpn[0];
            string strmpn = partmpn[1];
            string strpackunit = partmpn[2];
            string strPALLETQTY = partmpn[3];

            if (string.IsNullOrEmpty(strpartno))
            {
                btnCreateCarton.Enabled = true;
                return;
            }


            //string partno=txtPartNo.Text.Trim();
            var palletNo = DateTime.Now.ToString("yyyyMMddHHmmssms");
            txtCartons.Clear();

            if (!cc.CheckPartNo(strpartno, out partID))
            {
                MessageBox.Show("料号 错误");
                btnCreateCarton.Enabled = true;
                return;
            }
            if (string.IsNullOrEmpty(partID))
            {
                MessageBox.Show("在sys_part中获取不到料号");
                btnCreateCarton.Enabled = true;
                return;
            }
            if (!cc.ACCheckLocationId(cmbLocation.Text.Trim(), out locationID))
            {
                MessageBox.Show("储位 错误");
                btnCreateCarton.Enabled = true;
                return;
            }

            if ((strpackunit.Trim() == "1") && (this.rdoSCCarton.Checked || this.rdoSCCartonA.Checked))
            {
                MessageBox.Show("单包料号不能用SSCC18箱号,请检查!");
                btnCreateCarton.Enabled = true;
                return;
            }

            string strLOCATIONNO = cmbLocation.Text.Trim();

            string strCARTONCOUNT = nudCartonCount.Value.ToString();
            string strCSNTYPE = getCSNType().ToString();
            string strCARTONTYPE = getCartonType().ToString();
            string strResult = CreateCSN(strLOCATIONNO, strpartno, strCARTONCOUNT, strCSNTYPE, strCARTONTYPE);
            if (!strResult.Equals("OK"))
            {
                MessageBox.Show("产生序号错误，请联系IT");
            }

            string strRetMsg = string.Empty;
            string strResult2 = bll.ACTRANWMSDATA(out strRetMsg);
            if (!strResult2.Equals("OK"))
            {
                MessageBox.Show(strRetMsg);
            }


            initComboBox();
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
                string strSCC14 = string.Empty;
                txtGTIN.Text = cc.ACgetGTIN(strpartno, out strUPC, out strJAN, out strCustModel, out strSCC14);
                lblJAN.Text = strJAN;
                lblUPC.Text = strUPC;
                lblModel.Text = strCustModel;
                labSCC14.Text = strSCC14;

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
            sql = string.Format("select a.ictpartno, "
                               + "        a.upc_code, "
                               + "        a.jan_code, "
                               + "        a.custmodel,a.MPN, a.packunit,a.TOTALQTY,  "
                               + "        case "
                               + "          when a.custmodel = 'B188' then "
                               + "           decode(a.jan_code, '', a.upc_code, a.jan_code) "
                               + "          when a.custmodel = 'B501' then "
                               + "           decode(a.upc_code, '', a.jan_code, a.upc_code) "
                               + "          else "
                               + "           a.upc_code "
                               + "        end GTIN, "
                               + "        b.customer_sn "
                               + "        ,a.SCC14 "
                               + "   from NONEDIPPS.t_sn_status b "
                               + "   join NONEDIPPS.vw_mpn_info a "
                               + "      on b.part_no = a.ICTPARTNO "
                               //+ "  where  (a.packcode <> subpackcode or a.subpackcode is null) and  b.carton_no = '{0}'", strCarton);
                               + "  where   b.carton_no = '{0}'", strCarton);

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

            string strmpn = dataSet.Tables[0].Rows[0]["MPN"].ToString();
            string strpartno = dataSet.Tables[0].Rows[0]["ictpartno"].ToString();
            string strpackunit = dataSet.Tables[0].Rows[0]["packunit"].ToString();

            //通过料号查upccode 
            string strUPC = string.Empty;
            string strJAN = string.Empty;
            string strCustModel = string.Empty;
            string strSCC14 = string.Empty;
            txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel, out strSCC14);
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
            strGS1snlist = "SSCC|CSN1|CSN2|CSN3|CSN4|CSN5|CSN6|CSN7|CSN8|CSN9|CSN10|" + "\r\n";

            string strSN2D = string.Empty;
            string strGTIN = string.Empty;
            strGTIN = txtGTIN.Text.PadLeft(14, '0');
            string strGS1cartonlist = string.Empty;
            strGS1cartonlist = "SSCC | MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN|" + "\r\n";

            string snlist = string.Empty;
            string ssnlist = string.Empty;

            for (int m = 0; m < dataSet.Tables[0].Rows.Count; m++)
            {
                CSN = dataSet.Tables[0].Rows[m]["customer_sn"].ToString();
                snlist += CSN + "|";
                if (m == dataSet.Tables[0].Rows.Count - 1)
                {
                    ssnlist += "S" + CSN;
                }
                else
                {
                    ssnlist += "S" + CSN + ",";
                }
            }

            strGS1snlist += strCarton + "|" + snlist + "\r\n";
            string strQTY = qty.ToString().PadLeft(2, '0');
            //SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN
            strSN2D = "V3," + "SSCC" + strCarton + ",GTIN" + strGTIN + ",SCC" + strSCC14 + ",MPN" + strmpn + ",QTY" + strQTY + "," + ssnlist;
            strGS1cartonlist += strCarton + "|" + strmpn + "|" + strSN2D + "|" + strGTIN + "|" + qty + "|" + strGTIN + strQTY + "|" + strUPC + "|" + strJAN + "|" + "\r\n";


            //产生2个lst文件，然后打印
            string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";

            string strBTWname = string.Empty;
            if (strmodel.Equals("B188"))
            {
                strBTWname = "B188_GS1_MAIN";
            }
            else if (strmodel.Equals("B288"))
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
            if (strmodel.Equals("B501") || strmodel.Equals("B188") || strmodel.Equals("B288"))
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


            if (qty == 1)
            {
                using (Process p = new Process())
                {
                    string strsinglecsnlist = "PARTNO|MPN|CSN" + "\r\n" + strpartno + "|" + strmpn + "|" + CSN;

                    string strSampleFile = string.Empty;
                    strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "SINGLE_SN" + ".btw";

                    string str9 = Path.GetFullPath(strStartupPath) + @"\" + "SINGLE_SN" + CSN + ".lst";
                    cc.WriteToPrintGo(str9, strsinglecsnlist);

                    if (!File.Exists(strSampleFile))
                    {
                        MessageBox.Show("Sample File Not exists-" + strSampleFile);

                    }
                    p.StartInfo.FileName = "bartend.exe";
                    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str9 + '"').Replace("@QTY", "1");
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
            string strRetMsg = string.Empty;
            string strResult2 = bll.ACTRANWMSDATA(out strRetMsg);
            if (!strResult2.Equals("OK"))
            {
                MessageBox.Show(strRetMsg);
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
        public string CreateCSN(string locationno, string partno, string cartoncount, string csntype, string cartontype)
        {
            if (string.IsNullOrEmpty(partno))
            {
                MessageBox.Show("料号为空");
                return "NG";
            }

            string ictpartnompn = cc.ACgetMPNInfo(partno);
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
            if (string.IsNullOrEmpty(partID))
            {
                MessageBox.Show("在sys_part中获取不到料号");
                return "NG";
            }
            if (!cc.ACCheckLocationId(locationno, out locationID))
            {
                MessageBox.Show("储位 错误");
                return "NG";
            }
            if (cc.ACcheckLocation(locationno).Equals("NG"))
            {
                MessageBox.Show(locationno + "储位已经使用");
                return "NG";
            }

            int cartonscount = Convert.ToInt32(cartoncount);

            //string PPP = "ASD";
            //VN测试用VSD
            string PPP = "VNN";
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
            if (!strResultGetSN.Equals("OK"))
            {
                return "NG";
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
                if (!strResultGetSNCarton.Equals("OK"))
                {
                    return "NG";
                }
            }
            if (cartontype.Equals("SCCARTONA"))
            {
                //"SCCARTONA"
                strResultGetSNCarton = cc.GetSNRangeByProcedure(cartontype, "888462", cartonscount, out outqtyCarton);
                if (!strResultGetSNCarton.Equals("OK"))
                {
                    return "NG";
                }
            }

            int startCartonnum = outqtyCarton;


            string strGS1snlist = string.Empty;
            strGS1snlist = "SSCC|CSN1|CSN2|CSN3|CSN4|CSN5|CSN6|CSN7|CSN8|CSN9|CSN10|" + "\r\n";

            string strSN2D = string.Empty;
            string strGTIN = string.Empty;
            string strUPC = string.Empty;
            string strJAN = string.Empty;
            string strSCC14 = string.Empty;
            //txtGTIN.Text = cc.getGTIN(strpartno, out strUPC, out strJAN, out strCustModel);
            strGTIN = cc.ACgetGTIN(strpartno, out strUPC, out strJAN, out strCustModel, out strSCC14);
            strGTIN = strGTIN.PadLeft(14, '0');

            string strGS1cartonlist = string.Empty;
            strGS1cartonlist = "SSCC | MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN|" + "\r\n";
            string strsinglecsnlist = "PARTNO|MPN|CSN" + "\r\n";

            for (int i = 1; i <= cartonscount; i++)
            {

                string cartonno = string.Empty;
                if (cartontype.Equals("SCCARTONA") || cartontype.Equals("SCCARTON"))
                {
                    cartonno = "00888462" + (startCartonnum + i).ToString().PadLeft(9, '0');
                    cartonno = cartonno + cc.CheckSSCCSum(cartonno);
                }
                else
                {
                    //cartonno = (strpartno.StartsWith("L1S") ? strpartno.Substring(strpartno.Length - 3, 3) : strpartno.Substring(0, 3)) + txtCartonFix.Text.Trim() + DateTime.Now.ToString("MMddHH") + i.ToString().PadLeft(4, '0');
                    cartonno = strpartno.Substring(0, 3) + DateTime.Now.ToString("yyMMddHHmmssfff") + i.ToString().PadLeft(4, '0');

                }

                string sqlstr = "SELECT WORK_ORDER,SERIAL_NUMBER FROM NONEDIPPS.G_SN_STATUS WHERE CARTON_NO=:CARTON_NO AND ROWNUM=1";
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
                    //如果为单包,则外箱号和Custmer_SN号一致
                    string strTemp1 = cartonno.Substring(3) + j.ToString().PadLeft(3, '0');
                    string strTemp2 = cartonno.Substring(9);
                    if (qty == 1)
                    {
                        cartonno = CSN;
                    }
                    if (!cc.ACSN_InsertWo(strTemp1, CSN, strTemp2, partID, cartonno, locationID, palletNo))
                    {
                        return "NG";
                    }
                    snlist += CSN + "|";
                    if (j == qty)
                    {
                        ssnlist += "S" + CSN;
                    }
                    else
                    {
                        ssnlist += "S" + CSN + ",";
                    }
                    startnum++;

                    strsinglecsnlist += strpartno + "|" + strmpn + "|" + cartonno + "\r\n";




                }
                strGS1snlist += cartonno + "|" + snlist + "\r\n";
                string strQTY = qty.ToString().PadLeft(2, '0');
                //SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN
                strSN2D = "V3," + "SSCC" + cartonno + ",GTIN" + strGTIN + ",SCC" + strSCC14 + ",MPN" + strmpn + ",QTY" + strQTY + "," + ssnlist;
                strGS1cartonlist += cartonno + "|" + strmpn + "|" + strSN2D + "|" + strGTIN + "|" + qty + "|" + strGTIN + strQTY + "|" + strUPC + "|" + strJAN + "|" + "\r\n";
                txtCartons.AppendText(cartonno + Environment.NewLine);
            }

            if ((qty == 1) && (chkCSNListLabel.Checked))
            {
                using (Process p = new Process())
                {
                    string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";
                    string strSampleFile = string.Empty;
                    strSampleFile = Path.GetFullPath(strStartupPath) + @"\" + "SINGLE_SN" + ".btw";

                    string str9 = Path.GetFullPath(strStartupPath) + @"\" + "SINGLE_SN" + locationno + ".lst";
                    cc.WriteToPrintGo(str9, strsinglecsnlist);

                    if (!File.Exists(strSampleFile))
                    {
                        MessageBox.Show("Sample File Not exists-" + strSampleFile);

                    }
                    p.StartInfo.FileName = "bartend.exe";
                    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + str9 + '"').Replace("@QTY", "1");
                    p.StartInfo.Arguments = sArguments;
                    p.Start();
                    p.WaitForExit();
                }
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
                else if (strCustModel.Equals("B288"))
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
                else if (strCustModel.Equals("N144") || strCustModel.Equals("N146"))
                {
                    strBTWname = "WATCH_GS1_MAIN";
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
                if ((strCustModel.Equals("B501") || strCustModel.Equals("B188") || strCustModel.Equals("B288")) && (chkCSNListLabel.Checked))
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

            //string PPP = "ASD";
            //江西测试用JSD
            string PPP = "JSD";
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
            strGS1snlist = "SSCC|CSN1|CSN2|CSN3|CSN4|CSN5|CSN6|CSN7|CSN8|CSN9|CSN10|" + "\r\n";

            string strSN2D = string.Empty;
            string strGTIN = string.Empty;
            strGTIN = txtGTIN.Text.PadLeft(14, '0');
            string strUPC = string.Empty;
            string strJAN = string.Empty;
            string strSCC14 = string.Empty;
            strUPC = lblUPC.Text;
            strJAN = lblJAN.Text;
            strSCC14 = labSCC14.Text;
            string strGS1cartonlist = string.Empty;
            strGS1cartonlist = "SSCC | MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN|" + "\r\n";

            for (int i = 1; i <= cartonscount; i++)
            {
                //string oldcartonno = (strpartno.StartsWith("L1S") ? strpartno.Substring(strpartno.Length - 3, 3) : strpartno.Substring(0,3)) +txtCartonFix.Text.Trim()+ DateTime.Now.ToString("MMddHH") + i.ToString().PadLeft(4, '0');
                string cartonno = "00885909" + (startCartonnum + i).ToString().PadLeft(9, '0');
                cartonno = cartonno + cc.CheckSSCCSum(cartonno);
                string sqlstr = "SELECT WORK_ORDER,SERIAL_NUMBER FROM NONEDIPPS.G_SN_STATUS WHERE CARTON_NO=:CARTON_NO AND ROWNUM=1";
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
                    if (j == qty)
                    {
                        ssnlist += "S" + CSN;
                    }
                    else
                    {
                        ssnlist += "S" + CSN + ",";
                    }
                    startnum++;
                }
                strGS1snlist += cartonno + "|" + snlist + "\r\n";
                string strQTY = qty.ToString().PadLeft(2, '0');
                //SSCC|MPN|SN2D|GTIN|QTY|GTINQTY|UPC|JAN
                strSN2D = "V3," + "SSCC" + cartonno + ",GTIN" + strGTIN + ",SCC" + strSCC14 + ",MPN" + strmpn + ",QTY" + strQTY + "," + ssnlist;
                strGS1cartonlist += cartonno + "|" + strmpn + "|" + strSN2D + "|" + strGTIN + "|" + qty + "|" + strGTIN + strQTY + "|" + strUPC + "|" + strJAN + "|" + "\r\n";


                txtCartons.AppendText(cartonno + Environment.NewLine);
            }

            //产生2个lst文件，然后打印
            string strStartupPath = System.Windows.Forms.Application.StartupPath + "\\label";

            string strBTWname = string.Empty;
            if (lblModel.Text.Equals("B188"))
            {
                strBTWname = "B188_GS1_MAIN";
            }
            else if (lblModel.Text.Equals("B288"))
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

            string strRetMsg = string.Empty;
            string strResult2 = bll.ACTRANWMSDATA(out strRetMsg);
            if (!strResult2.Equals("OK"))
            {
                MessageBox.Show(strRetMsg);
                return "NG";
            }
            return "OK";
        }

        //private void btnPpart_Click(object sender, EventArgs e)
        //{
        //    MainForm2 pr = new MainForm2();
        //    pr.ShowDialog();
        //}
    }
}
