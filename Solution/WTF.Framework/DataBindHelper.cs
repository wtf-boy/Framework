namespace WTF.Framework
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public static class DataBindHelper
    {
        public static Dictionary<string, string> BindDataKeyValue(string connectionKeyOrConnectionString, string tableName, string condition, string sort, string keyField, string nameFiled)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string commandText = string.Format("SELECT DISTINCT {0},{1} FROM {2}", keyField, nameFiled, tableName);
            if (!string.IsNullOrWhiteSpace(condition))
            {
                commandText = commandText + "  where " + condition;
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                commandText = commandText + "  order by  " + sort;
            }
            foreach (DataRow row in MySqlHelper.ExecuteDataTable(connectionKeyOrConnectionString, commandText, new MySqlParameter[0]).Rows)
            {
                dictionary.Add(row[keyField].ToString(), row[nameFiled].ToString());
            }
            return dictionary;
        }

        public static Dictionary<K, V> BindDataKeyValue<K, V>(string connectionKeyOrConnectionString, string tableName, string condition, string sort, string keyField, string nameFiled)
        {
            Dictionary<K, V> dictionary = new Dictionary<K, V>();
            string commandText = string.Format("SELECT DISTINCT {0},{1} FROM {2}", keyField, nameFiled, tableName);
            if (!string.IsNullOrWhiteSpace(condition))
            {
                commandText = commandText + "  where " + condition;
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                commandText = commandText + "  order by  " + sort;
            }
            foreach (DataRow row in MySqlHelper.ExecuteDataTable(connectionKeyOrConnectionString, commandText, new MySqlParameter[0]).Rows)
            {
                dictionary.Add((K) row[keyField], (V) row[nameFiled]);
            }
            return dictionary;
        }

        public static void BindListControl(this DataTable dataTable, ListControl listControl, string text, string value)
        {
            listControl.Items.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                listControl.Items.Add(new ListItem(row[text].ToString(), row[value].ToString()));
            }
        }

        public static void BindListControl(this DataTable dataTable, ListControl listControl, string text, string value, string firstText, string firstValue)
        {
            listControl.Items.Clear();
            listControl.Items.Add(new ListItem(firstText, firstValue));
            foreach (DataRow row in dataTable.Rows)
            {
                listControl.Items.Add(new ListItem(row[text].ToString(), row[value].ToString()));
            }
        }

        public static Dictionary<string, string> BindSourceData<K>(this string keyIDString, string connectionKeyOrConnectionString, string tableName, string keyField, string nameFiled)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(keyIDString))
            {
                if (typeof(K) == typeof(string))
                {
                    keyIDString = keyIDString.ConvertStringID();
                }
                string commandText = string.Format("SELECT DISTINCT {0},{1} FROM {2} where {0} in ({3})", new object[] { keyField, nameFiled, tableName, keyIDString });
                foreach (DataRow row in MySqlHelper.ExecuteDataTable(connectionKeyOrConnectionString, commandText, new MySqlParameter[0]).Rows)
                {
                    dictionary.Add(row[keyField].ToString(), row[nameFiled].ToString());
                }
            }
            return dictionary;
        }

        public static Dictionary<K, V> BindSourceData<K, V>(this string keyIDString, string connectionKeyOrConnectionString, string tableName, string keyField, string nameFiled)
        {
            Dictionary<K, V> dictionary = new Dictionary<K, V>();
            if (!string.IsNullOrWhiteSpace(keyIDString))
            {
                if (typeof(K) == typeof(string))
                {
                    keyIDString = keyIDString.ConvertStringID();
                }
                string commandText = string.Format("SELECT DISTINCT {0},{1} FROM {2} where {0} in ({3})", new object[] { keyField, nameFiled, tableName, keyIDString });
                foreach (DataRow row in MySqlHelper.ExecuteDataTable(connectionKeyOrConnectionString, commandText, new MySqlParameter[0]).Rows)
                {
                    dictionary.Add((K) row[keyField], (V) row[nameFiled]);
                }
            }
            return dictionary;
        }

        public static List<string> ListBoxToArray(this ListControl lcSource)
        {
            List<string> list = new List<string>();
            for (int i = lcSource.Items.Count - 1; i >= 0; i--)
            {
                list.Add(lcSource.Items[i].Value);
            }
            return list;
        }

        public static void RemoveListBox(ListControl lcSource, ListControl lcDes)
        {
            for (int i = lcDes.Items.Count - 1; i >= 0; i--)
            {
                lcSource.Items.Remove(new ListItem(lcDes.Items[i].Text, lcDes.Items[i].Value));
            }
        }

        public static void TransferListBox(ListControl lcSource, ListControl lcDes)
        {
            for (int i = lcSource.Items.Count - 1; i >= 0; i--)
            {
                if (lcSource.Items[i].Selected)
                {
                    lcDes.Items.Add(new ListItem(lcSource.Items[i].Text, lcSource.Items[i].Value));
                    lcSource.Items.Remove(new ListItem(lcSource.Items[i].Text, lcSource.Items[i].Value));
                }
            }
        }
    }
}

