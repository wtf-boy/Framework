namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ShortCodeHelper
    {
        public static string[] ShortCode(this string value, string addKey = "")
        {
            string[] strArray = new string[] { 
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", 
                "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", 
                "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
                "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
             };
            string str = value.MD5Encrypt(addKey);
            string[] strArray2 = new string[4];
            for (int i = 0; i < 4; i++)
            {
                long num2 = 0x3fffffffL & Convert.ToInt64("0x" + str.Substring(i * 8, 8), 0x10);
                string str2 = string.Empty;
                for (int j = 0; j < 6; j++)
                {
                    long num4 = 0x3dL & num2;
                    str2 = str2 + strArray[(int) ((IntPtr) num4)];
                    num2 = num2 >> 5;
                }
                strArray2[i] = str2;
            }
            return strArray2;
        }

        public static string ShortCodeIndex(this string value, int getIndex = 0, string addKey = "")
        {
            if (getIndex < 0)
            {
                getIndex = 0;
            }
            else if (getIndex >= 4)
            {
                getIndex = 3;
            }
            return value.ShortCode(addKey)[getIndex];
        }

        public static string ShortCodeRand(this string value, string addKey = "")
        {
            return value.ShortCodeIndex(RandomHelper.GetRandomNumber(0, 4), addKey);
        }
    }
}

