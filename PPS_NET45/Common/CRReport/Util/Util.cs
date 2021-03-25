using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using CrystalDecisions.Windows.Forms;
using System.Drawing.Printing;
using CrystalDecisions.Shared;
using System.Threading;
using System.IO;

namespace CRReport.CRfrom
{
    class Util
    {
        /*
         * 注意:这个方法加了打印和释放ReportDocument对象.**/
        public static ReportDocument CreateDataTable(String reportPath, Object ds)
        {
            //TableLogOnInfo logOnInfo = new TableLogOnInfo();
            //logOnInfo.ConnectionInfo.ServerName = "";
            //logOnInfo.ConnectionInfo.DatabaseName = "";
            //logOnInfo.ConnectionInfo.UserID = "";
            //logOnInfo.ConnectionInfo.Password = "";
            reportPath = checkCRReportVersion(reportPath);
            ReportDocument doc = newRD(reportPath);
            doc.SetDataSource(ds);
            //crv.ReportSource = doc;  // 加载报表 

            //自动打印
            //crv.ShowFirstPage();

            //显示 选择时选择打印机
            //crv.PrintReport();

            doc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            doc.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //doc.PrintOptions.PrinterName ="HP LaserJet 1022";//设置打印机
            doc.PrintToPrinter(1, false, 0, 0);

            doc.Dispose();

            return doc;
        }

