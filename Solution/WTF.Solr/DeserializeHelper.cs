using WTF.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace WTF.Solr
{
	public abstract class DeserializeHelper<T> where T : class, new()
	{
		protected List<PropertyInfo> _PropertyList = new List<PropertyInfo>();

		private string _TableName = "";

		public string _Fields = "";

		private DataTable _DataTableSchema;

		public IDictionary<string, IDictionary<string, IList<string>>> Highlight
		{
			get;
			set;
		}

		public List<PropertyInfo> PropertyList
		{
			get
			{
				return this._PropertyList;
			}
		}

		public PrimaryKeyAttribute PrimaryKey
		{
			get;
			set;
		}

		public object ConvertValue(object value, Type objType)
		{
			return Convert.ChangeType(value, objType);
		}

		public DeserializeHelper(string tableName, string fields)
		{
			this.Highlight = null;
			this._TableName = tableName;
			if (fields == "*" || string.IsNullOrWhiteSpace(fields))
			{
				fields = "";
			}
			this._Fields = fields;
			string[] source = fields.ToLower().Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			Type typeFromHandle = typeof(T);
			PropertyInfo[] properties = typeFromHandle.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				PrimaryKeyAttribute primaryKeyAttribute = PrimaryKeyHelper.GetPrimaryKeyAttribute(propertyInfo);
				if (primaryKeyAttribute != null)
				{
					this.PrimaryKey = primaryKeyAttribute;
				}
				if (string.IsNullOrWhiteSpace(fields) || source.Contains(propertyInfo.Name.ToLower()))
				{
					this._PropertyList.Add(propertyInfo);
				}
			}
			if (!string.IsNullOrWhiteSpace(this._TableName))
			{
				this._DataTableSchema = new DataTable(this._TableName);
			}
			else
			{
				this._DataTableSchema = new DataTable("QueryData");
			}
			foreach (PropertyInfo current in this.PropertyList)
			{
				DataColumn column = new DataColumn(current.Name, current.PropertyType);
				this._DataTableSchema.Columns.Add(column);
			}
			if (!string.IsNullOrWhiteSpace(this._Fields))
			{
				if (source.Contains("score"))
				{
					DataColumn column2 = new DataColumn("score", typeof(double));
					this._DataTableSchema.Columns.Add(column2);
				}
				if (source.Contains("_version_"))
				{
					DataColumn column3 = new DataColumn("_version_", typeof(string));
					this._DataTableSchema.Columns.Add(column3);
				}
			}
		}

		public DataTable CreateDataTable()
		{
			return this._DataTableSchema.Clone();
		}

		public DataRow CreateDataTableNewRow()
		{
			return this._DataTableSchema.NewRow();
		}
	}
}
