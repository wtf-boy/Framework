using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Logging;
using WTF.DataConfig;
public partial class ServiceLayer_CacheKey_CacheTree : SupportPageBase
{
    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_CacheKey_CacheKeyList";
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
    public CacheKeyRule objCacheKeyRule = new CacheKeyRule();

    public override void RenderPage()
    {
        if (CacheSiteID.IsNoNull())
        {


            List<string> objIDPathList = objCacheKeyRule.cache_cachesite.Where(s => s.CacheSiteID == CacheSiteID).Select(s => s.IDPath).ToList<string>();

            if (objIDPathList.Count > 0)
            {
                treeContent.ExpandPath = objIDPathList.First();

            }
        }

        XmlDataSource.Data = objCacheKeyRule.GetSiteXmlText("CacheKeyList.aspx");
        treeContent.DataSource = XmlDataSource;
        treeContent.DataBind();
    }
}