namespace WTF.Controls
{
    using FastDFS.Client;
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Resource;
    using WTF.Resource.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.UI.WebControls;

    public static class HttpPostedFileHelper
    {
        private static string[] imageType = new string[] { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };

        public static bool CheckIsImage(string fileExtension)
        {
            return (Array.IndexOf<string>(imageType, fileExtension) >= 0);
        }

        public static List<ResourceInfo> DownUrlListSaveResource(this string url, string ResourceCode, string RestrictCode)
        {
            string str2;
            string urlFileName = url.GetUrlFileName();
            if (urlFileName.IndexOf('.') >= 0)
            {
                str2 = urlFileName.Substring(urlFileName.LastIndexOf('.'));
                return url.GetResponseByte().SaveResourceList((Guid.NewGuid().ToString() + str2), ResourceCode, RestrictCode);
            }
            string contentType = "";
            byte[] responseByte = url.GetResponseByte(out contentType);
            str2 = contentType.ImageContentTypeToFileExtension();
            return responseByte.SaveResourceList((Guid.NewGuid().ToString() + str2), ResourceCode, RestrictCode);
        }

        public static ResourceInfo DownUrlSaveResource(this string url, string ResourceCode, string RestrictCode)
        {
            return url.DownUrlListSaveResource(ResourceCode, RestrictCode)[0];
        }

        public static ResourceInfo DownUrlSaveResourceByContentType(this string url, string ResourceCode, string RestrictCode)
        {
            return url.DownUrlSaveResourceListByContentType(ResourceCode, RestrictCode)[0];
        }

        public static List<ResourceInfo> DownUrlSaveResourceListByContentType(this string url, string ResourceCode, string RestrictCode)
        {
            string contentType = "";
            byte[] responseByte = url.GetResponseByte(out contentType);
            string str2 = contentType.ImageContentTypeToFileExtension();
            return responseByte.SaveResourceList((Guid.NewGuid().ToString() + str2), ResourceCode, RestrictCode);
        }

