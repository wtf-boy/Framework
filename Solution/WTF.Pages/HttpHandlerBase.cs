namespace WTF.Pages
{
    using WTF.Framework;
    using WTF.Logging;
    using System;
    using System.Web;

    public class HttpHandlerBase : HandlerBase
    {
        public virtual void Process(HttpContext context, InvokeResult checkPowerResult)
        {
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.CurrentContext = context;
            context.Response.ContentType = "text/plain";
            InvokeResult checkPowerResult = this.CheckHandlerPower();
            try
            {
                this.Process(context, checkPowerResult);
            }
            catch (Exception exception)
            {
                LogModuleInfo logModuleInfo = this.GetLogModuleInfo();
                if (!logModuleInfo.IsDispose)
                {
                    throw exception;
                }
                LogHelper.Write(logModuleInfo.ModuleTypeCode, exception, "");
                checkPowerResult.ResultCode = "-2";
                checkPowerResult.ResultMessage = "访问出错";
                this.Process(context, checkPowerResult);
            }
        }
    }
}

