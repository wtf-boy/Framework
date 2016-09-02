<%@ WebHandler Language="C#" Class="scrawlImgUp" %>
 

using System;
using System.Web;
using System.IO;
using System.Collections;
using WTF.Framework;
using WTF.Controls;
using WTF.Resource;
public class scrawlImgUp : UeditorResourceBase
{

    public override void Process(HttpContext context, InvokeResult checkPowerResult)
    {
        context.Response.ContentType = "text/html";
        if (checkPowerResult.ResultCode != "0")
        {
            HttpContext.Current.Response.Write("<script>parent.ue_callback('" + "" + "','" + checkPowerResult.ResultMessage + "')</script>");//回调函数
            return;
        }
        context.Response.ContentType = "text/html";
        string action = context.Request["action"];
        InvokeResult objInvokeResult = new InvokeResult();
        if (action == "tmpImg")
        {
            HttpPostedFile uploadFile = context.Request.Files[0];
            ResourceInfo objResourceInfo = new ResourceInfo();
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
            HttpContext.Current.Response.Write("<script>parent.ue_callback('" + objResourceInfo.ResourcePath + "','" + (objInvokeResult.ResultCode == "0" ? "SUCCESS" : objInvokeResult.ResultMessage) + "')</script>");//回调函数
        }
        else
        {
            byte[] bytes = Convert.FromBase64String(context.Request["content"]);
            ResourceInfo objResourceInfo = new ResourceInfo();
            if (ResourceCode.IsNull())
            {
                objInvokeResult = bytes.SaveImageFile(System.Guid.NewGuid() + ".png", "image/x-png", ResourceTypeID, ResourceID, 0, RestrictCode, IsWaterMark, HorizontalAlign, VerticalAlign, out objResourceInfo, "");
            }
            else
            {

                try
                {
                    objResourceInfo = bytes.SaveResourceImage(System.Guid.NewGuid() + ".png",ResourceCode, RestrictCode, IsWaterMark, (System.Web.UI.WebControls.HorizontalAlign)HorizontalAlign, (System.Web.UI.WebControls.VerticalAlign)VerticalAlign)[0];
                }
                catch (Exception objExp)
                {
                    WTF.Logging.LogHelper.Write(LogModuleType, objExp);
                    objInvokeResult.ResultCode = "-1";
                    objInvokeResult.ResultMessage = "对不起上传失败";
                }
            }

            //向浏览器返回json数据
            HttpContext.Current.Response.Write("{'url':'" + objResourceInfo.ResourcePath + "',state:'" + (objInvokeResult.ResultCode == "0" ? "SUCCESS" : objInvokeResult.ResultMessage) + "'}");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}