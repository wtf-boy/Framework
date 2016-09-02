<%@ WebHandler Language="C#" Class="fileUp" %>
 
/**
 * Created by visual studio 2010
 * User: xuheng
 * Date: 12-3-9
 * Time: 下午13:53
 * To change this template use File | Settings | File Templates.
 */
using System;
using System.Web;
using System.IO;
using System.Collections;
using WTF.Framework;
using WTF.Controls;
using WTF.Resource;
using WTF.Logging;
using System.Linq;
public class fileUp : UeditorResourceBase
{

    public override void Process(HttpContext context, InvokeResult checkPowerResult)
    {
        context.Response.ContentType = "text/plain";
        if (checkPowerResult.ResultCode != "0")
        {
            context.Response.Write("{'state':'" + checkPowerResult.ResultMessage + "','url':'" + "" + "','fileType':'" + "" + "','original':'" + "" + "'}"); //向浏览器返回数据json数据
            return;
        }

        HttpPostedFile uploadFile = context.Request.Files[0];
        ResourceInfo objResourceInfo = new ResourceInfo();
        InvokeResult objInvokeResult = new InvokeResult();
        if (ResourceCode.IsNull())
        {
            objInvokeResult = uploadFile.SaveFile(ResourceTypeID, ResourceID, RestrictCode, out objResourceInfo, "");
        }
        else
        {
            try
            {
                objResourceInfo = uploadFile.SaveResource(ResourceCode, RestrictCode).First();
            }
            catch (Exception objExp)
            {
                LogHelper.Write(LogModuleType, objExp);
                objInvokeResult.ResultCode = "-1";
                objInvokeResult.ResultMessage = "对不起上传失败";
            }
        }


        string currentType = "";
        if (objInvokeResult.ResultCode == "0")
        {
            string[] temp = objResourceInfo.OriginalName.Split('.');
            currentType = "." + temp[temp.Length - 1].ToLower();
        }
        context.Response.Write("{'state':'" + (objInvokeResult.ResultCode == "0" ? "SUCCESS" : objInvokeResult.ResultMessage) + "','url':'" + objResourceInfo.ResourcePath + "','fileType':'" + currentType + "','original':'" + objResourceInfo.OriginalName + "'}"); //向浏览器返回数据json数据
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}