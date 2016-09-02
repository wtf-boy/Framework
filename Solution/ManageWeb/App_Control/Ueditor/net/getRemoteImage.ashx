<%@ WebHandler Language="C#" Class="getRemoteImage" %>
/**
 * Created by visual studio 2010
 * User: xuheng
 * Date: 12-3-8
 * Time: 下午13:33
 * To get the Remote image.
 */
using System;
using System.Web;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using WTF.Framework;
using WTF.Controls;
using WTF.Resource;
public class getRemoteImage : UeditorResourceBase
{


    public override void Process(HttpContext context, InvokeResult checkPowerResult)
    {

        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };             //文件允许格式


        string uri = context.Server.HtmlEncode(context.Request["upfile"]);
        uri = uri.Replace("&amp;", "&");
        string[] imgUrls = Regex.Split(uri, "ue_separate_ue", RegexOptions.IgnoreCase);

        ArrayList tmpNames = new ArrayList();
        WebClient wc = new WebClient();
        HttpWebResponse res;
        String tmpName = String.Empty;
        String imgUrl = String.Empty;
        String currentType = String.Empty;

        try
        {
            for (int i = 0, len = imgUrls.Length; i < len; i++)
            {
                imgUrl = imgUrls[i];

                if (imgUrl.Substring(0, 7) != "http://")
                {
                    tmpNames.Add("error");
                    continue;
                }

                //格式验证
                int temp = imgUrl.LastIndexOf('.');
                currentType = imgUrl.Substring(temp).ToLower();
                if (Array.IndexOf(filetype, currentType) == -1)
                {
                    tmpNames.Add("对不起此地址不是图片地址!");
                    continue;
                }

                res = (HttpWebResponse)WebRequest.Create(imgUrl).GetResponse();

                //http检测
                if (res.ResponseUri.Scheme.ToLower().Trim() != "http")
                {
                    tmpNames.Add("error");
                    continue;
                }

                //死链验证
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    tmpNames.Add("error");
                    continue;
                }
                //检查mime类型
                if (res.ContentType.IndexOf("image") == -1)
                {
                    tmpNames.Add("error");
                    continue;
                }
                Stream stream = res.GetResponseStream();
                ResourceInfo ojResourceInfo = new ResourceInfo();
                byte[] photocontent = new Byte[res.ContentLength];

                int readecount = 0;
                while (readecount < (int)res.ContentLength)
                {
                    readecount += stream.Read(photocontent, readecount, (int)res.ContentLength - readecount);
                }

                if (photocontent.Length > 0)
                {
                    if (ResourceCode.IsNull())
                    {
                        photocontent.SaveImageFile(System.Guid.NewGuid() + currentType, res.ContentType, ResourceTypeID, ResourceID, 0, RestrictCode, IsWaterMark, HorizontalAlign, VerticalAlign, out ojResourceInfo, "");
                    }
                    else
                    {
                        try
                        {
                            ojResourceInfo = photocontent.SaveResourceImage(System.Guid.NewGuid() + currentType,  ResourceCode, RestrictCode, IsWaterMark, (System.Web.UI.WebControls.HorizontalAlign)HorizontalAlign, (System.Web.UI.WebControls.VerticalAlign)VerticalAlign)[0];
                        }
                        catch (Exception objExp)
                        {
                            WTF.Logging.LogHelper.Write(LogModuleType, objExp);
                            ojResourceInfo.ResourcePath = imgUrl;
                        }
                    }
                    tmpNames.Add(ojResourceInfo.ResourcePath);
                }
                else
                {
                    tmpNames.Add(imgUrl);
                }
            }
        }
        catch (Exception objException)
        {
            tmpNames.Add("error");
        }
        finally
        {
            wc.Dispose();
        }
        context.Response.Write("{url:'" + converToString(tmpNames) + "',tip:'远程图片抓取成功！',srcUrl:'" + uri + "'}");
    }

    //集合转换字符串
    private string converToString(ArrayList tmpNames)
    {
        String str = String.Empty;
        for (int i = 0, len = tmpNames.Count; i < len; i++)
        {
            str += tmpNames[i] + "ue_separate_ue";
            if (i == tmpNames.Count - 1)
                str += tmpNames[i];
        }
        return str;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}