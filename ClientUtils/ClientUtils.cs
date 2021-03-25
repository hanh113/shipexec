using ClientUtilsDll;
using MESModel;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using RemoteService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml;


public class ClientUtils
{
    public static string fClientLang;

    public static string cultureName;

    public static string UserPara1;

    public static string fLoginUser;

    public static string fUserName;

    public static string fLoginPassword;

    public static string fPath;

    public static string fComputer;

    public static string fCurrentProject;

    public static string fProgramName;

    public static string fFunctionName;

    public static string fParameter;

    private static string fClientSystem;

    public static bool bChangeFont;

    public static bool bLoadImage;

    public static Font fModuleFont;


    public static int iExcelRow;


    private static IEnumerable<LanguageModel> _language { get; set; }
    public static IEnumerable<LanguageModel> Language
    {
        get
        {
            if (_language == null || _language.Count() < 1)
            {
                _language = new List<LanguageModel>();
                try
                {
                    //                   var ds = ExecuteSQL(@" SELECT *
                    // FROM PPSUSER.T_BASICLANGUAGE_INFO a
                    //WHERE enabled = 'Y' order by case substr(langcode, 0, 1)
                    //           when 'S' then
                    //            1
                    //           when 'E' then
                    //            2
                    //           else
                    //            3
                    //         end
                    //                      ");
                    var ds = ExecuteSQL(@"SELECT *
                      FROM PPSUSER.T_BASICLANGUAGE_INFO a
                     WHERE enabled = 'Y'
                     order by  proname,langcode desc ");
                    if (ds.Tables.Count == 1)
                    {
                        _language = ds.Tables[0].DatatableToObject<LanguageModel>();
                    }
                }
                catch
                {
                }
            }
            return _language;

        }
    }

    public static string GetUserList()
    {
        return Connect().GetUserList();
    }

    public static void ClearLanguage()
    {
        _language = null;
    }

    public static void runBackground(Form form)
    {
        BackgroundWorker _bw = new BackgroundWorker();
        _bw.DoWork += new DoWorkEventHandler(runChangeIcon);
        _bw.RunWorkerAsync(form);
    }

    private static void runChangeIcon(object sender, DoWorkEventArgs e)
    {

        var form = e.Argument as Form;
        ClientUtils.ClearLanguage();
        var ctrls = form.GetAllControls();
        // foreach(var c in ctrls)
        Parallel.ForEach(ctrls, c =>
        {
            try
            {
                if (c.GetType() == typeof(Label))
                    setValue((Label)c, ((Label)c).Text.TL());
                else if (c.GetType() == typeof(GroupBox))
                    setValue((GroupBox)c, ((GroupBox)c).Text.TL());
                else if (c.GetType() == typeof(RadioButton))
                    setValue((RadioButton)c, ((RadioButton)c).Text.TL());
                else if (c.GetType() == typeof(Button))
                    setValue((Button)c, ((Button)c).Text.TL());
                else if (c.GetType() == typeof(CheckBox))
                    setValue((CheckBox)c, ((CheckBox)c).Text.TL());
                else if (c.GetType() == typeof(DataGridView))
                {
                    var grid = (DataGridView)c;
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        setGridViewValue(col, grid, col.HeaderText.TL());
                    }
                }
            }
            catch (Exception ex)
            {
            }


        }
        );
    }

    private static void setValue(System.Windows.Forms.Control c, string value)
    {
        Action action = () =>
        {
            c.Text = value;
            c.Font = new Font(fModuleFont.FontFamily.Name, c.Font.Size);
        };
        if (c.InvokeRequired)
        {
            c.Invoke(action);
        }
        else
        {
            action();
        }
    }

    private static void setGridViewValue(DataGridViewColumn c, DataGridView grid, string value)
    {
        Action action = () =>
        {
            c.HeaderText = value;
        };
        if (grid.InvokeRequired)
        {
            grid.Invoke(action);
        }
        else
        {
            action();
        }
    }

