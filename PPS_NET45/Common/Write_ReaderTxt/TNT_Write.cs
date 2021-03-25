using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace Write_ReaderTxt
{

    public class TNT_Write
    {
        //public void tnt_Write(string path, string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(@"select 
        //                        RPAD(nvl(A.hawb,' '),15,' ') as HAWB,
        //                        RPAD(nvl(a.shiper_contact_name,' '),9,' ') AS SHIPER_CCONTACT_NAME,
        //                        RPAD(nvl(a.shiper_corp_name ,' '),35,' ') AS SHIPER_CCORP_NAME,
        //                        RPAD(nvl(a.shiper_address1 ,' '),35,' ') AS SHIPER_CADDRESS1,
        //                        RPAD(nvl(a.shiper_address2 ,' '),35,' ') AS SHIPER_CADDRESS2,
        //                        RPAD(nvl(a.shiper_address3 ,' '),35,' ') AS SHIPER_CADDRESS3,
        //                        RPAD(nvl(a.shiper_postcode ,' '),9,' ') AS SHIPER_CPOSTCODE,
        //                        RPAD(nvl(a.shiper_city ,' '),35,' ') AS SHIPER_CCITY,
        //                        RPAD(nvl(a.shiper_state_province,' '),35,' ') AS SHIPER_STATE_PROVINCE,
        //                        RPAD(nvl(a.shiper_country ,' '),3,' ') AS SHIPER_CCOUNTRY,
        //                        RPAD(nvl('',' '),20,' ') AS AA,
        //                        RPAD(nvl('',' '),7,' ') AS BB,
        //                        RPAD(nvl(a.shiper_tel ,' '),9,' ') AS SHIPER_TEL,
        //                        RPAD(nvl('',' '),22,' ') AS CC,
        //                        RPAD(nvl('',' '),35,' ') AS DD,
        //                        RPAD(nvl('',' '),35,' ') AS EE,
        //                        RPAD(nvl('',' '),35,' ') AS FF,
        //                        RPAD(nvl('',' '),35,' ') AS GG,
        //                        RPAD(nvl('',' '),9,' ') AS HH,
        //                        RPAD(nvl('',' '),35,' ') AS II,
        //                        RPAD(nvl('',' '),35,' ') AS JJ,
        //                        RPAD(nvl('',' '),3,' ') AS KK,
        //                        RPAD(nvl('',' '),7,' ') AS LL,
        //                        RPAD(nvl('',' '),9,' ') AS MM,
        //                        RPAD(nvl('',' '),22,' ') AS NN,
        //                        RPAD(nvl('',' '),9,' ') AS OO,
        //                        RPAD(nvl(a.ST_NAME,' '),35,' ') AS ST_NAME,
        //                        RPAD(nvl(a.ST_ADDR1 ,' '),35,' ') AS ST_ADDR1,
        //                        RPAD(nvl(a.ST_ADDR2 ,' '),35,' ') AS ST_ADDR2,
        //                        RPAD(nvl(a.ST_ADDR3 ,' '),35,' ') AS ST_ADDR3,
        //                        RPAD(nvl(a.ST_POSTAL ,' '),9,' ') AS ST_POSTAL,
        //                        RPAD(nvl(a.ST_CITY ,' '),35,' ') AS ST_CITY,
        //                        RPAD(nvl(a.ST_PROVINCE ,' '),35,' ') AS ST_PROVINCE,
        //                        RPAD(nvl(a.ST_COUNTRY_CODE ,' '),3,' ') AS ST_COUNTRY_CODE,
        //                        RPAD(nvl(a.RECE_C_TELE ,' '),7,' ') AS RECE_C_TELE,
        //                        RPAD(nvl('','745'),9,' ') AS PP,
        //                        RPAD(nvl(a.RECE_CONTACT ,' '),22,' ') AS RECE_CONTACT,
        //                        RPAD(nvl('',' '),35,' ') AS QQ,
        //                        RPAD(nvl('',' '),35,' ') AS RR,
        //                        RPAD(nvl('',' '),35,' ') AS RS,
        //                        RPAD(nvl('',' '),35,' ') AS TT,
        //                        RPAD(nvl('',' '),9,' ') AS UU,
        //                        RPAD(nvl('',' '),35,' ') AS VV,
        //                        RPAD(nvl('',' '),35,' ') AS WW,
        //                        RPAD(nvl('',' '),3,' ') AS XX,
        //                        RPAD(nvl('',' '),7,' ') AS YY,
        //                        RPAD(nvl('',' '),9,' ') AS ZZ,
        //                        RPAD(nvl('',' '),22,' ') AS AB,
        //                        RPAD(nvl(a.ROUTE_CODE ,' '),3,' ') AS ROUTE_CODE,
        //                        RPAD(nvl('',' '),3,' ') AS AC,
        //                        RPAD(nvl('',' '),3,' ') AS AD,
        //                        RPAD(nvl('',' '),3,' ') AS AE,
        //                        RPAD(nvl('',' '),3,' ') AS AF,
        //                        RPAD(nvl('',' '),3,' ') AS AG,
        //                        LPAD(nvl(TO_CHAR(A.CARTON_COUNT) ,' '),5,'0') AS CARTON_COUNT,
        //                        LPAD(nvl(b.SEQUENCE_NUM ,' '),5,'0') AS SEQUENCE_NUM,
        //                        RPAD(nvl('',' '),20,' ') AS AH,
        //                        RPAD(nvl('',' '),10,' ') AS AI,
        //                        RPAD(nvl('',' '),3,' ') AS AJ,
        //                        RPAD(nvl('',' '),3,' ') AS AK,
        //                        RPAD(nvl('',' '),3,' ') AS AL,
        //                        RPAD(nvl('',' '),3,' ') AS AM,
        //                        RPAD(nvl('',' '),3,' ') AS AN,
        //                        RPAD(nvl(TO_CHAR(a.dn_total_weight) ,' '),4,' ') AS DN_TOTAL_WEIGHT,
        //                        RPAD(nvl('','660'),3,' ') AS AO,
        //                        RPAD(nvl('',' '),3,' ') AS AP,
        //                        RPAD(nvl('',' '),11,' ') AS AQ,
        //                        RPAD(nvl('',' '),2,' ') AS AR,
        //                        RPAD(nvl('',' '),3,' ') AS AU,
        //                        RPAD(nvl('','1345'),11,' ') AS AV,
        //                        RPAD(nvl('','14'),2,' ') AS AW,
        //                        RPAD(nvl('','USD'),3,' ') AS AX,
        //                        RPAD(nvl('',' '),11,' ') AS AY,
        //                        RPAD(nvl('',' '),2,' ') AS AZ,
        //                        RPAD(nvl('',' '),3,' ') AS BA,
        //                        RPAD(nvl('',' '),24,' ') AS BC,
        //                        RPAD(nvl(A.AC_DN ,' '),24,' ') AS AC_DN,
        //                        RPAD(nvl('',' '),22,' ') AS BD,
        //                        RPAD(nvl('',' '),30,' ') AS BE,
        //                        RPAD(nvl('',' '),30,' ') AS BF,
        //                        RPAD(nvl('','20'),2,' ') AS BG,
        //                        RPAD(nvl('','17'),2,' ') AS BH,
        //                        RPAD(nvl('','11'),2,' ') AS BI,
        //                        RPAD(nvl('','29'),2,' ') AS BJ,
        //                        RPAD(nvl('',' '),4,' ') AS BK,
        //                        RPAD(nvl('',' '),30,' ') AS BL,
        //                        RPAD(nvl('',' '),25,' ') AS BM,
        //                        RPAD(nvl('',' '),4,' ') AS BN,
        //                        RPAD(nvl('',' '),3,' ') AS BO,
        //                        RPAD(nvl('',' '),4,' ') AS BP,
        //                        RPAD(nvl('',' '),2,' ') AS BQ,
        //                        RPAD(nvl('','D'),1,' ') AS BR,
        //                        RPAD(nvl('',' '),5,' ') AS BS,
        //                        RPAD(nvl('',' '),5,' ') AS BT,
        //                        RPAD(nvl('',' '),3,' ') AS BU,
        //                        RPAD(nvl('',' '),1,' ') AS BV,
        //                        RPAD(nvl('',' '),1,' ') AS BW,
        //                        RPAD(nvl('',' '),5,' ') AS BX,
        //                        RPAD(nvl('',' '),50,' ') AS BZ,
        //                        RPAD(nvl('',' '),6,' ') AS CA,
        //                        RPAD(nvl('',' '),3,' ') AS CB,
        //                        RPAD(nvl('',' '),3,' ') AS CD,
        //                        RPAD(nvl('',' '),3,' ') AS CE,
        //                        RPAD(nvl('','020_015_Z0UP000A3_C02VQ0T4HV2T_5543'),35,' ') AS CF,
        //                        RPAD(nvl('','C0000000000000000000000'),23,' ') AS CG,
        //                        RPAD(nvl('',' '),6,' ') AS CH,
        //                        RPAD(nvl('',' '),1,' ') AS CI,
        //                        RPAD(nvl('',' '),2,' ') AS CJ,
        //                        RPAD(nvl('',' '),20,' ') AS CK,
        //                        RPAD(nvl(A.WO_NUM ,' '),16,' ') AS WO_NUM,
        //                        RPAD(nvl(A.SHIP_PAYMENT ,' '),1,' ') AS SHIP_PAYMENT,
        //                        RPAD(nvl('',' '),4,' ') AS CL,
        //                        RPAD(nvl(A.SHIP_CONDI_CODE ,' '),5,' ') AS SHIP_CONDI_CODE
        //                       from ppsuser.ict_lps_header a left join ppsuser.ict_lps_line b on a.msg_id=b.msg_id WHERE a.msg_id='111'");
        //    DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
        //    //string data = string.Empty;
        //    StringBuilder data = new StringBuilder();
        //    int i = 1;
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataColumn dc in ds.Tables[0].Columns)
        //        {
        //            i++;
        //            //data += ds.Tables[0].Rows[0][dc].ToString() + "|";
        //            data.Append(ds.Tables[0].Rows[0][dc].ToString());
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

        public void tnt_Write(string DN,string DN_LINE,string path,string fileName,string cartonNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select 
                                RPAD(nvl(b.connote_no,' '),15,' ') as HAWB,
                                RPAD(nvl(a.accout_num,' '),9,' ') AS SHIPER_CCONTACT_NAME,
                                nvl(a.shiper_corp_name,' ')||RPAD(' ', 35-(length(nvl(a.shiper_corp_name,' '))), ' ') as SHIPER_CCORP_NAME,
                                nvl(a.shiper_address1,' ')||RPAD(' ', 35-(length(nvl(a.shiper_address1,' '))), ' ') as SHIPER_CADDRESS1,
                                nvl(a.shiper_address2,' ')||RPAD(' ', 35-(length(nvl(a.shiper_address2,' '))), ' ') as SHIPER_CADDRESS2,
                                nvl(a.shiper_address3,' ')||RPAD(' ', 35-(length(nvl(a.shiper_address3,' '))), ' ') as SHIPER_CADDRESS3,
                                RPAD(nvl(a.shiper_postcode ,' '),9,' ') AS SHIPER_CPOSTCODE,
                                nvl(a.shiper_city,' ')||RPAD(' ', 35-(length(nvl(a.shiper_city,' '))), ' ') as SHIPER_CCITY,
                                nvl(a.shiper_state_province,' ')||RPAD(' ', 35-(length(nvl(a.shiper_state_province,' '))), ' ') as SHIPER_STATE_PROVINCE,
                                RPAD(nvl(a.shiper_country ,' '),3,' ') AS SHIPER_CCOUNTRY,
                                RPAD(nvl('',' '),20,' ') AS AA,
                                RPAD(nvl('',' '),7,' ') AS BB,
                                RPAD(nvl(a.shiper_tel ,' '),9,' ') AS SHIPER_TEL,
                                RPAD(nvl('',' '),22,' ') AS CC,
                                RPAD(nvl('',' '),35,' ') AS DD,
                                RPAD(nvl('',' '),35,' ') AS EE,
                                RPAD(nvl('',' '),35,' ') AS FF,
                                RPAD(nvl('',' '),35,' ') AS GG,
                                RPAD(nvl('',' '),9,' ') AS HH,
                                RPAD(nvl('',' '),35,' ') AS II,
                                RPAD(nvl('',' '),35,' ') AS JJ,
                                RPAD(nvl('',' '),3,' ') AS KK,
                                RPAD(nvl('',' '),7,' ') AS LL,
                                RPAD(nvl('',' '),9,' ') AS MM,
                                RPAD(nvl('',' '),22,' ') AS NN,
                                RPAD(nvl('',' '),9,' ') AS OO,
                                nvl(a.ST_NAME,' ')||RPAD(' ', 35-(length(nvl(a.ST_NAME,' '))), ' ') as ST_NAME,
                                nvl(a.ST_ADDR1,' ')||RPAD(' ', 35-(length(nvl(a.ST_ADDR1,' '))), ' ') as ST_ADDR1,
                                nvl(a.ST_ADDR2,' ')||RPAD(' ', 35-(length(nvl(a.ST_ADDR2,' '))), ' ') as ST_ADDR2,
                                nvl(a.ST_ADDR3,' ')||RPAD(' ', 35-(length(nvl(a.ST_ADDR3,' '))), ' ') as ST_ADDR3,
                                RPAD(nvl(a.ST_POSTAL ,' '),9,' ') AS ST_POSTAL,
                                nvl(a.ST_CITY,' ')||RPAD(' ', 35-(length(nvl(a.ST_CITY,' '))), ' ') as ST_CITY,
                                nvl(a.ST_PROVINCE,' ')||RPAD(' ', 35-(length(nvl(a.ST_PROVINCE,' '))), ' ') as ST_PROVINCE,
                                RPAD(nvl(a.ST_COUNTRY_CODE ,' '),3,' ') AS ST_COUNTRY_CODE,
                                RPAD(nvl(a.RECE_C_TELE ,' '),7,' ') AS RECE_C_TELE,
                                RPAD(nvl('','745'),9,' ') AS PP, 
                                nvl(a.RECE_CONTACT,' ')||RPAD(' ', 22-(length(nvl(a.RECE_CONTACT,' '))), ' ') as RECE_CONTACT,
                                RPAD(nvl('',' '),35,' ') AS QQ,
                                RPAD(nvl('',' '),35,' ') AS RR,
                                RPAD(nvl('',' '),35,' ') AS RS,
                                RPAD(nvl('',' '),35,' ') AS TT,
                                RPAD(nvl('',' '),9,' ') AS UU,
                                RPAD(nvl('',' '),35,' ') AS VV,
                                RPAD(nvl('',' '),35,' ') AS WW,
                                RPAD(nvl('',' '),3,' ') AS XX,
                                RPAD(nvl('',' '),7,' ') AS YY,
                                RPAD(nvl('',' '),9,' ') AS ZZ,
                                RPAD(nvl('',' '),22,' ') AS AB,
                                RPAD(nvl(a.ROUTE_CODE ,' '),3,' ') AS ROUTE_CODE,
                                RPAD(nvl('',' '),3,' ') AS AC,
                                RPAD(nvl('',' '),3,' ') AS AD,
                                RPAD(nvl('',' '),3,' ') AS AE,
                                RPAD(nvl('',' '),3,' ') AS AF,
                                RPAD(nvl('',' '),3,' ') AS AG,
                                LPAD(nvl(TO_CHAR(A.CARTON_COUNT) ,'0'),5,'0') AS CARTON_COUNT,
                                LPAD(nvl(b.SEQUENCE_NUM ,'0'),5,'0') AS SEQUENCE_NUM,
                                RPAD(nvl('',' '),20,' ') AS AH,
                                RPAD(nvl('',' '),10,' ') AS AI,
                                RPAD(nvl('',' '),3,' ') AS AJ,
                                RPAD(nvl('',' '),3,' ') AS AK,
                                RPAD(nvl('',' '),3,' ') AS AL,
                                RPAD(nvl('',' '),3,' ') AS AM,
                                RPAD(nvl('',' '),3,' ') AS AN,
                                RPAD(a.dn_total_weight,4,' ') AS DN_TOTAL_WEIGHT,
                                RPAD(nvl('','660'),3,' ') AS AO,
                                RPAD(nvl('',' '),3,' ') AS AP,
                                RPAD(nvl('',' '),11,' ') AS AQ,
                                RPAD(nvl('',' '),2,' ') AS AR,
                                RPAD(nvl('',' '),3,' ') AS AU,
                                RPAD(nvl('','1345'),11,' ') AS AV,
                                RPAD(nvl('','14'),2,' ') AS AW,
                                RPAD(nvl('','USD'),3,' ') AS AX,
                                RPAD(nvl('',' '),11,' ') AS AY,
                                RPAD(nvl('',' '),2,' ') AS AZ,
                                RPAD(nvl('',' '),3,' ') AS BA,
                                RPAD(nvl('',' '),24,' ') AS BC,
                                RPAD(nvl(A.AC_DN ,' '),24,' ') AS AC_DN,
                                RPAD(nvl('',' '),22,' ') AS BD,
                                RPAD(nvl('',' '),30,' ') AS BE,
                                RPAD(nvl('',' '),30,' ') AS BF,
                                substr(TO_CHAR(sysdate,'YYYY'),0,2) AS BG,
                                TO_CHAR(sysdate,'YY') AS BH,
                                TO_CHAR(sysdate,'MM') AS BI,
                                TO_CHAR(sysdate,'DD') AS BJ,
                                RPAD(nvl('',' '),4,' ') AS BK,
                                RPAD(nvl('',' '),30,' ') AS BL,
                                RPAD(nvl('',' '),25,' ') AS BM,
                                RPAD(nvl('',' '),4,' ') AS BN,
                                RPAD(nvl('',' '),3,' ') AS BO,
                                RPAD(nvl('',' '),4,' ') AS BP,
                                RPAD(nvl('',' '),2,' ') AS BQ,
                                RPAD(nvl('','D'),1,' ') AS BR,
                                RPAD(nvl('',' '),5,' ') AS BS,
                                RPAD(nvl('',' '),5,' ') AS BT,
                                RPAD(nvl('',' '),3,' ') AS BU,
                                RPAD(nvl('',' '),1,' ') AS BV,
                                RPAD(nvl('',' '),1,' ') AS BW,
                                RPAD(nvl('',' '),5,' ') AS BX,
                                RPAD(nvl('',' '),50,' ') AS BZ,
                                RPAD(nvl('',' '),6,' ') AS CA,
                                RPAD(nvl('',' '),3,' ') AS CB,
                                RPAD(nvl('',' '),3,' ') AS CD,
                                RPAD(nvl('',' '),3,' ') AS CE,
                                RPAD(nvl('','020_015_Z0UP000A3_C02VQ0T4HV2T_5543'),35,' ') AS CF,
                                RPAD(nvl('','C0000000000000000000000'),23,' ') AS CG,
                                RPAD(nvl('',' '),6,' ') AS CH,
                                RPAD(nvl('',' '),1,' ') AS CI,
                                RPAD(nvl('',' '),2,' ') AS CJ,
                                RPAD(nvl('',' '),20,' ') AS CK,
                                RPAD(nvl(A.WO_NUM ,' '),16,' ') AS WO_NUM,
                                RPAD(nvl(A.SHIP_PAYMENT ,' '),1,' ') AS SHIP_PAYMENT,
                                RPAD(nvl('',' '),4,' ') AS CL,
                                RPAD(nvl(A.SHIP_CONDI_CODE ,' '),5,' ') AS SHIP_CONDI_CODE
                               from ppsuser.ict_lps_header a left join ppsuser.ict_lps_line b on a.msg_id=b.msg_id WHERE b.mother_child_tag = 'C' and  b.AC_DN = '" + DN + "' and b.AC_DN_LINE='" + DN_LINE + "' ");
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sb.Append(" and carton_id = '" + cartonNo + "'");
            }
            sb.Append(" order by TO_NUMBER(b.carton_sequnece) desc NULLS LAST");
            DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
            //string data = string.Empty;
            StringBuilder data = new StringBuilder();
            int i = 1;
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    i++;
                    //data += ds.Tables[0].Rows[0][dc].ToString() + "|";
                    data.Append(ds.Tables[0].Rows[0][dc].ToString());
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
