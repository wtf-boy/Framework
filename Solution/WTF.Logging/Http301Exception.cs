namespace WTF.Logging
{
    using System;
    using System.Web;

    public class Http301Exception : HttpException
    {
        private string _RedirectUrl;

        public Http301Exception(string redirectUrl) : base(0x12d, "页面进行跳转")
        {
            this._RedirectUrl = redirectUrl;
        }

        public string RedirectUrl
        {
            get
            {
                return this._RedirectUrl;
            }
        }
    }
}

