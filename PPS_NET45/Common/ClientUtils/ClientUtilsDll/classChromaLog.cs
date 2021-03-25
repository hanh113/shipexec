using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace ClientUtilsDll
{
	internal class classChromaLog
	{
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
					return m_sLogDate;
				}
				set
				{
					if (!(value == m_sLogDate))
					{
						m_sLogDate = value;
						if (!Directory.Exists(m_sLogBasePath))
						{
							Directory.CreateDirectory(m_sLogBasePath);
						}
						LogOpen = false;
						deleteOldLog("Chroma");
						deleteOldLog(m_sLogDate);
						LogOpen = true;
					}
				}
			}

			private bool LogOpen
			{
				get
				{
					return m_bLogOpen;
				}
				set
				{
					if (value != m_bLogOpen)
					{
						if (!value)
						{
							m_Writer.Close();
						}
						else
						{
							string[] values = new string[6]
							{
								m_sLogBasePath,
								"\\",
								m_sLogHead,
								"_",
								m_sLogDate,
								".Log"
							};
							m_Writer = new StreamWriter(string.Concat(values), true);
						}
						m_bLogOpen = value;
					}
				}
			}

			static classChromaLogThread()
			{
				m_sLogBasePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log";
			}

			public classChromaLogThread(string f_sHead)
			{
				m_sLogHead = f_sHead;
				m_eventArray[0] = m_eventClose;
				m_eventArray[1] = m_eventNewData;
				m_eventArray[2] = m_eventContinue;
				m_LogThread = new Thread(ThreadRun);
				m_LogThread.Start();
			}

			public void addLog(LogType f_LogType, string f_sFunction, string f_sMessage)
			{
				try
				{
					lock (m_tsLogData)
					{
						string text = "";
						switch (f_LogType)
						{
						case LogType.Debug:
							text = "Debug";
							break;
						case LogType.Normal:
							text = "";
							break;
						case LogType.Warning:
							text = "Warning";
							break;
						case LogType.Error:
							text = "Error";
							break;
						default:
							text = "Unknow";
							break;
						}
						DateTime now = DateTime.Now;
						lock (m_tsLogData)
						{
							string[] values = new string[7]
							{
								now.ToString("yyyyMMddHH"),
								now.ToString("yyyy/MM/dd HH:mm:ss"),
								string.Format("【{0,-10}】", text),
								"(",
								f_sFunction,
								")",
								f_sMessage
							};
							string value = string.Concat(values);
							m_tsLogData.Add(value);
						}
						m_eventNewData.Set();
					}
				}
				catch (Exception)
				{
				}
			}

			private void addNewLog(string f_sTime, string f_sData)
			{
				try
				{
					LogDate = f_sTime;
					LogOpen = true;
					m_Writer.WriteLine(f_sData);
				}
				catch (Exception)
				{
				}
			}

			internal void deleteOldLog(string f_sHead)
			{
				try
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(m_sLogBasePath);
					FileInfo[] files = directoryInfo.GetFiles(f_sHead + "*.Log");
					string text = f_sHead + "_" + DateTime.Now.AddDays(-3.0).ToString("yyyyMMddHH") + ".Log";
					for (int i = 0; i < files.Length; i++)
					{
						if (text.CompareTo(files[i].Name) >= 0)
						{
							File.Delete(files[i].FullName);
						}
					}
				}
				catch (Exception)
				{
				}
			}

			private bool getLogData(ref string f_sTime, ref string f_sData)
			{
				try
				{
					lock (m_tsLogData)
					{
						if (m_tsLogData.Count != 0)
						{
							f_sData = m_tsLogData[0];
							m_tsLogData.RemoveAt(0);
							f_sTime = f_sData.Substring(0, 10);
							f_sData = f_sData.Substring(10);
							return true;
						}
						return false;
					}
				}
				catch (Exception)
				{
					return false;
				}
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

			public void ThreadRun()
			{
				try
				{
					try
					{
						string f_sData = "";
						string f_sTime = "";
						m_eventThreadOver.Reset();
						while (WaitHandle.WaitAny(m_eventArray) >= 0)
						{
							m_eventContinue.Reset();
							m_eventNewData.Reset();
							if (!getLogData(ref f_sTime, ref f_sData))
							{
								LogOpen = false;
								if (m_eventClose.WaitOne(1, false))
								{
									break;
								}
							}
							else
							{
								m_eventContinue.Set();
								addNewLog(f_sTime, f_sData);
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

		public static classChromaLogThread LogThread;

		static classChromaLog()
		{
			LogThread = null;
		}

		public classChromaLog(string f_sHead)
		{
			LogThread = new classChromaLogThread(f_sHead);
		}

		private static void addComponentLog(LogType f_LogType, string f_sFunction, string f_sMessage)
		{
			try
			{
				string text = "";
				switch (f_LogType)
				{
				case LogType.Debug:
					text = "Debug";
					break;
				case LogType.Normal:
					text = "";
					break;
				case LogType.Warning:
					text = "Warning";
					break;
				case LogType.Error:
					text = "Error";
					break;
				default:
					text = "Unknow";
					break;
				}
				if (!Directory.Exists(classChromaLogThread.m_sLogBasePath))
				{
					Directory.CreateDirectory(classChromaLogThread.m_sLogBasePath);
				}
				string sLogBasePath = classChromaLogThread.m_sLogBasePath;
				StreamWriter streamWriter = new StreamWriter(sLogBasePath + "\\Chroma_" + DateTime.Now.ToString("yyyyMMddHH") + ".Log", true);
				try
				{
					streamWriter.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + string.Format("【{0,-10}】", text) + "(" + f_sFunction + ")" + f_sMessage);
				}
				finally
				{
					streamWriter.Close();
				}
			}
			catch (Exception)
			{
			}
		}

		public static void addLog(LogType f_LogType, string f_sFunction, string f_sMessage)
		{
			if (LogThread != null)
			{
				LogThread.addLog(f_LogType, f_sFunction, f_sMessage);
			}
			else
			{
				addComponentLog(f_LogType, f_sFunction, f_sMessage);
			}
		}

		public static void deleteOldLog()
		{
			if (LogThread != null)
			{
				LogThread.deleteOldLog("Chroma");
			}
			else
			{
				deleteOldLog("Chroma");
			}
		}

		private static void deleteOldLog(string f_sHead)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(classChromaLogThread.m_sLogBasePath);
				FileInfo[] files = directoryInfo.GetFiles(f_sHead + "*.Log");
				string text = f_sHead + "_" + DateTime.Now.AddDays(-3.0).ToString("yyyyMMddHH") + ".Log";
				for (int i = 0; i < files.Length; i++)
				{
					if (text.CompareTo(files[i].Name) >= 0)
					{
						File.Delete(files[i].FullName);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		~classChromaLog()
		{
			Stop();
		}

		public void Stop()
		{
			LogThread.StopExecute();
		}
	}
}
