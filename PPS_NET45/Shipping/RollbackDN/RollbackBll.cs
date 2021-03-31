using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RollbackDN
{
    class RollbackBll
    {
        /// <summary>
        /// 检查DN的状态
        /// </summary>
        /// <param name="DN"></param>
        /// <returns></returns>
        public string CheckDNStatus(string DN, out string errmsg)
        {

            errmsg = string.Empty;
            RollbackDal reservelDal = new RollbackDal();
            int rownum = reservelDal.CheckDNBySQL(DN);

            if (rownum == 0)
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                errmsg = "NG,未找到DN信息或不能ZC";
                return "NG";
            }
            errmsg = "OK,查询OK";
            return "OK";
        }


        public DataTable GetDNInfoDataTable(string dn)
        {
            if (string.IsNullOrEmpty(dn)) { return null; }
            RollbackDal DNDal = new RollbackDal();
            DataSet dataSet = DNDal.GetDNInfoDataTable(dn);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public DataTable GetPICKInfoDataTable(string dn)
        {
            if (string.IsNullOrEmpty(dn)) { return null; }
            RollbackDal DNDal = new RollbackDal();
            DataSet dataSet = DNDal.GetPICKInfoDataTable(dn);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetDN_ZCStatusDataTable(string dn)
        {
            if (string.IsNullOrEmpty(dn)) { return null; }
            RollbackDal DNDal = new RollbackDal();
            DataSet dataSet = DNDal.GetDN_ZCStatusDataTable(dn);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }




        /// <summary>
        /// 还原SHIPMENTID
        /// </summary>
        /// <param name="SHIPMENTID"></param>
        /// <returns></returns>
        public string RBShipmentID(string shipmentid, out string errmsg)
        {

            errmsg = string.Empty;
            RollbackDal rbShipmentid = new RollbackDal();
            string strRB = rbShipmentid.RBShipmentIDByProcedure(shipmentid, out errmsg);

            if (strRB.Equals("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK,查询OK";
            return "OK";
        }
        public string RBShipmentID2(string shipmentid, out string errmsg)
        {

            errmsg = string.Empty;
            RollbackDal rbShipmentid = new RollbackDal();
            string strRB = rbShipmentid.RBShipmentIDByProcedure2(shipmentid, out errmsg);

            if (strRB.Equals("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK,查询OK";
            return "OK";
        }

        public string RBSNbyDN(string shipmentid, string strGroupcode, out string errmsg)
        {

            errmsg = string.Empty;
            RollbackDal rbsn = new RollbackDal();
            string strRBSN = rbsn.RBSNbyDNByProcedure(shipmentid, strGroupcode, out errmsg);

            if (strRBSN.Contains("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            return "OK";
        }


        public string CheckDNtoShipmentID(string shipmentid, out string dntype, out string errmsg)
        {

            errmsg = string.Empty;
            dntype = string.Empty;
            RollbackDal rbdn = new RollbackDal();
            string strRB = rbdn.CheckDNtoShipmentIDByProcedure(shipmentid, out dntype, out errmsg);

            if (strRB.Contains("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK,查询OK";
            return "OK";
        }


        public string LockDNtoShipmentID(string shipmentid, out string errmsg)
        {

            errmsg = string.Empty;
            RollbackDal rbdn = new RollbackDal();
            string strRB = rbdn.LockDNtoShipmentIDBySP(shipmentid, out errmsg);

            if (strRB.Contains("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK,锁定OK";
            return "OK";
        }

        public string GetDBType(string inparatype, out string outparavalue, out string errmsg)
        {

            errmsg = string.Empty;
            RollbackDal pl = new RollbackDal();
            string strRB = pl.GetDBTypeBySP(inparatype, out outparavalue, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }

        public string DelPrefixCartonSN(string insn)
        {
            if (insn.Length == 20 && insn.Substring(0, 2).Equals("00"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("3S"))
            { insn = insn.Substring(2); }
            else if (insn.StartsWith("S"))
            { insn = insn.Substring(1); }

            return insn;

        }



        public DataTable GetZCDNListDataTable(string strStartTime, string strEndTime)
        {
            RollbackDal DNDal = new RollbackDal();
            DataSet dataSet = DNDal.GetZCDNListBySQL(strStartTime, strEndTime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public DataTable GetZCSIDListDataTable(string strStartTime, string strEndTime)
        {
            RollbackDal DNDal = new RollbackDal();
            DataSet dataSet = DNDal.GetZCSIDListBySQL(strStartTime, strEndTime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public string CheckDNGroupCodetoBackUP(string strSID, string strDN, out string strGroupcode, out string RetMsg)
        {

            //RetMsg = string.Empty;
            RollbackDal rb = new RollbackDal();
            string strRBSN = rb.CheckDNGroupCodetoBackUPBySP(strSID, strDN, out strGroupcode, out RetMsg);

            if (strRBSN.Contains("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            return "OK";
        }
        //showNeedZCCartonList
        public DataTable showNeedZCCartonListDataTable(string strGroupcode)
        {
            RollbackDal DNDal = new RollbackDal();
            DataSet dataSet = DNDal.showNeedZCCartonListBySQL(strGroupcode);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        //showZCOKCartonListDataTable
        public DataTable showZCOKCartonListDataTable(string strGroupcode)
        {
            RollbackDal DNDal = new RollbackDal();
            DataSet dataSet = DNDal.showZCOKCartonListBySQL(strGroupcode);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public DataTable showZCNoPpartPickCartonDataTable(string strGroupcode)
        {
            RollbackDal DNDal = new RollbackDal();
            DataSet dataSet = DNDal.showZCNoPpartPickCartonBySQL(strGroupcode);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public List<int> GetListGlobalMSN(string shipmentID)
        {
            var lstGlbMSN = new List<int>();
            RollbackDal DNDal = new RollbackDal();
            var dt = DNDal.getGlobalMSN(shipmentID);
            if (dt.Rows.Count > 0)
            {
                lstGlbMSN = dt.AsEnumerable().Select(x => int.Parse(x["globalmsn"].ToString())).ToList();
            }
            return lstGlbMSN;
        }
    }
}
