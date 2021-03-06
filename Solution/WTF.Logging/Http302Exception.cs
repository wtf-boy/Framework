﻿namespace WTF.Logging
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web;

    public class Http302Exception : HttpException
    {
        private string _RedirectUrl;

        public Http302Exception(string redirectUrl, string message = "临时移动,页面进行跳转") : base(0x12e, message)
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

