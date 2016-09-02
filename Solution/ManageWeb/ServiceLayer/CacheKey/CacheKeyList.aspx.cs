using WTF.DataConfig;
using WTF.DataConfig.Entity;
using WTF.Logging;
using WTF.Logging.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Controls;
using WTF.Cache.Helper;
public partial class ServiceLayer_CacheKey_CacheKeyList : SupportPageBase
{
    public override string SortExpression
    {
        get
        {
            return "it.CacheKeyID";
        }
    }

    /// <summary>
    /// 获取程序标识
    /// </summary>
    public int CacheSiteID
    {
        get
        {
            return GetInt("CacheSiteID");

        }

    }

    public override string Condition
    {
        get
        {

            string IDPath = objCacheRule.cache_cachesite.FirstOrDefault(s => s.CacheSiteID == CacheSiteID).IDPath;
            return "it.IDPath like '" + IDPath + "%'";
        }
    }

    CacheKeyRule objCacheRule = new CacheKeyRule();
    public override void RenderPage()
    {

        this.CurrentBindData<cache_cachekey>(gdvContent, objCacheRule.cache_cachekey);
    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                Redirect("CacheKeyEdit.aspx?CacheSiteID=" + CacheSiteID);
                break;
            case "CreateSite":
                Redirect("CacheSiteEdit.aspx?ParentID=" + CacheSiteID);
                break;
            case "ModifySite":
                Redirect("CacheSiteEdit.aspx?CacheSiteID=" + CacheSiteID + "&ParentID=" + CacheSiteID);
                break;
            case "CacheManage":
                Redirect("ClearKeyEdit.aspx?CacheSiteID=" + CacheSiteID);
                break;
            case "Redis":
                Redirect("RedisCache.aspx?CacheSiteID=" + CacheSiteID);
                break;
            case "RemoveSite":
                int ParentID = objCacheRule.Deletecachesite(CacheSiteID); ;
                RefreshFrame("frmCacheSiteTree", "CacheTree.aspx?CacheSiteID=" + ParentID, "删除成功", "CacheKeyList.aspx?CacheSiteID=" + ParentID);
                break;


            case "Search":

                SearchCondition();
                break;


        }

    }
    protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Modify":
                RedirectState("CacheKeyEdit.aspx?CacheSiteID=" + CacheSiteID + "&CacheKeyID=" + e.CommandArgument.ToString());
                break;

            case "CacheManage":
                RedirectState("ClearKeyEdit.aspx?CacheSiteID=" + CacheSiteID + "&CacheKeyID=" + e.CommandArgument.ToString());
                break;

            case "Remove":
                objCacheRule.DeletecachekeyByKey(e.CommandArgument.ToString());
                RenderPage();
                break;

        }
    }

}