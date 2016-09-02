using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Cache.Helper;
using WTF.DataConfig;
using WTF.DataConfig.Entity;
public partial class ServiceLayer_CacheKey_ClearKeyEdit : SupportPageBase
{

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
    public int CacheKeyID
    {
        get
        {
            return GetInt("CacheKeyID");

        }

    }
    /// <summary>
    /// 变量
    /// </summary>

    public override void RenderPage()
    {
        if (CacheKeyID.IsNoNull())
        {
            CacheKeyRule objCacheRule = new CacheKeyRule();
            cache_cachekey objcache_cachekey = objCacheRule.cache_cachekey.FirstOrDefault(s => s.CacheKeyID == CacheKeyID);
            txtKey.Text = objcache_cachekey.CacheKey;
        }

    }
    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        string key = txtKey.TextTrim;
        if (txtID.Text.IsNull())
        {
            MemcacheCacheHelper.DeleteCacheMemcached(key);
        }
        else
        {
            MemcacheCacheHelper.DeleteCacheMemcached(key.ToLower() + "_" + txtID.TextTrim.ToLower());
        }
        MessageDialog("删除缓存成功");

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
            case "GetCache":
                string key = txtKey.TextTrim;
                object cacheValue;
                if (txtID.Text.IsNull())
                {
                    cacheValue = MemcacheCacheHelper.GetCacheMemcached(key);


                }
                else
                {
                    cacheValue = MemcacheCacheHelper.GetCacheMemcached(key.ToLower() + "_" + txtID.TextTrim.ToLower());

                }
                if (cacheValue == null)
                {
                    txtResult.Text = "null";
                }
                else
                {
                    txtResult.Text = cacheValue.JsonJsSerialize();
                    if (txtResult.Text == "null")
                    {
                        txtResult.Text = "有缓存值,后台无法格式化内容输出";
                    }
                }

                break;

            case "Back":
                Redirect("CacheKeyList.aspx?CacheSiteID=" + CacheSiteID);
                break;
        }

    }
}