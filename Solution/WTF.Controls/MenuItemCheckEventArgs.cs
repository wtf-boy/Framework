namespace WTF.Controls
{
    using System;
    using System.Web.UI.WebControls;

    public class MenuItemCheckEventArgs : EventArgs
    {
        private string commandName;
        private GridViewRow item;

        public MenuItemCheckEventArgs(string commandName, GridViewRow item)
        {
            this.item = item;
            this.commandName = commandName;
        }

        public string CommandName
        {
            get
            {
                return this.commandName;
            }
        }

        public object DataItem
        {
            get
            {
                return this.Item.DataItem;
            }
        }

        public GridViewRow Item
        {
            get
            {
                return this.item;
            }
        }
    }
}

