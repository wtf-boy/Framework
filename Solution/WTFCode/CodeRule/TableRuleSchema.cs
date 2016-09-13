using WTFCode.CodeRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTF.CodeRule
{
	public class TableRuleSchema
	{
		private List<ColumnRuleSchema> _Columns = new List<ColumnRuleSchema>();

		public string TableName
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

		public string EntityMemberName
		{
			get
			{
				return char.ToLower(this.EntityName[0]) + this.EntityName.Substring(1);
			}
		}

		public bool IsCreate
		{
			get;
			set;
		}

		public bool IsInsert
		{
			get;
			set;
		}

		public bool IsUpdate
		{
			get;
			set;
		}

		public bool IsDelete
		{
			get;
			set;
		}

		public bool IsMongoDB
		{
			get;
			set;
		}

		public bool IsCheckFieldLength
		{
			get;
			set;
		}

		public string LogModuleType
		{
			get;
			set;
		}

		public string UIProjectName
		{
			get;
			set;
		}

		public string UIProjectPath
		{
			get;
			set;
		}

		public string ConnectionKeyOrConnectionString
		{
			get;
			set;
		}

		public string ConnectionKey
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string EntityName
		{
			get;
			set;
		}

		public ColumnRuleSchema PrimaryKey
		{
			get
			{
				return this.Columns.FirstOrDefault((ColumnRuleSchema s) => s.ColumnType == "PrimaryKey");
			}
		}

		public string ModuleID
		{
			get;
			set;
		}

		public string EditUrl
		{
			get;
			set;
		}

		public string ListUrl
		{
			get;
			set;
		}

		public string PrimaryKeyType
		{
			get
			{
				return (this.PrimaryKey == null) ? "" : ((this.PrimaryKey.DataType == "int") ? "int" : ((this.PrimaryKey.DataType == "uniqueidentifier") ? "Guid" : ""));
			}
		}

		public List<ColumnRuleSchema> Columns
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

		public TableRuleSchema()
		{
			this.IsCheckFieldLength = true;
		}

		public string ToInsertReturn()
		{
			string result;
			if (!this.PrimaryKey.IsIdentity)
			{
				result = string.Format(Resources.GetInsertNoIdentity, this.EntityMemberName, this.PrimaryKey.PropertyName);
			}
			else
			{
				ColumnRuleSchema columnRuleSchema = (from s in this.Columns
				where s.FieldName.ToLower().Contains("guid")
				select s).FirstOrDefault<ColumnRuleSchema>();
				string text = (columnRuleSchema != null) ? string.Concat(new string[]
				{
					"SELECT ",
					this.PrimaryKey.FieldName,
					" FROM \" + _TableName + \" WHERE ",
					columnRuleSchema.FieldName,
					"='\"+",
					this.EntityMemberName,
					".",
					columnRuleSchema.PropertyName,
					"+\"' LIMIT 1;"
				}) : "SELECT LAST_INSERT_ID();";
				result = string.Format(Resources.GetInsertIdentity, new object[]
				{
					this.EntityMemberName,
					this.PrimaryKey.PropertyName,
					text,
					this.PrimaryKey.GetCSharpSystemType
				});
			}
			return result;
		}

		public string ToInsertFields()
		{
			List<ColumnRuleSchema> arg_46_0;
			if (this.PrimaryKey.IsIdentity)
			{
				arg_46_0 = (from s in this.Columns
				where s.ColumnType != "PrimaryKey"
				select s).ToList<ColumnRuleSchema>();
			}
			else
			{
				arg_46_0 = this.Columns;
			}
			List<ColumnRuleSchema> list = arg_46_0;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int count = list.Count;
			foreach (ColumnRuleSchema current in list)
			{
				num++;
				string text;
				if (num == count)
				{
					text = "";
				}
				else
				{
					text = ",";
				}
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"            sb.Append(\"`",
					current.FieldName,
					"`",
					text,
					"\");"
				}));
			}
			return stringBuilder.ToString();
		}

		public string ToInsertFieldValues()
		{
			List<ColumnRuleSchema> arg_46_0;
			if (this.PrimaryKey.IsIdentity)
			{
				arg_46_0 = (from s in this.Columns
				where s.ColumnType != "PrimaryKey"
				select s).ToList<ColumnRuleSchema>();
			}
			else
			{
				arg_46_0 = this.Columns;
			}
			List<ColumnRuleSchema> list = arg_46_0;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int count = list.Count;
			foreach (ColumnRuleSchema current in list)
			{
				num++;
				string str;
				if (num == count)
				{
					str = "";
				}
				else
				{
					str = ",";
				}
				stringBuilder.AppendLine("            sb.Append(\"?" + current.FieldName + str + "\");");
			}
			return stringBuilder.ToString();
		}

		public string ToInsertValuesParameter()
		{
			List<ColumnRuleSchema> arg_46_0;
			if (this.PrimaryKey.IsIdentity)
			{
				arg_46_0 = (from s in this.Columns
				where s.ColumnType != "PrimaryKey"
				select s).ToList<ColumnRuleSchema>();
			}
			else
			{
				arg_46_0 = this.Columns;
			}
			List<ColumnRuleSchema> list = arg_46_0;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int count = list.Count;
			stringBuilder.AppendLine("            MySqlParameter[] param = new MySqlParameter[" + count + "];");
			foreach (ColumnRuleSchema current in list)
			{
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"            param[",
					num,
					"] = new MySqlParameter(\"?",
					current.FieldName,
					"\",",
					this.EntityMemberName,
					".",
					current.PropertyName,
					");"
				}));
				num++;
			}
			return stringBuilder.ToString();
		}

		public string ToUpdateFields()
		{
			List<ColumnRuleSchema> list = (from s in this.Columns
			where s.ColumnType != "PrimaryKey"
			select s).ToList<ColumnRuleSchema>();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int count = list.Count;
			foreach (ColumnRuleSchema current in list)
			{
				num++;
				string text;
				if (num == count)
				{
					text = "";
				}
				else
				{
					text = ",";
				}
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"            sb.Append(\"`",
					current.FieldName,
					"`=?",
					current.FieldName,
					text,
					"\");"
				}));
			}
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				"            sb.Append(\" WHERE ",
				this.PrimaryKey.FieldName,
				"=?",
				this.PrimaryKey.FieldName,
				"\");"
			}));
			return stringBuilder.ToString();
		}

		public string ToUpdateValuesParameter()
		{
			List<ColumnRuleSchema> list = (from s in this.Columns
			where s.ColumnType != "PrimaryKey"
			select s).ToList<ColumnRuleSchema>();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = list.Count + 1;
			stringBuilder.AppendLine("            MySqlParameter[] param = new MySqlParameter[" + num2 + "];");
			foreach (ColumnRuleSchema current in list)
			{
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"            param[",
					num,
					"] = new MySqlParameter(\"?",
					current.FieldName,
					"\",",
					this.EntityMemberName,
					".",
					current.PropertyName,
					");"
				}));
				num++;
			}
			stringBuilder.AppendLine(string.Concat(new object[]
			{
				"            param[",
				num,
				"] = new MySqlParameter(\"?",
				this.PrimaryKey.FieldName,
				"\",",
				this.EntityMemberName,
				".",
				this.PrimaryKey.PropertyName,
				");"
			}));
			return stringBuilder.ToString();
		}

		public string ToSqlDaRule(string _NameSpace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ColumnRuleSchema current in this.Columns)
			{
				stringBuilder.AppendLine(current.ToEntityValue(this.EntityMemberName));
			}
			return string.Format(Resources.GetDalAccess, new object[]
			{
				_NameSpace,
				this.EntityName,
				this.EntityMemberName,
				this.TableName.ToLower(),
				this.PrimaryKey.FieldName,
				this.PrimaryKey.GetCSharpSystemType,
				stringBuilder.ToString(),
				this.ToUpdateFields(),
				this.ToUpdateValuesParameter(),
				this.ToInsertFields(),
				this.ToInsertFieldValues(),
				this.ToInsertValuesParameter(),
				this.ToInsertReturn(),
				this.Description,
				this.TableName.ToLower().Replace("_tb", "").Replace("tb", "")
			});
		}

		public string ToSqlBizRule(string _NameSpace)
		{
			return string.Format(Resources.GetBizRule, new object[]
			{
				_NameSpace,
				this.EntityName,
				this.EntityMemberName,
				this.PrimaryKey.GetCSharpSystemType,
				this.Description,
				this.LogModuleType,
				this.ToCheckSql(),
				this.ConnectionKeyOrConnectionString
			});
		}

		public string ToJavaDalRule(string _NameSpace)
		{
			return string.Format(Resources.GetJavaDal, this.EntityName, this.TableName, this.TableName.ToLower().Replace("_tb", "").Replace("tb", ""));
		}

		public string ToDataEntity(string _NameSpace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("using System;");
			stringBuilder.AppendLine("using System.Collections;");
			stringBuilder.AppendLine("using System.Runtime.Serialization;");
			stringBuilder.AppendLine("using WTF.Framework;");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine("namespace " + _NameSpace + ".DataEntity");
			stringBuilder.AppendLine("{");
			stringBuilder.AppendLine("    #region " + this.EntityName);
			stringBuilder.AppendLine("    /// <summary>");
			if (string.IsNullOrEmpty(this.Description))
			{
				stringBuilder.AppendLine("    /// " + this.EntityName);
			}
			else
			{
				stringBuilder.AppendLine("    /// " + this.Description + " 实体层");
			}
			stringBuilder.AppendLine("    /// </summary>");
			stringBuilder.AppendLine("    [Serializable]");
			stringBuilder.AppendLine("    public partial class " + this.EntityName);
			stringBuilder.AppendLine("    {");
			stringBuilder.AppendLine("        #region Member Variables ");
			foreach (ColumnRuleSchema current in this.Columns)
			{
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"        private System.",
					current.GetCSharpSystemType,
					" ",
					current.MemberName,
					";"
				}));
			}
			stringBuilder.AppendLine("        #endregion ");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine("        #region  Constructors ");
			stringBuilder.AppendLine("        public " + this.EntityName + "()");
			stringBuilder.AppendLine("        {");
			foreach (ColumnRuleSchema current in this.Columns)
			{
				string text = current.FieldName.ToLower();
				if (text.IndexOf("guid") >= 0)
				{
					stringBuilder.AppendLine("         " + current.MemberName + " = System.Guid.NewGuid().ToString();");
				}
				else
				{
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						"         ",
						current.MemberName,
						"=",
						current.DefaultValue,
						";"
					}));
				}
			}
			stringBuilder.AppendLine("        }");
			stringBuilder.AppendLine("        #endregion ");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine("        #region  Public Properties ");
			ColumnRuleSchema primaryKey = this.PrimaryKey;
			foreach (ColumnRuleSchema current in this.Columns)
			{
				string getCSharpSystemType = current.GetCSharpSystemType;
				stringBuilder.AppendLine("        /// <summary>");
				stringBuilder.AppendLine("        /// " + current.FieldName);
				if (!string.IsNullOrEmpty(current.Description))
				{
					stringBuilder.AppendLine("        /// " + current.Description);
				}
				stringBuilder.AppendLine("        /// </summary>");
				if (primaryKey != null && primaryKey.FieldName == current.FieldName)
				{
					if (primaryKey.IsIdentity)
					{
						stringBuilder.AppendLine("        [PrimaryKey]");
					}
					else
					{
						stringBuilder.AppendLine("        [PrimaryKey(false)]");
					}
				}
				stringBuilder.AppendLine("        public " + getCSharpSystemType + " " + current.PropertyName);
				stringBuilder.AppendLine("        {");
				stringBuilder.AppendLine("            get { return " + current.MemberName + "; }");
				if (this.IsCheckFieldLength && getCSharpSystemType == "String" && current.DataType != "text" && current.DataType != "longtext" && current.DataType != "mediumtext")
				{
					stringBuilder.AppendLine(string.Concat(new object[]
					{
						"            set { if (value.Length > ",
						current.Length,
						") ",
						Environment.NewLine
					}));
					stringBuilder.AppendLine(string.Concat(new object[]
					{
						"\t    \t        throw new Exception(\"",
						current.FieldName,
						"请小于",
						current.Length,
						"位\");"
					}));
					stringBuilder.AppendLine("\t    \t    else");
					stringBuilder.AppendLine("\t    \t        " + current.MemberName + " = value ;}");
				}
				else
				{
					stringBuilder.AppendLine("            set { " + current.MemberName + " = value ;}");
				}
				stringBuilder.AppendLine("        }");
				if (this.IsMongoDB && primaryKey != null && primaryKey.FieldName == current.FieldName)
				{
					stringBuilder.AppendLine("        /// <summary>");
					stringBuilder.AppendLine("        ///MongoDB_id");
					stringBuilder.AppendLine("        /// </summary>");
					stringBuilder.AppendLine("        public " + getCSharpSystemType + " _id");
					stringBuilder.AppendLine("        {");
					stringBuilder.AppendLine("            get { return " + current.MemberName + "; }");
					stringBuilder.AppendLine("            set { " + current.MemberName + " = value ;}");
					stringBuilder.AppendLine("        }");
				}
			}
			stringBuilder.AppendLine("    #endregion ");
			stringBuilder.AppendLine("    }");
			stringBuilder.AppendLine("    #endregion ");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		public string ToJavaDataSetEntity(string _NameSpace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				this.EntityName,
				" ",
				this.EntityMemberName,
				"= new ",
				this.EntityName,
				"();"
			}));
			ColumnRuleSchema primaryKey = this.PrimaryKey;
			foreach (ColumnRuleSchema current in from s in this.Columns
			where s.ColumnType != "PrimaryKey" && s.FieldName.ToLower().IndexOf("guid") < 0
			select s)
			{
				stringBuilder.AppendLine(this.EntityMemberName + ".set" + current.PropertyName + "();");
			}
			return stringBuilder.ToString();
		}

		public string ToDataEntityValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Concat(new string[]
			{
				this.EntityName,
				" obj",
				this.EntityName,
				"= new ",
				this.EntityName,
				"();"
			}));
			ColumnRuleSchema primaryKey = this.PrimaryKey;
			foreach (ColumnRuleSchema current in from s in this.Columns
			where s.FieldName.ToLower().IndexOf("guid") < 0
			select s)
			{
				if (!current.IsIdentity)
				{
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						" obj",
						this.EntityName,
						".",
						current.PropertyName,
						"=;"
					}));
				}
			}
			return stringBuilder.ToString();
		}

		public string ToJavaDataEntity(string _NameSpace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("import java.util.Date;");
			stringBuilder.AppendLine("import com.WTF.core.*;");
			stringBuilder.AppendLine("import com.WTF.dal.*;");
			stringBuilder.AppendLine("public class  " + this.EntityName);
			stringBuilder.AppendLine("{");
			ColumnRuleSchema primaryKey = this.PrimaryKey;
			foreach (ColumnRuleSchema current in this.Columns)
			{
				if (primaryKey != null && primaryKey.FieldName == current.FieldName)
				{
					if (primaryKey.IsIdentity)
					{
						stringBuilder.AppendLine("\t@ColumnInfo(PrimaryKey=true,Identity=true)");
					}
					else
					{
						stringBuilder.AppendLine("\t@ColumnInfo(PrimaryKey=true)");
					}
				}
				if (current.DataType.Contains("text"))
				{
					stringBuilder.AppendLine("\t@ColumnInfo(JdbcType= JdbcType.LONGVARCHAR)");
				}
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"\tprivate ",
					current.GetJavaSystemType,
					" ",
					current.PropertyCamelName,
					";"
				}));
				stringBuilder.AppendLine("\t/**");
				stringBuilder.AppendLine("\t* " + current.Description);
				stringBuilder.AppendLine("\t* @return ");
				stringBuilder.AppendLine("\t*/");
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"\tpublic ",
					current.GetJavaSystemType,
					" get",
					current.PropertyName,
					"() {"
				}));
				stringBuilder.AppendLine("\t\treturn " + current.PropertyCamelName + ";");
				stringBuilder.AppendLine("\t}");
				stringBuilder.AppendLine("\t/**");
				stringBuilder.AppendLine("\t* " + current.Description);
				stringBuilder.AppendLine("\t* @param " + current.PropertyCamelName);
				stringBuilder.AppendLine("\t*/");
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"\tpublic void set",
					current.PropertyName,
					"(",
					current.GetJavaSystemType,
					" ",
					current.PropertyCamelName,
					") {"
				}));
				stringBuilder.AppendLine(string.Concat(new string[]
				{
					"\t\tthis.",
					current.PropertyCamelName,
					" = ",
					current.PropertyCamelName,
					";"
				}));
				stringBuilder.AppendLine("\t}");
			}
			stringBuilder.AppendLine("\tpublic " + this.EntityName + "()");
			stringBuilder.AppendLine("\t{");
			foreach (ColumnRuleSchema current in this.Columns)
			{
				string text = current.FieldName.ToLower();
				if (text.IndexOf("guid") >= 0)
				{
					stringBuilder.AppendLine("\t\t" + current.PropertyCamelName + " = StringHelper.UUID36();");
				}
				else
				{
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						"\t\t",
						current.PropertyCamelName,
						"=",
						current.DefaultJavaValue,
						";"
					}));
				}
			}
			stringBuilder.AppendLine("\t}");
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		public string ToDataEntitySolrIndex(string _NameSpace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ColumnRuleSchema current in this.Columns)
			{
				if (current.IsPrimaryKey)
				{
					stringBuilder.AppendLine(string.Format("<field name=\"{0}\" type=\"{1}\" indexed=\"true\" stored=\"true\" multiValued=\"false\" required=\"true\" />", current.FieldName, current.GetSolrharpSystemType));
				}
				else
				{
					stringBuilder.AppendLine(string.Format("<field name=\"{0}\" type=\"{1}\" indexed=\"true\" stored=\"true\" multiValued=\"false\" />", current.FieldName, current.GetSolrharpSystemType));
				}
			}
			return stringBuilder.ToString();
		}

		public string ToDataEntityESMappingIndex(string _NameSpace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("{");
			stringBuilder.AppendLine("    \"dynamic\":\"strict\",");
			stringBuilder.Append("    \"properties\" :{");
			foreach (ColumnRuleSchema current in this.Columns)
			{
				stringBuilder.AppendLine("");
				stringBuilder.AppendLine(string.Format("        \"{0}\":{{", current.FieldName));
				string getESMappingSystemType = current.GetESMappingSystemType;
				if (getESMappingSystemType.Equals("timestamp"))
				{
					stringBuilder.AppendLine("            \"type\":\"date\"");
					stringBuilder.AppendLine("            ,\"format\":\"epoch_millis\"");
				}
				else
				{
					stringBuilder.AppendLine(string.Format("            \"type\":\"{0}\"", getESMappingSystemType));
					if (getESMappingSystemType.Equals("date"))
					{
						stringBuilder.AppendLine("            ,\"format\":\"yyyy-MM-dd HH:mm:ss\"");
					}
					if (getESMappingSystemType.Equals("string"))
					{
						stringBuilder.AppendLine("            ,\"index\":\"not_analyzed\"");
					}
				}
				stringBuilder.Append("        },");
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			}) + "\r\n       }\r\n}";
		}

		public string ToDataEntitySolrEntity(string _NameSpace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ColumnRuleSchema current in this.Columns)
			{
				stringBuilder.AppendLine(string.Format("<field column=\"{0}\" name=\"{0}\" />", current.FieldName));
			}
			return stringBuilder.ToString();
		}

		public string ToDataEntityProperties(string _NameSpace)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("using System;");
			stringBuilder.AppendLine("using System.Collections;");
			stringBuilder.AppendLine("using System.Runtime.Serialization;");
			stringBuilder.AppendLine("using WTF.Framework;");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine("namespace " + _NameSpace + ".DataEntity");
			stringBuilder.AppendLine("{");
			stringBuilder.AppendLine("    #region " + this.EntityName);
			stringBuilder.AppendLine("    /// <summary>");
			if (string.IsNullOrEmpty(this.Description))
			{
				stringBuilder.AppendLine("    /// " + this.EntityName);
			}
			else
			{
				stringBuilder.AppendLine("    /// " + this.Description + " 实体层");
			}
			stringBuilder.AppendLine("    /// </summary>");
			stringBuilder.AppendLine("    [Serializable]");
			stringBuilder.AppendLine("    public class " + this.EntityName);
			stringBuilder.AppendLine("    {");
			stringBuilder.AppendLine("        #region  Constructors ");
			stringBuilder.AppendLine("        public " + this.EntityName + "()");
			stringBuilder.AppendLine("        {");
			foreach (ColumnRuleSchema current in this.Columns)
			{
				string text = current.FieldName.ToLower();
				if (text.IndexOf("guid") >= 0)
				{
					stringBuilder.AppendLine("         " + current.PropertyName + " = System.Guid.NewGuid().ToString();");
				}
				else
				{
					stringBuilder.AppendLine(string.Concat(new string[]
					{
						"         ",
						current.PropertyName,
						"=",
						current.DefaultValue,
						";"
					}));
				}
			}
			stringBuilder.AppendLine("        }");
			stringBuilder.AppendLine("        #endregion ");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine("        #region  Public Properties ");
			ColumnRuleSchema primaryKey = this.PrimaryKey;
			foreach (ColumnRuleSchema current in this.Columns)
			{
				string getCSharpSystemType = current.GetCSharpSystemType;
				stringBuilder.AppendLine("        /// <summary>");
				stringBuilder.AppendLine("        /// " + current.FieldName);
				if (!string.IsNullOrEmpty(current.Description))
				{
					stringBuilder.AppendLine("        /// " + current.Description);
				}
				stringBuilder.AppendLine("        /// </summary>");
				if (primaryKey != null && primaryKey.FieldName == current.FieldName)
				{
					stringBuilder.AppendLine("        [PrimaryKey]");
				}
				stringBuilder.AppendLine("        public " + getCSharpSystemType + " " + current.PropertyName);
				stringBuilder.AppendLine("        {");
				stringBuilder.AppendLine("            get;");
				stringBuilder.AppendLine("            set;");
				stringBuilder.AppendLine("        }");
			}
			stringBuilder.AppendLine("    #endregion ");
			stringBuilder.AppendLine("    }");
			stringBuilder.AppendLine("    #endregion ");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		public string ToObjecQueryCode(bool isdefault)
		{
			return string.Format(isdefault ? Resources.GetObjectQuery : Resources.GetDbSet, this.TableName, this.Description);
		}

		public string ToOperateCode(bool isdefault, string LogLogModuleType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsInsert)
			{
				stringBuilder.AppendLine(this.ToInsert(isdefault, LogLogModuleType));
			}
			if (this.IsUpdate)
			{
				stringBuilder.AppendLine(this.ToUpdate(LogLogModuleType));
			}
			if (this.IsDelete)
			{
				stringBuilder.AppendLine(this.ToDeleteKey(isdefault));
			}
			return stringBuilder.ToString();
		}

		private string ToInsert(bool isdefault, string LogLogModuleType)
		{
			return string.Format(isdefault ? Resources.GetInsertObject : Resources.GetInsertDbSet, new object[]
			{
				this.TableName,
				this.SimpleTableName,
				this.Description,
				this.ToCheck(LogLogModuleType)
			});
		}

		private string ToCheck(string LogLogModuleType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ColumnRuleSchema current in from s in this.Columns
			where s.IsCheck
			select s)
			{
				stringBuilder.AppendLine(string.Format("            obj{0}.{1}.CheckIsNull(\"{2}\",\"{3}\");", new object[]
				{
					this.TableName,
					current.FieldName,
					current.ErrorMessage,
					LogLogModuleType
				}));
			}
			return stringBuilder.ToString();
		}

		private string ToCheckSql()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ColumnRuleSchema current in from s in this.Columns
			where s.IsCheck
			select s)
			{
				stringBuilder.AppendLine(string.Format("            {0}.{1}.CheckIsNull(\"{2}\",LogModuleType);", this.EntityMemberName, current.FieldName, current.ErrorMessage));
			}
			return stringBuilder.ToString();
		}

		private string ToUpdate(string LogLogModuleType)
		{
			return string.Format(Resources.GetUpdateObject, new object[]
			{
				this.TableName,
				this.SimpleTableName,
				this.Description,
				this.ToCheck(LogLogModuleType)
			});
		}

		private string ToDeleteKey(bool isdefault)
		{
			string result;
			if (isdefault)
			{
				result = string.Format(Resources.GetDeleteKey, this.TableName, this.SimpleTableName, this.Description);
			}
			else if (this.PrimaryKey == null)
			{
				result = string.Format(Resources.GetDeleteTEmpty, this.Description, this.SimpleTableName);
			}
			else
			{
				result = string.Format(Resources.GetDeleteT, new object[]
				{
					this.PrimaryKeyType,
					this.PrimaryKey.FieldName,
					this.SimpleTableName,
					this.TableName,
					this.Description,
					(this.PrimaryKeyType == "int") ? "Int" : this.PrimaryKeyType
				});
			}
			return result;
		}

		private string ToDelete()
		{
			return string.Format(Resources.GetDeleteCondition, this.PrimaryKey.FieldName, this.SimpleTableName, this.Description);
		}
	}
}
