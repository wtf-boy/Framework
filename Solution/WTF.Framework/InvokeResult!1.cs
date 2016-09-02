namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class InvokeResult<T> where T: class
    {
        public InvokeResult()
        {
            this.ResultCode = "0";
            this.ResultMessage = "调用成功";
        }

        public T Data { get; set; }

        public string ResultCode { get; set; }

        public string ResultMessage { get; set; }
    }
}

