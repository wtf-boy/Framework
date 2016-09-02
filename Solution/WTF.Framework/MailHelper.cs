namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public class MailHelper
    {
        private MailAddress _MailAddress;
        private Encoding _MailEncoding;
        private System.Net.Mail.SmtpClient _SmtpClient;

        public MailHelper()
        {
            this._MailEncoding = Encoding.UTF8;
            this._MailAddress = null;
            this._SmtpClient = new System.Net.Mail.SmtpClient();
        }

        public MailHelper(string fromMailAddress, string userName, string password, string host, int port, bool enableSsl, bool useDefaultCredentials)
        {
            this._MailEncoding = Encoding.UTF8;
            this._MailAddress = null;
            this._SmtpClient = new System.Net.Mail.SmtpClient();
            this._MailAddress = new MailAddress(fromMailAddress);
            this._SmtpClient.Host = host;
            this._SmtpClient.Port = port;
            this._SmtpClient.UseDefaultCredentials = useDefaultCredentials;
            this._SmtpClient.EnableSsl = enableSsl;
            this._SmtpClient.Credentials = new NetworkCredential(userName, password);
        }

        public MailHelper(string fromMailAddress, string displayName, string userName, string password, string host, int port, bool enableSsl, bool useDefaultCredentials)
        {
            this._MailEncoding = Encoding.UTF8;
            this._MailAddress = null;
            this._SmtpClient = new System.Net.Mail.SmtpClient();
            this._MailAddress = new MailAddress(fromMailAddress, displayName);
            this._SmtpClient.Host = host;
            this._SmtpClient.Port = port;
            this._SmtpClient.EnableSsl = enableSsl;
            this._SmtpClient.UseDefaultCredentials = useDefaultCredentials;
            this._SmtpClient.Credentials = new NetworkCredential(userName, password);
        }

        public bool SendMail(MailMessage mailMessage)
        {
            try
            {
                this._SmtpClient.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendMail(string toMailAddress, MailPriority mailPriority, string subject, string body)
        {
            return this.SendMail(toMailAddress, mailPriority, subject, body, false, null);
        }

        public bool SendMail(string toMailAddress, MailPriority mailPriority, string subject, string body, bool isBodyHtml)
        {
            return this.SendMail(toMailAddress, mailPriority, subject, body, isBodyHtml, null);
        }

        public bool SendMail(string toMailAddress, MailPriority mailPriority, string subject, string body, bool isBodyHtml, Dictionary<string, Stream> attachments)
        {
            return this.SendMail(toMailAddress, "", "", mailPriority, subject, this._MailEncoding, body, this._MailEncoding, isBodyHtml, attachments, this._MailEncoding);
        }

        public bool SendMail(string toMailAddress, string ccMailAddress, string bccMailAddress, MailPriority mailPriority, string subject, string body, bool isBodyHtml, Dictionary<string, Stream> attachments)
        {
            return this.SendMail(toMailAddress, ccMailAddress, bccMailAddress, mailPriority, subject, this._MailEncoding, body, this._MailEncoding, isBodyHtml, attachments, this._MailEncoding);
        }

        public bool SendMail(string toMailAddress, string ccMailAddress, string bccMailAddress, MailPriority mailPriority, string subject, Encoding subjectEncoding, string body, Encoding bodyEncoding, bool isBodyHtml, Dictionary<string, Stream> attachments, Encoding attachmentNameEncoding)
        {
            MailMessage mailMessage = new MailMessage {
                Priority = mailPriority
            };
            foreach (string str in toMailAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                mailMessage.To.Add(str);
            }
            if (ccMailAddress.IsNoNull())
            {
                foreach (string str in ccMailAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.To.Add(str);
                }
            }
            if (bccMailAddress.IsNoNull())
            {
                foreach (string str in bccMailAddress.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mailMessage.Bcc.Add(str);
                }
            }
            if (this._MailAddress != null)
            {
                mailMessage.From = this._MailAddress;
                mailMessage.Sender = this._MailAddress;
            }
            else
            {
                mailMessage.Sender = mailMessage.From;
            }
            mailMessage.SubjectEncoding = subjectEncoding;
            mailMessage.Subject = subject;
            mailMessage.BodyEncoding = bodyEncoding;
            mailMessage.IsBodyHtml = isBodyHtml;
            mailMessage.Body = body;
            if (attachments != null)
            {
                foreach (KeyValuePair<string, Stream> pair in attachments)
                {
                    Attachment item = new Attachment(pair.Value, pair.Key) {
                        NameEncoding = attachmentNameEncoding
                    };
                    mailMessage.Attachments.Add(item);
                }
            }
            return this.SendMail(mailMessage);
        }

        public Encoding MailEncoding
        {
            get
            {
                return this._MailEncoding;
            }
            set
            {
                this._MailEncoding = value;
            }
        }

        public System.Net.Mail.SmtpClient SmtpClient
        {
            get
            {
                return this._SmtpClient;
            }
        }
    }
}