    public static string IPAddressV4
    {
        get
        {
            string str = null;
            IPAddress[] hostAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            int num = 0;
            while (num < (int)hostAddresses.Length)
            {
                IPAddress pAddress = hostAddresses[num];
                if ((pAddress.AddressFamily != AddressFamily.InterNetwork ? true : pAddress.ToString().StartsWith("169.")))
                {
                    num++;
                }
                else
                {
                    str = pAddress.ToString();
                    break;
                }
            }
            return str;
        }
    }
    public static string url;

    static ClientUtils()
    {
        ClientUtils.fClientSystem = "Y";
        ClientUtils.bChangeFont = false;
        ClientUtils.bLoadImage = true;
    }

    public ClientUtils()
    {
        ClientUtils.GetBroadCast();
    }
    /// <summary>
    /// bartender ��ӡ�ı��ĵ�����Դ
    /// </summary>
    /// <param name="LabelPath">��ǩ��ַ</param>
    /// <param name="DataPath">�ı�����Դ��ַ</param>
    /// <param name="Printer">ָ����ӡ��</param>
    /// <param name="PrintQty">��ӡ����</param>
    public async static Task PrintBartender(string LabelPath, string DataPath, string Printer = null, int PrintQty = 1)
    {
        await print(LabelPath, DataPath, Printer, PrintQty);
    }
    private static Task print(string LabelPath, string DataPath, string Printer = null, int PrintQty = 1)
    {
        var task = Task.Run(() =>
       {
           BarTender.Application btEngine = new BarTender.Application();
           BarTender.Format btFormat = new BarTender.Format();
           //��ȡģ��
           btFormat = btEngine.Formats.Open(LabelPath);
           //��ӡ����
           btFormat.PrintSetup.IdenticalCopiesOfLabel = PrintQty;
           //���¼����ı����ݿ�
           btFormat.InputDataFileSetup.TextFile.FileName = DataPath;
           //ָ����ӡ��/ץȡ��ǩĬ�ϴ�ӡ��
           if (Printer != null)
               btFormat.Printer = Printer;
           //��ӡ
           btFormat.PrintOut();
           btFormat.Close();
       });
        // task.Wait();
        return task;
    }
    public static void addConnLog(LogType f_LogType, string f_sFunction, string f_sMessage)
    {
        classChromaLog.addLog(f_LogType, f_sFunction, f_sMessage);
    }

    public static IRemoteServiceObject Connect()
    {
        return new RemoteObject().New();
    }

    //public static bool CheckImport(string sType, string sValue, string iParam)
    //{
    //    return Connect().CheckImport(sType, sValue, iParam);
    //}

    public static string CheckUser(string userNo, string password, string computerName, string sAPServer, out string empId, out string empName)
    {
        ClientUtils.fComputer = computerName;
        int broadCast = ClientUtils.GetBroadCast();
        DALModel model = Connect().CheckEmployee(userNo, password, computerName, broadCast, out empId, out empName);
        if (!model.IsSuccess)
        {
            ClientUtils.addConnLog(LogType.Error, "Check User", model.Message);
            throw new Exception(model.Message);
        }
        return model.Message;
    }

    public static DataTable Columns(string sSQL, object[] param)
    {
        return null;
        //return Connect().Columns(sSQL, param);
    }

    public static DataTable Columns(string sSQL, object[][] param)
    {
        return null;
        //return Connect().Columns(sSQL, param);
    }


    public static FileStream DownloadFile(string program, string fileName)
    {

        return Connect().DownloadFile(program, fileName);
    }

    public static byte[] Excel_DataReader(string sReportID, int iMax, Dictionary<int, object[]> dc, string sSQL, object[][] Params, int iTotal, int iColumn, Dictionary<int, object[]> dc2, string sSQL2, object[][] Params2, int iTotal2, int iColumn2, bool bMasterDetail)
    {
        byte[] numArray = Connect().Excel_DataReader(ClientUtils.fComputer, sReportID, iMax, dc, sSQL, Params, iTotal, iColumn, dc2, sSQL2, Params2, iTotal2, iColumn2, bMasterDetail);
        return numArray;
    }

    public static object[] EXECUTE_COMMAND(object[] sSQL)
    {

        return Connect().EXECUTE_COMMAND(sSQL);
    }

