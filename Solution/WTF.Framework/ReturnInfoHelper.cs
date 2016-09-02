namespace WTF.Framework
{
    using System;

    [Serializable]
    public class ReturnInfoHelper
    {
        public string Description = "";
        public Exception OperateException = null;
        public object ReturnObject = null;
        public int StatusNumber = 0;
        public bool Success = false;

        public void SetReturnInfo(bool success, int statusNumber, string description, Exception operateException, object returnObject)
        {
            this.Success = success;
            this.StatusNumber = statusNumber;
            this.Description = description;
            this.OperateException = operateException;
            this.ReturnObject = returnObject;
        }
    }
}

