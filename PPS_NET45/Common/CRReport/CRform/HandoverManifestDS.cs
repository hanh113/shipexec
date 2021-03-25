using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class HandoverManifestDS
    {
        public HandoverManifestDS(string shipmentId)
        {
            setDataSoure(shipmentId);
        }

        private void setDataSoure(String shipmentId)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            //HYQ：----
            string headerSql = string.Format("select b.SHIPMENT_ID, "
                                            + "  '--' SHIP_POINT, "
                                            + "  b.CARRIER_NAME, "
                                            + "  B.POE POE_COC, "
                                            + "  b.shipping_time SHIP_DATE, "
                                            + "  (select count(c.DELIVERY_NO) "
                                            + "     from(select distinct(DELIVERY_NO) "
                                            + "             from ppsuser.t_order_info a "
                                            + "            where a.SHIPMENT_ID = '{0}') c) TOTAL_DN, "
                                            + "  B.QTY TOTAL_QTY, "
                                            + "  B.CARTON_QTY CARTON_NO "
                                      + "   from ppsuser.t_shipment_info b "
                                      + "  where b.SHIPMENT_ID = '{1}'", shipmentId, shipmentId);

            string lineSql = string.Format("SELECT DISTINCT a.SHIPMENT_ID,'--' MAWB,b.HAWB,a.DELIVERY_NO AC_DN, a.LINE_ITEM AC_DN_LINE, "
                                            + " a.MPN AC_PN, a.ASSIGN_QTY QTY, '--' PSI_REQUIRED, c.real_pallet_no END_PALLETNO "
                                            + " from ppsuser.t_pallet_order a, ppsuser.t_shipment_info b, PPSUSER.T_SHIPMENT_PALLET C "
                                            + " where a.SHIPMENT_ID = b.SHIPMENT_ID AND A.PALLET_NO = C.PALLET_NO "
                                            + " and a.shipment_id = c.shipment_id "
                                            + " AND b.SHIPMENT_ID = '{0}'", shipmentId);

            string palletNoSql = string.Format("SELECT SHIPMENT_ID, count(distinct pallet_no) AS PALLET_NO "
                                            + "  FROM ppsuser.t_shipment_pallet "
                                            + "  where shipment_id = '{0}' "
                                            + "  group by shipment_id", shipmentId);
            //包含在Header里面了
            //string cantonNoSql = @"select COUNT(DISTINCT input) as CARTON_NO  from ppsuser.g_ds_scandata_t where SHIPMENT_ID = '" + shipmentId + "'";

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(palletNoSql, "PalletNo"));
            Util.CreateDataTable(Constant.HandoverManifestDS_URL, ds);
        }
    }
}
