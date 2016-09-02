namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public static class PathHelper
    {
        public static string ApplicationDataDir(string ApplicationData)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationData);
        }

        public static void CreateDirectory(this FileInfo fileInfo)
        {
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
        }

        public static void CreateDirectory(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            if (!info.Directory.Exists)
            {
                info.Directory.Create();
            }
        }

        public static string ApplicationPath
        {
            get
            {
                return Path.GetDirectoryName(Application.ExecutablePath);
            }
        }
    }
}

