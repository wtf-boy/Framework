<%@ WebHandler Language="C#" Class="PowerRoleInfo" %>

using System;
using System.Web;
using WTF.Framework;
using System.Collections.Generic;
using WTF.Power;
using System.Linq;
using WTF.Power.Entity;
public class PowerRoleInfo : SupportHandlerBase
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
        string UserID = GetString("UserID");
        InvokeResult objInvokeResult = new InvokeResult();

        if (PowerModuleID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "PowerModuleID不能为空标识";
            return objInvokeResult;
        }
        if (UserID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "UserID不能为空标识";
            return objInvokeResult;
        }
        UserRule objUserRule = new UserRule();
        List<RolePowerKey> objRolePowerKeyList = RolePowerKey.ConvertRolePowerKeyValue(PowerModuleID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        RolePowerKey objRolePowerKey = objRolePowerKeyList.First();
        List<string> objRoleIDList = objUserRule.Sys_RoleUser.Where(s => s.UserID == UserID).Select(s => s.RoleID).ToList();
        List<string> objRoleIDInfoList = objUserRule.Sys_RolePower.Where(s => objRoleIDList.Contains(s.RoleID) && s.ModuleID == objRolePowerKey.ModuleID && s.CoteModuleID == objRolePowerKey.CoteModuleID && s.CoteID == objRolePowerKey.CoteID && s.IsShare == objRolePowerKey.IsShare && s.IsCoteSupper == objRolePowerKey.IsCoteSupper).Select(s => s.RoleID).ToList();
        List<sys_authorizegroup> objsys_authorizegroupList = objUserRule.sys_authorizegroup.ToList();
        List<Sys_Role> RoleInfo = objUserRule.Sys_Role.Where(s => objRoleIDInfoList.Contains(s.RoleID)).ToList();
        foreach (var item in RoleInfo)
        {
            sys_authorizegroup objsys_authorizegroup = objsys_authorizegroupList.FirstOrDefault(s => s.AuthorizeGroupID == item.AuthorizeGroupID);
            item.Remark = objsys_authorizegroup != null ? objsys_authorizegroup.AuthorizeGroupName : "平台授权虚拟组";
        }
        objInvokeResult.Data = RoleInfo.Select(s => new { s.RoleName, s.RoleID, s.AuthorizeGroupID, AuthorizeGroupName = s.Remark });
        return objInvokeResult;
    }

}