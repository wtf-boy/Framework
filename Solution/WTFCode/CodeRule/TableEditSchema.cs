using WTFCode.CodeRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTF.CodeRule
{
	public class TableEditSchema
	{
		private string _BackUrl = "";

		private List<ColumnEditSchema> _Columns = new List<ColumnEditSchema>();

		public string RuleName
		{
			get;
			set;
		}

		public string BackUrl
		{
			get
			{
				string result;
				if (string.IsNullOrEmpty(this._BackUrl))
				{
					result = this.EntityName + "List.aspx";
				}
				else
				{
					result = this._BackUrl;
				}
				return result;
			}
			set
			{
				this._BackUrl = value;
			}
		}

		public string EntityName
		{
			get;
			set;
		}

		public string Description
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

		public string TableName
		{
			get;
			set;
		}

		public ColumnEditSchema PrimaryKey
		{
			get
			{
				return this.Columns.FirstOrDefault((ColumnEditSchema s) => s.ColumnType == "PrimaryKey");
			}
		}

		public ColumnEditSchema ForeignKey
		{
			get
			{
				return this.Columns.FirstOrDefault((ColumnEditSchema s) => s.ColumnType == "ForeignKey");
			}
		}

		public List<ColumnEditSchema> Columns
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

		public string ToUiString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Resources.EditTableHeader);
			foreach (ColumnEditSchema current in from s in this.Columns
			where s.ColumnType == "Common"
			select s)
			{
				if (current.IsShow)
				{
					stringBuilder.Append(current.ToUiString());
				}
			}
			stringBuilder.Append("</table>");
			return stringBuilder.ToString();
		}

		public string ToUiStringSql()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(Resources.EditTableHeader, this.Description);
			foreach (ColumnEditSchema current in from s in this.Columns
			where s.ColumnType == "Common" && !s.FieldName.ToLower().Contains("guid")
			select s)
			{
				if (current.IsShow)
				{
					stringBuilder.Append(current.ToUiStringSql());
				}
			}
			stringBuilder.Append("</table>");
			return stringBuilder.ToString();
		}

		public string CreateSummary(string summary)
		{
			return string.Format(Resources.GetSummary, summary);
		}

		public string ToCodeString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.PrimaryKey != null)
			{
				stringBuilder.AppendLine(this.CreateSummary("获取" + this.PrimaryKey.FieldTitle));
				stringBuilder.AppendLine(this.PrimaryKey.ToRequestString());
			}
			if (this.ForeignKey != null)
			{
				stringBuilder.AppendLine(this.CreateSummary("获取" + this.ForeignKey.FieldTitle));
				stringBuilder.AppendLine(this.ForeignKey.ToRequestString());
			}
			stringBuilder.AppendLine(this.CreateSummary("变量"));
			stringBuilder.AppendLine(string.Format("public {0} obj{0}= new {0}();", this.TableName));
			stringBuilder.AppendLine(string.Format("{0} obj{0}= new {0}();", this.RuleName));
			stringBuilder.AppendLine(this.CreateSummary("页面加载"));
			stringBuilder.AppendLine(this.ToRenderPage());
			stringBuilder.AppendLine(this.ToSaveInfoString());
			stringBuilder.AppendLine(this.CreateSummary("工具栏操作"));
			stringBuilder.AppendLine(this.ToToolCommand());
			return stringBuilder.ToString();
		}

		public string ToCodeStringSql()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.PrimaryKey != null)
			{
				stringBuilder.AppendLine(this.CreateSummary("获取" + this.PrimaryKey.FieldTitle));
				stringBuilder.AppendLine(this.PrimaryKey.ToRequestString());
			}
			if (this.ForeignKey != null)
			{
				stringBuilder.AppendLine(this.CreateSummary("获取" + this.ForeignKey.FieldTitle));
				stringBuilder.AppendLine(this.ForeignKey.ToRequestString());
			}
			stringBuilder.AppendLine(this.CreateSummary(string.Format("实体变量{0}", this.EntityName)));
			stringBuilder.AppendLine(string.Format("public {0} obj{0}= new {0}();", this.EntityName));
			stringBuilder.AppendLine(this.CreateSummary(string.Format("业务逻辑层{0}", this.RuleName)));
			stringBuilder.AppendLine(string.Format("{0} obj{0}= new {0}();", this.RuleName));
			stringBuilder.AppendLine(this.CreateSummary("页面加载"));
			stringBuilder.AppendLine(this.ToRenderPageSql());
			stringBuilder.AppendLine(this.ToSaveInfoStringSql());
			stringBuilder.AppendLine(this.CreateSummary("工具栏操作"));
			stringBuilder.AppendLine(this.ToToolCommand());
			return stringBuilder.ToString();
		}

		public string ToRenderPageSql()
		{
			return string.Format(" public override void RenderPage()\r\n    {{\r\n\r\n        if ({0}.IsNoNull())\r\n        {{\r\n            obj{1}= obj{2}.GetRecord({0});\r\n            if (CheckEditObjectIsNull(obj{1})) return;\r\n {3}\r\n            Page.DataBind();\r\n        }}\r\n        else\r\n        {{\r\n        }}\r\n\r\n    }}", new object[]
			{
				this.PrimaryKey.PropertyName,
				this.EntityName,
				this.RuleName,
				this.ToRenderControlPageSql()
			});
		}

		public string ToRenderPage()
		{
			return string.Format(" public override void RenderPage()\r\n    {{\r\n\r\n        if ({0}.IsNoNull())\r\n        {{\r\n            obj{1}= obj{2}.{1}.FirstOrDefault(s => s.{0} == {0});\r\n            if (CheckEditObjectIsNull(obj{1})) return;\r\n {3}\r\n            Page.DataBind();\r\n        }}\r\n        else\r\n        {{\r\n        }}\r\n\r\n    }}", new object[]
			{
				this.PrimaryKey.FieldName,
				this.TableName,
				this.RuleName,
				this.ToRenderControlPage()
			});
		}

		public string ToRenderControlPage()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ColumnEditSchema current in from s in this.Columns
			where s.ColumnType == "Common"
			select s)
			{
				if (current.IsShow)
				{
					string text = current.ToRenderControlPage();
					if (!string.IsNullOrEmpty(text))
					{
						stringBuilder.AppendLine("    ///" + current.FieldTitle);
						stringBuilder.AppendLine("    " + text);
					}
				}
			}
			return stringBuilder.ToString();
		}

		public string ToRenderControlPageSql()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ColumnEditSchema current in from s in this.Columns
			where s.ColumnType == "Common"
			select s)
			{
				if (current.IsShow)
				{
					string text = current.ToRenderControlPageSql();
					if (!string.IsNullOrEmpty(text))
					{
						stringBuilder.AppendLine("    ///" + current.FieldTitle);
						stringBuilder.AppendLine("    " + text);
					}
				}
			}
			return stringBuilder.ToString();
		}

		public string ToSaveInfoString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("  /// <summary>");
			stringBuilder.AppendLine("  /// 保存界面上的值");
			stringBuilder.AppendLine(" /// </summary>");
			stringBuilder.AppendLine(string.Format("  /// <param name=\"{0}\"></param>", "obj" + this.TableName));
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				"  public void SaveValue( ",
				this.TableName,
				" obj",
				this.TableName,
				") "
			}));
			stringBuilder.AppendLine(" {");
			if (this.Columns.Any((ColumnEditSchema s) => s.IsAutoValue))
			{
				stringBuilder.AppendLine("    ///自动赋值");
				stringBuilder.AppendLine("    AutoObjectSetValue(obj" + this.TableName + ");");
			}
			foreach (ColumnEditSchema current in from s in this.Columns
			where s.ColumnType == "Common"
			select s)
			{
				if (!current.IsAutoValue)
				{
					stringBuilder.AppendLine("    ///" + current.FieldTitle);
					stringBuilder.AppendLine("    " + current.ToSaveInfo());
				}
			}
			stringBuilder.AppendLine(" }");
			stringBuilder.AppendLine(this.CreateSummary("保存信息"));
			stringBuilder.AppendLine("  public void SaveInfo() ");
			stringBuilder.AppendLine(" {");
			stringBuilder.AppendLine("  if (" + this.PrimaryKey.FieldName + ".IsNull())");
			stringBuilder.AppendLine(" {");
			if (this.PrimaryKey != null)
			{
				if (this.PrimaryKey.DataType == "uniqueidentifier")
				{
					stringBuilder.AppendLine("    " + string.Format("{0}.{1}={2};", "obj" + this.TableName, this.PrimaryKey.FieldName, " Guid.NewGuid()"));
				}
			}
			if (this.ForeignKey != null)
			{
				stringBuilder.AppendLine("    " + string.Format("{0}.{1}={2};", "obj" + this.TableName, this.ForeignKey.FieldName, this.ForeignKey.FieldName));
			}
			stringBuilder.AppendLine("     SaveValue(obj" + this.TableName + ");");
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				"    obj",
				this.RuleName,
				".Insert",
				this.SimpleTableName,
				"(obj",
				this.TableName,
				");"
			}));
			stringBuilder.AppendLine("    " + string.Format("MessageDialog(\"新增成功\", \"{0}{1});", this.BackUrl, (this.ForeignKey == null) ? "\"" : ("?" + this.ForeignKey.FieldName + "=\"+" + this.ForeignKey.FieldName)));
			stringBuilder.AppendLine(" }");
			stringBuilder.AppendLine(" else");
			stringBuilder.AppendLine(" {");
			stringBuilder.AppendLine("    " + string.Format("obj{0} = obj{2}.{0}.FirstOrDefault(p => p.{1} == {1});", this.TableName, this.PrimaryKey.FieldName, this.RuleName));
			stringBuilder.AppendLine("    " + string.Format(" if (CheckEditObjectIsNull(obj{0})) return;", this.TableName));
			stringBuilder.AppendLine("    SaveValue(obj" + this.TableName + ");");
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				"    obj",
				this.RuleName,
				".Update",
				this.SimpleTableName,
				"(obj",
				this.TableName,
				");"
			}));
			stringBuilder.AppendLine("    " + string.Format("MessageDialog(\"修改成功\", \"{0}{1});", this.BackUrl, (this.ForeignKey == null) ? "\"" : ("?" + this.ForeignKey.FieldName + "=\"+" + this.ForeignKey.FieldName)));
			stringBuilder.AppendLine(" }");
			stringBuilder.AppendLine(" }");
			return stringBuilder.ToString();
		}

		public string ToSaveInfoStringSql()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("  /// <summary>");
			stringBuilder.AppendLine("  /// 保存界面上的值");
			stringBuilder.AppendLine(" /// </summary>");
			stringBuilder.AppendLine(string.Format("  /// <param name=\"{0}\"></param>", "obj" + this.EntityName));
			stringBuilder.AppendLine(string.Format("  public void SaveValue({0}  {1}) ", this.EntityName, "obj" + this.EntityName));
			stringBuilder.AppendLine(" {");
			foreach (ColumnEditSchema current in from s in this.Columns
			where s.IsShow && s.ColumnType == "Common" && !s.FieldName.ToLower().Contains("guid")
			select s)
			{
				stringBuilder.AppendLine("    //" + current.FieldTitle);
				stringBuilder.AppendLine("    " + current.ToSaveInfoSql());
			}
			stringBuilder.AppendLine("  }");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine("  /// <summary>");
			stringBuilder.AppendLine("  /// 保存信息");
			stringBuilder.AppendLine(" /// </summary>");
			stringBuilder.AppendLine("  public void SaveInfo() ");
			stringBuilder.AppendLine(" {");
			stringBuilder.AppendLine("  if (" + this.PrimaryKey.PropertyName + ".IsNull())");
			stringBuilder.AppendLine(" {");
			stringBuilder.AppendLine(string.Format("   SaveValue({0}); ", "obj" + this.EntityName));
			if (this.PrimaryKey != null)
			{
				if (this.PrimaryKey.DataType.IndexOf("char") >= 0 && this.PrimaryKey.Length == 36)
				{
					stringBuilder.AppendLine("    " + string.Format("{0}.{1}={2};", "obj" + this.EntityName, this.PrimaryKey.PropertyName, " Guid.NewGuid().ToString()"));
				}
			}
			if (this.ForeignKey != null)
			{
				stringBuilder.AppendLine("    " + string.Format("{0}.{1}={2};", "obj" + this.EntityName, this.ForeignKey.PropertyName, this.ForeignKey.PropertyName));
			}
			foreach (ColumnEditSchema current in from s in this.Columns
			where !s.IsShow && s.ColumnType == "Common" && !s.FieldName.ToLower().Contains("guid")
			select s)
			{
				stringBuilder.AppendLine("    //" + current.FieldTitle);
				stringBuilder.AppendLine("    " + current.ToSaveInfoSql());
			}
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				"    obj",
				this.RuleName,
				".Add(obj",
				this.EntityName,
				");"
			}));
			stringBuilder.AppendLine("    " + string.Format("MessageDialog(\"新增成功\", \"{0}{1});", this.BackUrl, (this.ForeignKey == null) ? "\"" : ("?" + this.ForeignKey.PropertyName + "=\"+" + this.ForeignKey.PropertyName)));
			stringBuilder.AppendLine(" }");
			stringBuilder.AppendLine(" else");
			stringBuilder.AppendLine(" {");
			stringBuilder.AppendLine("    " + string.Format("obj{0} = obj{2}.GetRecord({1});", this.EntityName, this.PrimaryKey.PropertyName, this.RuleName));
			stringBuilder.AppendLine("    " + string.Format(" if (CheckEditObjectIsNull(obj{0})) return;", this.EntityName));
			stringBuilder.AppendLine("    ");
			stringBuilder.AppendLine(string.Format("     SaveValue({0}); ", "obj" + this.EntityName));
			foreach (ColumnEditSchema current in from s in this.Columns
			where !s.IsShow && s.ColumnType == "Common" && !s.FieldName.ToLower().Contains("guid")
			select s)
			{
				stringBuilder.AppendLine("    //" + current.FieldTitle);
				stringBuilder.AppendLine("    " + current.ToSaveInfoSql());
			}
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				"    obj",
				this.RuleName,
				".Update(obj",
				this.EntityName,
				");"
			}));
			stringBuilder.AppendLine("    " + string.Format("MessageDialog(\"修改成功\", \"{0}{1});", this.BackUrl, (this.ForeignKey == null) ? "\"" : ("?" + this.ForeignKey.PropertyName + "=\"+" + this.ForeignKey.PropertyName)));
			stringBuilder.AppendLine(" }");
			stringBuilder.AppendLine(" }");
			return stringBuilder.ToString();
		}

		public string ToToolCommand()
		{
			return string.Format("    protected override void CurrentTool_ItemCommand(object sender, WTF.Controls.MyCommandEventArgs e)\r\n    {{\r\n\r\n        switch (e.CommandName)\r\n        {{\r\n            case \"Save\":\r\n                SaveInfo();\r\n                break;\r\n            case \"Back\":\r\n                Redirect(\"{0}{1});\r\n                break;\r\n        }}\r\n\r\n    }}", this.BackUrl, (this.ForeignKey == null) ? "\"" : ("?" + this.ForeignKey.FieldName + "=\"+" + this.ForeignKey.FieldName));
		}
	}
}
