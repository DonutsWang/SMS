﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Common.Plugins
{
    public class Mail
    {
        #region 私有成员
        private static object lockHelper = new object();
        private string _From;
        private string _FromEmail;
        private string _Subject;
        private string _Body;
        private string _SmtpServer;
        private int _SmtpPort = 25;
        private string _SmtpUserName;
        private string _SmtpPassword;
        //private System.Web.Mail.MailFormat _Format = System.Web.Mail.MailFormat.Html;
        private string _Format;
        private System.Text.Encoding _Encoding = System.Text.Encoding.Default;
        #endregion

        #region 属性
        /// <summary>       
        /// 正文内容类型        
        /// </summary>        
        //public System.Web.Mail.MailFormat Format { set { _Format = value; } }
        public string Format { set { _Format = value; } }
        /// <summary>        
        /// 正文内容编码        
        /// </summary>        
        public System.Text.Encoding Encoding { set { _Encoding = value; } }
        /// <summary>        
        /// FromEmail 发送方地址(如test@163.com)         
        /// </summary>        
        public string FromEmail { set { _FromEmail = value; } }
        /// <summary>       
        /// From        
        /// </summary>        
        public string From { set { _From = value; } }
        /// <summary>        
        /// 主题        
        /// </summary>        
        public string Subject { set { _Subject = value; } }
        /// <summary>        
        /// 内容        
        /// </summary>        
        public string Body { set { _Body = value; } }
        /// <summary>        
        /// SmtpServer        
        /// </summary>        
        public string SmtpServer { set { _SmtpServer = value; } }
        /// <summary>        
        /// SmtpPort        
        /// </summary>        
        public int SmtpPort { set { _SmtpPort = value; } }
        /// <summary>        
        /// SmtpUserName        
        /// </summary>        
        public string SmtpUserName { set { _SmtpUserName = value; } }
        /// <summary>        
        /// SmtpPassword        
        /// </summary>        
        public string SmtpPassword { set { _SmtpPassword = value; } }
        #endregion

        public static void Show(string toMail, string from, string title, string text, string smtpName, string smtpPassWord, string smtpHost, int smtpPort)
        {
            Mail um = new Mail();
            um._FromEmail = from;
            string _toMail = toMail;
            um._Subject = title;
            um._Body = text;
            um._SmtpPort = smtpPort;
            um.SmtpUserName = smtpName;
            um._SmtpPassword = smtpPassWord;
            um._SmtpServer = smtpHost;

            bool b = um.SmtpMailSend(_toMail);
            b.ToString();
        }


        public bool SmtpMailSend(string toEmail)
        {
            lock (lockHelper)
            {
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress(_FromEmail);
                //发送方地址(如test@163.com)                     
                msg.To.Add(toEmail);
                //接收方地址                    
                msg.Body = _Format;
                //正文内容类型                   
                msg.BodyEncoding = _Encoding;
                //正文内容编码                    
                msg.Subject = _Subject;
                //主题                    
                msg.Body = _Body;
                //内容
                msg.IsBodyHtml = false;
                //是否是HTML邮件
                //msg.Priority = MailPriority.High;
                //邮件优先级  
                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(_SmtpUserName, _SmtpPassword);
                smtp.Port = _SmtpPort;
                smtp.Host = _SmtpServer;
                smtp.EnableSsl = false;
                //smtp.SendCompleted += new  SendCompletedEventHandler(SendCompletedCallback);
                try
                {
                    smtp.Send(msg);
                    return true;
                }
                catch
                {
                }

            }
            return false;
        }
    }
}