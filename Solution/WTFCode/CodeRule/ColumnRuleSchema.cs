using WTFCode.CodeRule;
using System;

namespace WTF.CodeRule
{
	public class ColumnRuleSchema
	{
		public string ColumnType
		{
			get;
			set;
		}

		public string DataType
		{
			get;
			set;
		}

		public bool IsIdentity
		{
			get;
			set;
		}

		public bool IsUnsigned
		{
			get;
			set;
		}

		public bool IsPrimaryKey
		{
			get
			{
				return this.ColumnType == "PrimaryKey";
			}
		}

		public bool IsXmlField
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public int Length
		{
			get;
			set;
		}

		public bool IsEmpty
		{
			get;
			set;
		}

		public string TableName
		{
			get;
			set;
		}

		public string FieldName
		{
			get;
			set;
		}

		public string PropertyName
		{
			get
			{
				return this.FieldName.PropertyName();
			}
		}

		public string PropertyCamelName
		{
			get
			{
				return this.FieldName.CamelCase();
			}
		}

		public string MemberName
		{
			get
			{
				return this.FieldName.MemberName();
			}
		}

		public string FieldTitle
		{
			get;
			set;
		}

		public bool IsCheck
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string OldErrorMessage
		{
			get;
			set;
		}

		public string OldFieldTitle
		{
			get;
			set;
		}

		public string GetCSharpSystemType
		{
			get
			{
				string dataType = this.DataType;
				string dataType2 = this.DataType;
				string result;
				switch (dataType2)
				{
				case "bigint":
					result = (this.IsUnsigned ? "UInt64" : "Int64");
					return result;
				case "binary":
					result = "Object";
					return result;
				case "bit":
					result = "Boolean";
					return result;
				case "char":
					result = "String";
					return result;
				case "datetime":
					result = "DateTime";
					return result;
				case "decimal":
					result = "Decimal";
					return result;
				case "float":
					result = "Single";
					return result;
				case "double":
					result = "Double";
					return result;
				case "image":
					result = "Object";
					return result;
				case "int":
					result = (this.IsUnsigned ? "UInt32" : "Int32");
					return result;
				case "money":
					result = "Decimal";
					return result;
				case "nchar":
					result = "String";
					return result;
				case "ntext":
					result = "String";
					return result;
				case "longtext":
					result = "String";
					return result;
				case "nvarchar":
					result = "String";
					return result;
				case "real":
					result = "Single";
					return result;
				case "smalldatetime":
					result = "DateTime";
					return result;
				case "smallint":
					result = (this.IsUnsigned ? "UInt16" : "Int16");
					return result;
				case "smallmoney":
					result = "Decimal";
					return result;
				case "text":
					result = "String";
					return result;
				case "mediumtext":
					result = "String";
					return result;
				case "timestamp":
					result = "Object";
					return result;
				case "tinyint":
					result = "Byte";
					return result;
				case "udt":
					result = "Object";
					return result;
				case "uniqueidentifier":
					result = "Object";
					return result;
				case "varbinary":
					result = "Object";
					return result;
				case "varchar":
					result = "String";
					return result;
				case "variant":
					result = "Object";
					return result;
				case "xml":
					result = "Object";
					return result;
				case "varchar2":
					result = "String.Empty";
					return result;
				case "DATE":
					result = "DateTime";
					return result;
				case "number":
					result = "int";
					return result;
				case "INTEGER":
					result = "int";
					return result;
				case "CLOB":
					result = "String";
					return result;
				}
				result = dataType;
				return result;
			}
		}

