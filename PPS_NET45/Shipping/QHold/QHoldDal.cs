using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;

namespace QHold
{
    class QHoldDal
    {
        public DataSet GetHoldInfoDataTable(string sid, string strDBtype)
        {
            string sql = string.Empty;
            if (strDBtype.Equals("TEST"))
            {
                sql = string.Format("   select AA.shipment_id,                             "
                                    + "        AA.pallet_no,                               "
                                    + "        AA.ictpn,                                   "
                                    + "        count(AA.carton_no) HOLD箱数                "
                                    + "   from (select distinct a.shipment_id,             "
                                    + "                         a.pallet_no,               "
                                    + "                         b.ictpn,                   "
                                    + "                         c.carton_no,               "
                                    + "                         d.hold_flag                "
                                    + "           from ppsuser.t_shipment_pallet a         "
                                    + "           join ppsuser.t_pallet_pick b             "
                                    + "             on a.pallet_no = b.pallet_no           "
                                    + "           join ppsuser.t_sn_status c               "
                                    + "             on b.pick_pallet_no = c.pick_pallet_no "
                                    + "            and b.ictpn = c.part_no                 "
                                    + "           join ppsuser.g_sn_status d               "
                                    + "             on c.serial_number = d.serial_number       "
                                    + "          where a.shipment_id ='{0}'  "
                                    + "            and d.hold_flag = 'Y') AA               "
                                    + "  group by AA.shipment_id, AA.pallet_no, AA.ictpn   ", sid);
            }
            else
            {
                sql = string.Format("   select AA.shipment_id,                             "
                                    + "        AA.pallet_no,                               "
                                    + "        AA.ictpn,                                   "
                                    + "        count(AA.carton_no) HOLD箱数                "
                                    + "   from (select distinct a.shipment_id,             "
                                    + "                         a.pallet_no,               "
                                    + "                         b.ictpn,                   "
                                    + "                         c.carton_no,               "
                                    + "                         d.hold_flag                "
                                    + "           from ppsuser.t_shipment_pallet a         "
                                    + "           join ppsuser.t_pallet_pick b             "
                                    + "             on a.pallet_no = b.pallet_no           "
                                    + "           join ppsuser.t_sn_status c               "
                                    + "             on b.pick_pallet_no = c.pick_pallet_no "
                                    + "            and b.ictpn = c.part_no                 "
                                    + "           join sajet.g_sn_status d               "
                                    + "             on to_char(c.serial_number) = d.serial_number       "
                                    + "          where a.shipment_id ='{0}'  "
                                    + "            and d.hold_flag = 'Y') AA               "
                                    + "  group by AA.shipment_id, AA.pallet_no, AA.ictpn   ", sid);
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

        //检查ShipmentID和箱号的关系
        public string CheckHoldCartonByProcedure(string shipmentid, string cartonno, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentid };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "cartonno", cartonno };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            //ppsuser.SP_HOLD_CHECKHOLDCARTON(shipmentid in varchar2,cartonno in varchar2,
            //                                                 RetMsg   out varchar2) as
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_HOLD_CHECKHOLDCARTON", procParams);
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


        public string ReplaceHoldCartonByProcedure(string shipmentid, string newcartonno, string oldsnorcarton, out string strlocationinfo, out string RetMsg)
        {
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentid };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "newcartonno", newcartonno };
            procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "oldsnorcarton", oldsnorcarton };
            procParams[3] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "newlocationname", "" };
            procParams[4] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_HOLD_REPLACECARTON", procParams);
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            //返回值是 储位|当前箱数
            strlocationinfo = ds.Tables[0].Rows[0]["newlocationname"].ToString();
            if (RetMsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                return "NG";
            }

        }


        public string ZCHoldPalletByProcedure(string shipmentid, string holdpallet, out string RetMsg)
        {
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "shipmentid", shipmentid };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "holdpallet", holdpallet };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_HOLD_ZCHOLDPALLET", procParams);
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

        public string TOHoldCartonByProcedure(string cartonno, Int16 holdcount, out string RetMsg)
        {
            //Double holdcount1 = Convert.ToDouble(holdcount);
            object[][] procParams = new object[3][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "cartonno", cartonno };
            procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Int32, "holdcount", holdcount };
            procParams[2] = new object[] { ParameterDirection.Output, OracleDbType.Varchar2, "RetMsg", "" };

            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_HOLD_TRANSHOLD", procParams);
            RetMsg = ds.Tables[0].Rows[0]["RetMsg"].ToString();
            if (RetMsg.Contains("OK"))
            {
                return RetMsg;
            }
            else
            {
                return RetMsg;
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
