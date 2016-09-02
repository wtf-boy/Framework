<%@ WebHandler Language="C#" Class="imageUp" %>


using System;
using System.Web;
using System.IO;
using System.Collections;
using WTF.Framework;
using WTF.Controls;
using WTF.Resource;
public class imageUp : UeditorResourceBase
{


    public override void Process(HttpContext context, InvokeResult checkPowerResult)
    {
        context.Response.ContentType = "text/plain";
        if (checkPowerResult.ResultCode != "0")
        {
            context.Response.Write("{'url':'" + "" + "','title':'" + "" + "','original':'" + "" + "','state':'" + checkPowerResult.ResultMessage + "'}");  //向浏览器返回数据json数据
            return;
        }

        HttpPostedFile uploadFile = context.Request.Files[0];
        ResourceInfo objResourceInfo = new ResourceInfo();
        InvokeResult objInvokeResult = new InvokeResult();
        if (ResourceCode.IsNull())
        {
            objInvokeResult = uploadFile.SaveImageFile(ResourceTypeID, ResourceID, RestrictCode, IsWaterMark, HorizontalAlign, VerticalAlign, out objResourceInfo, GetFileTileInfo);
        }
        else
        {
            try
            {
                objResourceInfo = uploadFile.SaveResourceImage(ResourceCode, RestrictCode, IsWaterMark, (System.Web.UI.WebControls.HorizontalAlign)HorizontalAlign, (System.Web.UI.WebControls.VerticalAlign)VerticalAlign)[0];
            }
            catch (Exception objExp)
            {
                WTF.Logging.LogHelper.Write(LogModuleType, objExp);
                objInvokeResult.ResultCode = "-1";
                objInvokeResult.ResultMessage = "对不起上传失败";
            }
        }
        string title = GetFileTileInfo;                   //获取图片描述
        context.Response.Write("{'url':'" + objResourceInfo.ResourcePath + "','title':'" + title + "','original':'" + objResourceInfo.OriginalName + "','state':'" + (objInvokeResult.ResultCode == "0" ? "SUCCESS" : objInvokeResult.ResultMessage) + "'}");  //向浏览器返回数据json数据

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}