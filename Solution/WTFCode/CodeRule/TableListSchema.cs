using WTFCode.CodeRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTF.CodeRule
{
	public class TableListSchema
	{
		private List<ColumnListSchema> _Columns = new List<ColumnListSchema>();

		private List<CommandSchema> _CommandSchemas = new List<CommandSchema>();

		public string TableName
		{
			get;
			set;
		}

		public string EntityName
		{
			get;
			set;
		}

		public string SimpleTableName
		{
			get
			{
				return (this.TableName.Split(new char[]
				{
					'_'
				}).Count<string>() > 1) ? this.TableName.Split(new char[]
				{
					'_'
				})[1] : this.TableName;
			}
		}

		public string Description
		{
			get;
			set;
		}

		public string DataEntity
		{
			get;
			set;
		}

		public ColumnListSchema PrimaryKey
		{
			get
			{
				return this.Columns.FirstOrDefault((ColumnListSchema s) => s.ColumnType == "PrimaryKey");
			}
		}

		public ColumnListSchema ForeignKey
		{
			get
			{
				return this.Columns.FirstOrDefault((ColumnListSchema s) => s.ColumnType == "ForeignKey");
			}
		}

		public List<ColumnListSchema> Columns
		{
			get
			{
				return this._Columns;
			}
			set
			{
				this._Columns = value;
			}
		}

		public List<CommandSchema> Commands
		{
			get
			{
				return this._CommandSchemas;
			}
			set
			{
				this._CommandSchemas = value;
			}
		}

		public string RuleName
		{
			get;
			set;
		}

		public string ToUiSearch()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ColumnListSchema current in from s in this.Columns
			where s.IsSearch
			select s)
			{
				stringBuilder.AppendLine(current.ToSearchString());
			}
			return stringBuilder.ToString();
		}

		public string ToUiCode()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.AppendLine("   <WTF:SelectField>  </WTF:SelectField>");
			foreach (ColumnListSchema current in from s in this.Columns
			where s.ColumnType == "Common" && s.IsShow
			select s)
			{
				stringBuilder2.AppendLine(current.ToUiString());
			}
			stringBuilder.Append(string.Format(Resources.GetMyGridView, this.PrimaryKey.FieldName, stringBuilder2.ToString()));
			return stringBuilder.ToString();
		}

		public string ToCode(bool isdefault)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.ForeignKey != null)
			{
				stringBuilder.AppendLine(this.ForeignKey.ToRequestString());
				if (isdefault)
				{
					stringBuilder.AppendLine(string.Format(Resources.GetCondition, this.ForeignKey.FieldName, (this.ForeignKey.DataType == "uniqueidentifier") ? ("Guid'\"+" + this.ForeignKey.FieldName + "+\"'\"") : ((this.ForeignKey.DataType.IndexOf("char") >= 0) ? ("'\"+" + this.ForeignKey.FieldName + "+\"'\"") : ("\"+" + this.ForeignKey.FieldName))));
				}
				else
				{
					stringBuilder.AppendLine(string.Format(Resources.GetConditionT, this.TableName, this.ForeignKey.FieldName));
				}
			}
			stringBuilder.AppendLine(Resources.GetSortExpression);
			stringBuilder.AppendLine(string.Format("{0} obj{0}= new {0}();", this.RuleName));
			stringBuilder.AppendLine(string.Format(isdefault ? Resources.GetRenderPage : Resources.GetRenderPageT, this.TableName, this.RuleName));
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (CommandSchema current in from s in this.Commands
			where s.IsTop || s.IsBotom
			orderby s.SortIndex
			select s)
			{
				stringBuilder2.AppendLine(current.ToItemCode(this.SimpleTableName, this.RuleName, this.PrimaryKey.FieldName, (this.ForeignKey != null) ? this.ForeignKey.FieldName : ""));
			}
			stringBuilder.AppendLine(string.Format(Resources.GetItemCommand, stringBuilder2.ToString()));
			stringBuilder2.Clear();
			foreach (CommandSchema current in from s in this.Commands
			where s.IsList
			orderby s.SortIndex
			select s)
			{
				stringBuilder2.AppendLine(current.ToRowItemCode(this.SimpleTableName, this.RuleName, this.PrimaryKey.FieldName, (this.ForeignKey != null) ? this.ForeignKey.FieldName : ""));
			}
			stringBuilder.AppendLine(string.Format(Resources.GetRowCommand, stringBuilder2.ToString()));
			return stringBuilder.ToString();
		}

		public string ToCodeSql()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(Resources.GetSortExpression);
			stringBuilder.AppendLine(string.Format("{0} obj{0}= new {0}();", this.RuleName));
			stringBuilder.AppendLine(string.Format(Resources.GetRenderPageSql, this.RuleName));
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (CommandSchema current in from s in this.Commands
			where s.IsTop || s.IsBotom
			orderby s.SortIndex
			select s)
			{
				stringBuilder2.AppendLine(current.ToItemCodeSql(this.RuleName, this.PrimaryKey.PropertyName, (this.ForeignKey != null) ? this.ForeignKey.PropertyName : ""));
			}
			stringBuilder.AppendLine(string.Format(Resources.GetItemCommand, stringBuilder2.ToString()));
			stringBuilder2.Clear();
			foreach (CommandSchema current in from s in this.Commands
			where s.IsList
			orderby s.SortIndex
			select s)
			{
				stringBuilder2.AppendLine(current.ToRowItemCodeSql(this.RuleName, this.PrimaryKey.PropertyName, (this.ForeignKey != null) ? this.ForeignKey.PropertyName : ""));
			}
			stringBuilder.AppendLine(string.Format(Resources.GetRowCommand, stringBuilder2.ToString()));
			return stringBuilder.ToString();
		}
	}
}
