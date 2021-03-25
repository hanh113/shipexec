using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PickListAC
{
    class PickPalletLabel
    {

        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        //HYQ： 如果是9号pick栈板，palletno必须状态是FP的才能打印和补印，.....
        public bool PrintPickPalletLabel(string strPickpalletno)
        {
            //HYQ： 本来下面这个是一个功能很多的版本，但是现在打印PickPalletLabel 只要几个栏位就好。
            //完整的版类似水晶报表， strHead +strLine=strALL
            //这里只要strHead 就好
            if (string.IsNullOrEmpty(strPickpalletno))
            {
                return false;
            }

            string sMessage = "";
            string strLabelName = @"Pick_Pallet_Label";
            string strLabelPath = @"D:\MES_CLIENT\Shipping\Label";

            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
            if (!File.Exists(strSampleFile))
            {
                sMessage = "Sample File Not exists-" + strSampleFile;
                return false;
            }

            //HYQ： 这部分是写.dat文件。 5个栏位就好
            string LabelParam = @"SHIPMENT_ID|PICK_PALLET_NO|CARRIER|QTY|SHIPMENT_TYPE|QTY1|QTY2|";
            //目前 打印的不知道在哪设置 支持|  ， 目前只能用逗号
            LabelParam = LabelParam.Replace("|", @",");


            string str8 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".dat";
            if (!File.Exists(str8))
            {
                sMessage = "Label File Not Found (.dat)" + Environment.NewLine + Environment.NewLine + str8 + Environment.NewLine + Environment.NewLine;
                return false;
            }

            //HYQ：重新写这个文件的内容.WriteToPrintGo借用这个方法，将label打印的欄位清单LabelParam写进str8文件。
            this.WriteToPrintGo(str8, LabelParam);

            //label 总共5个栏位：
            // SHIPMENT_ID|PICK_PALLET_NO|CARRIER|QTY|SHIPMENT_TYPE|QTY1|QTY2|
            //qty1 : 1-105/105  pickpalletno 数量统计
            //qty2 : 1/9   pickpalletno号
            string SQL = string.Empty;

            SQL = string.Format("select c.shipment_id, c.pick_pallet_no,c.CARRNAME,c.pallettype, sum(qty) as sumqty "
                     + " from(select a.shipment_id, "
                     + "              b.pick_pallet_no, "
                     + "            (SELECT distinct SCACCODE "
                     + "         FROM NONEDIOMS.OMS_CARRIER_TRACKING_PREFIX D "
                     + "        where trim(D.carriercode) = e.carrier_code"
                     + "          and D.ShipMode = e.transport "
                     + "          and D.region = e.region "
                     + "          and D.isdisabled = '0' "
                     + "          and D.type = 'HAWB') AS CARRNAME, "
                     + "              case "
                     + "                when a.pallet_type = '001' and a.gs1flag = 'N'   then 'NO MIX / NO GS1' "
                     + "                when a.pallet_type = '001' and a.gs1flag = 'Y'   then '  NO MIX / GS1' "
                     + "                when a.pallet_type = '999' and a.gs1flag = 'N'   then '  MIX / NO GS1' "
                     + "                when a.pallet_type = '999' and a.gs1flag = 'Y'   then '   MIX / GS1' "
                     + "                when a.pallet_type = '999' and a.gs1flag is null then '      MIX' "
                     + "                when a.pallet_type = '001' and a.gs1flag is null then '     NO MIX' "
                     + "                else a.pallet_type "
                     + "              end pallettype, "
                     + "              b.qty "
                     + "         from NONEDIPPS.t_shipment_pallet a "
                     + "         join NONEDIPPS.t_pallet_pick b "
                     + "           on a.pallet_no = b.pallet_no "
                     + "         join NONEDIPPS.t_shipment_info e "
                     + "           on a.shipment_id = e.shipment_id "
                     + "        where b.pick_pallet_no = '{0}') c "
                     + "group by c.shipment_id, c.pick_pallet_no, c.CARRNAME, c.pallettype", strPickpalletno);

            DataTable dt = new DataTable();

            try
            {
                dt = ClientUtils.ExecuteSQL(SQL).Tables[0];
            }

            catch (Exception ex)
            {
                MessageBox.Show("strSQL执行异常"+ex.ToString());
                return false;
            }
            string strHead = "";
            if (dt.Rows.Count == 1)
            {
                string str1 = dt.Rows[0]["shipment_id"].ToString();
                string str2 = dt.Rows[0]["pick_pallet_no"].ToString();
                string str3 = dt.Rows[0]["CARRNAME"].ToString();
                string str4 = dt.Rows[0]["sumqty"].ToString();
                // HYQ：20181126 如果sumqty=0 直接return false 不用打印
                if (Convert.ToInt32(str4)==0) { return false; }
                string str5 = dt.Rows[0]["pallettype"].ToString();
                //HYQ: 前面补空格无效， .lst文件是有空格的， 但是打印程序被屏蔽掉了。
                str5 = str5.PadRight((15 - str5.Length)/2, ' ');


                strHead = str1 +","+ str2 + "," + str3 + "," + str4 + "," + str5 + "," ;

            }
            else
            {
                return false;
            }

            //HYQ：后面的QTY1 QTY2 
            string strSQL2 = string.Empty;
            string totalPalletqty = "";
            string str7 = "";
            strSQL2=string.Format("select a.shipment_id, a.pallet_no, a.carton_qty, b.pick_pallet_no,b.pallet_number "
                                  + "from(select shipment_id, pallet_no, carton_qty "
                                  + "        from NONEDIPPS.t_shipment_pallet "
                                  + "       where shipment_id in "
                                  + "             (select shipment_id "
                                  + "                from NONEDIPPS.t_shipment_pallet "
                                  + "               where pallet_no in "
                                  + "                     (select pallet_no "
                                  + "                        from NONEDIPPS.t_pallet_pick "
                                  + "                       where pick_pallet_no = '{0}'))) a "
                                  + " left join(select distinct pallet_no, pick_pallet_no,pallet_number "
                                  + "             from NONEDIPPS.t_pallet_pick "
                                  + "            where pick_pallet_no = '{1}') b "
                                  + "  on a.pallet_no = b.pallet_no "
                                  + " order by a.pallet_no asc ", strPickpalletno, strPickpalletno);
            DataTable dt2 = new DataTable();

            try
            {
                dt2 = ClientUtils.ExecuteSQL(strSQL2).Tables[0];
            }

            catch (Exception ex2)
            {
                MessageBox.Show("QTY2执行异常"+ex2.ToString());
                return false;
            }
            if (dt2.Rows.Count >0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["pick_pallet_no"].ToString().Contains(strPickpalletno))
                    {
                        totalPalletqty = dt2.Rows[i]["CARTON_QTY"].ToString();
                        str7 = dt2.Rows[i]["pallet_number"].ToString() + "/" + dt2.Rows.Count.ToString();
                        break;
                    }
                }
                
            }
            else
            {
                return false;
            }


            string strSQL3 = string.Empty;
            string str6 = "";
            int endQTY = 0;
            strSQL3 = string.Format("select pallet_no, pick_pallet_no , sum(CARTON) as CARTON "
                                    + "  from NONEDIPPS.t_pallet_pick "
                                    + " where pallet_no in "
                                    + "       (select pallet_no "
                                    + "          from NONEDIPPS.t_pallet_pick "
                                    + "         where pick_pallet_no = '{0}') "
                                    + " group by pallet_no, pick_pallet_no "
                                    + " order by pick_pallet_no asc", strPickpalletno);
            DataTable dt3 = new DataTable();

            try
            {
                dt3 = ClientUtils.ExecuteSQL(strSQL3).Tables[0];
            }

            catch (Exception ex3)
            {
                MessageBox.Show("QTY1执行异常" + ex3.ToString());
                return false;
            }
            if (dt3.Rows.Count > 0)
            {
                int startQTY = 1;
               
                for (int i = 0; i < dt3.Rows.Count; i++)
                {

                    if (i > 0)
                    {
                        startQTY = startQTY + Convert.ToInt32(dt3.Rows[i-1]["CARTON"]);
                    }
                   
                    endQTY= endQTY + Convert.ToInt32(dt3.Rows[i]["CARTON"]);
                    if (dt3.Rows[i]["pick_pallet_no"].ToString().Contains(strPickpalletno))
                    {
                        break;
                    } 
                }

                str6 = startQTY.ToString() + "-" + endQTY.ToString() + "/" + totalPalletqty;
            }
            else
            {
                return false;
            }

            if (strPickpalletno.Substring(1, 1).Equals("9") )
            {
                if (!endQTY.ToString().Equals(totalPalletqty) )
                {
                    //这么写不好，再改改。
                    MessageBox.Show("9号pickpallet,必须拣货完成自动打印");
                    return false;
                }
                
            }

            strHead = strHead+ str6 + "," + str7 + ",";
            strHead = LabelParam + "\r\n" + strHead;
            string strLst = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".lst";
            if (File.Exists(strLst))
            {
                File.Delete(strLst);
            }
            this.WriteToPrintGo(strLst, strHead);

            string sFile = strStartupPath + @"\PrintGo.bat";
            string str11 = strStartupPath + @"\PrintLabel.bat";

            string sData = this.LoadBatFile(str11, ref sMessage);

            if (!string.IsNullOrEmpty(sMessage))
            {
                return false;
            }
            else
            {
                sData = sData.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".lst" + '"').Replace("@QTY", "1");
                this.WriteToPrintGo(sFile, sData);
                int num4 = WinExec(sFile, 0);
                System.Threading.Thread.Sleep(1 * 1000);
                return true;
            }
        }

        public bool PrintPickListLabel(DataTable dt, int listrows)
        {
            string strLabelName = "PickListLabelII";
            string strLabelPath = Path.Combine(System.Windows.Forms.Application.StartupPath, @"Shipping\Label");
            if (dt.Rows.Count > 0)
            {
                int mixTotalrow = dt.Rows.Count;
                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));


                //这部分是写.dat文件,默认显示15行数据
                string LabelParam = @"TOTALPAGE,CURRENTPAGE,SHIPMENT_ID,SHIPPING_TIME,REGION,TDM,PALLET_NO,QTY,";
                //页打印行数
                int pagerows= listrows> mixTotalrow? mixTotalrow : listrows;

                for (int a = 1; a <= pagerows; a++)
                {
                    if (a == pagerows)
                    {
                        LabelParam = LabelParam + @"PALLET_NO" + a.ToString() + ",LOCATION_NO" + a.ToString() + ",POINTNO" + a.ToString() + ",TROLLEY_LINE_NO" + a.ToString() + "";
                    }
                    else
                    {
                        LabelParam = LabelParam + @"PALLET_NO" + a.ToString() + ",LOCATION_NO" + a.ToString() + ",POINTNO" + a.ToString() + ",TROLLEY_LINE_NO" + a.ToString() + ",";
                    }
                }

                string strLine = "";
                string strPALLET_NO = "";// = "";
                string strLOCATION_NO = "";//= dt.Rows[j]["LOCATION_NO"].ToString().Trim();
                string strPOINTNO = "";//= dt.Rows[j]["POINTNO"].ToString().Trim();
                string strTROLLEYLINENO = "";// = dt.Rows[j]["TROLLEY_LINE_NO"].ToString().Trim();
                //string strcount;
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    strLine = "";
                    string strSHIPMENT_ID = dt.Rows[i]["SHIPMENT_ID"].ToString().Trim();
                    string strSHIPPING_TIME = dt.Rows[i]["SHIPPING_TIME"].ToString().Trim();
                    string strREGION = dt.Rows[i]["REGION"].ToString().Trim();
                    string strtdm = dt.Rows[i]["TDM"].ToString().Trim();
                    strLine = strLine + TOTALPAGE.ToString() + "," +
                                    (i + 1).ToString() + "," +
                                    strSHIPMENT_ID + "," +
                                    strSHIPPING_TIME + "," +
                                    strREGION + "," + strtdm + ",";
                    strLine = strLine + dt.Rows[i]["PALLET_NO"].ToString() + "," + dt.Rows.Count.ToString() + ",";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dt.Rows.Count)
                        {
                            break;
                        }

                        strPALLET_NO = "";
                        strLOCATION_NO = dt.Rows[j]["LOCATION_NO"].ToString().Trim();
                        strPOINTNO = dt.Rows[j]["POINTNO"].ToString().Trim();
                        strTROLLEYLINENO = dt.Rows[j]["TROLLEY_LINE_NO"].ToString().Trim();

                        strLine = strLine + strPALLET_NO + "," +
                            strLOCATION_NO + " ," +
                            strPOINTNO + "," + strTROLLEYLINENO + ",";

                    }
                    strLine = LabelParam + "\r\n" + strLine;

                    string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i + ".lst";
                    if (File.Exists(str7))
                    {
                        File.Delete(str7);
                    }
                    this.WriteToPrintGo(str7, strLine);
                }
                //循环打印所有label，
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    using (Process p = new Process())
                    {
                        string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                        if (!File.Exists(strSampleFile))
                        {
                            MessageBox.Show("Sample File Not exists-" + strSampleFile);
                            return false;
                        }
                        p.StartInfo.FileName = "bartend.exe";
                        string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i.ToString() + ".lst" + '"').Replace("@QTY", "1");
                        p.StartInfo.Arguments = sArguments;
                        p.Start();
                        p.WaitForExit();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PrintPickListLabel_bk(DataTable dt, int listrows)
        {
            string strLabelName = "PickListLabel";
            string strLabelPath = Path.Combine(System.Windows.Forms.Application.StartupPath, @"Shipping\Label");
            if (dt.Rows.Count > 0)
            {
                int mixTotalrow = dt.Rows.Count;
                //最终打印的张数
                int TOTALPAGE = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(mixTotalrow) / Convert.ToDouble(listrows)));

                //这部分是写.dat文件,默认显示15行数据
                string LabelParam = @"TOTALPAGE,CURRENTPAGE,SHIPMENT_ID,SHIPPING_TIME,REGION,TDM,PALLET_NO,QTY,";
                LabelParam = LabelParam + @"PALLET_NO1,ICTPN1,MPN1,LOCATION_NO1,SIDES_NO1,LEVEL_NO1,SEQ_NO1,POINTNO1,TROLLEY_LINE_NO1,";
                LabelParam = LabelParam + @"PALLET_NO2,ICTPN2,MPN2,LOCATION_NO2,SIDES_NO2,LEVEL_NO2,SEQ_NO2,POINTNO2,TROLLEY_LINE_NO2,";
                LabelParam = LabelParam + @"PALLET_NO3,ICTPN3,MPN3,LOCATION_NO3,SIDES_NO3,LEVEL_NO3,SEQ_NO3,POINTNO3,TROLLEY_LINE_NO3,";
                LabelParam = LabelParam + @"PALLET_NO4,ICTPN4,MPN4,LOCATION_NO4,SIDES_NO4,LEVEL_NO4,SEQ_NO4,POINTNO4,TROLLEY_LINE_NO4,";
                LabelParam = LabelParam + @"PALLET_NO5,ICTPN5,MPN5,LOCATION_NO5,SIDES_NO5,LEVEL_NO5,SEQ_NO5,POINTNO5,TROLLEY_LINE_NO5,";
                LabelParam = LabelParam + @"PALLET_NO6,ICTPN6,MPN6,LOCATION_NO6,SIDES_NO6,LEVEL_NO6,SEQ_NO6,POINTNO6,TROLLEY_LINE_NO6,";
                LabelParam = LabelParam + @"PALLET_NO7,ICTPN7,MPN7,LOCATION_NO7,SIDES_NO7,LEVEL_NO7,SEQ_NO7,POINTNO7,TROLLEY_LINE_NO7,";
                LabelParam = LabelParam + @"PALLET_NO8,ICTPN8,MPN8,LOCATION_NO8,SIDES_NO8,LEVEL_NO8,SEQ_NO8,POINTNO8,TROLLEY_LINE_NO8,";
                LabelParam = LabelParam + @"PALLET_NO9,ICTPN9,MPN9,LOCATION_NO9,SIDES_NO9,LEVEL_NO9,SEQ_NO9,POINTNO9,TROLLEY_LINE_NO9,";
                LabelParam = LabelParam + @"PALLET_NO10,ICTPN10,MPN10,LOCATION_NO10,SIDES_NO10,LEVEL_NO10,SEQ_NO10,POINTNO10,TROLLEY_LINE_NO10,";
                LabelParam = LabelParam + @"PALLET_NO11,ICTPN11,MPN11,LOCATION_NO11,SIDES_NO11,LEVEL_NO11,SEQ_NO11,POINTNO11,TROLLEY_LINE_NO11,";
                LabelParam = LabelParam + @"PALLET_NO12,ICTPN12,MPN12,LOCATION_NO12,SIDES_NO12,LEVEL_NO12,SEQ_NO12,POINTNO12,TROLLEY_LINE_NO12,";
                LabelParam = LabelParam + @"PALLET_NO13,ICTPN13,MPN13,LOCATION_NO13,SIDES_NO13,LEVEL_NO13,SEQ_NO13,POINTNO13,TROLLEY_LINE_NO13,";
                LabelParam = LabelParam + @"PALLET_NO14,ICTPN14,MPN14,LOCATION_NO14,SIDES_NO14,LEVEL_NO14,SEQ_NO14,POINTNO14,TROLLEY_LINE_NO14,";
                LabelParam = LabelParam + @"PALLET_NO15,ICTPN15,MPN15,LOCATION_NO15,SIDES_NO15,LEVEL_NO15,SEQ_NO15,POINTNO15,TROLLEY_LINE_NO15,";
                LabelParam = LabelParam + @"PALLET_NO16,ICTPN16,MPN16,LOCATION_NO16,SIDES_NO16,LEVEL_NO16,SEQ_NO16,POINTNO16,TROLLEY_LINE_NO16,";
                LabelParam = LabelParam + @"PALLET_NO17,ICTPN17,MPN17,LOCATION_NO17,SIDES_NO17,LEVEL_NO17,SEQ_NO17,POINTNO17,TROLLEY_LINE_NO17,";
                LabelParam = LabelParam + @"PALLET_NO18,ICTPN18,MPN18,LOCATION_NO18,SIDES_NO18,LEVEL_NO18,SEQ_NO18,POINTNO18,TROLLEY_LINE_NO18,";
                LabelParam = LabelParam + @"PALLET_NO19,ICTPN19,MPN19,LOCATION_NO19,SIDES_NO19,LEVEL_NO19,SEQ_NO19,POINTNO19,TROLLEY_LINE_NO19,";
                LabelParam = LabelParam + @"PALLET_NO20,ICTPN20,MPN20,LOCATION_NO20,SIDES_NO20,LEVEL_NO20,SEQ_NO20,POINTNO20,TROLLEY_LINE_NO20,";
                LabelParam = LabelParam + @"PALLET_NO21,ICTPN21,MPN21,LOCATION_NO21,SIDES_NO21,LEVEL_NO21,SEQ_NO21,POINTNO21,TROLLEY_LINE_NO21,";
                LabelParam = LabelParam + @"PALLET_NO22,ICTPN22,MPN22,LOCATION_NO22,SIDES_NO22,LEVEL_NO22,SEQ_NO22,POINTNO22,TROLLEY_LINE_NO22,";
                LabelParam = LabelParam + @"PALLET_NO23,ICTPN23,MPN23,LOCATION_NO23,SIDES_NO23,LEVEL_NO23,SEQ_NO23,POINTNO23,TROLLEY_LINE_NO23,";
                LabelParam = LabelParam + @"PALLET_NO24,ICTPN24,MPN24,LOCATION_NO24,SIDES_NO24,LEVEL_NO24,SEQ_NO24,POINTNO24,TROLLEY_LINE_NO24,";
                LabelParam = LabelParam + @"PALLET_NO25,ICTPN25,MPN25,LOCATION_NO25,SIDES_NO25,LEVEL_NO25,SEQ_NO25,POINTNO25,TROLLEY_LINE_NO25,";
                LabelParam = LabelParam + @"PALLET_NO26,ICTPN26,MPN26,LOCATION_NO26,SIDES_NO26,LEVEL_NO26,SEQ_NO26,POINTNO26,TROLLEY_LINE_NO26,";
                LabelParam = LabelParam + @"PALLET_NO27,ICTPN27,MPN27,LOCATION_NO27,SIDES_NO27,LEVEL_NO27,SEQ_NO27,POINTNO27,TROLLEY_LINE_NO27";

                string strLine = "";

                string strPALLET_NO = "";// = "";
                string strICTPN = "";//= dt.Rows[j]["ICTPN"].ToString().Trim();
                string strMPN = "";//= dt.Rows[j]["MPN"].ToString().Trim();
                string strLOCATION_NO = "";//= dt.Rows[j]["LOCATION_NO"].ToString().Trim();
                string strSIDES_NO = "";//= dt.Rows[j]["SIDES_NO"].ToString().Trim();
                string strLEVEL_NO = "";//= dt.Rows[j]["LEVEL_NO"].ToString().Trim();
                string strSEQ_NO = "";//= dt.Rows[j]["SEQ_NO"].ToString().Trim();
                string strPOINTNO = "";//= dt.Rows[j]["POINTNO"].ToString().Trim();
                string strTROLLEYLINENO = "";// = dt.Rows[j]["TROLLEY_LINE_NO"].ToString().Trim();
                //string strcount;
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    strLine = "";
                    string strSHIPMENT_ID = dt.Rows[i]["SHIPMENT_ID"].ToString().Trim();
                    string strSHIPPING_TIME = dt.Rows[i]["SHIPPING_TIME"].ToString().Trim();
                    string strREGION = dt.Rows[i]["REGION"].ToString().Trim();
                    string strtdm = dt.Rows[i]["TDM"].ToString().Trim();
                    strLine = strLine + TOTALPAGE.ToString() + "," +
                                    (i + 1).ToString() + "," +
                                    strSHIPMENT_ID + "," +
                                    strSHIPPING_TIME + "," +
                                    strREGION + "," + strtdm + ",";
                    strLine = strLine + dt.Rows[i]["PALLET_NO"].ToString() + "," +
                        dt.Rows.Count.ToString() + ",";
                    for (int j = i * listrows; j < (i + 1) * listrows; j++)
                    {
                        if (j == dt.Rows.Count)
                        {
                            break;
                        }

                        strPALLET_NO = "";
                        strICTPN = dt.Rows[j]["ICTPN"].ToString().Trim();
                        strMPN = dt.Rows[j]["MPN"].ToString().Trim();
                        if (strTROLLEYLINENO != dt.Rows[j]["TROLLEY_LINE_NO"].ToString().Trim()
                            )
                        {
                            strLOCATION_NO = dt.Rows[j]["LOCATION_NO"].ToString().Trim();
                            strSIDES_NO = dt.Rows[j]["SIDES_NO"].ToString().Trim();
                            strLEVEL_NO = dt.Rows[j]["LEVEL_NO"].ToString().Trim();
                            strSEQ_NO = dt.Rows[j]["SEQ_NO"].ToString().Trim();
                            strPOINTNO = dt.Rows[j]["POINTNO"].ToString().Trim();
                            strTROLLEYLINENO = dt.Rows[j]["TROLLEY_LINE_NO"].ToString().Trim();

                            var r = dt.Select("LOCATION_NO ='" + strLOCATION_NO + "' and SIDES_NO='" + strSIDES_NO + "' and LEVEL_NO ='" + strLEVEL_NO + "'");

                            strLine = strLine + strPALLET_NO + "," +
                                strICTPN + "," +
                                strMPN + "," +
                                strLOCATION_NO + " ," +
                                strSIDES_NO + "," +
                                strLEVEL_NO + "," +
                                strSEQ_NO + "," +
                                strPOINTNO + "," + strTROLLEYLINENO + ",";

                            //strLine = strLine + strPALLET_NO + "," +
                            //      strICTPN + "," +
                            //      strMPN + "," +
                            //      strLOCATION_NO+" ," +
                            //      strSIDES_NO + "," +
                            //      strLEVEL_NO + "," +
                            //      strSEQ_NO + "," +
                            //      strPOINTNO + "," + strTROLLEYLINENO + "(" + r.Length + "),";

                        }
                        else
                        {
                            //strLOCATION_NO = "";// dt.Rows[j]["LOCATION_NO"].ToString().Trim();
                            //strSIDES_NO = ""; //dt.Rows[j]["SIDES_NO"].ToString().Trim();
                            //strLEVEL_NO = ""; //dt.Rows[j]["LEVEL_NO"].ToString().Trim();
                            //strSEQ_NO = ""; //dt.Rows[j]["SEQ_NO"].ToString().Trim();
                            strPOINTNO = dt.Rows[j]["POINTNO"].ToString().Trim();
                            //strTROLLEYLINENO = "";// dt.Rows[j]["TROLLEY_LINE_NO"].ToString().Trim();

                            strLine = strLine + "," +
                                    "," +
                                     "," +
                                     "," +
                                     "," +
                                     "," +
                                     "," +
                                   strPOINTNO + "," + ",";


                        }




                    }
                    strLine = LabelParam + "\r\n" + strLine;

                    string str7 = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i + ".lst";
                    if (File.Exists(str7))
                    {
                        File.Delete(str7);
                    }
                    this.WriteToPrintGo(str7, strLine);
                }
                //循环打印所有label，
                for (int i = 0; i < TOTALPAGE; i++)
                {
                    using (Process p = new Process())
                    {
                        string strSampleFile = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".btw";
                        if (!File.Exists(strSampleFile))
                        {
                            MessageBox.Show("Sample File Not exists-" + strSampleFile);
                            return false;
                        }
                        p.StartInfo.FileName = "bartend.exe";
                        string sArguments = @" /F=@PATH1 /D=@PATH2 /P /X /C=@QTY";
                        sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + Path.GetFullPath(strLabelPath) + @"\" + strLabelName + i.ToString() + ".lst" + '"').Replace("@QTY", "1");
                        p.StartInfo.Arguments = sArguments;
                        p.Start();
                        p.WaitForExit();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PrintPickPalletLabel_new(string strPickpalletno)
        {
            //HYQ： 本来下面这个是一个功能很多的版本，但是现在打印PickPalletLabel 只要几个栏位就好。
            //完整的版类似水晶报表， strHead +strLine=strALL
            //这里只要strHead 就好
            if (string.IsNullOrEmpty(strPickpalletno))
            {
                return false;
            }

            string sMessage = "";
            string strLabelName = @"Pick_Pallet_Label";
            string strStartupPath = System.Windows.Forms.Application.StartupPath;
            string strLabelPath = strStartupPath +@"\Shipping\Label";
            //HYQ： 这部分是写.dat文件。 5个栏位就好
            string LabelParam = @"SHIPMENT_ID|PICK_PALLET_NO|CARRIER|QTY|SHIPMENT_TYPE|QTY1|QTY2|";

            //目前 打印的不知道在哪设置 支持|  ， 目前只能用逗号
            LabelParam = LabelParam.Replace("|", @",");
            

            //label 总共5个栏位：
            // SHIPMENT_ID|PICK_PALLET_NO|CARRIER|QTY|SHIPMENT_TYPE|QTY1|QTY2|
            //qty1 : 1-105/105  pickpalletno 数量统计
            //qty2 : 1/9   pickpalletno号
            string SQL = string.Empty;

            SQL = string.Format("select c.shipment_id, c.pick_pallet_no,c.CARRNAME,c.pallettype,c.remark, sum(qty) as sumqty "
                     + " from(select a.shipment_id, "
                     + "              b.pick_pallet_no, "
                     + "          e.Carrier_Name AS CARRNAME, "
                     + "              case "
                     + "                when a.pallet_type = '001' and a.gs1flag = 'N'   then 'NO MIX / NO GS1' "
                     + "                when a.pallet_type = '001' and a.gs1flag = 'Y'   then '  NO MIX / GS1' "
                     + "                when a.pallet_type = '999' and a.gs1flag = 'N'   then '  MIX / NO GS1' "
                     + "                when a.pallet_type = '999' and a.gs1flag = 'Y'   then '   MIX / GS1' "
                     + "                when a.pallet_type = '999' and a.gs1flag is null then '      MIX' "
                     + "                when a.pallet_type = '001' and a.gs1flag is null then '     NO MIX' "
                     + "                else a.pallet_type "
                     + "              end pallettype, "
                     + "              b.qty,f.remark "
                     + "         from NONEDIPPS.t_shipment_pallet a "
                     + "         join NONEDIPPS.t_pallet_pick b "
                     + "           on a.pallet_no = b.pallet_no "
                     + "         join NONEDIPPS.T_SHIPMENT_INFO e "
                     + "           on a.shipment_id = e.shipment_id "
                      + " left join (select packcode, min(remark) remark "
                    + "          from(select distinct packcode,PALLETLENGTHCM|| '*' || PALLETWIDTHCM as remark from NONEDIPPS.vw_mpn_info) "
                    + "        group by packcode) f "
                    + "    on a.pack_code = f.packcode "
                     + "        where b.pick_pallet_no = '{0}') c "
                     + "group by c.shipment_id, c.pick_pallet_no, c.CARRNAME, c.pallettype ,c.remark", strPickpalletno);

            DataTable dt = new DataTable();

            try
            {
                dt = ClientUtils.ExecuteSQL(SQL).Tables[0];
            }

            catch (Exception ex)
            {
                MessageBox.Show("strSQL执行异常" + ex.ToString());
                return false;
            }
            string strHead = "";
            string strpackcodedesc = string.Empty;
            if (dt.Rows.Count == 1)
            {
                string str1 = dt.Rows[0]["shipment_id"].ToString();
                string str2 = dt.Rows[0]["pick_pallet_no"].ToString();
                string str3 = dt.Rows[0]["CARRNAME"].ToString();
                string str4 = dt.Rows[0]["sumqty"].ToString();
                // HYQ：20181126 如果sumqty=0 直接return false 不用打印
                if (Convert.ToInt32(str4) == 0) { return false; }
                string str5 = dt.Rows[0]["pallettype"].ToString();
                //HYQ: 前面补空格无效， .lst文件是有空格的， 但是打印程序被屏蔽掉了。
                str5 = str5.PadRight((15 - str5.Length) / 2, ' ');

                strpackcodedesc = dt.Rows[0]["remark"].ToString();
                strHead = str1 + "," + str2 + "," + str3 + "," + str4 + "," + str5 + ",";

            }
            else
            {
                return false;
            }

            //HYQ：后面的QTY1 QTY2 
            string strSQL2 = string.Empty;
            string totalPalletqty = "";
            string str7 = "";
            strSQL2 = string.Format("select a.shipment_id, a.pallet_no, a.carton_qty, b.pick_pallet_no,b.pallet_number "
                                  + "from(select shipment_id, pallet_no, carton_qty "
                                  + "        from NONEDIPPS.t_shipment_pallet "
                                  + "       where shipment_id in "
                                  + "             (select shipment_id "
                                  + "                from NONEDIPPS.t_shipment_pallet "
                                  + "               where pallet_no in "
                                  + "                     (select pallet_no "
                                  + "                        from NONEDIPPS.t_pallet_pick "
                                  + "                       where pick_pallet_no = '{0}'))) a "
                                  + " left join(select distinct pallet_no, pick_pallet_no,pallet_number "
                                  + "             from NONEDIPPS.t_pallet_pick "
                                  + "            where pick_pallet_no = '{1}') b "
                                  + "  on a.pallet_no = b.pallet_no "
                                  + " order by a.pallet_no asc ", strPickpalletno, strPickpalletno);
            DataTable dt2 = new DataTable();

            try
            {
                dt2 = ClientUtils.ExecuteSQL(strSQL2).Tables[0];
            }

            catch (Exception ex2)
            {
                MessageBox.Show("QTY2执行异常" + ex2.ToString());
                return false;
            }
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["pick_pallet_no"].ToString().Contains(strPickpalletno))
                    {
                        totalPalletqty = dt2.Rows[i]["CARTON_QTY"].ToString();
                        str7 = dt2.Rows[i]["pallet_number"].ToString() + "/" + dt2.Rows.Count.ToString();
                        break;
                    }
                }

            }
            else
            {
                return false;
            }


            string strSQL3 = string.Empty;
            string str6 = "";
            int endQTY = 0;
            strSQL3 = string.Format("select pallet_no, pick_pallet_no , sum(CARTON) as CARTON "
                                    + "  from NONEDIPPS.t_pallet_pick "
                                    + " where pallet_no in "
                                    + "       (select pallet_no "
                                    + "          from NONEDIPPS.t_pallet_pick "
                                    + "         where pick_pallet_no = '{0}') "
                                    + " group by pallet_no, pick_pallet_no "
                                    + " order by pick_pallet_no asc", strPickpalletno);
            DataTable dt3 = new DataTable();

            try
            {
                dt3 = ClientUtils.ExecuteSQL(strSQL3).Tables[0];
            }

            catch (Exception ex3)
            {
                MessageBox.Show("QTY1执行异常" + ex3.ToString());
                return false;
            }
            if (dt3.Rows.Count > 0)
            {
                int startQTY = 1;

                for (int i = 0; i < dt3.Rows.Count; i++)
                {

                    if (i > 0)
                    {
                        startQTY = startQTY + Convert.ToInt32(dt3.Rows[i - 1]["CARTON"]);
                    }

                    endQTY = endQTY + Convert.ToInt32(dt3.Rows[i]["CARTON"]);
                    if (dt3.Rows[i]["pick_pallet_no"].ToString().Contains(strPickpalletno))
                    {
                        break;
                    }
                }

                str6 = startQTY.ToString() + "-" + endQTY.ToString() + "/" + totalPalletqty;
            }
            else
            {
                return false;
            }

            if (strPickpalletno.Substring(1, 1).Equals("9"))
            {
                if (!endQTY.ToString().Equals(totalPalletqty))
                {
                    //这么写不好，再改改。
                    MessageBox.Show("9号pickpallet,必须拣货完成自动打印");
                    return false;
                }

            }
            strHead = strHead + str6 + "," + str7 + ","+ strpackcodedesc + ",";
            strHead = LabelParam + "\r\n" + strHead;
            
            string strLst = Path.GetFullPath(strLabelPath) + @"\" + strLabelName + ".lst";
            this.WriteToPrintGo(strLst, strHead);
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
                sArguments = sArguments.Replace("@PATH1", '"' + strSampleFile + '"').Replace("@PATH2", '"' + strLst + '"').Replace("@QTY", "1");
                p.StartInfo.Arguments = sArguments;
                p.Start();
                p.WaitForExit();
            }
            return true;
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
        
        }
    }



