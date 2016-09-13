using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Linq;

namespace WTF.CodeRule
{
	public class MySqlHelper
	{
		private string _connectionKeyOrConnectionString = "WTFConnectionString";

		public MySqlHelper()
		{
		}

		public MySqlHelper(string _connectionKeyOrConnectionString)
		{
			this._connectionKeyOrConnectionString = _connectionKeyOrConnectionString;
		}

		public int ExecuteNonQuery(string commandText)
		{
			return this.ExecuteNonQuery(commandText, CommandType.Text);
		}

		public int ExecuteNonQuery(string commandText, CommandType commandType)
		{
			return this.ExecuteNonQuery(commandText, commandType, null);
		}

		public int ExecuteNonQuery(string commandText, CommandType commandType, MySqlParameter[] param)
		{
			return MySqlHelper.ExecuteNonQuery(this._connectionKeyOrConnectionString, commandText, commandType, param);
		}

		public object ExecuteScalar(string commandText)
		{
			return this.ExecuteScalar(commandText, CommandType.Text);
		}

		public object ExecuteScalar(string commandText, CommandType commandType)
		{
			return this.ExecuteScalar(commandText, commandType, null);
		}

		public object ExecuteScalar(string commandText, CommandType commandType, MySqlParameter[] param)
		{
			return MySqlHelper.ExecuteScalar(this._connectionKeyOrConnectionString, commandText, commandType, param);
		}

		public MySqlDataReader ExecuteReader(string commandText, CommandType commandType, MySqlParameter[] prams)
		{
			return MySqlHelper.ExecuteReader(this._connectionKeyOrConnectionString, commandText, commandType, prams);
		}

		public MySqlDataReader ExecuteReader(string commandText, CommandType commandType)
		{
			return this.ExecuteReader(commandText, commandType, null);
		}

		public MySqlDataReader ExecuteReader(string commandText)
		{
			return this.ExecuteReader(commandText, CommandType.Text);
		}

		public DataTable ExecuteDataTable(string commandText)
		{
			return this.ExecuteDataTable(commandText, CommandType.Text, null, "");
		}

		public DataTable ExecuteDataTable(string commandText, CommandType commandType)
		{
			return this.ExecuteDataTable(commandText, commandType, null, "");
		}

		public DataTable ExecuteDataTable(string commandText, CommandType commandType, MySqlParameter[] prams, string CodeConnectionString = "")
		{
			if (!string.IsNullOrWhiteSpace(CodeConnectionString))
			{
				this._connectionKeyOrConnectionString = CodeConnectionString;
			}
			return MySqlHelper.ExecuteDataTable(this._connectionKeyOrConnectionString, commandText, commandType, prams);
		}

		public DataSet ExecuteDataSet(string commandText)
		{
			return this.ExecuteDataSet(commandText, CommandType.Text);
		}

		public DataSet ExecuteDataSet(string commandText, CommandType commandType)
		{
			return this.ExecuteDataSet(commandText, commandType, null);
		}

		public DataSet ExecuteDataSet(string commandText, CommandType commandType, MySqlParameter[] prams)
		{
			return MySqlHelper.ExecuteDataSet(this._connectionKeyOrConnectionString, commandText, commandType, prams);
		}

		public static string GetConnectionString(string connectionKeyOrConnectionString)
		{
			if (string.IsNullOrWhiteSpace(connectionKeyOrConnectionString))
			{
				throw new ArgumentNullException(connectionKeyOrConnectionString, "参数connectionKeyOrConnectionString不能为空值");
			}
			string text;
			if (connectionKeyOrConnectionString.Split(new char[]
			{
				'='
			}).Count<string>() > 2)
			{
				text = connectionKeyOrConnectionString;
			}
			else
			{
				text = ConfigurationManager.ConnectionStrings[connectionKeyOrConnectionString].ToString();
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException("对不起你设置的连接:" + connectionKeyOrConnectionString + "获取不到相关值");
			}
			return text;
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
				for (int i = 0; i < parms.Length; i++)
				{
					MySqlParameter value = parms[i];
					sqlCommand.Parameters.Add(value);
				}
			}
		}

