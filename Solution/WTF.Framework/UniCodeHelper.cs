namespace WTF.Framework
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

    public static class UniCodeHelper
    {
        public static string DecryptUniCode(this string uniCodeValue)
        {
            string message = "";
            if (!string.IsNullOrEmpty(uniCodeValue))
            {
                string[] strArray = uniCodeValue.Replace("/", "").Split(new char[] { 'u' });
                try
                {
                    for (int i = 1; i < strArray.Length; i++)
                    {
                        message = message + ((char) int.Parse(strArray[i], NumberStyles.HexNumber));
                    }
                }
                catch (FormatException exception)
                {
                    message = exception.Message;
                }
            }
            return message;
        }

        public static string DecryptUniCodeJs(this string uniCodeValue)
        {
            Regex regex = new Regex(@"(?i)\\u([0-9a-f]{4})");
            return regex.Replace(uniCodeValue, (MatchEvaluator) (m1 => ((char) Convert.ToInt32(m1.Groups[1].Value, 0x10)).ToString()));
        }

        public static string EncryptUnicode(this string source)
        {
            string str = "";
            if (!string.IsNullOrEmpty(source))
            {
                for (int i = 0; i < source.Length; i++)
                {
                    str = str + "/u" + ((int) source[i]).ToString("x");
                }
            }
            return str;
        }

        public static string EncryptUnicodeJs(this string source)
        {
            string str = "";
            if (!string.IsNullOrEmpty(source))
            {
                for (int i = 0; i < source.Length; i++)
                {
                    char ch = source[i];
                    if (Regex.IsMatch(ch.ToString(), @"[\u4e00-\u9fa5]"))
                    {
                        str = str + @"\u" + ((int) source[i]).ToString("x");
                    }
                    else
                    {
                        str = str + source[i];
                    }
                }
            }
            return str;
        }
    }
}

