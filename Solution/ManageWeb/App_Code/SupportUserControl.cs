using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Pages;

/// <summary>
///UserControl 的摘要说明
/// </summary>
public class SupportUserControl : UserControlBase
{
    public override string ModuleTypeCode
    {
        get
        {
            return "SupportModule";
        }
    }
}
