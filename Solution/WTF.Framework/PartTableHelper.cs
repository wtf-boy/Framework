namespace WTF.Framework
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class PartTableHelper
    {
        public static void CreatePartTableObject(string dataObjectParam, string sourcetable, string connectionKeyOrConnectionString)
        {
            if (dataObjectParam.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("dataObjectParam参数不能为空");
            }
            if (sourcetable.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("sourcetable参数不能为空");
            }
            if (connectionKeyOrConnectionString.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("connectionKeyOrConnectionString参数不能为空");
            }
            string str = sourcetable.ToLower().Replace("_tb", "") + "_" + dataObjectParam + "_tb";
            if (MySqlHelper.ExecuteDataSet(connectionKeyOrConnectionString, string.Format("SHOW TABLES LIKE '{0}';", str), new MySqlParameter[0]).IsNull())
            {
                MySqlHelper.ExecuteNonQuery(connectionKeyOrConnectionString, string.Format("CREATE TABLE {0}  LIKE {1};", str, sourcetable), new MySqlParameter[0]);
            }
        }

        public static string PartMoldTableName(this int ID, int moldValue = 10)
        {
            return ID.PartMoldTableName("", moldValue);
        }

        public static string PartMoldTableName(this int ID, string tableNameObject, int moldValue = 10)
        {
            int num = ID % moldValue;
            if (string.IsNullOrWhiteSpace(tableNameObject))
            {
                return string.Format("{0}", num);
            }
            return string.Format("{0}_{1}", tableNameObject, num);
        }
    }
}

