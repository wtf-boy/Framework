namespace WTF.Resource
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Resource.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Web;

    public class ResourceRule
    {
        private ResourceEntities objCurrentEntities = null;

        public void CheckResourceID(string ResourceID, string resourceName, int resourceTypeID)
        {
            if (!this.CurrentEntities.sys_resource.Any<WTF.Resource.Entity.Sys_Resource>(s => (s.ResourceID == ResourceID)))
            {
                ResourceID.CheckIsNull("资源标识不能为空", LogModuleType.ResourceLog);
                resourceName.CheckIsNull("资源类型名称不为空", LogModuleType.ResourceLog);
                SysAssert.CheckCondition(resourceTypeID <= 0, "资源类型标识不为空", LogModuleType.ResourceLog);
                WTF.Resource.Entity.Sys_ResourceType type = this.CurrentEntities.sys_resourcetype.FirstOrDefault<WTF.Resource.Entity.Sys_ResourceType>(p => p.ResourceTypeID == resourceTypeID);
                SysAssert.CheckCondition(type == null, "资源类型不存在,请在资源申请处添加", LogModuleType.ResourceLog);
                WTF.Resource.Entity.Sys_Resource entity = new WTF.Resource.Entity.Sys_Resource
                {
                    ResourceID = ResourceID,
                    ResourceName = resourceName,
                    VerCount = 0,
                    CreateDate = DateTime.Now
                };
                type.Sys_Resource.Add(entity);
                this.CurrentEntities.SaveChanges();
            }
        }

        public void DeleteResource(IEnumerable<string> resourceIDList)
        {
            this.DeleteResource(resourceIDList.ConvertListToString<string>());
        }

        public void DeleteResource(string resourceIDString)
        {
            if (resourceIDString.ConvertListString().Count > 0)
            {
                //ObjectQuery<WTF.Resource.Entity.Sys_Resource> query = this.CurrentEntities.sys_resource.Where("it.ResourceID in {" + resourceIDString.ConvertStringID() + "}", new ObjectParameter[0]).Include("Sys_ResourceType").Include("Sys_ResourceVer");
                List<Sys_Resource> query = this.CurrentEntities.sys_resource.Where("it.ResourceID in {" + resourceIDString.ConvertStringID() + "}", new ObjectParameter[0]).Include("Sys_ResourceType").Include("Sys_ResourceVer").ToList();
                List<string> resourceFilePath = new List<string>();
                using (IEnumerator<WTF.Resource.Entity.Sys_Resource> enumerator = query.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        WTF.Resource.Entity.Sys_Resource objResource = enumerator.Current;
                        WTF.Resource.Entity.Sys_ResourcePath path = this.CurrentEntities.sys_resourcepath.First<WTF.Resource.Entity.Sys_ResourcePath>(s => s.ResourcePathID == objResource.Sys_ResourceType.ResourcePathID);
                        foreach (WTF.Resource.Entity.Sys_ResourceVer ver in objResource.Sys_ResourceVer)
                        {
                            if (string.IsNullOrEmpty(ver.DictionaryPath))
                            {
                                resourceFilePath.Add(this.GetresourceFullFileNamePath(path.StoragePath, (AccessModeCodeType)objResource.Sys_ResourceType.AccessModeCodeType, objResource.Sys_ResourceType.ResourceTypeCode, this.GetResourcePathFormatValue(ver.UpdateDateTime, ver.ResourceVerID, objResource.Sys_ResourceType.PathFormatCodeType), ver.ResourceGUIDFileName, ver.ResourceFileName, false));
                            }
                            else
                            {
                                resourceFilePath.Add(Path.Combine(path.StoragePath, ver.DictionaryPath));
                            }
                        }
                    }
                }
                if (resourceFilePath.Count > 0)
                {
                    this.DeleteResourceFile(resourceFilePath);
                }
                this.objCurrentEntities = null;
                this.CurrentEntities.sys_resource.DeleteDataPrimaryKey<WTF.Resource.Entity.Sys_Resource>(resourceIDString);
            }
        }

        public void DeleteResource(string resourceID, string verNoString)
        {
            WTF.Resource.Entity.Sys_Resource objResource;
            if (verNoString.Length > 0)
            {
                objResource = this.CurrentEntities.sys_resource.Where("it.ResourceID=='" + resourceID + "'", new ObjectParameter[0]).Include("Sys_ResourceType").FirstOrDefault<WTF.Resource.Entity.Sys_Resource>();
                if (objResource != null)
                {
                    List<WTF.Resource.Entity.Sys_ResourceVer> list = this.CurrentEntities.sys_resourcever.Where("it.ResourceID=='" + resourceID + "' and  it.VerNo in {" + verNoString + "}", new ObjectParameter[0]).Include("Sys_ResourceData").ToList<WTF.Resource.Entity.Sys_ResourceVer>();
                    WTF.Resource.Entity.Sys_ResourcePath path = this.CurrentEntities.sys_resourcepath.First<WTF.Resource.Entity.Sys_ResourcePath>(s => s.ResourcePathID == objResource.Sys_ResourceType.ResourcePathID);
                    List<string> resourceFilePath = new List<string>();
                    foreach (WTF.Resource.Entity.Sys_ResourceVer ver in list)
                    {
                        objResource.VerCount--;
                        if (string.IsNullOrEmpty(ver.DictionaryPath))
                        {
                            resourceFilePath.Add(this.GetresourceFullFileNamePath(path.StoragePath, (AccessModeCodeType)objResource.Sys_ResourceType.AccessModeCodeType, objResource.Sys_ResourceType.ResourceTypeCode, this.GetResourcePathFormatValue(ver.UpdateDateTime, ver.ResourceVerID, objResource.Sys_ResourceType.PathFormatCodeType), ver.ResourceGUIDFileName, ver.ResourceFileName, false));
                        }
                        else
                        {
                            resourceFilePath.Add(Path.Combine(path.StoragePath, ver.DictionaryPath));
                        }
                        if (ver.Sys_ResourceData != null)
                        {
                            this.CurrentEntities.DeleteObject(ver.Sys_ResourceData);
                        }
                        this.CurrentEntities.DeleteObject(ver);
                    }
                    if (resourceFilePath.Count > 0)
                    {
                        this.DeleteResourceFile(resourceFilePath);
                    }
                    this.CurrentEntities.SaveChanges();
                }
            }
        }

        public void DeleteResourceFile(IEnumerable<string> resourceFilePath)
        {
            foreach (string str in resourceFilePath)
            {
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
            }
        }

        public void DeleteResourceType(int resourceTypeID)
        {
            this.DeleteResourceType(resourceTypeID.ToString());
        }

        public void DeleteResourceType(string resourceTypeIDString)
        {
            //ObjectQuery<WTF.Resource.Entity.Sys_ResourceType> query = this.CurrentEntities.sys_resourcetype.Where("it.ResourceTypeID in {" + resourceTypeIDString + "}", new ObjectParameter[0]).Include("Sys_Resource");
            List<Sys_ResourceType> query = this.CurrentEntities.sys_resourcetype.Where("it.ResourceTypeID in {" + resourceTypeIDString + "}", new ObjectParameter[0]).Include("Sys_Resource").ToList();

            List<string> resourceFilePath = new List<string>();
            using (IEnumerator<WTF.Resource.Entity.Sys_ResourceType> enumerator = query.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    WTF.Resource.Entity.Sys_ResourceType objResourceType = enumerator.Current;
                    WTF.Resource.Entity.Sys_ResourcePath path = this.CurrentEntities.sys_resourcepath.First<WTF.Resource.Entity.Sys_ResourcePath>(s => s.ResourcePathID == objResourceType.ResourcePathID);
                    foreach (WTF.Resource.Entity.Sys_Resource resource in objResourceType.Sys_Resource)
                    {
                        resource.Sys_ResourceVer.Load();
                        foreach (WTF.Resource.Entity.Sys_ResourceVer ver in resource.Sys_ResourceVer)
                        {
                            if (string.IsNullOrEmpty(ver.DictionaryPath))
                            {
                                resourceFilePath.Add(this.GetresourceFullFileNamePath(path.StoragePath, (AccessModeCodeType)objResourceType.AccessModeCodeType, objResourceType.ResourceTypeCode, this.GetResourcePathFormatValue(ver.UpdateDateTime, ver.ResourceVerID, objResourceType.PathFormatCodeType), ver.ResourceGUIDFileName, ver.ResourceFileName, false));
                            }
                            else
                            {
                                resourceFilePath.Add(Path.Combine(path.StoragePath, ver.DictionaryPath));
                            }
                            ver.Sys_ResourceDataReference.Load();
                        }
                    }
                }
            }
            if (resourceFilePath.Count > 0)
            {
                this.DeleteResourceFile(resourceFilePath);
            }
            this.objCurrentEntities = null;
            this.CurrentEntities.sys_resourcetype.DeleteDataPrimaryKey<WTF.Resource.Entity.Sys_ResourceType>(resourceTypeIDString);
        }

        private string GetDirectoryPath(AccessModeCodeType accessModeCodeValue, string resourceTypeCode, string pathFormatValue, string resourceGUIDFileName, string resourceFileName)
        {
            string str = this.GetResourceDirectoryPath(accessModeCodeValue, resourceTypeCode, pathFormatValue, resourceGUIDFileName);
            if (accessModeCodeValue == AccessModeCodeType.VirtualOriginalAccess)
            {
                return (str + @"\" + resourceFileName);
            }
            return (str + @"\" + resourceGUIDFileName);
        }

        private string GetResourceDirectoryPath(AccessModeCodeType accessModeCodeValue, string resourceTypeCode, string pathFormatValue, string resourceGUIDFileName)
        {
            string str;
            if (pathFormatValue != "")
            {
                str = Path.Combine(resourceTypeCode, pathFormatValue);
            }
            else
            {
                str = resourceTypeCode;
            }
            if (accessModeCodeValue == AccessModeCodeType.VirtualOriginalAccess)
            {
                str = Path.Combine(str, Path.GetFileNameWithoutExtension(resourceGUIDFileName));
            }
            return str;
        }

        private string GetresourceFullFileNamePath(string StoragePath, AccessModeCodeType accessModeCodeValue, string resourceTypeCode, string pathFormatValue, string resourceGUIDFileName, string resourceFileName, bool isCreateDirectory = true)
        {
            string fileName = Path.Combine(StoragePath, this.GetResourceDirectoryPath(accessModeCodeValue, resourceTypeCode, pathFormatValue, resourceGUIDFileName));
            if (isCreateDirectory)
            {
                fileName.CreateFileDirectory();
            }
            if (accessModeCodeValue == AccessModeCodeType.VirtualOriginalAccess)
            {
                return (fileName + @"\" + resourceFileName);
            }
            return (fileName + @"\" + resourceGUIDFileName);
        }

        public int GetResourceMaxVerNo(string resourceID, int beginVerNo, int endVerNo)
        {
            try
            {
                if ((beginVerNo > 0) && (endVerNo > 0))
                {
                    beginVerNo = (from s in this.CurrentEntities.sys_resourcever
                                  where ((s.ResourceID == resourceID) && (s.VerNo >= beginVerNo)) && (s.VerNo <= endVerNo)
                                  select s.VerNo).Max<int>() + 1;
                }
                else
                {
                    beginVerNo = (from s in this.CurrentEntities.sys_resourcever
                                  where s.ResourceID == resourceID
                                  select s.VerNo).Max<int>() + 1;
                }
            }
            catch
            {
                beginVerNo = (beginVerNo == 0) ? 1 : beginVerNo;
            }
            if ((beginVerNo > 0) && (endVerNo > 0))
            {
                SysAssert.CheckCondition(beginVerNo > endVerNo, "超最大上传文件数", LogModuleType.ResourceLog);
            }
            return beginVerNo;
        }

        private string GetResourcePathFormatValue(DateTime createDateTime, string resourceVerID, int pathFormatCodeID)
        {
            if (pathFormatCodeID == 0)
            {
                return createDateTime.ToString("yyyy-MM-dd");
            }
            if (pathFormatCodeID == 1)
            {
                return createDateTime.ToString("yyyy-MM-dd-HH");
            }
            if (pathFormatCodeID == 2)
            {
                return resourceVerID.ToString();
            }
            if (pathFormatCodeID == 3)
            {
                return "";
            }
            return "";
        }

        public WTF.Resource.Entity.Sys_ResourceRestrict GetResourceRestrict(int resourceTypeID, string restrictCode)
        {
            WTF.Resource.Entity.Sys_ResourceRestrict instance = this.Sys_ResourceRestrict.Where(string.Concat(new object[] { "it.ResourceTypeID=", resourceTypeID, " and it.RestrictCode='", restrictCode, "'" }), new ObjectParameter[0]).Include("Sys_ResourceRestrictPic").ToList<WTF.Resource.Entity.Sys_ResourceRestrict>().FirstOrDefault<WTF.Resource.Entity.Sys_ResourceRestrict>();
            SysAssert.CheckCondition(instance.IsNull(), string.Concat(new object[] { "请正确传入的资源类型标识:", resourceTypeID, ",限制码", restrictCode }), LogModuleType.ResourceLog);
            return instance;
        }

        private string GetResourceURL(string VirtualName, AccessModeCodeType accessModeCodeValue, string resourceTypeCode, string pathFormatValue, string resourceGUIDFileName, string resourceFileName, string resourceVerID)
        {
            VirtualName = VirtualName.TrimEnd(new char[] { '/' });
            if (pathFormatValue != "")
            {
                if (accessModeCodeValue == AccessModeCodeType.VirtualAccess)
                {
                    return (VirtualName + "/" + resourceTypeCode + "/" + pathFormatValue + "/" + resourceGUIDFileName);
                }
                return (VirtualName + "/" + resourceTypeCode + "/" + pathFormatValue + "/" + Path.GetFileNameWithoutExtension(resourceGUIDFileName) + "/" + resourceFileName);
            }
            if (accessModeCodeValue == AccessModeCodeType.VirtualAccess)
            {
                return (VirtualName + "/" + resourceTypeCode + "/" + resourceGUIDFileName);
            }
            return (VirtualName + "/" + resourceTypeCode + "/" + Path.GetFileNameWithoutExtension(resourceGUIDFileName) + "/" + resourceFileName);
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_ResourceVer> GetResourceVerByPage(string condition, string sortExpression, int pageSize, int currentPageIndex, out int recordCount)
        {
            return this.CurrentEntities.sys_resourcever.GetPage<WTF.Resource.Entity.Sys_ResourceVer>(condition, sortExpression, pageSize, currentPageIndex, out recordCount);
        }

        public ObjectResult<Sys_ResourceFileInfo> GetResourceVerFilePathInfo(int resourceTypeID)
        {
            return this.CurrentEntities.GetResourceFilePathByResourceTypeID(new int?(resourceTypeID));
        }

        public ObjectResult<WTF.Resource.Entity.Sys_ResourceVer> GetResourceVerInfo(int resourceTypeID)
        {
            return this.CurrentEntities.GetResourceVerByResourceTypeID(new int?(resourceTypeID));
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_ResourceVer> GetResourceVerInfo(string resourceID)
        {
            return this.CurrentEntities.sys_resource.First<WTF.Resource.Entity.Sys_Resource>(p => (p.ResourceID == resourceID)).Sys_ResourceVer.CreateSourceQuery();
        }

        public string GetResourceVerPath(string resourceVerID)
        {
            return this.CurrentEntities.sys_resourcever.First<WTF.Resource.Entity.Sys_ResourceVer>(p => (p.ResourceVerID == resourceVerID)).ResourcePath;
        }

        public string GetResourceVerPath(string resourceID, int verNo)
        {
            return this.CurrentEntities.sys_resource.First<WTF.Resource.Entity.Sys_Resource>(s => (s.ResourceID == resourceID)).Sys_ResourceVer.CreateSourceQuery().First<WTF.Resource.Entity.Sys_ResourceVer>(s => (s.VerNo == verNo)).ResourcePath;
        }

        public string InsertResource(string resourceName, int resourceTypeID)
        {
            resourceName.CheckIsNull("资源类型名称不为空", LogModuleType.ResourceLog);
            SysAssert.CheckCondition(resourceTypeID <= 0, "资源类型标识不为空", LogModuleType.ResourceLog);
            WTF.Resource.Entity.Sys_ResourceType type = this.CurrentEntities.sys_resourcetype.FirstOrDefault<WTF.Resource.Entity.Sys_ResourceType>(p => p.ResourceTypeID == resourceTypeID);
            SysAssert.CheckCondition(type == null, "资源类型不存在,请在资源申请处添加", LogModuleType.ResourceLog);
            WTF.Resource.Entity.Sys_Resource entity = new WTF.Resource.Entity.Sys_Resource
            {
                ResourceID = Guid.NewGuid().ToString(),
                ResourceName = resourceName,
                VerCount = 0,
                CreateDate = DateTime.Now
            };
            type.Sys_Resource.Add(entity);
            this.CurrentEntities.SaveChanges();
            return entity.ResourceID;
        }

        public string InsertResource(string ResourceID, string resourceName, int resourceTypeID)
        {
            ResourceID.CheckIsNull("资源标识不能为空", LogModuleType.ResourceLog);
            resourceName.CheckIsNull("资源类型名称不为空", LogModuleType.ResourceLog);
            SysAssert.CheckCondition(resourceTypeID <= 0, "资源类型标识不为空", LogModuleType.ResourceLog);
            WTF.Resource.Entity.Sys_ResourceType type = this.CurrentEntities.sys_resourcetype.FirstOrDefault<WTF.Resource.Entity.Sys_ResourceType>(p => p.ResourceTypeID == resourceTypeID);
            SysAssert.CheckCondition(type == null, "资源类型不存在,请在资源申请处添加", LogModuleType.ResourceLog);
            WTF.Resource.Entity.Sys_Resource entity = new WTF.Resource.Entity.Sys_Resource
            {
                ResourceID = ResourceID,
                ResourceName = resourceName,
                VerCount = 0,
                CreateDate = DateTime.Now
            };
            type.Sys_Resource.Add(entity);
            this.CurrentEntities.SaveChanges();
            return entity.ResourceID;
        }

        public void InsertResourceType(WTF.Resource.Entity.Sys_ResourceType objResourceType)
        {
            objResourceType.ResourceTypeCode.CheckIsNull("请输入资源类型代码", LogModuleType.ResourceLog);
            SysAssert.CheckCondition(this.CurrentEntities.sys_resourcetype.Any<WTF.Resource.Entity.Sys_ResourceType>(p => p.ResourceTypeCode == objResourceType.ResourceTypeCode), "输入的资源类型代码已经存在", LogModuleType.ResourceLog);
            objResourceType.ResourceTypeName.CheckIsNull("请输入资源类型名称", LogModuleType.ResourceLog);
            this.CurrentEntities.AddTosys_resourcetype(objResourceType);
            this.CurrentEntities.SaveChanges();
        }

        public ResourceInfo InsertResourceVer(string resourceID, int verNo, string account, HttpPostedFile objPostedFile, string remark)
        {
            string fileName = Path.GetFileName(objPostedFile.FileName);
            int contentLength = objPostedFile.ContentLength;
            string contentType = objPostedFile.ContentType;
            string resourceVerID = Guid.NewGuid().ToString();
            fileName.CheckIsNull("资源文件名为空", LogModuleType.ResourceLog);
            contentType.CheckIsNull("资源文件类型为空", LogModuleType.ResourceLog);
            account.CheckIsNull("帐号为空", LogModuleType.ResourceLog);
            SysAssert.CheckCondition(verNo <= 0, "版本号小于零", LogModuleType.ResourceLog);
            this.DeleteResource(resourceID, verNo.ToString());
            SysAssert.CheckCondition(resourceID.IsNull(), "资源类型标识不为空", LogModuleType.ResourceLog);
            WTF.Resource.Entity.Sys_Resource objResource = this.CurrentEntities.sys_resource.Where(" it.ResourceID == '" + resourceID.ToString() + "'", new ObjectParameter[0]).Include("Sys_ResourceType").First<WTF.Resource.Entity.Sys_Resource>();
            DateTime now = DateTime.Now;
            WTF.Resource.Entity.Sys_ResourcePath path = this.CurrentEntities.sys_resourcepath.First<WTF.Resource.Entity.Sys_ResourcePath>(s => s.ResourcePathID == objResource.Sys_ResourceType.ResourcePathID);
            string resourceGUIDFileName = Guid.NewGuid() + Path.GetExtension(fileName);
            string pathFormatValue = this.GetResourcePathFormatValue(now, resourceVerID, objResource.Sys_ResourceType.PathFormatCodeType);
            string resourcePath = this.GetResourceURL(path.VirtualName, (AccessModeCodeType)objResource.Sys_ResourceType.AccessModeCodeType, objResource.Sys_ResourceType.ResourceTypeCode, pathFormatValue, resourceGUIDFileName, fileName, resourceVerID);
            string fileNamePath = this.GetresourceFullFileNamePath(path.StoragePath, (AccessModeCodeType)objResource.Sys_ResourceType.AccessModeCodeType, objResource.Sys_ResourceType.ResourceTypeCode, pathFormatValue, resourceGUIDFileName, fileName, true);
            string str8 = this.GetDirectoryPath((AccessModeCodeType)objResource.Sys_ResourceType.AccessModeCodeType, objResource.Sys_ResourceType.ResourceTypeCode, pathFormatValue, resourceGUIDFileName, fileName);
            WTF.Resource.Entity.Sys_ResourceVer entity = new WTF.Resource.Entity.Sys_ResourceVer
            {
                ResourceVerID = resourceVerID,
                ResourceSize = contentLength,
                ResourcePath = resourcePath,
                DictionaryPath = str8,
                ResourceGUIDFileName = resourceGUIDFileName,
                ResourceFileName = fileName,
                VerNo = verNo,
                CreateDateTime = now,
                ContentType = contentType,
                UpdateDateTime = now,
                Account = account,
                RefCount = 1,
                Remark = remark
            };
            objResource.VerCount++;
            if (objResource.Sys_ResourceType.StorageType != 1)
            {
                Stream inputStream = objPostedFile.InputStream;
                byte[] fileByteData = new BinaryReader(inputStream).ReadBytes((int)inputStream.Length);
                WTF.Resource.Entity.Sys_ResourceData data = new WTF.Resource.Entity.Sys_ResourceData
                {
                    ResourceVerID = resourceVerID,
                    ResourceData = fileByteData,
                    FileExtension = Path.GetExtension(fileName)
                };
                entity.Sys_ResourceData = data;
                objResource.Sys_ResourceVer.Add(entity);
                this.CurrentEntities.SaveChanges();
                if (objResource.Sys_ResourceType.StorageType != 2)
                {
                    fileByteData.CreateFile(fileNamePath, true);
                }
                if (inputStream != null)
                {
                    inputStream.Close();
                }
            }
            else
            {
                objResource.Sys_ResourceVer.Add(entity);
                this.CurrentEntities.SaveChanges();
                fileNamePath.CreateFileDirectory();
                objPostedFile.SaveAs(fileNamePath);
            }
            return new ResourceInfo(resourceID, resourceVerID, verNo, resourcePath, fileNamePath, fileName);
        }

        public ResourceInfo InsertResourceVer(string resourceID, int beginVerNo, int endVerNo, string account, HttpPostedFile objPostedFile, string remark)
        {
            int verNo = this.GetResourceMaxVerNo(resourceID, beginVerNo, endVerNo);
            return this.InsertResourceVer(resourceID, Path.GetFileName(objPostedFile.FileName), objPostedFile.ContentLength, objPostedFile.ContentType, account, objPostedFile.InputStream, verNo, remark);
        }

        public ResourceInfo InsertResourceVer(string resourceID, string resourceFileName, int resourceSize, string contentType, string account, Stream objStream, int verNo, string remark)
        {
            string resourceVerID = Guid.NewGuid().ToString();
            resourceFileName.CheckIsNull("资源文件名为空", LogModuleType.ResourceLog);
            contentType.CheckIsNull("资源文件类型为空", LogModuleType.ResourceLog);
            account.CheckIsNull("帐号为空", LogModuleType.ResourceLog);
            SysAssert.CheckCondition((objStream == null) || (objStream.Length == 0L), "资源流为空", LogModuleType.ResourceLog);
            SysAssert.CheckCondition(verNo <= 0, "版本号小于零", LogModuleType.ResourceLog);
            this.DeleteResource(resourceID, verNo.ToString());
            SysAssert.CheckCondition(resourceID.IsNull(), "资源类型标识不为空", LogModuleType.ResourceLog);
            WTF.Resource.Entity.Sys_Resource objResource = this.CurrentEntities.sys_resource.Where(" it.ResourceID == '" + resourceID.ToString() + "'", new ObjectParameter[0]).Include("Sys_ResourceType").First<WTF.Resource.Entity.Sys_Resource>();
            DateTime now = DateTime.Now;
            WTF.Resource.Entity.Sys_ResourcePath path = this.CurrentEntities.sys_resourcepath.First<WTF.Resource.Entity.Sys_ResourcePath>(s => s.ResourcePathID == objResource.Sys_ResourceType.ResourcePathID);
            string resourceGUIDFileName = Guid.NewGuid() + Path.GetExtension(resourceFileName);
            string pathFormatValue = this.GetResourcePathFormatValue(now, resourceVerID, objResource.Sys_ResourceType.PathFormatCodeType);
            string resourcePath = this.GetResourceURL(path.VirtualName, (AccessModeCodeType)objResource.Sys_ResourceType.AccessModeCodeType, objResource.Sys_ResourceType.ResourceTypeCode, pathFormatValue, resourceGUIDFileName, resourceFileName, resourceVerID);
            string fileNamePath = this.GetresourceFullFileNamePath(path.StoragePath, (AccessModeCodeType)objResource.Sys_ResourceType.AccessModeCodeType, objResource.Sys_ResourceType.ResourceTypeCode, pathFormatValue, resourceGUIDFileName, resourceFileName, true);
            string str6 = this.GetDirectoryPath((AccessModeCodeType)objResource.Sys_ResourceType.AccessModeCodeType, objResource.Sys_ResourceType.ResourceTypeCode, pathFormatValue, resourceGUIDFileName, resourceFileName);
            WTF.Resource.Entity.Sys_ResourceVer entity = new WTF.Resource.Entity.Sys_ResourceVer
            {
                ResourceVerID = resourceVerID,
                ResourceSize = resourceSize,
                ResourcePath = resourcePath,
                DictionaryPath = str6,
                ResourceGUIDFileName = resourceGUIDFileName,
                ResourceFileName = resourceFileName,
                VerNo = verNo,
                CreateDateTime = now,
                ContentType = contentType,
                UpdateDateTime = now,
                Account = account,
                RefCount = 1,
                Remark = remark
            };
            objResource.VerCount++;
            byte[] fileByteData = new BinaryReader(objStream).ReadBytes((int)objStream.Length);
            if (objResource.Sys_ResourceType.StorageType != 1)
            {
                WTF.Resource.Entity.Sys_ResourceData data = new WTF.Resource.Entity.Sys_ResourceData
                {
                    ResourceVerID = resourceVerID,
                    ResourceData = fileByteData,
                    FileExtension = Path.GetExtension(resourceFileName)
                };
                entity.Sys_ResourceData = data;
            }
            objResource.Sys_ResourceVer.Add(entity);
            this.CurrentEntities.SaveChanges();
            if (objResource.Sys_ResourceType.StorageType != 2)
            {
                fileByteData.CreateFile(fileNamePath, true);
            }
            if (objStream != null)
            {
                objStream.Close();
            }
            return new ResourceInfo(resourceID, resourceVerID, verNo, resourcePath, fileNamePath, resourceFileName);
        }

        public ResourceInfo InsertResourceVer(string resourceID, int beginVerNo, int endVerNo, string resourceFileName, int resourceSize, string contentType, string account, Stream objStream, string remark)
        {
            int verNo = this.GetResourceMaxVerNo(resourceID, beginVerNo, endVerNo);
            string str = Guid.NewGuid().ToString();
            return this.InsertResourceVer(resourceID, resourceFileName, resourceSize, contentType, account, objStream, verNo, remark);
        }

        public void SaveChanges()
        {
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateDirectoryPath()
        {
            ObjectQuery<WTF.Resource.Entity.Sys_Resource> query = this.CurrentEntities.sys_resource.Include("Sys_ResourceType").Include("Sys_ResourceVer");
            foreach (WTF.Resource.Entity.Sys_Resource resource in query)
            {
                foreach (WTF.Resource.Entity.Sys_ResourceVer ver in resource.Sys_ResourceVer)
                {
                    ver.DictionaryPath = this.GetDirectoryPath((AccessModeCodeType)resource.Sys_ResourceType.AccessModeCodeType, resource.Sys_ResourceType.ResourceTypeCode, this.GetResourcePathFormatValue(ver.UpdateDateTime, ver.ResourceVerID, resource.Sys_ResourceType.PathFormatCodeType), ver.ResourceGUIDFileName, ver.ResourceFileName);
                }
            }
            this.CurrentEntities.SaveChanges();
        }

        public ResourceEntities CurrentEntities
        {
            get
            {
                if (this.objCurrentEntities == null)
                {
                    this.objCurrentEntities = new ResourceEntities(EntitiesHelper.GetConnectionString<ResourceEntities>("WTF.File.ConnectionString"));
                }
                return this.objCurrentEntities;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_Resource> Sys_Resource
        {
            get
            {
                return this.CurrentEntities.sys_resource;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_ResourceData> Sys_ResourceData
        {
            get
            {
                return this.CurrentEntities.sys_resourcedata;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_ResourcePath> Sys_ResourcePath
        {
            get
            {
                return this.CurrentEntities.sys_resourcepath;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_ResourceRestrict> Sys_ResourceRestrict
        {
            get
            {
                return this.CurrentEntities.sys_resourcerestrict;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_ResourceRestrictPic> Sys_ResourceRestrictPic
        {
            get
            {
                return this.CurrentEntities.sys_resourcerestrictpic;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_ResourceType> Sys_ResourceType
        {
            get
            {
                return this.CurrentEntities.sys_resourcetype;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_ResourceVer> Sys_ResourceVer
        {
            get
            {
                return this.CurrentEntities.sys_resourcever;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.Sys_WaterImage> Sys_WaterImage
        {
            get
            {
                return this.CurrentEntities.sys_waterimage;
            }
        }
    }
}

