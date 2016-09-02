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
    /// ��ȡ��ǰӦ�õĻỰ��
    /// </summary>
    /// <returns>�Ự��</returns>
    [WebMethod]
    public int GetSessionCount()
    {
        //��ʼ��
        string categoryName = "ASP.NET Apps v" + Environment.Version.Major + "." + Environment.Version.Minor + "." + Environment.Version.Build;
        string counterName = "Sessions Active";
        string instanceName = SysVariable.CurrentContext.Request.ServerVariables["APPL_MD_PATH"].Replace("/", "_");

        //��ȡ�Ự��
        PerformanceCounter counter = new PerformanceCounter(categoryName, counterName, instanceName);

        int sessionCount =  counter.NextValue().ToString().ConvertInt();
        counter.Close();

        //����
        return sessionCount;
    }
}

