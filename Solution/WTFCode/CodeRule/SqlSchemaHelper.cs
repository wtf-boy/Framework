using MySql.Data.MySqlClient;
using WTFCode.CodeRule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using WTFCode;

namespace WTF.CodeRule
{
	public class SqlSchemaHelper
	{
		private static MySqlHelper _CodeSqlHelper = null;

		private static MySqlHelper _MySqlHelper = null;

		public static MySqlHelper CodeSqlHelper
		{
			get
			{
				if (SqlSchemaHelper._CodeSqlHelper == null)
				{
					SqlSchemaHelper._CodeSqlHelper = new MySqlHelper("CodeConnectionString");
				}
				return SqlSchemaHelper._CodeSqlHelper;
			}
		}

		public static MySqlHelper MySqlHelper
		{
			get
			{
				if (SqlSchemaHelper._MySqlHelper == null)
				{
					SqlSchemaHelper._MySqlHelper = new MySqlHelper("WTFConnectionString");
				}
				return SqlSchemaHelper._MySqlHelper;
			}
		}

		public static DataTable GetAllSchemata()
		{
			return SqlSchemaHelper.CodeSqlHelper.ExecuteDataTable(" SELECT * FROM INFORMATION_SCHEMA.SCHEMATA");
		}

		public static DataTable GetAllSchemata(string CodeConnectionString)
		{
			return SqlSchemaHelper.CodeSqlHelper.ExecuteDataTable(" SELECT * FROM INFORMATION_SCHEMA.SCHEMATA", CommandType.Text, null, CodeConnectionString);
		}

		public static DataTable GetAllTables(string SchemaName, string CodeConnectionString, string TableType = "BASE TABLE")
		{
			MySqlParameter[] prams = new MySqlParameter[]
			{
				new MySqlParameter("?TableType", TableType),
				new MySqlParameter("?SchemaName", SchemaName)
			};
			return SqlSchemaHelper.CodeSqlHelper.ExecuteDataTable(Resources.GetTables, CommandType.Text, prams, CodeConnectionString);
		}

		public static string CreateEntity(string EntityName)
		{
			return string.Format(Resources.GetEntitys, EntityName);
		}

		public static DataTable GetLogType(string ConfigPath)
		{
			DataTable result;
			if (!string.IsNullOrWhiteSpace(ConfigPath))
			{
				result = SqlSchemaHelper.MySqlHelper.ExecuteDataTable("select * from loger_moduletype", CommandType.Text, null, Common.GetConfigConnectionString(ConfigPath));
			}
			else
			{
				result = SqlSchemaHelper.MySqlHelper.ExecuteDataTable("select * from loger_moduletype");
			}
			return result;
		}

		public static List<TableRuleSchema> GetAllRuleTables(string SchemaName, string CodeConnectionString, bool IsTable = true)
		{
			List<TableRuleSchema> list = new List<TableRuleSchema>();
			foreach (DataRow dataRow in SqlSchemaHelper.GetAllTables(SchemaName, CodeConnectionString, IsTable ? "BASE TABLE" : "VIEW").Rows)
			{
				string text = "";
				string text2 = dataRow["TableName"].ToString().Replace("_tb", "");
				string[] array = text2.Split(new char[]
				{
					'_'
				});
				if (array.Length == 1)
				{
					text2 = array[0];
				}
				else
				{
					text2 = array[1];
				}
				text = text + char.ToUpper(text2[0]) + text2.Substring(1);
				list.Add(new TableRuleSchema
				{
					UIProjectName = "",
					UIProjectPath = "",
					ConnectionKeyOrConnectionString = "",
					LogModuleType = "",
					IsMongoDB = false,
					IsDelete = true,
					IsInsert = true,
					IsUpdate = true,
					TableName = dataRow["TableName"].ToString(),
					Description = Regex.Replace(dataRow["Description"].ToString(), "[A-Za-z0-9_]", "", RegexOptions.IgnoreCase),
					EntityName = text,
					ModuleID = "",
					ListUrl = "",
					EditUrl = ""
				});
			}
			return list;
		}

