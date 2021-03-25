using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class FDHubGlobalLayoutForm
    {
        public string diskCompletePath = "";

        public FDHubGlobalLayoutForm(string ac_po, string so_no, string diskPath, bool print)
        {
            Initialize(ac_po, so_no, diskPath, print);
        }

        private void Initialize(String ac_po, string so_no, string diskPath, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            string headerSql = string.Format(@"SELECT distinct A.shipment_id AS SHIPID_TT,
                                                B.SUPPLIERNAME AS SUP_NAME1,
                                                B.SUPPLIERADDRESS1 AS SUP_ADDR1,
                                                B.SUPPLIERADDRESS2 AS SUP_ADDR2,
                                                B.SUPPLIERADDRESS3 AS SUP_ADDR3,
                                                B.SUPPLIERADDRESS4 AS SUP_ADDR4,
                                                B.SUPPLIERTEL AS SUP_TEL,
                                                A.SHIPTONAME1 AS ST_NAME1,
                                                A.SHIPTONAME2 AS ST_NAME2,
                                                A.SHIPTOADDRESS1 AS ST_ADDR1,
                                                A.SHIPTOCITY || ' ' || A.SHIPTOSTATE || ' ' || a.SHIPTOZIP AS ST_ADDR2,
                                                (select iso3.country
                                                   from ppsuser.iso3166_1 iso3
                                                  where iso3.cntycode2 = A.SHIPTOCOUNTRY) AS ST_ADDR3,
                                                A.HAWB,
                                                A.CARRIER_CODE AS CARR_CODE,
                                                A.TRANSPORT AS SHIP_PER,
                                              --  DECODE(A.invtype,'SOI','DAP, Freight Unpaid Ex Hub ' || A.SHIPTOCITY,'AOI','FAC,'||A.shiptocity||','||A.shiptocountry)     AS F_TERM,
                                                'EXW' AS F_TERM,
                                                A.DELIVERY_NO AS AC_PO
                                  FROM PPSUSER.VW_FD_DATA A, PPSUSER.T_FD_SUPPLIER B
                                 WHERE a.shipment_id = '{0}'
                                   AND A.DELIVERY_NO = '{1}'", so_no, ac_po);
            //       string MixCooFlag = ClientUtils.ExecuteSQL(string.Format(@" select coo  from ppsuser.t_shipment_pallet
            //where shipment_id = '{0}'", so_no)).Tables[0].Rows[0][0].ToString();
            //       //修改COO取值逻辑 edit by franky 2020/3/13
            //       string lineSql = MixCooFlag.Equals("MIX") ?
            //                                           string.Format(@"     SELECT distinct round(SUM(B.FGWEIGHTKG * A.qty), 2) AS Net_Weight,
            //                                            round(SUM(B.GROSSWEIGHTKG * A.CARTON_QTY), 2) AS Gross_Weight,
            //                                            A.DELIVERY_NO AS AC_PO,
            //                                            A.LINE_ITEM AS AC_PO_LINE,
            //                                            A.MPN AS AC_PN,
            //                                            PPSUSER.T_SHOW_NPI(A.DELIVERY_NO,
            //                                                               a.Line_Item,
            //                                                               'Hub Packing List') AS ac_pn_desc,
            //                                            SUM(A.QTY) AS QTY,
            //                                            'Assembled in ' || ppsuser.f_transform_coo(TOC.COO, 1) as COO,
            //                                            a.shipment_id AS SHIPID_TT
            //                              FROM PPSUSER.T_ORDER_INFO A
            //                             INNER JOIN PPTEST.VW_MPN_INFO B
            //                                ON A.ICTPN = B.ICTPARTNO
            //                             inner join PPSUSER.T_FD_ORDER_DETAIL tod
            //                                on a.delivery_no = tod.ac_po
            //                               and a.line_item = tod.ac_po_line
            //                               and a.freightorder = tod.freightorder
            //                             inner join PPSUSER.T_ORDER_COO TOC
            //                                ON A.DELIVERY_NO = TOC.DN_NO
            //                               AND A.LINE_ITEM = TOC.DN_LINE
            //                               AND A.ICTPN = TOC.PART_NO
            //                             WHERE A.DELIVERY_NO = '{0}'
            //                               and a.shipment_id = '{1}'
            //                             GROUP BY A.DELIVERY_NO,
            //                                      A.LINE_ITEM,
            //                                      A.MPN,
            //                                      a.shipment_id,
            //                                      tod.ac_pn_desc,
            //                                      TOC.COO
            //                           ", ac_po, so_no) :
            string lineSql = string.Format(@"      SELECT distinct round(SUM(B.FGWEIGHTKG * tpo.assign_qty), 2) AS Net_Weight,
                                                 round(SUM(B.GROSSWEIGHTKG *  tpo.assign_carton), 2) AS Gross_Weight,
                                                 A.DELIVERY_NO AS AC_PO,
                                                 A.LINE_ITEM AS AC_PO_LINE,
                                                 A.MPN AS AC_PN,
                                                 PPSUSER.T_SHOW_NPI(A.DELIVERY_NO,
                                                                    a.Line_Item,
                                                                    'Hub Packing List') AS ac_pn_desc,
                                                 SUM(tpo.assign_qty) AS QTY,
                                                 'Assembled in ' || ppsuser.f_transform_coo(TPO.COO, 1) as COO,
                                                 a.shipment_id AS SHIPID_TT
                                   FROM PPSUSER.T_ORDER_INFO A
                                  INNER JOIN PPTEST.VW_MPN_INFO B
                                     ON A.ICTPN = B.ICTPARTNO
                                  inner join PPSUSER.T_FD_ORDER_DETAIL tod
                                     on a.delivery_no = tod.ac_po
                                    and a.line_item = tod.ac_po_line
                                    and a.freightorder = tod.freightorder
                                  inner join PPSUSER.T_PALLET_ORDER TPO
                                     ON A.DELIVERY_NO = TPO.DELIVERY_NO
                                    AND A.LINE_ITEM = TPO.LINE_ITEM
                                    AND A.ICTPN = TPO.ICTPN
                                   AND A.SHIPMENT_ID=TPO.SHIPMENT_ID
                                  WHERE A.DELIVERY_NO = '{0}'
                                    and a.shipment_id = '{1}'
                                  GROUP BY A.DELIVERY_NO,
                                           A.LINE_ITEM,
                                           A.MPN,
                                           a.shipment_id,
                                           tod.ac_pn_desc,
                                           TPO.COO", ac_po, so_no);


            string cartonSql = string.Format(@" SELECT sum(a.carton_qty) as Total_carton
                                  FROM PPSUSER.T_ORDER_INFO A 
                                  where  
                                  a.shipment_id = '{0}'
                                  and  a.delivery_no = '{1}'", so_no, ac_po);

            string carrierSql = string.Format(@"      SELECT distinct octp.SCACCODE AS CARRIER
                               FROM PPSUSER.T_SHIPMENT_INFO tsi,PPTEST.OMS_CARRIER_TRACKING_PREFIX octp
                              WHERE 
                              tsi.carrier_code = octp.carriercode
                              and octp.type = 'HAWB'
                              and octp.isdisabled = '0'
                              and    TSI.SHIPMENT_ID = '{0}'", so_no);
            string shipDateSql = string.Format(@"SELECT TSI.SHIPPING_TIME AS ship_date FROM PPSUSER.T_SHIPMENT_INFO tsi  WHERE  TSI.SHIPMENT_ID = '{0}'", so_no);
            string allPalletWgSql = @"SELECT '888888' AS ALLPALLET_WEIGHT FROM DUAL";
            string currentDate = "";
            currentDate = DateTime.Now.ToString("yyyyMMdd");
            string HAWB = "";
            DataTable headerDt = Util.getDataTaleC(headerSql, "Header");
            if (headerDt != null && headerDt.Rows.Count > 0)
            {
                HAWB = headerDt.Rows[0]["HAWB"].ToString();
            }
            ds.Tables.Add(headerDt);
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(cartonSql, "Cartons"));
            ds.Tables.Add(Util.getDataTaleC(carrierSql, "Carrier"));
            ds.Tables.Add(Util.getDataTaleC(shipDateSql, "ShipDate"));
            ds.Tables.Add(Util.getDataTaleC(allPalletWgSql, "AllPalletWg"));
            if (print)
            {
                Util.CreateDataTable(Constant.FDHubPKGlobal_URL, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.FDHubPKGlobal_URL_ByGW, ds, diskPath);
            }

        }

        public FDHubGlobalLayoutForm(string ac_po, string so_no, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            Initialize(ac_po, so_no, strCrystalFullPath, isCustom, isPDF, strPDFPath, nCopies, isOnlyGetDS, out dtCrystal, out serverFullLabelName);

        }
        private void Initialize(String ac_po, string so_no, string strCrystalFullPath, bool isCustom, bool isPDF, string strPDFPath, int nCopies, bool isOnlyGetDS, out DataSet dtCrystal, out string serverFullLabelName)
        {
            dtCrystal = null;
            serverFullLabelName = string.Empty;
            //初始化 DataSet
            DataSet ds = new DataSet();
            #region  headerSql
            string headerSql = string.Format(@"SELECT distinct A.shipment_id AS SHIPID_TT,
                                                B.SUPPLIERNAME AS SUP_NAME1,
                                                B.SUPPLIERADDRESS1 AS SUP_ADDR1,
                                                B.SUPPLIERADDRESS2 AS SUP_ADDR2,
                                                B.SUPPLIERADDRESS3 AS SUP_ADDR3,
                                                B.SUPPLIERADDRESS4 AS SUP_ADDR4,
                                                B.SUPPLIERTEL AS SUP_TEL,
                                                A.SHIPTONAME1 AS ST_NAME1,
                                                A.SHIPTONAME2 AS ST_NAME2,
                                                A.SHIPTOADDRESS1 AS ST_ADDR1,
                                                A.SHIPTOCITY || ' ' || A.SHIPTOSTATE || ' ' || a.SHIPTOZIP AS ST_ADDR2,
                                                (select iso3.country
                                                   from ppsuser.iso3166_1 iso3
                                                  where iso3.cntycode2 = A.SHIPTOCOUNTRY) AS ST_ADDR3,
                                                A.HAWB,
                                                A.CARRIER_CODE AS CARR_CODE,
                                                A.TRANSPORT AS SHIP_PER,
                                              --  DECODE(A.invtype,'SOI','DAP, Freight Unpaid Ex Hub ' || A.SHIPTOCITY,'AOI','FAC,'||A.shiptocity||','||A.shiptocountry)     AS F_TERM,
                                                'EXW' AS F_TERM,
                                                A.DELIVERY_NO AS AC_PO
                                  FROM PPSUSER.VW_FD_DATA A, PPSUSER.T_FD_SUPPLIER B
                                 WHERE a.shipment_id = '{0}'
                                   AND A.DELIVERY_NO = '{1}'", so_no, ac_po);
            #endregion
            #region lineSql
            //       string MixCooFlag = ClientUtils.ExecuteSQL(string.Format(@" select coo  from ppsuser.t_shipment_pallet
            //where shipment_id = '{0}'", so_no)).Tables[0].Rows[0][0].ToString();
            //       //修改COO取值逻辑 edit by franky 2020/3/13
            //       string lineSql = MixCooFlag.Equals("MIX") ?
            //                                           string.Format(@"     SELECT distinct round(SUM(B.FGWEIGHTKG * A.qty), 2) AS Net_Weight,
            //                                            round(SUM(B.GROSSWEIGHTKG * A.CARTON_QTY), 2) AS Gross_Weight,
            //                                            A.DELIVERY_NO AS AC_PO,
            //                                            A.LINE_ITEM AS AC_PO_LINE,
            //                                            A.MPN AS AC_PN,
            //                                            PPSUSER.T_SHOW_NPI(A.DELIVERY_NO,
            //                                                               a.Line_Item,
            //                                                               'Hub Packing List') AS ac_pn_desc,
            //                                            SUM(A.QTY) AS QTY,
            //                                            'Assembled in ' || ppsuser.f_transform_coo(TOC.COO, 1) as COO,
            //                                            a.shipment_id AS SHIPID_TT
            //                              FROM PPSUSER.T_ORDER_INFO A
            //                             INNER JOIN PPTEST.VW_MPN_INFO B
            //                                ON A.ICTPN = B.ICTPARTNO
            //                             inner join PPSUSER.T_FD_ORDER_DETAIL tod
            //                                on a.delivery_no = tod.ac_po
            //                               and a.line_item = tod.ac_po_line
            //                               and a.freightorder = tod.freightorder
            //                             inner join PPSUSER.T_ORDER_COO TOC
            //                                ON A.DELIVERY_NO = TOC.DN_NO
            //                               AND A.LINE_ITEM = TOC.DN_LINE
            //                               AND A.ICTPN = TOC.PART_NO
            //                             WHERE A.DELIVERY_NO = '{0}'
            //                               and a.shipment_id = '{1}'
            //                             GROUP BY A.DELIVERY_NO,
            //                                      A.LINE_ITEM,
            //                                      A.MPN,
            //                                      a.shipment_id,
            //                                      tod.ac_pn_desc,
            //                                      TOC.COO
            //                           ", ac_po, so_no) :
            string lineSql = string.Format(@"      SELECT distinct round(SUM(B.FGWEIGHTKG * A.qty), 2) AS Net_Weight,
                                                 round(SUM(B.GROSSWEIGHTKG * A.CARTON_QTY), 2) AS Gross_Weight,
                                                 A.DELIVERY_NO AS AC_PO,
                                                 A.LINE_ITEM AS AC_PO_LINE,
                                                 A.MPN AS AC_PN,
                                                 PPSUSER.T_SHOW_NPI(A.DELIVERY_NO,
                                                                    a.Line_Item,
                                                                    'Hub Packing List') AS ac_pn_desc,
                                                 SUM(TPO.ASSIGN_QTY) AS QTY,
                                                 'Assembled in ' || ppsuser.f_transform_coo(TPO.COO, 1) as COO,
                                                 a.shipment_id AS SHIPID_TT
                                   FROM PPSUSER.T_ORDER_INFO A
                                  INNER JOIN PPTEST.VW_MPN_INFO B
                                     ON A.ICTPN = B.ICTPARTNO
                                  inner join PPSUSER.T_FD_ORDER_DETAIL tod
                                     on a.delivery_no = tod.ac_po
                                    and a.line_item = tod.ac_po_line
                                    and a.freightorder = tod.freightorder
                                  inner join PPSUSER.T_PALLET_ORDER TPO
                                     ON A.DELIVERY_NO = TPO.DELIVERY_NO
                                    AND A.LINE_ITEM = TPO.LINE_ITEM
                                    AND A.ICTPN = TPO.ICTPN
                                    AND A.SHIPMENT_ID=TPO.SHIPMENT_ID
                                  WHERE A.DELIVERY_NO = '{0}'
                                    and a.shipment_id = '{1}'
                                  GROUP BY A.DELIVERY_NO,
                                           A.LINE_ITEM,
                                           A.MPN,
                                           a.shipment_id,
                                           tod.ac_pn_desc,
                                           TPO.COO", ac_po, so_no);
            //string lineSql = string.Format(@"       SELECT distinct
            //                      round(SUM(B.FGWEIGHTKG * A.qty),2) AS Net_Weight,
            //                      round(SUM(B.GROSSWEIGHTKG * A.CARTON_QTY),2) AS Gross_Weight,
            //                      A.DELIVERY_NO AS AC_PO,
            //                      A.LINE_ITEM AS AC_PO_LINE,
            //                      A.MPN AS AC_PN,
            //                      PPSUSER.T_SHOW_NPI(A.DELIVERY_NO,a.Line_Item,'Hub Packing List') AS ac_pn_desc,   
            //                      SUM(A.QTY) AS QTY,
            //                      --'Assembled in Vietnam' AS COO,
            //                      'Assembled in ' || ppsuser.getcoobykp(A.ICTPN,1) as  COO,
            //                      a.shipment_id AS SHIPID_TT
            //                      FROM PPSUSER.T_ORDER_INFO A INNER JOIN PPTEST.VW_MPN_INFO B
            //                      ON A.ICTPN=B.ICTPARTNO
            //                      inner join PPSUSER.T_FD_ORDER_DETAIL  tod  on a.delivery_no = tod.ac_po  and a.line_item = tod.ac_po_line and a.freightorder = tod.freightorder
            //                      WHERE  A.DELIVERY_NO = '{0}'
            //                      and a.shipment_id = '{1}'
            //                      GROUP BY A.DELIVERY_NO,A.LINE_ITEM,A.MPN,A.ICTPN,a.shipment_id,tod.ac_pn_desc", ac_po, so_no);
            #endregion

            #region cartonSql
            string cartonSql = string.Format(@" SELECT sum(a.carton_qty) as Total_carton
                                  FROM PPSUSER.T_ORDER_INFO A 
                                  where  
                                  a.shipment_id = '{0}'
                                  and  a.delivery_no = '{1}'", so_no, ac_po);
            #endregion

            #region carrierSql
            string carrierSql = string.Format(@"      SELECT distinct octp.SCACCODE AS CARRIER
                               FROM PPSUSER.T_SHIPMENT_INFO tsi,PPTEST.OMS_CARRIER_TRACKING_PREFIX octp
                              WHERE 
                              tsi.carrier_code = octp.carriercode
                              and octp.type = 'HAWB'
                              and octp.isdisabled = '0'
                              and    TSI.SHIPMENT_ID = '{0}'", so_no);
            #endregion

            string shipDateSql = string.Format(@"SELECT TSI.SHIPPING_TIME AS ship_date FROM PPSUSER.T_SHIPMENT_INFO tsi  WHERE  TSI.SHIPMENT_ID = '{0}'", so_no);
            string allPalletWgSql = @"SELECT '888888' AS ALLPALLET_WEIGHT FROM DUAL";
            string currentDate = "";
            currentDate = DateTime.Now.ToString("yyyyMMdd");
            string HAWB = "";
            DataTable headerDt = Util.getDataTaleC(headerSql, "Header");
            if (headerDt != null && headerDt.Rows.Count > 0)
            {
                HAWB = headerDt.Rows[0]["HAWB"].ToString();
            }
            ds.Tables.Add(headerDt);
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            ds.Tables.Add(Util.getDataTaleC(cartonSql, "Cartons"));
            ds.Tables.Add(Util.getDataTaleC(carrierSql, "Carrier"));
            ds.Tables.Add(Util.getDataTaleC(shipDateSql, "ShipDate"));
            ds.Tables.Add(Util.getDataTaleC(allPalletWgSql, "AllPalletWg"));

            if (string.IsNullOrEmpty(strCrystalFullPath))
            {
                if (!isCustom) //不是关务用默认值
                {
                    strCrystalFullPath = Constant.FDHubPKGlobal_URL;
                }
                else//isCustom如果是关务的是用宋体的模板 
                {
                    strCrystalFullPath = Constant.FDHubPKGlobal_URL_ByGW;
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
