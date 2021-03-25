using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Write_ReaderTxt
{
    public class DHL_Baby_Write
    {
        //public void dhl_Write(string path, string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(@"select 
        //                          nvl('','1') AS AA,
        //                          RPAD(nvl('',' '),6,' ') as LK,
        //                          nvl('','SPS 2.03') AS CC,
        //                          nvl('','SPS') AS DD,
        //                          '' AS  EE,
        //                          '' AS  FF,
        //                          '' AS  GG,
        //                          '' AS  HH,
        //                          a.MAWB AS MAWB,
        //                          a.HAWB AS HAWB,
        //                          '' AS  II,
        //                          '' AS  JJ,
        //                          '' AS  KK,
        //                          A.ACCOUT_NUM AS ACCOUT_NUM,
        //                          A.SHIPER_CORP_NAME AS SHIPER_CORP_NAME,
        //                          A.SHIPER_CONTACT_NAME AS SHIPER_CONTACT_NAME,
        //                          A.SHIPER_ADDRESS1 AS SHIPER_ADDRESS1,
        //                          A.SHIPER_ADDRESS2 AS SHIPER_ADDRESS2,
        //                          A.SHIPER_ADDRESS3 AS SHIPER_ADDRESS3,
        //                          A.SHIPER_CITY AS SHIPER_CITY,
        //                          A.SHIPER_STATE_PROVINCE AS SHIPER_STATE_PROVINCE,
        //                          A.SHIPER_POSTCODE AS SHIPER_POSTCODE,
        //                          A.SHIPER_COUNTRY AS SHIPER_COUNTRY,
        //                          A.SHIPER_COUNTRY_NAME AS SHIPER_COUNTRY_NAME,
        //                          A.AC_DN AS AC_DN,
        //                          NVL('','+86-2112344 Ext:1426') AS  WW,
        //                          '' AS  XX,
        //                          '' AS  YY,
        //                          '' AS  ZZ,
        //                          '' AS  AB,
        //                          nvl('','N') AS ZA,
        //                          '' AS ZB,
        //                          '' AS  AC,
        //                          A.ST_COMPANY AS ST_COMPANY,
        //                          A.ST_NAME AS ST_NAME,
        //                          A.ST_ADDR1 AS ST_ADDR1,
        //                          A.ST_ADDR2 AS ST_ADDR2,
        //                          A.ST_ADDR3 AS ST_ADDR3,
        //                          A.ST_CITY AS ST_CITY,
        //                          A.ST_PROVINCE AS ST_PROVINCE,
        //                          A.ST_POSTAL AS ST_POSTAL,
        //                          A.ST_COUNTRY_CODE AS ST_COUNTRY_CODE,
        //                          NVL('','TW') AS TW,
        //                          A.RECE_C_TELE AS RECE_C_TELE,
        //                          '' AS  AN,
        //                          '' AS  AO,
        //                          '' AS  AP,
        //                          '' AS  AQ,
        //                          nvl('','N') AS  AR,
        //                          '' AS  ZC,
        //                          '' AS  AU,
        //                          nvl('','B') AS  AV,
        //                          nvl('','B') AS ZD,
        //                          to_char(A.SHIP_DATE,'YYYYMMDD') AS SHIP_DATE,
        //                          to_char(A.SHIP_DATE,'HHMM') AS SHIP_DATE,
        //                          '' AS  AW,
        //                          '' AS  AX,
        //                          '1' AS TOTAL_COUNT,
        //                          nvl('','Y') AS ZF,
        //                          A.DN_TOTAL_WEIGHT AS DN_TOTAL_WEIGHT,
        //                          '' AS  AZ,
        //                          '' AS  BA,
        //                          '' AS  BC,
        //                          '' AS  YA,
        //                          nvl('','0') AS ZG,
        //                          '' AS  BD,
        //                          '' AS  BE,
        //                          '' AS  BF,
        //                          '' AS  BG,
        //                          '' AS  BH,
        //                          '' AS  BI,
        //                          '' AS  BJ,
        //                          '' AS  BK,
        //                          '' AS  BL,
        //                          '' AS  BM,
        //                          '' AS  BN,
        //                          '' AS  BO,
        //                          '' AS  BP,
        //                          '' AS  YB,
        //                          nvl('','N') AS ZH,
        //                          '' AS  BQ,
        //                          nvl('','0') AS ZI,
        //                          '' AS  BR,
        //                          '' AS  BS,
        //                          '' AS  BT,
        //                          nvl('','N') AS ZJ,
        //                           '' AS  BU,
        //                          nvl('','0') AS ZK,  
        //                          '' AS  BV,
        //                          '' AS  BW,
        //                          '' AS  BX,
        //                          '' AS  BZ,
        //                          '' AS  CA,
        //                          '' AS  CB,
        //                          '' AS  CD,
        //                           nvl('','2') AS ZL,
        //                          '' AS YC,
        //                          A.DN_SHIP_CONTENT AS DN_SHIP_CONTENT,
        //                          nvl('','0') AS ZM,
        //                          nvl('','0') AS ZN,
        //                          nvl('','N') AS ZO,
        //                          '' AS  CE,
        //                          '' AS  CF,
        //                          '' AS  CG,
        //                          '' AS  CH,
        //                          '' AS  CI,
        //                          '' AS  YD,
        //                          '' AS  CJ,
        //                          '' AS  CK,
        //                          '' AS  CL,
        //                          '' AS  CM,
        //                          nvl('','3') AS ZP,
        //                          a.Ac_Dn AS AC_DN,
        //                          '' AS  CN,
        //                          '' AS  CO,
        //                          '' AS  CP,
        //                          '' AS  CQ,
        //                          '' AS  CR,
        //                          '1' AS CARTON_COUNT,
        //                          B.CONNOTE_NO AS CONNOTE_NO,
        //                          B.BOX_WEIGHT AS BOX_WEIGHT,
        //                          '' AS  CU,
        //                          '' AS  CW,
        //                          '' AS  CX,
        //                          '' AS  CY,
        //                          '' AS  CZ,
        //                          nvl('','KG') AS ZR,
        //                          B.AC_PN AS AC_PN,
        //                          '' AS  DA,
        //                          NVL('','PD00658001') AS  DB,
        //                          '' AS  DC,
        //                          nvl('','3') AS ZS,
        //                          NVL('','000010') AS  DE,
        //                           '' AS  DG,
        //                          B.CARTON_SEQUNECE||'/'||A.CARTON_COUNT AS CARTON_WW,
        //                          '' AS  DH,
        //                          '' AS  DI,
        //                          '' AS  DJ
        //                               from ppsuser.ict_lps_header a left join ppsuser.ict_lps_line b on a.msg_id=b.msg_id WHERE a.msg_id='112'");
        //    DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
        //    //string data = string.Empty;
        //    StringBuilder data = new StringBuilder();
        //    int i = 1;
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataColumn dc in ds.Tables[0].Columns)
        //        {
        //            if (dc.ToString().ToUpper().Equals("LK"))
        //            {
        //                data.Append(ds.Tables[0].Rows[0][dc].ToString() + "\r\n");
        //            }else
        //            {
        //                i++;
        //                data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
        //            }
                    
        //            //data += ds.Tables[0].Rows[0][dc].ToString() + "|";

                    
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

        public void dhl_Write(string DN ,string DN_line,string path, string fileName,string cartonNo)
        {
            StringBuilder sb = new StringBuilder();//a.mawb||mod(a.MAWB,7)
            sb.Append(@"select 
                                  nvl('','1') AS AA,
                                  RPAD(nvl('',' '),6,' ') as LK,
                                  nvl('','SPS 2.03') AS CC,
                                  nvl('','SPS') AS DD,
                                  '' AS  EE,
                                  '' AS  FF,
                                  '' AS  GG,
                                  '' AS  HH,
                                  a.mawb AS MAWB,
                                  a.HAWB AS HAWB,
                                  '' AS  II,
                                  '' AS  JJ,
                                  '' AS  KK,
                                  nvl(A.ACCOUT_NUM,'.') AS ACCOUT_NUM,
                                  nvl(A.SHIPER_CORP_NAME,'.') AS SHIPER_CORP_NAME,
                                  A.SHIPER_CONTACT_NAME AS SHIPER_CONTACT_NAME,
                                  nvl(A.SHIPER_ADDRESS1,'.') AS SHIPER_ADDRESS1,
                                  nvl(A.SHIPER_ADDRESS2,'') AS SHIPER_ADDRESS2,
                                  A.SHIPER_ADDRESS3 AS SHIPER_ADDRESS3,
                                  nvl(A.SHIPER_CITY,'.') AS SHIPER_CITY,
                                  nvl(A.SHIPER_STATE_PROVINCE,'.') AS SHIPER_STATE_PROVINCE,
                                  nvl(A.SHIPER_POSTCODE,'.') AS SHIPER_POSTCODE,
                                  nvl(A.SHIPER_COUNTRY_NAME,'.') AS SHIPER_COUNTRY_NAME,
                                  NVL(A.SHIPER_COUNTRY,'.') AS ST_COUNTRY_NAME,
                                  nvl(A.AC_DN,'.') AS AC_DN,
                                  nvl(A.SHIPER_TEL,'.') AS SHIPER_TEL,
                                  '' AS  XX,
                                  '' AS  YY,
                                  '' AS  ZZ,
                                  '' AS  AB,
                                  nvl('','N') AS ZA,
                                  '' AS ZB,
                                  '' AS  AC,
                                  nvl(A.ST_NAME,'.') AS ST_COMPANY,
                                  nvl(A.ST_NAME,'.') AS ST_NAME,
                                  nvl(A.ST_ADDR1,'.') AS ST_ADDR1,
                                  nvl(A.ST_ADDR2,'.') AS ST_ADDR2,
                                  A.ST_ADDR3 AS ST_ADDR3,
                                  nvl(A.ST_CITY,'.') AS ST_CITY,
                                  A.ST_PROVINCE AS ST_PROVINCE,
                                  A.ST_POSTAL AS ST_POSTAL,
                                  A.ST_COUNTRY_CODE AS ST_COUNTRY_CODE,
                                  A.ST_COUNTRY_CODE AS TW,
                                  A.RECE_C_TELE AS RECE_C_TELE,
                                  '' AS  AN,
                                  '' AS  AO,
                                  '' AS  AP,
                                  '' AS  AQ,
                                  nvl('','N') AS  AR,
                                  '' AS  ZC,
                                  '' AS  AU,
                                  nvl('','B') AS  AV,
                                  nvl('','B') AS ZD,
                                  to_char(A.SHIP_DATE,'YYYYMMDD') AS SHIP_DATE,
                                  to_char(A.SHIP_DATE,'HHMM') AS SHIP_DATE,
                                  '' AS  AW,
                                  '' AS  AX,
                                  A.CARTON_COUNT AS TOTAL_COUNT,
                                  nvl('','Y') AS ZF,
                                  A.DN_TOTAL_WEIGHT AS DN_TOTAL_WEIGHT,
                                  '' AS  AZ,
                                  '' AS  BA,
                                  '' AS  BC,
                                  '' AS  YA,
                                  nvl('','0') AS ZG,
                                  '' AS  BD,
                                  '' AS  BE,
                                  '' AS  BF,
                                  '' AS  BG,
                                  '' AS  BH,
                                  '' AS  BI,
                                  '' AS  BJ,
                                  '' AS  BK,
                                  '' AS  BL,
                                  '' AS  BM,
                                  '' AS  BN,
                                  '' AS  BO,
                                  '' AS  BP,
                                  '' AS  YB,
                                  nvl('','N') AS ZH,
                                  '' AS  BQ,
                                  nvl('','0') AS ZI,
                                  '' AS  BR,
                                  '' AS  BS,
                                  '' AS  BT,
                                  nvl('','N') AS ZJ,
                                   '' AS  BU,
                                  nvl('','0') AS ZK,  
                                  '' AS  BV,
                                  '' AS  BW,
                                  '' AS  BX,
                                  '' AS  BZ,
                                  '' AS  CA,
                                  '' AS  CB,
                                  '' AS  CD,
                                   nvl('','4') AS ZL,
                                  '' AS TTT,
                                  B.DN_SHIP_CONTENT AS DN_SHIP_CONTENT,
                                  '' AS  RRE,
                                  '' AS  QWE,
                                  nvl('','0') AS ZM,
                                  nvl('','0') AS ZN,
                                  nvl('','N') AS ZO,
                                  '' AS  CE,
                                  '' AS  CF,
                                  '' AS  CG,
                                  '' AS  CH,
                                  '' AS  CI,
                                  '' AS  YD,
                                  '' AS  CJ,
                                  '' AS  CK,
                                  '' AS  CL,
                                  '' AS  CM,
                                  nvl('','3') AS ZP,
                                  nvl(a.Ac_Dn,'.') AS AC_DN,
                                  '' AS  CN,
                                  '' AS  CO,
                                  '' AS  CP,
                                  '' AS  CQ,
                                  '' AS  CR,
                                  '1' AS CARTON_COUNT,
                                  B.CONNOTE_NO AS CONNOTE_NO,
                                  TRUNC(B.BOX_WEIGHT,2) AS BOX_WEIGHT,
                                  '' AS  CU,
                                  '' AS  CW,
                                  '' AS  CX,
                                  '' AS  CY,
                                  '' AS  CZ,
                                  nvl('','KG') AS ZR,
                                  nvl(B.AC_PN,'.') AS AC_PN,
                                  '' AS  DA,
                                  NVL('','PD00658001') AS  DB,
                                  '' AS  DC,
                                  nvl('','3') AS ZS,
                                  B.CARTON_SEQUNECE AS DE,
                                  '' AS  DG,
                                  B.CARTON_SEQUNECE||'/'||A.CARTON_COUNT AS CARTON_WW,
                                  '' AS  DH,
                                  '' AS  DI,
                                  '' AS  DJ,
                                  '' AS  EEEE
                            from ppsuser.ict_lps_header a 
                            left join ppsuser.ict_lps_line b 
                              on a.shipment_id=b.shipment_id and a.ac_dn=b.ac_dn
                           WHERE  b.mother_child_tag = 'C' and a.AC_DN = '" + DN + "' and b.AC_DN_LINE='" + DN_line + "' ");
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sb.Append(" and carton_id = '" + cartonNo + "'");
            }
            sb.Append(" order by TO_NUMBER(b.carton_sequnece) desc NULLS LAST ");
            DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());//B.CARTON_SEQUNECE||'/'||A.CARTON_COUNT AS CARTON_WW,
            //string data = string.Empty;
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

                    //data += ds.Tables[0].Rows[0][dc].ToString() + "|";


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