        private static System.Drawing.Image ProcessRestrictPic(resource_filerestrictpic objFileRestrictpic, System.Drawing.Image objImage)
        {
            if (((objFileRestrictpic.ImageWidth > 0) && (objFileRestrictpic.ImageHeight > 0)) && ((objImage.Width > objFileRestrictpic.ImageWidth) || (objImage.Height > objFileRestrictpic.ImageHeight)))
            {
                objImage = objImage.CreateThumbnail("<=" + objFileRestrictpic.ImageWidth.ToString(), "<=" + objFileRestrictpic.ImageHeight.ToString());
            }
            if (objFileRestrictpic.IsCreateWaterMark)
            {
                if (objFileRestrictpic.WatermarkType == 2)
                {
                    SysAssert.CheckCondition(string.IsNullOrEmpty(objFileRestrictpic.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                    objImage = objImage.CreateWatermark(objFileRestrictpic.WatermarkText, (HorizontalAlign) objFileRestrictpic.HorizontalAlign, (VerticalAlign) objFileRestrictpic.VerticalAlign);
                    return objImage;
                }
                ResourceRule rule = new ResourceRule();
                Sys_WaterImage instance = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileRestrictpic.WaterImageID);
                if (!instance.IsNoNull())
                {
                    return objImage;
                }
                string waterImagePath = "";
                if (File.Exists(instance.WaterImagePath))
                {
                    waterImagePath = instance.WaterImagePath;
                }
                else if ((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Server != null))
                {
                    waterImagePath = SysVariable.CurrentContext.Server.MapPath(instance.WaterImagePath);
                }
                if (!string.IsNullOrWhiteSpace(waterImagePath))
                {
                    System.Drawing.Image watermarkImg = System.Drawing.Image.FromFile(waterImagePath, false);
                    objImage = objImage.CreateWatermark(watermarkImg, (HorizontalAlign) objFileRestrictpic.HorizontalAlign, (VerticalAlign) objFileRestrictpic.VerticalAlign);
                    watermarkImg.Dispose();
                }
            }
            return objImage;
        }

        private static System.Drawing.Image ProcessRestrictPic(resource_filerestrictpic objFileRestrictpic, System.Drawing.Image objImage, bool IsCreateWaterMark, HorizontalAlign objHorizontalAlign, VerticalAlign objVerticalAlign)
        {
            if (((objFileRestrictpic.ImageWidth > 0) && (objFileRestrictpic.ImageHeight > 0)) && ((objImage.Width > objFileRestrictpic.ImageWidth) || (objImage.Height > objFileRestrictpic.ImageHeight)))
            {
                objImage = objImage.CreateThumbnail("<=" + objFileRestrictpic.ImageWidth.ToString(), "<=" + objFileRestrictpic.ImageHeight.ToString());
            }
            if (IsCreateWaterMark)
            {
                if (objFileRestrictpic.WatermarkType == 2)
                {
                    SysAssert.CheckCondition(string.IsNullOrEmpty(objFileRestrictpic.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                    objImage = objImage.CreateWatermark(objFileRestrictpic.WatermarkText, objHorizontalAlign, objVerticalAlign);
                    return objImage;
                }
                ResourceRule rule = new ResourceRule();
                Sys_WaterImage instance = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileRestrictpic.WaterImageID);
                if (!instance.IsNoNull())
                {
                    return objImage;
                }
                string waterImagePath = "";
                if (File.Exists(instance.WaterImagePath))
                {
                    waterImagePath = instance.WaterImagePath;
                }
                else if ((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Server != null))
                {
                    waterImagePath = SysVariable.CurrentContext.Server.MapPath(instance.WaterImagePath);
                }
                if (!string.IsNullOrWhiteSpace(waterImagePath))
                {
                    System.Drawing.Image watermarkImg = System.Drawing.Image.FromFile(waterImagePath, false);
                    objImage = objImage.CreateWatermark(watermarkImg, objHorizontalAlign, objVerticalAlign);
                    watermarkImg.Dispose();
                }
            }
            return objImage;
        }

        public static InvokeResult SaveFile(this HttpPostedFile PostedFile, int ResourceTypeID, string ResourceID, string RestrictCode, out ResourceInfo objResourceInfo, string Remark = "")
        {
            int num2;
            objResourceInfo = new ResourceInfo();
            InvokeResult result = new InvokeResult();
            ResourceRule rule = new ResourceRule();
            if (ResourceTypeID.IsNull())
            {
                result.ResultCode = "-1";
                result.ResultMessage = "请设置资源类型标识";
                return result;
            }
            if (RestrictCode.IsNull())
            {
                result.ResultCode = "-1";
                result.ResultMessage = "请设置RestrictCode";
                return result;
            }
            Sys_ResourceRestrict resourceRestrict = rule.GetResourceRestrict(ResourceTypeID, RestrictCode);
            string fileExtension = Path.GetExtension(PostedFile.FileName).ToLower();
            if (!(string.IsNullOrEmpty(resourceRestrict.FileExtension) || (resourceRestrict.FileExtension.IndexOf(fileExtension.Trim(new char[] { '.' })) != -1)))
            {
                result.ResultCode = "-1";
                result.ResultMessage = "上传的文件扩展名必须是" + resourceRestrict.FileExtension;
                return result;
            }
            if (((((double) PostedFile.ContentLength) / 1024.0) > resourceRestrict.FileMaxSize) && (resourceRestrict.FileMaxSize > 0))
            {
                result.ResultCode = "-1";
                result.ResultMessage = "对不起支持上传的文件大小为" + ((int) (resourceRestrict.FileMaxSize * 0x400)).RenderFileSize() + ",当前的文件大小超出";
                return result;
            }
            string resourceName = string.Concat(new object[] { "类型:", resourceRestrict.ResourceTypeID, "限制标识", resourceRestrict.ResourceRestrictID.ToString(), PostedFile.FileName });
            if (ResourceID.IsNull())
            {
                ResourceID = rule.InsertResource(resourceName, ResourceTypeID);
            }
            else
            {
                rule.CheckResourceID(ResourceID, resourceName, resourceRestrict.ResourceTypeID);
            }
            List<ResourceInfo> source = new List<ResourceInfo>();
            if (CheckIsImage(fileExtension) && (resourceRestrict.Sys_ResourceRestrictPic.Count > 0))
            {
                System.Drawing.Image image;
                Sys_WaterImage image2;
                System.Drawing.Image image3;
                if (resourceRestrict.Sys_ResourceRestrictPic.Count > 1)
                {
                    source = new List<ResourceInfo>();
                    using (List<Sys_ResourceRestrictPic>.Enumerator enumerator = (from s in resourceRestrict.Sys_ResourceRestrictPic
                        orderby s.VerNo
                        orderby s.ResourceRestrictPicID
                        select s).ToList<Sys_ResourceRestrictPic>().GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Stream stream;
                            Sys_ResourceRestrictPic objFileVerInfo = enumerator.Current;
                            int verNo = objFileVerInfo.VerNo;
                            if (verNo == 0)
                            {
                                verNo = rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo);
                            }
                            image = PostedFile.InputStream.StreamToImage();
                            if ((objFileVerInfo.ImageWidth > 0) && (objFileVerInfo.ImageHeight > 0))
                            {
                                if ((image.Width > objFileVerInfo.ImageWidth) || (image.Height > objFileVerInfo.ImageHeight))
                                {
                                    image = image.CreateThumbnail("<=" + objFileVerInfo.ImageWidth.ToString(), "<=" + objFileVerInfo.ImageHeight.ToString());
                                    if (objFileVerInfo.CreateWaterMark)
                                    {
                                        if (objFileVerInfo.WatermarkType == 2)
                                        {
                                            SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                            image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                        }
                                        else
                                        {
                                            image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                            if (image2.IsNoNull())
                                            {
                                                image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                                image = image.CreateWatermark(image3, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                                image3.Dispose();
                                            }
                                        }
                                    }
                                    source.Add(rule.InsertResourceVer(ResourceID, Path.GetFileName(PostedFile.FileName), PostedFile.ContentLength, PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), verNo, Remark));
                                    image.Dispose();
                                }
                                else if (objFileVerInfo.CreateWaterMark)
                                {
                                    if (objFileVerInfo.WatermarkType == 2)
                                    {
                                        SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                        image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                    }
                                    else
                                    {
                                        image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                        if (image2.IsNoNull())
                                        {
                                            image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                            image = image.CreateWatermark(image3, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                            image3.Dispose();
                                        }
                                    }
                                    source.Add(rule.InsertResourceVer(ResourceID, Path.GetFileName(PostedFile.FileName), PostedFile.ContentLength, PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), verNo, Remark));
                                    image.Dispose();
                                }
                                else
                                {
                                    stream = image.ImageToStream(fileExtension);
                                    source.Add(rule.InsertResourceVer(ResourceID, Path.GetFileName(PostedFile.FileName), PostedFile.ContentLength, PostedFile.ContentType, "admin", stream, verNo, Remark));
                                }
                            }
                            else if (objFileVerInfo.CreateWaterMark)
                            {
                                if (objFileVerInfo.WatermarkType == 2)
                                {
                                    SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                    image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                }
                                else
                                {
                                    image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                    if (image2.IsNoNull())
                                    {
                                        image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                        image = image.CreateWatermark(image3, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                        image3.Dispose();
                                    }
                                }
                                source.Add(rule.InsertResourceVer(ResourceID, Path.GetFileName(PostedFile.FileName), PostedFile.ContentLength, PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), verNo, Remark));
                                image.Dispose();
                            }
                            else
                            {
                                stream = image.ImageToStream(fileExtension);
                                source.Add(rule.InsertResourceVer(ResourceID, Path.GetFileName(PostedFile.FileName), PostedFile.ContentLength, PostedFile.ContentType, "admin", stream, objFileVerInfo.VerNo, Remark));
                            }
                            image.Dispose();
                        }
                    }
                    objResourceInfo = source.FirstOrDefault<ResourceInfo>();
                    return result;
                }
                num2 = 0;
                Sys_ResourceRestrictPic objSys_ResourceRestrictPic = resourceRestrict.Sys_ResourceRestrictPic.FirstOrDefault<Sys_ResourceRestrictPic>();
                if (num2 == 0)
                {
                    num2 = objSys_ResourceRestrictPic.VerNo;
                }
                if (objSys_ResourceRestrictPic.CreateWaterMark && (fileExtension != ".gif"))
                {
                    image = PostedFile.InputStream.StreamToImage();
                    if (((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0)) && ((image.Width > objSys_ResourceRestrictPic.ImageWidth) || (image.Height > objSys_ResourceRestrictPic.ImageHeight)))
                    {
                        image = image.CreateThumbnail("<=" + objSys_ResourceRestrictPic.ImageWidth.ToString(), "<=" + objSys_ResourceRestrictPic.ImageHeight.ToString());
                    }
                    if (objSys_ResourceRestrictPic.WatermarkType == 2)
                    {
                        SysAssert.CheckCondition(string.IsNullOrEmpty(objSys_ResourceRestrictPic.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                        image = image.CreateWatermark(objSys_ResourceRestrictPic.WatermarkText, (HorizontalAlign) objSys_ResourceRestrictPic.HorizontalAlign, (VerticalAlign) objSys_ResourceRestrictPic.VerticalAlign);
                    }
                    else
                    {
                        image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objSys_ResourceRestrictPic.WaterImageID);
                        if (image2.IsNoNull())
                        {
                            image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                            image = image.CreateWatermark(image3, (HorizontalAlign) objSys_ResourceRestrictPic.HorizontalAlign, (VerticalAlign) objSys_ResourceRestrictPic.VerticalAlign);
                        }
                    }
                    objResourceInfo = rule.InsertResourceVer(ResourceID, Path.GetFileName(PostedFile.FileName), PostedFile.ContentLength, PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), Remark);
                    image.Dispose();
                    return result;
                }
                if ((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0))
                {
                    image = PostedFile.InputStream.StreamToImage();
                    if ((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0))
                    {
                        if ((image.Width > objSys_ResourceRestrictPic.ImageWidth) || (image.Height > objSys_ResourceRestrictPic.ImageHeight))
                        {
                            image = image.CreateThumbnail("<=" + objSys_ResourceRestrictPic.ImageWidth.ToString(), "<=" + objSys_ResourceRestrictPic.ImageHeight.ToString());
                            objResourceInfo = rule.InsertResourceVer(ResourceID, Path.GetFileName(PostedFile.FileName), PostedFile.ContentLength, PostedFile.ContentType, "admin", image.ImageToStream(fileExtension), (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), Remark);
                        }
                        else
                        {
                            objResourceInfo = rule.InsertResourceVer(ResourceID, (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), "admin", PostedFile, Remark);
                        }
                    }
                    else
                    {
                        objResourceInfo = rule.InsertResourceVer(ResourceID, (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), "admin", PostedFile, Remark);
                    }
                    image.Dispose();
                    return result;
                }
                objResourceInfo = rule.InsertResourceVer(ResourceID, (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), "admin", PostedFile, Remark);
                return result;
            }
            num2 = 0;
            if (num2 == 0)
            {
                num2 = resourceRestrict.VerNo;
            }
            objResourceInfo = rule.InsertResourceVer(ResourceID, (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), "admin", PostedFile, Remark);
            return result;
        }

