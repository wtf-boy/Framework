namespace WTF.Controls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyTreeNode : IStateManager, ICloneable
    {
        private TreeNodeBinding binding;
        private PropertyDescriptorCollection boundProperties;
        private object dataItem;
        private int depth;
        private bool gotBinding;
        private IHierarchyData hierarchyData;
        private bool marked;
        private MyTreeNodeCollection nodes;
        private MyTreeNode parent;
        private MyUlTreeView tree;
        private StateBag ViewState;

        public MyTreeNode()
        {
            this.ViewState = new StateBag();
            this.depth = -1;
            this.nodes = null;
        }

        public MyTreeNode(MyUlTreeView myPowerTree)
        {
            this.ViewState = new StateBag();
            this.depth = -1;
            this.nodes = null;
            this.tree = myPowerTree;
        }

        internal void Bind(IHierarchyData hierarchyData)
        {
            this.hierarchyData = hierarchyData;
            this.DataBound = true;
            this.DataPath = hierarchyData.Path;
            this.dataItem = hierarchyData.Item;
            TreeNodeBinding binding = this.GetBinding();
            if (binding != null)
            {
                if (binding.ImageUrlField.Length > 0)
                {
                    this.ImageUrl = Convert.ToString(this.GetBoundPropertyValue(binding.ImageUrlField));
                }
                if (string.IsNullOrEmpty(this.ImageUrl) && (binding.ImageUrl.Length > 0))
                {
                    this.ImageUrl = binding.ImageUrl;
                }
                if (binding.TargetField.Length > 0)
                {
                    this.Target = Convert.ToString(this.GetBoundPropertyValue(binding.TargetField));
                    if (this.Target.Length == 0)
                    {
                        this.Target = binding.Target;
                    }
                }
                else if (binding.Target.Length > 0)
                {
                    this.Target = binding.Target;
                }
                string text = null;
                if (binding.TextField.Length > 0)
                {
                    text = Convert.ToString(this.GetBoundPropertyValue(binding.TextField));
                    if (binding.FormatString.Length > 0)
                    {
                        text = string.Format(binding.FormatString, text);
                    }
                }
                if (string.IsNullOrEmpty(text))
                {
                    if (binding.Text.Length > 0)
                    {
                        text = binding.Text;
                    }
                    else if (binding.Value.Length > 0)
                    {
                        text = binding.Value;
                    }
                }
                if (!string.IsNullOrEmpty(text))
                {
                    this.Text = text;
                }
                if (binding.ToolTipField.Length > 0)
                {
                    this.ToolTip = Convert.ToString(this.GetBoundPropertyValue(binding.ToolTipField));
                }
                if (string.IsNullOrEmpty(this.ToolTip) && (binding.ToolTip.Length > 0))
                {
                    this.ToolTip = binding.ToolTip;
                }
                string str2 = null;
                if (binding.ValueField.Length > 0)
                {
                    str2 = Convert.ToString(this.GetBoundPropertyValue(binding.ValueField));
                }
                if (string.IsNullOrEmpty(str2))
                {
                    if (binding.Value.Length > 0)
                    {
                        str2 = binding.Value;
                    }
                    else if (binding.Text.Length > 0)
                    {
                        str2 = binding.Text;
                    }
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    this.Value = str2;
                }
                else
                {
                    this.Text = this.Value = this.GetDefaultBoundText();
                }
                if (binding.NavigateUrlField.Length > 0)
                {
                    this.NavigateUrl = Convert.ToString(this.GetBoundPropertyValue(binding.NavigateUrlField));
                    if (this.NavigateUrl.Length == 0)
                    {
                        this.NavigateUrl = binding.NavigateUrl;
                    }
                }
                else if (binding.NavigateUrl.Length > 0)
                {
                    this.NavigateUrl = binding.NavigateUrl;
                }
            }
            else
            {
                this.Text = this.Value;
            }
        }

        public virtual object Clone()
        {
            MyTreeNode node = (this.tree != null) ? this.tree.CreateNode() : new MyTreeNode();
            foreach (DictionaryEntry entry in this.ViewState)
            {
                node.ViewState[(string) entry.Key] = ((StateItem) entry.Value).Value;
            }
            foreach (MyTreeNode node2 in this.ChildNodes)
            {
                node.ChildNodes.Add((MyTreeNode) node2.Clone());
            }
            return node;
        }

        private TreeNodeBinding GetBinding()
        {
            if (this.tree == null)
            {
                return null;
            }
            if (!this.gotBinding)
            {
                this.binding = this.tree.FindBindingForNode(this.GetDataItemType(), this.Depth);
                this.gotBinding = true;
            }
            return this.binding;
        }

        private object GetBoundPropertyValue(string name)
        {
            if (this.boundProperties == null)
            {
                if (this.hierarchyData != null)
                {
                    this.boundProperties = TypeDescriptor.GetProperties(this.hierarchyData);
                }
                else
                {
                    this.boundProperties = TypeDescriptor.GetProperties(this.dataItem);
                }
            }
            PropertyDescriptor descriptor = this.boundProperties.Find(name, true);
            if (descriptor == null)
            {
                throw new InvalidOperationException("Property '" + name + "' not found in data bound item");
            }
            if (this.hierarchyData != null)
            {
                return descriptor.GetValue(this.hierarchyData);
            }
            return descriptor.GetValue(this.dataItem);
        }

        private string GetDataItemType()
        {
            if (this.hierarchyData != null)
            {
                return this.hierarchyData.Type;
            }
            if (this.dataItem != null)
            {
                return this.dataItem.GetType().ToString();
            }
            return string.Empty;
        }

        private string GetDefaultBoundText()
        {
            if (this.hierarchyData != null)
            {
                return this.hierarchyData.ToString();
            }
            if (this.dataItem != null)
            {
                return this.dataItem.ToString();
            }
            return string.Empty;
        }

        protected virtual void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] objArray = (object[]) savedState;
                ((IStateManager) this.ViewState).LoadViewState(objArray[0]);
                ((IStateManager) this.ChildNodes).LoadViewState(objArray[1]);
            }
        }

        protected virtual object SaveViewState()
        {
            object[] objArray = new object[] { ((IStateManager) this.ViewState).SaveViewState(), (this.nodes == null) ? null : ((IStateManager) this.nodes).SaveViewState() };
            for (int i = 0; i < objArray.Length; i++)
            {
                if (objArray[i] != null)
                {
                    return objArray;
                }
            }
            return null;
        }

        public void SetParent(MyTreeNode node)
        {
            this.parent = node;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        void IStateManager.LoadViewState(object savedState)
        {
            this.LoadViewState(savedState);
        }

        object IStateManager.SaveViewState()
        {
            return this.SaveViewState();
        }

        void IStateManager.TrackViewState()
        {
            this.TrackViewState();
        }

        protected void TrackViewState()
        {
            if (!this.marked)
            {
                this.marked = true;
                ((IStateManager) this.ViewState).TrackViewState();
                if (this.nodes != null)
                {
                    ((IStateManager) this.nodes).TrackViewState();
                }
            }
        }

        public MyTreeNodeCollection ChildNodes
        {
            get
            {
                if (this.nodes == null)
                {
                    this.nodes = new MyTreeNodeCollection(this);
                    if (this.IsTrackingViewState)
                    {
                        ((IStateManager) this.nodes).TrackViewState();
                    }
                }
                return this.nodes;
            }
        }

        [Browsable(false), DefaultValue(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DataBound
        {
            get
            {
                return ((this.ViewState["DataBound"] != null) && ((bool) this.ViewState["DataBound"]));
            }
            private set
            {
                this.ViewState["DataBound"] = value;
            }
        }

        public string DataPath
        {
            get
            {
                return ((this.ViewState["DataPath"] == null) ? string.Empty : ((string) this.ViewState["DataPath"]));
            }
            private set
            {
                this.ViewState["DataPath"] = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Depth
        {
            get
            {
                if (this.depth == -1)
                {
                    this.depth = 0;
                    for (MyTreeNode node = this.parent; node != null; node = node.parent)
                    {
                        this.depth++;
                    }
                }
                return this.depth;
            }
        }

        [DefaultValue(""), UrlProperty]
        public string ImageUrl
        {
            get
            {
                object obj2 = this.ViewState["ImageUrl"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ImageUrl"] = value;
            }
        }

        protected bool IsTrackingViewState
        {
            get
            {
                return this.marked;
            }
        }

        [UrlProperty, DefaultValue("")]
        public string NavigateUrl
        {
            get
            {
                object obj2 = this.ViewState["NavigateUrl"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["NavigateUrl"] = value;
            }
        }

        public MyTreeNode Parent
        {
            get
            {
                return this.parent;
            }
        }

        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return this.IsTrackingViewState;
            }
        }

        [DefaultValue("")]
        public string Target
        {
            get
            {
                object obj2 = this.ViewState["Target"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Target"] = value;
            }
        }

        [Description("The display text of the tree node."), DefaultValue(""), Localizable(true)]
        public string Text
        {
            get
            {
                object obj2 = this.ViewState["Text"];
                if (obj2 == null)
                {
                    obj2 = this.ViewState["Value"];
                }
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        [UrlProperty, DefaultValue("")]
        public string ToolTip
        {
            get
            {
                object obj2 = this.ViewState["ToolTip"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ToolTip"] = value;
            }
        }

        internal MyUlTreeView Tree
        {
            get
            {
                return this.tree;
            }
            set
            {
                this.tree = value;
                if (this.nodes != null)
                {
                    this.nodes.SetTree(this.tree);
                }
            }
        }

        [DefaultValue(""), Localizable(true)]
        public string Value
        {
            get
            {
                object obj2 = this.ViewState["Value"];
                if (obj2 == null)
                {
                    obj2 = this.ViewState["Text"];
                }
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Value"] = value;
            }
        }
    }
}

