namespace WTF.Controls
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Resource;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyFileUpload : FileUpload
    {
        private WTF.Controls.FileVers _FileVers;
        private List<ResourceInfo> _MoveResourceInfo = null;

        protected override void OnLoad(EventArgs e)
        {
            if (!this.ContentEditAble)
            {
                base.Attributes.Add("contenteditable", "false");
            }
            if (this.PreviewExp.IsNoNull())
            {
                base.Attributes.Add("onchange", "javascript:ImagePreview.Preview(this,\"" + this.PreviewExp + "\");");
            }
            base.OnLoad(e);
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
                ResourceInfo info;
                ResourceRule rule = new ResourceRule();
                string fileExtension = Path.GetExtension(base.FileName).ToLower();
                SysAssert.InfoHintAssert(!string.IsNullOrEmpty(this.FileExtension) && (this.FileExtension.IndexOf(fileExtension.Trim(new char[] { '.' })) == -1), "上传的文件扩展名必须是" + this.FileExtension);
                SysAssert.InfoHintAssert((this.MaxFileSize > 0) && ((((double) base.PostedFile.ContentLength) / 1024.0) > this.MaxFileSize), "对不起支持上传的文件大小为" + ((int) (this.MaxFileSize * 0x400)).RenderFileSize() + "当前的文件大小超出");
                if (this.ResourceID.IsNull())
                {
                    SysAssert.CheckCondition(this.ResourceTypeID <= 0, "请选择资源类型参数标识", LogModuleType.ResourceLog);
                    if (this.ResourceName.IsNull())
                    {
                        this.ResourceName = "类型:" + this.ResourceTypeID + base.FileName;
                    }
                    this.ResourceID = rule.InsertResource(this.ResourceName, this.ResourceTypeID);
                }
                else
                {
                    if (this.ResourceName.IsNull())
                    {
                        this.ResourceName = "类型:" + this.ResourceTypeID + base.FileName;
                    }
                    rule.CheckResourceID(this.ResourceID, this.ResourceName, this.ResourceTypeID);
                }
                if (base.PostedFile.ContentType.IndexOf("image") != -1)
                {
                    System.Drawing.Image image;
                    if (this.FileVers.Count > 0)
                    {
                        this._MoveResourceInfo = new List<ResourceInfo>();
                        foreach (FileVerInfo info2 in this.FileVers)
                        {
                            Stream stream;
                            if (info2.VerNo == 0)
                            {
                                info2.VerNo = rule.GetResourceMaxVerNo(this.ResourceID, this.BeginVerNo, this.EndVerNo);
                            }
                            image = base.PostedFile.InputStream.StreamToImage();
                            if ((info2.ImageWidth > 0) && (info2.ImageHeight > 0))
                            {
                                if ((image.Width > info2.ImageWidth) || (image.Height > info2.ImageHeight))
                                {
                                    image = image.CreateThumbnail("<=" + info2.ImageWidth.ToString(), "<=" + info2.ImageHeight.ToString());
                                    this._MoveResourceInfo.Add(rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), info2.VerNo, this.Remark));
                                }
                                else
                                {
                                    stream = image.ImageToStream(fileExtension);
                                    this._MoveResourceInfo.Add(rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", stream, info2.VerNo, this.Remark));
                                }
                            }
                            else
                            {
                                stream = image.ImageToStream(fileExtension);
                                this._MoveResourceInfo.Add(rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", stream, info2.VerNo, this.Remark));
                            }
                            image.Dispose();
                        }
                        return;
                    }
                    if (this.CreateWaterMark && (fileExtension != ".gif"))
                    {
                        image = base.PostedFile.InputStream.StreamToImage();
                        if (((this.ImageWidth > 0) && (this.ImageHeight > 0)) && ((image.Width > this.ImageWidth) || (image.Height > this.ImageHeight)))
                        {
                            image = image.CreateThumbnail("<=" + this.ImageWidth.ToString(), "<=" + this.ImageHeight.ToString());
                        }
                        if (this.WatermarkTypeValue == WatermarkType.WaterText)
                        {
                            SysAssert.CheckCondition(string.IsNullOrEmpty(this.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                            image = image.CreateWatermark(this.WatermarkText, this.WatermarkHorizontalAlign, this.WatermarkVerticalAlign);
                        }
                        info = rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, this.BeginVerNo, this.EndVerNo), this.Remark);
                        image.Dispose();
                    }
                    else if ((this.ImageWidth > 0) && (this.ImageHeight > 0))
                    {
                        image = base.PostedFile.InputStream.StreamToImage();
                        if ((this.ImageWidth > 0) && (this.ImageHeight > 0))
                        {
                            if ((image.Width > this.ImageWidth) || (image.Height > this.ImageHeight))
                            {
                                image = image.CreateThumbnail("<=" + this.ImageWidth.ToString(), "<=" + this.ImageHeight.ToString());
                                info = rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, this.BeginVerNo, this.EndVerNo), this.Remark);
                            }
                            else
                            {
                                info = rule.InsertResourceVer(this.ResourceID, (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, this.BeginVerNo, this.EndVerNo), "admin", base.PostedFile, this.Remark);
                            }
                        }
                        else
                        {
                            info = rule.InsertResourceVer(this.ResourceID, (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, this.BeginVerNo, this.EndVerNo), "admin", base.PostedFile, this.Remark);
                        }
                        image.Dispose();
                    }
                    else
                    {
                        info = rule.InsertResourceVer(this.ResourceID, (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, this.BeginVerNo, this.EndVerNo), "admin", base.PostedFile, this.Remark);
                    }
                }
                else
                {
                    info = rule.InsertResourceVer(this.ResourceID, (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, this.BeginVerNo, this.EndVerNo), "admin", base.PostedFile, this.Remark);
                }
                this.ResourceID = info.ResourceID;
                this.ResourcePath = info.ResourcePath;
                this.VerNo = info.VerNo;
            }
        }

        private void ValidationInit()
        {
            if (this.ValidationGroup.IsNoNull() && this.ResourceTypeID.IsNoNull())
            {
                if (this.FileExtension.IsNoNull())
                {
                    this.ValidationExpression = "(" + this.FileExtension.Replace(',', '|') + ")$";
                }
                if (this.HintMessage.IsNull())
                {
                    object hintMessage;
                    if ((this.MaxFileSize > 0) || this.FileExtension.IsNoNull())
                    {
                        this.HintMessage = (this.FileExtension.IsNoNull() ? ("文件类型:" + this.FileExtension) : "") + ((this.MaxFileSize > 0) ? ((this.FileExtension.IsNoNull() ? "," : "") + "文件必须小于" + ((int) (this.MaxFileSize * 0x400)).RenderFileSize()) : "");
                        this.ErrorMessage = "请上传:" + (this.FileExtension.IsNoNull() ? ("文件类型:" + this.FileExtension) : "") + ((this.MaxFileSize > 0) ? ((this.FileExtension.IsNoNull() ? "," : "") + "文件必须小于" + ((int) (this.MaxFileSize * 0x400)).RenderFileSize()) : "");
                    }
                    if ((this.ImageWidth > 0) && (this.ImageHeight > 0))
                    {
                        hintMessage = this.HintMessage;
                        this.HintMessage = string.Concat(new object[] { hintMessage, ",尺寸:", this.ImageWidth, " x ", this.ImageHeight });
                        hintMessage = this.ErrorMessage;
                        this.ErrorMessage = string.Concat(new object[] { hintMessage, ",尺寸:", this.ImageWidth, " x ", this.ImageHeight });
                    }
                    if (this.FileVers.Count > 0)
                    {
                        int imageWidth = 0;
                        FileVerInfo info = null;
                        foreach (FileVerInfo info2 in this.FileVers)
                        {
                            if (info2.ImageWidth > imageWidth)
                            {
                                info = info2;
                                imageWidth = info2.ImageWidth;
                            }
                        }
                        if ((info != null) && (info.ImageWidth > 0))
                        {
                            hintMessage = this.HintMessage;
                            this.HintMessage = string.Concat(new object[] { hintMessage, ",宽:", info.ImageWidth, "px 高:", info.ImageHeight, "px" });
                            hintMessage = this.ErrorMessage;
                            this.ErrorMessage = string.Concat(new object[] { hintMessage, ",宽:", info.ImageWidth, "px 高:", info.ImageHeight, "px" });
                        }
                    }
                }
                if (!string.IsNullOrEmpty(this.ValidationGroup))
                {
                    base.Attributes.Add("ValidationGroup", this.ValidationGroup);
                }
                if (!string.IsNullOrEmpty(this.ValidationExpression))
                {
                    base.Attributes.Add("ValidationExpression", this.ValidationExpression);
                }
                if (!string.IsNullOrEmpty(this.ErrorMessage))
                {
                    base.Attributes.Add("ErrorMessage", this.ErrorMessage);
                    base.Attributes.Add("onblur", "$(this).BlurValidationError(" + (this.BlurSucessCall.IsNull() ? "" : this.BlurSucessCall) + ");");
                }
                if (!string.IsNullOrEmpty(this.HintMessage))
                {
                    base.Attributes.Add("HintMessage", this.HintMessage);
                    base.Attributes.Add("onfocus", "$(this).FocusValidationHint();");
                }
                if (this.MessageWidth != 0)
                {
                    base.Attributes.Add("MessageWidth", this.MessageWidth.ToString());
                }
                if (this.CheckValueEmpty)
                {
                    base.Attributes.Add("CheckValueEmpty", this.CheckValueEmpty.ToString());
                }
            }
        }

        [Description("起始版本号"), DefaultValue(1), Category("Seven：版本属性"), Browsable(true)]
        public int BeginVerNo
        {
            get
            {
                return this.ViewState.GetInt("BeginVerNo", 1);
            }
            set
            {
                this.ViewState["BeginVerNo"] = value;
            }
        }

        [DefaultValue(""), Browsable(true), Description("失去焦点验证成功执行事件"), Category("Seven：验证功能")]
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

        [Category("Seven：文件属性"), Description("是否可手动输入文件地址"), DefaultValue(false), Browsable(true)]
        public bool ContentEditAble
        {
            get
            {
                object obj2 = this.ViewState["ContentEditAble"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["ContentEditAble"] = value;
            }
        }

        [Browsable(true), Description("是否进行水印"), DefaultValue(false), Category("Seven：水印属性")]
        public bool CreateWaterMark
        {
            get
            {
                return this.ViewState.GetBool("CreateWaterMark", false);
            }
            set
            {
                this.ViewState["CreateWaterMark"] = value;
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

        [Browsable(true), Category("Seven：版本属性"), DefaultValue(0x3e8), Description("止版本号")]
        public int EndVerNo
        {
            get
            {
                return this.ViewState.GetInt("EndVerNo", 0x5f5e100);
            }
            set
            {
                this.ViewState["EndVerNo"] = value;
            }
        }

        [Category("Seven：验证功能"), Browsable(true), Description("错误信息"), DefaultValue("")]
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

        [DefaultValue(0), Category("Seven：文件属性"), Description("支持上传的文件扩展名，空时为不限制类型，多种类型时以半角逗号分开"), Browsable(true)]
        public string FileExtension
        {
            get
            {
                return this.ViewState.GetString("FileExtension");
            }
            set
            {
                this.ViewState["FileExtension"] = value.ToLower();
            }
        }

        [Description("多版本上传设置"), Category("Seven：图片属性"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public WTF.Controls.FileVers FileVers
        {
            get
            {
                if (this._FileVers == null)
                {
                    this._FileVers = new WTF.Controls.FileVers();
                }
                return this._FileVers;
            }
        }

        [Category("Seven：验证功能"), Browsable(true), Description("提示信息"), DefaultValue("")]
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

        [DefaultValue(0), Description("图片缩略高度"), Browsable(true), Category("Seven：图片属性")]
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

        [Category("Seven：图片属性"), Browsable(true), DefaultValue(0), Description("图片缩略宽度")]
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

        [Category("Seven：文件属性"), Browsable(true), DefaultValue(0), Description("文件的最大上传的大小（单位KB），０时为不限制大小")]
        public int MaxFileSize
        {
            get
            {
                object obj2 = this.ViewState["MaxFileSize"];
                return ((obj2 == null) ? ConfigHelper.GetIntValue("MaxFileSize", 0) : ((int) obj2));
            }
            set
            {
                this.ViewState["MaxFileSize"] = value;
            }
        }

        [DefaultValue(0), Category("Seven：验证功能"), Browsable(true), Description("消息宽度")]
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

        public List<ResourceInfo> MoveResourceInfo
        {
            get
            {
                return this._MoveResourceInfo;
            }
        }

        [DefaultValue(0), Category("Seven：图片属性"), Description("图片预览JQ表达式"), Browsable(true)]
        public string PreviewExp
        {
            get
            {
                return this.ViewState.GetString("PreviewExp");
            }
            set
            {
                this.ViewState["PreviewExp"] = value;
            }
        }

        [Category("Seven：版本属性"), DefaultValue(""), Browsable(true), Description("资源备注")]
        public string Remark
        {
            get
            {
                object obj2 = this.ViewState["Remark"];
                return ((obj2 == null) ? "" : ((string) obj2));
            }
            set
            {
                this.ViewState["Remark"] = value;
            }
        }

        [Category("Seven：资源属性"), Description("资源唯一标识"), Browsable(true)]
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

        [Category("Seven：资源属性"), Description("资源名称"), Browsable(true)]
        public string ResourceName
        {
            get
            {
                return this.ViewState.GetString("ResourceName");
            }
            set
            {
                this.ViewState["ResourceName"] = value;
            }
        }

        [Category("Seven：版本属性"), Browsable(true), Description("资源路经")]
        public string ResourcePath
        {
            get
            {
                object obj2 = this.ViewState["ResourcePath"];
                return ((obj2 == null) ? "" : ((string) obj2));
            }
            set
            {
                this.ViewState["ResourcePath"] = value;
            }
        }

        [Category("Seven：资源属性"), Description("资源类型参数标识"), Browsable(true), DefaultValue(0)]
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

        [Themeable(false), Category("Seven：验证功能"), Editor(typeof(MyRegexTypeEditor), typeof(UITypeEditor)), DefaultValue("")]
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

        [Browsable(true), Description("验证组"), DefaultValue(""), Category("Seven：验证功能")]
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

        [Category("Seven：版本属性"), DefaultValue(0), Browsable(true), Description("资源版本号，当版本号为０时，为多版本上传，指定版本号时为特定单版本上传")]
        public int VerNo
        {
            get
            {
                return this.ViewState.GetInt("VerNo", 0);
            }
            set
            {
                this.ViewState["VerNo"] = value;
            }
        }

        [Browsable(true), Description("水印图片水平对齐方式"), Category("Seven：水印属性"), DefaultValue("")]
        public HorizontalAlign WatermarkHorizontalAlign
        {
            get
            {
                return this.ViewState.GetT<HorizontalAlign>("WatermarkHorizontalAlign", HorizontalAlign.Center);
            }
            set
            {
                this.ViewState["WatermarkHorizontalAlign"] = value;
            }
        }

        [Description("水印文字"), Browsable(true), Category("Seven：水印属性"), DefaultValue("")]
        public string WatermarkText
        {
            get
            {
                return this.ViewState.GetString("CreateWaterMark");
            }
            set
            {
                this.ViewState["WatermarkText"] = value;
            }
        }

        [Category("Seven：水印属性"), Browsable(true), DefaultValue(2), Description("水印类型")]
        public WatermarkType WatermarkTypeValue
        {
            get
            {
                return this.ViewState.GetT<WatermarkType>("WatermarkTypeValue", WatermarkType.WaterText);
            }
            set
            {
                this.ViewState["WatermarkTypeValue"] = value;
            }
        }

        [Browsable(true), DefaultValue(""), Description("水印图片垂直对齐方式"), Category("Seven：水印属性")]
        public VerticalAlign WatermarkVerticalAlign
        {
            get
            {
                return this.ViewState.GetT<VerticalAlign>("WatermarkVerticalAlign", VerticalAlign.Middle);
            }
            set
            {
                this.ViewState["WatermarkVerticalAlign"] = value;
            }
        }
    }
}

