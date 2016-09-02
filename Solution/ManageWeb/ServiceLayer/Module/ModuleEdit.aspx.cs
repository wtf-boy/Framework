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
using WTF.Power;
using WTF.Power.Entity;
using WTF.Framework;
using WTF.Resource;
using WTF.Logging;
using System.Collections.Generic;
using WTF.Logging.Entity;
public partial class ServiceLayer_Module_ModuleEdit : SupportPageBase
{
    private ModuleRule objModuleRule = new ModuleRule();
    public Sys_Module objModule;
    protected void Page_Load(object sender, EventArgs e)
    {


    }



    public override void InitDataPage()
    {

        ResourceRule objResourceRule = new ResourceRule();
        rptImglist.DataSource = objResourceRule.GetResourceVerFilePathInfo(1);

        LogRule objLogRule = new LogRule();
        chkDataList.Items.Clear();
        dropCote.Items.Insert(0, new ListItem("无设定", "-1"));
        foreach (Sys_ModuleCote objSys_ModuleCote in objModuleRule.Sys_ModuleCote)
        {
            dropCote.Items.Add(new ListItem(objSys_ModuleCote.CoteTitle, objSys_ModuleCote.ModuleCoteID.ToString()));
        }

    }

    public string ModuleID
    {
        get
        {

            return GetString("ModuleId");

        }
    }
    public string ParentModuleID
    {
        get
        {
            return GetString("ParentModuleID");

        }
    }



    public override void RenderPage()
    {


        if (ModuleID.IsNoNull())
        {

            objModule = objModuleRule.Sys_Module.FirstOrDefault(c => c.ModuleID == ModuleID);
            foreach (var ModuleData in objModuleRule.Sys_ModuleData.Where(s => s.ModuleID == objModule.ModuleTypeID).Select(s => new { s.ModuleDataID, s.DataName }))
            {

                chkDataList.Items.Add(new ListItem(ModuleData.DataName, ModuleData.ModuleDataID.ToString()));
            }




            chkDataList.SetSelectValue<string>(objModuleRule.Sys_ModuleCheckData.Where(s => s.ModuleID == ModuleID).Select(s => s.ModuleDataID).ToList<string>());
            radModuleFunID.SelectedValue = objModule.ModuleFunID.ToString();
            radOperateTypeID.SelectedValue = objModule.OperateTypeID.ToString();
            chkPlaceType.SetSelectValue(objModule.PlaceType);
            chkModuleShow.Checked = objModule.ModuleShow;
            chkIsEdit.Checked = objModule.IsEdit;
            chkIsCheckPowerData.Checked = objModule.IsCheckPowerData;
            dropCote.SelectedValue = objModule.ModuleCoteID.ToString();
            hidMenuField.Value = objModule.MenuField;
            hidMenuValue.Value = objModule.MenuValue;
            hidMenuCal.Value = objModule.MenuCal;
            chkIsPower.Checked = objModule.IsPower;
            chkIsSupperPower.Checked = objModule.IsSupperPower;
            if (objModule.IsSupperPower)
            {
                chkIsSupperPower.Enabled = false;
            }
            if (objModule.ShareModuleID.IsNoNull())
            {
                Sys_Module objShareModule = objModuleRule.Sys_Module.FirstOrDefault(c => c.ModuleID == objModule.ShareModuleID);
                if (objShareModule != null)
                {
                    List<string> moduleIDList = objShareModule.ModuleIDPath.ConvertListString();
                    foreach (Sys_Module objSys_Module in objModuleRule.Sys_Module.Where(s => moduleIDList.Contains(s.ModuleID)).OrderBy(s => s.ModuleLevel))
                    {
                        litShareModule.Text += objSys_Module.ModuleName + ">";
                    }
                }
                else
                {
                    litShareModule.Text += "此共享权限找不到";
                }

            }
            Page.DataBind();

        }
        else
        {
            chkModuleShow.Checked = true;
            txtCoteKeyID.Text = "0";
            Sys_Module objSys_Module = objModuleRule.Sys_Module.FirstOrDefault(s => s.ModuleID == ParentModuleID);
            if (objSys_Module.IsNull())
            {

                foreach (var ModuleData in objModuleRule.Sys_ModuleData.Where(s => s.ModuleID == ParentModuleID).Select(s => new { s.ModuleDataID, s.DataName }))
                {

                    chkDataList.Items.Add(new ListItem(ModuleData.DataName, ModuleData.ModuleDataID.ToString()));
                }
            }
            else
            {
                foreach (var ModuleData in objModuleRule.Sys_ModuleData.Where(s => s.ModuleID == objSys_Module.ModuleTypeID).Select(s => new { s.ModuleDataID, s.DataName }))
                {

                    chkDataList.Items.Add(new ListItem(ModuleData.DataName, ModuleData.ModuleDataID.ToString()));
                }
            }

            rptImglist.DataBind();
        }

    }

