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
using WTF.DataConfig;
using WTF.Framework;
using WTF.DataConfig.Entity;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data.Objects.DataClasses;
using WTF.Controls;
public partial class ServiceLayer_HashType_HashTypeEdit : SupportPageBase
{
    public int HashTypeID
    {
        get
        {
            return GetInt("HashTypeID");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    public Sys_HashType objHashType;
    public HashRule objHashRule = new HashRule();
    public override void RenderPage()
    {
        if (HashTypeID > 0)
        {
            objHashType = objHashRule.Sys_HashType.First(p => p.HashTypeID == HashTypeID);
            Page.DataBind();
        }
    }


    private void SaveInfo()
    {


        if (HashTypeID.IsNull())
        {
            objHashType = new Sys_HashType();
            AutoObjectSetValue(objHashType);
            objHashRule.InsertHashType(objHashType);

            MessageDialog("新增成功！", "../../ServiceLayer/HashType/HashTypeList.aspx");

        }
        else
        {
            objHashType = objHashRule.Sys_HashType.First(p => p.HashTypeID == HashTypeID);

            if (objHashRule.Sys_HashType.Any(s => s.HashTypeID != HashTypeID && s.HashTypeCode == HashTypeCode.Text))
            {
                MessageDialog("代码已经存在无法修改");
                return;
            }
            AutoObjectSetValue(objHashType);
            objHashRule.SaveChanges();

            MessageDialog("修改成功！", "../../ServiceLayer/HashType/HashTypeList.aspx");

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
                Redirect("../../ServiceLayer/HashType/HashTypeList.aspx");
                break;
        }

    }
}
