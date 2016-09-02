<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using WTF.Pages;
using WTF.Framework;
using WTF.Resource;
using WTF.Controls;
using System.Linq;
using WTF.Logging;
public class UploadHandler : UeditorResourceBase
{
    public override void Process(HttpContext context, InvokeResult checkPowerResult)
    {
        HttpPostedFile uploadFile = context.Request.Files[0];
        InvokeResult objInvokeResult = new InvokeResult();
        string ResourcePath = "";
        foreach (ResourceInfo objResourceInfo in uploadFile.SaveResource(ResourceCode, RestrictCode))
        {
            ResourcePath += objResourceInfo.ResourcePath + "|";
        }
        context.Response.Write(ResourcePath.TrimEnd('|'));
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


}