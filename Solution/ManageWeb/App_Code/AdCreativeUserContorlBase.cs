using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AdCreativeUserContorlBase 的摘要说明
/// </summary>
public class AdCreativeUserContorlBase : System.Web.UI.UserControl
{
	public AdCreativeUserContorlBase()
	{
		
	}
    /// <summary>
    /// 物料标识
    /// </summary>
    public int AdMaterielID
    {
        get;
        set; 
    }
    /// <summary>
    /// 广告位标识
    /// </summary>
    public int PlaceID { get; set; }
    /// <summary>
    /// 是否源物料
    /// </summary>
    public bool IsSource
    {
        get;
        set;
    }
    /// <summary>
    /// 保存json
    /// </summary>
    /// <returns></returns>
    public virtual string SaveInfo()
    {
        return "";
    }
    
}