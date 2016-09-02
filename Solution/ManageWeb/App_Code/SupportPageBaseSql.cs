using WTF.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WTF.Framework;
using System.Web.UI.WebControls;
using System.Web.UI;


/// <summary>
/// SupportPageBaseSql 的摘要说明
/// </summary>
public class SupportPageBaseSql : PageBaseSql
{

    public override string ModuleTypeCode
    {
        get
        {
            return "SupportModule";
        }
    }

    /// <summary>
    /// 推荐设置
    /// </summary>
    /// <param name="sourceId">来源id</param>
    /// <param name="sourceTypeId">来源类型id</param>
    /// <param name="vouchTitle">推荐标题</param>
    /// <param name="summary">推荐摘要</param>
    /// <param name="androidId">android关联id</param>
    /// <param name="iosId">ios关联id</param>
    /// <param name="pictureUrl">推荐图片地址</param>
    protected void VouchSet(int sourceId, int sourceTypeId, string vouchTitle, string summary, int productId, int androidId, int iosId, string pictureUrl)
    {
        Session["VouchInfo_SourceID"] = sourceId;
        Session["VouchInfo_SourceTypeID"] = sourceTypeId;
        Session["VouchInfo_VouchTitle"] = vouchTitle.IsNoNullOrWhiteSpace() ? vouchTitle : string.Empty;
        Session["VouchInfo_VouchSubTitle"] = "";
        Session["VouchInfo_VouchSimpleTitle"] = "";
        Session["VouchInfo_VouchSimpleTitleUrl"] = "";
        Session["VouchInfo_VouchMSimpleTitleUrl"] = "";
        Session["VouchInfo_VouchSummary"] = summary.IsNoNullOrWhiteSpace() ? summary : string.Empty;
        Session["VouchInfo_ISOID"] = iosId;
        Session["VouchInfo_AndroidID"] = androidId;
        Session["VouchInfo_ProductID"] = productId;
        Session["VouchInfo_IsRelease"] = 1;
        Session["VouchInfo_InfoPic"] = pictureUrl.IsNoNullOrWhiteSpace() ? pictureUrl : string.Empty;
        Session["VouchInfo_UrlPath"] = "";
        Session["VouchInfo_MUrlPath"] = "";
        Session["VouchInfo_CurrentPageUrl"] = Request.Url.AbsoluteUri;
        Server.Transfer("~/Manage/VouchInfo/VouchSet.aspx");
    }

   
    /// <summary>
    /// 检查输入的作者
    /// </summary>
    /// <param name="inputValue"></param>
    /// <returns></returns>
    public string CheckWriteAuthor(string inputValue)
    {

        return inputValue.IsNoNullOrWhiteSpace() ? inputValue : (CurrentUser.NickName.IsNoNull() ? CurrentUser.NickName : CurrentUser.Account);
    }
}