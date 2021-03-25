using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Write_ReaderTxt
{
    public class SF_Write
    {
        //public void sf_Write(string path, string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(@"select h.HAWB as AWB,h.MAWB as MAWB , h.shiper_country as Shipper_Country, h.SHIPER_STATE_PROVINCE as Shipper_Province,
        //                h.shiper_city as Shipper_City,h.SHIPER_DISTRICT as Shipper_District,
        //                (case when l.AC_PLANT = 'VC50' then h.shiper_address3 else (h.shiper_address1||h.SHIPER_ADDRESS2) end)  as Shipper_Address,
        //                h.shiper_corp_name as Shipper_Company,h.Shiper_Contact_Name as Shipper_Name,h.SHIPER_TEL as Shipper_Tel, h.SHIPER_POSTCODE  as Shipper_Postal_Code,
        //                h.st_country_code as Consignee_Country , h.ST_PROVINCE as Consignee_Province,h.ST_CITY as Consignee_City,h.ST_DISTRICT as Consignee_District,
        //                h.ST_ADDR1 as Consignee_Add_Line_1,h.ST_ADDR2 as Consignee_Add_Line_2,h.ST_COMPANY as Consignee_Company,h.ST_NAME as Consignee_Name,
        //                h.RECE_C_TELE as Consignee_Tel,h.ST_POSTAL as Consignee_Postal_Code, '' as Commodity ,h.CARTON_COUNT as Total_Parcel_Quantity,
        //                h.DN_TOTAL_WEIGHT as Total_Actual_Weight, (l.CARTON_SEQUNECE || '/'||h.CARTON_COUNT) as Mother_Child_Tag,h.SERVICE_LVL_INDI as Service_Type,h.AC_DN as OrderID,
        //                l.AC_PLANT as Plant_Code,(case when  to_Number(h.TOT_COD) > 0  then 'Y' else 'N' end) as Is_COD,
        //                h.TOT_COD as COD_Amount,h.PAY_TYPE as Pay_Type ,to_char(h.SHIP_DATE,'yyyy-MM-dd HH24:mm') as Shipping_Time,h.DN_SHIP_CONTENT as c_omment,l.sscc as SSCCbarcode,
        //                l.sscc as SSCC,l.AC_PN as ApplePartNum , l.SERIAL_NUM as SerialNum , '' as OEMInfoLine  ,
        //                h.WO_NUM as AppleWebOrder,'' as Filler1,'' as Filler2, '' as Filler3
        //                from ppsuser.ict_lps_header h left join  ppsuser.ict_lps_line l on h.msg_id = l.msg_id");
        //    sb.Append("  where and l.AC_DN = 'CASE79186405' and l.AC_DN_LINE='000010'");
        //    DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
        //    //string data = string.Empty;
        //    StringBuilder data = new StringBuilder();
        //    int i = 1;
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach(DataColumn dc in ds.Tables[0].Columns)
        //        {
        //            i++;
        //            data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
        //            //if (dc.ToString().ToUpper().Equals("SHIPPING_TIME"))
        //            //{
        //            //    string shippingTime = DateTime.Parse(ds.Tables[0].Rows[0][dc].ToString()).ToString("yyyy-MM-dd HH:mm");
        //            //    data.Append(shippingTime + "|");
        //            //} 
        //            //else
        //            //{
        //            //    data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
        //            //}
                    
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

        public void sf_Write(string DN,string DN_LINE, string path, string fileName,string cartonNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select 
                        CASE l.CARTON_SEQUNECE WHEN '1' THEN   NVL(h.MAWB,'.')
                           ELSE  NVL(h.HAWB,'.') END as AWB ,
                        NVL(h.MAWB,'.') as MAWB , 
                        NVL(h.shiper_country,'.') as Shipper_Country, 
                        NVL(h.SHIPER_STATE_PROVINCE,'.') as Shipper_Province,
                        NVL(h.shiper_city,'.') as Shipper_City,
                        NVL(h.SHIPER_DISTRICT,'.') as Shipper_District, 
                        NVL(h.shiper_address1,'.')||h.SHIPER_ADDRESS2 as Shipper_Address,
                        NVL(h.shiper_corp_name,'.') as Shipper_Company,
                        NVL(h.Shiper_Contact_Name,'.') as Shipper_Name,
                        NVL(h.SHIPER_TEL,'.') as Shipper_Tel, 
                        NVL(h.SHIPER_POSTCODE,'.')  as Shipper_Postal_Code,
                        NVL(h.st_country_code,'.') as Consignee_Country , 
                        NVL(h.ST_PROVINCE,'.') as Consignee_Province,
                        NVL(h.ST_CITY,'.') as Consignee_City,
                        NVL(h.ST_DISTRICT,'.') as Consignee_District,
                        NVL(h.ST_ADDR1,'.') as Consignee_Add_Line_1,
                        NVL(h.ST_ADDR2,'.') as Consignee_Add_Line_2,
                        NVL(h.ST_COMPANY,'.') as Consignee_Company,
                        NVL(h.ST_NAME,'.') as Consignee_Name,
                        NVL(h.RECE_C_TELE,'.') as Consignee_Tel,
                        NVL(h.ST_POSTAL,'.') as Consignee_Postal_Code, 
                        '' as Commodity ,
                        h.CARTON_COUNT as Total_Parcel_Quantity,
                        h.DN_TOTAL_WEIGHT as Total_Actual_Weight, 
                        (l.CARTON_SEQUNECE || case when l.carton_sequnece is not null then '/'else '' end||h.CARTON_COUNT) as Mother_Child_Tag,
                        NVL(h.SERVICE_LVL_INDI,'.') as Service_Type,
                        NVL(h.AC_DN,'.') as OrderID,
                        NVL(l.AC_PLANT,'.') as Plant_Code,
                        (case when  to_Number(h.TOT_COD) > 0  then 'Y' else 'N' end) as Is_COD,
                        NVL(h.TOT_COD,'.') as COD_Amount,
                        NVL(h.PAY_TYPE,'.') as Pay_Type ,
                        to_char(h.SHIP_DATE,'yyyy-MM-dd HH24:mm') as Shipping_Time,h.DN_SHIP_CONTENT as c_omment,
                        NVL(l.sscc,'.') as SSCCbarcode,
                        NVL(l.sscc,'.') as SSCC,
                        NVL(l.AC_PN,'.') as ApplePartNum , 
                        NVL(l.SERIAL_NUM,'.') as SerialNum ,
                        '' as OEMInfoLine  ,
                        NVL(h.WO_NUM,'.') as AppleWebOrder,
                        '' as Filler1,
                        '' as Filler2, 
                        '' as Filler3
                        from ppsuser.ict_lps_header h left join  ppsuser.ict_lps_line l on h.msg_id = l.msg_id");
            sb.Append("  where  l.mother_child_tag = 'C' and  l.AC_DN = '" + DN + "' and l.AC_DN_LINE='" + DN_LINE + "' ");
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sb.Append(" and carton_id = '" + cartonNo + "'");
            }
            sb.Append(" order by TO_NUMBER(L.carton_sequnece) desc NULLS LAST");
            DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
            //string data = string.Empty;
            StringBuilder data = new StringBuilder();
            int i = 1;
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    i++;
                    data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
                    //if (dc.ToString().ToUpper().Equals("SHIPPING_TIME"))
                    //{
                    //    string shippingTime = DateTime.Parse(ds.Tables[0].Rows[0][dc].ToString()).ToString("yyyy-MM-dd HH:mm");
                    //    data.Append(shippingTime + "|");
                    //} 
                    //else
                    //{
                    //    data.Append(ds.Tables[0].Rows[0][dc].ToString() + "|");
                    //}

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
