namespace WTF.Framework
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class MachineKeyHelper
    {
        private static string CreateKey(int len)
        {
            byte[] data = new byte[len];
            new RNGCryptoServiceProvider().GetBytes(data);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(string.Format("{0:X2}", data[i]));
            }
            return builder.ToString();
        }

        public static string DecryptionKeySHA1(int len = 0x18)
        {
            return CreateKey(len);
        }

        public static string ValidationKeySHA1(int len = 20)
        {
            return CreateKey(len);
        }
    }
}

