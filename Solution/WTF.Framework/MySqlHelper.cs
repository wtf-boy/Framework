namespace WTF.Framework
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
    using System.Linq;

    public class MySqlHelper
    {
        private string _connectionKeyOrConnectionString;

        public MySqlHelper()
        {
            this._connectionKeyOrConnectionString = "SevenConnectionString";
        }

        public MySqlHelper(string _connectionKeyOrConnectionString)
        {
            this._connectionKeyOrConnectionString = "SevenConnectionString";
            this._connectionKeyOrConnectionString = _connectionKeyOrConnectionString;
        }

        public DataSet ExecuteDataSet(string commandText, params MySqlParameter[] prams)
        {
            return this.ExecuteDataSet(commandText, CommandType.Text, prams);
        }

        public DataSet ExecuteDataSet(string commandText, CommandType commandType, params MySqlParameter[] prams)
        {
            return ExecuteDataSet(this._connectionKeyOrConnectionString, commandText, commandType, prams);
        }

        public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return ExecuteDataSet(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                PrepareCommand(connection, null, sqlCommand, commandType, commandText, parms);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                connection.Close();
                sqlCommand.Parameters.Clear();
                return dataSet;
            }
        }

        public DataTable ExecuteDataTable(string commandText, params MySqlParameter[] prams)
        {
            return this.ExecuteDataTable(commandText, CommandType.Text, prams);
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType, params MySqlParameter[] prams)
        {
            return ExecuteDataTable(this._connectionKeyOrConnectionString, commandText, commandType, prams);
        }

        public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return ExecuteDataTable(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                PrepareCommand(connection, null, sqlCommand, commandType, commandText, parms);
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                connection.Close();
                sqlCommand.Parameters.Clear();
                return dataTable;
            }
        }

        public int ExecuteNonQuery(string commandText, params MySqlParameter[] param)
        {
            return this.ExecuteNonQuery(commandText, CommandType.Text, param);
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, params MySqlParameter[] param)
        {
            return ExecuteNonQuery(this._connectionKeyOrConnectionString, commandText, commandType, param);
        }

        public static int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return ExecuteNonQuery(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public static int ExecuteNonQuery(MySqlConnection conn, CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            PrepareCommand(conn, null, sqlCommand, commandType, commandText, parms);
            int num = sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            PrepareCommand(trans.Connection, trans, sqlCommand, commandType, commandText, parms);
            int num = sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                PrepareCommand(connection, null, sqlCommand, commandType, commandText, parms);
                int num = sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                return num;
            }
        }

        public MySqlDataReader ExecuteReader(string commandText, params MySqlParameter[] prams)
        {
            return this.ExecuteReader(commandText, CommandType.Text, prams);
        }

        public MySqlDataReader ExecuteReader(string commandText, CommandType commandType, params MySqlParameter[] prams)
        {
            return ExecuteReader(this._connectionKeyOrConnectionString, commandText, commandType, prams);
        }

        public static MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return ExecuteReader(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public static MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            MySqlDataReader reader2;
            MySqlCommand sqlCommand = new MySqlCommand();
            MySqlConnection sqlConnection = new MySqlConnection(GetConnectionString(connectionKeyOrConnectionString));
            try
            {
                PrepareCommand(sqlConnection, null, sqlCommand, commandType, commandText, parms);
                MySqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                sqlCommand.Parameters.Clear();
                reader2 = reader;
            }
            catch
            {
                sqlConnection.Close();
                throw;
            }
            return reader2;
        }

        public object ExecuteScalar(string commandText, params MySqlParameter[] param)
        {
            return this.ExecuteScalar(commandText, CommandType.Text, param);
        }

        public object ExecuteScalar(string commandText, CommandType commandType, params MySqlParameter[] param)
        {
            return ExecuteScalar(this._connectionKeyOrConnectionString, commandText, commandType, param);
        }

        public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] parms)
        {
            return ExecuteScalar(connectionKeyOrConnectionString, commandText, CommandType.Text, parms);
        }

        public static object ExecuteScalar(MySqlConnection conn, CommandType commandType, string commandText, params MySqlParameter[] parms)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            PrepareCommand(conn, null, sqlCommand, commandType, commandText, parms);
            object obj2 = sqlCommand.ExecuteScalar();
            sqlCommand.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(connectionKeyOrConnectionString)))
            {
                PrepareCommand(connection, null, sqlCommand, commandType, commandText, parms);
                object obj2 = sqlCommand.ExecuteScalar();
                sqlCommand.Parameters.Clear();
                return obj2;
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

        private static void PrepareCommand(MySqlConnection sqlConnection, MySqlTransaction sqlTransaction, MySqlCommand sqlCommand, CommandType commandType, string commandText, MySqlParameter[] parms)
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = commandText;
            if (sqlTransaction != null)
            {
                sqlCommand.Transaction = sqlTransaction;
            }
            sqlCommand.CommandType = commandType;
            if (parms != null)
            {
                foreach (MySqlParameter parameter in parms)
                {
                    sqlCommand.Parameters.Add(parameter);
                }
            }
        }
    }
}

