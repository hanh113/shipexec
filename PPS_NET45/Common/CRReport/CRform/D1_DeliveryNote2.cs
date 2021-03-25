using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class D1_DeliveryNote2 : Form
    {
        public D1_DeliveryNote2(string deliveryNo, string lineItem, string shipmentId, bool isPDF, string filePath)
        {
            Initialize(deliveryNo, lineItem, shipmentId, isPDF, filePath);
        }
        public void Initialize(string deliveryNo, string lineItem, string shipmentId, bool isPDF, string filePath)
        {

            #region  lineSql
            string lineSql = string.Format(@"  SELECT  decode(t9u.itemcustpo,
                                                         null,
                                                         t9u.custpono,
                                                         '',
                                                         t9u.custpono,
                                                         t9u.itemcustpo) as CustPoNo,
                                                  t9u.itemcustpoline ItemCustPoLine,
                                                  T9U.CUSTSONO CustSoNo,
                                                  TOI.DELIVERY_NO as DeliveryNo,
                                                  TOI.LINE_ITEM as LineItem,
                                                  TOI.MPN,
                                                  PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Delivery Note') AS PartDesc,
                                                  toi.qty Qty
                                             FROM PPSUSER.T_ORDER_INFO   TOI,
                                                  PPSUSER.T_940_UNICODE  T9U
                                            WHERE TOI.DELIVERY_NO =T9U.DELIVERYNO
                                            AND   TOI.LINE_ITEM = T9U.CUSTDELITEM
                                            AND   TOI.DELIVERY_NO='{0}'
                                            AND   TOI.SHIPMENT_ID = '{1}'
                                            order by toi.line_item", deliveryNo, shipmentId);
            #endregion

            #region headerSql
            string headerSql = string.Format(@"  SELECT distinct    T9U.DELIVERYNO,
                                                                    (SELECT distinct MLD.DATACONTENT
                                                                        FROM pptest.oms_lmd          ol,
                                                                            pptest.oms_lmd_overview olo,
                                                                            pptest.oms_lmd_data     mld
                                                                        where  t9u.deliverytype  =   ol.dntype
                                                                        and  t9u.shipcntycode  =  ol.country
                                                                        and  t9u.saleorgcode  =  ol.salesorg
                                                                        and  ol.sccode  = olo.sccode
                                                                        and mld.country = ol.country
                                                                        and mld.salesorg = ol.salesorg
                                                                        and mld.sccode = ol.sccode
                                                                        and  t9u.portofentry  = mld.poe
                                                                        and olo.datatype = mld.datatype
                                                                        and olo.lmdmode = 'PARCEL'
                                                                        and olo.document = 'DeliveryNote'
                                                                        and olo.item = 'Shipper'
                                                                        and olo.createlmd = 'Y'
                                                                        AND DECODE(MLD.CARRIERCODE,
                                                                                    '',
                                                                                    T9U.CARRIERCODE,
                                                                                    MLD.CARRIERCODE) = T9U.CARRIERCODE) as DataContent,
                                                                    t9u.weborderno as webOrderCode,
                                                                    t9u.carriercode as CarrierCode,
                                                                    ppsuser.t_get_deliverynote_mode('{2}') AS shipconditdesc,
                                                                    t9u.soldtoname,
                                                                    t9u.soldtocompany,
                                                                    t9u.shiptoname,
                                                                    t9u.shiptocompany,
                                                                    t9u.shiptoaddress,
                                                                    t9u.shiptoaddress2,
                                                                    t9u.shiptoaddress3,
                                                                    t9u.shiptoaddress4,
                                                                    t9u.shiptocity,
                                                                    DECODE(T9U.SHIPCNTYCODE,
                                                                            'AU',
                                                                            decode(instr(t9u.shiptostate, '=', 1, 1),
                                                                                    0,
                                                                                    t9u.shiptostate,
                                                                                    substr(t9u.shiptostate,
                                                                                            1,
                                                                                            instr(t9u.shiptostate, '=', 1, 1) - 1)),
                                                                            decode(instr(t9u.shiptostate, '=', 1, 1),
                                                                                    0,
                                                                                    t9u.shiptostate,
                                                                                    substr(t9u.shiptostate,
                                                                                            instr(t9u.shiptostate, '=', 1, 1) + 1))) as shiptostate,
                                                                    t9u.shiptozip,
                                                                    t9u.shiptocountry,
                                                                    t9u.shipinstruct,
                                                                    t9u.custwhinst,
                                                                    t9u.excustnote,
                                                                    t9u.excustnote1,
                                                                    (SELECT distinct TO_CHAR(tsi.shipping_time,
                                                                                                'dd-MON-yy',
                                                                                                'NLS_DATE_LANGUAGE=AMERICAN')
                                                                        FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_INFO TSI
                                                                        WHERE TSS.SHIPMENT_ID = TSI.SHIPMENT_ID
                                                                        AND TSS.DELIVERY_NO = T9U.DELIVERYNO) as shipDate,
                                                                    t9u.soldtoaddress,
                                                                    t9u.soldtoaddress3,
                                                                    t9u.soldtoaddress2,
                                                                    t9u.soldtocity,
                                                                    t9u.soldtoaddress4,
                                                                    DECODE(T9U.SHIPCNTYCODE,
                                                                            'AU',
                                                                            decode(instr(t9u.soldtostate, '=', 1, 1),
                                                                                    0,
                                                                                    t9u.soldtostate,
                                                                                    substr(t9u.soldtostate,
                                                                                            1,
                                                                                            instr(t9u.soldtostate, '=', 1, 1) - 1)),
                                                                            decode(instr(t9u.soldtostate, '=', 1, 1),
                                                                                    0,
                                                                                    t9u.soldtostate,
                                                                                    substr(t9u.soldtostate,
                                                                                            instr(t9u.soldtostate, '=', 1, 1) + 1))) as soldtostate,
                                                                    t9u.soldtozip,
                                                                    t9u.soldtocountry,
                                                                    (SELECT sum(toi.carton_qty)
                                                                        FROM PPSUSER.T_ORDER_INFO TOI
                                                                        where toi.delivery_no = '{0}'
                                                                        AND   TOI.SHIPMENT_ID = '{2}') as totalCartonQty,
                                                                    (SELECT sum(toi.qty)
                                                                        FROM PPSUSER.T_ORDER_INFO TOI
                                                                        where toi.delivery_no = '{0}'
                                                                        AND   TOI.SHIPMENT_ID = '{2}') as totalQty,
                                                                    (select round(sum(vmi.GROSSWEIGHTKG * toi.carton_qty), 2)
                                                                        from PPTEST.vw_mpn_info vmi, ppsuser.t_order_info toi
                                                                        where toi.ictpn = vmi.ICTPARTNO
                                                                        and toi.delivery_no = '{0}'
                                                                        AND TOI.SHIPMENT_ID = '{2}') || ' KGS' as totalWeight,
                                                                    (select round(sum(((vmi.CARTONWIDTHCM * vmi.CARTONLENGTHCM *
                                                                                        vmi.CARTONHEIGHTCM) / 1000000) *
                                                                                        toi.carton_qty),
                                                                                    2)
                                                                        from PPTEST.vw_mpn_info vmi, ppsuser.t_order_info toi
                                                                        where toi.ictpn = vmi.ICTPARTNO
                                                                        and toi.delivery_no = '{0}'
                                                                        AND TOI.SHIPMENT_ID = '{2}') as v_cube
                                                        FROM PPSUSER.T_940_UNICODE T9U,
                                                            PPTEST.OMS_CARRIER_TRACKING_PREFIX octp,
                                                            PPSUSER.T_ORDER_INFO TOI
                                                        WHERE T9U.DELIVERYNO = TOI.DELIVERY_NO
                                                        AND T9U.CUSTDELITEM = TOI.LINE_ITEM
                                                        AND T9U.CARRIERCODE = OCTP.CARRIERCODE
                                                        and octp.type = 'HAWB'
                                                        AND OCTP.SHIPMODE = TOI.TRANSPORT
                                                        AND TOI.DELIVERY_NO = '{0}'
                                                        AND TOI.LINE_ITEM = '{1}'
                                                        AND TOI.SHIPMENT_ID = '{2}'", deliveryNo, lineItem, shipmentId);
            #endregion
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = Util.getDataTaleC(headerSql, "HeadFile");
            dt2 = Util.getDataTaleC(lineSql, "M_line");
            ds.Tables.Add(Util.getDataTaleC(headerSql, "HeadFile"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "M_line"));
            if (isPDF)
            {
                Util.printPDFCrystalReportV2(Constant.PAC_DeliveryNote_URL2_ByGW, ds, filePath);
            }
            else
            {
                Util.CreateDataTable(Constant.PAC_DeliveryNote_URL2, ds);
            }

        }
        public D1_DeliveryNote2(string deliveryNo, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            Initialize2(deliveryNo, lineItem, shipmentId, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);
        }
        public void Initialize2(string deliveryNo, string lineItem, string shipmentId, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            dtCrystal = null;
            serverFullLabelName = string.Empty;
            //其他情况都是默认传进来的路径+***.rpt
            #region  lineSql
            string lineSql = string.Format(@"  SELECT  decode(t9u.itemcustpo,
                                                         null,
                                                         t9u.custpono,
                                                         '',
                                                         t9u.custpono,
                                                         t9u.itemcustpo) as CustPoNo,
                                                  t9u.itemcustpoline ItemCustPoLine,
                                                  T9U.CUSTSONO CustSoNo,
                                                  TOI.DELIVERY_NO as DeliveryNo,
                                                  TOI.LINE_ITEM as LineItem,
                                                  TOI.MPN,
                                                  PPSUSER.T_SHOW_NPI(T9U.DELIVERYNO,T9U.CUSTDELITEM,'Delivery Note') AS PartDesc,
                                                  toi.qty Qty
                                             FROM PPSUSER.T_ORDER_INFO   TOI,
                                                  PPSUSER.T_940_UNICODE  T9U
                                            WHERE TOI.DELIVERY_NO =T9U.DELIVERYNO
                                            AND   TOI.LINE_ITEM = T9U.CUSTDELITEM
                                            AND   TOI.DELIVERY_NO='{0}'
                                            AND   TOI.SHIPMENT_ID = '{1}'
                                            order by toi.line_item", deliveryNo, shipmentId);
            #endregion

            #region headerSql
            string headerSql = string.Format(@"  SELECT distinct    T9U.DELIVERYNO,
                                                                    (SELECT distinct MLD.DATACONTENT
                                                                        FROM pptest.oms_lmd          ol,
                                                                            pptest.oms_lmd_overview olo,
                                                                            pptest.oms_lmd_data     mld
                                                                        where  t9u.deliverytype  = ol.dntype
                                                                        and  t9u.shipcntycode  =  ol.country
                                                                        and  t9u.saleorgcode  = ol.salesorg
                                                                        and ol.sccode  = olo.sccode
                                                                        and mld.country = ol.country
                                                                        and mld.salesorg = ol.salesorg
                                                                        and mld.sccode = ol.sccode
                                                                        and  t9u.portofentry =  mld.poe
                                                                        and olo.datatype = mld.datatype
                                                                        and olo.lmdmode = 'PARCEL'
                                                                        and olo.document = 'DeliveryNote'
                                                                        and olo.item = 'Shipper'
                                                                        and olo.createlmd = 'Y'
                                                                        AND DECODE(MLD.CARRIERCODE,
                                                                                    '',
                                                                                    T9U.CARRIERCODE,
                                                                                    MLD.CARRIERCODE) = T9U.CARRIERCODE) as DataContent,
                                                                    t9u.weborderno as webOrderCode,
                                                                    t9u.carriercode as CarrierCode,
                                                                    ppsuser.t_get_deliverynote_mode('{2}') AS shipconditdesc,
                                                                    t9u.soldtoname,
                                                                    t9u.soldtocompany,
                                                                    t9u.shiptoname,
                                                                    t9u.shiptocompany,
                                                                    t9u.shiptoaddress,
                                                                    t9u.shiptoaddress2,
                                                                    t9u.shiptoaddress3,
                                                                    t9u.shiptoaddress4,
                                                                    t9u.shiptocity,
                                                                    DECODE(T9U.SHIPCNTYCODE,
                                                                            'AU',
                                                                            decode(instr(t9u.shiptostate, '=', 1, 1),
                                                                                    0,
                                                                                    t9u.shiptostate,
                                                                                    substr(t9u.shiptostate,
                                                                                            1,
                                                                                            instr(t9u.shiptostate, '=', 1, 1) - 1)),
                                                                            decode(instr(t9u.shiptostate, '=', 1, 1),
                                                                                    0,
                                                                                    t9u.shiptostate,
                                                                                    substr(t9u.shiptostate,
                                                                                            instr(t9u.shiptostate, '=', 1, 1) + 1))) as shiptostate,
                                                                    t9u.shiptozip,
                                                                    t9u.shiptocountry,
                                                                    t9u.shipinstruct,
                                                                    t9u.custwhinst,
                                                                    t9u.excustnote,
                                                                    t9u.excustnote1,
                                                                    (SELECT distinct TO_CHAR(tsi.shipping_time,
                                                                                                'dd-MON-yy',
                                                                                                'NLS_DATE_LANGUAGE=AMERICAN')
                                                                        FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_INFO TSI
                                                                        WHERE TSS.SHIPMENT_ID = TSI.SHIPMENT_ID
                                                                        AND TSS.DELIVERY_NO = T9U.DELIVERYNO) as shipDate,
                                                                    t9u.soldtoaddress,
                                                                    t9u.soldtoaddress3,
                                                                    t9u.soldtoaddress2,
                                                                    t9u.soldtocity,
                                                                    t9u.soldtoaddress4,
                                                                    DECODE(T9U.SHIPCNTYCODE,
                                                                            'AU',
                                                                            decode(instr(t9u.soldtostate, '=', 1, 1),
                                                                                    0,
                                                                                    t9u.soldtostate,
                                                                                    substr(t9u.soldtostate,
                                                                                            1,
                                                                                            instr(t9u.soldtostate, '=', 1, 1) - 1)),
                                                                            decode(instr(t9u.soldtostate, '=', 1, 1),
                                                                                    0,
                                                                                    t9u.soldtostate,
                                                                                    substr(t9u.soldtostate,
                                                                                            instr(t9u.soldtostate, '=', 1, 1) + 1))) as soldtostate,
                                                                    t9u.soldtozip,
                                                                    t9u.soldtocountry,
                                                                    (SELECT sum(toi.carton_qty)
                                                                        FROM PPSUSER.T_ORDER_INFO TOI
                                                                        where toi.delivery_no = '{0}'
                                                                        AND   TOI.SHIPMENT_ID = '{2}') as totalCartonQty,
                                                                    (SELECT sum(toi.qty)
                                                                        FROM PPSUSER.T_ORDER_INFO TOI
                                                                        where toi.delivery_no = '{0}'
                                                                        AND   TOI.SHIPMENT_ID = '{2}') as totalQty,
                                                                    (select round(sum(vmi.GROSSWEIGHTKG * toi.carton_qty), 2)
                                                                        from PPTEST.vw_mpn_info vmi, ppsuser.t_order_info toi
                                                                        where toi.ictpn = vmi.ICTPARTNO
                                                                        and toi.delivery_no = '{0}'
                                                                        AND TOI.SHIPMENT_ID = '{2}') || ' KGS' as totalWeight,
                                                                    (select round(sum(((vmi.CARTONWIDTHCM * vmi.CARTONLENGTHCM *
                                                                                        vmi.CARTONHEIGHTCM) / 1000000) *
                                                                                        toi.carton_qty),
                                                                                    2)
                                                                        from PPTEST.vw_mpn_info vmi, ppsuser.t_order_info toi
                                                                        where toi.ictpn = vmi.ICTPARTNO
                                                                        and toi.delivery_no = '{0}'
                                                                        AND TOI.SHIPMENT_ID = '{2}') as v_cube
                                                        FROM PPSUSER.T_940_UNICODE T9U,
                                                            PPTEST.OMS_CARRIER_TRACKING_PREFIX octp,
                                                            PPSUSER.T_ORDER_INFO TOI
                                                        WHERE T9U.DELIVERYNO = TOI.DELIVERY_NO
                                                        AND T9U.CUSTDELITEM = TOI.LINE_ITEM
                                                        AND T9U.CARRIERCODE = OCTP.CARRIERCODE
                                                        and octp.type = 'HAWB'
                                                        AND OCTP.SHIPMODE = TOI.TRANSPORT
                                                        AND TOI.DELIVERY_NO = '{0}'
                                                        AND TOI.LINE_ITEM = '{1}'
                                                        AND TOI.SHIPMENT_ID = '{2}'", deliveryNo, lineItem, shipmentId);
            #endregion
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = Util.getDataTaleC(headerSql, "HeadFile");
            dt2 = Util.getDataTaleC(lineSql, "M_line");
            ds.Tables.Add(Util.getDataTaleC(headerSql, "HeadFile"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "M_line"));

            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                if (!isCustom) //不是关务用默认值
                {
                    strCrystalFullPath = Constant.PAC_DeliveryNote_URL2;
                }
                else//isCustom如果是关务的是用宋体的模板 
                {
                    strCrystalFullPath = Constant.PAC_DeliveryNote_URL2_ByGW;
                }
            }

            dtCrystal = ds;

            if (!isOnlyGetDS)
            {

                if (isPDF)
                {

                    if (string.IsNullOrEmpty(strPDFPath))
                    {
                        strPDFPath = AppDomain.CurrentDomain.BaseDirectory + @"\PDF\CR_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".PDF";
                    }
                    Util.printPDFCrystalReportV3(strCrystalFullPath, ds, strPDFPath, out serverFullLabelName);
                }
                else
                {
                    //this.reportDocument1 = Util.CreateDataTable(strCrystalFullPath, ds, this.crystalReportViewer1);
                    Util.CreateDataTableADDcount(strCrystalFullPath, ds, nCopies, out serverFullLabelName);
                }
            }
            else
            {
                string reportPath = Util.checkCRReportVersion(strCrystalFullPath, out serverFullLabelName);
            }
        }
    }
}

