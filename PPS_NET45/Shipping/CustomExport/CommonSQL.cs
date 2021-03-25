using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Threading;
using LibHelper;
using System.Net;
using System.Net.Mail;
using SajetClass;

namespace CustomExport
{
    public class CommonSQL
    {
        public DataTable GetDeliveryNoteT90Info(string deliveryNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DeliveryNo", deliveryNo };
            return ClientUtils.ExecuteSQL("SELECT SHIPCNTYCODE, CUSTOMERGROUP FROM PPSUSER.T_940_UNICODE WHERE DELIVERYNO = :DeliveryNo", sqlparams).Tables[0];
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
                where  t9u.deliverytype  = ol.dntype
                  and  t9u.shipcntycode  = ol.country
                  and  t9u.saleorgcode  = ol.salesorg
                  and  ol.sccode  = olo.sccode
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

        public DataTable isMultiOrSignleCustSoNoByDeliveryNo(string deliveryNo)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "deliveryNo", deliveryNo };
            return ClientUtils.ExecuteSQL(@"SELECT COUNT(DISTINCT t9u.custsono) as checkCustSo
                                          FROM PPSUSER.T_940_UNICODE T9U
                                         WHERE T9U.DELIVERYNO = :deliveryNo", sqlparams).Tables[0];
        }

        public DataTable checkOmsBucketDocIsExist(string region, string documentName)
        {
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "region", region };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "documentName", documentName };
            return ClientUtils.ExecuteSQL(@"    select count(*) as checkCount
                                             from pptest.OMS_BUCKET_DOC OBD
                                            WHERE OBD.REGION = :region
                                              AND OBD.DOCUMENTNAME = :documentName
                                              AND OBD.BUCKETTYPE IN ('Bucket 1', 'Bucket 2', 'Bucket 3')", sqlparams).Tables[0];
        }
    }
}
