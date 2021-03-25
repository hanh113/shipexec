using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace PackLogic
{
    class CommonSQL
    {
        public DataTable GetShipmentInfoByShipTime(string strShipmentTime)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentTime", strShipmentTime };
            return ClientUtils.ExecuteSQL("SELECT DISTINCT SHIPMENT_ID FROM PPSUSER.T_SHIPMENT_INFO WHERE SHIPPING_TIME=TO_DATE(:ShipmentTime,'yyyy-mm-dd')  and status not in ('CP','HO','SF','WS','IN')  ORDER BY SHIPMENT_ID DESC ", sqlparams).Tables[0];
        }

        public DataTable GetShipentInfoByShipmentID(string strShipmentID)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentID", strShipmentID };
            return ClientUtils.ExecuteSQL(@"
                    SELECT DISTINCT A.SHIPMENT_ID,B.PALLET_NO,A.POE,A.SHIPMENT_TYPE,A.REGION,A.TYPE,A.TRANSPORT,A.SERVICE_LEVEL,A.CARRIER_CODE,A.CARRIER_NAME,
                    (SELECT DISTINCT SCACCODE FROM PPTEST.OMS_CARRIER_TRACKING_PREFIX D WHERE TRIM(D.CARRIERCODE)=A.CARRIER_CODE
                    AND D.SHIPMODE=A.TRANSPORT AND D.ISDISABLED='0' AND D.TYPE='HAWB') AS CARRIERSCACCODE,
                    DECODE(B.PALLET_TYPE,'001','NO_MIX','999','MIX') as PALLETTYPE,C.ICTPN,E.DELIVERY_NO,E.LINE_ITEM,F.SHIPPLANT,F.GS1FLAG,B.PACK_CODE
                    FROM PPSUSER.T_SHIPMENT_INFO A INNER JOIN PPSUSER.T_SHIPMENT_PALLET B ON A.SHIPMENT_ID=B.SHIPMENT_ID
                    INNER JOIN PPSUSER.T_SHIPMENT_PALLET_PART C ON B.PALLET_NO=C.PALLET_NO
                    INNER JOIN PPSUSER.T_PALLET_ORDER E ON A.SHIPMENT_ID=E.SHIPMENT_ID AND B.PALLET_NO=E.PALLET_NO AND C.ICTPN=E.ICTPN
                    LEFT JOIN PPSUSER.T_940_UNICODE F ON E.DELIVERY_NO=F.DELIVERYNO AND E.LINE_ITEM=F.CUSTDELITEM
                    WHERE A.SHIPMENT_ID=:ShipmentID ORDER BY B.PALLET_NO,E.DELIVERY_NO,E.LINE_ITEM ASC
", sqlparams).Tables[0];
        }

        public DataTable GetPrintPACShippingLabel(string strShipmentID, string strType)
        {
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentID", strShipmentID };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipInfoType", strType };
            return ClientUtils.ExecuteSQL(@"
                 SELECT DISTINCT toi.SHIPMENT_ID
                 FROM PPSUSER.T_940_UNICODE   T9U,
                      pptest.oms_lmd          ol,
                      pptest.oms_lmd_overview olo,
                      PPSUSER.T_ORDER_INFO toi
                where  t9u.deliverytype  = ol.dntype
                  and  t9u.shipcntycode = ol.country
                  and  t9u.saleorgcode   = ol.salesorg
                  and  ol.sccode  = olo.sccode
                  and t9u.deliveryno = toi.delivery_no
                  and t9u.custdelitem = toi.line_item
                  and UPPER(olo.lmdmode) = :ShipInfoType
                  and olo.document = 'ShippingLabel'
                  and olo.item = 'ReturnTo'
                  and olo.createlmd = 'Y'
                  and  t9u.region  = 'PAC'
                  AND toi.SHIPMENT_ID=:ShipmentID
", sqlparams).Tables[0];
        }

        public DataTable GetICTCountByPalletNo(string palletNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PALLET_NO", palletNo };
            return ClientUtils.ExecuteSQL("SELECT COUNT(DISTINCT ICTPN) AS ICTPNCOUNT FROM PPSUSER.T_SHIPMENT_PALLET_PART WHERE PALLET_NO=:PALLET_NO ", sqlparams).Tables[0];
        }

        public DataTable GetICTUnitCountByPackCode(string packCode, string ictPartNo)
        {
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PACKCODE", packCode };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ICTPARTNO", ictPartNo };
            return ClientUtils.ExecuteSQL("SELECT PACKUNIT,TOTALCARTON FROM PPSUSER.VW_MPN_INFO WHERE PACKCODE=:PACKCODE AND ICTPARTNO=:ICTPARTNO ", sqlparams).Tables[0];
        }

        public DataTable GetPalletCartonCount(string palletNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PALLET_NO", palletNo };
            return ClientUtils.ExecuteSQL("SELECT CARTON_QTY FROM ppsuser.T_SHIPMENT_PALLET WHERE PALLET_NO=:PALLET_NO ", sqlparams).Tables[0];
        }

        public DataTable GetPrintPACDeliveryNoteLabel(string strShipmentID, string strType)
        {
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentID", strShipmentID };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipInfoType", strType };
            return ClientUtils.ExecuteSQL(@"
                 SELECT DISTINCT toi.SHIPMENT_ID
                 FROM PPSUSER.T_940_UNICODE   T9U,
                      pptest.oms_lmd          ol,
                      pptest.oms_lmd_overview olo,
                      PPSUSER.T_ORDER_INFO toi
                where  t9u.deliverytype = ol.dntype
                  and  t9u.shipcntycode  = ol.country
                  and  t9u.saleorgcode  = ol.salesorg
                  and  ol.sccode = olo.sccode
                  and t9u.deliveryno = toi.delivery_no
                  and t9u.custdelitem = toi.line_item
                  and UPPER(olo.lmdmode) = :ShipInfoType
                  and olo.document = 'DeliveryNote'
                  and olo.item = 'Shipper'
                  and olo.createlmd = 'Y'
                  and  t9u.region  = 'PAC'
                  AND toi.SHIPMENT_ID=:ShipmentID
", sqlparams).Tables[0];
        }

        public DataTable GetDeliveryNoteT90Info(string deliveryNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DeliveryNo", deliveryNo };
            return ClientUtils.ExecuteSQL("SELECT SHIPCNTYCODE, CUSTOMERGROUP FROM PPSUSER.T_940_UNICODE WHERE DELIVERYNO = :DeliveryNo", sqlparams).Tables[0];
        }

        public DataTable GetT940UnicodeInfoByDeliveryNoAndLineItem(string deliveryNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DeliveryNo", deliveryNo };
            return ClientUtils.ExecuteSQL("SELECT customergroup,msgflag,gpflag,region,shipcntycode FROM PPSUSER.T_940_UNICODE WHERE  DeliveryNo  = :DeliveryNo", sqlparams).Tables[0];
        }

        public DataTable JudgeCrystalReportByCondition(string region, string custOmerGroup, string msgFlag, string gpFlag)
        {
            object[][] sqlparams = new object[4][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Region", region };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CustOmerGroup", custOmerGroup };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "MsgFlag", msgFlag };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "GpFlag", gpFlag };
            return ClientUtils.ExecuteSQL(@" SELECT ow.documentname
                                  FROM pptest.oms_ww ow
                                 where ow.region =:Region
                                   and ow.customergroup = :CustOmerGroup 
                                   and (ow.msgflag = :MsgFlag or ow.msgflag = 'ALL')
                                   and (ow.gpflag = :GpFlag or ow.gpflag = 'ALL')", sqlparams).Tables[0];
        }
    }
}
