namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class MyRadioButtonList : RadioButtonList
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!(string.IsNullOrEmpty(this.ValidationGroup) && !this.CheckValueEmpty))
            {
                base.Attributes.Add("IsCheck", "True");
            }
            else
            {
                base.Attributes.Remove("IsCheck");
            }
            if (!string.IsNullOrEmpty(this.ValidationGroup))
            {
                base.Attributes.Add("ValidationGroup", this.ValidationGroup);
            }
            else
            {
                base.Attributes.Remove("ValidationGroup");
            }
            if (this.CheckValueEmpty)
            {
                base.Attributes.Add("CheckValueEmpty", this.CheckValueEmpty.ToString());
            }
            else
            {
                base.Attributes.Remove("CheckValueEmpty");
            }
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                base.Attributes.Add("ErrorMessage", this.ErrorMessage);
            }
            else
            {
                base.Attributes.Remove("ErrorMessage");
            }
            if (this.MessageWidth != 0)
            {
                base.Attributes.Add("MessageWidth", this.MessageWidth.ToString());
            }
            else
            {
                base.Attributes.Remove("MessageWidth");
            }
        }

        [Category("Seven：验证功能"), Description("正则表达式"), DefaultValue(false), Browsable(true)]
        public bool CheckValueEmpty
        {
            get
            {
                return this.ViewState.GetBool("CheckValueEmpty", false);
            }
            set
            {
                this.ViewState["CheckValueEmpty"] = value;
            }
        }

        [DefaultValue(""), Description("错误信息"), Category("Seven：验证功能"), Browsable(true)]
        public string ErrorMessage
        {
            get
            {
                return this.ViewState.GetString("ErrorMessage", "");
            }
            set
            {
                this.ViewState["ErrorMessage"] = value;
            }
        }

        public bool IsAutoSetError
        {
            get
            {
                return this.ViewState.GetBool("IsAutoSetError", true);
            }
            set
            {
                this.ViewState["IsAutoSetError"] = value;
            }
        }

        [DefaultValue(0), Description("消息宽度"), Browsable(true), Category("Seven：验证功能")]
        public int MessageWidth
        {
            get
            {
                return this.ViewState.GetInt("MessageWidth", 0);
            }
            set
            {
                this.ViewState["MessageWidth"] = value;
            }
        }

        public override string SelectedValue
        {
            get
            {
                return base.SelectedValue;
            }
            set
            {
                if (this.IsAutoSetError)
                {
                    bool flag = false;
                    foreach (ListItem item in this.Items)
                    {
                        if (item.Value == value)
                        {
                            flag = true;
                            base.SelectedValue = value;
                        }
                    }
                    if (!flag)
                    {
                        ListItem item2 = new ListItem("原选项已经不存在", value);
                        item2.Attributes.Add("style", "color:Red");
                        item2.Selected = true;
                        this.Items.Add(item2);
                    }
                }
                else
                {
                    base.SelectedValue = value;
                }
            }
        }

        public int SelectValueInt
        {
            get
            {
                return this.SelectedValue.ConvertInt();
            }
            set
            {
                this.SelectedValue = value.ToString();
            }
        }

        [Description("验证组"), Browsable(true), DefaultValue(""), Category("Seven：验证功能")]
        public override string ValidationGroup
        {
            get
            {
                return this.ViewState.GetString("ValidationGroup", "");
            }
            set
            {
                this.ViewState["ValidationGroup"] = value;
            }
        }
    }
}

