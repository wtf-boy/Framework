<%@ WebHandler Language="C#" Class="UserPowerHandler" %>

using System;
using System.Web;
using WTF.Framework;
using System.Collections.Generic;
using WTF.Power;
using System.Linq;
using WTF.Power.Entity;
public class UserPowerHandler : SupportHandlerBase
{


    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_User_UserRolePower";
        }
    }
    public override PowerType CheckPowerType
    {
        get
        {
            return PowerType.PagePower;
        }
    }
    public override InvokeResult Process(HttpContext context)
    {
        InvokeResult objInvokeResult = new InvokeResult();
        string PowerModuleID = GetString("PowerModuleID");
        string UserID = GetString("UserID");
        if (UserID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "用户标识不能为空";
            return objInvokeResult;
        }

        string AuthorizeGroupID = GetString("AuthorizeGroupID");
        if (AuthorizeGroupID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "授权组不能为空标识不能为空";
            return objInvokeResult;
        }
        string PowerModuleTypeID = GetString("PowerModuleTypeID");
        if (PowerModuleTypeID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "授权组模块类型不能为空";
            return objInvokeResult;
        }
        List<RolePowerKey> objRolePowerKeyList = RolePowerKey.ConvertRolePowerKeyValue(PowerModuleID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

        UserRule objUserRule = new UserRule();
        Sys_Role objRole = objUserRule.GetUserSelfAuthorizeGroupRole(AuthorizeGroupID, UserID, PowerModuleTypeID);
        objUserRule.UpdateRolePower(objRole.RoleID, objRolePowerKeyList);
        return objInvokeResult;
    }


}