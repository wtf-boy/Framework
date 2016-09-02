namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyPager : WebControl, INamingContainer
    {
        private string _className = "gridPager";

        public event PagerChangeEventHandler PagerChangeCommand;

        protected override void CreateChildControls()
        {
            Literal literal;
            this.Controls.Clear();
            this.Controls.Add(new LiteralControl("<div class=\"" + this.ClassName + "\" ><b class='PagerLeft' >"));
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
            this.Controls.Add(new LiteralControl("<b class='PagerRight' >"));
            LinkButton child = new LinkButton {
                Text = "首页",
                ToolTip = "首页",
                CommandName = "1"
            };
            if (this.CurrentPageIndex != 0)
            {
                child.Click += new EventHandler(this.LinkButtonClick);
            }
            else
            {
                child.OnClientClick = "return false;";
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
                    child = new LinkButton {
                        Text = num.ToString(),
                        ToolTip = "第" + num.ToString() + "页",
                        CommandName = num.ToString()
                    };
                    if (num == (this.CurrentPageIndex + 1))
                    {
                        child.CssClass = "pager-current";
                        child.OnClientClick = "return false;";
                    }
                    else
                    {
                        child.Click += new EventHandler(this.LinkButtonClick);
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
                    child = new LinkButton {
                        Text = num.ToString(),
                        ToolTip = "第" + num.ToString() + "页",
                        CommandName = num.ToString()
                    };
                    if (num == (this.CurrentPageIndex + 1))
                    {
                        child.CssClass = "pager-current";
                        child.OnClientClick = "return false;";
                    }
                    else
                    {
                        child.Click += new EventHandler(this.LinkButtonClick);
                    }
                    this.Controls.Add(child);
                }
            }
            child = new LinkButton {
                Text = "末页",
                ToolTip = "末页",
                CommandName = this.PageCount.ToString()
            };
            if (this.CurrentPageIndex != (this.PageCount - 1))
            {
                child.Click += new EventHandler(this.LinkButtonClick);
            }
            else
            {
                child.OnClientClick = "return false;";
            }
            this.Controls.Add(child);
            this.Controls.Add(new LiteralControl("</b>"));
            this.Controls.Add(new LiteralControl("</div>"));
        }

        protected void LinkButtonClick(object sender, EventArgs e)
        {
            if (this.PageCount > 0)
            {
                this.CurrentPageIndex = ((LinkButton) sender).CommandName.ConvertInt() - 1;
            }
            else
            {
                this.CurrentPageIndex = 0;
            }
            this.CreateChildControls();
            if (this.PagerChangeCommand != null)
            {
                this.PagerChangeCommand(this, new PagerEventArgs(this.CurrentPageIndex, PagerChangeType.PageIndex));
            }
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

        [Browsable(true), Category("MyPager：分页属性"), Description("当前页")]
        public int CurrentPageIndex
        {
            get
            {
                if (this.ViewState["CurrentPageIndex"] == null)
                {
                    return 0;
                }
                return (int) this.ViewState["CurrentPageIndex"];
            }
            set
            {
                this.ViewState["CurrentPageIndex"] = value;
            }
        }

        [Category("MyPager：分页属性"), Browsable(true), Description("页数")]
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

        [Browsable(true), Description("页大小"), Category("MyPager：分页属性")]
        public int PageSize
        {
            get
            {
                if (this.ViewState["PageSize"] == null)
                {
                    return 1;
                }
                return (int) this.ViewState["PageSize"];
            }
            set
            {
                this.ViewState["PageSize"] = value;
            }
        }

        [DefaultValue(0), Browsable(true), Description("记录数"), Category("MyPager：分页属性")]
        public int RecordCount
        {
            get
            {
                if (this.ViewState["RecordCount"] == null)
                {
                    return 0;
                }
                return (int) this.ViewState["RecordCount"];
            }
            set
            {
                this.ViewState["RecordCount"] = value;
            }
        }
    }
}

