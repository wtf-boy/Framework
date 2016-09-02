namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class PagerTemplate : ITemplate, INamingContainer
    {
        private string _className = "gridPager";
        private MyGridView objMyGridView;

        public event PagerChangeEventHandler PagerChangeCommand;

        public PagerTemplate(MyGridView objGridView)
        {
            this.objMyGridView = objGridView;
        }

        public void InstantiateIn(Control container)
        {
            LinkButton button;
            Literal literal;
            container.Controls.Clear();
            container.Controls.Add(new LiteralControl("<div class=\"" + this.ClassName + "\" >"));
            container.Controls.Add(new LiteralControl("<div class='PagerLeft' >"));
            int num = 0;
            if (this.PageCount > 0)
            {
                string format = "<input id=\"{0}\" name=\"{0}\" style=\"{1}\" value=\"{2}\" />";
                literal = new Literal
                {
                    Text = "共" + this.CurrentGridView.RecordCount.ToString() + "条记录&nbsp;页大小："
                };
                container.Controls.Add(literal);
                DropDownList child = new DropDownList();
                child.Items.Add(new ListItem("20条", "20"));
                child.Items.Add(new ListItem("30条", "30"));
                child.Items.Add(new ListItem("50条", "50"));
                child.Items.Add(new ListItem("100条", "100"));
                child.Items.Add(new ListItem("150条", "150"));
                child.Items.Add(new ListItem("200条", "200"));
                if (this.CurrentGridView.IsSelectPageSizeMax)
                {
                    child.Items.Add(new ListItem("3百条", "300"));
                    child.Items.Add(new ListItem("5百条", "500"));
                    child.Items.Add(new ListItem("1千条", "1000"));
                    child.Items.Add(new ListItem("5千条", "5000"));
                }
                if (child.Items.FindByValue(this.PageSize.ToString()) == null)
                {
                    child.Items.Insert(0, new ListItem(this.PageSize.ToString() + "条", this.PageSize.ToString()));
                }
                child.SelectedValue = this.PageSize.ToString();
                child.AutoPostBack = true;
                child.SelectedIndexChanged += new EventHandler(this.PageSize_SelectedIndexChanged);
                container.Controls.Add(child);
                literal = new Literal
                {
                    Text = "&nbsp;第" +
                     literal.Text + string.Format(format, this.CurrentGridView.ClientID.Replace("_", "") + "PagerNumber", "width:40px;height:15px; border:1px solid #295897;", this.CurrentPageIndex + 1)
                    + literal.Text + "/" + this.PageCount.ToString() + "页&nbsp;"
                };
                container.Controls.Add(literal);
                button = new LinkButton
                {
                    Text = "跳转",
                    ToolTip = "跳转",
                    CommandName = "Go"
                };
                button.Click += new EventHandler(this.LinkButtonClick);
                container.Controls.Add(button);
            }
            else
            {
                literal = new Literal
                {
                    Text = "记录数：" + this.CurrentGridView.RecordCount.ToString() + "条 &nbsp;页次：0/0&nbsp;&nbsp;"
                };
                container.Controls.Add(literal);
            }
            container.Controls.Add(new LiteralControl("</div>"));
            container.Controls.Add(new LiteralControl("<div class='PagerRight' >"));
            button = new LinkButton
            {
                Text = "首页",
                ToolTip = "首页",
                CommandName = "1"
            };
            button.Click += new EventHandler(this.LinkButtonClick);
            container.Controls.Add(button);
            if (this.CurrentPageIndex <= 5)
            {
                for (num = 1; num <= 9; num++)
                {
                    if (num > this.PageCount)
                    {
                        break;
                    }
                    button = new LinkButton
                    {
                        Text = num.ToString(),
                        ToolTip = "第" + num.ToString() + "页",
                        CommandName = num.ToString()
                    };
                    if (num == (this.CurrentPageIndex + 1))
                    {
                        button.CssClass = "pager-current";
                        button.OnClientClick = "return false;";
                    }
                    button.Click += new EventHandler(this.LinkButtonClick);
                    container.Controls.Add(button);
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
                    button = new LinkButton
                    {
                        Text = num.ToString(),
                        ToolTip = "第" + num.ToString() + "页",
                        CommandName = num.ToString()
                    };
                    if (num == (this.CurrentPageIndex + 1))
                    {
                        button.CssClass = "pager-current";
                        button.OnClientClick = "return false;";
                    }
                    button.Click += new EventHandler(this.LinkButtonClick);
                    container.Controls.Add(button);
                }
            }
            button = new LinkButton
            {
                Text = "末页",
                ToolTip = "末页",
                CommandName = this.PageCount.ToString()
            };
            button.Click += new EventHandler(this.LinkButtonClick);
            container.Controls.Add(button);
            container.Controls.Add(new LiteralControl("</div>"));
            container.Controls.Add(new LiteralControl("</div>"));
        }

        protected void LinkButtonClick(object sender, EventArgs e)
        {
            if (this.PageCount > 0)
            {
                if (((LinkButton)sender).CommandName != "Go")
                {
                    this.CurrentPageIndex = ((LinkButton)sender).CommandName.ConvertInt() - 1;
                }
                else
                {
                    string str = this.CurrentGridView.ClientID.Replace("_", "") + "PagerNumber";
                    if (SysVariable.CurrentContext.Request[str] != null)
                    {
                        string s = SysVariable.CurrentContext.Request[str];
                        int result = 0;
                        if (int.TryParse(s, out result))
                        {
                            if (result <= 0)
                            {
                                this.CurrentPageIndex = 0;
                            }
                            else if (result > this.PageCount)
                            {
                                this.CurrentPageIndex = this.PageCount - 1;
                            }
                            else
                            {
                                this.CurrentPageIndex = result - 1;
                            }
                        }
                    }
                }
            }
            else
            {
                this.CurrentPageIndex = 0;
            }
            this.On_PagerChangeCommand(this, new PagerEventArgs(this.CurrentPageIndex, PagerChangeType.PageIndex));
        }

        private void On_PagerChangeCommand(object sender, PagerEventArgs e)
        {
            if (this.PagerChangeCommand != null)
            {
                this.PagerChangeCommand(sender, e);
            }
        }

        private void PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int changeValue = int.Parse(((DropDownList)sender).SelectedValue);
            this.On_PagerChangeCommand(this, new PagerEventArgs(changeValue, PagerChangeType.PageSize));
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

        public MyGridView CurrentGridView
        {
            get
            {
                return this.objMyGridView;
            }
            set
            {
                this.objMyGridView = value;
            }
        }

        public int CurrentPageIndex
        {
            get
            {
                return this.CurrentGridView.CurrentPageIndex;
            }
            set
            {
                this.CurrentGridView.CurrentPageIndex = value;
            }
        }

        public int PageCount
        {
            get
            {
                if ((this.CurrentGridView.RecordCount % this.CurrentGridView.PageSize) > 0)
                {
                    return ((this.CurrentGridView.RecordCount / this.CurrentGridView.PageSize) + 1);
                }
                return (this.CurrentGridView.RecordCount / this.CurrentGridView.PageSize);
            }
        }

        public int PageSize
        {
            get
            {
                return this.CurrentGridView.PageSize;
            }
            set
            {
                this.CurrentGridView.PageSize = value;
            }
        }
    }
}

