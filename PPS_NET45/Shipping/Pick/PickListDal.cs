using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace PickList
{
    class PickListDal
    {
        public DataSet GetStockInfoDataTable(string ictpartno)
        {

            string sql = string.Empty;
            sql = string.Format("Select b.part_no 料号, b.location_no 库位, b.CARTONQTY - b.QHCARTONQTY 箱数 "
                                + "      from ppsuser.t_location b "
                                + "     where b.qty > 0 "
                                + "       and b. cartonqty > 0 "
                                + "       and(b.part_no = '{0}' or "
                                + "           b.part_no in (select distinct a.part_no "
                                + "                          from ppsuser.t_sn_status a "
                                + "                         where a.customer_sn = '{1}' "
                                + "                            or a.carton_no = '{2}' "
                                + "                            or a.pallet_no = '{3}')) "
                                + "     order by b.Udt asc", ictpartno, ictpartno, ictpartno, ictpartno);
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
        public DataSet GetStockCarInfoDataTable(string ictpartno, string palletno)
        {
            //是否为Person
            Int32 intPersonDNCount = 0;
            string strsql = string.Empty;
            strsql = string.Format("select count (delivery_no) as PPARTCOUNT "
                                   + "   from ppsuser.t_order_info "
                                   + "  where (delivery_no, line_item, ictpn) in "
                                   + "        (select delivery_no, line_item, ictpn "
                                   + "           from ppsuser.t_pallet_order "
                                   + "          where pallet_no = '{0}' "
                                   + "            and (ictpn = '{1}' or  "
                                    + "           ictpn in (select distinct a.part_no "
                                    + "                          from ppsuser.t_sn_status a "
                                    + "                         where a.customer_sn = '{2}' "
                                    + "                            or a.carton_no = '{3}' "
                                    + "                            or a.pallet_no = '{4}'))) "
                                   + "            and person_flag = 'Y'", palletno, ictpartno, ictpartno, ictpartno, ictpartno);

            try
            {
                DataTable db = ClientUtils.ExecuteSQL(strsql).Tables[0];
                if (db.Rows.Count > 0)
                {
                    intPersonDNCount = Convert.ToInt32(db.Rows[0]["PPARTCOUNT"].ToString());
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

            string sql = string.Empty;
            if (intPersonDNCount == 0)
            {
                //location order by update time by wenxing 2020-11-27
                sql = string.Format(@"select t.location_no 库位, t.qty 箱数, t.trolley 车行号, t.coo
                                      from (Select distinct b.part_no,
                                                            b.location_no,
                                                            '' trolley,
                                                            (b.CARTONQTY - b.QHCARTONQTY) qty,
                                                            A.COO,
                                                            min(a.udt) as udt
                                              from ppsuser.t_location b, ppsuser.t_sn_status a
                                             where a.wc = 'W0'
                                               and A.LOCATION_ID(+) = B.LOCATION_ID
                                               AND b.qty > 0
                                               and b. cartonqty > 0
                                               and (b.part_no = '{0}' or
                                                   b.part_no in
                                                   (select distinct a.part_no
                                                       from ppsuser.t_sn_status a
                                                      where a.customer_sn = '{1}'
                                                         or a.carton_no = '{2}'
                                                         or a.pallet_no = '{3}'))
                                                and b.PART_NO = a.PART_NO
                                             group by b.part_no,
                                                      b.location_no,
                                                      b.CARTONQTY,
                                                      b.QHCARTONQTY,
                                                      A.COO) t
                                     where t.qty > 0
                                     order by t.Udt asc
                                     ", ictpartno, ictpartno, ictpartno, ictpartno);
                //sql = string.Format(@"select t.location_no  库位,t.qty 箱数,t.trolley 车行号, t.coo from (Select distinct  b.part_no,
                //                   b.location_no,
                //                   '' trolley,
                //                  ( b.CARTONQTY - b.QHCARTONQTY) qty,
                //                   A.COO,
                //                   b.Udt
                //              from ppsuser.t_location b,ppsuser.t_sn_status a
                //             where a.wc='W0' and 
                //             A.LOCATION_ID(+)=B.LOCATION_ID
                //              AND b.qty > 0
                //               and b. cartonqty > 0
                //               and (b.part_no = '{0}' or
                //                   b.part_no in
                //                   (select distinct a.part_no
                //                       from ppsuser.t_sn_status a
                //                      where a.customer_sn ='{1}'
                //                         or a.carton_no = '{2}'
                //                         or a.pallet_no = '{3}')))t where t.qty>0                                  
                //             order by t.Udt asc ", ictpartno, ictpartno, ictpartno, ictpartno);
                //sql = string.Format("Select b.part_no 料号, b.location_no 库位,'' 车行号, b.CARTONQTY - b.QHCARTONQTY 箱数 "
                //                    + "      from ppsuser.t_location b "
                //                    + "     where b.qty > 0 "
                //                    + "       and b. cartonqty > 0 "
                //                    + "       and(b.part_no = '{0}' or "
                //                    + "           b.part_no in (select distinct a.part_no "
                //                    + "                          from ppsuser.t_sn_status a "
                //                    + "                         where a.customer_sn = '{1}' "
                //                    + "                            or a.carton_no = '{2}' "
                //                    + "                            or a.pallet_no = '{3}')) "
                //                    + "     order by b.Udt asc", ictpartno, ictpartno, ictpartno, ictpartno);
            }
            else
            {


                sql = string.Format(@" select aa.ictpartno 料号,
                                             aa.location_no 库位,
                                             aa.trolley_line_no || '(' || aa.pointno || ')' 车行号,
                                             aa.csnqty 箱数,
                                              aa.coo
                                        from (select a.ictpartno,
                                                     e.location_no,
                                                     b.trolley_line_no,
                                                     count(distinct a.custom_sn) csnqty,
                                                     tss.coo,
                                                     LISTAGG(decode(a.pointno, 0, null, a.pointno), ',') WITHIN GROUP(ORDER BY a.pointno) as pointno
                                                from ppsuser.t_trolley_sn_status a
                                                join ppsuser.t_trolley_line_info b
                                                  on a.trolley_no = b.trolley_no
                                                 and a.sides_no = b.sides_no
                                                 and a.level_no = b.level_no
                                                 and a.seq_no = b.seq_no
                                                left join ppsuser.t_location_trolley d
                                                  on a.trolley_no = d.trolley_no
                                                join (select location_id, max(location_no) as location_no
                                                       from sajet.wms_location
                                                      group by location_id) e
                                                  on d.location_id = e.location_id
                                                  join ppsuser.t_sn_status tss on a.carton_no=tss.carton_no
                                               where (a.delivery_no, a.line_item, a.ictpartno) in
                                                     (select delivery_no, line_item, ictpn
                                                        from ppsuser.t_pallet_order d
                                                      where pallet_no = '{0}'
                                                         and (ictpn = '{1}' or
                                                             ictpn in (select distinct a.part_no
                                                                          from ppsuser.t_sn_status a
                                                                         where a.customer_sn = '{2}'
                                                                            or a.carton_no = '{3}'
                                                                            or a.pallet_no = '{4}'))
                                                                            )
                                                 and a.trolley_no <> 'ICT-00-00-000'
                                                 and a.carton_no not in(
                                                   select carton_no from ppsuser.t_sn_ppart 
                                                 where pack_pallet_no <>'{5}' )
                                               group by a.ictpartno, e.location_no, b.trolley_line_no,tss.coo) aa
                                               ",
                                              palletno, ictpartno, ictpartno, ictpartno, ictpartno, palletno);
            }
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;
        }

        internal string GetPalletMixFlag(string palletno)
        {
            string sql = string.Format(@"selecT coo from ppsuser.t_shipment_pallet where pallet_no ='{0}'", palletno);
            DataTable dt = new DataTable();
            string strCooFlag = "";
            try
            {
                dt = ClientUtils.ExecuteSQL(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strCooFlag = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return strCooFlag;
        }

        /// <summary>
        /// 根据栈板获取目前对应coo数量
        /// </summary>
        /// <param name="mixFlag">是否混coo状态</param>
        /// <param name="strPalletno">Pack栈板号</param>
        /// <returns></returns>
        internal DataTable GetCooQty(string strPalletno, string strKP)
        {
            string sql = "";
            //if (mixFlag)
            //{
            //    sql = @"SELECT part_no,coo,sum(coo_qty) as qty FROM 
            //  ppsuser.T_ORDER_COO  where pallet_no ='{0}' ";
            //    if (!string.IsNullOrEmpty(strKP))
            //    {
            //        sql += " and part_no='{1}' ";
            //    }
            //    sql += "  group by pallet_no,part_no,coo";
            //}
            //else
            //{
            sql = @"select ictpn part_no,coo,sum(assign_qty) as qty  from ppsuser.t_pallet_order 
                      where pallet_no ='{0}' ";
            if (!string.IsNullOrEmpty(strKP))
            {
                sql += " and ictpn='{1}' ";
            }
            sql += " group by pallet_no,ictpn,coo";
            // }
            return ClientUtils.ExecuteSQL(string.Format(sql, strPalletno, strKP)).Tables[0];
        }

        public DataSet GetSNInfoDataTableDAL(string inputSno)
        {
            string sql = string.Empty;
            //sql = string.Format("select distinct customer_sn ,carton_no "
            //                              + "    from ppsuser.t_sn_status "
            //                              + "   where customer_sn = '{0}' "
            //                              + "      or carton_no = '{1}'or pallet_no = '{2}'", inputSno, inputSno, inputSno);
            sql = string.Format(@"select customer_sn, carton_no
                                                  from ppsuser.t_sn_status
                                                 where customer_sn = '{0}'
                                                union
                                                select customer_sn, carton_no
                                                  from ppsuser.t_sn_status
                                                 where carton_no = '{1}'
                                                union
                                                select customer_sn, carton_no
                                                  from ppsuser.t_sn_status
                                                 where pallet_no = '{2}' ", inputSno, inputSno, inputSno);
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public DataSet GetShipmentTypeDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format("select distinct  type "
                                          + "    from ppsuser.t_shipment_info "
                                          + "   where shipment_id = '{0}' ", inputSno);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;
        }

        public string PPSInsertWorkLogByProcedure(string insn, string inwc, string macaddress, out string errmsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inwc", inwc };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "macaddress", macaddress };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_INSERTWORKLOG", procParams);
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

        public DataSet GetDataTableDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format("select pallet_no,ictpn,qty,carton_qty,pick_qty,pick_carton, "
               + " (case "
               + "      when pick_status = 'WP' then "
               + "       'WP-未拣货' "
               + "      when pick_status = 'IP' then "
               + "       'IP-拣货中' "
               + "      when pick_status = 'FP' then "
               + "       'FP-已拣货' "
               + "      when pick_status = 'QH' then "
               + "       'QH-QHold' "
               + "      else pick_status "
               + "    end) pick_status ,"
               + " computer_name "
               + " from ppsuser.T_SHIPMENT_PALLET_PART "
               + " where pallet_no = '{0}' "
               + " order by ictpn asc", inputSno);
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }

        public string CreateSAWBSIDByProcedure(out string errmsg)
        {
            object[][] procParams = new object[1][];
            procParams[0] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "Tmes", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.T_HANDLE_SAWB_UPS", procParams);
            errmsg = ds.Tables[0].Rows[0]["Tmes"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public string GetDBTypeBySP(string inparatype, out string outparavalue, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inparatype", inparatype };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outparavalue", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_GETBASICPARAMETER", procParams);
            //create or replace procedure SP_PPS_GETBASICPARAMETER(inparatype   in varchar2,
            //                                                 outparavalue  out varchar2,
            //                                                 errmsg out varchar2) as
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

        //create or replace procedure SP_PICK_PREASSIGN(insid  in varchar2,
        //                                 errmsg out varchar2) as
        public string PPartPreAssinPalletNOBySP(string strSID, out string errmsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PICK_PREASSIGN", procParams);
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
        public string CreateTrackingNo(string strSID, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varshipmentId", strSID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varretcode", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "Tmes", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_CREATE_TRACKINGNO", procParams);
            errmsg = ds.Tables[0].Rows[0]["Tmes"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }
        }

        public string GetSugShipment(string strType, string strMac, out string strMsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "v_parttype", strType };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "v_pick_mac", strMac };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "v_shipmentid", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRetCode", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PICK_SORTSHIPMENT", procParams);
            string strRetCode = ds.Tables[0].Rows[0]["varRetCode"].ToString();
            if (strRetCode.Equals("0"))
            {
                strMsg = ds.Tables[0].Rows[0]["v_shipmentid"].ToString();
                return "OK";
            }
            else
            {
                strMsg = ds.Tables[0].Rows[0]["varRetMsg"].ToString();
                return "NG";
            }
        }

        public DataTable ShowSugData(string strMac)
        {
            string strSql = string.Format(@"
SELECT a.PARA_VALUE 
FROM PPSUSER.T_BASICPARAMETER_INFO a
WHERE a.PARA_TYPE='PICK_MAC' AND a.PARA_VALUE='{0}' AND a.ENABLED='Y'
", strMac);
            return ClientUtils.ExecuteSQL(strSql).Tables[0];
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
                           and tss.carton_no = '{0}'";
            if (region.Equals("AMR"))
            {
                handleSql = @"select distinct  (select sum(GROSSWEIGHTKG * t.CARTON_QTY) total_DN
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
                                        AND P_VMI.PACKCODE = T.PACK_CODE) TOTAL_WEIGHT,
                            t9u.SERVICELEVELID,
                            (select coo from PPSUSER.T_SN_STATUS where CARTON_NO='{0}' and rownum=1) OriginCountry, 
                            (SELECT distinct   tsi.hawb
                              FROM ppsuser.t_shipment_info tsi
                             where 
                                tsi.shipment_id in
                                   (SELECT DISTINCT TSSA.SHIPMENT_ID
                                      FROM PPSUSER.t_allo_trackingno  TAT, PPSUSER.T_SHIPMENT_SAWB TSSA
                                     WHERE TAT.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                       AND TAT.CARTON_NO = '{0}')
                               ) AS HAWB,
                                   (SELECT distinct   tsi.shipment_tracking
                                  FROM ppsuser.t_shipment_info tsi,
                                       PPSUSER.t_allo_trackingno tat
                                 where tsi.shipment_id = tsi.shipment_id
                                    and tsi.shipment_id in
                                       (SELECT DISTINCT TSSA.SHIPMENT_ID
                                      FROM PPSUSER.t_allo_trackingno  TAT, PPSUSER.T_SHIPMENT_SAWB TSSA
                                     WHERE TAT.SHIPMENT_ID = TSSA.SAWB_SHIPMENT_ID
                                       AND TAT.CARTON_NO = '{0}')
                                       and tat.carton_no = '{0}'
                                  ) AS SHIPMENTREACKING,
                                    tat.tracking_no,
                                    to_char(tsi.shipping_time, 'yyyy/MM/dd') SHIPDATE,
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
                                    tat.box_no as CARTON_SEQUNECE,
                                    (select sum(toi.carton_qty)
                                       from ppsuser.t_order_info toi
                                      where toi.delivery_no =
                                            (select distinct tat.delivery_no
                                               from ppsuser.t_allo_trackingno tat
                                              where tat.carton_no = '{0}')
                                         AND TOI.SHIPMENT_ID = Tat.SHIPMENT_ID) as CARTON_COUNT,        
                                   (select GROSSWEIGHTKG
                                       from ppsuser.vw_mpn_info P_VMI,
                                            (SELECT DISTINCT TSS.PART_NO, TSP.PACK_CODE
                                               FROM PPSUSER.T_SN_STATUS       TSS,
                                                    PPSUSER.T_SHIPMENT_PALLET TSP
                                              WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
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
                                              WHERE substr(TSS.pick_pallet_no,3) = TSP.PALLET_NO
                                                AND TSS.CARTON_NO = '{0}') T
                                      where P_VMI.ICTPARTNO = T.PART_NO
                                        AND P_VMI.PACKCODE = T.PACK_CODE) as packSize,
                                    ttl.tracking_no SSCC,
                                    tat.delivery_no,
                                    t9u.custsono,
                                    t9u.custpono,
                                    t9u.weborderno,
                                    t9u.custdelitem,
                                    (SELECT DISTINCT TOI.MPN
                                       FROM PPSUSER.T_ORDER_INFO TOI
                                      WHERE TOI.DELIVERY_NO = T9U.DELIVERYNO
                                        and toi.ictpn in (select tss.part_no from PPSUSER.T_SN_STATUS tss where tss.CARTON_NO=tat.Carton_no and rownum=1)
                                      ) AS AC_PN,
                                    (select count(tss_.serial_number)
                                       from ppsuser.t_sn_status tss_
                                      where tss_.carton_no = '{0}') as perCartonQty,
                                    tat.carton_no,
                                   '{1}' as Delivery_Instruction,
                                    ROUND(t9u.endprice*(SELECT  COUNT(TSS.SERIAL_NUMBER) FROM  PPSUSER.T_SN_STATUS  TSS
                                        WHERE  TSS.CARTON_NO='{0}'),2) as SHIPMENT_TOTAL_VALUE,
                                        
                                    'USD',
                                    DECODE(SUBSTR(t9u.custshipinst, 1, 7),
                                           'ACDES--',
                                           substr(t9u.custshipinst, 8),
                                           t9u.custshipinst) custshipinst,
                                    '' as HAWB_,
                                    (select distinct substr( substr(pick_pallet_no,3),-4)from ppsuser.t_sn_status tss where tss.carton_no = '{0}')as PALLET_ID,
                                    '' as CARTON_ID,
                                    tsi.shipment_tracking,
                                    tsi.hawb as SAWB,
                                    tsi.carton_qty,
                                    tsi.poe
                      from ppsuser.t_shipment_info tsi,
                           ppsuser.t_allo_trackingno tat,
                           ppsuser.t_tracking_no_log ttl,
                           ppsuser.t_940_unicode t9u
                     where tsi.shipment_id = tat.shipment_id
                       and tat.delivery_no = t9u.deliveryno
                       and tat.line_item = trim(t9u.custdelitem)
                       and tat.carton_no = ttl.carton_no
                       and tat.carton_no = '{0}'";
            }
            string sql = string.Format(handleSql, cartonNo, instruction);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public DataTable GetShipmentInfo(string shipmentID)
        {
            string sql = "SELECT REGION from T_SHIPMENT_INFO where SHIPMENT_ID='{0}'";
            sql = String.Format(sql, shipmentID);
            return ClientUtils.ExecuteSQL(sql).Tables[0];
        }
        public bool IsFinishShipExec(string pickPallet, out string errmsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inputData", pickPallet };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.sp_ups_api_finish", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            if (errmsg.Equals("OK"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
