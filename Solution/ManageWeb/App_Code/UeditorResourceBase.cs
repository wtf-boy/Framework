using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTF.Framework;
using WTF.Logging;
using WTF.Pages;
public class UeditorResourceBase : HttpHandlerBase
{

    public override string ModuleTypeCode
    {
        get
        {
            return "SupportModule";
        }
    }

    public override bool IsPowerCheck
    {
        get
        {
            return true;
        }
    }
    public override PowerType CheckPowerType
    {
        get
        {
            return PowerType.FramePower;
        }
    }
    public override bool IsCheckUrl
    {
        get
        {
            return false;
        }
    }

    public int ResourceTypeID
    {
        get
        {
            return GetInt("ResourceTypeID");
        }

    }

    public string ResourceID
    {
        get
        {
            return GetString("ResourceID");

        }

    }

    public string RestrictCode
    {
        get
        {
            return GetString("RestrictCode");

        }

    }

    public string ResourceCode
    {
        get
        {
            return GetString("ResourceCode");

        }

    }


    public string LogModuleType
    {
        get
        {
            return GetString("LogModuleType");

        }

    }

    public bool IsWaterMark
    {
        get
        {
            return GetString("IsWaterMark").IsNull() ? false : GetString("IsWaterMark").ToLower() == "true";

        }

    }

    //水平位置
    public int HorizontalAlign
    {
        get
        {
            return GetInt("HorizontalAlign", 3);

        }

    }
    /// <summary>
    /// 垂直
    /// </summary>
    public int VerticalAlign
    {
        get
        {
            return GetInt("VerticalAlign", 3);

        }

    }
    public string GetFileTileInfo
    {
        get
        {


            string field = "pictitle";
            string info = null;
            if (CurrentContext.Request.Form[field] != null && !String.IsNullOrEmpty(CurrentContext.Request.Form[field]))
            {
                info = field == "fileName" ? CurrentContext.Request.Form[field].Split(',')[1] : CurrentContext.Request.Form[field];
            }
            return info;
        }
    }

}