    //public static object[] ExecuteObjectProc(string sProcName, object[][] Params)
    //{

    //    return Connect().ExecuteProc(sProcName, Params);
    //}

    //public static object[] ExecuteObjectProc(object[] Params)
    //{

    //    return Connect().ExecuteProcCommand(Params);
    //}

    public static DataSet ExecuteObjectSQL(string sSQL)
    {
        return Connect().ExecuteSQL(sSQL);
    }

    public static object[] ExecuteObjectSQL(string sSQL, object[][] Params)
    {
        return null;
        //return Connect().ExecuteSQL(sSQL, Params);
    }

    public static object[] ExecuteObjectSQL(object[] sSQL)
    {

        return Connect().ExecuteSQLCommand(sSQL);
    }

    public static void DoTransaction<T>(string sql, List<T> data)
    {
        Dictionary<string, List<T>> dict = new Dictionary<string, List<T>>();
        dict.Add(sql, data);
        DoTransaction(dict);
    }
    public static void DoTransaction<T>(string sql, T data)
    {
        Dictionary<string, List<T>> dict = new Dictionary<string, List<T>>();
        dict.Add(sql, new List<T> { data });
        DoTransaction(dict);
    }

    public static void DoTransaction<T>(Dictionary<string, List<T>> obj)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        foreach (var o in obj)
        {
            dict.Add(o.Key,
              JsonConvert.SerializeObject(o.Value));
        }

