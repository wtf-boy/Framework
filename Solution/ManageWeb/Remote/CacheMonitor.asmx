<%@ WebService Language="C#" Class="CacheMonitor" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
 
using WTF.Framework;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CacheMonitor : System.Web.Services.WebService
{

    /// <summary>
    /// 获取当前缓存条目数
    /// </summary>
    /// <param name="cacheSource">缓存源</param>
    /// <returns>当前缓存条目数</returns>
    [WebMethod]
    public int GetCurrentCacheCount(string cacheManagerName)
    {
        //ASP.NET数据缓存处理
        if (cacheManagerName == "ASP.NET Cache")
        {
            return SysVariable.CurrentContext.Cache.Count;
        }
        return 0;

    }

}

