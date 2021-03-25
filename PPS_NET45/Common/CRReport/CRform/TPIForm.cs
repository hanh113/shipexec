using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class TPIForm
    {
        public string completeDiskPath = "";
        public TPIForm(string shipID, bool print)
        {
            initialize(shipID, print);
        }

        public void initialize(string shipID, bool print)
        {

            DataSet ds = new DataSet();
            string invoiceType = "";
            string rptType = "";
            string pdfNameSuffix = "";
            string filePath = "";
            string headerSql = "select * from wmuser.AC_APAC_DS_CCI_HEADER@dgedi where SHIPMENT_ID = '" + shipID + "'";
            string lineSql = "select * from wmuser.AC_APAC_DS_CCI_LINE@dgedi where SHIPMENT_ID = '" + shipID + "'";
            DataSet headerDS = ClientUtils.ExecuteSQL(headerSql);
            if (headerDS != null && headerDS.Tables.Count > 0)
            {
                invoiceType = headerDS.Tables[0].Rows[0][31].ToString();
            }
            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));
            if (invoiceType.Equals("TPI"))
            {
                rptType = Constant.DSTPI_URL;
                pdfNameSuffix = "TPI.pdf";

            }
            else
            {
                rptType = Constant.DSShippingInvoice_URL;
                pdfNameSuffix = "SI.pdf";
            }

            filePath = Application.StartupPath + "\\PDF\\" + shipID + pdfNameSuffix;
            completeDiskPath = filePath;
            if (print)
            {
                Util.CreateDataTable(rptType, ds);
            }
            else
            {
                Util.printPDFCrystalReportV2(rptType, ds, filePath);
            }

        }
    }
}
