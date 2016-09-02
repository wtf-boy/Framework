namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyUrlPager : WebControl, INamingContainer
    {
        private string _className = "MyPager";
        private int _CurrentPageIndex = 0;
        private int _PageSize = 20;
        private int _RecordCount = 0;

        protected override void CreateChildControls()
        {
            Literal literal;
            this.Controls.Clear();
            this.Controls.Add(new LiteralControl("<div class=\"" + this.ClassName + "\" ><b class='MyPagerLeft' >"));
            int num = 0;
            if (this.PageCount > 0)
            {
                literal = new Literal();
                string[] strArray = new string[] { "记录数：", this.RecordCount.ToString(), "条 &nbsp;页次：", (this.CurrentPageIndex + 1).ToString(), "/", this.PageCount.ToString(), "&nbsp;&nbsp;</b>" };
                literal.Text = string.Concat(strArray);
                this.Controls.Add(literal);
            }
            else
            {
                literal = new Literal {
                    Text = "记录数：" + this.RecordCount.ToString() + "条 &nbsp;页次：0/0&nbsp;&nbsp;</b>"
                };
                this.Controls.Add(literal);
            }
            this.Controls.Add(new LiteralControl("<b class='MyPagerRight' >"));
            HyperLink child = new HyperLink {
                Text = "首页",
                ToolTip = "首页",
                Target = "_self"
            };
            string str = RequestHelper.RawUrl.AddUrlQueryString("PageIndex", "0");
            child.NavigateUrl = str;
            if (this.CurrentPageIndex == 0)
            {
                child.Attributes.Add("onclick", "return false;");
            }
            this.Controls.Add(child);
            if (this.CurrentPageIndex <= 5)
            {
                for (num = 1; num <= 9; num++)
                {
                    if (num > this.PageCount)
                    {
                        break;
                    }
                    child = new HyperLink {
                        Text = num.ToString(),
                        ToolTip = "第" + num.ToString() + "页"
                    };
                    str = RequestHelper.RawUrl.AddUrlQueryString("PageIndex", num - 1);
                    child.NavigateUrl = str;
                    if (num == (this.CurrentPageIndex + 1))
                    {
                        child.CssClass = "pager-current";
                        child.Attributes.Add("onclick", "return false;");
                    }
                    this.Controls.Add(child);
                }
            }
            else
            {
                for (num = (this.CurrentPageIndex + 1) - 4; num <= ((this.CurrentPageIndex + 1) + 4); num++)
                {
                    if (num > this.PageCount)
                    {
                        break;
                    }
                    child = new HyperLink {
                        Text = num.ToString(),
                        ToolTip = "第" + num.ToString() + "页"
                    };
                    str = RequestHelper.RawUrl.AddUrlQueryString("PageIndex", num - 1);
                    child.NavigateUrl = str;
                    if (num == (this.CurrentPageIndex + 1))
                    {
                        child.CssClass = "pager-current";
                        child.Attributes.Add("onclick", "return false;");
                    }
                    this.Controls.Add(child);
                }
            }
            child = new HyperLink {
                Text = "末页",
                ToolTip = "末页",
                NavigateUrl = str
            };
            if (this.CurrentPageIndex != (this.PageCount - 1))
            {
                str = RequestHelper.RawUrl.AddUrlQueryString("PageIndex", this.PageCount - 1);
            }
            else
            {
                child.Attributes.Add("onclick", "return false;");
            }
            this.Controls.Add(child);
            this.Controls.Add(new LiteralControl("</b>"));
            this.Controls.Add(new LiteralControl("</div>"));
        }

        public string ClassName
        {
            get
            {
                return this._className;
            }
            set
            {
                this._className = value;
            }
        }

        [Description("当前页"), Category("MyPager：分页属性"), Browsable(true)]
        public int CurrentPageIndex
        {
            get
            {
                return this._CurrentPageIndex;
            }
            set
            {
                this._CurrentPageIndex = value;
            }
        }

        [Description("页数"), Browsable(true), Category("MyPager：分页属性")]
        public int PageCount
        {
            get
            {
                if ((this.RecordCount % this.PageSize) > 0)
                {
                    return ((this.RecordCount / this.PageSize) + 1);
                }
                return (this.RecordCount / this.PageSize);
            }
        }

        [Category("MyPager：分页属性"), Description("页大小"), Browsable(true)]
        public int PageSize
        {
            get
            {
                return this._PageSize;
            }
            set
            {
                this._PageSize = value;
            }
        }

        [Category("MyPager：分页属性"), Description("记录数"), Browsable(true), DefaultValue(0)]
        public int RecordCount
        {
            get
            {
                return this._RecordCount;
            }
            set
            {
                this._RecordCount = value;
            }
        }
    }
}

