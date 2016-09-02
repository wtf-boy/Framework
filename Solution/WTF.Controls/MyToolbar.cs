namespace WTF.Controls
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Power;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [Designer("WTF.Controls.MyToolbarDesigner")]
    public class MyToolbar : WebControl, INamingContainer
    {
        private string _className = "toolbar";
        private ToolButtons _ToolButtons;
        private ModuleRule CurrentModuleRule = new ModuleRule();

        public event MyToolbarCommandEventHandler ItemCommand;

        protected override void CreateChildControls()
        {
            LogModuleInfo info = new LogModuleInfo(false, "");
            IList<OperateModuleInfo> list = null;
            if (list == null)
            {
                if (this.CoteID.IsNoNull() && this.CoteModuleID.IsNoNull())
                {
                    list = ((ModulePage) this.Page).GetPowerOperateModule(this.ModuleTypeID, this.ModuleCode, this.CoteModuleID, this.CoteID, this.OperatePlaceTypeValue);
                }
                else
                {
                    list = ((ModulePage) this.Page).GetPowerOperateModule(this.ModuleTypeID, this.ModuleCode, this.OperatePlaceTypeValue);
                }
                List<OperateModuleInfo> buttonsModule = this.Buttons.GetButtonsModule();
                if (buttonsModule != null)
                {
                    foreach (OperateModuleInfo info2 in buttonsModule)
                    {
                        list.Add(info2);
                    }
                }
            }
            if (list.Count != 0)
            {
                this.Controls.Clear();
                int num = 0;
                bool flag = true;
                foreach (OperateModuleInfo info3 in from s in list
                    orderby s.SortIndex
                    select s)
                {
                    if (this.CommandHidden.IsNoNull() && this.CommandHidden.Split(new char[] { ',' }).Contains<string>(info3.CommandName))
                    {
                        continue;
                    }
                    bool flag2 = true;
                    if (this.CommandDisabled.IsNoNull() && this.CommandDisabled.Split(new char[] { ',' }).Contains<string>(info3.CommandName))
                    {
                        flag2 = false;
                    }
                    if ((flag2 && info3.MenuField.IsNoNull()) && (this.DataSource != null))
                    {
                        string[] strArray = info3.MenuField.Split(new char[] { '|' });
                        string[] strArray2 = info3.MenuValue.Split(new char[] { '|' });
                        string[] strArray3 = info3.MenuCal.Split(new char[] { '|' });
                        int length = strArray.Length;
                        if ((strArray2.Length == length) && (strArray3.Length == length))
                        {
                            for (int i = 0; i < length; i++)
                            {
                                string item = Reflector.GetPropertyValue(this.DataSource, strArray[i]).DataConvertString();
                                List<string> list3 = strArray2[i].ConvertListString();
                                string str3 = strArray3[i];
                                if (str3 != null)
                                {
                                    if (!(str3 == "in"))
                                    {
                                        if (str3 == "not")
                                        {
                                            goto Label_0305;
                                        }
                                    }
                                    else
                                    {
                                        flag2 = list3.IndexOf(item) >= 0;
                                    }
                                }
                                goto Label_0315;
                            Label_0305:
                                flag2 = list3.IndexOf(item) == -1;
                            Label_0315:
                                if (!flag2)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (flag)
                    {
                        this.Controls.Add(new LiteralControl("<div class=\"" + this.ClassName + "\" ><div class=\"operator-left-menu\">"));
                        flag = false;
                    }
                    ToolbarLinkButton child = new ToolbarLinkButton {
                        Enabled = flag2
                    };
                    if (!flag2)
                    {
                        child.Attributes.Add("disabled ", "disabled");
                    }
                    else
                    {
                        child.ClickScriptFun = info3.ClickScriptFun;
                    }
                    if (!string.IsNullOrEmpty(info3.ImageUrl))
                    {
                        child.CssClass = "btn-ico";
                        child.Style.Value = string.Format("background-image: url({0}) !important;", info3.ImageUrl);
                    }
                    else
                    {
                        child.CssClass = "btn-text ";
                    }
                    if (!string.IsNullOrEmpty(info3.ImageCss))
                    {
                        child.CssClass = "btn-ico " + info3.ImageCss;
                    }
                    child.Text = info3.ModuleName;
                    child.ID = info3.CommandName;
                    child.ToolTip = info3.ToolTip;
                    child.CommandName = info3.CommandName;
                    child.UserValidationGroup = info3.ValGroupName;
                    child.CommandArgument = info3.CommandArgument;
                    child.Click += new EventHandler(this.LinkButtonClick);
                    Literal literal = new Literal {
                        Text = "<span>|</span>"
                    };
                    if (num > 0)
                    {
                        this.Controls.Add(literal);
                    }
                    num++;
                    this.Controls.Add(child);
                }
                if (this.Controls.Count > 0)
                {
                    this.Controls.Add(new LiteralControl("</div>"));
                    string str2 = "";
                    if (this.IsAddOperatorLog && this.MenuPowerID.IsNoNullOrWhiteSpace())
                    {
                        str2 = "<a  class=\"btn-ico moduleButton\"  onclick=\"javascript:showopen('" + ("../../ServiceLayer/Loging/OperatorHistoryList.aspx?HistoryID=" + this.MenuPowerID).EncryptModuleQuery() + "',1200,500);\">操作日志</a>";
                    }
                    if (this.IsAddRefresh)
                    {
                        str2 = str2 + (str2.IsNoNullOrWhiteSpace() ? "<span>|</span>" : "") + "<a  class=\"btn-ico refreshButton\" href=\"javascript: window.location=window.location;\">&nbsp;</a>";
                    }
                    if (str2.IsNoNullOrWhiteSpace())
                    {
                        this.Controls.Add(new LiteralControl("<div class=\"operator-right-menu\"> " + str2 + "</div>"));
                    }
                    this.Controls.Add(new LiteralControl("</div>"));
                }
            }
        }

        private void LinkButtonClick(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton) sender;
            try
            {
                this.OnItemCommand(button.CommandName, button.CommandArgument);
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

        public void OnItemCommand(string commandName, string commandArgument)
        {
            if (this.ItemCommand != null)
            {
                this.ItemCommand(this, new MyCommandEventArgs(commandName, commandArgument));
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("按钮设置"), Category("Seven:按钮属性"), PersistenceMode(PersistenceMode.InnerProperty)]
        public ToolButtons Buttons
        {
            get
            {
                if (this._ToolButtons == null)
                {
                    this._ToolButtons = new ToolButtons();
                }
                return this._ToolButtons;
            }
        }

        public string ClassName
        {
            get
            {
                return this._className;
            }
            set
            {
                this._className = value;
            }
        }

        [Category("Seven：工具栏"), Browsable(true), Description("禁止命令起用")]
        public string CommandDisabled
        {
            get
            {
                if (this.ViewState["CommandDisabled"] != null)
                {
                    return (string) this.ViewState["CommandDisabled"];
                }
                return "";
            }
            set
            {
                this.ViewState["CommandDisabled"] = value;
            }
        }

        [Description("隐藏命令"), Browsable(true), Category("Seven：工具栏")]
        public string CommandHidden
        {
            get
            {
                if (this.ViewState["CommandHidden"] != null)
                {
                    return (string) this.ViewState["CommandHidden"];
                }
                return "";
            }
            set
            {
                this.ViewState["CommandHidden"] = value;
            }
        }

        [Browsable(false), Description("栏目标识"), Category("MyVs：栏目权限")]
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

        public object DataSource
        {
            get
            {
                return this.ViewState["DataSource"];
            }
            set
            {
                this.ViewState["DataSource"] = value;
            }
        }

        public bool IsAddOperatorLog
        {
            get
            {
                return ((this.ViewState["IsAddOperatorLog"] != null) && ((bool) this.ViewState["IsAddOperatorLog"]));
            }
            set
            {
                this.ViewState["IsAddOperatorLog"] = value;
            }
        }

        public bool IsAddRefresh
        {
            get
            {
                if (this.ViewState["IsAddRefresh"] != null)
                {
                    return (bool) this.ViewState["IsAddRefresh"];
                }
                return true;
            }
            set
            {
                this.ViewState["IsAddRefresh"] = value;
            }
        }

        [Browsable(false), Description("菜单标识"), Category("MyVs：栏目权限")]
        public string MenuPowerID
        {
            get
            {
                if (!base.DesignMode)
                {
                    if (this.ViewState["MenuPowerID"] == null)
                    {
                        this.ViewState["MenuPowerID"] = ((ModulePage) this.Page).MenuPowerID;
                    }
                    return (string) this.ViewState["MenuPowerID"];
                }
                return "";
            }
            set
            {
                this.ViewState["MenuPowerID"] = value;
            }
        }

        [Description("模块代码"), Browsable(true), Category("Seven：工具栏")]
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

        public string ModuleTypeID
        {
            get
            {
                if (!base.DesignMode)
                {
                    if (this.ViewState["ModuleTypeID"] == null)
                    {
                        this.ViewState["ModuleTypeID"] = ((ModulePage) this.Page).ModuleTypeID;
                    }
                    return (string) this.ViewState["ModuleTypeID"];
                }
                return "";
            }
            set
            {
                this.ViewState["ModuleTypeID"] = value;
            }
        }

        [Browsable(true), Description("按钮位置"), Category("Seven：工具栏")]
        public OperatePlaceType OperatePlaceTypeValue
        {
            get
            {
                if (this.ViewState["OperatePlaceType"] == null)
                {
                    return OperatePlaceType.OperateBottomBar;
                }
                return (OperatePlaceType) this.ViewState["OperatePlaceType"];
            }
            set
            {
                this.ViewState["OperatePlaceType"] = value;
            }
        }

        public delegate void MyToolbarCommandEventHandler(object sender, MyCommandEventArgs e);
    }
}

