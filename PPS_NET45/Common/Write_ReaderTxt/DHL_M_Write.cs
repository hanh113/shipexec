using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Write_ReaderTxt
{
    public class DHL_M_Write
    {
        //public void dhl_m_Write(string path, string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(@"select 
        //                nvl('','1') AS AA,RPAD(nvl('',' '),6,' ') as LK,nvl('','SPS 2.03') AS CC,'SPS' as L_2,'' as L_3,
        //                '' as L_4,'' as L_5,'' as L_6,H.Mawb as MAWB,
        //                H.mawb as MAWB,'' as L_9,'' as L_10,'' as L_11,H.Accout_Num as ACCOUT_NUM,'' as L_13,'' as L_14,'' as L_15,
        //                '' as L_16,'' as L_17,'' as L_18,'' as L_19 ,'' as L_20,'' as L_21,'' as L_22,H.Mawb as MAWB,'' as L_24,
        //                '' as L_25,'' as L_26,'' as L_27,'' as L_28 ,'N' as L_29,'USD' as L_30,'' as L_31,'' as L_32,'' as L_33,
        //                '' as L_34,'' as L_35 ,'' as L_36,'' as L_37,'' as L_38,'' as L_39 ,'' as L_40,'' as L_41,'' as L_42,
        //                '' as L_43 ,'' as L_44,'' as L_45,'' as L_46,'N' as L_47 ,'' as L_48,'' as L_49,'P' as L_50,'P' as L_51 ,
        //                to_char(H.Ship_Date,'YYYYMMDD') as SHIP_DATE,to_char(H.Ship_Date,'HHMM') as SHIP_DATE,'' as L_54,
        //                '' as L_55,H.Total_Count as TOTAL_COUNT,'Y' as L_57,H.Shipment_Total_Weight as SHIPMENT_TOTAL_WEIGHT,
        //                '' as L_59 ,'' as L_60,'' as L_61,H.Shipment_Total_Value as SHIPMENT_TOTAL_VALUE,'0' as L_63,'' as L_64 ,
        //                '' as L_65,'' as L_66,'' as L_67,'' as L_68 ,'' as L_69,'' as L_70,'' as L_71,'' as L_72 ,'' as L_73,
        //                '' as L_74,'' as L_75,'' as L_76,'' as L_77,'N' as L_78,'' as L_79,'0' as L_80 ,'' as L_81,'' as L_82,
        //                '' as L_83,'N' as L_84 ,'' as L_85,'0' as L_86,'' as L_87,'' as L_88,'' as L_89,'' as L_90,'' as L_91,
        //                '' as L_92 ,'' as L_93,'2' as L_94,'Electronic Products' as L_95,'' as L_96,'0' as L_97, '0' as L_98 ,'N' as L_99,
        //                '' as L_100,'' as L_101,'' as L_102 ,'' as L_103,'' as L_104,H.Trade_Term as trade_term,'' as L_106,'' as L_107 ,
        //                '' as L_108,'' as L_109,'3' as L_110,H.Mawb as MAWB,'' as L_112 ,'' as L_113,'' as L_114,'' as L_115,'' as L_116 ,
        //                '1' as L_117,'' as L_118,H.Shipment_Total_Weight as shipment_total_weight,'6.95' as L_120,'' as L_121 ,'' as L_122,
        //                '' as L_123,'' as L_124,'KG' as L_125,'' as L_126,'' as L_127,'' as L_128,'' as L_129 ,'3' as L_130,
        //                '' as L_131 ,'' as L_132,'1/1' as L_133,'' as L_134,'' as L_135 ,'' as L_136,'' as L_137
        //                from ppsuser.ict_lps_header H left join ppsuser.ict_lps_line L on H.Msg_Id = L.Msg_Id where H.msg_id = 10086");
        //    DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
        //    StringBuilder data = new StringBuilder();
        //    int i = 1;
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataColumn dc in ds.Tables[0].Columns)
        //        {
        //            if (dc.ToString().ToUpper().Equals("LK"))
        //            {
        //                data.Append(ds.Tables[0].Rows[0][dc].ToString() + "\r\n");
        //            }
        //            else
        //            {
        //                i++;
        //                data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
        //            }
        //        }
        //    }
        //    int j = i;
        //    string result = string.Empty;
        //    if (!string.IsNullOrEmpty(data.ToString()))
        //    {
        //        result = data.ToString().Substring(0, data.Length - 1);
        //    }
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path);
        //    }
        //    WriteFile wf = new WriteFile();
        //    wf.writeFile(path, fileName, result);
        //}

        public void dhl_m_Write(string DN,string DN_Line,string path, string fileName)
        {
            StringBuilder sb = new StringBuilder();// h.mawb||mod(h.MAWB,7) AS HAWB,h.mawb || mod(h.MAWB, 7) AS HAWB,
   sb.Append(@"select 
                        nvl('','1') AS AA,
                        RPAD(nvl('',' '),6,' ') as LK,
                        nvl('','SPS 2.03') AS CC,
                        'SPS' as L_2,
                        '' as L_3,
                        '' as L_4,
                        '' as L_5,
                        '' as L_6,
                        h.mawb AS HAWB,
                        h.mawb AS HAWB,
                        '' as L_9,
                        '' as L_10,
                        '' as L_11,
                        nvl(H.Accout_Num,'.') as ACCOUT_NUM,
                        '' as L_13,
                        '' as L_14,
                        '' as L_15,
                        '' as L_16,
                        '' as L_17,
                        '' as L_18,
                        '' as L_19 ,
                        '' as L_20,
                        '' as L_21,
                        '' as L_22,
                        nvl(H.Mawb,'.') as MAWB,
                        '' as L_24,
                        '' as L_25,
                        '' as L_26,
                        '' as L_27,
                        '' as L_28 ,
                        'N' as L_29,
                        'USD' as L_30,
                        '' as L_31,
                        '' as L_32,
                        '' as L_33,
                        '' as L_34,
                        '' as L_35 ,
                        '' as L_36,
                        '' as L_37,
                        '' as L_38,
                        '' as L_39 ,
                        '' as L_40,
                        '' as L_41,
                        '' as L_42,
                        '' as L_43 ,
                        '' as L_44,
                        '' as L_45,
                        '' as L_46,
                        'N' as L_47 ,
                        '' as L_48,
                        '' as L_49,
                        'P' as L_50,
                        'P' as L_51 ,
                        to_char(H.Ship_Date,'YYYYMMDD') as SHIP_DATE,
                        to_char(H.Ship_Date,'HHMM') as SHIP_DATE,
                        '' as L_54,
                        '' as L_55,
                        H.Total_Count as TOTAL_COUNT,
                        'Y' as L_57,
                        H.Shipment_Total_Weight as SHIPMENT_TOTAL_WEIGHT,
                        '' as L_59 ,
                        '' as L_60,
                        '' as L_61,
                        H.Shipment_Total_Value as SHIPMENT_TOTAL_VALUE,
                        '0' as L_63,
                        '' as L_64 ,
                        '' as L_65,
                        '' as L_66,
                        '' as L_67,
                        '' as L_68 ,
                        '' as L_69,
                        '' as L_70,
                        '' as L_71,'' as L_72 ,
                        '' as L_73,
                        '' as L_74,
                        '' as L_75,
                        '' as L_76,
                        '' as L_77,
                        'N' as L_78,
                        '' as L_79,
                        '0' as L_80 ,
                        '' as L_81,
                        '' as L_82,
                        '' as L_83,
                        'N' as L_84 ,
                        '' as L_85,
                        '0' as L_86,
                        '' as L_87,
                        '' as L_88,
                        '' as L_89,
                        '' as L_90,
                        '' as L_91,
                        '' as L_92 ,
                        '' as L_93,
                        '2' as L_94,
                        'Electronic Products' as L_95,
                        '' as L_96,
                        '0' as L_97, 
                        '0' as L_98 ,
                        'N' as L_99,
                        '' as L_100,
                        '' as L_101,
                        '' as L_102 ,
                        '' as L_103,
                        '' as L_104,
                        nvl(H.Trade_Term,'.') as trade_term,
                        '' as L_106,
                        '' as L_107 ,
                        '' as L_108,
                        '' as L_109,
                        '3' as L_110,
                        nvl(H.Mawb,'.') as MAWB,
                        '' as L_112 ,
                        '' as L_113,
                        '' as L_114,
                        '' as L_115,
                        '' as L_116 ,
                        '1' as L_117,
                        '' as L_118,
                        H.Shipment_Total_Weight as shipment_total_weight,
                        '6.95' as L_120,
                        '' as L_121 ,
                        '' as L_122,
                        '' as L_123,
                        '' as L_124,
                        'KG' as L_125,
                        '' as L_126,
                        '' as L_127,
                        '' as L_128,
                        '' as L_129 ,
                        '3' as L_130,
                        '' as L_131 ,
                        '' as L_132,
                        '1/1' as L_133,
                        '' as L_134,
                        '' as L_135 ,
                        '' as L_136,
                        '' as L_137
                        from ppsuser.ict_lps_header H left join ppsuser.ict_lps_line L on H.Msg_Id = L.Msg_Id where  L.mother_child_tag = 'M' and  L.AC_DN = '" + DN + "' and L.AC_DN_LINE='" + DN_Line + "'");
            DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
            StringBuilder data = new StringBuilder();
            int i = 1;
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    if (dc.ToString().ToUpper().Equals("LK"))
                    {
                        data.Append(ds.Tables[0].Rows[0][dc].ToString() + "\r\n");
                    }
                    else
                    {
                        i++;
                        data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
                    }
                }
            }
            int j = i;
            string result = string.Empty;
            if (!string.IsNullOrEmpty(data.ToString()))
            {
                result = data.ToString().Substring(0, data.Length - 1);
            }
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            WriteFile wf = new WriteFile();
            wf.writeFile(path, fileName, result);
        }
    }
}
