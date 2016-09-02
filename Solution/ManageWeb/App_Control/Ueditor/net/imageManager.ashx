<%@ WebHandler Language="C#" Class="imageManager" %>
/**
 * Created by visual studio2010
 * User: xuheng
 * Date: 12-3-7
 * Time: 下午16:29
 * To change this template use File | Settings | File Templates.
 */
using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using WTF.Framework;
using WTF.Controls;
using WTF.Resource;
using WTF.Resource.Entity;
using System.Linq;
public class imageManager : UeditorResourceBase
{

    public override void Process(HttpContext context, InvokeResult checkPowerResult)
    {
        context.Response.ContentType = "text/plain";
        if (checkPowerResult.ResultCode != "0")
        {
            context.Response.Write("");
        }
        string action = context.Server.HtmlEncode(context.Request["action"]);

        if (action == "get")
        {

            String str = String.Empty;
            if (ResourceID.IsNoNull() && ResourceCode.IsNull())
            {
                ResourceRule objResourceRule = new ResourceRule();

                foreach (Sys_ResourceVer objSys_ResourceVer in objResourceRule.Sys_ResourceVer.Where(s => s.ResourceID == ResourceID).OrderByDescending(s => s.VerNo))
                {
                    str += objSys_ResourceVer.ResourcePath + "ue_separate_ue";

                }

            }


            context.Response.Write(str);
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