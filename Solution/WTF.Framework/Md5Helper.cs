namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class Md5Helper
    {
        public static byte[] MD5Byte(this byte[] Bytes)
        {
            MD5 md = new MD5CryptoServiceProvider();
            return md.ComputeHash(Bytes);
        }

        public static byte[] MD5Byte(this string data, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(data);
            MD5 md = new MD5CryptoServiceProvider();
            return md.ComputeHash(bytes);
        }

        public static string MD5Encrypt(this byte[] Bytes)
        {
            if ((Bytes == null) || (Bytes.Length == 0))
            {
                return "";
            }
            byte[] buffer = Bytes.MD5Byte();
            string str = null;
            for (int i = 0; i < buffer.Length; i++)
            {
                string str2 = buffer[i].ToString("x");
                if (str2.Length == 1)
                {
                    str2 = "0" + str2;
                }
                str = str + str2;
            }
            return str.ToUpper();
        }

        public static string MD5Encrypt(this string data)
        {
            return data.MD5Encrypt("");
        }

        public static string MD5Encrypt(this string data, string encryptKey)
        {
            return data.MD5Encrypt(encryptKey, Encoding.UTF8);
        }

        public static string MD5Encrypt(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            string str = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                str = str + buffer[i].ToString("X2");
            }
            return str.ToUpper();
        }

        public static long MD5EncryptInt64(this string data)
        {
            return data.MD5EncryptInt64("");
        }

        public static long MD5EncryptInt64(this string data, string encryptKey)
        {
            return data.MD5EncryptInt64(encryptKey, Encoding.UTF8);
        }

        public static long MD5EncryptInt64(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            long num = BitConverter.ToInt64(buffer, 0);
            long num2 = BitConverter.ToInt64(buffer, 8);
            return (num ^ num2);
        }

        public static ulong MD5EncryptUInt64(this string data)
        {
            return data.MD5EncryptUInt64("");
        }

        public static ulong MD5EncryptUInt64(this string data, string encryptKey)
        {
            return data.MD5EncryptUInt64(encryptKey, Encoding.UTF8);
        }

        public static ulong MD5EncryptUInt64(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            ulong num = BitConverter.ToUInt64(buffer, 0);
            ulong num2 = BitConverter.ToUInt64(buffer, 8);
            return (num ^ num2);
        }

        public static bool SignatureCheckMD5(this string sourceData, string signatureMd5, string encryptKey = "")
        {
            if (string.IsNullOrWhiteSpace(encryptKey))
            {
                encryptKey = ConfigHelper.GetValue("Seven_Signature", "E36D03EA6B98B0C89C1E3C5F4A4F44CB33E26588");
            }
            return sourceData.SignatureMD5(encryptKey).ToLower().Equals(signatureMd5.ToLower());
        }

        public static string SignatureMD5(this string data, string encryptKey = "")
        {
            if (string.IsNullOrWhiteSpace(encryptKey))
            {
                encryptKey = ConfigHelper.GetValue("Seven_Signature", "E36D03EA6B98B0C89C1E3C5F4A4F44CB33E26588");
            }
            return data.ToLower().MD5Encrypt(encryptKey);
        }

        public static string GetSignatureDefaultKey
        {
            get
            {
                return ConfigHelper.GetValue("Seven_Signature", "E36D03EA6B98B0C89C1E3C5F4A4F44CB33E26588");
            }
        }
    }
}