        public static DataTable getLableVersionInfoByLabelName(string labelName)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Format(@"   SELECT TLV.* FROM PPSUSER.T_LABEL_VERSION TLV
                                            WHERE TLV.LABEL_NAME = '{0}'", labelName);
                dt = ClientUtils.ExecuteSQL(sql).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {

                }
                else
                {
                    dt = null;
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        public static string checkCRReportVersion(string reportPath)
        {
            DataTable dt = new DataTable();
            string lastPath = "";
            string frontPartPath = reportPath.Substring(0, reportPath.LastIndexOf(@"\"));
            string latterPartPath = reportPath.Substring(reportPath.LastIndexOf(@"\") + 1);
            string serverVersion = "";
            string lastLabelPath = "";
            string labelName = latterPartPath.Substring(0, latterPartPath.IndexOf('.'));
            bool isSame = false;
            if (Directory.Exists(frontPartPath))//判断本地文件夹是否存在
            {
                dt = getLableVersionInfoByLabelName(labelName);
                if (dt != null)
                {
                    serverVersion = dt.Rows[0]["LABEL_VERSION"].ToString();
                    string serverLabelName = dt.Rows[0]["LABEL_NAME"].ToString();
                    string cSymbol = dt.Rows[0]["splicing_symbol"].ToString();
                    string serverLabelPathDir = dt.Rows[0]["label_server_path"].ToString();
                    string serverFullLabelName = serverLabelName + cSymbol + serverVersion + ".rpt";
                    string[] locRptFiles = Directory.GetFiles(frontPartPath, labelName + "#*.rpt", SearchOption.TopDirectoryOnly);
                    foreach (string locRptFile in locRptFiles)
                    {
                        string locRptName = locRptFile.Substring(locRptFile.LastIndexOf(@"\") + 1);
                        if (locRptName.Equals(serverFullLabelName))
                        {
                            isSame = true;
                            lastPath = locRptFile;
                            break;
                        }
                        File.Delete(locRptFile);
                    }

                    if (!isSame)
                    {
                        lastLabelPath = serverLabelPathDir + @"\" + serverFullLabelName;
                        if (File.Exists(lastLabelPath))
                        {
                            File.Copy(lastLabelPath, frontPartPath + @"\" + serverFullLabelName, true);
                        }
                        lastPath = frontPartPath + @"\" + serverFullLabelName;
                    }
                }
            }
            return lastPath;
        }
        public static string checkCRReportVersion(string reportPath, out string serverFullLabelName)
        {
            DataTable dt = new DataTable();
            string lastPath = "";
            string frontPartPath = reportPath.Substring(0, reportPath.LastIndexOf(@"\"));
            string latterPartPath = reportPath.Substring(reportPath.LastIndexOf(@"\") + 1);
            string serverVersion = "";
            string lastLabelPath = "";
            string labelName = latterPartPath.Substring(0, latterPartPath.IndexOf('.'));
            bool isSame = false;
            serverFullLabelName = "";
            if (Directory.Exists(frontPartPath))//判断本地文件夹是否存在
            {
                dt = getLableVersionInfoByLabelName(labelName);
                if (dt != null)
                {
                    serverVersion = dt.Rows[0]["LABEL_VERSION"].ToString();
                    string serverLabelName = dt.Rows[0]["LABEL_NAME"].ToString();
                    string cSymbol = dt.Rows[0]["splicing_symbol"].ToString();
                    string serverLabelPathDir = dt.Rows[0]["label_server_path"].ToString();
                    serverFullLabelName = serverLabelName + cSymbol + serverVersion + ".rpt";
                    string[] locRptFiles = Directory.GetFiles(frontPartPath, labelName + "#*.rpt", SearchOption.TopDirectoryOnly);
                    foreach (string locRptFile in locRptFiles)
                    {
                        string locRptName = locRptFile.Substring(locRptFile.LastIndexOf(@"\") + 1);
                        if (locRptName.Equals(serverFullLabelName))
                        {
                            isSame = true;
                            lastPath = locRptFile;
                            break;
                        }
                        File.Delete(locRptFile);
                    }

                    if (!isSame)
                    {
                        lastLabelPath = serverLabelPathDir + @"\" + serverFullLabelName;
                        if (File.Exists(lastLabelPath))
                        {
                            File.Copy(lastLabelPath, frontPartPath + @"\" + serverFullLabelName, true);
                        }
                        lastPath = frontPartPath + @"\" + serverFullLabelName;
                    }
                }
            }
            return lastPath;
        }
        public static ReportDocument CreateDataTableADDcount(String reportPath, Object ds, int nCopies)
        {
            //HYQ： 增加一个参数，打印几份。 nCopies
            //TableLogOnInfo logOnInfo = new TableLogOnInfo();
            //logOnInfo.ConnectionInfo.ServerName = "";
            //logOnInfo.ConnectionInfo.DatabaseName = "";
            //logOnInfo.ConnectionInfo.UserID = "";
            //logOnInfo.ConnectionInfo.Password = "";
            reportPath = checkCRReportVersion(reportPath);
            ReportDocument doc = newRD(reportPath);
            doc.SetDataSource(ds);

            // crv.ReportSource = doc;  // 加载报表 


            //自动打印
            //crv.ShowFirstPage();

            //显示 选择时选择打印机
            //crv.PrintReport();

            doc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            doc.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //doc.PrintOptions.PrinterName ="HP LaserJet 1022";//设置打印机
            doc.PrintToPrinter(nCopies, false, 0, 0);

            doc.Dispose();

            return doc;
        }
        public static ReportDocument CreateDataTableADDcount(String reportPath, Object ds, int nCopies, out string serverFullLabelName)
        {
            //HYQ： 增加一个参数，打印几份。 nCopies
            //TableLogOnInfo logOnInfo = new TableLogOnInfo();
            //logOnInfo.ConnectionInfo.ServerName = "";
            //logOnInfo.ConnectionInfo.DatabaseName = "";
            //logOnInfo.ConnectionInfo.UserID = "";
            //logOnInfo.ConnectionInfo.Password = "";
            reportPath = checkCRReportVersion(reportPath, out serverFullLabelName);
            ReportDocument doc = newRD(reportPath);
            doc.SetDataSource(ds);
            //crv.ReportSource = doc;  // 加载报表 


            //自动打印
            //crv.ShowFirstPage();

            //显示 选择时选择打印机
            //crv.PrintReport();

            doc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            doc.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //doc.PrintOptions.PrinterName ="HP LaserJet 1022";//设置打印机
            doc.PrintToPrinter(nCopies, false, 0, 0);

            doc.Dispose();

            return doc;
        }


        public static ReportDocument createRpt(String reportPath, Object ds)
        {
            reportPath = checkCRReportVersion(reportPath);
            ReportDocument doc = newRD(reportPath);
            doc.SetDataSource(ds);
            //  crv.ReportSource = doc;  // 加载报表 
            //自动打印
            //crv.ShowFirstPage();

            //显示 选择时选择打印机
            //crv.PrintReport();

            doc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            //doc.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //doc.PrintOptions.PrinterName ="HP LaserJet 1022";//设置打印机
            doc.PrintToPrinter(1, false, 0, 0);

            doc.Dispose();

            return doc;
        }

        public static ReportDocument printPDFFile(String reportPath, Object ds, CrystalReportViewer crv)
        {
            reportPath = checkCRReportVersion(reportPath);
            ReportDocument doc = newRD(reportPath);
            doc.SetDataSource(ds);
            crv.ReportSource = doc;  // 加载报表 
            //自动打印
            //crv.ShowFirstPage();
            //显示 选择时选择打印机
            //crv.PrintReport();
            doc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
            //doc.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //doc.PrintOptions.PrinterName ="HP LaserJet 1022";//设置打印机
            doc.PrintToPrinter(1, false, 0, 0);
            doc.Dispose();
            return doc;
        }
        private static ReportDocument newRD(String reportPath)
        {
            ReportDocument doc = new ReportDocument();
            doc.Load(reportPath);
            return doc;
        }


        public static DataTable getDataTaleC(string strsql, string tableName)
        {
            DataTable action = null;
            DataSet ds = ClientUtils.ExecuteSQL(strsql);
            action = ds.Tables[0].Copy();
            if (action != null) action.TableName = tableName;
            return action;

        }


        //public static DataTable getDataTale(string strsql, string tableName, string type)
        //{
        //    DataTable action;
        //    if (type.Equals("PPS"))
        //    {
        //        action = GetData.getPPSDatatable(strsql);
        //    }
        //    else
        //    {
        //        action = GetData.getDatatable(strsql);
        //    }
        //    if (action != null) action.TableName = tableName;
        //    return action;

        //}


        /**Summary:此方法用于将CR报表导出为PDF，并保存在指定的文件中
         *Paramater List:  ①.Crystal Report 报表rpt文件路径（在Constant中）  ②.获取的Dataset 
         *                 ③.保存PDF文件的路径
         *Returns :   void     ---By Lk 2018/07/08  **/
        public static void printPDFCrystalReport(string reportPath, Object ds, string filePath)
        {
            //HYQ:必须时不存在的路径
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            //设置登陆信息
            TableLogOnInfo logOnInfo = new TableLogOnInfo();
            ReportDocument reportDoc = newRD(reportPath);
            logOnInfo.ConnectionInfo.ServerName = "www";
            logOnInfo.ConnectionInfo.DatabaseName = "archives";
            logOnInfo.ConnectionInfo.UserID = "sa";
            logOnInfo.ConnectionInfo.Password = "123456";
            reportDoc.Database.Tables[0].ApplyLogOnInfo(logOnInfo);
            reportDoc.SetDataSource(ds);
            reportDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath);
            reportDoc.Close();
        }
        public static void printPDFCrystalReportV2(string reportPath, Object ds, string filePath)
        {
            //目录不存在时创建目录
            string strDirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(strDirPath))
            {
                Directory.CreateDirectory(strDirPath);
            }
            //HYQ:必须时不存在的路径
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            //增加水晶报表版本管控
            reportPath = checkCRReportVersion(reportPath);
            //设置登陆信息
            TableLogOnInfo logOnInfo = new TableLogOnInfo();
            ReportDocument reportDoc = newRD(reportPath);
            logOnInfo.ConnectionInfo.ServerName = "www";
            logOnInfo.ConnectionInfo.DatabaseName = "archives";
            logOnInfo.ConnectionInfo.UserID = "sa";
            logOnInfo.ConnectionInfo.Password = "123456";
            reportDoc.Database.Tables[0].ApplyLogOnInfo(logOnInfo);
            reportDoc.SetDataSource(ds);
            reportDoc.ExportToDisk(ExportFormatType.PortableDocFormat, filePath);
            reportDoc.Close();
        }
        /*Summary Export PDF for Crystal Report and send Email to customer  
         *Parameter List: ①.loadRptDir: loading rpt file directory  ②.DataSet:source dataset
         *                ③.diskPath: save PDF document complete disk path           
                       **/
        public static void printPDFCrystalReportV3(string reportPath, Object ds, string filePath, out string serverFullLabelName)
        {
            //目录不存在时创建目录
            string strDirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(strDirPath))
            {
                Directory.CreateDirectory(strDirPath);
            }
            //HYQ:必须时不存在的路径
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            //增加水晶报表版本管控
            reportPath = checkCRReportVersion(reportPath, out serverFullLabelName);
            //设置登陆信息
            TableLogOnInfo logOnInfo = new TableLogOnInfo();
            ReportDocument reportDoc = newRD(reportPath);
            logOnInfo.ConnectionInfo.ServerName = "www";
            logOnInfo.ConnectionInfo.DatabaseName = "archives";
            logOnInfo.ConnectionInfo.UserID = "sa";
            logOnInfo.ConnectionInfo.Password = "123456";
            reportDoc.Database.Tables[0].ApplyLogOnInfo(logOnInfo);
            reportDoc.SetDataSource(ds);
            reportDoc.ExportToDisk(ExportFormatType.PortableDocFormat, filePath);
            reportDoc.Close();
        }
        public static void exportCRPDFAndSendEmail(string loadRptDir, DataSet ds, string completeDiskPath)
        {
            try
            {
                List<string> filePath = new List<string>();
                filePath.Add(completeDiskPath);
                Util.printPDFCrystalReportV2(loadRptDir, ds, completeDiskPath);
                //Thread.Sleep(5000);
                //SendEmail.Send(filePath);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /**
         * Summary:print Crystal Report 
         * Parameter List: ①.crv
         * 
         *                  **/

    }
}
