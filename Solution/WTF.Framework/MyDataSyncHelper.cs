namespace WTF.Framework
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MyDataSyncHelper : WTF.Framework.MySqlHelper
    {
        private string _SyncEnd;
        private string _SyncHeader;
        private const string TableSchemaQuery = "SELECT  \r\nTABLE_NAME as TableName,\r\nCOLUMN_NAME AS FieldName ,\r\nDATA_TYPE   AS DataType,\r\nCOLUMN_COMMENT as Description,\r\nCASE WHEN IS_NULLABLE ='YES' \r\n THEN 1 ELSE 0 END   as IsNullable,\r\nCASE WHEN COLUMN_KEY ='PRI' \r\n THEN 1 ELSE 0 END   as IsKey,\r\n\r\n \r\n CASE WHEN EXTRA ='auto_increment'\r\n  \r\n THEN 1 ELSE 0 END AS Identity,\r\n\r\nCASE WHEN CHARACTER_MAXIMUM_LENGTH is not NULL\r\n  \r\n THEN CHARACTER_MAXIMUM_LENGTH\r\n \r\n ELSE 0 END AS Length\r\n\r\n  FROM INFORMATION_SCHEMA.COLUMNS   where table_name=?tableName  and TABLE_SCHEMA=?SchemaName;";

        public MyDataSyncHelper(string connectionKeyOrConnectionString) : base(connectionKeyOrConnectionString)
        {
            this._SyncHeader = "DELIMITER $$\r\n\r\nDROP PROCEDURE IF EXISTS `SevenDataSync`$$\r\n\r\nCREATE  PROCEDURE `SevenDataSync`(\r\n \r\n)\r\nBEGIN";
            this._SyncEnd = "END$$\r\nDELIMITER ;\r\nCALL SevenDataSync;\r\n\r\n\r\nDELIMITER $$\r\nDROP PROCEDURE IF EXISTS `SevenDataSync`$$\r\nDELIMITER ;";
            this.UpdateFieldType = WTF.Framework.UpdateFieldType.UpdateField;
        }

        public string CreateSyncInsertQuery(string schemaName, string tableName, string Condition, bool isTransaction, bool isAddHeader = true)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("参数tableName不能为空");
            }
            if (string.IsNullOrEmpty(schemaName))
            {
                throw new ArgumentNullException("参数schemaName不能为空");
            }
            DataTable table = this.SchemaTable(schemaName, tableName);
            string commandText = "select * from " + tableName;
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + "  where " + Condition;
            }
            DataTable table2 = base.ExecuteDataTable(commandText, new MySqlParameter[0]);
            StringBuilder builder = new StringBuilder();
            bool flag = table.Select("Identity=1").Length > 0;
            DataRow[] rowArray = table.Select("IsKey=1");
            if (rowArray.Length <= 0)
            {
                throw new ArgumentException("该表未设置主键无法生成更新语句");
            }
            string str2 = rowArray[0]["FieldName"].ToString();
            builder.AppendLine(this._SyncHeader);
            foreach (DataRow row in table2.Rows)
            {
                builder.AppendLine("IF  NOT  EXISTS(SELECT * from " + tableName + " WHERE " + str2 + "=" + this.GetFieldValue(table2.Columns[str2].DataType, row[str2]) + " )");
                builder.AppendLine("THEN");
                string str3 = "INSERT INTO " + tableName + " (";
                string str4 = "VALUES (";
                foreach (DataColumn column in table2.Columns)
                {
                    string fieldValue = this.GetFieldValue(column.DataType, row[column.ColumnName]);
                    str3 = str3 + column.ColumnName + ",";
                    str4 = str4 + fieldValue + ",";
                }
                builder.AppendLine((str3.TrimEnd(new char[] { ',' }) + ")") + (str4.TrimEnd(new char[] { ',' }) + ")") + ";");
                builder.AppendLine("END IF ;");
                builder.AppendLine("");
            }
            builder.AppendLine(this._SyncEnd);
            return builder.ToString();
        }

        public string CreateSyncQuery(string schemaName, string tableName, string Condition, string UpdateFields, bool isTransaction, bool isAddHeader = true)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("参数tableName不能为空");
            }
            DataTable table = this.SchemaTable(schemaName, tableName);
            string commandText = "select * from " + tableName;
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + "  where " + Condition;
            }
            DataTable table2 = base.ExecuteDataTable(commandText, new MySqlParameter[0]);
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
            builder.AppendLine(this._SyncHeader);
            foreach (DataRow row in table2.Rows)
            {
                string str8;
                builder.AppendLine("IF  NOT  EXISTS(SELECT * from " + tableName + " WHERE " + str2 + "=" + this.GetFieldValue(table2.Columns[str2].DataType, row[str2]) + " )");
                builder.AppendLine("THEN");
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
                builder.AppendLine("ELSE ");
                builder.AppendLine("  ");
                str8 = str5.TrimEnd(new char[] { ',' });
                str5 = str8 + " WHERE " + str2 + "=" + this.GetFieldValue(table2.Columns[str2].DataType, row[str2]) + ";";
                builder.AppendLine(str5);
                builder.AppendLine("END IF;");
                builder.AppendLine("");
            }
            builder.AppendLine(this._SyncEnd);
            return builder.ToString();
        }

        public string CreateSyncUpdateQuery(string schemaName, string tableName, string Condition, string UpdateFields, bool isTransaction, bool isAddHeader = true)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("参数tableName不能为空");
            }
            if (string.IsNullOrEmpty(schemaName))
            {
                throw new ArgumentNullException("参数schemaName不能为空");
            }
            DataTable table = this.SchemaTable(schemaName, tableName);
            string commandText = "select * from " + tableName;
            if (!string.IsNullOrEmpty(Condition))
            {
                commandText = commandText + "  where " + Condition;
            }
            DataTable table2 = base.ExecuteDataTable(commandText, new MySqlParameter[0]);
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
                str3 = str6 + " WHERE " + str2 + "=" + this.GetFieldValue(table2.Columns[str2].DataType, row[str2]) + ";";
                builder.AppendLine(str3);
                builder.AppendLine("");
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

        public DataTable SchemaTable(string schemaName, string tableName)
        {
            MySqlParameter[] prams = new MySqlParameter[] { new MySqlParameter("?tableName", tableName), new MySqlParameter("?SchemaName", schemaName) };
            return base.ExecuteDataTable("SELECT  \r\nTABLE_NAME as TableName,\r\nCOLUMN_NAME AS FieldName ,\r\nDATA_TYPE   AS DataType,\r\nCOLUMN_COMMENT as Description,\r\nCASE WHEN IS_NULLABLE ='YES' \r\n THEN 1 ELSE 0 END   as IsNullable,\r\nCASE WHEN COLUMN_KEY ='PRI' \r\n THEN 1 ELSE 0 END   as IsKey,\r\n\r\n \r\n CASE WHEN EXTRA ='auto_increment'\r\n  \r\n THEN 1 ELSE 0 END AS Identity,\r\n\r\nCASE WHEN CHARACTER_MAXIMUM_LENGTH is not NULL\r\n  \r\n THEN CHARACTER_MAXIMUM_LENGTH\r\n \r\n ELSE 0 END AS Length\r\n\r\n  FROM INFORMATION_SCHEMA.COLUMNS   where table_name=?tableName  and TABLE_SCHEMA=?SchemaName;", CommandType.Text, prams);
        }

        public WTF.Framework.UpdateFieldType UpdateFieldType { get; set; }
    }
}

