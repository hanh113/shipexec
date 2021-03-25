using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace UpLoad856
{
    class UploadDAL
    {
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


        public string PPSInsertWebServieByProcedure(string insn, string serverip, string url, string result, out string errmsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strserverip", serverip };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strurl", url };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strresult", result };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_UPLOAD_INSERTWEBSERVICELOG", procParams);
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

        public string PPSCheckWebServieByProcedure(string insn, out string tturl, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "tturl", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_UPLOAD_CHECKWEBSERVICELOG", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            tturl = ds.Tables[0].Rows[0]["tturl"].ToString();
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }
        public DataSet GetTodayCarNoListBySQL()
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                 select distinct car_no
                                  from pptest.oms_load_car a
                                 where a.expectedtime >= trunc(sysdate)
                                   and a.expectedtime < trunc(sysdate + 1)
                                     ");

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
        public DataSet GetTodayShipmentListByCar(string strCarNo)
        {
            string sql = string.Empty;


            sql = string.Format(@"
                                 SELECT DISTINCT a.shipment_id
                                 FROM PPTEST.OMS_LOAD_CAR a, ppsuser.t_shipment_info b
                                WHERE a.CAR_NO = '{0}'
                                 and a.expectedtime >= trunc(sysdate)
                                 and a.expectedtime < trunc(sysdate + 1) and a.SHIPMENT_ID = b.SHIPMENT_ID
                                AND b.STATUS <> 'UF' and b.shipping_time >= trunc(sysdate)
                                     ", strCarNo);

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

        public DataSet GetTodayCarNoSIDListBySQL(string strCarNO, string strSID)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                select a.shipment_id 集货单号,
                                            c.status,
                                            decode(c.status, 'UF', 'Y', 'N') uploadedi,
                                            upload_edi_time,
                                            decode(e.shipment_id, null, 'N', 'Y') uploadsap,
                                            upload_erp_time,
                                            c.shipping_time 出货时间,
                                            c.carrier_name carrier,
                                            c.poe,
                                            c.region 地区,
                                            a.pallet_no 栈板号,
                                            case
                                              when a.pallet_type = '001' then
                                               'NO MIX'
                                              when a.pallet_type = '999' then
                                               'MIX'
                                              else
                                               a.pallet_type
                                            end 栈板类型,
                                            b.ictpn 料号,
                                            b.qty 数量,
                                            b.carton_qty 箱数,
                                            b.pack_carton 已pack箱数,
                                            case
                                              when b.pack_status = 'WP' then
                                               'WP-未PACK'
                                              when b.pack_status = 'IP' then
                                               'IP-PACK中'
                                              when b.pack_status = 'FP' then
                                               'FP-已PACK'
                                              when b.pack_status = 'QH' then
                                               'QH-QHold'
                                              else
                                               b.pack_status
                                            end part_pack_status,
                                            upload_emp_no
                              from ppsuser.t_shipment_pallet a
                             inner join ppsuser.t_shipment_pallet_part b
                                on a.pallet_no = b.pallet_no
                             inner join ppsuser.t_shipment_info c
                                on a.shipment_id = c.shipment_id
                              left join (select *
                                           from ppsuser.t_upload_webservice
                                          where strresult is null) e
                                on a.shipment_id = e.shipment_id
                             where c.shipping_time >= trunc(sysdate)
                               and a.shipment_id in (select distinct d.shipment_id
                                                       from pptest.oms_load_car d
                                                      where d.expectedtime >= trunc(sysdate)
                                                        and d.expectedtime < trunc(sysdate + 1)
                                                        and d.car_no = '{0}')
                                     ", strCarNO);
            if (!string.IsNullOrEmpty(strSID) && !strSID.Equals("-ALL-"))
            {
                sql += string.Format("  and a.shipment_id ='{0}' ", strSID);
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


        public DataSet GetTodayCarNoSIDList2BySQL(string strCarNO)
        {
            string sql = string.Empty;

            sql = string.Format(@"
                                select distinct c.shipment_id 集货单号,
                                            c.status,
                                            decode(c.status, 'UF', 'Y', 'N') uploadedi,
                                            upload_edi_time,
                                            decode(e.shipment_id, null, 'N', 'Y') uploadsap,
                                            upload_erp_time
                              from  ppsuser.t_shipment_info c
                              left join (select *
                                           from ppsuser.t_upload_webservice
                                          where strresult is null) e
                                on c.shipment_id = e.shipment_id
                             where c.shipping_time >= trunc(sysdate)
                               and c.shipment_id in (select distinct d.shipment_id
                                                       from pptest.oms_load_car d
                                                      where d.expectedtime >= trunc(sysdate)
                                                        and d.expectedtime < trunc(sysdate + 1)
                                                        and d.car_no = '{0}')
                                     ", strCarNO);

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
        public string Insert856ByCar(string carNo, string strLocalMACADDRESS,out string errmsg1)
        {
            errmsg1 = "";
            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "p_car_no", carNo };
            procParams[1] = new object[] { ParameterDirection.Input,OracleDbType.Varchar2, "p_macAddress", strLocalMACADDRESS };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("PPSUSER.sp_upload_insert856_byCar", procParams).Tables[0];
            }
            //ppsuser.SP_UPLOAD_INSERT856ASN(shipmentid in varchar2,
            //                                           --checktype  in varchar2,
            //                                           errmsg out varchar2) as errmsg     out varchar2)

            catch (Exception e1)
            {
                //Show_Message(e1.ToString(), 0);
                return e1.ToString();
            }
            return dt.Rows[0]["errmsg"].ToString();

        }
    }
}