		public static int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText)
		{
			return MySqlHelper.ExecuteNonQuery(connectionKeyOrConnectionString, commandText, CommandType.Text);
		}

		public static int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
		{
			return MySqlHelper.ExecuteNonQuery(connectionKeyOrConnectionString, commandText, commandType, null);
		}

		public static int ExecuteNonQuery(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			int result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(MySqlHelper.GetConnectionString(connectionKeyOrConnectionString)))
			{
				MySqlHelper.PrepareCommand(mySqlConnection, null, mySqlCommand, commandType, commandText, parms);
				int num = mySqlCommand.ExecuteNonQuery();
				mySqlCommand.Parameters.Clear();
				result = num;
			}
			return result;
		}

		public static int ExecuteNonQuery(MySqlConnection conn, CommandType commandType, string commandText, params MySqlParameter[] parms)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			MySqlHelper.PrepareCommand(conn, null, mySqlCommand, commandType, commandText, parms);
			int result = mySqlCommand.ExecuteNonQuery();
			mySqlCommand.Parameters.Clear();
			return result;
		}

		public static int ExecuteNonQuery(MySqlTransaction trans, CommandType commandType, string commandText, params MySqlParameter[] parms)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			MySqlHelper.PrepareCommand(trans.Connection, trans, mySqlCommand, commandType, commandText, parms);
			int result = mySqlCommand.ExecuteNonQuery();
			mySqlCommand.Parameters.Clear();
			return result;
		}

		public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText)
		{
			return MySqlHelper.ExecuteScalar(connectionKeyOrConnectionString, commandText, CommandType.Text);
		}

		public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
		{
			return MySqlHelper.ExecuteScalar(connectionKeyOrConnectionString, commandText, commandType, null);
		}

		public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			object result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(MySqlHelper.GetConnectionString(connectionKeyOrConnectionString)))
			{
				MySqlHelper.PrepareCommand(mySqlConnection, null, mySqlCommand, commandType, commandText, parms);
				object obj = mySqlCommand.ExecuteScalar();
				mySqlCommand.Parameters.Clear();
				result = obj;
			}
			return result;
		}

		public static object ExecuteScalar(MySqlConnection conn, CommandType commandType, string commandText, params MySqlParameter[] parms)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			MySqlHelper.PrepareCommand(conn, null, mySqlCommand, commandType, commandText, parms);
			object result = mySqlCommand.ExecuteScalar();
			mySqlCommand.Parameters.Clear();
			return result;
		}

		public static MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
		{
			return MySqlHelper.ExecuteReader(connectionKeyOrConnectionString, commandText, commandType, null);
		}

		public static MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText)
		{
			return MySqlHelper.ExecuteReader(connectionKeyOrConnectionString, commandText, CommandType.Text);
		}

		public static MySqlDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			MySqlConnection mySqlConnection = new MySqlConnection(MySqlHelper.GetConnectionString(connectionKeyOrConnectionString));
			MySqlDataReader result;
			try
			{
				MySqlHelper.PrepareCommand(mySqlConnection, null, mySqlCommand, commandType, commandText, parms);
				MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				mySqlCommand.Parameters.Clear();
				result = mySqlDataReader;
			}
			catch
			{
				mySqlConnection.Close();
				throw;
			}
			return result;
		}

		public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText)
		{
			return MySqlHelper.ExecuteDataSet(connectionKeyOrConnectionString, commandText, CommandType.Text);
		}

		public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
		{
			return MySqlHelper.ExecuteDataSet(connectionKeyOrConnectionString, commandText, commandType, null);
		}

		public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			DataSet result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(MySqlHelper.GetConnectionString(connectionKeyOrConnectionString)))
			{
				MySqlHelper.PrepareCommand(mySqlConnection, null, mySqlCommand, commandType, commandText, parms);
				MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
				DataSet dataSet = new DataSet();
				mySqlDataAdapter.Fill(dataSet);
				mySqlConnection.Close();
				mySqlCommand.Parameters.Clear();
				result = dataSet;
			}
			return result;
		}

		public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText)
		{
			return MySqlHelper.ExecuteDataTable(connectionKeyOrConnectionString, commandText, CommandType.Text, null);
		}

		public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText, CommandType commandType)
		{
			return MySqlHelper.ExecuteDataTable(connectionKeyOrConnectionString, commandText, commandType, null);
		}

		public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText, CommandType commandType, params MySqlParameter[] parms)
		{
			MySqlCommand mySqlCommand = new MySqlCommand();
			DataTable result;
			using (MySqlConnection mySqlConnection = new MySqlConnection(MySqlHelper.GetConnectionString(connectionKeyOrConnectionString)))
			{
				MySqlHelper.PrepareCommand(mySqlConnection, null, mySqlCommand, commandType, commandText, parms);
				MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
				DataTable dataTable = new DataTable();
				mySqlDataAdapter.Fill(dataTable);
				mySqlConnection.Close();
				mySqlCommand.Parameters.Clear();
				result = dataTable;
			}
			return result;
		}
	}
}
