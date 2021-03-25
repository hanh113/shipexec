using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace InPaShippingLabel
{
    public class AMR_PackingList_New
    {
        public StringBuilder GetAMP_PackinglistStr(string so_no, DataTable dt2, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            try
            {


                int qty = 0;
                int total_cartons = 0;
                double product_weight = 0;
                double rough_weight = 0;
                //string palletSql = string.Format(@"select count(*) as pallets from (select count(a.sscc)
                //                                from ppsuser.g_shipping_detail a
                //                                where a.SO_NO = '{0}'
                //                                group by a.sscc) a ", so_no);




                string soSql = string.Format(@" select distinct t.trans_mode from wmuser.ac_tms_req_header@dgedi t,ppsuser.g_shipping_detail b
                                     where t.req_num = b.req_num and b.so_no = '{0}'", so_no);
                DataTable soDt = ClientUtils.ExecuteSQL(soSql).Tables[0];
                string incotenms = soDt == null ? "" : soDt.Rows.Count > 0 ? soDt.Rows[0]["trans_mode"].ToString() : "0";
                incotenms = GetIncoterms(incotenms);







                //for (int j = 0; j < dt.Rows.Count; j++)
                //{

                //group by b.so_no
                //获取总箱数、空箱数
                string total_cartonsSQL = string.Format(@"select sum(c.total_cartons)  as total_cartons,sum(c.empty_cartons) as empty_cartons
                                                            from (select distinct a.*
                                                            from  ppsuser.g_shipping_pallet_label_print a,ppsuser.g_shipping_detail_t b
                                                            where a.sscc = b.sscc and
                                                            b.so_no = '{0}' ) c
                                                        ", so_no);
                DataTable total_cartonstDt = ClientUtils.ExecuteSQL(total_cartonsSQL).Tables[0];
                total_cartons = int.Parse(total_cartonstDt.Rows[0]["total_cartons"].ToString());

                //获取--栈板数
                string palletSql = string.Format(@"select count(1) as pallets
                                                from 
                                                (
                                                select distinct t.sscc
                                                from   ppsuser.g_palletedi_info t
                                                where t.so_no = '{0}' 
                                                group by t.SSCC) a", so_no);
                DataTable palletDt = ClientUtils.ExecuteSQL(palletSql).Tables[0];
                //获取TOTAL_PALLETS
                int pallets = palletDt == null ? 0 : palletDt.Rows.Count > 0 ? Convert.ToInt32(palletDt.Rows[0]["pallets"].ToString()) : 0;

                //获取--毛重、体积
                string gross_WeightSQL = string.Format(@"select sum(a.allpallet_weight) as Gross_Weight,sum(a.Volume) as volume
                                                    from 
                                                    (select distinct t.so_no,t.allpallet_weight,t.pallet_length * t.pallet_width * t.pallet_height/1000000 as Volume
                                                    from  ppsuser.g_palletedi_info t,ppsuser.g_shipping_detail_t b
                                                    where t.so_no = '{0}') a
                                                    group by a.so_no", so_no);
                DataTable gross_WeightDt = ClientUtils.ExecuteSQL(gross_WeightSQL).Tables[0];

                //获取PacklingList列表的值
                string pl_lineSQL = string.Format(@"select invoice_No , ac_pn ,ac_pn_desc, sum(qty) as qty
                                                from wmuser.AC_FD_AMR_PL_LINE@dgedi a where invoice_no = '{0}'
                                                group by invoice_No , ac_pn ,ac_pn_desc order by AC_PN ", dt.Rows[0]["invoice_no"].ToString());
                DataTable pl_lineDt = ClientUtils.ExecuteSQL(pl_lineSQL).Tables[0];
                for (int i = 0; i < 10; i++)
                {
                    if (i < pl_lineDt.Rows.Count)
                    {
                        string weightSql = string.Format(@"select a.ICTPN,a.PRODUCT_WEIGHT,a.ROUGH_WEIGHT
                            FROM ppsuser.g_ds_partinfo_t a,ppsuser.g_shipping_detail b,sajet.sys_part c
                            WHERE  c.part_id = b.part_id
                            and a.ictpn = c.part_no and b.mpn = '{0}'", pl_lineDt.Rows[i]["ac_pn"].ToString());

                        DataTable weightDt = ClientUtils.ExecuteSQL(weightSql).Tables[0];

                        //取实际出货数量
                        string qtySql = string.Format(@"select a.mpn,sum(a.qty) qty
                                from ppsuser.g_shipping_detail_t a
                                where a.mpn = '{0}'
                                and a.so_no = '{1}'
                                group by a.mpn", pl_lineDt.Rows[i]["ac_pn"].ToString(), so_no);

                        DataTable invoiceqtyDtList = ClientUtils.ExecuteSQL(qtySql).Tables[0];


                        string PRODUCT_WEIGHT = weightDt == null ? "0" : weightDt.Rows.Count > 0 ? weightDt.Rows[0]["PRODUCT_WEIGHT"].ToString() : "0";
                        string ROUGH_WEIGHT = weightDt == null ? "0" : weightDt.Rows.Count > 0 ? weightDt.Rows[0]["ROUGH_WEIGHT"].ToString() : "0";

                        //qty += Convert.ToInt32(pl_lineDt.Rows[i]["qty"].ToString());

                        qty += Convert.ToInt32(invoiceqtyDtList.Rows[0]["qty"].ToString());

                        //product_weight += Convert.ToDouble(PRODUCT_WEIGHT) * Convert.ToUInt32(pl_lineDt.Rows[i]["qty"].ToString());
                        //rough_weight += Convert.ToDouble(ROUGH_WEIGHT) * Convert.ToUInt32(pl_lineDt.Rows[i]["qty"].ToString());

                        product_weight += Convert.ToDouble(PRODUCT_WEIGHT) * Convert.ToUInt32(invoiceqtyDtList.Rows[0]["qty"].ToString());
                        rough_weight += Convert.ToDouble(ROUGH_WEIGHT) * Convert.ToUInt32(invoiceqtyDtList.Rows[0]["qty"].ToString());

                        string empty_carton = string.Empty;
                        if (i == 0)
                        {
                            empty_carton = total_cartonstDt.Rows[0]["empty_cartons"].ToString();
                        }
                        sb.Append((i + 1) + "|"
                            + "Assembled in Vietnam" + "|"
                            + pl_lineDt.Rows[i]["ac_pn"].ToString() + "|"
                            + pl_lineDt.Rows[i]["ac_pn_desc"].ToString() + "|"
                           //+ pl_lineDt.Rows[i]["qty"].ToString() + "|"
                           + invoiceqtyDtList.Rows[0]["qty"].ToString() + "|"
                           //+ (Convert.ToDouble(PRODUCT_WEIGHT) * Convert.ToUInt32(pl_lineDt.Rows[i]["qty"].ToString())).ToString("F2") + "|"
                           //+ (Convert.ToDouble(ROUGH_WEIGHT) * Convert.ToUInt32(pl_lineDt.Rows[i]["qty"].ToString())).ToString("F2") + "|"

                           + (Convert.ToDouble(PRODUCT_WEIGHT) * Convert.ToUInt32(invoiceqtyDtList.Rows[0]["qty"].ToString())).ToString("F2") + "|"
                            + (Convert.ToDouble(ROUGH_WEIGHT) * Convert.ToUInt32(invoiceqtyDtList.Rows[0]["qty"].ToString())).ToString("F2") + "|"


                            + empty_carton + "|"
                            );
                    }
                    else
                    {
                        sb.Append("||||||||");
                    }
                }
                #region mpo 计算方式
                //string qtySQL = string.Format(@"select sum(h.qty/f.sn_qty) as total_cartons ,ceil(sum(h.qty)/f.total_qty) as pallets
                //                                from ppsuser.g_shipping_detail_t t,sajet.sys_part d,ppsuser.g_ds_partinfo_t e,ppsuser.g_ds_packinfo_t f, wmuser.AC_FD_AMR_PL_LINE@dgedi h
                //                                where t.so_no = '{0}'
                //                                and h.ac_po = '{1}'
                //                                and t.part_id = d.part_id
                //                                and d.part_no = e.ictpn
                //                                and t.invoiceno = h.invoice_no
                //                                and e.mpn = h.ac_pn
                //                                and e.pack_code = f.pack_code
                //                                group by  f.sn_qty,f.total_qty", so_no, dt2.Rows[j]["ac_po"].ToString());
                //DataTable qtyDt = ClientUtils.ExecuteSQL(qtySQL).Tables[0];
                //total_cartons = int.Parse(qtyDt.Rows[0]["total_cartons"].ToString());
                //pallets = int.Parse(qtyDt.Rows[0]["pallets"].ToString());
                #endregion
                //第一种总的重量计算
                rough_weight = double.Parse(gross_WeightDt.Rows[0]["Gross_Weight"].ToString());
                sb.Append(dt.Rows[0]["invoice_no"].ToString() + "|"
                    + DateTime.Now.ToString("yyyy/MM/dd") + "|"
                    + dt.Rows[0]["st_name"].ToString() + "|"
                    + dt.Rows[0]["hawb"].ToString() + "|"
                    //+ dt.Rows[0]["ac_po"].ToString() + "|"
                    + qty + "|"
                    + total_cartons + "|"
                    + product_weight.ToString("F2") + "|"
                    + rough_weight.ToString("F2") + "|"
                    + pallets + "|"
                    + dt.Rows[0]["carr_code"].ToString() + "|"
                    + dt.Rows[0]["ship_per"].ToString() + "|"
                    + dt.Rows[0]["st_addr"].ToString() + "|"
                    + incotenms + "|"
                    //+ (j + 1).ToString() + "|"
                    //+ dt.Rows.Count + "|"
                    );
                //    sb.Append("&");
                //}
                return sb;
            }
            catch (Exception ex)
            {
                string mes = ex.Message.ToString();
                return sb;
            }
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
        //01:Bulk Air
        //02:Bulk Motor
        //03:Bulk Rail
        //04:Bulk Ocean
        //10:Parcel Air
        //11:Parcel Motor
        //12:Parcel Rail
        //13:Parcel Ocean
    }
}
