using MESModel;
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.ServiceModel;

namespace RemoteService
{
	[ServiceContract]
	public interface IRemoteServiceObject
	{
		[OperationContract]
		DALModel CheckEmployee(string employeeNo, string password, string ComputerName, int Port, out string empId, out string empName);

		[OperationContract]
		bool CheckImport(string sType, string sValue, string iParam);

		[OperationContract]
		void CheckList(string ComputerName, string employeeNo, string employeeName, int Port);

		[OperationContract]
		DALModel CHK_PRIVILEGE(string EmpId, string PRG, string Fun);

		[OperationContract]
		FileStream DownloadFile(string program, string fileName);

		[OperationContract]
		byte[] DownloadFileByte(string program, string fileName);

		[OperationContract]
		byte[] Excel_DataReader(string sComputer, string sReportID, int iMax, Dictionary<int, object[]> dc, string sSQL, object[][] Params, int iTotal, int iColumn, Dictionary<int, object[]> dc2, string sSQL2, object[][] Params2, int iTotal2, int iColumn2, bool bMasterDetail);

		[OperationContract]
		object[] EXECUTE_COMMAND(object[] commandText);

		[OperationContract]
		object[] EXECUTE_PROC(object[] commandText);

		[OperationContract]
		object[] ExecuteBatch(object[][] dt, string sTable, string[] sFields, string[] sTypes);

		[OperationContract]
		object[] ExecuteProcCommand(object[] commandText);

		[OperationContract]
		DALModel ExecuteProc(string sProcName, string Params);

		[OperationContract]
		object[] ExecuteSQLCommand(object[] commandText);

		[OperationContract]
		DataSet ExecuteSQL(string sSQL);

		[OperationContract]
		DataSet ExecuteParameterSQL(string sSQL, Dictionary<string, object> Param);

		[OperationContract]
		string ExportFile(string sAction, string sType, string sData, string iParam);

		[OperationContract]
		int GetCount();

		[OperationContract]
		DataSet GetData(string sSQL, object[][] Params, string iParam);

		[OperationContract]
		string GetDmpList(string sType, string iParam);

		[OperationContract]
		List<FileObject> GetFileList(string program);

		[OperationContract]
		List<FileObject> GetFileLists(string program);

		[OperationContract]
		DALModel GetFunction(string program, string empID, string webflag, string cultureName);

		[OperationContract]
		string GetLogFile(string sFile, string iParam);

		[OperationContract]
		string GetLogList(string sType, string iParam);

		[OperationContract]
		DALModel GetProgram(string empId, string webFlag, string cultureName);

		[OperationContract]
		object[] GetSysBaseData(string sProgram, string sParamName);

		[OperationContract]
		DateTime GetSysData();

		[OperationContract]
		string GetUserList();

		[OperationContract]
		string ImportFile(int iFirst, string sFile, string iParam);

		[OperationContract]
		void Logout(string ComputerName, string employeeNo);

		[OperationContract]
		string MoveLogFile(string sFile, string sStatus, string iParam);

		[OperationContract]
		bool refreshStoredProcedureParam(string sProcedureName, ref OracleParameter[] f_Parameter);

		[OperationContract]
		int RowCount(string sSQL, object[] param);

		[OperationContract]
		bool RsvFromRemote(string f_ComputerName, int f_iCommandNo, int f_isubCommandNo, string f_pData, ref string f_sResult);

		[OperationContract]
		DataTable SupplyPageOfData(string sSQL, object[] param, int lowerPageBoundary, int rowsPerPage);

		[OperationContract]
		string TestConnection(string connection);
	}
}