        var errMsg = Connect().DoTransaction(dict);
        if (!string.IsNullOrEmpty(errMsg))
            throw new Exception(errMsg);
    }
    public static IEnumerable<T> Query<T>(string sql, object o)
    {

        var dict = o.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(o, null));
        string ret = Connect().ExecuteQuery(sql, dict);
        DALModel m = JsonConvert.DeserializeObject<DALModel>(ret);
        if (!m.IsSuccess)
            throw new Exception(m.Message);

        return JsonConvert.DeserializeObject<IEnumerable<T>>(m.Message);
    }

    public static DataSet ExecuteProc(string sProcName, object[][] Params)
    {
        var m = transformSPP(Params);
        var model = Connect().ExecuteProc(sProcName, m);
        if (!model.IsSuccess)
        {
            ClientUtils.addConnLog(LogType.Error, string.Concat("Execute Proc: ", sProcName),
                model.Message);
            throw new Exception(model.Message);
        }

        DataSet dataSet = new DataSet();
        if (string.IsNullOrEmpty(model.Message))
            return dataSet;

        List<StoredProcedureParameter> ret
            = JsonConvert.DeserializeObject<List<StoredProcedureParameter>>(model.Message);

        DataTable dataTable = new DataTable();
        DataTable dtCur = new DataTable();
        DataSet dsCur = new DataSet();
        DataRow dataRow = dataTable.NewRow();
        foreach (var r in ret)
        {
            if (r.Direction == OraDirection.Output && r.Cursor != null && r.Cursor.Tables.Count > 0)
            {

                dtCur = new DataTable();
                //���ڴ˴��Ὣ�����dt������,��Ϊds��ֵ�����dt�������
                //dataSet.Tables.Add(dtCur = r.Cursor.Tables[0].Copy());
                dtCur = r.Cursor.Tables[0].Copy();
                dtCur.TableName = r.Name;
                dsCur.Tables.Add(dtCur);
            }
            dataTable.Columns.Add(r.Name, typeof(string));
            //  dataRow[r.Name] = r.Value.ToString().ToUpper().Equals("NULL") ? "" : r.Value.ToString();
            dataRow[r.Name] = string.IsNullOrEmpty(r.Value) ? "" : r.Value.ToString().ToUpper().Equals("NULL") ? "" : r.Value.ToString();
        }
        dataTable.Rows.Add(dataRow);
        dataSet.Tables.Add(dataTable);
        if (dsCur.Tables.Count > 0)
        {
            foreach (DataTable dt in dsCur.Tables)
            {
                dataSet.Tables.Add(dt.Copy());
            }
        }
        return dataSet;
    }

    private static string transformSPP(object[][] Params)
    {
        //StoredProcedureParameter[]
        if (Params == null)
            return null;
        List<StoredProcedureParameter> model = new List<StoredProcedureParameter>();
        foreach (var p in Params)
        {
            var p0 = (ParameterDirection)p[0];
            //OracleType => OracleDBType modify by franky 2020/3/19
            var p1 = (OracleDbType)p[1];
            var p2 = (string)p[2];
            var p3 = p[3] == null ? "" : p[3].ToString();
            model.Add(new StoredProcedureParameter
            {
                Direction = (OraDirection)((int)p0),
                Size = 200,
                Name = p2,
                Value = p3,
                DbType = p1 == OracleDbType.Int16 ? OraDbType.Int16 : p1 == OracleDbType.Int32 ? OraDbType.Int32 : p1 == OracleDbType.RefCursor ? OraDbType.RefCursor : OraDbType.Varchar2
            });
        }
        return JsonConvert.SerializeObject(model);
    }

    public static DataSet ExecuteSQL(string sSQL)
    {
        return Connect().ExecuteSQL(sSQL);
    }

    public static DataSet ExecuteSQL(string sSQL, object[][] Params)
    {
        var model = getParameter(Params);
        try
        {
            DataSet ds = Connect().ExecuteParameterSQL(sSQL, model);
            return ds;
        }
        catch (Exception ex)
        {
            ClientUtils.addConnLog(LogType.Error, string.Concat("Execute SQL: ", sSQL), ex.Message);
            throw ex;
        }
    }

    public static DataSet ExecuteSQL(string ModuleName, string DataSetName, string sSQL, bool IsCursor, string sCurProject)
    {
        return Connect().ExecuteSQL(sSQL);
    }

    public static string ExportFile(string sAction, string sType, string sData, string iParam)
    {

        return Connect().ExportFile(sAction, sType, sData, iParam);
    }

    public static int GetBroadCast()
    {
        int num = 8086;
        try
        {
            DataSet dataSet = new DataSet();
            if (File.Exists(string.Concat(Application.StartupPath, Path.DirectorySeparatorChar, "PPS.xml")))
            {
                dataSet.ReadXml(string.Concat(Application.StartupPath, Path.DirectorySeparatorChar, "PPS.xml"));
                if (dataSet.Tables["BroadCast"] != null)
                {
                    int.TryParse(dataSet.Tables["BroadCast"].Rows[0]["Port"].ToString(), out num);
                }
            }
            dataSet.Dispose();
        }
        catch
        {
        }
        return num;
    }

    public static DataSet GetData(string sSQL, object[][] Params, string iParam)
    {

        return Connect().GetData(sSQL, Params, iParam);
    }

    public static string GetDmpList(string sType, string iParam)
    {

        return Connect().GetDmpList(sType, iParam);
    }

    public static List<FileObject> GetFileList(string program)
    {

        return Connect().GetFileList(program);
    }

    public static DataSet GetFunction(string program, string webflag)
    {

        var model = Connect().GetFunction(program, ClientUtils.UserPara1, webflag, ClientUtils.fClientLang.ToUpper());
        if (!model.IsSuccess)
        {
            ClientUtils.addConnLog(LogType.Error, "Get Function", model.Message);
            throw new Exception(model.Message);
        }
        return model.DS;
    }

    public static DataSet GetFunction(string program, string webflag, string fClientLang, string userID)
    {

        var model = Connect().GetFunction(program, userID, webflag, fClientLang.ToUpper());
        if (!model.IsSuccess)
        {
            ClientUtils.addConnLog(LogType.Error, "Get Function", model.Message);
            throw new Exception(model.Message);
        }
        return model.DS;
    }

    public static string GetLogFile(string sFile, string iParam)
    {

        return Connect().GetLogFile(sFile, iParam);
    }

    public static string GetLogList(string sType, string iParam)
    {

        return Connect().GetLogList(sType, iParam);
    }

    public static string GetOrderField(string moduleName, string fileName)
    {
        //return Localization.GetOrderField(fileName, moduleName);
        return "";

    }

    public static int GetPrivilege(string userID, string function, string programName)
    {

        var model = Connect().CHK_PRIVILEGE(userID, programName, function);
        if (!model.IsSuccess)
        {
            ClientUtils.addConnLog(LogType.Error, "Get Privilege", model.Message);
            throw new Exception(model.Message);
        }
        return Convert.ToInt32(model.Message);
    }

    public static DataSet GetProgram(string webflag)
    {

        ClientUtils.fClientSystem = webflag;
        var model = Connect().GetProgram(ClientUtils.UserPara1, webflag, ClientUtils.fClientLang);
        if (!model.IsSuccess)
        {
            ClientUtils.addConnLog(LogType.Error, "Get Program", model.Message);
            throw new Exception(model.Message);
        }
        return model.DS;
    }

    public static DataSet GetProgram(string webflag, string fClientLang, string sUserID)
    {

        ClientUtils.fClientSystem = webflag;
        var model = Connect().GetProgram(sUserID, webflag, fClientLang);
        if (!model.IsSuccess)
        {
            ClientUtils.addConnLog(LogType.Error, "Get Program", model.Message);
            throw new Exception(model.Message);
        }
        return model.DS;
    }

    public static DataSet GetSysBaseData(string program, string paramName)
    {

        object[] sysBaseData = Connect().GetSysBaseData(program, paramName);
        if ((int)sysBaseData[0] != 0)
        {
            ClientUtils.addConnLog(LogType.Error, "Get Sys Base Data", (string)sysBaseData[1]);
            throw new Exception((string)sysBaseData[1]);
        }
        return (DataSet)sysBaseData[1];
    }

    public static DateTime GetSysDate()
    {

        return Connect().GetSysData();
    }

    public static string GetValue(string section, string field, string sValue)
    {
        string str;
        string str1 = string.Concat(Application.StartupPath, Path.DirectorySeparatorChar, "PPS.xml");
        if (!File.Exists(str1))
        {
            str = sValue;
        }
        else
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(str1);
            XmlNode xmlNodes = xmlDocument.SelectSingleNode(string.Concat("//", section));
            if (xmlNodes != null)
            {
                str = (xmlNodes.Attributes[field] != null ? xmlNodes.Attributes[field].Value : sValue);
            }
            else
            {
                str = sValue;
            }
        }
        return str;
    }

    public static string GetValue(string message, string moduleName, string fileName, out bool visible)
    {
        return Localization.GetValue(message, moduleName, fileName, out visible);
    }

    public static string ImportFile(int iFirst, string sFile, string iParam)
    {

        return Connect().ImportFile(iFirst, sFile, iParam);
    }

    public static bool IsNumber(string data)
    {
        int num;
        return int.TryParse(data, out num);
    }

    public static Image LoadImage(string sFileName)
    {
        Image image;
        if (!ClientUtils.bLoadImage)
        {
            image = null;
        }
        else
        {
            object[] directorySeparatorChar = new object[] { "Skin", Path.DirectorySeparatorChar, ClientUtils.GetValue("Setting", "Skin", "Default"), Path.DirectorySeparatorChar };
            string str = string.Concat(directorySeparatorChar);
            string str1 = string.Concat(Application.StartupPath, Path.DirectorySeparatorChar, str);
            if (!File.Exists(string.Concat(str1, sFileName)))
            {
                image = null;
            }
            else
            {
                image = Image.FromFile(string.Concat(str1, sFileName));
            }
        }
        return image;
    }

    public static string MoveLogFile(string sFile, string sStatus, string iParam)
    {

        return Connect().MoveLogFile(sFile, sStatus, iParam);
    }

    //public static Dictionary<string, List<object[,]>> Object_DataReader(int iMax, Dictionary<int, object[]> dc, string sSQL, object[][] Params, int iTotal, int iColumn, Dictionary<int, object[]> dc2, string sSQL2, object[][] Params2, int iTotal2, int iColumn2, bool bMasterDetail)
    //{
    //    Dictionary<string, List<object[,]>> strs = Connect().Object_DataReader(iMax, dc, sSQL, Params, iTotal, iColumn, dc2, sSQL2, Params2, iTotal2, iColumn2, bMasterDetail);
    //    return strs;
    //}

    protected static void OnErrorOccur(object sender, ThreadExceptionEventArgs ex)
    {
        Exception exception = ex.Exception;
        if (ClientUtils.fClientSystem.ToUpper() == "Y")
        {
            throw exception;
        }
        if ((!(exception is TargetInvocationException) ? false : exception.InnerException != null))
        {
            exception = exception.InnerException;
        }
        fError _fError = new fError(exception.Message, exception.StackTrace);
        _fError.ShowDialog();
        _fError.Dispose();
        ClientUtils.addConnLog(LogType.Error, string.Concat("Error Occur: ", exception.Message), exception.StackTrace);
    }

    public static bool refreshStoredProcedureParam(string sProcedureName, ref System.Data.OracleClient.OracleParameter[] f_Parameter)
    {

        return Connect().refreshStoredProcedureParam(sProcedureName, ref f_Parameter);
    }

    public static int RowCount(string sSQL, object[] param)
    {
        throw new Exception("Not Implement");

    }

    public static int RowCount(string sSQL, object[][] param)
    {
        var model = getParameter(param);
        return Connect().RowCount(sSQL, model);
    }

    public static void SetFont(System.Windows.Forms.Control ctrl)
    {
        Localization.SetFont(ctrl);
    }

    public static string SetLanguage(string sMessage, string moduleName, string xmlFileName)
    {
        return Localization.SetLanguage(sMessage, xmlFileName, moduleName, Application.StartupPath);
    }

    public static void SetLanguage(System.Windows.Forms.Control ctrl, string moduleName, string xmlFileName)
    {
        if (!(ctrl.GetType().Name != "ContextMenuStrip"))
        {
            //Localization.SetLanguage((ContextMenuStrip)ctrl, xmlFileName, moduleName, ClientUtils.bChangeFont);
        }
        else
        {
            //Localization.SetLanguage(ctrl, xmlFileName, moduleName, ClientUtils.bChangeFont);
        }
    }

    public static void SetLanguage(System.Windows.Forms.Control ctrl, string moduleName)
    {
        string str = ctrl.ToString();
        char[] chrArray = new char[] { '.', ',' };
        string[] strArrays = str.Split(chrArray);
        if ((!ClientUtils.bChangeFont ? true : string.IsNullOrEmpty(moduleName)))
        {
            //Localization.SetLanguage(ctrl, strArrays[0], moduleName, false);
        }
        else
        {
            //Localization.SetLanguage(ctrl, strArrays[0], moduleName, true);
        }
    }

    internal static void SetValue(string Node, string sField, string Value)
    {
        string str = string.Concat(Application.StartupPath, Path.DirectorySeparatorChar, "PPS.xml");
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(str);
        XmlNode documentElement = xmlDocument.SelectSingleNode(string.Concat("//", Node));
        if (documentElement == null)
        {
            documentElement = xmlDocument.DocumentElement;
            XmlElement xmlElement = xmlDocument.CreateElement(Node);
            xmlElement.SetAttribute(sField, Value);
            documentElement.AppendChild(xmlElement);
        }
        else if (documentElement.Attributes[sField] != null)
        {
            documentElement.Attributes[sField].Value = Value;
        }
        else
        {
            XmlAttribute itemOf = documentElement.Attributes[sField];
            itemOf = documentElement.OwnerDocument.CreateAttribute(sField);
            itemOf.Value = Value;
            documentElement.Attributes.Append(itemOf);
        }
        xmlDocument.Save(str);
    }

    public static DialogResult ShowMessage(string message, int iType, string moduleName, string fileName)
    {
        return Localization.ShowMessage(message, iType, fileName, moduleName);
    }

    public static DialogResult ShowMessage(string message, int iType)
    {
        return Localization.ShowMessage(message, iType, "", "");
    }

    public static DataTable SupplyPageOfData(string sSQL, object[] param, int lowerPageBoundary, int rowsPerPage)
    {

        return Connect().SupplyPageOfData(sSQL, param, lowerPageBoundary, rowsPerPage);
    }

    //public static DataTable SupplyPageOfData(string sSQL, object[] param, int lowerPageBoundary, int rowsPerPage, ref int rowCountValue, ref DataTable dt)
    //{

    //    DataTable dataTable = Connect().SupplyPageOfData(sSQL, param, lowerPageBoundary, rowsPerPage, ref rowCountValue, ref dt);
    //    return dataTable;
    //}

    public static DataTable SupplyPageOfData(string sSQL, object[][] param, int lowerPageBoundary, int rowsPerPage)
    {

        return Connect().SupplyPageOfData(sSQL, param, lowerPageBoundary, rowsPerPage);
    }

    //public static DataTable SupplyPageOfData(string sSQL, object[][] param, int lowerPageBoundary, int rowsPerPage, ref int rowCountValue, ref DataTable dt)
    //{

    //    DataTable dataTable = Connect().SupplyPageOfData(sSQL, param, lowerPageBoundary, rowsPerPage, ref rowCountValue, ref dt);
    //    return dataTable;
    //}


    public static void WebLanguage(System.Web.UI.Control ctrl, string moduleName, string xmlFileName, string sLanguage)
    {
        Web.SetLanguage(ctrl, xmlFileName, moduleName, ClientUtils.fPath, sLanguage);
    }

    public static void WebLanguage(System.Web.UI.Control ctrl, string moduleName, string xmlFileName)
    {
        Web.SetLanguage(ctrl, xmlFileName, moduleName, ClientUtils.fPath, ClientUtils.cultureName);
    }

    public static string WebMessage(string sMessage, string moduleName, string xmlFileName, string sLanguage)
    {
        string str = Web.SetLanguage(sMessage, xmlFileName, moduleName, ClientUtils.fPath, sLanguage);
        return str;
    }

    public static string WebMessage(string sMessage, string moduleName, string xmlFileName)
    {
        string str = Web.SetLanguage(sMessage, xmlFileName, moduleName, ClientUtils.fPath, ClientUtils.cultureName);
        return str;
    }
    public static void Logout()
    {
        Connect().Logout(ClientUtils.fComputer, ClientUtils.fLoginUser);
    }
    public static void Logout(string Name)
    {
        Connect().Logout(Name, ClientUtils.fLoginUser);
    }

    public static List<FileObject> GetFileLists(string program)
    {
        return Connect().GetFileLists(program);
    }
    public static byte[] DownloadFileByte(string program, string fromFile)
    {
        return Connect().DownloadFileByte(program, fromFile);
    }


    public static int GetCount()
    {
        return Connect().GetCount();
    }

    private static string _serverUrl { get; set; }
    public static string ServerUrl { get { return _serverUrl; } set { _serverUrl = value; } }

    private static Dictionary<string, object> getParameter(object[][] param)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        foreach (var p in param)
        {
            if (model.ContainsKey((string)p[2]))
            {
                model[(string)p[2]] = p[3];
            }
            else
            {
                model.Add((string)p[2], p[3]);
            }
        }
        return model;
    }

    public static void WritePrintLabelLog(string strLabelType, string strCartonNo, string strPalletNo, string strMacAddress)
    {
        object[][] procParams = new object[6][];
        procParams[0] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "p_LABEL_TYPE", strLabelType };
        procParams[1] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "p_CARTON_NO", strCartonNo };
        procParams[2] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "p_PALLET_NO", strPalletNo };
        procParams[3] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "p_EMP_ID", ClientUtils.fLoginUser };
        procParams[4] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "p_IP_ADDRESS", ClientUtils.IPAddressV4 };
        procParams[5] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "p_MAC_ADDRESS", strMacAddress };
        //procParams[6] = new object[] { ParameterDirection.Input, OracleDbType.Varchar2, "p_PRINT_TYPE", strPRINT_TYPE };
        //DataTable dt = new DataTable();
        DataSet ds = new DataSet();
       ds=ClientUtils.ExecuteProc("ppsuser.p_print_Label_log", procParams);
    }
}


public class LanguageModel
{
    public string LANGCODE { get; set; }
    public string CN_DESC { get; set; }
    public string EN_DESC { get; set; }
    public string VN_DESC { get; set; }
    public string REMARK { get; set; }
    public string LANGFUNC { get; set; }
}
