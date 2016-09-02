namespace WTF.Framework
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;

    public static class FileHelper
    {
        private static ReaderWriterLockSlimHelper objFileCacheWriterLockSlimHelper = new ReaderWriterLockSlimHelper();
        private const string PATH_SPLIT_CHAR = @"\";

        public static bool CopyFile(this string sourceFile, string targetFile, bool overwrite = true)
        {
            bool flag = false;
            FileInfo info = new FileInfo(sourceFile);
            try
            {
                if (flag)
                {
                    info.CopyTo(targetFile, overwrite);
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static void CopyFiles(this string sourceDir, string targetDir, bool overWrite, bool copySubDir = false)
        {
            foreach (string str in Directory.GetFiles(sourceDir))
            {
                string path = Path.Combine(targetDir, str.Substring(str.LastIndexOf(@"\") + 1));
                if (System.IO.File.Exists(path))
                {
                    if (overWrite)
                    {
                        System.IO.File.SetAttributes(path, FileAttributes.Normal);
                        System.IO.File.Copy(str, path, overWrite);
                    }
                }
                else
                {
                    System.IO.File.Copy(str, path, overWrite);
                }
            }
            if (copySubDir)
            {
                foreach (string str3 in Directory.GetDirectories(sourceDir))
                {
                    string str4 = Path.Combine(targetDir, str3.Substring(str3.LastIndexOf(@"\") + 1));
                    if (!Directory.Exists(str4))
                    {
                        Directory.CreateDirectory(str4);
                    }
                    str3.CopyFiles(str4, overWrite, true);
                }
            }
        }

        private static void CreateBranch(string targetDir, XmlElement xmlNode, XmlDocument myDocument)
        {
            XmlElement element;
            foreach (string str in Directory.GetFiles(targetDir))
            {
                element = myDocument.CreateElement("File");
                element.InnerText = str.Substring(str.LastIndexOf(@"\") + 1);
                xmlNode.AppendChild(element);
            }
            foreach (string str2 in Directory.GetDirectories(targetDir))
            {
                element = myDocument.CreateElement("Directory");
                element.SetAttribute("Name", str2.Substring(str2.LastIndexOf(@"\") + 1));
                xmlNode.AppendChild(element);
                CreateBranch(str2, element, myDocument);
            }
        }

        public static void CreateDirectory(this string targetDir)
        {
            DirectoryInfo info = new DirectoryInfo(targetDir);
            if (!info.Exists)
            {
                info.Create();
            }
        }

        public static bool CreateFile(this HttpWebResponse httpWebResponse, string fileNamePath)
        {
            Stream input = null;
            bool flag;
            try
            {
                input = httpWebResponse.GetResponseStream();
                BinaryReader reader = new BinaryReader(input);
                FileStream stream2 = new FileStream(fileNamePath, FileMode.Create, FileAccess.Write);
                stream2.Write(reader.ReadBytes((int) httpWebResponse.ContentLength), 0, (int) httpWebResponse.ContentLength);
                stream2.Close();
                reader.Close();
                input.Close();
                flag = true;
            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
            }
            return flag;
        }

        public static bool CreateFile(this byte[] fileByteData, string fileNamePath, bool isAutoClearByte = true)
        {
            fileNamePath.CreateFileDirectory();
            bool flag = true;
            FileStream output = null;
            BinaryWriter writer = null;
            try
            {
                output = new FileStream(fileNamePath, FileMode.Create, FileAccess.Write);
                writer = new BinaryWriter(output);
                writer.Write(fileByteData);
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (output != null)
                {
                    output.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }
            if (isAutoClearByte)
            {
                Array.Clear(fileByteData, 0, fileByteData.Length);
            }
            return flag;
        }

        public static void CreateFileDirectory(this string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            if (!info.Directory.Exists)
            {
                info.Directory.Create();
            }
        }

        public static void CreateFileDirectory(this string parentDir, string subDirName)
        {
            (parentDir + @"\" + subDirName).CreateDirectory();
        }

        public static XmlDocument CreateXml(this string targetDir)
        {
            XmlElement element2;
            XmlDocument myDocument = new XmlDocument();
            XmlDeclaration newChild = myDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            myDocument.AppendChild(newChild);
            XmlElement element = myDocument.CreateElement(targetDir.Substring(targetDir.LastIndexOf(@"\") + 1));
            myDocument.AppendChild(element);
            foreach (string str in Directory.GetFiles(targetDir))
            {
                element2 = myDocument.CreateElement("File");
                element2.InnerText = str.Substring(str.LastIndexOf(@"\") + 1);
                element.AppendChild(element2);
            }
            foreach (string str2 in Directory.GetDirectories(targetDir))
            {
                element2 = myDocument.CreateElement("Directory");
                element2.SetAttribute("Name", str2.Substring(str2.LastIndexOf(@"\") + 1));
                element.AppendChild(element2);
                CreateBranch(str2, element2, myDocument);
            }
            return myDocument;
        }

        public static void DeleteDirectory(this string targetDir)
        {
            DirectoryInfo info = new DirectoryInfo(targetDir);
            if (info.Exists)
            {
                targetDir.DeleteFiles(true);
                info.Delete(true);
            }
        }

        public static bool DeleteFile(this string filePath)
        {
            bool flag = false;
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    flag = true;
                }
                catch
                {
                }
                return flag;
            }
            return true;
        }

        public static void DeleteFiles(this string targetDir, bool delSubDir = false)
        {
            foreach (string str in Directory.GetFiles(targetDir))
            {
                System.IO.File.SetAttributes(str, FileAttributes.Normal);
                System.IO.File.Delete(str);
            }
            if (delSubDir)
            {
                DirectoryInfo info = new DirectoryInfo(targetDir);
                foreach (DirectoryInfo info2 in info.GetDirectories())
                {
                    info2.FullName.DeleteFiles(true);
                    info2.Delete();
                }
            }
        }

        public static void DeleteSubDirectory(this string targetDir)
        {
            foreach (string str in Directory.GetDirectories(targetDir))
            {
                str.DeleteDirectory();
            }
        }

        public static string DisplayFileSize(this long pSize)
        {
            float num = pSize;
            if (pSize < 0x400L)
            {
                return (pSize.ToString() + "字节");
            }
            if ((pSize >= 0x400L) && (pSize <= 0x100000L))
            {
                num /= 1024f;
                return (string.Format("{0:F2}", num) + "KB");
            }
            if ((pSize >= 0x100000L) && (pSize <= 0x40000000L))
            {
                num /= 1048576f;
                return (string.Format("{0:F2}", num) + "MB");
            }
            if (pSize >= 0x40000000L)
            {
                num /= 1.073742E+09f;
                return (string.Format("{0:F2}", num) + "GB");
            }
            return "";
        }

        public static bool Download(this string remoteFile, string saveToFile)
        {
            byte[] buffer = null;
            int num = remoteFile.ReadFileToByteData(ref buffer);
            if (num < 0)
            {
                return false;
            }
            string extension = Path.GetExtension(remoteFile);
            string str = saveToFile;
            if (saveToFile.ToUpper().LastIndexOf(extension.ToUpper()) != (saveToFile.Length - extension.Length))
            {
                str = str + extension;
            }
            str = HttpUtility.UrlEncode(str);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AppendHeader("Accept-Ranges", "bytes");
            HttpContext.Current.Response.AppendHeader("Accept-Length", num.ToString());
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + str);
            HttpContext.Current.Response.BinaryWrite(buffer);
            HttpContext.Current.Response.End();
            return true;
        }

        public static Encoding FileGetEncoding(this string file)
        {
            try
            {
                FileStream stream = new FileStream(file, FileMode.Open);
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                if (stream.Length > 0L)
                {
                    num = stream.ReadByte();
                }
                if (stream.Length > 1L)
                {
                    num2 = stream.ReadByte();
                }
                if (stream.Length > 2L)
                {
                    num3 = stream.ReadByte();
                }
                stream.Close();
                if ((num == 0xff) && (num2 == 0xfe))
                {
                    return Encoding.Unicode;
                }
                if ((num == 0xfe) && (num2 == 0xff))
                {
                    return Encoding.BigEndianUnicode;
                }
                if (((num == 0xef) && (num2 == 0xbb)) && (num3 == 0xbf))
                {
                    return Encoding.UTF8;
                }
                return Encoding.Default;
            }
            catch (Exception)
            {
                return Encoding.Default;
            }
        }

        public static Stream FileReader(this StreamReader sr)
        {
            string pattern = @"[\x00-\x08\x0b-\x0c\x0e-\x1f]";
            string s = Regex.Replace(sr.ReadToEnd(), pattern, "");
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            return new MemoryStream(bytes, 0, bytes.Length);
        }

        public static bool FileShareConnectState(this string path, string userName, string passWord)
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string str = "net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                process.StandardInput.WriteLine(str);
                process.StandardInput.WriteLine("exit");
                while (!process.HasExited)
                {
                    process.WaitForExit(0x3e8);
                }
                string str2 = process.StandardError.ReadToEnd();
                process.StandardError.Close();
                if (!string.IsNullOrEmpty(str2))
                {
                    throw new Exception(str2);
                }
                return true;
            }
            catch
            {
                process.Close();
                process.Dispose();
            }
            finally
            {
                process.Close();
                process.Dispose();
            }
            return false;
        }

        public static string FileStreamReader(this StreamReader sr)
        {
            string pattern = @"[\x00-\x08\x0b-\x0c\x0e-\x1f]";
            return Regex.Replace(sr.ReadToEnd(), pattern, "");
        }

        public static bool FileWriteData(this byte[] Buffer, string local_file)
        {
            try
            {
                FileStream stream = new FileStream(local_file, FileMode.Create);
                stream.Write(Buffer, 0, Buffer.Length);
                stream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetFileDirectoryName(this string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            return info.DirectoryName;
        }

        public static string GetFileExtension(this string path)
        {
            return Path.GetExtension(path).Trim(new char[] { '.' });
        }

        public static string GetFileName(this string path)
        {
            return Path.GetFileName(path);
        }

        public static long GetFileSize(this string local_file, string unit)
        {
            if (!System.IO.File.Exists(local_file))
            {
                return -1L;
            }
            try
            {
                FileStream stream = new FileStream(local_file, FileMode.Open);
                long length = stream.Length;
                stream.Close();
                if (unit == "K")
                {
                    return (length / 0x400L);
                }
                if (unit == "M")
                {
                    return ((length / 0x400L) / 0x400L);
                }
                return length;
            }
            catch (Exception)
            {
                return -1L;
            }
        }

        public static bool IsFileName(this string fileName)
        {
            return fileName.IsMatch("^[^\\/\\\\ <> \\*\\?\\: \"\\|]{1,16}\\.\\w{1,5}$");
        }

        public static bool MoveFile(this string source, string target, bool overwrite = true)
        {
            bool flag = false;
            FileInfo info = new FileInfo(source);
            try
            {
                if (overwrite)
                {
                    flag = target.DeleteFile();
                }
                if (flag)
                {
                    info.MoveTo(target);
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static void MoveFiles(this string sourceDir, string targetDir, bool overWrite, bool moveSubDir = false)
        {
            foreach (string str in Directory.GetFiles(sourceDir))
            {
                string path = Path.Combine(targetDir, str.Substring(str.LastIndexOf(@"\") + 1));
                if (System.IO.File.Exists(path))
                {
                    if (overWrite)
                    {
                        System.IO.File.SetAttributes(path, FileAttributes.Normal);
                        System.IO.File.Delete(path);
                        System.IO.File.Move(str, path);
                    }
                }
                else
                {
                    System.IO.File.Move(str, path);
                }
            }
            if (moveSubDir)
            {
                foreach (string str3 in Directory.GetDirectories(sourceDir))
                {
                    string str4 = Path.Combine(targetDir, str3.Substring(str3.LastIndexOf(@"\") + 1));
                    if (!Directory.Exists(str4))
                    {
                        Directory.CreateDirectory(str4);
                    }
                    str3.MoveFiles(str4, overWrite, true);
                    Directory.Delete(str3);
                }
            }
        }

        public static void Postfix(this string postfix, string fileDescription, params string[] postfixStr)
        {
            string str = "";
            int num = 0;
            for (int i = 0; i < postfixStr.Length; i++)
            {
                if (postfix.ToLower() == postfixStr[i].ToLower())
                {
                    num++;
                }
                str = str + postfixStr[i] + " ";
            }
            if (num < 1)
            {
                HttpContext.Current.Response.Write("<script language=javascript>alert('上传" + fileDescription + "限制格式为： " + str + "等格式');history.back(-1);</script>");
                HttpContext.Current.Response.End();
            }
        }

        public static bool PostfixValidate(this string postfix, params string[] postfixStr)
        {
            for (int i = 0; i < postfixStr.Length; i++)
            {
                if (postfix.ToLower() == postfixStr[i].ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static byte[] ReadFileToByteData(this string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return null;
            }
            try
            {
                byte[] buffer = null;
                FileStream stream = new FileStream(path, FileMode.Open);
                long length = stream.Length;
                buffer = new byte[(int) length];
                length = stream.Read(buffer, 0, (int) length);
                stream.Close();
                return buffer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int ReadFileToByteData(this string path, ref byte[] buffer)
        {
            buffer = path.ReadFileToByteData();
            return buffer.Length;
        }

        public static string ReadLocalCacheFile(this string filePath, Encoding objEncoding = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return "";
            }
            string childKey = filePath.MD5Encrypt();
            string fromCache = CacheHelper.GetFromCache<string>("LocalFile", childKey);
            if (fromCache == null)
            {
                if (CacheHelper.GetFromCache("LocalFile", childKey + "NoFile") != null)
                {
                    return "";
                }
                ReaderWriterLockSlim slim = objFileCacheWriterLockSlimHelper.CreateLock(childKey);
                slim.EnterWriteLock();
                try
                {
                    fromCache = CacheHelper.GetFromCache<string>("LocalFile", childKey);
                    if (fromCache == null)
                    {
                        if (CacheHelper.GetFromCache("LocalFile", childKey + "NoFile") != null)
                        {
                            return "";
                        }
                        if (!System.IO.File.Exists(filePath))
                        {
                            true.AddToFileCache("LocalFile", childKey + "NoFile", filePath);
                            return "";
                        }
                        if (objEncoding == null)
                        {
                            fromCache = System.IO.File.ReadAllText(filePath);
                        }
                        else
                        {
                            fromCache = System.IO.File.ReadAllText(filePath, objEncoding);
                        }
                        fromCache.AddToFileCache("LocalFile", childKey, filePath);
                    }
                    return fromCache;
                }
                finally
                {
                    slim.ExitWriteLock();
                }
            }
            return fromCache;
        }

        public static bool SaveAccessory(this HtmlInputFile inputFile, string uploadDirectory, int limitSize, out string fileGuidName)
        {
            string fileName = inputFile.PostedFile.FileName;
            string str2 = string.Empty;
            fileGuidName = string.Empty;
            if (inputFile.PostedFile.ContentLength > limitSize)
            {
                HttpContext.Current.Response.Write("<script language=javascript>alert('上传文件限制最大为" + Convert.ToString((int) (limitSize / 0x400)) + "k');history.back(-1);</script>");
                HttpContext.Current.Response.End();
                return false;
            }
            if (fileName.Trim().Length > 0)
            {
                str2 = Guid.NewGuid().ToString() + "." + fileName.GetFileExtension();
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                string filename = uploadDirectory + str2;
                try
                {
                    inputFile.PostedFile.SaveAs(filename);
                    fileGuidName = str2;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public static bool SaveAccessory(this HtmlInputFile inputFile, string uploadDirectory, int limitSize, string fileName)
        {
            string str = inputFile.PostedFile.FileName;
            uploadDirectory = HttpContext.Current.Server.MapPath("./") + uploadDirectory;
            if (inputFile.PostedFile.ContentLength > limitSize)
            {
                HttpContext.Current.Response.Write("<script language=javascript>alert('上传文件限制最大为" + Convert.ToString((int) (limitSize / 0x400)) + "k');history.back(-1);</script>");
                HttpContext.Current.Response.End();
                return false;
            }
            if (str.Trim().Length > 0)
            {
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                fileName = uploadDirectory + fileName;
                try
                {
                    inputFile.PostedFile.SaveAs(fileName);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public static bool SaveAccessory(this FileUpload inputFile, string uploadDirectory, int limitSize, out string fileGuidName)
        {
            string fileName = inputFile.FileName;
            string str2 = string.Empty;
            fileGuidName = string.Empty;
            if (inputFile.FileContent.Length > limitSize)
            {
                HttpContext.Current.Response.Write("<script language=javascript>alert('上传文件限制最大为" + Convert.ToString((int) (limitSize / 0x400)) + "k');history.back(-1);</script>");
                HttpContext.Current.Response.End();
                return false;
            }
            if (fileName.Trim().Length > 0)
            {
                str2 = Guid.NewGuid().ToString() + "." + fileName.GetFileExtension();
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                string filename = uploadDirectory + str2;
                try
                {
                    inputFile.SaveAs(filename);
                    fileGuidName = str2;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public static bool SaveAccessory(this FileUpload inputFile, string uploadDirectory, int limitSize, string fileName)
        {
            string str = inputFile.FileName;
            uploadDirectory = HttpContext.Current.Server.MapPath("./") + uploadDirectory;
            if (inputFile.FileContent.Length > limitSize)
            {
                HttpContext.Current.Response.Write("<script language=javascript>alert('上传文件限制最大为" + Convert.ToString((int) (limitSize / 0x400)) + "k');history.back(-1);</script>");
                HttpContext.Current.Response.End();
                return false;
            }
            if (str.Trim().Length > 0)
            {
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                fileName = uploadDirectory + fileName;
                try
                {
                    inputFile.SaveAs(fileName);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}

