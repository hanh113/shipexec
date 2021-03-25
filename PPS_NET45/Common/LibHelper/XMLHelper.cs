/*
 * 读取xml的方法
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LibHelper
{
    public partial class XMLHelper
    {
        public EmailModel email()
        {
            XmlTextReader reader = new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + @"\EmailSend.xml");
            EmailModel model = new EmailModel();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {

                    if (reader.Name == "FromEmail")  //发件人邮箱
                    {
                        model.FromEmail = reader.ReadElementString().Trim();
                    }
                    if (reader.Name == "FromPerson")//发件人名字
                    {
                        model.FromPerson = reader.ReadElementString().Trim();
                    }
                    if (reader.Name == "ToEmail") //收件人邮箱
                    {
                        model.ToEmail = reader.ReadElementString().Trim();
                    }
                    if (reader.Name == "ToPerson")//收件人名字
                    {
                        model.ToPerson = reader.ReadElementString().Trim();
                    }
                    if (reader.Name == "Encoding") //UTF-8
                    {
                        model.Encoding = reader.ReadElementString().Trim();
                    }
                    if (reader.Name == "SmtpServer")  // 邮件服务器
                    {
                        model.SmtpServer = reader.ReadElementString().Trim();
                    }
                    if (reader.Name == "UserName")  // user
                    {
                        model.UserName = reader.ReadElementString().Trim();
                    }
                    if (reader.Name == "PassWord") //PassWord
                    {
                        model.PassWord = reader.ReadElementString().Trim();
                    }
                    if (reader.Name == "EmailCC") //EmailCC 抄送
                    {
                        model.EmailCC = reader.ReadElementString().Trim();
                    }
                }
            }
            return model;
        }

        public EmailModel PPSemail()
        {

            EmailModel model = new EmailModel();
            model.FromEmail = "KS-MES-Report@luxshare-ict.com";
            model.FromPerson = "KS-MES-Report@luxshare-ict.com";
            model.ToEmail = "Weifeng.Gu@luxshare-ict.com";
            model.ToPerson = "Weifeng.Gu@luxshare-ict.com";
            model.Encoding = "utf-8";
            model.SmtpServer = "10.33.22.101";
            model.UserName = @"FD.MES@luxshare.com.cn";
            model.PassWord = "Luxshare0705";
            model.EmailCC = "Yaoquan.Hu@luxshare-ict.com";

            return model;
        }
        public partial class EmailModel
        {
            private string fromEmail;   /// 发件人
            private string fromPerson;  /// 发件人Name 
            private string toEmail;     /// 收件人
            private string toPerson;    /// 收件人Name
            private string encoding;    /// UTF-8
            private string smtpServer;  /// 邮件服务器
            private string userName;    /// user
            private string passWord;    ///PassWord
            private string emailCC;
            public string EmailCC
           {
                 get { return emailCC; }
                 set { emailCC = value; }
            }
            public string FromEmail
            {
                get { return fromEmail; }
                set { fromEmail = value; }
            }
            public string FromPerson
            {
                get { return fromPerson; }
                set { fromPerson = value; }
            }
            public string ToEmail
            {
                get { return toEmail; }
                set { toEmail = value; }
            }
            public string ToPerson
            {
                get { return toPerson; }
                set { toPerson = value; }
            }
            public string Encoding
            {
                get { return encoding; }
                set { encoding = value; }
            }
            public string SmtpServer
            {
                get { return smtpServer; }
                set { smtpServer = value; }
            }
            public string UserName
            {
                get { return userName; }
                set { userName = value; }
            }
            public string PassWord
            {
                get { return passWord; }
                set { passWord = value; }
            }
        }
    }
}
