namespace WTF.DAL
{
    using MySql.Data.MySqlClient;
    using WTF.Framework;
    using WTF.Logging;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DalBase
    {
        public string _ConnectionKeyOrConnectionString;
        private bool _IsRecordErrorCommandText;
        private Logger _Logger;
        public string CurrentCommandText;

        public DalBase()
        {
            this._ConnectionKeyOrConnectionString = "SevenConnectionString";
            this._Logger = new Logger("Application");
            this.CurrentCommandText = "";
            this._IsRecordErrorCommandText = true;
        }

        public DalBase(string connectionKeyOrConnectionString)
        {
            this._ConnectionKeyOrConnectionString = "SevenConnectionString";
            this._Logger = new Logger("Application");
            this.CurrentCommandText = "";
            this._IsRecordErrorCommandText = true;
            if (!string.IsNullOrWhiteSpace(connectionKeyOrConnectionString))
            {
                this._ConnectionKeyOrConnectionString = connectionKeyOrConnectionString;
            }
            else
            {
                this._ConnectionKeyOrConnectionString = this.AssemblySimpleName + ".ConnectionString";
            }
        }

        public MySqlParameter[] CreateSqlParameter(string parameterNames, params object[] parmsValue)
        {
            if (parameterNames.IsNull())
            {
                return null;
            }
            string[] strArray = parameterNames.Split(new char[] { ',' });
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

        private string DataSql(object value)
        {
            string str = "";
            if (value == null)
            {
                str = "null";
            }
            else if (value is bool)
            {
                str = ((bool) value) ? "1" : "0";
            }
            else
            {
                str = value.ToString().Replace("'", "''");
            }
            if ((value != null) && ((value.GetType() == typeof(string)) || (value.GetType() == typeof(DateTime))))
            {
                str = "'" + str + "'";
            }
            return str;
        }

        public int ExecuteCommond(MySqlCommand cmd)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = WTF.Framework.MySqlHelper.GetConnectionString(this._ConnectionKeyOrConnectionString);
                cmd.Connection = connection;
                connection.Open();
                int num = cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
                return num;
            }
        }

        public DataSet ExecuteDataSet(string commandText, params MySqlParameter[] parms)
        {
            return this.ExecuteDataSet(this._ConnectionKeyOrConnectionString, commandText, parms);
        }

        public DataSet ExecuteDataSet(string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            return this.ExecuteDataSet(this._ConnectionKeyOrConnectionString, commandText, commandType, parms);
        }

        public DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return this.ExecuteDataSet(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            DataSet set = null;
            try
            {
                set = WTF.Framework.MySqlHelper.ExecuteDataSet(connectionKeyOrConnectionString, ((commandType != CommandType.StoredProcedure) ? this.RunSQlRemark : "") + commandText, commandType, parms);
            }
            catch (Exception exception)
            {
                this.LogErrorCommandText(commandText, exception, parms);
                throw exception;
            }
            finally
            {
                this.LogCommandText(commandText, parms);
            }
            return set;
        }

        public DataSet ExecuteDataSet(string tableName, string condition, string sortExpression, string fields = "*")
        {
            return this.ExecuteDataSet(this._ConnectionKeyOrConnectionString, tableName, condition, sortExpression, fields);
        }

