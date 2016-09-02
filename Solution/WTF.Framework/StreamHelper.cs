namespace WTF.Framework
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class StreamHelper
    {
        public static byte[] StreamResponseToBytes(this Stream stream)
        {
            int num;
            MemoryStream stream2 = new MemoryStream();
            byte[] buffer = new byte[0x10000];
            while ((num = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                stream2.Write(buffer, 0, num);
            }
            byte[] buffer2 = stream2.ToArray();
            stream2.Close();
            return buffer2;
        }

        public static byte[] StreamToBytes(this Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0L, SeekOrigin.Begin);
            return buffer;
        }

        public static Image StreamToImage(this Stream stream)
        {
            Image image = Image.FromStream(stream);
            stream.Seek(0L, SeekOrigin.Begin);
            return image;
        }

        public static string StreamToString(this Stream stream)
        {
            return stream.StreamToString(Encoding.UTF8);
        }

        public static string StreamToString(this Stream stream, Encoding encode)
        {
            string str;
            using (Stream stream2 = stream)
            {
                using (StreamReader reader = new StreamReader(stream2, encode))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }
    }
}

