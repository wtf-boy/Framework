<%@ WebHandler Language="C#" Class="GroupPowerHandler" %>

using System;
using System.Web;
using WTF.Framework;
using System.Collections.Generic;
using WTF.Power;
public class GroupPowerHandler : SupportHandlerBase
{


    public override string PowerPageCode
    {
        get
        {
            return "ServiceLayer_Authorize_GroupPower";
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

        string PowerModuleID = GetString("PowerModuleID");
        string AuthorizeGroupID = GetString("AuthorizeGroupID");
        InvokeResult objInvokeResult = new InvokeResult();
        if (AuthorizeGroupID.IsNull())
        {
            objInvokeResult.ResultCode = "-1";
            objInvokeResult.ResultMessage = "授权组不能为空标识不能为空";
            return objInvokeResult;
        }
        List<RolePowerKey> objRolePowerKeyList = RolePowerKey.ConvertRolePowerKeyValue(PowerModuleID.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        UserRule objUserRule = new UserRule();
        objUserRule.UpdateAuthorizeGroupPower(AuthorizeGroupID, objRolePowerKeyList);
        return objInvokeResult;
    }


}