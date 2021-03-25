using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace AutoAssignSID
{
    class AutoAssignSIDDLL
    {

        public DataSet GetSIDListBySQL(string strStartTime, string strEndTime)
        {
            string sql = string.Format(@"
                                        select a.shipment_id,
                                                a.shipment_type   FD_DS,
                                                a.type,
                                                a.region,
                                                a.carrier_name    CNAME,
                                                a.carton_qty,
                                                a.pack_carton_qty PACK_CARTON,
                                                a.priority
                                           from ppsuser.t_shipment_info a
                                          where a.status not in ('CP', 'UF', 'WS', 'IN', 'SF', 'LF')
                                            and a.pack_carton_qty = 0
                                            and a.shipping_time >= to_date('{0}', 'yyyy-mm-dd')
                                            and a.shipping_time < to_date('{1}', 'yyyy-mm-dd')
                                            order by a.shipment_type  asc,
                                                     a.type desc,
                                                     a.priority asc,
                                                     a.region asc   
                                                        ", strStartTime, strEndTime);

            DataSet dataSet = new DataSet();
            try
            {
                //
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }



        public DataSet GetLineListBySQL()
        {
            string sql = @"select a.linename,
                                a.isok,
                                a.shipmenttype    TYPE,
                                a.supportcarriers CNAME,
                                a.region,
                                a.uph
                            from ppsuser.t_line_info a";

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

        public string AutoAssignSIDByProcedure(string shipdate, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipdate", shipdate };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_AUTOASSIGNSID", procParams);
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

        public DataSet GetAssignLineListBySQL()
        {
            string sql = @"
                        select a.linename,
                               a.isok,
                               a.shipmenttype    TYPE,
                               a.supportcarriers CNAME,
                               a.region,
                               a.uph,
                               b.shipment_id,
                               c.shipment_type   FD_DS,
                               c.type TYPE1,
                               c.region REGION1,
                               c.carrier_name    CNAME1,
                               c.carton_qty,
                               c.pack_carton_qty PACK_CARTON,
                               c.priority
                          from ppsuser.t_line_info a
                          left join ppsuser.t_line_assign b
                            on a.linename = b.linename
                          left join ppsuser.t_shipment_info c
                            on b.shipment_id = c.shipment_id
                         where a.isok = 'Y'
                         order by c.priority asc nulls last
                        ";

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
        public DataSet GetAssignLineListBySQL2()
        {
            string sql = @"
                         select distinct a.linename,
                             a.isok,
                             a.shipmenttype TYPE,
                             a.supportcarriers CNAME,
                             a.region,
                             a.uph,
                             b.shipment_id,
                             c.shipment_type FD_DS,
                             c.type TYPE1,
                             c.region REGION1,
                             c.carrier_name CNAME1,
                             c.carton_qty,
                             c.pack_carton_qty PACK_CARTON,
                             c.priority,
                             c.poe,
                             c.shipping_time,
                             d.pallet_no,
                             d.carton_qty,
                             decode(d.pallet_type, '001', 'NO_MIX', 'MIX') PALLETTYPE,
                             e.mpn,
                             e.ictpn,
                             e.assign_carton,
                             f.remark
               from ppsuser.t_line_info a
               join ppsuser.t_line_assign b
                 on a.linename = b.linename
               join ppsuser.t_shipment_info c
                 on b.shipment_id = c.shipment_id
               join ppsuser.t_shipment_pallet d
                 on c.shipment_id = d.shipment_id
               join (select tpo.shipment_id,
                           tpo.pallet_no,
                           tpo.mpn,
                           tpo.ictpn,
                           sum(assign_carton) as assign_carton
                      from ppsuser.t_pallet_order tpo
                     group by tpo.shipment_id, tpo.pallet_no, tpo.mpn, tpo.ictpn) e
                 on d.pallet_no = e.pallet_no
               join (select g.packcode, min(g.remark) remark
                       from (select distinct packcode,
                                             PALLETLENGTHCM || '*' || PALLETWIDTHCM as remark
                               from ppsuser.vw_mpn_info) g
                      group by packcode) f
                 on d.pack_code = f.packcode
              where a.isok = 'Y'
              order by c.priority     asc nulls last,
                       a.shipmenttype desc,
                       a.linename     asc,
                       b.shipment_id  asc,
                       d.pallet_no    asc,
                       e.mpn          asc
                        ";

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

        public string UpdateLineSIDByProcedure(string strLine,string strSID,string strNewSID,string shipdate, out string RetMsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inline", strLine };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "innewsid", strNewSID };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipdate", shipdate };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_PPS_UPDATEASSIGNLINESID", procParams);
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
    }
}

