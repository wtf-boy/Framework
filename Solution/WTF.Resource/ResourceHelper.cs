namespace WTF.Resource
{
    using WTF.Framework;
    using WTF.Resource.Entity;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    public class ResourceHelper
    {
        public static string GetDirectoryPath(AccessModeCodeType accessModeCodeValue, string resourceTypeCode, string pathFormatValue, string resourceGUIDFileName, string resourceFileName)
        {
            string str = GetResourceDirectoryPath(accessModeCodeValue, resourceTypeCode, pathFormatValue, resourceGUIDFileName);
            if (accessModeCodeValue == AccessModeCodeType.VirtualOriginalAccess)
            {
                return (str + @"\" + resourceFileName);
            }
            return (str + @"\" + resourceGUIDFileName);
        }

        public static string GetFtpFullFileNamePath(resource_filerestrict objresource_filerestrict, string resourceFileName)
        {
            DateTime now = DateTime.Now;
            string fileResourceCode = objresource_filerestrict.resource_fileresource.FileResourceCode;
            string resourceVerID = Guid.NewGuid().ToString();
            resource_filestoragepath _filestoragepath = objresource_filerestrict.resource_filestoragepath;
            string resourceGUIDFileName = resourceVerID + Path.GetExtension(resourceFileName);
            string pathFormatValue = GetResourcePathFormatValue(now, resourceVerID, objresource_filerestrict.PathFormatCodeType);
            return GetresourceFullFileNamePath(_filestoragepath.StoragePath, (AccessModeCodeType) objresource_filerestrict.AccessModeCodeType, fileResourceCode, pathFormatValue, resourceGUIDFileName, resourceFileName, false).Replace('\\', '/');
        }

        public static string GetResourceDirectoryPath(AccessModeCodeType accessModeCodeValue, string resourceTypeCode, string pathFormatValue, string resourceGUIDFileName)
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

        public static string GetresourceFullFileNamePath(string StoragePath, AccessModeCodeType accessModeCodeValue, string resourceTypeCode, string pathFormatValue, string resourceGUIDFileName, string resourceFileName, bool isCreateDirectory = true)
        {
            string fileName = "";
            if (string.IsNullOrWhiteSpace(StoragePath))
            {
                fileName = GetResourceDirectoryPath(accessModeCodeValue, resourceTypeCode, pathFormatValue, resourceGUIDFileName);
            }
            else
            {
                fileName = Path.Combine(StoragePath, GetResourceDirectoryPath(accessModeCodeValue, resourceTypeCode, pathFormatValue, resourceGUIDFileName));
            }
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

        public static string GetResourcePathFormatValue(DateTime createDateTime, string resourceVerID, int pathFormatCodeID)
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

        public static string GetResourceURL(string VirtualName, AccessModeCodeType accessModeCodeValue, string resourceTypeCode, string pathFormatValue, string resourceGUIDFileName, string resourceFileName, string resourceVerID)
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
    }
}

