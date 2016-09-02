using WTF.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceLayer_Work_WorkProcessLogInfo : SupportPageBase
{
    public override WTF.Framework.PowerType CheckPowerType
    {
        get
        {
            return WTF.Framework.PowerType.LoginPower;
        }
    }
    public override bool IsCheckUrl
    {
        get
        {
            return false;
        }
    }

    public int WorkProcessLogID
    {
        get
        {
            return GetInt("WorkProcessLogID");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        WorkRule objWorkRule = new WorkRule();
        litDetailInfo.Text = objWorkRule.Work_WorkProcessLog.FirstOrDefault(s => s.WorkProcessLogID == WorkProcessLogID).Message;
    }
}