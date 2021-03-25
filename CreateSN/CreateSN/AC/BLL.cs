using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePPSDataAC
{
    class BLL
    {
        public string TRANWMSDATA( out string errmsg)
        {
            DAL dal = new DAL();
            errmsg = string.Empty;
            string strResult = dal.TRANWMSDATABySP( out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

        public string ACTRANWMSDATA(out string errmsg)
        {
            DAL dal = new DAL();
            errmsg = string.Empty;
            string strResult = dal.ACTRANWMSDATABySP(out errmsg);
            if (strResult.Equals("NG"))
            {
                return "NG";
            }
            return "OK";
        }

    }
}
