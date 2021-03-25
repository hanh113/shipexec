using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InPaShippingLabel
{
    public class EMEA_Packlist
    {
        StringBuilder packlistsb = new StringBuilder();
        StringBuilder packlistList = new StringBuilder();
        public string getEMEA_Packlist(string so_no, DataTable packlistDt, DataTable cartonsDt, string incotenms, out string type)
        {
            int cartons = cartonsDt == null ? 0 : cartonsDt.Rows.Count > 0 ? Convert.ToInt32(cartonsDt.Rows[0]["cartons"].ToString()) : 0;

            string toSql = string.Format(@"select DISTINCT NVL(t.s_country_code, 'NL') tovalue
                                               from wmuser.ac_850_po_header @dgedi t where t.AC_PO = '{0}'", packlistDt.Rows[0]["ac_po"].ToString());

            DataTable to = ClientUtils.ExecuteSQL(toSql).Tables[0];

            //AE ship to
            string ST_ADDR = "";
            if (to.Rows[0]["tovalue"].ToString().Trim() == "AE")
            {
                string AEAddressSql = "select B.SHIP_LABLE_ADD_FORMAT AEADDRESS from WMUSER.ICT_3PL_LOCATION_MD@DGEDI B where b.poe_coc = 'AE'";
                DataTable AEADDress = ClientUtils.ExecuteSQL(AEAddressSql).Tables[0];
                ST_ADDR = AEADDress.Rows[0]["AEADDRESS"].ToString();
            }
            else
            {
                ST_ADDR = packlistDt.Rows[0]["ST_ADDR"].ToString();
            }

            // 

            packlistsb.Append(packlistDt.Rows[0]["invoice_no"].ToString() + "|"
                                  + DateTime.Now.ToString("yyyy/MM/dd") + "|"
                                  + packlistDt.Rows[0]["hawb"].ToString() + "|"
                                  + packlistDt.Rows[0]["poe_coc"].ToString() + "|"
                                  + ST_ADDR + "|"
                                  //+ packlistDt.Rows[0]["ST_ADDR"].ToString() + "|"
                                  + "AOE Hollyhill indl est Cork Ireland" + "|"
                                  //+ packlistDt.Rows[0]["SOLD_ADDR"].ToString() + "|"
                                  + "EXW" + "|"
                                  + packlistDt.Rows[0]["ship_per"].ToString() + "|"
                                  + to.Rows[0]["tovalue"].ToString() + "|"
                                  );

            string lineSql = string.Format(@" select distinct a.po_no,a.mpn,replace(c.pallet,'OF','-') pallet, 
                                                    b.start_carton || '-' || b.end_carton as cartons,
                                                    a.qty,
                                                    t.allpallet_weight,
                                                    d.product_weight,
                                                    d.rough_weight, b.start_carton
                                                    from ppsuser.g_shipping_detail_t a ,ppsuser.g_shipping_detail_cartons b, ppsuser.g_shipping_pallet_label_print c, ppsuser.g_palletedi_info t,ppsuser.g_ds_partinfo_t d,sajet.sys_part e
                                                    where
                                                    a.po_no = b.po_no 
                                                    and a.mix_pallets = b.mix_pallets
                                                    and   a.invoiceno = c.invoice
                                                    and   a.sscc = c.sscc
                                                    and  t.sscc = a.sscc
                                                    and  a.part_id = e.part_id
                                                    and  e.part_no = d.ictpn
                                                    and  a.so_no = '{0}'
                                                    order by pallet,to_number(b.start_carton),a.mpn", so_no);

            DataTable lineDt = ClientUtils.ExecuteSQL(lineSql).Tables[0];

            int i = 1;
            int qty = 0;
            double Gross_Weight_Total = 0.00;
            foreach (DataRow dr in lineDt.Rows)
            {
                string descSql =string.Format(@" select t.ac_pn_desc
                                    from wmuser.AC_FD_EMEIA_PL_LINE@dgedi t
                                    where t.invoice_no = '{0}'
                                    and   t.ac_po = '{1}'
                                    and   t.ac_pn = '{2}'", packlistDt.Rows[0]["invoice_no"].ToString(), dr["po_no"].ToString(), dr["mpn"].ToString());
                DataTable descDt = ClientUtils.ExecuteSQL(descSql).Tables[0];

                //取实际出货数量
                string qtySql = string.Format(@"select a.mpn,a.po_no,sum(a.qty) qty
                                from ppsuser.g_shipping_detail_t a
                                where a.po_no ='{0}' 
                                and a.mpn = '{1}'
                                and a.so_no = '{2}'
                                
                                group by a.mpn,a.po_no", lineDt.Rows[i - 1]["po_no"].ToString(), lineDt.Rows[i - 1]["mpn"].ToString(), so_no);

                DataTable invoiceqtyDtList = ClientUtils.ExecuteSQL(qtySql).Tables[0];

                Gross_Weight_Total += double.Parse(dr["rough_weight"].ToString()) * Convert.ToDouble(dr["qty"].ToString());
                double product_weight = double.Parse(dr["product_weight"].ToString()) * Convert.ToDouble(dr["qty"].ToString());
                double rough_weight = double.Parse(dr["rough_weight"].ToString()) * Convert.ToDouble(dr["qty"].ToString());
                qty += int.Parse(dr["qty"].ToString());
                //Gross_Weight_Total += double.Parse(dr["rough_weight"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString());
                //double product_weight = double.Parse(dr["product_weight"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString());
                //double rough_weight = double.Parse(dr["rough_weight"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString());
               // qty += int.Parse(invoiceqtyDtList.Rows[0]["qty"].ToString());

                packlistList.Append(dr["pallet"].ToString() + "|"
                + dr["cartons"].ToString() + "|"
                + dr["mpn"].ToString() + "|"
                + dr["po_no"].ToString() + "|"
                + descDt.Rows[0]["ac_pn_desc"].ToString() + "|"
                + "Assembled in Vietnam" + "|"
                + dr["qty"].ToString() + "|"
                //+ invoiceqtyDtList.Rows[0]["qty"].ToString() + "|"
                + product_weight.ToString("0.00") + "|"
                + rough_weight.ToString("0.00") + "|"
                + "/" + "|"
                );
                i++;
            }
            packlistsb.Append(qty.ToString() + "|"
                        + Gross_Weight_Total.ToString("0.00") + "|"
                );
            packlistsb.Append(packlistList.ToString());
            type = to.Rows[0]["tovalue"].ToString();
            return packlistsb.ToString();
        }

        private string GetIncoterms(string code)
        {
            string incoterms = string.Empty;
            switch (code)
            {
                case "01":
                    incoterms = "Bulk Air";
                    break;
                case "02":
                    incoterms = "Bulk Motor";
                    break;
                case "03":
                    incoterms = "Bulk Rail";
                    break;
                case "04":
                    incoterms = "Bulk Ocean";
                    break;
                case "10":
                    incoterms = "Parcel Air";
                    break;
                case "11":
                    incoterms = "Parcel Motor";
                    break;
                case "12":
                    incoterms = "Parcel Rail";
                    break;
                case "13":
                    incoterms = "Parcel Ocean";
                    break;
            }

            return incoterms;
        }
    }
}
