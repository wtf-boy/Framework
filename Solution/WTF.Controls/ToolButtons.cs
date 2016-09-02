namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.UI;

    [Serializable]
    public class ToolButtons : CollectionBase, IStateManager
    {
        private List<OperateModuleInfo> _OperateModuleInfoList = null;
        private bool marked;

        public void Add(ToolButton aItem)
        {
            base.List.Add(aItem);
        }

        public List<OperateModuleInfo> GetButtonsModule()
        {
            if (this._OperateModuleInfoList != null)
            {
                return this._OperateModuleInfoList;
            }
            if (base.Count == 0)
            {
                return null;
            }
            List<OperateModuleInfo> list = new List<OperateModuleInfo>();
            foreach (ToolButton button in this)
            {
                OperateModuleInfo item = new OperateModuleInfo {
                    ClickScriptFun = button.OnClickClick,
                    CommandArgument = "",
                    CommandName = button.CommandName,
                    ImageUrl = button.ImageUrl,
                    MenuCal = button.MenuCal,
                    MenuField = button.MenuField,
                    MenuValue = button.MenuValue,
                    ModuleName = button.Name,
                    ModuleID = Guid.NewGuid().ToString(),
                    PlaceType = "101",
                    SortIndex = button.SortIndex,
                    ToolTip = button.ToolTip.IsNoNull() ? button.ToolTip : button.Name,
                    ValGroupName = button.ValGroupName,
                    ImageCss = button.ImageCss
                };
                list.Add(item);
            }
            return list;
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

        public ToolButton this[int index]
        {
            get
            {
                return (ToolButton) base.List[index];
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