        public static InvokeResult SaveFile(this byte[] PostedFile, string fileName, string contentType, int ResourceTypeID, string ResourceID, int VerNo, string RestrictCode, out ResourceInfo objResourceInfo, string Remark = "")
        {
            InvokeResult result = new InvokeResult();
            ResourceRule rule = new ResourceRule();
            rule.CheckResourceID(ResourceID, fileName, ResourceTypeID);
            if (VerNo <= 0)
            {
                VerNo = rule.GetResourceMaxVerNo(ResourceID, 0, 0x7fffffff);
            }
            objResourceInfo = rule.InsertResourceVer(ResourceID, fileName, PostedFile.Length, contentType, "admin", PostedFile.BytesToStream(), VerNo, Remark);
            return result;
        }

        public static InvokeResult SaveFile(this Stream PostedFile, string fileName, string contentType, int ResourceTypeID, string ResourceID, int VerNo, string RestrictCode, out ResourceInfo objResourceInfo, string Remark = "")
        {
            return PostedFile.StreamToBytes().SaveFile(fileName, contentType, ResourceTypeID, ResourceID, VerNo, RestrictCode, out objResourceInfo, Remark);
        }

        public static InvokeResult SaveImageFile(this HttpPostedFile PostedFile, int ResourceTypeID, string ResourceID, string RestrictCode, bool createWaterMark, int horizontalAlign, int verticalAlign, out ResourceInfo objResourceInfo, string Remark = "")
        {
            return PostedFile.InputStream.SaveImageFile(PostedFile.FileName, PostedFile.ContentType, ResourceTypeID, ResourceID, RestrictCode, createWaterMark, horizontalAlign, verticalAlign, out objResourceInfo, Remark);
        }

