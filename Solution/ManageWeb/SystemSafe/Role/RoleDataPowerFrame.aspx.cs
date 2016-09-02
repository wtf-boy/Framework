using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
public partial class SystemSafe_Role_RoleDataPowerFrame : SupportPageBase
{
    public override string PowerPageCode
    {
        get
        {
            return "SystemSafe_Role_RoleDataPower";
        }
    }
    public Guid RoleID
    {
        get
        {
            return GetGuid("RoleID");

        }
    }
}