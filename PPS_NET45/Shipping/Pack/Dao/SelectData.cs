using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Packingparcel.Dao
{
    class SelectData
    {
        public DataTable getShipMentInfoByshipmentId(string shipMentId)
        {
            string sql = string.Format(@"select  t1.* from  (select tsp.shipment_id AS shipmentId,
                           tsi.service_level,
                           decode(upper(tsi.security),'BASIC','低','MEDIUM','中','HIGH','高') as security,
                           tsi.carrier_code,
                           tsi.carrier_name as Carrier,
                           tsi.poe as POE,
                           tsi.region,
                           tsp.pallet_no as palletNo,
                           decode(tsp.pallet_type,'001','NO_MIX','999','MIX') as palletType,
                           tspp.ictpn as ictPn_,
                           tspp.carton_qty as cartonQty,
                           tspp.pick_carton as alreadyPickCartonQty,
                           tspp.pack_carton as alreadyPackCartonQty,
                           tsi.shipment_type,
                           tsi.type,
                           decode(tspp.pack_status,'IP','进行中','FP','已完成','QH','QAHOLD','WP','等待作业')  as status
                      from ppsuser.t_shipment_pallet      tsp,
                           ppsuser.t_shipment_pallet_part tspp,
                           ppsuser.t_shipment_info        tsi
                     where tsp.pallet_no = tspp.pallet_no
                       and tsp.shipment_id = tsi.shipment_id
                    and tsp.shipment_id = '{0}'
                    order by alreadyPackCartonQty ,palletNo)t1  order by decode(status,'进行中','01','等待作业','02','已完成','03','05')
                    ", shipMentId);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getPalletPickInfoByPickPalletNo(string pickPalletNo, string shipMentId)
        {
            //wpx modify by Franky 2020-3-31
            string sql = string.Format(@" select    distinct         tsp.carton_qty,
                                                           tsp.pack_carton,
                                                           (case
                                                             when tsp.pallet_type = '999' then
                                                              'MIX'
                                                             else
                                                              'NO_MIX'
                                                           end) as palletType,
                                                           tpp.sn_type,
                                                           vmi.REMARK,
                                                           DECODE(UPPER(TSP.SECURITY),'BASIC','低','MEDIUM','中','HIGH','高') AS SECURITY,
                                                           CASE WHEN tsi.REGION='EMEIA' and (tsi.CARRIER_CODE LIKE '%DHL%' OR tsi.CARRIER_NAME LIKE '%DHL%') AND tsi.SERVICE_LEVEL='WPX'
                                                           THEN 'WPX' ELSE '' END AS SERVICE_LEVEL
                                                      from ppsuser.t_pallet_pick tpp,
                                                           ppsuser.t_shipment_pallet tsp,
                                                           ppsuser.VW_MPN_INFO vmi,
                                                           ppsuser.t_shipment_info tsi
                                                     where tpp.pallet_no = tsp.pallet_no
                                                     and tsp.shipment_id = tsi.shipment_id
                                                     and   tsp.pack_code = vmi.PACKCODE
                                                     and tpp.pick_pallet_no = '{0}'
                                                     and tsp.shipment_id ='{1}' ", pickPalletNo, shipMentId);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable isMultiOrSignleCustSoNoByDeliveryNo(string deliveryNo)
        {
            string sql = string.Format(@"SELECT COUNT(DISTINCT t9u.custsono) as checkCustSo
                                          FROM PPSUSER.T_940_UNICODE T9U
                                         WHERE T9U.DELIVERYNO = '{0}'", deliveryNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getGS1LabelInfo(string cartonNo)
        {
            //string sql = string.Format(@"      SELECT
            //                            distinct vpm.CUSTPART,
            //                                        vpm.MODELNO,
            //                                         decode(opa.jan_code,'',opa.upc_code,opa.jan_code)  gtin,
            //                                        tsp.sscc,
            //                                        tpo.assign_qty AS qty,
            //                                        tsp.gs1flag,
            //                                      'Assembled In '||  ppsuser.f_transform_Coo(tpo.coo,1) coo
            //                                FROM PPTEST.VW_PARTMAPPING_MODEL VPM,
            //                                    pptest.oms_partmapping  opa,
            //                                    ppsuser.t_sn_status         tss,
            //                                    ppsuser.t_shipment_pallet   tsp,
            //                                    ppsuser.t_pallet_order tpo
            //                                where vpm.PART = tss.part_no
            //                                and opa.part = tss.part_no
            //                                and tsp.pallet_no = tss.pack_pallet_no
            //                                and tsp.pallet_no=tpo.pallet_no
            //                                and tss.carton_no = '{0}'", cartonNo);

            //add COO for GS1 by wenxing 2020-11-17 
            string sql = string.Format(@"      SELECT
                                        distinct vpm.CUSTPART,
                                                    vpm.MODELNO,
                                                     decode(opa.jan_code,'',opa.upc_code,opa.jan_code)  gtin,
                                                    tsp.sscc,
                                                    tpo.assign_qty AS qty,
                                                    tsp.gs1flag,
                                                  'Assembled In '||  ppsuser.f_transform_Coo(tpo.coo,1) coo,
                                                    'COO:'||tpo.coo QRCOO
                                            FROM PPTEST.VW_PARTMAPPING_MODEL VPM,
                                                pptest.oms_partmapping  opa,
                                                ppsuser.t_sn_status         tss,
                                                ppsuser.t_shipment_pallet   tsp,
                                                ppsuser.t_pallet_order tpo
                                            where vpm.PART = tss.part_no
                                            and opa.part = tss.part_no
                                            and tsp.pallet_no = tss.pack_pallet_no
                                            and tsp.pallet_no=tpo.pallet_no
                                            and tss.carton_no = '{0}'", cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable checkOmsBucketDocIsExist(string region, string documentName)
        {
            string sql = string.Format(@"    select count(*) as checkCount
                                             from pptest.OMS_BUCKET_DOC OBD
                                            WHERE OBD.REGION = '{0}'
                                              AND OBD.DOCUMENTNAME = '{1}'
                                              AND OBD.BUCKETTYPE IN ('Bucket 1', 'Bucket 2', 'Bucket 3')", region, documentName);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable IsGs1Coo()
        {
            string sql = @"  select COUNT(*) as checkGs1Coo
                             from PPSUSER.T_BASICPARAMETER_INFO where 
                             PARA_TYPE='GS1_COO' and PARA_VALUE='Y' and ENABLED='Y'";
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable isMultiMpnForGs1ByCartonNo(string cartonNo)
        {
            string sql = string.Format(@"   SELECT COUNT(DISTINCT TPO.MPN) as checkMpn
                                              FROM PPSUSER.T_PALLET_ORDER TPO, PPSUSER.T_SN_STATUS TSS
                                             WHERE TPO.PALLET_NO = TSS.PACK_PALLET_NO
                                               AND TSS.CARTON_NO = '{0}'", cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getReprintInfoByCartonNo(string cartonNo)
        {
            string sql = string.Format(@"select distinct tss.delivery_no, tss.line_item,tss.pack_pallet_no
                                               from ppsuser.t_sn_status tss
                                               where tss.carton_no = '{0}'
                                            ", cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable isFinishWorkByPickPalletNo(string pickPalletNo)
        {
            string sql = string.Format(@" select *
                                           from (SELECT count(*) as totalQty
                                                   FROM ppsuser.t_sn_status tss
                                                  where tss.pick_pallet_no = '{0}') t2,
                                                (select count(*) as passStationQty
                                                   from ppsuser.t_sn_status tss1
                                                  where tss1.pick_pallet_no = '{0}'
                                                    and tss1.wc not in ('W0','W1')) t1
                                          where t2.totalQty = t1.passStationQty", pickPalletNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable isFinishWorkByShipMentId(string shipmentId)
        {
            string sql = string.Format(@"select tsi.*
                                          from ppsuser.t_shipment_info tsi
                                         where tsi.shipment_id = '{0}'
                                           and tsi.status = 'FP'", shipmentId);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable getPrintInfoByShipmentIdAndCartonNo_FD(string shipmentId, string cartonNo)
        {
            string sql = @" select distinct octp.SCACCODE,
                            vfd.hubcode,
                            vfd.hubdesc,
                            vfd.invtype,
                            vfd.shiptoname1,
                            vfd.shiptoname2,
                            vfd.shiptoaddress1,
                            vfd.shiptoaddress2,
                            vfd.shiptoaddress3,
                            vfd.shiptoaddress4,
                            vfd.shiptocity,
                            vfd.shiptostate,
                            vfd.shiptozip,
                            (select iso3.country
                               from ppsuser.iso3166_1 iso3
                              where iso3.cntycode2 = vfd.shiptocountry) as shiptocountry,
                            vfd.shiptotelephone,
                            vfd.returntoname1,
                            vfd.returntoname2,
                            vfd.returntoaddress1,
                            vfd.returntoaddress2,
                            vfd.returntoaddress3,
                            vfd.returntoaddress4,
                            vfd.returntocity,
                            vfd.returntostate,
                            vfd.returntozip,
                            f.asn shipid,
                            (select iso3.country
                               from ppsuser.iso3166_1 iso3
                              where iso3.cntycode2 = vfd.returntocountry) as returntocountry,
                            vfd.delivery_no,
                            vfd.line_item,
                            vfd.mpn,
                            vfd.ictpn,
                            vfd.shipment_id,
                            vfd.carrier_name,
                            vfd.poe,
                            vfd.hawb,
                            vfd.shipment_type,
                            vfd.region,
                            vfd.qty,
                            vfd.carton_qty,
                            vfd.shipping_time,
                            vfd.transport,
                            vfd.service_level,
                            vfd.carrier_code,
                            vfd.cdt,
                            vfd.pack_code,
                            vfd.pack_type,
                            vfd.status,
                            vfd.pack_qty,
                            vfd.pack_carton_qty,
                            vfd.udt,
                            vfd.person_flag,
                            vfd.AC_PN_DESC,
                            vfd.FREIGHTORDER,
                            (SELECT TSI.ORIGION
                               FROM PPSUSER.T_SHIPMENT_INFO TSI
                              WHERE TSI.SHIPMENT_ID = :ShipmentId) as ORIGION
              from ppsuser.t_sn_status tss
              join ppsuser.vw_fd_data vfd
                on tss.delivery_no = vfd.DELIVERY_NO
               and tss.line_item = vfd.LINE_ITEM
              join PPTEST.OMS_CARRIER_TRACKING_PREFIX octp
                on vfd.carrier_code = octp.carriercode 
                and vfd.TRANSPORT = octp.SHIPMODE
              join (select shipment_id,
                           delivery_no,
                           decode(region ,'EMEIA',freightorder || lpad(to_char(rownum), 4, '0'),freightorder) asn
                      from (select distinct shipment_id,
                                            delivery_no,
                                            region,
                                            freightorder
                              from ppsuser.t_order_info
                            --var is there
                             where shipment_id = :ShipmentId
                             order by delivery_no asc)) f
                on tss.delivery_no = f.delivery_no
             where octp.type = 'HAWB'
               and octp.isdisabled = '0'
               and vfd.SHIPMENT_ID = :ShipmentId
               and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable GetSFShiptoCNTYCODE(string shipMentId)
        {
            string sql = string.Format(@"SELECT DISTINCT f.SHIPCNTYCODE FROM ppsuser.T_940_UNICODE f WHERE f.DELIVERYNO IN(
SELECT a.DELIVERY_NO FROM ppsuser.T_ORDER_INFO a WHERE a.SHIPMENT_ID='{0}')
AND (f.SHIPCNTYCODE='HK' OR f.SHIPCNTYCODE='TW')
                    ", shipMentId);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable getPrintInfoOfPACByCartonNO(string cartonNo)
        {
            string sql = @"  SELECT distinct decode(t9u.region,'EMEIA',OCTP.SCACCODE,'PAC',T9U.CARRIERCODE) as carrier,
                                t9u.shipplant,
                                tsi.poe,
                                tss.hawb,
                                tss.delivery_no,
                                TSI.ORIGION as ORIGIN,
                                '' as INVOICENO,
                                to_char(tsi.shipping_time,'dd/MM/yyyy') as shipDate,
                                t9u.shipidentifier as SHIPID,
                                t9u.shiptoconttel as tel,
                                t9u.shiptoname,
                                t9u.shiptocompany,
                                t9u.shiptoaddress,
                                t9u.shiptoaddress2,
                                t9u.shiptoaddress3,
                                t9u.shiptoaddress4,
                                t9u.shiptocity,
                                t9u.shiptostate,
                                t9u.shiptozip,
                                to_char(TO_DATE(T9U.CUSTPLANDELDATE,'yyyyMMdd'),'Dy','NLS_DATE_LANGUAGE=AMERICAN') AS DEL_DATE,
                                t9u.PORTOFENTRY,
                                T9U.Custsono AS SALESORDER,
                                T9U.WEBORDERNO  AS WEBORDER,
                                (select iso3.country
                                   from ppsuser.iso3166_1 iso3
                                  where iso3.cntycode2 = t9u.shipcntycode) as shiptocountry,
                                  t9u.custpono,
                                  t9u.shipcntycode  as ctry
                  FROM PPSUSER.T_SN_STATUS                TSS,
                       PPSUSER.T_940_UNICODE              T9U,
                       PPTEST.OMS_CARRIER_TRACKING_PREFIX octp,
                       ppsuser.t_shipment_info            tsi
                 WHERE TSS.LINE_ITEM = T9U.CUSTDELITEM
                   AND TSS.DELIVERY_NO = T9U.DELIVERYNO
                   AND T9U.CARRIERCODE = OCTP.CARRIERCODE
                   and tss.shipment_id = tsi.shipment_id
                   AND OCTP.TYPE = 'HAWB'
                   AND TSI.TRANSPORT=OCTP.SHIPMODE
                   AND TSS.CARTON_NO =:CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }


        public DataTable getPrintInfoReturnInfo_EMEIA_ORES_ByCartonNo(string cartonNo)
        {
            string sql = @"  SELECT distinct ores.returnaddress1,
                                    ores.returnaddress2,
                                    ores.returnaddress3,
                                    ores.returnaddress4,
                                    ores.returnaddress5,
                                    ores.returnaddress6,
                                    '' as returnaddress7 ,
                                    '' as returnaddress8 
                      FROM PPSUSER.T_940_UNICODE       T9U,
                           pptest.oms_return_emeia_sto ores,
                           PPSUSER.T_SN_STATUS         TSS
                     WHERE TSS.DELIVERY_NO = T9U.DELIVERYNO
                       AND TSS.LINE_ITEM =  T9U.CUSTDELITEM 
                       AND  T9U.SHIPTOCODE  = ORES.SHIPTOCODE
                       AND TSS.CARTON_NO =  :CartonNo ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getPrintInfoReturnInfo_EMEIA_OTHER()
        {
            string para = "OTHER";
            string sql = @"      select ored.returnaddress1,
                                        ored.returnaddress2,
                                        ored.returnaddress3,
                                        ored.returnaddress4,
                                        ored.returnaddress5,
                                        ored.returnaddress6,
                                        ored.returnaddress7,
                                        ored.returnaddress8
                                   from pptest.oms_return_emeia_ds ored
                                  where ored.shipcntycode = :Para ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Para", para };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable isEMEIA_MIT(string cartonNo)
        {
            string sql = @"      SELECT DISTINCT T9U.SHIPPLANT
                                  FROM PPSUSER.T_940_UNICODE T9U, PPSUSER.T_SN_STATUS TSS
                                 WHERE T9U.DELIVERYNO = TSS.DELIVERY_NO
                                   AND T9U.CUSTDELITEM = TSS.LINE_ITEM
                                   AND TSS.CARTON_NO =  :CartonNo ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getEMEIA_MIT_RETURNINFO()
        {
            string para = "MIT5";
            string sql = @"     select OIV.RU_2_NAME,OIV.RU_2_ADDRESS from pptest.oms_invmstr OIV where OIV.ru_2_tel =:Para ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Para", para };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getPrintInfoReturnInfo_EMEIA_ORED_ByCartonNo(string cartonNo)
        {
            string sql = @"  SELECT distinct ored.returnaddress1,
                                            ored.returnaddress2,
                                            ored.returnaddress3,
                                            ored.returnaddress4,
                                            ored.returnaddress5,
                                            ored.returnaddress6,
                                            ored.returnaddress7,
                                            ored.returnaddress8
                                FROM PPSUSER.T_940_UNICODE      T9U,
                                    pptest.oms_return_emeia_ds ored,
                                    PPSUSER.T_SN_STATUS        TSS
                                WHERE TSS.DELIVERY_NO = T9U.DELIVERYNO
                                AND TSS.LINE_ITEM = T9U.CUSTDELITEM
                                AND  T9U.Shipcntycode  = ored.shipcntycode
                                AND TSS.CARTON_NO =:CartonNo ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable getPrintInfoByShipmentIdAndCartonNo_DS_AMR(string cartonNo)
        {
            //string sql = @"  select *
            //                  from (select distinct
            //                               orea.shiplabelname as returnto1,
            //                               orea.address as returnto2,
            //                               orea.city || ',' || orea.state || ' ' || orea.postalcode as returnto3,
            //                               t9u.deliveryno,
            //                               t9u.custsono as SaleOrder,
            //                               t9u.weborderno as WebOrder,
            //                               t9u.shiptoname,
            //                               t9u.shiptocompany,
            //                               t9u.shiptoaddress,
            //                               t9u.shiptoaddress2,
            //                               t9u.shiptoaddress3,
            //                               t9u.shiptoaddress4,
            //                               t9u.shiptocity || t9u.shiptostate || t9u.shiptozip as ShiptoAddress5,
            //                               t9u.shiptocountry,
            //                               t9u.shiptoconttel as tel,
            //                               t9u.custpono
            //                          from ppsuser.t_940_unicode t9u,
            //                               pptest.oms_return_amr orea,
            //                               ppsuser.t_sn_status   tss
            //                         where tss.delivery_no = t9u.deliveryno
            //                           and tss.line_item = t9u.custdelitem
            //                           and trim(t9u.shipcntycode) = orea.shiptocntycode
            //                           and substr(t9u.shiptozip, 1, 3) >= orea.zipcodebegin
            //                           and substr(t9u.shiptozip, 1, 3) <= orea.zipcodeend
            //                           and tss.carton_no = :CartonNo) t1,
            //                       (select vsf.*
            //                          from ppsuser.VW_SHIPMENT_FORWARDER vsf
            //                         where vsf.SHIPMENT_ID = :ShipmentId) t2";
            //object[][] sqlparams = new object[2][];
            //sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            //sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            object[][] procParams = new object[28][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "cartonNo", cartonNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto1", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto2", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto3", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "dn", "" };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "saleOrder", "" };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "web_Order_NO", "" };
            procParams[7] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shipto_name", "" };
            procParams[8] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shipto_company", "" };
            procParams[9] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shipto_address", "" };
            procParams[10] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shipto_address2", "" };
            procParams[11] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shipto_address3", "" };
            procParams[12] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shipto_address4", "" };
            procParams[13] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shipto_address5", "" };
            procParams[14] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shipto_country", "" };
            procParams[15] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "tel", "" };
            procParams[16] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "shippingTime", "" };
            procParams[17] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "custpo_no", "" };
            procParams[18] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "P_OE", "" };
            procParams[19] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "CARRIERNAME", "" };
            procParams[20] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "H_awb", "" };
            procParams[21] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "O_RIGION", "" };
            procParams[22] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto4", "" };
            procParams[23] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto5", "" };
            procParams[24] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto6", "" };
            procParams[25] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto7", "" };
            procParams[26] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto8", "" };
            procParams[27] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "TMES", "" };
            return ClientUtils.ExecuteProc("ppsuser.t_shippingLable_for_DS_AMR", procParams).Tables[0];
        }
        public DataTable isPrintPACShippingLabelByCartonNoAndShipInfoType(string cartonNo, string shipInfoType)
        {
            string sql = @"      SELECT count(*) as  checkCount
                                 FROM PPSUSER.T_940_UNICODE   T9U,
                                      pptest.oms_lmd          ol,
                                      pptest.oms_lmd_overview olo,
                                      ppsuser.t_sn_status tss
                                where  t9u.deliverytype  = ol.dntype
                                  and  t9u.shipcntycode  = ol.country
                                  and  t9u.saleorgcode  = ol.salesorg
                                  and  ol.sccode  = olo.sccode
                                  and t9u.deliveryno = tss.delivery_no
                                  and t9u.custdelitem = tss.line_item
                                  and olo.lmdmode = :ShipInfoType
                                  and olo.document = 'ShippingLabel'
                                  and olo.item = 'ReturnTo'
                                  and olo.createlmd = 'Y'
                                  and  t9u.region  = 'PAC'
                                  and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipInfoType", shipInfoType };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable isPrintDeliveryNoteLableByCartonNoAndShipInfoType(string cartonNo, string shipInfoType)
        {
            string sql = @"      SELECT count(*)  checkCount
                                 FROM PPSUSER.T_940_UNICODE   T9U,
                                      pptest.oms_lmd          ol,
                                      pptest.oms_lmd_overview olo,
                                      ppsuser.t_sn_status tss
                                where  t9u.deliverytype  = ol.dntype
                                  and  t9u.shipcntycode  = ol.country
                                  and  t9u.saleorgcode  = ol.salesorg
                                  and  ol.sccode = olo.sccode
                                  and t9u.deliveryno = tss.delivery_no
                                  and t9u.custdelitem = tss.line_item
                                  and olo.lmdmode = :ShipInfoType
                                  and olo.document = 'DeliveryNote'
                                  and olo.item = 'Shipper'
                                  and olo.createlmd = 'Y'
                                  and  t9u.region  = 'PAC'
                                  and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipInfoType", shipInfoType };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getPACReturnInfoByCartonNoAndShipInfoType(string cartonNo, string shipInfoType)
        {
            string sql = @"         SELECT distinct mld.datacontent
                                     FROM PPSUSER.T_940_UNICODE   T9U,
                                          pptest.oms_lmd          ol,
                                          pptest.oms_lmd_overview olo,
                                          ppsuser.t_sn_status tss,
                                           pptest.oms_lmd_data mld
                                    where  t9u.deliverytype  = ol.dntype
                                      and  t9u.shipcntycode  = ol.country
                                      and  t9u.saleorgcode  = ol.salesorg
                                      and  ol.sccode  = olo.sccode
                                      and t9u.deliveryno = tss.delivery_no
                                      and t9u.custdelitem = tss.line_item
                                      and mld.country = ol.country
                                      and mld.salesorg = ol.salesorg
                                      and mld.sccode = ol.sccode
                                      and  t9u.portofentry  = mld.poe
                                      and olo.datatype = mld.datatype
                                      and mld.lmdmode ='ALL'
                                      and olo.lmdmode = :ShipInfoType
                                      and olo.document = 'ShippingLabel'
                                      and olo.item = 'ReturnTo'
                                      and olo.createlmd = 'Y'
                                      and  t9u.region  = 'PAC'
                                      and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipInfoType", shipInfoType };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable getCartonsOfPrintLabelInfoByCartonNo_No_MIX(string cartonNo, string shipmentId)
        {
            string sql = @"        select distinct *
                                   from (select sum(toi.carton_qty) as totalQty
                                           from ppsuser.t_order_info toi
                                          where toi.delivery_no =
                                                (select distinct tss.delivery_no
                                                   from ppsuser.t_sn_status tss
                                                  where tss.carton_no = :CartonNo)
                                            and toi.shipment_id = :ShipmentId ) t1,
                                        (select min(tss.box_no) as startBoxNo
                                           from ppsuser.t_sn_status tss, ppsuser.t_sn_status tss1
                                          where tss1.pack_pallet_no = tss.pack_pallet_no
                                            and tss1.carton_no = :CartonNo) t2,
                                        (select 
                                          max(tss.box_no) as endBoxNo
                                           from ppsuser.t_sn_status tss, ppsuser.t_sn_status tss1
                                          where tss1.pack_pallet_no = tss.pack_pallet_no
                                            and tss1.carton_no = :CartonNo) t3";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable calculatePAC_PoString(string deliveryNo)
        {
            string sql = @"     SELECT t9u.deliveryno, T9U.Custpono, T9U.ITEMCUSTPOLINE
                                FROM PPSUSER.T_940_UNICODE T9U
                                WHERE T9U.DELIVERYNO = :DeliveryNo
                                group by t9u.deliveryno, t9u.custpono, T9U.ITEMCUSTPOLINE";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DeliveryNo", deliveryNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }


        public DataTable getCartonsOfPrintLabelInfoByCartonNo_MIX(string cartonNo, string shipmentId)
        {
            string sql = @"       select *
                                  from (select sum(toi.carton_qty) as totalQty
                                          from ppsuser.t_order_info toi
                                         where toi.delivery_no =
                                               (select distinct tss.delivery_no
                                                  from ppsuser.t_sn_status tss
                                                 where tss.carton_no = :CartonNo)
                                                 and toi.shipment_id= :ShipmentId) t1
                                 cross join (select distinct tss.box_no as cartonQty
                                               from ppsuser.t_sn_status tss
                                              where tss.carton_no = :CartonNo) t2";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getEMEIA_UUIDByCartonNo(string cartonNo)
        {
            string sql = @" SELECT  tss.uuicode FROM   PPSUSER.T_SN_STATUS  TSS  WHERE  TSS.CARTON_NO = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable calPackPalletNoQtyByCartonNo(string cartonNo)
        {
            string sql = @" SELECT DISTINCT (TSS.CARTON_NO) AS CARTONQTY
                             FROM PPSUSER.T_SN_STATUS TSS1, PPSUSER.T_SN_STATUS TSS
                            WHERE SUBSTR(TSS1.PICK_PALLET_NO, 3) = TSS.PACK_PALLET_NO
                              AND TSS1.CARTON_NO = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable getDNisKTOByCartonNo(string cartonNo)
        {
            string sql = @" 
                             select a.*
                               from pptest.oms_940_d_kto a
                              where (a.kto_ac_dn, a.kto_ac_dn_line) in
                                    (select distinct b.delivery_no, b.line_item
                                       from ppsuser.t_sn_status b
                                      where b.carton_no = :CartonNo)
                            ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getAllUUICodeByCartonNo(string cartonNo)
        {
            string sql = @" SELECT  TSS.*
                               FROM PPSUSER.T_SN_STATUS TSS
                              WHERE TSS.CARTON_NO = :CartonNo
                              ORDER BY TSS.UUICODE";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getAllUUICodeByCartonNoKTO(string cartonNo)
        {
            string sql = @"
                                select *
                                  from (select a.customer_sn,
                                               'U21' || substr(trim(b.ac_dn), 1, 10) ||
                                               substr(trim(b.ac_dn_line), 1, 6) || substr(a.uuicode, -4, 4) as uuicode
                                          from ppsuser.t_sn_status a
                                          join pptest.oms_940_d_kto b
                                            on a.delivery_no = b.kto_ac_dn
                                           and a.line_item = b.kto_ac_dn_line
                                         where a.carton_no =  :CartonNo) aa
                                 order by aa.uuicode
                                ";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getYMT_File_Info_ByCartonNo(string cartonNo)
        {
            // HYQ: 20191210new  add  Branch Classiﬁcation (PuP Type Indicator)
            string sql = @"      select distinct tss.carton_no as productserialnoproductno,
                                        tss.tracking_no as carriertrackingnotakkyubinno, --tracking no 根据是cod 和nocod 区分
                                        t9u.weborderno as reference1weborderno,
                                        t9u.deliveryno as reference2sapdeliveryno,
                                        tss.sscc as cartonmatchinginformation1ssc,
                                        tss.carton_no || tss.part_no as cartonmatchinginformation2ser,
                                        tss.hawb as cartonmatchinginformation3ser,
                                        '' as housewaybillno, --?
                                        case
                                          when t9u.shipcntycode = 'JP' and
                                               ((instr(t9u.shiptoaddress, '_6_') > 0) or
                                               (instr(t9u.shiptoaddress, '_7_') > 0)) then
                                           'JP'
                                          else
                                           ''
                                        end as yamatoareacode,
                                        t9u.shiptozip as consigneepostcode,
                                        nvl(t9u.shiptoconttel, '00000000') as consigneetelephoneno, --如果空白放 00000000
                                        decode(t9u.shipcntycode,
                                               'JP',
                                               t9u.shiptocity,
                                               t9u.shiptocity || t9u.shiptoaddress4) as consigneeaddress1,
                                        t9u.shiptoaddress as consigneeaddress2,
                                        t9u.shiptoaddress2 as consigneeaddress3,
                                        t9u.shiptocompany as consigneeaddress4,
                                        t9u.shiptoname as consigneename,
                                        '' as shipperpostcode,
                                        '' as shippertelephoneno,
                                        '' as shipperaddress1,
                                        '' as shipperaddress2,
                                        '' as shipperaddress3,
                                        '' as shipperaddress4,
                                        '' as shippername,
                                        '' as customercode,
                                        '' as customerbranchcode,
                                        to_char(tsi.shipping_time, 'yyyyMMdd') as shipdate,
                                        '' as designatedeliverydate,
                                        '' as designatefordeliverytime,
                                        case
                                          when t9u.shipcntycode = 'JP' and
                                               instr(t9u.shiptoaddress, '_6_') > 0 then
                                           '6'
                                          when t9u.shipcntycode = 'JP' and
                                               instr(t9u.shiptoaddress, '_7_') > 0 then
                                           '7'
                                          else
                                           ''
                                        end as handlinginformation1,
                                        '' as handlinginformation2,
                                        '' as specialinstruction,
                                        '2' as sizeflag,
                                        '' as chargeableweightkgs,
                                        'XX' as yamatopackagesize,
                                        vmi.packunit as totalnumberofunitsinthiscarton,
                                        (select sum(tpo.assign_carton)
                                           from ppsuser.t_pallet_order tpo
                                          where tpo.delivery_no = tss.delivery_no) as totalnumberofcartonsperthecorr,
                                        tss.box_no as cartonsequencenointhecorrespon,
                                        '' as coolflag,
                                        '' as yamatobranchcode,
                                        t9u.codamount as amountofcod,
                                        t9u.codtax as amountoftax,
                                        case
                                          when tss.box_no = 1 then
                                           decode(t9u.shipofpay, '', '0', '1')
                                          else
                                           '0'
                                        end as paymentflagforcod
                          from ppsuser.t_sn_status     tss,
                               ppsuser.t_940_unicode   t9u,
                               pptest.vw_mpn_info      vmi,
                               ppsuser.t_shipment_info tsi
                         where tss.line_item = t9u.custdelitem
                           and tss.delivery_no = t9u.deliveryno
                           and vmi.ictpartno = tss.part_no
                           and tss.shipment_id = tsi.shipment_id
                           and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable isPrintCrt(string inputData)
        {
            //string sql = @" SELECT SUM(TOI.PACK_CARTON_QTY) AS CARTONQTY
            //                FROM PPSUSER.T_ORDER_INFO TOI,
            //                    (SELECT DISTINCT TSS.DELIVERY_NO AS DELIVERYNO,
            //                                    TSS.SHIPMENT_ID AS SHIPMENTID
            //                        FROM PPSUSER.T_SN_STATUS TSS
            //                        WHERE (tss.serial_number = :InputData 
            //                            or tss.carton_no = :InputData 
            //                            or tss.pick_pallet_no = :InputData)) A
            //                WHERE A.DELIVERYNO = TOI.DELIVERY_NO
            //                AND A.SHIPMENTID = TOI.SHIPMENT_ID";
            string sql = @"       SELECT MIN(TSS.BOX_NO) AS BOXNO
                                   FROM PPSUSER.T_SN_STATUS TSS
                                  WHERE (TSS.CARTON_NO = :InputData OR
                                        TSS.CARTON_NO =
                                        (SELECT DISTINCT (TSS.CARTON_NO)
                                            FROM PPSUSER.T_SN_STATUS TSS
                                           WHERE TSS.SERIAL_NUMBER = :InputData) OR
                                        TSS.PICK_PALLET_NO = :InputData)";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputData", inputData };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }


        public DataTable getSps203ImportWpx(string cartonNo)
        {
            string sql = @"      select distinct '1' as Numberofawbshipment,
                                        '      ' as BackEndType1,
                                        'SPS 2.03' as SPSVersionID,
                                        'SPS' as BackEndType,
                                        '' as ReferenceDataversionnumber,
                                        '' as MeterNumber,
                                        '' as Opunit,
                                        '' as ScheduleNumber,
                                        '' as ConsignmentNumber,
                                        tss.tracking_no as ShipmentNumber,
                                        '' as OriginIATA,
                                        '' as DestinationIATA,
                                        '' as InvoiceNumber,
                                        t9u.parcelaccountnumber as ShippersAccountNumber,
                                        (SELECT TSH.SHIPPERNAME  FROM  PPSUSER.T_SHIPPER TSH)||'(A.I.)' as ShippersCompanyName,
                                        (SELECT TSH.SHIPPERNAME  FROM  PPSUSER.T_SHIPPER TSH)||'(A.I.)' as ShippersContactName,
                                        (SELECT TSH.SHIPPERADDRESS1  FROM  PPSUSER.T_SHIPPER TSH) as ShippersAddressLine1,
                                        (SELECT TSH.SHIPPERADDRESS2  FROM  PPSUSER.T_SHIPPER TSH) as ShippersAddressLine2,
                                        '' as ShippersAddressLine3,
                                        (SELECT TSH.SHIPPERCITY  FROM  PPSUSER.T_SHIPPER TSH) as ShippersCity,
                                        (SELECT TSH.SHIPPERSTATE  FROM  PPSUSER.T_SHIPPER TSH) as ShippersState,
                                        (SELECT TSH.SHIPPERPOSTAL  FROM  PPSUSER.T_SHIPPER TSH) as ShippersZipPostalCode,
                                        (SELECT TSH.SHIPPERCOUNTRY  FROM  PPSUSER.T_SHIPPER TSH) as ShippersCountryName,
                                        (SELECT TSH.SHIPPERCNTYCODE  FROM  PPSUSER.T_SHIPPER TSH) as ShippersCountryCode,
                                        t9u.deliveryno as ShippersReference,
                                        (SELECT tsh.shippertel  FROM  PPSUSER.T_SHIPPER TSH) as ShippersPhoneNumber,
                                        '' as ShippersFAXNumber,
                                        '' as ShippersTelexNumber,
                                        '' as ShippersTelexAnswerBack,
                                        '' as ShippersCurrencyCode,
                                        'N' as ShippersWorldClassAcctflag,
                                        'USD' as RecipientCode,
                                        t9u.parcelaccountnumber as RecipientAccountNumber,
                                        decode(t9u.shiptocompany,
                                                '',
                                                t9u.shiptoname,
                                                t9u.shiptocompany) as RecipientCompanyName, --如果ShipToCompany空白 ShipToName 
                                        t9u.shiptoname as RecipientAttentionName,
                                        t9u.shiptoaddress as RecipientAddressLine1,
                                        decode(t9u.shiptoaddress2, '', '.', t9u.shiptoaddress2) as RecipientAddressLine2, --ShipToAddress2 如果空白 放.
                                        t9u.shiptoaddress3 as RecipientAddressLine3,
                                        t9u.shiptocity as RecipientCity,
                                        decode(instr(t9u.regiondesc, '='),
                                                0,
                                                t9u.regiondesc,
                                                substr(t9u.regiondesc,
                                                        0,
                                                        instr(t9u.regiondesc, '=') - 1)) as RecipientState, --RegionDesc = 前面   
                                        t9u.shiptozip as RecipientZipCode,
                                        t9u.shiptocountry as RecipientCountryName,
                                        t9u.shipcntycode as RecipientCountryCode,
                                        decode(t9u.shiptoconttel,'','.',null,'.',t9u.shiptoconttel) as RecipientPhoneNumber,
                                        '' as RecipientFAXNumber,
                                        '' as RecipientTelexNumber,
                                        '' as RecipientTelexAnswerBack,
                                        '' as DHLsRecipientTableRef,
                                        'N' as FlagforThirdPartyBilling,
                                        '' as AccountChargeforshipment,
                                        '' as RateTableused,
                                        'P' as TheDHLExternalProductCode,
                                        'P' as TheDHLinternalProductCode,
                                        to_char(tsi.cdt, 'YYYYMMDD') as DateAWBwasentered,
                                        to_char(tsi.cdt, 'hh24mi') as TimeAWBwasentered,
                                        '' as DateAWBcomplete,
                                        '' as TimeAWBcomplete,
                                        (select sum(toi.carton_qty)
                                            from ppsuser.t_order_info toi
                                            where toi.delivery_no = tss.delivery_no
                                            and toi.shipment_id = tss.shipment_id) as Totalpiecesinshipment,
                                        'Y' as OneAWB#perpieceflag,
                                        (SELECT round(sum(VMI.GROSSWEIGHTKG * toi.qty),2) 
                                              FROM PPSUSER.T_ORDER_INFO TOI, PPTEST.VW_MPN_INFO VMI
                                             WHERE TOI.ICTPN = VMI.ICTPARTNO
                                               AND (TOI.DELIVERY_NO, TOI.SHIPMENT_ID) IN
                                                   (SELECT DISTINCT TSS.DELIVERY_NO, TSS.SHIPMENT_ID
                                                      FROM PPSUSER.T_SN_STATUS TSS
                                                     WHERE TSS.CARTON_NO = :CartonNo)) as ActualTotalShipmentWeight,
                                        '' as RoundedMFTotalShip,
                                        '' as DimensionalWeightCompFactor,
                                        '' as TotalDimensionalWeight,
                                        ppsuser.t_dhl_value('HAWB', TSI.SHIPMENT_ID) as TotalDeclaredValue,
                                        '0' as InsuranceAmount,
                                        '' as PaymentMethodChargeType,
                                        '' as CheckNumber,
                                        '' as CreditCardType,
                                        '' as CreditCardNumber,
                                        '' as CreditCardExpiryDate,
                                        '' as AmountReceived,
                                        '' as SpecialInstructionsCode,
                                        '' as RouteId,
                                        '' as CutOffCode,
                                        '' as CouriersInitials,
                                        '' as CourierPickupDate,
                                        '' as CourierPickupTime,
                                        '' as CustomsAlertDate,
                                        '' as CustomsAlertTime,
                                        'N' as LabelPrintingFlag,
                                        '' as PalletFlag,
                                        '0' as CODValue,
                                        '' as CODCurrencyCode,
                                        '' as CODPaymentMethod,
                                        '' as FixedDeliveryDate,
                                        'N' as DangerousGoodsFlag,
                                        '' as Class_,
                                        '0' as Numberofpallets,
                                        '' as PalletType,
                                        '' as InsuredAmountCurrencyCode,
                                        '' as DestinationTerminalCode,
                                        '' as DestinationBranchCode,
                                        '' as DeliveryInstruction,
                                        '' as OriginStationCode,
                                        '' as CustomerSiretNumber,
                                        '2' as NumberofContentsEntries,
                                        'ElectronicProducts' as ShipmentContents1,
                                        '' as ShipmentContents,
                                        '0' as NumberofChargeServiceentries,
                                        '0' as NumberofEntriestoFollow1,
                                        'N' as DutiableFlag,
                                        '' as ShippersExportLicense,
                                        '' as ShippersEINSSNorVAT#,
                                        '' as MovementCertificateITSAD,
                                        '' as ConsigneeImportLicense,
                                        '' as ConsigneeEINSSNorVAT#,
                                         decode(t9u.CarrierCode,'1060029373','DTP','1060032962','DAP',''), 
                                        --'DTU' as TermsofTrade,
                                        '' as ReasonforExportCode,
                                        '' as ReceiversFederalTaxId,
                                        '' as ShippersStateTaxId,
                                        '' as ReceiversStateTaxId,                          
                                        '3' as NumberofEntriestoFollow,
                                        t9u.deliveryno as ShipperReference1,
                                        '' as Weight1,
                                        '' as ShipperReference2,
                                        '' as Weight2,
                                        '' as ShipperReference,
                                        '' as Weight3,
                                        '1' as Numberofpieceentries,
                                        tss.babytracking_no as PieceIdentifier,
                                        vmi.GROSSWEIGHTKG as PieceWeight, 
                                        '' as PieceVolumetricWeight,
                                        '' as PieceVolume,
                                        '' as PieceLength,
                                        '' as PieceWidth,
                                        '' as PieceHeight,
                                        'KG' as UnitofMeasurement,
                                        tss.carton_no as ContentDescriptionText,
                                        '' as PackageType,
                                        '' as PackageRemarks,
                                        '' as PackageChargeCode,
                                        '3' as Numberofpiece,
                                        '' as Piecereference1,
                                        '' as Weight4,
                                        TSS.BOX_NO|| CHR(47)||(select sum(toi.Carton_Qty)
                                   from ppsuser.t_order_info toi
                                  where toi.delivery_no = tss.delivery_no
                                    and toi.shipment_id = tss.shipment_id) as Piecereference2, 
                                        '' as Weight5,
                                        '' as Piecereference3,
                                        '' as Weight
                            from ppsuser.t_940_unicode   t9u,
                                ppsuser.t_sn_status     tss,
                                ppsuser.t_shipper       tsh,
                                ppsuser.t_shipment_info tsi,
                                pptest.vw_mpn_info   vmi  
                            where t9u.deliveryno = tss.delivery_no
                            and t9u.custdelitem = tss.line_item
                            and tss.shipment_id = tsi.shipment_id
                            and vmi.ICTPARTNO = tss.part_no
                            and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getSPSForAppleECXFileInfo(string cartonNo)
        {
            string sql = @"select distinct '1' as TOTALNUMBEROFSHIPMENTRECORDS,
                                '      ' as BACKENDTYPE1,
                                'SPS 2.03' as SPSVERSIONID,
                                'SPS' as BACKENDTYPE,
                                '' as REFERENCEDATAVERSIONNUMBER,
                                '' as METERNUMBER,
                                '' as OPUNIT,
                                '' as PRINTERNUMBER,
                                '' as CONSIGNMENTNUMBER,
                                tss.tracking_no as AIRWAYBILLNUMBER,
                                'EIN' as ORIGINSERVICEAREA,
                                '' as DESTINATIONSERVICEAREA,
                                '' as INVOICENUMBER,
                                t9u.parcelaccountnumber as SHIPPERACCOUNTNUMBER,
                                (SELECT returnaddress2
                                   FROM pptest.oms_return_emeia_ds
                                  where shipcntycode = 'OTHER' and rownum=1) as SHIPPPERNAME,
                                'Syncreon' as SHIPPERCONTACTNAME,
                                (SELECT returnaddress3
                                   FROM pptest.oms_return_emeia_ds
                                  where shipcntycode = 'OTHER' and rownum=1) as SHIPPERADDRESSLINE1,
                                '.' as SHIPPERADDRESSLINE2,
                                '' as SHIPPERADDRESSLINE3,
                                'WAALWIJK' as SHIPPERCITY,
                                '' as SHIPPERSTATE,
                                '5145 RK' as SHIPPERZIPCODE,
                                'NETHERLANDS, THE' as SHIPPERCOUNTRYNAME,
                                'NL' as SHIPPERCOUNTRYCODE,
                                t9u.deliveryno as SHIPMENTREFERENCE,
                                '+31(0)416711300' as SHIPPERPHONENUMBER,
                                '' as SHIPPERFAXNUMBER,
                                '' as SHIPPERTELEXNUMBER,
                                '' as SHIPPERTELEXANSWERBACK,
                                '' as SHIPPERCURRENCYCODE,
                                'N' as SHIPPERWORLDCLASSACCTFLAG,
                                'USD' as RECEIVERCURRENCY,
                                t9u.parcelaccountnumber as RECEIVERACCOUNTNUMBER,
                                decode(t9u.shiptocompany,
                                        '',
                                        t9u.shiptoname,
                                        t9u.shiptocompany) as RECEIVERNAME, --如果ShipToCompany空白 ShipToName ,
                                t9u.shiptoname as RECEIVERCONTACTNAME,
                                t9u.shiptoaddress as RECEIVERADDRESSLINE1,
                                decode(t9u.shiptoaddress2, '', '.', t9u.shiptoaddress2) as RECEIVERADDRESSLINE2, --ShipToAddress2 如果空白 放.
                                t9u.shiptoaddress3 as RECEIVERADDRESSLINE3,
                                t9u.shiptocity as RECEIVERCITY,
                                decode(instr(t9u.regiondesc, '='),
                                        0,
                                        t9u.regiondesc,
                                        substr(t9u.regiondesc,
                                                0,
                                                instr(t9u.regiondesc, '=') - 1)) as RECEIVERSTATE,
                                t9u.shiptozip as RECEIVERZIPCODE,
                                t9u.shiptocountry as RECEIVERCOUNTRYNAME,
                                t9u.shipcntycode as RECEIVERCOUNTRYCODE,
                                decode(t9u.shiptoconttel,'','.',null,'.',t9u.shiptoconttel) as RECEIVERPHONE,
                                '' as RECEIVERFAXNUMBER,
                                '' as RECEIVERTELEXNUMBER,
                                '' as RECEIVERTELEXANSWERBACK,
                                '' as DHLSRECEIVERTABLEREF,
                                'N' as FLAGFORTHIRDPARTYBILLING,
                                '' as BILLINGACCOUNT,
                                '' as RATETABLEUSED,
                                'U' as NETWORKPRODUCTCODE,
                                'U' as LOCALPRODUCTCODE,
                                to_char(tsi.CDT, 'YYYYMMDD') as DATEAWBWASENTERED, --格式YYYYMMDD
                                to_char(tsi.CDT, 'hh24mi') as TIMEAWBWASENTERED, --格式HHMM
                                '' as DATEAWBCOMPLETE,
                                '' as TIMEAWBCOMPLETE,
                                (select sum(toi.pack_carton_qty)
                                    from ppsuser.t_order_info toi
                                    where toi.delivery_no = tss.delivery_no
                                    and toi.shipment_id = tss.shipment_id) as NUMBEROFPIECES,
                                'Y' as ONEAWBPERPIECEFLAG,
                                (select round(sum(t1.totalWeight), 2)
                                    from (select tss.part_no,
                                                tss.delivery_no,
                                                vmi.GROSSWEIGHTKG *
                                                count(distinct tss.carton_no) as totalWeight
                                            from ppsuser.t_sn_status tss,
                                                pptest.vw_mpn_info  vmi,
                                                ppsuser.t_sn_status tss1
                                            where tss.part_no = vmi.ICTPARTNO
                                            and tss.delivery_no = tss1.delivery_no
                                            and tss1.carton_no = :CartonNo
                                            group by tss.part_no,
                                                    tss.delivery_no,
                                                    vmi.GROSSWEIGHTKG) t1) as ACTUALTOTALSHIPMENTWEIGHT,
                                '' as SHIPMENTWEIGHT,
                                '' as DIMENSIONALWEIGHTCOMPFACTOR,
                                '' as TOTALDIMENSIONALWEIGHT,
                                ppsuser.t_dhl_value('BOX', TSS.CARTON_NO) as DECLAREDVALUE,
                                '0' as INSUREDVALUE,
                                '' as PAYMENTMETHOD,
                                '' as CHECKNUMBER,
                                '' as CREDITCARDTYPE,
                                '' as CREDITCARDNUMBER,
                                '' as CREDITCARDEXPIRYDATE,
                                '' as AMOUNTRECEIVED,
                                '' as SPECIALINSTRUCTIONTYPECODE,
                                '' as ROUTECODE,
                                '' as CUTOFFCODE,
                                '' as COURIERSINITIALS,
                                '' as PICKUPDATE,
                                '' as PICKUPTIME,
                                '' as CUSTOMSALERTDATE,
                                '' as CUSTOMSALERTTIME,
                                'N' as LABELPRINTINGFLAG,
                                '' as PALLETFLAG,
                                '0' as CODVALUE,
                                '' as CODCURRENCYCODE,
                                '' as CODPAYMENTMETHOD,
                                '' as FIXEDDELIVERYDATE,
                                'N' as DANGEROUSGOODSFLAG,
                                '' as CLASS,
                                '0' as NUMBEROFPALLETS,
                                '' as PALLETTYPE,
                                '' as INSUREDAMOUNTCURRENCYCODE,
                                '' as DESTINATIONTERMINALCODE,
                                '' as DESTINATIONBRANCHCODE,
                                '' as DELIVERYINSTRUCTION,
                                '' as ORIGINSTATIONCODE,
                                '' as CUSTOMERSIRETNUMBER,
                                '2' as NUMBEROFCONTENTSENTRIES,
                                'Electronic Products' as SHIPMENTCONTENTS1,
                                '' as SHIPMENTCONTENTS,
                                '0' as NUMBEROFCHARGESERVICEENTRIES,                    
                                '0' as NUMBEROFENTRIESTOFOLLOW1,
                                'N' as DUTIABLEFLAG,
                                '' as SHIPPEREXPORTLICENSE,
                                '' as SHIPPEREINSSNORVAT,
                                '' as MOVEMENTCERTIFICATEITSAD,
                                '' as RECEIVERIMPORTLICENSE,
                                '' as RECEIVEREINSSNORVAT,
                                'CIP' as TERMSOFTRADECODE,
                                '' as REASONFOREXPORTCODEBELOWITEMS,
                                '' as RECEIVERSFEDERALTAXID,
                                '' as SHIPPERSSTATETAXID,
                                '' as RECEIVERSSTATETAXID,                            
                                '3' as NUMBEROFENTRIESTOFOLLOW,
                                t9u.deliveryno as SHIPPERREFERENCE1,
                                '' as WEIGHT1,
                                '' as SHIPPERREFERENCE2,
                                '' as WEIGHT2,
                                '' as SHIPPERREFERENCE,
                                '' as WEIGHT3,
                                '1' as NUMBEROFPIECEENTRIESPART6NOOF,
                                tss.BABYTRACKING_NO as PIECEIDENTIFIER,
                                vmi.GROSSWEIGHTKG as PIECEWEIGHT,
                                '' as PIECEVOLUMETRICWEIGHT,
                                '' as PIECEVOLUME,
                                '' as PIECELENGTH,
                                '' as PIECEWIDTH,
                                '' as PIECEHEIGHT,
                                'KG' as UNITOFMEASUREMENT,
                                tss.carton_no as CONTENTDESCRIPTION,
                                '' as PACKAGETYPE,
                                '' as PACKAGEREMARKS,
                                '' as PACKAGECHARGECODE,
                                '3' as NUMBEROFPIECESHIPPERREFERENCEE,
                                '' as PIECEREFERENCE1,
                                '' as WEIGHT4,
                                TSS.BOX_NO|| CHR(47)||(select sum(toi.Carton_Qty)
                                   from ppsuser.t_order_info toi
                                  where toi.delivery_no = tss.delivery_no
                                    and toi.shipment_id = tss.shipment_id) as PIECEREFERENCE2,
                                '' as WEIGHT5,
                                '' as PIECEREFERENCE3,
                                '' as WEIGHT
                    from ppsuser.t_940_unicode   t9u,
                        ppsuser.t_sn_status     tss,
                        ppsuser.t_shipper       tsh,
                        ppsuser.t_shipment_info tsi,
                        pptest.vw_mpn_info      vmi
                    where t9u.deliveryno = tss.delivery_no
                    and t9u.custdelitem = tss.line_item
                    and tss.shipment_id = tsi.shipment_id
                    and vmi.ICTPARTNO = tss.part_no
                    and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getBBXBabyFileForEMEA(string cartonNo)
        {
            string sql = @"select distinct '1' as NUMBEROFAWBSHIPMENT,
                                '      ' as BACKENDTYPE1,
                                'SPS 2.03' as SPSVERSIONID,
                                'SPS' as BACKENDTYPE,
                                '' as REFERENCEDATAVERSIONNUMBER,
                                '' as METERNUMBER,
                                '' as OPUNIT,
                                '' as SCHEDULENUMBER,
                                tss.hawb as CONSIGNMENTNUMBER,
                                tss.tracking_no as SHIPMENTNUMBER,
                                '' as ORIGINIATA,
                                '' as DESTINATIONIATA,
                                '' as INVOICENUMBER,
                                t9u.parcelaccountnumber as SHIPPERSACCOUNTNUMBER,
                                tsh.ShipperName || '(A.I.)' as SHIPPERSCOMPANYNAME,
                                tsh.ShipperName || '(A.I.)' as SHIPPERSCONTACTNAME,
                                tsh.SHIPPERADDRESS1 as SHIPPERSADDRESSLINE1,
                                tsh.SHIPPERADDRESS2 as SHIPPERSADDRESSLINE2,
                                '' as SHIPPERSADDRESSLINE3,
                                tsh.SHIPPERCITY as SHIPPERSCITY,
                                tsh.SHIPPERState as SHIPPERSSTATE,
                                tsh.SHIPPERPostal as SHIPPERSZIPPOSTALCODE,
                                tsh.SHIPPERCountry as SHIPPERSCOUNTRYNAME,
                                tsh.SHIPPERCntyCode as SHIPPERSCOUNTRYCODE,
                                tss.delivery_no as SHIPPERSREFERENCE,
                                tsh.SHIPPERTel as SHIPPERSPHONENUMBER,
                                '' as SHIPPERSFAXNUMBER,
                                '' as SHIPPERSTELEXNUMBER,
                                '' as SHIPPERSTELEXANSWERBACK,
                                '' as SHIPPERSCURRENCYCODE,
                                'N' as SHIPPERSWORLDCLASSACCTFLAG,
                                '' as RECIPIENTCODE,
                                '' as RECIPIENTACCOUNTNUMBER,
                                decode(t9u.shiptocompany,
                                        '',
                                        t9u.shiptoname,
                                        t9u.shiptocompany) as RECIPIENTCOMPANYNAME, --如果ShipToCompany空白  ShipToName
                                t9u.shiptoname as RECIPIENTATTENTIONNAME,
                                t9u.shiptoaddress as RECIPIENTADDRESSLINE1,
                                decode(t9u.shiptoaddress2, '', '.', t9u.shiptoaddress2) as RECIPIENTADDRESSLINE2, --ShipToAddress2 如果空白 放.
                                t9u.shiptoaddress3 as RECIPIENTADDRESSLINE3,
                                t9u.shiptocity as RECIPIENTCITY,
                                decode(instr(t9u.regiondesc, '='),
                                        0,
                                        t9u.regiondesc,
                                        substr(t9u.regiondesc,
                                                0,
                                                instr(t9u.regiondesc, '=') - 1)) as RECIPIENTSTATE, --RegionDesc = 前面
                                t9u.shiptozip as RECIPIENTZIPCODE,
                                t9u.shiptocountry as RECIPIENTCOUNTRYNAME,
                                t9u.shipcntycode as RECIPIENTCOUNTRYCODE,
                                decode(t9u.shiptoconttel,'','.',null,'.',t9u.shiptoconttel) as RECIPIENTPHONENUMBER,
                                '' as RECIPIENTFAXNUMBER,
                                '' as RECIPIENTTELEXNUMBER,
                                '' as RECIPIENTTELEXANSWERBACK,
                                '' as DHLSRECIPIENTTABLEREF,
                                'N' as FLAGFORTHIRDPARTYBILLING,
                                '' as ACCOUNTCHARGEFORSHIPMENT,
                                '' as RATETABLEUSED,
                                'B' as THEDHLEXTERNALPRODUCTCODE,
                                'B' as THEDHLINTERNALPRODUCTCODE,
                                to_char(tsi.cdt, 'YYYYMMDD') as DATEAWBWASENTERED,
                                TO_CHAR(TSI.CDT, 'hh24mi') as TIMEAWBWASENTERED,
                                '' as DATEAWBCOMPLETE,
                                '' as TIMEAWBCOMPLETE,
                                (SELECT SUM(TOI.CARTON_QTY)  FROM  PPSUSER.T_ORDER_INFO  TOI
                                WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
                                AND TOI.SHIPMENT_ID = TSI.SHIPMENT_ID) as TOTALPIECESINSHIPMENT,
                                'Y' as ONEAWBPERPIECEFLAG,
                                (select round(sum(t1.totalWeight), 2)
                                    from (select tss.part_no,
                                                tss.delivery_no,
                                                vmi.GROSSWEIGHTKG *
                                                count(distinct tss.carton_no) as totalWeight
                                            from ppsuser.t_sn_status tss,
                                                PPTEST.vw_mpn_info  vmi,
                                                ppsuser.t_sn_status tss1
                                            where tss.part_no = vmi.ICTPARTNO
                                            and tss.delivery_no = tss1.delivery_no
                                            and tss1.carton_no = :CartonNo
                                            group by tss.part_no,
                                                    tss.delivery_no,
                                                    vmi.GROSSWEIGHTKG) t1) as ACTUALTOTALSHIPMENTWEIGHT,
                                '' as ROUNDEDMFTTOTALSHIPWT,
                                '' as DIMENSIONALWEIGHTCOMPFACTOR,
                                '' as TOTALDIMENSIONALWEIGHT,
                                '0' as TOTALDECLAREDVALUE,
                                '0' as INSURANCEAMOUNT,
                                '' as PAYMENTMETHODCHARGETYPE,
                                '' as CHECKNUMBER,
                                '' as CREDITCARDTYPE,
                                '' as CREDITCARDNUMBER,
                                '' as CREDITCARDEXPIRYDATE,
                                '' as AMOUNTRECEIVED,
                                '' as SPECIALINSTRUCTIONSCODE,
                                '' as ROUTEID,
                                '' as CUTOFFCODE,
                                '' as COURIERSINITIALS,
                                '' as COURIERPICKUPDATE,
                                '' as COURIERPICKUPTIME,
                                '' as CUSTOMSALERTDATE,
                                '' as CUSTOMSALERTTIME,
                                'N' as LABELPRINTINGFLAG,
                                '' as PALLETFLAG,
                                '0' as CODVALUE,
                                '' as CODCURRENCYCODE,
                                '' as CODPAYMENTMETHOD,
                                '' as FIXEDDELIVERYDATE,
                                'N' as DANGEROUSGOODSFLAG,
                                '' as CLASS,
                                '0' as NUMBEROFPALLETS,
                                '' as PALLETTYPE,
                                '' as INSUREDAMOUNTCURRENCYCODE,
                                '' as DESTINATIONTERMINALCODE,
                                '' as DESTINATIONBRANCHCODE,
                                '' as DELIVERYINSTRUCTION,
                                '' as ORIGINSTATIONCODE,
                                '' as CUSTOMERSIRETNUMBER,
                                '2' as NUMBEROFCONTENTSENTRIESBELOWI,
                                'Electronic Products' as SHIPMENTCONTENTS1,
                                '' as SHIPMENTCONTENTS,
                                '0' as NUMBEROFCHARGESERVICEENTRIESB,                                
                                0 as NUMBEROFENTRIESTOFOLLOW1,
                                'N' as DUTIABLEFLAG,
                                '' as SHIPPERSEXPORTLICENSE,
                                '' as SHIPPERSEINSSNORVAT,
                                '' as MOVEMENTCERTIFICATEITSAD,
                                '' as CONSIGNEEIMPORTLICENSE,
                                '' as CONSIGNEEEINSSNORVAT,
                                '' as TERMSOFTRADE,
                                '' as REASONFOREXPORTCODEBELOWITEMS,
                                '' as RECEIVERSFEDERALTAXID,
                                '' as SHIPPERSSTATETAXID,
                                '' as RECEIVERSSTATETAXID,                                
                                '3' as NUMBEROFENTRIESTOFOLLOW,
                                TSS.DELIVERY_NO as SHIPPERREFERENCE1,
                                '' as WEIGHT1,
                                '' as SHIPPERREFERENCE2,
                                '' as WEIGHT2,
                                '' as SHIPPERREFERENCE,
                                '' as WEIGHT3,
                                '1' as NUMBEROFPIECEENTRIESPART6NOOF,
                                TSS.babytracking_no as PIECEIDENTIFIER,
                                VMI.GROSSWEIGHTKG as PIECEWEIGHT,
                                '' as PIECEVOLUMETRICWEIGHT,
                                '' as PIECEVOLUME,
                                '' as PIECELENGTH,
                                '' as PIECEWIDTH,
                                '' as PIECEHEIGHT,
                                'KG' as UNITOFMEASUREMENT,
                                TSS.CARTON_NO as CONTENTDESCRIPTIONTEXT,
                                '' as PACKAGETYPE,
                                '' as PACKAGEREMARKS,
                                '' as PACKAGECHARGECODE,
                                '3' as NUMBEROFPIECESHIPPERREFERENCEE,
                                '' as PIECEREFERENCE1,
                                '' as WEIGHT4,
                                tss.box_no||'/'||(select sum(toi.carton_qty)
                                       from ppsuser.t_order_info toi
                                      where toi.delivery_no = tss.delivery_no) as PIECEREFERENCE2,
                                '' as WEIGHT5,
                                '' as PIECEREFERENCE3,
                                '' as WEIGHT
                    from ppsuser.t_940_unicode   t9u,
                        ppsuser.t_sn_status     tss,
                        pptest.vw_mpn_info      vmi,
                        ppsuser.t_shipper       tsh,
                        ppsuser.t_shipment_info tsi
                    where t9u.deliveryno = tss.delivery_no
                    and t9u.custdelitem = tss.line_item
                    and tss.part_no = vmi.ICTPARTNO
                    and tss.shipment_id = tsi.shipment_id
                    and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getBBXBabyFileForPAC(string cartonNo)
        {
            #region
            //string sql = @"select distinct '1' as NUMBEROFAWBSHIPMENT,
            //                        '      ' as BACKENDTYPE,
            //                        'SPS 2.03' as SPSVERSIONID,
            //                        'SPS' as BACKENDTYPE,
            //                        '' as REFERENCEDATAVERSIONNUMBER,
            //                        '' as METERNUMBER,
            //                        '' as OPUNIT,
            //                        '' as SCHEDULENUMBER,
            //                        tss.hawb as CONSIGNMENTNUMBER,
            //                        tss.tracking_no as SHIPMENTNUMBER,
            //                        '' as ORIGINIATA,
            //                        '' as DESTINATIONIATA,
            //                        '' as INVOICENUMBER,
            //                        t9u.parcelaccountnumber as SHIPPERSACCOUNTNUMBER,
            //                        tsh.ShipperName as SHIPPERSCOMPANYNAME,
            //                        tsh.ShipperName as SHIPPERSCONTACTNAME,
            //                        tsh.SHIPPERADDRESS1 as SHIPPERSADDRESSLINE1,
            //                        tsh.SHIPPERADDRESS2 as SHIPPERSADDRESSLINE2,
            //                        '' as SHIPPERSADDRESSLINE3,
            //                        tsh.SHIPPERCITY as SHIPPERSCITY,
            //                        tsh.SHIPPERState as SHIPPERSSTATE,
            //                        tsh.SHIPPERPostal as SHIPPERSZIPPOSTALCODE,
            //                        tsh.SHIPPERCountry as SHIPPERSCOUNTRYNAME,
            //                        tsh.SHIPPERCntyCode as SHIPPERSCOUNTRYCODE,
            //                        tss.delivery_no as SHIPPERSREFERENCE,
            //                        tsh.SHIPPERTel as SHIPPERSPHONENUMBER,
            //                        '' as SHIPPERSFAXNUMBER,
            //                        '' as SHIPPERSTELEXNUMBER,
            //                        '' as SHIPPERSTELEXANSWERBACK,
            //                        '' as SHIPPERSCURRENCYCODE,
            //                        'N' as SHIPPERSWORLDCLASSACCTFLAG,
            //                        '' as RECIPIENTCODE,
            //                        '' as RECIPIENTACCOUNTNUMBER,
            //                        decode(t9u.shiptocompany,
            //                               '',
            //                               t9u.shiptoname,
            //                               t9u.shiptocompany) as RECIPIENTCOMPANYNAME, --如果ShipToCompany空白 ShipToName 
            //                        t9u.shiptoname as RECIPIENTATTENTIONNAME,
            //                        t9u.shiptoaddress as RECIPIENTADDRESSLINE1,
            //                        decode(t9u.shiptoaddress2, '', '.', t9u.shiptoaddress2) as RECIPIENTADDRESSLINE2, --ShipToAddress2 如果空白 放.
            //                        t9u.shiptoaddress3 as RECIPIENTADDRESSLINE3,
            //                        t9u.shiptocity as RECIPIENTCITY,
            //                        decode(instr(t9u.regiondesc, '='),
            //                               0,
            //                               t9u.regiondesc,
            //                               substr(t9u.regiondesc,
            //                                      0,
            //                                      instr(t9u.regiondesc, '=') - 1)) as RECIPIENTSTATE,
            //                        t9u.shiptozip as RECIPIENTZIPCODE,
            //                        t9u.shiptocountry as RECIPIENTCOUNTRYNAME,
            //                        t9u.shipcntycode as RECIPIENTCOUNTRYCODE,
            //                        decode(t9u.shiptoconttel,'','.',null,'.',t9u.shiptoconttel) as RECIPIENTPHONENUMBER,
            //                        '' as RECIPIENTFAXNUMBER,
            //                        '' as RECIPIENTTELEXNUMBER,
            //                        '' as RECIPIENTTELEXANSWERBACK,
            //                        '' as DHLSRECIPIENTTABLEREF,
            //                        'N' as FLAGFORTHIRDPARTYBILLING,
            //                        '' as ACCOUNTCHARGEFORSHIPMENT,
            //                        '' as RATETABLEUSED,
            //                        'B' as THEDHLEXTERNALPRODUCTCODE,
            //                        'B' as THEDHLINTERNALPRODUCTCODE,
            //                        to_char(tsi.cdt, 'YYYYMMDD') as DATEAWBWASENTERED,
            //                        to_char(tsi.cdt, 'hh24mi') as TIMEAWBWASENTERED,
            //                        '' as DATEAWBCOMPLETE,
            //                        '' as TIMEAWBCOMPLETE,
            //                        (SELECT SUM(TOI.CARTON_QTY)  FROM  PPSUSER.T_ORDER_INFO  TOI
            //                        WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
            //                        AND TOI.SHIPMENT_ID = TSI.SHIPMENT_ID) as TOTALPIECESINSHIPMENT,
            //                        'Y' as ONEAWBPERPIECEFLAG,
            //                        ( select round(sum(t1.totalWeight), 2)
            //                            from (select toi.ictpn,
            //                                         toi.delivery_no,
            //                                         vmi.GROSSWEIGHTKG *
            //                                         sum(toi.carton_qty) as totalWeight
            //                                    from ppsuser.t_order_info toi,
            //                                         PPTEST.vw_mpn_info   vmi
            //                                   where toi.ictpn =
            //                                         vmi.ICTPARTNO
            //                                     and toi.delivery_no =
            //                                         (SELECT DISTINCT TSS.DELIVERY_NO
            //                                            FROM PPSUSER.T_SN_STATUS TSS
            //                                           WHERE TSS.carton_no =: CartonNo)
            //                                   group by toi.ictpn,
            //                                            toi.delivery_no,
            //                                            vmi.GROSSWEIGHTKG) t1) as ACTUALTOTALSHIPMENTWEIGHT,
            //                        '' as ROUNDEDMFTTOTALSHIPWT,
            //                        '' as DIMENSIONALWEIGHTCOMPFACTOR,
            //                        '' as TOTALDIMENSIONALWEIGHT,
            //                        '0' as TOTALDECLAREDVALUE,
            //                        '0' as INSURANCEAMOUNT,
            //                        '' as PAYMENTMETHODCHARGETYPE,
            //                        '' as CHECKNUMBER,
            //                        '' as CREDITCARDTYPE,
            //                        '' as CREDITCARDNUMBER,
            //                        '' as CREDITCARDEXPIRYDATE,
            //                        '' as AMOUNTRECEIVED,
            //                        '' as SPECIALINSTRUCTIONSCODE,
            //                        '' as ROUTEID,
            //                        '' as CUTOFFCODE,
            //                        '' as COURIERSINITIALS,
            //                        '' as COURIERPICKUPDATE,
            //                        '' as COURIERPICKUPTIME,
            //                        '' as CUSTOMSALERTDATE,
            //                        '' as CUSTOMSALERTTIME,
            //                        'N' as LABELPRINTINGFLAG,
            //                        '' as PALLETFLAG,
            //                        '0' as CODVALUE,
            //                        '' as CODCURRENCYCODE,
            //                        '' as CODPAYMENTMETHOD,
            //                        '' as FIXEDDELIVERYDATE,
            //                        'N' as DANGEROUSGOODSFLAG,
            //                        '' as CLASS_,
            //                        '0' as NUMBEROFPALLETS,
            //                        '' as PALLETTYPE,
            //                        '' as INSUREDAMOUNTCURRENCYCODE,
            //                        '' as DESTINATIONTERMINALCODE,
            //                        '' as DESTINATIONBRANCHCODE,
            //                        '' as DELIVERYINSTRUCTION,
            //                        '' as ORIGINSTATIONCODE,
            //                        '' as CUSTOMERSIRETNUMBER,
            //                        '2' as NUMBEROFCONTENTSENTRIESBELOWI,
            //                        'Electronic Products' as SHIPMENTCONTENTS,
            //                        '' as SHIPMENTCONTENTS,
            //                        '0' as NUMBEROFCHARGESERVICEENTRIESB,                                 
            //                        '0' as NUMBEROFENTRIESTOFOLLOW,
            //                        'N' as DUTIABLEFLAG,
            //                        '' as SHIPPERSEXPORTLICENSE,
            //                        '' as SHIPPERSEINSSNORVAT,
            //                        '' as MOVEMENTCERTIFICATEITSAD,
            //                        '' as CONSIGNEEIMPORTLICENSE,
            //                        '' as CONSIGNEEEINSSNORVAT,
            //                        '' as TERMSOFTRADE,--
            //                        '' as REASONFOREXPORTCODEBELOWITEMS,
            //                        '' as RECEIVERSFEDERALTAXID,
            //                        '' as SHIPPERSSTATETAXID,
            //                        '' as RECEIVERSSTATETAXID,                                 
            //                        '3' as NUMBEROFENTRIESTOFOLLOW,
            //                        tss.delivery_no as SHIPPERREFERENCE,
            //                        '' as WEIGHT,
            //                        '' as SHIPPERREFERENCE,
            //                        '' as WEIGHT,
            //                        '' as SHIPPERREFERENCE,
            //                        '' as WEIGHT,
            //                        '1' as NUMBEROFPIECEENTRIESPART6NOOF,
            //                        tss.babytracking_no as PIECEIDENTIFIER,
            //                        vmi.GROSSWEIGHTKG as PIECEWEIGHT,
            //                        '' as PIECEVOLUMETRICWEIGHT,
            //                        '' as PIECEVOLUME,
            //                        '' as PIECELENGTH,
            //                        '' as PIECEWIDTH,
            //                        '' as PIECEHEIGHT,
            //                        'KG' as UNITOFMEASUREMENT,
            //                        tss.carton_no as CONTENTDESCRIPTIONTEXT,
            //                        '' as PACKAGETYPE,
            //                        '' as PACKAGEREMARKS,
            //                        '' as PACKAGECHARGECODE,
            //                        '3' as NUMBEROFPIECESHIPPERREFERENCEE,
            //                        '' as PIECEREFERENCE1,
            //                        '' as WEIGHT,
            //                         tss.box_no||'/'||(select sum(toi.carton_qty)
            //                           from ppsuser.t_order_info toi
            //                          where toi.delivery_no = tss.delivery_no) as PIECEREFERENCE2,
            //                        '' as WEIGHT,
            //                        '' as PIECEREFERENCE3,
            //                        '' as WEIGHT
            //          from ppsuser.t_940_unicode   t9u,
            //               ppsuser.t_sn_status     tss,
            //               pptest.vw_mpn_info      vmi,
            //               ppsuser.t_shipper       tsh,
            //               ppsuser.t_shipment_info tsi
            //         where t9u.deliveryno = tss.delivery_no
            //           and t9u.custdelitem = tss.line_item
            //           and tss.part_no = vmi.ICTPARTNO                    
            //           and tss.shipment_id = tsi.shipment_id
            //           and tss.carton_no = :CartonNo";
            #endregion
            string sql = @"select distinct '1' as NUMBEROFAWBSHIPMENT,
                                    '      ' as BACKENDTYPE1,
                                    'SPS 2.03' as SPSVERSIONID,
                                    'SPS' as BACKENDTYPE,
                                    '' as REFERENCEDATAVERSIONNUMBER,
                                    '' as METERNUMBER,
                                    '' as OPUNIT,
                                    '' as SCHEDULENUMBER,
                                    tss.hawb as CONSIGNMENTNUMBER,
                                    tss.tracking_no as SHIPMENTNUMBER,
                                    '' as ORIGINIATA,
                                    '' as DESTINATIONIATA,
                                    '' as INVOICENUMBER,
                                    t9u.parcelaccountnumber as SHIPPERSACCOUNTNUMBER,
                                    tsh.ShipperName || '(A.I.)' as SHIPPERSCOMPANYNAME,
                                    tsh.ShipperName || '(A.I.)' as SHIPPERSCONTACTNAME,
                                    tsh.SHIPPERADDRESS1 as SHIPPERSADDRESSLINE1,
                                    tsh.SHIPPERADDRESS2 as SHIPPERSADDRESSLINE2,
                                    '' as SHIPPERSADDRESSLINE3,
                                    tsh.SHIPPERCITY as SHIPPERSCITY,
                                    tsh.SHIPPERState as SHIPPERSSTATE,
                                    tsh.SHIPPERPostal as SHIPPERSZIPPOSTALCODE,
                                    tsh.SHIPPERCountry as SHIPPERSCOUNTRYNAME,
                                    tsh.SHIPPERCntyCode as SHIPPERSCOUNTRYCODE,
                                    tss.delivery_no as SHIPPERSREFERENCE,
                                    tsh.SHIPPERTel as SHIPPERSPHONENUMBER,
                                    '' as SHIPPERSFAXNUMBER,
                                    '' as SHIPPERSTELEXNUMBER,
                                    '' as SHIPPERSTELEXANSWERBACK,
                                    '' as SHIPPERSCURRENCYCODE,
                                    'N' as SHIPPERSWORLDCLASSACCTFLAG,
                                    '' as RECIPIENTCODE,
                                    '' as RECIPIENTACCOUNTNUMBER,
                                    NVL(t9u.shiptocompany,t9u.shiptoname) as RECIPIENTCOMPANYNAME, --如果ShipToCompany空白 ShipToName 
                                    decode(t9u.shipcntycode,'JP',t9u.shiptoname||'様',t9u.shiptoname) as RECIPIENTATTENTIONNAME,
                                    DECODE(T9U.SHIPCNTYCODE,'JP',t9u.shiptocity,t9u.shiptoaddress) as RECIPIENTADDRESSLINE1,
                                    DECODE(T9U.SHIPCNTYCODE,'JP',t9u.shiptoAddress,nvl(t9u.shiptoaddress2,'.')) as RECIPIENTADDRESSLINE2, --ShipToAddress2 如果空白 放.
                                    decode(t9u.shipcntycode,'TW',t9u.shiptoaddress4,'NZ',t9u.shiptoaddress4,'AU','','SG','','HK','','KR','','JP',t9u.shiptoaddress2,t9u.shiptoaddress3) as RECIPIENTADDRESSLINE3,
                                    DECODE(T9U.SHIPCNTYCODE,'JP','',t9u.shiptocity) as RECIPIENTCITY,
                                    DECODE(T9U.SHIPCNTYCODE,'TW','','NZ','','SG','','HK','','JP','','AU',decode(instr(t9u.regiondesc, '='),0,t9u.regiondesc,substr(t9u.regiondesc,0,instr(t9u.regiondesc, '=') - 1)),decode(instr(t9u.regiondesc, '='),0,t9u.regiondesc,substr(t9u.regiondesc,instr(t9u.regiondesc, '=') + 1))) as RECIPIENTSTATE,
                                    DECODE(T9U.SHIPCNTYCODE,'HK','.',NVL(t9u.shiptozip,'.')) as RECIPIENTZIPCODE,
                                    DECODE(T9U.SHIPCNTYCODE,'JP','',t9u.shiptocountry) as RECIPIENTCOUNTRYNAME,
                                    t9u.shipcntycode as RECIPIENTCOUNTRYCODE,
                                    nvl(t9u.shiptoconttel,'.') as RECIPIENTPHONENUMBER,
                                    '' as RECIPIENTFAXNUMBER,
                                    '' as RECIPIENTTELEXNUMBER,
                                    '' as RECIPIENTTELEXANSWERBACK,
                                    '' as DHLSRECIPIENTTABLEREF,
                                    'N' as FLAGFORTHIRDPARTYBILLING,
                                    '' as ACCOUNTCHARGEFORSHIPMENT,
                                    '' as RATETABLEUSED,
                                    'B' as THEDHLEXTERNALPRODUCTCODE,
                                    'B' as THEDHLINTERNALPRODUCTCODE,
                                    to_char(tsi.cdt, 'YYYYMMDD') as DATEAWBWASENTERED,
                                    to_char(tsi.cdt, 'hh24mi') as TIMEAWBWASENTERED,
                                    '' as DATEAWBCOMPLETE,
                                    '' as TIMEAWBCOMPLETE,
                                    (SELECT SUM(TOI.CARTON_QTY)  FROM  PPSUSER.T_ORDER_INFO  TOI
                                    WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
                                    AND TOI.SHIPMENT_ID = TSI.SHIPMENT_ID) as TOTALPIECESINSHIPMENT,
                                    'Y' as ONEAWBPERPIECEFLAG,
                                    ( select round(sum(t1.totalWeight), 2)
                                        from (select toi.ictpn,
                                                     toi.delivery_no,
                                                     vmi.GROSSWEIGHTKG *
                                                     sum(toi.carton_qty) as totalWeight
                                                from ppsuser.t_order_info toi,
                                                     PPTEST.vw_mpn_info   vmi
                                               where toi.ictpn =
                                                     vmi.ICTPARTNO
                                                 and toi.delivery_no =
                                                     (SELECT DISTINCT TSS.DELIVERY_NO
                                                        FROM PPSUSER.T_SN_STATUS TSS
                                                       WHERE TSS.carton_no =:CartonNo)
                                               group by toi.ictpn,
                                                        toi.delivery_no,
                                                        vmi.GROSSWEIGHTKG) t1) as ACTUALTOTALSHIPMENTWEIGHT,
                                    '' as ROUNDEDMFTTOTALSHIPWT,
                                    '' as DIMENSIONALWEIGHTCOMPFACTOR,
                                    '' as TOTALDIMENSIONALWEIGHT,
                                    '0' as TOTALDECLAREDVALUE,
                                    '0' as INSURANCEAMOUNT,
                                    '' as PAYMENTMETHODCHARGETYPE,
                                    '' as CHECKNUMBER,
                                    '' as CREDITCARDTYPE,
                                    '' as CREDITCARDNUMBER,
                                    '' as CREDITCARDEXPIRYDATE,
                                    '' as AMOUNTRECEIVED,
                                    '' as SPECIALINSTRUCTIONSCODE,
                                    '' as ROUTEID,
                                    '' as CUTOFFCODE,
                                    '' as COURIERSINITIALS,
                                    '' as COURIERPICKUPDATE,
                                    '' as COURIERPICKUPTIME,
                                    '' as CUSTOMSALERTDATE,
                                    '' as CUSTOMSALERTTIME,
                                    'N' as LABELPRINTINGFLAG,
                                    '' as PALLETFLAG,
                                    '0' as CODVALUE,
                                    '' as CODCURRENCYCODE,
                                    '' as CODPAYMENTMETHOD,
                                    '' as FIXEDDELIVERYDATE,
                                    'N' as DANGEROUSGOODSFLAG,
                                    '' as CLASS_,
                                    '0' as NUMBEROFPALLETS,
                                    '' as PALLETTYPE,
                                    '' as INSUREDAMOUNTCURRENCYCODE,
                                    '' as DESTINATIONTERMINALCODE,
                                    '' as DESTINATIONBRANCHCODE,
                                    '' as DELIVERYINSTRUCTION,
                                    '' as ORIGINSTATIONCODE,
                                    '' as CUSTOMERSIRETNUMBER,
                                    '2' as NUMBEROFCONTENTSENTRIESBELOWI,
                                    'Electronic Products' as SHIPMENTCONTENTS1,
                                    '' as SHIPMENTCONTENTS,
                                    '0' as NUMBEROFCHARGESERVICEENTRIESB,                                 
                                    '0' as NUMBEROFENTRIESTOFOLLOW1,
                                    'N' as DUTIABLEFLAG,
                                    '' as SHIPPERSEXPORTLICENSE,
                                    '' as SHIPPERSEINSSNORVAT,
                                    '' as MOVEMENTCERTIFICATEITSAD,
                                    '' as CONSIGNEEIMPORTLICENSE,
                                    '' as CONSIGNEEEINSSNORVAT,
                                    '' as TERMSOFTRADE,--
                                    '' as REASONFOREXPORTCODEBELOWITEMS,
                                    '' as RECEIVERSFEDERALTAXID,
                                    '' as SHIPPERSSTATETAXID,
                                    '' as RECEIVERSSTATETAXID,                                 
                                    '3' as NUMBEROFENTRIESTOFOLLOW,
                                    tss.delivery_no as SHIPPERREFERENCE1,
                                    '' as WEIGHT1,
                                    '' as SHIPPERREFERENCE2,
                                    '' as WEIGHT2,
                                    '' as SHIPPERREFERENCE,
                                    '' as WEIGHT3,
                                    '1' as NUMBEROFPIECEENTRIESPART6NOOF,
                                    tss.babytracking_no as PIECEIDENTIFIER,
                                    vmi.GROSSWEIGHTKG as PIECEWEIGHT,
                                    '' as PIECEVOLUMETRICWEIGHT,
                                    '' as PIECEVOLUME,
                                    '' as PIECELENGTH,
                                    '' as PIECEWIDTH,
                                    '' as PIECEHEIGHT,
                                    'KG' as UNITOFMEASUREMENT,
                                    tss.carton_no as CONTENTDESCRIPTIONTEXT,
                                    '' as PACKAGETYPE,
                                    '' as PACKAGEREMARKS,
                                    '' as PACKAGECHARGECODE,
                                    '3' as NUMBEROFPIECESHIPPERREFERENCEE,
                                    '' as PIECEREFERENCE1,
                                    '' as WEIGHT4,
                                     tss.box_no||'/'||(select sum(toi.carton_qty)
                                       from ppsuser.t_order_info toi
                                      where toi.delivery_no = tss.delivery_no) as PIECEREFERENCE2,
                                    '' as WEIGHT5,
                                    '' as PIECEREFERENCE3,
                                    '' as WEIGHT
                      from ppsuser.t_940_unicode   t9u,
                           ppsuser.t_sn_status     tss,
                           pptest.vw_mpn_info      vmi,
                           ppsuser.t_shipper       tsh,
                           ppsuser.t_shipment_info tsi
                     where t9u.deliveryno = tss.delivery_no
                       and t9u.custdelitem = tss.line_item
                       and tss.part_no = vmi.ICTPARTNO                    
                       and tss.shipment_id = tsi.shipment_id
                       and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getSPSImportBBXMotherForEMEA(string shipmentId)
        {
            string sql = @"select distinct '1' as NUMBEROFAWBSHIPMENT,
                                    '      ' as BACKENDTYPE1,
                                    'SPS 2.03' as SPSVERSIONID,
                                    'SPS' as BACKENDTYPE,
                                    '' as REFERENCEDATAVERSIONNUMBER,
                                    '' as METERNUMBER,
                                    '' as OPUNIT,
                                    '' as SCHEDULENUMBER,
                                    tss.hawb as CONSIGNMENTNUMBER,
                                    tss.hawb as SHIPMENTNUMBER,
                                    '' as ORIGINIATA,
                                    '' as DESTINATIONIATA,
                                    '' as INVOICENUMBER,
                                    '' as SHIPPERSACCOUNTNUMBER,
                                    tsh.ShipperName as SHIPPERSCOMPANYNAME,
                                    tsh.ShipperName as SHIPPERSCONTACTNAME,
                                    tsh.SHIPPERADDRESS1 as SHIPPERSADDRESSLINE1,
                                    tsh.SHIPPERADDRESS2 as SHIPPERSADDRESSLINE2,
                                    '' as SHIPPERSADDRESSLINE3,
                                    tsh.SHIPPERCITY as SHIPPERSCITY,
                                    tsh.SHIPPERState as SHIPPERSSTATE,
                                    tsh.SHIPPERPostal as SHIPPERSZIPPOSTALCODE,
                                    tsh.SHIPPERCountry as SHIPPERSCOUNTRYNAME,
                                    tsh.SHIPPERCntyCode as SHIPPERSCOUNTRYCODE,
                                    tss.hawb as SHIPPERSREFERENCE,
                                    tsh.SHIPPERTel as SHIPPERSPHONENUMBER,
                                    '' as SHIPPERSFAXNUMBER,
                                    '' as SHIPPERSTELEXNUMBER,
                                    '' as SHIPPERSTELEXANSWERBACK,
                                    '' as SHIPPERSCURRENCYCODE,
                                    'N' as SHIPPERSWORLDCLASSACCTFLAG,
                                    'USD' as RECIPIENTCODE,
                                    '' as RECIPIENTACCOUNTNUMBER,
                                    '' as RECIPIENTCOMPANYNAME,
                                    '' as RECIPIENTATTENTIONNAME,
                                    '' as RECIPIENTADDRESSLINE1,
                                    '' as RECIPIENTADDRESSLINE2,
                                    '' as RECIPIENTADDRESSLINE3,
                                    '' as RECIPIENTCITY,
                                    '' as RECIPIENTSTATE,
                                    '' as RECIPIENTZIPCODE,
                                    '' as RECIPIENTCOUNTRYNAME,
                                    '' as RECIPIENTCOUNTRYCODE,
                                    '' as RECIPIENTPHONENUMBER,
                                    '' as RECIPIENTFAXNUMBER,
                                    '' as RECIPIENTTELEXNUMBER,
                                    '' as RECIPIENTTELEXANSWERBACK,
                                    '' as DHLSRECIPIENTTABLEREF,
                                    'N' as FLAGFORTHIRDPARTYBILLING,
                                    '' as ACCOUNTCHARGEFORSHIPMENT,
                                    '' as RATETABLEUSED,
                                    'P' as THEDHLEXTERNALPRODUCTCODE,
                                    'P' as THEDHLINTERNALPRODUCTCODE,
                                    to_char(tsi.CDT, 'YYYYMMDD') as DATEAWBWASENTERED,
                                    to_char(tsi.cdt, 'hh24mi') as TIMEAWBWASENTERED,
                                    '' as DATEAWBCOMPLETE,
                                    '' as TIMEAWBCOMPLETE,
                                    (select sum(tsi.carton_qty)
                                       from ppsuser.t_shipment_info tsi
                                      where tsi.shipment_id = tss.shipment_id) as TOTALPIECESINSHIPMENT,
                                    'Y' as ONEAWBPERPIECEFLAG,
                                    (select round(sum(t1.totalWeight), 2)
                                       from (select tss.part_no,
                                                    tss.delivery_no,
                                                    vmi.GROSSWEIGHTKG *
                                                    count(distinct tss.carton_no) as totalWeight
                                               from ppsuser.t_sn_status tss,
                                                    pptest.vw_mpn_info  vmi
                                              where tss.part_no = vmi.ICTPARTNO
                                                and tss.shipment_id = :ShipmentId
                                              group by tss.part_no,
                                                       tss.delivery_no,
                                                       vmi.GROSSWEIGHTKG) t1) as ACTUALTOTALSHIPMENTWEIGHT,
                                    '' as ROUNDEDMFTTOTALSHIPWT,
                                    '' as DIMENSIONALWEIGHTCOMPFACTOR,
                                    '' as TOTALDIMENSIONALWEIGHT,
                                    '0' as TOTALDECLAREDVALUE,
                                    '0' as INSURANCEAMOUNT,
                                    '' as PAYMENTMETHODCHARGETYPE,
                                    '' as CHECKNUMBER,
                                    '' as CREDITCARDTYPE,
                                    '' as CREDITCARDNUMBER,
                                    '' as CREDITCARDEXPIRYDATE,
                                    '' as AMOUNTRECEIVED,
                                    '' as SPECIALINSTRUCTIONSCODE,
                                    '' as ROUTEID,
                                    '' as CUTOFFCODE,
                                    '' as COURIERSINITIALS,
                                    '' as COURIERPICKUPDATE,
                                    '' as COURIERPICKUPTIME,
                                    '' as CUSTOMSALERTDATE,
                                    '' as CUSTOMSALERTTIME,
                                    'N' as LABELPRINTINGFLAG,
                                    '' as PALLETFLAG,
                                    '0' as CODVALUE,
                                    '' as CODCURRENCYCODE,
                                    '' as CODPAYMENTMETHOD,
                                    '' as FIXEDDELIVERYDATE,
                                    'N' as DANGEROUSGOODSFLAG,
                                    '' as CLASS,
                                    '0' as NUMBEROFPALLETS,
                                    '' as PALLETTYPE,
                                    '' as INSUREDAMOUNTCURRENCYCODE,
                                    '' as DESTINATIONTERMINALCODE,
                                    '' as DESTINATIONBRANCHCODE,
                                    '' as DELIVERYINSTRUCTION,
                                    '' as ORIGINSTATIONCODE,
                                    '' as CUSTOMERSIRETNUMBER,
                                    '2' as NUMBEROFCONTENTSENTRIESBELOWI,
                                    'Electronic Products' as SHIPMENTCONTENTS1,
                                    '' as SHIPMENTCONTENTS,
                                    '0' as NUMBEROFCHARGESERVICEENTRIESB,
                                    '0' as NUMBEROFENTRIESTOFOLLOW1,
                                    'N' as DUTIABLEFLAG,
                                    '' as SHIPPERSEXPORTLICENSE,
                                    '' as SHIPPERSEINSSNORVAT,
                                    '' as MOVEMENTCERTIFICATEITSAD,
                                    '' as CONSIGNEEIMPORTLICENSE,
                                    '' as CONSIGNEEEINSSNORVAT,
                                    'DAP' as TERMSOFTRADE,
                                    '' as REASONFOREXPORTCODEBELOWITEMS,
                                    '' as RECEIVERSFEDERALTAXID,
                                    '' as SHIPPERSSTATETAXID,
                                    '' as RECEIVERSSTATETAXID,                                 
                                    '3' as NUMBEROFENTRIESTOFOLLOW,
                                    tss.hawb as SHIPPERREFERENCE1,
                                    '' as WEIGHT1,
                                    '' as SHIPPERREFERENCE2,
                                    '' as WEIGHT2,
                                    '' as SHIPPERREFERENCE,
                                    '' as WEIGHT3,
                                    '1' as NUMBEROFPIECEENTRIESPART6NOOF,
                                    '' as PIECEIDENTIFIER,
                                    (select round(sum(t1.totalWeight), 2)
                                       from (select tss.part_no,
                                                    tss.delivery_no,
                                                    vmi.GROSSWEIGHTKG *
                                                    count(distinct tss.carton_no) as totalWeight
                                               from ppsuser.t_sn_status tss,
                                                    pptest.vw_mpn_info  vmi
                                              where tss.part_no = vmi.ICTPARTNO
                                                and tss.shipment_id = :ShipmentId
                                              group by tss.part_no,
                                                       tss.delivery_no,
                                                       vmi.GROSSWEIGHTKG) t1) as PIECEWEIGHT,
                                    '' as PIECEVOLUMETRICWEIGHT,
                                    '' as PIECEVOLUME,
                                    '' as PIECELENGTH,
                                    '' as PIECEWIDTH,
                                    '' as PIECEHEIGHT,
                                    'KG' as UNITOFMEASUREMENT,
                                    '' as CONTENTDESCRIPTIONTEXT,
                                    '' as PACKAGETYPE,
                                    '' as PACKAGEREMARKS,
                                    '' as PACKAGECHARGECODE,
                                    '3' as NUMBEROFPIECESHIPPERREFERENCEE,
                                    '' as PIECEREFERENCE1,
                                    '' as WEIGHT4,
                                    '1/1' as PIECEREFERENCE2,
                                    '' as WEIGHT5,
                                    '' as PIECEREFERENCE3,
                                    '' as WEIGHT
                      from ppsuser.t_940_unicode   t9u,
                           ppsuser.t_sn_status     tss,
                           pptest.vw_mpn_info      vmi,
                           ppsuser.t_shipper       tsh,
                           ppsuser.t_shipment_info tsi
                     where t9u.deliveryno = tss.delivery_no
                       and t9u.custdelitem = tss.line_item
                       and tss.part_no = vmi.ICTPARTNO
                       and tss.shipment_id = tsi.shipment_id
                       and tss.shipment_id = :ShipmentId";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];

        }
        public DataTable getSPSImportBBXMotherForPAC(string shipmentId)
        {
            string sql = @"select   distinct
                                    '1' as NUMBEROFAWBSHIPMENT,
                                    '      ' as BACKENDTYPE1,
                                    'SPS 2.03' as SPSVERSIONID,
                                    'SPS' as BACKENDTYPE,
                                    '' as REFERENCEDATAVERSIONNUMBER,
                                    '' as METERNUMBER,
                                    '' as OPUNIT,
                                    '' as SCHEDULENUMBER,
                                    tss.hawb as CONSIGNMENTNUMBER,
                                    tss.hawb as SHIPMENTNUMBER,
                                    '' as ORIGINIATA,
                                    '' as DESTINATIONIATA,
                                    '' as INVOICENUMBER,
                                    '' as SHIPPERSACCOUNTNUMBER,
                                    '' as SHIPPERSCOMPANYNAME,
                                    '' as SHIPPERSCONTACTNAME,
                                    '' as SHIPPERSADDRESSLINE1,
                                    '' as SHIPPERSADDRESSLINE2,
                                    '' as SHIPPERSADDRESSLINE3,
                                    '' as SHIPPERSCITY,
                                    '' as SHIPPERSSTATE,
                                    '' as SHIPPERSZIPPOSTALCODE,
                                    '' as SHIPPERSCOUNTRYNAME,
                                    '' as SHIPPERSCOUNTRYCODE,
                                    tss.hawb as SHIPPERSREFERENCE,
                                    '' as SHIPPERSPHONENUMBER,
                                    '' as SHIPPERSFAXNUMBER,
                                    '' as SHIPPERSTELEXNUMBER,
                                    '' as SHIPPERSTELEXANSWERBACK,
                                    '' as SHIPPERSCURRENCYCODE,
                                    'N' as SHIPPERSWORLDCLASSACCTFLAG,
                                    decode(t9u.endcurr,'',t9u.tpc,t9u.endcurr) as RECIPIENTCODE,--t9u.endcurr  如果空白TPC 
                                    '' as RECIPIENTACCOUNTNUMBER,
                                    '' as RECIPIENTCOMPANYNAME,
                                    '' as RECIPIENTATTENTIONNAME,
                                    '' as RECIPIENTADDRESSLINE1,
                                    '' as RECIPIENTADDRESSLINE2,
                                    '' as RECIPIENTADDRESSLINE3,
                                    '' as RECIPIENTCITY,
                                    '' as RECIPIENTSTATE,
                                    '' as RECIPIENTZIPCODE,
                                    '' as RECIPIENTCOUNTRYNAME,
                                    '' as RECIPIENTCOUNTRYCODE,
                                    '' as RECIPIENTPHONENUMBER,
                                    '' as RECIPIENTFAXNUMBER,
                                    '' as RECIPIENTTELEXNUMBER,
                                    '' as RECIPIENTTELEXANSWERBACK,
                                    '' as DHLSRECIPIENTTABLEREF,
                                    'N' as FLAGFORTHIRDPARTYBILLING,
                                    '' as ACCOUNTCHARGEFORSHIPMENT,
                                    '' as RATETABLEUSED,
                                    'P' as THEDHLEXTERNALPRODUCTCODE,
                                    'P' as THEDHLINTERNALPRODUCTCODE,
                                    to_char(tsi.cdt,'YYYYMMDD')  as DATEAWBWASENTERED,
                                    to_char(tsi.cdt,'hh24mi') as TIMEAWBWASENTERED,
                                    '' as DATEAWBCOMPLETE,
                                    '' as TIMEAWBCOMPLETE,
                                       (select sum(tsi.carton_qty)
                                       from ppsuser.t_shipment_info tsi
                                      where tsi.shipment_id = tss.shipment_id) as TOTALPIECESINSHIPMENT,
                                    'Y' as ONEAWBPERPIECEFLAG,
                                        (select round(sum(t1.totalWeight), 2)
                                                        from (select tss.part_no,
                                                                    tss.delivery_no,
                                                                    vmi.GROSSWEIGHTKG *
                                                                    count(distinct tss.carton_no) as totalWeight
                                                                from ppsuser.t_sn_status tss,
                                                                    PPTEST.vw_mpn_info  vmi
                                                                where tss.part_no = vmi.ICTPARTNO
                                                                and tss.shipment_id = :ShipmentId
                                                                group by tss.part_no,
                                                                        tss.delivery_no,
                                                                        vmi.GROSSWEIGHTKG) t1) as ACTUALTOTALSHIPMENTWEIGHT,
                                    '' as ROUNDEDMFTTOTALSHIPWT,
                                    '' as DIMENSIONALWEIGHTCOMPFACTOR,
                                    '' as TOTALDIMENSIONALWEIGHT,
                                    ppsuser.t_dhl_value('HAWB', TSI.SHIPMENT_ID) as TOTALDECLAREDVALUE,
                                    '0' as INSURANCEAMOUNT,
                                    '' as PAYMENTMETHODCHARGETYPE,
                                    '' as CHECKNUMBER,
                                    '' as CREDITCARDTYPE,
                                    '' as CREDITCARDNUMBER,
                                    '' as CREDITCARDEXPIRYDATE,
                                    '' as AMOUNTRECEIVED,
                                    '' as SPECIALINSTRUCTIONSCODE,
                                    '' as ROUTEID,
                                    '' as CUTOFFCODE,
                                    '' as COURIERSINITIALS,
                                    '' as COURIERPICKUPDATE,
                                    '' as COURIERPICKUPTIME,
                                    '' as CUSTOMSALERTDATE,
                                    '' as CUSTOMSALERTTIME,
                                    'N' as LABELPRINTINGFLAG,
                                    '' as PALLETFLAG,
                                    '0' as CODVALUE,
                                    '' as CODCURRENCYCODE,
                                    '' as CODPAYMENTMETHOD,
                                    '' as FIXEDDELIVERYDATE,
                                    'N' as DANGEROUSGOODSFLAG,
                                    '' as CLASS,
                                    '0' as NUMBEROFPALLETS,
                                    '' as PALLETTYPE,
                                    '' as INSUREDAMOUNTCURRENCYCODE,
                                    '' as DESTINATIONTERMINALCODE,
                                    '' as DESTINATIONBRANCHCODE,
                                    '' as DELIVERYINSTRUCTION,
                                    '' as ORIGINSTATIONCODE,
                                    '' as CUSTOMERSIRETNUMBER,
                                    '2' as NUMBEROFCONTENTSENTRIESBELOWI,
                                    'Electronic Products' as SHIPMENTCONTENTS1,
                                    '' as SHIPMENTCONTENTS,
                                    '0' as NUMBEROFCHARGESERVICEENTRIESB,
                                    '0' as NUMBEROFENTRIESTOFOLLOW1,
                                    'N' as DUTIABLEFLAG,
                                    '' as SHIPPERSEXPORTLICENSE,
                                    '' as SHIPPERSEINSSNORVAT,
                                    '' as MOVEMENTCERTIFICATEITSAD,
                                    '' as CONSIGNEEIMPORTLICENSE,
                                    '' as CONSIGNEEEINSSNORVAT,
                                    decode(t9u.shipcntycode,'SG','DTP','KR','DTP','HK','DTP','TH','DTP','MY','DTP','PH','DTP','TW','DTU','AU','DTU','NZ','DTP','US','DTP','JP','DTU') as TERMSOFTRADE,--如果是 JP,AU,TW=DTU NZ=DTP
                                    '' as REASONFOREXPORTCODEBELOWITEMS,
                                    '' as RECEIVERSFEDERALTAXID,
                                    '' as SHIPPERSSTATETAXID,
                                    '' as RECEIVERSSTATETAXID,                
                                    '3' as NUMBEROFENTRIESTOFOLLOW,
                                    tss.hawb as SHIPPERREFERENCE1,
                                    '' as WEIGHT1,
                                    '' as SHIPPERREFERENCE2,
                                    '' as WEIGHT2,
                                    '' as SHIPPERREFERENCE,
                                    '' as WEIGHT3,
                                    '1' as NUMBEROFPIECEENTRIESPART6NOOF,
                                    '' as PIECEIDENTIFIER,
                                        (select round(sum(t1.totalWeight), 2)
                                                        from (select tss.part_no,
                                                                    tss.delivery_no,
                                                                    vmi.GROSSWEIGHTKG *
                                                                    count(distinct tss.carton_no) as totalWeight
                                                                from ppsuser.t_sn_status tss,
                                                                    PPTEST.vw_mpn_info  vmi
                                                                where tss.part_no = vmi.ICTPARTNO
                                                                and tss.shipment_id = :ShipmentId
                                                                group by tss.part_no,
                                                                        tss.delivery_no,
                                                                        vmi.GROSSWEIGHTKG) t1)as PIECEWEIGHT,
                                    '' as PIECEVOLUMETRICWEIGHT,
                                    '' as PIECEVOLUME,
                                    '' as PIECELENGTH,
                                    '' as PIECEWIDTH,
                                    '' as PIECEHEIGHT,
                                    'KG' as UNITOFMEASUREMENT,
                                    '' as CONTENTDESCRIPTIONTEXT,
                                    '' as PACKAGETYPE,
                                    '' as PACKAGEREMARKS,
                                    '' as PACKAGECHARGECODE,
                                    '3' as NUMBEROFPIECESHIPPERREFERENCEE,
                                    '' as PIECEREFERENCE1,
                                    '' as WEIGHT4,
                                    '1/1' as PIECEREFERENCE2,
                                    '' as WEIGHT5,
                                    '' as PIECEREFERENCE,
                                    '' as WEIGHT
                                        from ppsuser.t_940_unicode   t9u,
                                            ppsuser.t_sn_status     tss,
                                            pptest.vw_mpn_info      vmi,
                                            ppsuser.t_shipper       tsh,
                                            ppsuser.t_shipment_info tsi
                                        where t9u.deliveryno = tss.delivery_no
                                        and t9u.custdelitem = tss.line_item
                                        and tss.part_no = vmi.ICTPARTNO
                                        and tss.shipment_id = tsi.shipment_id
                                        and tss.shipment_id = :ShipmentId";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getSsccOfPrintLabelInfoByCartonNo(string cartonNo, bool isMix)
        {
            string sql1 = @"select tsp.*
                            from ppsuser.t_shipment_pallet tsp
                            where tsp.pallet_no =
                           (select distinct tss.pack_pallet_no
                              from ppsuser.t_sn_status tss
                             where tss.carton_no = :CartonNo)";
            string sql2 = @"select distinct tss.sscc
                            from ppsuser.t_sn_status tss
                            where tss.carton_no = :CartonNo";
            string lastSql = "";
            if (!isMix)
            {
                lastSql = sql1;//mix
            }
            else
            {
                lastSql = sql2;
            }
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(lastSql, sqlparams).Tables[0];
        }

        public DataTable getKnboxNoOfPrintLabelInfoByCartonNo(string cartonNo)
        {
            string sql = @"select distinct tss.knboxno
                              from ppsuser.t_sn_status tss
                             where tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getT940InfoByDeliveryNo(string deliveryNo)
        {
            string sql = @"SELECT T9U.SHIPCNTYCODE, T9U.CUSTOMERGROUP
                            FROM PPSUSER.T_940_UNICODE T9U
                           WHERE T9U.DELIVERYNO = :DeliveryNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DeliveryNo", deliveryNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getDHL_MotherFileShipmentIdsByShipmentTime(string shipmentTime)
        {
            string sql = @" select tsi.* from ppsuser.t_shipment_info tsi
                            where tsi.carrier_code like '%DHL%'
                            and tsi.service_level in('BBX','EXPRESS')
                            and TO_CHAR(tsi.Shipping_Time,'YYYYMMDD') = :ShipmentTime";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentTime", shipmentTime };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getShipmentInfoOfRegionByshipmentId(string shipmentId)
        {
            string sql = @"select tsi.* from ppsuser.t_shipment_info tsi
                            where tsi.shipment_id = :ShipmentId";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable checkShipmentIdStatusByShipmentId(string shipmentId)
        {
            string sql = @"  select tsi.status from ppsuser.t_shipment_info tsi
                              where tsi.shipment_id = :ShipmentId";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable inputDataConvertCartonNo(string inputData)
        {
            string sql = @"select distinct tss.carton_no as cartonNo
            from ppsuser.t_sn_status tss
            where (tss.serial_number = :inputData or tss.carton_no =:inputData or tss.pick_pallet_no =:inputData)";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", inputData } };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable getLatterPartOfLabelPrintInfo_mix(string cartonNo)
        {
            string sql = @"select tss.line_item as lineItem,opp.custpart as mpn ,count(tss.part_no) as qty
                              from ppsuser.t_sn_status tss,pptest.oms_partmapping opp
                             where 
                             tss.part_no = opp.part
                             and tss.carton_no = : CartonNo
                             group by tss.part_no, tss.pack_pallet_no,tss.line_item,opp.custpart";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo } };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable isPrintForNoMix(string cartonNo)
        {
            string sql = @"select distinct tsp.*
                            from ppsuser.t_shipment_pallet tsp, ppsuser.t_sn_status tss
                           where tsp.pallet_no = tss.pack_pallet_no
                           and  tsp.carton_qty = tsp.pack_carton
                           and tss.carton_no = : CartonNo";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo } };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable getLatterPartOfLabelPrintInfo_noMix(string cartonNo)
        {
            string sql = @"select tss.line_item as lineItem,opp.custpart as mpn ,count(tss.part_no) as qty
                              from ppsuser.t_sn_status tss,pptest.oms_partmapping opp
                             where 
                             tss.part_no = opp.part
                            and  tss.pack_pallet_no =
                           (select distinct tss1.pack_pallet_no
                              from ppsuser.t_sn_status tss1
                             where tss1.carton_no =: CartonNo)
                             group by tss.part_no, tss.pack_pallet_no,tss.line_item,opp.custpart";
            object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo } };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public DataTable queryPalletOrderInfoByCartonNo(string cartonNo, string palletNo, bool isAfterData)
        {
            string frontSql = @" and tpo.assign_carton  > tpo.pack_carton ";
            if (isAfterData)
            {
                frontSql = "";
            }
            string sql = @"select distinct tpo.*,
                                        (case tspp.pack_status
                                          when 'FP' then
                                           '已完成'
                                          when 'IP' then
                                           '进行中'
                                          else
                                           '--'
                                        end) as  status_,
                                        (select tsp.qty
                                           from ppsuser.t_shipment_pallet tsp
                                          where tsp.pallet_no =
                                                tpo.pallet_no) as totalQty,
                                        (select tsp.carton_qty
                                           from ppsuser.t_shipment_pallet tsp
                                          where tsp.pallet_no =
                                                tpo.pallet_no) as totalcartonQty
                          from ppsuser.t_sn_status            tss,
                               ppsuser.t_pallet_order         tpo,
                               ppsuser.t_shipment_pallet_part tspp
                         where tss.part_no = tpo.ictpn
                           and tpo.ictpn = tspp.ictpn
                           and tpo.pallet_no = tspp.pallet_no
                           and tss.carton_no = :CartonNo
                           and tpo.pallet_no = :PalletNo " + frontSql + @" order by tpo.cdt desc";
            object[][] sqlparams = new object[2][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "PalletNo", palletNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }


        public DataTable getCartonStatusByInputData(string inputData)
        {
            string sql = @"select tss.carton_no as cartonNo,
                           tss.part_no as partNo,
                           count(tss.carton_no) as cartonQty
                      from ppsuser.t_sn_status tss
                     where (tss.carton_no = (select tss.carton_no
                                               from ppsuser.t_sn_status tss
                                              where tss.serial_number = :InputData) or
                           tss.carton_no = :InputData or tss.pick_pallet_no = :InputData)
                     group by tss.carton_no, tss.part_no
                     order by tss.carton_no";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputData", inputData };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable isPpartByCartonNo(string cartonNo)
        {
            string sql = @"SELECT tss.delivery_no,tss.line_item
                               FROM PPTEST.OMS_PARTMAPPING OPA, 
                                    PPSUSER.T_SN_STATUS TSS
                              WHERE OPA.PART = TSS.PART_NO
                                AND OPA.PARTTYPE  like 'P%'
                                AND TSS.CARTON_NO = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable getPpartDnInfoByCartonNo(string cartonNo)
        {
            string sql = @"select distinct  TPO.DELIVERY_NO,
                                            TPO.LINE_ITEM,
                                            tpo.Assign_Qty,
                                            TPO.ASSIGN_CARTON,
                                            TPO.PACK_QTY,
                                            TPO.PACK_CARTON,
                                            TPO.ICTPN,
                                            (case tspp.pack_status
                                              when 'FP' then
                                               '已完成'
                                              when 'IP' then
                                               '进行中'
                                              else
                                               '--'
                                            end) as status_,
                                            (select tsp.qty
                                               from ppsuser.t_shipment_pallet tsp
                                              where tsp.pallet_no = tpo.pallet_no) as totalQty,
                                            (select tsp.carton_qty
                                               from ppsuser.t_shipment_pallet tsp
                                              where tsp.pallet_no = tpo.pallet_no) as totalcartonQty
                              from ppsuser.t_sn_status            tss,
                                   ppsuser.t_pallet_order         tpo,
                                   ppsuser.t_shipment_pallet_part tspp,
                                   ppsuser.vw_person_log          vpl
                             WHERE TSS.CARTON_NO = VPL.CARTON_NO
                               AND SUBSTR(TSS.PICK_PALLET_NO, 3) = TPO.PALLET_NO
                               AND TPO.DELIVERY_NO = VPL.DELIVERY_NO
                               AND TPO.LINE_ITEM = VPL.LINE_ITEM
                               AND TSS.PART_NO = TPO.ICTPN
                               AND TSPP.PALLET_NO = SUBSTR(TSS.PICK_PALLET_NO, 3)
                               AND TSPP.ICTPN = TSS.PART_NO
                               AND TSS.CARTON_NO = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }



        public DataTable getTransInFileCNPLInfoByCartonNo(string cartonNo)
        {
            string sql = "select distinct  to_char(sysdate,'YYYY\"年\"MM\"月\"dd\"日\"') as PRINTDATE, --Pack 时间 需要格式化YYYY年MM月DD日" + @"
                                        t9u.deliveryno as ORDERID,
                                        (SELECT SUM(TOI.CARTON_QTY)
                                           FROM PPSUSER.T_ORDER_INFO TOI
                                          WHERE TOI.DELIVERY_NO =
                                                (SELECT DISTINCT TSS.DELIVERY_NO
                                                   FROM PPSUSER.T_SN_STATUS TSS
                                                  WHERE TSS.CARTON_NO = :CartonNO)) as TOTALNUM,
                                        tss.box_no as SEQNUM,
                                        t9u.shiptoname as CUSNAME,
                                        Decode(instr(t9u.regiondesc, '='),
                                               0,
                                               t9u.regiondesc,
                                               substr(t9u.regiondesc, instr(t9u.regiondesc, '=') + 1)) as PROVINCE,
                                        t9u.shiptocity as CITY,
                                        t9u.shiptoaddress4 as DISTRICT,
                                        t9u.shiptocompany as CUSCOMNAME,
                                        t9u.shiptoaddress as CUSADD1,
                                        t9u.shiptoaddress2 as CUSADD2,
                                        t9u.shiptoconttel as CUSTEL,
                                        t9u.shiptozip as CUSADDCODE,
                                        '电子产品' as PROINFO,
                                        t9u.shipplant as SENCODE,
                                        tss.sscc as SSCC,
                                        tss.sscc as SSCCBARCODE,
                                        '' as LOADID,
                                        '' as PALLETID,
                                        '' as SEQUENCE_,
                                        vmi.MPN as APPLEPARTNUM,
                                        DECODE(VMI.PACKUNIT, '1', TSS.CARTON_NO, '') as SERIALNUM, --如果是Single 放cartonNO Multi 放空白
                                        DECODE(VMI.PACKUNIT, '1', '', TSS.CARTON_NO) as CARTONID, --如果是Multi 放cartonNO Single 放空白
                                        (select tsb.sendname
                                           from ppsuser.t_slc_blp tsb
                                          where rownum = 1) as SENNAME,
                                        (select tsb.sencomname
                                           from ppsuser.t_slc_blp tsb
                                          where rownum = 1) as SENCOMNAME,
                                        (select tsb.senadd
                                           from ppsuser.t_slc_blp tsb
                                          where rownum = 1) as SENADD,
                                        (select tsb.sentel
                                           from ppsuser.t_slc_blp tsb
                                          where rownum = 1) as SENTEL,
                                        (select tsb.sencode
                                           from ppsuser.t_slc_blp tsb
                                          where rownum = 1) as SENCODE1,
                                        decode(t9u.shipofpay, 'COD', '1', 'CODPOS', '2', '0') as MAILTYPE, --Shipofpay =COD 是1 Shipofpay =CODPOS 是2 其他是0
                                        round(t9u.codamount, 2) as PRODPRCE, --CODAmount 保留2位小数
                                        t9u.weborderno as APPLEWEBORDER,
                                        vmi.GROSSWEIGHTKG as ACTUALWEIGHT,
                                        '' as VOLUME,
                                        tss.BABYTRACKING_NO as EMSBARCODE,
                                        tsi.poe as POE,
                                        '' as FILLER1,
                                        tss.hawb as FILLER2
                          from ppsuser.t_940_unicode   t9u,
                               ppsuser.t_sn_status     tss,
                               pptest.vw_mpn_info      vmi,
                               ppsuser.t_shipment_info tsi
                         where t9u.deliveryno = tss.delivery_no
                           and t9u.custdelitem = tss.line_item
                           and vmi.ICTPARTNO = tss.part_no
                           and tss.shipment_id = tsi.shipment_id
                           and tss.carton_no = :CartonNO";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNO", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable isReprint(string cartonNo)
        {
            //添加and rownum=1 当前站别为W0时 ppsuser.t_process_info
            //中会对应到两笔资料 ，oracle返回报错，修改为只取其中一笔 by Franky
            string currentStation = "W1";//当前站别
            string sql = string.Format(@" 
                     select decode(sign((select distinct tpci.sequence
                       from ppsuser.t_sn_status    tss,
                            ppsuser.t_process_info tpci
                      where tss.wc = tpci.inwc
                        and tss.carton_no = '{0}'
                        and rownum=1 ) -
                    (select tpci.sequence
                       from ppsuser.t_process_info tpci
                      where tpci.inwc = '{1}')),
                        0,
                        'OK',
                        1,
                        'OK',
                        'FAIL') as checkResult
            from dual", cartonNo, currentStation);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable getReprintLabelFormInfo(string cartonNo)
        {
            string sql = string.Format(@" 
                     select distinct    tss.pick_pallet_no,  
                                        tss.sscc,
                                        tss.shipment_id,
                                        tsi.shipment_type,
                                        tsi.type,
                                        decode(tsp.pallet_type,'001','NO_MIX','MIX') AS PALLETTYPE,
                                        tsi.region,
                                        tsi.carrier_name,
                                        tss.delivery_no,
                                        tsi.carrier_code,
                                        tsi.service_level
                          from ppsuser.t_sn_status       tss,
                               ppsuser.t_shipment_info   tsi,
                               ppsuser.t_shipment_pallet tsp
                         where tss.shipment_id = tsi.shipment_id
                           and tss.pack_pallet_no = tsp.pallet_no
                           and tss.carton_no = '{0}'", cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable queryOrderInfoByDn(string dn, string shipmentId)
        {
            string sql = @" select sum(toi.pack_carton_qty) as packQty,
                                    sum(toi.carton_qty) as cartonQty
                               from ppsuser.t_order_info toi
                              where toi.delivery_no = '{0}'
                             and    toi.shipment_id = '{1}'";
            sql = string.Format(sql, dn, shipmentId);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getUpsInfoByCartonNo2(string cartonNo, string region)
        {
            string handleSql = @" select distinct tsi.hawb,
                                        tsi.shipment_tracking,
                                        tss.tracking_no,
                                        to_char(tsi.shipping_time, 'yyyy/MM/dd') as shipdate,
                                        t9u.parcelaccountnumber,
                                        (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                        (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                        (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                        '' as SHIPER_ADDRESS3,
                                        (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                        (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                        (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                        (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                        '' as Consignee_UPS_Account_number,
                                        t9u.shiptoname,
                                        t9u.shiptocompany,
                                        t9u.shiptoconttel,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 1, 35)
                                          else
                                           t9u.shiptoaddress
                                        end as ST_ADDR1,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                          else
                                           t9u.shiptoaddress2
                                        end as ST_ADDR2,                     
                                        case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end       
                                         as ST_ADDR3,
                                        t9u.shiptocity,
                                        t9u.regiondesc as regiondesc, --RegionDesc  如果有=号码, 那么取=号之前的
                                        t9u.shiptozip,
                                        t9u.shipcntycode,
                                        tss.box_no as CARTON_SEQUNECE,
                                        (select sum(toi.carton_qty)
                                           from ppsuser.t_order_info toi
                                          where toi.delivery_no =
                                                (select distinct tss.delivery_no
                                                   from ppsuser.t_sn_status tss
                                                  where tss.carton_no = '{0}')) as CARTON_COUNT,
                                        (  select GROSSWEIGHTKG
                                              from PPsuser.vw_mpn_info P_VMI,
                                                   (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                      FROM PPSUSER.T_SN_STATUS TSS, 
                                                           PPSUSER.T_SHIPMENT_PALLET TSP
                                                     WHERE TSS.pack_pallet_no = TSP.PALLET_NO
                                                       AND TSS.CARTON_NO = '{0}') T
                                             where P_VMI.ICTPARTNO = T.PART_NO
                                               AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,              
                                        (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM * P_VMI.CARTONWIDTHCM) / 1000000/6000,2)
                                        from PPsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                FROM PPSUSER.T_SN_STATUS TSS, 
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                                WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
                                                  AND TSS.CARTON_NO = '{0}') T
                                        where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                        tss.sscc,
                                        tss.delivery_no,
                                        t9u.custsono,
                                        t9u.custpono,
                                        t9u.weborderno,
                                        t9u.custdelitem,
                                        (SELECT DISTINCT TOI.MPN
                                           FROM PPSUSER.T_ORDER_INFO TOI
                                          WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO  and toi.ictpn  =   tss.part_no) AS AC_PN,
                                        (select count(tss_.serial_number)
                                           from ppsuser.t_sn_status tss_
                                          where tss_.carton_no = '{0}') as perCartonQty,
                                        tss.carton_no,
                                        '' as Delivery_Instruction,
                                        ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                        'USD',
                                        DECODE(SUBSTR(t9u.custshipinst, 1, 5),
                                               'ACDES',
                                               substr(t9u.custshipinst, 5),
                                               t9u.custshipinst),
                                        '' as HAWB_,
                                        SUBSTR(TSS.PACK_PALLET_NO,-4) as PALLET_ID,
                                        '' as CARTON_ID
                          from ppsuser.t_shipment_info tsi,
                               ppsuser.t_sn_status     tss,
                               ppsuser.t_940_unicode   t9u
                         where tsi.shipment_id = tss.shipment_id
                           and tss.delivery_no = t9u.deliveryno
                           and tss.line_item = trim(t9u.custdelitem) 
                           and tss.carton_no = '{0}'";
            if (region.Equals("AMR"))
            {
                handleSql = @"select distinct (SELECT distinct   tsi.hawb
                              FROM PPSUSER.t_Order_Info    toi,
                                   ppsuser.t_sn_status     tss,
                                   ppsuser.t_shipment_info tsi
                             where toi.delivery_no = tss.delivery_no
                               and tsi.shipment_id = toi.shipment_id
                               and toi.shipment_id in
                                   (SELECT DISTINCT TSSA.SHIPMENT_ID
                                      FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_SAWB TSSA
                                     WHERE TSS.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                       AND TSS.CARTON_NO = '{0}')
                               and tss.carton_no = '{0}') AS HAWB,
                                   (SELECT distinct   tsi.shipment_tracking
                                  FROM PPSUSER.t_Order_Info    toi,
                                       ppsuser.t_sn_status     tss,
                                       ppsuser.t_shipment_info tsi
                                 where toi.delivery_no = tss.delivery_no
                                   and tsi.shipment_id = toi.shipment_id
                                   and toi.shipment_id in
                                       (SELECT DISTINCT TSSA.SHIPMENT_ID
                                          FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_SAWB TSSA
                                         WHERE TSS.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                           AND TSS.CARTON_NO = '{0}')
                                   and tss.carton_no = '{0}') AS SHIPMENTREACKING,
                                    tss.tracking_no,
                                    to_char(tsi.shipping_time, 'yyyy/MM/dd'),
                                    t9u.parcelaccountnumber,
                                    (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                    (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                    (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                    '' as SHIPER_ADDRESS3,
                                    (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                    (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                    (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                    (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                    '' as Consignee_UPS_Account_number,
                                    t9u.shiptoname,
                                    t9u.shiptocompany,
                                    t9u.shiptoconttel,
                                    case
                                      when length(t9u.shiptoaddress) > 35 then
                                       substr(t9u.shiptoaddress, 1, 35)
                                      else
                                       t9u.shiptoaddress
                                    end as ST_ADDR1,
                                    case
                                      when length(t9u.shiptoaddress) > 35 then
                                       substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                      else
                                       t9u.shiptoaddress2
                                    end as ST_ADDR2,
                                    case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end        as ST_ADDR3,
                                    t9u.shiptocity,
                                    decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) AS  REGIONDESC, --RegionDesc  如果有 = 号码, 那么取 = 号之前的
                                    t9u.shiptozip,
                                    t9u.shipcntycode,
                                    tss.box_no as CARTON_SEQUNECE,
                                    (select sum(toi.carton_qty)
                                       from ppsuser.t_order_info toi
                                      where toi.delivery_no =
                                            (select distinct tss.delivery_no
                                               from ppsuser.t_sn_status tss
                                              where tss.carton_no = '{0}')
                                                AND TOI.SHIPMENT_ID = TSS.SHIPMENT_ID) as CARTON_COUNT,
                                    (select GROSSWEIGHTKG
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE TSS.pack_pallet_no = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,
                
                                    (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM *
                                                  P_VMI.CARTONWIDTHCM) / 1000000 / 6000,
                                                  2)
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                    tss.sscc,
                                    tss.delivery_no,
                                    t9u.custsono,
                                    t9u.custpono,
                                    t9u.weborderno,
                                    t9u.custdelitem,
                                    (SELECT DISTINCT TOI.MPN
                                       FROM PPSUSER.T_ORDER_INFO TOI
                                      WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
                                        and toi.ictpn = tss.part_no) AS AC_PN,
                                    (select count(tss_.serial_number)
                                       from ppsuser.t_sn_status tss_
                                      where tss_.carton_no = '{0}') as perCartonQty,
                                    tss.carton_no,
                                    '' as Delivery_Instruction,
                                    ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                    'USD',
                                    DECODE(SUBSTR(t9u.custshipinst, 1, 7),
                                           'ACDES--',
                                           substr(t9u.custshipinst, 8),
                                           t9u.custshipinst),
                                    '' as HAWB_,
                                    SUBSTR(TSS.PACK_PALLET_NO, -4) as PALLET_ID,
                                    '' as CARTON_ID,
                                    tsi.shipment_tracking,
                                    tsi.hawb,
                                    tsi.carton_qty,
                                    tsi.poe
                      from ppsuser.t_shipment_info tsi,
                           ppsuser.t_sn_status tss,
                           ppsuser.t_940_unicode t9u
                     where tsi.shipment_id = tss.shipment_id
                       and tss.delivery_no = t9u.deliveryno
                       and tss.line_item = trim(t9u.custdelitem)
                       and tss.carton_no = '{0}'";
            }
            string sql = string.Format(handleSql, cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getUpsInfoByCartonNo(string cartonNo, string region)
        {
            //修改逻辑 按MODEL来显示 AMR/PAC/EMEIA都要显示
            string instruction = "";
            DataTable dtTemp = ClientUtils.ExecuteSQL(string.Format(@"
SELECT DISTINCT c.HAZARDOUS 
FROM ppsuser.T_SN_STATUS a INNER JOIN ppsuser.VW_MPN_INFO b ON a.PART_NO=b.ICTPARTNO
INNER JOIN PPTEST.OMS_MODEL c ON b.CUSTMODEL=c.CUSTMODEL
WHERE a.CARTON_NO='{0}'
", cartonNo)).Tables[0];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                if (dtTemp.Rows[0]["HAZARDOUS"].ToString().ToUpper() == "Y")
                {
                    instruction = "Lithium Ion Batteries in Compliance with PI966 Section II";
                }
            }

            string handleSql = @" select distinct tsi.hawb,
                                        tsi.shipment_tracking,
                                        tss.tracking_no,
                                        to_char(tsi.shipping_time, 'yyyy/MM/dd') as shipdate,
                                        t9u.parcelaccountnumber,
                                        (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                        (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                        (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                        '' as SHIPER_ADDRESS3,
                                        (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                        (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                        (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                        (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                        '' as Consignee_UPS_Account_number,
                                        t9u.shiptoname,
                                        t9u.shiptocompany,
                                        t9u.shiptoconttel,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 1, 35)
                                          else
                                           t9u.shiptoaddress
                                        end as ST_ADDR1,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                          else
                                           t9u.shiptoaddress2
                                        end as ST_ADDR2,                     
                                        case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end       
                                         as ST_ADDR3,
                                        t9u.shiptocity,
                                        decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) as regiondesc, --RegionDesc  如果有=号码, 那么取=号之前的
                                        t9u.shiptozip,
                                        t9u.shipcntycode,
                                        tss.box_no as CARTON_SEQUNECE,
                                        (select sum(toi.carton_qty)
                                           from ppsuser.t_order_info toi
                                          where toi.delivery_no =
                                                (select distinct tss.delivery_no
                                                   from ppsuser.t_sn_status tss
                                                  where tss.carton_no = '{0}')) as CARTON_COUNT,
                                        (  select GROSSWEIGHTKG
                                              from PPsuser.vw_mpn_info P_VMI,
                                                   (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                      FROM PPSUSER.T_SN_STATUS TSS, 
                                                           PPSUSER.T_SHIPMENT_PALLET TSP
                                                     WHERE TSS.pack_pallet_no = TSP.PALLET_NO
                                                       AND TSS.CARTON_NO = '{0}') T
                                             where P_VMI.ICTPARTNO = T.PART_NO
                                               AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,              
                                        (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM * P_VMI.CARTONWIDTHCM) / 1000000/6000,2)
                                        from PPsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                FROM PPSUSER.T_SN_STATUS TSS, 
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                                WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
                                                  AND TSS.CARTON_NO = '{0}') T
                                        where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                        tss.sscc,
                                        tss.delivery_no,
                                        t9u.custsono,
                                        t9u.custpono,
                                        t9u.weborderno,
                                        t9u.custdelitem,
                                        (SELECT DISTINCT TOI.MPN
                                           FROM PPSUSER.T_ORDER_INFO TOI
                                          WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO  and toi.ictpn  =   tss.part_no) AS AC_PN,
                                        (select count(tss_.serial_number)
                                           from ppsuser.t_sn_status tss_
                                          where tss_.carton_no = '{0}') as perCartonQty,
                                        tss.carton_no,
                                        '{1}' as Delivery_Instruction,
                                        ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                        'USD',
                                        DECODE(SUBSTR(t9u.custshipinst, 1, 5),
                                               'ACDES',
                                               substr(t9u.custshipinst, 5),
                                               t9u.custshipinst),
                                        '' as HAWB_,
                                        SUBSTR(TSS.PACK_PALLET_NO,-4) as PALLET_ID,
                                        '' as CARTON_ID
                          from ppsuser.t_shipment_info tsi,
                               ppsuser.t_sn_status     tss,
                               ppsuser.t_940_unicode   t9u
                         where tsi.shipment_id = tss.shipment_id
                           and tss.delivery_no = t9u.deliveryno
                           and tss.line_item = trim(t9u.custdelitem) 
                           and tss.carton_no = '{0}'";
            if (region.Equals("AMR"))
            {
                handleSql = @"select distinct (SELECT distinct   tsi.hawb
                              FROM PPSUSER.t_Order_Info    toi,
                                   ppsuser.t_sn_status     tss,
                                   ppsuser.t_shipment_info tsi
                             where toi.delivery_no = tss.delivery_no
                               and tsi.shipment_id = toi.shipment_id
                               and toi.shipment_id in
                                   (SELECT DISTINCT TSSA.SHIPMENT_ID
                                      FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_SAWB TSSA
                                     WHERE TSS.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                       AND TSS.CARTON_NO = '{0}')
                               and tss.carton_no = '{0}') AS HAWB,
                                   (SELECT distinct   tsi.shipment_tracking
                                  FROM PPSUSER.t_Order_Info    toi,
                                       ppsuser.t_sn_status     tss,
                                       ppsuser.t_shipment_info tsi
                                 where toi.delivery_no = tss.delivery_no
                                   and tsi.shipment_id = toi.shipment_id
                                   and toi.shipment_id in
                                       (SELECT DISTINCT TSSA.SHIPMENT_ID
                                          FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_SAWB TSSA
                                         WHERE TSS.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                           AND TSS.CARTON_NO = '{0}')
                                   and tss.carton_no = '{0}') AS SHIPMENTREACKING,
                                    tss.tracking_no,
                                    to_char(tsi.shipping_time, 'yyyy/MM/dd')  as shipdate,
                                    t9u.parcelaccountnumber,
                                    (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                    (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                    (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                    '' as SHIPER_ADDRESS3,
                                    (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                    (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                    (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                    (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                    '' as Consignee_UPS_Account_number,
                                    t9u.shiptoname,
                                    t9u.shiptocompany,
                                    t9u.shiptoconttel,
                                    case
                                      when length(t9u.shiptoaddress) > 35 then
                                       substr(t9u.shiptoaddress, 1, 35)
                                      else
                                       t9u.shiptoaddress
                                    end as ST_ADDR1,
                                    case
                                      when length(t9u.shiptoaddress) > 35 then
                                       substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                      else
                                       t9u.shiptoaddress2
                                    end as ST_ADDR2,
                                    case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end        as ST_ADDR3,
                                    t9u.shiptocity,
                                    decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) AS  REGIONDESC, --RegionDesc  如果有 = 号码, 那么取 = 号之前的
                                    t9u.shiptozip,
                                    t9u.shipcntycode,
                                    tss.box_no as CARTON_SEQUNECE,
                                    (select sum(toi.carton_qty)
                                       from ppsuser.t_order_info toi
                                      where toi.delivery_no =
                                            (select distinct tss.delivery_no
                                               from ppsuser.t_sn_status tss
                                              where tss.carton_no = '{0}')
                                                AND TOI.SHIPMENT_ID = TSS.SHIPMENT_ID) as CARTON_COUNT,
                                    (select GROSSWEIGHTKG
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE TSS.pack_pallet_no = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,
                
                                    (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM *
                                                  P_VMI.CARTONWIDTHCM) / 1000000 / 6000,
                                                  2)
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                    tss.sscc,
                                    tss.delivery_no,
                                    t9u.custsono,
                                    t9u.custpono,
                                    t9u.weborderno,
                                    t9u.custdelitem,
                                    (SELECT DISTINCT TOI.MPN
                                       FROM PPSUSER.T_ORDER_INFO TOI
                                      WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
                                        and toi.ictpn = tss.part_no) AS AC_PN,
                                    (select count(tss_.serial_number)
                                       from ppsuser.t_sn_status tss_
                                      where tss_.carton_no = '{0}') as perCartonQty,
                                    tss.carton_no,
                                    '{1}' as Delivery_Instruction,
                                    ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                    'USD',
                                    DECODE(SUBSTR(t9u.custshipinst, 1, 7),
                                           'ACDES--',
                                           substr(t9u.custshipinst, 8),
                                           t9u.custshipinst),
                                    '' as HAWB_,
                                    SUBSTR(TSS.PACK_PALLET_NO, -4) as PALLET_ID,
                                    '' as CARTON_ID,
                                    tsi.shipment_tracking,
                                    tsi.hawb as SAWB,
                                    tsi.carton_qty,
                                    tsi.poe
                      from ppsuser.t_shipment_info tsi,
                           ppsuser.t_sn_status tss,
                           ppsuser.t_940_unicode t9u
                     where tsi.shipment_id = tss.shipment_id
                       and tss.delivery_no = t9u.deliveryno
                       and tss.line_item = trim(t9u.custdelitem)
                       and tss.carton_no = '{0}'";
            }
            string sql = string.Format(handleSql, cartonNo, instruction);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getFedInfoByCartonNo(string cartonNo)
        {
            string strreturnto1 = "";
            string strreturnto2 = "";
            string strreturnto3 = "";
            string strreturnto4 = "";
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "cartonNo", cartonNo };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto1", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto2", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "returnto3", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RETURNTO4", "" };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "Tmes", "" };

            DataTable dtTemp = ClientUtils.ExecuteProc("ppsuser.t_Fedex_Return", procParams).Tables[0];
            if ((dtTemp == null) || (dtTemp.Rows.Count <= 0))
            {
                return null;
            }
            else
            {
                if (dtTemp.Rows[0]["Tmes"].ToString().Trim() != "OK")
                {
                    return null;
                }
                else
                {
                    strreturnto1 = dtTemp.Rows[0]["returnto1"].ToString().Trim();
                    strreturnto2 = dtTemp.Rows[0]["returnto2"].ToString().Trim();
                    strreturnto3 = dtTemp.Rows[0]["returnto3"].ToString().Trim();
                    strreturnto4 = dtTemp.Rows[0]["returnto4"].ToString().Trim();
                }
            }

            //modify by wenxing 2020/07/08
            #region set fixed value
            //string handleSql = @"
            //                    SELECT  DISTINCT
            //                    '51' AS TransactionType,      --0
            //                    B.CARTON_NO AS TransactionID, --1
            //                    (select shippername   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderCompany,--4
            //                    (select shipperaddress1   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderAddress1,--5
            //                    (select shipperaddress2   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderAddress2,--6
            //                    (select shippercity   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderCity,--7
            //                    (select shipperstate   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderState,--8
            //                    (select shipperpostal   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderPostalCode,--9
            //                    --'343000' as SenderPostalCode,--9
            //                    '688912636' as SenderAccountNumber,    --10--origin: 625908990, modified to 688912636 for test
            //                    a.shiptocompany  as RecipientCompany,  --11
            //                    a.shiptoname  as RecipientContactName, --12
            //                    a.shiptoaddress as RecipientAddress1,  --13
            //                    a.shiptoaddress2 as RecipientAddress2, --14
            //                    a.shiptocity as     RecipientCity,     --15
            //                    REGEXP_SUBSTR(a.regiondesc, '[^=]+') as RecipientState,  --16
            //                    a.shiptozip  as RecipientPostalCode, --17
            //                    a.shiptoconttel  as RecipientPhone,  --18
            //                    '' as RecipientBusinessCode,         --19
            //                    a.PARCELACCOUNTNUMBER as PayAccountNumber,              --20 待客人确认
            //                    CASE WHEN A.Servicelevel='IPD' THEN '18'
            //                        WHEN A.Servicelevel='IED' THEN '17'  
            //                        WHEN A.Servicelevel='IP' THEN '01'
            //                        WHEN A.Servicelevel='IPF' THEN '70'
            //                            END AS PS2ServiceType,--22
            //                    '3' as PaymentCode,                  --23
            //                    to_char(C.SHIPPING_TIME,'YYYYMMDD') AS ShipDate,--24
            //                    '' as ReferenceNotes,               --25
            //                    b.hawb as MasterTrackingNumber,  --29
            //                    a.shipcntycode as RecipientCountryCode, --50
            //                    '' as PackageHeight,      --57
            //                    '' as PackageWidth,       --58
            //                    '' as PackageLength,      --59
            //                    a.endcurr  as CurrencyType,             --68                 
            //                    --a.endprice*d.PACKUNIT as CarriageValue, --69
            //                    0 as CarriageValue, --69
            //                    '2' as DutyPaymentType,                 --70
            //                    '208398105' as DutyPayerAccountNumber,  --71
            //                    '6' as TermsOfSale,                      --72 待客人确认
            //                    'KGS' AS WeightType,                     --75
            //                    --a.endprice*d.PACKUNIT as CommodityCustomsValue,             --78 待客人确认
            //                    a.endprice*d.PACKUNIT*100 as CommodityCustomsValue,             --78 待客人确认
            //                    'Electronic Products' as CommodityDescriptionLine,          --79 待客人确认
            //                    (select shippercntycode   from PPSUSER.t_shipper where rownum<=1) as CountryofManufacture,--80
            //                    '' as CommodityDescriptionLine2,         --88
            //                    '' as CommodityDescriptionLine3,         --97
            //                    'N' as CommercialInvoiceFlag,            --113
            //                    b.tracking_no as TrackingNumberofCrn,     --114
            //                    CASE WHEN A.Servicelevel='IPD' THEN 1
            //                        WHEN A.Servicelevel='IED' THEN 1  
            //                        WHEN A.Servicelevel='IP' THEN C.CARTON_QTY
            //                        WHEN A.Servicelevel='IPF' THEN C.CARTON_QTY
            //                            END AS totalNumberofPackages ,           --116 是Service
            //                    (select SHIPPERCNTYCODE   from PPSUSER.t_shipper where rownum<=1) as SenderCountryCode,--117
            //                    (select shippertel   from PPSUSER.t_shipper where rownum<=1) as SenderPhoneNumber,--183
            //                    a.servicelevel  as  ServiceType,--627
            //                    '{1}' AS ReturnContactName , --700
            //                    '{2}' AS ReturnCompany, --701
            //                    '{3}' AS ReturnAddress1, --702
            //                    '{4}' AS ReturnAddress2, --703
            //                    a.custpono AS CustomerPurchaseOrderNo,   --709
            //                    b.box_no AS CartonNo,--711
            //                    C.CARTON_QTY AS CartonCount,--712
            //                    b.sscc AS CustomBarcode,--713
            //                    a.custsono AS SalesOrderNo,--714
            //                    a.weborder AS WebOrderNo,--715
            //                    a.deliveryno,--716                     
            //                    to_char(C.SHIPPING_TIME,'YYYYMMDD') AS ShipDate2,--717
            //                    a.mpn AS PartNo,--718
            //                    'As Described in Invoice' AS DescriptionLine1,--719
            //                    '' AS DescriptionLine2,--720
            //                    '' AS ShippingInstruction,--721
            //                    a.custdelitem AS LineItemNumber,--722
            //                    d.PACKUNIT AS QuantityShipped,--723
            //                    a.qty AS QuantityOrdered,--724
            //                    '' AS ProductModelLine1,--726
            //                    a.ShipToAddress4 AS District,--728
            //                    'C' AS DimUnit,--1116
            //                    'N' AS SaturdayDeliveryFlag,--1226
            //                    CASE WHEN A.Servicelevel='IPD' THEN '18'
            //                        WHEN A.Servicelevel='IED' THEN '17'  
            //                        WHEN A.Servicelevel='IP' THEN '01'
            //                        WHEN A.Servicelevel='IPF' THEN '70'
            //                            END AS ServiceType2,--1274
            //                            d.GROSSWEIGHTKG AS PackageWeight--1670
            //                    FROM PPSUSER.T_940_UNICODE  A INNER JOIN PPSUSER.T_SN_STATUS B 
            //                    ON A.DELIVERYNO=B.DELIVERY_NO AND A.CUSTDELITEM=B.LINE_ITEM
            //                    INNER JOIN PPSUSER.T_SHIPMENT_INFO C ON B.SHIPMENT_ID=C.SHIPMENT_ID
            //                    INNER JOIN PPTEST.VW_MPN_INFO D ON B.PART_NO=D.ICTPARTNO
            //                    WHERE B.CARTON_NO='{0}'
            //                    ";
            //string sql = string.Format(handleSql, cartonNo, strreturnto1, strreturnto2, strreturnto3, strreturnto4);
            #endregion

            #region dynamic value get from database modify by wenxing 2020/07/08
            string sqlQueryFedex = "SELECT * from PPSUSER.T_CARRIER_CONSTANT_SETUP where carrier_name = 'FEDEX' and FLAG = 'Y'";
            var dtFedexField = ClientUtils.ExecuteSQL(sqlQueryFedex).Tables[0];

            string SenderAccountNumber_5 = dtFedexField.AsEnumerable().Where(x => x.Field<string>("FIELD_NAME") == "SenderAccountNumber").FirstOrDefault().Field<string>("SET_VALUE");
            string DutyPayerAccountNumber_6 = dtFedexField.AsEnumerable().Where(x => x.Field<string>("FIELD_NAME") == "DutyPayerAccountNumber").FirstOrDefault().Field<string>("SET_VALUE");
            string CommodityDescriptionLine_7 = dtFedexField.AsEnumerable().Where(x => x.Field<string>("FIELD_NAME") == "CommodityDescriptionLine").FirstOrDefault().Field<string>("SET_VALUE");
            string DescriptionLine1_8 = dtFedexField.AsEnumerable().Where(x => x.Field<string>("FIELD_NAME") == "DescriptionLine1").FirstOrDefault().Field<string>("SET_VALUE");

            string handleSql = @"
                                SELECT  DISTINCT
                                '51' AS TransactionType,      --0
                                B.CARTON_NO AS TransactionID, --1
                                (select shippername   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderCompany,--4
                                (select shipperaddress1   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderAddress1,--5
                                (select shipperaddress2   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderAddress2,--6
                                (select shippercity   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderCity,--7
                                (select shipperstate   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderState,--8
                                (select shipperpostal   from PPSUSER.T_SHIPPER_FEDEX where rownum<=1) as SenderPostalCode,--9
                                --'343000' as SenderPostalCode,--9
                                '{5}' as SenderAccountNumber,    --10--origin: 625908990, modified to 688912636 for test
                                a.shiptocompany  as RecipientCompany,  --11
                                a.shiptoname  as RecipientContactName, --12
                                a.shiptoaddress as RecipientAddress1,  --13
                                a.shiptoaddress2 as RecipientAddress2, --14
                                a.shiptocity as     RecipientCity,     --15
                                REGEXP_SUBSTR(a.regiondesc, '[^=]+') as RecipientState,  --16
                                a.shiptozip  as RecipientPostalCode, --17
                                a.shiptoconttel  as RecipientPhone,  --18
                                '' as RecipientBusinessCode,         --19
                                a.PARCELACCOUNTNUMBER as PayAccountNumber,              --20 待客人确认
                                CASE WHEN A.Servicelevel='IPD' THEN '18'
                                    WHEN A.Servicelevel='IED' THEN '17'  
                                    WHEN A.Servicelevel='IP' THEN '01'
                                    WHEN A.Servicelevel='IPF' THEN '70'
                                        END AS PS2ServiceType,--22
                                '3' as PaymentCode,                  --23
                                to_char(C.SHIPPING_TIME,'YYYYMMDD') AS ShipDate,--24
                                '' as ReferenceNotes,               --25
                                b.hawb as MasterTrackingNumber,  --29
                                a.shipcntycode as RecipientCountryCode, --50
                                '' as PackageHeight,      --57
                                '' as PackageWidth,       --58
                                '' as PackageLength,      --59
                                a.endcurr  as CurrencyType,             --68                 
                                --a.endprice*d.PACKUNIT as CarriageValue, --69
                                0 as CarriageValue, --69
                                '2' as DutyPaymentType,                 --70
                                '{6}' as DutyPayerAccountNumber,  --71
                                '6' as TermsOfSale,                      --72 待客人确认
                                'KGS' AS WeightType,                     --75
                                --a.endprice*d.PACKUNIT as CommodityCustomsValue,             --78 待客人确认
                                a.endprice*d.PACKUNIT*100 as CommodityCustomsValue,             --78 待客人确认
                                '{7}' as CommodityDescriptionLine,          --79 待客人确认
                                (select shippercntycode   from PPSUSER.t_shipper where rownum<=1) as CountryofManufacture,--80
                                '' as CommodityDescriptionLine2,         --88
                                '' as CommodityDescriptionLine3,         --97
                                'N' as CommercialInvoiceFlag,            --113
                                b.tracking_no as TrackingNumberofCrn,     --114
                                CASE WHEN A.Servicelevel='IPD' THEN 1
                                    WHEN A.Servicelevel='IED' THEN 1  
                                    WHEN A.Servicelevel='IP' THEN C.CARTON_QTY
                                    WHEN A.Servicelevel='IPF' THEN C.CARTON_QTY
                                        END AS totalNumberofPackages ,           --116 是Service
                                (select SHIPPERCNTYCODE   from PPSUSER.t_shipper where rownum<=1) as SenderCountryCode,--117
                                (select shippertel   from PPSUSER.t_shipper where rownum<=1) as SenderPhoneNumber,--183
                                a.servicelevel  as  ServiceType,--627
                                '{1}' AS ReturnContactName , --700
                                '{2}' AS ReturnCompany, --701
                                '{3}' AS ReturnAddress1, --702
                                '{4}' AS ReturnAddress2, --703
                                a.custpono AS CustomerPurchaseOrderNo,   --709
                                b.box_no AS CartonNo,--711
                                C.CARTON_QTY AS CartonCount,--712
                                b.sscc AS CustomBarcode,--713
                                a.custsono AS SalesOrderNo,--714
                                a.weborderNo AS WebOrderNo,--715  ---modified by Jammy 20200915
                                a.deliveryno,--716                     
                                to_char(C.SHIPPING_TIME,'YYYYMMDD') AS ShipDate2,--717
                                a.mpn AS PartNo,--718
                                '{8}' AS DescriptionLine1,--719
                                '' AS DescriptionLine2,--720
                                '' AS ShippingInstruction,--721
                                a.custdelitem AS LineItemNumber,--722
                                d.PACKUNIT AS QuantityShipped,--723
                                a.qty AS QuantityOrdered,--724
                                '' AS ProductModelLine1,--726
                                a.ShipToAddress4 AS District,--728
                                'C' AS DimUnit,--1116
                                'N' AS SaturdayDeliveryFlag,--1226
                                CASE WHEN A.Servicelevel='IPD' THEN '18'
                                    WHEN A.Servicelevel='IED' THEN '17'  
                                    WHEN A.Servicelevel='IP' THEN '01'
                                    WHEN A.Servicelevel='IPF' THEN '70'
                                        END AS ServiceType2,--1274
                                        d.GROSSWEIGHTKG AS PackageWeight--1670
                                FROM PPSUSER.T_940_UNICODE  A INNER JOIN PPSUSER.T_SN_STATUS B 
                                ON A.DELIVERYNO=B.DELIVERY_NO AND A.CUSTDELITEM=B.LINE_ITEM
                                INNER JOIN PPSUSER.T_SHIPMENT_INFO C ON B.SHIPMENT_ID=C.SHIPMENT_ID
                                INNER JOIN PPTEST.VW_MPN_INFO D ON B.PART_NO=D.ICTPARTNO
                                WHERE B.CARTON_NO='{0}'
                                ";
            string sql = string.Format(handleSql, cartonNo, strreturnto1, strreturnto2, strreturnto3, strreturnto4, SenderAccountNumber_5, DutyPayerAccountNumber_6,
                CommodityDescriptionLine_7, DescriptionLine1_8);
            #endregion


            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        //public DataTable getUpsInfoByCartonNo(string cartonNo, string region)
        //{
        //    string handleSql = @" select distinct tsi.hawb,
        //                                tsi.shipment_tracking,
        //                                tss.tracking_no,
        //                                to_char(tsi.shipping_time, 'yyyy/MM/dd') as shipdate,
        //                                t9u.parcelaccountnumber,
        //                                (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
        //                                (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
        //                                (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
        //                                '' as SHIPER_ADDRESS3,
        //                                (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
        //                                (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
        //                                (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
        //                                (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
        //                                '' as Consignee_UPS_Account_number,
        //                                t9u.shiptoname,
        //                                t9u.shiptocompany,
        //                                t9u.shiptoconttel,
        //                                case
        //                                  when length(t9u.shiptoaddress) > 35 then
        //                                   substr(t9u.shiptoaddress, 1, 35)
        //                                  else
        //                                   t9u.shiptoaddress
        //                                end as ST_ADDR1,
        //                                case
        //                                  when length(t9u.shiptoaddress) > 35 then
        //                                   substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
        //                                  else
        //                                   t9u.shiptoaddress2
        //                                end as ST_ADDR2,                     
        //                                case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
        //                                          NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
        //                                          THEN ''
        //                                      when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
        //                                   to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
        //                                  when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
        //                                   to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
        //                                  else
        //                                   to_char(cast((t9u.shiptoaddress3 || ',' ||
        //                                                t9u. shiptoaddress4) as varchar2(100)))
        //                                end       
        //                                 as ST_ADDR3,
        //                                t9u.shiptocity,
        //                                decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) as regiondesc, --RegionDesc  如果有=号码, 那么取=号之前的
        //                                t9u.shiptozip,
        //                                t9u.shipcntycode,
        //                                tss.box_no as CARTON_SEQUNECE,
        //                                (select sum(toi.carton_qty)
        //                                   from ppsuser.t_order_info toi
        //                                  where toi.delivery_no =
        //                                        (select distinct tss.delivery_no
        //                                           from ppsuser.t_sn_status tss
        //                                          where tss.carton_no = '{0}')) as CARTON_COUNT,
        //                                (  select GROSSWEIGHTKG
        //                                      from PPsuser.vw_mpn_info P_VMI,
        //                                           (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
        //                                              FROM PPSUSER.T_SN_STATUS TSS, 
        //                                                   PPSUSER.T_SHIPMENT_PALLET TSP
        //                                             WHERE TSS.pack_pallet_no = TSP.PALLET_NO
        //                                               AND TSS.CARTON_NO = '{0}') T
        //                                     where P_VMI.ICTPARTNO = T.PART_NO
        //                                       AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,              
        //                                (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM * P_VMI.CARTONWIDTHCM) / 1000000/6000,2)
        //                                from PPsuser.vw_mpn_info P_VMI,
        //                                    (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
        //                                        FROM PPSUSER.T_SN_STATUS TSS, 
        //                                            PPSUSER.T_SHIPMENT_PALLET TSP
        //                                        WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
        //                                          AND TSS.CARTON_NO = '{0}') T
        //                                where P_VMI.ICTPARTNO = T.PART_NO
        //                                AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
        //                                tss.sscc,
        //                                tss.delivery_no,
        //                                t9u.custsono,
        //                                t9u.custpono,
        //                                t9u.weborderno,
        //                                t9u.custdelitem,
        //                                (SELECT DISTINCT TOI.MPN
        //                                   FROM PPSUSER.T_ORDER_INFO TOI
        //                                  WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO  and toi.ictpn  =   tss.part_no) AS AC_PN,
        //                                (select count(tss_.serial_number)
        //                                   from ppsuser.t_sn_status tss_
        //                                  where tss_.carton_no = '{0}') as perCartonQty,
        //                                tss.carton_no,
        //                                '' as Delivery_Instruction,
        //                                ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
        //                                WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
        //                                'USD',
        //                                DECODE(SUBSTR(t9u.custshipinst, 1, 5),
        //                                       'ACDES',
        //                                       substr(t9u.custshipinst, 5),
        //                                       t9u.custshipinst),
        //                                '' as HAWB_,
        //                                SUBSTR(TSS.PACK_PALLET_NO,-4) as PALLET_ID,
        //                                '' as CARTON_ID
        //                  from ppsuser.t_shipment_info tsi,
        //                       ppsuser.t_sn_status     tss,
        //                       ppsuser.t_940_unicode   t9u
        //                 where tsi.shipment_id = tss.shipment_id
        //                   and tss.delivery_no = t9u.deliveryno
        //                   and tss.line_item = trim(t9u.custdelitem) 
        //                   and tss.carton_no = '{0}'";
        //    if (region.Equals("AMR"))
        //    {
        //        handleSql = @"select distinct (SELECT distinct   tsi.hawb
        //                      FROM PPSUSER.t_Order_Info    toi,
        //                           ppsuser.t_sn_status     tss,
        //                           ppsuser.t_shipment_info tsi
        //                     where toi.delivery_no = tss.delivery_no
        //                       and tsi.shipment_id = toi.shipment_id
        //                       and toi.shipment_id in
        //                           (SELECT DISTINCT TSSA.SHIPMENT_ID
        //                              FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_SAWB TSSA
        //                             WHERE TSS.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
        //                               AND TSS.CARTON_NO = '{0}')
        //                       and tss.carton_no = '{0}') AS HAWB,
        //                           (SELECT distinct   tsi.shipment_tracking
        //                          FROM PPSUSER.t_Order_Info    toi,
        //                               ppsuser.t_sn_status     tss,
        //                               ppsuser.t_shipment_info tsi
        //                         where toi.delivery_no = tss.delivery_no
        //                           and tsi.shipment_id = toi.shipment_id
        //                           and toi.shipment_id in
        //                               (SELECT DISTINCT TSSA.SHIPMENT_ID
        //                                  FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_SAWB TSSA
        //                                 WHERE TSS.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
        //                                   AND TSS.CARTON_NO = '{0}')
        //                           and tss.carton_no = '{0}') AS SHIPMENTREACKING,
        //                            tss.tracking_no,
        //                            to_char(tsi.shipping_time, 'yyyy/MM/dd') AS SHIPDATE,
        //                            t9u.parcelaccountnumber,
        //                            (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
        //                            (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
        //                            (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
        //                            '' as SHIPER_ADDRESS3,
        //                            (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
        //                            (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
        //                            (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
        //                            (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
        //                            '' as Consignee_UPS_Account_number,
        //                            t9u.shiptoname,
        //                            t9u.shiptocompany,
        //                            t9u.shiptoconttel,
        //                            case
        //                              when length(t9u.shiptoaddress) > 35 then
        //                               substr(t9u.shiptoaddress, 1, 35)
        //                              else
        //                               t9u.shiptoaddress
        //                            end as ST_ADDR1,
        //                            case
        //                              when length(t9u.shiptoaddress) > 35 then
        //                               substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
        //                              else
        //                               t9u.shiptoaddress2
        //                            end as ST_ADDR2,
        //                            case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
        //                                          NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
        //                                          THEN ''
        //                                      when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
        //                                   to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
        //                                  when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
        //                                   to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
        //                                  else
        //                                   to_char(cast((t9u.shiptoaddress3 || ',' ||
        //                                                t9u. shiptoaddress4) as varchar2(100)))
        //                                end        as ST_ADDR3,
        //                            t9u.shiptocity,
        //                            decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) AS  REGIONDESC, --RegionDesc  如果有 = 号码, 那么取 = 号之前的
        //                            t9u.shiptozip,
        //                            t9u.shipcntycode,
        //                            tss.box_no as CARTON_SEQUNECE,
        //                            (select sum(toi.carton_qty)
        //                               from ppsuser.t_order_info toi
        //                              where toi.delivery_no =
        //                                    (select distinct tss.delivery_no
        //                                       from ppsuser.t_sn_status tss
        //                                      where tss.carton_no = '{0}')
        //                                        AND TOI.SHIPMENT_ID = TSS.SHIPMENT_ID) as CARTON_COUNT,
        //                            (select GROSSWEIGHTKG
        //                               from ppsuser.vw_mpn_info P_VMI,
        //                                    (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
        //                                       FROM PPSUSER.T_SN_STATUS       TSS,
        //                                            PPSUSER.T_SHIPMENT_PALLET TSP
        //                                      WHERE TSS.pack_pallet_no = TSP.PALLET_NO
        //                                        AND TSS.CARTON_NO = '{0}') T
        //                              where P_VMI.ICTPARTNO = T.PART_NO
        //                                AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,

        //                            (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM *
        //                                          P_VMI.CARTONWIDTHCM) / 1000000 / 6000,
        //                                          2)
        //                               from ppsuser.vw_mpn_info P_VMI,
        //                                    (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
        //                                       FROM PPSUSER.T_SN_STATUS TSS,
        //                                            PPSUSER.T_SHIPMENT_PALLET TSP
        //                                      WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
        //                                        AND TSS.CARTON_NO = '{0}') T
        //                              where P_VMI.ICTPARTNO = T.PART_NO
        //                                AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
        //                            tss.sscc,
        //                            tss.delivery_no,
        //                            t9u.custsono,
        //                            t9u.custpono,
        //                            t9u.weborderno,
        //                            t9u.custdelitem,
        //                            (SELECT DISTINCT TOI.MPN
        //                               FROM PPSUSER.T_ORDER_INFO TOI
        //                              WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
        //                                and toi.ictpn = tss.part_no) AS AC_PN,
        //                            (select count(tss_.serial_number)
        //                               from ppsuser.t_sn_status tss_
        //                              where tss_.carton_no = '{0}') as perCartonQty,
        //                            tss.carton_no,
        //                            '' as Delivery_Instruction,
        //                            ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
        //                                WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
        //                            'USD',
        //                            DECODE(SUBSTR(t9u.custshipinst, 1, 7),
        //                                   'ACDES--',
        //                                   substr(t9u.custshipinst, 8),
        //                                   t9u.custshipinst),
        //                            '' as HAWB_,
        //                            SUBSTR(TSS.PACK_PALLET_NO, -4) as PALLET_ID,
        //                            '' as CARTON_ID,
        //                            tsi.shipment_tracking,
        //                            tsi.hawb as SAWB,
        //                            tsi.carton_qty,
        //                            tsi.poe
        //              from ppsuser.t_shipment_info tsi,
        //                   ppsuser.t_sn_status tss,
        //                   ppsuser.t_940_unicode t9u
        //             where tsi.shipment_id = tss.shipment_id
        //               and tss.delivery_no = t9u.deliveryno
        //               and tss.line_item = trim(t9u.custdelitem)
        //               and tss.carton_no = '{0}'";
        //    }
        //    string sql = string.Format(handleSql, cartonNo);
        //    return ClientUtils.ExecuteSQL(sql).Tables[0];
        //}
        public DataTable getTransInFileInfo(string cartonNo)
        {
            //string sql = string.Format(@"select distinct LPAD(tss.tracking_no, 9, ' ') as ConsignmentNumber,
            //                                t9u.parcelaccountnumber as SenderAccountNumber,
            //                                (select tsh.shippername from ppsuser.t_shipper tsh) as SenderName,
            //                                (select tsh.shipperaddress1 from ppsuser.t_shipper tsh) as SenderAddress1,
            //                                (select tsh.shipperaddress2 from ppsuser.t_shipper tsh) as SenderAddress2,
            //                                '' as SenderAddress3,
            //                                (select tsh.shippercity from ppsuser.t_shipper tsh) as SenderPostcode,
            //                                (select tsh.shipperstate from ppsuser.t_shipper tsh) as SenderCity,
            //                                (select tsh.shipperpostal from ppsuser.t_shipper tsh) as SenderProvince,
            //                                (select tsh.SHIPPERCNTYCODE from ppsuser.t_shipper tsh) as SenderCountry,
            //                                '' as SenderVATNumber,
            //                                '' as SenderTelephoneArea,
            //                                '' as SenderTelephoneNumber,
            //                                '' as SenderContactName,
            //                                '' as PickupName,
            //                                '' as PickupAddress1,
            //                                '' as PickupAddress2,
            //                                '' as PickupAddress3,
            //                                '' as PickupPostcode,
            //                                '' as PickupCity,
            //                                '' as PickupProvince,
            //                                '' as PickupCountry,
            //                                '' as PickupTelephone1,
            //                                '' as PickupTelephone2,
            //                                '' as PickupContactName,
            //                                '' as ReceiverAccountNumber,
            //                                t9u.shiptoname as ReceiverName,
            //                                t9u.shiptoaddress as ReceiverAddress1,
            //                                t9u.shiptostate as ReceiverAddress2,
            //                                decode(t9u.shiptoaddress4,
            //                                       '',
            //                                       t9u.shiptosploc,
            //                                       SUBSTR(t9u.shiptoaddress4 || t9u.shiptosploc,1,35)) as ReceiverAddress3,
            //                                t9u.shiptozip as ReceiverPostcode,
            //                                t9u.shiptocity as ReceiverCity,
            //                                DECODE(INSTR(t9u.regiondesc, '='),
            //                                       0,
            //                                       t9u.regiondesc,
            //                                       SUBSTR(t9u.regiondesc, INSTR(t9u.regiondesc, '=') + 1)) as ReceiverProvince,
            //                                t9u.shipcntycode as ReceiverCountry,
            //                                '' as ReceiverTelephone1,
            //                                substr(t9u.shiptoconttel,1,9) as ReceiverTelephone2,
            //                                SUBSTR(t9u.shiptocompany,1,35) as ReceiverContactName,
            //                                '' as DeliveryName,
            //                                '' as DeliveryAddress1,
            //                                '' as DeliveryAddress2,
            //                                '' as DeliveryAddress3,
            //                                '' as DeliveryPostcode,
            //                                '' as DeliveryCity,
            //                                '' as DeliveryProvince,
            //                                '' as DeliveryCountry,
            //                                '' as DeliveryTelephone1,
            //                                '' as DeliveryTelephone2,
            //                                '' as DeliveryContactName,
            //                                SUBSTR(t9u.shiproute,1,3) as ServicecodeAppleRoute,
            //                                '' as SenderReceiverpays,
            //                                '' as Serviceoption1,
            //                                '' as Serviceoption2,
            //                                '' as Serviceoption3,
            //                                '' as Serviceoption4,
            //                                (select lpad(sum(toi.CARTON_QTY), 5, '0')
            //                                   from ppsuser.t_order_info toi
            //                                  where toi.delivery_no = tss.delivery_no) as Numberofparcels,
            //                                lpad(tss.box_no,5,'0') as Sequencenumber,
            //                                '' as Typeofpacking,
            //                                '' as Marksofpacking,
            //                                '' as Height_,
            //                                '' as Width_,
            //                                '' as Length_,
            //                                '' as Volumeintegerm3,
            //                                '' as Volumedecimalsm3,
            //                                '' as Grossweightkg,
            //                                '' as Grossweightgrams,
            //                                '' as IncreasedCMRliability,
            //                                '' as Insurancevalueintege,
            //                                '' as Insurancevaluedecima,
            //                                '' as Insurancecurrency,
            //                                '' as InvoicevalueInteger,
            //                                '' as Invoicevaluedecimal,
            //                                '' as Invoicecurrency,
            //                                '' as Samplevalueint,
            //                                '' as Samplecurrencydec,
            //                                '' as Samplecurrency,
            //                                '' as Receiverreference,
            //                                tss.delivery_no as Senderreference,
            //                                '' as Sendersignatory,
            //                                '' as SpecialInstructions1,
            //                                '' as SpecialInstructions2,
            //                                '' as Collectiondatecc,
            //                                '' as Collectiondateyy,
            //                                '' as Collectiondatemm,
            //                                '' as Collectiondatedd,
            //                                '' as Collectiontime,
            //                                '' as Articledescription,
            //                                '' as BTNnumber,
            //                                '' as Numberofarticles,
            //                                '' as Hazardousclass,
            //                                '' as Tradestatus,
            //                                '' as Printerportnumber,
            //                                '' as Divisioncode,
            //                                '' as Sendingdepot,
            //                                '' as Larosedepot,
            //                                '' as Endpiecenumber,
            //                                '' as Retaincode,
            //                                '' as Actioncode,
            //                                '' as Returncode,
            //                                '' as Printeraddress,
            //                                '' as Printerdriver,
            //                                '' as Weighttypetext,
            //                                '' as Dimensiontypetext,
            //                                '' as Volumetypetext,
            //                                tss.hawb || ',' || tss.carton_no as AppleLabelRemarks,
            //                                '' as AppleUUIReference,
            //                                '' as AppleSortText,
            //                                '' as RoadAirIndicator,
            //                                '' as TradeStatusValue,
            //                                '' as AppleDCReference,
            //                                '' as WebOrderNumber,
            //                                '' as CoDIndicator,
            //                                '' as Filler,
            //                                t9u.shipidentifier as AppleShippingCondition,
            //                                '' as AppleStore,
            //                                '' as ReturnsConsignment,
            //                                '' as ReturnsDispatchRefApple,
            //                                '' as ReturnsRMARefApple,
            //                                '' as ReturnsCustomerRefTNT,
            //                                '' as ReturnsPrinterAddress,
            //                                '' as ReturnsAppleRoute,
            //                                '' as ReturnsShipCond
            //                  from ppsuser.t_940_unicode t9u, ppsuser.t_sn_status tss
            //                 where t9u.deliveryno = tss.delivery_no
            //                   and t9u.custdelitem = tss.line_item
            //                   and tss.carton_no = '{0}' ", cartonNo);
            string sql = string.Format(@"select distinct LPAD(tss.tracking_no, 9, ' ') as ConsignmentNumber,
                                            t9u.parcelaccountnumber as SenderAccountNumber,
                                            (select tsh.shippername from ppsuser.t_shipper tsh) as SenderName,
                                            (select tsh.shipperaddress1 from ppsuser.t_shipper tsh) as SenderAddress1,
                                            (select tsh.shipperaddress2 from ppsuser.t_shipper tsh) as SenderAddress2,
                                            '' as SenderAddress3,
                                            (select tsh.shipperpostal  from ppsuser.t_shipper tsh) as SenderPostcode,
                                            (select tsh.shippercity  from ppsuser.t_shipper tsh) as SenderCity,
                                            (select tsh.shipperstate  from ppsuser.t_shipper tsh) as SenderProvince,
                                            (select tsh.SHIPPERCNTYCODE from ppsuser.t_shipper tsh) as SenderCountry,
                                            '' as SenderVATNumber,
                                            '' as SenderTelephoneArea,
                                            '' as SenderTelephoneNumber,
                                            '' as SenderContactName,
                                            '' as PickupName,
                                            '' as PickupAddress1,
                                            '' as PickupAddress2,
                                            '' as PickupAddress3,
                                            '' as PickupPostcode,
                                            '' as PickupCity,
                                            '' as PickupProvince,
                                            '' as PickupCountry,
                                            '' as PickupTelephone1,
                                            '' as PickupTelephone2,
                                            '' as PickupContactName,
                                            '' as ReceiverAccountNumber,
                                            t9u.shiptoname as ReceiverName,
                                            t9u.shiptoaddress as ReceiverAddress1,
                                            t9u.ShiptoAddress2  as ReceiverAddress2,
                                            decode(t9u.shiptoaddress3, '',  t9u. shiptoaddress4,SUBSTR(t9u.shiptoaddress3 ||','|| t9u. shiptoaddress4,1,35)) as ReceiverAddress3,
                                            t9u.shiptozip as ReceiverPostcode,
                                            t9u.shiptocity as ReceiverCity,
                                            DECODE(INSTR(t9u.regiondesc, '='),
                                                   0,
                                                   t9u.regiondesc,
                                                   SUBSTR(t9u.regiondesc, INSTR(t9u.regiondesc, '=') + 1)) as ReceiverProvince,
                                            t9u.shipcntycode as ReceiverCountry,
                                            substr(t9u.shiptoconttel,1,7) as ReceiverTelephone1,
                                            substr(t9u.shiptoconttel,8) as ReceiverTelephone2,
                                            SUBSTR(t9u.shiptocompany,1,35) as ReceiverContactName,
                                            '' as DeliveryName,
                                            '' as DeliveryAddress1,
                                            '' as DeliveryAddress2,
                                            '' as DeliveryAddress3,
                                            '' as DeliveryPostcode,
                                            '' as DeliveryCity,
                                            '' as DeliveryProvince,
                                            '' as DeliveryCountry,
                                            '' as DeliveryTelephone1,
                                            '' as DeliveryTelephone2,
                                            '' as DeliveryContactName,
                                            SUBSTR(t9u.shiproute,1,3) as ServicecodeAppleRoute,
                                            '' as SenderReceiverpays,
                                            '' as Serviceoption1,
                                            '' as Serviceoption2,
                                            '' as Serviceoption3,
                                            '' as Serviceoption4,
                                            (select lpad(sum(toi.CARTON_QTY), 5, '0')
                                               from ppsuser.t_order_info toi
                                              where toi.delivery_no = tss.delivery_no) as Numberofparcels,
                                            lpad(tss.box_no,5,'0') as Sequencenumber,
                                            '' as Typeofpacking,
                                            '' as Marksofpacking,
                                            '' as Height_,
                                            '' as Width_,
                                            '' as Length_,
                                            '' as Volumeintegerm3,
                                            '' as Volumedecimalsm3,
                                            '' as Grossweightkg,
                                            '' as Grossweightgrams,
                                            '' as IncreasedCMRliability,
                                            '' as Insurancevalueintege,
                                            '' as Insurancevaluedecima,
                                            '' as Insurancecurrency,
                                            '' as InvoicevalueInteger,
                                            '' as Invoicevaluedecimal,
                                            '' as Invoicecurrency,
                                            '' as Samplevalueint,
                                            '' as Samplecurrencydec,
                                            '' as Samplecurrency,
                                            '' as Receiverreference,
                                            tss.delivery_no as Senderreference,
                                            '' as Sendersignatory,
                                            '' as SpecialInstructions1,
                                            '' as SpecialInstructions2,
                                            '' as Collectiondatecc,
                                            '' as Collectiondateyy,
                                            '' as Collectiondatemm,
                                            '' as Collectiondatedd,
                                            '' as Collectiontime,
                                            '' as Articledescription,
                                            '' as BTNnumber,
                                            '' as Numberofarticles,
                                            '' as Hazardousclass,
                                            '' as Tradestatus,
                                            '' as Printerportnumber,
                                            '' as Divisioncode,
                                            '' as Sendingdepot,
                                            '' as Larosedepot,
                                            '' as Endpiecenumber,
                                            '' as Retaincode,
                                            '' as Actioncode,
                                            '' as Returncode,
                                            '' as Printeraddress,
                                            '' as Printerdriver,
                                            '' as Weighttypetext,
                                            '' as Dimensiontypetext,
                                            '' as Volumetypetext,
                                            tss.hawb || ',' || tss.carton_no as AppleLabelRemarks,
                                            '' as AppleUUIReference,
                                            '' as AppleSortText,
                                            '' as RoadAirIndicator,
                                            '' as TradeStatusValue,
                                            '' as AppleDCReference,
                                            '' as WebOrderNumber,
                                            '' as CoDIndicator,
                                            '' as Filler,
                                            t9u.shipidentifier as AppleShippingCondition,
                                            '' as AppleStore,
                                            '' as ReturnsConsignment,
                                            '' as ReturnsDispatchRefApple,
                                            '' as ReturnsRMARefApple,
                                            '' as ReturnsCustomerRefTNT,
                                            '' as ReturnsPrinterAddress,
                                            '' as ReturnsAppleRoute,
                                            '' as ReturnsShipCond
                              from ppsuser.t_940_unicode t9u, ppsuser.t_sn_status tss
                             where t9u.deliveryno = tss.delivery_no
                               and t9u.custdelitem = tss.line_item
                               and tss.carton_no = '{0}' ", cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getT940UnicodeInfoByDeliveryNoAndLineItem(string deliveryNo)
        {
            string sql = @" select t9u.*
                              from PPSUSER.T_940_UNICODE t9u
                             where  t9u.deliveryno = '{0}' ";
            sql = string.Format(sql, deliveryNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable getUpsCodeByCartonNo(string cartonNo)
        {//tntCode  同样放于trackingNo栏位  --upscode一样
            string sql = string.Format(@" select distinct tss.tracking_no
                                          from ppsuser.t_sn_status tss
                                         where tss.carton_no = '{0}'", cartonNo);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }

        public DataTable getSF_File_InfoByCartonNo(string cartonNo)
        {
            //string sql = @"     select distinct tss.babytracking_no as AWB,
            //                    decode((select distinct tss.box_no
            //                             from ppsuser.t_sn_status tss
            //                            where tss.carton_no = :CartonNo),
            //                           '1',
            //                           '',
            //                           (select distinct tss.tracking_no
            //                              from ppsuser.t_sn_status tss,
            //                                   ppsuser.t_sn_status tss1
            //                             where tss.delivery_no = tss1.delivery_no
            //                               and tss.box_no = '1'
            //                               and tss1.carton_no = :CartonNo)) as MOTHERAWB, --如果是第一箱则是空白  反之则是DN第一箱的TrackingNo
            //                    '' as SHIPPERCOUNTRY,
            //                    '上海' as SHIPPERPROVINCE,
            //                    '上海' as SHIPPERCITY,
            //                   '' as SHIPPERDISTRICT,
            //                    (SELECT tsb.SENADD FROM PPSUSER.T_SLC_BLP tsb
            //                      where rownum = 1) as SHIPPERADDRESS,
            //                    (SELECT tsb.SENCOMNAME FROM PPSUSER.T_SLC_BLP tsb
            //                      where rownum = 1) as SHIPPERCOMPANY,
            //                    (SELECT tsb.SENDNAME FROM PPSUSER.T_SLC_BLP tsb
            //                      where rownum = 1) as SHIPPERNAME,
            //                    '' as SHIPPERTEL,
            //                    (SELECT tsb.SENCODE FROM PPSUSER.T_SLC_BLP tsb
            //                      where rownum = 1) as SHIPPERPOSTALCODE,
            //                    '' as CONSIGNEECOUNTRY,
            //                    DECODE(INSTR(t9u.regiondesc,'='),0,t9u.regiondesc,SUBSTR(t9u.regiondesc,INSTR(t9u.regiondesc,'=')+1)) as CONSIGNEEPROVINCE,
            //                    t9u.shiptocity as CONSIGNEECITY,
            //                    t9u.shiptoaddress4 as CONSIGNEEDISTRICT,
            //                    t9u.shiptoaddress as CONSIGNEEADDLINE1,
            //                    t9u.shiptoaddress2 as CONSIGNEEADDLINE2,
            //                    t9u.shiptocompany as CONSIGNEECOMPANY,
            //                    t9u.shiptoname as CONSIGNEENAME,
            //                    t9u.shiptoconttel as CONSIGNEETEL,
            //                    t9u.shiptozip as CONSIGNEEPOSTALCODE,
            //                    '电子产品' as COMMODITY,
            //                    (SELECT SUM(TOI.QTY)
            //                         FROM PPSUSER.T_ORDER_INFO TOI
            //                        WHERE toi.delivery_no =
            //                              (SELECT DISTINCT TSS.DELIVERY_NO
            //                                 FROM PPSUSER.T_SN_STATUS TSS
            //                                WHERE TSS.CARTON_NO = :CartonNo)) as TOTALPARCELQUANTITY,
            //                    (  select round(sum(t1.totalWeight), 2)
            //                       from (select   toi.Ictpn,
            //                                      toi.delivery_no,
            //                                      vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) AS  totalWeight
            //                                 from ppsuser.t_order_info toi, pptest.vw_mpn_info vmi
            //                                where toi.Ictpn = vmi.ICTPARTNO
            //                                  and toi.delivery_no =
            //                                      (SELECT DISTINCT TSS.DELIVERY_NO
            //                                         FROM PPSUSER.T_SN_STATUS TSS
            //                                        WHERE TSS.CARTON_NO = :CartonNo)
            //                         group by toi.Ictpn, toi.delivery_no, vmi.GROSSWEIGHTKG) t1) as TOTALACTUALWEIGHT,
            //                    tss.box_no || '/' ||
            //                    ( SELECT SUM(TOI.CARTON_QTY)
            //                         FROM PPSUSER.T_ORDER_INFO TOI
            //                        WHERE toi.delivery_no =
            //                              (SELECT DISTINCT TSS.DELIVERY_NO
            //                                 FROM PPSUSER.T_SN_STATUS TSS
            //                                WHERE TSS.CARTON_NO = :CartonNo)) as MOTHERCHILDTAG, --t_sn_status.BoxNo / DN 的总箱数
            //                    decode(t9u.shipidentifier,
            //                           'A8',
            //                           '标准快递',
            //                           'T1',
            //                           '标准快递',
            //                           'T4',
            //                           '标准快递',
            //                           'T5',
            //                           '标准快递',
            //                           'S1',
            //                           '即日到',
            //                           '') as SERVICETYPE,
            //                    t9u.deliveryno as ORDERID,
            //                    t9u.shipplant as PLANTCODE,
            //                    decode(t9u.shipofpay, '', 'N', 'Y') as ISCOD, --ShipOfPay=空白 是N 反之Y
            //                    '' as CODAMOUNT,
            //                    '' as PAYTYPE,
            //                    TO_CHAR(tsi.cdt,'yyyy-MM-dd HH:mi:ss') as SHIPPINGTIME,
            //                    t9u.custpono as COMMENT_,
            //                    '' as SSCCBARCODE,
            //                    tss.sscc as SSCC,
            //                    vmi.MPN as APPLEPARTNUM,
            //                    tss.carton_no as SERIALNUM,
            //                    tss.hawb as OEMINFOLINE,
            //                    t9u.weborderno as APPLEWEBORDER,
            //                    '' as FILLER1,
            //                    '' as FILLER2,
            //                    '' as FILLER3
            //      from ppsuser.t_sn_status     tss,
            //           ppsuser.t_940_unicode   t9u,
            //           ppsuser.t_shipment_info tsi,
            //           pptest.vw_mpn_info      vmi
            //     where tss.delivery_no = t9u.deliveryno
            //       and tss.line_item = t9u.custdelitem
            //       and tss.shipment_id = tsi.shipment_id
            //       and vmi.ICTPARTNO = tss.part_no
            //       and tss.carton_no = :CartonNo
            //    ";
            //string sql = @"select distinct tss.babytracking_no as AWB,
            //                    decode((select distinct tss.box_no
            //                             from ppsuser.t_sn_status tss
            //                            where tss.carton_no = :CartonNo),
            //                           '1',
            //                           '',
            //                           (select distinct tss.tracking_no
            //                              from ppsuser.t_sn_status tss,
            //                                   ppsuser.t_sn_status tss1
            //                             where tss.delivery_no = tss1.delivery_no                                           
            //                               and tss1.carton_no = :CartonNo)) as MOTHERAWB, --如果是第一箱则是空白  反之则是DN第一箱的TrackingNo
            //                    decode(t9u.shipcntycode,'CN','CN',(SELECT TSH.SHIPPERCOUNTRY FROM  PPSUSER.T_SHIPPER TSH)) as SHIPPERCOUNTRY,
            //                    DECODE(T9U.SHIPCNTYCODE,'CN','上海',(SELECT TSH.SHIPPERSTATE FROM  PPSUSER.T_SHIPPER TSH)) as SHIPPERPROVINCE,
            //                    DECODE(T9U.SHIPCNTYCODE,'CN','上海',(SELECT TSH.SHIPPERCITY FROM  PPSUSER.T_SHIPPER TSH)) as SHIPPERCITY,
            //                    DECODE(T9U.SHIPCNTYCODE,'CN','',(SELECT TSH.SHIPPERSTATE FROM  PPSUSER.T_SHIPPER TSH)) as SHIPPERDISTRICT,
            //                    DECODE(T9U.SHIPCNTYCODE,'CN',(SELECT tsb.SENADD FROM PPSUSER.T_SLC_BLP tsb
            //                      where rownum = 1),(SELECT TSH.SHIPPERADDRESS1||TSH.SHIPPERADDRESS2 FROM  PPSUSER.T_SHIPPER TSH)) as SHIPPERADDRESS,
            //                    DECODE(T9U.SHIPCNTYCODE,'CN',(SELECT tsb.SENCOMNAME FROM PPSUSER.T_SLC_BLP tsb
            //                      where rownum = 1),(SELECT TSH.SHIPPERNAME FROM  PPSUSER.T_SHIPPER TSH)) as SHIPPERCOMPANY,
            //                    DECODE(T9U.SHIPCNTYCODE,'CN',(SELECT tsb.SENDNAME FROM PPSUSER.T_SLC_BLP tsb
            //                      where rownum = 1),(SELECT TSH.SHIPPERNAME FROM  PPSUSER.T_SHIPPER TSH)) as SHIPPERNAME,
            //                    DECODE(T9U.SHIPCNTYCODE,'CN','',(SELECT TSH.SHIPPERTEL FROM  PPSUSER.T_SHIPPER TSH)) as SHIPPERTEL,
            //                    DECODE(T9U.SHIPCNTYCODE,'CN',(SELECT tsb.SENCODE FROM PPSUSER.T_SLC_BLP tsb),(SELECT TSH.SHIPPERPOSTAL FROM  PPSUSER.T_SHIPPER TSH)) AS SHIPPERPOSTALCODE,
            //                    T9U.SHIPCNTYCODE as CONSIGNEECOUNTRY,
            //                    DECODE(INSTR(t9u.regiondesc,'='),0,t9u.regiondesc,SUBSTR(t9u.regiondesc,INSTR(t9u.regiondesc,'=')+1)) as CONSIGNEEPROVINCE,
            //                    t9u.shiptocity as CONSIGNEECITY,
            //                    t9u.shiptoaddress4 as CONSIGNEEDISTRICT,
            //                    t9u.shiptoaddress as CONSIGNEEADDLINE1,
            //                    t9u.shiptoaddress2 as CONSIGNEEADDLINE2,
            //                    t9u.shiptocompany as CONSIGNEECOMPANY,
            //                    t9u.shiptoname as CONSIGNEENAME,
            //                    t9u.shiptoconttel as CONSIGNEETEL,
            //                    t9u.shiptozip as CONSIGNEEPOSTALCODE,
            //                    '电子产品' as COMMODITY,
            //                    (SELECT SUM(TOI.QTY)
            //                         FROM PPSUSER.T_ORDER_INFO TOI
            //                        WHERE toi.delivery_no =
            //                              (SELECT DISTINCT TSS.DELIVERY_NO
            //                                 FROM PPSUSER.T_SN_STATUS TSS
            //                                WHERE TSS.CARTON_NO = :CartonNo)) as TOTALPARCELQUANTITY,
            //                    (  select round(sum(t1.totalWeight), 2)
            //                       from (select   toi.Ictpn,
            //                                      toi.delivery_no,
            //                                      vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) AS  totalWeight
            //                                 from ppsuser.t_order_info toi, PPTEST.vw_mpn_info vmi
            //                                where toi.Ictpn = vmi.ICTPARTNO
            //                                  and toi.delivery_no =
            //                                      (SELECT DISTINCT TSS.DELIVERY_NO
            //                                         FROM PPSUSER.T_SN_STATUS TSS
            //                                        WHERE TSS.CARTON_NO = :CartonNo)
            //                         group by toi.Ictpn, toi.delivery_no, vmi.GROSSWEIGHTKG) t1) as TOTALACTUALWEIGHT,
            //                    tss.box_no || '/' ||
            //                    ( SELECT SUM(TOI.CARTON_QTY)
            //                         FROM PPSUSER.T_ORDER_INFO TOI
            //                        WHERE toi.delivery_no =
            //                              (SELECT DISTINCT TSS.DELIVERY_NO
            //                                 FROM PPSUSER.T_SN_STATUS TSS
            //                                WHERE TSS.CARTON_NO = :CartonNo)) as MOTHERCHILDTAG, --t_sn_status.BoxNo / DN 的总箱数
            //                    decode(t9u.shipidentifier,
            //                           'A8',
            //                           '标准快递',
            //                           'T1',
            //                           '标准快递',
            //                           'T4',
            //                           '标准快递',
            //                           'T5',
            //                           '标准快递',
            //                           'S1',
            //                           '即日到',
            //                           '') as SERVICETYPE,
            //                    t9u.deliveryno as ORDERID,
            //                    t9u.shipplant as PLANTCODE,
            //                    decode(t9u.shipofpay, '', 'N', 'Y') as ISCOD, --ShipOfPay=空白 是N 反之Y
            //                    '' as CODAMOUNT,
            //                    '' as PAYTYPE,
            //                    TO_CHAR(tsi.cdt,'yyyy-MM-dd HH:mi:ss') as SHIPPINGTIME,
            //                    t9u.custpono as COMMENT_,
            //                    '' as SSCCBARCODE,
            //                    tss.sscc as SSCC,
            //                    vmi.MPN as APPLEPARTNUM,
            //                    tss.carton_no as SERIALNUM,
            //                    tss.hawb as OEMINFOLINE,
            //                    t9u.weborderno as APPLEWEBORDER,
            //                    '' as FILLER1,
            //                    '' as FILLER2,
            //                    '' as FILLER3
            //      from ppsuser.t_sn_status     tss,
            //           ppsuser.t_940_unicode   t9u,
            //           ppsuser.t_shipment_info tsi,
            //           pptest.vw_mpn_info      vmi
            //     where tss.delivery_no = t9u.deliveryno
            //       and tss.line_item = t9u.custdelitem
            //       and tss.shipment_id = tsi.shipment_id
            //       and vmi.ICTPARTNO = tss.part_no
            //       and tss.carton_no = :CartonNo";
            string sql = @"select distinct tss.babytracking_no as AWB,
                                decode((select distinct tss.box_no
                                         from ppsuser.t_sn_status tss
                                        where tss.carton_no = :CartonNo),
                                       '1',
                                       '',
                                       (select distinct tss.tracking_no
                                          from ppsuser.t_sn_status tss,
                                               ppsuser.t_sn_status tss1
                                         where tss.delivery_no = tss1.delivery_no                                           
                                           and tss1.carton_no = :CartonNo)) as MOTHERAWB, --如果是第一箱则是空白  反之则是DN第一箱的TrackingNo
                                  decode(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.shippercountry
                 FROM PPSUSER.T_SLC_BLP tsb
                where rownum = 1),
              (SELECT TSH.SHIPPERCOUNTRY FROM PPSUSER.T_SHIPPER TSH)) as SHIPPERCOUNTRY,
       DECODE(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.shipperprovince
                 FROM PPSUSER.T_SLC_BLP tsb
                where rownum = 1),
              (SELECT TSH.SHIPPERSTATE FROM PPSUSER.T_SHIPPER TSH)) as SHIPPERPROVINCE,
       DECODE(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.shippercity
                 FROM PPSUSER.T_SLC_BLP tsb
                where rownum = 1),
              (SELECT TSH.SHIPPERCITY FROM PPSUSER.T_SHIPPER TSH)) as SHIPPERCITY,
       DECODE(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.shipperdistrict
                 FROM PPSUSER.T_SLC_BLP tsb
                where rownum = 1),
              (SELECT TSH.SHIPPERSTATE FROM PPSUSER.T_SHIPPER TSH)) as SHIPPERDISTRICT,
       DECODE(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.SENADD FROM PPSUSER.T_SLC_BLP tsb where rownum = 1),
              (SELECT TSH.SHIPPERADDRESS1 || TSH.SHIPPERADDRESS2
                 FROM PPSUSER.T_SHIPPER TSH)) as SHIPPERADDRESS,
       DECODE(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.SENCOMNAME
                 FROM PPSUSER.T_SLC_BLP tsb
                where rownum = 1),
              (SELECT TSH.SHIPPERNAME FROM PPSUSER.T_SHIPPER TSH)) as SHIPPERCOMPANY,
       DECODE(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.SENDNAME
                 FROM PPSUSER.T_SLC_BLP tsb
                where rownum = 1),
              (SELECT TSH.SHIPPERNAME FROM PPSUSER.T_SHIPPER TSH)) as SHIPPERNAME,
       DECODE(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.sentel FROM PPSUSER.T_SLC_BLP tsb where rownum = 1),
              (SELECT TSH.SHIPPERTEL FROM PPSUSER.T_SHIPPER TSH)) as SHIPPERTEL,
       DECODE(T9U.SHIPCNTYCODE,
              'CN',
              (SELECT tsb.SENCODE FROM PPSUSER.T_SLC_BLP tsb),
              (SELECT TSH.SHIPPERPOSTAL FROM PPSUSER.T_SHIPPER TSH)) AS SHIPPERPOSTALCODE,
                T9U.SHIPCNTYCODE as CONSIGNEECOUNTRY,
                                DECODE(t9u.shipcntycode,'HK','','SG','','TW','',DECODE(INSTR(t9u.regiondesc,'='),0,t9u.regiondesc,SUBSTR(t9u.regiondesc,INSTR(t9u.regiondesc,'=')+1))) as CONSIGNEEPROVINCE,
                                t9u.shiptocity as CONSIGNEECITY,
                                decode(t9u.shipcntycode,'HK','','SG','','TW','',t9u.shiptoaddress4) as CONSIGNEEDISTRICT,
                                t9u.shiptoaddress as CONSIGNEEADDLINE1,
                                t9u.shiptoaddress2 as CONSIGNEEADDLINE2,
                                t9u.shiptocompany as CONSIGNEECOMPANY,
                                t9u.shiptoname as CONSIGNEENAME,
                                t9u.shiptoconttel as CONSIGNEETEL,
                                decode(t9u.shipcntycode,'HK','',t9u.shiptozip) as CONSIGNEEPOSTALCODE,
                                '电子产品' as COMMODITY,
                                (SELECT SUM(TOI.QTY)
                                     FROM PPSUSER.T_ORDER_INFO TOI
                                    WHERE toi.delivery_no =
                                          (SELECT DISTINCT TSS.DELIVERY_NO
                                             FROM PPSUSER.T_SN_STATUS TSS
                                            WHERE TSS.CARTON_NO = :CartonNo)) as TOTALPARCELQUANTITY,
                                (  select round(sum(t1.totalWeight), 2)
                                   from (select   toi.Ictpn,
                                                  toi.delivery_no,
                                                  vmi.GROSSWEIGHTKG * SUM(TOI.CARTON_QTY) AS  totalWeight
                                             from ppsuser.t_order_info toi, PPTEST.vw_mpn_info vmi
                                            where toi.Ictpn = vmi.ICTPARTNO
                                              and toi.delivery_no =
                                                  (SELECT DISTINCT TSS.DELIVERY_NO
                                                     FROM PPSUSER.T_SN_STATUS TSS
                                                    WHERE TSS.CARTON_NO = :CartonNo)
                                     group by toi.Ictpn, toi.delivery_no, vmi.GROSSWEIGHTKG) t1) as TOTALACTUALWEIGHT,
                                tss.box_no || '/' ||
                                ( SELECT SUM(TOI.CARTON_QTY)
                                     FROM PPSUSER.T_ORDER_INFO TOI
                                    WHERE toi.delivery_no =
                                          (SELECT DISTINCT TSS.DELIVERY_NO
                                             FROM PPSUSER.T_SN_STATUS TSS
                                            WHERE TSS.CARTON_NO = :CartonNo)) as MOTHERCHILDTAG, --t_sn_status.BoxNo / DN 的总箱数
                                decode(t9u.shipidentifier,
                                       'A8',
                                       '标准快递',
                                       'T1',
                                       '标准快递',
                                       'T4',
                                       '标准快递',
                                       'T5',
                                       '标准快递',
                                       'S1',
                                       '即日到',
                                       '') as SERVICETYPE,
                                t9u.deliveryno as ORDERID,
                                t9u.shipplant as PLANTCODE,
                                decode(t9u.shipofpay, '', 'N', 'Y') as ISCOD, --ShipOfPay=空白 是N 反之Y
                                '' as CODAMOUNT,
                                '' as PAYTYPE,
                                TO_CHAR(sysdate,'yyyy-MM-dd HH24:mi') as SHIPPINGTIME,
                                t9u.custpono as COMMENT_,
                                '' as SSCCBARCODE,
                                tss.sscc as SSCC,
                                vmi.MPN as APPLEPARTNUM,
                                tss.carton_no as SERIALNUM,
                                tss.hawb as OEMINFOLINE,
                                t9u.weborderno as APPLEWEBORDER,
                                '' as FILLER1,
                                '' as FILLER2,
                                '' as FILLER3
                  from ppsuser.t_sn_status     tss,
                       ppsuser.t_940_unicode   t9u,
                       ppsuser.t_shipment_info tsi,
                       pptest.vw_mpn_info      vmi
                 where tss.delivery_no = t9u.deliveryno
                   and tss.line_item = t9u.custdelitem
                   and tss.shipment_id = tsi.shipment_id
                   and vmi.ICTPARTNO = tss.part_no
                   and tss.carton_no = :CartonNo";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CartonNo", cartonNo };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable judgeCrystalReportByCondition(string region, string custOmerGroup, string msgFlag, string gpFlag)
        {
            string sql = @"      SELECT ow.*
                                  FROM pptest.oms_ww ow
                                 where ow.region =:Region
                                   and ow.customergroup = :CustOmerGroup 
                                   and (ow.msgflag = :MsgFlag or ow.msgflag = 'ALL')
                                   and (ow.gpflag = :GpFlag or ow.gpflag = 'ALL')";
            object[][] sqlparams = new object[4][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "Region", region };
            sqlparams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "CustOmerGroup", custOmerGroup };
            sqlparams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "MsgFlag", msgFlag };
            sqlparams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "GpFlag", gpFlag };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }

        public DataTable GetMESPrintDN(string dn_no)
        {
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "DNNO", dn_no };
            return ClientUtils.ExecuteSQL(@"
SELECT DN_NO FROM PPSUSER.T_MES_PACKINGLIST WHERE DN_NO=:DNNO "
                        , sqlparams).Tables[0];
        }

        public DataTable isShipmentFinishByShipmentId(string shipmentId)
        {
            string sql = @"  SELECT TSI.*   FROM  PPSUSER.T_SHIPMENT_INFO  TSI
                               WHERE  TSI.SHIPMENT_ID = :ShipmentId
                              AND  TSI.STATUS in ('FP','LF','UF')";
            object[][] sqlparams = new object[1][];
            sqlparams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ShipmentId", shipmentId };
            return ClientUtils.ExecuteSQL(sql, sqlparams).Tables[0];
        }
        public string GetDBTypeBySP(string inparatype, out string outparavalue, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inparatype", inparatype };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outparavalue", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_GETBASICPARAMETER", procParams);
            outparavalue = ds.Tables[0].Rows[0]["outparavalue"].ToString();
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        public DataTable getShipexecInfoByCartonNo(string cartonNo, string region)
        {
            //修改逻辑 按MODEL来显示 AMR/PAC/EMEIA都要显示
            string instruction = "";
            DataTable dtTemp = ClientUtils.ExecuteSQL(string.Format(@"
SELECT DISTINCT c.HAZARDOUS 
FROM ppsuser.T_SN_STATUS a INNER JOIN ppsuser.VW_MPN_INFO b ON a.PART_NO=b.ICTPARTNO
INNER JOIN PPTEST.OMS_MODEL c ON b.CUSTMODEL=c.CUSTMODEL
WHERE a.CARTON_NO='{0}'
", cartonNo)).Tables[0];
            if ((dtTemp != null) && (dtTemp.Rows.Count > 0))
            {
                if (dtTemp.Rows[0]["HAZARDOUS"].ToString().ToUpper() == "Y")
                {
                    instruction = "Lithium Ion Batteries in Compliance with PI966 Section II";
                }
            }
            string handleSql = @" select distinct (select FGWEIGHTKGP
                                       from pptest.oms_partmapping OPP,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where OPP.PART = T.PART_NO
                                         AND (OPP.SUBPACKCODE = T.PACK_CODE OR T.PACK_CODE = OPP.PACKCODE )) as WEIGHT_UNIT,
                                        (select sum(GROSSWEIGHTKG * t.CARTON_QTY) total_DN
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT tpo.ictpn, TSP.PACK_CODE,  tot.CARTON_QTY
                                               FROM PPSUSER.T_PALLET_ORDER       tpo,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP,
                                              --PPSUSER.T_SHIPMENT_SAWB tsw,
                                              PPSUSER.T_ORDER_INFO tot,
                                              PPSUSER.T_ALLO_TRACKINGNO tat
                                                                  WHERE tpo.PALLET_NO = TSP.PALLET_NO
                                           -- and tpo.SHIPMENT_ID = tsw.SHIPMENT_ID
                                            and tot.DELIVERY_NO=tpo.DELIVERY_NO
                                                                    AND tpo.DELIVERY_NO = tat.DELIVERY_NO
                                            and tat.CARTON_NO='{0}'
                                           ) T
                                      where P_VMI.ICTPARTNO = T.ictpn
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as TOTAL_WEIGHT,
                                        '' SHIPMENTREACKING,
                                        '' SAWB,
                                        t9u.SERVICELEVELID,
                                        (select coo from PPSUSER.T_SN_STATUS where CARTON_NO='{0}' and rownum=1) OriginCountry,
                                        tsi.hawb,
                                        tsi.shipment_tracking,
                                        tss.tracking_no,
                                        to_char(tsi.shipping_time, 'yyyy/MM/dd') as shipdate,
                                        t9u.parcelaccountnumber,
                                        (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                        (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                        (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                        '' as SHIPER_ADDRESS3,
                                        (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                        (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                        (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                        (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                        '' as Consignee_UPS_Account_number,
                                        t9u.shiptoname,
                                        t9u.shiptocompany,
                                        t9u.shiptoconttel,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 1, 35)
                                          else
                                           t9u.shiptoaddress
                                        end as ST_ADDR1,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                          else
                                           t9u.shiptoaddress2
                                        end as ST_ADDR2,                     
                                        case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end       
                                         as ST_ADDR3,
                                        t9u.shiptocity,
                                        decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) as regiondesc, --RegionDesc  如果有=号码, 那么取=号之前的
                                        t9u.shiptozip,
                                        t9u.shipcntycode,
                                        tss.box_no as CARTON_SEQUNECE,
                                        (select sum(toi.carton_qty)
                                           from ppsuser.t_order_info toi
                                          where toi.delivery_no =
                                                (select distinct tss.delivery_no
                                                   from ppsuser.t_sn_status tss
                                                  where tss.carton_no = '{0}')) as CARTON_COUNT,
                                        (  select GROSSWEIGHTKG
                                              from PPsuser.vw_mpn_info P_VMI,
                                                   (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                      FROM PPSUSER.T_SN_STATUS TSS, 
                                                           PPSUSER.T_SHIPMENT_PALLET TSP
                                                     WHERE TSS.pack_pallet_no = TSP.PALLET_NO
                                                       AND TSS.CARTON_NO = '{0}') T
                                             where P_VMI.ICTPARTNO = T.PART_NO
                                               AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,              
                                        (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM * P_VMI.CARTONWIDTHCM) / 1000000/6000,2)
                                        from PPsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                FROM PPSUSER.T_SN_STATUS TSS, 
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                                WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
                                                  AND TSS.CARTON_NO = '{0}') T
                                        where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                        tss.sscc,
                                        tss.delivery_no,
                                        t9u.custsono,
                                        t9u.custpono,
                                        t9u.weborderno,
                                        t9u.custdelitem,
                                        (SELECT DISTINCT TOI.MPN
                                           FROM PPSUSER.T_ORDER_INFO TOI
                                          WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO  and toi.ictpn  =   tss.part_no) AS AC_PN,
                                        (select count(tss_.serial_number)
                                           from ppsuser.t_sn_status tss_
                                          where tss_.carton_no = '{0}') as perCartonQty,
                                        tss.carton_no,
                                        '{1}' as Delivery_Instruction,
                                        ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                        'USD',
                                        DECODE(SUBSTR(t9u.custshipinst, 1, 5),
                                               'ACDES',
                                               substr(t9u.custshipinst, 5),
                                               t9u.custshipinst),
                                        '' as HAWB_,
                                        SUBSTR(TSS.PACK_PALLET_NO,-4) as PALLET_ID,
                                        '' as CARTON_ID
                          from ppsuser.t_shipment_info tsi,
                               ppsuser.t_sn_status     tss,
                               ppsuser.t_940_unicode   t9u
                         where tsi.shipment_id = tss.shipment_id
                           and tss.delivery_no = t9u.deliveryno
                           and tss.line_item = trim(t9u.custdelitem) 
                           and tss.carton_no = '{0}'";
            #region
            /*string handleSql = @" select distinct tsi.hawb,
            tsi.shipment_tracking,
                                        tss.tracking_no,
                                        to_char(tsi.shipping_time, 'yyyy/MM/dd') as shipdate,
                                        t9u.parcelaccountnumber,
                                        (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                        (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                        (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                        '' as SHIPER_ADDRESS3,
                                        (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                        (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                        (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                        (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                        '' as Consignee_UPS_Account_number,
                                        t9u.shiptoname,
                                        t9u.shiptocompany,
                                        t9u.shiptoconttel,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 1, 35)
                                          else
                                           t9u.shiptoaddress
                                        end as ST_ADDR1,
                                        case
                                          when length(t9u.shiptoaddress) > 35 then
                                           substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                          else
                                           t9u.shiptoaddress2
                                        end as ST_ADDR2,                     
                                        case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end       
                                         as ST_ADDR3,
                                        t9u.shiptocity,
                                        decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) as regiondesc, --RegionDesc  如果有=号码, 那么取=号之前的
                                        t9u.shiptozip,
                                        t9u.shipcntycode,
                                        tss.box_no as CARTON_SEQUNECE,
                                        (select sum(toi.carton_qty)
                                           from ppsuser.t_order_info toi
                                          where toi.delivery_no =
                                                (select distinct tss.delivery_no
                                                   from ppsuser.t_sn_status tss
                                                  where tss.carton_no = '{0}')) as CARTON_COUNT,
                                        (  select GROSSWEIGHTKG
                                              from PPsuser.vw_mpn_info P_VMI,
                                                   (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                      FROM PPSUSER.T_SN_STATUS TSS, 
                                                           PPSUSER.T_SHIPMENT_PALLET TSP
                                                     WHERE TSS.pack_pallet_no = TSP.PALLET_NO
                                                       AND TSS.CARTON_NO = '{0}') T
                                             where P_VMI.ICTPARTNO = T.PART_NO
                                               AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,              
                                        (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM * P_VMI.CARTONWIDTHCM) / 1000000/6000,2)
                                        from PPsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                                FROM PPSUSER.T_SN_STATUS TSS, 
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                                WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
                                                  AND TSS.CARTON_NO = '{0}') T
                                        where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                        tss.sscc,
                                        tss.delivery_no,
                                        t9u.custsono,
                                        t9u.custpono,
                                        t9u.weborderno,
                                        t9u.custdelitem,
                                        (SELECT DISTINCT TOI.MPN
                                           FROM PPSUSER.T_ORDER_INFO TOI
                                          WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO  and toi.ictpn  =   tss.part_no) AS AC_PN,
                                        (select count(tss_.serial_number)
                                           from ppsuser.t_sn_status tss_
                                          where tss_.carton_no = '{0}') as perCartonQty,
                                        tss.carton_no,
                                        '{1}' as Delivery_Instruction,
                                        ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                        'USD',
                                        DECODE(SUBSTR(t9u.custshipinst, 1, 5),
                                               'ACDES',
                                               substr(t9u.custshipinst, 5),
                                               t9u.custshipinst) custshipinst,
                                        '' as HAWB_,
                                        SUBSTR(TSS.PACK_PALLET_NO,-4) as PALLET_ID,
                                        '' as CARTON_ID
                          from ppsuser.t_shipment_info tsi,
                               ppsuser.t_sn_status     tss,
                               ppsuser.t_940_unicode   t9u
                         where tsi.shipment_id = tss.shipment_id
                           and tss.delivery_no = t9u.deliveryno
                           and tss.line_item = trim(t9u.custdelitem) 
                           and tss.carton_no = '{0}'";*/
            #endregion 
            if (region.Equals("AMR"))
            {
                handleSql = @"select distinct (select FGWEIGHTKGP
                               from pptest.oms_partmapping OPP,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where OPP.PART = T.PART_NO
                                        AND (OPP.SUBPACKCODE = T.PACK_CODE OR T.PACK_CODE = OPP.PACKCODE )) as WEIGHT_UNIT,
                             
                             (select sum(GROSSWEIGHTKG * t.CARTON_QTY) total_DN
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT tpo.ictpn, TSP.PACK_CODE,  tot.CARTON_QTY
                                               FROM PPSUSER.T_PALLET_ORDER       tpo,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP,
                                                    PPSUSER.T_SHIPMENT_SAWB tsw,
                                                    PPSUSER.T_ORDER_INFO tot,
                                                    PPSUSER.T_ALLO_TRACKINGNO tat
                                              WHERE tpo.PALLET_NO = TSP.PALLET_NO
                                                and tpo.SHIPMENT_ID = tsw.SHIPMENT_ID
                                                and tot.DELIVERY_NO=tpo.DELIVERY_NO
                                                AND tpo.DELIVERY_NO = tat.DELIVERY_NO
                        and tat.CARTON_NO='{0}'
                       ) T
                                      where P_VMI.ICTPARTNO = T.ictpn
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as TOTAL_WEIGHT,
                            t9u.SERVICELEVELID,
                            (select coo from PPSUSER.T_SN_STATUS where CARTON_NO='{0}' and rownum=1) OriginCountry,

                          (SELECT distinct   tsi.hawb
                              FROM PPSUSER.t_Order_Info    toi,
                                   ppsuser.t_sn_status     tss,
                                   ppsuser.t_shipment_info tsi
                             where toi.delivery_no = tss.delivery_no
                               and tsi.shipment_id = toi.shipment_id
                               and toi.shipment_id in
                                   (SELECT DISTINCT TSSA.SHIPMENT_ID
                                      FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_SAWB TSSA
                                     WHERE TSS.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                       AND TSS.CARTON_NO = '{0}')
                               and tss.carton_no = '{0}') AS HAWB,
                                   (SELECT distinct   tsi.shipment_tracking
                                  FROM PPSUSER.t_Order_Info    toi,
                                       ppsuser.t_sn_status     tss,
                                       ppsuser.t_shipment_info tsi
                                 where toi.delivery_no = tss.delivery_no
                                   and tsi.shipment_id = toi.shipment_id
                                   and toi.shipment_id in
                                       (SELECT DISTINCT TSSA.SHIPMENT_ID
                                          FROM PPSUSER.T_SN_STATUS TSS, PPSUSER.T_SHIPMENT_SAWB TSSA
                                         WHERE TSS.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                           AND TSS.CARTON_NO = '{0}')
                                   and tss.carton_no = '{0}') AS SHIPMENTREACKING,
                                    tss.tracking_no,
                                    to_char(tsi.shipping_time, 'yyyy/MM/dd')  as shipdate,
                                    t9u.parcelaccountnumber,
                                    (SELECT tsh.SHIPPERNAME FROM ppsuser.t_shipper tsh) as SHIPER_CORP_NAME,
                                    (SELECT tsh.shipperaddress1 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS1,
                                    (SELECT tsh.shipperaddress2 FROM ppsuser.t_shipper tsh) as SHIPER_ADDRESS2,
                                    '' as SHIPER_ADDRESS3,
                                    (SELECT tsh.shippercity FROM ppsuser.t_shipper tsh) as SHIPER_CITY,
                                    (SELECT tsh.shipperstate FROM ppsuser.t_shipper tsh) as SHIPER_STATE_PROVINCE,
                                    (SELECT tsh.shipperpostal FROM ppsuser.t_shipper tsh) as SHIPER_POSTCODE,
                                    (SELECT tsh.SHIPPERCNTYCODE FROM ppsuser.t_shipper tsh) as SHIPER_COUNTRY,
                                    '' as Consignee_UPS_Account_number,
                                    t9u.shiptoname,
                                    t9u.shiptocompany,
                                    t9u.shiptoconttel,
                                    case
                                      when length(t9u.shiptoaddress) > 35 then
                                       substr(t9u.shiptoaddress, 1, 35)
                                      else
                                       t9u.shiptoaddress
                                    end as ST_ADDR1,
                                    case
                                      when length(t9u.shiptoaddress) > 35 then
                                       substr(t9u.shiptoaddress, 35) || t9u.shiptoaddress2
                                      else
                                       t9u.shiptoaddress2
                                    end as ST_ADDR2,
                                    case when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' AND
                                                  NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL'
                                                  THEN ''
                                              when nvl(t9u.shiptoaddress3,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress4 as varchar2(100)))
                                          when NVL(t9u.shiptoaddress4,'IS_NULL')='IS_NULL' then
                                           to_char(cast(t9u.shiptoaddress3 as varchar2(100)))
                                          else
                                           to_char(cast((t9u.shiptoaddress3 || ',' ||
                                                        t9u. shiptoaddress4) as varchar2(100)))
                                        end        as ST_ADDR3,
                                    t9u.shiptocity,
                                    decode(instr(t9u.regiondesc,'='),0,t9u.regiondesc,substr(t9u.regiondesc,1,instr(t9u.regiondesc,'=')+1)) AS  REGIONDESC, --RegionDesc  如果有 = 号码, 那么取 = 号之前的
                                    t9u.shiptozip,
                                    t9u.shipcntycode,
                                    tss.box_no as CARTON_SEQUNECE,
                                    (select sum(toi.carton_qty)
                                       from ppsuser.t_order_info toi
                                      where toi.delivery_no =
                                            (select distinct tss.delivery_no
                                               from ppsuser.t_sn_status tss
                                              where tss.carton_no = '{0}')
                                                AND TOI.SHIPMENT_ID = TSS.SHIPMENT_ID) as CARTON_COUNT,
                                    (select GROSSWEIGHTKG
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE TSS.pack_pallet_no = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as DN_TOTAL_WEIGHT,
                
                                    (select ROUND((P_VMI.CARTONLENGTHCM * P_VMI.CARTONHEIGHTCM *
                                                  P_VMI.CARTONWIDTHCM) / 1000000 / 6000,
                                                  2)
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE TSS.Pack_Pallet_No = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                    tss.sscc,
                                    tss.delivery_no,
                                    t9u.custsono,
                                    t9u.custpono,
                                    t9u.weborderno,
                                    t9u.custdelitem,
                                    (SELECT DISTINCT TOI.MPN
                                       FROM PPSUSER.T_ORDER_INFO TOI
                                      WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
                                        and toi.ictpn = tss.part_no) AS AC_PN,
                                    (select count(tss_.serial_number)
                                       from ppsuser.t_sn_status tss_
                                      where tss_.carton_no = '{0}') as perCartonQty,
                                    tss.carton_no,
                                    '{1}' as Delivery_Instruction,
                                    ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                    'USD',
                                    DECODE(SUBSTR(t9u.custshipinst, 1, 7),
                                           'ACDES--',
                                           substr(t9u.custshipinst, 8),
                                           t9u.custshipinst),
                                    '' as HAWB_,
                                    SUBSTR(TSS.PACK_PALLET_NO, -4) as PALLET_ID,
                                    '' as CARTON_ID,
                                    tsi.shipment_tracking,
                                    tsi.hawb as SAWB,
                                    tsi.carton_qty,
                                    tsi.poe
                      from ppsuser.t_shipment_info tsi,
                           ppsuser.t_sn_status tss,
                           ppsuser.t_940_unicode t9u
                     where tsi.shipment_id = tss.shipment_id
                       and tss.delivery_no = t9u.deliveryno
                       and tss.line_item = trim(t9u.custdelitem)
                       and tss.carton_no = '{0}'";
            }
            string sql = string.Format(handleSql, cartonNo, instruction);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
    }
}
