<%@ WebService Language="C#" Class="SessionMonitor" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Diagnostics;
using WTF.Framework;
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SessionMonitor : System.Web.Services.WebService
{

    /// <summary>
    /// 获取当前应用的会话数
    /// </summary>
    /// <returns>会话数</returns>
    [WebMethod]
    public int GetSessionCount()
    {
        //初始化
        string categoryName = "ASP.NET Apps v" + Environment.Version.Major + "." + Environment.Version.Minor + "." + Environment.Version.Build;
        string counterName = "Sessions Active";
        string instanceName = SysVariable.CurrentContext.Request.ServerVariables["APPL_MD_PATH"].Replace("/", "_");

        //获取会话数
        PerformanceCounter counter = new PerformanceCounter(categoryName, counterName, instanceName);

        int sessionCount =  counter.NextValue().ToString().ConvertInt();
        counter.Close();

        //返回
        return sessionCount;
    }
}

