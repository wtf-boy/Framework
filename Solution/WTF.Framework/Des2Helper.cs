namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class Des2Helper
    {
        public static string DecryptDES2(this string source, string sKey)
        {
            byte[] buffer = Convert.FromBase64String(source);
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(sKey);
                provider.IV = Encoding.ASCII.GetBytes(sKey);
                MemoryStream stream = new MemoryStream();
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                    stream2.Close();
                }
                string str = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
                return str;
            }
        }

        public static string EncryptDES2(this string source, string _DESKey)
        {
            StringBuilder builder = new StringBuilder();
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(_DESKey);
                byte[] buffer2 = Encoding.ASCII.GetBytes(_DESKey);
                byte[] buffer = Encoding.UTF8.GetBytes(source);
                provider.Mode = CipherMode.CBC;
                provider.Key = bytes;
                provider.IV = buffer2;
                string str = "";
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        stream2.Write(buffer, 0, buffer.Length);
                        stream2.FlushFinalBlock();
                        str = Convert.ToBase64String(stream.ToArray());
                    }
                }
                return str;
            }
        }
    }
}

