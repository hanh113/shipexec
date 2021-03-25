using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Write_ReaderTxt
{
    public class YMT_Write
    {
        /// <summary>
        /// 写入txt 文件方法
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fileName">文件名</param>
        //public void ymt_Write(string path, string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    //sb.Append(@" select 
        //    //              line.Ac_Pn as AC_PN,
        //    //              line.tracking_no as TrackingNo,
        //    //              head.wo_num as WO_NUM,
        //    //              head.Ac_So as AC_SO,
        //    //              line.sscc as SSCC_BARCODE,
        //    //              line.serial_num as SERIAL_NUM,
        //    //              line.carton_id as CARTON_ID,
        //    //              head.Hawb as HAWB,
        //    //              head.st_country_code as ST_COUNTRY_CODE,
        //    //              head.St_Postal as ST_POSTAL,
        //    //              head.rece_c_tele as RECE_C_TELE,
        //    //              head.St_Addr1 as ST_ADDR1,
        //    //              head.st_addr2 as ST_ADDR2,
        //    //              head.st_addr3 as ST_ADDR3,
        //    //              head.st_name as ST_NAME,
        //    //              head.Shiper_Postcode as SHIPER_CPOSTCODE,
        //    //              head.shiper_tel as SHIPER_TEL,
        //    //              head.shiper_address1 as SHIPER_CADDRESS1,
        //    //              head.shiper_city as SHIPER_CCITY,
        //    //              head.shiper_state_province as SHIPER_STATE_PROVINCE,
        //    //              head.shiper_corp_name as SHIPER_CCORP_NAME,
        //    //              head.Accout_Num as ACCOUT_NUM,
        //    //              '' as CustomerBranchCode,
        //    //              head.ship_date as SHIP_DATE,
        //    //              '' as DesignateDeliveryDate,
        //    //              '' as DesignateforDeliveryTime,
        //    //              '' as HandlingInformation1,
        //    //              '' as HandlingInformation2,
        //    //              '' as SpecialInstruction,
        //    //              '' as SizeFlag,
        //    //              '' as GROSS_WEIGHT,
        //    //              '' as YamatoPackageSize,
        //    //              line.carton_inner_count as CARTON_INNER_COUNT,
        //    //              head.Carton_Count as CARTON_COUNT,
        //    //              '' as CartSeqNocorpondDN,
        //    //              '' as CoolFlag,
        //    //              '' as YamatoBranchcode,
        //    //              head.tot_cod as TOT_COD,
        //    //              '' as AmountofTAX,
        //    //              head.ship_payment as SHIP_PAYMENT
        //    //from ppsuser.ict_lps_header  head 
        //    //left join ppsuser.ict_lps_line  line on head.MSG_ID=line.MSG_ID ");

        //    // 第二种
        //    //sb.Append(@" select
        //    //                line.Ac_Pn as AC_PN,
        //    //                line.connote_no as CONNOTE_NO,
        //    //                head.wo_num as WO_NUM,
        //    //                head.Ac_Dn as AC_DN,
        //    //                line.sscc as SSCC_BARCODE,
        //    //                line.Ac_Pn as AC_PN,
        //    //                line.carton_id as CARTON_ID,
        //    //                head.Hawb as HAWB,
        //    //                head.st_country_code as ST_COUNTRY_CODE,
        //    //                head.St_Postal as ST_POSTAL,
        //    //                head.rece_c_tele as RECE_C_TELE,
        //    //                head.St_Addr1 as ST_ADDR1,
        //    //                head.st_addr2 as ST_ADDR2,
        //    //                head.st_addr3 as ST_ADDR3,
        //    //                '' as ConsigneeAddress4,
        //    //                head.st_name as ST_NAME,
        //    //                head.Shiper_Postcode as SHIPER_CPOSTCODE,
        //    //                head.shiper_tel as SHIPER_TEL,
        //    //                head.shiper_address1 as SHIPER_CADDRESS1,
        //    //                head.shiper_city as SHIPER_CCITY,
        //    //                head.shiper_state_province as SHIPER_STATE_PROVINCE,
        //    //                '' as ShipperAddress4,
        //    //                '' as ShipperName,
        //    //                head.Accout_Num as ACCOUT_NUM,
        //    //                '' as CustomerBranchCode,
        //    //                head.ship_date as SHIP_DATE,
        //    //                '' as DesignateDeliveryDate,
        //    //                '' as DesignateforDeliveryTime,
        //    //                '' as HandlingInformation1,
        //    //                '' as HandlingInformation2,
        //    //                '' as SpecialInstruction,
        //    //                '' as SizeFlag,
        //    //                '' as GROSS_WEIGHT,
        //    //                '' as YamatoPackageSize,
        //    //                line.carton_inner_count as CARTON_INNER_COUNT,
        //    //                head.Carton_Count as CARTON_COUNT,
        //    //                line.carton_sequnece as CARTON_SEQUNECE,
        //    //                '' as CoolFlag,
        //    //                '' as YamatoBranchcode,
        //    //                head.tot_cod as TOT_COD,
        //    //                head.tot_cod * 0.05 as AmountofTAX,
        //    //                case  head.tot_cod when '0' then '0' else '1' end
        //    //           from  ppsuser.ict_lps_header head
        //    //           left join ppsuser.ict_lps_line line on head.MSG_ID = line.MSG_ID ");

        //    //第三种
        //    sb.Append(@"  select
        //                    line.Ac_Pn as AC_PN,
        //                    line.connote_no as CONNOTE_NO,
        //                    head.wo_num as WO_NUM,
        //                    head.Ac_Dn as AC_DN,
        //                    line.sscc as SSCC_BARCODE,
        //                    line.Ac_Pn as AC_PN,
        //                    line.carton_id as CARTON_ID,
        //                    head.Hawb as HAWB,
        //                    head.st_country_code as ST_COUNTRY_CODE,
        //                    head.St_Postal as ST_POSTAL,
        //                    head.rece_c_tele as RECE_C_TELE,
        //                    head.St_Addr1 as ST_ADDR1,
        //                    head.st_addr2 as ST_ADDR2,
        //                    head.st_addr3 as ST_ADDR3,
        //                    '' as ConsigneeAddress4,
        //                    head.st_name as ST_NAME,
        //                    head.Shiper_Postcode as SHIPER_CPOSTCODE,
        //                    head.shiper_tel as SHIPER_TEL,
        //                    head.shiper_address1 as SHIPER_CADDRESS1,
        //                    head.shiper_city as SHIPER_CCITY,
        //                    head.shiper_state_province as SHIPER_STATE_PROVINCE,
        //                    '' as ShipperAddress4,
        //                    '' as ShipperName,
        //                    head.Accout_Num as ACCOUT_NUM,
        //                    '' as CustomerBranchCode,
                            
        //                    to_char(head.ship_date,'yyyymmdd') as SHIP_DATE,
        //                    '' as DesignateDeliveryDate,
        //                    '' as DesignateforDeliveryTime,
        //                    '' as HandlingInformation1,
        //                    '' as HandlingInformation2,
        //                    '' as SpecialInstruction,
        //                    nvl('','2') as SizeFlag,
        //                    '' as GROSS_WEIGHT,
        //                    nvl('','XX') as YamatoPackageSize,
        //                    line.carton_inner_count as CARTON_INNER_COUNT,
        //                    head.Carton_Count as CARTON_COUNT,
        //                    line.carton_sequnece as CARTON_SEQUNECE,
        //                    nvl('','0') as CoolFlag,
        //                    '' as YamatoBranchcode,
        //                    head.tot_cod as TOT_COD,
        //                    head.tot_cod * 0.05 as AmountofTAX,
        //                    case  head.tot_cod when '0' then '0' else '1' end
        //               from  ppsuser.ict_lps_header head
        //               left join ppsuser.ict_lps_line line on head.MSG_ID = line.MSG_ID  where line.MSG_ID=113 ");
           

        //    DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
        //    StringBuilder data = new StringBuilder();

        //    int i = 1;
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataColumn dc in ds.Tables[0].Columns)
        //        {
        //           i++;
        //           data.Append("\"" + ds.Tables[0].Rows[0][dc].ToString() + "\""+",");
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

        public void ymt_Write(string DN,string DN_Line,string path, string fileName,string cartonNo)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(@"  select
                            NVL(line.Ac_Pn,'.') as AC_PN,
                            NVL(line.connote_no,'.') as CONNOTE_NO,
                            NVL(head.wo_num,'.') as WO_NUM,
                            NVL(head.Ac_Dn,'.') as AC_DN,
                            NVL(line.sscc ,'.')as SSCC_BARCODE,
                            NVL(line.Ac_Pn,'.') as AC_PN,
                            NVL(line.carton_id,'.') as CARTON_ID,
                            NVL(head.Hawb ,'.')as HAWB,
                            NVL(head.st_country_code,'.') as ST_COUNTRY_CODE,
                            NVL(head.St_Postal,'.') as ST_POSTAL,
                            NVL(head.rece_c_tele,'.') as RECE_C_TELE,
                            NVL(head.St_Addr1,'.')  as ST_ADDR1,
                            NVL(head.st_addr2,'.') as ST_ADDR2,
                            NVL(head.st_addr3,'.') as ST_ADDR3,
                            '' as ConsigneeAddress4,
                            NVL(head.st_name,'.') as ST_NAME,
                            NVL(head.Shiper_Postcode,'.') as SHIPER_CPOSTCODE,
                            NVL(head.shiper_tel,'.') as SHIPER_TEL,
                            NVL(head.shiper_address1,'.') as SHIPER_CADDRESS1,
                            NVL(head.shiper_city,'.') as SHIPER_CCITY,
                            NVL(head.shiper_state_province,'.') as SHIPER_STATE_PROVINCE,
                            '' as ShipperAddress4,
                            '' as ShipperName,
                            NVL(head.Accout_Num,'.') as ACCOUT_NUM,
                            '' as CustomerBranchCode,
                            to_char(head.ship_date,'yyyymmdd') as SHIP_DATE,
                            '' as DesignateDeliveryDate,
                            '' as DesignateforDeliveryTime,
                            '' as HandlingInformation1,
                            '' as HandlingInformation2,
                            '' as SpecialInstruction,
                            nvl('','2') as SizeFlag,
                            '' as GROSS_WEIGHT,
                            nvl('','XX') as YamatoPackageSize,
                            line.carton_inner_count as CARTON_INNER_COUNT,
                            head.Carton_Count as CARTON_COUNT,
                            NVL(line.carton_sequnece,'.') as CARTON_SEQUNECE,
                            nvl('','0') as CoolFlag,
                            '' as YamatoBranchcode,
                            NVL(head.tot_cod,'') as TOT_COD,
                            head.tot_cod * 0.05 as AmountofTAX,
                            case  head.tot_cod when '0' then '0' else '1' end
                       from  ppsuser.ict_lps_header head
                       left join ppsuser.ict_lps_line line on head.MSG_ID = line.MSG_ID  where  line.mother_child_tag = 'C' and head.AC_DN = '" + DN + "' and line.AC_DN_LINE='" + DN_Line + "' ");
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sb.Append(" and carton_id = '" + cartonNo + "'");
            }
            sb.Append(" order by TO_NUMBER(line.carton_sequnece) desc NULLS LAST");

            DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
            StringBuilder data = new StringBuilder();

            int i = 1;
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    i++;
                    data.Append("\"" + ds.Tables[0].Rows[0][dc].ToString() + "\"" + ",");
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
