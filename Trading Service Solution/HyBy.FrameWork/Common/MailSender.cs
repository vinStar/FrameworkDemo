using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 版权所有: 版权所有(C) 2013，华跃博弈
    /// 内容摘要: 邮件发送类
    /// 完成日期：2014-5-16
    /// 版    本：V1.0
    /// 作    者：段帅峰
    /// </summary>
    public class MailHelper
    {
        private string _mailHost;
        /// <summary>
        /// SMTP服务器
        /// </summary>
        public string MailHost
        {
            get
            {
                if (string.IsNullOrEmpty(_mailHost))
                    throw new Exception("SMTP服务器为空");
                return _mailHost;
            }
            set { _mailHost = value; }
        }

        private int _mailPort;
        /// <summary>
        /// SMTP事务的端口
        /// </summary>
        public int MailPort
        {
            get { return _mailPort; }
            set { _mailPort = value; }
        }

        private bool _enableSsl = false;
        /// <summary>
        /// 使用安全套接字层
        /// </summary>
        public bool EnableSsl
        {
            get { return _enableSsl; }
            set { _enableSsl = value; }
        }

        private string _mailFrom;
        /// <summary>
        /// 发件人邮箱地址
        /// </summary>
        public string MailFrom
        {
            get
            {
                if (string.IsNullOrEmpty(_mailFrom))
                    throw new Exception("发件人邮箱地址为空");
                return _mailFrom;
            }
            set { _mailFrom = value; }
        }

        private string _mailFromPassword;
        /// <summary>
        /// 发件人邮箱密码
        /// </summary>
        public string MailFromPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_mailFrom))
                    throw new Exception("发件人邮箱密码为空");
                return _mailFromPassword;
            }
            set { _mailFromPassword = value; }
        }

        public MailHelper() 
        {
            _mailHost = ConfigHelper.GetConfigString("Smtp");
            _mailPort = 25;
            _mailFrom = ConfigHelper.GetConfigString("SenderEmail");
            _mailFromPassword = ConfigHelper.GetConfigString("PassWord");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailHost">SMTP服务器</param>
        /// <param name="mailPort">SMTP事务的端口</param>
        /// <param name="enableSsl">使用安全套接字层</param>
        /// <param name="mailFrom">发件人邮箱地址</param>
        /// <param name="mailFromPassword">发件人邮箱密码</param>
        public MailHelper(string mailHost, int mailPort, bool enableSsl, string mailFrom, string mailFromPassword)
        {
            _mailHost = mailHost;
            _mailPort = mailPort;
            _enableSsl = enableSsl;
            _mailFrom = mailFrom;
            _mailFromPassword = mailFromPassword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailHost">SMTP服务器</param>
        /// <param name="mailFrom">发件人邮箱地址</param>
        /// <param name="mailFromPassword">发件人邮箱密码</param>
        public MailHelper(string mailHost, string mailFrom, string mailFromPassword)
            : this(mailHost, 25, false, mailFrom, mailFromPassword) { }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">收件人地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        public void SendMail(string mailTo, string subject, string body, bool isBodyHtml)
        {
            SendMail(new string[] { mailTo }, null, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, MailPriority.Normal);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">收件人地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        /// <param name="priority">电子邮件优先级</param>
        public void SendMail(string mailTo, string subject, string body, bool isBodyHtml, MailPriority priority)
        {
            SendMail(new string[] { mailTo }, null, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, priority);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailToList">收件人地址列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        public void SendMail(string[] mailToList, string subject, string body, bool isBodyHtml)
        {
            SendMail(mailToList, null, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, MailPriority.Normal);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailToList">收件人地址列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        /// <param name="priority">电子邮件优先级</param>
        public void SendMail(string[] mailToList, string subject, string body, bool isBodyHtml, MailPriority priority)
        {
            SendMail(mailToList, null, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, priority);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailToList">收件人地址列表</param>
        /// <param name="ccList">抄送收件人地址列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        public void SendMail(string[] mailToList, string[] ccList, string subject, string body, bool isBodyHtml)
        {
            SendMail(mailToList, ccList, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, MailPriority.Normal);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailToList">收件人地址列表</param>
        /// <param name="ccList">抄送收件人地址列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        /// <param name="priority">电子邮件优先级</param>
        public void SendMail(string[] mailToList, string[] ccList, string subject, string body, bool isBodyHtml, MailPriority priority)
        {
            SendMail(mailToList, ccList, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, priority);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailToList">收件人地址列表</param>
        /// <param name="ccList">抄送收件人地址列表</param>
        /// <param name="bccList">密送收件人地址列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        public void SendMail(string[] mailToList, string[] ccList, string[] bccList, string subject, string body, bool isBodyHtml)
        {
            SendMail(mailToList, ccList, bccList, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, MailPriority.Normal);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailToList">收件人地址列表</param>
        /// <param name="ccList">抄送收件人地址列表</param>
        /// <param name="bccList">密送收件人地址列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        /// <param name="priority">电子邮件优先级</param>
        public void SendMail(string[] mailToList, string[] ccList, string[] bccList, string subject, string body, bool isBodyHtml, MailPriority priority)
        {
            SendMail(mailToList, ccList, bccList, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, priority);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailToList">收件人地址列表</param>
        /// <param name="ccList">抄送收件人地址列表</param>
        /// <param name="bccList">密送收件人地址列表</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="subjectEncoding">邮件标题编码格式</param>
        /// <param name="body">邮件正文</param>
        /// <param name="bodyEncoding">邮件正文编码格式</param>
        /// <param name="isBodyHtml">邮件正文是否为Html格式</param>
        /// <param name="priority">电子邮件优先级</param>
        public void SendMail(string[] mailToList, string[] ccList, string[] bccList, string subject, Encoding subjectEncoding, string body, Encoding bodyEncoding, bool isBodyHtml, MailPriority priority)
        {
            try
            {
                MailMessage myMail = new MailMessage();

                myMail.From = new MailAddress(_mailFrom);

                if (mailToList == null || mailToList.Length == 0)
                {
                    throw new Exception("没有可用收件人地址");
                }
                foreach (string mail in mailToList)
                    myMail.To.Add(new MailAddress(mail));

                myMail.Subject = subject;
                myMail.SubjectEncoding = subjectEncoding;
                myMail.Body = body;
                myMail.BodyEncoding = bodyEncoding;
                myMail.IsBodyHtml = isBodyHtml;
                myMail.Priority = priority;

                if (ccList != null && ccList.Length > 0)
                {
                    foreach (string cc in ccList)
                        myMail.CC.Add(new MailAddress(cc));
                }
                if (bccList != null && bccList.Length > 0)
                {
                    foreach (string bcc in bccList)
                        myMail.Bcc.Add(new MailAddress(bcc));
                }

                SmtpClient sender = new SmtpClient();
                sender.Host = _mailHost;
                sender.Port = _mailPort;
                sender.Credentials = new NetworkCredential(_mailFrom, _mailFromPassword);
                sender.DeliveryMethod = SmtpDeliveryMethod.Network;
                sender.EnableSsl = _enableSsl;

                sender.Send(myMail);
            }
            catch
            {
                throw;
            }
        }
    }

}
