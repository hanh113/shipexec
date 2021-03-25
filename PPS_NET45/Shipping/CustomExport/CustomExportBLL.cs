using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomExport
{
    class CustomExportBLL
    {
        public void CheckShipmentWeightStatus(string inpalletno, out string strregion, out string errmsg)
        {
            CustomExportDAL ced = new CustomExportDAL();
            string strRB = ced.CheckShipmentWeightStatusSP(inpalletno, out strregion, out errmsg);

        }



    }
}
