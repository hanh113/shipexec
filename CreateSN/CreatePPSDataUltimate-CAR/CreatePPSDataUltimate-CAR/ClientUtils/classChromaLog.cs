using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace ClientUtilsDll
{
    internal class classChromaLog
    {
        public static classChromaLog.classChromaLogThread LogThread;

        static classChromaLog()
        {
            classChromaLog.LogThread = null;
        }

        public classChromaLog(string f_sHead)
        {
            classChromaLog.LogThread = new classChromaLog.classChromaLogThread(f_sHead);
        }

        private static void addComponentLog(LogType f_LogType, string f_sFunction, string f_sMessage)
        {
            try
            {
                string str = "";
                switch (f_LogType)
                {
                    case LogType.Debug:
                        {
                            str = "Debug";
                            break;
                        }
                    case LogType.Normal:
                        {
                            str = "";
                            break;
                        }
                    case LogType.Warning:
                        {
                            str = "Warning";
                            break;
                        }
                    case LogType.Error:
                        {
                            str = "Error";
                            break;
                        }
                    default:
                        {
                            str = "Unknow";
                            break;
                        }
                }
                if (!Directory.Exists(classChromaLog.classChromaLogThread.m_sLogBasePath))
                {
                    Directory.CreateDirectory(classChromaLog.classChromaLogThread.m_sLogBasePath);
                }
                string mSLogBasePath = classChromaLog.classChromaLogThread.m_sLogBasePath;
                DateTime now = DateTime.Now;
                StreamWriter streamWriter = new StreamWriter(string.Concat(mSLogBasePath, "\\Chroma_", now.ToString("yyyyMMddHH"), ".Log"), true);
                try
                {
                    string[] fSFunction = new string[6];
                    now = DateTime.Now;
                    fSFunction[0] = now.ToString("yyyy/MM/dd HH:mm:ss");
                    fSFunction[1] = string.Format("【{0,-10}】", str);
                    fSFunction[2] = "(";
                    fSFunction[3] = f_sFunction;
                    fSFunction[4] = ")";
                    fSFunction[5] = f_sMessage;
                    streamWriter.WriteLine(string.Concat(fSFunction));
                }
                finally
                {
                    streamWriter.Close();
                }
            }
            catch (Exception exception)
            {
            }
        }

        public static void addLog(LogType f_LogType, string f_sFunction, string f_sMessage)
        {
            if (classChromaLog.LogThread != null)
            {
                classChromaLog.LogThread.addLog(f_LogType, f_sFunction, f_sMessage);
            }
            else
            {
                classChromaLog.addComponentLog(f_LogType, f_sFunction, f_sMessage);
            }
        }

        public static void deleteOldLog()
        {
            if (classChromaLog.LogThread != null)
            {
                classChromaLog.LogThread.deleteOldLog("Chroma");
            }
            else
            {
                classChromaLog.deleteOldLog("Chroma");
            }
        }

        private static void deleteOldLog(string f_sHead)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(classChromaLog.classChromaLogThread.m_sLogBasePath);
                FileInfo[] files = directoryInfo.GetFiles(string.Concat(f_sHead, "*.Log"));
                DateTime dateTime = DateTime.Now.AddDays(-3);
                string str = string.Concat(f_sHead, "_", dateTime.ToString("yyyyMMddHH"), ".Log");
                for (int i = 0; i < (int)files.Length; i++)
                {
                    if (str.CompareTo(files[i].Name) >= 0)
                    {
                        File.Delete(files[i].FullName);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        ~classChromaLog()
        {
            this.Stop();
        }

        public void Stop()
        {
            classChromaLog.LogThread.StopExecute();
        }

        public class classChromaLogThread
        {
            public const string m_sChromaLog = "Chroma";

            public static string m_sLogBasePath;

            private StringCollection m_tsLogData;

            private EventWaitHandle m_eventClose;

            private EventWaitHandle m_eventNewData;

            private EventWaitHandle m_eventContinue;

            private EventWaitHandle m_eventThreadOver;

            private WaitHandle[] m_eventArray;

            private Thread m_LogThread;

            private string m_sLogDate;

            private bool m_bLogOpen;

            private StreamWriter m_Writer;

            private string m_sLogHead;

            private string LogDate
            {
                get
                {
                    return this.m_sLogDate;
                }
                set
                {
                    if (!(value == this.m_sLogDate))
                    {
                        this.m_sLogDate = value;
                        if (!Directory.Exists(classChromaLog.classChromaLogThread.m_sLogBasePath))
                        {
                            Directory.CreateDirectory(classChromaLog.classChromaLogThread.m_sLogBasePath);
                        }
                        this.LogOpen = false;
                        this.deleteOldLog("Chroma");
                        this.deleteOldLog(this.m_sLogDate);
                        this.LogOpen = true;
                    }
                }
            }

            private bool LogOpen
            {
                get
                {
                    return this.m_bLogOpen;
                }
                set
                {
                    if (value != this.m_bLogOpen)
                    {
                        if (!value)
                        {
                            this.m_Writer.Close();
                        }
                        else
                        {
                            string[] mSLogBasePath = new string[] { classChromaLog.classChromaLogThread.m_sLogBasePath, "\\", this.m_sLogHead, "_", this.m_sLogDate, ".Log" };
                            this.m_Writer = new StreamWriter(string.Concat(mSLogBasePath), true);
                        }
                        this.m_bLogOpen = value;
                    }
                }
            }

            static classChromaLogThread()
            {
                classChromaLog.classChromaLogThread.m_sLogBasePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\Log");
            }

            public classChromaLogThread(string f_sHead)
            {
                this.m_sLogHead = f_sHead;
                this.m_eventArray[0] = this.m_eventClose;
                this.m_eventArray[1] = this.m_eventNewData;
                this.m_eventArray[2] = this.m_eventContinue;
                this.m_LogThread = new Thread(new ThreadStart(this.ThreadRun));
                this.m_LogThread.Start();
            }

            public void addLog(LogType f_LogType, string f_sFunction, string f_sMessage)
            {
                try
                {
                    lock (this.m_tsLogData)
                    {
                        string str = "";
                        switch (f_LogType)
                        {
                            case LogType.Debug:
                                {
                                    str = "Debug";
                                    break;
                                }
                            case LogType.Normal:
                                {
                                    str = "";
                                    break;
                                }
                            case LogType.Warning:
                                {
                                    str = "Warning";
                                    break;
                                }
                            case LogType.Error:
                                {
                                    str = "Error";
                                    break;
                                }
                            default:
                                {
                                    str = "Unknow";
                                    break;
                                }
                        }
                        DateTime now = DateTime.Now;
                        lock (this.m_tsLogData)
                        {
                            string[] strArrays = new string[] { now.ToString("yyyyMMddHH"), now.ToString("yyyy/MM/dd HH:mm:ss"), string.Format("【{0,-10}】", str), "(", f_sFunction, ")", f_sMessage };
                            string str1 = string.Concat(strArrays);
                            this.m_tsLogData.Add(str1);
                        }
                        this.m_eventNewData.Set();
                    }
                }
                catch (Exception exception)
                {
                }
            }

            private void addNewLog(string f_sTime, string f_sData)
            {
                try
                {
                    this.LogDate = f_sTime;
                    this.LogOpen = true;
                    this.m_Writer.WriteLine(f_sData);
                }
                catch (Exception exception)
                {
                }
            }

            internal void deleteOldLog(string f_sHead)
            {
                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(classChromaLog.classChromaLogThread.m_sLogBasePath);
                    FileInfo[] files = directoryInfo.GetFiles(string.Concat(f_sHead, "*.Log"));
                    DateTime dateTime = DateTime.Now.AddDays(-3);
                    string str = string.Concat(f_sHead, "_", dateTime.ToString("yyyyMMddHH"), ".Log");
                    for (int i = 0; i < (int)files.Length; i++)
                    {
                        if (str.CompareTo(files[i].Name) >= 0)
                        {
                            File.Delete(files[i].FullName);
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }

            private bool getLogData(ref string f_sTime, ref string f_sData)
            {
                bool flag;
                try
                {
                    lock (this.m_tsLogData)
                    {
                        if (this.m_tsLogData.Count != 0)
                        {
                            f_sData = this.m_tsLogData[0];
                            this.m_tsLogData.RemoveAt(0);
                            f_sTime = f_sData.Substring(0, 10);
                            f_sData = f_sData.Substring(10);
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
                catch (Exception exception)
                {
                    flag = false;
                }
                return flag;
            }

            public void StopExecute()
            {
                try
                {
                    this.m_eventClose.Set();
                    this.m_eventThreadOver.WaitOne();
                }
                catch (Exception exception)
                {
                }
            }

            public void ThreadRun()
            {
                try
                {
                    try
                    {
                        string str = "";
                        string str1 = "";
                        this.m_eventThreadOver.Reset();
                        while (WaitHandle.WaitAny(this.m_eventArray) >= 0)
                        {
                            this.m_eventContinue.Reset();
                            this.m_eventNewData.Reset();
                            if (!this.getLogData(ref str1, ref str))
                            {
                                this.LogOpen = false;
                                if (this.m_eventClose.WaitOne(1, false))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                this.m_eventContinue.Set();
                                this.addNewLog(str1, str);
                            }
                        }
                    }
                    finally
                    {
                        this.m_eventThreadOver.Set();
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }
    }
}