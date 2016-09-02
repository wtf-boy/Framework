namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class SqlHelper
    {
        private string _connectionKeyOrConnectionString;

        public SqlHelper()
        {
            this._connectionKeyOrConnectionString = "SevenConnectionString";
        }

        public SqlHelper(string _connectionKeyOrConnectionString)
        {
            this._connectionKeyOrConnectionString = "SevenConnectionString";
            this._connectionKeyOrConnectionString = _connectionKeyOrConnectionString;
        }

        public DataSet ExecuteDataSet(string commandText)
        {
            return this.ExecuteDataSet(commandText, CommandType.Text);
        }

        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            return this.ExecuteDataSet(commandText, commandType, null);
        }

        public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText)
        {
            return ExecuteDataSet(connectionKeyOrConnectionString, commandText, CommandType.Text);
        }

        public DataSet ExecuteDataSet(string commandText, CommandType commandType, SqlParameter[] prams)
        {
            return ExecuteDataSet(this._connectionKeyOrConnectionString, commandText, commandType, prams);
        }

        public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
        {
            return ExecuteDataSet(connectionKeyOrConnectionString, commandText, commandType, null);
        }

        public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, CommandType commandType, SqlParameter[] prams)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(connection, null, sqlCommand, commandType, commandText, prams);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
        }

        public DataTable ExecuteDataTable(string commandText)
        {
            return this.ExecuteDataTable(commandText, CommandType.Text, null);
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType)
        {
            return this.ExecuteDataTable(commandText, commandType, null);
        }

        public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText)
        {
            return ExecuteDataTable(connectionKeyOrConnectionString, commandText, CommandType.Text, null);
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType, SqlParameter[] prams)
        {
            return ExecuteDataTable(this._connectionKeyOrConnectionString, commandText, commandType, prams);
        }

        public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
        {
            return ExecuteDataTable(connectionKeyOrConnectionString, commandText, commandType, null);
        }

        public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText, CommandType commandType, SqlParameter[] prams)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(connection, null, sqlCommand, commandType, commandText, prams);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                connection.Close();
                return dataTable;
            }
        }

        public int ExecuteNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(commandText, CommandType.Text);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return this.ExecuteNonQuery(commandText, commandType, null);
        }

        public static int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText)
        {
            return ExecuteNonQuery(connectionKeyOrConnectionString, commandText, CommandType.Text);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, SqlParameter[] param)
        {
            return ExecuteNonQuery(this._connectionKeyOrConnectionString, commandText, commandType, param);
        }

        public static int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(connectionKeyOrConnectionString, commandText, commandType, null);
        }

        public static int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText, CommandType commandType, SqlParameter[] param)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(connection, null, sqlCommand, commandType, commandText, param);
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public int ExecuteNonQueryTransaction(string commandText, CommandType commandType, SqlParameter[] param)
        {
            return ExecuteNonQueryTransaction(this._connectionKeyOrConnectionString, commandText, commandType, param);
        }

        public static int ExecuteNonQueryTransaction(string connectionKeyOrConnectionString, string commandText, CommandType commandType, SqlParameter[] param)
        {
            int num2;
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    PrepareCommand(connection, sqlTransaction, sqlCommand, commandType, commandText, param);
                    int num = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    return num;
                }
                catch
                {
                    sqlTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (sqlTransaction != null)
                    {
                        sqlTransaction.Dispose();
                    }
                }
            }
            return num2;
        }

        public SqlDataReader ExecuteReader(string commandText)
        {
            return this.ExecuteReader(commandText, CommandType.Text);
        }

        public SqlDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            return this.ExecuteReader(commandText, commandType, null);
        }

        public static SqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText)
        {
            return ExecuteReader(connectionKeyOrConnectionString, commandText, CommandType.Text);
        }

        public SqlDataReader ExecuteReader(string commandText, CommandType commandType, SqlParameter[] prams)
        {
            return ExecuteReader(this._connectionKeyOrConnectionString, commandText, commandType, prams);
        }

        public static SqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
        {
            return ExecuteReader(connectionKeyOrConnectionString, commandText, commandType, null);
        }

        public static SqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, CommandType commandType, SqlParameter[] prams)
        {
            SqlConnection sqlConnection = new SqlConnection(GetConnectionString(connectionKeyOrConnectionString));
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(sqlConnection, null, sqlCommand, commandType, commandText, prams);
                return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                sqlConnection.Close();
                return null;
            }
        }

        public object ExecuteScalar(string commandText)
        {
            return this.ExecuteScalar(commandText, CommandType.Text);
        }

        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            return this.ExecuteScalar(commandText, commandType, null);
        }

        public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText)
        {
            return ExecuteScalar(connectionKeyOrConnectionString, commandText, CommandType.Text);
        }

        public object ExecuteScalar(string commandText, CommandType commandType, SqlParameter[] param)
        {
            return ExecuteScalar(this._connectionKeyOrConnectionString, commandText, commandType, param);
        }

        public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
        {
            return ExecuteScalar(connectionKeyOrConnectionString, commandText, commandType, null);
        }

        public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, CommandType commandType, SqlParameter[] param)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(connection, null, sqlCommand, commandType, commandText, param);
                return sqlCommand.ExecuteScalar();
            }
        }

        public static string GetConnectionString(string connectionKeyOrConnectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionKeyOrConnectionString))
            {
                throw new ArgumentNullException(connectionKeyOrConnectionString, "参数connectionKeyOrConnectionString不能为空值");
            }
            string str = "";
            if (connectionKeyOrConnectionString.Split(new char[] { '=' }).Count<string>() > 2)
            {
                str = connectionKeyOrConnectionString;
            }
            else
            {
                str = EncryptConnectionHelper.ConnectionString(connectionKeyOrConnectionString);
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException("对不起你设置的连接:" + connectionKeyOrConnectionString + "获取不到相关值");
            }
            return str;
        }

        public List<T> GetFieldValueList<T>(string tableName, string Condition, string fieldName)
        {
            return GetFieldValueList<T>(this._connectionKeyOrConnectionString, tableName, Condition, fieldName);
        }

        public static List<T> GetFieldValueList<T>(string connectionKeyOrConnectionString, string tableName, string condition, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("fieldName 不能为空值");
            }
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName 不能为空值");
            }
            List<T> list = new List<T>();
            string commandText = "select " + fieldName + " from " + tableName;
            if (!string.IsNullOrEmpty(condition))
            {
                commandText = commandText + "  where " + condition;
            }
            DataTable table = ExecuteDataTable(connectionKeyOrConnectionString, commandText, CommandType.Text, null);
            foreach (DataRow row in table.Rows)
            {
                list.Add((T) row[fieldName]);
            }
            return list;
        }

        private static void PrepareCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction, SqlCommand sqlCommand, CommandType commandType, string commandText, SqlParameter[] parms)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            if (sqlTransaction != null)
            {
                sqlCommand.Transaction = sqlTransaction;
            }
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = commandText;
            sqlCommand.CommandType = commandType;
            if (parms != null)
            {
                sqlCommand.Parameters.AddRange(parms);
            }
        }

        public DataTable ShowPageRecord(string tableName, string fields, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount)
        {
            return ShowPageRecord(this._connectionKeyOrConnectionString, tableName, fields, condition, sortExpression, pageSize, pageIndex, out recordCount);
        }

        public static DataTable ShowPageRecord(string connectionKeyOrConnectionString, string TableName, string Fields, string Condition, string SortExpression, int PageSize, int PageIndex, out int RecordCount)
        {
            if (string.IsNullOrEmpty(SortExpression))
            {
                throw new ArgumentNullException("SortExpression");
            }
            if (PageSize <= 0)
            {
                throw new ArgumentException("请输入大于0的整数", "PageSize");
            }
            if (PageIndex < 0)
            {
                throw new ArgumentException("请输入大于等于0的整数", "PageIndex");
            }
            RecordCount = 0;
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@tblName", TableName), new SqlParameter("@strGetFields", string.IsNullOrEmpty(Fields) ? "*" : Fields), new SqlParameter("@strWhere", Condition), new SqlParameter("@strOrder", SortExpression), new SqlParameter("@PageSize", PageSize), new SqlParameter("@PageIndex", PageIndex), new SqlParameter("@RecordCount", (int) RecordCount) };
            prams[6].Direction = ParameterDirection.Output;
            DataTable table = ExecuteDataTable(connectionKeyOrConnectionString, "Sys_ShowPageRecord", CommandType.StoredProcedure, prams);
            RecordCount = (int) prams[6].Value;
            return table;
        }
    }
}

