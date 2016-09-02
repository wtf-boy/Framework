using WTF.Logging;
using WTF.Logging.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.DataConfig.Entity;
using WTF.DataConfig;
public partial class ServiceLayer_CacheKey_CacheKeyEdit : SupportPageBase
{
    /// <summary>
    /// 获取依懒值标识
    /// </summary>
    public int CacheKeyID
    {
        get
        {
            return GetInt("CacheKeyID");

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
    /// <summary>
    /// 变量
    /// </summary>
    public cache_cachekey objcache_cachekey = new cache_cachekey();
    CacheKeyRule objCacheRule = new CacheKeyRule();
    /// <summary>
    /// 页面加载
    /// </summary>
    public override void RenderPage()
    {

        if (CacheKeyID.IsNoNull())
        {
            objcache_cachekey = objCacheRule.cache_cachekey.FirstOrDefault(s => s.CacheKeyID == CacheKeyID);
            if (CheckEditObjectIsNull(objcache_cachekey)) return;

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
        if (CacheKeyID.IsNull())
        {
            ///程序标识
            objcache_cachekey.CacheSiteID = CacheSiteID;
            ///路经
            objcache_cachekey.IDPath = "";
            ///依懒Key
            objcache_cachekey.CacheKey = txtCacheKey.TextCutWord(100);
            ///依懒名称
            objcache_cachekey.CacheName = txtCacheName.TextCutWord(100);
            ///备注
            objcache_cachekey.Remark = txtRemark.TextCutWord(500);
            objCacheRule.Insertcachekey(objcache_cachekey);
            MessageDialog("新增成功", "CacheKeyList.aspx?CacheSiteID=" + CacheSiteID);
        }
        else
        {
            objcache_cachekey = objCacheRule.cache_cachekey.FirstOrDefault(p => p.CacheKeyID == CacheKeyID);
            if (CheckEditObjectIsNull(objcache_cachekey)) return;

            ///依懒Key
            objcache_cachekey.CacheKey = txtCacheKey.TextCutWord(100);
            ///依懒名称
            objcache_cachekey.CacheName = txtCacheName.TextCutWord(100);
            ///备注
            objcache_cachekey.Remark = txtRemark.TextCutWord(500);
            objCacheRule.Updatecachekey(objcache_cachekey);
            MessageDialog("修改成功", "CacheKeyList.aspx?CacheSiteID=" + CacheSiteID);
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
                Redirect("CacheKeyList.aspx?CacheSiteID=" + CacheSiteID);
                break;
        }

    }

}