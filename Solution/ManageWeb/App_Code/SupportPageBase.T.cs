using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WTF.Power;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.ELinq;
using System.Data.Query;
using WTF.Controls;
using WTF.Framework;
using System.Data.Common;
using WTF.Pages;
/// <summary>
///PageBase 的摘要说明
/// </summary>
public class SupportPageBase<T> : PageBase<T> where T : class
{
    public override string ModuleTypeCode
    {
        get
        {
            return "SupportModule";
        }
    }
}



