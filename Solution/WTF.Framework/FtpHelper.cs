namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class FtpHelper
    {
        private string _DirectoryPath;
        private string _ErrorMsg;
        private bool _isDeleteTempFile;
        private string _Password;
        private WebProxy _Proxy;
        private string _UploadTempFile;
        private System.Uri _Uri;
        private string _UserName;
        private FtpWebRequest Request;
        private FtpWebResponse Response;

        public event De_DownloadDataCompleted DownloadDataCompleted;

        public event De_DownloadProgressChanged DownloadProgressChanged;

        public event De_UploadFileCompleted UploadFileCompleted;

        public event De_UploadProgressChanged UploadProgressChanged;

        public FtpHelper()
        {
            this.Request = null;
            this.Response = null;
            this._Proxy = null;
            this._isDeleteTempFile = false;
            this._UploadTempFile = "";
            this._UserName = "anonymous";
            this._Password = "@anonymous";
            this._Uri = null;
            this._Proxy = null;
        }

        public FtpHelper(System.Uri FtpUri, string strUserName, string strPassword)
        {
            this.Request = null;
            this.Response = null;
            this._Proxy = null;
            this._isDeleteTempFile = false;
            this._UploadTempFile = "";
            this._Uri = new System.Uri(FtpUri.GetLeftPart(UriPartial.Authority));
            this._DirectoryPath = FtpUri.AbsolutePath;
            if (!this._DirectoryPath.EndsWith("/"))
            {
                this._DirectoryPath = this._DirectoryPath + "/";
            }
            this._UserName = strUserName;
            this._Password = strPassword;
            this._Proxy = null;
        }

        public FtpHelper(System.Uri FtpUri, string strUserName, string strPassword, WebProxy objProxy)
        {
            this.Request = null;
            this.Response = null;
            this._Proxy = null;
            this._isDeleteTempFile = false;
            this._UploadTempFile = "";
            this._Uri = new System.Uri(FtpUri.GetLeftPart(UriPartial.Authority));
            this._DirectoryPath = FtpUri.AbsolutePath;
            if (!this._DirectoryPath.EndsWith("/"))
            {
                this._DirectoryPath = this._DirectoryPath + "/";
            }
            this._UserName = strUserName;
            this._Password = strPassword;
            this._Proxy = objProxy;
        }

        private string _cutSubstringFromStringWithTrim(ref string s, char c, int startIndex)
        {
            int index = s.IndexOf(c, startIndex);
            string str = s.Substring(0, index);
            s = s.Substring(index).Trim();
            return str;
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (this.DownloadDataCompleted != null)
            {
                this.DownloadDataCompleted(sender, e);
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.DownloadProgressChanged != null)
            {
                this.DownloadProgressChanged(sender, e);
            }
        }

        private void client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (this._isDeleteTempFile)
            {
                if (System.IO.File.Exists(this._UploadTempFile))
                {
                    System.IO.File.SetAttributes(this._UploadTempFile, FileAttributes.Normal);
                    System.IO.File.Delete(this._UploadTempFile);
                }
                this._isDeleteTempFile = false;
            }
            if (this.UploadFileCompleted != null)
            {
                this.UploadFileCompleted(sender, e);
            }
        }

        private void client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (this.UploadProgressChanged != null)
            {
                this.UploadProgressChanged(sender, e);
            }
        }

        public bool ComeoutDirectory()
        {
            if (this._DirectoryPath == "/")
            {
                this.ErrorMsg = "当前目录已经是根目录！";
                throw new Exception("当前目录已经是根目录！");
            }
            char[] separator = new char[] { '/' };
            string[] strArray = this._DirectoryPath.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 1)
            {
                this._DirectoryPath = "/";
            }
            else
            {
                this._DirectoryPath = string.Join("/", strArray, 0, strArray.Length - 1);
            }
            return true;
        }

        public bool CopyFileToAnotherDirectory(string RemoteFile, string DirectoryName)
        {
            bool flag2;
            string directoryPath = this.DirectoryPath;
            try
            {
                byte[] fileBytes = this.DownloadFile(RemoteFile);
                this.GotoDirectory(DirectoryName);
                bool flag = this.UploadFile(fileBytes, RemoteFile, false);
                this.DirectoryPath = directoryPath;
                flag2 = flag;
            }
            catch (Exception exception)
            {
                this.DirectoryPath = directoryPath;
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag2;
        }

        public bool CreateDirectoryPath(string DirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(DirectoryPath))
            {
                throw new ArgumentNullException("DirectoryPath不能为空");
            }
            foreach (string str in DirectoryPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!this.DirectoryExist(str))
                {
                    this.MakeDirectory(str);
                }
                this.GotoDirectory(str);
            }
            return true;
        }

        public void DeleteFile(string RemoteFileName)
        {
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("文件名非法！");
                }
                this.Response = this.Open(new System.Uri(this.Uri.ToString() + RemoteFileName), "DELE");
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public bool DirectoryExist(string RemoteDirectoryName)
        {
            bool flag;
            try
            {
                if (!this.IsValidPathChars(RemoteDirectoryName))
                {
                    throw new Exception("目录名非法！");
                }
                FileStruct[] structArray = this.ListDirectories();
                foreach (FileStruct struct2 in structArray)
                {
                    if (struct2.Name == RemoteDirectoryName)
                    {
                        return true;
                    }
                }
                flag = false;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public byte[] DownloadFile(string RemoteFileName)
        {
            byte[] buffer2;
            try
            {
                bool flag;
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                this.Response = this.Open(new System.Uri(this.Uri.ToString() + RemoteFileName), "RETR");
                Stream responseStream = this.Response.GetResponseStream();
                MemoryStream stream2 = new MemoryStream(0x7d000);
                byte[] buffer = new byte[0x400];
                int count = 0;
                int num2 = 0;
                goto Label_0098;
            Label_006B:
                count = responseStream.Read(buffer, 0, buffer.Length);
                num2 += count;
                if (count == 0)
                {
                    goto Label_009D;
                }
                stream2.Write(buffer, 0, count);
            Label_0098:
                flag = true;
                goto Label_006B;
            Label_009D:
                if (stream2.Length > 0L)
                {
                    return stream2.ToArray();
                }
                buffer2 = null;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return buffer2;
        }

        public bool DownloadFile(string RemoteFileName, string LocalPath)
        {
            return this.DownloadFile(RemoteFileName, LocalPath, RemoteFileName);
        }

        public bool DownloadFile(string RemoteFileName, string LocalPath, string LocalFileName)
        {
            byte[] buffer = null;
            bool flag;
            try
            {
                if (!((this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(LocalFileName)) && this.IsValidPathChars(LocalPath)))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!Directory.Exists(LocalPath))
                {
                    throw new Exception("本地文件路径不存在!");
                }
                string path = Path.Combine(LocalPath, LocalFileName);
                if (System.IO.File.Exists(path))
                {
                    throw new Exception("当前路径下已经存在同名文件！");
                }
                buffer = this.DownloadFile(RemoteFileName);
                if (buffer != null)
                {
                    FileStream stream = new FileStream(path, FileMode.Create);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();
                    stream.Close();
                    return true;
                }
                flag = false;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public void DownloadFileAsync(string RemoteFileName, string LocalFullPath)
        {
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (System.IO.File.Exists(LocalFullPath))
                {
                    throw new Exception("当前路径下已经存在同名文件！");
                }
                MyWebClient client = new MyWebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(this.client_DownloadFileCompleted);
                client.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    client.Proxy = this.Proxy;
                }
                client.DownloadFileAsync(new System.Uri(this.Uri.ToString() + RemoteFileName), LocalFullPath);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public void DownloadFileAsync(string RemoteFileName, string LocalPath, string LocalFileName)
        {
            try
            {
                if (!((this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(LocalFileName)) && this.IsValidPathChars(LocalPath)))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!Directory.Exists(LocalPath))
                {
                    throw new Exception("本地文件路径不存在!");
                }
                string path = Path.Combine(LocalPath, LocalFileName);
                if (System.IO.File.Exists(path))
                {
                    throw new Exception("当前路径下已经存在同名文件！");
                }
                this.DownloadFileAsync(RemoteFileName, path);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        private bool EnterOneSubDirectory(string DirectoryName)
        {
            bool flag;
            try
            {
                if (!((DirectoryName.IndexOf("/") < 0) && this.IsValidPathChars(DirectoryName)))
                {
                    throw new Exception("目录名非法!");
                }
                if ((DirectoryName.Length > 0) && this.DirectoryExist(DirectoryName))
                {
                    if (!DirectoryName.EndsWith("/"))
                    {
                        DirectoryName = DirectoryName + "/";
                    }
                    this._DirectoryPath = this._DirectoryPath + DirectoryName;
                    return true;
                }
                flag = false;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool FileExist(string RemoteFileName)
        {
            bool flag;
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("文件名非法！");
                }
                FileStruct[] structArray = this.ListFiles();
                foreach (FileStruct struct2 in structArray)
                {
                    if (struct2.Name == RemoteFileName)
                    {
                        return true;
                    }
                }
                flag = false;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        ~FtpHelper()
        {
            if (this.Response != null)
            {
                this.Response.Close();
                this.Response = null;
            }
            if (this.Request != null)
            {
                this.Request.Abort();
                this.Request = null;
            }
        }

        private FileStruct[] GetList(string datastring)
        {
            List<FileStruct> list = new List<FileStruct>();
            string[] recordList = datastring.Split(new char[] { '\n' });
            FileListStyle style = this.GuessFileListStyle(recordList);
            foreach (string str in recordList)
            {
                if ((style == FileListStyle.Unknown) || !(str != ""))
                {
                    continue;
                }
                FileStruct item = new FileStruct {
                    Name = ".."
                };
                switch (style)
                {
                    case FileListStyle.UnixStyle:
                        item = this.ParseFileStructFromUnixStyleRecord(str);
                        break;

                    case FileListStyle.WindowsStyle:
                        item = this.ParseFileStructFromWindowsStyleRecord(str);
                        break;
                }
                if ((item.Name != ".") && (item.Name != ".."))
                {
                    list.Add(item);
                }
            }
            return list.ToArray();
        }

        public bool GotoDirectory(string DirectoryName)
        {
            bool flag2;
            string directoryPath = this.DirectoryPath;
            try
            {
                DirectoryName = DirectoryName.Replace(@"\", "/");
                string[] array = DirectoryName.Split(new char[] { '/' });
                if (array[0] == ".")
                {
                    this.DirectoryPath = "/";
                    if (array.Length == 1)
                    {
                        return true;
                    }
                    Array.Clear(array, 0, 1);
                }
                bool flag = false;
                foreach (string str2 in array)
                {
                    if (str2 != null)
                    {
                        flag = this.EnterOneSubDirectory(str2);
                        if (!flag)
                        {
                            this.DirectoryPath = directoryPath;
                            return false;
                        }
                    }
                }
                flag2 = flag;
            }
            catch (Exception exception)
            {
                this.DirectoryPath = directoryPath;
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag2;
        }

        private FileListStyle GuessFileListStyle(string[] recordList)
        {
            foreach (string str in recordList)
            {
                if ((str.Length > 10) && Regex.IsMatch(str.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                {
                    return FileListStyle.UnixStyle;
                }
                if ((str.Length > 8) && Regex.IsMatch(str.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                {
                    return FileListStyle.WindowsStyle;
                }
            }
            return FileListStyle.Unknown;
        }

        public bool IsValidFileChars(string FileName)
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            char[] chArray2 = FileName.ToCharArray();
            foreach (char ch in chArray2)
            {
                if (Array.BinarySearch<char>(invalidFileNameChars, ch) >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsValidPathChars(string DirectoryName)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            char[] chArray2 = DirectoryName.ToCharArray();
            foreach (char ch in chArray2)
            {
                if (Array.BinarySearch<char>(invalidPathChars, ch) >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public FileStruct[] ListDirectories()
        {
            FileStruct[] structArray = this.ListFilesAndDirectories();
            List<FileStruct> list = new List<FileStruct>();
            foreach (FileStruct struct2 in structArray)
            {
                if (struct2.IsDirectory)
                {
                    list.Add(struct2);
                }
            }
            return list.ToArray();
        }

        public FileStruct[] ListFiles()
        {
            FileStruct[] structArray = this.ListFilesAndDirectories();
            List<FileStruct> list = new List<FileStruct>();
            foreach (FileStruct struct2 in structArray)
            {
                if (!struct2.IsDirectory)
                {
                    list.Add(struct2);
                }
            }
            return list.ToArray();
        }

        public FileStruct[] ListFilesAndDirectories()
        {
            this.Response = this.Open(this.Uri, "LIST");
            string datastring = new StreamReader(this.Response.GetResponseStream(), Encoding.Default).ReadToEnd();
            return this.GetList(datastring);
        }

        public bool MakeDirectory(string DirectoryName)
        {
            bool flag;
            try
            {
                if (!this.IsValidPathChars(DirectoryName))
                {
                    throw new Exception("目录名非法！");
                }
                if (this.DirectoryExist(DirectoryName))
                {
                    throw new Exception("服务器上面已经存在同名的文件名或目录名！");
                }
                this.Response = this.Open(new System.Uri(this.Uri.ToString() + DirectoryName), "MKD");
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool MoveFileToAnotherDirectory(string RemoteFile, string DirectoryName)
        {
            bool flag2;
            string directoryPath = this.DirectoryPath;
            try
            {
                if (DirectoryName == "")
                {
                    return false;
                }
                if (!DirectoryName.StartsWith("/"))
                {
                    DirectoryName = "/" + DirectoryName;
                }
                if (!DirectoryName.EndsWith("/"))
                {
                    DirectoryName = DirectoryName + "/";
                }
                bool flag = this.ReName(RemoteFile, DirectoryName + RemoteFile);
                this.DirectoryPath = directoryPath;
                flag2 = flag;
            }
            catch (Exception exception)
            {
                this.DirectoryPath = directoryPath;
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag2;
        }

        private FtpWebResponse Open(System.Uri uri, string FtpMathod)
        {
            FtpWebResponse response;
            try
            {
                this.Request = (FtpWebRequest) WebRequest.Create(uri);
                this.Request.Method = FtpMathod;
                this.Request.UseBinary = true;
                this.Request.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    this.Request.Proxy = this.Proxy;
                }
                response = (FtpWebResponse) this.Request.GetResponse();
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return response;
        }

        private FtpWebRequest OpenRequest(System.Uri uri, string FtpMathod)
        {
            FtpWebRequest request;
            try
            {
                this.Request = (FtpWebRequest) WebRequest.Create(uri);
                this.Request.Method = FtpMathod;
                this.Request.UseBinary = true;
                this.Request.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    this.Request.Proxy = this.Proxy;
                }
                request = this.Request;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return request;
        }

        private FileStruct ParseFileStructFromUnixStyleRecord(string Record)
        {
            FileStruct struct2 = new FileStruct();
            string s = Record.Trim();
            struct2.Flags = s.Substring(0, 10);
            struct2.IsDirectory = struct2.Flags[0] == 'd';
            s = s.Substring(11).Trim();
            this._cutSubstringFromStringWithTrim(ref s, ' ', 0);
            struct2.Owner = this._cutSubstringFromStringWithTrim(ref s, ' ', 0);
            struct2.Group = this._cutSubstringFromStringWithTrim(ref s, ' ', 0);
            this._cutSubstringFromStringWithTrim(ref s, ' ', 0);
            string oldValue = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
            if (oldValue.IndexOf(":") >= 0)
            {
                s = s.Replace(oldValue, DateTime.Now.Year.ToString());
            }
            struct2.CreateTime = DateTime.Parse(this._cutSubstringFromStringWithTrim(ref s, ' ', 8));
            struct2.Name = s;
            return struct2;
        }

        private FileStruct ParseFileStructFromWindowsStyleRecord(string Record)
        {
            FileStruct struct2 = new FileStruct();
            string str = Record.Trim();
            string str2 = str.Substring(0, 8);
            str = str.Substring(8, str.Length - 8).Trim();
            string str3 = str.Substring(0, 7);
            str = str.Substring(7, str.Length - 7).Trim();
            DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
            dateTimeFormat.ShortTimePattern = "t";
            struct2.CreateTime = DateTime.Parse(str2 + " " + str3, dateTimeFormat);
            if (str.Substring(0, 5) == "<DIR>")
            {
                struct2.IsDirectory = true;
                str = str.Substring(5, str.Length - 5).Trim();
            }
            else
            {
                str = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                struct2.IsDirectory = false;
            }
            struct2.Name = str;
            return struct2;
        }

        public bool RemoveDirectory(string DirectoryName)
        {
            bool flag;
            try
            {
                if (!this.IsValidPathChars(DirectoryName))
                {
                    throw new Exception("目录名非法！");
                }
                if (!this.DirectoryExist(DirectoryName))
                {
                    throw new Exception("服务器上面不存在指定的文件名或目录名！");
                }
                this.Response = this.Open(new System.Uri(this.Uri.ToString() + DirectoryName), "RMD");
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool ReName(string RemoteFileName, string NewFileName)
        {
            bool flag;
            try
            {
                if (!(this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(NewFileName)))
                {
                    throw new Exception("文件名非法！");
                }
                if (RemoteFileName == NewFileName)
                {
                    return true;
                }
                if (!this.FileExist(RemoteFileName))
                {
                    throw new Exception("文件在服务器上不存在！");
                }
                this.Request = this.OpenRequest(new System.Uri(this.Uri.ToString() + RemoteFileName), "RENAME");
                this.Request.RenameTo = NewFileName;
                this.Response = (FtpWebResponse) this.Request.GetResponse();
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool UploadFile(string LocalFullPath)
        {
            return this.UploadFile(LocalFullPath, Path.GetFileName(LocalFullPath), false);
        }

        public bool UploadFile(string LocalFullPath, bool OverWriteRemoteFile)
        {
            return this.UploadFile(LocalFullPath, Path.GetFileName(LocalFullPath), OverWriteRemoteFile);
        }

        public bool UploadFile(string LocalFullPath, string RemoteFileName)
        {
            return this.UploadFile(LocalFullPath, RemoteFileName, false);
        }

        public bool UploadFile(byte[] FileBytes, string RemoteFileName)
        {
            if (!this.IsValidFileChars(RemoteFileName))
            {
                throw new Exception("非法文件名或目录名!");
            }
            return this.UploadFile(FileBytes, RemoteFileName, false);
        }

        public bool UploadFile(string LocalFullPath, string RemoteFileName, bool OverWriteRemoteFile)
        {
            bool flag;
            try
            {
                if (!(this.IsValidFileChars(Path.GetFileName(LocalFullPath)) && this.IsValidPathChars(Path.GetDirectoryName(LocalFullPath))))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!System.IO.File.Exists(LocalFullPath))
                {
                    throw new Exception("本地文件不存在!");
                }
                FileStream stream = new FileStream(LocalFullPath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int) stream.Length);
                stream.Close();
                flag = this.UploadFile(buffer, RemoteFileName, OverWriteRemoteFile);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public bool UploadFile(byte[] FileBytes, string RemoteFileName, bool OverWriteRemoteFile)
        {
            bool flag;
            try
            {
                bool flag2;
                if (RemoteFileName.IndexOf('/') >= 0)
                {
                    string directoryPath = RemoteFileName.Substring(0, RemoteFileName.LastIndexOf('/'));
                    RemoteFileName = RemoteFileName.Substring(RemoteFileName.LastIndexOf('/') + 1);
                    if (!this.IsValidFileChars(RemoteFileName))
                    {
                        throw new Exception("非法文件名！");
                    }
                    this.CreateDirectoryPath(directoryPath);
                }
                else if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名！");
                }
                if (!(OverWriteRemoteFile || !this.FileExist(RemoteFileName)))
                {
                    throw new Exception("FTP服务上面已经存在同名文件！");
                }
                this.Response = this.Open(new System.Uri(this.Uri.ToString() + RemoteFileName), "STOR");
                Stream requestStream = this.Request.GetRequestStream();
                MemoryStream stream2 = new MemoryStream(FileBytes);
                byte[] buffer = new byte[0x2800];
                int count = 0;
                int num2 = 0;
                goto Label_0116;
            Label_00E5:
                count = stream2.Read(buffer, 0, buffer.Length);
                if (count == 0)
                {
                    goto Label_011B;
                }
                num2 += count;
                requestStream.Write(buffer, 0, count);
            Label_0116:
                flag2 = true;
                goto Label_00E5;
            Label_011B:
                requestStream.Close();
                this.Response = (FtpWebResponse) this.Request.GetResponse();
                stream2.Close();
                stream2.Dispose();
                FileBytes = null;
                flag = true;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
            return flag;
        }

        public void UploadFileAsync(string LocalFullPath)
        {
            this.UploadFileAsync(LocalFullPath, Path.GetFileName(LocalFullPath), false);
        }

        public void UploadFileAsync(string LocalFullPath, bool OverWriteRemoteFile)
        {
            this.UploadFileAsync(LocalFullPath, Path.GetFileName(LocalFullPath), OverWriteRemoteFile);
        }

        public void UploadFileAsync(string LocalFullPath, string RemoteFileName)
        {
            this.UploadFileAsync(LocalFullPath, RemoteFileName, false);
        }

        public void UploadFileAsync(byte[] FileBytes, string RemoteFileName)
        {
            if (!this.IsValidFileChars(RemoteFileName))
            {
                throw new Exception("非法文件名或目录名!");
            }
            this.UploadFileAsync(FileBytes, RemoteFileName, false);
        }

        public void UploadFileAsync(string LocalFullPath, string RemoteFileName, bool OverWriteRemoteFile)
        {
            try
            {
                if (!((this.IsValidFileChars(RemoteFileName) && this.IsValidFileChars(Path.GetFileName(LocalFullPath))) && this.IsValidPathChars(Path.GetDirectoryName(LocalFullPath))))
                {
                    throw new Exception("非法文件名或目录名!");
                }
                if (!(OverWriteRemoteFile || !this.FileExist(RemoteFileName)))
                {
                    throw new Exception("FTP服务上面已经存在同名文件！");
                }
                if (!System.IO.File.Exists(LocalFullPath))
                {
                    throw new Exception("本地文件不存在!");
                }
                MyWebClient client = new MyWebClient();
                client.UploadProgressChanged += new UploadProgressChangedEventHandler(this.client_UploadProgressChanged);
                client.UploadFileCompleted += new UploadFileCompletedEventHandler(this.client_UploadFileCompleted);
                client.Credentials = new NetworkCredential(this.UserName, this.Password);
                if (this.Proxy != null)
                {
                    client.Proxy = this.Proxy;
                }
                client.UploadFileAsync(new System.Uri(this.Uri.ToString() + RemoteFileName), LocalFullPath);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public void UploadFileAsync(byte[] FileBytes, string RemoteFileName, bool OverWriteRemoteFile)
        {
            try
            {
                if (!this.IsValidFileChars(RemoteFileName))
                {
                    throw new Exception("非法文件名！");
                }
                if (!(OverWriteRemoteFile || !this.FileExist(RemoteFileName)))
                {
                    throw new Exception("FTP服务上面已经存在同名文件！");
                }
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                if (!folderPath.EndsWith(@"\"))
                {
                    folderPath = folderPath + @"\";
                }
                string path = Path.ChangeExtension(folderPath + Path.GetRandomFileName(), Path.GetExtension(RemoteFileName));
                FileStream stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
                stream.Write(FileBytes, 0, FileBytes.Length);
                stream.Flush();
                stream.Close();
                stream.Dispose();
                this._isDeleteTempFile = true;
                this._UploadTempFile = path;
                FileBytes = null;
                this.UploadFileAsync(path, RemoteFileName, OverWriteRemoteFile);
            }
            catch (Exception exception)
            {
                this.ErrorMsg = exception.ToString();
                throw exception;
            }
        }

        public string DirectoryPath
        {
            get
            {
                return this._DirectoryPath;
            }
            set
            {
                this._DirectoryPath = value;
            }
        }

        public string ErrorMsg
        {
            get
            {
                return this._ErrorMsg;
            }
            set
            {
                this._ErrorMsg = value;
            }
        }

        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        public WebProxy Proxy
        {
            get
            {
                return this._Proxy;
            }
            set
            {
                this._Proxy = value;
            }
        }

        public System.Uri Uri
        {
            get
            {
                if (this._DirectoryPath == "/")
                {
                    return this._Uri;
                }
                string str = this._Uri.ToString();
                if (str.EndsWith("/"))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                return new System.Uri(str + this.DirectoryPath);
            }
            set
            {
                if (value.Scheme != System.Uri.UriSchemeFtp)
                {
                    throw new Exception("Ftp 地址格式错误!");
                }
                this._Uri = new System.Uri(value.GetLeftPart(UriPartial.Authority));
                this._DirectoryPath = value.AbsolutePath;
                if (!this._DirectoryPath.EndsWith("/"))
                {
                    this._DirectoryPath = this._DirectoryPath + "/";
                }
            }
        }

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        public delegate void De_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e);

        public delegate void De_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e);

        public delegate void De_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e);

        public delegate void De_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e);

        internal class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                FtpWebRequest webRequest = (FtpWebRequest) base.GetWebRequest(address);
                webRequest.UsePassive = false;
                return webRequest;
            }
        }
    }
}

