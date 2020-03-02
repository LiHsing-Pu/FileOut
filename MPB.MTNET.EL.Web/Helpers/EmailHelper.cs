using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MPB.MTNET.EL.Web.Helpers
{
    public class EmailHelper
	{
        string m_sSendEmailer = ConfigurationManager.AppSettings["email"];
        string m_sSMTP_IP = ConfigurationManager.AppSettings["host"];
        string m_sSMTP_Account = ConfigurationManager.AppSettings["emailAccount"];


		string m_sSMTPwd = HeapInspectionHelper.getPwdSecurity(ConfigurationManager.AppSettings["emailPwd"]);


        int m_iPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        bool m_bSSL = false;

        /// <summary>
        /// 寄發Email
        /// </summary>
        /// <param name="sEmail">接收人的Email</param>
        /// <param name="sSubject">主旨</param>
        /// <param name="sBody">內文</param>
        /// <param name="sAttachFile">附檔</param>
        public bool SendMail(string sEmail, string sSubject, string sBody, string sAttachFile)
        {
            try
            {
                bool result = false;

                #region 如果Email沒資料就不寄信
                if (string.IsNullOrEmpty(sEmail))
                {
                    return result;
                }

                string real_mail_to = "";
                for (int i = 0; i < sEmail.Split(',').Length; i++)
                {
                    if (!string.IsNullOrEmpty(real_mail_to))
                    {
                        real_mail_to += ",";
                    }
                    if (!string.IsNullOrEmpty(sEmail.Split(',')[i]))
                    {
                        real_mail_to += sEmail.Split(',')[i];
                    }
                }
                if (string.IsNullOrEmpty(real_mail_to))
                {
                    return result;
                }
                #endregion

                //MailMessage oMailMessage = new MailMessage(m_sSendEmailer, sEmail);//MailMessage(寄信者, 收信者)
                MailMessage oMailMessage = new MailMessage(m_sSendEmailer, real_mail_to);//MailMessage(寄信者, 收信者)
                oMailMessage.IsBodyHtml = true;
                oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;//E-mail編碼
                oMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;//E-mail編碼
                oMailMessage.Priority = MailPriority.Normal;//設定優先權
                oMailMessage.Subject = sSubject;//E-mail主旨
                oMailMessage.Body = sBody;//E-mail內容

                //Modify by Johnes 2014/01/19 define:加入附檔
                if (sAttachFile != "")
                {
                    Attachment oAttachMail;
                    try
                    {
                        oAttachMail = new Attachment(sAttachFile);
                        oMailMessage.Attachments.Add(oAttachMail);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                //設定Host
                try
                {
                    SmtpClient oSmtpClient = new SmtpClient(m_sSMTP_IP);
                    oSmtpClient.Credentials = new System.Net.NetworkCredential(m_sSMTP_Account, m_sSMTPwd);
                    oSmtpClient.Port = m_iPort; //SMTP Port
                    oSmtpClient.EnableSsl = false; //SSL
                    oSmtpClient.Send(oMailMessage);
                    oMailMessage.Dispose();
                    result = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}