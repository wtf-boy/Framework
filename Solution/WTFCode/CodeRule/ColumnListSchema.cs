using WTFCode.CodeRule;
using System;

namespace WTF.CodeRule
{
	public class ColumnListSchema
	{
		public string PropertyName
		{
			get
			{
				return this.FieldName.PropertyName();
			}
		}

		public string MemberName
		{
			get
			{
				return this.FieldName.MemberName();
			}
		}

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

		public bool IsSort
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

		public string FieldTitle
		{
			get;
			set;
		}

		public bool IsSearch
		{
			get;
			set;
		}

		public bool IsIdentity
		{
			get;
			set;
		}

		public string ControlType
		{
			get;
			set;
		}

		public bool IsShow
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}

		public string FormatString
		{
			get;
			set;
		}

		public string ToUiFormantString
		{
			get
			{
				string controlType = this.ControlType;
				string result;
				if (controlType != null)
				{
					if (controlType == "OperateField")
					{
						result = Resources.GetOperateField;
						return result;
					}
					if (controlType == "TemplateField")
					{
						result = Resources.GetTemplateField;
						return result;
					}
					if (controlType == "BoundField")
					{
						result = Resources.GetBoundField;
						return result;
					}
				}
				result = Resources.GetBoundField;
				return result;
			}
		}

		public string GetDataType()
		{
			string dataType = this.DataType;
			string result;
			switch (dataType)
			{
			case "int":
				result = "int";
				return result;
			case "bigint":
				result = "long";
				return result;
			case "uniqueidentifier":
				result = "Guid";
				return result;
			case "varchar":
				result = "string";
				return result;
			case "nvarchar":
				result = "string";
				return result;
			case "text":
				result = "string";
				return result;
			case "bit":
				result = "bool";
				return result;
			case "datetime":
				result = "DateTime";
				return result;
			case "date":
				result = "DateTime";
				return result;
			}
			result = "string";
			return result;
		}

		public string GetRequestType()
		{
			string dataType = this.DataType;
			string result;
			switch (dataType)
			{
			case "int":
				result = "GetInt";
				return result;
			case "bigint":
				result = "GetLong";
				return result;
			case "uniqueidentifier":
				result = "GetGuid";
				return result;
			case "varchar":
				result = "GetString";
				return result;
			case "nvarchar":
				result = "GetString";
				return result;
			case "text":
				result = "GetString";
				return result;
			}
			result = "GetString";
			return result;
		}

		public string ToRequestString()
		{
			return string.Format("public {1} {0}\r\n    {{\r\n        get\r\n        {{\r\n            return {2}(\"{0}\");\r\n\r\n        }}\r\n\r\n    }}", this.FieldName, this.GetDataType(), this.GetRequestType());
		}

		public string ToUiString()
		{
			return string.Format(this.ToUiFormantString, new object[]
			{
				this.FieldName,
				this.FieldTitle,
				(this.Width > 0) ? string.Format(Resources.GetWidth, this.Width) : "",
				(this.ControlType == "BoundField" && !string.IsNullOrEmpty(this.FormatString)) ? ("DataFormatString=\"{0:" + this.FormatString + "}\"") : "",
				this.IsSort ? ("SortExpression=\"" + this.FieldName + "\"") : ""
			});
		}

		public string ToSearchString()
		{
			return string.Format(this.ToSearchFormant(), this.FieldName, this.FieldTitle);
		}

		private string ToSearchFormant()
		{
			string dataType = this.DataType;
			string result;
			switch (dataType)
			{
			case "int":
			case "bit":
				result = Resources.GetQueryDrop;
				return result;
			case "varchar":
			case "nvarchar":
			case "text":
				result = Resources.GetQueryText;
				return result;
			case "datetime":
			case "date":
				result = Resources.GetQueryDate;
				return result;
			}
			result = Resources.GetQueryText;
			return result;
		}
	}
}
