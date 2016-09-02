namespace WTF.Controls
{
    using WTF.Framework;
    using WTF.Power;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Linq;
    [Designer("WTF.Controls.MyToolbarDesigner")]
    public class MyTabBar : WebControl, INamingContainer
    {
        private string _className = "mytabbar";
        private ModuleRule CurrentModuleRule = new ModuleRule();

        public event MyToolbarCommandEventHandler ItemCommand;

        protected override void CreateChildControls()
        {
            IList<OperateModuleInfo> list = null;
            if ((this.CoteID.IsNoNull() && this.CoteModuleID.IsNoNull()) && ((ModulePage)this.Page).IsCheckCotePower(this.CoteModuleID))
            {
                list = ((ModulePage)this.Page).GetPowerOperateModule(this.ModuleTypeID, this.ModuleCode, this.CoteModuleID, this.CoteID, this.OperatePlaceTypeValue);
            }
            else
            {
                list = ((ModulePage)this.Page).GetPowerOperateModule(this.ModuleTypeID, this.ModuleCode, this.OperatePlaceTypeValue);
            }
            this.Controls.Clear();
            this.Controls.Add(new LiteralControl("<div class=\"" + this.ClassName + " tabsbarevent\" ><div class='mytabbar_tab_wrap'>"));
            foreach (OperateModuleInfo info in from s in list
                                               orderby s.SortIndex
                                               select s)
            {
                if (this.CommandHidden.IsNoNull() && this.CommandHidden.IsMatch(info.CommandName))
                {
                    continue;
                }
                bool flag = true;
                if (this.OnlyEnableCommand.IsNoNull())
                {
                    if (this.OnlyEnableCommand.IndexOf(info.CommandName) >= 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else if (this.CommandDisabled.IsNoNull() && this.CommandDisabled.IsMatch(info.CommandName))
                {
                    flag = false;
                }
                if ((flag && info.MenuField.IsNoNull()) && (this.DataSource != null))
                {
                    string[] strArray = info.MenuField.Split(new char[] { '|' });
                    string[] strArray2 = info.MenuValue.Split(new char[] { '|' });
                    string[] strArray3 = info.MenuCal.Split(new char[] { '|' });
                    int length = strArray.Length;
                    if ((strArray2.Length == length) && (strArray3.Length == length))
                    {
                        for (int i = 0; i < length; i++)
                        {
                            int item = Reflector.GetPropertyValue(this.DataSource, strArray[i]).ConvertInt();
                            List<int> list2 = strArray2[i].ConvertListInt();
                            string str2 = strArray3[i];
                            if (str2 != null)
                            {
                                if (!(str2 == "in"))
                                {
                                    if (str2 == "not")
                                    {
                                        goto Label_02B4;
                                    }
                                }
                                else
                                {
                                    flag = list2.IndexOf(item) >= 0;
                                }
                            }
                            goto Label_02C3;
                        Label_02B4:
                            flag = list2.IndexOf(item) == -1;
                        Label_02C3:
                            if (!flag)
                            {
                                break;
                            }
                        }
                    }
                }
                string commandArgument = info.CommandArgument;
                if (this.QueryString.IsNoNull())
                {
                    commandArgument = commandArgument + ((commandArgument.IndexOf("?") >= 0) ? "" : "?") + commandArgument;
                }
                commandArgument = commandArgument.EncryptModuleQuery();
                HyperLink child = new HyperLink
                {
                    Enabled = flag
                };
                if (!flag)
                {
                    child.Attributes.Add("disabled ", "disabled");
                }
                child.Attributes.Add("refUrl", info.CommandArgument);
                child.Attributes.Add("relModuleID", info.ModuleID.ToString());
                if ((flag && this.DefaultLoadCommand.IsNoNull()) && (info.CommandName.ToLower() == this.DefaultLoadCommand.ToLower()))
                {
                    child.CssClass = "tab_current";
                }
                child.Text = info.ModuleName;
                child.ID = info.CommandName;
                child.ToolTip = info.ToolTip;
                this.Controls.Add(child);
            }
            this.Controls.Add(new LiteralControl("</div></div>"));
        }

        protected override void OnInit(EventArgs e)
        {
            if (!base.DesignMode)
            {
                Literal child = new Literal
                {
                    Text = "<script src=\"" + SysVariable.ApplicationPath + "/" + this.ThemePath + "/mytabbar.js\" type=\"text/javascript\"></script>"
                };
                this.Page.Header.Controls.Add(child);
            }
        }

        public void OnItemCommand(string commandName, string commandArgument)
        {
            if (this.ItemCommand != null)
            {
                this.ItemCommand(this, new MyCommandEventArgs(commandName, commandArgument));
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

        [Browsable(true), Category("Seven：标签栏"), Description("禁止命令起用")]
        public string CommandDisabled
        {
            get
            {
                if (this.ViewState["CommandDisabled"] != null)
                {
                    return (string)this.ViewState["CommandDisabled"];
                }
                return "";
            }
            set
            {
                this.ViewState["CommandDisabled"] = value;
            }
        }

        [Description("隐藏命令"), Browsable(true), Category("Seven：标签栏")]
        public string CommandHidden
        {
            get
            {
                if (this.ViewState["CommandHidden"] != null)
                {
                    return (string)this.ViewState["CommandHidden"];
                }
                return "";
            }
            set
            {
                this.ViewState["CommandHidden"] = value;
            }
        }

        [Browsable(false), Description("栏目标识"), Category("Seven：栏目权限")]
        public string CoteID
        {
            get
            {
                if (this.ViewState["CoteID"] != null)
                {
                    return (string)this.ViewState["CoteID"];
                }
                return "";
            }
            set
            {
                this.ViewState["CoteID"] = value;
            }
        }

        [Description("栏目模块标识"), Browsable(false), Category("Seven：栏目权限")]
        public string CoteModuleID
        {
            get
            {
                return this.ViewState.GetString("CoteModuleID", "");
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

        [Description("默认加载标签"), Browsable(true), Category("Seven：标签栏")]
        public string DefaultLoadCommand
        {
            get
            {
                if (this.ViewState["DefaultLoadCommand"] != null)
                {
                    return (string)this.ViewState["DefaultLoadCommand"];
                }
                return "";
            }
            set
            {
                this.ViewState["DefaultLoadCommand"] = value;
            }
        }

        [Description("模块代码"), Category("Seven：工具栏"), Browsable(true)]
        public string ModuleCode
        {
            get
            {
                if ((this.ViewState["ModuleCode"] == null) || string.IsNullOrEmpty((string)this.ViewState["ModuleCode"]))
                {
                    this.ViewState["ModuleCode"] = ((ModulePage)this.Page).PowerPageCode;
                }
                return (string)this.ViewState["ModuleCode"];
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
                        this.ViewState["ModuleTypeID"] = ((ModulePage)this.Page).ModuleTypeID;
                    }
                    return (string)this.ViewState["ModuleTypeID"];
                }
                return "";
            }
            set
            {
                this.ViewState["ModuleTypeID"] = value;
            }
        }

        [Browsable(true), Category("Seven：标签栏"), Description("仅起用标签")]
        public string OnlyEnableCommand
        {
            get
            {
                if (this.ViewState["OnlyEnableCommand"] != null)
                {
                    return (string)this.ViewState["OnlyEnableCommand"];
                }
                return "";
            }
            set
            {
                this.ViewState["OnlyEnableCommand"] = value;
            }
        }

        [Description("按钮位置"), Browsable(true), Category("Seven：工具栏")]
        public OperatePlaceType OperatePlaceTypeValue
        {
            get
            {
                if (this.ViewState["OperatePlaceType"] == null)
                {
                    return OperatePlaceType.OperateBottomBar;
                }
                return (OperatePlaceType)this.ViewState["OperatePlaceType"];
            }
            set
            {
                this.ViewState["OperatePlaceType"] = value;
            }
        }

        [Description("链接地址参数"), Browsable(true), Category("Seven：标签栏")]
        public string QueryString
        {
            get
            {
                if (this.ViewState["QueryString"] != null)
                {
                    return (string)this.ViewState["QueryString"];
                }
                return "";
            }
            set
            {
                this.ViewState["QueryString"] = value;
            }
        }

        public string ThemePath
        {
            get
            {
                if (this.ViewState["ThemePath"] == null)
                {
                    return "App_Control/MyTabBar";
                }
                return (string)this.ViewState["ThemePath"];
            }
            set
            {
                this.ViewState["ThemePath"] = value;
            }
        }

        public delegate void MyToolbarCommandEventHandler(object sender, MyCommandEventArgs e);
    }
}

