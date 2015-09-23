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
    /// ��Ȩ����: ��Ȩ����(C) 2013����Ծ����
    /// ����ժҪ: �ʼ�������
    /// ������ڣ�2014-5-16
    /// ��    ����V1.0
    /// ��    �ߣ���˧��
    /// </summary>
    public class MailHelper
    {
        private string _mailHost;
        /// <summary>
        /// SMTP������
        /// </summary>
        public string MailHost
        {
            get
            {
                if (string.IsNullOrEmpty(_mailHost))
                    throw new Exception("SMTP������Ϊ��");
                return _mailHost;
            }
            set { _mailHost = value; }
        }

        private int _mailPort;
        /// <summary>
        /// SMTP����Ķ˿�
        /// </summary>
        public int MailPort
        {
            get { return _mailPort; }
            set { _mailPort = value; }
        }

        private bool _enableSsl = false;
        /// <summary>
        /// ʹ�ð�ȫ�׽��ֲ�
        /// </summary>
        public bool EnableSsl
        {
            get { return _enableSsl; }
            set { _enableSsl = value; }
        }

        private string _mailFrom;
        /// <summary>
        /// �����������ַ
        /// </summary>
        public string MailFrom
        {
            get
            {
                if (string.IsNullOrEmpty(_mailFrom))
                    throw new Exception("�����������ַΪ��");
                return _mailFrom;
            }
            set { _mailFrom = value; }
        }

        private string _mailFromPassword;
        /// <summary>
        /// ��������������
        /// </summary>
        public string MailFromPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_mailFrom))
                    throw new Exception("��������������Ϊ��");
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
        /// <param name="mailHost">SMTP������</param>
        /// <param name="mailPort">SMTP����Ķ˿�</param>
        /// <param name="enableSsl">ʹ�ð�ȫ�׽��ֲ�</param>
        /// <param name="mailFrom">�����������ַ</param>
        /// <param name="mailFromPassword">��������������</param>
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
        /// <param name="mailHost">SMTP������</param>
        /// <param name="mailFrom">�����������ַ</param>
        /// <param name="mailFromPassword">��������������</param>
        public MailHelper(string mailHost, string mailFrom, string mailFromPassword)
            : this(mailHost, 25, false, mailFrom, mailFromPassword) { }

        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailTo">�ռ��˵�ַ</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        public void SendMail(string mailTo, string subject, string body, bool isBodyHtml)
        {
            SendMail(new string[] { mailTo }, null, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, MailPriority.Normal);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailTo">�ռ��˵�ַ</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        /// <param name="priority">�����ʼ����ȼ�</param>
        public void SendMail(string mailTo, string subject, string body, bool isBodyHtml, MailPriority priority)
        {
            SendMail(new string[] { mailTo }, null, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, priority);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailToList">�ռ��˵�ַ�б�</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        public void SendMail(string[] mailToList, string subject, string body, bool isBodyHtml)
        {
            SendMail(mailToList, null, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, MailPriority.Normal);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailToList">�ռ��˵�ַ�б�</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        /// <param name="priority">�����ʼ����ȼ�</param>
        public void SendMail(string[] mailToList, string subject, string body, bool isBodyHtml, MailPriority priority)
        {
            SendMail(mailToList, null, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, priority);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailToList">�ռ��˵�ַ�б�</param>
        /// <param name="ccList">�����ռ��˵�ַ�б�</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        public void SendMail(string[] mailToList, string[] ccList, string subject, string body, bool isBodyHtml)
        {
            SendMail(mailToList, ccList, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, MailPriority.Normal);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailToList">�ռ��˵�ַ�б�</param>
        /// <param name="ccList">�����ռ��˵�ַ�б�</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        /// <param name="priority">�����ʼ����ȼ�</param>
        public void SendMail(string[] mailToList, string[] ccList, string subject, string body, bool isBodyHtml, MailPriority priority)
        {
            SendMail(mailToList, ccList, null, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, priority);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailToList">�ռ��˵�ַ�б�</param>
        /// <param name="ccList">�����ռ��˵�ַ�б�</param>
        /// <param name="bccList">�����ռ��˵�ַ�б�</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        public void SendMail(string[] mailToList, string[] ccList, string[] bccList, string subject, string body, bool isBodyHtml)
        {
            SendMail(mailToList, ccList, bccList, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, MailPriority.Normal);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailToList">�ռ��˵�ַ�б�</param>
        /// <param name="ccList">�����ռ��˵�ַ�б�</param>
        /// <param name="bccList">�����ռ��˵�ַ�б�</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        /// <param name="priority">�����ʼ����ȼ�</param>
        public void SendMail(string[] mailToList, string[] ccList, string[] bccList, string subject, string body, bool isBodyHtml, MailPriority priority)
        {
            SendMail(mailToList, ccList, bccList, subject, Encoding.UTF8, body, Encoding.UTF8, isBodyHtml, priority);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mailToList">�ռ��˵�ַ�б�</param>
        /// <param name="ccList">�����ռ��˵�ַ�б�</param>
        /// <param name="bccList">�����ռ��˵�ַ�б�</param>
        /// <param name="subject">�ʼ�����</param>
        /// <param name="subjectEncoding">�ʼ���������ʽ</param>
        /// <param name="body">�ʼ�����</param>
        /// <param name="bodyEncoding">�ʼ����ı����ʽ</param>
        /// <param name="isBodyHtml">�ʼ������Ƿ�ΪHtml��ʽ</param>
        /// <param name="priority">�����ʼ����ȼ�</param>
        public void SendMail(string[] mailToList, string[] ccList, string[] bccList, string subject, Encoding subjectEncoding, string body, Encoding bodyEncoding, bool isBodyHtml, MailPriority priority)
        {
            try
            {
                MailMessage myMail = new MailMessage();

                myMail.From = new MailAddress(_mailFrom);

                if (mailToList == null || mailToList.Length == 0)
                {
                    throw new Exception("û�п����ռ��˵�ַ");
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
