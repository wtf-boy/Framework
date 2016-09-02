﻿namespace WTF.Logging
{
    using System;
    using System.Runtime.InteropServices;
    using System.Web;

    public class Http404Exception : HttpException
    {
        public Http404Exception(string message = "对不起，此页面不存在") : base(0x194, message)
        {
        }
    }
}

