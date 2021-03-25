using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WMSTransIN
{
    class WMSTransINBLL
    {

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

        public DataTable GetSNInfoDataTable(string sn,string sntype)
        {
            if (string.IsNullOrEmpty(sn)) { return null; }
            WMSTransINDAL wd = new WMSTransINDAL();
            DataSet dataSet = wd.GetSNInfoDataTable(sn, sntype);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public string ExecuteWMSTransIN(string strsn, string strLocationid, string sntype, out string errmsg)
        {

            errmsg = string.Empty;
            WMSTransINDAL wd = new WMSTransINDAL();
            string strResult = wd.WMSTransINBySP(strsn, strLocationid, sntype, out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }
    }
}
