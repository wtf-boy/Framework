<%@ WebHandler Language="C#" Class="RolePowerHandler" %>

using System;
using System.Web;
using WTF.Framework;
using System.Collections.Generic;
using WTF.Power;
public class RolePowerHandler : SupportHandlerBase
{


    public override string PowerPageCode
    {
        get
        {
            return "SystemSafe_Power_PowerSet";
        }
    }
    public override PowerType CheckPowerType
    {
        get
        {
            return PowerType.PagePower;
        }
    }
    public override bool IsCheckUrl
    {
        get
        {
            return false;
        }
    }
    public override InvokeResult Process(HttpContext context)
    {
        string PowerModuleID = GetString("PowerModuleID");
        string RoleID = GetString("RoleID");
        InvokeResult objInvokeResult = new InvokeResult();
        if (RoleID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "RoleID不能为空标识不能为空";
            return objInvokeResult;
        }
        List<RolePowerKey> objRolePowerKeyList = RolePowerKey.ConvertRolePowerKeyValue(PowerModuleID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        UserRule objUserRule = new UserRule();
        objUserRule.UpdateRolePower(RoleID, objRolePowerKeyList);
        return objInvokeResult;
    }


}