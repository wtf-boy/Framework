namespace WTF.Controls
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Power;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyGridView : GridView
    {
        private IList<OperateModuleInfo> _objModuleList = null;
        private Dictionary<int, string> _StatFieldList = new Dictionary<int, string>();
        private Dictionary<string, string> _StatSetValueList = new Dictionary<string, string>();
        private Dictionary<int, double> _StatValueList = new Dictionary<int, double>();
        private ModuleRule CurrentModuleRule = new ModuleRule();
        private static readonly object MenuItemCheckEvent = new object();

        [Description("菜单项检查"), Browsable(true), Category("Seven：菜单属性")]
        public event MenuItemCheckEventHandler MenuItemCheckCommand
        {
            add
            {
                base.Events.AddHandler(MenuItemCheckEvent, value);
            }
            remove
            {
                base.Events.RemoveHandler(MenuItemCheckEvent, value);
            }
        }

        public event PagerChangeEventHandler PagerChangeCommand;

        private void MyGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            ((ModulePage) this.Page).CurrentContent_Sorting(sender, e);
        }

        private void On_PagerChangeCommand(object sender, PagerEventArgs e)
        {
            if (this.PagerChangeCommand != null)
            {
                this.PagerChangeCommand(sender, e);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!base.DesignMode)
            {
                int key = 0;
                foreach (DataControlField field in this.Columns)
                {
                    string statField = "";
                    if (field is WTF.Controls.BoundField)
                    {
                        statField = ((WTF.Controls.BoundField) field).StatField;
                    }
                    else if (field is WTF.Controls.HyperLinkField)
                    {
                        statField = ((WTF.Controls.HyperLinkField) field).StatField;
                    }
                    else if (field is WTF.Controls.TemplateField)
                    {
                        statField = ((WTF.Controls.TemplateField) field).StatField;
                    }
                    if (!string.IsNullOrWhiteSpace(statField))
                    {
                        this._StatFieldList.Add(key, statField);
                        this._StatValueList.Add(key, 0.0);
                        this.ShowFooter = true;
                    }
                    key++;
                }
                if (this.AllowSorting)
                {
                    base.Sorting += new GridViewSortEventHandler(this.MyGridView_Sorting);
                    if (this.IsAutoSortFields)
                    {
                        foreach (DataControlField field in this.Columns)
                        {
                            if (field is WTF.Controls.BoundField)
                            {
                                if (string.IsNullOrEmpty(field.SortExpression) && ((WTF.Controls.BoundField) field).IsAutoSort)
                                {
                                    field.SortExpression = ((WTF.Controls.BoundField) field).DataField;
                                }
                            }
                            else if ((field is WTF.Controls.HyperLinkField) && (string.IsNullOrEmpty(field.SortExpression) && ((WTF.Controls.HyperLinkField) field).IsAutoSort))
                            {
                                field.SortExpression = ((WTF.Controls.HyperLinkField) field).DataTextField;
                            }
                        }
                    }
                }
                this.PageSize = 500;
                if (this.IsUserPager)
                {
                    PagerTemplate template = new PagerTemplate(this);
                    template.PagerChangeCommand += new PagerChangeEventHandler(this.On_PagerChangeCommand);
                    template.CurrentPageIndex = this.PageIndex;
                    this.PagerTemplate = template;
                }
                if (this.OperateStyle == WTF.Framework.OperateStyle.RowOperate)
                {
                    bool flag = false;
                    DataControlField field2 = null;
                    Operate operate = new Operate();
                    foreach (DataControlField field in this.Columns)
                    {
                        if (field is OperateField)
                        {
                            if (((OperateField) field).DataTextField.IsNull())
                            {
                                field2 = field;
                            }
                            operate.Buttons = ((OperateField) field).Buttons;
                            flag = true;
                        }
                    }
                    if (field2 != null)
                    {
                        this.Columns.Remove(field2);
                    }
                    if (flag)
                    {
                        this.Columns.Add(operate);
                    }
                }
            }
        }

        public virtual bool OnMenuItemCheckCommand(MenuItemCheckEventArgs e)
        {
            bool flag = true;
            MenuItemCheckEventHandler handler = (MenuItemCheckEventHandler) base.Events[MenuItemCheckEvent];
            if (handler != null)
            {
                flag = handler(this, e);
            }
            return flag;
        }

        protected override void OnPagePreLoad(object sender, EventArgs e)
        {
            base.OnPagePreLoad(sender, e);
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(typeof(MyGridView), "MyGrid.js"))
            {
                string url = SysVariable.ApplicationPath + "/" + this.ThemePath + "/MyGrid.js" + ControlVerHelper.GetVer("?");
                this.Page.ClientScript.RegisterClientScriptInclude(typeof(MyGridView), "MyGrid.js", url);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.FieldHidden.IsNoNullOrWhiteSpace())
            {
                string[] array = this.FieldHidden.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length > 0)
                {
                    foreach (DataControlField field in this.Columns)
                    {
                        if (field is WTF.Controls.BoundField)
                        {
                            if (array.Contains<string>(((WTF.Controls.BoundField) field).DataField))
                            {
                                field.Visible = false;
                            }
                        }
                        else if (field is WTF.Controls.HyperLinkField)
                        {
                            if (array.Contains<string>(((WTF.Controls.HyperLinkField) field).DataTextField))
                            {
                                field.Visible = false;
                            }
                        }
                        else if ((field is WTF.Controls.TemplateField) && array.Contains<string>(((WTF.Controls.TemplateField) field).DataField))
                        {
                            field.Visible = false;
                        }
                    }
                }
            }
            base.OnPreRender(e);
            if (this.BottomPagerRow != null)
            {
                this.BottomPagerRow.Visible = true;
            }
        }

        protected override void OnRowCommand(GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Sort")
            {
                try
                {
                    base.OnRowCommand(e);
                }
                catch (Exception exception)
                {
                    Type type = exception.GetType();
                    LogModuleInfo logModuleInfo = ((ModulePage) this.Page).GetLogModuleInfo();
                    if (((type != typeof(ThreadAbortException)) && (type != typeof(InfoHintException))) && !(type == typeof(ArgumentInputNullException)))
                    {
                        if (!logModuleInfo.IsDispose)
                        {
                            throw exception;
                        }
                        LogHelper.DisposeException(logModuleInfo.ModuleTypeCode, exception);
                    }
                    else
                    {
                        LogHelper.DisposeException(logModuleInfo.ModuleTypeCode, exception);
                    }
                }
            }
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (KeyValuePair<int, string> pair in this._StatFieldList)
                {
                    if (!this.StatValueList.ContainsKey(pair.Value))
                    {
                        this._StatValueList[pair.Key] = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, pair.Value)) + this._StatValueList[pair.Key];
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                foreach (KeyValuePair<int, string> pair in this._StatFieldList)
                {
                    if (!this.StatValueList.ContainsKey(pair.Value))
                    {
                        e.Row.Cells[pair.Key].Text = this._StatValueList[pair.Key].ToString();
                    }
                    else
                    {
                        e.Row.Cells[pair.Key].Text = this.StatValueList[pair.Value].ToString();
                    }
                }
            }
            base.OnRowDataBound(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            StringBuilder builder = new StringBuilder();
            string str = "{}";
            builder.AppendLine("var " + this.GridViewHandle + ";");
            builder.AppendLine("jQuery(function () {   " + this.GridViewHandle + "=$('#" + this.ClientID + "').createmyGrid(" + str + ");});");
            writer.Write(builder.ToString());
            writer.RenderEndTag();
        }

        public string AlreadySelectedRowKeys
        {
            get
            {
                return this.ViewState.GetString("AlreadySelectedRowKeys", "");
            }
            set
            {
                this.ViewState["AlreadySelectedRowKeys"] = value;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Seven:按钮属性"), Description("按钮设置")]
        public ToolButtons Buttons
        {
            get
            {
                if (this.ViewState["GriewToolButtons"] == null)
                {
                    this.ViewState["GriewToolButtons"] = new ToolButtons();
                }
                return (ToolButtons) this.ViewState["GriewToolButtons"];
            }
            set
            {
                this.ViewState["GriewToolButtons"] = value;
            }
        }

        [Browsable(true), Description("禁止命令起用"), Category("Seven：工具栏")]
        public string CommandDisabled
        {
            get
            {
                return this.ViewState.GetString("CommandDisabled", "");
            }
            set
            {
                this.ViewState["CommandDisabled"] = value;
            }
        }

        public string CommandExpandArgumentFields
        {
            get
            {
                return this.ViewState.GetString("CommandExpandArgumentFields", "");
            }
            set
            {
                this.ViewState["CommandExpandArgumentFields"] = value;
            }
        }

        [Browsable(true), Category("Seven：工具栏"), Description("隐藏命令")]
        public string CommandHidden
        {
            get
            {
                return this.ViewState.GetString("CommandHidden", "");
            }
            set
            {
                this.ViewState["CommandHidden"] = value;
            }
        }

        [Description("栏目标识"), Category("MyVs：栏目权限"), Browsable(false)]
        public string CoteID
        {
            get
            {
                if (!base.DesignMode)
                {
                    if (this.ViewState["CoteID"] == null)
                    {
                        this.ViewState["CoteID"] = ((ModulePage) this.Page).CoteID;
                    }
                    return (string) this.ViewState["CoteID"];
                }
                return "";
            }
            set
            {
                this.ViewState["CoteID"] = value;
            }
        }

        [Description("栏目模块标识"), Browsable(false), Category("MyVs：栏目权限")]
        public string CoteModuleID
        {
            get
            {
                if (!base.DesignMode)
                {
                    if (this.ViewState["CoteModuleID"] == null)
                    {
                        this.ViewState["CoteModuleID"] = ((ModulePage) this.Page).CoteModuleID;
                    }
                    return (string) this.ViewState["CoteModuleID"];
                }
                return "";
            }
            set
            {
                this.ViewState["CoteModuleID"] = value;
            }
        }

        public int CurrentPageIndex
        {
            get
            {
                return this.ViewState.GetInt("CurrentPageIndex", 0);
            }
            set
            {
                this.ViewState["CurrentPageIndex"] = value;
            }
        }

        [Description("字段隐藏"), Browsable(true), Category("Seven：工具栏")]
        public string FieldHidden
        {
            get
            {
                return this.ViewState.GetString("FieldHidden", "");
            }
            set
            {
                this.ViewState["FieldHidden"] = value;
            }
        }

        public string GridViewHandle
        {
            get
            {
                return this.ViewState.GetString("GridViewHandle", "myGridView");
            }
            set
            {
                this.ViewState["GridViewHandle"] = value;
            }
        }

        public bool IsAutoSortFields
        {
            get
            {
                return this.ViewState.GetBool("IsAutoSortFields", true);
            }
            set
            {
                this.ViewState["IsAutoSortFields"] = value;
            }
        }

        public bool IsSelectNoInfo
        {
            get
            {
                return this.ViewState.GetBool("IsSelectNoInfo", true);
            }
            set
            {
                this.ViewState["IsSelectNoInfo"] = value;
            }
        }

        public bool IsSelectPageSizeMax
        {
            get
            {
                return this.ViewState.GetBool("IsSelectPageSizeMax", false);
            }
            set
            {
                this.ViewState["IsSelectPageSizeMax"] = value;
            }
        }

        public bool IsUserPager
        {
            get
            {
                return this.ViewState.GetBool("IsUserPager", true);
            }
            set
            {
                this.ViewState["IsUserPager"] = value;
            }
        }

        public string ModuleCode
        {
            get
            {
                if ((this.ViewState["ModuleCode"] == null) || string.IsNullOrEmpty((string) this.ViewState["ModuleCode"]))
                {
                    this.ViewState["ModuleCode"] = ((ModulePage) this.Page).PowerPageCode;
                }
                return (string) this.ViewState["ModuleCode"];
            }
            set
            {
                this.ViewState["ModuleCode"] = value;
            }
        }

        internal IList<OperateModuleInfo> ModuleInfoTable
        {
            get
            {
                if (base.DesignMode)
                {
                    return new List<OperateModuleInfo>();
                }
                if (this._objModuleList == null)
                {
                    if (this.ModuleCode.IsNoNull())
                    {
                        if (this.CoteID.IsNoNull() && this.CoteModuleID.IsNoNull())
                        {
                            this._objModuleList = ((ModulePage) this.Page).GetPowerOperateModule(this.ModuleTypeID, this.ModuleCode, this.CoteModuleID, this.CoteID, OperatePlaceType.OperateListBar);
                        }
                        else
                        {
                            this._objModuleList = ((ModulePage) this.Page).GetPowerOperateModule(this.ModuleTypeID, this.ModuleCode, OperatePlaceType.OperateListBar);
                        }
                    }
                    else
                    {
                        this._objModuleList = new List<OperateModuleInfo>();
                    }
                }
                return this._objModuleList;
            }
        }

        public string ModuleTypeID
        {
            get
            {
                return this.ViewState.GetString("ModuleTypeID", ((ModulePage) this.Page).ModuleTypeID);
            }
            set
            {
                this.ViewState["ModuleTypeID"] = value;
            }
        }

        public WTF.Framework.OperateStyle OperateStyle
        {
            get
            {
                if (!base.DesignMode)
                {
                    if (this.ViewState["OperateStyle"] != null)
                    {
                        return (WTF.Framework.OperateStyle) this.ViewState["OperateStyle"];
                    }
                    return ((ModulePage) this.Page).OperateStyle;
                }
                return WTF.Framework.OperateStyle.RightOperate;
            }
            set
            {
                this.ViewState["OperateStyle"] = value;
            }
        }

        public WTF.Controls.OperateType OperateType
        {
            get
            {
                if (this.ViewState["OperateType"] != null)
                {
                    return (WTF.Controls.OperateType) this.ViewState["OperateType"];
                }
                return WTF.Controls.OperateType.TxtButton;
            }
            set
            {
                this.ViewState["OperateType"] = value;
            }
        }

        public override int PageIndex
        {
            get
            {
                return base.PageIndex;
            }
            set
            {
                base.PageIndex = value;
                this.CurrentPageIndex = value;
            }
        }

        public int RecordCount
        {
            get
            {
                return this.ViewState.GetInt("RecordCount", 0);
            }
            set
            {
                this.ViewState["RecordCount"] = value;
            }
        }

        [Browsable(true), Description("列操作命令"), Category("Seven：工具栏")]
        public string RowOperateCommand
        {
            get
            {
                return this.ViewState.GetString("RowOperateCommand", "");
            }
            set
            {
                this.ViewState["RowOperateCommand"] = value;
            }
        }

        public string SelectedNoRowDataKeys
        {
            get
            {
                string str = "";
                foreach (GridViewRow row in this.Rows)
                {
                    CheckBox box = (CheckBox) row.FindControl("chkSelect");
                    if (!((box == null) || box.Checked))
                    {
                        str = str + this.DataKeys[row.RowIndex].Value.ToString() + ",";
                    }
                }
                return str.Trim(new char[] { ',' });
            }
        }

        public string SelectedRadioValue
        {
            get
            {
                foreach (GridViewRow row in this.Rows)
                {
                    RadioButton button = (RadioButton) row.FindControl("chkSelect");
                    if ((button != null) && button.Checked)
                    {
                        return this.DataKeys[row.RowIndex].Value.ToString();
                    }
                }
                if (this.IsSelectNoInfo)
                {
                    SysAssert.InfoHintAssert("对不起你未选择不能进行此操作");
                }
                return "";
            }
        }

        public string SelectedRowDataKeys
        {
            get
            {
                string str = "";
                foreach (GridViewRow row in this.Rows)
                {
                    CheckBox box = (CheckBox) row.FindControl("chkSelect");
                    if ((box != null) && box.Checked)
                    {
                        str = str + this.DataKeys[row.RowIndex].Value.ToString() + ",";
                    }
                }
                str = str.Trim(new char[] { ',' });
                if (this.IsSelectNoInfo && str.IsNull())
                {
                    SysAssert.InfoHintAssert("对不起你未选择不能进行此操作");
                }
                return str;
            }
        }

        public string SelectedRowFirstKey
        {
            get
            {
                foreach (GridViewRow row in this.Rows)
                {
                    CheckBox box = (CheckBox) row.FindControl("chkSelect");
                    if ((box != null) && box.Checked)
                    {
                        return this.DataKeys[row.RowIndex].Value.ToString();
                    }
                }
                if (this.IsSelectNoInfo)
                {
                    SysAssert.InfoHintAssert("对不起你未选择不能进行此操作");
                }
                return "";
            }
        }

        public Dictionary<string, string> StatValueList
        {
            get
            {
                return this._StatSetValueList;
            }
            set
            {
                this._StatSetValueList = value;
            }
        }

        public string ThemePath
        {
            get
            {
                return this.ViewState.GetString("ThemePath", "App_Control/MyGridView");
            }
            set
            {
                this.ViewState["ThemePath"] = value;
            }
        }

        public delegate bool MenuItemCheckEventHandler(object sender, MenuItemCheckEventArgs e);
    }
}

