namespace WTF.Controls
{
    using System;
    using System.Web.UI.WebControls;

    public class MyImagePreview : Image
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ToolTip = "点击查看原图";
            base.Attributes.Add("onclick", "OpenImagePreview(this);");
            base.Attributes.Add("style", "cursor:pointer;");
        }

        public override string ImageUrl
        {
            get
            {
                return (string.IsNullOrEmpty(base.ImageUrl) ? "~/App_Themes/Default/Image/Default.gif" : base.ImageUrl);
            }
            set
            {
                base.ImageUrl = value;
            }
        }
    }
}

