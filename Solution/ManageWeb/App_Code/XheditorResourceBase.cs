using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTF.Framework;
using WTF.Logging;
using WTF.Pages;
public class XheditorResourceBase : PageBase
{

    public override string ModuleTypeCode
    {
        get
        {
            return "SupportModule";
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
    public override bool PowerIsRedirect
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
            return RequestHelper.GetDecodeEnhanced64Int("ResourceTypeID");
        }

    }
    public string ResourceClientID
    {
        get
        {
            return RequestHelper.GetString("ResourceClientID");
        }

    }


    public string RestrictCode
    {
        get
        {
            return RequestHelper.GetDecodeEnhanced64String("RestrictCode");
        }

    }

    public string LogModuleType
    {
        get
        {
            return RequestHelper.GetDecodeEnhanced64String("LogModuleType");
        }

    }


}
