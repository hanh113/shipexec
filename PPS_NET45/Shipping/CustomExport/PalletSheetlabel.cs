using ClientUtilsDll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CustomExport
{
    public class PalletSheetlabel
    {
        public bool PrintPalletLabel(string strpalletno, int listrows, string pages)
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
            wb.CheckPalletNoSAWB(strpalletno, out strRegion, out strSAWBerrmsg);
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
                MessageBox.Show(strSAWBerrmsg + "检查SAWB异常。".TP());
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
            dt_mixTotal = wb.GetPalletLabelDataTableBLL(strpalletno, strPalletNOSAWB, strRegion);

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
    }
}
