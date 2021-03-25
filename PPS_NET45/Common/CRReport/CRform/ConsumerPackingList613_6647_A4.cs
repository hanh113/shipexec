using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRReport.CRfrom
{
    public partial class ConsumerPackingList613_6647_A4
    {
        public string completeDiskPath = "";
        public ConsumerPackingList613_6647_A4(string acDn, bool print, string diskPath)
        {
            Initialize(acDn, print, diskPath);
        }

        public void Initialize(string acDn, bool print, string diskPath)
        {
            DataSet ds = new DataSet();
            string headerSql = @"select * from WMUSER.AC_DS_AMR_GL_CSPL_HEADER@dgedi where AC_DN = '" + acDn + "'";
            string lineSql = @"select * from WMUSER.AC_DS_AMR_GL_CSPL_LINE@dgedi where AC_DN = '" + acDn + "'";

            ds.Tables.Add(Util.getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(Util.getDataTaleC(lineSql, "Line"));

            string filePath = "";
            filePath = diskPath + acDn + "613_6647_A4_ConsumerPackingList.pdf";
            if (print)
            {
                Util.CreateDataTable(Constant.A4ConsumerPackingList_613_664_URL, ds);

            }
            else
            {
                Util.printPDFCrystalReportV2(Constant.A4ConsumerPackingList_613_664_URL, ds, filePath);
            }
        }
    }
}
