namespace WTF.Framework
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Web.UI.WebControls;

    public static class ImageHelper
    {
        public static bool CheckIsImage(string sourceFile)
        {
            bool flag = false;
            string str = ".gif,.jpg,.png,.bmp,.jpeg,.tiff,.wmf,.emf";
            string str2 = sourceFile.GetFileExtension().ToLower();
            if (str.IndexOf(str2) >= 0)
            {
                flag = true;
            }
            if (flag)
            {
                System.Drawing.Image image = null;
                try
                {
                    image = System.Drawing.Image.FromFile(sourceFile);
                }
                catch
                {
                    flag = false;
                }
                finally
                {
                    image.Dispose();
                }
            }
            return flag;
        }

        public static void ConvertImageType(string OldImagePath, string UploadDirectory, string OldFileName, string ThumbnailImageType, out string NewFileName)
        {
            string str = OldFileName.GetFileExtension().ToLower();
            NewFileName = OldFileName.Substring(0, OldFileName.LastIndexOf(".")) + "." + ThumbnailImageType;
            System.Drawing.Image image = System.Drawing.Image.FromFile(OldImagePath + OldFileName);
            int width = image.Width;
            int height = image.Height;
            System.Drawing.Image original = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ImageHelper.ThumbnailCallback), IntPtr.Zero);
            Bitmap bitmap = new Bitmap(original);
            string filename = UploadDirectory + NewFileName;
            bitmap.Save(filename, GetImageFormat(ThumbnailImageType));
            image.Dispose();
            original.Dispose();
            bitmap.Dispose();
            System.IO.File.Delete(OldImagePath + OldFileName);
        }

        public static System.Drawing.Image CreateCutImage(this System.Drawing.Image original, int x, int y, int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentNullException("width", "缩略图宽度必须大于0");
            }
            if (height <= 0)
            {
                throw new ArgumentNullException("height", "缩略图高度必须大于0");
            }
            Bitmap image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(original, new Rectangle(0, 0, image.Width, image.Height), x, y, width, height, GraphicsUnit.Pixel);
            graphics.Dispose();
            return image;
        }

        public static System.Drawing.Image CreateThumbnail(this System.Drawing.Image original, int percentage)
        {
            if (percentage < 1)
            {
                throw new ArgumentNullException("percentage", "缩放比例不能小于1%");
            }
            return original.CreateThumbnail(((int) ((original.Width * 0.01f) * percentage)), ((int) ((original.Height * 0.01f) * percentage)));
        }

        public static System.Drawing.Image CreateThumbnail(this System.Drawing.Image original, int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentNullException("width", "缩略图宽度必须大于0");
            }
            if (height <= 0)
            {
                throw new ArgumentNullException("height", "缩略图高度必须大于0");
            }
            Bitmap image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(original, new Rectangle(0, 0, image.Width, image.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
            graphics.Dispose();
            return image;
        }

        public static System.Drawing.Image CreateThumbnail(this System.Drawing.Image original, string widthCondition, string heightCondition)
        {
            int width = 0;
            int num2 = 0;
            string str = "";
            int height = 0;
            int num4 = 0;
            string str2 = "";
            if (widthCondition.StartsWith("="))
            {
                str = "=";
                if (widthCondition.EndsWith("%"))
                {
                    num2 = (int) ((int.Parse(widthCondition.Substring(1).TrimEnd(new char[] { '%' })) * original.Width) * 0.01);
                }
                else
                {
                    num2 = int.Parse(widthCondition.Substring(1));
                }
            }
            else
            {
                str = "<=";
                if (widthCondition.EndsWith("%"))
                {
                    num2 = (int) ((int.Parse(widthCondition.Substring(2).TrimEnd(new char[] { '%' })) * original.Width) * 0.01);
                }
                else
                {
                    num2 = int.Parse(widthCondition.Substring(2));
                }
            }
            if (heightCondition.StartsWith("="))
            {
                str2 = "=";
                if (heightCondition.EndsWith("%"))
                {
                    num4 = (int) ((int.Parse(heightCondition.Substring(1).TrimEnd(new char[] { '%' })) * original.Height) * 0.01);
                }
                else
                {
                    num4 = int.Parse(heightCondition.Substring(1));
                }
            }
            else
            {
                str2 = "<=";
                if (heightCondition.EndsWith("%"))
                {
                    num4 = (int) ((int.Parse(heightCondition.Substring(2).TrimEnd(new char[] { '%' })) * original.Height) * 0.01);
                }
                else
                {
                    num4 = int.Parse(heightCondition.Substring(2));
                }
            }
            float num5 = ((float) num4) / ((float) original.Height);
            float num6 = ((float) num2) / ((float) original.Width);
            if ((str == "=") && (str2 == "="))
            {
                width = num2;
                height = num4;
            }
            else if (str == "=")
            {
                int num7 = (int) (original.Height * num6);
                width = num2;
                height = (num7 <= num4) ? num7 : num4;
            }
            else if (str2 == "=")
            {
                int num8 = (int) (original.Width * num5);
                width = (num8 <= num2) ? num8 : num2;
                height = num4;
            }
            else
            {
                if ((original.Width <= num2) && (original.Height <= num4))
                {
                    return original;
                }
                float num9 = (num6 >= num5) ? num5 : num6;
                width = (int) (original.Width * num9);
                height = (int) (original.Height * num9);
            }
            return original.CreateThumbnail(width, height);
        }

        public static string CreateThumbnailContentPath(this object OriginalContent, Size objSize, string urlPattern = "")
        {
            return OriginalContent.CreateThumbnailContentPath(objSize.Width, objSize.Height, urlPattern);
        }

        public static string CreateThumbnailContentPath(this object OriginalContent, int width, int height, string urlPattern = "")
        {
            if (width <= 0)
            {
                throw new ArgumentNullException("请设置缩略宽度");
            }
            if (height <= 0)
            {
                throw new ArgumentNullException("请设置缩略高度");
            }
            if (OriginalContent == null)
            {
                return "";
            }
            string str = OriginalContent.ToString();
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (!ConfigHelper.GetBoolTrueValue("Image_IsCreateThumbnail"))
                {
                    return str;
                }
                if (string.IsNullOrWhiteSpace(urlPattern))
                {
                    urlPattern = ConfigHelper.GetValue("Image_Thumbnail_Pattern", "");
                }
                if (string.IsNullOrWhiteSpace(urlPattern))
                {
                    return str;
                }
                MatchCollection matchs = new Regex("<img[\\s\\S]*?src=\"(?<imgSrc>[\\s\\S]*?)\"[\\s\\S]*?/>", RegexOptions.None).Matches(str);
                foreach (Match match in matchs)
                {
                    string str2 = match.Groups["imgSrc"].Value;
                    if (!string.IsNullOrWhiteSpace(str2) && str2.IsMatch(urlPattern))
                    {
                        string str3 = str2.CreateThumbnailItemPath(width, height);
                        if (!string.IsNullOrWhiteSpace(str3))
                        {
                            str = str.Replace(str2, str3);
                        }
                    }
                }
            }
            return str;
        }

        public static bool CreateThumbnailImage(string sourceImage, string targetImage, int targetWidth, int targetHeight)
        {
            bool flag = false;
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourceImage);
            if (targetHeight == 0)
            {
                targetHeight = int.Parse(Math.Floor(GetHeight(image, targetWidth)).ToString());
            }
            if (targetWidth == 0)
            {
                targetWidth = int.Parse(Math.Floor(GetWidth(image, targetHeight)).ToString());
            }
            Bitmap bitmap = new Bitmap(targetWidth, targetHeight);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(image, 0, 0, targetWidth, targetHeight);
            }
            try
            {
                bitmap.Save(targetImage, GetImageFormat(targetImage.GetFileExtension()));
                flag = true;
            }
            finally
            {
                image.Dispose();
                bitmap.Dispose();
            }
            return flag;
        }

        public static bool CreateThumbnailImage(Stream sourceImage, string targetImage, int targetWidth, int targetHeight, ImageFormat imageFormat)
        {
            bool flag = false;
            System.Drawing.Image image = System.Drawing.Image.FromStream(sourceImage);
            if ((image.Height < targetHeight) || (image.Width < targetWidth))
            {
                targetHeight = image.Height;
                targetWidth = image.Width;
            }
            if (targetHeight == 0)
            {
                targetHeight = int.Parse(Math.Floor(GetHeight(image, targetWidth)).ToString());
            }
            if (targetWidth == 0)
            {
                targetWidth = int.Parse(Math.Floor(GetWidth(image, targetHeight)).ToString());
            }
            Bitmap bitmap = new Bitmap(targetWidth, targetHeight);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(image, 0, 0, targetWidth, targetHeight);
            }
            try
            {
                bitmap.Save(targetImage, imageFormat);
                flag = true;
            }
            finally
            {
                image.Dispose();
                bitmap.Dispose();
            }
            return flag;
        }

        private static string CreateThumbnailItemPath(this string OriginalPath, int width, int height)
        {
            string str = OriginalPath.Substring(0, OriginalPath.LastIndexOf('.'));
            string str2 = OriginalPath.Substring(OriginalPath.LastIndexOf('.'));
            return string.Concat(new object[] { str, "_", width, "x", height, str2 });
        }

        public static string CreateThumbnailPath(this object OriginalPath, Size objSize, string urlPattern = "")
        {
            return OriginalPath.CreateThumbnailPath(objSize.Width, objSize.Height, urlPattern);
        }

        public static string CreateThumbnailPath(this object OriginalPath, int width, int height, string urlPattern = "")
        {
            if (width <= 0)
            {
                throw new ArgumentNullException("请设置缩略宽度");
            }
            if (height <= 0)
            {
                throw new ArgumentNullException("请设置缩略高度");
            }
            if (OriginalPath == null)
            {
                return "";
            }
            string str = OriginalPath.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            if (!ConfigHelper.GetBoolTrueValue("Image_IsCreateThumbnail"))
            {
                return str;
            }
            if (str.LastIndexOf('.') < 0)
            {
                return str;
            }
            if (string.IsNullOrWhiteSpace(urlPattern))
            {
                urlPattern = ConfigHelper.GetValue("Image_Thumbnail_Pattern", "");
            }
            if (string.IsNullOrWhiteSpace(urlPattern))
            {
                return str;
            }
            if (!str.IsMatch(urlPattern))
            {
                return str;
            }
            return str.CreateThumbnailItemPath(width, height);
        }

        public static System.Drawing.Image CreateWatermark(this System.Drawing.Image originalImg, System.Drawing.Image watermarkImg, int x, int y)
        {
            Graphics graphics = Graphics.FromImage(originalImg);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(watermarkImg, new Rectangle(x, y, watermarkImg.Width, watermarkImg.Height), 0, 0, watermarkImg.Width, watermarkImg.Height, GraphicsUnit.Pixel);
            graphics.Dispose();
            return originalImg;
        }

        public static System.Drawing.Image CreateWatermark(this System.Drawing.Image originalImg, System.Drawing.Image watermarkImg, HorizontalAlign align, VerticalAlign valign)
        {
            int x = 0;
            int y = 0;
            int num3 = originalImg.Width - watermarkImg.Width;
            int num4 = originalImg.Height - watermarkImg.Height;
            switch (align)
            {
                case HorizontalAlign.Left:
                    x = 5;
                    break;

                case HorizontalAlign.Center:
                    x = ((num3 / 2) <= 5) ? 5 : (num3 / 2);
                    break;

                case HorizontalAlign.Right:
                    x = (num3 <= 10) ? 5 : (num3 - 5);
                    break;

                default:
                    x = 5;
                    break;
            }
            switch (valign)
            {
                case VerticalAlign.Top:
                    y = 5;
                    break;

                case VerticalAlign.Middle:
                    y = ((num4 / 2) <= 5) ? 5 : (num4 / 2);
                    break;

                case VerticalAlign.Bottom:
                    y = (num4 <= 10) ? 5 : (num4 - 5);
                    break;

                default:
                    y = 5;
                    break;
            }
            return originalImg.CreateWatermark(watermarkImg, x, y);
        }

        public static System.Drawing.Image CreateWatermark(this System.Drawing.Image originalImg, string text, int x, int y)
        {
            Font font = new Font("Arial", 10f);
            Brush brush = new SolidBrush(Color.Black);
            Graphics graphics = Graphics.FromImage(originalImg);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawString(text, font, brush, (PointF) new Point(x, y));
            graphics.Dispose();
            font.Dispose();
            brush.Dispose();
            return originalImg;
        }

        public static System.Drawing.Image CreateWatermark(this System.Drawing.Image originalImg, string text, HorizontalAlign align, VerticalAlign valign)
        {
            int x = 0;
            int y = 0;
            Font font = new Font("Arial", 10f);
            int num3 = originalImg.Width - ((int) (font.SizeInPoints * text.Length));
            int num4 = originalImg.Height - ((int) (font.SizeInPoints * 2f));
            switch (align)
            {
                case HorizontalAlign.Left:
                    x = 5;
                    break;

                case HorizontalAlign.Center:
                    x = ((num3 / 2) <= 5) ? 5 : (num3 / 2);
                    break;

                case HorizontalAlign.Right:
                    x = (num3 <= 10) ? 5 : (num3 - 5);
                    break;

                default:
                    x = 5;
                    break;
            }
            switch (valign)
            {
                case VerticalAlign.Top:
                    y = 5;
                    break;

                case VerticalAlign.Middle:
                    y = ((num4 / 2) <= 5) ? 5 : (num4 / 2);
                    break;

                case VerticalAlign.Bottom:
                    y = (num4 <= 10) ? 5 : (num4 - 5);
                    break;

                default:
                    y = 5;
                    break;
            }
            font.Dispose();
            return originalImg.CreateWatermark(text, x, y);
        }

        public static byte[] GetFile(string url)
        {
            WebResponse response = null;
            byte[] buffer = null;
            try
            {
                Uri requestUri = new Uri(url);
                WebRequest request = WebRequest.Create(requestUri);
                if (request != null)
                {
                    response = request.GetResponse();
                    if (response == null)
                    {
                        return buffer;
                    }
                    Stream responseStream = response.GetResponseStream();
                    using (MemoryStream stream2 = new MemoryStream())
                    {
                        int num = 0;
                        if (responseStream == null)
                        {
                            return buffer;
                        }
                        while ((num = responseStream.ReadByte()) != -1)
                        {
                            stream2.WriteByte((byte) num);
                        }
                        return stream2.ToArray();
                    }
                }
                return buffer;
            }
            catch
            {
                buffer = null;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return buffer;
        }

        private static double GetHeight(System.Drawing.Image image, int width)
        {
            double num = image.Width;
            double height = image.Height;
            return ((Convert.ToDouble(width) / num) * height);
        }

        private static ImageFormat GetImageFormat(string ExtName)
        {
            switch (ExtName)
            {
                case "bmp":
                    return ImageFormat.Bmp;

                case "emf":
                    return ImageFormat.Emf;

                case "exif":
                    return ImageFormat.Exif;

                case "gif":
                    return ImageFormat.Gif;

                case "icon":
                    return ImageFormat.Icon;

                case "jpg":
                    return ImageFormat.Jpeg;

                case "memorybmp":
                    return ImageFormat.MemoryBmp;

                case "png":
                    return ImageFormat.Png;

                case "tiff":
                    return ImageFormat.Tiff;

                case "wmf":
                    return ImageFormat.Wmf;
            }
            return ImageFormat.Jpeg;
        }

        private static double GetWidth(System.Drawing.Image image, int height)
        {
            double width = image.Width;
            double num2 = image.Height;
            return ((Convert.ToDouble(height) / num2) * width);
        }

        public static string ImageContentTypeToFileExtension(this string ContentType)
        {
            if (string.IsNullOrWhiteSpace(ContentType))
            {
                throw new ArgumentNullException("ContentType");
            }
            if (ContentType.ToString() != "image/jpeg")
            {
                if (ContentType.ToString() == "image/png")
                {
                    return ".png";
                }
                if (ContentType.ToString() == "image/gif")
                {
                    return ".gif";
                }
            }
            return ".jpg";
        }

        public static Stream ImageToStream(this System.Drawing.Image image)
        {
            Stream stream = new MemoryStream();
            new Bitmap(image).Save(stream, ImageFormat.Jpeg);
            stream.Position = 0L;
            return stream;
        }

        public static Stream ImageToStream(this System.Drawing.Image image, string fileExtension)
        {
            Stream stream = new MemoryStream();
            if (fileExtension.ToLower().IndexOf("png") >= 0)
            {
                new Bitmap(image).Save(stream, ImageFormat.Png);
            }
            else if ((fileExtension.ToLower().IndexOf("jpeg") >= 0) || (fileExtension.ToLower().IndexOf("jpg") >= 0))
            {
                new Bitmap(image).Save(stream, ImageFormat.Jpeg);
            }
            else if (fileExtension.ToLower().IndexOf("icon") >= 0)
            {
                new Bitmap(image).Save(stream, ImageFormat.Icon);
            }
            else if (fileExtension.ToLower().IndexOf("bmp") >= 0)
            {
                new Bitmap(image).Save(stream, ImageFormat.Bmp);
            }
            else if (fileExtension.ToLower().IndexOf("gif") >= 0)
            {
                new Bitmap(image).Save(stream, ImageFormat.Gif);
            }
            else
            {
                new Bitmap(image).Save(stream, ImageFormat.Jpeg);
            }
            stream.Position = 0L;
            return stream;
        }

        public static bool MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, int targetRate, MakeThumbnailMode mode, ref byte[] retBytes)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            if (((num > num5) || (num2 > num6)) && ((mode == MakeThumbnailMode.Width) || (mode == MakeThumbnailMode.Height)))
            {
                num = num5;
                num2 = num6;
                mode = MakeThumbnailMode.Solid;
            }
            switch (mode)
            {
                case MakeThumbnailMode.Liquid:
                    if ((((double) image.Width) / ((double) image.Height)) > (((double) num) / ((double) num2)))
                    {
                        num2 = (num * image.Height) / image.Width;
                    }
                    else
                    {
                        num = (num2 * image.Width) / image.Height;
                    }
                    break;

                case MakeThumbnailMode.Width:
                    num2 = (image.Height * width) / image.Width;
                    break;

                case MakeThumbnailMode.Height:
                    num = (image.Width * height) / image.Height;
                    break;

                case MakeThumbnailMode.Cut:
                    if ((((double) image.Width) / ((double) image.Height)) >= (((double) num) / ((double) num2)))
                    {
                        num5 = (num6 * num) / num2;
                        y = 0;
                        x = (image.Width - num5) / 2;
                        break;
                    }
                    num6 = (num5 * num2) / num;
                    x = 0;
                    y = (image.Height - num6) / 2;
                    break;

                case MakeThumbnailMode.Part:
                    if ((((double) image.Width) / ((double) image.Height)) <= (((double) num) / ((double) num2)))
                    {
                        num6 = num2;
                        num5 = (num6 * image.Width) / image.Height;
                        y = 0;
                        x = (num - num5) / 2;
                        break;
                    }
                    num5 = num;
                    num6 = (num5 * image.Height) / image.Width;
                    x = 0;
                    y = (num2 - num6) / 2;
                    break;

                case MakeThumbnailMode.Contain:
                    if ((((double) image.Width) / ((double) image.Height)) <= (((double) num) / ((double) num2)))
                    {
                        num6 = image.Height;
                        num5 = (num6 * num) / num2;
                        y = 0;
                        x = (image.Width - num5) / 2;
                        break;
                    }
                    num5 = image.Width;
                    num6 = (num5 * num2) / num;
                    x = 0;
                    y = (image.Height - num6) / 2;
                    break;

                case MakeThumbnailMode.Rate:
                    height = (image.Height * targetRate) / 100;
                    width = (image.Width * targetRate) / 100;
                    num = (image.Width * height) / image.Height;
                    num2 = (image.Height * width) / image.Width;
                    break;
            }
            System.Drawing.Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            try
            {
                if (!string.IsNullOrEmpty(thumbnailPath))
                {
                    image2.Save(thumbnailPath, ImageFormat.Jpeg);
                }
                MemoryStream stream = new MemoryStream();
                image2.Save(stream, ImageFormat.Jpeg);
                retBytes = stream.GetBuffer();
                stream.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
            return true;
        }

        public static string Resize(string localFile, string templateWidth, string templateHeight, string x, string y, string fileSaveUrl)
        {
            FileStream stream = null;
            try
            {
                stream = System.IO.File.Open(localFile, FileMode.Open);
            }
            catch
            {
                return "读取图像错误";
            }
            if (stream == null)
            {
                return "读取图像错误";
            }
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream, true);
            System.Drawing.Image image2 = null;
            Graphics graphics = null;
            Rectangle srcRect = new Rectangle(0, 0, 0, 0);
            Rectangle destRect = new Rectangle(0, 0, 0, 0);
            image2 = new Bitmap(image.Width, image.Height);
            graphics = Graphics.FromImage(image2);
            srcRect.X = int.Parse(x);
            srcRect.Y = int.Parse(y);
            srcRect.Width = int.Parse(templateWidth);
            srcRect.Height = int.Parse(templateHeight);
            destRect.X = 0;
            destRect.Y = 0;
            destRect.Width = int.Parse(templateWidth);
            destRect.Height = int.Parse(templateHeight);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
            System.Drawing.Image image3 = new Bitmap(int.Parse(templateWidth), int.Parse(templateHeight));
            Graphics graphics2 = Graphics.FromImage(image3);
            graphics2.InterpolationMode = InterpolationMode.High;
            graphics2.SmoothingMode = SmoothingMode.HighQuality;
            graphics2.Clear(Color.White);
            graphics2.DrawImage(image2, new Rectangle(0, 0, int.Parse(templateWidth), int.Parse(templateHeight)), new Rectangle(0, 0, int.Parse(templateWidth), int.Parse(templateHeight)), GraphicsUnit.Pixel);
            image3.Save(fileSaveUrl, ImageFormat.Jpeg);
            try
            {
                graphics2.Dispose();
                image3.Dispose();
                graphics.Dispose();
                image2.Dispose();
                stream.Dispose();
            }
            catch
            {
                return "";
            }
            return "";
        }

        public static void SavePhotoFromUrl(string fullName, string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                using (Bitmap bitmap = new Bitmap(request.GetResponse().GetResponseStream()))
                {
                    bitmap.Save(fullName);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool SmallPic(string strOldPic, string strNewPic, int intWidth, int intHeight)
        {
            Bitmap bitmap;
            try
            {
                bitmap = new Bitmap(strOldPic);
                new Bitmap(bitmap, intWidth, intHeight).Save(strNewPic);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                bitmap = null;
            }
            return true;
        }

        public static bool SmallPic(string originalImagePath, string thumbnailPath, int width, int height, int targetRate, MakeThumbnailMode mode, ref byte[] retBytes, ImageFormat format)
        {
            Bitmap bitmap;
            Bitmap bitmap2;
            System.Drawing.Image image = System.Drawing.Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            int num3 = 0;
            int num4 = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            if (((num > num5) || (num2 > num6)) && ((mode == MakeThumbnailMode.Width) || (mode == MakeThumbnailMode.Height)))
            {
                num = num5;
                num2 = num6;
                mode = MakeThumbnailMode.Solid;
            }
            switch (mode)
            {
                case MakeThumbnailMode.Liquid:
                    if ((((double) image.Width) / ((double) image.Height)) > (((double) num) / ((double) num2)))
                    {
                        num2 = (num * image.Height) / image.Width;
                    }
                    else
                    {
                        num = (num2 * image.Width) / image.Height;
                    }
                    break;

                case MakeThumbnailMode.Width:
                    num2 = (image.Height * width) / image.Width;
                    break;

                case MakeThumbnailMode.Height:
                    num = (image.Width * height) / image.Height;
                    break;

                case MakeThumbnailMode.Cut:
                    if ((((double) image.Width) / ((double) image.Height)) >= (((double) num) / ((double) num2)))
                    {
                        num5 = (num6 * num) / num2;
                        num4 = 0;
                        num3 = (image.Width - num5) / 2;
                        break;
                    }
                    num6 = (num5 * num2) / num;
                    num3 = 0;
                    num4 = (image.Height - num6) / 2;
                    break;

                case MakeThumbnailMode.Part:
                    if ((((double) image.Width) / ((double) image.Height)) <= (((double) num) / ((double) num2)))
                    {
                        num6 = num2;
                        num5 = (num6 * image.Width) / image.Height;
                        num4 = 0;
                        num3 = (num - num5) / 2;
                        break;
                    }
                    num5 = num;
                    num6 = (num5 * image.Height) / image.Width;
                    num3 = 0;
                    num4 = (num2 - num6) / 2;
                    break;

                case MakeThumbnailMode.Contain:
                    if ((((double) image.Width) / ((double) image.Height)) <= (((double) num) / ((double) num2)))
                    {
                        num5 = (image.Height * num) / num2;
                        num4 = 0;
                        num3 = (image.Width - num5) / 2;
                        break;
                    }
                    num6 = (image.Width * num2) / num;
                    num3 = 0;
                    num4 = (image.Height - num6) / 2;
                    break;

                case MakeThumbnailMode.Rate:
                    height = (image.Height * targetRate) / 100;
                    width = (image.Width * targetRate) / 100;
                    num = (image.Width * height) / image.Height;
                    num2 = (image.Height * width) / image.Width;
                    break;
            }
            try
            {
                bitmap = new Bitmap(originalImagePath);
                bitmap2 = new Bitmap(bitmap, num, num2);
                if (!string.IsNullOrEmpty(thumbnailPath))
                {
                    bitmap2.Save(thumbnailPath);
                }
                MemoryStream stream = new MemoryStream();
                bitmap2.Save(stream, format);
                retBytes = stream.GetBuffer();
                stream.Close();
                bitmap.Dispose();
                bitmap2.Dispose();
            }
            catch (Exception exception)
            {
                if (image != null)
                {
                    image.Dispose();
                }
                bitmap = null;
                bitmap2 = null;
                throw exception;
            }
            finally
            {
                image.Dispose();
                bitmap = null;
                bitmap2 = null;
            }
            return true;
        }

        private static bool ThumbnailCallback()
        {
            return true;
        }

        public static bool WatermarkByImage(string sourceImage, string watermarkImage, string targetImage)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourceImage);
            System.Drawing.Image image2 = System.Drawing.Image.FromFile(watermarkImage);
            try
            {
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImage(image2, new Rectangle(image.Width - image2.Width, image.Height - image2.Height, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);
                graphics.Dispose();
            }
            catch
            {
            }
            image.Save(targetImage);
            image.Dispose();
            return true;
        }

        public static bool WatermarkByImage(string sourceImage, string watermarkImage, string targetImage, WaterImagePosition position)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourceImage);
            System.Drawing.Image image2 = System.Drawing.Image.FromFile(watermarkImage);
            int x = 0;
            int y = 0;
            switch (position)
            {
                case WaterImagePosition.LeftUp:
                    x = 0;
                    y = 0;
                    break;

                case WaterImagePosition.LeftDown:
                    x = 0;
                    y = image.Height - image2.Height;
                    break;

                case WaterImagePosition.RightUp:
                    x = image.Width - image2.Width;
                    y = 0;
                    break;

                case WaterImagePosition.RightDown:
                    x = image.Width - image2.Width;
                    y = image.Height - image2.Height;
                    break;

                case WaterImagePosition.MiddleUp:
                    x = (image.Width - image2.Width) / 2;
                    y = 0;
                    break;

                case WaterImagePosition.MiddleDown:
                    x = (image.Width - image2.Width) / 2;
                    y = image.Height - image2.Height;
                    break;

                case WaterImagePosition.Middle:
                    x = (image.Width - image2.Width) / 2;
                    y = (image.Height - image2.Height) / 2;
                    break;

                default:
                    x = image.Width - image2.Width;
                    y = image.Height - image2.Height;
                    break;
            }
            try
            {
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImage(image2, new Rectangle(x, y, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);
                graphics.Dispose();
            }
            catch
            {
            }
            image.Save(targetImage);
            image.Dispose();
            return true;
        }

        public static bool WatermarkByText(string sourceImage, string targetImage, string font, string text, Color color, int size, int top, int left)
        {
            Font font2;
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourceImage);
            if (font.Length > 0)
            {
                font2 = new Font(font, (float) size);
            }
            else
            {
                font2 = new Font("宋体", (float) size);
            }
            Brush brush = new SolidBrush(color);
            try
            {
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                graphics.DrawString(text, font2, brush, (float) (image.Width - left), (float) (image.Height - top));
                graphics.Dispose();
            }
            catch
            {
            }
            image.Save(targetImage);
            image.Dispose();
            return true;
        }

        public enum MakeThumbnailMode
        {
            Contain = 7,
            Cut = 5,
            Height = 4,
            Liquid = 2,
            Part = 6,
            Rate = 8,
            Solid = 1,
            Width = 3
        }

        public enum WaterImagePosition
        {
            LeftDown = 2,
            LeftUp = 1,
            Middle = 7,
            MiddleDown = 6,
            MiddleUp = 5,
            RightDown = 4,
            RightUp = 3
        }
    }
}

