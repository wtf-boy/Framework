using WTF.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeskTop_FoldTree_Default : SupportPageBase
{
    public override PowerType CheckPowerType
    {
        get
        {
            return PowerType.FramePower;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        this.Title = SystemName;

    }
}