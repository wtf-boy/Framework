namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Web.UI.WebControls;

    public class MyCheckBoxList : CheckBoxList
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

        public string SelectValue(bool isTrimSplit = true, char splitChar = ',')
        {
            string str = "";
            foreach (ListItem item in this.Items)
            {
                if (item.Selected)
                {
                    str = str + item.Value + splitChar;
                }
            }
            if (isTrimSplit)
            {
                return str.Trim(new char[] { splitChar });
            }
            return (string.IsNullOrWhiteSpace(str) ? "" : (splitChar.ToString() + str));
        }

        public void SetSelectValue<T>(IEnumerable<T> objValueList)
        {
            foreach (ListItem item in this.Items)
            {
                item.Selected = false;
            }
            foreach (T local in objValueList)
            {
                foreach (ListItem item in this.Items)
                {
                    if (item.Value == local.ToString())
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public void SetSelectValue(string objValue, char splitChar = ',')
        {
            foreach (ListItem item in this.Items)
            {
                item.Selected = false;
            }
            foreach (string str in objValue.Split(new char[] { splitChar }, StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (ListItem item in this.Items)
                {
                    if (item.Value == str)
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public string AllValueString
        {
            get
            {
                string str = "";
                foreach (ListItem item in this.Items)
                {
                    str = str + item.Value + ",";
                }
                return str.TrimEnd(new char[] { ',' });
            }
        }

        [DefaultValue(false), Description("正则表达式"), Category("Seven：验证功能"), Browsable(true)]
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

        [Description("错误信息"), DefaultValue(""), Browsable(true), Category("Seven：验证功能")]
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

        [Category("Seven：验证功能"), DefaultValue(0), Browsable(true), Description("消息宽度")]
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

        public string SelectNoValueString
        {
            get
            {
                string str = "";
                foreach (ListItem item in this.Items)
                {
                    if (!(item.Selected || !item.Value.IsNoNullOrWhiteSpace()))
                    {
                        str = str + item.Value + ",";
                    }
                }
                return str.TrimEnd(new char[] { ',' });
            }
        }

        public string SelectValueString
        {
            get
            {
                string str = "";
                foreach (ListItem item in this.Items)
                {
                    if (item.Selected)
                    {
                        str = str + item.Value + ",";
                    }
                }
                return str.TrimEnd(new char[] { ',' });
            }
        }

        [Description("验证组"), Category("Seven：验证功能"), DefaultValue(""), Browsable(true)]
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

