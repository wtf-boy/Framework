namespace WTF.Controls
{
    using System;
    using System.ComponentModel;

    [Serializable]
    public class ToolButton
    {
        private string _CommandName = "";
        private string _ImageCss = "";
        private string _ImageUrl = "";
        private string _MenuCal = "";
        private string _MenuField = "";
        private string _MenuValue = "";
        private string _Name = "";
        private string _OnClickClick = "";
        private int _SortIndex = 0;
        private string _ToolTip = "";
        private string _ValGroupName = "";

        [Category("Seven：按钮属性"), DefaultValue(""), Description("按钮命令名"), Browsable(true)]
        public string CommandName
        {
            get
            {
                return this._CommandName;
            }
            set
            {
                this._CommandName = value;
            }
        }

        [Browsable(true), DefaultValue(""), Category("Seven：按钮属性"), Description("按钮图标样式")]
        public string ImageCss
        {
            get
            {
                return this._ImageCss;
            }
            set
            {
                this._ImageCss = value;
            }
        }

        [Browsable(true), Category("Seven：按钮属性"), Description("按钮图标路经"), DefaultValue("")]
        public string ImageUrl
        {
            get
            {
                return this._ImageUrl;
            }
            set
            {
                this._ImageUrl = value;
            }
        }

        [Browsable(true), Description("菜单计算in,not多个用|隔开"), DefaultValue(""), Category("Seven：按钮属性")]
        public string MenuCal
        {
            get
            {
                return this._MenuCal;
            }
            set
            {
                this._MenuCal = value;
            }
        }

        [Category("Seven：按钮属性"), DefaultValue(""), Description("菜单字段多个用|隔开"), Browsable(true)]
        public string MenuField
        {
            get
            {
                return this._MenuField;
            }
            set
            {
                this._MenuField = value;
            }
        }

        [Browsable(true), Description("菜单值多个用|隔开"), DefaultValue(""), Category("Seven：按钮属性")]
        public string MenuValue
        {
            get
            {
                return this._MenuField;
            }
            set
            {
                this._MenuValue = value;
            }
        }

        [DefaultValue(""), Description("按钮名称"), Browsable(true), Category("Seven：按钮属性")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        [Browsable(true), Category("Seven：按钮属性"), DefaultValue(""), Description("按钮点击事件")]
        public string OnClickClick
        {
            get
            {
                return this._OnClickClick;
            }
            set
            {
                this._OnClickClick = value;
            }
        }

        [Browsable(true), DefaultValue(0), Category("Seven：按钮属性"), Description("排序")]
        public int SortIndex
        {
            get
            {
                return this._SortIndex;
            }
            set
            {
                this._SortIndex = value;
            }
        }

        [Category("Seven：按钮属性"), Browsable(true), DefaultValue(""), Description("按钮提示")]
        public string ToolTip
        {
            get
            {
                return this._ToolTip;
            }
            set
            {
                this._ToolTip = value;
            }
        }

        [DefaultValue(""), Category("Seven：按钮属性"), Description("验证组名"), Browsable(true)]
        public string ValGroupName
        {
            get
            {
                return this._ValGroupName;
            }
            set
            {
                this._ValGroupName = value;
            }
        }
    }
}

