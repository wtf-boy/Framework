namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyUeditor : TextBox
    {
        private string _ResourceID = Guid.NewGuid().ToString();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.TextMode = TextBoxMode.MultiLine;
            if (this.IsAutoLoadJs && !this.Page.ClientScript.IsClientScriptIncludeRegistered(typeof(MyUeditor), "MyUeditor.js"))
            {
                string script = " window.UEDITOR_HOME_URL ='" + SysVariable.ApplicationPath + "/App_Control/Ueditor/';";
                this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "UEDITOR_HOME_URL", script, true);
                string url = SysVariable.ApplicationPath + "/App_Control/Ueditor/" + this.Ueditor + ControlVerHelper.GetVer("?");
                string str3 = SysVariable.ApplicationPath + "/App_Control/Ueditor/" + this.ConfigName + ControlVerHelper.GetVer("?");
                this.Page.ClientScript.RegisterClientScriptInclude("UeditorConfigPath", str3);
                this.Page.ClientScript.RegisterClientScriptInclude(typeof(MyUeditor), "MyUeditor.js", url);
            }
            if (this.Page.Request.Form[this.ClientID + "_ResourceID"] != null)
            {
                this.ResourceID = this.Page.Request.Form[this.ClientID + "_ResourceID"];
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            base.Attributes.Add("IsUeditor", "true");
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
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            StringBuilder builder = new StringBuilder();
            if (this.IsLoadConfig)
            {
                string str = "\"?ResourceTypeID={0}&ResourceID={1}&LogModuleType={2}&RestrictCode={3}&ResourceCode={4}\";";
                builder.AppendFormat("window.UEDITOR_CONFIG.imageUrl=window.UEDITOR_CONFIG.imageUrl+" + str, new object[] { this.ResourceTypeID.ToString(), this.ResourceID.ToString(), this.LogModuleType, this.RestrictImgCode, this.ResourceCode });
                builder.AppendFormat("window.UEDITOR_CONFIG.scrawlUrl=window.UEDITOR_CONFIG.scrawlUrl+" + str, new object[] { this.ResourceTypeID.ToString(), this.ResourceID.ToString(), this.LogModuleType, this.RestrictScrawImgCode.IsNull() ? this.RestrictImgCode : this.RestrictScrawImgCode, this.ResourceCode });
                builder.AppendFormat("window.UEDITOR_CONFIG.fileUrl=window.UEDITOR_CONFIG.fileUrl+" + str, new object[] { this.ResourceTypeID.ToString(), this.ResourceID.ToString(), this.LogModuleType, this.RestrictFileCode, this.ResourceCode });
                builder.AppendFormat("window.UEDITOR_CONFIG.catcherUrl=window.UEDITOR_CONFIG.catcherUrl+" + str, new object[] { this.ResourceTypeID.ToString(), this.ResourceID.ToString(), this.LogModuleType, this.RestrictRemoteImgCode.IsNull() ? this.RestrictImgCode : this.RestrictRemoteImgCode, this.ResourceCode });
                builder.AppendFormat("window.UEDITOR_CONFIG.imageManagerUrl=window.UEDITOR_CONFIG.imageManagerUrl+" + str, new object[] { this.ResourceTypeID.ToString(), this.ResourceID.ToString(), this.LogModuleType, this.RestrictImgCode, this.ResourceCode });
                builder.AppendFormat("window.UEDITOR_CONFIG.snapscreenServerUrl=window.UEDITOR_CONFIG.snapscreenServerUrl+" + str, new object[] { this.ResourceTypeID.ToString(), this.ResourceID.ToString(), this.LogModuleType, this.RestrictScreenImgCode.IsNull() ? this.RestrictImgCode : this.RestrictScreenImgCode, this.ResourceCode });
                builder.AppendFormat("window.UEDITOR_CONFIG.wordImageUrl=window.UEDITOR_CONFIG.wordImageUrl+" + str, new object[] { this.ResourceTypeID.ToString(), this.ResourceID.ToString(), this.LogModuleType, this.RestrictWordImgCode.IsNull() ? this.RestrictImgCode : this.RestrictWordImgCode, this.ResourceCode });
                builder.AppendFormat("window.UEDITOR_CONFIG.getMovieUrl=window.UEDITOR_CONFIG.getMovieUrl+" + str, new object[] { this.ResourceTypeID.ToString(), this.ResourceID.ToString(), this.LogModuleType, this.RestrictMediaCode, this.ResourceCode });
            }
            if (this.Width != Unit.Empty)
            {
                builder.AppendFormat("window.UEDITOR_CONFIG.initialFrameWidth={0};", this.Width.Value);
            }
            if (this.Height != Unit.Empty)
            {
                builder.AppendFormat("window.UEDITOR_CONFIG.initialFrameHeight={0};", this.Height.Value);
            }
            builder.AppendLine(" var " + this.UeditorName + " = new UE.ui.Editor(); " + this.UeditorName + ".render('" + this.ClientID + "');");
            writer.Write(builder.ToString());
            writer.RenderEndTag();
        }

        public string TextCut(int length)
        {
            return this.Text.Trim().CutText(length, CutTextTailTye.RemoveTail);
        }

        public string TextCutWord(int length)
        {
            return this.Text.Trim().CutWord(length, CutTextTailTye.RemoveTail);
        }

        [Browsable(true), Category("Seven：验证功能"), Description("失去焦点验证成功执行事件"), DefaultValue("")]
        public string BlurSucessCall
        {
            get
            {
                return this.ViewState.GetString("MessageWidth", "");
            }
            set
            {
                this.ViewState["BlurSucessCall"] = value;
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

        [Browsable(true), Description("配置名称"), Category("Seven:配置名称")]
        public string ConfigName
        {
            get
            {
                return this.ViewState.GetString("ConfigName", "ueditor.config.js");
            }
            set
            {
                this.ViewState["ConfigName"] = value;
            }
        }

        [Category("Seven：验证功能"), DefaultValue(""), Browsable(true), Description("错误信息")]
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

        [Description("提示信息"), Browsable(true), Category("Seven：验证功能"), DefaultValue("")]
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

        [Browsable(true), Description("是否自动引入脚本文件"), Category("Seven：Ueditor")]
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

        [Browsable(true), Description("菜单栏"), Category("Seven：IsClearSpm")]
        public bool IsClearSpm
        {
            get
            {
                return this.ViewState.GetBool("IsClearSpm", true);
            }
            set
            {
                this.ViewState["IsClearSpm"] = value;
            }
        }

        [Browsable(true), Description("是否加载配置信息"), Category("Seven：Ueditor")]
        public bool IsLoadConfig
        {
            get
            {
                return this.ViewState.GetBool("IsLoadConfig", true);
            }
            set
            {
                this.ViewState["IsLoadConfig"] = value;
            }
        }

        [Category("Seven:日志属性"), Description("日志模块"), Browsable(true)]
        public string LogModuleType
        {
            get
            {
                return this.ViewState.GetString("LogModuleType", "ResourceLog");
            }
            set
            {
                this.ViewState["LogModuleType"] = value;
            }
        }

        [Category("Seven：验证功能"), Browsable(true), Description("最大字符长度"), DefaultValue(0)]
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

        [Description("消息宽度"), DefaultValue(0), Browsable(true), Category("Seven：验证功能")]
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

        [Category("Seven：验证功能"), DefaultValue(0), Browsable(true), Description("最小字符长度")]
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

        [DefaultValue(0), Category("Seven：验证功能"), Browsable(true), Description("最小个字长度")]
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

        [Category("Seven：Ueditor"), Description("资源文件码"), Browsable(true)]
        public string ResourceCode
        {
            get
            {
                return this.ViewState.GetString("ResourceCode", "");
            }
            set
            {
                this.ViewState["ResourceCode"] = value;
            }
        }

        [Category("Seven：Ueditor"), Description("资源标识"), Browsable(true)]
        public string ResourceID
        {
            get
            {
                if (this.ViewState["ResourceID"] != null)
                {
                    return (string) this.ViewState["ResourceID"];
                }
                return this._ResourceID;
            }
            set
            {
                this.ViewState["ResourceID"] = value;
                this._ResourceID = value;
            }
        }

        [Browsable(true), Category("Seven：Ueditor"), Description("资源类型")]
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

        [Description("资源文件限制码"), Browsable(true), Category("Seven：Ueditor")]
        public string RestrictFileCode
        {
            get
            {
                return this.ViewState.GetString("RestrictFileCode", "");
            }
            set
            {
                this.ViewState["RestrictFileCode"] = value;
            }
        }

        [Description("资源Flash限制码"), Browsable(true), Category("Seven：Ueditor")]
        public string RestrictFlashCode
        {
            get
            {
                return this.ViewState.GetString("RestrictFlashCode", "");
            }
            set
            {
                this.ViewState["RestrictFlashCode"] = value;
            }
        }

        [Browsable(true), Category("Seven：Ueditor"), Description("资源图片限制码")]
        public string RestrictImgCode
        {
            get
            {
                return this.ViewState.GetString("RestrictImgCode", "");
            }
            set
            {
                this.ViewState["RestrictImgCode"] = value;
            }
        }

        [Description("资源多媒体限制码"), Browsable(true), Category("Seven：Ueditor")]
        public string RestrictMediaCode
        {
            get
            {
                return this.ViewState.GetString("RestrictMediaCode", "");
            }
            set
            {
                this.ViewState["RestrictMediaCode"] = value;
            }
        }

        [Description("资源远程图片限制码"), Category("Seven：Ueditor"), Browsable(true)]
        public string RestrictRemoteImgCode
        {
            get
            {
                return this.ViewState.GetString("RestrictRemoteImgCode", "");
            }
            set
            {
                this.ViewState["RestrictRemoteImgCode"] = value;
            }
        }

        [Browsable(true), Description("资源涂鸦图片限制码"), Category("Seven：Ueditor")]
        public string RestrictScrawImgCode
        {
            get
            {
                return this.ViewState.GetString("RestrictScrawImgCode", "");
            }
            set
            {
                this.ViewState["RestrictScrawImgCode"] = value;
            }
        }

        [Browsable(true), Description("资源截图图片限制码"), Category("Seven：Ueditor")]
        public string RestrictScreenImgCode
        {
            get
            {
                return this.ViewState.GetString("RestrictScreenImgCode", "");
            }
            set
            {
                this.ViewState["RestrictScreenImgCode"] = value;
            }
        }

        [Browsable(true), Description("资源Word图片限制码"), Category("Seven：Ueditor")]
        public string RestrictWordImgCode
        {
            get
            {
                return this.ViewState.GetString("RestrictWordImgCode", "");
            }
            set
            {
                this.ViewState["RestrictWordImgCode"] = value;
            }
        }

        [Browsable(true), Category("Seven：Ueditor"), Description("资源多媒体限制码")]
        public string Settings
        {
            get
            {
                return this.ViewState.GetString("Settings", "");
            }
            set
            {
                this.ViewState["Settings"] = value;
            }
        }

        public override string Text
        {
            get
            {
                string content = base.Text.Replace("_ueditor_page_break_tag_", "<hr class=\"pagebreak\" />").Replace("&amp;", "&");
                if (this.IsClearSpm)
                {
                    return content.ClearContentSpm();
                }
                return content;
            }
            set
            {
                base.Text = value.Replace("<hr class=\"pagebreak\" />", "_ueditor_page_break_tag_");
            }
        }

        public string TextTrim
        {
            get
            {
                return this.Text.Trim();
            }
        }

        [Browsable(true), Description("菜单栏"), Category("Seven：Ueditor")]
        public string Tools
        {
            get
            {
                return this.ViewState.GetString("Tools", "Tools");
            }
            set
            {
                this.ViewState["Tools"] = value;
            }
        }

        [Description("Ueditor源"), Category("Seven:Ueditor源"), Browsable(true)]
        public string Ueditor
        {
            get
            {
                return this.ViewState.GetString("Ueditor", "ueditor.all.min.js");
            }
            set
            {
                this.ViewState["Ueditor"] = value;
            }
        }

        [Description("菜单栏"), Browsable(true), Category("Seven：UeditorName")]
        public string UeditorName
        {
            get
            {
                return this.ViewState.GetString("UeditorName", this.ClientID.Replace("_", ""));
            }
            set
            {
                this.ViewState["UeditorName"] = value;
            }
        }

        [Editor(typeof(MyRegexTypeEditor), typeof(UITypeEditor)), Themeable(false), Category("Seven：验证功能"), DefaultValue("")]
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

        [Browsable(true), Description("验证组"), DefaultValue(""), Category("Seven：验证功能")]
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

