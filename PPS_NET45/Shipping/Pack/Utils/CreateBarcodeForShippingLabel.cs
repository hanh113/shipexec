using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Packingparcel.Utils
{
    class CreateBarcodeForShippingLabel
    {
        /// <summary>
        /// 生成SSCC "00" Carton, "10" Pallet
        /// </summary>
        /// <param name="type">"00" Carton, "10" Pallet</param>
        /// <param name="sscc">生成的SSCC</param>
        /// <returns>OK True</returns>
        public bool GetSSCC(bool isvirtual, string type, out string sscc)//SSCC 产生
        {
            string sSSCC = string.Empty;
            string sUCC = type + "885909";
            string sSQL = "SELECT PPSUSER.G_SSCC_CODE.NEXTVAL CODE FROM DUAL";
            if (isvirtual && type == "10") //混PO的栈板取虚拟SSCC
            {
                sUCC = type + "995909";
                sSQL = "SELECT PPSUSER.G_SSCC_CODE_V.NEXTVAL CODE FROM DUAL";
            }
            try
            {
                DataSet sDataSet = ClientUtils.ExecuteSQL(sSQL);
                string sFlowCode = sDataSet.Tables[0].Rows[0][0].ToString();
                sFlowCode = sFlowCode.PadLeft(9, '0');
                sSSCC = sUCC + sFlowCode;

                sscc = CalculateSSCC(sSSCC);
                return true;
            }
            catch (Exception ex)
            {
                sscc = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 创建sscc
        /// </summary>
        /// <param name="sStr"></param>
        /// <returns></returns>
        private string CalculateSSCC(string sStr)
        {
            int Total = 0;

            char[] sAarry = sStr.ToCharArray();

            for (int i = 1; i <= sAarry.Length; i++)
            {
                if ((i & 1) == 1)
                {
                    // odd digit, add 3 times value
                    Total += 3 * (int.Parse(sAarry[i - 1].ToString()));
                }
                else
                {
                    // even digit, just add value
                    Total += int.Parse(sAarry[i - 1].ToString());

                }
            }
            int CheckValue = Total % 10;
            int M = Total;
            while (CheckValue != 0)
            {
                M = M + 1;
                CheckValue = M % 10;
            }
            int checkDigit = M - Total;
            return sStr + checkDigit.ToString();
        }


        private string createKNBoxNo(string ptype)
        {
            string hawb = string.Empty;
            string fixedvalue = "00";
            string packtype = "1";
            //International Location Number,KS:4058644   JX:4061498
            string iln = "4058644";
            string sequence = string.Empty;

            string sqlstr = string.Empty;
            try
            {
                if (ptype == "C")
                {
                    sqlstr = "SELECT PPSUSER.G_KN_HAWB_C.NEXTVAL CODE FROM DUAL";
                    packtype = "3";
                }
                else { sqlstr = "SELECT PPSUSER.G_KN_HAWB_P.NEXTVAL CODE FROM DUAL"; }
                sequence = ClientUtils.ExecuteSQL(sqlstr).Tables[0].Rows[0]["CODE"].ToString().PadLeft(9, '0');
                hawb = fixedvalue + packtype + iln + sequence;

                hawb = CalculateSSCC(hawb);
                return hawb;
            }
            catch (Exception ex)
            {
                return hawb;
            }
        }

        /// <summary>
        /// EMEIA SHIP_TYPE = DSM  Create UUI
        /// </summary>
        /// <param name="shipmentid">shipment id</param>
        /// <param name="dnno">dn</param>
        /// <param name="dnline">dnline</param>
        /// <param name="printtype">Print Type P:Pallet  C:Carton</param>
        /// <param name="cartonno">carton no</param>
        /// <param name="msg">error Msg</param>
        /// <returns>UUI</returns>
       //public static string CreateUUI(string shipmentid, string dnno, string dnline, string printtype, string csscc, out string msg)
       // {
       //     string uui = string.Empty;
       //     if (dnno.Length != 10)
       //     {
       //         msg = "DN Length Error";
       //         return string.Empty;
       //     }
       //     if (dnline.Length != 6)
       //     {
       //         msg = "DN Line Length Error";
       //         return string.Empty;

       //     }
       //     try
       //     {
       //         string sqlstr = string.Empty;
       //         DataTable dt = new DataTable();
       //         #region 外层判断类型
       //         /*
       //         string sqlstr = "SELECT SHIP_TYPE FROM WMUSER.AC_CONSOL_SHIPMENT_HEADER@DGEDI WHERE SHIPMENT_ID=:SHIPMENT_ID AND ROWNUM=1 ";
       //         object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "SHIPMENT_ID", shipmentid } };
       //         DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
       //         if (dt.Rows.Count > 0)
       //         {
       //             string shiptype = dt.Rows[0]["SHIP_TYPE"].ToString();
       //             if (shiptype != "DSM")
       //             {
       //                 uui = "000000000000000000000";
       //                 msg = string.Empty;
       //                 return uui;
       //             }
       //             else
       //             {
       //              }
       //          */
       //         #endregion
       //         if (printtype == "P")
       //         {
       //             sqlstr = "SELECT  LPAD(QTY,4,'0') QTY FROM PPSUSER.G_DS_SHIPMENT_DNLINE_T WHERE SHIPMENT_ID=:SHIPMENT_ID AND DN=:DN AND  DN_LINE=:DN_LINE  AND ROWNUM=1";
       //             object[][] sqlparams1 = new object[][]{
       //                         new object[]{ParameterDirection.Input,OracleType.VarChar,"SHIPMENT_ID",shipmentid},
       //                         new object[]{ParameterDirection.Input,OracleType.VarChar,"DN",dnno},
       //                         new object[]{ParameterDirection.Input,OracleType.VarChar,"DN_LINE",dnline}};
       //             dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams1).Tables[0];
       //         }
       //         else if (printtype == "C")
       //         {
       //             sqlstr = "SELECT LPAD(QTY,4,0) QTY from PPSUSER.G_DS_SCANDATA_T WHERE SSCC=:CSSCC AND SHIPMENT_ID=:SHIPMENT_ID AND DN=:DN AND DN_LINE=:DN_LINE ";
       //             object[][] sqlparams1 = new object[][]{
       //                         new object[]{ParameterDirection.Input,OracleType.VarChar,"SHIPMENT_ID",shipmentid},
       //                         new object[]{ParameterDirection.Input,OracleType.VarChar,"DN",dnno},
       //                         new object[]{ParameterDirection.Input,OracleType.VarChar,"DN_LINE",dnline},
       //                     new object[]{ParameterDirection.Input,OracleType.VarChar,"CSSCC",csscc}};
       //             dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams1).Tables[0];
       //         }
       //         else
       //         {
       //             msg = string.Format("PrintType Error:'P' Pallet, 'C' Carton");
       //             return string.Empty;
       //         }
       //         if (dt.Rows.Count > 0)
       //         {
       //             string qty = dt.Rows[0]["QTY"].ToString();
       //             msg = string.Empty;
       //             uui = "U21" + dnno + dnline + qty;
       //             return uui;
       //         }
       //         else
       //         {
       //             msg = string.Format("PPSUSER.G_DS_SHIPMENT_DNLINE_T 未发现Shipmentid:{0}  DN:{1}  DNLine:{2}数据", shipmentid, dnno, dnline);
       //             return string.Empty;
       //         }
       //         //}
       //         //else
       //         //{
       //         //    msg = string.Format("WMUSER.AC_CONSOL_SHIPMENT_HEADER 未发现Shipmentid:{0}  SHIP_TYPE数据", shipmentid);
       //         //    return string.Empty;
       //         //}
       //     }
       //     catch (Exception ex)
       //     {
       //         msg = ex.Message;
       //         return string.Empty;
       //     }
       // }
    }
}
