using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WeightAC
{
    public class DSPalletSheetlabel
    {
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        /// <summary>
        /// Print PalletLabel main
        /// </summary>
        /// <param name="endPallet"></param>
        /// <returns></returns>
        public bool PrintPalletLabel(string endPallet)
        {
            bool printFlag = false;
            int ibaishu = 0;
            string sline = "";
            string shead = "";

            shead = getPalletLabelHeadData(endPallet);
            //Label line 超过11行循环 打印
            string srownum = string.Format(" select ceil(count(*)/9) baishu "
                             + "   from (select a.shipment_id,a.dn,a.dn_line,a.mpn,c.poe,sum(a.qty) qty"
                             + "   from NONEDIPPS.g_ds_pick_t a, NONEDIPPS.g_ds_shimment_base_t c"
                             + "   where a.pallet_no = (select b.pallet_no from NONEDIPPS.g_ds_pallet_t b where b.end_palletno = '{0}') "
                             + "   and a.shipment_id = c.shipment_id  "
                             + "   group by a.shipment_id,a.dn,a.dn_line,a.mpn,c.POE"
                             + "   order by a.shipment_id,a.dn,a.dn_line,a.mpn,c.poe) d ", endPallet);
            DataTable dtRownum = ClientUtils.ExecuteSQL(srownum).Tables[0];
            if (dtRownum.Rows.Count > 0)
            {
                ibaishu = int.Parse(dtRownum.Rows[0]["baishu"].ToString());

                if (ibaishu > 0)
                {
                    for (int i = 0; i < ibaishu; i++)
                    {
                        sline = getPalletLabelLineData(endPallet, i);

                        string sall = shead + sline;
                        string labelName = @"PalletSheetlabel";
                        string labelPath = @"D:\MES_CLIENT\Shipping\Label";
                        string sMessage = string.Empty;
                        Print_Bartender_DataSource(labelName, Path.GetFullPath(labelPath), sall, 1, out sMessage);
                        if (sMessage == "OK")
                        {
                            printFlag = true;
                        }
                        System.Threading.Thread.Sleep(3 * 1000);
                        //return printFlag;
                    }
                }
            }
            //Label line 超过11行循环 打印
            return printFlag;
        }

        /// <summary>
        /// PalletLoadingSheet head
        /// </summary>
        /// <param name="endPallet"></param>
        /// <returns></returns>
        private string getPalletLabelHeadData(string endPallet)
        {
            StringBuilder sb = new StringBuilder();
            string carrier = "";
            string hawb = "", region = "";
            string totalCartons = "";
            string emptyCartons = "";
            string weight = "";
            string shipdate = "";
            string Pstart = "", Pend = "";

            string sqlHead = @"select a.pallet_no,a.shipment_id,b.carrier_name,b.region,b.hawb,a.sscc,a.total_cartons,a.empty_cartons,c.weight,to_char(sysdate,'mm-dd-yyyy') shipdate
                                from  NONEDIPPS.g_ds_pallet_t a
                                left join NONEDIPPS.g_ds_shimment_base_t b on a.shipment_id = b.shipment_id
                                left join NONEDIPPS.g_ds_weight_t c on a.shipment_id = c.shipment_id and a.end_palletno = c.pallet_no
                                where a.end_palletno = '" + endPallet + "'";
            DataTable dt_head = ClientUtils.ExecuteSQL(sqlHead).Tables[0];
            if (dt_head.Rows.Count > 0)
            {
                carrier = dt_head.Rows[0]["carrier_name"].ToString();
                hawb = dt_head.Rows[0]["hawb"].ToString();
                region = dt_head.Rows[0]["region"].ToString();
                totalCartons = dt_head.Rows[0]["total_cartons"].ToString();
                emptyCartons = dt_head.Rows[0]["empty_cartons"].ToString();
                weight = dt_head.Rows[0]["weight"].ToString();
                shipdate = dt_head.Rows[0]["shipdate"].ToString();
            }

            string sqlrownum = @"select rownum,a.end_palletno
                                    from NONEDIPPS.g_ds_pallet_t a
                                    where a.shipment_id = (select b.shipment_id from NONEDIPPS.g_ds_pallet_t b where b.end_palletno = '" + endPallet + "') order by a.end_palletno";

            DataTable dtRownun = ClientUtils.ExecuteSQL(sqlrownum).Tables[0];
            if (dtRownun.Rows.Count > 0)
            {
                for (int i = 0; i < dtRownun.Rows.Count; i++)
                {
                    if (dtRownun.Rows[i]["end_palletno"].ToString() == endPallet)
                    {
                        Pstart = (i + 1).ToString();
                        Pend = dtRownun.Rows.Count.ToString();
                    }

                }
            }
            sb.Append(endPallet + "|"
                + " " + "|"
                + hawb + "|"
                + Pstart + "|"
                + Pend + "|"
                + region + "|"
                + carrier + "|"
                + totalCartons + "|"
                + emptyCartons + "|"
                + shipdate + "|"
                + weight + "|"
                );


            return sb.ToString();
        }

        /// <summary>
        /// PalletLoadingSheet line
        /// </summary>
        /// <param name="end_pallet"></param>
        /// <returns></returns>
        private string getPalletLabelLineData(string endPallet, int istarrownum)
        {
            StringBuilder sb = new StringBuilder();
            string sDn = "", sDn_line = "";
            string sMpn = "", sQty = "";
            string sPoe = "", sHub = "DS";
            string sqlLine = string.Format(" select rownum,d.shipment_id,d.dn,d.dn_line,d.mpn,d.poe,d.qty"
                             + "   from (select a.shipment_id,a.dn,a.dn_line,a.mpn,c.poe,sum(a.qty) qty"
                             + "   from NONEDIPPS.g_ds_pick_t a, NONEDIPPS.g_ds_shimment_base_t c"
                             + "   where a.pallet_no = (select b.pallet_no from NONEDIPPS.g_ds_pallet_t b where b.end_palletno = '{0}') "
                             + "   and a.shipment_id = c.shipment_id  "
                             + "   group by a.shipment_id,a.dn,a.dn_line,a.mpn,c.POE"
                             + "   order by a.shipment_id,a.dn,a.dn_line,a.mpn,c.poe) d ", endPallet);
            DataTable dtLine = ClientUtils.ExecuteSQL(sqlLine).Tables[0];
            if (dtLine.Rows.Count > 0)
            {
                int iendrownum = 0;
                if (dtLine.Rows.Count > istarrownum * 9 + 9)
                {
                    iendrownum = istarrownum * 9 + 9;
                }
                else
                {
                    iendrownum = dtLine.Rows.Count;
                }

                //for (int i = 0; i < dt_line.Rows.Count; i++)
                for (int i = istarrownum * 9; i < iendrownum; i++)
                {
                    sDn = dtLine.Rows[i]["dn"].ToString();
                    sDn_line = dtLine.Rows[i]["dn_line"].ToString();
                    sMpn = dtLine.Rows[i]["mpn"].ToString();
                    sQty = dtLine.Rows[i]["qty"].ToString();
                    sPoe = dtLine.Rows[i]["poe"].ToString();
                    sb.Append(sDn + "|"
                                           + sDn_line + "|"
                                           + sMpn + "|"
                                           + sQty + "|"
                                           + sPoe + "|"
                                           + sHub + "|"
                             );
                }
            }

            return sb.ToString();
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

        public bool Print_Bartender_DataSource(string sFileName, string sDirectory, string sInputText, int iPrintQty, out string sMessage)
        {
            try
            {
                sMessage = "OK";
                string startupPath = System.Windows.Forms.Application.StartupPath;
                string sSampleFile = sDirectory + @"\" + sFileName + ".btw";
                string str7 = sDirectory + @"\" + sFileName + ".lst";
                string str8 = sDirectory + @"\" + sFileName + ".dat";
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


        #endregion

    }



}
