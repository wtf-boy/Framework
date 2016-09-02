namespace WTF.Framework
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class VarnishxHelper
    {
        private static QueuePoolHelper<VarnishxUrl> _QueuePoolHelper = null;

        static VarnishxHelper()
        {
            _QueuePoolHelper = new QueuePoolHelper<VarnishxUrl>(1);
            _QueuePoolHelper.SendMessage += new EventHandler<QueuePoolEventArgs<VarnishxUrl>>(VarnishxHelper._QueuePoolHelper_SendMessage);
            _QueuePoolHelper.StartProcess(3, 0);
        }

        private static void _QueuePoolHelper_SendMessage(object sender, QueuePoolEventArgs<VarnishxUrl> e)
        {
            try
            {
                VarnishxUrl message = e.Message;
                if (message != null)
                {
                    message.ClearUrl.ClearVarnishx(message.Domain, message.VarnishxIP);
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ClearQueuePoolVarnishx(this string clearUrl, string VarnishxIP = "")
        {
            clearUrl.ClearQueuePoolVarnishx("", VarnishxIP);
        }

        public static void ClearQueuePoolVarnishx(this string clearUrl, string domain, string VarnishxIP = "")
        {
            VarnishxUrl invokeMessage = new VarnishxUrl {
                VarnishxIP = VarnishxIP,
                Domain = domain,
                ClearUrl = clearUrl
            };
            _QueuePoolHelper.EnqueueInvokePool(invokeMessage);
        }

        public static void ClearVarnishx(this string clearUrl, string VarnishxIP = "")
        {
            clearUrl.ClearVarnishx("", VarnishxIP);
        }

        public static void ClearVarnishx(this string clearUrl, string domain, string VarnishxIP = "")
        {
            if (string.IsNullOrWhiteSpace(VarnishxIP))
            {
                VarnishxIP = ConfigHelper.GetValue("VarnishxIP");
            }
            else if (VarnishxIP.Split(new char[] { ',' }).Count<string>() <= 1)
            {
                VarnishxIP = ConfigHelper.GetValue(VarnishxIP);
            }
            if (!string.IsNullOrWhiteSpace(VarnishxIP) && !string.IsNullOrWhiteSpace(clearUrl))
            {
                foreach (string str in VarnishxIP.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Telnet telnet = new Telnet(str.Split(new char[] { ':' })[0], int.Parse(str.Split(new char[] { ':' })[1]), 10, 40, 40);
                    try
                    {
                        telnet.Connect();
                        foreach (string str2 in clearUrl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            string str3 = str2.Trim();
                            if (string.IsNullOrWhiteSpace(domain))
                            {
                                telnet.SendResponse("ban.url " + str3, true);
                            }
                            else
                            {
                                telnet.SendResponse("ban req.http.host == " + domain + " && req.url ~ " + str3, true);
                            }
                            telnet.WaitForString("200");
                            telnet.VirtualScreen.CleanScreen();
                            Thread.Sleep(10);
                        }
                    }
                    finally
                    {
                        telnet.Close();
                    }
                }
            }
        }
    }
}

