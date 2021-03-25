using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Write_ReaderTxt
{
    public class UPS_Write
    {
        //public void ups_Write(string path, string fileName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(@"select h.MAWB as ship_id ,H.MAWB as trackinig_no, h.HAWB as Carton_Tracking_Number,to_char(h.SHIP_DATE,'yyyyMMdd') as ShipDate,
        //                h.ACCOUT_NUM as Shipper_UPS_Account_Number,h.SHIPER_CORP_NAME as Shipper_Name,h.shiper_address1 as Shipper_Address_Line_1,
        //                h.shiper_address2 as Shipper_Address_Line_2,h.shiper_address3 as Shipper_Address_Line_3,h.SHIPER_CITY as Shipper_City,
        //                h.SHIPER_STATE_PROVINCE as Shipper_State,h.SHIPER_POSTCODE as Shipper_Postal,h.SHIPER_COUNTRY as Shipper_Country,
        //                '' as Consignee_UPS_Account_number,h.ST_NAME as Apple_Consignee_Contact_Name,h.ST_COMPANY as Apple_Consignee_Company_Name,
        //                h.RECE_C_TELE as Consignee_Tel,h.ST_ADDR1 as Consignee_Add_Line_1 , h.ST_ADDR2 as Consignee_Add_Line_2,
        //                h.ST_ADDR3 as Consignee_Add_Line_3,h.ST_CITY as Consignee_City,h.ST_PROVINCE as Consignee_State_Code,h.ST_POSTAL as Consignee_Postal_Code,
        //                h.ST_COUNTRY_CODE as Consignee_Country_Code,l.CARTON_SEQUNECE as Package_Sequence,l.CARTON_INNER_COUNT as Total_Package_Count,
        //                h.DN_TOTAL_WEIGHT as Package_Actual_Weight,'' as Package_Dimensional_Weight , l.SSCC as SSCC18,h.AC_DN as DO,h.AC_SO as SO,l.AC_PO as PO,
        //                h.AC_ECPON as Webno,l.AC_PO_LINE as Linenumber,l.AC_PN as Partnumber,l.QTY as Quantity,l.SERIAL_NUM as Serial_No,'' as Delivery_Instruction,
        //                h.TOT_COD as Package_Price_Value , '' as Package_Price_Currency_Code ,'' as Package_Desc_1 ,h.HAWB as tracking_number,
        //                l.PALLET_ID as Pallet_Number, l.CARTON_ID as Box_Number
        //                from ppsuser.ict_lps_header h left join  ppsuser.ict_lps_line l on h.msg_id = l.msg_id");
        //    sb.Append(" where h.MSG_ID = '116' and l.mother_child_tag = 'C'");
        //    DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
        //    StringBuilder data = new StringBuilder();
        //    int i = 1;
        //    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataColumn dc in ds.Tables[0].Columns)
        //        {
        //            //SHIP_ID 规则转换
        //            if (dc.ToString().ToUpper().Equals("SHIP_ID"))
        //            {
        //                data.Append(i.ToString() + "," + getShipMentNumber(ds.Tables[0].Rows[0][dc].ToString()) + "\r\n");
        //            }
        //            ////时间日期转换
        //            //else  if (dc.ToString().ToUpper().Equals("SHIPDATE"))
        //            //{
        //            //    string shipDate = DateTime.Parse(ds.Tables[0].Rows[0][dc].ToString()).ToString("yyyyMMdd");
        //            //    data.Append(i.ToString() + "," + shipDate + "\r\n");
        //            //}
        //            else
        //            {
        //                data.Append(i.ToString() + "," + ds.Tables[0].Rows[0][dc].ToString() + "\r\n");
        //            }
                    
        //            i++;
        //        }
        //    }
        //    int j = i;
        //    string result = string.Empty;
        //    //if (!string.IsNullOrEmpty(data.ToString()))
        //    //{
        //    //    result = data.ToString().Substring(0, data.Length - 1);
        //    //}
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path);
        //    }
        //    WriteFile wf = new WriteFile();
        //    wf.writeFile(path, fileName, data.ToString());
        //}


        public string getShipMentNumber(string trackinig_No)
        {
            try
            {
                if (!string.IsNullOrEmpty(trackinig_No))
                {
                    _26Conversion_Table ct = new _26Conversion_Table();
                    //trackinig_No = "1Z1234566620754864";
                    trackinig_No = trackinig_No.Substring(2, trackinig_No.Length - 2);
                    string firstNum = trackinig_No.Substring(0, 6);
                    trackinig_No = trackinig_No.Substring(6, trackinig_No.Length - 6);
                    trackinig_No = trackinig_No.Substring(2, 7);

                    int postion1 = int.Parse(trackinig_No) / int.Parse(Math.Pow(26, 4).ToString());
                    int postion2 = (int.Parse(trackinig_No) - (postion1 * int.Parse(Math.Pow(26, 4).ToString()))) / int.Parse(Math.Pow(26, 3).ToString());
                    int postion3 = (int.Parse(trackinig_No) - (postion1 * int.Parse(Math.Pow(26, 4).ToString())) - (postion2 * int.Parse(Math.Pow(26, 3).ToString()))) / int.Parse(Math.Pow(26, 2).ToString());
                    int postion4 = (int.Parse(trackinig_No) - (postion1 * int.Parse(Math.Pow(26, 4).ToString())) - (postion2 * int.Parse(Math.Pow(26, 3).ToString())) - (postion3 * int.Parse(Math.Pow(26, 2).ToString()))) / 26;
                    int postion5 = (int.Parse(trackinig_No) - (postion1 * int.Parse(Math.Pow(26, 4).ToString())) - (postion2 * int.Parse(Math.Pow(26, 3).ToString())) - (postion3 * int.Parse(Math.Pow(26, 2).ToString())) - (postion4 * 26));
                    string result = firstNum + ct.ConvertConversion_Table(postion1) + ct.ConvertConversion_Table(postion2) + ct.ConvertConversion_Table(postion3) + ct.ConvertConversion_Table(postion4) + ct.ConvertConversion_Table(postion5);
                    return result;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception )
            {
                return "";
            }
        }

        public void ups_Write(string DN,string DN_Line, string path, string fileName,string cartonNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select 
                        NVL(h.MAWB,'.') as ship_id ,
                        NVL(H.MAWB,'.') as trackinig_no, 
                        NVL(L.CONNOTE_NO,'.') as Carton_Tracking_Number,
                        to_char(h.SHIP_DATE,'yyyyMMdd') as ShipDate,
                        NVL(h.ACCOUT_NUM,'.') as Shipper_UPS_Account_Number,
                        NVL(h.SHIPER_CORP_NAME,'.') as Shipper_Name,
                        NVL(h.shiper_address1,'.') as Shipper_Address_Line_1,
                        NVL(h.shiper_address2,'.') as Shipper_Address_Line_2,
                        NVL(h.shiper_address3,'.') as Shipper_Address_Line_3,
                        NVL(h.SHIPER_CITY,'.') as Shipper_City,
                        NVL(h.SHIPER_STATE_PROVINCE,'.') as Shipper_State,
                        NVL(h.SHIPER_POSTCODE,'.') as Shipper_Postal,
                        NVL(h.SHIPER_COUNTRY,'.') as Shipper_Country,
                        '' as Consignee_UPS_Account_number,
                        NVL(h.ST_NAME,'.') as Apple_Consignee_Contact_Name,
                        NVL(h.ST_COMPANY,'.') as Apple_Consignee_Company_Name,
                        NVL(h.RECE_C_TELE,'.') as Consignee_Tel,
                        NVL(h.ST_ADDR1,'.') as Consignee_Add_Line_1 , 
                        NVL(h.ST_ADDR2,'.') as Consignee_Add_Line_2,
                        NVL(h.ST_ADDR3,'.') as Consignee_Add_Line_3,
                        NVL(h.ST_CITY,'.') as Consignee_City,
                        NVL(h.ST_PROVINCE,'.') as Consignee_State_Code,
                        NVL(h.ST_POSTAL,'.') as Consignee_Postal_Code,
                        NVL(h.ST_COUNTRY_CODE,'.') as Consignee_Country_Code,
                        NVL(l.CARTON_SEQUNECE,'.') as Package_Sequence,
                        H.CARTON_COUNT as Total_Package_Count,
                        L.BOX_WEIGHT as Package_Actual_Weight,
                        '' as Package_Dimensional_Weight , 
                        NVL(l.SSCC,'.') as SSCC18,
                        NVL(h.AC_DN,'.') as DO,
                        '' as SO,
                        NVL(h.WO_NUM,'.') as PO,
                        NVL(h.AC_ECPON,'.') as Webno,
                        NVL(l.AC_PO_LINE,'.') as Linenumber,
                        NVL(l.AC_PN,'.') as Partnumber,
                        l.QTY as Quantity,
                        NVL(l.SERIAL_NUM,'.') as Serial_No,
                        '' as Delivery_Instruction,
                        --NVL(h.SHIPMENT_TOTAL_VALUE,'.') as Package_Price_Value , 
                        h.SHIPMENT_TOTAL_VALUE as Package_Price_Value , 
                        '' as Package_Price_Currency_Code ,
                        '' as Package_Desc_1 ,
                        NVL(h.HAWB,'.') as tracking_number,
                        NVL(l.PALLET_ID,'.') as Pallet_Number, 
                        NVL(l.CARTON_ID,'.') as Box_Number
                        from ppsuser.ict_lps_header h left join  ppsuser.ict_lps_line l on h.shipment_id = l.shipment_id  and h.ac_dn=l.ac_dn");
            sb.Append(" where l.mother_child_tag = 'C' and h.AC_DN = '" + DN + "' and l.AC_DN_LINE='" + DN_Line + "' ");
            if(!string.IsNullOrEmpty(cartonNo))
            {
                sb.Append(" and carton_id = '"+ cartonNo + "'");
            }
            sb.Append(" order by TO_NUMBER(L.carton_sequnece) desc NULLS LAST");
            DataSet ds = ClientUtils.ExecuteSQL(sb.ToString());
            StringBuilder data = new StringBuilder();
            int i = 1;
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    //SHIP_ID 规则转换
                    if (dc.ToString().ToUpper().Equals("SHIP_ID"))
                    {
                        data.Append(i.ToString() + "," + getShipMentNumber(ds.Tables[0].Rows[0][dc].ToString()) + "\r\n");
                    }
                    ////时间日期转换
                    //else  if (dc.ToString().ToUpper().Equals("SHIPDATE"))
                    //{
                    //    string shipDate = DateTime.Parse(ds.Tables[0].Rows[0][dc].ToString()).ToString("yyyyMMdd");
                    //    data.Append(i.ToString() + "," + shipDate + "\r\n");
                    //}
                    else
                    {
                        data.Append(i.ToString() + "," + ds.Tables[0].Rows[0][dc].ToString() + "\r\n");
                    }

                    i++;
                }
            }
            int j = i;
            string result = string.Empty;
            //if (!string.IsNullOrEmpty(data.ToString()))
            //{
            //    result = data.ToString().Substring(0, data.Length - 1);
            //}
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            WriteFile wf = new WriteFile();
            wf.writeFile(path, fileName, data.ToString());
        }
    }
}
