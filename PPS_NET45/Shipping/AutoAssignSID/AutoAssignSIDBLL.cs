using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoAssignSID
{
    class AutoAssignSIDBLL
    {
        public DataTable GetSIDListDataTable(string strStartTime, string strEndTime)
        {
            AutoAssignSIDDLL aasd = new AutoAssignSIDDLL();
            DataSet dataSet = aasd.GetSIDListBySQL(strStartTime, strEndTime);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public DataTable GetLineListDataTable()
        {
            AutoAssignSIDDLL aasd = new AutoAssignSIDDLL();
            DataSet dataSet = aasd.GetLineListBySQL();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string AutoAssignSIDbydate(string shipdate, out string errmsg)
        {

            errmsg = string.Empty;
            AutoAssignSIDDLL aasd = new AutoAssignSIDDLL();
            string strRB = aasd.AutoAssignSIDByProcedure(shipdate, out errmsg);

            if (strRB.Equals("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK,查询OK";
            return "OK";
        }

        public DataTable GetAssignLineListDataTable()
        {
            AutoAssignSIDDLL aasd = new AutoAssignSIDDLL();
            DataSet dataSet = aasd.GetAssignLineListBySQL();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }
        public DataTable GetAssignLineListDataTable2()
        {
            AutoAssignSIDDLL aasd = new AutoAssignSIDDLL();
            DataSet dataSet = aasd.GetAssignLineListBySQL2();
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }

        public string UpdateLineSID(string strLine, string strSID, string strNewSID, string shipdate, out string errmsg)
        {

            errmsg = string.Empty;
            AutoAssignSIDDLL aasd = new AutoAssignSIDDLL();
            string strRB = aasd.UpdateLineSIDByProcedure( strLine,  strSID,  strNewSID,  shipdate, out  errmsg);

            if (strRB.Equals("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK,查询OK";
            return "OK";
        }
    }
}
