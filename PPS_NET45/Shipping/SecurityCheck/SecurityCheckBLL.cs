using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecurityCheck
{
    class SecurityCheckBLL
    {

        public void fillCmb(string strSQL, string colName, ComboBox cmb)
        {

            DataSet dts = ClientUtils.ExecuteSQL(strSQL);
            if (dts.Tables[0].Rows.Count > 0)
            {
                cmb.DataSource = dts.Tables[0];
                cmb.ValueMember = "id";
                cmb.DisplayMember = "name";
            }
            else
            {
                cmb.Items.Clear();
            }
        }

        public bool getPalletsQtyByTruck(string starttime, string endtime, string truckno, out string allreturnlist, out string errmsg)
        {
            SecurityCheckDAL sd = new SecurityCheckDAL();
            string strRB = sd.getPalletsQtyByTruckByProcedure(starttime, endtime, truckno, out allreturnlist, out errmsg);
            if (strRB.Equals("NG"))
            {
                return false;
            }
            errmsg = "OK";
            return true;
        }

        public DataTable GetCarInfoDataTable(string starttime, string endtime, string truckno)
        {
            if (string.IsNullOrEmpty(truckno)) { return null; }
            SecurityCheckDAL shipmentDal = new SecurityCheckDAL();
            DataSet dataSet = shipmentDal.GetCarInfoDataTablebySQL(starttime, endtime, truckno);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public bool InertTruckConfirm(string starttime, string endtime, string truckno, string whoconfirm, string passw, out string errmsg)
        {
            SecurityCheckDAL sd = new SecurityCheckDAL();
            string strRB = sd.TruckConfirmByProcedure(starttime, endtime, truckno, whoconfirm, passw, out errmsg);
            if (strRB.Equals("NG"))
            {
                return false;
            }
            errmsg = "OK";
            return true;
        }

    }
}
