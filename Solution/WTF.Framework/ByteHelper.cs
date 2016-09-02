namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public static class ByteHelper
    {
        private static char[] hexDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        public static Stream BytesToStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static string ConvertHexString(this byte[] bytes)
        {
            if (bytes == null)
            {
                return "";
            }
            char[] chArray = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int num2 = bytes[i];
                chArray[i * 2] = hexDigits[num2 >> 4];
                chArray[(i * 2) + 1] = hexDigits[num2 & 15];
            }
            return new string(chArray);
        }
    }
}

