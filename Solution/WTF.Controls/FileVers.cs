namespace WTF.Controls
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Web.UI;

    public class FileVers : CollectionBase, IStateManager
    {
        private bool marked;

        public void Add(FileVerInfo aItem)
        {
            base.List.Add(aItem);
        }

        private void Initialize()
        {
            this.marked = false;
        }

        public void Remove(int index)
        {
            if ((index < (base.Count - 1)) && (index > 0))
            {
                base.List.RemoveAt(index);
            }
        }

        void IStateManager.LoadViewState(object state)
        {
            if (state != null)
            {
                object[] objArray = (object[]) state;
                for (int i = 0; i < objArray.Length; i++)
                {
                    ((IStateManager) base.List[i]).LoadViewState(objArray[i]);
                }
            }
        }

        object IStateManager.SaveViewState()
        {
            object[] objArray = new object[base.List.Count];
            for (int i = 0; i < base.List.Count; i++)
            {
                objArray[i] = ((IStateManager) base.List[i]).SaveViewState();
            }
            return objArray;
        }

        void IStateManager.TrackViewState()
        {
            for (int i = 0; i < base.List.Count; i++)
            {
                ((IStateManager) base.List[i]).TrackViewState();
            }
        }

        public FileVerInfo this[int index]
        {
            get
            {
                return (FileVerInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
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

