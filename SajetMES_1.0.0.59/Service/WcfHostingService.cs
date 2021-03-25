using OperationWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDIPPS.Service
{
    public class WcfHostingService
    {
        private delegate void writeLogHandler(string path, string fileName, string text);

        private static HttpHosting hh { get; set; }

        private static string serviceUrl
        {
            get
            {
                return string.Format("http://{0}:8901/ClientObject",ClientUtils.IPAddressV4);
            }
        }

        public static void StartWcfHost()
        {
            if (hh == null)
            {
                BaseHosting.Delegates = new Dictionary<string, Delegate>();
                BaseHosting.Delegates["WRITELOG"] = new writeLogHandler(ClientUtilsDll.SysBasic.WriteLog);
                string strPath = System.Windows.Forms.Application.StartupPath;
                hh = (HttpHosting)Activator.CreateInstance(typeof(ClientHosting));
                hh.HttpBinding = (WcfBinding)Enum.Parse(typeof(WcfBinding), "BasicHttpBinding", true);
                hh.StartService(serviceUrl, strPath + "\\Log");
            }
        }

        public static void StopWcfHostings()
        {
            if (hh != null)
            {
                hh.StopService();
                hh = null;
            }
        }
    }
}
