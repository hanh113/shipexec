using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace WMSTransIN
{
    class WMSTransINDAL
    {
        public DataSet GetSNInfoDataTable(string sn, string sntype)
        {
            string sql = string.Empty;
            if (sntype.Equals("CARTON"))
            {
                sql = string.Format(@"
                                 select a.carton_no, a.customer_sn, a.custpart, a.isinware
                                   from ppsuser.t_other_locate_sn a
                                  where a.carton_no in (select distinct carton_no
                                                          from ppsuser.t_other_locate_sn
                                                         where customer_sn = '{0}'
                                                            or carton_no = '{1}')
                                     ", sn, sn);
            }
            else
            {

                sql = string.Format(@"
                    select a.palletno, a.carton_no, count(a.customer_sn) cartonqty
                       from ppsuser.t_other_locate_sn a
                      where a.palletno in
                            (select distinct palletno
                               from ppsuser.t_other_locate_sn
                              where palletno = '{0}'
                                 or carton_no = '{1}')
                        and a.palletno is not null
                      group by a.palletno, a.carton_no
                      order by count(a.customer_sn) asc
                    ", sn, sn);

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

        //异地资料入库
        public string WMSTransINBySP(string strsn, string strLocationId, string sntype, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "insn", strsn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "inlocationid", strLocationId };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };
            //create or replace procedure SP_WMS_TRANSIN(insn         in varchar2,
            //                               inlocationid in varchar2,
            //                               errmsg       out varchar2) as

            string strSPname = string.Empty;
            if (sntype.Equals("PALLET"))
            { strSPname = "PPSUSER.SP_WMS_TRANSIN"; }
            else
            { strSPname = "PPSUSER.SP_WMS_TRANSINCARTON"; }
            DataSet ds = ClientUtils.ExecuteProc(strSPname, procParams);
            RetMsg = ds.Tables[0].Rows[0]["errmsg"].ToString();

            if (RetMsg.StartsWith("OK"))
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
