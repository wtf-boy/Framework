namespace WTF.Framework
{
    using System;
    using System.Configuration;

    public class EncryptConnectionHelper
    {
        public static string EncryptIV = "EAA80282E05EA510";

        public static string ConnectionString(string name)
        {
            EncryptConnectionSection encryptConnectionSection = GetEncryptConnectionSection();
            ConnectionStringElement element = encryptConnectionSection.ConnectionStrings[name];
            if (element == null)
            {
                throw new ConfigurationErrorsException("未设置连接串名:" + name);
            }
            string encryptKey = encryptConnectionSection.EncryptKey;
            if (encryptConnectionSection.IsEncrypt)
            {
                return element.connectionString.Decrypt3DESFromBase64(encryptKey, EncryptIV);
            }
            return element.connectionString;
        }

        private static EncryptConnectionSection GetEncryptConnectionSection()
        {
            return (EncryptConnectionSection) ConfigHelper.GetSection("EncryptConnectionStrings", "WTFConfig");
        }
    }
}

