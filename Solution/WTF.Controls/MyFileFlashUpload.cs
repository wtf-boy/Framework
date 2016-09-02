namespace WTF.Controls
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Resource;
    using WTF.Resource.Entity;
    using System;
    using System.ComponentModel;
    using System.Data.Objects;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MyFileFlashUpload : FileUpload
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(typeof(MyFileFlashUpload), "uploadify.js"))
            {
                string url = SysVariable.ApplicationPath + "/" + this.ThemePath + "/uploadify.js";
                this.Page.ClientScript.RegisterClientScriptInclude(typeof(MyFileFlashUpload), "uploadify.js", url);
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
            writer.RenderBeginTag(HtmlTextWriterTag.Script);
            StringBuilder builder = new StringBuilder();
            string str = SysVariable.ApplicationPath + "/" + this.ThemePath + "/uploadify.swf";
            writer.Write(builder.ToString());
            builder.AppendLine(" $(function () {");
            builder.AppendLine(" setTimeout(function(){ $(\"#" + this.ClientID + "\").uploadify({");
            builder.AppendFormat("'{0}':{1},", "auto", this.AutoPost.ToString().ToLower());
            builder.AppendFormat("'{0}':{1},", "removeTimeout", this.RemoveTimeout);
            builder.AppendFormat("'{0}':{1},", "multi", this.IsMulti.ToString().ToLower());
            builder.AppendFormat("'{0}':'{1}',", "buttonText", this.ButtonText);
            builder.AppendFormat("'{0}':'{1}',", "swf", str);
            string str2 = SysVariable.ApplicationPath + "/" + this.ThemePath + "/UploadHandler.ashx?ResourceCode=" + this.ResourceCode + "&RestrictCode=" + this.RestrictCode;
            builder.AppendFormat("'{0}':'{1}',", "uploader", str2);
            if (!string.IsNullOrWhiteSpace(this.ConfigOptions))
            {
                this.ConfigOptions = this.ConfigOptions.Trim();
                this.ConfigOptions = this.ConfigOptions + ",";
                builder.Append(this.ConfigOptions);
            }
            if (this.ResourceCode.IsNoNull() && this.RestrictCode.IsNoNull())
            {
                FileResourceRule rule = new FileResourceRule();
                resource_filerestrict _filerestrict = rule.resource_filerestrict.Where("it.RestrictCode='" + this.RestrictCode + "' and it.resource_fileresource.FileResourceCode='" + this.ResourceCode + "'", new ObjectParameter[0]).Include("resource_filestoragepath").Include("resource_filerestrictpic").FirstOrDefault<resource_filerestrict>();
                if (_filerestrict == null)
                {
                    SysAssert.InfoHintAssert("找不到此文件配置");
                }
                if (_filerestrict.FileExtension.IsNoNull())
                {
                    builder.AppendFormat("'{0}':'{1}',", "fileTypeExts", "*." + _filerestrict.FileExtension.Replace(",", ";*."));
                }
                if (_filerestrict.FileMaxSize > 0)
                {
                    builder.AppendFormat("'{0}':'{1}KB',", "fileSizeLimit", _filerestrict.FileMaxSize);
                }
                string str3 = "";
                if ((_filerestrict.FileMaxSize > 0) || _filerestrict.FileExtension.IsNoNull())
                {
                    str3 = "请上传:" + (_filerestrict.FileExtension.IsNoNull() ? ("文件类型:" + _filerestrict.FileExtension) : "") + ((_filerestrict.FileMaxSize > 0) ? ((_filerestrict.FileExtension.IsNoNull() ? "," : "") + "文件必须小于" + ((int)(_filerestrict.FileMaxSize * 0x400)).RenderFileSize()) : "");
                }
                resource_filerestrictpic _filerestrictpic = (from s in _filerestrict.resource_filerestrictpic
                                                             orderby s.ImageWidth descending
                                                             select s).FirstOrDefault<resource_filerestrictpic>();
                if ((_filerestrictpic != null) && (_filerestrictpic.ImageWidth > 0))
                {
                    object obj2 = str3;
                    str3 = string.Concat(new object[] { obj2, ",尺寸:", _filerestrictpic.ImageWidth, " x ", _filerestrictpic.ImageHeight });
                }
                if (!string.IsNullOrWhiteSpace(str3))
                {
                    builder.AppendFormat("'{0}':'{1}',", "fileTypeDesc", str3);
                }
            }
            builder.AppendFormat("'{0}':'{1}',", "fileObjName", this.FileObjName);
            builder.AppendFormat("'{0}':'{1}',", "width", this.Width.Value);
            builder.AppendFormat("'{0}':'{1}',", "height", this.Height.Value);
            builder.AppendFormat("'{0}':{1},", "onUploadSuccess", this.OnUploadSuccess);
            string onUploadError = " function(file, errorCode, errorMsg, errorString) { alert('文件：' + file.name + ' 上传失败: ' + errorString)}";
            if (!string.IsNullOrWhiteSpace(this.OnUploadError))
            {
                onUploadError = this.OnUploadError;
            }
            builder.AppendFormat("'{0}':{1}", "onUploadError", onUploadError);
            builder.AppendLine("});");
            builder.AppendLine("},10);");
            builder.AppendLine("});");
            writer.Write(builder.ToString());
            writer.RenderEndTag();
        }

        public bool AutoPost
        {
            get
            {
                return this.ViewState.GetBool("AutoPost", true);
            }
            set
            {
                this.ViewState["AutoPost"] = value;
            }
        }

        [Category("Seven：MyFileMultiUpload"), Description("文件"), Browsable(true)]
        public string ButtonText
        {
            get
            {
                return this.ViewState.GetString("buttonText", "选择文件");
            }
            set
            {
                this.ViewState["buttonText"] = value;
            }
        }

        [Category("Seven：MyFileMultiUpload"), Browsable(true), Description("上传成功")]
        public string ConfigOptions
        {
            get
            {
                return this.ViewState.GetString("ConfigOptions", "");
            }
            set
            {
                this.ViewState["ConfigOptions"] = value;
            }
        }

        public string FileObjName
        {
            get
            {
                return this.ViewState.GetString("fileObjName", "MultiFileName");
            }
            set
            {
                this.ViewState["fileObjName"] = value;
            }
        }

        public bool IsMulti
        {
            get
            {
                return this.ViewState.GetBool("IsMulti", true);
            }
            set
            {
                this.ViewState["IsMulti"] = value;
            }
        }

        [Category("Seven：MyFileMultiUpload"), Description("上传失败"), Browsable(true)]
        public string OnUploadError
        {
            get
            {
                return this.ViewState.GetString("OnUploadError", "");
            }
            set
            {
                this.ViewState["OnUploadError"] = value;
            }
        }

        [Category("Seven：MyFileMultiUpload"), Description("上传成功"), Browsable(true)]
        public string OnUploadSuccess
        {
            get
            {
                return this.ViewState.GetString("OnUploadSuccess", "");
            }
            set
            {
                this.ViewState["OnUploadSuccess"] = value;
            }
        }

        public double RemoveTimeout
        {
            get
            {
                return this.ViewState.GetDouble("RemoveTimeout", 0.5);
            }
            set
            {
                this.ViewState["RemoveTimeout"] = value;
            }
        }

        [Category("Seven：MyFileMultiUpload"), Description("资源文件码"), Browsable(true)]
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

        [Browsable(true), Category("Seven：MyFileMultiUpload"), Description("资源文件限制码")]
        public string RestrictCode
        {
            get
            {
                return this.ViewState.GetString("RestrictCode", "UploadSuccess");
            }
            set
            {
                this.ViewState["RestrictCode"] = value;
            }
        }

        public string ThemePath
        {
            get
            {
                return this.ViewState.GetString("ThemePath", "App_Control/Uploadify");
            }
            set
            {
                this.ViewState["ThemePath"] = value;
            }
        }
    }
}