    void SaveValue(Sys_Module objSys_Module)
    {
        objModule.ModuleCode = txtModuleCode.Text.Trim();
        objModule.ModuleName = txtModuleName.Text.Trim();
        objModule.ModuleShow = chkModuleShow.Checked;
        objModule.ModuleFunID = radModuleFunID.SelectedValue.ConvertInt();
        objModule.OperateTypeID = radOperateTypeID.SelectedValue.ConvertInt();
        objModule.PlaceType = chkPlaceType.SelectValueString;
        objModule.Remark = "";
        objModule.ToolTip = txtToolTip.Text;
        objModule.ValGroupName = txtGroupName.Text;
        objModule.CommandArgument = txtCommandArgument.Text;
        objModule.CommandName = txtCommandName.Text.Trim();
        objModule.ClickScriptFun = txtClickScriptFun.Text.Trim();
        objModule.ImageUrl = txtImageUrl.Text;
        objModule.MenuField = hidMenuField.Value.CutText(100, CutTextTailTye.RemoveTail);
        objModule.MenuValue = hidMenuValue.Value.CutText(15, CutTextTailTye.RemoveTail);
        objModule.MenuCal = hidMenuCal.Value.CutText(100, CutTextTailTye.RemoveTail);
        objModule.TargetUrl = "";
        objModule.ModuleCoteID = dropCote.SelectValueInt;
        objModule.IsCheckPowerData = chkIsCheckPowerData.Checked;
        objModule.IsEdit = chkIsEdit.Checked;
        objModule.ShareModuleID = txtShareModuleID.TextCut(36);
        objModule.IsPower = chkIsPower.Checked;
        objModule.IsSupperPower = chkIsSupperPower.Checked;
        if (objModule.ModuleFunID != 3)
        {
            objModule.IsPower = true;
        }
        objModule.CoteKeyID = txtCoteKeyID.TextInt;
        if (objModule.CoteKeyID == 0 && chkIsAutoCoteKeyID.Checked)
        {
            objModule.CoteKeyID = objModuleRule.Sys_Module.Max(s => s.CoteKeyID) + 1;
        }
    }

    public void SaveModule()
    {
        if (ModuleID.IsNoNull())
        {
            objModule = objModuleRule.Sys_Module.FirstOrDefault(m => m.ModuleID == ModuleID);
            SaveValue(objModule);


            if (objModule.ShareModuleID.IsNoNull())
            {
                Sys_Module objShareModule = objModuleRule.Sys_Module.FirstOrDefault(s => s.ModuleID == objModule.ShareModuleID);
                if (objShareModule == null)
                {
                    MessageDialog("找不到此共享模块标识");
                    return;
                }
                if (objModuleRule.Sys_Module.Any(s => objShareModule.ModuleIDPath.StartsWith(s.ModuleIDPath) && s.ModuleCoteID != -1))
                {
                    MessageDialog("共享标识已经设置为栏目权限因此无法共享");
                    return;
                }

            }

            if (objModule.CoteKeyID > 0 && objModuleRule.Sys_Module.Any(s => s.ModuleID != objModule.ModuleID && s.CoteKeyID == objModule.CoteKeyID))
            {

                MessageDialog("已经有别的模块设置了此栏目标识:" + objModule.CoteKeyID);
                return;
            }
            objModuleRule.UpdateModule(objModule);
            objModuleRule.UpdateModuleCheckData(ModuleID, chkDataList.SelectValueString);
            RefreshFrame("frmModuleTree", "ModuleTree.aspx?ModuleID=" + ModuleID.ToString(), "保存成功", "ModuleInfo.aspx?ModuleID=" + ModuleID.ToString());

        }
        else
        {

            objModule = new Sys_Module();
            SaveValue(objModule);
            objModule.ParentModuleID = ParentModuleID;
            objModule.LogCategoryID = -1;
            objModule.IsDispose = false;
            objModule.TargetUrl = "";
            objModule.Remark = "";
            objModule.IsMvc = false;
            objModule.IsController = false;
            objModule.ModuleID = Guid.NewGuid().ToString();

            if (objModule.ShareModuleID.IsNoNull())
            {
                Sys_Module objShareModule = objModuleRule.Sys_Module.FirstOrDefault(s => s.ModuleID == objModule.ShareModuleID);
                if (objShareModule == null)
                {
                    MessageDialog("找不到此共享模块标识");
                    return;
                }
                if (objModuleRule.Sys_Module.Any(s => objShareModule.ModuleIDPath.StartsWith(s.ModuleIDPath) && s.ModuleCoteID != -1))
                {
                    MessageDialog("共享标识已经设置为栏目权限因此无法共享");
                    return;
                }

            }
            if (objModule.CoteKeyID > 0 && objModuleRule.Sys_Module.Any(s => s.CoteKeyID == objModule.CoteKeyID))
            {

                MessageDialog("已经有别的模块设置了此栏目标识:" + objModule.CoteKeyID);
                return;
            }
            objModuleRule.InsertModule(objModule);
            objModuleRule.UpdateModuleCheckData(objModule.ModuleID, chkDataList.SelectValueString);
            RefreshFrame("frmModuleTree", "ModuleTree.aspx?ModuleID=" + objModule.ModuleID.ToString(), "新增成功", "ModuleInfo.aspx?ModuleID=" + objModule.ModuleID.ToString());

        }

    }
    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)
    {
        switch (e.CommandName)
        {

            case "Save":
                SaveModule();

                break;
            case "Back":

                if (GetInt("ModuleType") != 1)
                {
                    Redirect("ModuleInfo.aspx?ModuleID=" + (ModuleID.IsNull() ? ParentModuleID.ToString() : ModuleID.ToString()));
                }
                else
                {

                    Redirect("ModuleTypeInfo.aspx?ModuleID=" + ParentModuleID.ToString());

                }
                break;
        }
    }
}
