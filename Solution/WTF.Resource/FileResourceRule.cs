namespace WTF.Resource
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Resource.Entity;
    using System;
    using System.Data.Objects;
    using System.IO;
    using System.Linq;
    using System.Web;

    public class FileResourceRule
    {
        private FileResourceEntities objCurrentEntities = null;

        public void DeletefileresourceByKey(string primaryKey)
        {
            this.CurrentEntities.resource_fileresource.DeleteDataPrimaryKey<WTF.Resource.Entity.resource_fileresource>(primaryKey);
        }

        public void DeletefilerestrictByKey(string primaryKey)
        {
            this.CurrentEntities.resource_filerestrict.DeleteDataPrimaryKey<WTF.Resource.Entity.resource_filerestrict>(primaryKey);
        }

        public void DeletefilerestrictpicByKey(string primaryKey)
        {
            this.CurrentEntities.resource_filerestrictpic.DeleteDataPrimaryKey<WTF.Resource.Entity.resource_filerestrictpic>(primaryKey);
        }

        public void DeletefilestoragepathByKey(string primaryKey)
        {
            this.CurrentEntities.resource_filestoragepath.DeleteDataPrimaryKey<WTF.Resource.Entity.resource_filestoragepath>(primaryKey);
        }

        public void Insertfilehistory(WTF.Resource.Entity.resource_filehistory objresource_filehistory)
        {
            this.CurrentEntities.AddToresource_filehistory(objresource_filehistory);
            this.CurrentEntities.SaveChanges();
        }

        public void Insertfileresource(WTF.Resource.Entity.resource_fileresource objresource_fileresource)
        {
            objresource_fileresource.FileResourceName.CheckIsNull<string>("请输入文件资源名称", "ResourceLog");
            objresource_fileresource.FileResourceCode.CheckIsNull<string>("请输入文件资源代码", "ResourceLog");
            this.CurrentEntities.AddToresource_fileresource(objresource_fileresource);
            this.CurrentEntities.SaveChanges();
        }

        public void Insertfilerestrict(WTF.Resource.Entity.resource_filerestrict objresource_filerestrict)
        {
            objresource_filerestrict.RestrictName.CheckIsNull<string>("请输入限制名称", "ResourceLog");
            objresource_filerestrict.RestrictCode.CheckIsNull<string>("请输入限制码", "ResourceLog");
            this.CurrentEntities.AddToresource_filerestrict(objresource_filerestrict);
            this.CurrentEntities.SaveChanges();
        }

        public void Insertfilerestrictpic(WTF.Resource.Entity.resource_filerestrictpic objresource_filerestrictpic)
        {
            this.CurrentEntities.AddToresource_filerestrictpic(objresource_filerestrictpic);
            this.CurrentEntities.SaveChanges();
        }

        public void Insertfilestoragepath(WTF.Resource.Entity.resource_filestoragepath objresource_filestoragepath)
        {
            objresource_filestoragepath.StoragePathName.CheckIsNull<string>("请输入存储名称", "ResourceLog");
            this.CurrentEntities.AddToresource_filestoragepath(objresource_filestoragepath);
            this.CurrentEntities.SaveChanges();
        }

        public ResourceInfo SaveResource(WTF.Resource.Entity.resource_filerestrict objresource_filerestrict, HttpPostedFile objPostedFile)
        {
            string fileName = Path.GetFileName(objPostedFile.FileName);
            int contentLength = objPostedFile.ContentLength;
            string contentType = objPostedFile.ContentType;
            SysAssert.CheckCondition(objresource_filerestrict == null, "传入的资源配置不存在", LogModuleType.ResourceLog);
            string fileResourceCode = objresource_filerestrict.resource_fileresource.FileResourceCode;
            DateTime now = DateTime.Now;
            string resourceVerID = Guid.NewGuid().ToString();
            WTF.Resource.Entity.resource_filestoragepath _filestoragepath = objresource_filerestrict.resource_filestoragepath;
            string resourceGUIDFileName = resourceVerID + Path.GetExtension(fileName);
            string pathFormatValue = WTF.Resource.ResourceHelper.GetResourcePathFormatValue(now, resourceVerID, objresource_filerestrict.PathFormatCodeType);
            string resourcePath = WTF.Resource.ResourceHelper.GetResourceURL(_filestoragepath.VirtualName, (AccessModeCodeType) objresource_filerestrict.AccessModeCodeType, fileResourceCode, pathFormatValue, resourceGUIDFileName, fileName, resourceVerID);
            string str8 = WTF.Resource.ResourceHelper.GetresourceFullFileNamePath(_filestoragepath.StoragePath, (AccessModeCodeType) objresource_filerestrict.AccessModeCodeType, fileResourceCode, pathFormatValue, resourceGUIDFileName, fileName, true);
            string str9 = WTF.Resource.ResourceHelper.GetDirectoryPath((AccessModeCodeType) objresource_filerestrict.AccessModeCodeType, fileResourceCode, pathFormatValue, resourceGUIDFileName, fileName);
            str8.CreateFileDirectory();
            objPostedFile.SaveAs(str8);
            return new ResourceInfo(Guid.Empty.ToString(), resourceVerID, 0, resourcePath, str8, fileName);
        }

        public ResourceInfo SaveResource(WTF.Resource.Entity.resource_filerestrict objresource_filerestrict, string resourceFileName, Stream objStream)
        {
            SysAssert.CheckCondition(objresource_filerestrict == null, "传入的资源配置不存在", LogModuleType.ResourceLog);
            SysAssert.CheckCondition((objStream == null) || (objStream.Length == 0L), "资源流为空", LogModuleType.ResourceLog);
            string fileResourceCode = objresource_filerestrict.resource_fileresource.FileResourceCode;
            DateTime now = DateTime.Now;
            string resourceVerID = Guid.NewGuid().ToString();
            WTF.Resource.Entity.resource_filestoragepath _filestoragepath = objresource_filerestrict.resource_filestoragepath;
            string resourceGUIDFileName = resourceVerID + Path.GetExtension(resourceFileName);
            string randomSwitchString = _filestoragepath.VirtualName.GetRandomSwitchString();
            string pathFormatValue = WTF.Resource.ResourceHelper.GetResourcePathFormatValue(now, resourceVerID, objresource_filerestrict.PathFormatCodeType);
            string resourcePath = WTF.Resource.ResourceHelper.GetResourceURL(randomSwitchString, (AccessModeCodeType) objresource_filerestrict.AccessModeCodeType, fileResourceCode, pathFormatValue, resourceGUIDFileName, resourceFileName, resourceVerID);
            string fileNamePath = WTF.Resource.ResourceHelper.GetresourceFullFileNamePath(_filestoragepath.StoragePath, (AccessModeCodeType) objresource_filerestrict.AccessModeCodeType, fileResourceCode, pathFormatValue, resourceGUIDFileName, resourceFileName, true);
            string str8 = WTF.Resource.ResourceHelper.GetDirectoryPath((AccessModeCodeType) objresource_filerestrict.AccessModeCodeType, fileResourceCode, pathFormatValue, resourceGUIDFileName, resourceFileName);
            byte[] bytes = new BinaryReader(objStream).ReadBytes((int) objStream.Length);
            string str9 = "";
            if (objresource_filerestrict.IsMd5 == 1)
            {
                str9 = bytes.MD5Encrypt();
            }
            bytes.CreateFile(fileNamePath, true);
            if (objStream != null)
            {
                objStream.Close();
            }
            return new ResourceInfo(Guid.Empty.ToString(), resourceVerID, 0, resourcePath, fileNamePath, resourceFileName) { Md5Value = str9 };
        }

        public ResourceInfo SaveResource(string ResourceCode, string RestrictCode, HttpPostedFile objPostedFile)
        {
            ResourceCode.CheckIsNull("请设置资源类型编码ResourceCode", LogModuleType.ResourceLog);
            RestrictCode.CheckIsNull("请设置RestrictCode", LogModuleType.ResourceLog);
            WTF.Resource.Entity.resource_filerestrict _filerestrict = this.resource_filerestrict.Where("it.RestrictCode='" + RestrictCode + "' and it.resource_fileresource.FileResourceCode='" + ResourceCode + "'", new ObjectParameter[0]).Include("resource_filestoragepath").FirstOrDefault<WTF.Resource.Entity.resource_filerestrict>();
            if (_filerestrict == null)
            {
                SysAssert.InfoHintAssert("找不到此文件配置");
            }
            return this.SaveResource(_filerestrict, objPostedFile);
        }

        public ResourceInfo SaveResource(string ResourceCode, string RestrictCode, string resourceFileName, Stream objStream)
        {
            ResourceCode.CheckIsNull("请设置资源类型编码ResourceCode", LogModuleType.ResourceLog);
            RestrictCode.CheckIsNull("请设置RestrictCode", LogModuleType.ResourceLog);
            WTF.Resource.Entity.resource_filerestrict _filerestrict = this.resource_filerestrict.Where("it.RestrictCode='" + RestrictCode + "' and it.resource_fileresource.FileResourceCode='" + ResourceCode + "'", new ObjectParameter[0]).Include("resource_filestoragepath").FirstOrDefault<WTF.Resource.Entity.resource_filerestrict>();
            if (_filerestrict == null)
            {
                SysAssert.InfoHintAssert("找不到此文件配置");
            }
            return this.SaveResource(_filerestrict, resourceFileName, objStream);
        }

        public void Updatefileresource(WTF.Resource.Entity.resource_fileresource objresource_fileresource)
        {
            objresource_fileresource.FileResourceName.CheckIsNull<string>("请输入文件资源名称", "ResourceLog");
            objresource_fileresource.FileResourceCode.CheckIsNull<string>("请输入文件资源代码", "ResourceLog");
            this.CurrentEntities.SaveChanges();
        }

        public void Updatefilerestrict(WTF.Resource.Entity.resource_filerestrict objresource_filerestrict)
        {
            objresource_filerestrict.RestrictName.CheckIsNull<string>("请输入限制名称", "ResourceLog");
            objresource_filerestrict.RestrictCode.CheckIsNull<string>("请输入限制码", "ResourceLog");
            this.CurrentEntities.SaveChanges();
        }

        public void Updatefilerestrictpic(WTF.Resource.Entity.resource_filerestrictpic objresource_filerestrictpic)
        {
            this.CurrentEntities.SaveChanges();
        }

        public void Updatefilestoragepath(WTF.Resource.Entity.resource_filestoragepath objresource_filestoragepath)
        {
            objresource_filestoragepath.StoragePathName.CheckIsNull<string>("请输入存储名称", "ResourceLog");
            this.CurrentEntities.SaveChanges();
        }

        public FileResourceEntities CurrentEntities
        {
            get
            {
                if (this.objCurrentEntities == null)
                {
                    this.objCurrentEntities = new FileResourceEntities(EntitiesHelper.GetConnectionString<FileResourceEntities>("WTF.File.ConnectionString"));
                }
                return this.objCurrentEntities;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.resource_filehistory> resource_filehistory
        {
            get
            {
                return this.CurrentEntities.resource_filehistory;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.resource_fileresource> resource_fileresource
        {
            get
            {
                return this.CurrentEntities.resource_fileresource;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.resource_filerestrict> resource_filerestrict
        {
            get
            {
                return this.CurrentEntities.resource_filerestrict;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.resource_filerestrictpic> resource_filerestrictpic
        {
            get
            {
                return this.CurrentEntities.resource_filerestrictpic;
            }
        }

        public ObjectQuery<WTF.Resource.Entity.resource_filestoragepath> resource_filestoragepath
        {
            get
            {
                return this.CurrentEntities.resource_filestoragepath;
            }
        }
    }
}

