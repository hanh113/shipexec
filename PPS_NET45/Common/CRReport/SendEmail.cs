/*
 * 发送邮件的方法
 * 2018-07 -12
 */
using LibHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace CRReport
{
    public class SendEmail
    {
        #region 邮件发送代码
        /// <summary>
        /// 邮件发送代码
        /// </summary>
        /// <param name="email"></param>
        public static void Send(List<string> filePath, string ICT_DN, string FDORDS)
        {
            string HAWB = "";
            string REGION = "";
            string carrier = "";
            string Air = "";
            if (FDORDS == "DS")
            {
                string Airsql = "SELECT DISTINCT A.MAWB,A.REGION,B.CARR_SHORT_NAME,A.TRANS_MODE FROM PPSUSER.SHIPMENT_HEADER A LEFT JOIN WMUSER.ICT_CARRIER_MASTER @DGEDI B ON A.CARR_CODE = B.CARR_CODE WHERE A.SHIPMENT_ID = '" + ICT_DN + "'";
                if (ClientUtils.ExecuteSQL(Airsql).Tables[0].Rows[0][0] != null)
                {
                    HAWB = ClientUtils.ExecuteSQL(Airsql).Tables[0].Rows[0][0].ToString();
                    REGION = ClientUtils.ExecuteSQL(Airsql).Tables[0].Rows[0][1].ToString();
                    carrier = ClientUtils.ExecuteSQL(Airsql).Tables[0].Rows[0][2].ToString();
                    Air = ClientUtils.ExecuteSQL(Airsql).Tables[0].Rows[0][3].ToString();
                }
            }
            else
            {
                string Airsql = "SELECT CASE WHEN 	TRANS_MODE='01' THEN 'AIR'"
                                   + "WHEN TRANS_MODE = '02' THEN 'Motor'"
                                   + "WHEN TRANS_MODE = '03' THEN 'Rail'"
                                   + "WHEN TRANS_MODE = '04' THEN 'Ocean'"
                                   + "WHEN TRANS_MODE = '10' THEN 'AIR'"
                                   + "WHEN TRANS_MODE = '11' THEN 'Motor'"
                                   + "WHEN TRANS_MODE = '12' THEN 'Rail'"
                                   + "WHEN TRANS_MODE = '13' THEN 'Ocean'"
                + " ELSE 'AIR' END AS TRANS_MODE FROM WMUSER.AC_TMS_RES_HEADER@DGEDI WHERE REQ_NUM=(SELECT OGAUD01 FROM SZFD.OGA_FILE WHERE OGA01='" + ICT_DN + "'AND ROWNUM=1)";
                string pack_car = "select DISTINCT HAWB,REGION,carrier from PPSUSER.g_shipping_detail_t WHERE so_no = '" + ICT_DN + "'";
                if (ClientUtils.ExecuteSQL(pack_car).Tables[0].Rows[0][0] != null)
                {
                    Air = ClientUtils.ExecuteSQL(Airsql).Tables[0].Rows[0][0].ToString();
                    HAWB = ClientUtils.ExecuteSQL(pack_car).Tables[0].Rows[0][0].ToString();
                    REGION = ClientUtils.ExecuteSQL(pack_car).Tables[0].Rows[0][1].ToString();
                    carrier = ClientUtils.ExecuteSQL(pack_car).Tables[0].Rows[0][2].ToString();
                }
            }
            XMLHelper xmlh = new XMLHelper();
            XMLHelper.EmailModel model = new XMLHelper.EmailModel();
            model = xmlh.email();
            //设置发件人信箱,及显示名字 
            MailAddress from = new MailAddress(model.FromEmail, model.FromPerson);
            //设置收件人信箱,及显示名字 
            MailAddress to = new MailAddress(model.ToEmail, model.ToPerson);
            //创建一个MailMessage对象 
            MailMessage oMail = new MailMessage(from, to);
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            //设置收件人,可添加多个,添加方法与下面的一样
            oMail.CC.Add(model.EmailCC);
            oMail.Subject = "" + FDORDS + " TEST " + date + " " + HAWB + " Import Customs Clearance Doc. ICT Ship to  " + REGION + " " + Air + "  " + carrier + ""; //邮件标题 
            oMail.Body = @"<!DOCTYPE html><html><body><p> Dear " + carrier + " team，</p><br/>"
             + "<p> &nbsp; &nbsp; &nbsp; 附件是" + date + "出货目的港清关文件，请查收，谢谢！</p>"
             + "<br/><br/><br/><p> With Best regards</p>"
             + "<p> 立讯电子科技（昆山）有限公司 </p>"
             + "<p> LUXSHARE ELECTRONIC TECHNOLOGY(KUNSHAN)LTD.</p>"
             + "<p><img src='http://www.luxshare-ict.com/images/logo.png'/></p></body></html>";
            oMail.IsBodyHtml = true; //指定邮件格式,支持HTML格式 
            foreach (string EmailFile in filePath)
            {
                System.Net.Mail.Attachment mailAttach_1 = new Attachment(EmailFile);//附件
                oMail.Attachments.Add(mailAttach_1);
            }
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding(model.Encoding);//邮件采用的编码 
            oMail.Priority = MailPriority.High;//设置邮件的优先级为高 
            SmtpClient client = new SmtpClient();
            client.Host = model.SmtpServer; //指定邮件服务器 
            client.Port = 25;
            try
            {
                client.UseDefaultCredentials = false;
                NetworkCredential senderCredential = new NetworkCredential(model.UserName, model.PassWord);//指定服务器邮件,及密码 
                client.Credentials = senderCredential;
                client.Send(oMail);//发送邮件 
                //MessageBox.Show("发送成功!");
                oMail.Dispose(); //释放资源  
            }
            catch
            {
            }
        }
        #endregion

    }
}

