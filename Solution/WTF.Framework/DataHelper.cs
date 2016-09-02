namespace WTF.Framework
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web.UI.WebControls;

    public static class DataHelper
    {
        public static void BindControl(this DataTable dataTable, ListControl bindControl, string textField, string valueField, HeaderType headerType = 0)
        {
            bindControl.BindControl(dataTable, textField, valueField, headerType);
        }

        public static void BindControl(this ListControl bindControl, DataTable dataTable, string textField, string valueField, HeaderType headerType = 0)
        {
            bindControl.Items.Clear();
            if (headerType != HeaderType.None)
            {
                bindControl.Items.Add(new ListItem(headerType.GetEnumDescription(), ""));
            }
            foreach (DataRow row in dataTable.Rows)
            {
                bindControl.Items.Add(new ListItem(row[textField].ToString(), row[valueField].ToString()));
            }
        }

        public static void BindControl(this ListControl bindControl, string connectionKeyOrConnectionString, string tableName, string condition, string sort, string textField, string valueField, HeaderType headerType = 0)
        {
            string commandText = string.Format("SELECT DISTINCT {0},{1} FROM {2}", textField, valueField, tableName);
            if (!string.IsNullOrWhiteSpace(condition))
            {
                commandText = commandText + "  where " + condition;
            }
            if (!string.IsNullOrWhiteSpace(sort))
            {
                commandText = commandText + "  order by  " + sort;
            }
            DataTable table = WTF.Framework.MySqlHelper.ExecuteDataTable(connectionKeyOrConnectionString, commandText, new MySqlParameter[0]);
            bindControl.Items.Clear();
            if (headerType != HeaderType.None)
            {
                bindControl.Items.Add(new ListItem(headerType.GetEnumDescription(), ""));
            }
            foreach (DataRow row in table.Rows)
            {
                bindControl.Items.Add(new ListItem(row[textField].ToString(), row[valueField].ToString()));
            }
        }

        public static DataTable ConvertDataTable(this IEnumerable<Dictionary<string, object>> data)
        {
            DataTable table = new DataTable();
            if (data.Count<Dictionary<string, object>>() > 0)
            {
                foreach (Dictionary<string, object> dictionary in data)
                {
                    if (dictionary.Keys.Count == 0)
                    {
                        return table;
                    }
                    if (table.Columns.Count == 0)
                    {
                        foreach (string str in dictionary.Keys)
                        {
                            table.Columns.Add(str, dictionary[str].GetType());
                        }
                    }
                    DataRow row = table.NewRow();
                    foreach (string str in dictionary.Keys)
                    {
                        row[str] = dictionary[str];
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static DataTable ConvertDataTable(this string jsonValue)
        {
            DataTable table = new DataTable();
            if (string.IsNullOrWhiteSpace(jsonValue))
            {
                return table;
            }
            return jsonValue.JsonJsDeserialize<List<Dictionary<string, object>>>().ConvertDataTable();
        }

        public static List<T> ConvertList<T>(this DataSet objDataSet) where T : class, new()
        {
            return objDataSet.Tables[0].ConvertList<T>();
        }

        public static List<T> ConvertList<T>(this DataTable objDataTable) where T : class, new()
        {
            List<T> list = new List<T>();
            if (objDataTable.Rows.Count != 0)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (DataRow row in objDataTable.Rows)
                {
                    T local = Activator.CreateInstance<T>();
                    IEnumerator enumerator2 = objDataTable.Columns.GetEnumerator();

                    while (enumerator2.MoveNext())
                    {
                        Func<PropertyInfo, bool> predicate = null;
                        DataColumn objDataColumn = (DataColumn)enumerator2.Current;
                        if (predicate == null)
                        {
                            predicate = s => s.Name == objDataColumn.ColumnName;
                        }
                        PropertyInfo info = properties.FirstOrDefault<PropertyInfo>(predicate);
                        if ((info != null) && (row[objDataColumn.ColumnName] != DBNull.Value))
                        {
                            info.SetValue(local, row[objDataColumn.ColumnName], null);
                        }
                    }

                    list.Add(local);
                }
            }
            return list;
        }

        public static List<T> ConvertList<T>(this DataSet objDataSet, string fieldName)
        {
            return (from cols in objDataSet.Tables[0].Select() select (T)cols[fieldName]).ToList<T>();
        }

        public static List<T> ConvertList<T>(this DataTable objDataTable, string fieldName)
        {
            return (from cols in objDataTable.Select() select (T)cols[fieldName]).ToList<T>();
        }

        public static List<int> ConvertListInt(this DataTable objDataTable, string fieldName)
        {
            return (from cols in objDataTable.Select() select (int)cols[fieldName]).ToList<int>();
        }

        public static string ConvertListToString(this DataTable objDataTable, string fieldName)
        {
            return (from cols in objDataTable.Select() select cols[fieldName].ToString()).ConvertListToString<string>();
        }

        public static DataTable CopySortFieldID<I>(this DataTable objDataTable, string fieldName, IEnumerable<I> IDList)
        {
            DataTable table = objDataTable.Clone();
            bool flag = typeof(I) == typeof(string);
            if (IDList.Count<I>() > 0)
            {
                foreach (I local in IDList)
                {
                    string filterExpression = string.Format("{0}={1}{2}{1}", fieldName, flag ? "'" : "", local);
                    foreach (DataRow row in objDataTable.Select(filterExpression))
                    {
                        table.Rows.Add(row.ItemArray);
                    }
                }
            }
            return table;
        }

        public static MySqlParameter[] CreateSqlParameter(this string parameterNames, params object[] parmsValue)
        {
            if (string.IsNullOrWhiteSpace(parameterNames))
            {
                throw new ArgumentException("parameterNames,不能为空值");
            }
            if ((parmsValue == null) || (parmsValue.Length == 0))
            {
                throw new ArgumentException("parmsValue,不能为空值");
            }
            string[] strArray = parameterNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 0)
            {
                throw new ArgumentException("parameterNames,不能为空值：" + strArray.Length);
            }
            if (strArray.Length != parmsValue.Length)
            {
                throw new ArgumentException("传入的参数个数和值的个数不一致");
            }
            MySqlParameter[] parameterArray = new MySqlParameter[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                parameterArray[i] = new MySqlParameter("?" + strArray[i].TrimStart(new char[] { '?' }), parmsValue[i]);
            }
            return parameterArray;
        }

        public static bool IsNoNull(this DataSet objDataSet)
        {
            return (((objDataSet != null) && (objDataSet.Tables.Count > 0)) && (objDataSet.Tables[0].Rows.Count > 0));
        }

        public static bool IsNoNull(this DataTable objDataTable)
        {
            return ((objDataTable != null) && (objDataTable.Rows.Count > 0));
        }

        public static bool IsNull(this DataSet objDataSet)
        {
            return !objDataSet.IsNoNull();
        }

        public static bool IsNull(this DataTable objDataTable)
        {
            return !objDataTable.IsNoNull();
        }

        public static DataTable MergeDataTable(this DataTable dt1, string key1, DataTable dt2, string key2)
        {
            return dt1.MergeDataTable("", key1, dt2, "", key2, dt2.TableName);
        }

        public static DataTable MergeDataTable(this DataTable dt1, string dt1RemoveFields, string key1, DataTable dt2, string dt2RemoveFields, string key2, string dt2AliasesName)
        {
            int num;
            DataColumn column;
            DataTable table = new DataTable();
            List<string> list = new List<string>();
            for (num = 0; num < dt1.Columns.Count; num++)
            {
                column = dt1.Columns[num];
                if (!(!string.IsNullOrWhiteSpace(dt1RemoveFields) && dt1RemoveFields.ToLower().Split(new char[] { ',' }).Contains<string>(column.ColumnName.ToLower())))
                {
                    list.Add(column.ColumnName);
                    table.Columns.Add(column.ColumnName, column.DataType);
                }
            }
            List<string> list2 = new List<string>();
            for (num = 0; num < dt2.Columns.Count; num++)
            {
                column = dt2.Columns[num];
                if (string.IsNullOrWhiteSpace(dt2RemoveFields) || !dt2RemoveFields.ToLower().Split(new char[] { ',' }).Contains<string>(column.ColumnName.ToLower()))
                {
                    list2.Add(column.ColumnName);
                    if (!list.Contains(column.ColumnName))
                    {
                        table.Columns.Add(column.ColumnName, column.DataType);
                    }
                    else
                    {
                        table.Columns.Add(dt2AliasesName + "_" + column.ColumnName, column.DataType);
                    }
                }
            }
            foreach (DataRow row in dt1.Rows)
            {
                DataRow row2 = table.NewRow();
                foreach (string str in list)
                {
                    row2[str] = row[str];
                }
                object obj2 = row[key1];
                string filterExpression = string.Format("{0}={1}", key2, obj2);
                foreach (DataRow row3 in dt2.Select(filterExpression))
                {
                    foreach (string str in list2)
                    {
                        if (!list.Contains(str))
                        {
                            row2[str] = row3[str];
                        }
                        else
                        {
                            row2[dt2AliasesName + "_" + str] = row3[str];
                        }
                    }
                }
                table.Rows.Add(row2);
            }
            return table;
        }
    }
}

