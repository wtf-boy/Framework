namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyXheditor : TextBox
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.TextMode = TextBoxMode.MultiLine;
            if (this.IsAutoLoadJs && !this.Page.ClientScript.IsClientScriptIncludeRegistered(typeof(MyXheditor), "xheditor.js"))
            {
                string url = SysVariable.ApplicationPath + "/App_Control/Xheditor/xheditor.js";
                string str2 = SysVariable.ApplicationPath + "/App_Control/Xheditor/xheditor_Seven/xheditor-resource-zh.js";
                this.Page.ClientScript.RegisterClientScriptInclude(typeof(MyXheditor), "xheditor.js", url);
                this.Page.ClientScript.RegisterClientScriptInclude("xheditorresource", str2);
            }
            if (this.Page.Request.Form[this.ClientID + "_ResourceID"] != null)
            {
                this.ResourceID = this.Page.Request.Form[this.ClientID + "_ResourceID"];
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            base.Attributes.Add("IsRich", "true");
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
                base.Attributes.Remove("onblur");
            }
            if (!string.IsNullOrEmpty(this.HintMessage))
            {
                base.Attributes.Add("HintMessage", this.HintMessage);
                base.Attributes.Add("onfocus", "$(this).FocusValidationHint();");
            }
            else
            {
                base.Attributes.Remove("HintMessage");
                base.Attributes.Remove("onfocus");
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
            if (this.MaxLength != 0)
            {
                base.Attributes.Add("maxlength", this.MaxLength.ToString());
            }
            else
            {
                base.Attributes.Remove("maxlength");
            }
            if (this.MinLength != 0)
            {
                base.Attributes.Add("MinLength", this.MinLength.ToString());
            }
            else
            {
                base.Attributes.Remove("MinLength");
            }
            if (this.MaximumValue != 0x7fffffff)
            {
                base.Attributes.Add("MaximumValue", this.MaximumValue.ToString());
            }
            else
            {
                base.Attributes.Remove("MaximumValue");
            }
            if (this.MinimumValue != -2147483648)
            {
                base.Attributes.Add("MinimumValue", this.MinimumValue.ToString());
            }
            else
            {
                base.Attributes.Remove("MinimumValue");
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

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_ResourceID");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID + "_ResourceID");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, this.ResourceID.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            base.Render(writer);
            object obj2 = ("" + "<script type='text/javascript'>") + "function SevenXheditorInit() {" + "var editor;";
            string str = string.Concat(new object[] { obj2, "editor = $('#", this.ClientID, "').xheditor({ plugins: resourcePlugin, tools:", this.Tools, ",skin:'", this.Skin, "', width: '", this.Width.Value, "px', height: '", this.Height.Value, "px'" });
            string format = "ResourceClientID:'" + this.ClientID + "_ResourceID',ResourceTypeID:'" + this.ResourceTypeID.ToString().EncodeEnhancedBase64() + "',RestrictCode:'{0}',ResourceID:'" + this.ResourceID.ToString() + "',LogModuleType:'" + this.LogModuleType.EncodeEnhancedBase64() + "'";
            if (this.RestrictImgCode.IsNoNull())
            {
                str = str + ",RestrictImgCode:{" + string.Format(format, this.RestrictImgCode.EncodeEnhancedBase64()) + "}";
            }
            if (this.RestrictFileCode.IsNoNull())
            {
                str = str + ",RestrictFileCode:{" + string.Format(format, this.RestrictFileCode.EncodeEnhancedBase64()) + "}";
            }
            if (this.RestrictMediaCode.IsNoNull())
            {
                str = str + ",RestrictMediaCode:{" + string.Format(format, this.RestrictMediaCode.EncodeEnhancedBase64()) + "}";
            }
            if (this.RestrictFlashCode.IsNoNull())
            {
                str = str + ",RestrictFlashCode:{" + string.Format(format, this.RestrictFlashCode.EncodeEnhancedBase64()) + "}";
            }
            str = (str + (this.Settings.IsNull() ? "" : ("," + this.Settings)) + " })};") + "SevenXheditorInit();" + "</script>";
            writer.Write(str);
        }

        public string TextCut(int length)
        {
            return this.Text.Trim().CutText(length, CutTextTailTye.RemoveTail);
        }

        public string TextCutWord(int length)
        {
            return this.Text.Trim().CutWord(length, CutTextTailTye.RemoveTail);
        }

        [Description("失去焦点验证成功执行事件"), Category("Seven：验证功能"), Browsable(true), DefaultValue("")]
        public string BlurSucessCall
        {
            get
            {
                if (this.ViewState["BlurSucessCall"] != null)
                {
                    return (string) this.ViewState["BlurSucessCall"];
                }
                return "";
            }
            set
            {
                this.ViewState["BlurSucessCall"] = value;
            }
        }

        [Category("Seven：验证功能"), DefaultValue(false), Browsable(true), Description("正则表达式")]
        public bool CheckValueEmpty
        {
            get
            {
                return ((this.ViewState["CheckValueEmpty"] != null) && ((bool) this.ViewState["CheckValueEmpty"]));
            }
            set
            {
                this.ViewState["CheckValueEmpty"] = value;
            }
        }

        [Category("Seven：验证功能"), DefaultValue(""), Browsable(true), Description("错误信息")]
        public string ErrorMessage
        {
            get
            {
                return this.ViewState.GetString("ErrorMessage");
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
                return this.ViewState.GetString("HintMessage");
            }
            set
            {
                this.ViewState["HintMessage"] = value;
            }
        }

        [Description("是否自动引入脚本文件"), Category("Seven：Xheditor"), Browsable(true)]
        public bool IsAutoLoadJs
        {
            get
            {
                return this.ViewState.GetBool("IsAutoLoadJs", true);
            }
            set
            {
                this.ViewState["IsAutoLoadJs"] = value;
            }
        }

        [Description("日志模块"), Browsable(true), Category("Seven:日志属性")]
        public string LogModuleType
        {
            get
            {
                return this.ViewState.GetString("ContentLog", "full");
            }
            set
            {
                this.ViewState["LogModuleType"] = value;
            }
        }

        [DefaultValue(0), Category("Seven：验证功能"), Browsable(true), Description("最大字符长度")]
        public int MaxCharLength
        {
            get
            {
                if (this.ViewState["MaxCharLength"] != null)
                {
                    return (int) this.ViewState["MaxCharLength"];
                }
                return 0;
            }
            set
            {
                this.ViewState["MaxCharLength"] = value;
            }
        }

        [Description("最大值"), Browsable(true), Category("Seven：验证功能"), DefaultValue(0x7fffffff)]
        public int MaximumValue
        {
            get
            {
                if (this.ViewState["MaximumValue"] != null)
                {
                    return (int) this.ViewState["MaximumValue"];
                }
                return 0x7fffffff;
            }
            set
            {
                this.ViewState["MaximumValue"] = value;
            }
        }

        [DefaultValue(0), Category("Seven：验证功能"), Description("消息宽度"), Browsable(true)]
        public int MessageWidth
        {
            get
            {
                if (this.ViewState["MessageWidth"] != null)
                {
                    return (int) this.ViewState["MessageWidth"];
                }
                return 0;
            }
            set
            {
                this.ViewState["MessageWidth"] = value;
            }
        }

        [Browsable(true), Category("Seven：验证功能"), DefaultValue(0), Description("最小字符长度")]
        public int MinCharLength
        {
            get
            {
                if (this.ViewState["MinCharLength"] != null)
                {
                    return (int) this.ViewState["MinCharLength"];
                }
                return 0;
            }
            set
            {
                this.ViewState["MinCharLength"] = value;
            }
        }

        [Category("Seven：验证功能"), Browsable(true), Description("最小值"), DefaultValue(-2147483648)]
        public int MinimumValue
        {
            get
            {
                if (this.ViewState["MinimumValue"] != null)
                {
                    return (int) this.ViewState["MinimumValue"];
                }
                return -2147483648;
            }
            set
            {
                this.ViewState["MinimumValue"] = value;
            }
        }

        [Category("Seven：验证功能"), DefaultValue(0), Browsable(true), Description("最小个字长度")]
        public int MinLength
        {
            get
            {
                if (this.ViewState["MinLength"] != null)
                {
                    return (int) this.ViewState["MinLength"];
                }
                return 0;
            }
            set
            {
                this.ViewState["MinLength"] = value;
            }
        }

        [Category("Seven：Xheditor"), Browsable(true), Description("资源标识")]
        public string ResourceID
        {
            get
            {
                return this.ViewState.GetString("ResourceID");
            }
            set
            {
                this.ViewState["ResourceID"] = value;
            }
        }

        [Browsable(true), Category("Seven：Xheditor"), Description("资源类型")]
        public int ResourceTypeID
        {
            get
            {
                return this.ViewState.GetInt("ResourceTypeID", 0);
            }
            set
            {
                this.ViewState["ResourceTypeID"] = value;
            }
        }

        [Browsable(true), Category("Seven：Xheditor"), Description("资源文件限制码")]
        public string RestrictFileCode
        {
            get
            {
                return this.ViewState.GetString("RestrictFileCode");
            }
            set
            {
                this.ViewState["RestrictFileCode"] = value;
            }
        }

        [Browsable(true), Category("Seven：Xheditor"), Description("资源Flash限制码")]
        public string RestrictFlashCode
        {
            get
            {
                return this.ViewState.GetString("RestrictFlashCode");
            }
            set
            {
                this.ViewState["RestrictFlashCode"] = value;
            }
        }

        [Browsable(true), Description("资源图片限制码"), Category("Seven：Xheditor")]
        public string RestrictImgCode
        {
            get
            {
                return this.ViewState.GetString("RestrictImgCode");
            }
            set
            {
                this.ViewState["RestrictImgCode"] = value;
            }
        }

        [Browsable(true), Category("Seven：Xheditor"), Description("资源多媒体限制码")]
        public string RestrictMediaCode
        {
            get
            {
                return this.ViewState.GetString("RestrictMediaCode");
            }
            set
            {
                this.ViewState["RestrictMediaCode"] = value;
            }
        }

        [Browsable(true), Description("资源多媒体限制码"), Category("Seven：Xheditor")]
        public string Settings
        {
            get
            {
                return this.ViewState.GetString("Settings");
            }
            set
            {
                this.ViewState["Settings"] = value;
            }
        }

        [Browsable(true), Category("Seven：Xheditor"), Description("皮肤default,nostyle,o2007silver,o2007blue")]
        public string Skin
        {
            get
            {
                return this.ViewState.GetString("Skin", "vista");
            }
            set
            {
                this.ViewState["Skin"] = value;
            }
        }

        public string TextTrim
        {
            get
            {
                return this.Text.Trim();
            }
        }

        [Description("菜单栏"), Browsable(true), Category("Seven：Xheditor")]
        public string Tools
        {
            get
            {
                return this.ViewState.GetString("Tools", "full");
            }
            set
            {
                this.ViewState["Tools"] = value;
            }
        }

        [Category("Seven：验证功能"), Editor(typeof(MyRegexTypeEditor), typeof(UITypeEditor)), Themeable(false), DefaultValue("")]
        public string ValidationExpression
        {
            get
            {
                return this.ViewState.GetString("ValidationExpression");
            }
            set
            {
                this.ViewState["ValidationExpression"] = value;
            }
        }

        [Browsable(true), Description("验证组"), DefaultValue(""), Category("Seven：验证功能")]
        public override string ValidationGroup
        {
            get
            {
                return this.ViewState.GetString("ValidationGroup");
            }
            set
            {
                this.ViewState["ValidationGroup"] = value;
            }
        }
    }
}

