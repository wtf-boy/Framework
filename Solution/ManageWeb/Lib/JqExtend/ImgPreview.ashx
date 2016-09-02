<%@ WebHandler Language="C#" Class="ImgPreview" %>

using System;
using System.Web;
using WTF.Framework;
public class ImgPreview : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        int width = context.Request["width"].IsNoNull() ? context.Request["width"].ConvertInt() : 0;
        int height = context.Request["height"].IsNoNull() ? context.Request["height"].ConvertInt() : 0;

        width = width > 0 ? width : 100;
        height = height > 0 ? height : 100;
        if (width == 0 || height == 0)
        {
            context.Response.Write("图片预览出现错误，请设置图片预览大小");
            return;
        }
        try
        {

            if (context.Request.Files.Count > 0)
            {

                HttpFileCollection filecollection = context.Request.Files;
                HttpPostedFile postedfile = filecollection[0];
                System.IO.Stream objStream = null;
                System.Drawing.Image objImage = postedfile.InputStream.StreamToImage();
                string fileExt = System.IO.Path.GetExtension(postedfile.FileName).Trim(new char[] { '.' });
                if (objImage.Width > width || objImage.Height > height)
                {
                    objImage = ImageHelper.CreateThumbnail(objImage, "<=" + width.ToString(), "<=" + height.ToString());
                    objStream = ImageHelper.ImageToStream(objImage, fileExt);
                }
                else
                {
                    objStream = postedfile.InputStream;
                }
                objImage.Dispose();
                byte[] resourceData = new System.IO.BinaryReader(objStream).ReadBytes((int)objStream.Length);

                context.Response.Write("data:image/" + fileExt + ";base64," + Convert.ToBase64String(resourceData));


            }

        }
        catch (Exception objExp)
        {

            if (objExp.GetType() == typeof(HttpException) && objExp.Message.IsMatch("超过了最大请求长度"))
            {
                context.Response.Write("选择的图片超过" + (ConfigHelper.GetIntValue("MaxFileSize", 1024) * 1024).RenderFileSize() + "因此无预览，同时也不支持上传");
                return;
            }
            else
            {
                context.Response.Write("图片预览出现错误，对不起暂时无法预览");
            }

        }

    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

}