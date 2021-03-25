using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InPaShippingLabel
{
    public class APAC_Invoice
    {
        StringBuilder Invoicesb = new StringBuilder();
        StringBuilder invoiceList = new StringBuilder();
        public string getAPAC_Invoice(string so_no,DataTable invoiceDt, DataTable cartonsDt, string incotenms)
        {
            int cartons = cartonsDt == null ? 0 : cartonsDt.Rows.Count > 0 ? Convert.ToInt32(cartonsDt.Rows[0]["cartons"].ToString()) : 0;

            //获取InvoiceList列表的值
            string invoiceSQL = string.Format(@" select invoice_No ,ac_po, ac_pn ,ac_pn_desc,AC_PO_Line， sum(qty) as qty,model_no,unit_price  
                                                from  wmuser.AC_FD_APAC_CI_LINE@dgedi  where invoice_no = '{0}'
                                                group by  invoice_No ,ac_po, ac_pn ,ac_pn_desc,AC_PO_Line，model_no,unit_price  order by AC_PO,AC_PO_LINE,AC_PN ", invoiceDt.Rows[0]["invoice_No"].ToString());
            DataTable invoiceDtList = ClientUtils.ExecuteSQL(invoiceSQL).Tables[0];

            Invoicesb.Append(invoiceDt.Rows[0]["invoice_no"].ToString() + "|"
                                  + DateTime.Now.ToString("MM/dd/yyyy") + "|"
                                  + invoiceDt.Rows[0]["st_addr"].ToString() + "|"
                                  + invoiceDt.Rows[0]["hawb"].ToString() + "|"
                                  + "PVG" + "|"
                                  //+ invoiceDt.Rows[0]["poe"].ToString() + "|"
                                  + invoiceDt.Rows[0]["FINAL_DEST"].ToString() + "|"
                                  + DateTime.Now.ToString("MM-dd-yyyy") + "|"
                                  );
            int i = 1;
            int qty = 0;
            string model = string.Empty;
            double EXTENDED_PRICE_Total = 0.00;
            //double ROUGH_WEIGHT = 0.00;
            foreach (DataRow dr in invoiceDtList.Rows)
            {
                string weightSql = string.Format(@"select a.ICTPN,a.PRODUCT_WEIGHT,a.ROUGH_WEIGHT
                            FROM ppsuser.g_ds_partinfo_t a,ppsuser.g_shipping_detail b,sajet.sys_part c
                            WHERE  c.part_id = b.part_id
                            and a.ictpn = c.part_no and b.mpn = '{0}'", dr["ac_pn"].ToString());

                DataTable weightDt = ClientUtils.ExecuteSQL(weightSql).Tables[0];

                //ROUGH_WEIGHT += weightDt == null ? 0 : weightDt.Rows.Count > 0 ? (double.Parse(weightDt.Rows[0]["ROUGH_WEIGHT"].ToString()) * Convert.ToDouble(dr["qty"].ToString())) : 0;
                qty += int.Parse(dr["qty"].ToString());
                EXTENDED_PRICE_Total += double.Parse((Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(dr["qty"].ToString())).ToString("0.00"));
                if (string.Empty.Equals(model))
                {
                    model = dr["model_no"].ToString();
                }
                if (!model.Contains(dr["model_no"].ToString()))
                {
                    model = model + "," + dr["model_no"].ToString();
                }
                invoiceList.Append(dr["ac_pn"].ToString() + "|"
                + dr["ac_pn_desc"].ToString() + "|"
                + dr["invoice_no"].ToString() + "|"
                + dr["ac_po_line"].ToString() + "|"
                + " " + "|"
                + dr["ac_po"].ToString() + "|"
                + " " + "|"
                + dr["qty"].ToString() + "|"
                + double.Parse(dr["unit_price"].ToString()).ToString("0.00") + "|"
                 + (Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(dr["qty"].ToString())).ToString("0.00") + "|"
                );
                i++;
            }
            //Gross_Weight and Volume
            string gWeightSql = string.Format(@"select sum(a.allpallet_weight) as Gross_Weight,
                                                sum(a.Volume) as volume
                                                from 
                                                (select distinct t.so_no,t.allpallet_weight,t.pallet_length * t.pallet_width * t.pallet_height/1000000 as Volume
                                                from  ppsuser.g_palletedi_info t
                                                where t.so_no = '{0}') a
                                                group by a.so_no", so_no);

            DataTable gWeightDt = ClientUtils.ExecuteSQL(gWeightSql).Tables[0];

            //No.of Packages
            string noOfPackages = string.Format(@"select sum(aa.qty/dd.sn_qty) no_package        
                                                  from ppsuser.g_shipping_detail aa,
                                                       sajet.sys_part bb,ppsuser.g_ds_partinfo_t cc,ppsuser.g_ds_packinfo_t dd
                                                  where aa.invoiceno = '{0}'
                                                  and   aa.part_id = bb.part_id
                                                  and  bb.part_no = cc.ictpn
                                                  and  cc.pack_code = dd.pack_code", invoiceDt.Rows[0]["invoice_no"].ToString());

            DataTable noOfPackagesDt = ClientUtils.ExecuteSQL(noOfPackages).Tables[0];


            Invoicesb.Append(model + "|"
                        + double.Parse(gWeightDt.Rows[0]["Gross_Weight"].ToString()).ToString("0.00") + "|"
                        + double.Parse(gWeightDt.Rows[0]["volume"].ToString()).ToString("0.00") + "|"
                        + double.Parse(noOfPackagesDt.Rows[0]["no_package"].ToString()).ToString("0.00") + "|"
                        + EXTENDED_PRICE_Total.ToString("0.00") + "|"
                );
            Invoicesb.Append(invoiceList.ToString());
            return Invoicesb.ToString();
        }
    }
}
