using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace EDIPPS
{
    static class Program
    {
        public static string skinPath = Application.StartupPath + Path.DirectorySeparatorChar + "Skin" + Path.DirectorySeparatorChar;
        public static string g_sFileName = Path.GetFileNameWithoutExtension(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName); //檔案名稱        
        public static string skinName = "Default";
        public static string g_sUserID = "";
        public static bool visible = true;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1) g_sUserID = args[0];
            Process instance = RunningInstance();
            if (instance == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new fMain());
            }
            else
            {
                HandleRunningInstance(instance);
            }
        }
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();  // 取得目前作用中的處理序 
            Process[] processes = Process.GetProcessesByName(current.ProcessName);  // 取得指定的處理緒名稱的所有處理序 
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 3; // 1.Normal   2.Minimized   3.Maximized 
    }
}