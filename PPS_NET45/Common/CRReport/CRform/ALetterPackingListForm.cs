using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRReport.CRfrom
{
    public partial class ALetterPackingListForm
    {
        /**
         * Summary:Crystal Report function 
         * ParameterList:①.acDn:query table index  ②.print：judge print Crystal Report or export Crsytal Report for PDF
         *                 */

        public ALetterPackingListForm(string acDn, bool print)
        {
            InitializeC(acDn, print);
        }


        private void InitializeC(string acDn, bool print)
        {
            //初始化 DataSet
            DataSet ds = new DataSet();
            string headerSql = @"SELECT * FROM WMUSER.AC_DS_AMR_GL_LT_CSPL_HEADER@dgedi T0 WHERE T0.AC_DN = '" + acDn + "'";
            string lineSql = @"SELECT * FROM WMUSER.AC_DS_AMR_GL_LT_CSPL_LINE@dgedi T0 WHERE T0.AC_DN = '" + acDn + "'";

            ds.Tables.Add(getDataTaleC(headerSql, "Header"));
            ds.Tables.Add(getDataTaleC(lineSql, "Line"));

            if (print)
            {
                Util.CreateDataTable(
                Constant.ALLETERPK_URL,
                ds);

            }
            else
            {
                string completeDiskPath = "";
                //completeDiskPath = "D:\\MES_CLIENT\\PDF\\" + acDn + "ALetterPackingList.pdf";
                completeDiskPath = Application.StartupPath + "\\PDF\\" + acDn + "ALetterPackingList.pdf";
                Util.printPDFCrystalReportV2(Constant.ALLETERPK_URL, ds, completeDiskPath);
            }

        }

        private DataTable getDataTaleC(string strsql, string tableName)
        {
            DataTable action = null;
            DataSet ds = ClientUtils.ExecuteSQL(strsql);
            action = ds.Tables[0].Copy();
            if (action != null) action.TableName = tableName;
            return action;

        }
    }
}
