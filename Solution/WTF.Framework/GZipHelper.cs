namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class GZipHelper
    {
        public static Stream CompressGZip(this Stream objStream)
        {
            MemoryStream stream = new MemoryStream();
            return new GZipStream(objStream, CompressionMode.Compress);
        }

        public static byte[] CompressGZip(this byte[] array)
        {
            MemoryStream stream = new MemoryStream();
            GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress, true);
            stream2.Write(array, 0, array.Length);
            stream2.Close();
            return stream.ToArray();
        }

        public static string CompressGZipBase64(this string value)
        {
            return value.CompressGZipBase64(Encoding.UTF8);
        }

        public static string CompressGZipBase64(this string value, Encoding objEncoding)
        {
            if (string.IsNullOrEmpty(value) || (value.Length == 0))
            {
                return "";
            }
            return Convert.ToBase64String(objEncoding.GetBytes(value.ToString()).CompressGZip());
        }

        public static byte[] CompressGZipByte(this string value)
        {
            return value.CompressGZipByte(Encoding.UTF8);
        }

        public static byte[] CompressGZipByte(this string value, Encoding objEncoding)
        {
            if (string.IsNullOrEmpty(value) || (value.Length == 0))
            {
                return new byte[0];
            }
            return objEncoding.GetBytes(value.ToString()).CompressGZip();
        }

        public static Stream CompressGZipStream(this string value)
        {
            return value.CompressGZipStream(Encoding.UTF8);
        }

        public static Stream CompressGZipStream(this string value, Encoding objEncoding)
        {
            return value.CompressGZipByte(objEncoding).BytesToStream();
        }

        public static Stream DecompressGZip(this Stream objStream)
        {
            MemoryStream stream = new MemoryStream();
            return new GZipStream(objStream, CompressionMode.Decompress);
        }

        public static byte[] DecompressGZip(this byte[] array)
        {
            MemoryStream stream = new MemoryStream(array);
            GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress);
            MemoryStream stream3 = new MemoryStream();
            byte[] buffer = new byte[0x400];
            while (true)
            {
                int count = stream2.Read(buffer, 0, buffer.Length);
                if (count <= 0)
                {
                    stream2.Close();
                    return stream3.ToArray();
                }
                stream3.Write(buffer, 0, count);
            }
        }

        public static string DecompressGZipBase64(this string value)
        {
            return value.DecompressGZipBase64(Encoding.UTF8);
        }

        public static string DecompressGZipBase64(this string value, Encoding objEncoding)
        {
            if (string.IsNullOrEmpty(value) || (value.Length == 0))
            {
                return "";
            }
            byte[] array = Convert.FromBase64String(value.ToString());
            return objEncoding.GetString(array.DecompressGZip());
        }

        public static byte[] DecompressGZipByte(this Stream value)
        {
            return value.DecompressGZip().StreamResponseToBytes();
        }

        public static string DecompressGZipString(this Stream value)
        {
            return value.DecompressGZipString(Encoding.UTF8);
        }

        public static string DecompressGZipString(this Stream value, Encoding objEncoding)
        {
            return objEncoding.GetString(value.DecompressGZip().StreamResponseToBytes());
        }
    }
}