		public string GetJavaSystemType
		{
			get
			{
				string dataType = this.DataType;
				string dataType2 = this.DataType;
				string result;
				switch (dataType2)
				{
				case "bigint":
					result = "long";
					return result;
				case "binary":
					result = "Object";
					return result;
				case "bit":
					result = "boolean";
					return result;
				case "char":
					result = "String";
					return result;
				case "datetime":
					result = "Date";
					return result;
				case "decimal":
					result = "Decimal";
					return result;
				case "float":
					result = "float";
					return result;
				case "double":
					result = "double";
					return result;
				case "image":
					result = "Object";
					return result;
				case "int":
					result = "int";
					return result;
				case "money":
					result = "Decimal";
					return result;
				case "nchar":
					result = "String";
					return result;
				case "ntext":
					result = "String";
					return result;
				case "longtext":
					result = "String";
					return result;
				case "nvarchar":
					result = "String";
					return result;
				case "real":
					result = "Single";
					return result;
				case "smalldatetime":
					result = "Date";
					return result;
				case "smallint":
					result = "int";
					return result;
				case "smallmoney":
					result = "Decimal";
					return result;
				case "text":
					result = "String";
					return result;
				case "mediumtext":
					result = "String";
					return result;
				case "timestamp":
					result = "Object";
					return result;
				case "tinyint":
					result = "Byte";
					return result;
				case "udt":
					result = "Object";
					return result;
				case "uniqueidentifier":
					result = "Object";
					return result;
				case "varbinary":
					result = "Object";
					return result;
				case "varchar":
					result = "String";
					return result;
				case "variant":
					result = "Object";
					return result;
				case "xml":
					result = "Object";
					return result;
				case "varchar2":
					result = "String.Empty";
					return result;
				case "DATE":
					result = "Date";
					return result;
				case "number":
					result = "int";
					return result;
				case "INTEGER":
					result = "int";
					return result;
				case "CLOB":
					result = "String";
					return result;
				}
				result = dataType;
				return result;
			}
		}

		public string GetSolrharpSystemType
		{
			get
			{
				string dataType = this.DataType;
				string dataType2 = this.DataType;
				string result;
				switch (dataType2)
				{
				case "bigint":
					result = "long";
					return result;
				case "binary":
					result = "int";
					return result;
				case "bit":
					result = "int";
					return result;
				case "char":
					result = "string";
					return result;
				case "datetime":
					result = "date";
					return result;
				case "decimal":
					result = "float";
					return result;
				case "float":
					result = "float";
					return result;
				case "double":
					result = "float";
					return result;
				case "image":
					result = "string";
					return result;
				case "int":
					result = "int";
					return result;
				case "money":
					result = "float";
					return result;
				case "nchar":
					result = "string";
					return result;
				case "ntext":
					result = "string";
					return result;
				case "longtext":
					result = "string";
					return result;
				case "nvarchar":
					result = "string";
					return result;
				case "real":
					result = "float";
					return result;
				case "smalldatetime":
					result = "date";
					return result;
				case "smallint":
					result = "int";
					return result;
				case "smallmoney":
					result = "float";
					return result;
				case "text":
					result = "string";
					return result;
				case "timestamp":
					result = "int";
					return result;
				case "tinyint":
					result = "int";
					return result;
				case "udt":
					result = "Object";
					return result;
				case "uniqueidentifier":
					result = "string";
					return result;
				case "varbinary":
					result = "string";
					return result;
				case "varchar":
					result = "string";
					return result;
				case "variant":
					result = "string";
					return result;
				case "xml":
					result = "string";
					return result;
				case "varchar2":
					result = "string";
					return result;
				case "DATE":
					result = "date";
					return result;
				case "number":
					result = "int";
					return result;
				case "INTEGER":
					result = "int";
					return result;
				case "CLOB":
					result = "string";
					return result;
				}
				result = "string";
				return result;
			}
		}

		public string DefaultValue
		{
			get
			{
				string dataType = this.DataType;
				string result;
				switch (dataType)
				{
				case "bigint":
					result = "0";
					return result;
				case "binary":
					result = "0";
					return result;
				case "bit":
					result = "false";
					return result;
				case "char":
					result = "String.Empty";
					return result;
				case "datetime":
					result = "System.DateTime.Now";
					return result;
				case "decimal":
					result = "0";
					return result;
				case "float":
					result = "0";
					return result;
				case "image":
					result = "object";
					return result;
				case "int":
					result = "0";
					return result;
				case "money":
					result = "0";
					return result;
				case "nchar":
					result = "String.Empty";
					return result;
				case "ntext":
					result = "String.Empty";
					return result;
				case "nvarchar":
					result = "String.Empty";
					return result;
				case "mediumtext":
					result = "String.Empty";
					return result;
				case "longtext":
					result = "String.Empty";
					return result;
				case "real":
					result = "0";
					return result;
				case "smalldatetime":
					result = "System.DateTime.Now";
					return result;
				case "smallint":
					result = "0";
					return result;
				case "smallmoney":
					result = "0";
					return result;
				case "text":
					result = "String.Empty";
					return result;
				case "timestamp":
					result = "object";
					return result;
				case "tinyint":
					result = "0";
					return result;
				case "udt":
					result = "object";
					return result;
				case "uniqueidentifier":
					result = "object";
					return result;
				case "varbinary":
					result = "object";
					return result;
				case "varchar":
					result = "String.Empty";
					return result;
				case "variant":
					result = "object";
					return result;
				case "xml":
					result = "object";
					return result;
				case "varchar2":
					result = "String.Empty";
					return result;
				case "DATE":
					result = "DateTime";
					return result;
				case "number":
					result = "int";
					return result;
				case "double":
					result = "0.0";
					return result;
				}
				result = null;
				return result;
			}
		}

