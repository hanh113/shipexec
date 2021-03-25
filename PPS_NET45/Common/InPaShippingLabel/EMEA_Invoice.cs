using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InPaShippingLabel
{
    public class EMEA_Invoice
    {
        StringBuilder Invoicesb = new StringBuilder();
        StringBuilder invoiceList = new StringBuilder();
        public string getEMEA_Invoice(DataTable invoiceDt, DataTable cartonsDt, string incotenms,string so_no, out string type)
        {
            int cartons = cartonsDt == null ? 0 : cartonsDt.Rows.Count > 0 ? Convert.ToInt32(cartonsDt.Rows[0]["cartons"].ToString()) : 0;

            string toSql = string.Format(@"select DISTINCT NVL(t.s_country_code, 'NL') tovalue
                                               from wmuser.ac_850_po_header @dgedi t where t.AC_PO = '{0}'", invoiceDt.Rows[0]["ac_po"].ToString());

            DataTable to = ClientUtils.ExecuteSQL(toSql).Tables[0];

            //AE ship to
            string ST_ADDR="";
            if (to.Rows[0]["tovalue"].ToString().Trim() == "AE")
            {
                string AEAddressSql = "select B.SHIP_LABLE_ADD_FORMAT AEADDRESS from WMUSER.ICT_3PL_LOCATION_MD@DGEDI B where b.poe_coc = 'AE'";
                DataTable AEADDress = ClientUtils.ExecuteSQL(AEAddressSql).Tables[0];
                ST_ADDR = AEADDress.Rows[0]["AEADDRESS"].ToString();
            }
            else
            {
                ST_ADDR = invoiceDt.Rows[0]["ST_ADDR"].ToString();
            }

            // 




            Invoicesb.Append(invoiceDt.Rows[0]["invoice_no"].ToString() + "|"
                                  + DateTime.Now.ToString("yyyy/MM/dd") + "|"
                                  + invoiceDt.Rows[0]["hawb"].ToString() + "|"
                                  + invoiceDt.Rows[0]["poe_coc"].ToString() + "|"
                                  + ST_ADDR + "|"
                                  //+ invoiceDt.Rows[0]["ST_ADDR"].ToString() + "|"
                                  + "AOE Hollyhill indl est Cork Ireland" + "|"
                                  //+ invoiceDt.Rows[0]["SOLD_ADDR"].ToString() + "|"
                                  + "EXW" + "|"
                                  + invoiceDt.Rows[0]["ship_per"].ToString() + "|"
                                  + to.Rows[0]["tovalue"].ToString() + "|"
                                  );
            int i = 1;
            int qty = 0;
            double EXTENDED_PRICE_Total = 0.00;
            foreach (DataRow dr in invoiceDt.Rows)
            {
                //取实际出货数量
                string qtySql = string.Format(@"select a.mpn,a.po_no,sum(a.qty) qty
                                from ppsuser.g_shipping_detail_t a
                                where a.po_no ='{0}' 
                                and a.mpn = '{1}'
                                and a.so_no = '{2}'
                                group by a.mpn,a.po_no", invoiceDt.Rows[i-1]["ac_po"].ToString(), invoiceDt.Rows[i-1]["ac_pn"].ToString(), so_no);

                DataTable invoiceqtyDtList = ClientUtils.ExecuteSQL(qtySql).Tables[0];

                //qty += int.Parse(dr["qty"].ToString());
                qty += int.Parse(invoiceqtyDtList.Rows[0]["qty"].ToString());
                //EXTENDED_PRICE_Total += double.Parse((Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(dr["qty"].ToString())).ToString("0.00"));
                EXTENDED_PRICE_Total += double.Parse((Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString())).ToString("0.00"));

                invoiceList.Append(i.ToString()+"|"
                + dr["ac_pn"].ToString() + "|"
                + dr["ac_po"].ToString() + "|"
                + dr["ac_pn_desc"].ToString() + "|"
                + "Assembled in Vietnam" + "|"
                //+ dr["qty"].ToString() + "|"
                + invoiceqtyDtList.Rows[0]["qty"].ToString() + "|"
                + double.Parse(dr["unit_price"].ToString()).ToString("0.00") + "|"
                // + (Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(dr["qty"].ToString())).ToString("0.00") + "|"
                 + (Convert.ToDouble(dr["unit_price"].ToString()) * Convert.ToDouble(invoiceqtyDtList.Rows[0]["qty"].ToString())).ToString("0.00") + "|"
                + "/" + "|"
                );
                i++;
            }

            Invoicesb.Append(qty.ToString() + "|"
                            + EXTENDED_PRICE_Total.ToString("0.00") + "|"
                );
            Invoicesb.Append(invoiceList.ToString());
            type = to.Rows[0]["tovalue"].ToString();
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
