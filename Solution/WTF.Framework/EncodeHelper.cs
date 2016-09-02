namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncodeHelper
    {
        public static string DecodeBase64(this string str)
        {
            return str.DecodeBase64(Encoding.UTF8);
        }

        public static string DecodeBase64(this string str, Encoding encoding)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return encoding.GetString(bytes);
        }

        public static byte[] DecodeBase64Byte(this byte[] source)
        {
            byte[] buffer2;
            if ((source == null) || (source.Length == 0))
            {
                throw new ArgumentException("source is not valid");
            }
            FromBase64Transform transform = new FromBase64Transform();
            MemoryStream stream = new MemoryStream();
            int inputOffset = 0;
            try
            {
                byte[] buffer;
                while ((inputOffset + 4) < source.Length)
                {
                    buffer = transform.TransformFinalBlock(source, inputOffset, 4);
                    stream.Write(buffer, 0, buffer.Length);
                    inputOffset += 4;
                }
                buffer = transform.TransformFinalBlock(source, inputOffset, source.Length - inputOffset);
                stream.Write(buffer, 0, buffer.Length);
                buffer2 = stream.ToArray();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return buffer2;
        }

        public static byte[] DecodeBase64Byte(this string source)
        {
            return source.DecodeBase64Byte(Encoding.UTF8);
        }

        public static byte[] DecodeBase64Byte(this string source, Encoding encoding)
        {
            return encoding.GetBytes(source).DecodeBase64Byte();
        }

        public static string DecodeEnhancedBase64(this string source)
        {
            return source.DecodeEnhancedBase64(Encoding.UTF8);
        }

        public static string DecodeEnhancedBase64(this string source, Encoding objEncoding)
        {
            return objEncoding.GetString(source.DecodeEnhancedBase64Byte());
        }

        public static byte[] DecodeEnhancedBase64Byte(this string source)
        {
            string s = source.Replace("~", "+").Replace("@", "/").Replace("$", "=");
            return Encoding.UTF8.GetBytes(s).DecodeBase64Byte();
        }

        public static string DecodeInterfaceBase64(this string source)
        {
            return source.DecodeInterfaceBase64(Encoding.UTF8);
        }

        public static string DecodeInterfaceBase64(this string source, Encoding objEncoding)
        {
            return objEncoding.GetString(Encoding.UTF8.GetBytes(source).DecodeBase64Byte());
        }

        public static string EncodeBase64(this byte[] source)
        {
            return source.EncodeBase64(Encoding.UTF8);
        }

        public static string EncodeBase64(this string str)
        {
            return str.EncodeBase64(Encoding.UTF8);
        }

        public static string EncodeBase64(this byte[] source, Encoding encoding)
        {
            byte[] bytes = source.EncodeBase64Byte();
            return encoding.GetString(bytes);
        }

        public static string EncodeBase64(this string str, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(str));
        }

        public static byte[] EncodeBase64Byte(this byte[] source)
        {
            byte[] buffer2;
            if ((source == null) || (source.Length == 0))
            {
                throw new ArgumentException("source is not valid");
            }
            ToBase64Transform transform = new ToBase64Transform();
            MemoryStream stream = new MemoryStream();
            int inputOffset = 0;
            try
            {
                byte[] buffer;
                while ((inputOffset + 3) < source.Length)
                {
                    buffer = transform.TransformFinalBlock(source, inputOffset, 3);
                    stream.Write(buffer, 0, buffer.Length);
                    inputOffset += 3;
                }
                buffer = transform.TransformFinalBlock(source, inputOffset, source.Length - inputOffset);
                stream.Write(buffer, 0, buffer.Length);
                buffer2 = stream.ToArray();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return buffer2;
        }

        public static string EncodeEnhancedBase64(this byte[] source)
        {
            byte[] bytes = source.EncodeBase64Byte();
            return Encoding.UTF8.GetString(bytes).Replace("+", "~").Replace("/", "@").Replace("=", "$");
        }

        public static string EncodeEnhancedBase64(this string source)
        {
            return source.EncodeEnhancedBase64(Encoding.UTF8);
        }

        public static string EncodeEnhancedBase64(this string source, Encoding objEncoding)
        {
            return objEncoding.GetBytes(source).EncodeEnhancedBase64();
        }

        public static string EncodeInterfaceBase64(this string source)
        {
            return source.EncodeInterfaceBase64(Encoding.UTF8);
        }

        public static string EncodeInterfaceBase64(this string source, Encoding objEncoding)
        {
            byte[] bytes = objEncoding.GetBytes(source);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}

