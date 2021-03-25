using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysInfo
{
    public class DBLogin
    {
        private static string _DBPwd { get; set; }
        private static string _DBUser { get; set; }

        public static string DBPwd
        {
            get
            {
                if (string.IsNullOrEmpty(_DBPwd)) {
                    _DBPwd = DecodeBase64(ConfigurationManager.AppSettings["DBPwd"].ToString());
                }
                return _DBPwd;
            }
        }
        public static string DBUser { get {
                if (string.IsNullOrEmpty(_DBUser))
                {
                    _DBUser = "ppsuser";
                }
                return _DBUser;
            } }

        public DBLogin()
        {
        }

        public static string DecodeBase64(string code)
        {
            string str;
            byte[] numArray = Convert.FromBase64String(code);
            try
            {
                str = Encoding.GetEncoding("utf-8").GetString(numArray);
            }
            catch
            {
                str = code;
            }
            return str;
        }

        public static string EncodeBase64(string code)
        {
            string base64String;
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(code);
            try
            {
                base64String = Convert.ToBase64String(bytes);
            }
            catch
            {
                base64String = code;
            }
            return base64String;
        }

    }
}
