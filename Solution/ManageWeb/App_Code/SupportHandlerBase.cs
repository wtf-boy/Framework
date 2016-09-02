using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WTF.Framework;

/// <summary>
///SupportHandlerBase 的摘要说明
/// </summary>
public class SupportHandlerBase : WTF.Pages.JsonHandlerBase
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
            return PowerType.LoginPower;
        }
    }
}