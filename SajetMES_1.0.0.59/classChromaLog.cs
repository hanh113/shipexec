using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Configuration;

namespace EDIPPS
{
    public enum LogType : short
    {
        Debug = 0,
        Normal = 1,
        Warning = 2,
        Error = 3,
    };

    public class classChromaLog
    {
        public static classChromaLogThread LogThread = null;

        public classChromaLog(string f_sHead)
        {
            LogThread = new classChromaLogThread(f_sHead);
        }

        ~classChromaLog()
        {
            Stop();
        }

        public void Stop()
        {
            LogThread.StopExecute();
        }

        public static void addLog(LogType f_LogType, string f_sFunction, string f_sMessage)
        {
            if (LogThread == null)
                addComponentLog(f_LogType, f_sFunction, f_sMessage);
            else
                LogThread.addLog(f_LogType, f_sFunction, f_sMessage);
        }
        public static void deleteOldLog()
        {
            if (LogThread == null)
                deleteOldLog(classChromaLogThread.m_sChromaLog);
            else
                LogThread.deleteOldLog(classChromaLogThread.m_sChromaLog);
        }

        private static void deleteOldLog(string f_sHead)
        {
            try
            {
                System.IO.DirectoryInfo dir = new DirectoryInfo(classChromaLogThread.m_sLogBasePath);
                System.IO.FileInfo[] AllLog = dir.GetFiles(f_sHead + "*.Log");

                string sDeleteFile = f_sHead + "_" + DateTime.Now.AddDays(-3).ToString("yyyyMMddHH") + ".Log";

                for (int i = 0; i < AllLog.Length; i++)
                {
                    if (sDeleteFile.CompareTo(AllLog[i].Name) >= 0)
                    {
                        System.IO.File.Delete(AllLog[i].FullName);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private static void addComponentLog(LogType f_LogType, string f_sFunction, string f_sMessage)
        {
            try
            {

                string sHead = "";

                switch (f_LogType)
                {
                    case LogType.Debug:
                        sHead = "Debug";
                        break;
                    case LogType.Normal:
                        sHead = "";
                        break;
                    case LogType.Warning:
                        sHead = "Warning";
                        break;
                    case LogType.Error:
                        sHead = "Error";
                        break;
                    default:
                        sHead = "Unknow";
                        break;
                }


                if (!Directory.Exists(classChromaLogThread.m_sLogBasePath)) Directory.CreateDirectory(classChromaLogThread.m_sLogBasePath); //判斷LOG目錄是否已建立

                StreamWriter writer = new StreamWriter(classChromaLogThread.m_sLogBasePath + @"\" + classChromaLogThread.m_sChromaLog + "_" + DateTime.Now.ToString("yyyyMMddHH") + ".Log", true);
                try
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + string.Format("【{0,-10}】", sHead) + "(" + f_sFunction + ")" + f_sMessage);
                }
                finally
                {
                    writer.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        public class classChromaLogThread
        {

            public const string m_sChromaLog = "PPS";
            static public string m_sLogBasePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log";//log的基本目錄
            private StringCollection m_tsLogData = new StringCollection();  //用來存放還沒寫入檔案的log
            private EventWaitHandle m_eventClose = new ManualResetEvent(false);  //用來促發整個componet的結束行為
            private EventWaitHandle m_eventNewData = new ManualResetEvent(false);    //用來通知有新的log要記錄
            private EventWaitHandle m_eventContinue = new ManualResetEvent(false);   //用來通知還有log需記錄
            private EventWaitHandle m_eventThreadOver = new ManualResetEvent(true); //用來通知整個thread已結束
            private WaitHandle[] m_eventArray = new WaitHandle[3];   //用來等候相關的handle
            private Thread m_LogThread;  //thread 主體
            private string m_sLogDate = "";   //記錄的日記，格式為yyyymmddHH，當改變時，要關閉原本的log檔案，並建立新的
            private Boolean m_bLogOpen = false;   //記錄_Writer是否為open，當改變時，會去處理相關open和close的工作
            private StreamWriter m_Writer;   //用來處理LOG檔案
            private string m_sLogHead = "";//LOG的title


            //================================================================================================================
            //給外部呼叫的LOG Function，會把LOG記錄到BUFFER，並通知THREAD有新的資料要處理
            public void addLog(LogType f_LogType, string f_sFunction, string f_sMessage)
            {
                try
                {
                    lock (m_tsLogData)
                    {
                        string sHead = "";

                        switch (f_LogType)
                        {
                            case LogType.Debug:
                                sHead = "Debug";
                                break;
                            case LogType.Normal:
                                sHead = "";
                                break;
                            case LogType.Warning:
                                sHead = "Warning";
                                break;
                            case LogType.Error:
                                sHead = "Error";
                                break;
                            default:
                                sHead = "Unknow";
                                break;
                        }

                        DateTime _NowTime = DateTime.Now;

                        lock (m_tsLogData)
                        {
                            string sTemp = _NowTime.ToString("yyyyMMddHH") + _NowTime.ToString("yyyy/MM/dd HH:mm:ss") + string.Format("【{0,-10}】", sHead) + "(" + f_sFunction + ")" + f_sMessage;
                            m_tsLogData.Add(sTemp);
                        }

                        m_eventNewData.Set();
                    }
                }
                catch (Exception)
                {
                }
            }
            //================================================================================================
            //初始及釋放相關資源        
            public classChromaLogThread(string f_sHead)
            {
                m_sLogHead = f_sHead;
                m_eventArray[0] = m_eventClose;
                m_eventArray[1] = m_eventNewData;
                m_eventArray[2] = m_eventContinue;
                m_LogThread = new Thread(ThreadRun);
                m_LogThread.Start();
            }

            public void StopExecute()
            {
                try
                {
                    m_eventClose.Set();
                    m_eventThreadOver.WaitOne();
                }
                catch (Exception)
                {
                }
            }
            //用來刪除舊有的LOG檔案
            internal void deleteOldLog(string f_sHead)
            {
                try
                {
                    System.IO.DirectoryInfo dir = new DirectoryInfo(m_sLogBasePath);
                    System.IO.FileInfo[] AllLog = dir.GetFiles(f_sHead + "*.Log");

                    string sDeleteFile = f_sHead + "_" + DateTime.Now.AddDays(-3).ToString("yyyyMMddHH") + ".Log";

                    for (int i = 0; i < AllLog.Length; i++)
                    {
                        if (sDeleteFile.CompareTo(AllLog[i].Name) >= 0)
                        {
                            System.IO.File.Delete(AllLog[i].FullName);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            //================================================================================================
            //用來記錄處理時間，當時間改變時，則需CLOSE原本的WRITER，並建立新的
            private String LogDate
            {
                get
                {
                    return m_sLogDate;
                }
                set
                {
                    if (value == m_sLogDate) return;
                    m_sLogDate = value;
                    if (!Directory.Exists(m_sLogBasePath)) Directory.CreateDirectory(m_sLogBasePath); //判斷LOG目錄是否已建立

                    LogOpen = false;
                    deleteOldLog(m_sChromaLog);
                    deleteOldLog(m_sLogDate);
                    LogOpen = true;
                }
            }



            private Boolean LogOpen
            {
                get
                {
                    return m_bLogOpen;
                }
                set
                {
                    if (value == m_bLogOpen) return;

                    if (value)
                    {
                        m_Writer = new StreamWriter(m_sLogBasePath + @"\" + m_sLogHead + "_" + m_sLogDate + ".Log", true);
                    }
                    else
                    {
                        m_Writer.Close();
                    }

                    m_bLogOpen = value;
                }

            }

            private Boolean getLogData(ref string f_sTime, ref string f_sData)
            {
                try
                {
                    lock (m_tsLogData)
                    {
                        if (m_tsLogData.Count == 0) return false;

                        f_sData = m_tsLogData[0];
                        m_tsLogData.RemoveAt(0);
                        f_sTime = f_sData.Substring(0, 10);
                        f_sData = f_sData.Substring(10);

                        return true;
                    }

                }
                catch (Exception)
                {
                    return false;
                }
            }


            private void addNewLog(string f_sTime, string f_sData)
            {
                try
                {
                    LogDate = f_sTime;
                    LogOpen = true;
                    //m_Writer.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "   " + f_sData);
                    m_Writer.WriteLine(f_sData);
                }
                catch (Exception)
                {
                }
            }


            public void ThreadRun()
            {
                try
                {
                    try
                    {
                        string sData = "";
                        string sTime = "";

                        m_eventThreadOver.Reset();

                        while (WaitHandle.WaitAny(m_eventArray) >= 0)
                        {
                            m_eventContinue.Reset();
                            m_eventNewData.Reset();

                            if (getLogData(ref sTime, ref sData))
                            {
                                m_eventContinue.Set();
                                addNewLog(sTime, sData);
                            }
                            else
                            {
                                LogOpen = false;
                                if (m_eventClose.WaitOne(1, false)) return;
                            }
                        }
                    }
                    finally
                    {
                        m_eventThreadOver.Set();
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
