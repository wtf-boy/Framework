using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WTF.Framework;
using WTF.Work.Entity;
using WTF.Work;
using WTF.Power;

public partial class ServiceLayer_Work_ProcessList :SupportPageBase
{
        public int WorkInfoID
        {
            get
            {
                return GetInt("WorkInfoID");

            }

        }
        public override string Condition
        {
            get
                {
                return "it.WorkInfoID="+WorkInfoID;
            }
            }
        public override string SortExpression
        {
            get
            {
                return "it.ProcessName";
            }
        }
        WorkRule objWorkRule= new WorkRule();
        public override void RenderPage()
        {

            this.CurrentBindData<Work_Process>(gdvContent, objWorkRule.Work_Process);
        }
        protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                    case "Create":
                    Redirect("ProcessEdit.aspx?WorkInfoID="+WorkInfoID);
                    break;
                    case "Back":
                    Redirect("WorkInfoList.aspx");
                    break;
                    case "Search":
                    SearchCondition();
                    break;


            }



        }
        protected override void CurrentContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                    case "Modify":
                    RedirectState("ProcessEdit.aspx?WorkInfoID="+WorkInfoID+"&ProcessID=" + e.CommandArgument.ToString());
                    break;
                    case "Remove":
                    objWorkRule.DeleteProcessByKey(e.CommandArgument.ToString());
                    RenderPage();
                    break;

            }
        }

}