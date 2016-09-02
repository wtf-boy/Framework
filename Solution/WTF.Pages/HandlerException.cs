namespace WTF.Pages
{
    using System;
    using System.Runtime.CompilerServices;

    public class HandlerException : Exception
    {
        public HandlerException(int resultCode, string resultMessage)
        {
            this.ResultCode = resultCode;
            this.ResultMessage = resultMessage;
        }

        public int ResultCode { get; set; }

        public string ResultMessage { get; set; }
    }
}

