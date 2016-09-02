namespace WTF.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public sealed class MyTreeNodeBindingCollection : StateManagedCollection
    {
        private static Type[] types = new Type[] { typeof(TreeNodeBinding) };

        public int Add(TreeNodeBinding binding)
        {
            return ((IList) this).Add(binding);
        }

        public bool Contains(TreeNodeBinding binding)
        {
            return ((IList) this).Contains(binding);
        }

        public void CopyTo(TreeNodeBinding[] array, int index)
        {
            this.CopyTo(array, index);
        }

        protected override object CreateKnownType(int index)
        {
            return new TreeNodeBinding();
        }

        protected override Type[] GetKnownTypes()
        {
            return types;
        }

        public int IndexOf(TreeNodeBinding binding)
        {
            return ((IList) this).IndexOf(binding);
        }

        public void Insert(int index, TreeNodeBinding binding)
        {
            ((IList) this).Insert(index, binding);
        }

        protected override void OnClear()
        {
            base.OnClear();
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            base.OnRemoveComplete(index, value);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
        }

        public void Remove(TreeNodeBinding binding)
        {
            ((IList) this).Remove(binding);
        }

        public void RemoveAt(int index)
        {
            ((IList) this).RemoveAt(index);
        }

        protected override void SetDirtyObject(object o)
        {
        }

        public TreeNodeBinding this[int i]
        {
            get
            {
                return (TreeNodeBinding) this[i];
            }
            set
            {
                this[i] = value;
            }
        }
    }
}

