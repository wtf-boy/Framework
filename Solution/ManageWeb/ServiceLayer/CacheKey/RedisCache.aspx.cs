using ServiceStack.Redis;
using WTF.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_CacheKey_RedisCache : SupportPageBaseSql
{   /// <summary>
    /// 获取程序标识
    /// </summary>
    public int CacheSiteID
    {
        get
        {
            return GetInt("CacheSiteID");

        }

    }
    public RedisClient GetRedisClient()
    {
        string RedisWriteF1 = ConfigHelper.GetValue(txtConfigValue.Text);
        if (string.IsNullOrWhiteSpace(RedisWriteF1))
        {
            RedisWriteF1 = txtConfigValue.Text;
        }
        string host = RedisWriteF1.Substring(0, RedisWriteF1.IndexOf(':'));
        int post = Convert.ToInt32(RedisWriteF1.Substring(RedisWriteF1.IndexOf(':') + 1));
        RedisClient objRedisClient = new RedisClient(host, post);
        return objRedisClient;

    }

    /// <summary>
    /// 保存信息
    /// </summary>
    public void SaveInfo()
    {
        RedisClient objRedisClient = GetRedisClient();

        txtValue.Text = objRedisClient.GetValue(txtCacheKey.Text);
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