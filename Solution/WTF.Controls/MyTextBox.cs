namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.InteropServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyTextBox : TextBox
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!string.IsNullOrEmpty(this.ControlToCompare))
            {
                string str = this.ClientID.Replace(this.ID, "");
                base.Attributes.Add("ControlToCompare", str + this.ControlToCompare);
            }
            else
            {
                base.Attributes.Remove("ControlToCompare");
            }
            if (!string.IsNullOrEmpty(this.ValidationGroup))
            {
                base.Attributes.Add("ValidationGroup", this.ValidationGroup);
            }
            else
            {
                base.Attributes.Remove("ValidationGroup");
            }
            if (!string.IsNullOrEmpty(this.ValidationExpression))
            {
                base.Attributes.Add("ValidationExpression", this.ValidationExpression);
            }
            else
            {
                base.Attributes.Remove("ValidationExpression");
            }
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                base.Attributes.Add("ErrorMessage", this.ErrorMessage);
                base.Attributes.Add("onblur", "$(this).BlurValidationError(" + (this.BlurSucessCall.IsNull() ? "" : this.BlurSucessCall) + ");");
            }
            else
            {
                base.Attributes.Remove("ErrorMessage");
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
            if (this.MaxCharLength != 0)
            {
                base.Attributes.Add("MaxCharLength", this.MaxCharLength.ToString());
            }
            else
            {
                base.Attributes.Remove("MaxCharLength");
            }
            if (this.MinCharLength != 0)
            {
                base.Attributes.Add("MinCharLength", this.MinCharLength.ToString());
            }
            else
            {
                base.Attributes.Remove("MinCharLength");
            }
            if (this.MinLength != 0)
            {
                base.Attributes.Add("MinLength", this.MinLength.ToString());
            }
            else
            {
                base.Attributes.Remove("MinLength");
            }
            if (!(this.MaximumValue == float.MaxValue))
            {
                base.Attributes.Add("MaximumValue", this.MaximumValue.ToString());
            }
            else
            {
                base.Attributes.Remove("MaximumValue");
            }
            if (!(this.MinimumValue == float.MinValue))
            {
                base.Attributes.Add("MinimumValue", this.MinimumValue.ToString());
            }
            else
            {
                base.Attributes.Remove("MinimumValue");
            }
            if ((this.MaxLength != 0) && (this.TextMode == TextBoxMode.MultiLine))
            {
                base.Attributes.Add("maxlength", this.MaxLength.ToString());
            }
            else
            {
                base.Attributes.Remove("maxlength");
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

        public string TextCut(int length)
        {
            return this.Text.Trim().CutText(length, CutTextTailTye.RemoveTail);
        }

        public string TextCutWord(int length)
        {
            return this.Text.Trim().CutWord(length, CutTextTailTye.RemoveTail);
        }

        public string TextTag(bool isTrimSplit = true, char splitChar = ',')
        {
            return this.Text.Trim().ProcessFiterTag(isTrimSplit, splitChar);
        }

        public void TextTag(string textValue, bool isTrimSplit = true, char splitChar = ',')
        {
            if (isTrimSplit)
            {
                this.Text = textValue.Trim(new char[] { splitChar });
            }
            else
            {
                this.Text = textValue;
            }
        }

        [Category("Seven：验证功能"), DefaultValue(""), Browsable(true), Description("失去焦点验证成功执行事件")]
        public string BlurSucessCall
        {
            get
            {
                return this.ViewState.GetString("BlurSucessCall", "");
            }
            set
            {
                this.ViewState["BlurSucessCall"] = value;
            }
        }

        [Bindable(true), Browsable(true), DefaultValue(false), Category("Seven：验证功能"), Description("正则表达式")]
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

        [IDReferenceProperty, DefaultValue(""), TypeConverter(typeof(ValidatedControlConverter)), Category("Seven：验证功能"), Description("需要比较的控件"), Themeable(false)]
        public string ControlToCompare
        {
            get
            {
                return this.ViewState.GetString("ControlToCompare", "");
            }
            set
            {
                this.ViewState["ControlToCompare"] = value;
            }
        }

        [Browsable(true), Description("错误信息"), DefaultValue(""), Category("Seven：验证功能")]
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

        [DefaultValue(""), Browsable(true), Category("Seven：验证功能"), Description("提示信息")]
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

        [DefaultValue(0), Description("最大字符长度"), Category("Seven：验证功能"), Browsable(true)]
        public int MaxCharLength
        {
            get
            {
                return this.ViewState.GetInt("MaxCharLength", 0);
            }
            set
            {
                this.ViewState["MaxCharLength"] = value;
            }
        }

        [Category("Seven：验证功能"), Description("最大值"), DefaultValue(float.MaxValue), Browsable(true)]
        public float MaximumValue
        {
            get
            {
                return this.ViewState.GetFloat("MaximumValue", float.MaxValue);
            }
            set
            {
                this.ViewState["MaximumValue"] = value;
            }
        }

        [Browsable(true), Category("Seven：验证功能"), DefaultValue(0), Description("消息宽度")]
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

        [DefaultValue(0), Category("Seven：验证功能"), Browsable(true), Description("最小字符长度")]
        public int MinCharLength
        {
            get
            {
                return this.ViewState.GetInt("MinCharLength", 0);
            }
            set
            {
                this.ViewState["MinCharLength"] = value;
            }
        }

        [Browsable(true), Description("最小值"), DefaultValue(float.MinValue), Category("Seven：验证功能")]
        public float MinimumValue
        {
            get
            {
                return this.ViewState.GetFloat("MinimumValue", float.MinValue);
            }
            set
            {
                this.ViewState["MinimumValue"] = value;
            }
        }

        [Category("Seven：验证功能"), DefaultValue(0), Description("最小个字长度"), Browsable(true)]
        public int MinLength
        {
            get
            {
                return this.ViewState.GetInt("MinLength", 0);
            }
            set
            {
                this.ViewState["MinLength"] = value;
            }
        }

        public DateTime TextCurrentDateTime
        {
            get
            {
                return this.Text.ConvertCurrentDateTime();
            }
        }

        public DateTime TextDateTime
        {
            get
            {
                return this.Text.ConvertDateTime();
            }
        }

        public decimal TextDecimal
        {
            get
            {
                return this.Text.Trim().ConvertDecimal();
            }
        }

        public double TextDouble
        {
            get
            {
                return this.Text.Trim().ConvertDouble();
            }
        }

        public int TextInt
        {
            get
            {
                return this.Text.Trim().ConvertInt();
            }
        }

        public long TextLong
        {
            get
            {
                return this.Text.Trim().ConvertLong();
            }
        }

        public int TextSecondStamp
        {
            get
            {
                return this.TextCurrentDateTime.ConvertToSecondStamp();
            }
        }

        public long TextTimeStamp
        {
            get
            {
                return this.TextCurrentDateTime.ConvertToStamp();
            }
        }

        public string TextTrim
        {
            get
            {
                return this.Text.Trim();
            }
        }

        public string TextUrl
        {
            get
            {
                string str = this.Text.Trim();
                if (string.IsNullOrWhiteSpace(str))
                {
                    return "";
                }
                if (!str.StartsWith("http"))
                {
                    str = "http://" + str;
                }
                return str.Replace(" ", "");
            }
        }

        [Category("Seven：验证功能"), Themeable(false), Editor(typeof(MyRegexTypeEditor), typeof(UITypeEditor)), DefaultValue("")]
        public string ValidationExpression
        {
            get
            {
                return this.ViewState.GetString("ValidationExpression", "");
            }
            set
            {
                this.ViewState["ValidationExpression"] = value;
            }
        }

        [Description("验证组"), Browsable(true), Category("Seven：验证功能"), DefaultValue("")]
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

