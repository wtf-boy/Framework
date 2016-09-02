namespace WTF.Controls
{
    using WTF.Framework;
    using System;
    using System.Runtime.InteropServices;

    public class ControlVerHelper
    {
        public static string GetVer(string addQuery = "?")
        {
            string str = ConfigHelper.GetValue("ControlVer");
            return (string.IsNullOrWhiteSpace(str) ? "" : (addQuery + "ver=" + str));
        }
    }
}

