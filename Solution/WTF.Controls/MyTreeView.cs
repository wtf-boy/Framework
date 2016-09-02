namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class MyTreeView : TreeView
    {
        private void GetChild(TreeNode treeNode, List<string> value, bool isExpanded)
        {
            foreach (TreeNode node in treeNode.ChildNodes)
            {
                if (value.Contains(node.Value))
                {
                    node.Checked = true;
                    node.Expanded = true;
                }
                this.GetChild(node, value, isExpanded);
            }
        }

        public void SetSelectValue(string value, bool isExpanded)
        {
            List<string> list = value.ConvertListString();
            if (list.Count > 0)
            {
                foreach (TreeNode node in base.Nodes)
                {
                    if (list.Contains(node.Value))
                    {
                        node.Checked = true;
                        node.Expanded = new bool?(isExpanded);
                    }
                    this.GetChild(node, list, isExpanded);
                }
            }
        }

        public void SetSelectValue<T>(IEnumerable<T> value, bool isExpanded)
        {
            this.SetSelectValue(value.ConvertListToString<T>(), isExpanded);
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

        public string SelectValue
        {
            get
            {
                string str = "";
                foreach (TreeNode node in base.CheckedNodes)
                {
                    if (str.IndexOf(node.Value) < 0)
                    {
                        str = str + node.Value + ",";
                    }
                }
                return str.TrimEndComma();
            }
        }
    }
}

