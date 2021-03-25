using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace PickListAC
{
    class PickListDal
    {
      
        public DataSet GetStockCarInfoDataTable(string ictpartno, string palletno)
        {
            //是否为Person
            Int32 intPersonDNCount = 0;
            //string strsql = string.Empty;
            //strsql = string.Format("select count (delivery_no) as PPARTCOUNT "
            //                       + "   from NONEDIPPS.t_order_info "
            //                       + "  where (delivery_no, line_item, ictpn) in "
            //                       + "        (select delivery_no, line_item, ictpn "
            //                       + "           from NONEDIPPS.T_PALLET_ORDER "
            //                       + "          where pallet_no = '{0}' "
            //                       + "            and (ictpn = '{1}' or  "
            //                        + "           ictpn in (select distinct a.part_no "
            //                        + "                          from NONEDIPPS.t_sn_status a "
            //                        + "                         where a.customer_sn = '{2}' "
            //                        + "                            or a.carton_no = '{3}' "
            //                        + "                            or a.pallet_no = '{4}'))) "
            //                       + "            and person_flag = 'Y'", palletno, ictpartno, ictpartno, ictpartno, ictpartno);

            //try
            //{
            //    DataTable db = ClientUtils.ExecuteSQL(strsql).Tables[0];
            //    if (db.Rows.Count > 0)
            //    {
            //        intPersonDNCount = Convert.ToInt32(db.Rows[0]["PPARTCOUNT"].ToString());
            //    }
            //}
            //catch (Exception e)
            //{
            //    System.Windows.Forms.MessageBox.Show(e.ToString());
            //}

            string sql = string.Empty;
            if (intPersonDNCount == 0)
            {
                sql = string.Format("Select b.part_no 料号, b.location_no 库位,'' 车行号, b.CARTONQTY - b.QHCARTONQTY 箱数 "
                                    + "      from NONEDIPPS.T_LOCATION b "
                                    + "     where b.qty > 0 "
                                    + "       and b. cartonqty > 0 "
                                    + "       and(b.part_no = '{0}' or "
                                    + "           b.part_no in (select distinct a.part_no "
                                    + "                          from NONEDIPPS.t_sn_status a "
                                    + "                         where a.customer_sn = '{1}' "
                                    + "                            or a.carton_no = '{2}' "
                                    + "                            or a.pallet_no = '{3}')) "
                                    + "     order by b.Udt asc", ictpartno, ictpartno, ictpartno, ictpartno);
            }
            //else
            //{
            //    sql = string.Format(@"  select aa.ictpartno 料号,
            //                                 aa.location_no 库位,
            //                                 aa.trolley_line_no || '(' || aa.pointno || ')' 车行号,
            //                                 aa.csnqty 箱数
            //                            from (select a.ictpartno,
            //                                         e.location_no,
            //                                         b.trolley_line_no,
            //                                         count(distinct a.custom_sn) csnqty,
            //                                         LISTAGG(decode(a.pointno, 0, null, a.pointno), ',') WITHIN GROUP(ORDER BY a.pointno) as pointno
            //                                    from NONEDIPPS.t_trolley_sn_status a
            //                                    join NONEDIPPS.t_trolley_line_info b
            //                                      on a.trolley_no = b.trolley_no
            //                                     and a.sides_no = b.sides_no
            //                                     and a.level_no = b.level_no
            //                                     and a.seq_no = b.seq_no
            //                                    left join NONEDIPPS.T_LOCATION_trolley d
            //                                      on a.trolley_no = d.trolley_no
            //                                    join (select location_id, max(location_no) as location_no
            //                                           from NONEDIPPS.T_LOCATION
            //                                          group by location_id) e
            //                                      on d.location_id = e.location_id
            //                                   where (a.delivery_no, a.line_item, a.ictpartno) in
            //                                         (select delivery_no, line_item, ictpn
            //                                            from NONEDIPPS.T_PALLET_ORDER d
            //                                           where pallet_no = '{0}'
            //                                             and (ictpn = '{1}' or
            //                                                 ictpn in (select distinct a.part_no
            //                                                              from NONEDIPPS.t_sn_status a
            //                                                             where a.customer_sn = '{2}'
            //                                                                or a.carton_no = '{3}'
            //                                                                or a.pallet_no = '{4}')))
            //                                     and a.trolley_no <> 'ICT-00-00-000'
            //                                     and a.carton_no not in(
            //                                       select carton_no from NONEDIPPS.t_sn_ppart 
            //                                       where pack_pallet_no <>'{5}')
            //                                   group by a.ictpartno, e.location_no, b.trolley_line_no) aa",
            //                                  palletno, ictpartno, ictpartno, ictpartno, ictpartno, palletno);
            //}
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


        public DataSet GetSNInfoDataTableDAL(string inputSno)
        {
            string sql = string.Empty;
            sql = string.Format("select distinct customer_sn ,carton_no "
                                          + "    from NONEDIPPS.t_sn_status "
                                          + "   where customer_sn = '{0}' "
                                          + "      or carton_no = '{1}'or pallet_no = '{2}'", inputSno, inputSno, inputSno);

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
                                          + "    from NONEDIPPS.T_SHIPMENT_INFO "
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
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "inwc", inwc };
            procParams[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "macaddress", macaddress };
            procParams[3] = new object[] { ParameterDirection.Output, OracleType.VarChar, "errmsg", "" };
            
            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_PPS_INSERTWORKLOG", procParams);
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
               + " from NONEDIPPS.T_SHIPMENT_PALLET_PART "
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

    
        public string GetDBTypeBySP(string inparatype, out string outparavalue, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "inparatype", inparatype };
            procParams[1] = new object[] { ParameterDirection.Output, OracleType.VarChar, "outparavalue", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleType.VarChar, "errmsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_PPS_GETBASICPARAMETER", procParams);
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
    }
}
