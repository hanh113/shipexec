using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class EMEA_OEM_PK_RussiaForm
    {
        public string diskCompletePath = ""; //全局变量返回pdf路径
        public EMEA_OEM_PK_RussiaForm(string shipID, bool print)
        {
            Initialize(shipID, print);
        }

        private void Initialize(string shipID, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            //AmrDsShippingLabel
            string headerSql = @"SELECT * FROM WMUSER.AC_EMEIA_DS_CPL_HEADER@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            string lineSql = @"SELECT * FROM WMUSER.AC_EMEIA_DS_CPL_LINE@dgedi T0 WHERE T0.SHIPMENT_ID = '" + shipID + "'";
            DataTable lineDt = Util.getDataTaleC(lineSql, "Line");
            int endCartons = 0;
            int startCartons = 1;
            int currentCartons = 0;
            if (lineDt != null && lineDt.Rows.Count > 0)
            {
                for (int i = 0; i < lineDt.Rows.Count; i++)
                {
                    currentCartons = Convert.ToInt32(lineDt.Rows[i][2].ToString());
                    endCartons = endCartons + currentCartons;
                    lineDt.Rows[i][2] = startCartons + "-" + endCartons;
                    startCartons = startCartons + currentCartons;
                }
            }
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(lineDt);
            string completePath = "";
            completePath = Application.StartupPath + "\\PDF\\" + shipID + "EMEA_OEM_PK_Russia.pdf";
            diskCompletePath = completePath; //全局变量返回pdf路径
            if (print)
            {
                Util.CreateDataTable(Constant.EMEARussia_URL, ds);
            }
            else
            {
                Util.exportCRPDFAndSendEmail(Constant.EMEARussia_URL, ds, completePath);
            }
        }
    }
}
