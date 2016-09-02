namespace WTF.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Web.UI;

    public class MyTreeNodeCollection : ICollection, IEnumerable, IStateManager
    {
        private bool dirty;
        private ArrayList items;
        private bool marked;
        private MyTreeNode parent;
        private MyUlTreeView tree;

        public MyTreeNodeCollection()
        {
            this.items = new ArrayList();
        }

        public MyTreeNodeCollection(MyTreeNode owner)
        {
            this.items = new ArrayList();
            this.parent = owner;
            this.tree = owner.Tree;
        }

        public MyTreeNodeCollection(MyUlTreeView tree)
        {
            this.items = new ArrayList();
            this.tree = tree;
        }

        public void Add(MyTreeNode child)
        {
            this.Add(child, true);
        }

        internal void Add(MyTreeNode child, bool updateParent)
        {
            int num = this.items.Add(child);
            if ((this.parent == null) || updateParent)
            {
                child.SetParent(this.parent);
                child.Tree = this.tree;
                if (this.marked)
                {
                    ((IStateManager) child).TrackViewState();
                    this.SetDirty();
                }
            }
        }

        public void AddAt(int index, MyTreeNode child)
        {
            this.items.Insert(index, child);
            child.SetParent(this.parent);
            child.Tree = this.tree;
            for (int i = index + 1; i < this.items.Count; i++)
            {
                if (this.marked)
                {
                    ((IStateManager) child).TrackViewState();
                    this.SetDirty();
                }
            }
        }

        public void Clear()
        {
            if ((this.tree != null) || (this.parent != null))
            {
                foreach (MyTreeNode node in this.items)
                {
                    node.Tree = null;
                    node.SetParent(null);
                }
            }
            this.items.Clear();
            if (this.marked)
            {
                this.dirty = true;
            }
        }

        public bool Contains(MyTreeNode child)
        {
            return this.items.Contains(child);
        }

        public void CopyTo(MyTreeNode[] nodeArray, int index)
        {
            this.items.CopyTo(nodeArray, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public int IndexOf(MyTreeNode node)
        {
            return this.items.IndexOf(node);
        }

        public void Remove(MyTreeNode node)
        {
            int index = this.IndexOf(node);
            if (index != -1)
            {
                this.items.RemoveAt(index);
                if (this.tree != null)
                {
                    node.Tree = null;
                }
                if (this.marked)
                {
                    this.SetDirty();
                }
            }
        }

        public void RemoveAt(int index)
        {
            MyTreeNode node = (MyTreeNode) this.items[index];
            this.items.RemoveAt(index);
            if (this.tree != null)
            {
                node.Tree = null;
            }
            if (this.marked)
            {
                this.SetDirty();
            }
        }

        internal void SetDirty()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this.dirty = true;
            }
        }

        internal void SetTree(MyUlTreeView tree)
        {
            this.tree = tree;
            foreach (MyTreeNode node in this.items)
            {
                node.Tree = tree;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.items.CopyTo(array, index);
        }

        void IStateManager.LoadViewState(object state)
        {
            if (state != null)
            {
                int num;
                Pair pair;
                object[] objArray = (object[]) state;
                this.dirty = (bool) objArray[0];
                if (this.dirty)
                {
                    this.items.Clear();
                    for (num = 1; num < objArray.Length; num++)
                    {
                        MyTreeNode node;
                        pair = objArray[num] as Pair;
                        if (pair == null)
                        {
                            throw new InvalidOperationException("Broken view state (item " + num + ")");
                        }
                        Type first = pair.First as Type;
                        if (first == null)
                        {
                            node = new MyTreeNode();
                        }
                        else
                        {
                            node = Activator.CreateInstance(pair.First as Type) as MyTreeNode;
                        }
                        this.Add(node);
                        object second = pair.Second;
                        if (second != null)
                        {
                            ((IStateManager) node).LoadViewState(second);
                        }
                    }
                }
                else
                {
                    for (num = 1; num < objArray.Length; num++)
                    {
                        pair = objArray[num] as Pair;
                        if (pair == null)
                        {
                            throw new InvalidOperationException("Broken view state " + num + ")");
                        }
                        int num2 = (int) pair.First;
                        MyTreeNode node2 = (MyTreeNode) this.items[num2];
                        ((IStateManager) node2).LoadViewState(pair.Second);
                    }
                }
            }
        }

        object IStateManager.SaveViewState()
        {
            object[] objArray = null;
            int num;
            MyTreeNode node;
            object obj2;
            bool flag = false;
            if (this.dirty)
            {
                if (this.items.Count > 0)
                {
                    flag = true;
                    objArray = new object[this.items.Count + 1];
                    objArray[0] = true;
                    for (num = 0; num < this.items.Count; num++)
                    {
                        node = this.items[num] as MyTreeNode;
                        obj2 = ((IStateManager) node).SaveViewState();
                        Type type = node.GetType();
                        objArray[num + 1] = new Pair((type == typeof(MyTreeNode)) ? null : type, obj2);
                    }
                }
            }
            else
            {
                ArrayList list = new ArrayList();
                for (num = 0; num < this.items.Count; num++)
                {
                    node = this.items[num] as MyTreeNode;
                    obj2 = ((IStateManager) node).SaveViewState();
                    if (obj2 != null)
                    {
                        flag = true;
                        list.Add(new Pair(num, obj2));
                    }
                }
                if (flag)
                {
                    list.Insert(0, false);
                    objArray = list.ToArray();
                }
            }
            if (flag)
            {
                return objArray;
            }
            return null;
        }

        void IStateManager.TrackViewState()
        {
            this.marked = true;
            for (int i = 0; i < this.items.Count; i++)
            {
                ((IStateManager) this.items[i]).TrackViewState();
            }
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public MyTreeNode this[int i]
        {
            get
            {
                return (MyTreeNode) this.items[i];
            }
        }

        public object SyncRoot
        {
            get
            {
                return this.items;
            }
        }

        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return this.marked;
            }
        }
    }
}

