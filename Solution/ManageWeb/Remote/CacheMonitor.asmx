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
    /// ��ȡ��ǰ������Ŀ��
    /// </summary>
    /// <param name="cacheSource">����Դ</param>
    /// <returns>��ǰ������Ŀ��</returns>
    [WebMethod]
    public int GetCurrentCacheCount(string cacheManagerName)
    {
        //ASP.NET���ݻ��洦��
        if (cacheManagerName == "ASP.NET Cache")
        {
            return SysVariable.CurrentContext.Cache.Count;
        }
        return 0;

    }

}

