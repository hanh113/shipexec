using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PTTWebServices.Models
{
     public class User_Account
    {
        public string Acount { get; set; }
        public string PassWord { get; set; }
        public string Application { get; set; }
        public string IP { get; set; }
        public string Organization { get; set; }
        public string Profit { get; set; }
        /// <summary>
        /// 语言:zh_tw(繁体)/zh_cn(简体)/en_us(英文)
        /// </summary>
        public string language { get; set; }

        public User_Account(string Organization,string Profit,string language= "zh_CN", string Acount= "tiptop", string PassWord= "tiptop", string Application="PPS", string IP= "192.168.1.2")
        {
            this.Organization = Organization;
            this.Profit = Profit;
            this.language = language;
            this.Acount = Acount;
            this.PassWord = PassWord;
            this.Application = Application;
            this.IP = IP;

        }       

     

    }
}
