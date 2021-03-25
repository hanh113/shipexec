using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PickListAC
{
    public partial class TESTSN_Form : Form
    {
        public TESTSN_Form()
        {
            InitializeComponent();
            initComboBox();
        }

        //private string g_SQL;
        private void initComboBox()
        {

            //MPN
            cmbPartno.Items.Clear();
            string strSql = @"select trim(part||'*'||custpart||'*'||packqty)  as MPN  "
                            + "  from NONEDIOMS.oms_partmapping "
                            + " where isdisabled <> 1 "
                            + "    or isdisabled is null "
                            + " order by MPN";

            DataSet dts = ClientUtils.ExecuteSQL(strSql);
            if (dts != null && dts.Tables[0].Rows.Count > 0)
            {


                List<string> carrierList = (from d in dts.Tables[0].AsEnumerable()
                                            select d.Field<string>("MPN")).ToList();
                carrierList.Sort();
                cmbPartno.DataSource = carrierList;

            }
            else
            {
                cmbPartno.DataSource = null;
            }
        }

        private void TESTSN_Form_Load(object sender, EventArgs e)
        {

        }

        private void btnS_Click(object sender, EventArgs e)
        {
            //查询料号WO 站可用数量，
            //打印一个栈板 和其中carton的箱号
            string ictpartnompn = cmbPartno.Text;

            string[] partmpn = ictpartnompn.Split('*');
            string strpartno = partmpn[0];
            string strmpn = partmpn[1];
            string strpackunit = partmpn[2];

            int cartonscount = (int)numericUpDown1.Value;
            //打印的栏位PALLET_NO,CURPAGE,TOTALPAGE,CARTONNO1,CARTONNO2,CARTONNO3,
            if (radioButton1.Checked)
            {
                dgvPC.DataSource = null;
                string sql = string.Empty;
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    sql = string.Format("SELECT distinct pallet_no,wc,qty,carton_no "
                                    + "     FROM(SELECT ROW_NUMBER() OVER(PARTITION BY pallet_no ORDER BY carton_no, pallet_no DESC) rn,"
                                    + "                  b.* "
                                    + "             FROM(select distinct t1.*, tss.carton_no "
                                    + "                     from NONEDIPPS.t_sn_status tss, "
                                    + "                          (select * from (select PALLET_NO, wc, COUNT(serial_number) as QTY "
                                    + "                             from NONEDIPPS.t_sn_status "
                                    + "                            WHERE PART_NO = '{0}' "
                                    + "                              and wc = 'W0' "
                                    + "                              and pallet_no not in "
                                    + "                                  (select pallet_no "
                                    + "                                    from NONEDIPPS.t_sn_status "
                                    + "                                    where wc <> 'W0') "
                                    + "                            GROUP BY PALLET_NO, wc) where rownum<= {1}) t1 "
                                    + "                    where tss.pallet_no = t1.pallet_no) b) t "
                                    + "    WHERE t.rn <= 3 "
                                    + "    order by pallet_no ", strpartno, cartonscount);
                }
                else
                {
                    sql = string.Format("SELECT distinct pallet_no,wc,qty,carton_no "
                                    + "     FROM(SELECT ROW_NUMBER() OVER(PARTITION BY pallet_no ORDER BY carton_no, pallet_no DESC) rn,"
                                    + "                  b.* "
                                    + "             FROM(select distinct t1.*, tss.carton_no "
                                    + "                     from NONEDIPPS.t_sn_status tss, "
                                    + "                          (select * from (select PALLET_NO, wc, COUNT(serial_number) as QTY "
                                    + "                             from NONEDIPPS.t_sn_status "
                                    + "                            WHERE PART_NO = '{0}' and location_no ='{1}' "
                                    + "                              and wc = 'W0' "
                                    + "                              and pallet_no not in "
                                    + "                                  (select pallet_no "
                                    + "                                    from NONEDIPPS.t_sn_status "
                                    + "                                    where wc <> 'W0') "
                                    + "                            GROUP BY PALLET_NO, wc) where rownum<= {2}) t1 "
                                    + "                    where tss.pallet_no = t1.pallet_no) b) t "
                                    + "    WHERE t.rn <= 3 "
                                    + "    order by pallet_no ", strpartno,txtLocation.Text, cartonscount);
                }
                
                DataTable dt2 = new DataTable();
                try
                {
                    dt2 = ClientUtils.ExecuteSQL(sql).Tables[0];
                }
                catch (Exception e2)
                {
                    MessageBox.Show(e2.ToString());
                    return;
                }
                dgvPC.DataSource = dt2;
                dgvPC.AutoResizeColumns();
            }

            if (radioButton2.Checked || this.radSingle.Checked)
            {
                dgvPC.DataSource = null;
                string sql = string.Empty;
                if (radSingle.Checked)
                {
                    if (string.IsNullOrEmpty(txtLocation.Text))
                    {
                        sql = string.Format("select pallet_no,CARTON_NO,qty "
                                          + "      from(select pallet_no, CUSTOMER_SN AS CARTON_NO, COUNT(serial_number) as QTY "
                                          + "              from NONEDIPPS.t_sn_status "
                                          + "            WHERE PART_NO = '{0}' "
                                          + "              and wc = 'W0' "
                                          + "            GROUP BY pallet_no, CUSTOMER_SN) "
                                          + "    where rownum < ={1} ", strpartno, cartonscount);
                    }
                    else
                    {
                        sql = string.Format("select pallet_no,CARTON_NO,qty "
                                          + "      from(select pallet_no, CUSTOMER_SN AS CARTON_NO, COUNT(serial_number) as QTY "
                                          + "              from NONEDIPPS.t_sn_status "
                                          + "            WHERE PART_NO = '{0}' and location_no ='{1}'  "
                                          + "              and wc = 'W0' "
                                          + "            GROUP BY pallet_no, CUSTOMER_SN) "
                                          + "    where rownum < ={2} ", strpartno, txtLocation.Text, cartonscount);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtLocation.Text))
                    {
                        sql = string.Format("select pallet_no,carton_no,qty "
                                          + "      from(select pallet_no, CARTON_NO, COUNT(serial_number) as QTY "
                                          + "              from NONEDIPPS.t_sn_status "
                                          + "            WHERE PART_NO = '{0}' "
                                          + "              and wc = 'W0' "
                                          + "            GROUP BY pallet_no, CARTON_NO) "
                                          + "    where rownum < ={1} ", strpartno, cartonscount);
                    }
                    else
                    {
                        sql = string.Format("select pallet_no,carton_no,qty "
                                          + "      from(select pallet_no, CARTON_NO, COUNT(serial_number) as QTY "
                                          + "              from NONEDIPPS.t_sn_status "
                                          + "            WHERE PART_NO = '{0}' and location_no ='{1}'  "
                                          + "              and wc = 'W0' "
                                          + "            GROUP BY pallet_no, CARTON_NO) "
                                          + "    where rownum < ={2} ", strpartno, txtLocation.Text, cartonscount);
                    }
                }
                   
                DataTable dt = new DataTable();
                try
                {
                    dt = ClientUtils.ExecuteSQL(sql).Tables[0];
                }
                catch (Exception e2)
                {
                    MessageBox.Show(e2.ToString());
                    return;
                }
                dgvPC.DataSource = dt;
                dgvPC.AutoResizeColumns();
            }




        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            showCMB();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            showCMB();
        }

        private void radSingle_CheckedChanged(object sender, EventArgs e)
        {
            showCMB();
            this.dgvPC.Rows.Clear();
            this.dgvPC.DataSource = null;
        }

        private void showCMB()
        {
            if (radioButton1.Checked)
            {
                numericUpDown1.Value = 1;
                label4.Text = "栈板数:";
                this.mudList.Enabled = true;
            }

            if (radioButton2.Checked)
            {
                numericUpDown1.Value = 3;
                label4.Text = "箱 数:";
                this.mudList.Enabled = true;
            }
            if(this.radSingle.Checked)
            {
                this.mudList.Value = 1;
                numericUpDown1.Value = 3;
                label4.Text = "箱 数:";
                this.mudList.Enabled = false;
            }
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            string strPage = txtPage.Text.Trim();

            int listcount = (int)mudList.Value;
            //判断是否是单包
            if (this.radSingle.Checked)
            {
                string[] strArrPart = this.cmbPartno.Text.Trim().Split('*');
                if (strArrPart.Length >= 3)
                {
                    int cartonCount = 0;
                    try
                    {
                        cartonCount = Convert.ToInt32(strArrPart[2]);
                        if (cartonCount != 1)
                        {
                            MessageBox.Show("该料号为非单包料号,请检查!");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("料号PACKUNIT信息有误,请检查!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("料号信息有误,请检查!");
                    return;
                }
            }
            if (string.IsNullOrEmpty(strPage))
            {
                if (PrintPalletLabel(listcount, "ALL"))
                {

                    MessageBox.Show("打印OK");
                }
                else
                {
                    MessageBox.Show("打印失败");
                }
            }
            else
            {
                if (PrintPalletLabel(listcount, strPage))
                {

                    MessageBox.Show("打印OK");
                }
                else
                {
                    MessageBox.Show("打印失败");
                }
            }
           
            txtPage.Text = "";
        }

        private bool PrintPalletLabel( int listrows, string pages)
        {
            //打印的栏位PALLET_NO,CURPAGE,TOTALPAGE,CARTONNO1,CARTONNO2,CARTONNO3,
          
            if (dgvPC.Rows.Count > 0)
            {
                //确定打印的label的张数palletcount， 定义个list的行数datarows，
                //mix的所有行数
                int mixTotalrow = dgvPC.Rows.Count-1;

                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));


                //---------------------
                string sMessage = "";
                //string strMessage = string.Empty;
                string strLabelName = @"TESTSN_LABEL";
                if (this.radSingle.Checked)
                {
                    strLabelName = "TESTSN_LABEL_SINGLE";
                }
                string strLabelPath = @"D:\MES_CLIENT\Shipping\Label";

                string strStartupPath = System.Windows.Forms.Application.StartupPath;
                string LabelParam = @"PALLET_NO|CURPAGE|TOTALPAGE|CARTONNO1|CARTONNO2|CARTONNO3|";
                if (this.radSingle.Checked)
                {
                    LabelParam = @"PALLET_NO|CURPAGE|TOTALPAGE|MPN|ICTPN|CARTONNO1|CARTONNO2|CARTONNO3|";
                }
                //目前 打印的不知道在哪设置 支持|  ， 目前只能用逗号

                LabelParam = LabelParam.Replace("|", @",");
                
                string strHead = "";

                //确定这两个值
                //|CURPAGE|TOTALPAGE|PALLET_NO
               

                //label上清单值的部分
                //    PALLET_NO|CARTONNO1|CARTONNO2|CARTONNO3|

                string strLine = "";

                //string strAll = "";
                //HYQ：这循环里面只是产生.lst文件， 如果打印5张就产生5个文档。 
                //.lst 的内容是 一行栏位名 来源于.dat文件strItemno,加换行， 再加已将数据，数据部分是由strHead + strLine。

                //所有页产生到一个文档里面

                string strALLlist = string.Empty;
                strALLlist = LabelParam;
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    string[] strHeadArr = new string[TOTALPAGE];
                    string[] strLineArr = new string[TOTALPAGE];
                    string[] strAllArr = new string[TOTALPAGE];

                    //确定这些值
                    //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC
                    //pallet_no

                    string strpallet = "";
                    //因i从0开始，所以加1
                    string strcurpage = (i + 1).ToString();
                    strpallet = dgvPC.Rows[i * listrows].Cells["pallet_no"].Value.ToString();

                    strHead = "";
                    strHead = strpallet + "," +
                              strcurpage + "," +
                              TOTALPAGE + ",";
                    strHeadArr[i] = strHead;
                    if (this.radSingle.Checked)
                    {
                        string[] strArrPart = this.cmbPartno.Text.Trim().Split('*');
                        strHeadArr[i] = strHeadArr[i] + strArrPart[1] + "," + strArrPart[0] + ",";
                    }

                    //确定以下的部分 循环
                    // |CARTONNO1|

                    strLine = "";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dgvPC.Rows.Count)
                        {
                            break;
                        }
                        string strcarton = "";
                        try { strcarton = dgvPC.Rows[j].Cells["carton_no"].Value.ToString(); }
                        catch (Exception){ strcarton = ""; }

                        strLine = strLine + strcarton + ",";
                    }
                    strLineArr[i] = strLine;
                    strAllArr[i] = "\r\n" + strHeadArr[i] + strLineArr[i];
                    if (pages.Equals("ALL"))
                    {
                        strALLlist = strALLlist + strAllArr[i];
                    }
                    else
                    {
                        int page = Convert.ToInt32(pages);
                        if (page < TOTALPAGE || page == TOTALPAGE)
                        {
                            page = page - 1;
                            if (page==i)
                            {
                                strALLlist = strALLlist + strAllArr[i];
                            }
                        }
                     }
                }//end for
                string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".lst";
                this.WriteToPrintGo(str7, strALLlist);
                using (Process p = new Process())
                {
                    string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                    if (!File.Exists(strSampleFile))
                    {
                        sMessage = "Sample File Not exists-" + strSampleFile;
                        return false;
                    }
                    p.StartInfo.FileName = "bartend.exe";
                    string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName  + ".lst" + '"').Replace("@QTY", "1");
                    p.StartInfo.Arguments = sArguments;
                    p.Start();
                    p.WaitForExit();
                }
                return true; 
            }
            else
            {
                return false;
            }

        }

        private bool PrintPalletLabel_bak2(int listrows, string pages)
        {
            //打印的栏位PALLET_NO,CURPAGE,TOTALPAGE,CARTONNO1,CARTONNO2,CARTONNO3,

            if (dgvPC.Rows.Count > 0)
            {
                //确定打印的label的张数palletcount， 定义个list的行数datarows，
                //mix的所有行数
                int mixTotalrow = dgvPC.Rows.Count - 1;

                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));


                //---------------------
                string sMessage = "";
                //string strMessage = string.Empty;
                string strLabelName = @"TESTSN_LABEL";
                string strLabelPath = @"D:\MES_CLIENT\Shipping\Label";

                string strStartupPath = System.Windows.Forms.Application.StartupPath;
                string LabelParam = @"PALLET_NO|CURPAGE|TOTALPAGE|CARTONNO1|CARTONNO2|CARTONNO3|";
                //目前 打印的不知道在哪设置 支持|  ， 目前只能用逗号

                LabelParam = LabelParam.Replace("|", @",");

                string strHead = "";

                //确定这两个值
                //|CURPAGE|TOTALPAGE|PALLET_NO


                //label上清单值的部分
                //    PALLET_NO|CARTONNO1|CARTONNO2|CARTONNO3|

                string strLine = "";

                //string strAll = "";
                //HYQ：这循环里面只是产生.lst文件， 如果打印5张就产生5个文档。 
                //.lst 的内容是 一行栏位名 来源于.dat文件strItemno,加换行， 再加已将数据，数据部分是由strHead + strLine。

                for (int i = 0; i < TOTALPAGE; i++)
                {
                    string[] strHeadArr = new string[TOTALPAGE];
                    string[] strLineArr = new string[TOTALPAGE];
                    string[] strAllArr = new string[TOTALPAGE];

                    //确定这些值
                    //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC
                    //pallet_no

                    string strpallet = "";
                    //因i从0开始，所以加1
                    string strcurpage = (i + 1).ToString();
                    strpallet = dgvPC.Rows[i * listrows].Cells["pallet_no"].Value.ToString();

                    strHead = "";
                    strHead = strpallet + "," +
                              strcurpage + "," +
                              TOTALPAGE + ",";
                    strHeadArr[i] = strHead;

                    //确定以下的部分 循环
                    // |CARTONNO1|

                    strLine = "";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dgvPC.Rows.Count)
                        {
                            break;
                        }
                        string strcarton = "";
                        try { strcarton = dgvPC.Rows[j].Cells["carton_no"].Value.ToString(); }
                        catch (Exception)
                        { strcarton = ""; }



                        strLine = strLine + strcarton + ",";
                    }
                    strLineArr[i] = strLine;

                    strAllArr[i] = LabelParam + "\r\n" + strHeadArr[i] + strLineArr[i];


                    //HYQ： 以下3行不一定会用

                    string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i + ".lst";
                    // if (File.Exists(str7))
                    //{
                    //     File.Delete(str7);
                    // }
                    this.WriteToPrintGo(str7, strAllArr[i]);

                    if (pages.Equals("ALL"))
                    {

                        using (Process p = new Process())
                        {
                            string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                            if (!File.Exists(strSampleFile))
                            {
                                sMessage = "Sample File Not exists-" + strSampleFile;
                                return false;
                            }

                            if (i == TOTALPAGE - 1)
                            {
                                p.StartInfo.FileName = "bartend.exe";
                                string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";

                                sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i.ToString() + ".lst" + '"').Replace("@QTY", "1");
                                p.StartInfo.Arguments = sArguments;
                                p.Start();
                                p.WaitForExit();
                            }
                            else
                            {
                                p.StartInfo.FileName = "bartend.exe";
                                string sArguments = @" /F=@PATH1 /D=@PATH2 /P  /C=@QTY";
                                sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i.ToString() + ".lst" + '"').Replace("@QTY", "1");
                                p.StartInfo.Arguments = sArguments;
                                p.Start();
                                p.WaitForInputIdle();
                            }
                            //p.WaitForExit();
                        }

                        //using (Process p = new Process())
                        //{
                        //    #region   
                        //    string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                        //    if (!File.Exists(strSampleFile))
                        //    {
                        //        sMessage = "Sample File Not exists-" + strSampleFile;
                        //        return false;
                        //    }

                        //    string sData = @"""C:\Program Files (x86)\Seagull\BarTender Suite\bartend.exe"" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        //    sData = sData.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i.ToString() + ".lst" + '"').Replace("@QTY", "1");

                        //    p.StartInfo.FileName = "cmd.exe";
                        //    p.StartInfo.UseShellExecute = false;
                        //    p.StartInfo.RedirectStandardInput = true;
                        //    p.StartInfo.RedirectStandardOutput = true;
                        //    p.StartInfo.CreateNoWindow = true;
                        //    p.Start();
                        //    p.StandardInput.WriteLine(sData + "&exit");
                        //    p.StandardInput.AutoFlush = true;
                        //    p.StandardInput.Close();
                        //    p.WaitForExit();
                        //    //System.Threading.Thread.Sleep(1000);
                        //    #endregion
                        //}

                    }
                }



                if (!string.IsNullOrEmpty(sMessage))
                {
                    return false;
                }


                if (!pages.Equals("ALL"))
                {
                    //打印 指定的第几张
                    #region
                    int page = Convert.ToInt32(pages);
                    if (page < TOTALPAGE || page == TOTALPAGE)
                    {

                        page = page - 1;

                        using (Process p = new Process())
                        {
                            string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                            if (!File.Exists(strSampleFile))
                            {
                                sMessage = "Sample File Not exists-" + strSampleFile;
                                return false;
                            }
                            p.StartInfo.FileName = "bartend.exe";
                            string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                            sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + page + ".lst" + '"').Replace("@QTY", "1");
                            p.StartInfo.Arguments = sArguments;
                            p.Start();
                            p.WaitForExit();
                        }
                    }
                    else
                    {
                        return false;
                    }
                    #endregion
                }

                return true;
            }
            else
            {
                return false;
            }

        }

        private bool PrintPalletLabel2_back(int listrows, string pages)
        {
            //打印的栏位PALLET_NO,CURPAGE,TOTALPAGE,CARTONNO1,CARTONNO2,CARTONNO3,

            if (dgvPC.Rows.Count > 0)
            {
                //确定打印的label的张数palletcount， 定义个list的行数datarows，
                //mix的所有行数
                int mixTotalrow = dgvPC.Rows.Count - 1;

                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));


                //---------------------
                string sMessage = "";
                //string strMessage = string.Empty;
                string strLabelName = @"TESTSN_LABEL";
                string strLabelPath = @"D:\MES_CLIENT\Shipping\Label";

                string strStartupPath = System.Windows.Forms.Application.StartupPath;
                string LabelParam = @"PALLET_NO|CURPAGE|TOTALPAGE|CARTONNO1|CARTONNO2|CARTONNO3|";
                //目前 打印的不知道在哪设置 支持|  ， 目前只能用逗号

                LabelParam = LabelParam.Replace("|", @",");

                string strHead = "";

                //确定这两个值
                //|CURPAGE|TOTALPAGE|PALLET_NO


                //label上清单值的部分
                //    PALLET_NO|CARTONNO1|CARTONNO2|CARTONNO3|

                string strLine = "";

                //string strAll = "";
                //HYQ：这循环里面只是产生.lst文件， 如果打印5张就产生5个文档。 
                //.lst 的内容是 一行栏位名 来源于.dat文件strItemno,加换行， 再加已将数据，数据部分是由strHead + strLine。

                for (int i = 0; i < TOTALPAGE; i++)
                {
                    string[] strHeadArr = new string[TOTALPAGE];
                    string[] strLineArr = new string[TOTALPAGE];
                    string[] strAllArr = new string[TOTALPAGE];

                    //确定这些值
                    //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC
                    //pallet_no

                    string strpallet = "";
                    //因i从0开始，所以加1
                    string strcurpage = (i + 1).ToString();
                    strpallet = dgvPC.Rows[i * listrows].Cells["pallet_no"].Value.ToString();

                    strHead = "";
                    strHead = strpallet + "," +
                              strcurpage + "," +
                              TOTALPAGE + ",";
                    strHeadArr[i] = strHead;

                    //确定以下的部分 循环
                    // |CARTONNO1|

                    strLine = "";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dgvPC.Rows.Count)
                        {
                            break;
                        }
                        string strcarton = "";
                        try { strcarton = dgvPC.Rows[j].Cells["carton_no"].Value.ToString(); }
                        catch (Exception )
                        { strcarton = ""; }



                        strLine = strLine + strcarton + ",";
                    }
                    strLineArr[i] = strLine;

                    strAllArr[i] = LabelParam + "\r\n" + strHeadArr[i] + strLineArr[i];


                    //HYQ： 以下3行不一定会用

                    string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i + ".lst";
                    // if (File.Exists(str7))
                    //{
                    //     File.Delete(str7);
                    // }
                    this.WriteToPrintGo(str7, strAllArr[i]);

                    if (pages.Equals("ALL"))
                    {
                        using (Process p = new Process())
                        {

                            string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                            if (!File.Exists(strSampleFile))
                            {
                                sMessage = "Sample File Not exists-" + strSampleFile;
                                return false;
                            }

                            string sData = @"""C:\Program Files (x86)\Seagull\BarTender Suite\bartend.exe"" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                            sData = sData.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i.ToString() + ".lst" + '"').Replace("@QTY", "1");

                            //HYQ：20181120  这地方要改， 需改为执行命令
                            //this.WriteToPrintGo(sFile, sData);
                            //int num4 = WinExec(sFile, 0);
                            //System.Threading.Thread.Sleep(1 * 1000);

                            p.StartInfo.FileName = "cmd.exe";
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.RedirectStandardInput = true;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.CreateNoWindow = true;
                            MessageBox.Show("LABEL" + (i + 1).ToString());
                            //Process[] KillmyProcess = Process.GetProcessesByName("cmd.exe");
                            //foreach (Process process in KillmyProcess)
                            //{
                            //    process.Kill();
                            //}
                            p.Start();
                            p.StandardInput.WriteLine(sData + "&exit");

                            p.StandardInput.AutoFlush = true;
                            p.StandardInput.Close();
                            p.WaitForExit();
                            MessageBox.Show("LABELB" + (i + 1).ToString());
                            //System.Threading.Thread.Sleep(1000);

                        }

                    }
                }



                if (!string.IsNullOrEmpty(sMessage))
                {
                    return false;
                }


                if (!pages.Equals("ALL"))
                {
                    //打印 指定的第几张
                    #region
                    int page = Convert.ToInt32(pages);
                    if (page < TOTALPAGE || page == TOTALPAGE)
                    {
                        string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                        if (!File.Exists(strSampleFile))
                        {
                            sMessage = "Sample File Not exists-" + strSampleFile;
                            return false;
                        }
                        page = page - 1;
                        string sData = @"""C:\Program Files (x86)\Seagull\BarTender Suite\bartend.exe"" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        sData = sData.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + page + ".lst" + '"').Replace("@QTY", "1");
                        //this.WriteToPrintGo(sFile, sData);
                        //int num4 = WinExec(sFile, 0);

                        using (Process p = new Process())
                        {
                            p.StartInfo.FileName = "cmd.exe";
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.RedirectStandardInput = true;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.CreateNoWindow = true;
                            p.Start();
                            p.StandardInput.WriteLine(sData + "&exit");
                            p.WaitForExit();


                        }
                    }
                    else
                    {
                        return false;
                    }
                    #endregion
                }

                return true;
            }
            else
            {
                return false;
            }

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
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void dgvPC_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            for (int i = 0; i < this.dgvPC.Rows.Count; i++)
            {
                DataGridViewRow r = this.dgvPC.Rows[i];
                r.HeaderCell.Value = (i + 1).ToString()+"-"+ Math.Ceiling((double)(i + 1)/3).ToString(); 

            }
            
        }
    }
}
