using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CreatePPSDataPpart
{
    class CommClass
    {
        HShippingLabel.PrintLabel _printlabel;

        /// <summary>
        /// 原料KPSN投入工单
        /// </summary>
        /// <param name="sn">原料KPSN</param>
        /// <param name="wo">工单</param>
        /// <returns>OK true</returns>
        public string SN_InsertWo2(string sn, string csn, string wo, string partid, string cartonno, string locationid, string locationNo, string palletno, string strdn, string strdnline, string strcarlineno, string strPoint, string strCoo)
        {
            string sqlstr2 = @"INSERT INTO sajet.g_sn_coo (serial_number, carton_no, part_no, coo, create_date, customer_sn)
              select :serial_number,
           :CARTON_NO,
         ( select part_no from sajet.sys_part where  part_id =:part_id and rownum=1)  as part_no,
          :coo ,
           SYSDATE UDT,
           :CUSTOMER_SN
            from dual";
            object[][] sqlParams2 = new object[][]{
            new object[] { ParameterDirection.Input, OracleType.VarChar, "SERIAL_NUMBER", sn },
            new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", cartonno },
            new object[] { ParameterDirection.Input, OracleType.VarChar, "part_id", partid },
            new object[] { ParameterDirection.Input, OracleType.VarChar, "CUSTOMER_SN", csn },
            new object[] { ParameterDirection.Input, OracleType.VarChar, "coo", strCoo }
            };
            ClientUtils.ExecuteSQL(sqlstr2, sqlParams2);

            object[][] procParams = new object[13][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "sn", sn };
            procParams[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "csn", csn };
            procParams[2] = new object[] { ParameterDirection.Input, OracleType.VarChar, "wo", wo };
            procParams[3] = new object[] { ParameterDirection.Input, OracleType.VarChar, "partid", partid };
            procParams[4] = new object[] { ParameterDirection.Input, OracleType.VarChar, "cartonno", cartonno };
            procParams[5] = new object[] { ParameterDirection.Input, OracleType.VarChar, "locationid", locationid };
            procParams[6] = new object[] { ParameterDirection.Input, OracleType.VarChar, "locationNo", locationNo };

            procParams[7] = new object[] { ParameterDirection.Input, OracleType.VarChar, "palletno", palletno };
            procParams[8] = new object[] { ParameterDirection.Input, OracleType.VarChar, "strdn", strdn };
            procParams[9] = new object[] { ParameterDirection.Input, OracleType.VarChar, "strdnline", strdnline };
            procParams[10] = new object[] { ParameterDirection.Input, OracleType.VarChar, "strcarlineno", strcarlineno };

            procParams[11] = new object[] { ParameterDirection.Input, OracleType.VarChar, "strPoint", strPoint };
            procParams[12] = new object[] { ParameterDirection.Output, OracleType.VarChar, "errmsg", "" };
            string steResult = string.Empty;
            try
            {
                DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_TEST_INSERTPPARTCSN_Single", procParams);
                steResult = ds.Tables[0].Rows[0]["errmsg"].ToString();
                return steResult;

            }
            catch (Exception ex)
            {
                MessageBox.Show(steResult + ex.Message);
                return steResult;
            }

        }
        public string SN_Trolley(string strcarlineno)
        {
            object[][] procParams = new object[2][];

            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "strcarlineno", strcarlineno };
            procParams[1] = new object[] { ParameterDirection.Output, OracleType.VarChar, "errmsg", "" };
            string steResult = string.Empty;
            try
            {
                DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_TEST_INSERTPPARTCSN_TROLLEY", procParams);
                steResult = ds.Tables[0].Rows[0]["errmsg"].ToString();
                return steResult;

            }
            catch (Exception ex)
            {
                MessageBox.Show(steResult + ex.Message);
                return steResult;
            }

        }
        public bool SN_InsertWo(string sn, string csn, string wo, string partid, string cartonno, string locationid, string PALLET_NO)
        {
            try
            {
                string sqlstr = @"INSERT INTO PPSUSER.G_SN_STATUS (WORK_ORDER,
                                   PART_ID,
                                   SERIAL_NUMBER,
                                   CARTON_NO,
                                   CONTAINER,
                                   RC_NO,OUT_PROCESS_TIME,CUSTOMER_SN,PALLET_NO)
         VALUES ( :WORK_ORDER,
                 :PART_ID,
                 :SERIAL_NUMBER,
                 :CARTON_NO,
                 '2017072800016',
                 :LOCATION_ID,SYSDATE,:CUSTOMER_SN,:PALLET_NO)";
                object[][] sqlParams = new object[][]{new object[] { ParameterDirection.Input, OracleType.VarChar, "WORK_ORDER", wo },
            new object[] { ParameterDirection.Input, OracleType.VarChar, "PART_ID", partid },
            new object[] { ParameterDirection.Input, OracleType.VarChar, "SERIAL_NUMBER", sn },
            new object[] { ParameterDirection.Input, OracleType.VarChar, "CARTON_NO", cartonno} ,
              new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATION_ID", locationid },
              new object[] { ParameterDirection.Input, OracleType.VarChar, "CUSTOMER_SN", csn },
                new object[] { ParameterDirection.Input, OracleType.VarChar, "PALLET_NO", PALLET_NO }
            };

                ClientUtils.ExecuteSQL(sqlstr, sqlParams);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public bool CreatSNListByCarton(string cartonno, string partid, string locationid, int qty, string palletNo)
        {


            for (int i = 1; i <= qty; i++)
            {
                string strCSN = string.Empty;
                //每次取一个序号


                if (!SN_InsertWo(cartonno + i.ToString().PadLeft(5, '0'), strCSN, cartonno.Substring(9), partid, cartonno, locationid, palletNo))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckPartNo(string partno, out string partid)
        {
            try
            {
                string sqlstr = "SELECT PART_ID FROM SAJET.SYS_PART WHERE PART_NO=:PART_NO AND ROWNUM=1";
                object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "PART_NO", partno } };
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    partid = dt.Rows[0]["PART_ID"].ToString();
                    return true;
                }
                else
                {
                    partid = string.Empty;
                    return false;

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); partid = string.Empty; return false; }
        }

        public bool ClearLocation(string locationid)
        {
            try
            {
                string sqlstr = "UPDATE PPSUSER.G_SN_STATUS SET RC_NO='N/A' WHERE RC_NO=:LOCATION_ID";
                object[][] sqlparams = new object[][] {
          new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATION_ID", locationid } };
                ClientUtils.ExecuteSQL(sqlstr, sqlparams);
                return true;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }


        public bool CheckLocationId(string locationno, out string locationid)
        {
            try
            {
                string sqlstr = "SELECT LOCATION_ID FROM SAJET.WMS_LOCATION WHERE LOCATION_NO=:LOCATION_NO AND ROWNUM=1";
                object[][] sqlparams = new object[][] { new object[] { ParameterDirection.Input, OracleType.VarChar, "LOCATION_NO", locationno } };
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr, sqlparams).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    locationid = dt.Rows[0]["LOCATION_ID"].ToString();
                    return true;
                }
                else
                {
                    locationid = string.Empty;
                    return false;

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); locationid = string.Empty; return false; }
        }

        public string GetSNRangeByProcedure(string strsntype, string strprefix, int qty, out int outqty)
        {
            string errmsg = string.Empty;
            object[][] procParams = new object[5][];
            procParams[0] = new object[] { ParameterDirection.Input, OracleType.VarChar, "strsntype", strsntype };
            procParams[1] = new object[] { ParameterDirection.Input, OracleType.VarChar, "strprefix", strprefix };
            procParams[2] = new object[] { ParameterDirection.Input, OracleType.Number, "qty", qty };
            procParams[3] = new object[] { ParameterDirection.Output, OracleType.Number, "outqty", 0 };
            procParams[4] = new object[] { ParameterDirection.Output, OracleType.VarChar, "errmsg", "" };
            //create or replace procedure ppsuser.SP_TEST_GETSNRANGE(strsntype in varchar2,
            //                                            strprefix in varchar2,
            //                                            qty       in number,
            //                                            outqty    out number,
            //                                            errmsg    out varchar2) as
            //  --qty是外面程序的需求数
            //  --outqty是当前用到数量值，外面使用需加1.
            DataSet ds = ClientUtils.ExecuteProc("PPSUSER.SP_TEST_GETSNRANGE", procParams);
            errmsg = ds.Tables[0].Rows[0]["errmsg"].ToString();
            outqty = int.Parse(ds.Tables[0].Rows[0]["outqty"].ToString());
            if (errmsg.Equals("OK"))
            {
                return "OK";
            }
            else
            {
                MessageBox.Show(errmsg);
                return "NG";
            }

        }

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="sn"></param>
        /// <returns></returns>
        public bool Print_DNLabel(string[] cartonsdata)
        {
            //StringBuilder printdata = new StringBuilder();
            string labelName = "SH_CartonLabel";
            string labelPath = Application.StartupPath + @"\" + labelName + ".btw";

            if (!File.Exists(labelPath))
            {

                MessageBox.Show("模板文件不存在" + labelPath);
                return false;

            }
            List<string> printdata = new List<string>();
            printdata = cartonsdata.ToList();
            try
            {

                string sMessage = string.Empty;
                if (_printlabel.Print_Bartender_DataSource(labelName, Path.GetFullPath(Application.StartupPath + @"\"), printdata, 1, out sMessage))
                {
                    return true;
                }

                MessageBox.Show(sMessage);
                return false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }

        }

        public string CheckSSCCSum(string a)
        {
            if (a.Length != 17)
            {
                MessageBox.Show("the serilNo is error!");
                System.Environment.Exit(0);
            }

            int EEnum = 0;
            int OOnum = 0;
            int i;

            for (i = 1; i <= a.Length; i++)
            {
                if (i % 2 == 1)
                { OOnum = OOnum + Convert.ToInt32(a.Substring(i - 1, 1)) * 3; }
                else
                { EEnum = EEnum + Convert.ToInt32(a.Substring(i - 1, 1)); }
            }

            if ((EEnum + OOnum) % 10 == 0)
                return "0";
            else
            {
                string s;
                s = (10 - (EEnum + OOnum) % 10).ToString();
                return s;
            }
        }


        public string getGTIN(string ictpartno, out string strUPC, out string strJAN, out string strCustModel)
        {
            //string strSQL = string.Format("SELECT  a.part_no ,decode(a.jan_code, '', a.upc, a.jan_code) as gtin  "
            //                              + "   FROM sajet.sys_part a "
            //                              + "  where a.part_no = '{0}' "
            //                                , ictpartno);
            string strSQL = string.Format("select a.ictpartno, "
                                      + "         a.upc_code, "
                                      + "         a.jan_code, "
                                      + "         a.custmodel, "
                                      + "         case "
                                      + "           when a.custmodel = 'B188' then "
                                      + "            decode(a.jan_code, '', a.upc_code, a.jan_code) "
                                      + "           when a.custmodel = 'B501' then "
                                      + "            decode(a.upc_code, '', a.jan_code, a.upc_code) "
                                      + "           else "
                                      + "            a.upc_code "
                                      + "         end GTIN "
                                      + "    from ppsuser.vw_mpn_info a "
                                      + "   where  (a.packcode <> subpackcode or a.subpackcode is null) and a.ictpartno = '{0}' ", ictpartno);

            DataTable dt0 = new DataTable();
            try
            {
                dt0 = ClientUtils.ExecuteSQL(strSQL).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                strUPC = string.Empty;
                strJAN = string.Empty;
                strCustModel = string.Empty;
                return string.Empty;
            }

            if (dt0.Rows.Count == 0)
            {
                MessageBox.Show("输入非法无效的序号或者箱号，不做统计。");
                strUPC = string.Empty;
                strJAN = string.Empty;
                strCustModel = string.Empty;
                return string.Empty;
            }
            //HYQ： 如果等于1 ，说明刷入的序号， single的箱号是空，继续保持序号， 如果箱号不为空， 则序号转成箱号处理。
            if (dt0.Rows.Count == 1)
            {
                if (!string.IsNullOrEmpty(dt0.Rows[0]["gtin"].ToString()))
                {

                    strUPC = dt0.Rows[0]["upc_code"].ToString();
                    strJAN = dt0.Rows[0]["jan_code"].ToString();
                    strCustModel = dt0.Rows[0]["custmodel"].ToString();
                    return dt0.Rows[0]["gtin"].ToString();
                }
                strUPC = string.Empty;
                strJAN = string.Empty;
                strCustModel = string.Empty;
                return string.Empty;
            }
            else
            {
                strUPC = string.Empty;
                strJAN = string.Empty;
                strCustModel = string.Empty;
                return string.Empty;
            }

            //换个表查
            //if () { }

        }

        public string getMPNInfo(string Partno)
        {
            string strSql = string.Format("select  trim(ICTPARTNO||'*'||MPN||'*'||PACKUNIT||'*'||TOTALQTY)  as MPN1  "
                            + "  from ppsuser.vw_mpn_info "
                            + " where  ( packcode <> subpackcode or subpackcode is null) and ICTPARTNO ='{0}' ", Partno);


            DataTable dt = ClientUtils.ExecuteSQL(strSql).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["MPN1"].ToString();
            }
            else
            {
                return "NG";
            }

        }


        public string checkLocation(string LocationNo)
        {
            //已经存在于t_location return "NG" 
            string strSql = string.Format("select  *  "
                          + "  from ppsuser.t_location "
                          + " where  location_no='{0}' or location_name ='{1}' ", LocationNo, LocationNo);


            DataTable dt = ClientUtils.ExecuteSQL(strSql).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return "NG";
            }
            else
            {
                return "OK";
            }

        }


        public string LoadBatFile(string sFile, ref string sMessage)
        {
            sMessage = string.Empty;
            string str = string.Empty;
            if (!File.Exists(sFile))
            {
                sMessage = "File not exist - " + sFile;
                return str;
            }
            StreamReader reader = new StreamReader(sFile);
            try
            {
                str = reader.ReadLine().Trim();
            }
            finally
            {
                reader.Close();
            }
            return str;
        }
        public string Readtxt(string sFile)
        {
            try
            {
                string sData = string.Empty;
                using (StreamReader _sr = new StreamReader(sFile))
                {
                    sData = _sr.ReadLine();
                    return sData;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {

            }
        }
        public void WriteToTxt(string sFile, string sData)
        {
            StreamWriter writer = null;
            try
            {
                using (writer = new StreamWriter(sFile, false, Encoding.UTF8))
                {
                    writer.WriteLine(sData);
                    writer.Flush();
                    writer.Close();
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public void WriteToPrintGo(string sFile, string sData)
        {
            try
            {
                if (File.Exists(sFile))
                {
                    File.Delete(sFile);
                }
                File.AppendAllText(sFile, sData, Encoding.Default);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 料号或coo转换
        /// 1.转换成PPP code
        /// 2.料号转换成国别全称（Assembled  in  ）
        /// 3.料号转换成国别全称
        /// 4.coo转换成国别全称（Assembled  in  ）
        /// 5.coo转换成国别全称
        /// 6.coo转换成国别简称
        /// 7.coo转换PPP CODE
        /// 默认.料号转换成国别简称
        /// </summary>
        /// <param name="strpartno">料号/coo</param>
        /// <param name="iFlag">转换标识</param>
        /// <returns></returns>
        internal string GetCoo(string strpartno, int iFlag)
        {
            string coo = string.Empty;
            string sqlstr = "";
            try
            {
                switch (iFlag)
                {
                    // PPP
                    case 1:
                        sqlstr = string.Format(@"select case coo
                                                                     when 'VN' then
                                                                      'H19'
                                                                     when 'CN' then
                                                                      'H1D'
                                                                     when 'KR' then
                                                                      'H1F'
                                                                     else
                                                                                ''
                                                                   end coo
                                                              from pptest.oms_partmapping
                                                             where part ='{0}'
                                                            ", strpartno);
                        break;
                    //get assembled in country by keypart
                    case 2: sqlstr = string.Format(@"select    'Assembled  in  '||  ppsuser.getcoobykp('{0}',1)  as coo from dual ", strpartno); break;
                    // get country by keypart  
                    case 3: sqlstr = string.Format(@"select    ppsuser.getcoobykp('{0}',1)  as coo from dual ", strpartno); break;
                    //get assembled in country by coo
                    case 4: sqlstr = string.Format(@"select    'Assembled  in  '|| ppsuser.f_transform_coo('{0}',1)  as coo from dual ", strpartno); break;
                    //get  country by coo
                    case 5: sqlstr = string.Format(@"select     ppsuser.f_transform_coo('{0}',1)  as coo from dual ", strpartno); break;
                    //get  country code  by coo
                    case 6: sqlstr = string.Format(@"select     ppsuser.f_transform_coo('{0}',0)  as coo from dual ", strpartno); break;
                    // PPP by coo
                    case 7:
                        sqlstr = string.Format(@"select case '{0}'
                                                                     when 'VN' then
                                                                      'H19'
                                                                     when 'CN' then
                                                                      'H1D'
                                                                     when 'KR' then
                                                                      'H1F'
                                                                     else
                                                                       ''
                                                                   end coo
                                                              from dual
                                                            ", strpartno);
                        break;
                    default:
                        //country code
                        sqlstr = string.Format(@"select    ppsuser.getcoobykp('{0}',0)  as coo from dual ", strpartno);
                        break;
                }
                DataTable dt = ClientUtils.ExecuteSQL(sqlstr).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    coo = dt.Rows[0]["coo"].ToString();
                }
            }
            catch { }
            return coo;
        }
    }
}
