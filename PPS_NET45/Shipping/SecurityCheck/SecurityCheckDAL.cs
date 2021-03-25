using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecurityCheck
{
    class SecurityCheckDAL
    {
        public DataSet GetCarInfoDataTablebySQL(string starttime, string endtime, string truckno)
        {
            string sql = string.Empty;
            sql = string.Format("select a.shipment_id 集货单, "
                         + "          d.shipping_time 出货时间, "
                         + "          b.pallet_no 栈板号, "
                         + "          e.ictpn 料号, "
                         + "          e.carton_qty 箱数, "
                         + "          e.qty 数量, "
                         + "          b.car_no 车牌号, "
                         + "          decode(b.isload, 1, 'Y', 'N') 装车状态, "
                         + "          c.whconfirm_name 仓库确认人, "
                         + "          c.whconfirm_time 仓库确认时间, "
                         + "          c.securityconfirm_name 安保确认人, "
                         + "          c.securityconfirm_time 安保确认时间 "
                         + "     FROM ppsuser.t_shipment_pallet a "
                         + "     join pptest.oms_load_car b "
                         + "       on a.shipment_id = b.shipment_id "
                         + "      and a.pallet_no = b.pallet_no "
                         + "      and(b.active = 0 or b.active is null) "
                         + "     join ppsuser.t_shipment_pallet_part e "
                         + "      on a.pallet_no = e.pallet_no "
                         + "     join ppsuser.t_shipment_info d  "
                         + "       on a.shipment_id = d.shipment_id "
                         + "     left join ppsuser.t_truck_confirm_log c "
                         + "       on a.shipment_id = c.shipment_id "
                         + "      and a.pallet_no = c.pallet_no "
                         + "      and c.car_no = b.car_no "
                         + "    where (to_date(a.cdt) >= to_date('{0}', 'YYYY-MM-DD')  "
                         + "      and  to_date(a.cdt) <= to_date('{1}', 'YYYY-MM-DD')) "
                         + "      and b.car_no = '{2}' "
                         + "      order by  d.shipping_time  desc ,b.pallet_no  asc ,e.ictpn  asc ", starttime, endtime, truckno);

            DataSet dataSet = new DataSet();
            try
            {
                dataSet = ClientUtils.ExecuteSQL(sql);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
            return dataSet;
        }

        public string getPalletsQtyByTruckByProcedure(string starttime, string endtime, string truckno, out string allreturnlist, out string errmsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "starttime", starttime };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endtime", endtime };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "truckno", truckno };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "allreturnlist", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_SHIPMENT_GETTRUCKINFO", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            allreturnlist = ds.Tables[0].Rows[0]["allreturnlist"].ToString();
            //return errmsg;
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }


        public string TruckConfirmByProcedure(string starttime, string endtime, string truckno, string whoconfirm, string passw, out string errmsg)
        {
            object[][] procParams = new object[6][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "starttime", starttime };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "endtime", endtime };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "truckno", truckno };
            procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "whoconfirm", whoconfirm };
            procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "passw", passw };
            procParams[5] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "errmsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_SHIPMENT_TRUCKCONFIRM", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            //return errmsg;
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
