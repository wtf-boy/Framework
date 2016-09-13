using System;
using System.Linq;

namespace WTF.CodeRule
{
	public class ColumnEditSchema
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
				return "_" + this.FieldName.CamelCase();
			}
		}

		public string DataType
		{
			get;
			set;
		}

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

		public string EntityName
		{
			get;
			set;
		}

		public string EntityMemberName
		{
			get
			{
				return char.ToLower(this.EntityName[0]) + this.EntityName.Substring(1);
			}
		}

		public int Length
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

		public bool IsAutoValue
		{
			get;
			set;
		}

		public bool IsEmpty
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string ValidationReg
		{
			get;
			set;
		}

		public string ColumnType
		{
			get;
			set;
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
			case "char":
				result = "string";
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
			case "char":
				result = "GetString";
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
			return string.Format("public {1} {0}\r\n    {{\r\n        get\r\n        {{\r\n            return {2}(\"{0}\");\r\n\r\n        }}\r\n\r\n    }}", this.PropertyName, this.GetDataType(), this.GetRequestType());
		}

		public string ToUiString()
		{
			return string.Format("<tr>\r\n            <td>\r\n             {0}{1}\r\n            </td>\r\n            <td>\r\n             {2}\r\n            </td>\r\n        </tr>", this.IsEmpty ? "" : "<span class=\"txtNoNull\">*</span>", this.FieldTitle + ":", this.GetControl());
		}

		public string ToUiStringSql()
		{
			return string.Format("<tr>\r\n            <td>\r\n             {0}{1}\r\n            </td>\r\n            <td>\r\n             {2}\r\n            </td>\r\n        </tr>", this.IsEmpty ? "" : "<span class=\"txtNoNull\">*</span>", this.FieldTitle + ":", this.GetControlSql());
		}

		public string ToSaveInfo()
		{
			string result;
			if (this.IsShow)
			{
				result = string.Format("{0}.{1}={2};", "obj" + this.TableName, this.FieldName, this.GetControlVale());
			}
			else
			{
				result = string.Format("{0}.{1}={2};", "obj" + this.TableName, this.FieldName, this.GetControlHideVale());
			}
			return result;
		}

		public string ToSaveInfoSql()
		{
			string result;
			if (this.IsShow)
			{
				result = string.Format("{0}.{1}={2};", "obj" + this.EntityName, this.PropertyName, this.GetControlVale());
			}
			else
			{
				result = string.Format("{0}.{1}={2};", "obj" + this.EntityName, this.PropertyName, this.GetControlHideVale());
			}
			return result;
		}

		public string ToRenderControlPage()
		{
			string controlType = this.ControlType;
			string result;
			if (controlType != null)
			{
				if (controlType == "DropDown")
				{
					result = string.Format("drop{0}.SelectedValue=obj{1}.{0}.ToString();", this.FieldName, this.TableName);
					return result;
				}
				if (controlType == "CheckBox")
				{
					result = string.Format("chk{0}.Checked=obj{1}.{0};", this.FieldName, this.TableName);
					return result;
				}
				if (controlType == "CheckBoxList")
				{
					result = string.Format("chk{0}.SetSelectValue(obj{1}.{0}.ToString());", this.FieldName, this.TableName);
					return result;
				}
				if (controlType == "RadioButton")
				{
					result = string.Format("rad{0}.SelectedValue=obj{1}.{0}.ToString();", this.FieldName, this.TableName);
					return result;
				}
			}
			result = "";
			return result;
		}

		public string ToRenderControlPageSql()
		{
			string controlType = this.ControlType;
			string result;
			if (controlType != null)
			{
				if (controlType == "DropDown")
				{
					result = string.Format("drop{0}.SelectedValue=obj{1}.{0}.ToString();", this.PropertyName, this.EntityName);
					return result;
				}
				if (controlType == "CheckBox")
				{
					result = string.Format("chk{0}.Checked=obj{1}.{0}==1;", this.PropertyName, this.EntityName);
					return result;
				}
				if (controlType == "CheckBoxList")
				{
					result = string.Format("chk{0}.SetSelectValue(obj{1}.{0}.ToString());", this.PropertyName, this.EntityName);
					return result;
				}
				if (controlType == "RadioButton")
				{
					result = string.Format("rad{0}.SelectedValue=obj{1}.{0}.ToString();", this.PropertyName, this.EntityName);
					return result;
				}
			}
			result = "";
			return result;
		}

		public string GetControl()
		{
			string controlType = this.ControlType;
			string result;
			switch (controlType)
			{
			case "DropDown":
				result = string.Format("<WTF:MyDropDownList ID=\"" + (this.IsAutoValue ? "" : "drop") + "{0}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" > </WTF:MyDropDownList>", this.FieldName, this.IsEmpty ? "" : "CheckValueEmpty=\"true\"", this.ErrorMessage);
				return result;
			case "RadioButton":
				result = string.Format("<WTF:MyRadioButtonList ID=\"" + (this.IsAutoValue ? "" : "rad") + "{0}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" > </WTF:MyRadioButtonList>", this.FieldName, this.IsEmpty ? "" : "CheckValueEmpty=\"true\"", this.ErrorMessage);
				return result;
			case "TextBox":
				result = string.Format("<WTF:MyTextBox ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\"   ValidationGroup=\"SaveGroup\" {1}  {4} {5} ErrorMessage=\"{2}\" runat=\"server\" Text=\"<%# {3}.{0} %>\"></WTF:MyTextBox>", new object[]
				{
					this.FieldName,
					this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
					this.ErrorMessage,
					"obj" + this.TableName,
					string.IsNullOrEmpty(this.ValidationReg) ? "" : ("ValidationExpression=\"" + this.ValidationReg + "\""),
					(this.DataType.IndexOf("char") >= 0) ? ("MaxLength=\"" + this.Length + "\"") : ""
				});
				return result;
			case "CheckBox":
				result = string.Format(" <asp:CheckBox ID=\"" + (this.IsAutoValue ? "" : "chk") + "{0}\" runat=\"server\"  />", this.FieldName);
				return result;
			case "CheckBoxList":
				result = string.Format(" <WTF:MyCheckBoxList ID=\"" + (this.IsAutoValue ? "" : "chk") + "{0}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\"   ></WTF:MyCheckBoxList>", this.FieldName, this.IsEmpty ? "" : "CheckValueEmpty=\"true\"", this.ErrorMessage);
				return result;
			case "Xhtml":
				result = string.Format("<WTF:MyXheditor ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\" runat=\"server\"  ResourceTypeID=\"\" Skin=\"vista\" RestrictFlashCode=\"\" RestrictFileCode=\"\"  RestrictImgCode=\"\" RestrictMediaCode=\"\" Tools=\"\" LogModuleType=\"\"  Text=\"<%# {2}.{0} %>\" ValidationGroup=\"SaveGroup\"  {3}  ErrorMessage=\"{4}\" ></WTF:MyXheditor>{1}", new object[]
				{
					this.FieldName,
					this.IsEmpty ? ("<script type=\"text/javascript\">function getxhvalue() {$('#<%=txt" + this.FieldName + ".ClientID %>').val(); return true;} </script>") : "",
					"obj" + this.TableName,
					this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
					this.ErrorMessage
				});
				return result;
			case "TextDateTime":
				result = string.Format("<WTF:MyTextBox ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\"  SkinID=\"Date\" onfocus=\"{4}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" Text='<%# {3}.{0}.ToString(\"yyyy-MM-dd HH:mm:ss\") %>'></WTF:MyTextBox>", new object[]
				{
					this.FieldName,
					this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
					this.ErrorMessage,
					"obj" + this.TableName,
					"new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
				});
				return result;
			case "TextDate":
				result = string.Format("<WTF:MyTextBox ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\"  SkinID=\"Date\"  onfocus=\"{4}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" Text='<%# {3}.{0}.ToString(\"yyyy-MM-dd\") %>'></WTF:MyTextBox>", new object[]
				{
					this.FieldName,
					this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
					this.ErrorMessage,
					"obj" + this.TableName,
					"new WdatePicker({dateFmt:'yyyy-MM-dd'})"
				});
				return result;
			}
			result = string.Format("<WTF:MyTextBox ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\"   ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" Text=\"<%# {3}.{0} %>\"></WTF:MyTextBox>", new object[]
			{
				this.FieldName,
				this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
				this.ErrorMessage,
				"obj" + this.TableName
			});
			return result;
		}

		public string GetControlSql()
		{
			string controlType = this.ControlType;
			string result;
			switch (controlType)
			{
			case "DropDown":
				result = string.Format("<WTF:MyDropDownList ID=\"" + (this.IsAutoValue ? "" : "drop") + "{0}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" > </WTF:MyDropDownList>", this.PropertyName, this.IsEmpty ? "" : "CheckValueEmpty=\"true\"", this.ErrorMessage);
				return result;
			case "RadioButton":
				result = string.Format("<WTF:MyRadioButtonList ID=\"" + (this.IsAutoValue ? "" : "rad") + "{0}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" > </WTF:MyRadioButtonList>", this.PropertyName, this.IsEmpty ? "" : "CheckValueEmpty=\"true\"", this.ErrorMessage);
				return result;
			case "TextBox":
				result = string.Format("<WTF:MyTextBox ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\"   ValidationGroup=\"SaveGroup\" {1}  {4} {5} ErrorMessage=\"{2}\" runat=\"server\" Text=\"<%# {3}.{0} %>\"></WTF:MyTextBox>", new object[]
				{
					this.PropertyName,
					this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
					this.ErrorMessage,
					"obj" + this.EntityName,
					string.IsNullOrEmpty(this.ValidationReg) ? "" : ("ValidationExpression=\"" + this.ValidationReg + "\""),
					(this.DataType.IndexOf("char") >= 0) ? ("MaxLength=\"" + this.Length + "\"") : ""
				});
				return result;
			case "CheckBox":
				result = string.Format(" <asp:CheckBox ID=\"" + (this.IsAutoValue ? "" : "chk") + "{0}\" runat=\"server\"  />", this.PropertyName);
				return result;
			case "CheckBoxList":
				result = string.Format(" <WTF:MyCheckBoxList ID=\"" + (this.IsAutoValue ? "" : "chk") + "{0}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\"   ></WTF:MyCheckBoxList>", this.PropertyName, this.IsEmpty ? "" : "CheckValueEmpty=\"true\"", this.ErrorMessage);
				return result;
			case "Xhtml":
				result = string.Format("<WTF:MyXheditor ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\" runat=\"server\"  ResourceTypeID=\"\" Skin=\"vista\" RestrictFlashCode=\"\" RestrictFileCode=\"\"  RestrictImgCode=\"\" RestrictMediaCode=\"\" Tools=\"\" LogModuleType=\"\"  Text=\"<%# {2}.{0} %>\" ValidationGroup=\"SaveGroup\"  {3}  ErrorMessage=\"{4}\" ></WTF:MyXheditor>{1}", new object[]
				{
					this.PropertyName,
					this.IsEmpty ? ("<script type=\"text/javascript\">function getxhvalue() {$('#<%=txt" + this.PropertyName + ".ClientID %>').val(); return true;} </script>") : "",
					"obj" + this.EntityName,
					this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
					this.ErrorMessage
				});
				return result;
			case "TextDateTime":
				result = string.Format("<WTF:MyTextBox ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\"  SkinID=\"Date\" onfocus=\"{4}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" Text='<%# {3}.{0}.ToString(\"yyyy-MM-dd HH:mm:ss\") %>'></WTF:MyTextBox>", new object[]
				{
					this.PropertyName,
					this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
					this.ErrorMessage,
					"obj" + this.EntityName,
					"new WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
				});
				return result;
			case "TextDate":
				result = string.Format("<WTF:MyTextBox ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\"  SkinID=\"Date\"  onfocus=\"{4}\"  ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" Text='<%# {3}.{0}.ToString(\"yyyy-MM-dd\") %>'></WTF:MyTextBox>", new object[]
				{
					this.PropertyName,
					this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
					this.ErrorMessage,
					"obj" + this.EntityName,
					"new WdatePicker({dateFmt:'yyyy-MM-dd'})"
				});
				return result;
			}
			result = string.Format("<WTF:MyTextBox ID=\"" + (this.IsAutoValue ? "" : "txt") + "{0}\"   ValidationGroup=\"SaveGroup\" {1}  ErrorMessage=\"{2}\" runat=\"server\" Text=\"<%# {3}.{0} %>\"></WTF:MyTextBox>", new object[]
			{
				this.PropertyName,
				this.IsEmpty ? "" : "CheckValueEmpty=\"true\"",
				this.ErrorMessage,
				"obj" + this.EntityName
			});
			return result;
		}

		public string GetControlVale()
		{
			string controlType = this.ControlType;
			string result;
			switch (controlType)
			{
			case "DropDown":
				result = string.Format("drop{0}.{1}", this.PropertyName, (this.DataType == "int") ? "SelectValueInt" : ((this.DataType == "uniqueidentifier") ? "SelectValueGuid" : "SelectedValue"));
				return result;
			case "TextBox":
				result = string.Format("txt{0}.{1}", this.PropertyName, (this.DataType.IndexOf("char") >= 0) ? ("TextCutWord(" + this.Length + ")") : "TextInt");
				return result;
			case "CheckBox":
				result = string.Format("chk{0}.Checked?1:0", this.PropertyName);
				return result;
			case "RadioButton":
				result = string.Format("rad{0}.SelectValueInt", this.PropertyName);
				return result;
			case "CheckBoxList":
				result = string.Format("chk{0}.SelectValueString", this.PropertyName);
				return result;
			case "Xhtml":
				result = string.Format("txt{0}.Text", this.PropertyName);
				return result;
			case "TextDateTime":
				result = string.Format("txt{0}.TextCurrentDateTime", this.PropertyName);
				return result;
			case "TextDate":
				result = string.Format("txt{0}.TextCurrentDateTime", this.PropertyName);
				return result;
			}
			result = string.Format("txt{0}.Text", this.PropertyName);
			return result;
		}

		public string GetControlHideVale()
		{
			string controlType = this.ControlType;
			string result;
			if (controlType != null)
			{
				if (controlType == "TextDateTime" || controlType == "TextDate")
				{
					result = "DateTime.Now";
					return result;
				}
			}
			result = "";
			return result;
		}
	}
}
