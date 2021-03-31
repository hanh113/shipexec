using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RollbackDN
{
    class RollbackDal
    {


        public string CheckDNByProcedure(string dn, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", dn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.T_Check_Hold--", procParams);
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return RetMsg;
            }

        }

        public int CheckDNBySQL(string dn)
        {
            string sql = @"select  * from ppsuser.t_order_info where delivery_no=:dn
                          and shipment_id not in(
                                select shipment_id from ppsuser.t_shipment_info 
                                                    where status in('CP','UF','WS','IN','SF','LF'))";
            object[][] parameterArray = new object[1][];
            parameterArray[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "dn", dn };
            return ClientUtils.RowCount(sql, parameterArray);

        }

        public DataSet GetDNInfoDataTable(string dn)
        {
            string sql = string.Empty;
            sql = string.Format(@"select delivery_no, 
                                   line_item, 
                                   mpn, 
                                   ictpn, 
                                   status,qty,carton_qty, 
                                   pack_qty, 
                                   pack_carton_qty as pack_carton, 
                                   shipment_id, 
                                   shipment_type, 
                                   carrier_name, 
                                   poe, 
                                   region 
                              from ppsuser.t_order_info 
                              where delivery_no = '{0}' 
                                    and shipment_id not in(
                                        select shipment_id from ppsuser.t_shipment_info 
                                                            where status in('CP','UF','WS','IN','SF','LF'))", dn);

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

        public DataSet GetPICKInfoDataTable(string dn)
        {
            string sql = string.Empty;
            sql = string.Format(@"select b.delivery_no, 
                                   b.line_item, 
                                   a.ictpn, 
                                   c.qty         DN_qty, 
                                   c.carton_qty  DN_carton, 
                                   b.shipment_id, 
                                   a.pallet_no, 
                                   a.qty         shipment_qty, 
                                   a.carton_qty  shipment_Carton, 
                                   b.pack_qty, 
                                   b.pack_carton, 
                                   a.pick_qty, 
                                   a.pick_carton 
                              from ppsuser.t_shipment_pallet_part a 
                              join ppsuser.t_pallet_order b 
                                on a.pallet_no = b.pallet_no 
                               and a.ictpn = b.ictpn 
                              join ppsuser.t_order_info c 
                                on b.shipment_id = c.shipment_id 
                               and b.delivery_no = c.delivery_no 
                               and b.line_item = c.line_item 
                               and b.ictpn = c.ictpn 
                             where b.delivery_no = '{0}'
                                    and c.shipment_id not in(
                                        select shipment_id from ppsuser.t_shipment_info 
                                                            where status in('CP','UF','WS','IN','SF','LF'))", dn);

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


        public string RBShipmentIDByProcedure(string shipmentid, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", shipmentid };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            //ppsuser.SP_ZC_ROLLBACKSHIPMENTID(InputSno in varchar2,
            //                                                 RetMsg   out varchar2) as
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_ZC_ROLLBACKSHIPMENTID", procParams);
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        public string RBShipmentIDByProcedure2(string shipmentid, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", shipmentid };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            //ppsuser.SP_ZC2_ROLLBACKSHIPMENTID(InputSno in varchar2,
            //                                                 RetMsg   out varchar2) as
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_ZC2_ROLLBACKSHIPMENTID", procParams);
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        //开始按钮检查对应shipment的状态
        public string CheckDNtoShipmentIDByProcedure(string dn, out string DNType, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", dn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "DNType", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_ZC_CheckDNtoShipmentID", procParams);
            DNType = ds.Tables[0].Rows[0]["DNType"].ToString();
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return RetMsg;
            }

        }

        //锁定对应shipment的状态
        public string LockDNtoShipmentIDBySP(string dn, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", dn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_ZC_LOCKDNTOSID", procParams);
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return RetMsg;
            }

        }

        public string RBSNbyDNByProcedure(string sn, string strGroupcode, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incartonno", sn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "ingroupcode", strGroupcode };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_ZC2_ROLLBACKCARTON", procParams);
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return RetMsg;
            }

        }
        public DataSet GetDN_ZCStatusDataTable(string dn)
        {
            string sql = string.Empty;
            sql = string.Format(@"   select shipment_id, 
                                  pallet_no, 
                                  delivery_no, 
                                  ictpn, 
                                  assign_qty as 原需求数量, 
                                  assign_carton as 原需求箱数, 
                                  pack_qty as 已PACK数量, 
                                  pack_carton as 已PACK箱数, 
                                  case  when c_pick_qty >0 then c_pick_qty else 0 end   as 已PiCK数量, 
                                  case  when c_pick_carton >0 then c_pick_carton else 0 end  as 已PICK箱数, 
                                  case  when zc_pack_qty >0 then zc_pack_qty else 0 end  as ZCPACK数量, 
                                  case  when zc_pack_carton >0 then zc_pack_carton else 0 end  as ZCPACK箱数, 
                                  case  when zc_pick_qty >0 then zc_pick_qty else 0  end as ZCPiCK数量, 
                                  case  when zc_pick_carton >0 then zc_pick_carton else 0  end  as ZCPICK箱数 
                             from ppsuser.t_pallet_order2_zc 
                            where delivery_no = '{0}' and zc_dn = '{1}'", dn, dn);

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


        public DataSet GetZCDNListBySQL(string strStartTime, string strEndTime)
        {
            string sql = string.Format(@"
                                        select a.shipment_id,
                                               a.delivery_no,
                                               a.status as OMS_STATUS,
                                               a.pps_status,
                                               a.group_code,
                                               a.cdt,a.udt,a.remark
                                          from ppsuser.t_zc_dn_info a
                                         where a.cdt >= to_date('{0}', 'yyyy-mm-dd')
                                           and a.cdt <= to_date('{1}', 'yyyy-mm-dd')
                                           and status is not null
                                                        ", strStartTime, strEndTime);

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
        public string CheckDNGroupCodetoBackUPBySP(string strSID, string strDN, out string strGroupcode, out string RetMsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "indn", strDN };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "outgroupcode", "" };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_ZC2_CHECKDNSTATUS", procParams);
            //procedure SP_ZC2_CHECKDNSTATUS(insid  in varchar2,
            //                                       indn   in varchar2,
            //                                       outgroupcode out varchar2,
            //                                       RetMsg out varchar2) as
            strGroupcode = ds.Tables[0].Rows[0]["outgroupcode"].ToString();
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return RetMsg;
            }

        }
        public DataSet showNeedZCCartonListBySQL(string strGroupcode)
        {
            string sql = string.Format(@"
                             select *
                               from (select distinct b.pallet_no, b.delivery_no, c.carton_no
                                       from ppsuser.t_pallet_order b
                                       join ppsuser.t_sn_status c
                                         on b.shipment_id = c.shipment_id
                                        and b.pallet_no = c.pack_pallet_no
                                        and b.delivery_no = c.delivery_no
                                        and b.line_item = c.line_item
                                        and b.ictpn = c.part_no
                                      where (b.delivery_no, b.shipment_id) in
                                            (select a.delivery_no, a.shipment_id
                                               from ppsuser.t_zc_dn_info a
                                              where a.group_code = '{0}')
                                     union
                                     select distinct decode(dd.pack_pallet_no,
                                                            null,
                                                            cc.pallet_no,
                                                            '',
                                                            cc.pallet_no,
                                                            dd.pack_pallet_no) pallet_no,
                                                     aa.delivery_no,
                                                     bb.carton_no
                                       from ppsuser.t_trolley_sn_status aa
                                       join ppsuser.t_sn_status bb
                                         on aa.custom_sn = bb.customer_sn
                                       join ppsuser.T_PALLET_PICK cc
                                         on bb.pick_pallet_no = cc.pick_pallet_no
                                       left join ppsuser.t_sn_ppart dd
                                         on bb.carton_no = dd.carton_no
                                      where bb.wc = 'W1'
                                       and aa.delivery_no in( select distinct delivery_no
                                                       from ppsuser.t_zc_dn_info
                                                      where group_code = '{1}')
                                        and cc.pallet_no in
                                            (select pallet_no
                                               from ppsuser.t_shipment_pallet
                                              where shipment_id in
                                                    (select distinct shipment_id
                                                       from ppsuser.t_zc_dn_info
                                                      where group_code = '{1}'))) aaa
                              order by aaa.delivery_no asc, aaa.pallet_no asc, aaa.carton_no asc
                                                        ", strGroupcode, strGroupcode);

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
        public DataSet showZCOKCartonListBySQL(string strGroupcode)
        {
            string sql = string.Format(@"
                     select a.carton_no from ppsuser.T_ZC_CARTON_log a where a.group_code = '{0}'                   
                                                        ", strGroupcode);

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


        public DataSet showZCNoPpartPickCartonBySQL(string strGroupcode)
        {
            string sql = string.Format(@"
                      select aaa.pallet_no PALLET_NO2,
                             aaa.ictpn,
                             decode(bbb.okpickcarton, null, 0, bbb.okpickcarton)- aaa.otherneedPICKcarton ZCCartonCount
                        from (select b.pallet_no,
                                     b.ictpn,
                                     sum(b.assign_carton - b.pack_carton) otherneedPICKcarton
                                from ppsuser.t_pallet_order b
                               where (b.shipment_id, b.delivery_no, b.line_item, b.ictpn) in
                                     (select a.shipment_id, a.delivery_no, a.line_item, a.ictpn
                                        from ppsuser.t_order_info a
                                       where (a.shipment_id, a.delivery_no) not in
                                             (select shipment_id, delivery_no
                                                from ppsuser.t_zc_dn_info
                                               where group_code = '{0}')
                                         and a.shipment_id in
                                             (select shipment_id
                                                from ppsuser.t_zc_dn_info
                                               where group_code = '{1}')
                                         and a.person_flag = 'N')
                               group by b.pallet_no, b.ictpn) aaa
                        left join (select substr(aa.pick_pallet_no, 3) pallet_no,
                                          aa.part_no,
                                          count(distinct aa.carton_no) as okpickcarton
                                     from ppsuser.t_sn_status aa
                                     join ppsuser.t_pallet_pick bb
                                       on aa.part_no = bb.ictpn
                                      and aa.pick_pallet_no = bb.pick_pallet_no
                                     join ppsuser.t_shipment_pallet_part cc
                                       on bb.pallet_no = cc.pallet_no
                                      and bb.ictpn = cc.ictpn
                                     join ppsuser.t_shipment_pallet dd
                                       on cc.pallet_no = dd.pallet_no
                                    where aa.wc = 'W1'
                                      and dd.shipment_id in
                                          (select shipment_id
                                             from ppsuser.t_zc_dn_info
                                            where group_code = '{2}')
                                    group by substr(aa.pick_pallet_no, 3), aa.part_no) bbb
                          on aaa.pallet_no = bbb.pallet_no
                         and aaa.ictpn = bbb.part_no
                      where  aaa.otherneedPICKcarton <
                             decode(bbb.okpickcarton, null, 0, bbb.okpickcarton)                
                                                        ", strGroupcode, strGroupcode, strGroupcode);

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


        public DataSet GetZCSIDListBySQL(string strStartTime, string strEndTime)
        {
            string sql = string.Format(@"
                                       select a.shipment_id,
                                                a.status as OMS_STATUS,
                                                a.modifytime
                                           from pptest.oms_shipment_cancel a
                                          where a.modifytime >=
                                                to_date('{0}', 'yyyy-mm-dd')
                                            and a.modifytime <=
                                                to_date('{1}', 'yyyy-mm-dd')
                                            and status is not null
                                                        ", strStartTime, strEndTime);

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

        public DataTable getGlobalMSN(string shipmentID)
        {
            string sql = @"SELECT DISTINCT globalmsn from PPSUSER.T_UPS_RAWDATA
                                where tracking_no in
                                (SELECT distinct T.TRACKING_NO trackingNo
                                                          FROM ppsuser.t_allo_trackingno t
                                                         WHERE     t.shipment_id IN (SELECT shipment_id
                                                                   FROM ppsuser.t_shipment_info
                                                              WHERE  carrier_name LIKE '%UPS%'
                                                                    AND TYPE = 'PARCEL')
                                AND shipment_id = :shipment_id)";
            object[][] para = new object[1][];
            para[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipment_id", shipmentID };
            return ClientUtils.ExecuteSQL(sql, para).Tables[0];
        }

    }
}
