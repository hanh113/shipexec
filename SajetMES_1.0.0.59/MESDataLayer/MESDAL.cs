using Dapper;
using MESModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESDataLayer
{
    public class MESDAL : DbDapperExtension
    {
        private static string conn {
            get {
                return
                    string.Format("User Id={0};password={1};{2}",
                    SysInfo.DBLogin.DBUser,
                    SysInfo.DBLogin.DBPwd,
                    ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            }
        }
        private string g_sPath = AppDomain.CurrentDomain.BaseDirectory;
        public MESDAL()
             : base(conn)
        {
        }

        public static void addConnLog(LogType f_LogType, string f_sFunction, string f_sMessage)
        {
            classChromaLog.addLog(f_LogType, f_sFunction, f_sMessage);
        }

        public DALModel CheckEmployee(string employeeNo, string password, out string empId, out string empName)
        {
            DALModel retModel= new DALModel { };
            try
            {
                var p = new DynamicParameters();
                p.Add("@TEMPNO", employeeNo);
                p.Add("@TEMPPWD", password);
                p.Add("@TRES", dbType: DbType.String, direction: ParameterDirection.Output, size: 25);
                p.Add("@TEMPID", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@TEMPNAME", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                DbCon.Execute("SAJET.LOGIN", p, commandType: CommandType.StoredProcedure);
                

                string retMsg = p.Get<string>("@TRES");

                retModel.IsSuccess = retMsg == "OK";
                retModel.Message = retMsg;
                empId = p.Get<string>("@TEMPID");
                empName = p.Get<string>("@TEMPNAME");
            }
            catch (Exception ex)
            {
                classChromaLog.addLog(LogType.Error, "CHK_PRIVILEGE", ex.Message.ToString());
                throw ex;
            }
            return retModel;
        }

        public DALModel CHK_PRIVILEGE(string EmpId, string PRG, string Fun)
        {
            DALModel model = new DALModel { };
            try
            {
                var p = new DynamicParameters();
                p.Add("@EMPID", EmpId);
                p.Add("@PRG", PRG);
                p.Add("@FUN", Fun);

                p.Add("@TRES", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                p.Add("@PRIVILEGE", dbType: DbType.String, direction: ParameterDirection.Output, size: 5);

                DbCon.Execute("SAJET.SJ_CHK_PRG_PRIVILEGE", p, commandType: CommandType.StoredProcedure);
                string ret = p.Get<string>("@TRES");
                string retMsg = p.Get<string>("@PRIVILEGE");
                model.IsSuccess = ret == "OK";
                model.Message = retMsg;
 
            }
            catch (Exception ex)
            {
                classChromaLog.addLog(LogType.Error, "CHK_PRIVILEGE", ex.Message.ToString());
                throw ex;
            }
            return model;
        }

        public DALModel GetProgram(string empId, string webFlag, string cultureName)
        {
            DALModel model = new DALModel { };

            try
            {
                string str1 = string.Concat("select param_value from sajet.sys_base_param where program = 'ALL' and upper(param_name) = '", cultureName, "'");
                string str2 = this.GetSingleData(str1);
                str2 = (string.IsNullOrEmpty(str2) ? "C.PROGRAM SHOWNAME" : string.Concat("nvl(C.PROGRAM_", str2, ", C.PROGRAM) SHOWNAME"));
                string[] strArrays = new string[] { "Select distinct ", str2, ", EXE_FILENAME, C.PROGRAM, C.FUN_TYPE_IDX From SAJET.SYS_ROLE_PRIVILEGE A, SAJET.SYS_ROLE_EMP B, SAJET.SYS_PROGRAM_NAME C, SAJET.SYS_PROGRAM_FUN_NAME D Where A.ROLE_ID = B.ROLE_ID and B.EMP_ID = ", empId, " and A.PROGRAM = C.PROGRAM and A.PROGRAM = D.PROGRAM AND A.FUNCTION = D.FUNCTION AND D.WEB_FLAG = '", webFlag, "' AND D.DLL_FILENAME IS NOT NULL AND C.ENABLED = 'Y' AND D.ENABLED = 'Y' union Select ", str2, ", EXE_FILENAME, C.PROGRAM, C.FUN_TYPE_IDX From SAJET.SYS_EMP_PRIVILEGE A, SAJET.SYS_PROGRAM_NAME C, SAJET.SYS_PROGRAM_FUN_NAME D Where A.PROGRAM = C.PROGRAM and A.EMP_ID = ", empId, " AND A.FUNCTION = D.FUNCTION AND D.WEB_FLAG = '", webFlag, "' AND D.DLL_FILENAME IS NOT NULL AND C.ENABLED = 'Y' AND D.ENABLED = 'Y' union select distinct ", str2, ", EXE_FILENAME, C.PROGRAM, C.FUN_TYPE_IDX from sajet.sys_program_name C, sajet.sys_base_param e where c.program_type = '2' and c.enabled = 'Y' and c.program = e.program and exists (select distinct b.REPORT_TYPE_ID from sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d where b.enabled = 'Y' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID minus select distinct b.REPORT_TYPE_ID from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d where a.report_type_id = b.report_type_id and b.enabled = 'Y' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and exception_flag = 0 minus select distinct b.REPORT_TYPE_ID from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, sajet.sys_role c where a.report_type_id = b.report_type_id and b.enabled = 'Y' and a.role_id = c.role_id(+) and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and a.role_id in (select role_id from sajet.sys_role_emp where emp_id= ", empId, ") and exception_flag = 1 minus select distinct b.REPORT_TYPE_ID from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d where a.report_id = b.report_id and b.enabled = 'Y' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and exception_flag = 0 minus select distinct b.REPORT_TYPE_ID from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, sajet.sys_role c where a.report_id = b.report_id and b.enabled = 'Y' and a.role_id = c.role_id(+) and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and a.role_id in (select role_id from sajet.sys_role_emp where emp_id= ", empId, ") and exception_flag = 1 union select distinct b.REPORT_TYPE_ID from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, sajet.sys_role c where a.report_type_id(+) = b.report_type_id and b.enabled = 'Y' and a.role_id = c.role_id(+) and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and a.role_id in (select role_id from sajet.sys_role_emp where emp_id= ", empId, ") and exception_flag = 0 union select distinct b.REPORT_TYPE_ID from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, sajet.sys_role c where a.report_id(+) = b.report_id and b.enabled = 'Y' and a.role_id = c.role_id(+) and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and a.role_id in (select role_id from sajet.sys_role_emp where emp_id= ", empId, ") and exception_flag = 0) Order by FUN_TYPE_IDX, SHOWNAME" };
                string sql = string.Join("", strArrays);
                DataSet dataSet = this.ExecuteQuery(sql);
                model.IsSuccess = true;
                model.DS = dataSet;
            }
            catch (Exception ex) {
                classChromaLog.addLog(LogType.Error, "GetProgram", ex.Message.ToString());
                model.Message = ex.Message.ToString();
            }
            return model;
        }
        public DALModel GetFunction(string program, string empId, string webflag, string cultureName)
        {
            DALModel model = new DALModel { };
            try {
                //str2 = sql
                string sql = string.Format("SELECT PROGRAM_TYPE FROM SAJET.SYS_PROGRAM_NAME WHERE PROGRAM = '{0}'",
                    program);
                //str3 = program_type
                string program_type = this.GetSingleData(sql);
                sql = string.Format(@"select param_value from sajet.sys_base_param 
                        where program = 'ALL' and upper(param_name) = '{0}'", cultureName);

                //str4  = param_value
                string param_value = this.GetSingleData(sql);

                string str = "";
                string[] strArrays;
                if (string.IsNullOrEmpty(param_value))
                {
                    str = "d.REPORT_TYPE_NAME FUN_TYPE, b.REPORT_NAME SHOWNAME";
                    param_value = "C.FUN_TYPE, C.FUNCTION SHOWNAME";
                }
                else
                {
                    strArrays = new string[] { "nvl(d.REPORT_TYPE_", param_value, ",d.REPORT_TYPE_NAME) FUN_TYPE, nvl(b.REPORT_", param_value, ",b.REPORT_NAME) SHOWNAME" };
                    str = string.Concat(strArrays);
                    strArrays = new string[] { "nvl(C.FUN_TYPE_", param_value, ",C.FUN_TYPE) FUN_TYPE, nvl(C.FUN_", param_value, ",C.FUNCTION) SHOWNAME" };
                    param_value = string.Concat(strArrays);
                }

                if (!string.IsNullOrEmpty(program_type))
                {
                    if (program_type == "1")
                        sql = string.Concat("Select RE_TYPE, RE_NAME,DLL_FILENAME, RP_ID, 'fMain' From sajet.SYS_REPORT_NAME where (EMP_ID = 0 or EMP_ID = ", empId, ") and ENABLED = 'Y' Order By RP_TYPE_IDX, RP_TYPE, RP_ID, RP_NAME, GROUP_FLAG");
                    else
                    {
                        if (program_type != "2")
                        {
                            if (!string.IsNullOrEmpty(empId))
                            {
                                strArrays = new string[] { "Select distinct ", param_value, ",C.DLL_FILENAME,C.FUN_PARAM, C.FORM_NAME, C.FUNCTION, C.FUN_TYPE_IDX FUN_TYPE_IDX, C.FUN_IDX FUN_IDX From SAJET.SYS_ROLE_PRIVILEGE A, SAJET.SYS_ROLE_EMP B, SAJET.SYS_PROGRAM_FUN_NAME C Where A.ROLE_ID = B.ROLE_ID and B.EMP_ID = ", empId, " and A.PROGRAM = '", program, "' and A.PROGRAM = C.PROGRAM and A.FUNCTION = C.FUNCTION AND C.DLL_FILENAME IS NOT NULL and c.ENABLED = 'Y' and web_flag = '", webflag, "' Order By FUN_TYPE_IDX, FUN_IDX" };
                                sql = string.Concat(strArrays);
                            }
                            else
                            {
                                strArrays = new string[] { "Select distinct ", param_value, ",C.DLL_FILENAME,C.FUN_PARAM, C.FORM_NAME, C.FUNCTION, C.FUN_TYPE_IDX FUN_TYPE_IDX, C.FUN_IDX FUN_IDX From SAJET.SYS_PROGRAM_FUN_NAME C Where PROGRAM = '", program, "' AND C.DLL_FILENAME IS NOT NULL and c.ENABLED = 'Y' and web_flag = '", webflag, "' Order By FUN_TYPE_IDX, FUN_IDX" };
                                sql = string.Concat(strArrays);
                            }
                        }
                        string str6 = "0";
                        sql = string.Concat("SELECT NVL(MAX(FUN_TYPE_IDX), 0) FROM SAJET.SYS_PROGRAM_FUN_NAME WHERE PROGRAM = '", program, "'");

                        var stmp = this.GetSingleData(sql);
                        str6 = string.IsNullOrEmpty(stmp) ? str6 : stmp;

                        strArrays = new string[] { "Select distinct ", param_value, ",C.DLL_FILENAME,C.FUN_PARAM, C.FORM_NAME, C.FUNCTION, C.FUN_TYPE_IDX FUN_TYPE_IDX, C.FUN_IDX FUN_IDX From SAJET.SYS_ROLE_PRIVILEGE A, SAJET.SYS_ROLE_EMP B, SAJET.SYS_PROGRAM_FUN_NAME C Where A.ROLE_ID = B.ROLE_ID and B.EMP_ID = ", empId, " and A.PROGRAM = '", program, "' and A.PROGRAM = C.PROGRAM and A.FUNCTION = C.FUNCTION and c.ENABLED = 'Y' and web_flag = '", webflag, "' AND C.DLL_FILENAME IS NOT NULL union select distinct ", str, ", param_value, to_char(b.REPORT_ID), 'fMain', B.REPORT_NAME, D.DISPLAY_IDX+", str6, " FUN_TYPE_IDX, B.DISPLAY_IDX FUN_IDX from sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, SAJET.SYS_BASE_PARAM E where b.enabled = 'Y' AND E.PROGRAM = '", program, "' and E.PARAM_NAME = '", webflag, "' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID minus select distinct ", str, ", param_value, to_char(b.REPORT_ID), 'fMain', B.REPORT_NAME, D.DISPLAY_IDX+", str6, " FUN_TYPE_IDX, B.DISPLAY_IDX FUN_IDX from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, SAJET.SYS_BASE_PARAM E where a.report_type_id = b.report_type_id and b.enabled = 'Y' AND E.PROGRAM = '", program, "' and E.PARAM_NAME = '", webflag, "' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and exception_flag = 0 MINUS select distinct ", str, ", param_value, to_char(b.REPORT_ID), 'fMain', B.REPORT_NAME, D.DISPLAY_IDX+", str6, " FUN_TYPE_IDX, B.DISPLAY_IDX FUN_IDX from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, sajet.sys_role c, SAJET.SYS_BASE_PARAM E where a.report_type_id = b.report_type_id and b.enabled = 'Y' and a.role_id = c.role_id(+) AND E.PROGRAM = '", program, "' and E.PARAM_NAME = '", webflag, "' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and a.role_id in (select role_id from sajet.sys_role_emp where emp_id= ", empId, ") and exception_flag = 1 minus select distinct ", str, ", param_value, to_char(b.REPORT_ID), 'fMain', B.REPORT_NAME, D.DISPLAY_IDX+", str6, " FUN_TYPE_IDX, B.DISPLAY_IDX FUN_IDX from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, SAJET.SYS_BASE_PARAM E where a.report_id = b.report_id and b.enabled = 'Y' AND E.PROGRAM = '", program, "' and E.PARAM_NAME = '", webflag, "' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and exception_flag = 0 MINUS select distinct ", str, ", param_value, to_char(b.REPORT_ID), 'fMain', B.REPORT_NAME, D.DISPLAY_IDX+", str6, " FUN_TYPE_IDX, B.DISPLAY_IDX FUN_IDX from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, sajet.sys_role c, SAJET.SYS_BASE_PARAM E where a.report_id = b.report_id and b.enabled = 'Y' and a.role_id = c.role_id(+) AND E.PROGRAM = '", program, "' and E.PARAM_NAME = '", webflag, "' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and a.role_id in (select role_id from sajet.sys_role_emp where emp_id= ", empId, ") and exception_flag = 1 union select distinct ", str, ", param_value, to_char(b.REPORT_ID), 'fMain', B.REPORT_NAME, D.DISPLAY_IDX+", str6, " FUN_TYPE_IDX, B.DISPLAY_IDX FUN_IDX from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, sajet.sys_role c, SAJET.SYS_BASE_PARAM E where a.report_type_id(+) = b.report_type_id and b.enabled = 'Y' and a.role_id = c.role_id(+) AND E.PROGRAM = '", program, "' and E.PARAM_NAME = '", webflag, "' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and a.role_id in (select role_id from sajet.sys_role_emp where emp_id= ", empId, ") and exception_flag = 0 union select distinct ", str, ", param_value, to_char(b.REPORT_ID), 'fMain', B.REPORT_NAME, D.DISPLAY_IDX+", str6, " FUN_TYPE_IDX, B.DISPLAY_IDX FUN_IDX from sajet.sys_cust_report_privilege a, sajet.sys_cust_report b, sajet.SYS_CUST_REPORT_TYPE d, sajet.sys_role c, SAJET.SYS_BASE_PARAM E where a.report_id(+) = b.report_id and b.enabled = 'Y' and a.role_id = c.role_id(+) AND E.PROGRAM = '", program, "' and E.PARAM_NAME = '", webflag, "' and b.REPORT_TYPE_ID = d.REPORT_TYPE_ID and a.role_id in (select role_id from sajet.sys_role_emp where emp_id= ", empId, ") and exception_flag = 0 Order By FUN_TYPE_IDX,FUN_IDX,FUN_TYPE,SHOWNAME " };
                        sql = string.Concat(strArrays);
                    }
                }

                DataSet dataSet = this.ExecuteQuery(sql);
                model.IsSuccess = true;
                model.DS = dataSet;

            }
            catch (Exception ex) {
                classChromaLog.addLog(LogType.Error, "GetProgram", ex.Message.ToString());
                model.Message = ex.Message.ToString();
            }
            return model;
        }

    }
}
