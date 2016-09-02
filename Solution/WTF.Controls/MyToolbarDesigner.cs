namespace WTF.Controls
{
    using System;
    using System.Globalization;
    using System.Web.UI.Design;

    public class MyToolbarDesigner : ControlDesigner
    {
        public override string GetDesignTimeHtml()
        {
            MyToolbar component = (MyToolbar) base.Component;
            return string.Format(CultureInfo.InvariantCulture, "<div  style='  height: 25px;line-height:25px; text-align:center;font-weight:bold' >SevenToolBar</div>", new object[0]);
        }
    }
}

