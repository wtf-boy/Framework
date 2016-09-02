namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class MyDropDownList : DropDownList
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!string.IsNullOrEmpty(this.ValidationGroup))
            {
                base.Attributes.Add("ValidationGroup", this.ValidationGroup);
            }
            else
            {
                base.Attributes.Remove("ValidationGroup");
            }
            if (!string.IsNullOrEmpty(this.HintMessage))
            {
                base.Attributes.Add("HintMessage", this.HintMessage);
                base.Attributes.Add("onfocus", "$(this).FocusValidationHint();");
            }
            else
            {
                base.Attributes.Remove("HintMessage");
            }
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                base.Attributes.Add("ErrorMessage", this.ErrorMessage);
                base.Attributes.Add("onblur", "$(this).BlurValidationError('');");
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
            if (this.CheckValueEmpty)
            {
                base.Attributes.Add("CheckValueEmpty", this.CheckValueEmpty.ToString());
            }
            else
            {
                base.Attributes.Remove("CheckValueEmpty");
            }
        }

        public void SetSelectValue(object objValue)
        {
            foreach (ListItem item in this.Items)
            {
                if (item.Value == objValue.ToString())
                {
                    item.Selected = true;
                }
            }
        }

        [DefaultValue(false), Description("正则表达式"), Browsable(true), Category("Seven：验证功能")]
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

        [Browsable(true), DefaultValue(""), Category("Seven：验证功能"), Description("错误信息")]
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

        [DefaultValue(""), Category("Seven：验证功能"), Browsable(true), Description("提示信息")]
        public string HintMessage
        {
            get
            {
                return this.ViewState.GetString("HintMessage", "");
            }
            set
            {
                this.ViewState["HintMessage"] = value;
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

        [Browsable(true), Category("Seven：验证功能"), Description("消息宽度"), DefaultValue(0)]
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

        public Guid SelectValueGuid
        {
            get
            {
                return this.SelectedValue.ConvertGuid();
            }
            set
            {
                this.SelectedValue = value.ToString();
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

        [Browsable(true), DefaultValue(""), Description("验证组"), Category("Seven：验证功能")]
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

