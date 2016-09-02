<%@ WebHandler Language="C#" Class="PowerInfoHandler" %>

using System;
using System.Web;
using WTF.Framework;
using System.Collections.Generic;
using WTF.Power;
using System.Linq;
using WTF.Power.Entity;
public class PowerInfoHandler : SupportHandlerBase
{

    public override PowerType CheckPowerType
    {
        get
        {
            return PowerType.LoginPower;
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
        string AuthorizeGroupID = GetString("AuthorizeGroupID");

        InvokeResult objInvokeResult = new InvokeResult();

        if (PowerModuleID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "PowerModuleID不能为空标识";
            return objInvokeResult;
        }
        if (AuthorizeGroupID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "AuthorizeGroupID不能为空标识";
            return objInvokeResult;
        }
        UserRule objUserRule = new UserRule();
        List<RolePowerKey> objRolePowerKeyList = RolePowerKey.ConvertRolePowerKeyValue(PowerModuleID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        RolePowerKey objRolePowerKey = objRolePowerKeyList.First();

        List<string> objRoleIDInfoList = objUserRule.Sys_RolePower.Where(s => s.ModuleID == objRolePowerKey.ModuleID && s.CoteModuleID == objRolePowerKey.CoteModuleID && s.CoteID == objRolePowerKey.CoteID && s.IsShare == objRolePowerKey.IsShare && s.IsCoteSupper == objRolePowerKey.IsCoteSupper).Select(s => s.RoleID).ToList();
        List<Sys_Role> RoleInfo = objUserRule.Sys_Role.Where(s => s.AuthorizeGroupID == AuthorizeGroupID && objRoleIDInfoList.Contains(s.RoleID)).ToList();
        objInvokeResult.Data = RoleInfo.Select(s => new { s.RoleName, s.RoleID, s.Remark });
        return objInvokeResult;
    }

}