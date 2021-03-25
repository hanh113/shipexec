using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class ShippingInvoiceForm
    {
        public string completeDiskPath = "";
        public ShippingInvoiceForm(string shipID, bool print)
        {
            Initialize(shipID, print);
        }

        public void Initialize(string shipID, bool print)
        {
            DataSet ds = new DataSet();

            string headerSql = "select * from wmuser.AC_APAC_DS_CCI_HEADER@dgedi where SHIPMENT_ID = '" + shipID + "'";
            string lineSql = "select * from wmuser.AC_APAC_DS_CCI_LINE@dgedi where SHIPMENT_ID = '" + shipID + "'";

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));

            string filePath = "";
            filePath = Application.StartupPath + "\\PDF\\" + shipID + "SI.pdf";
            completeDiskPath = filePath;
            if (print)
            {
                Util.CreateDataTable(Constant.DSShippingInvoice_URL, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.DSShippingInvoice_URL, ds, filePath);
            }
        }
    }
}
