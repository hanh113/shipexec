using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace wmsReportAC
{
    class WMSDLL
    {
        public DataSet GetStockInfoDataTable(string carlineno, string incsn = "")
        {
            string sql = string.Empty;
            //虚拟储位加载太多资料,导致产线作业比较卡 Modified By KyLinQiu 20190808
            if (carlineno == "ICT-00-00-SYS-SYS")
            {
                sql = string.Format(@"select aa.location_no,
                               aa.custom_sn,
                               aa.trolley_line_no,
                               aa.pointno,
                               aa.group_code,
                               aa.delivery_no,
                               aa.line_item,
                               LISTAGG(aa.shipment_id, ',') WITHIN GROUP(ORDER BY aa.shipment_id) as shipment_id
                          from (select distinct d.location_no,
                                                a.custom_sn,
                                                e.trolley_line_no,
                                                a.pointno,
                                                a.group_code,
                                                b.delivery_no,
                                                b.line_item,
                                                c.shipment_id
                                  from NONEDIPPS.t_trolley_sn_status a
                                  join NONEDIPPS.vw_person_log b
                                    on a.custom_sn = b.customer_sn
                                  left join NONEDIPPS.t_order_info c
                                    on b.delivery_no = c.delivery_no
                                   and b.line_item = c.line_item
                                   and b.PART_NO = c.ictpn
                                  join NONEDIPPS.t_sn_status d
                                    on a.custom_sn = d.customer_sn
                                  join NONEDIPPS.t_trolley_line_info e
                                    on a.trolley_no = e.trolley_no
                                   and a.sides_no = e.sides_no
                                   and a.level_no = e.level_no
                                   and a.seq_no = e.seq_no
                                 where e.trolley_line_no = '{0}'
                                   and e.trolley_line_no <> 'ICT-00-00-000-0000'
                                   AND (A.CUSTOM_SN='{1}' OR A.CARTON_NO='{1}')
                                   and (c.shipment_id not in
                                       (select shipment_id from NONEDIPPS.t_shipment_sawb) or
                                       c.shipment_id is null)) aa
                         group by aa.location_no,
                                  aa.custom_sn,
                                  aa.trolley_line_no,
                                  aa.pointno,
                                  aa.group_code,
                                  aa.delivery_no,
                                  aa.line_item
                         order by aa.pointno asc", carlineno, incsn);
            }
            else
            {
                sql = string.Format(@"select aa.location_no,
                               aa.custom_sn,
                               aa.trolley_line_no,
                               aa.pointno,
                               aa.group_code,
                               aa.delivery_no,
                               aa.line_item,
                               LISTAGG(aa.shipment_id, ',') WITHIN GROUP(ORDER BY aa.shipment_id) as shipment_id
                          from (select distinct d.location_no,
                                                a.custom_sn,
                                                e.trolley_line_no,
                                                a.pointno,
                                                a.group_code,
                                                b.delivery_no,
                                                b.line_item,
                                                c.shipment_id
                                  from NONEDIPPS.t_trolley_sn_status a
                                  join NONEDIPPS.vw_person_log b
                                    on a.custom_sn = b.customer_sn
                                  left join NONEDIPPS.t_order_info c
                                    on b.delivery_no = c.delivery_no
                                   and b.line_item = c.line_item
                                   and b.PART_NO = c.ictpn
                                  join NONEDIPPS.t_sn_status d
                                    on a.custom_sn = d.customer_sn
                                  join NONEDIPPS.t_trolley_line_info e
                                    on a.trolley_no = e.trolley_no
                                   and a.sides_no = e.sides_no
                                   and a.level_no = e.level_no
                                   and a.seq_no = e.seq_no
                                 where e.trolley_line_no = '{0}'
                                   and e.trolley_line_no <> 'ICT-00-00-000-0000'
                                   and (c.shipment_id not in
                                       (select shipment_id from NONEDIPPS.t_shipment_sawb) or
                                       c.shipment_id is null)) aa
                         group by aa.location_no,
                                  aa.custom_sn,
                                  aa.trolley_line_no,
                                  aa.pointno,
                                  aa.group_code,
                                  aa.delivery_no,
                                  aa.line_item
                         order by aa.pointno asc", carlineno);
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

        public string WMSPpartCheckByProcedure(string incarlineno, string incsn, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlineno", incarlineno };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_WMS_PPARTCHECK", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;
            //if (errmsg.Equals("OK"))
            //{
            //    return "OK";
            //}
            //else
            //{
            //    return "NG";
            //}

        }

        public string WMSPpartTransByProcedure(string incarlinenofrom, string incarlinenoto, string incsn, out string errmsg)
        {
            object[][] procParams = new object[4][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlinenofrom", incarlinenofrom };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incarlinenoto", incarlinenoto };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_WMS_PPARTTRANS", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;
        }


        public string GetCarlinenoByCSNByProcedure(string incsn, out string carlineno, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incsn", incsn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "carlineno", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_WMS_GETCARLINENOBYCSN", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            carlineno = ds.Tables[0].Rows[0]["carlineno"].ToString();
            return errmsg;
        }
        //P_PROPOSAL_TROLLEY(vCartonNo        in varchar2, vTROLLEY_LINE_NO out varchar2)

        public void GetCarlinenoByAdviseByProcedure(string incsn, out string carlineno)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "vCartonNo", incsn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "vTROLLEY_LINE_NO", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.P_PROPOSAL_TROLLEY", procParams);
            carlineno = ds.Tables[0].Rows[0]["vTROLLEY_LINE_NO"].ToString();

        }

        public string WMSTrolleyMoceByProcedure(string incar, string inlocationto, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "incar", incar };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationto", inlocationto };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_WMS_MOVETROLLEY", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            return errmsg;

        }
        public DataSet GetCarInfoDataTable(string strcar)
        {
            string sql = string.Empty;
            sql = string.Format(@"
                                select a.trolley_no,
                                       a.trolley_line_no,
                                       a.pointno,
                                       a.pallet_no,
                                       a.carton_no,
                                       a.custom_sn,
                                       a.delivery_no,
                                       a.line_item
                                  from NONEDIPPS.t_trolley_sn_status a
                                 where a.trolley_no = '{0}'
                                 order by a.trolley_line_no asc, a.pointno asc
                            ", strcar);
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

        public string AppleCareOut(string strCarton)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varPalletNo", "NA" };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "varCartonNo", strCarton };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Int32, "varEMPid", 10086 };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Int32, "varRetCode", 0 };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "varRetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.T_WMS_OUT_BYCARTON", procParams);
            if (ds.Tables[0].Rows[0]["varRetCode"].ToString().Trim() == "0")
            {
                return "OK";
            }
            else
            {
                return "NG:" + ds.Tables[0].Rows[0]["varRetMsg"].ToString().Trim();
            }
        }

        public string AppleCareZC(string strShipmentID)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", strShipmentID };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_ZC_ROLLBACKSHIPMENTID", procParams);
            string strRetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (strRetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG:" + strRetMsg;
            }
        }

        public DataSet GetZCSIDListBySQL(string strStartTime, string strEndTime)
        {
            string sql = string.Format(@"
                                       select a.shipment_id,
                                                a.status as OMS_STATUS,
                                                a.modifytime
                                           from NONEDIOMS.oms_shipment_cancel a
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

        public string RBShipmentIDByProcedure2(string shipmentid, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "InputSno", shipmentid };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_ZC2_ROLLBACKSHIPMENTID", procParams);
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
    }
}
