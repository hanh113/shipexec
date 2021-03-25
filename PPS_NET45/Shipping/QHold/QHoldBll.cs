using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QHold
{
    class QHoldBll
    {

        public DataTable GetHoldInfoDataTable(string sid,string strDBtype)
        {
            if (string.IsNullOrEmpty(sid)) { return null; }
            QHoldDal SIDDal = new QHoldDal();
            DataSet dataSet = SIDDal.GetHoldInfoDataTable(sid, strDBtype);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dataSet.Tables[0];
            }
        }


        public string CheckHoldCarton(string shipmentid, string cartonno, out string errmsg)
        {

            errmsg = string.Empty;
            QHoldDal QHdal = new QHoldDal();
            string strRB = QHdal.CheckHoldCartonByProcedure(shipmentid, cartonno, out errmsg);

            if (strRB.Equals("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK,查询OK";
            return "OK";
        }


        public string ReplaceHoldCarton(string shipmentid, string newcartonno,string oldsnorcarton,out string strlocationinfo, out string errmsg)
        {

            errmsg = string.Empty;
            strlocationinfo = string.Empty;
            QHoldDal QHdal = new QHoldDal();
            string strRB = QHdal.ReplaceHoldCartonByProcedure(shipmentid, newcartonno, oldsnorcarton,out strlocationinfo, out errmsg);

            if (strRB.Equals("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
        public string ZCHoldPallet(string shipmentid, string HoldPallet,  out string errmsg)
        {

            errmsg = string.Empty;
            QHoldDal QHdal = new QHoldDal();
            string strRB = QHdal.ZCHoldPalletByProcedure(shipmentid, HoldPallet, out errmsg);

            if (strRB.Equals("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
                return "NG";
            }
            errmsg = "OK";
            return "OK";
        }


        public string TOHoldCarton(string CartonNo, Int16 HoldCount,out string errmsg)
        {

            errmsg = string.Empty;
            QHoldDal QHdal = new QHoldDal();
            string strRB = QHdal.TOHoldCartonByProcedure(CartonNo, HoldCount, out errmsg);

            if (strRB.Contains("NG"))
            {
                LibHelper.MediasHelper.PlaySoundAsyncByHold();
            }
            return errmsg;
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
        public string GetDBType(string inparatype, out string outparavalue, out string errmsg)
        {

            errmsg = string.Empty;
            QHoldDal pl = new QHoldDal();
            string strRB = pl.GetDBTypeBySP(inparatype, out outparavalue, out errmsg);
            if (strRB.Equals("NG"))
            {
                return "NG";
            }
            errmsg = "OK,执行OK";
            return "OK";
        }
    }
}
