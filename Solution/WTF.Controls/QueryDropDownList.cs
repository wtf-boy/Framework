﻿namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Web.UI;

    public class QueryDropDownList : MyEnumDropDownList
    {
        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(this.QueryTitle))
            {
                writer.Write(this.QueryTitle + ": ");
            }
            base.Render(writer);
        }

        [Description("数据类型"), Browsable(true), DefaultValue(""), Category("Seven：查询功能")]
        public WTF.Framework.QueryDataType QueryDataType
        {
            get
            {
                if (this.ViewState["QueryDataType"] != null)
                {
                    return (WTF.Framework.QueryDataType) this.ViewState["QueryDataType"];
                }
                return WTF.Framework.QueryDataType.ObjectT;
            }
            set
            {
                this.ViewState["QueryDataType"] = value;
            }
        }

        [DefaultValue(""), Description("查询字段"), Category("Seven：查询功能"), Browsable(true)]
        public string QueryField
        {
            get
            {
                if (this.ViewState["QueryField"] != null)
                {
                    return (string) this.ViewState["QueryField"];
                }
                return "";
            }
            set
            {
                this.ViewState["QueryField"] = value;
            }
        }

        [Description("查询方式"), Browsable(true), DefaultValue(""), Category("Seven：查询功能")]
        public WTF.Framework.QueryMethod QueryMethod
        {
            get
            {
                if (this.ViewState["QueryMethod"] != null)
                {
                    return (WTF.Framework.QueryMethod) this.ViewState["QueryMethod"];
                }
                return WTF.Framework.QueryMethod.Equal;
            }
            set
            {
                this.ViewState["QueryMethod"] = value;
            }
        }

        [Description("查询QueryPrefix默认it"), Category("Seven：查询功能"), DefaultValue(""), Browsable(true)]
        public string QueryPrefix
        {
            get
            {
                if (this.ViewState["QueryPrefix"] != null)
                {
                    return (string) this.ViewState["QueryPrefix"];
                }
                return "it";
            }
            set
            {
                this.ViewState["QueryPrefix"] = value;
            }
        }

        [Category("Seven：查询标题"), DefaultValue(""), Browsable(true), Description("查询标题")]
        public string QueryTitle
        {
            get
            {
                if (this.ViewState["QueryTitle"] != null)
                {
                    return (string) this.ViewState["QueryTitle"];
                }
                return "";
            }
            set
            {
                this.ViewState["QueryTitle"] = value;
            }
        }

        [Browsable(true), Description("查询合并方式"), DefaultValue(""), Category("Seven：查询功能")]
        public WTF.Framework.QueryUnite QueryUnite
        {
            get
            {
                if (this.ViewState["QueryUnite"] != null)
                {
                    return (WTF.Framework.QueryUnite) this.ViewState["QueryUnite"];
                }
                return WTF.Framework.QueryUnite.AND;
            }
            set
            {
                this.ViewState["QueryUnite"] = value;
            }
        }

        public string SearchQueryField
        {
            get
            {
                return (this.QueryField.IsNull() ? this.ID : this.QueryField);
            }
        }
    }
}

