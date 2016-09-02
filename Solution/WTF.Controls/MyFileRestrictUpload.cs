namespace WTF.Controls
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Resource;
    using WTF.Resource.Entity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.IO;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyFileRestrictUpload : FileUpload
    {
        private List<ResourceInfo> _MoveResourceInfo = null;
        public string _ResourcePath = "";

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
                ResourceInfo info;
                ResourceRule rule = new ResourceRule();
                this.ResourceTypeID.CheckIsNull("请设置资源类型标识", LogModuleType.ResourceLog);
                this.RestrictCode.CheckIsNull("请设置RestrictCode", LogModuleType.ResourceLog);
                Sys_ResourceRestrict resourceRestrict = rule.GetResourceRestrict(this.ResourceTypeID, this.RestrictCode);
                string fileExtension = Path.GetExtension(base.FileName).ToLower();
                SysAssert.InfoHintAssert(!string.IsNullOrEmpty(resourceRestrict.FileExtension) && (resourceRestrict.FileExtension.IndexOf(fileExtension.Trim(new char[] { '.' })) == -1), "上传的文件扩展名必须是" + resourceRestrict.FileExtension);
                SysAssert.InfoHintAssert(((((double) base.PostedFile.ContentLength) / 1024.0) > resourceRestrict.FileMaxSize) && (resourceRestrict.FileMaxSize > 0), "对不起支持上传的文件大小为" + ((int) (resourceRestrict.FileMaxSize * 0x400)).RenderFileSize() + ",当前的文件大小超出");
                if (this.ResourceID.IsNull())
                {
                    if (this.ResourceName.IsNull())
                    {
                        this.ResourceName = string.Concat(new object[] { "类型:", resourceRestrict.ResourceTypeID, "限制标识", resourceRestrict.ResourceRestrictID.ToString(), base.FileName });
                    }
                    this.ResourceID = rule.InsertResource(this.ResourceName, this.ResourceTypeID);
                }
                else
                {
                    if (this.ResourceName.IsNull())
                    {
                        this.ResourceName = string.Concat(new object[] { "类型:", resourceRestrict.ResourceTypeID, "限制标识", resourceRestrict.ResourceRestrictID.ToString(), base.FileName });
                    }
                    rule.CheckResourceID(this.ResourceID, this.ResourceName, resourceRestrict.ResourceTypeID);
                }
                if ((base.PostedFile.ContentType.IndexOf("image") != -1) && (resourceRestrict.Sys_ResourceRestrictPic.Count > 0))
                {
                    System.Drawing.Image image;
                    Sys_WaterImage image2;
                    System.Drawing.Image image3;
                    if (resourceRestrict.Sys_ResourceRestrictPic.Count > 1)
                    {
                        this._MoveResourceInfo = new List<ResourceInfo>();
                        using (List<Sys_ResourceRestrictPic>.Enumerator enumerator = (from s in resourceRestrict.Sys_ResourceRestrictPic
                            orderby s.VerNo
                            orderby s.ResourceRestrictPicID
                            select s).ToList<Sys_ResourceRestrictPic>().GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Stream stream;
                                Sys_ResourceRestrictPic objFileVerInfo = enumerator.Current;
                                int verNo = objFileVerInfo.VerNo;
                                if (verNo == 0)
                                {
                                    verNo = rule.GetResourceMaxVerNo(this.ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo);
                                }
                                image = base.PostedFile.InputStream.StreamToImage();
                                if ((objFileVerInfo.ImageWidth > 0) && (objFileVerInfo.ImageHeight > 0))
                                {
                                    if ((image.Width > objFileVerInfo.ImageWidth) || (image.Height > objFileVerInfo.ImageHeight))
                                    {
                                        image = image.CreateThumbnail("<=" + objFileVerInfo.ImageWidth.ToString(), "<=" + objFileVerInfo.ImageHeight.ToString());
                                        if (objFileVerInfo.CreateWaterMark)
                                        {
                                            if (objFileVerInfo.WatermarkType == 2)
                                            {
                                                SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                                image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                            }
                                            else
                                            {
                                                image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                                if (image2.IsNoNull())
                                                {
                                                    image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                                    image = image.CreateWatermark(image3, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                                    image3.Dispose();
                                                }
                                            }
                                        }
                                        this._MoveResourceInfo.Add(rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), verNo, this.Remark));
                                        image.Dispose();
                                    }
                                    else if (objFileVerInfo.CreateWaterMark)
                                    {
                                        if (objFileVerInfo.WatermarkType == 2)
                                        {
                                            SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                            image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                        }
                                        else
                                        {
                                            image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                            if (image2.IsNoNull())
                                            {
                                                image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                                image = image.CreateWatermark(image3, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                                image3.Dispose();
                                            }
                                        }
                                        this._MoveResourceInfo.Add(rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), verNo, this.Remark));
                                        image.Dispose();
                                    }
                                    else
                                    {
                                        stream = image.ImageToStream(fileExtension);
                                        this._MoveResourceInfo.Add(rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", stream, verNo, this.Remark));
                                    }
                                }
                                else if (objFileVerInfo.CreateWaterMark)
                                {
                                    if (objFileVerInfo.WatermarkType == 2)
                                    {
                                        SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                        image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                    }
                                    else
                                    {
                                        image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                        if (image2.IsNoNull())
                                        {
                                            image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                            image = image.CreateWatermark(image3, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                            image3.Dispose();
                                        }
                                    }
                                    this._MoveResourceInfo.Add(rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), verNo, this.Remark));
                                    image.Dispose();
                                }
                                else
                                {
                                    stream = image.ImageToStream(fileExtension);
                                    this._MoveResourceInfo.Add(rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", stream, objFileVerInfo.VerNo, this.Remark));
                                }
                                image.Dispose();
                            }
                        }
                        return;
                    }
                    Sys_ResourceRestrictPic objSys_ResourceRestrictPic = resourceRestrict.Sys_ResourceRestrictPic.FirstOrDefault<Sys_ResourceRestrictPic>();
                    if (this.VerNo == 0)
                    {
                        this.VerNo = objSys_ResourceRestrictPic.VerNo;
                    }
                    if (objSys_ResourceRestrictPic.CreateWaterMark && (fileExtension != ".gif"))
                    {
                        image = base.PostedFile.InputStream.StreamToImage();
                        if (((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0)) && ((image.Width > objSys_ResourceRestrictPic.ImageWidth) || (image.Height > objSys_ResourceRestrictPic.ImageHeight)))
                        {
                            image = image.CreateThumbnail("<=" + objSys_ResourceRestrictPic.ImageWidth.ToString(), "<=" + objSys_ResourceRestrictPic.ImageHeight.ToString());
                        }
                        if (objSys_ResourceRestrictPic.WatermarkType == 2)
                        {
                            SysAssert.CheckCondition(string.IsNullOrEmpty(objSys_ResourceRestrictPic.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                            image = image.CreateWatermark(objSys_ResourceRestrictPic.WatermarkText, (HorizontalAlign) objSys_ResourceRestrictPic.HorizontalAlign, (VerticalAlign) objSys_ResourceRestrictPic.VerticalAlign);
                        }
                        else
                        {
                            image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objSys_ResourceRestrictPic.WaterImageID);
                            if (image2.IsNoNull())
                            {
                                image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                image = image.CreateWatermark(image3, (HorizontalAlign) objSys_ResourceRestrictPic.HorizontalAlign, (VerticalAlign) objSys_ResourceRestrictPic.VerticalAlign);
                            }
                        }
                        info = rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), this.Remark);
                        image.Dispose();
                    }
                    else if ((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0))
                    {
                        image = base.PostedFile.InputStream.StreamToImage();
                        if ((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0))
                        {
                            if ((image.Width > objSys_ResourceRestrictPic.ImageWidth) || (image.Height > objSys_ResourceRestrictPic.ImageHeight))
                            {
                                image = image.CreateThumbnail("<=" + objSys_ResourceRestrictPic.ImageWidth.ToString(), "<=" + objSys_ResourceRestrictPic.ImageHeight.ToString());
                                info = rule.InsertResourceVer(this.ResourceID, Path.GetFileName(base.PostedFile.FileName), base.PostedFile.ContentLength, base.PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), this.Remark);
                            }
                            else
                            {
                                info = rule.InsertResourceVer(this.ResourceID, (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), "admin", base.PostedFile, this.Remark);
                            }
                        }
                        else
                        {
                            info = rule.InsertResourceVer(this.ResourceID, (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), "admin", base.PostedFile, this.Remark);
                        }
                        image.Dispose();
                    }
                    else
                    {
                        info = rule.InsertResourceVer(this.ResourceID, (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), "admin", base.PostedFile, this.Remark);
                    }
                }
                else
                {
                    if (this.VerNo == 0)
                    {
                        this.VerNo = resourceRestrict.VerNo;
                    }
                    info = rule.InsertResourceVer(this.ResourceID, (this.VerNo != 0) ? this.VerNo : rule.GetResourceMaxVerNo(this.ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), "admin", base.PostedFile, this.Remark);
                }
                this.ResourceID = info.ResourceID;
                this.ResourcePath = info.ResourcePath;
                this.VerNo = info.VerNo;
            }
        }

        private void ValidationInit()
        {
            if ((this.ValidationGroup.IsNoNull() && this.ResourceTypeID.IsNoNull()) && this.RestrictCode.IsNoNull())
            {
                Sys_ResourceRestrict resourceRestrict = new ResourceRule().GetResourceRestrict(this.ResourceTypeID, this.RestrictCode);
                if (resourceRestrict.FileExtension.IsNoNull())
                {
                    this.ValidationExpression = "(" + resourceRestrict.FileExtension.Replace(',', '|') + ")$";
                }
                if (this.HintMessage.IsNull())
                {
                    if ((resourceRestrict.FileMaxSize > 0) || resourceRestrict.FileExtension.IsNoNull())
                    {
                        this.HintMessage = (resourceRestrict.FileExtension.IsNoNull() ? ("文件类型:" + resourceRestrict.FileExtension) : "") + ((resourceRestrict.FileMaxSize > 0) ? ((resourceRestrict.FileExtension.IsNoNull() ? "," : "") + "文件必须小于" + ((int) (resourceRestrict.FileMaxSize * 0x400)).RenderFileSize()) : "");
                        this.ErrorMessage = "请上传:" + (resourceRestrict.FileExtension.IsNoNull() ? ("文件类型:" + resourceRestrict.FileExtension) : "") + ((resourceRestrict.FileMaxSize > 0) ? ((resourceRestrict.FileExtension.IsNoNull() ? "," : "") + "文件必须小于" + ((int) (resourceRestrict.FileMaxSize * 0x400)).RenderFileSize()) : "");
                    }
                    Sys_ResourceRestrictPic pic = (from s in resourceRestrict.Sys_ResourceRestrictPic
                        orderby s.ImageWidth descending
                        select s).FirstOrDefault<Sys_ResourceRestrictPic>();
                    if ((pic != null) && (pic.ImageWidth > 0))
                    {
                        object hintMessage = this.HintMessage;
                        this.HintMessage = string.Concat(new object[] { hintMessage, ",尺寸:", pic.ImageWidth, " x ", pic.ImageHeight });
                        hintMessage = this.ErrorMessage;
                        this.ErrorMessage = string.Concat(new object[] { hintMessage, ",尺寸:", pic.ImageWidth, " x ", pic.ImageHeight });
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

        [Category("Seven：验证功能"), Description("失去焦点验证成功执行事件"), DefaultValue(""), Browsable(true)]
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

        [DefaultValue(false), Description("正则表达式"), Browsable(true), Category("Seven：验证功能")]
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

        [Browsable(true), Description("是否可手动输入文件地址"), DefaultValue(false), Category("Seven：文件属性")]
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

        [Browsable(true), DefaultValue(""), Category("Seven：验证功能"), Description("错误信息")]
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

        [Browsable(true), DefaultValue(""), Category("Seven：验证功能"), Description("提示信息")]
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

        [Category("Seven：验证功能"), Browsable(true), Description("消息宽度"), DefaultValue(0)]
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

        [Category("Seven：图片属性"), DefaultValue(0), Description("图片预览JQ表达式"), Browsable(true)]
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

        [Description("资源备注"), Browsable(true), DefaultValue(""), Category("Seven：版本属性")]
        public string Remark
        {
            get
            {
                return this.ViewState.GetString("Remark", "");
            }
            set
            {
                this.ViewState["Remark"] = value;
            }
        }

        [Description("资源唯一标识"), Browsable(true), Category("Seven：资源属性")]
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

        [Browsable(true), Category("Seven：资源属性"), Description("资源名称")]
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

        public string ResourcePath
        {
            get
            {
                return this._ResourcePath;
            }
            set
            {
                this._ResourcePath = value;
            }
        }

        [DefaultValue(0), Category("Seven：资源属性"), Description("资源类型参数标识"), Browsable(true)]
        public int ResourceTypeID
        {
            get
            {
                return this.ViewState.GetInt("ResourceTypeID", -1);
            }
            set
            {
                this.ViewState["ResourceTypeID"] = value;
            }
        }

        [Category("Seven：资源属性"), Description("资源唯一标识"), Browsable(true)]
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

        [Category("Seven：验证功能"), Editor(typeof(MyRegexTypeEditor), typeof(UITypeEditor)), DefaultValue(""), Themeable(false)]
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
    }
}

