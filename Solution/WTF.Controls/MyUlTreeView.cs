namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyUlTreeView : HierarchicalDataBoundControl
    {
        private string _SelectValue = string.Empty;
        private bool _SetIsExpanded = false;
        private List<string> _SetSelectValueList = new List<string>();
        private Hashtable bindings;
        private MyTreeNodeBindingCollection dataBindings = new MyTreeNodeBindingCollection();
        private int ID = 0;
        private MyTreeNodeCollection nodes = null;

        protected internal virtual MyTreeNode CreateNode()
        {
            return new MyTreeNode(this);
        }

        private void FillBoundChildrenRecursive(IHierarchicalEnumerable hEnumerable, MyTreeNodeCollection nodeCollection)
        {
            if (hEnumerable != null)
            {
                foreach (object obj2 in hEnumerable)
                {
                    IHierarchyData hierarchyData = hEnumerable.GetHierarchyData(obj2);
                    MyTreeNode child = new MyTreeNode(this);
                    nodeCollection.Add(child);
                    child.Bind(hierarchyData);
                    if (((this.MaxDataBindDepth < 0) || (child.Depth != this.MaxDataBindDepth)) && ((hierarchyData != null) && hierarchyData.HasChildren))
                    {
                        IHierarchicalEnumerable children = hierarchyData.GetChildren();
                        this.FillBoundChildrenRecursive(children, child.ChildNodes);
                    }
                }
            }
        }

        internal TreeNodeBinding FindBindingForNode(string type, int depth)
        {
            if (this.bindings == null)
            {
                return null;
            }
            TreeNodeBinding binding = (TreeNodeBinding) this.bindings[this.GetBindingKey(type, depth)];
            if (binding != null)
            {
                return binding;
            }
            binding = (TreeNodeBinding) this.bindings[this.GetBindingKey(type, -1)];
            if (binding != null)
            {
                return binding;
            }
            binding = (TreeNodeBinding) this.bindings[this.GetBindingKey(string.Empty, depth)];
            if (binding != null)
            {
                return binding;
            }
            return (TreeNodeBinding) this.bindings[this.GetBindingKey(string.Empty, -1)];
        }

        private string GetBindingKey(string dataMember, int depth)
        {
            return (dataMember + " " + depth);
        }

        private void InitializeDataBindings()
        {
            if ((this.dataBindings != null) && (this.dataBindings.Count > 0))
            {
                this.bindings = new Hashtable();
                foreach (TreeNodeBinding binding in this.dataBindings)
                {
                    string bindingKey = this.GetBindingKey(binding.DataMember, binding.Depth);
                    if (!this.bindings.ContainsKey(bindingKey))
                    {
                        this.bindings[bindingKey] = binding;
                    }
                }
            }
            else
            {
                this.bindings = null;
            }
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] objArray = (object[]) savedState;
                base.LoadViewState(objArray[0]);
                if (objArray[1] != null)
                {
                    ((IStateManager) this.DataBindings).LoadViewState(objArray[1]);
                }
                if (this.ViewStateData && (objArray[2] != null))
                {
                    ((IStateManager) this.Nodes).LoadViewState(objArray[2]);
                }
            }
        }

        protected override void OnPagePreLoad(object sender, EventArgs e)
        {
            base.OnPagePreLoad(sender, e);
            Page page = this.Page;
            if ((page != null) && !page.ClientScript.IsClientScriptIncludeRegistered(typeof(MyUlTreeView), "UlTreeView.js"))
            {
                string url = SysVariable.ApplicationPath + "/" + this.ThemePath + "/UlTreeView.js" + ControlVerHelper.GetVer("?");
                page.ClientScript.RegisterClientScriptInclude(typeof(MyUlTreeView), "UlTreeView.js", url);
            }
        }

        protected override void PerformDataBinding()
        {
            base.PerformDataBinding();
            this.InitializeDataBindings();
            HierarchicalDataSourceView data = this.GetData(string.Empty);
            if (data != null)
            {
                this.Nodes.Clear();
                IHierarchicalEnumerable hEnumerable = data.Select();
                this.FillBoundChildrenRecursive(hEnumerable, this.Nodes);
            }
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID.Replace("$", ""));
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ultreeview");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            List<string> expandPathList = this.ExpandPath.ConvertListString();
            ArrayList list2 = new ArrayList();
            int count = this.Nodes.Count;
            for (int i = 0; i < count; i++)
            {
                this.RenderNode(writer, this.Nodes[i], 1, expandPathList);
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            string str = "{refFather:" + this.RefFather.ToString().ToLower() + ",refChild:" + this.RefChild.ToString().ToLower() + "}";
            writer.Write("jQuery(function () { " + ((this.TreeViewHandle == string.Empty) ? " var myUlTreeView " : this.TreeViewHandle) + " = $('#" + this.UniqueID.Replace("$", "") + "').createmytree(" + str + ");  })");
            writer.RenderEndTag();
        }

        private void RenderNode(HtmlTextWriter writer, MyTreeNode node, int level, List<string> ExpandPathList)
        {
            bool flag = node.ChildNodes.Count > 0;
            string str = "";
            if (flag)
            {
                str = "folder";
                if (string.IsNullOrEmpty(node.NavigateUrl))
                {
                    str = "folder isCliecka";
                }
                if ((((this.Page.IsPostBack && this.SelectListStringValue.Contains(node.Value.ToString())) || (this.ShowExpandCollapse || (level <= this.ExpandDepth))) || (((ExpandPathList.Count > 0) && ExpandPathList.Contains(node.Value.ToString())) && flag)) || (this._SetIsExpanded && this._SetSelectValueList.Contains(node.Value.ToString())))
                {
                    str = str + " Expand";
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Class, str);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Li);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write("&nbsp;");
            writer.RenderEndTag();
            if ((this.ShowType != WTF.Controls.ShowType.None) && (!this.IsLastShowType || (!flag && this.IsLastShowType)))
            {
                if (this.ShowType == WTF.Controls.ShowType.Radio)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, "UL" + this.UniqueID);
                }
                else
                {
                    this.ID++;
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, "UL" + this.ID);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Value, node.Value);
                if (this.SelectListStringValue.Contains(node.Value) || this._SetSelectValueList.Contains(node.Value))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Checked, "Checked");
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Type, (this.ShowType == WTF.Controls.ShowType.CheckBox) ? "checkbox" : "radio");
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();
            }
            if (!string.IsNullOrEmpty(node.NavigateUrl))
            {
                if (!this.NavigateUrlIsClick)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, base.ResolveClientUrl(node.NavigateUrl));
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, node.NavigateUrl);
                }
                string target = node.Target;
                if (string.IsNullOrEmpty(target))
                {
                    target = this.Target;
                }
                if (!string.IsNullOrEmpty(target))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Target, target);
                }
            }
            if (!string.IsNullOrEmpty(node.ToolTip))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Title, node.ToolTip);
            }
            if (!string.IsNullOrEmpty(node.ImageUrl))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "treeAico");
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "background-image: url('" + node.ImageUrl + "')");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.WriteEncodedText(node.Text);
            writer.RenderEndTag();
            writer.RenderEndTag();
            if (flag)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "node");
                writer.AddAttribute("treeLevel", level.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                int count = node.ChildNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    this.RenderNode(writer, node.ChildNodes[i], level + 1, ExpandPathList);
                }
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
        }

        protected override object SaveViewState()
        {
            int num = this.ViewStateData ? 3 : 2;
            object[] objArray = new object[num];
            objArray[0] = base.SaveViewState();
            objArray[1] = (this.dataBindings == null) ? null : ((IStateManager) this.dataBindings).SaveViewState();
            if (this.ViewStateData)
            {
                objArray[2] = (this.nodes == null) ? null : ((IStateManager) this.nodes).SaveViewState();
            }
            for (int i = objArray.Length - 1; i >= 0; i--)
            {
                if (objArray[i] != null)
                {
                    return objArray;
                }
            }
            return null;
        }

        public void SetSelectValue(IEnumerable<RolePowerKey> value, bool isExpanded)
        {
            foreach (RolePowerKey key in value)
            {
                this._SetSelectValueList.Add(key.ToKey);
            }
            this._SetIsExpanded = isExpanded;
        }

        public void SetSelectValue(string value, bool isExpanded)
        {
            this._SetSelectValueList = value.ConvertListString();
            this._SetIsExpanded = isExpanded;
        }

        public void SetSelectValue<T>(IEnumerable<T> value, bool isExpanded)
        {
            this.SetSelectValue(value.ConvertListToString<T>(), isExpanded);
        }

        protected override void TrackViewState()
        {
            this.EnsureDataBound();
            base.TrackViewState();
            if (this.dataBindings != null)
            {
                ((IStateManager) this.dataBindings).TrackViewState();
            }
            if (this.ViewStateData && (this.nodes != null))
            {
                ((IStateManager) this.nodes).TrackViewState();
            }
        }

        [Editor("System.Web.UI.Design.WebControls.TreeViewBindingsEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Category("Data"), Description("TreeView_DataBindings"), DefaultValue(""), MergableProperty(false), PersistenceMode(PersistenceMode.InnerProperty)]
        public MyTreeNodeBindingCollection DataBindings
        {
            get
            {
                if (this.dataBindings == null)
                {
                    this.dataBindings = new MyTreeNodeBindingCollection();
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager) this.dataBindings).TrackViewState();
                    }
                }
                return this.dataBindings;
            }
        }

        public int ExpandDepth
        {
            get
            {
                return this.ViewState.GetInt("ExpandDepth", 0);
            }
            set
            {
                this.ViewState["ExpandDepth"] = value;
            }
        }

        public string ExpandPath
        {
            get
            {
                return this.ViewState.GetString("ExpandPath");
            }
            set
            {
                this.ViewState["ExpandPath"] = value;
            }
        }

        public bool IsLastShowType
        {
            get
            {
                return this.ViewState.GetBool("IsLastShowType", false);
            }
            set
            {
                this.ViewState["IsLastShowType"] = value;
            }
        }

        [DefaultValue(-1)]
        public int MaxDataBindDepth
        {
            get
            {
                return ((this.ViewState["MaxDataBindDepth"] == null) ? -1 : ((int) this.ViewState["MaxDataBindDepth"]));
            }
            set
            {
                this.ViewState["MaxDataBindDepth"] = value;
            }
        }

        public bool NavigateUrlIsClick
        {
            get
            {
                return this.ViewState.GetBool("NavigateUrlIsClick", false);
            }
            set
            {
                this.ViewState["NavigateUrlIsClick"] = value;
            }
        }

        public MyTreeNodeCollection Nodes
        {
            get
            {
                if (this.nodes == null)
                {
                    this.nodes = new MyTreeNodeCollection(this);
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager) this.nodes).TrackViewState();
                    }
                }
                return this.nodes;
            }
        }

        [DefaultValue(true)]
        public bool RefChild
        {
            get
            {
                return this.ViewState.GetBool("RefChild", true);
            }
            set
            {
                this.ViewState["RefChild"] = value;
            }
        }

        [DefaultValue(true)]
        public bool RefFather
        {
            get
            {
                return this.ViewState.GetBool("RefFather", true);
            }
            set
            {
                this.ViewState["RefFather"] = value;
            }
        }

        public List<Guid> SelectListGuidValue
        {
            get
            {
                return this.SelectValue.ConvertListGuid();
            }
        }

        public List<int> SelectListIntValue
        {
            get
            {
                return this.SelectValue.ConvertListInt();
            }
        }

        public List<string> SelectListStringValue
        {
            get
            {
                return this.SelectValue.ConvertListString();
            }
        }

        public List<RolePowerKey> SelectRolePowerKeyValue
        {
            get
            {
                List<RolePowerKey> list = new List<RolePowerKey>();
                foreach (string str in this.SelectListStringValue)
                {
                    list.Add(new RolePowerKey(str));
                }
                return list;
            }
        }

        public string SelectValue
        {
            get
            {
                if (this._SelectValue == string.Empty)
                {
                    foreach (string str in this.Page.Request.Form.Keys)
                    {
                        if (str.IndexOf("UL") >= 0)
                        {
                            this._SelectValue = this._SelectValue + this.Page.Request.Form[str] + ",";
                        }
                    }
                    this._SelectValue = this._SelectValue.TrimEndComma();
                }
                return this._SelectValue;
            }
        }

        [DefaultValue(true)]
        public bool ShowExpandCollapse
        {
            get
            {
                return this.ViewState.GetBool("ShowExpandCollapse", false);
            }
            set
            {
                this.ViewState["ShowExpandCollapse"] = value;
            }
        }

        [DefaultValue(false)]
        public bool ShowLines
        {
            get
            {
                return this.ViewState.GetBool("ShowLines", false);
            }
            set
            {
                this.ViewState["ShowLines"] = value;
            }
        }

        public WTF.Controls.ShowType ShowType
        {
            get
            {
                return this.ViewState.GetT<WTF.Controls.ShowType>("ShowType", WTF.Controls.ShowType.None);
            }
            set
            {
                this.ViewState["ShowType"] = value;
            }
        }

        [DefaultValue("")]
        public string Target
        {
            get
            {
                return this.ViewState.GetString("Target", string.Empty);
            }
            set
            {
                this.ViewState["Target"] = value;
            }
        }

        public string ThemePath
        {
            get
            {
                return this.ViewState.GetString("ThemePath", "App_Control/MyUlTreeView");
            }
            set
            {
                this.ViewState["ThemePath"] = value;
            }
        }

        public string TreeViewHandle
        {
            get
            {
                return this.ViewState.GetString("TreeViewHandle");
            }
            set
            {
                this.ViewState["TreeViewHandle"] = value;
            }
        }

        public bool ViewStateData
        {
            get
            {
                return this.ViewState.GetBool("ViewStateData", true);
            }
            set
            {
                this.ViewState["ViewStateData"] = value;
            }
        }
    }
}

