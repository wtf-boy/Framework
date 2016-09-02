namespace WTF.Pages
{
    using WTF.Framework;
    using WTF.Logging;
    using System;
    using System.Web;

    public class JsonHandlerBase : HandlerBase
    {
        public virtual InvokeResult Process(HttpContext context)
        {
            return new InvokeResult();
        }

        public override void ProcessRequest(HttpContext context)
        {
            base.CurrentContext = context;
            context.Response.ContentType = "text/plain";
            InvokeResult data = this.CheckHandlerPower();
            if (data.ResultCode != "0")
            {
                context.Response.Write(data.JsonJsSerialize());
            }
            else
            {
                try
                {
                    data = this.Process(context);
                    context.Response.Write(data.JsonJsSerialize());
                }
                catch (Exception exception)
                {
                    LogModuleInfo logModuleInfo = this.GetLogModuleInfo();
                    if (!logModuleInfo.IsDispose)
                    {
                        throw exception;
                    }
                    LogHelper.Write(logModuleInfo.ModuleTypeCode, exception, "");
                    data.ResultCode = "Error";
                    data.ResultMessage = "访问出错";
                    context.Response.Write(data.JsonJsSerialize());
                }
            }
        }
    }
}

