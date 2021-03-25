using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InPaShippingLabel
{
    public class AMR_Invoice
    {
        StringBuilder Invoicesb = new StringBuilder();
        StringBuilder invoiceList = new StringBuilder();
        public string getAMR_Invoice(DataTable invoiceDt, DataTable cartonsDt, int pallets, string incotenms, string so_no)
        {

            int cartons = cartonsDt == null ? 0 : cartonsDt.Rows.Count > 0 ? Convert.ToInt32(cartonsDt.Rows[0]["cartons"].ToString()) : 0;
            //int pallets = cartonsDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;

            string gross_WeightSQL = string.Format(@"select sum(a.allpallet_weight) as Gross_Weight,sum(a.Volume) as volume
                                                    from 
                                                    (select distinct t.so_no,t.allpallet_weight,t.pallet_length * t.pallet_width * t.pallet_height/1000000 as Volume
                                                    from  ppsuser.g_palletedi_info t
                                                    where t.so_no = '{0}') a
                                                    group by a.so_no", so_no);
            //总重量数
            DataTable gross_WeightDt = ClientUtils.ExecuteSQL(gross_WeightSQL).Tables[0];


            ///group by b.so_no
            string total_cartonsSQL = string.Format(@"select sum(c.total_cartons)  as total_cartons,sum(c.empty_cartons) as empty_cartons
                                                            from (select distinct a.*
                                                            from  ppsuser.g_shipping_pallet_label_print a,ppsuser.g_shipping_detail_t b
                                                            where a.sscc = b.sscc
                                                            and b.so_no = '{0}') c
                                                        ", so_no);
            DataTable total_cartonstDt = ClientUtils.ExecuteSQL(total_cartonsSQL).Tables[0];
            // 空号数
            string total_cartons = total_cartonstDt.Rows[0]["total_cartons"].ToString();


            string palletSql = string.Format(@"select count(1) as pallets
                                                from 
                                                (
                                                select distinct t.sscc
                                                from  ppsuser.g_palletedi_info t
                                                where t.so_no = '{0}'
                                                group by t.SSCC) a", so_no);
            DataTable palletDt = ClientUtils.ExecuteSQL(palletSql).Tables[0];
            //获取TOTAL_PALLETS
            pallets = palletDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;

            //获取InvoiceList列表的值
            string invoiceSQL = string.Format(@"select invoice_No ,ac_po, ac_pn ,ac_pn_desc, sum(qty) as qty,unit_price
                                                from wmuser.AC_FD_AMR_CI_LINE@dgedi a where invoice_no = '{0}'
                                                group by invoice_No ,ac_po, ac_pn ,ac_pn_desc，unit_price  order by AC_PO, AC_PN", invoiceDt.Rows[0]["invoice_No"].ToString());
            DataTable invoiceDtList = ClientUtils.ExecuteSQL(invoiceSQL).Tables[0];










            Invoicesb.Append(invoiceDt.Rows[0]["invoice_no"].ToString() + "|"
                            + invoiceDt.Rows[0]["hawb"].ToString() + "|"
                            + invoiceDt.Rows[0]["ac_pn"].ToString() + "|"
                            + "123" + "|"
                            + double.Parse (gross_WeightDt.Rows[0]["Gross_Weight"].ToString()).ToString("0.00") + "|"
                            + invoiceDt.Rows[0]["st_name"].ToString() + "|"
                            + invoiceDt.Rows[0]["st_addr"].ToString() + "|"
                            + invoiceDt.Rows[0]["sold_name"].ToString() + "|"
                            + total_cartons + "|"
                            + pallets + "|"
                            //+ "AMR_INVO_UNIT_PRICE_Total" + "|"
                            //+ "AMR_INVO_EXTENDED_PRICE_Total" + "|"
                            //+ invoiceDt.Rows[0]["ac_po"].ToString() + "|"
                            //+ invoiceDt.Rows[0]["ac_po"].ToString() + "|"
                            //+ invoiceDt.Rows[0]["unit_price"].ToString() + "|"
                            //+ invoiceDt.Rows[0]["qty"].ToString() + "|"
                            //+ (Convert.ToDouble(invoiceDt.Rows[0]["unit_price"].ToString()) * Convert.ToDouble(invoiceDt.Rows[0]["qty"].ToString())).ToString("0.00") + "|"  
                            );
            int i = 1;
            int qty = 0;
            double EXTENDED_PRICE_Total = 0.00;
            double PRODUCT_WEIGHT = 0.00;
            double ROUGH_WEIGHT = 0.00;
            foreach (DataRow dr in invoiceDtList.Rows)
            {
                string weightSql = string.Format(@"select a.ICTPN,a.PRODUCT_WEIGHT,a.ROUGH_WEIGHT
                            FROM ppsuser.g_ds_partinfo_t a,ppsuser.g_shipping_detail b,sajet.sys_part c
                            WHERE  c.part_id = b.part_id
                            and a.ictpn = c.part_no and b.mpn = '{0}'", dr["ac_pn"].ToString());
                DataTable weightDt = ClientUtils.ExecuteSQL(weightSql).Tables[0];

                string qtySql = string.Format(@"select a.mpn,a.po_no,sum(a.qty) qty
                                from ppsuser.g_shipping_detail_t a
                                where a.po_no ='{0}' 
                                and a.mpn = '{1}'
                                and a.so_no = '{2}'
                                group by a.mpn,a.po_no", dr["ac_po"].ToString(), dr["ac_pn"].ToString(), so_no);

                DataTable invoiceqtyDtList = ClientUtils.ExecuteSQL(qtySql).Tables[0];

                PRODUCT_WEIGHT += weightDt == null ? 0 : weightDt.Rows.Count > 0 ? (double.Parse(weightDt.Rows[0]["PRODUCT_WEIGHT"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString())) : 0;
                ROUGH_WEIGHT += weightDt == null ? 0 : weightDt.Rows.Count > 0 ? (double.Parse(weightDt.Rows[0]["ROUGH_WEIGHT"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString())) : 0;



                //PRODUCT_WEIGHT += weightDt == null ? 0 : weightDt.Rows.Count > 0 ? (double.Parse(weightDt.Rows[0]["PRODUCT_WEIGHT"].ToString()) * Convert.ToDouble(dr["qty"].ToString())) : 0;
                //ROUGH_WEIGHT += weightDt == null ? 0 : weightDt.Rows.Count > 0 ? (double.Parse(weightDt.Rows[0]["ROUGH_WEIGHT"].ToString()) * Convert.ToDouble(dr["qty"].ToString())) : 0;
                // qty += int.Parse(dr["qty"].ToString());
                qty += int.Parse(invoiceqtyDtList.Rows[0]["qty"].ToString());
                //EXTENDED_PRICE_Total += double.Parse((Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(dr["qty"].ToString())).ToString("0.00"));
                EXTENDED_PRICE_Total += double.Parse((Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString())).ToString("0.00"));

                invoiceList.Append(i.ToString() + "|"
                      + dr["ac_po"].ToString() + "|"
                                     + "China" + "|"
                     + dr["ac_pn"].ToString() + "|"
                + dr["ac_pn_desc"].ToString() + "|"
                //+ dr["qty"].ToString() + "|"
                + invoiceqtyDtList.Rows[0]["qty"].ToString() + "|"
                + Convert.ToDouble(dr["unit_price"].ToString()).ToString("0.00") + "|"
                //+ (Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(dr["qty"].ToString())).ToString("0.00") + "|"
                + (Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString())).ToString("0.00") + "|"
                );
                i++;
            }
        
            Invoicesb.Append(qty.ToString() + "|"
                        + EXTENDED_PRICE_Total.ToString("0.00") + "|"
                        + PRODUCT_WEIGHT.ToString("0.00") + "|"                       
                        + ROUGH_WEIGHT.ToString("0.00") + "|"                         //用上面一个毛重 这个没用
                        + invoiceDt.Rows[0]["Carr_code"].ToString() + "|"
                        + invoiceDt.Rows[0]["ship_per"].ToString() + "|"
                        + GetIncoterms(incotenms) + "|"
                        + invoiceDt.Rows[0]["POE"].ToString() + "|"
                );
            Invoicesb.Append(invoiceList.ToString());


            return Invoicesb.ToString();
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
