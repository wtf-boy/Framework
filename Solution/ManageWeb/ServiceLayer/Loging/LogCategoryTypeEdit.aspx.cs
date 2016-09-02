using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using WTF.Framework;

using WTF.Logging;
using WTF.Logging.Entity;
public partial class ServiceLayer_Loging_LogCategoryTypeEdit : SupportPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public LogRule objLogRule = new LogRule();
    public loger_category objCategory;

    public int CategoryID
    {
        get
        {
            return GetInt("CategoryID");
        }
    }
    /// <summary>
    /// 获取程序标识
    /// </summary>
    public int ApplicationID
    {
        get
        {
            return GetInt("ApplicationID");

        }

    }
    public override void RenderPage()
    {
        if (CategoryID.IsNoNull())
        {
            objCategory = objLogRule.loger_category.First(p => p.CategoryID == CategoryID);
            chkLogWriteType.SetSelectValue(objCategory.LogWriteType);
            Page.DataBind();
        }
    }

    public void SaveInfo()
    {

        if (CategoryID.IsNoNull())
        {
            objCategory = objLogRule.loger_category.First(p => p.CategoryID == CategoryID);
            objCategory.CategoryName = txtCategoryName.Text;
            objCategory.LogWriteType = chkLogWriteType.SelectValueString;
            objCategory.CategoryTypeCode = txtCategoryTypeCode.Text.Trim();

            if (objLogRule.loger_category.Any(s => s.ApplicationID == objCategory.ApplicationID && s.CategoryID != objCategory.CategoryID && s.CategoryTypeCode == objCategory.CategoryTypeCode))
            {
                MessageDialog("输入的日志类型代码已经存在");
                return;
            }
            objLogRule.SaveChanges();
            MessageDialog("修改成功", "ApplicationInfo.aspx?ApplicationID=" + ApplicationID);

        }
        else
        {

            objCategory = new loger_category();
            objCategory.CategoryName = txtCategoryName.Text;
            objCategory.LogWriteType = chkLogWriteType.SelectValueString;
            objCategory.CategoryTypeCode = txtCategoryTypeCode.Text.Trim();
            objCategory.ApplicationID = ApplicationID;
            objLogRule.InsertCategory(objCategory);
            MessageDialog("新增成功", "ApplicationInfo.aspx?ApplicationID=" + ApplicationID);

        }

    }

    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Save":
                SaveInfo();
                break;
            case "Back":
                Redirect("ApplicationInfo.aspx?ApplicationID=" + ApplicationID);
                break;
        }
    }

}
