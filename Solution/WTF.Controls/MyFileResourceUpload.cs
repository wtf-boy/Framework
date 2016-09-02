namespace WTF.Controls
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Resource;
    using WTF.Resource.Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Objects;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyFileResourceUpload : FileUpload
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.ContentEditAble)
            {
                base.Attributes.Add("contenteditable", "false");
            }
            if (this.PreviewExp.IsNoNull())
            {
                base.Attributes.Add("onchange", "javascript:ImagePreview.Preview(this,\"" + this.PreviewExp + "\");$(this).BlurValidationError();");
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.ValidationInit();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DownLoadUrl.IsNoNull() && this.DownLoadName.IsNoNull())
            {
                string str = " <a class=\"file-down-a\" href=\"" + this.DownLoadUrl + "\" target=\"_blank\">" + this.DownLoadName + "</a>";
                writer.Write(str);
            }
            base.Render(writer);
        }

        public void Save()
        {
            if (base.HasFile)
            {
                if ((this.ImageHeight > 0) || (this.ImageWidth > 0))
                {
                    object obj2;
                    System.Drawing.Image image = base.PostedFile.InputStream.StreamToImage();
                    string str = "";
                    if ((this.ImageWidth > 0) && ((image.Width < (this.ImageWidth - this.ImageRange)) || (image.Width > (this.ImageWidth + this.ImageRange))))
                    {
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, @"\n图片宽度不正确!限制宽度:", this.ImageWidth, "px", (this.ImageRange > 0) ? ("误差:" + this.ImageRange + "px") : "", ",当前宽度:", image.Width, "px" });
                    }
                    if ((this.ImageHeight > 0) && ((image.Height < (this.ImageHeight - this.ImageRange)) || (image.Height > (this.ImageHeight + this.ImageRange))))
                    {
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, @"\n图片高度不正确!限制高度:", this.ImageHeight, "px", (this.ImageRange > 0) ? ("误差:" + this.ImageRange + "px") : "", ",当前高度:", image.Height, "px" });
                    }
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        SysAssert.InfoHintAssert(this.FieldName + str);
                    }
                }
                this.MoveResourceInfo = base.PostedFile.SaveResource(this.ResourceCode, this.RestrictCode);
                if (this.MoveResourceInfo.Count > 0)
                {
                    ResourceInfo info = this.MoveResourceInfo[0];
                    this.ResourcePath = info.ResourcePath;
                    this.Md5Value = info.Md5Value;
                }
            }
        }

        private void ValidationInit()
        {
            if ((this.ValidationGroup.IsNoNull() && this.ResourceCode.IsNoNull()) && this.RestrictCode.IsNoNull())
            {
                FileResourceRule rule = new FileResourceRule();
                resource_filerestrict _filerestrict = rule.resource_filerestrict.Where("it.RestrictCode='" + this.RestrictCode + "' and it.resource_fileresource.FileResourceCode='" + this.ResourceCode + "'", new ObjectParameter[0]).Include("resource_filestoragepath").Include("resource_filerestrictpic").FirstOrDefault<resource_filerestrict>();
                if (_filerestrict == null)
                {
                    SysAssert.InfoHintAssert("找不到此文件配置");
                }
                if (_filerestrict.FileExtension.IsNoNull())
                {
                    this.ValidationExpression = "(" + _filerestrict.FileExtension.Replace(',', '|') + ")$";
                }
                if (this.HintMessage.IsNull())
                {
                    object hintMessage;
                    if ((_filerestrict.FileMaxSize > 0) || _filerestrict.FileExtension.IsNoNull())
                    {
                        this.HintMessage = (_filerestrict.FileExtension.IsNoNull() ? ("文件类型:" + _filerestrict.FileExtension) : "") + ((_filerestrict.FileMaxSize > 0) ? ((_filerestrict.FileExtension.IsNoNull() ? "," : "") + "文件必须小于" + ((int) (_filerestrict.FileMaxSize * 0x400)).RenderFileSize()) : "");
                        this.ErrorMessage = "请上传:" + (_filerestrict.FileExtension.IsNoNull() ? ("文件类型:" + _filerestrict.FileExtension) : "") + ((_filerestrict.FileMaxSize > 0) ? ((_filerestrict.FileExtension.IsNoNull() ? "," : "") + "文件必须小于" + ((int) (_filerestrict.FileMaxSize * 0x400)).RenderFileSize()) : "");
                    }
                    resource_filerestrictpic _filerestrictpic = (from s in _filerestrict.resource_filerestrictpic
                        orderby s.ImageWidth descending
                        select s).FirstOrDefault<resource_filerestrictpic>();
                    if (_filerestrictpic != null)
                    {
                        if (_filerestrictpic.ImageWidth > 0)
                        {
                            hintMessage = this.HintMessage;
                            this.HintMessage = string.Concat(new object[] { hintMessage, ",尺寸:", _filerestrictpic.ImageWidth, "x", _filerestrictpic.ImageHeight });
                            hintMessage = this.ErrorMessage;
                            this.ErrorMessage = string.Concat(new object[] { hintMessage, ",尺寸:", _filerestrictpic.ImageWidth, "x", _filerestrictpic.ImageHeight });
                        }
                    }
                    else if ((this.ImageWidth > 0) || (this.ImageHeight > 0))
                    {
                        hintMessage = this.HintMessage;
                        this.HintMessage = string.Concat(new object[] { hintMessage, ",尺寸:", this.ImageWidth, "x", this.ImageHeight, (this.ImageRange > 0) ? (",误差:" + this.ImageRange) : "" });
                        hintMessage = this.ErrorMessage;
                        this.ErrorMessage = string.Concat(new object[] { hintMessage, ",尺寸:", this.ImageWidth, "x", this.ImageHeight, (this.ImageRange > 0) ? (",误差:" + this.ImageRange) : "" });
                    }
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
        }

        [Description("失去焦点验证成功执行事件"), Browsable(true), DefaultValue(""), Category("Seven：验证功能")]
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

        [Category("Seven：验证功能"), Description("正则表达式"), DefaultValue(false), Browsable(true)]
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

        [DefaultValue(false), Browsable(true), Category("Seven：文件属性"), Description("是否可手动输入文件地址")]
        public bool ContentEditAble
        {
            get
            {
                return this.ViewState.GetBool("ContentEditAble", false);
            }
            set
            {
                this.ViewState["ContentEditAble"] = value;
            }
        }

        public string DownLoadName
        {
            get
            {
                return this.ViewState.GetString("DownLoadName", "下载文件");
            }
            set
            {
                this.ViewState["DownLoadName"] = value;
            }
        }

        public string DownLoadUrl
        {
            get
            {
                return this.ViewState.GetString("DownLoadUrl");
            }
            set
            {
                this.ViewState["DownLoadUrl"] = value;
            }
        }

        [Description("错误信息"), DefaultValue(""), Category("Seven：验证功能"), Browsable(true)]
        public string ErrorMessage
        {
            get
            {
                if (this.ViewState["ErrorMessage"] != null)
                {
                    return (string) this.ViewState["ErrorMessage"];
                }
                return "";
            }
            set
            {
                this.ViewState["ErrorMessage"] = value;
            }
        }

        [Description("限制字段"), DefaultValue(""), Category("Seven：图片属性"), Browsable(true)]
        public string FieldName
        {
            get
            {
                return this.ViewState.GetString("ImageFieldName");
            }
            set
            {
                this.ViewState["ImageFieldName"] = value;
            }
        }

        [DefaultValue(""), Browsable(true), Description("提示信息"), Category("Seven：验证功能")]
        public string HintMessage
        {
            get
            {
                if (this.ViewState["HintMessage"] != null)
                {
                    return (string) this.ViewState["HintMessage"];
                }
                return "";
            }
            set
            {
                this.ViewState["HintMessage"] = value;
            }
        }

        [Category("Seven：图片属性"), DefaultValue(0), Description("图片限制高度"), Browsable(true)]
        public int ImageHeight
        {
            get
            {
                return this.ViewState.GetInt("ImageHeight", 0);
            }
            set
            {
                this.ViewState["ImageHeight"] = value;
            }
        }

        [Description("图片范围"), DefaultValue(0), Browsable(true), Category("Seven：图片属性")]
        public int ImageRange
        {
            get
            {
                return this.ViewState.GetInt("ImageRange", 0);
            }
            set
            {
                this.ViewState["ImageRange"] = value;
            }
        }

        [Category("Seven：图片属性"), Browsable(true), DefaultValue(0), Description("图片限制宽度")]
        public int ImageWidth
        {
            get
            {
                return this.ViewState.GetInt("ImageWidth", 0);
            }
            set
            {
                this.ViewState["ImageWidth"] = value;
            }
        }

        public string Md5Value { get; set; }

        [Browsable(true), DefaultValue(0), Category("Seven：验证功能"), Description("消息宽度")]
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

        public List<ResourceInfo> MoveResourceInfo { get; set; }

        [Category("Seven：图片属性"), Browsable(true), DefaultValue(0), Description("图片预览JQ表达式")]
        public string PreviewExp
        {
            get
            {
                return this.ViewState.GetString("PreviewExp", "");
            }
            set
            {
                this.ViewState["PreviewExp"] = value;
            }
        }

        [Description("文件资源代码"), Category("Seven：资源属性"), DefaultValue(""), Browsable(true)]
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

        public string ResourcePath { get; set; }

        [Browsable(true), Category("Seven：资源属性"), Description("资源唯一标识")]
        public string RestrictCode
        {
            get
            {
                return this.ViewState.GetString("RestrictCode", "");
            }
            set
            {
                this.ViewState["RestrictCode"] = value;
            }
        }

        [DefaultValue(""), Editor(typeof(MyRegexTypeEditor), typeof(UITypeEditor)), Themeable(false), Category("Seven：验证功能")]
        public string ValidationExpression
        {
            get
            {
                if (this.ViewState["ValidationExpression"] != null)
                {
                    return (string) this.ViewState["ValidationExpression"];
                }
                return "";
            }
            set
            {
                this.ViewState["ValidationExpression"] = value;
            }
        }

        [Category("Seven：验证功能"), Description("验证组"), DefaultValue(""), Browsable(true)]
        public string ValidationGroup
        {
            get
            {
                if (this.ViewState["ValidationGroup"] != null)
                {
                    return (string) this.ViewState["ValidationGroup"];
                }
                return "";
            }
            set
            {
                this.ViewState["ValidationGroup"] = value;
            }
        }
    }
}

