namespace WTF.Framework
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class DataSyncHelper : SqlHelper
    {
        public const string BeginTransaction = "BEGIN TRANSACTION";
        public const string EndTransaction = "\r\nIF @@ERROR <> 0\r\n    IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION\r\nGO\r\nIF @@TRANCOUNT = 1\r\nBEGIN\r\n    PRINT 'Data Update  Successfully Completed'\r\n    COMMIT TRANSACTION\r\nEND ELSE\r\nBEGIN\r\n    PRINT 'Data Update  Failed'\r\nEND\r\nGO";
        public const string HeaderQuery = "\r\nSET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL, XACT_ABORT ON\r\nGO\r\n  SET NUMERIC_ROUNDABORT OFF\r\nGO";
        private const string TableSchemaQuery = "SELECT   \r\n\tclmns.[name] AS [FieldName],\r\n \r\n\tISNULL(baset.[name], N'') AS [DataType],\r\n\tCAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],\r\n\tCAST(clmns.isnullable AS bit) AS [IsNullable], \r\n\tCAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') AS int) AS [Identity],\r\n  CASE WHEN EXISTS((SELECT  * FROM SysColumns s WHERE s.id=Object_Id(tbl.[name]) and s.colid=(select top 1 keyno from sysindexkeys where id=Object_Id(tbl.[name])) and clmns.[name]=s.name )) then 1 ELSE 0 end  as IsKey\r\nFROM dbo.sysobjects AS tbl WITH (NOLOCK)\r\n\tINNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id\r\n\tLEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype\r\n\r\nWHERE (tbl.[type] = 'U') and  tbl.[name]=@tableName";

        public DataSyncHelper(string connectionKeyOrConnectionString) : base(connectionKeyOrConnectionString)
        {
            this.UpdateFieldType = WTF.Framework.UpdateFieldType.UpdateField;
        }

        public string CreateSyncInsertQuery(string tableName, string Condition, bool isTransaction, bool isAddHeader = true)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("参数tableName不能为空");
            }
            DataTable table = this.SchemaTable(tableName);
            string commandText = "select * from " + tableName;
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + "  where " + Condition;
            }
            DataTable table2 = base.ExecuteDataTable(commandText);
            StringBuilder builder = new StringBuilder();
            bool flag = table.Select("Identity=1").Length > 0;
            DataRow[] rowArray = table.Select("IsKey=1");
            if (rowArray.Length <= 0)
            {
                throw new ArgumentException("该表未设置主键无法生成更新语句");
            }
            string str2 = rowArray[0]["FieldName"].ToString();
            if (isAddHeader)
            {
                builder.AppendLine("\r\nSET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL, XACT_ABORT ON\r\nGO\r\n  SET NUMERIC_ROUNDABORT OFF\r\nGO");
            }
            if (isTransaction)
            {
                builder.AppendLine("BEGIN TRANSACTION");
            }
            if (flag)
            {
                builder.AppendLine("SET IDENTITY_INSERT " + tableName + " ON");
                builder.AppendLine("GO");
            }
            foreach (DataRow row in table2.Rows)
            {
                builder.AppendLine("IF  NOT  EXISTS(SELECT * from " + tableName + " WHERE " + str2 + "=" + this.GetFieldValue(table2.Columns[str2].DataType, row[str2]) + " )");
                builder.AppendLine("BEGIN");
                string str3 = "INSERT INTO " + tableName + " (";
                string str4 = "VALUES (";
                foreach (DataColumn column in table2.Columns)
                {
                    string fieldValue = this.GetFieldValue(column.DataType, row[column.ColumnName]);
                    str3 = str3 + column.ColumnName + ",";
                    str4 = str4 + fieldValue + ",";
                }
                builder.AppendLine((str3.TrimEnd(new char[] { ',' }) + ")") + (str4.TrimEnd(new char[] { ',' }) + ")") + ";");
                builder.AppendLine("END");
                builder.AppendLine("");
            }
            if (flag)
            {
                builder.AppendLine("SET IDENTITY_INSERT " + tableName + " OFF");
                builder.AppendLine("GO");
            }
            if (isTransaction)
            {
                builder.AppendLine(string.Format("\r\nIF @@ERROR <> 0\r\n    IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION\r\nGO\r\nIF @@TRANCOUNT = 1\r\nBEGIN\r\n    PRINT 'Data Update Of {0} Successfully Completed'\r\n    COMMIT TRANSACTION\r\nEND ELSE\r\nBEGIN\r\n    PRINT 'Data Update Of {0} Failed'\r\nEND\r\nGO", tableName));
            }
            return builder.ToString();
        }

        public string CreateSyncQuery(string tableName, string Condition, string UpdateFields, bool isTransaction, bool isAddHeader = true)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("参数tableName不能为空");
            }
            DataTable table = this.SchemaTable(tableName);
            string commandText = "select * from " + tableName;
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + "  where " + Condition;
            }
            DataTable table2 = base.ExecuteDataTable(commandText);
            StringBuilder builder = new StringBuilder();
            bool flag = table.Select("Identity=1").Length > 0;
            DataRow[] rowArray = table.Select("IsKey=1");
            if (rowArray.Length <= 0)
            {
                throw new ArgumentException("该表未设置主键无法生成更新语句");
            }
            string str2 = rowArray[0]["FieldName"].ToString();
            string[] array = new string[0];
            if (!string.IsNullOrEmpty(UpdateFields))
            {
                array = UpdateFields.Split(new char[] { ',' });
            }
            if (isAddHeader)
            {
                builder.AppendLine("\r\nSET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL, XACT_ABORT ON\r\nGO\r\n  SET NUMERIC_ROUNDABORT OFF\r\nGO");
            }
            if (isTransaction)
            {
                builder.AppendLine("BEGIN TRANSACTION");
            }
            if (flag)
            {
                builder.AppendLine("SET IDENTITY_INSERT " + tableName + " ON");
                builder.AppendLine("GO");
            }
            foreach (DataRow row in table2.Rows)
            {
                string str8;
                builder.AppendLine("IF  NOT  EXISTS(SELECT * from " + tableName + " WHERE " + str2 + "=" + this.GetFieldValue(table2.Columns[str2].DataType, row[str2]) + " )");
                builder.AppendLine("BEGIN");
                string str3 = "INSERT INTO " + tableName + " (";
                string str4 = "VALUES (";
                string str5 = "UPDATE " + tableName + " SET ";
                foreach (DataColumn column in table2.Columns)
                {
                    string fieldValue = this.GetFieldValue(column.DataType, row[column.ColumnName]);
                    str3 = str3 + column.ColumnName + ",";
                    str4 = str4 + fieldValue + ",";
                    if (this.UpdateFieldType == WTF.Framework.UpdateFieldType.RemoveField)
                    {
                        if (!(!(str2 != column.ColumnName) || array.Contains<string>(column.ColumnName)))
                        {
                            str8 = str5;
                            str5 = str8 + column.ColumnName + "=" + fieldValue + ",";
                        }
                    }
                    else if (((str2 != column.ColumnName) && array.Contains<string>(column.ColumnName)) || (array.Length == 0))
                    {
                        str8 = str5;
                        str5 = str8 + column.ColumnName + "=" + fieldValue + ",";
                    }
                }
                builder.AppendLine((str3.TrimEnd(new char[] { ',' }) + ")") + (str4.TrimEnd(new char[] { ',' }) + ")") + ";");
                builder.AppendLine("END");
                builder.AppendLine("ELSE ");
                builder.AppendLine("BEGIN ");
                str8 = str5.TrimEnd(new char[] { ',' });
                str5 = str8 + " WHERE " + str2 + "=" + this.GetFieldValue(table2.Columns[str2].DataType, row[str2]);
                builder.AppendLine(str5);
                builder.AppendLine("END");
                builder.AppendLine("");
            }
            if (flag)
            {
                builder.AppendLine("SET IDENTITY_INSERT " + tableName + " OFF");
                builder.AppendLine("GO");
            }
            if (isTransaction)
            {
                builder.AppendLine(string.Format("\r\nIF @@ERROR <> 0\r\n    IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION\r\nGO\r\nIF @@TRANCOUNT = 1\r\nBEGIN\r\n    PRINT 'Data Update Of {0} Successfully Completed'\r\n    COMMIT TRANSACTION\r\nEND ELSE\r\nBEGIN\r\n    PRINT 'Data Update Of {0} Failed'\r\nEND\r\nGO", tableName));
            }
            return builder.ToString();
        }

        public string CreateSyncUpdateQuery(string tableName, string Condition, string UpdateFields, bool isTransaction, bool isAddHeader = true)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("参数tableName不能为空");
            }
            DataTable table = this.SchemaTable(tableName);
            string commandText = "select * from " + tableName;
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + "  where " + Condition;
            }
            DataTable table2 = base.ExecuteDataTable(commandText);
            StringBuilder builder = new StringBuilder();
            bool flag = table.Select("Identity=1").Length > 0;
            DataRow[] rowArray = table.Select("IsKey=1");
            if (rowArray.Length <= 0)
            {
                throw new ArgumentException("该表未设置主键无法生成更新语句");
            }
            string str2 = rowArray[0]["FieldName"].ToString();
            string[] array = new string[0];
            if (!string.IsNullOrEmpty(UpdateFields))
            {
                array = UpdateFields.Split(new char[] { ',' });
            }
            if (isAddHeader)
            {
                builder.AppendLine("\r\nSET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, QUOTED_IDENTIFIER, CONCAT_NULL_YIELDS_NULL, XACT_ABORT ON\r\nGO\r\n  SET NUMERIC_ROUNDABORT OFF\r\nGO");
            }
            if (isTransaction)
            {
                builder.AppendLine("BEGIN TRANSACTION");
            }
            foreach (DataRow row in table2.Rows)
            {
                string str6;
                string str3 = "UPDATE " + tableName + " SET ";
                foreach (DataColumn column in table2.Columns)
                {
                    string fieldValue = this.GetFieldValue(column.DataType, row[column.ColumnName]);
                    if (this.UpdateFieldType == WTF.Framework.UpdateFieldType.RemoveField)
                    {
                        if (!(!(str2 != column.ColumnName) || array.Contains<string>(column.ColumnName)))
                        {
                            str6 = str3;
                            str3 = str6 + column.ColumnName + "=" + fieldValue + ",";
                        }
                    }
                    else if (((str2 != column.ColumnName) && array.Contains<string>(column.ColumnName)) || (array.Length == 0))
                    {
                        str6 = str3;
                        str3 = str6 + column.ColumnName + "=" + fieldValue + ",";
                    }
                }
                str6 = str3.TrimEnd(new char[] { ',' });
                str3 = str6 + " WHERE " + str2 + "=" + this.GetFieldValue(table2.Columns[str2].DataType, row[str2]);
                builder.AppendLine(str3);
                builder.AppendLine("");
            }
            if (isTransaction)
            {
                builder.AppendLine(string.Format("\r\nIF @@ERROR <> 0\r\n    IF @@TRANCOUNT = 1 ROLLBACK TRANSACTION\r\nGO\r\nIF @@TRANCOUNT = 1\r\nBEGIN\r\n    PRINT 'Data Update Of {0} Successfully Completed'\r\n    COMMIT TRANSACTION\r\nEND ELSE\r\nBEGIN\r\n    PRINT 'Data Update Of {0} Failed'\r\nEND\r\nGO", tableName));
            }
            return builder.ToString();
        }

        private string GetFieldValue(Type dataType, object value)
        {
            if (value.GetType() == typeof(DBNull))
            {
                return "null";
            }
            if ((((dataType == typeof(string)) || (dataType == typeof(Guid))) || (dataType == typeof(DateTime))) || (dataType == typeof(string)))
            {
                return ("'" + value.ToString() + "'");
            }
            if ((((((dataType != typeof(int)) && (dataType != typeof(int))) && ((dataType != typeof(short)) && (dataType != typeof(short)))) && (((dataType != typeof(float)) && (dataType != typeof(double))) && (dataType != typeof(double)))) && (dataType != typeof(long))) && (dataType == typeof(bool)))
            {
                return (((bool) value) ? "1" : "0");
            }
            return value.ToString();
        }

        public DataTable SchemaTable(string tableName)
        {
            SqlParameter[] prams = new SqlParameter[] { new SqlParameter("@tableName", tableName) };
            return base.ExecuteDataTable("SELECT   \r\n\tclmns.[name] AS [FieldName],\r\n \r\n\tISNULL(baset.[name], N'') AS [DataType],\r\n\tCAST(CASE WHEN baset.[name] IN (N'char', N'varchar', N'binary', N'varbinary', N'nchar', N'nvarchar') THEN clmns.prec ELSE clmns.length END AS int) AS [Length],\r\n\tCAST(clmns.isnullable AS bit) AS [IsNullable], \r\n\tCAST(COLUMNPROPERTY(clmns.id, clmns.[name], N'IsIdentity') AS int) AS [Identity],\r\n  CASE WHEN EXISTS((SELECT  * FROM SysColumns s WHERE s.id=Object_Id(tbl.[name]) and s.colid=(select top 1 keyno from sysindexkeys where id=Object_Id(tbl.[name])) and clmns.[name]=s.name )) then 1 ELSE 0 end  as IsKey\r\nFROM dbo.sysobjects AS tbl WITH (NOLOCK)\r\n\tINNER JOIN dbo.syscolumns AS clmns WITH (NOLOCK) ON clmns.id=tbl.id\r\n\tLEFT JOIN dbo.systypes AS baset WITH (NOLOCK) ON baset.xusertype = clmns.xtype and baset.xusertype = baset.xtype\r\n\r\nWHERE (tbl.[type] = 'U') and  tbl.[name]=@tableName", CommandType.Text, prams);
        }

        public WTF.Framework.UpdateFieldType UpdateFieldType { get; set; }
    }
}

