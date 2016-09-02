namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class InvokeResult
    {
        public InvokeResult()
        {
            this.ResultCode = "0";
            this.ResultMessage = "调用成功";
        }

        public object Data { get; set; }

        public string ResultCode { get; set; }

        public string ResultMessage { get; set; }
    }
}

