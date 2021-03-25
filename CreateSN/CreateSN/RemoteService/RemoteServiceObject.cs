using MESDataLayer;
using MESModel;
using Newtonsoft.Json;
using OperationWCF;
using SysInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteService
{
    public class RemoteServiceObject : HttpHosting, IRemoteServiceObject
    {
        //public const int WM_APP = 32768;

        //public const int WM_REFRESH = 32769;

        //public const int WM_CLEAR = 32770;

        //public static Dictionary<string, List<ClientObject>> clientList;


        private static System.Threading.Timer time;


        public RemoteServiceObject()
        {
            //fileList = new List<FileObject>();
        }


        //public object[] CallMethod(string ServerName, string cmd, object[] commandText)
        //{
        //    object[] objArray;
        //    string str = cmd;
        //    if (str == null)
        //    {
        //        objArray = this.EXECUTE_COMMAND(commandText);
        //        return objArray;
        //    }
        //    else if (str == "EXECUTE_PROC")
        //    {
        //        objArray = this.EXECUTE_PROC(commandText);
        //    }
        //    else if (str == "CHK_PRIVILEGE")
        //    {
        //        objArray = this.CHK_PRIVILEGE("","","");
        //    }
        //    else if (str == "ExecuteProc")
        //    {
        //        objArray = this.ExecuteProcCommand(commandText);
        //    }
        //    else if (str == "ExecuteSQL")
        //    {
        //        objArray = this.ExecuteSQLCommand(commandText);
        //    }
        //    else
        //    {
        //        if (str != "EXECUTE_COMMAND_PROC")
        //        {
        //            objArray = this.EXECUTE_COMMAND(commandText);
        //            return objArray;
        //        }
        //        objArray = (new MESDAL()).EXECUTE_COMMAND_PROC(commandText);
        //    }
        //    return objArray;
        //}


        public DALModel CheckEmployee(string employeeNo, string password, string ComputerName, int Port, out string empId, out string empName)
        {
            empId = null;
            empName = null;
            DALModel model = (new MESDAL()).CheckEmployee(employeeNo, password, out empId, out empName);
            if (model.IsSuccess)
            {
                ConnStatics.SaveLogin(ComputerName
                    , new ClientObject(ComputerName, employeeNo, empName, DateTime.Now, Port));
                SaveLog(LogType.Normal, "Login", string.Concat(employeeNo, " ", ComputerName));
            }
            return model;
        }

        public bool CheckImport(string sType, string sValue, string iParam)
        {
            return (new DataManager()).CheckImport(sType, sValue, iParam);
        }

        public void CheckList(string ComputerName, string employeeNo, string employeeName, int Port)
        {
            if (!string.IsNullOrEmpty(ComputerName))
            {
                if (!ConnStatics.ClientList.ContainsKey(ComputerName))
                {
                    SaveLog(LogType.Error, "Check List", string.Concat(employeeNo, " ", ComputerName, " Server Fail & Relogin"));
                    ConnStatics.SaveLogin(ComputerName,
                        new ClientObject(ComputerName, employeeNo, employeeName, DateTime.Now, Port));
                }
            }
        }

        public void CheckList(string ComputerName, string employeeNo, string employeeName)
        {
            this.CheckList(ComputerName, employeeNo, employeeName, 8086);
        }

        public DALModel CHK_PRIVILEGE(string EmpId, string PRG, string Fun)
        {
            return (new MESDAL()).CHK_PRIVILEGE(EmpId, PRG, Fun);
        }

        //public DataTable Columns(string sSQL, object[][] param)
        //{
        //    return (new MESDAL()).Columns(sSQL, param);
        //}

        //public DataTable Columns(string sSQL, object[] param)
        //{
        //    return (new MESDAL()).Columns(sSQL, param);
        //}

        public FileStream DownloadFile(string program, string fileName)
        {
            object[] startupPath = new object[] { ConnStatics.DownloadFolder, Path.DirectorySeparatorChar, program, Path.DirectorySeparatorChar };
            string str = string.Concat(startupPath);
            FileStream fileStream = new FileStream(string.Concat(str, fileName), FileMode.Open, FileAccess.Read, FileShare.Read);
            return fileStream;
        }

        public byte[] DownloadFileByte(string program, string fileName)
        {
            byte[] numArray;
            object[] startupPath = new object[] { ConnStatics.DownloadFolder, Path.DirectorySeparatorChar, program, Path.DirectorySeparatorChar };
            string str = string.Concat(startupPath);
            FileStream fileStream = new FileStream(string.Concat(str, fileName), FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                byte[] numArray1 = new byte[checked((int)fileStream.Length)];
                fileStream.Read(numArray1, 0, (int)fileStream.Length);
                fileStream.Dispose();
                numArray = numArray1;
            }
            finally
            {
                if (fileStream != null)
                {
                    ((IDisposable)fileStream).Dispose();
                }
            }
            return numArray;
        }

        public byte[] Excel_DataReader(string sComputer, string sReportID, int iMax, Dictionary<int, object[]> dc, string sSQL, object[][] Params, int iTotal, int iColumn, Dictionary<int, object[]> dc2, string sSQL2, object[][] Params2, int iTotal2, int iColumn2, bool bMasterDetail)
        {
            //byte[] numArray = (new MESDAL()).Excel_DataReader(sComputer, sReportID, iMax, dc, sSQL, Params, iTotal, iColumn, dc2, sSQL2, Params2, iTotal2, iColumn2, bMasterDetail);
            //return numArray;
            return null;
        }

        public object[] EXECUTE_COMMAND(object[] commandText)
        {
            //return (new MESDAL()).EXECUTE_COMMAND(commandText);
            return null;

        }

        public object[] EXECUTE_PROC(object[] commandText)
        {
            //return (new MESDAL()).EXECUTE_PROC(commandText);
            return null;


        }

        public object[] ExecuteBatch(object[][] dt, string sTable, string[] sFields, string[] sTypes)
        {
            // return (new MESDAL()).ExecuteBatch(dt, sTable, sFields, sTypes);
            return null;

        }

        public object[] ExecuteProcCommand(object[] commandText)
        {
            //return (new MESDAL()).ExecuteProc(commandText);
            return null;

        }

        public DALModel ExecuteProc(string sProcName, string Paramstr)
        {
            UniDb.StoredProcedureParameter[] Params = null;
            DALModel model = new DALModel { };
            try
            {
                if (!string.IsNullOrEmpty(Paramstr))
                    Params = JsonConvert.DeserializeObject<UniDb.StoredProcedureParameter[]>(Paramstr);
                var models = (new MESDAL()).GetStoredProcedureValues(sProcName, Params);
                model.IsSuccess = true;
                model.Message = JsonConvert.SerializeObject(models);
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = ex.Message;
            }
            return model;
        }

        public object[] ExecuteSQLCommand(object[] commandText)
        {
            //return (new MESDAL()).ExecuteSQL(commandText);
            return null;

        }

        public DataSet ExecuteSQL(string sSQL)
        {
            return (new MESDAL()).ExecuteQuery(sSQL);
        }

        public DataSet ExecuteParameterSQL(string sSQL, Dictionary<string, object> Param)
        {
            var model = (new MESDAL()).Query<dynamic>(sSQL, Param);
            var ds = new DataSet();
            if (model != null)
            {
                var json = JsonConvert.SerializeObject(model);
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                ds.Tables.Add(dt);
            }
            return ds;
        }


        public string ExportFile(string sAction, string sType, string sData, string iParam)
        {
            return (new DataManager()).ExportFile(sAction, sType, sData, iParam);
        }

        public int GetCount()
        {
            return ConnStatics.ClientList.Count;
        }

        public DataSet GetData(string sSQL, object[][] Params, string iParam)
        {
            return (new DataManager()).GetData(sSQL, Params, iParam);
        }

        private void GetDirectoryFile(string dir, string Parentdir,List<FileObject> fileList)
        {
            string str;
            string[] directories = Directory.GetDirectories(dir);
            for (int i = 0; i < (int)directories.Length; i++)
            {
                string str1 = directories[i];
                string str2 = string.Concat(Path.GetFileName(str1), Path.DirectorySeparatorChar);
                if (!string.IsNullOrEmpty(Parentdir))
                {
                    str2 = string.Concat(Parentdir, str2);
                }
                string[] files = Directory.GetFiles(str1);
                for (int j = 0; j < (int)files.Length; j++)
                {
                    string str3 = files[j];
                    try
                    {
                        str = FileVersionInfo.GetVersionInfo(str3).FileVersion.ToString();
                    }
                    catch
                    {
                        str = "";
                    }
                    DateTime lastWriteTime = File.GetLastWriteTime(str3);
                    fileList.Add(new FileObject(string.Concat(str2, Path.GetFileName(str3)), str, lastWriteTime));
                }
                this.GetDirectoryFile(str1, str2, fileList);
            }
        }

        public string GetDmpList(string sType, string iParam)
        {
            return (new DataManager()).GetDmpList(sType, iParam);
        }

        public List<FileObject> GetFileList(string program)
        {
            List<FileObject> fileList = new List<FileObject>();
            string str;
            string str1;
            DateTime lastWriteTime;
            int i;
            //
            string str2 = string.Concat(ConnStatics.DownloadFolder, Path.DirectorySeparatorChar, program);
            fileList.Clear();
            if (Directory.Exists(str2))
            {
                str2 = str2.ToUpper();
                var files = Directory.GetFiles(str2, "*", SearchOption.AllDirectories);

                fileList = files.Select(x =>
                {

                    var info = new FileInfo(x);
                    var version = FileVersionInfo.GetVersionInfo(x);
                    var name = info.FullName.ToUpper().Replace(str2 + "\\", "");
                    return new FileObject(name, version.FileVersion == null ? "" : version.FileVersion.ToString()
                        , info.LastWriteTime);
                }).ToList();
            }
            return fileList;
        }

        public List<FileObject> GetFileLists(string program)
        {
            List<FileObject> fileList = new List<FileObject>();

            string str;
            string str1 = string.Concat(ConnStatics.DownloadFolder, Path.DirectorySeparatorChar, program);
            fileList.Clear();
            if (Directory.Exists(str1))
            {
                str1 = str1.ToUpper();

                var files = Directory.GetFiles(str1, "*", SearchOption.AllDirectories);

                fileList = files.Select(x =>
                {

                    var info = new FileInfo(x);
                    var version = FileVersionInfo.GetVersionInfo(x);

                    var name = info.FullName.ToUpper().Replace(str1 + "\\", "");
                    return new FileObject(name, version.FileVersion == null ? "" : version.FileVersion.ToString()
                        , info.LastWriteTime);
                }).ToList();
            }
            return fileList;
        }

        public DALModel GetFunction(string program, string empID, string webflag, string cultureName)
        {
            return (new MESDAL()).GetFunction(program, empID, webflag, cultureName);
        }

        public string GetLogFile(string sFile, string iParam)
        {
            return (new DataManager()).GetLogFile(sFile, iParam);
        }

        public string GetLogList(string sType, string iParam)
        {

            return (new DataManager()).GetLogList(sType, iParam);
        }

        public DALModel GetProgram(string empId, string webFlag, string cultureName)
        {
            return (new MESDAL()).GetProgram(empId, webFlag, cultureName);
        }

        public object[] GetSysBaseData(string sProgram, string sParamName)
        {
            return null;

            // return (new MESDAL()).GetSysBaseData(sProgram, sParamName);
        }

        public DateTime GetSysData()
        {
            return DateTime.Now;

            //return (new MESDAL()).GetSysDate();
        }

        public string GetUserList()
        {
            return JsonConvert.SerializeObject(ConnStatics.ClientList);
        }

        public string ImportFile(int iFirst, string sFile, string iParam)
        {
            return (new DataManager()).ImportFile(iFirst, sFile, iParam);
        }

        public void Logout(string ComputerName, string employeeNo)
        {
            if (ConnStatics.ClientList.ContainsKey(ComputerName))
            {
                SaveLog(LogType.Normal, "Logout", string.Concat(employeeNo, " ", ComputerName));
                ConnStatics.RemoveClientList(ComputerName);
            }
        }

        public string MoveLogFile(string sFile, string sStatus, string iParam)
        {
            return (new DataManager()).MoveLogFile(sFile, sStatus, iParam);
        }

        public Dictionary<string, List<object[,]>> Object_DataReader(int iMax, Dictionary<int, object[]> dc, string sSQL, object[][] Params, int iTotal, int iColumn, Dictionary<int, object[]> dc2, string sSQL2, object[][] Params2, int iTotal2, int iColumn2, bool bMasterDetail)
        {
            return null;

            //Dictionary<string, List<object[,]>> strs = (new MESDAL()).Object_DataReader(iMax, dc, sSQL, Params, iTotal, iColumn, dc2, sSQL2, Params2, iTotal2, iColumn2, bMasterDetail);
            //return strs;
        }

        public bool refreshStoredProcedureParam(string sProcedureName, ref OracleParameter[] f_Parameter)
        {
            return false;

            //return (new MESDAL()).refreshStoredProcedureParam(sProcedureName, ref f_Parameter);
        }

        public int RowCount(string sSQL, object[][] param)
        {
            return 0;

            //return (new MESDAL()).RowCount(sSQL, param);
        }

        public int RowCount(string sSQL, object[] param)
        {
            return 0;

            //return (new MESDAL()).RowCount(sSQL, param);
        }

        public bool RsvFromRemote(string f_ComputerName, int f_iCommandNo, int f_isubCommandNo, string f_pData, ref string f_sResult)
        {
            return false;
        }

        private static void SaveLog(LogType f_LogType, string f_sFunction, string f_sMessage)
        {
            classChromaLog.addLog(f_LogType, f_sFunction, f_sMessage);
        }


        public DataTable SupplyPageOfData(string sSQL, object[][] param, int lowerPageBoundary, int rowsPerPage)
        {
            return null;
            //return (new MESDAL()).SupplyPageOfData(sSQL, param, lowerPageBoundary, rowsPerPage);
        }

        public DataTable SupplyPageOfData(string sSQL, object[][] param, int lowerPageBoundary, int rowsPerPage, ref int rowCountValue, ref DataTable dt)
        {
            return null;

            //DataTable dataTable = (new MESDAL()).SupplyPageOfData(sSQL, param, lowerPageBoundary, rowsPerPage, ref rowCountValue, ref dt);
            //return dataTable;
        }

        public DataTable SupplyPageOfData(string sSQL, object[] param, int lowerPageBoundary, int rowsPerPage)
        {
            return null;

            // return (new MESDAL()).SupplyPageOfData(sSQL, param, lowerPageBoundary, rowsPerPage);
        }

        public DataTable SupplyPageOfData(string sSQL, object[] param, int lowerPageBoundary, int rowsPerPage, ref int rowCountValue, ref DataTable dt)
        {
            return null;

            //DataTable dataTable = (new MESDAL()).SupplyPageOfData(sSQL, param, lowerPageBoundary, rowsPerPage, ref rowCountValue, ref dt);
            //return dataTable;
        }

        public string TestConnection(string connection)
        {
            return null;

            //return (new MESDAL()).TestConnection(connection);
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        private DataTable convertToDataTable(object data)
        {

            var xxx = data as IEnumerable<object>;



            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(data.GetType());
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            //foreach (var item in data)
            //{
            //    DataRow row = table.NewRow();
            //    foreach (PropertyDescriptor prop in properties)
            //        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            //    table.Rows.Add(row);
            //}
            return table;

        }
        private bool isList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
    }
}
