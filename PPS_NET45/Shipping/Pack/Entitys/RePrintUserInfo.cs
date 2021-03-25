using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packingparcel.Entitys
{
    public class RePrintUserInfo
    {
        //PPS登陆用户姓名
        public static string LoginUserName { get; set; }
        //PPS登陆用户工号
        public static string LoginEmpNo { get; set; }
        //补印登陆用户姓名
        public static string RePrintUserName { get; set; }
        //补印登陆用户工号
        public static string RePrintEmpNo { get; set; }

        //补印的站别
        public static string RePrintStationName { get; set; }
        //补印的Label
        public static string RePrintLabelName { get; set; }

        //补印登陆最新时间
        public static string RePrintLoginTime { get; set; }
    }
}