		public static string GetControlType(string DataType)
		{
			string result;
			switch (DataType)
			{
			case "int":
				result = "DropDown";
				return result;
			case "varchar":
				result = "TextBox";
				return result;
			case "nvarchar":
				result = "TextBox";
				return result;
			case "longtext":
				result = "Xhtml";
				return result;
			case "text":
				result = "Xhtml";
				return result;
			case "bit":
				result = "CheckBox";
				return result;
			case "datetime":
				result = "TextDateTime";
				return result;
			case "date":
				result = "TextDate";
				return result;
			}
			result = "TextBox";
			return result;
		}

		public static DataTable GetTableForeign(string tableName)
		{
			MySqlParameter[] prams = new MySqlParameter[]
			{
				new MySqlParameter("@tableName", tableName)
			};
			return SqlSchemaHelper.CodeSqlHelper.ExecuteDataTable(Resources.GetTableForeign, CommandType.Text, prams, "");
		}

		public static ColumnEditKeySchema GetTableForeignSchema(string SchemaName, string tableName)
		{
			MySqlParameter[] prams = new MySqlParameter[]
			{
				new MySqlParameter("?tableName", tableName)
			};
			DataTable dataTable = SqlSchemaHelper.CodeSqlHelper.ExecuteDataTable(Resources.GetTableForeign, CommandType.Text, prams, "");
			ColumnEditKeySchema result;
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in SqlSchemaHelper.GetTableColumns(SchemaName, tableName, "").Rows)
				{
					if (dataRow["FieldName"].ToString() == dataTable.Rows[0]["ForeignColumnName"].ToString() && dataTable.Rows[0]["PrimaryTableName"].ToString() != tableName)
					{
						result = new ColumnEditKeySchema
						{
							FieldName = dataRow["FieldName"].ToString(),
							DateType = dataRow["DataType"].ToString()
						};
						return result;
					}
				}
				result = null;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static string GetTableForeignFileName(string SchemaName, string tableName, string ConnectionString = "")
		{
			MySqlParameter[] prams = new MySqlParameter[]
			{
				new MySqlParameter("?tableName", tableName),
				new MySqlParameter("?tableSchema", SchemaName)
			};
			DataTable dataTable = SqlSchemaHelper.CodeSqlHelper.ExecuteDataTable(Resources.GetTableForeign, CommandType.Text, prams, ConnectionString);
			string result;
			if (dataTable.Rows.Count > 0)
			{
				result = dataTable.Rows[0]["ForeignColumnName"].ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}

		public static ColumnEditKeySchema GetTablePrimarySchema(string SchemaName, string tableName)
		{
			ColumnEditKeySchema result;
			foreach (DataRow dataRow in SqlSchemaHelper.GetTableColumns(SchemaName, tableName, "").Rows)
			{
				if (dataRow["FieldName"].ToString() == dataRow["TableKey"].ToString())
				{
					result = new ColumnEditKeySchema
					{
						FieldName = dataRow["FieldName"].ToString(),
						DateType = dataRow["DataType"].ToString()
					};
					return result;
				}
			}
			result = null;
			return result;
		}

		public static List<ColumnRuleSchema> GetTableRuleColumnsSchema(string Connectionstring, string SchemaName, string tableName, CodeConfigHelper objCodeConfigHelper)
		{
			DataTable dataTable = new DataTable("TableColumns");
			string tableForeignFileName = SqlSchemaHelper.GetTableForeignFileName(SchemaName, tableName, Connectionstring);
			List<ColumnRuleSchema> list = new List<ColumnRuleSchema>();
			foreach (DataRow dataRow in SqlSchemaHelper.GetTableColumns(SchemaName, tableName, Connectionstring).Rows)
			{
				bool isXmlField = false;
				string text = "请输入" + dataRow["Description"];
				XmlNode xmlNode = objCodeConfigHelper.CodeConfig.SelectSingleNode(string.Concat(new string[]
				{
					"//BusinessConfig/Business[@TableName='",
					tableName,
					"']/Field[@FieldName='",
					dataRow["FieldName"].ToString(),
					"']"
				}));
				bool isCheck;
				if (xmlNode == null)
				{
					isCheck = false;
				}
				else
				{
					isCheck = bool.Parse(xmlNode.Attributes["IsCheck"].Value);
					text = xmlNode.Attributes["ErrorMessage"].Value;
					isXmlField = true;
				}
				string text2 = (xmlNode == null) ? dataRow["Description"].ToString() : xmlNode.Attributes["FieldTitle"].Value;
				list.Add(new ColumnRuleSchema
				{
					TableName = tableName,
					IsXmlField = isXmlField,
					DataType = dataRow["DataType"].ToString(),
					IsIdentity = (dataRow["Identity"].ToString() == "1"),
					IsUnsigned = (dataRow["IsUnsigned"].ToString() == "1"),
					Length = ((dataRow["DataType"].ToString() == "longtext") ? 0 : int.Parse(dataRow["Length"].ToString())),
					IsEmpty = (dataRow["IsNullable"].ToString() == "1"),
					FieldName = dataRow["FieldName"].ToString(),
					Description = dataRow["Description"].ToString(),
					FieldTitle = text2,
					IsCheck = isCheck,
					ErrorMessage = text,
					OldErrorMessage = text,
					OldFieldTitle = text2,
					ColumnType = ((dataRow["IsKey"].ToString() == "1") ? "PrimaryKey" : ((dataRow["FieldName"].ToString() == tableForeignFileName) ? "ForeignKey" : "Common"))
				});
			}
			return list;
		}

		public static TableListSchema GetTableListSchema(string SchemaName, string tableName, string moduleID, string url, string ConnectionString, CodeConfigHelper objCodeConfigHelper, string WTFConfigPath)
		{
			TableListSchema tableListSchema = new TableListSchema();
			tableListSchema.TableName = tableName;
			DataTable dataTable = new DataTable("TableColumns");
			string tableForeignFileName = SqlSchemaHelper.GetTableForeignFileName(SchemaName, tableName, ConnectionString);
			foreach (DataRow dataRow in SqlSchemaHelper.GetTableColumns(SchemaName, tableName, ConnectionString).Rows)
			{
				XmlNode xmlNode = objCodeConfigHelper.CodeConfig.SelectSingleNode(string.Concat(new string[]
				{
					"//BusinessConfig/Business[@TableName='",
					tableName,
					"']/Field[@FieldName='",
					dataRow["FieldName"].ToString(),
					"']"
				}));
				bool flag = xmlNode == null;
				tableListSchema.Columns.Add(new ColumnListSchema
				{
					TableName = tableName,
					IsIdentity = (dataRow["Identity"].ToString() == "1"),
					DataType = dataRow["DataType"].ToString(),
					FieldName = dataRow["FieldName"].ToString(),
					FieldTitle = (flag ? dataRow["Description"].ToString() : xmlNode.Attributes["FieldTitle"].Value),
					ControlType = ((dataRow["DataType"].ToString() == "int" || dataRow["DataType"].ToString() == "bit") ? "TemplateField" : "BoundField"),
					Width = ((dataRow["DataType"].ToString().IndexOf("datetime") >= 0) ? 120 : 0),
					IsShow = false,
					IsSearch = false,
					IsSort = false,
					FormatString = ((dataRow["DataType"].ToString().IndexOf("datetime") >= 0) ? "yyyy-MM-dd HH:mm:ss" : ""),
					ColumnType = ((dataRow["IsKey"].ToString() == "1") ? "PrimaryKey" : ((dataRow["FieldName"].ToString() == tableForeignFileName) ? "ForeignKey" : "Common"))
				});
			}
			string configConnectionString = Common.GetConfigConnectionString(WTFConfigPath);
			foreach (DataRow dataRow in SqlSchemaHelper.MySqlHelper.ExecuteDataTable(string.Format(Resources.GetCommand, moduleID), CommandType.Text, null, configConnectionString).Rows)
			{
				tableListSchema.Commands.Add(new CommandSchema
				{
					CommandName = dataRow["CommandName"].ToString(),
					ProcessType = ((dataRow["CommandName"].ToString().IndexOf("Modify") >= 0 || dataRow["CommandName"].ToString().IndexOf("View") >= 0) ? "RedirectState" : ((dataRow["CommandName"].ToString().IndexOf("Create") >= 0 || dataRow["CommandName"].ToString().IndexOf("Back") >= 0) ? "Redirect" : "RenderPage")),
					RedirectUrl = ((dataRow["CommandName"].ToString().IndexOf("Modify") >= 0 || dataRow["CommandName"].ToString().IndexOf("Create") >= 0 || dataRow["CommandName"].ToString().IndexOf("View") >= 0 || dataRow["CommandName"].ToString().IndexOf("Back") >= 0) ? url : ""),
					ModuleName = dataRow["ModuleName"].ToString(),
					IsTop = (dataRow["PlaceType"].ToString().IndexOf("101") >= 0),
					IsBotom = (dataRow["PlaceType"].ToString().IndexOf("103") >= 0),
					IsList = (dataRow["PlaceType"].ToString().IndexOf("102") >= 0),
					SortIndex = int.Parse(dataRow["SortIndex"].ToString())
				});
			}
			return tableListSchema;
		}

		public static List<ColumnEditSchema> GetTableColumnsSchema(string SchemaName, string tableName, CodeConfigHelper objCodeConfigHelper, string connectionString = "")
		{
			DataTable dataTable = new DataTable("TableColumns");
			string tableForeignFileName = SqlSchemaHelper.GetTableForeignFileName(SchemaName, tableName, connectionString);
			List<ColumnEditSchema> list = new List<ColumnEditSchema>();
			foreach (DataRow dataRow in SqlSchemaHelper.GetTableColumns(SchemaName, tableName, connectionString).Rows)
			{
				XmlNode xmlNode = objCodeConfigHelper.CodeConfig.SelectSingleNode(string.Concat(new string[]
				{
					"//BusinessConfig/Business[@TableName='",
					tableName,
					"']/Field[@FieldName='",
					dataRow["FieldName"].ToString(),
					"']"
				}));
				bool flag = xmlNode == null;
				list.Add(new ColumnEditSchema
				{
					TableName = tableName,
					DataType = dataRow["DataType"].ToString(),
					Length = ((dataRow["DataType"].ToString() == "longtext") ? 0 : int.Parse(dataRow["Length"].ToString())),
					FieldName = dataRow["FieldName"].ToString(),
					FieldTitle = (flag ? dataRow["Description"].ToString() : xmlNode.Attributes["FieldTitle"].Value),
					ControlType = SqlSchemaHelper.GetControlType(dataRow["DataType"].ToString()),
					IsShow = false,
					IsEmpty = (dataRow["IsNullable"].ToString() == "1"),
					ErrorMessage = (flag ? (((dataRow["DataType"].ToString() == "int" || dataRow["DataType"].ToString() == "bit") ? "请选择" : "请输入") + dataRow["Description"]) : xmlNode.Attributes["ErrorMessage"].Value),
					IsAutoValue = false,
					ColumnType = ((dataRow["IsKey"].ToString() == "1") ? "PrimaryKey" : ((dataRow["FieldName"].ToString() == tableForeignFileName) ? "ForeignKey" : "Common")),
					ValidationReg = ""
				});
			}
			return list;
		}

		public static DataTable GetTableColumns(string SchemaName, string tableName, string ConnectionString = "")
		{
			MySqlParameter[] prams = new MySqlParameter[]
			{
				new MySqlParameter("?tableName", tableName),
				new MySqlParameter("?SchemaName", SchemaName)
			};
			DataTable dataTable = new DataTable("TableColumns");
			return SqlSchemaHelper.CodeSqlHelper.ExecuteDataTable(Resources.GetTableColumns, CommandType.Text, prams, ConnectionString);
		}
	}
}
