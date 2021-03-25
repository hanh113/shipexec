using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPIPickListAC
{
    class NPIPickListACDAL
    {

        public DataSet GetNPISIDDataTableBySQL( string strSID, string strSTATUS, string strStime, string strEtime)
        {
            string sql = string.Empty;
            object[][] sqlparams = new object[0][];
            int iPara = 0;
            sql = @"select a.shipment_id,
                           a.shipping_time,
                           a.hawb,
                           a.qty,
                           a.carton_qty,
                           a.status,
                           a.close_time,
                           a.container as computer_name
                      from nonedipps.t_shipment_info a
                         where 1=1 ";
                //SAP出货单号查询条件
                if (strSID != "" && strSID != "ALL")
                {
                    Array.Resize(ref sqlparams, sqlparams.Length + 1);
                    sql += " and a.lddnum = :strSID";
                    sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "strSID", strSID };
                    iPara = iPara + 1;
                }
            
           
            if (strSTATUS != "" && strSTATUS != "ALL")
            {

                Array.Resize(ref sqlparams, sqlparams.Length + 1);
                sql += " and a.status = :status";
                sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "status", strSTATUS };
                iPara = iPara + 1;
            }
            //开始日期
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.shipping_time >= to_date(:starttime ,'yyyy-mm-dd')";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "starttime", strStime };
            iPara = iPara + 1;

            //结束日期
            Array.Resize(ref sqlparams, sqlparams.Length + 1);
            sql += " and a.shipping_time <= to_date(:endtime ,'yyyy-mm-dd')";
            sqlparams[iPara] = new object[] { ParameterDirection.Input, OracleDbType.TimeStamp, "endtime", strEtime };
            iPara = iPara + 1;

            sql += " order by a.shipping_time asc";


            DataSet dataSet = new DataSet();
            try
            {
                //dataSet = ClientUtils.ExecuteSQL(sql);
                dataSet = ClientUtils.ExecuteSQL(sql, sqlparams);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;

        }
        public string NPICheckSIDBySP(string strSID,  string localHostname, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "computername", localHostname };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("nonedipps.SP_NPIPICK_CHECKSIDSTATUS", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                return "NG";
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else if (RetMsg.Contains("WA"))
            {
                return "WA";
            }
            else
            {
                return "NG";
            }

        }

        public DataSet GetSIDLINEINFOBySQL(string strSID)
        {
            string sql = string.Empty;


            sql = string.Format(@"select a.shipment_id,
                                         a.delivery_no,
                                         a.line_item,
                                         a.mpn,
                                         a.ictpn,
                                         a.status,
                                         a.qty,
                                         a.carton_qty,
                                         a.pack_qty,
                                         a.pack_carton_qty
                                    from nonedipps.t_order_info a
                                    where a.shipment_id='{0}'
                                    order by a.delivery_no asc ,a.line_item asc", strSID);

            
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
                                          + "    from nonedipps.t_sn_status "
                                          + "   where customer_sn = '{0}' "
                                          + "      or carton_no = '{1}'", inputSno, inputSno);

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

        public string NPIPICKInsertCartonBySP(string strPickNOA, out string strPickNO, string strSID, string strCarton, string strUserNo, out string RetMsg, out string strLBL)
        {
            object[][] procParams = new object[7][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inpickno", strPickNOA };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "pickno", "" };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "snOrCartonno", strCarton };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "empno", strUserNo };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            procParams[6] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "strlbl", "" };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("nonedipps.sp_npipick_insertcarton", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                strLBL = "00/00";
                strPickNO = "";
                return "NG";
            }
            RetMsg = dt.Rows[0]["errmsg"].ToString();
            strLBL = dt.Rows[0]["strlbl"].ToString();
            strPickNO = dt.Rows[0]["pickno"].ToString();

            if (RetMsg.StartsWith("OK"))
            {
                return "OK";
            }
            else if (RetMsg.StartsWith("FINISH"))
            {
                return "FINISH";
            }
            else
            {
                return "NG";
            }

        }

        public string NPIPICKUnlockComputerBySP(string strSID, out string RetMsg)
        {
            object[][] procParams = new object[2][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insid", strSID };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("NONEDIPPS.SP_NPIPICK_UNLOCKCOMPUTERNAME", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                RetMsg = e1.ToString();
                return "NG";
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }

        public DataSet GetStockInfoBySQL(string strPartNo)
        {
            string sql = string.Empty;
            sql = string.Format(@"Select b.part_no 料号,
                                   b.location_no 库位,
                                   '' 车行号,
                                   b.CARTONQTY - b.QHCARTONQTY 箱数
                              from nonedipps.t_location b
                             where b.qty > 0
                               and b.
                             cartonqty > 0
                               and (b.part_no = '{0}' or
                                   b.part_no in
                                   (select distinct a.part_no
                                       from ppsuser.t_sn_status a
                                      where a.customer_sn = '{0}'
                                         or a.carton_no = '{0}'))
                             order by b.Udt asc
                    ", strPartNo);

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

        public string PPSCheckWebServieByProcedure(string insn, out string tturl, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "tturl", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_UPLOAD_CHECKWEBSERVICELOG", procParams);
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
        public string NPIPPSCheckWebServieByProcedure(string insn, out string tturl, out string errmsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", insn };
            procParams[1] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "tturl", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("NONEDIPPS.SP_UPLOAD_CHECKACWEBSERVICELOG", procParams);
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

        public string PPSGetbasicparameterBySP(string strParaType, out string strParaValue, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            string errormsg = "";
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inparatype", strParaType };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "outparavalue", "" };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", errormsg };
            DataTable dt = new DataTable();
            try
            {
                dt = ClientUtils.ExecuteProc("ppsuser.sp_pps_getbasicparameter", procParams).Tables[0];
            }
            catch (Exception e1)
            {
                strParaValue = "";
                RetMsg = e1.ToString();
                return "NG";
            }

            RetMsg = dt.Rows[0]["errmsg"].ToString();
            strParaValue = dt.Rows[0]["outparavalue"].ToString();
            if (RetMsg.Equals("OK"))
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

            DataSet ds = ClientUtils.ExecuteProc("nonedipps.SP_UPLOAD_INSERTACWEBLOG", procParams);
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

        public DataSet GetPalletPrintInfoBySQL(string strPartNo)
        {
            string sql = string.Empty;
            sql = string.Format(@" select b.shipment_id shipmentid,
                                            b.hawb hawb,
                                            b.region region,
                                            a.pick_pallet_no palletno,
                                            a.pallet_number palletseq,
                                            sum(a.qty) snqty,
                                            sum(a.carton) cartonqty
                                       from nonedipps.t_pallet_pick a
                                       join nonedipps.t_shipment_info b
                                         on a.pallet_no = b.shipment_id
                                      where a.pick_pallet_no = '{0}'
                                      group by b.shipment_id,
                                               b.hawb,
                                               b.region,
                                               a.pick_pallet_no,
                                               a.pallet_number
                                                        ", strPartNo);

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

    }
}