		public string DefaultJavaValue
		{
			get
			{
				string dataType = this.DataType;
				string result;
				switch (dataType)
				{
				case "bigint":
					result = "0";
					return result;
				case "binary":
					result = "0";
					return result;
				case "bit":
					result = "false";
					return result;
				case "char":
					result = "\"\"";
					return result;
				case "datetime":
					result = "new Date()";
					return result;
				case "decimal":
					result = "0";
					return result;
				case "float":
					result = "0";
					return result;
				case "image":
					result = "object";
					return result;
				case "int":
					result = "0";
					return result;
				case "money":
					result = "0";
					return result;
				case "nchar":
					result = "\"\"";
					return result;
				case "ntext":
					result = "\"\"";
					return result;
				case "nvarchar":
					result = "\"\"";
					return result;
				case "mediumtext":
					result = "\"\"";
					return result;
				case "longtext":
					result = "\"\"";
					return result;
				case "real":
					result = "0";
					return result;
				case "smalldatetime":
					result = "new Date()";
					return result;
				case "smallint":
					result = "0";
					return result;
				case "smallmoney":
					result = "0";
					return result;
				case "text":
					result = "\"\"";
					return result;
				case "timestamp":
					result = "object";
					return result;
				case "tinyint":
					result = "0";
					return result;
				case "udt":
					result = "object";
					return result;
				case "uniqueidentifier":
					result = "object";
					return result;
				case "varbinary":
					result = "object";
					return result;
				case "varchar":
					result = "\"\"";
					return result;
				case "variant":
					result = "object";
					return result;
				case "xml":
					result = "object";
					return result;
				case "varchar2":
					result = "\"\"";
					return result;
				case "DATE":
					result = "new Date()";
					return result;
				case "number":
					result = "int";
					return result;
				case "double":
					result = "0.0";
					return result;
				}
				result = null;
				return result;
			}
		}

		public string GetESMappingSystemType
		{
			get
			{
				string dataType = this.DataType;
				string dataType2 = this.DataType;
				string result;
				switch (dataType2)
				{
				case "bigint":
					result = "long";
					return result;
				case "binary":
					result = "integer";
					return result;
				case "bit":
					result = "boolean";
					return result;
				case "char":
					result = "string";
					return result;
				case "datetime":
					result = "date";
					return result;
				case "decimal":
					result = "float";
					return result;
				case "float":
					result = "float";
					return result;
				case "double":
					result = "double";
					return result;
				case "image":
					result = "string";
					return result;
				case "int":
					result = "integer";
					return result;
				case "money":
					result = "float";
					return result;
				case "nchar":
					result = "string";
					return result;
				case "ntext":
					result = "string";
					return result;
				case "longtext":
					result = "string";
					return result;
				case "nvarchar":
					result = "string";
					return result;
				case "real":
					result = "float";
					return result;
				case "smalldatetime":
					result = "date";
					return result;
				case "smallint":
					result = "short";
					return result;
				case "smallmoney":
					result = "float";
					return result;
				case "text":
					result = "string";
					return result;
				case "timestamp":
					result = "timestamp";
					return result;
				case "tinyint":
					result = "integer";
					return result;
				case "udt":
					result = "object";
					return result;
				case "uniqueidentifier":
					result = "string";
					return result;
				case "varbinary":
					result = "string";
					return result;
				case "varchar":
					result = "string";
					return result;
				case "variant":
					result = "string";
					return result;
				case "xml":
					result = "string";
					return result;
				case "varchar2":
					result = "string";
					return result;
				case "DATE":
					result = "date";
					return result;
				case "number":
					result = "integer";
					return result;
				case "INTEGER":
					result = "integer";
					return result;
				case "CLOB":
					result = "string";
					return result;
				}
				result = "string";
				return result;
			}
		}

		public string ToEntityValue(string entityMemberName)
		{
			return string.Format(Resources.GetToEntityValue, new object[]
			{
				this.FieldName,
				entityMemberName,
				this.GetCSharpSystemType,
				this.PropertyName
			});
		}
	}
}
