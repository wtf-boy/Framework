namespace WTF.Controls
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public static class GridViewCommandEventArgsHelper
    {
        public static string CommandExpandArgument(this GridViewCommandEventArgs CommandEvent)
        {
            if (CommandEvent.CommandSource is MenuLinkButton)
            {
                return ((MenuLinkButton) CommandEvent.CommandSource).CommandExpandArgument;
            }
            if (CommandEvent.CommandSource is MenuImageButton)
            {
                return ((MenuImageButton) CommandEvent.CommandSource).CommandExpandArgument;
            }
            return string.Empty;
        }
    }
}

