using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Weight
{
    public class DSPalletSheetlabel_multi
    {
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        /// <summary>
        /// Print PalletLabel main   一个参数是栈板号，一个是一张label上打印清单的行数，一个page 是控制打印全部还是单张label ALL或者1234...。
        /// </summary>
        /// <param name="strpalletno"></param>
        /// <returns></returns>
        public bool PrintPalletLabel(string strpalletno,int listrows, string pages)
        {
            //---------------------
            string sMessage = "";
            string strLabelName = string.Empty;
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\Shipping\Label";
            string strPalletNOSAWB = string.Empty;

            string strSAWBerrmsg = string.Empty;
            string strRegion = string.Empty;
            WeightBll wb = new WeightBll();
            wb.CheckPalletNoSAWB(strpalletno, out strRegion,out strSAWBerrmsg);
            if (strSAWBerrmsg.Equals("OK-NSAWB"))
            {
                strPalletNOSAWB = "NSAWB";
                listrows = 10;
                strLabelName = @"weight_PalletLoadingSheet";
            }
            else if (strSAWBerrmsg.Equals("OK-SAWB"))
            {
                strPalletNOSAWB = "SAWB";
                listrows = 14;
                strLabelName = @"weight_PalletLoadingSheetSAWB";
            }
            else
            {
                MessageBox.Show(strSAWBerrmsg+"检查SAWB异常。");
                return false;
            }
            
            //    一张label上所有输出的栏位,下面list要循环。
            //    PALLET_NO |PRINT_PALLET_NO | CURPAGE | TOTALPAGE | CURPALLET | TOTALPALLET | REF | DELIVERY_DATE 
            //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC  | SAWB | TOTALQTY | 

            //    | PO1 | POITEM1 | DN1 | ITEM1 | MPN1 | QTY1 | POE_COC1 | HUB_DS1 | GCCN1 | GATEWAY1 
            //    | PO2 | POITEM2 | DN2 | ITEM2 | MPN2 | QTY2 | POE_COC2 | HUB_DS2 | GCCN2 | GATEWAY2  
            //    | PO3 | POITEM3 | DN3 | ITEM3 | MPN3 | QTY3 | POE_COC3 | HUB_DS3 | GCCN3 | GATEWAY3  
            //    | PO4 | POITEM4 | DN4 | ITEM4 | MPN4 | QTY4 | POE_COC4 | HUB_DS4 | GCCN4 | GATEWAY4  
            //    | PO5 | POITEM5 | DN5 | ITEM5 | MPN5 | QTY5 | POE_COC5 | HUB_DS5 | GCCN5 | GATEWAY5  
            //    | PO6 | POITEM6 | DN6 | ITEM6 | MPN6 | QTY6 | POE_COC6 | HUB_DS6 | GCCN6 | GATEWAY6  
            //    | PO7 | POITEM7 | DN7 | ITEM7 | MPN7 | QTY7 | POE_COC7 | HUB_DS7 | GCCN7 | GATEWAY7  
            //    | PO8 | POITEM8 | DN8 | ITEM8 | MPN8 | QTY8 | POE_COC8 | HUB_DS8 | GCCN8 | GATEWAY8  
            //    | PO9 | POITEM9 | DN9 | ITEM9 | MPN9 | QTY9 | POE_COC9 | HUB_DS9 | GCCN9 | GATEWAY9  
            //    | PO10| POITEM10| DN10 | ITEM10 | MPN10 | QTY10 | POE_COC10 | HUB_DS10 | GCCN10 | GATEWAY10  
            //    | PO11| POITEM11| DN11 | ITEM11 | MPN11 | QTY11 | POE_COC11 | HUB_DS11 | GCCN11 | GATEWAY11  
            //    | PO12| POITEM12| DN12 | ITEM12 | MPN12 | QTY12 | POE_COC12 | HUB_DS12 | GCCN12 | GATEWAY12  
            //    | PO13| POITEM13| DN13 | ITEM13 | MPN13 | QTY13 | POE_COC13 | HUB_DS13 | GCCN13 | GATEWAY13  
            //    | PO14| POITEM14| DN14 | ITEM14 | MPN14 | QTY14 | POE_COC14 | HUB_DS14 | GCCN14 | GATEWAY14 |

            //CURPAGE  TOTALPAGE  一个栈板MIX多少，打印就有多少行，依据label能打印的最大行数分页，打印palletloadingsheet
            string mixTotalSelect = string.Empty;

            DataTable dt_mixTotal = new DataTable();
            dt_mixTotal = wb.GetPalletLabelDataTableBLL( strpalletno, strPalletNOSAWB, strRegion);

            if (dt_mixTotal.Rows.Count > 0)
            {
                //确定打印的label的张数palletcount， 定义个list的行数datarows，
                //mix的所有行数
                int mixTotalrow = dt_mixTotal.Rows.Count;
                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));
                //HYQ： 这部分是写.dat文件。
                string LabelParam = @"PALLET_NO|PRINT_PALLET_NO|CURPAGE|TOTALPAGE|CURPALLET|TOTALPALLET|REF|DELIVERY_DATE|CARRIER|HAWB|WEIGHT|FULL_CARTON_QTY|EMPTY_CARTON_QTY|MIX_DESC|SAWB|TOTALQTY";
                LabelParam = LabelParam + @"|PO1|POITEM1|DN1|ITEM1|MPN1|QTY1|POE_COC1|HUB_DS1|GCCN1|GATEWAY1";
                LabelParam = LabelParam + @"|PO2|POITEM2|DN2|ITEM2|MPN2|QTY2|POE_COC2|HUB_DS2|GCCN2|GATEWAY2";
                LabelParam = LabelParam + @"|PO3|POITEM3|DN3|ITEM3|MPN3|QTY3|POE_COC3|HUB_DS3|GCCN3|GATEWAY3";
                LabelParam = LabelParam + @"|PO4|POITEM4|DN4|ITEM4|MPN4|QTY4|POE_COC4|HUB_DS4|GCCN4|GATEWAY4";
                LabelParam = LabelParam + @"|PO5|POITEM5|DN5|ITEM5|MPN5|QTY5|POE_COC5|HUB_DS5|GCCN5|GATEWAY5";
                LabelParam = LabelParam + @"|PO6|POITEM6|DN6|ITEM6|MPN6|QTY6|POE_COC6|HUB_DS6|GCCN6|GATEWAY6";
                LabelParam = LabelParam + @"|PO7|POITEM7|DN7|ITEM7|MPN7|QTY7|POE_COC7|HUB_DS7|GCCN7|GATEWAY7";
                LabelParam = LabelParam + @"|PO8|POITEM8|DN8|ITEM8|MPN8|QTY8|POE_COC8|HUB_DS8|GCCN8|GATEWAY8";
                LabelParam = LabelParam + @"|PO9|POITEM9|DN9|ITEM9|MPN9|QTY9|POE_COC9|HUB_DS9|GCCN9|GATEWAY9";
                LabelParam = LabelParam + @"|PO10|POITEM10|DN10|ITEM10|MPN10|QTY10|POE_COC10|HUB_DS10|GCCN10|GATEWAY10";
                LabelParam = LabelParam + @"|PO11|POITEM11|DN11|ITEM11|MPN11|QTY11|POE_COC11|HUB_DS11|GCCN11|GATEWAY11";
                LabelParam = LabelParam + @"|PO12|POITEM12|DN12|ITEM12|MPN12|QTY12|POE_COC12|HUB_DS12|GCCN12|GATEWAY12";
                LabelParam = LabelParam + @"|PO13|POITEM13|DN13|ITEM13|MPN13|QTY13|POE_COC13|HUB_DS13|GCCN13|GATEWAY13";
                LabelParam = LabelParam + @"|PO14|POITEM14|DN14|ITEM14|MPN14|QTY14|POE_COC14|HUB_DS14|GCCN14|GATEWAY14|";
                LabelParam = LabelParam.Replace("|", @",");
                
                //label上唯一值的部分  REF 现在不定义 以后再补
                //    PALLET_NO |PRINT_PALLET_NO | CURPAGE | TOTALPAGE | CURPALLET | TOTALPALLET | REF | DELIVERY_DATE 
                //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC | SAWB | TOTALQTY | 
                string strHead = "";

                //确定这两个值
                //CURPALLET | TOTALPALLET
                string totalPalletSelect = string.Format(" select aa.*,rownum from ( "
                                                        + "select distinct  a.shipment_id, a.pallet_no "
                                                        + "from ppsuser.t_pallet_order a where a.shipment_id in ( "
                                                        + "select shipment_id  from ppsuser.t_pallet_order b "
                                                        + "where b.pallet_no = '{0}') "
                                                        + "order by pallet_no asc) aa ",strpalletno);
                DataTable dt_totalPallet = ClientUtils.ExecuteSQL(totalPalletSelect).Tables[0];

                string totalpallet = "";
                string curpallet = "";

                if (dt_totalPallet.Rows.Count > 0)
                {
                    totalpallet = dt_totalPallet.Rows.Count.ToString();
                    for (int i = 0; i < Convert.ToInt32(totalpallet); i++)
                    {
                        string dt_pallet = dt_totalPallet.Rows[i]["pallet_no"].ToString();
                        dt_pallet = dt_pallet.Trim();
                        if (dt_pallet.Equals(strpalletno))
                        {
                            //Row 是从0开始， 所以需要加1.
                            curpallet = (i+1).ToString();
                        }
                    }
                    
                }
                else
                {
                    return false;
                }
                //label上清单值的部分
                //    | PO1 | POITEM1 | DN1 | ITEM1 | MPN1 | QTY1 | POE_COC1 | HUB_DS1 | GCCN1 | GATEWAY1 
                //    | PO2 | POITEM2 | DN2 | ITEM2 | MPN2 | QTY2 | POE_COC2 | HUB_DS2 | GCCN2 | GATEWAY2  
                //    ..................................
                //    | PO14| POITEM14| DN14 | ITEM14 | MPN14 | QTY14 | POE_COC14 | HUB_DS14 | GCCN14 | GATEWAY14 |
                string strLine = "";
                
                //HYQ：这循环里面只是产生.lst文件， 如果打印5张就产生5个文档。 
                //.lst 的内容是 一行栏位名 来源于.dat文件strItemno,加换行， 再加已将数据，数据部分是由strHead + strLine。
                //HYQ：20190801如果是ALL，就产生一个文档打印，快
                string strPalletList = string.Empty;
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    string[] strHeadArr = new string[TOTALPAGE];
                    string[] strLineArr = new string[TOTALPAGE];
                    string[] strAllArr = new string[TOTALPAGE];

                    //确定这些值
                    //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC| SAWB | TOTALQTY |

                    //这里设置变量也无所谓
                    string strcarrier = "";
                    string strhawb = "";
                    string strweight = "";
                    string strfullcartonqty = "";
                    string stremptycarton = "";
                    string strmixdesc = "";
                    string strdeliverydate = "";
                    string printpalletno = "";
                    string strsawb = "";
                    string strtotalqty = "";
                    try
                    {
                        strcarrier = dt_mixTotal.Rows[i]["carrier_code"].ToString();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    printpalletno = dt_mixTotal.Rows[i]["real_pallet_no"].ToString();
                    strhawb = dt_mixTotal.Rows[i]["hawb"].ToString();
                    strsawb = dt_mixTotal.Rows[i]["hawb"].ToString();
                    strweight =Convert.ToDouble(dt_mixTotal.Rows[i]["weight"]).ToString("0.00");
                    strfullcartonqty = dt_mixTotal.Rows[i]["fullcartonqty"].ToString();
                    stremptycarton = dt_mixTotal.Rows[i]["empty_carton"].ToString();
                    strmixdesc = dt_mixTotal.Rows[i]["mix_desc"].ToString();
                    strdeliverydate = dt_mixTotal.Rows[i]["cdt"].ToString();
                    strtotalqty = dt_mixTotal.Rows[i]["totalqty"].ToString();

                    //因i从0开始，所以加1
                    string strcurpage = (i + 1).ToString();
                    
                    strHead = "";
                    strHead = strpalletno + "," +
                                    printpalletno + "," +
                                    strcurpage + "," +
                                    TOTALPAGE + "," +
                                    curpallet + "," +
                                    totalpallet + "," +
                                    "" + "," +
                                    strdeliverydate + "," +
                                    strcarrier + "," +
                                    strhawb + "," +
                                    strweight + "," +
                                    strfullcartonqty + "," +
                                    stremptycarton + "," +
                                    strmixdesc + "," +
                                    strsawb + "," +
                                    strtotalqty + ",";
                    strHeadArr[i] = strHead;

                    //确定以下的部分 循环
                    //    | PO1 | POITEM1 | DN1 | ITEM1 | MPN1 | QTY1 | POE_COC1 | HUB_DS1 | GCCN1 | GATEWAY1 
                    strLine = "";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dt_mixTotal.Rows.Count)
                        {
                            break;
                        }
                        
                        string strpo = dt_mixTotal.Rows[j]["itemcustpo"].ToString();
                        string strpoitem= dt_mixTotal.Rows[j]["itemcustpoline"].ToString();
                        string strdn = dt_mixTotal.Rows[j]["delivery_no"].ToString();
                        string stritem = dt_mixTotal.Rows[j]["line_item"].ToString();
                        if (dt_mixTotal.Rows[j]["shipmenttype"].ToString().Equals("HUB"))
                        {
                            strpo = "";
                            strpoitem = "";
                            strdn = "";
                            stritem = "";
                        }
                        string strmpn = dt_mixTotal.Rows[j]["mpn"].ToString();
                        string strqty = dt_mixTotal.Rows[j]["assign_qty"].ToString();
                        string strpoecoc = dt_mixTotal.Rows[j]["poe"].ToString();
                        string strhubdsc = dt_mixTotal.Rows[j]["shipmenttype"].ToString();

                        string strgccn = dt_mixTotal.Rows[j]["gccn"].ToString();
                        string strgateway = dt_mixTotal.Rows[j]["gateway"].ToString();
                      
                        strLine = strLine + strpo + "," + 
                                    strpoitem + "," + 
                                    strdn + "," +
                                    stritem + "," +
                                    strmpn + "," +
                                    strqty + "," +
                                    strpoecoc + "," +
                                    strhubdsc + ","+
                                    strgccn + ","+
                                    strgateway + ","
                                  ;
                    }
                    strLineArr[i] = strLine;
                    //确定这些值
                    //---------------------
                    //    PALLET_NO | CURPAGE | TOTALPAGE | CURPALLET | TOTALPALLET | REF | DELIVERY_DATE 
                    //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC 

                    strAllArr[i] = LabelParam + "\r\n" + strHeadArr[i] + strLineArr[i];


                    strPalletList = strPalletList + strHeadArr[i] + strLineArr[i] + "\r\n";

                    //HYQ： 以下3行不一定会用
                    //strHead = getPalletLabelHeadData(strpalletno);
                    //strLine = getPalletLabelLineData(strpalletno, i);
                    //strAll = LabelParam + "\r\n" + strHead + strLine;

                    string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i + ".lst";
                    if (File.Exists(str7))
                    {
                        File.Delete(str7);
                    }
                    this.WriteToPrintGo(str7, strAllArr[i]);
                }

                strPalletList = LabelParam + "\r\n" + strPalletList;
                string strPalletfile= Path.GetFullPath(strLabelPath) + @"\" + strLabelName  + "ALL.lst";
                if (File.Exists(strPalletfile))
                {
                    File.Delete(strPalletfile);
                }
                this.WriteToPrintGo(strPalletfile, strPalletList);
                
                if (!string.IsNullOrEmpty(sMessage))
                {
                    return false;
                }
                if (pages.Equals("ALL"))
                {
                    //一次打印所有的
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
                        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName +  "ALL.lst" + '"').Replace("@QTY", "1");
                        p.StartInfo.Arguments = sArguments;
                        p.Start();
                        p.WaitForExit();
                    }
                    return true;
                }
                else
                {
                    //打印 指定的第几张
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
                        using (Process p = new Process())
                        {
                            if (!File.Exists(strSampleFile))
                            {
                                sMessage = "Sample File Not exists-" + strSampleFile;
                                return false;
                            }
                            p.StartInfo.FileName = "bartend.exe";
                            string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                            sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + page.ToString() + ".lst" + '"').Replace("@QTY", "1");
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
            }
            else
            {
                return false;
            }
       }



        #region print label file done
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
                using (writer = new StreamWriter(sFile, false, Encoding.UTF8))
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

        //HYQ:这里改下增加一个参数，打印具体第几张label 打印所有的label
        private bool PrintWeight_PalletLoadingSheet(string sFileName, string sDirectory, string sInputText,string pages, int iPrintQty, out string sMessage)
        {
            try
            {
                sMessage = "OK";
                string strStartupPath = System.Windows.Forms.Application.StartupPath;
                string strSampleFile = sDirectory + @"\" + sFileName + ".btw";
                string str7 = sDirectory + @"\" + sFileName + ".lst";
                /// <summary>
                /// ////HYQ:原先这个.dat的文件可能需要线外写了，
                /// </summary>
                //string str8 = sDirectory + @"\" + sFileName + ".dat";
                string LabelParam = @"PALLET_NO|CURPAGE|TOTALPAGE|CURPALLET|TOTALPALLET|REF|DELIVERY_DATE|CARRIER|HAWB|WEIGHT|FULL_CARTON_QTY|EMPTY_CARTON_QTY|MIX_DESC|DN1|ITEM1|MPN1|QTY1|POE_COC1|HUB_DS1|DN2|ITEM2|MPN2|QTY2|POE_COC2|HUB_DS2|DN3|ITEM3|MPN3|QTY3|POE_COC3|HUB_DS3|DN4|ITEM4|MPN4|QTY4|POE_COC4|HUB_DS4|DN5|ITEM5|MPN5|QTY5|POE_COC5|HUB_DS5|DN6|ITEM6|MPN6|QTY6|POE_COC6|HUB_DS6|DN7|ITEM7|MPN7|QTY7|POE_COC7|HUB_DS7|DN8|ITEM8|MPN8|QTY8|POE_COC8|HUB_DS8|DN9|ITEM9|MPN9|QTY9|POE_COC9|HUB_DS9|DN10|ITEM10|MPN10|QTY10|POE_COC10|HUB_DS10|";

                string str8 = sDirectory + @"\" + sFileName + ".dat";

                //HYQ：重新写这个文件的内容.WriteToPrintGo借用这个方法，将label打印的欄位清单LabelParam写进str8文件。
                this.WriteToPrintGo(str8, LabelParam);


                string sFile = strStartupPath + @"\PrintGo.bat";
                string str11 = strStartupPath + @"\PrintLabel.bat";
                string sData = this.LoadBatFile(str11, ref sMessage);
                string str9 = string.Empty;
                if (!File.Exists(strSampleFile))
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
                string path = strSampleFile;
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
        public bool PrintPalletLabel20190826bk(string strpalletno, int listrows, string pages)
        {
            //---------------------
            string sMessage = "";
            //string strMessage = string.Empty;
            string strLabelName = string.Empty;
            //string strLabelPath = @"D:\MES_CLIENT\Shipping\Label";
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath + @"\Shipping\Label";
            //string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
            //if (!File.Exists(strSampleFile))
            //{
            //    sMessage = "Sample File Not exists-" + strSampleFile;
            //    return false;
            //}
            ///20190508 增加SAWB 逻辑
            ///
            string strPalletNOSAWB = string.Empty;

            string strSAWBerrmsg = string.Empty;
            WeightBll pb1 = new WeightBll();
            pb1.CheckPalletNoSAWB(strpalletno, out strSAWBerrmsg);
            if (strSAWBerrmsg.Equals("OK-NSAWB"))
            {
                strPalletNOSAWB = "NSAWB";
                listrows = 10;
                strLabelName = @"weight_PalletLoadingSheet";
            }
            else if (strSAWBerrmsg.Equals("OK-SAWB"))
            {
                strPalletNOSAWB = "SAWB";
                listrows = 14;
                strLabelName = @"weight_PalletLoadingSheetSAWB";
            }
            else
            {
                MessageBox.Show(strSAWBerrmsg + "检查SAWB异常。");
                return false;
            }


            //栈板对应的SSCC 是 108859095262313969  108 85909 526231396 9  最后一位是检查码.
            //ADD:调整到更新t_shipment_pallet 重量时，一起产生 写入到此表

            //    一张label上所有输出的栏位,下面list要循环。
            //    PALLET_NO |PRINT_PALLET_NO | CURPAGE | TOTALPAGE | CURPALLET | TOTALPALLET | REF | DELIVERY_DATE 
            //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC  | SAWB | TOTALQTY | 

            //    | PO1 | POITEM1 | DN1 | ITEM1 | MPN1 | QTY1 | POE_COC1 | HUB_DS1 | GCCN1 | GATEWAY1 
            //    | PO2 | POITEM2 | DN2 | ITEM2 | MPN2 | QTY2 | POE_COC2 | HUB_DS2 | GCCN2 | GATEWAY2  
            //    | PO3 | POITEM3 | DN3 | ITEM3 | MPN3 | QTY3 | POE_COC3 | HUB_DS3 | GCCN3 | GATEWAY3  
            //    | PO4 | POITEM4 | DN4 | ITEM4 | MPN4 | QTY4 | POE_COC4 | HUB_DS4 | GCCN4 | GATEWAY4  
            //    | PO5 | POITEM5 | DN5 | ITEM5 | MPN5 | QTY5 | POE_COC5 | HUB_DS5 | GCCN5 | GATEWAY5  
            //    | PO6 | POITEM6 | DN6 | ITEM6 | MPN6 | QTY6 | POE_COC6 | HUB_DS6 | GCCN6 | GATEWAY6  
            //    | PO7 | POITEM7 | DN7 | ITEM7 | MPN7 | QTY7 | POE_COC7 | HUB_DS7 | GCCN7 | GATEWAY7  
            //    | PO8 | POITEM8 | DN8 | ITEM8 | MPN8 | QTY8 | POE_COC8 | HUB_DS8 | GCCN8 | GATEWAY8  
            //    | PO9 | POITEM9 | DN9 | ITEM9 | MPN9 | QTY9 | POE_COC9 | HUB_DS9 | GCCN9 | GATEWAY9  
            //    | PO10| POITEM10| DN10 | ITEM10 | MPN10 | QTY10 | POE_COC10 | HUB_DS10 | GCCN10 | GATEWAY10  
            //    | PO11| POITEM11| DN11 | ITEM11 | MPN11 | QTY11 | POE_COC11 | HUB_DS11 | GCCN11 | GATEWAY11  
            //    | PO12| POITEM12| DN12 | ITEM12 | MPN12 | QTY12 | POE_COC12 | HUB_DS12 | GCCN12 | GATEWAY12  
            //    | PO13| POITEM13| DN13 | ITEM13 | MPN13 | QTY13 | POE_COC13 | HUB_DS13 | GCCN13 | GATEWAY13  
            //    | PO14| POITEM14| DN14 | ITEM14 | MPN14 | QTY14 | POE_COC14 | HUB_DS14 | GCCN14 | GATEWAY14 |

            //CURPAGE  TOTALPAGE  一个栈板MIX多少，打印就有多少行，依据label能打印的最大行数分页，打印palletloadingsheet
            //SQL 语句最后一个需要加一个空格
            string mixTotalSelect = string.Empty;
            if (strPalletNOSAWB.Equals("NSAWB"))
            {
                mixTotalSelect = string.Format("select DISTINCT b.pallet_no, "
                                    + "            b.real_pallet_no, "
                                    + "                case "
                                    + "                  when a.shipment_type = 'DS' then "
                                    + "                    a.carrier_code"
                                    + "                else "
                                    + "            (SELECT distinct SCACCODE "
                                    + "         FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D "
                                    + "        where trim(D.carriercode) = a.carrier_code "
                                    + "          and D.ShipMode = a.transport "
                                    + "          and D.isdisabled = '0' "
                                    + "          and D.type = 'HAWB') end carrier_code, "
                                    + "            a.hawb, "
                                    + "            b.weight, "
                                    + "            b.empty_carton + b.carton_qty fullcartonqty, "
                                    + "            b.empty_carton, "
                                    + "            b.qty totalqty, "
                                    + "            to_char(a.shipping_time, 'dd/mm/yyyy') cdt, "
                                    + "            ppsuser.t_pallet_bottomdesc(b.pallet_no) mix_desc, "
                                    + "            c.shipment_id, "
                                    + "            e.itemcustpo itemcustpo, "
                                    + "            e.itemcustpoline itemcustpoline, "
                                    + "            c.delivery_no delivery_no, "
                                    + "            c.line_item line_item, "
                                    + "            c.mpn,c.ictpn, "
                                    + "            c.assign_qty, "
                                    + "           case when a.shipment_type='DS' and  a.region='EMEIA' and a.poe='SA' then "
                                    + "                      e.PORTOFENTRY "
                                    + "                      else  a.poe end poe,'' gateway,'' gccn ,"
                                    + "            decode(a.shipment_type,'FD','HUB',a.shipment_type) shipmenttype "
                                 + " from ppsuser.t_shipment_info a "
                                 + " join ppsuser.t_shipment_pallet b "
                                 + "   on a.shipment_id = b.shipment_id "
                                 + " join ppsuser.t_pallet_order c "
                                 + "   on b.pallet_no = c.pallet_no "
                                 + " join(select * "
                                 + "         from(select pallet_no, weight, cdt "
                                 + "                 from ppsuser.t_pallet_weight_log "
                                 + "                where pallet_no = '{0}' "
                                 + "                  AND PASS = '1' "
                                 + "                order by cdt desc) "
                                 + "        where rownum = 1) d "
                                 + "   on c.pallet_no = d.pallet_no "
                                 + " left join (select  decode(itemcustpo,'',weborderno,null,weborderno,itemcustpo ) itemcustpo,itemcustpoline,PORTOFENTRY,deliveryno,custdelitem,SHIPPLANT  from ppsuser.t_940_unicode) e "
                                 + "   on c.delivery_no = e.deliveryno "
                                 + "  and c.line_item = e.custdelitem "
                                 + " where b.pallet_no = '{1}' "
                                 + " ORDER BY B.PALLET_NO ASC ", strpalletno, strpalletno);

                //20190716修改待定
                //mixTotalSelect = string.Format(@"select pallet_no,real_pallet_no, carrier_code,hawb,weight,
                //                               fullcartonqty, empty_carton,totalqty,cdt,mix_desc,shipment_id,
                //                               itemcustpo,itemcustpoline,delivery_no, line_item,
                //                               mpn,ictpn,poe,gateway,gccn,shipmenttype,
                //                               sum(assign_qty) assign_qty
                //                          from (select  b.pallet_no,
                //                                                b.real_pallet_no,
                //                                                case
                //                                                  when a.shipment_type = 'DS' then
                //                                                   a.carrier_code
                //                                                  else
                //                                                   (SELECT distinct SCACCODE
                //                                                      FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D
                //                                                     where trim(D.carriercode) = a.carrier_code
                //                                                       and D.ShipMode = a.transport
                //                                                       and D.isdisabled = '0'
                //                                                       and D.type = 'HAWB')
                //                                                end carrier_code,
                //                                                a.hawb,
                //                                                b.weight,
                //                                                b.empty_carton + b.carton_qty fullcartonqty,
                //                                                b.empty_carton,
                //                                                b.qty totalqty,
                //                                                to_char(a.shipping_time, 'dd/mm/yyyy') cdt,
                //                                                case
                //                                                  when a.region = 'EMEIA' and
                //                                                       UPPER(e.SHIPPLANT) LIKE 'MIT%' then
                //                                                   'MIT'
                //                                                  when pallet_type = '001' then
                //                                                   'DO NOT BREAK BULK'
                //                                                  when pallet_type = '999' then
                //                                                   'CONSOLIDATED'
                //                                                end mix_desc,
                //                                                c.shipment_id,
                //                                                decode(a.shipment_type, 'DS', '', e.itemcustpo) itemcustpo,
                //                                                decode(a.shipment_type, 'DS', '', e.itemcustpoline) itemcustpoline,
                //                                                decode(a.shipment_type, 'DS', '', c.delivery_no) delivery_no,
                //                                                decode(a.shipment_type, 'DS', '', c.line_item) line_item,
                //                                                c.mpn,
                //                                                c.ictpn,
                //                                                c.assign_qty,
                //                                                case
                //                                                  when a.shipment_type = 'DS' and a.region = 'EMEIA' and
                //                                                       a.poe = 'SA' then
                //                                                   e.PORTOFENTRY
                //                                                  else
                //                                                   a.poe
                //                                                end poe,
                //                                                '' gateway,
                //                                                '' gccn,
                //                                                decode(a.shipment_type, 'FD', 'HUB', a.shipment_type) shipmenttype
                //                                  from ppsuser.t_shipment_info a
                //                                  join ppsuser.t_shipment_pallet b
                //                                    on a.shipment_id = b.shipment_id
                //                                  join ppsuser.t_pallet_order c
                //                                    on b.pallet_no = c.pallet_no
                //                                  join (select *
                //                                         from (select pallet_no, weight, cdt
                //                                                 from ppsuser.t_pallet_weight_log
                //                                                where pallet_no = '{0}'
                //                                                  AND PASS = '1'
                //                                                order by cdt desc)
                //                                        where rownum = 1) d
                //                                    on c.pallet_no = d.pallet_no
                //                                  left join (select decode(itemcustpo,
                //                                                          '',
                //                                                          weborderno,
                //                                                          null,
                //                                                          weborderno,
                //                                                          itemcustpo) itemcustpo,
                //                                                   itemcustpoline,
                //                                                   PORTOFENTRY,
                //                                                   deliveryno,
                //                                                   custdelitem,
                //                                                   SHIPPLANT
                //                                              from ppsuser.t_940_unicode) e
                //                                    on c.delivery_no = e.deliveryno
                //                                   and c.line_item = e.custdelitem
                //                                 where b.pallet_no = '{1}') aa
                //                         group by pallet_no,real_pallet_no, carrier_code,hawb,weight,
                //                               fullcartonqty, empty_carton,totalqty,cdt,mix_desc,shipment_id,
                //                               itemcustpo,itemcustpoline,delivery_no, line_item,
                //                               mpn,ictpn,poe,gateway,gccn,shipmenttype ", strpalletno, strpalletno);

            }
            else  //SAWB
            {
                mixTotalSelect = string.Format("select DISTINCT bb.pallet_no,"
                               + "                  bb.real_pallet_no, "
                                 + "                case "
                                   + "                  when aa.shipment_type = 'DS' then "
                                   + "                    aa.carrier_code"
                                   + "                else "
                                   + "            (SELECT distinct SCACCODE "
                                   + "         FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D "
                                   + "        where trim(D.carriercode) = aa.carrier_code "
                                   + "          and D.ShipMode = aa.transport "
                                   + "          and D.isdisabled = '0' "
                                   + "          and D.type = 'HAWB') end carrier_code, "
                               + "                  aa.hawb,aa.poe gateway ,"
                               + "                  bb.weight, "
                               + "                  bb.empty_carton + bb.carton_qty fullcartonqty, "
                               + "                  bb.empty_carton, "
                               + "                  bb.qty totalqty, "
                               + "                  to_char(aa.shipping_time, 'dd/mm/yyyy') cdt, "
                               + "                  ppsuser.t_pallet_bottomdesc(bb.pallet_no) mix_desc, "
                               + "                  aa.shipment_id, "
                               + "                  '' itemcustpo, "
                               + "                  '' itemcustpoline, "
                               + "                  '' delivery_no, "
                               + "                  '' line_item, "
                               + "                  decode(aa.shipment_type, 'FD', 'HUB', aa.shipment_type) shipmenttype, "
                               + "                  cc.mpn,cc.ictpn, "
                               + "                  cc.assign_qty, "
                               + "                  cc.gccn, "
                               + "                  cc.poe "
                               + "    from ppsuser.t_shipment_info aa "
                               + "    join ppsuser.t_shipment_pallet bb "
                               + "      on aa.shipment_id = bb.shipment_id "
                               + "    join(select a.pallet_no, "
                               + "                 b.hawb as gccn, "
                               + "                 b.mpn, "
                               + "                 b.poe,b.ictpn ,"
                               + "                 sum(a.assign_qty) as assign_qty "
                               + "            from ppsuser.t_pallet_order a "
                               + "            join ppsuser.t_order_info b "
                               + "         on a.delivery_no = b.delivery_no "
                               + "        and a.line_item = b.line_item "
                               + "             and a.ictpn = b.ictpn "
                               + "            join ppsuser.t_shipment_sawb c "
                               + "              on b.shipment_id = c.shipment_id "
                               + "             and a.shipment_id = c.sawb_shipment_id "
                               + "           where a.pallet_no = '{0}' "
                               + "           group by a.pallet_no, b.hawb, b.mpn, b.poe ,b.ictpn) cc "
                               + "      on bb.pallet_no = cc.pallet_no "
                               + "    join(select * "
                               + "            from(select pallet_no, weight, cdt "
                               + "                    from ppsuser.t_pallet_weight_log "
                               + "                  where pallet_no = '{1}' "
                               + "                     AND PASS = '1' "
                               + "                   order by cdt desc) "
                               + "           where rownum = 1) dd "
                               + "      on bb.pallet_no = dd.pallet_no "
                               + "    join ppsuser.t_pallet_order ee "
                               + "      on dd.pallet_no = ee.pallet_no "
                               + "    left join ppsuser.t_940_unicode ff "
                               + "      on ee.delivery_no = ff.deliveryno "
                               + "     and ee.line_item = ff.custdelitem ", strpalletno, strpalletno);

                //mixTotalSelect = string.Format("select DISTINCT bb.pallet_no,"
                //                + "                  bb.real_pallet_no, "
                //                  + "                case "
                //                    + "                  when aa.shipment_type = 'DS' then "
                //                    + "                    aa.carrier_code"
                //                    + "                else "
                //                    + "            (SELECT distinct SCACCODE "
                //                    + "         FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D "
                //                    + "        where trim(D.carriercode) = aa.carrier_code "
                //                    + "          and D.ShipMode = aa.transport "
                //                    + "          and D.isdisabled = '0' "
                //                    + "          and D.type = 'HAWB') end carrier_code, "
                //                + "                  aa.hawb,aa.poe gateway ,"
                //                + "                  bb.weight, "
                //                + "                  bb.empty_carton + bb.carton_qty fullcartonqty, "
                //                + "                  bb.empty_carton, "
                //                + "                  bb.qty totalqty, "
                //                + "                  to_char(aa.shipping_time, 'dd/mm/yyyy') cdt, "
                //                + "                  case "
                //                + "                    when aa.region = 'EMEIA' and "
                //                + "                         UPPER(ff.SHIPPLANT) LIKE 'MIT%' then "
                //                + "                     'MIT' "
                //                + "                    when pallet_type = '001' then "
                //                + "                     'DO NOT BREAK BULK' "
                //                + "                    when pallet_type = '999' then "
                //                + "                     'CONSOLIDATED' "
                //                + "                  end mix_desc, "
                //                + "                  aa.shipment_id, "
                //                + "                  '' itemcustpo, "
                //                + "                  '' itemcustpoline, "
                //                + "                  '' delivery_no, "
                //                + "                  '' line_item, "
                //                + "                  decode(aa.shipment_type, 'FD', 'HUB', aa.shipment_type) shipmenttype, "
                //                + "                  cc.mpn,cc.ictpn, "
                //                + "                  cc.assign_qty, "
                //                + "                  cc.gccn, "
                //                + "                  cc.poe "
                //                + "    from ppsuser.t_shipment_info aa "
                //                + "    join ppsuser.t_shipment_pallet bb "
                //                + "      on aa.shipment_id = bb.shipment_id "
                //                + "    join(select a.pallet_no, "
                //                + "                 b.hawb as gccn, "
                //                + "                 b.mpn, "
                //                + "                 b.poe,b.ictpn ,"
                //                + "                 sum(a.assign_qty) as assign_qty "
                //                + "            from ppsuser.t_pallet_order a "
                //                + "            join ppsuser.t_order_info b "
                //                + "         on a.delivery_no = b.delivery_no "
                //                + "        and a.line_item = b.line_item "
                //                + "             and a.ictpn = b.ictpn "
                //                + "            join ppsuser.t_shipment_sawb c "
                //                + "              on b.shipment_id = c.shipment_id "
                //                + "             and a.shipment_id = c.sawb_shipment_id "
                //                + "           where a.pallet_no = '{0}' "
                //                + "           group by a.pallet_no, b.hawb, b.mpn, b.poe ,b.ictpn) cc "
                //                + "      on bb.pallet_no = cc.pallet_no "
                //                + "    join(select * "
                //                + "            from(select pallet_no, weight, cdt "
                //                + "                    from ppsuser.t_pallet_weight_log "
                //                + "                  where pallet_no = '{1}' "
                //                + "                     AND PASS = '1' "
                //                + "                   order by cdt desc) "
                //                + "           where rownum = 1) dd "
                //                + "      on bb.pallet_no = dd.pallet_no "
                //                + "    join ppsuser.t_pallet_order ee "
                //                + "      on dd.pallet_no = ee.pallet_no "
                //                + "    left join ppsuser.t_940_unicode ff "
                //                + "      on ee.delivery_no = ff.deliveryno "
                //                + "     and ee.line_item = ff.custdelitem ", strpalletno, strpalletno);
            }

            DataTable dt_mixTotal = new DataTable();
            try
            {

                dt_mixTotal = ClientUtils.ExecuteSQL(mixTotalSelect).Tables[0];

            }
            catch (Exception e)
            { MessageBox.Show(e.ToString()); }

            if (dt_mixTotal.Rows.Count > 0)
            {

                //确定打印的label的张数palletcount， 定义个list的行数datarows，
                //mix的所有行数
                int mixTotalrow = dt_mixTotal.Rows.Count;

                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));



                //HYQ： 这部分是写.dat文件。
                string LabelParam = @"PALLET_NO|PRINT_PALLET_NO|CURPAGE|TOTALPAGE|CURPALLET|TOTALPALLET|REF|DELIVERY_DATE|CARRIER|HAWB|WEIGHT|FULL_CARTON_QTY|EMPTY_CARTON_QTY|MIX_DESC|SAWB|TOTALQTY";
                LabelParam = LabelParam + @"|PO1|POITEM1|DN1|ITEM1|MPN1|QTY1|POE_COC1|HUB_DS1|GCCN1|GATEWAY1";
                LabelParam = LabelParam + @"|PO2|POITEM2|DN2|ITEM2|MPN2|QTY2|POE_COC2|HUB_DS2|GCCN2|GATEWAY2";
                LabelParam = LabelParam + @"|PO3|POITEM3|DN3|ITEM3|MPN3|QTY3|POE_COC3|HUB_DS3|GCCN3|GATEWAY3";
                LabelParam = LabelParam + @"|PO4|POITEM4|DN4|ITEM4|MPN4|QTY4|POE_COC4|HUB_DS4|GCCN4|GATEWAY4";
                LabelParam = LabelParam + @"|PO5|POITEM5|DN5|ITEM5|MPN5|QTY5|POE_COC5|HUB_DS5|GCCN5|GATEWAY5";
                LabelParam = LabelParam + @"|PO6|POITEM6|DN6|ITEM6|MPN6|QTY6|POE_COC6|HUB_DS6|GCCN6|GATEWAY6";
                LabelParam = LabelParam + @"|PO7|POITEM7|DN7|ITEM7|MPN7|QTY7|POE_COC7|HUB_DS7|GCCN7|GATEWAY7";
                LabelParam = LabelParam + @"|PO8|POITEM8|DN8|ITEM8|MPN8|QTY8|POE_COC8|HUB_DS8|GCCN8|GATEWAY8";
                LabelParam = LabelParam + @"|PO9|POITEM9|DN9|ITEM9|MPN9|QTY9|POE_COC9|HUB_DS9|GCCN9|GATEWAY9";
                LabelParam = LabelParam + @"|PO10|POITEM10|DN10|ITEM10|MPN10|QTY10|POE_COC10|HUB_DS10||GCCN10|GATEWAY10";
                LabelParam = LabelParam + @"|PO11|POITEM11|DN11|ITEM11|MPN11|QTY11|POE_COC11|HUB_DS11||GCCN11|GATEWAY11";
                LabelParam = LabelParam + @"|PO12|POITEM12|DN12|ITEM12|MPN12|QTY12|POE_COC12|HUB_DS12||GCCN12|GATEWAY12";
                LabelParam = LabelParam + @"|PO13|POITEM13|DN13|ITEM13|MPN13|QTY13|POE_COC13|HUB_DS13||GCCN13|GATEWAY13";
                LabelParam = LabelParam + @"|PO14|POITEM14|DN14|ITEM14|MPN14|QTY14|POE_COC14|HUB_DS14||GCCN14|GATEWAY14|";
                LabelParam = LabelParam.Replace("|", @",");


                ///--------------------------------

                //label上唯一值的部分  REF 现在不定义 以后再补
                //    PALLET_NO |PRINT_PALLET_NO | CURPAGE | TOTALPAGE | CURPALLET | TOTALPALLET | REF | DELIVERY_DATE 
                //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC | SAWB | TOTALQTY | 
                string strHead = "";

                //确定这两个值
                //CURPALLET | TOTALPALLET
                string totalPalletSelect = string.Format(" select aa.*,rownum from ( "
                                                        + "select distinct  a.shipment_id, a.pallet_no "
                                                        + "from ppsuser.t_pallet_order a where a.shipment_id in ( "
                                                        + "select shipment_id  from ppsuser.t_pallet_order b "
                                                        + "where b.pallet_no = '{0}') "
                                                        + "order by pallet_no asc) aa ", strpalletno);
                DataTable dt_totalPallet = ClientUtils.ExecuteSQL(totalPalletSelect).Tables[0];

                string totalpallet = "";
                string curpallet = "";

                if (dt_totalPallet.Rows.Count > 0)
                {
                    totalpallet = dt_totalPallet.Rows.Count.ToString();
                    for (int i = 0; i < Convert.ToInt32(totalpallet); i++)
                    {
                        string dt_pallet = dt_totalPallet.Rows[i]["pallet_no"].ToString();
                        dt_pallet = dt_pallet.Trim();
                        if (dt_pallet.Equals(strpalletno))
                        {
                            //Row 是从0开始， 所以需要加1.
                            curpallet = (i + 1).ToString();
                        }
                    }

                }
                else
                {
                    return false;
                }

                //label上清单值的部分
                //    | PO1 | POITEM1 | DN1 | ITEM1 | MPN1 | QTY1 | POE_COC1 | HUB_DS1 | GCCN1 | GATEWAY1 
                //    | PO2 | POITEM2 | DN2 | ITEM2 | MPN2 | QTY2 | POE_COC2 | HUB_DS2 | GCCN2 | GATEWAY2  
                //    ..................................
                //    | PO14| POITEM14| DN14 | ITEM14 | MPN14 | QTY14 | POE_COC14 | HUB_DS14 | GCCN14 | GATEWAY14 |
                string strLine = "";

                //string strAll = "";

                //HYQ：这循环里面只是产生.lst文件， 如果打印5张就产生5个文档。 
                //.lst 的内容是 一行栏位名 来源于.dat文件strItemno,加换行， 再加已将数据，数据部分是由strHead + strLine。
                //HYQ：20190801如果是ALL，就产生一个文档打印，快
                string strPalletList = string.Empty;
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    string[] strHeadArr = new string[TOTALPAGE];
                    string[] strLineArr = new string[TOTALPAGE];
                    string[] strAllArr = new string[TOTALPAGE];

                    //确定这些值
                    //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC| SAWB | TOTALQTY |

                    //这里设置变量也无所谓


                    string strcarrier = "";
                    string strhawb = "";
                    string strweight = "";
                    string strfullcartonqty = "";
                    string stremptycarton = "";
                    string strmixdesc = "";
                    string strdeliverydate = "";
                    string printpalletno = "";
                    string strsawb = "";
                    string strtotalqty = "";
                    try
                    {
                        strcarrier = dt_mixTotal.Rows[i]["carrier_code"].ToString();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    printpalletno = dt_mixTotal.Rows[i]["real_pallet_no"].ToString();
                    strhawb = dt_mixTotal.Rows[i]["hawb"].ToString();
                    strsawb = dt_mixTotal.Rows[i]["hawb"].ToString();
                    strweight = Convert.ToDouble(dt_mixTotal.Rows[i]["weight"]).ToString("0.00");
                    strfullcartonqty = dt_mixTotal.Rows[i]["fullcartonqty"].ToString();
                    stremptycarton = dt_mixTotal.Rows[i]["empty_carton"].ToString();
                    strmixdesc = dt_mixTotal.Rows[i]["mix_desc"].ToString();
                    strdeliverydate = dt_mixTotal.Rows[i]["cdt"].ToString();
                    strtotalqty = dt_mixTotal.Rows[i]["totalqty"].ToString();

                    //因i从0开始，所以加1
                    string strcurpage = (i + 1).ToString();

                    //strHead = strpalletno + "|" +
                    //                printpalletno + "|" +
                    //                strcurpage + "|" +
                    //                TOTALPAGE + "|" +
                    //                curpallet + "|" +
                    //                totalpallet + "|" +
                    //                "" + "|" +
                    //                strdeliverydate + "|" +
                    //                strcarrier + "|" +
                    //                strhawb + "|" +
                    //                strweight + "|" +
                    //                strfullcartonqty + "|" +
                    //                stremptycarton + "|" +
                    //                strmixdesc + "|";
                    strHead = "";
                    strHead = strpalletno + "," +
                                    printpalletno + "," +
                                    strcurpage + "," +
                                    TOTALPAGE + "," +
                                    curpallet + "," +
                                    totalpallet + "," +
                                    "" + "," +
                                    strdeliverydate + "," +
                                    strcarrier + "," +
                                    strhawb + "," +
                                    strweight + "," +
                                    strfullcartonqty + "," +
                                    stremptycarton + "," +
                                    strmixdesc + "," +
                                    strsawb + "," +
                                    strtotalqty + ",";
                    strHeadArr[i] = strHead;

                    //确定以下的部分 循环
                    // | DN1 | ITEM1 | MPN1 | QTY1 | POE_COC1 | HUB_DS1
                    //c.shipment_id,c.line_item,c.mpn ,c.assign_qty ,a.poe ,a.shipment_type
                    strLine = "";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dt_mixTotal.Rows.Count)
                        {
                            break;
                        }

                        string strpo = dt_mixTotal.Rows[j]["itemcustpo"].ToString();
                        string strpoitem = dt_mixTotal.Rows[j]["itemcustpoline"].ToString();
                        string strdn = dt_mixTotal.Rows[j]["delivery_no"].ToString();
                        string stritem = dt_mixTotal.Rows[j]["line_item"].ToString();
                        if (dt_mixTotal.Rows[j]["shipmenttype"].ToString().Equals("HUB"))
                        {
                            strpo = "";
                            strpoitem = "";
                            strdn = "";
                            stritem = "";
                        }
                        string strmpn = dt_mixTotal.Rows[j]["mpn"].ToString();
                        string strqty = dt_mixTotal.Rows[j]["assign_qty"].ToString();
                        string strpoecoc = dt_mixTotal.Rows[j]["poe"].ToString();
                        string strhubdsc = dt_mixTotal.Rows[j]["shipmenttype"].ToString();

                        string strgccn = dt_mixTotal.Rows[j]["gccn"].ToString();
                        string strgateway = dt_mixTotal.Rows[j]["gateway"].ToString();
                        //strLine = strLine + strdn + "|" +
                        //            stritem + "|" +
                        //            strmpn + "|" +
                        //            strqty + "|" +
                        //            strpoecoc + "|" +
                        //            strhubdsc + "|";
                        strLine = strLine + strpo + "," +
                                    strpoitem + "," +
                                    strdn + "," +
                                    stritem + "," +
                                    strmpn + "," +
                                    strqty + "," +
                                    strpoecoc + "," +
                                    strhubdsc + "," +
                                    strgccn + "," +
                                    strgateway + ","
                                  ;
                    }
                    strLineArr[i] = strLine;


                    //确定这些值
                    //    PALLET_NO | CURPAGE | TOTALPAGE | CURPALLET | TOTALPALLET | REF | DELIVERY_DATE 
                    //--------------------写这些内容


                    //---------------------
                    //    PALLET_NO | CURPAGE | TOTALPAGE | CURPALLET | TOTALPALLET | REF | DELIVERY_DATE 
                    //    | CARRIER | HAWB | WEIGHT | FULL_CARTON_QTY | EMPTY_CARTON_QTY | MIX_DESC 

                    strAllArr[i] = LabelParam + "\r\n" + strHeadArr[i] + strLineArr[i];


                    strPalletList = strPalletList + strHeadArr[i] + strLineArr[i] + "\r\n";

                    //HYQ： 以下3行不一定会用
                    //strHead = getPalletLabelHeadData(strpalletno);
                    //strLine = getPalletLabelLineData(strpalletno, i);
                    //strAll = LabelParam + "\r\n" + strHead + strLine;

                    string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i + ".lst";
                    if (File.Exists(str7))
                    {
                        File.Delete(str7);
                    }
                    this.WriteToPrintGo(str7, strAllArr[i]);
                }

                strPalletList = LabelParam + "\r\n" + strPalletList;
                string strPalletfile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + "ALL.lst";
                if (File.Exists(strPalletfile))
                {
                    File.Delete(strPalletfile);
                }
                this.WriteToPrintGo(strPalletfile, strPalletList);


                //string sFile = strStartupPath + @"\PrintGo.bat";
                //string str11 = strStartupPath + @"\PrintLabel.bat";

                //string sData = this.LoadBatFile(str11, ref sMessage);

                //string sData = string.Empty;

                if (!string.IsNullOrEmpty(sMessage))
                {
                    return false;
                }

                if (pages.Equals("ALL"))
                {
                    //一次打印所有的
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
                        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + "ALL.lst" + '"').Replace("@QTY", "1");
                        p.StartInfo.Arguments = sArguments;
                        p.Start();
                        p.WaitForExit();
                    }


                    //循环打印所有label，
                    //for (int i = 0; i < TOTALPAGE; i++)
                    //{
                    //    using (Process p = new Process())
                    //    {
                    //        string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                    //        if (!File.Exists(strSampleFile))
                    //        {
                    //            sMessage = "Sample File Not exists-" + strSampleFile;
                    //            return false;
                    //        }
                    //        p.StartInfo.FileName = "bartend.exe";
                    //        string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    //        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i.ToString() + ".lst" + '"').Replace("@QTY", "1");
                    //        p.StartInfo.Arguments = sArguments;
                    //        p.Start();
                    //        p.WaitForExit();
                    //    }


                    //    //using (Process p = new Process())
                    //    //{
                    //    //    string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                    //    //    if (!File.Exists(strSampleFile))
                    //    //    {
                    //    //        sMessage = "Sample File Not exists-" + strSampleFile;
                    //    //        return false;
                    //    //    }

                    //    //    string sData = @"""C:\Program Files (x86)\Seagull\BarTender Suite\bartend.exe"" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                    //    //    sData = sData.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i.ToString() + ".lst" + '"').Replace("@QTY", "1");
                    //    //    //if ((i == TOTALPAGE) && i>1)
                    //    //    //{
                    //    //    //    sData = sData.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + (i-1).ToString() + ".lst" + '"').Replace("@QTY", "1");
                    //    //    //}

                    //    //    //HYQ：20181120  这地方要改， 需改为执行命令
                    //    //    //this.WriteToPrintGo(sFile, sData);
                    //    //    //int num4 = WinExec(sFile, 0);
                    //    //    //System.Threading.Thread.Sleep(1 * 1000);

                    //    //    p.StartInfo.FileName = "cmd.exe";
                    //    //    p.StartInfo.UseShellExecute = false;
                    //    //    p.StartInfo.RedirectStandardInput = true;
                    //    //    p.StartInfo.RedirectStandardOutput = true;
                    //    //    p.StartInfo.CreateNoWindow = true;
                    //    //    //MessageBox.Show(i.ToString());
                    //    //    p.Start();
                    //    //    p.StandardInput.WriteLine(sData + "&exit");
                    //    //    p.StandardInput.AutoFlush = true;
                    //    //    p.StandardInput.Close();
                    //    //    p.WaitForExit();

                    //    //    //System.Threading.Thread.Sleep(2 * 1000);

                    //    //}
                    //}
                    return true;
                }
                else
                {
                    //打印 指定的第几张
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
                        using (Process p = new Process())
                        {
                            //string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                            if (!File.Exists(strSampleFile))
                            {
                                sMessage = "Sample File Not exists-" + strSampleFile;
                                return false;
                            }
                            p.StartInfo.FileName = "bartend.exe";
                            string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                            sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + page.ToString() + ".lst" + '"').Replace("@QTY", "1");
                            p.StartInfo.Arguments = sArguments;
                            p.Start();
                            p.WaitForExit();
                        }


                        //string sData = @"""C:\Program Files (x86)\Seagull\BarTender Suite\bartend.exe"" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        //sData = sData.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + page + ".lst" + '"').Replace("@QTY", "1");
                        ////this.WriteToPrintGo(sFile, sData);
                        ////int num4 = WinExec(sFile, 0);

                        //using (Process p = new Process())
                        //{
                        //    p.StartInfo.FileName = "cmd.exe";
                        //    p.StartInfo.UseShellExecute = false;
                        //    p.StartInfo.RedirectStandardInput = true;
                        //    p.StartInfo.RedirectStandardOutput = true;
                        //    p.StartInfo.CreateNoWindow = true;
                        //    p.Start();
                        //    p.StandardInput.WriteLine(sData + "&exit");
                        //    p.WaitForExit();

                        //    //Process p = new Process();
                        //    //p.StartInfo.FileName = "cmd.exe";
                        //    //p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
                        //    //p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                        //    //p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                        //    //p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                        //    //p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                        //    //                                  //p.StartInfo.Arguments = strCMD;
                        //    //p.Start();//启动程序

                        //    ////向cmd窗口发送输入信息
                        //    //p.StandardInput.WriteLine(strCMD + "&exit");

                        //    //p.StandardInput.AutoFlush = true;
                        //    //p.StandardInput.Close();
                        //    //获取cmd窗口的输出信息
                        //    //**string output = p.StandardError.ReadToEnd(); **
                        //     //等待程序执行完退出进程
                        //     //p.WaitForExit();
                        //    //p.Close();

                        //}


                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                //HYQ：这里的pages 是来源于PrintPalletLabel的参数； 原先的stralld可能就不需要
                //PrintWeight_PalletLoadingSheet(strLabelName, Path.GetFullPath(strLabelPath), strAll, pages, 1, out strMessage);


                //if (strMessage == "OK")
                //{
                //    System.Threading.Thread.Sleep(3 * 1000);

                //    return true;
                //}
                //else
                //{
                //    return false;
                //}

            }
            else
            {
                return false;
            }

        }

        #endregion
    }
}