        public DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string tableName, string condition, string sortExpression, string fields = "*")
        {
            return this.ExecuteDataSet(connectionKeyOrConnectionString, tableName, condition, null, sortExpression, fields);
        }

        public DataSet ExecuteDataSet(string tableName, string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return this.ExecuteDataSet(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, fields);
        }

        public DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            string commandText = "select " + fields + " from " + tableName + " " + condition + sortExpression;
            return this.ExecuteDataSet(connectionKeyOrConnectionString, commandText, parms);
        }

        public DataSet ExecuteDataSetLimit(string tableName, string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.ExecuteDataSetLimit(this._ConnectionKeyOrConnectionString, tableName, condition, sortExpression, offset, limit, fields);
        }

        public DataSet ExecuteDataSetLimit(string connectionKeyOrConnectionString, string tableName, string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.ExecuteDataSetLimit(connectionKeyOrConnectionString, tableName, condition, null, sortExpression, offset, limit, fields);
        }

        public DataSet ExecuteDataSetLimit(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.ExecuteDataSetLimit(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, offset, limit, fields);
        }

        public DataSet ExecuteDataSetLimit(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            string commandText = string.Concat(new object[] { "select ", fields, " from ", tableName, " ", condition, sortExpression, " limit  ", offset, ",", limit });
            return this.ExecuteDataSet(connectionKeyOrConnectionString, commandText, parms);
        }

        public int ExecuteDelete(string tableName, string condition)
        {
            return this.ExecuteDelete(tableName, condition, null);
        }

        public int ExecuteDelete(string tableName, string condition, MySqlParameter[] parms)
        {
            string commandText = "DELETE FROM " + tableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            return this.ExecuteNonQuery(commandText, parms);
        }

        public int ExecuteNonQuery(string commandText, params MySqlParameter[] parms)
        {
            return this.ExecuteNonQuery(this._ConnectionKeyOrConnectionString, commandText, parms);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            return this.ExecuteNonQuery(this._ConnectionKeyOrConnectionString, commandText, commandType, parms);
        }

        public int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return this.ExecuteNonQuery(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            int num = 0;
            try
            {
                num = WTF.Framework.MySqlHelper.ExecuteNonQuery(connectionKeyOrConnectionString, ((commandType != CommandType.StoredProcedure) ? this.RunSQlRemark : "") + commandText, commandType, parms);
            }
            catch (Exception exception)
            {
                this.LogErrorCommandText(commandText, exception, parms);
                throw exception;
            }
            finally
            {
                this.LogCommandText(commandText, parms);
            }
            return num;
        }

        public DataSet ExecutePage(string tableName, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.ExecutePage(this._ConnectionKeyOrConnectionString, tableName, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public DataSet ExecutePage(string connectionKeyOrConnectionString, string tableName, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.ExecutePage(connectionKeyOrConnectionString, tableName, condition, null, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public DataSet ExecutePage(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.ExecutePage(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public DataSet ExecutePage(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            recordCount = 0;
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            string[] strArray = new string[] { "SELECT ", fields, " FROM ", tableName, " ", condition, sortExpression, " limit  ", (pageIndex * pageSize).ToString(), ",", pageSize.ToString() };
            string commandText = string.Concat(strArray);
            DataSet set = this.ExecuteDataSet(connectionKeyOrConnectionString, commandText, parms);
            if (set != null)
            {
                commandText = " SELECT Count(0) FROM " + tableName + condition;
                object obj2 = this.ExecuteScalar(connectionKeyOrConnectionString, commandText, parms);
                if (obj2 != null)
                {
                    recordCount = Convert.ToInt32(obj2);
                    DataTable table = new DataTable {
                        TableName = "RecordCount"
                    };
                    table.Columns.Add("RecordCount", typeof(int));
                    DataRow row = table.NewRow();
                    row["RecordCount"] = (int) recordCount;
                    table.Rows.Add(row);
                    set.Tables.Add(table);
                }
            }
            return set;
        }

        public DataSet ExecutePageCommand(string commandText, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.ExecutePageCommand(commandText, condition, null, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public DataSet ExecutePageCommand(string commandText, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.ExecutePageCommand(this._ConnectionKeyOrConnectionString, commandText, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public DataSet ExecutePageCommand(string connectionKeyOrConnectionString, string commandText, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            recordCount = 0;
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            string[] strArray = new string[] { "SELECT ", fields, " FROM (", commandText, ") as CommandTextTable ", condition, sortExpression, " limit  ", (pageIndex * pageSize).ToString(), ",", pageSize.ToString() };
            string str = string.Concat(strArray);
            DataSet set = this.ExecuteDataSet(connectionKeyOrConnectionString, str, parms);
            if (set != null)
            {
                str = " SELECT Count(0) FROM (" + commandText + ") AS CommandTextTable " + condition;
                object obj2 = this.ExecuteScalar(connectionKeyOrConnectionString, str, parms);
                if (obj2 != null)
                {
                    recordCount = Convert.ToInt32(obj2);
                    DataTable table = new DataTable {
                        TableName = "RecordCount"
                    };
                    table.Columns.Add("RecordCount", typeof(int));
                    DataRow row = table.NewRow();
                    row["RecordCount"] = (int) recordCount;
                    table.Rows.Add(row);
                    set.Tables.Add(table);
                }
            }
            return set;
        }

        public DataSet ExecutePageCommandLimit(string commandText, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageCommandLimit(commandText, condition, null, sortExpression, pageSize, pageIndex, fields);
        }

        public DataSet ExecutePageCommandLimit(string commandText, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageCommandLimit(this._ConnectionKeyOrConnectionString, commandText, condition, parms, sortExpression, pageSize, pageIndex, fields);
        }

        public DataSet ExecutePageCommandLimit(string connectionKeyOrConnectionString, string commandText, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            string[] strArray = new string[] { "SELECT ", fields, " FROM (", commandText, ") as CommandTextTable ", condition, sortExpression, " limit  ", (pageIndex * pageSize).ToString(), ",", pageSize.ToString() };
            string str = string.Concat(strArray);
            return this.ExecuteDataSet(connectionKeyOrConnectionString, str, parms);
        }

        public DataSet ExecutePageLimit(string tableName, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageLimit(this._ConnectionKeyOrConnectionString, tableName, condition, sortExpression, pageSize, pageIndex, fields);
        }

        public DataSet ExecutePageLimit(string connectionKeyOrConnectionString, string tableName, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageLimit(connectionKeyOrConnectionString, tableName, condition, null, sortExpression, pageSize, pageIndex, fields);
        }

        public DataSet ExecutePageLimit(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageLimit(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, pageSize, pageIndex, fields);
        }

        public DataSet ExecutePageLimit(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            string[] strArray = new string[] { "SELECT ", fields, " FROM ", tableName, " ", condition, sortExpression, " limit  ", (pageIndex * pageSize).ToString(), ",", pageSize.ToString() };
            string commandText = string.Concat(strArray);
            return this.ExecuteDataSet(connectionKeyOrConnectionString, commandText, parms);
        }

        public DataSet ExecutePageList(string tableName, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageList(this._ConnectionKeyOrConnectionString, tableName, condition, sortExpression, pageSize, pageIndex, fields);
        }

        public DataSet ExecutePageList(string connectionKeyOrConnectionString, string tableName, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecuteDataSetLimit(connectionKeyOrConnectionString, tableName, condition, sortExpression, pageSize * pageIndex, pageSize, fields);
        }

        public DataSet ExecutePageList(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageList(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, pageSize, pageIndex, fields);
        }

        public DataSet ExecutePageList(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecuteDataSetLimit(connectionKeyOrConnectionString, tableName, condition, parms, sortExpression, pageSize * pageIndex, pageSize, fields);
        }

        public MySqlDataReader ExecutePageReaderList(string tableName, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageReaderList(this._ConnectionKeyOrConnectionString, tableName, condition, sortExpression, pageSize, pageIndex, fields);
        }

        public MySqlDataReader ExecutePageReaderList(string connectionKeyOrConnectionString, string tableName, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecuteReaderLimit(connectionKeyOrConnectionString, tableName, condition, sortExpression, pageSize * pageIndex, pageSize, fields);
        }

        public MySqlDataReader ExecutePageReaderList(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecutePageReaderList(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, pageSize, pageIndex, fields);
        }

        public MySqlDataReader ExecutePageReaderList(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.ExecutePageReaderList(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public MySqlDataReader ExecutePageReaderList(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.ExecuteReaderLimit(connectionKeyOrConnectionString, tableName, condition, parms, sortExpression, pageSize * pageIndex, pageSize, fields);
        }

        public MySqlDataReader ExecutePageReaderList(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.ExecuteReaderLimit(connectionKeyOrConnectionString, tableName, condition, parms, sortExpression, pageSize * pageIndex, pageSize, out recordCount, fields);
        }

        public MySqlDataReader ExecuteReader(string commandText, params MySqlParameter[] parms)
        {
            return this.ExecuteReader(this._ConnectionKeyOrConnectionString, commandText, parms);
        }

        public MySqlDataReader ExecuteReader(string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            return this.ExecuteReader(this._ConnectionKeyOrConnectionString, commandText, commandType, parms);
        }

        public MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return this.ExecuteReader(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            MySqlDataReader reader = null;
            try
            {
                reader = WTF.Framework.MySqlHelper.ExecuteReader(connectionKeyOrConnectionString, ((commandType != CommandType.StoredProcedure) ? this.RunSQlRemark : "") + commandText, commandType, parms);
            }
            catch (Exception exception)
            {
                this.LogErrorCommandText(commandText, exception, parms);
                throw exception;
            }
            finally
            {
                this.LogCommandText(commandText, parms);
            }
            return reader;
        }

        public MySqlDataReader ExecuteReader(string tableName, string condition, string sortExpression, string fields = "*")
        {
            return this.ExecuteReader(this._ConnectionKeyOrConnectionString, tableName, condition, sortExpression, fields);
        }

        public MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string tableName, string condition, string sortExpression, string fields = "*")
        {
            return this.ExecuteReader(connectionKeyOrConnectionString, tableName, condition, null, sortExpression, fields);
        }

        public MySqlDataReader ExecuteReader(string tableName, string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return this.ExecuteReader(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, fields);
        }

        public MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            string commandText = "select " + fields + " from " + tableName + " " + condition + sortExpression;
            return this.ExecuteReader(connectionKeyOrConnectionString, commandText, parms);
        }

        public MySqlDataReader ExecuteReaderLimit(string tableName, string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.ExecuteReaderLimit(this._ConnectionKeyOrConnectionString, tableName, condition, sortExpression, offset, limit, fields);
        }

        public MySqlDataReader ExecuteReaderLimit(string connectionKeyOrConnectionString, string tableName, string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.ExecuteReaderLimit(connectionKeyOrConnectionString, tableName, condition, null, sortExpression, offset, limit, fields);
        }

        public MySqlDataReader ExecuteReaderLimit(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.ExecuteReaderLimit(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, offset, limit, fields);
        }

        public MySqlDataReader ExecuteReaderLimit(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, out int recordCount, string fields = "*")
        {
            return this.ExecuteReaderLimit(this._ConnectionKeyOrConnectionString, tableName, condition, parms, sortExpression, offset, limit, out recordCount, fields);
        }

        public MySqlDataReader ExecuteReaderLimit(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            string commandText = string.Concat(new object[] { "select ", fields, " from ", tableName, " ", condition, sortExpression, " limit  ", offset, ",", limit });
            return this.ExecuteReader(connectionKeyOrConnectionString, commandText, parms);
        }

        public MySqlDataReader ExecuteReaderLimit(string connectionKeyOrConnectionString, string tableName, string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, out int recordCount, string fields = "*")
        {
            if (!string.IsNullOrWhiteSpace(fields))
            {
                fields = " " + fields + " ";
            }
            else
            {
                fields = "*";
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                condition = " where " + condition;
            }
            else
            {
                condition = " ";
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                sortExpression = " order by " + sortExpression;
            }
            else
            {
                sortExpression = " ";
            }
            recordCount = 0;
            string commandText = " SELECT Count(0) FROM " + tableName + condition;
            object obj2 = this.ExecuteScalar(connectionKeyOrConnectionString, commandText, parms);
            if (obj2 != null)
            {
                recordCount = Convert.ToInt32(obj2);
            }
            commandText = string.Concat(new object[] { "select ", fields, " from ", tableName, " ", condition, sortExpression, " limit  ", offset, ",", limit });
            return this.ExecuteReader(connectionKeyOrConnectionString, commandText, parms);
        }

        public object ExecuteScalar(string commandText, params MySqlParameter[] parms)
        {
            return this.ExecuteScalar(this._ConnectionKeyOrConnectionString, commandText, parms);
        }

        public object ExecuteScalar(string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            return this.ExecuteScalar(this._ConnectionKeyOrConnectionString, commandText, commandType, parms);
        }

        public object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return this.ExecuteScalar(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            object obj2 = null;
            try
            {
                obj2 = WTF.Framework.MySqlHelper.ExecuteScalar(connectionKeyOrConnectionString, ((commandType != CommandType.StoredProcedure) ? this.RunSQlRemark : "") + commandText, commandType, parms);
            }
            catch (Exception exception)
            {
                this.LogErrorCommandText(commandText, exception, parms);
                throw exception;
            }
            finally
            {
                this.LogCommandText(commandText, parms);
            }
            return obj2;
        }

        public int ExecuteUpdate(string tableName, string updateFields, string condition)
        {
            return this.ExecuteUpdate(tableName, updateFields, condition, null);
        }

        public int ExecuteUpdate(string tableName, string updateFields, string condition, MySqlParameter[] parms)
        {
            if (string.IsNullOrWhiteSpace(updateFields))
            {
                return 0;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE " + tableName + " SET " + updateFields);
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                builder.Append(condition);
            }
            string commandText = builder.ToString();
            return this.ExecuteNonQuery(commandText, parms);
        }

        public string GetFieldCondition<FieldKey>(string FieldName, IList<FieldKey> IDList)
        {
            return this.GetFieldCondition<FieldKey>(FieldName, IDList.ConvertListToString<FieldKey>());
        }

        public string GetFieldCondition<FieldKey>(string FieldName, string IdString)
        {
            bool flag = typeof(FieldKey) == typeof(string);
            if (flag)
            {
                return (FieldName + " in (" + IdString.ConvertStringID() + ")");
            }
            return (FieldName + " in (" + IdString + ")");
        }

        public int GetTotal(string tableName, string condition)
        {
            return this.GetTotal(tableName, condition, null);
        }

        public int GetTotal(string tableName, string condition, MySqlParameter[] parms)
        {
            int num = 0;
            string commandText = "SELECT Count(0) AS Total FROM " + tableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            object obj2 = this.ExecuteScalar(commandText, parms);
            if (obj2 != null)
            {
                num = Convert.ToInt32(obj2);
            }
            return num;
        }

        public void LogCommandText(string commandText, params MySqlParameter[] parms)
        {
            if (this.IsRecordSql || this.IsRecordOperation)
            {
                string logTitle = commandText;
                if (parms != null)
                {
                    foreach (MySqlParameter parameter in parms)
                    {
                        logTitle = logTitle.Replace(parameter.ParameterName, this.DataSql(parameter.Value));
                    }
                }
                this.CurrentCommandText = logTitle;
                if (this.IsRecordSql)
                {
                    this.Log.WriteLog(LogCategory.SqlInfo, logTitle, logTitle);
                }
                if (this.IsRecordOperation)
                {
                    new OperationLoger(this.Log.LogModuleType).JudgeSqlLog(commandText, logTitle);
                }
            }
        }

        public void LogErrorCommandText(string commandText, Exception objExp, params MySqlParameter[] parms)
        {
            if (this.IsRecordErrorCommandText)
            {
                string message = commandText;
                if (parms != null)
                {
                    foreach (MySqlParameter parameter in parms)
                    {
                        string parameterName = parameter.ParameterName;
                        string newValue = this.DataSql(parameter.Value);
                        message = message.Replace(parameterName, newValue);
                    }
                }
                this.CurrentCommandText = message;
                this.Log.WriteLog(LogCategory.ExceptionError, "执行语句出现异常：" + message, message, objExp);
            }
        }

        public string AssemblySimpleName
        {
            get
            {
                return base.GetType().Assembly.GetName().Name;
            }
        }

        public string ConnectionKeyOrConnectionString
        {
            get
            {
                return this._ConnectionKeyOrConnectionString;
            }
            set
            {
                this._ConnectionKeyOrConnectionString = value;
            }
        }

        public bool IsRecordErrorCommandText
        {
            get
            {
                return this._IsRecordErrorCommandText;
            }
            set
            {
                this._IsRecordErrorCommandText = value;
            }
        }

        public bool IsRecordOperation
        {
            get
            {
                return LogSectionHelper.IsRecordOperation;
            }
        }

        public bool IsRecordSql
        {
            get
            {
                string str2 = ConfigHelper.GetValue(this.AssemblySimpleName + ".IsRecordSql", "");
                if (string.IsNullOrWhiteSpace(str2))
                {
                    return LogSectionHelper.IsRecordSql;
                }
                return (str2 == "true");
            }
        }

        public Logger Log
        {
            get
            {
                return this._Logger;
            }
        }

        public string RunSQlRemark
        {
            get
            {
                return ("/*" + LogSectionHelper.ApplicationCode + "*/  ");
            }
        }
    }
}

