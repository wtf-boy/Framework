using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
namespace WTF.Framework
{
    public static class AESCryptographyHelper
    {
        public static string DecryptAES(this string toDecrypt, string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBuffer = Convert.FromBase64String(toDecrypt);
            RijndaelManaged managed = new RijndaelManaged {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] buffer3 = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(buffer3);
        }

        public static string DecryptAES(this string text, string password, string iv)
        {
            RijndaelManaged managed = new RijndaelManaged {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 0x80,
                BlockSize = 0x80
            };
            byte[] inputBuffer = Convert.FromBase64String(text);
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] destinationArray = new byte[0x10];
            int length = bytes.Length;
            if (length > destinationArray.Length)
            {
                length = destinationArray.Length;
            }
            Array.Copy(bytes, destinationArray, length);
            managed.Key = destinationArray;
            byte[] buffer4 = Encoding.UTF8.GetBytes(iv);
            managed.IV = buffer4;
            byte[] buffer5 = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(buffer5);
        }

        public static byte[] DecryptAESByte(byte[] toArray, string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            RijndaelManaged managed = new RijndaelManaged {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            return managed.CreateDecryptor().TransformFinalBlock(toArray, 0, toArray.Length);
        }

        public static byte[] DecryptAESByte(byte[] Data, string Key, string Vector)
        {
            byte[] destinationArray = new byte[0x20];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(destinationArray.Length)), destinationArray, destinationArray.Length);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(buffer2.Length)), buffer2, buffer2.Length);
            byte[] buffer3 = null;
            Rijndael rijndael = Rijndael.Create();
            try
            {
                using (MemoryStream stream = new MemoryStream(Data))
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, rijndael.CreateDecryptor(destinationArray, buffer2), CryptoStreamMode.Read))
                    {
                        using (MemoryStream stream3 = new MemoryStream())
                        {
                            byte[] buffer = new byte[0x400];
                            int count = 0;
                            while ((count = stream2.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream3.Write(buffer, 0, count);
                            }
                            buffer3 = stream3.ToArray();
                        }
                    }
                }
            }
            catch
            {
                buffer3 = null;
            }
            rijndael.Clear();
            return buffer3;
        }

        public static string EncryptAES(this string toEncrypt, string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBuffer = Encoding.UTF8.GetBytes(toEncrypt);
            RijndaelManaged managed = new RijndaelManaged {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] inArray = managed.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public static string EncryptAES(this string text, string password, string iv)
        {
            RijndaelManaged managed = new RijndaelManaged {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 0x80,
                BlockSize = 0x80
            };
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] destinationArray = new byte[0x10];
            int length = bytes.Length;
            if (length > destinationArray.Length)
            {
                length = destinationArray.Length;
            }
            Array.Copy(bytes, destinationArray, length);
            managed.Key = destinationArray;
            byte[] buffer3 = Encoding.UTF8.GetBytes(iv);
            managed.IV = buffer3;
            ICryptoTransform transform = managed.CreateEncryptor();
            byte[] inputBuffer = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
        }

        public static byte[] EncryptAESByte(this string toArray, string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBuffer = Encoding.UTF8.GetBytes(toArray);
            RijndaelManaged managed = new RijndaelManaged {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            return managed.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
        }

        public static byte[] EncryptAESByte(this byte[] Data, string Key, string Vector)
        {
            byte[] destinationArray = new byte[0x20];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(destinationArray.Length)), destinationArray, destinationArray.Length);
            byte[] buffer2 = new byte[0x10];
            Array.Copy(Encoding.UTF8.GetBytes(Vector.PadRight(buffer2.Length)), buffer2, buffer2.Length);
            byte[] buffer3 = null;
            Rijndael rijndael = Rijndael.Create();
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream stream2 = new CryptoStream(stream, rijndael.CreateEncryptor(destinationArray, buffer2), CryptoStreamMode.Write))
                    {
                        stream2.Write(Data, 0, Data.Length);
                        stream2.FlushFinalBlock();
                        buffer3 = stream.ToArray();
                    }
                }
            }
            catch
            {
                buffer3 = null;
            }
            rijndael.Clear();
            return buffer3;
        }

        public static string GetIv(int n)
        {
            char[] chArray = new char[] { 
                'a', 'b', 'd', 'c', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 
                'q', 's', 't', 'u', 'v', 'w', 'z', 'y', 'x', '0', '1', '2', '3', '4', '5', '6', 
                '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 
                'N', 'Q', 'P', 'R', 'T', 'S', 'V', 'U', 'W', 'X', 'Y', 'Z'
             };
            StringBuilder builder = new StringBuilder();
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < n; i++)
            {
                builder.Append(chArray[random.Next(0, chArray.Length)].ToString());
            }
            return builder.ToString();
        }
    }
}

