namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    public class FileVerInfo
    {
        private bool _CreateWaterMark = false;
        private HorizontalAlign _HorizontalAlign = HorizontalAlign.Center;
        private int _ImageHeight = 0;
        private int _ImageWidth = 0;
        private int _VerNo;
        private string _WatermarkText;
        private WatermarkType _WatermarkTypeValue = WatermarkType.WaterText;
        private VerticalAlign _WatermarkVerticalAlign = VerticalAlign.Middle;

        [Description("是否进行水印"), DefaultValue(false), Browsable(true), Category("Seven：水印属性")]
        public bool CreateWaterMark
        {
            get
            {
                return this._CreateWaterMark;
            }
            set
            {
                this._CreateWaterMark = value;
            }
        }

        [Browsable(true), Description("图片缩略高度"), DefaultValue(0), Category("Seven：图片属性")]
        public int ImageHeight
        {
            get
            {
                return this._ImageHeight;
            }
            set
            {
                this._ImageHeight = value;
            }
        }

        [DefaultValue(0), Browsable(true), Category("Seven：图片属性"), Description("图片缩略宽度")]
        public int ImageWidth
        {
            get
            {
                return this._ImageWidth;
            }
            set
            {
                this._ImageWidth = value;
            }
        }

        [Browsable(true), DefaultValue(0), Description("资源版本号，当版本号为０时，为多版本上传，指定版本号时为特定单版本上传"), Category("Seven：版本属性")]
        public int VerNo
        {
            get
            {
                return this._VerNo;
            }
            set
            {
                this._VerNo = value;
            }
        }

        [Category("Seven：水印属性"), DefaultValue(""), Description("水印图片水平对齐方式"), Browsable(true)]
        public HorizontalAlign WatermarkHorizontalAlign
        {
            get
            {
                return this._HorizontalAlign;
            }
            set
            {
                this._HorizontalAlign = value;
            }
        }

        [Browsable(true), Category("Seven：水印属性"), Description("水印文字"), DefaultValue("")]
        public string WatermarkText
        {
            get
            {
                return this._WatermarkText;
            }
            set
            {
                this._WatermarkText = value;
            }
        }

        [DefaultValue(2), Category("Seven：水印属性"), Browsable(true), Description("水印类型")]
        public WatermarkType WatermarkTypeValue
        {
            get
            {
                return this._WatermarkTypeValue;
            }
            set
            {
                this._WatermarkTypeValue = value;
            }
        }

        [DefaultValue(""), Category("Seven：水印属性"), Browsable(true), Description("水印图片垂直对齐方式")]
        public VerticalAlign WatermarkVerticalAlign
        {
            get
            {
                return this._WatermarkVerticalAlign;
            }
            set
            {
                this._WatermarkVerticalAlign = value;
            }
        }
    }
}

