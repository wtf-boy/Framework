namespace WTF.DAL
{
    using MySql.Data.MySqlClient;
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;

    public static class DalBaseHelper
    {
        public static Dictionary<string, string> LoadSourceData<K>(this string keyIDString, string connectionKeyOrConnectionString, string tableName, string keyField, string nameFiled)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(keyIDString))
            {
                if (typeof(K) == typeof(string))
                {
                    keyIDString = keyIDString.ConvertStringID();
                }
                string commandText = string.Format("SELECT DISTINCT {0},{1} FROM {2} where {0} in ({3})", new object[] { keyField, nameFiled, tableName, keyIDString });
                DalBase base2 = new DalBase(connectionKeyOrConnectionString);
                foreach (DataRow row in base2.ExecuteDataSet(commandText, new MySqlParameter[0]).Tables[0].Rows)
                {
                    dictionary.Add(row[keyField].ToString(), row[nameFiled].ToString());
                }
            }
            return dictionary;
        }

        public static Dictionary<K, V> LoadSourceData<K, V>(this string keyIDString, string connectionKeyOrConnectionString, string tableName, string keyField, string nameFiled)
        {
            Dictionary<K, V> dictionary = new Dictionary<K, V>();
            if (!string.IsNullOrWhiteSpace(keyIDString))
            {
                if (typeof(K) == typeof(string))
                {
                    keyIDString = keyIDString.ConvertStringID();
                }
                string commandText = string.Format("SELECT DISTINCT {0},{1} FROM {2} where {0} in ({3})", new object[] { keyField, nameFiled, tableName, keyIDString });
                DalBase base2 = new DalBase(connectionKeyOrConnectionString);
                foreach (DataRow row in base2.ExecuteDataSet(commandText, new MySqlParameter[0]).Tables[0].Rows)
                {
                    dictionary.Add((K) row[keyField], (V) row[nameFiled]);
                }
            }
            return dictionary;
        }
    }
}

