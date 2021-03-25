using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Write_ReaderTxt
{
    public class CNP_Write
    {
        //public void cnp_Write(string path, string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(@"select to_char(h.print_date,'yyyy')||'年'||to_char(h.print_date,'mm')||'月'||to_char(h.print_date,'dd')||'日',L.ac_dn as OrderID,H.carton_count as TotalNum,L.carton_sequnece as SeqNum,H.st_name as CusName,H.st_province as Province,H.st_city as City,H.st_district as District,H.st_company as CusComName,H.st_addr1 as CusAdd1,
        //    H.st_addr2 as CusAdd2,H.rece_c_tele as CusTel,H.st_postal as CusAddCode,H.st_country_code as ProInfo,L.Ac_Plant as SenCode,H.Dn_Total_Weight as SSCC,L.Sscc as SSCCbarcode,L.load_id as LOADID,L.pallet_id as PalletID,L.sequence_num as Sequence,L.ac_pn as ApplePartNum,L.serial_num as SerialNum,L.carton_id as CartonID,H.SHIPER_CONTACT_NAME as SenName,H.SHIPER_CORP_NAME as SenComName,H.shiper_address1 as SenAdd,H.shiper_tel as SenTel,H.shiper_postcode as SenCode,case when H.tot_cod > 0 then 2 else 0 end as MailType,H.tot_cod as ProdPrce,H.wo_num as AppleWebOrder,
        //      H.Dn_Total_Weight as ActualWeight,case when H.ship_condi_code = 'A8'then '' when  H.ship_condi_code = 'T1' then '定时派送：9点-12点' when H.ship_condi_code = 'T5' then '定时派送：12点-18点' when H.ship_condi_code = 'T4' then '定时派送：18点-21点' when H.ship_condi_code = 'S1' then '当日派送'end as Volume,H.hawb as EMSbarcode,H.poe as POE,'' as filler1,'' as filler2,'' as filler3
        //        from ppsuser.ict_lps_header H left join ppsuser.ict_lps_line L on H.msg_id = L.msg_id    where H.MSG_ID = 113");
        //    DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
        //    StringBuilder data = new StringBuilder();
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataColumn dc in ds.Tables[0].Columns)
        //        {
        //            string PrintDate = "";
        //            string timer = "";
        //            if (dc.ToString().ToUpper().Equals("PRINTDATE") && !string.IsNullOrEmpty(ds.Tables[0].Rows[0][dc].ToString()))
        //            {
        //                PrintDate = ds.Tables[0].Rows[0][dc].ToString();
        //                string[] times = PrintDate.Split('/');
        //                timer = times[0] + "年" + times[1] + "月" + times[2].ToString().Remove(2) + "日";
        //                data.Append(timer + "|");
        //            }
  
        //            else
        //            {
        //                data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
        //            }
        //        }
        //    }
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

        public void cnp_Write(string DN, string DN_LINE, string path ,string fileName,string cartonNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select to_char(h.print_date,'yyyy')||'年'||to_char(h.print_date,'mm')||'月'||to_char(h.print_date,'dd')||'日' as PrintDate,
                        NVL(H.ac_dn,'.') as OrderID,
                        H.carton_count as TotalNum,
                        NVL(L.carton_sequnece,'.') as SeqNum,
                        NVL(H.st_name,'.') as CusName,
                        NVL(H.st_province,'.') as Province,
                        NVL(H.st_city,'.') as City,
                        NVL(H.st_district,'.') as District,
                        NVL(H.st_company,'.') as CusComName,
                        NVL(H.st_addr1,'.') as CusAdd1,
                        NVL(H.st_addr2,'.') as CusAdd2,
                        NVL(H.rece_c_tele,'.') as CusTel,
                        NVL(H.st_postal,'.') as CusAddCode,
                        NVL(H.st_country_code,'.') as ProInfo,
                        NVL(L.Ac_Plant,'.') as SenCode,
                        H.Dn_Total_Weight as SSCC,
                        NVL(L.Sscc,'.') as SSCCbarcode,
                        NVL(L.load_id,'.') as LOADID,
                        NVL(L.pallet_id,'.') as PalletID,
                        NVL(L.sequence_num,'.') as Sequence,
                        NVL(L.ac_pn,'.') as ApplePartNum,
                        NVL(L.serial_num,'.') as SerialNum,
                        NVL(L.carton_id,'.') as CartonID,
                        NVL(H.SHIPER_CONTACT_NAME,'.') as SenName,
                        NVL(H.SHIPER_CORP_NAME,'.') as SenComName,
                        NVL(H.shiper_address1,'.') as SenAdd,
                        NVL(H.shiper_tel,'.')  as SenTel,
                        NVL(H.shiper_postcode,'.') as SenCode,
                        case when H.tot_cod > 0 then 2 else 0 end as MailType,
                        NVL(H.tot_cod,'.') as ProdPrce,
                        NVL(H.wo_num,'.') as AppleWebOrder,
                        H.Dn_Total_Weight as ActualWeight,
                        case when H.ship_condi_code = 'A8'then '' when  H.ship_condi_code = 'T1' then '定时派送：9点-12点' when H.ship_condi_code = 'T5' then '定时派送：12点-18点' when H.ship_condi_code = 'T4' then '定时派送：18点-21点' when H.ship_condi_code = 'S1' then '当日派送'end as Volume,
                        NVL(H.hawb,'.') as EMSbarcode,
                        NVL(H.poe,'.') as POE,
                        '' as filler1,
                        '' as filler2,
                        '' as filler3
                from ppsuser.ict_lps_header H left join ppsuser.ict_lps_line L on H.msg_id = L.msg_id  where  l.mother_child_tag = 'C' and  l.AC_DN = '" + DN + "' and l.AC_DN_LINE='" + DN_LINE + "' ");
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sb.Append(" and carton_id = '" + cartonNo + "'");
            }
            sb.Append(" order by TO_NUMBER(L.carton_sequnece) desc NULLS LAST");
            DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
            StringBuilder data = new StringBuilder();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    //if(dc.ToString().ToUpper().Equals("PRINTDATE"))
                    //{
                    //    string PrintDate = DateTime.Parse(ds.Tables[0].Rows[0][dc].ToString()).ToString("yyyyMMdd");
                    //        data.Append(i.ToString() + "," + shipDate + "\r\n");
                    //}
                    data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
                }
            }
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