        public static InvokeResult SaveImageFile(this Stream objInputStream, string fileName, string contentType, int ResourceTypeID, string ResourceID, string RestrictCode, bool createWaterMark, int horizontalAlign, int verticalAlign, out ResourceInfo objResourceInfo, string Remark = "")
        {
            int num2;
            objResourceInfo = new ResourceInfo();
            InvokeResult result = new InvokeResult();
            ResourceRule rule = new ResourceRule();
            if (ResourceTypeID.IsNull())
            {
                result.ResultCode = "-1";
                result.ResultMessage = "请设置资源类型标识";
                return result;
            }
            if (RestrictCode.IsNull())
            {
                result.ResultCode = "-1";
                result.ResultMessage = "请设置RestrictCode";
                return result;
            }
            Sys_ResourceRestrict resourceRestrict = rule.GetResourceRestrict(ResourceTypeID, RestrictCode);
            string fileExtension = Path.GetExtension(fileName).ToLower();
            if (!(string.IsNullOrEmpty(resourceRestrict.FileExtension) || (resourceRestrict.FileExtension.IndexOf(fileExtension.Trim(new char[] { '.' })) != -1)))
            {
                result.ResultCode = "-1";
                result.ResultMessage = "上传的文件扩展名必须是" + resourceRestrict.FileExtension;
                return result;
            }
            if (((((double) objInputStream.Length) / 1024.0) > resourceRestrict.FileMaxSize) && (resourceRestrict.FileMaxSize > 0))
            {
                result.ResultCode = "-1";
                result.ResultMessage = "对不起支持上传的文件大小为" + ((int) (resourceRestrict.FileMaxSize * 0x400)).RenderFileSize() + ",当前的文件大小超出";
                return result;
            }
            string resourceName = string.Concat(new object[] { "类型:", resourceRestrict.ResourceTypeID, "限制标识", resourceRestrict.ResourceRestrictID.ToString(), fileName });
            if (ResourceID.IsNull())
            {
                ResourceID = rule.InsertResource(resourceName, ResourceTypeID);
            }
            else
            {
                rule.CheckResourceID(ResourceID, resourceName, resourceRestrict.ResourceTypeID);
            }
            List<ResourceInfo> source = new List<ResourceInfo>();
            if (CheckIsImage(fileExtension) && (resourceRestrict.Sys_ResourceRestrictPic.Count > 0))
            {
                System.Drawing.Image image;
                Sys_WaterImage image2;
                System.Drawing.Image image3;
                if (resourceRestrict.Sys_ResourceRestrictPic.Count > 1)
                {
                    source = new List<ResourceInfo>();
                    using (List<Sys_ResourceRestrictPic>.Enumerator enumerator = (from s in resourceRestrict.Sys_ResourceRestrictPic
                        orderby s.VerNo
                        orderby s.ResourceRestrictPicID
                        select s).ToList<Sys_ResourceRestrictPic>().GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Stream stream;
                            Sys_ResourceRestrictPic objFileVerInfo = enumerator.Current;
                            int verNo = objFileVerInfo.VerNo;
                            if (verNo == 0)
                            {
                                verNo = rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo);
                            }
                            image = objInputStream.StreamToImage();
                            if ((objFileVerInfo.ImageWidth > 0) && (objFileVerInfo.ImageHeight > 0))
                            {
                                if ((image.Width > objFileVerInfo.ImageWidth) || (image.Height > objFileVerInfo.ImageHeight))
                                {
                                    image = image.CreateThumbnail("<=" + objFileVerInfo.ImageWidth.ToString(), "<=" + objFileVerInfo.ImageHeight.ToString());
                                    if (createWaterMark)
                                    {
                                        if (objFileVerInfo.WatermarkType == 2)
                                        {
                                            SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                            image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) objFileVerInfo.HorizontalAlign, (VerticalAlign) objFileVerInfo.VerticalAlign);
                                        }
                                        else
                                        {
                                            image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                            if (image2.IsNoNull())
                                            {
                                                image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                                image = image.CreateWatermark(image3, (HorizontalAlign) horizontalAlign, (VerticalAlign) verticalAlign);
                                                image3.Dispose();
                                            }
                                        }
                                    }
                                    source.Add(rule.InsertResourceVer(ResourceID, Path.GetFileName(fileName), (int) objInputStream.Length, contentType, "admin", image.ImageToStream(fileExtension), verNo, Remark));
                                    image.Dispose();
                                }
                                else if (createWaterMark)
                                {
                                    if (objFileVerInfo.WatermarkType == 2)
                                    {
                                        SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                        image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) horizontalAlign, (VerticalAlign) verticalAlign);
                                    }
                                    else
                                    {
                                        image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                        if (image2.IsNoNull())
                                        {
                                            image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                            image = image.CreateWatermark(image3, (HorizontalAlign) horizontalAlign, (VerticalAlign) verticalAlign);
                                            image3.Dispose();
                                        }
                                    }
                                    source.Add(rule.InsertResourceVer(ResourceID, Path.GetFileName(fileName), (int) objInputStream.Length, contentType, "admin", image.ImageToStream(fileExtension), verNo, Remark));
                                    image.Dispose();
                                }
                                else
                                {
                                    stream = image.ImageToStream(fileExtension);
                                    source.Add(rule.InsertResourceVer(ResourceID, fileName, (int) objInputStream.Length, contentType, "admin", stream, verNo, Remark));
                                }
                            }
                            else if (createWaterMark)
                            {
                                if (objFileVerInfo.WatermarkType == 2)
                                {
                                    SysAssert.CheckCondition(string.IsNullOrEmpty(objFileVerInfo.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                                    image = image.CreateWatermark(objFileVerInfo.WatermarkText, (HorizontalAlign) horizontalAlign, (VerticalAlign) verticalAlign);
                                }
                                else
                                {
                                    image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objFileVerInfo.WaterImageID);
                                    if (image2.IsNoNull())
                                    {
                                        image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                                        image = image.CreateWatermark(image3, (HorizontalAlign) horizontalAlign, (VerticalAlign) verticalAlign);
                                        image3.Dispose();
                                    }
                                }
                                source.Add(rule.InsertResourceVer(ResourceID, fileName, (int) objInputStream.Length, contentType, "admin", image.ImageToStream(fileExtension), verNo, Remark));
                                image.Dispose();
                            }
                            else
                            {
                                stream = image.ImageToStream(fileExtension);
                                source.Add(rule.InsertResourceVer(ResourceID, fileName, (int) objInputStream.Length, contentType, "admin", stream, objFileVerInfo.VerNo, Remark));
                            }
                            image.Dispose();
                        }
                    }
                    objResourceInfo = source.FirstOrDefault<ResourceInfo>();
                    return result;
                }
                num2 = 0;
                Sys_ResourceRestrictPic objSys_ResourceRestrictPic = resourceRestrict.Sys_ResourceRestrictPic.FirstOrDefault<Sys_ResourceRestrictPic>();
                if (num2 == 0)
                {
                    num2 = objSys_ResourceRestrictPic.VerNo;
                }
                if (createWaterMark && (fileExtension != ".gif"))
                {
                    image = objInputStream.StreamToImage();
                    if (((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0)) && ((image.Width > objSys_ResourceRestrictPic.ImageWidth) || (image.Height > objSys_ResourceRestrictPic.ImageHeight)))
                    {
                        image = image.CreateThumbnail("<=" + objSys_ResourceRestrictPic.ImageWidth.ToString(), "<=" + objSys_ResourceRestrictPic.ImageHeight.ToString());
                    }
                    if (objSys_ResourceRestrictPic.WatermarkType == 2)
                    {
                        SysAssert.CheckCondition(string.IsNullOrEmpty(objSys_ResourceRestrictPic.WatermarkText), "请输入水印的文本", LogModuleType.ResourceLog);
                        image = image.CreateWatermark(objSys_ResourceRestrictPic.WatermarkText, (HorizontalAlign) horizontalAlign, (VerticalAlign) verticalAlign);
                    }
                    else
                    {
                        image2 = rule.Sys_WaterImage.FirstOrDefault<Sys_WaterImage>(s => s.WaterImageID == objSys_ResourceRestrictPic.WaterImageID);
                        if (image2.IsNoNull())
                        {
                            image3 = System.Drawing.Image.FromFile(SysVariable.CurrentContext.Server.MapPath(image2.WaterImagePath), false);
                            image = image.CreateWatermark(image3, (HorizontalAlign) horizontalAlign, (VerticalAlign) verticalAlign);
                        }
                    }
                    objResourceInfo = rule.InsertResourceVer(ResourceID, fileName, (int) objInputStream.Length, contentType, "admin", image.ImageToStream(fileExtension), (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), Remark);
                    image.Dispose();
                    return result;
                }
                if ((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0))
                {
                    image = objInputStream.StreamToImage();
                    if ((objSys_ResourceRestrictPic.ImageWidth > 0) && (objSys_ResourceRestrictPic.ImageHeight > 0))
                    {
                        if ((image.Width > objSys_ResourceRestrictPic.ImageWidth) || (image.Height > objSys_ResourceRestrictPic.ImageHeight))
                        {
                            image = image.CreateThumbnail("<=" + objSys_ResourceRestrictPic.ImageWidth.ToString(), "<=" + objSys_ResourceRestrictPic.ImageHeight.ToString());
                            objResourceInfo = rule.InsertResourceVer(ResourceID, Path.GetFileName(fileName), (int) objInputStream.Length, contentType, "admin", image.ImageToStream(fileExtension), (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), Remark);
                        }
                        else
                        {
                            objResourceInfo = rule.InsertResourceVer(ResourceID, fileName, (int) objInputStream.Length, contentType, "admin", objInputStream, (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), Remark);
                        }
                    }
                    else
                    {
                        objResourceInfo = rule.InsertResourceVer(ResourceID, fileName, (int) objInputStream.Length, contentType, "admin", objInputStream, (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), Remark);
                    }
                    image.Dispose();
                    return result;
                }
                objResourceInfo = rule.InsertResourceVer(ResourceID, fileName, (int) objInputStream.Length, contentType, "admin", objInputStream, (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), Remark);
                return result;
            }
            num2 = 0;
            if (num2 == 0)
            {
                num2 = resourceRestrict.VerNo;
            }
            objResourceInfo = rule.InsertResourceVer(ResourceID, fileName, (int) objInputStream.Length, contentType, "admin", objInputStream, (num2 != 0) ? num2 : rule.GetResourceMaxVerNo(ResourceID, resourceRestrict.BeginVerNo, resourceRestrict.EndVerNo), Remark);
            return result;
        }

        public static InvokeResult SaveImageFile(this byte[] PostedFile, string fileName, string contentType, int ResourceTypeID, string ResourceID, int VerNo, string RestrictCode, bool createWaterMark, int horizontalAlign, int verticalAlign, out ResourceInfo objResourceInfo, string Remark = "")
        {
            return PostedFile.BytesToStream().SaveImageFile(fileName, contentType, ResourceTypeID, ResourceID, RestrictCode, createWaterMark, horizontalAlign, verticalAlign, out objResourceInfo, Remark);
        }

        public static List<ResourceInfo> SaveResource(this HttpPostedFile objPostedFile, string ResourceCode, string RestrictCode)
        {
            ResourceInfo info;
            ResourceCode.CheckIsNull("请设置资源类型编码ResourceCode", LogModuleType.ResourceLog);
            RestrictCode.CheckIsNull("请设置RestrictCode", LogModuleType.ResourceLog);
            FileResourceRule rule = new FileResourceRule();
            resource_filerestrict _filerestrict = rule.resource_filerestrict.Where("it.RestrictCode='" + RestrictCode + "' and it.resource_fileresource.FileResourceCode='" + ResourceCode + "'", new ObjectParameter[0]).Include("resource_filestoragepath").Include("resource_filerestrictpic").FirstOrDefault<resource_filerestrict>();
            if (_filerestrict == null)
            {
                SysAssert.InfoHintAssert("找不到此文件配置");
            }
            string fileExtension = Path.GetExtension(objPostedFile.FileName).ToLower();
            SysAssert.InfoHintAssert(!string.IsNullOrEmpty(_filerestrict.FileExtension) && (_filerestrict.FileExtension.IndexOf(fileExtension.Trim(new char[] { '.' })) == -1), "上传的文件扩展名必须是" + _filerestrict.FileExtension);
            SysAssert.InfoHintAssert(((((double) objPostedFile.ContentLength) / 1024.0) > _filerestrict.FileMaxSize) && (_filerestrict.FileMaxSize > 0), "对不起支持上传的文件大小为" + ((int) (_filerestrict.FileMaxSize * 0x400)).RenderFileSize() + ",当前的文件大小超出");
            List<ResourceInfo> list = new List<ResourceInfo>();
            if (_filerestrict.resource_filerestrictpic.Count > 0)
            {
                foreach (resource_filerestrictpic _filerestrictpic in (from s in _filerestrict.resource_filerestrictpic
                    orderby s.SortIndex
                    select s).ToList<resource_filerestrictpic>())
                {
                    System.Drawing.Image objImage = objPostedFile.InputStream.StreamToImage();
                    objImage = ProcessRestrictPic(_filerestrictpic, objImage);
                    Stream objPostStream = objImage.ImageToStream(fileExtension);
                    info = SwitchResourceSave(_filerestrict, Path.GetFileName(objPostedFile.FileName), objPostStream);
                    info.VerNo = _filerestrictpic.SortIndex;
                    list.Add(info);
                    objImage.Dispose();
                }
                return list;
            }
            info = SwitchResourceSave(_filerestrict, Path.GetFileName(objPostedFile.FileName), objPostedFile.InputStream);
            list.Add(info);
            return list;
        }

        public static ResourceInfo SaveResource(this byte[] PostedFile, string fileName, string ResourceCode, string RestrictCode)
        {
            return PostedFile.SaveResourceList(fileName, ResourceCode, RestrictCode)[0];
        }

        public static ResourceInfo SaveResource(this Stream PostedFile, string fileName, string ResourceCode, string RestrictCode)
        {
            ResourceCode.CheckIsNull("请设置资源类型编码ResourceCode", LogModuleType.ResourceLog);
            FileResourceRule rule = new FileResourceRule();
            RestrictCode.CheckIsNull("请设置RestrictCode", LogModuleType.ResourceLog);
            resource_filerestrict _filerestrict = rule.resource_filerestrict.Where("it.RestrictCode='" + RestrictCode + "' and it.resource_fileresource.FileResourceCode='" + ResourceCode + "'", new ObjectParameter[0]).Include("resource_filestoragepath").FirstOrDefault<resource_filerestrict>();
            if (_filerestrict == null)
            {
                SysAssert.InfoHintAssert("找不到此文件配置");
            }
            return SwitchResourceSave(_filerestrict, fileName, PostedFile);
        }

        public static List<ResourceInfo> SaveResourceImage(this HttpPostedFile PostedFile, string ResourceCode, string RestrictCode, bool createWaterMark, HorizontalAlign objHorizontalAlign, VerticalAlign objVerticalAlign)
        {
            return PostedFile.InputStream.SaveResourceImage(PostedFile.FileName, ResourceCode, RestrictCode, createWaterMark, objHorizontalAlign, objVerticalAlign);
        }

        public static List<ResourceInfo> SaveResourceImage(this Stream objInputStream, string fileName, string ResourceCode, string RestrictCode, bool createWaterMark, HorizontalAlign objHorizontalAlign, VerticalAlign objVerticalAlign)
        {
            List<ResourceInfo> list = new List<ResourceInfo>();
            InvokeResult result = new InvokeResult();
            ResourceCode.CheckIsNull("请设置资源类型编码ResourceCode", LogModuleType.ResourceLog);
            RestrictCode.CheckIsNull("请设置RestrictCode", LogModuleType.ResourceLog);
            FileResourceRule rule = new FileResourceRule();
            resource_filerestrict _filerestrict = rule.resource_filerestrict.Where("it.RestrictCode='" + RestrictCode + "' and it.resource_fileresource.FileResourceCode='" + ResourceCode + "'", new ObjectParameter[0]).Include("resource_filestoragepath").Include("resource_filerestrictpic").FirstOrDefault<resource_filerestrict>();
            if (_filerestrict == null)
            {
                SysAssert.InfoHintAssert("找不到此文件配置");
            }
            string fileExtension = Path.GetExtension(fileName).ToLower();
            SysAssert.InfoHintAssert(!string.IsNullOrEmpty(_filerestrict.FileExtension) && (_filerestrict.FileExtension.IndexOf(fileExtension.Trim(new char[] { '.' })) == -1), "上传的文件扩展名必须是" + _filerestrict.FileExtension);
            SysAssert.InfoHintAssert(((((double) objInputStream.Length) / 1024.0) > _filerestrict.FileMaxSize) && (_filerestrict.FileMaxSize > 0), "对不起支持上传的文件大小为" + ((int) (_filerestrict.FileMaxSize * 0x400)).RenderFileSize() + ",当前的文件大小超出");
            List<ResourceInfo> list2 = new List<ResourceInfo>();
            if ((_filerestrict.resource_filerestrictpic.Count > 0) && (fileExtension != ".gif"))
            {
                foreach (resource_filerestrictpic _filerestrictpic in (from s in _filerestrict.resource_filerestrictpic
                    orderby s.SortIndex
                    select s).ToList<resource_filerestrictpic>())
                {
                    System.Drawing.Image objImage = objInputStream.StreamToImage();
                    objImage = ProcessRestrictPic(_filerestrictpic, objImage, createWaterMark, objHorizontalAlign, objVerticalAlign);
                    Stream objPostStream = objImage.ImageToStream(fileExtension);
                    ResourceInfo item = SwitchResourceSave(_filerestrict, Path.GetFileName(fileName), objPostStream);
                    item.VerNo = _filerestrictpic.SortIndex;
                    list2.Add(item);
                    objImage.Dispose();
                }
                return list2;
            }
            list2.Add(SwitchResourceSave(_filerestrict, Path.GetFileName(fileName), objInputStream));
            return list2;
        }

        public static List<ResourceInfo> SaveResourceImage(this byte[] PostedFile, string fileName, string ResourceCode, string RestrictCode, bool createWaterMark, HorizontalAlign objHorizontalAlign, VerticalAlign objVerticalAlign)
        {
            return PostedFile.BytesToStream().SaveResourceImage(fileName, ResourceCode, RestrictCode, createWaterMark, objHorizontalAlign, objVerticalAlign);
        }

        public static List<ResourceInfo> SaveResourceList(this byte[] PostedFile, string fileName, string ResourceCode, string RestrictCode)
        {
            ResourceInfo info;
            ResourceCode.CheckIsNull("请设置资源类型编码ResourceCode", LogModuleType.ResourceLog);
            RestrictCode.CheckIsNull("请设置RestrictCode", LogModuleType.ResourceLog);
            FileResourceRule rule = new FileResourceRule();
            resource_filerestrict _filerestrict = rule.resource_filerestrict.Where("it.RestrictCode='" + RestrictCode + "' and it.resource_fileresource.FileResourceCode='" + ResourceCode + "'", new ObjectParameter[0]).Include("resource_filestoragepath").Include("resource_filerestrictpic").FirstOrDefault<resource_filerestrict>();
            if (_filerestrict == null)
            {
                SysAssert.InfoHintAssert("找不到此文件配置");
            }
            List<ResourceInfo> list = new List<ResourceInfo>();
            Stream stream = PostedFile.BytesToStream();
            string extension = Path.GetExtension(fileName);
            if (_filerestrict.resource_filerestrictpic.Count > 0)
            {
                foreach (resource_filerestrictpic _filerestrictpic in (from s in _filerestrict.resource_filerestrictpic
                    orderby s.SortIndex
                    select s).ToList<resource_filerestrictpic>())
                {
                    System.Drawing.Image objImage = stream.StreamToImage();
                    objImage = ProcessRestrictPic(_filerestrictpic, objImage);
                    Stream objPostStream = objImage.ImageToStream(extension);
                    info = SwitchResourceSave(_filerestrict, fileName, objPostStream);
                    info.VerNo = _filerestrictpic.SortIndex;
                    list.Add(info);
                    objImage.Dispose();
                }
                return list;
            }
            info = SwitchResourceSave(_filerestrict, fileName, stream);
            list.Add(info);
            return list;
        }

        private static ResourceInfo SwitchResourceSave(resource_filerestrict objresource_filerestrict, string fileName, Stream objPostStream)
        {
            ResourceInfo info = new ResourceInfo();
            string str = Path.GetExtension(fileName).ToLower().Trim(new char[] { '.' });
            string str2 = "";
            int width = 0;
            int height = 0;
            if (objresource_filerestrict.IsReturnSize == 1)
            {
                try
                {
                    System.Drawing.Image image = objPostStream.StreamToImage();
                    if (image != null)
                    {
                        width = image.Size.Width;
                        height = image.Size.Height;
                        str2 = string.Concat(new object[] { "?w=", image.Size.Width, "&h=", image.Size.Height });
                    }
                }
                catch
                {
                }
            }
            resource_filestoragepath _filestoragepath = objresource_filerestrict.resource_filestoragepath;
            FileResourceRule rule = new FileResourceRule();
            if (_filestoragepath.StorageTypeID == 1)
            {
                info = rule.SaveResource(objresource_filerestrict, fileName, objPostStream);
            }
            else
            {
                string ftpFullFileNamePath;
                byte[] buffer;
                string str4;
                if (_filestoragepath.StorageTypeID == 2)
                {
                    FtpHelper helper = new FtpHelper(new Uri("Ftp://" + _filestoragepath.IPAddress + ":" + _filestoragepath.Port + "/"), _filestoragepath.Account, _filestoragepath.Password);
                    ftpFullFileNamePath = WTF.Resource.ResourceHelper.GetFtpFullFileNamePath(objresource_filerestrict, fileName);
                    buffer = objPostStream.StreamToBytes();
                    if (objresource_filerestrict.IsMd5 == 1)
                    {
                        info.Md5Value = buffer.MD5Encrypt();
                    }
                    helper.UploadFile(buffer, ftpFullFileNamePath);
                    info.FilePath = ftpFullFileNamePath;
                    info.OriginalName = fileName;
                    info.ResourcePath = ftpFullFileNamePath;
                    if (!string.IsNullOrWhiteSpace(_filestoragepath.VirtualName))
                    {
                        str4 = _filestoragepath.VirtualName.GetRandomSwitchString().TrimEnd(new char[] { '/' });
                        info.ResourcePath = str4 + "/" + ftpFullFileNamePath.TrimStart(new char[] { '/' });
                    }
                }
                else if (_filestoragepath.StorageTypeID == 3)
                {
                    FastDFSClient client = new FastDFSClient(_filestoragepath.StorageConfig, string.IsNullOrWhiteSpace(_filestoragepath.StorageConfig));
                    buffer = objPostStream.StreamToBytes();
                    if (objresource_filerestrict.IsMd5 == 1)
                    {
                        info.Md5Value = buffer.MD5Encrypt();
                    }
                    ftpFullFileNamePath = client.UploadFile(_filestoragepath.StoragePath, buffer, Path.GetExtension(fileName));
                    info.FilePath = ftpFullFileNamePath;
                    info.OriginalName = fileName;
                    info.ResourcePath = ftpFullFileNamePath;
                    if (!string.IsNullOrWhiteSpace(_filestoragepath.VirtualName))
                    {
                        str4 = _filestoragepath.VirtualName.GetRandomSwitchString().TrimEnd(new char[] { '/' });
                        info.ResourcePath = str4 + ftpFullFileNamePath;
                    }
                }
                else
                {
                    info = rule.SaveResource(objresource_filerestrict, fileName, objPostStream);
                }
            }
            if (objresource_filerestrict.IsHistory == 1)
            {
                resource_filehistory _filehistory = new resource_filehistory {
                    FileResourceID = objresource_filerestrict.FileResourceID,
                    FileType = objresource_filerestrict.FileType,
                    FileStatus = 1,
                    PicTitle = fileName,
                    PicUrl = info.ResourcePath,
                    CreateDate = DateTime.Now,
                    CreateUserID = 0,
                    UserName = ""
                };
                rule.Insertfilehistory(_filehistory);
            }
            info.ResourcePath = info.ResourcePath + str2;
            info.ImageWidth = width;
            info.ImageHeight = height;
            return info;
        }
    }
}

