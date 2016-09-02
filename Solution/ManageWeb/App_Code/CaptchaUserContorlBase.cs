using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AdCreativeUserContorlBase 的摘要说明
/// </summary>
public class CaptchaUserContorlBase : System.Web.UI.UserControl
{
    public CaptchaUserContorlBase()
	{
		
	}
    /// <summary>
    /// 物料标识
    /// </summary>
    public int CaptchaInfoID
    {
        get;
        set; 
    }
    /// <summary>
    /// 广告位标识
    /// </summary>
    public int ChannelID { get; set; }

    /// <summary>
    /// 保存json
    /// </summary>
    /// <returns></returns>
    public virtual string SaveInfo()
    {
        return "";
    }
    
}