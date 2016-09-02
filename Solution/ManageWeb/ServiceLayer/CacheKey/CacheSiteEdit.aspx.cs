using WTF.DataConfig;
using WTF.DataConfig.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class ServiceLayer_CacheKey_CacheSiteEdit : SupportPageBase
{
    /// <summary>
    /// 获取站点标识
    /// </summary>
    public int CacheSiteID
    {
        get
        {
            return GetInt("CacheSiteID");

        }

    }

    public int ParentID
    {
        get
        {
            return GetInt("ParentID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>
    public cache_cachesite objcache_cachesite = new cache_cachesite();
    CacheKeyRule objCacheKeyRule = new CacheKeyRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (CacheSiteID.IsNoNull())
        {
            objcache_cachesite = objCacheKeyRule.cache_cachesite.FirstOrDefault(s => s.CacheSiteID == CacheSiteID);
            if (CheckEditObjectIsNull(objcache_cachesite)) return;

            Page.DataBind();
        }
        else
        {
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        if (CacheSiteID.IsNull())
        {
            ///站点名称
            objcache_cachesite.SiteName = txtSiteName.TextCutWord(100);
            ///备注
            objcache_cachesite.Remark = txtRemark.TextCutWord(200);
            ///父节点
            objcache_cachesite.ParentID = ParentID;
            objcache_cachesite.CachePrefix = txtCachePrefix.TextCutWord(50);

            ///创建时间
            objcache_cachesite.CreateDate = DateTime.Now;
            objCacheKeyRule.Insertcachesite(objcache_cachesite);

            RefreshFrame("frmCacheSiteTree", "CacheTree.aspx?CacheSiteID=" + objcache_cachesite.CacheSiteID, "新增成功", "CacheKeyList.aspx?CacheSiteID=" + objcache_cachesite.CacheSiteID);


        }
        else
        {
            objcache_cachesite = objCacheKeyRule.cache_cachesite.FirstOrDefault(p => p.CacheSiteID == CacheSiteID);
            if (CheckEditObjectIsNull(objcache_cachesite)) return;
            ///站点名称
            objcache_cachesite.SiteName = txtSiteName.TextCutWord(100);
            ///备注
            objcache_cachesite.Remark = txtRemark.TextCutWord(200);
            objcache_cachesite.CachePrefix = txtCachePrefix.TextCutWord(50);
            objCacheKeyRule.Updatecachesite(objcache_cachesite);
            RefreshFrame("frmCacheSiteTree", "CacheTree.aspx?CacheSiteID=" + CacheSiteID, "新增成功", "CacheKeyList.aspx?CacheSiteID=" + CacheSiteID);

        }
    }

    /// <summary>
    /// 工具栏操作
    /// </summary>
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("CacheKeyList.aspx?CacheSiteID=" + ParentID);
                break;
        }

    }

}