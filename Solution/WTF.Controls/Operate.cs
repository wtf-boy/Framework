namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class Operate : DataControlField
    {
        private string _CommandExpandArgumentFields = string.Empty;
        private ToolButtons _ToolButtons;
        private IList<OperateModuleInfo> ModuleInfoTable = null;

        public Operate()
        {
            this.HeaderText = "操作";
        }

        private void cell_DataBinding(object sender, EventArgs e)
        {
            TableCell cell = (TableCell) sender;
            GridViewRow namingContainer = (GridViewRow) cell.NamingContainer;
            if (this.ModuleInfoTable == null)
            {
                this.ModuleInfoTable = ((MyGridView) base.Control).ModuleInfoTable;
                List<OperateModuleInfo> buttonsModule = this.Buttons.GetButtonsModule();
                if (buttonsModule != null)
                {
                    foreach (OperateModuleInfo info in buttonsModule)
                    {
                        this.ModuleInfoTable.Add(info);
                    }
                }
                List<OperateModuleInfo> list2 = ((MyGridView) base.Control).Buttons.GetButtonsModule();
                if (list2 != null)
                {
                    foreach (OperateModuleInfo info in list2)
                    {
                        this.ModuleInfoTable.Add(info);
                    }
                }
            }
            string commandDisabled = ((MyGridView) base.Control).CommandDisabled;
            foreach (OperateModuleInfo info2 in this.ModuleInfoTable)
            {
                bool flag = true;
                if (commandDisabled.IsNoNull() && commandDisabled.Split(new char[] { ',' }).Contains<string>(info2.CommandName))
                {
                    flag = false;
                }
                if (flag)
                {
                    MenuItemCheckEventArgs args = new MenuItemCheckEventArgs(info2.CommandName, namingContainer);
                    flag = ((MyGridView) base.Control).OnMenuItemCheckCommand(args);
                }
                if (flag && info2.MenuField.IsNoNull())
                {
                    string[] strArray = info2.MenuField.Split(new char[] { '|' });
                    string[] strArray2 = info2.MenuValue.Split(new char[] { '|' });
                    string[] strArray3 = info2.MenuCal.Split(new char[] { '|' });
                    int length = strArray.Length;
                    if ((strArray2.Length == length) && (strArray3.Length == length))
                    {
                        for (int i = 0; i < length; i++)
                        {
                            string item = DataBinder.Eval(namingContainer.DataItem, strArray[i]).DataConvertString();
                            List<string> list3 = strArray2[i].ConvertListString();
                            string str8 = strArray3[i];
                            if (str8 != null)
                            {
                                if (!(str8 == "in"))
                                {
                                    if (str8 == "not")
                                    {
                                        goto Label_02AA;
                                    }
                                }
                                else
                                {
                                    flag = list3.IndexOf(item) >= 0;
                                }
                            }
                            goto Label_02BA;
                        Label_02AA:
                            flag = list3.IndexOf(item) == -1;
                        Label_02BA:
                            if (!flag)
                            {
                                break;
                            }
                        }
                    }
                }
                if (!flag)
                {
                    WebControl control = (WebControl) cell.FindControl(info2.CommandName);
                    if (control != null)
                    {
                        control.Enabled = flag;
                        if (!flag)
                        {
                            control.Attributes.Add("disabled ", "disabled");
                            if (control is MenuLinkButton)
                            {
                                ((MenuLinkButton) control).OnClientClick = string.Empty;
                            }
                            else if (control is MenuImageButton)
                            {
                                ((MenuImageButton) control).OnClientClick = string.Empty;
                            }
                        }
                    }
                }
                else if (this.CommandExpandArgumentFields.IsNoNull() || info2.ClickScriptFun.IsNoNull())
                {
                    Control control2 = cell.FindControl(info2.CommandName);
                    if (control2 != null)
                    {
                        if (this.CommandExpandArgumentFields.IsNoNull())
                        {
                            string str3 = string.Empty;
                            foreach (string str4 in this.CommandExpandArgumentFields.Split(new char[] { ',' }))
                            {
                                str3 = str3 + DataBinder.Eval(namingContainer.DataItem, str4).DataConvertString() + ",";
                            }
                            str3 = str3.TrimEndComma();
                            if (control2 is MenuLinkButton)
                            {
                                ((MenuLinkButton) control2).CommandExpandArgument = str3;
                            }
                            else if (control2 is MenuImageButton)
                            {
                                ((MenuImageButton) control2).CommandExpandArgument = str3;
                            }
                        }
                        if (info2.ClickScriptFun.IsNoNull() && (info2.ClickScriptFun.Matches(@"{\w+}", RegexOptions.IgnoreCase).Count > 0))
                        {
                            string onClientClick = "";
                            if (control2 is MenuLinkButton)
                            {
                                onClientClick = ((MenuLinkButton) control2).OnClientClick;
                            }
                            else if (control2 is MenuImageButton)
                            {
                                onClientClick = ((MenuImageButton) control2).OnClientClick;
                            }
                            foreach (Match match in info2.ClickScriptFun.Matches(@"{\w+}", RegexOptions.IgnoreCase))
                            {
                                string str6 = match.Value.TrimStart(new char[] { '{' }).TrimEnd(new char[] { '}' }).Trim();
                                if (!string.IsNullOrWhiteSpace(str6))
                                {
                                    string newValue = DataBinder.Eval(namingContainer.DataItem, str6).DataConvertString();
                                    onClientClick = onClientClick.Replace(match.Value, newValue);
                                }
                            }
                            if (control2 is MenuLinkButton)
                            {
                                ((MenuLinkButton) control2).OnClientClick = onClientClick;
                            }
                            else if (control2 is MenuImageButton)
                            {
                                ((MenuImageButton) control2).OnClientClick = onClientClick;
                            }
                        }
                    }
                }
            }
        }

        protected override DataControlField CreateField()
        {
            return new Operate();
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cellType == DataControlCellType.DataCell)
            {
                cell.DataBinding += new EventHandler(this.cell_DataBinding);
                this.InitializeDataCell(cell, rowState, rowIndex);
            }
        }

        protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState, int rowIndex)
        {
            if (this.ModuleInfoTable == null)
            {
                this.ModuleInfoTable = ((MyGridView) base.Control).ModuleInfoTable;
                List<OperateModuleInfo> buttonsModule = this.Buttons.GetButtonsModule();
                if (buttonsModule != null)
                {
                    foreach (OperateModuleInfo info in buttonsModule)
                    {
                        this.ModuleInfoTable.Add(info);
                    }
                }
                List<OperateModuleInfo> list2 = ((MyGridView) base.Control).Buttons.GetButtonsModule();
                if (list2 != null)
                {
                    foreach (OperateModuleInfo info in list2)
                    {
                        this.ModuleInfoTable.Add(info);
                    }
                }
            }
            if (this.ModuleInfoTable.Count > 0)
            {
                string commandHidden = ((MyGridView) base.Control).CommandHidden;
                Guid guid = Guid.NewGuid();
                cell.Attributes.Add("class", "tbdOperate");
                foreach (OperateModuleInfo info2 in from s in this.ModuleInfoTable
                    orderby s.SortIndex
                    select s)
                {
                    if (commandHidden.IsNull() || !commandHidden.Split(new char[] { ',' }).Contains<string>(info2.CommandName))
                    {
                        if (this.OperateType == WTF.Controls.OperateType.TxtButton)
                        {
                            MenuLinkButton child = new MenuLinkButton {
                                ID = info2.CommandName,
                                Text = info2.ModuleName,
                                CommandName = info2.CommandName,
                                CommandArgument = ((MyGridView) base.Control).DataKeys[rowIndex].Value.ToString()
                            };
                            if (!string.IsNullOrEmpty(info2.ClickScriptFun))
                            {
                                child.OnClientClick = "try{return " + info2.ClickScriptFun + ";}catch(e){return true;}";
                            }
                            cell.Controls.Add(child);
                        }
                        else
                        {
                            MenuImageButton button2 = new MenuImageButton {
                                ID = info2.CommandName
                            };
                            if (commandHidden.IsMatch(info2.CommandName))
                            {
                                button2.Enabled = false;
                            }
                            button2.ImageUrl = info2.ImageUrl;
                            button2.ToolTip = info2.ModuleName;
                            button2.CommandName = info2.CommandName;
                            button2.CommandArgument = ((MyGridView) base.Control).DataKeys[rowIndex].Value.ToString();
                            if (!string.IsNullOrEmpty(info2.ClickScriptFun))
                            {
                                button2.OnClientClick = "try{return " + info2.ClickScriptFun + ";}catch(e){return true;}";
                            }
                            cell.Controls.Add(button2);
                        }
                    }
                }
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("按钮设置"), Category("Seven:按钮属性")]
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
            set
            {
                this._ToolButtons = value;
            }
        }

        public string CommandExpandArgumentFields
        {
            get
            {
                if (this._CommandExpandArgumentFields == string.Empty)
                {
                    this._CommandExpandArgumentFields = ((MyGridView) base.Control).CommandExpandArgumentFields;
                }
                return this._CommandExpandArgumentFields;
            }
        }

        public WTF.Controls.OperateType OperateType
        {
            get
            {
                if (base.ViewState["OperateType"] != null)
                {
                    return (WTF.Controls.OperateType) base.ViewState["OperateType"];
                }
                return WTF.Controls.OperateType.TxtButton;
            }
            set
            {
                base.ViewState["OperateType"] = value;
            }
        }
    }
}

