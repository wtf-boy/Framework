using WTF.CodeRule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace WTFCode
{
	public class CodeConfigHelper
	{
		public string _CodeConfigPath = "";

		private XmlDocument _XmlDocument = null;

		public XmlDocument CodeConfig
		{
			get
			{
				return this._XmlDocument;
			}
		}

		public string GetConnectionString(XmlNode objConnectionNode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SERVER=" + objConnectionNode.Attributes["ServerIP"].Value + ";");
			stringBuilder.Append("Port=" + objConnectionNode.Attributes["Port"].Value + ";");
			stringBuilder.Append("user id=" + objConnectionNode.Attributes["Uid"].Value + ";");
			stringBuilder.Append("password=" + objConnectionNode.Attributes["Pwd"].Value + ";");
			stringBuilder.Append("Database=" + objConnectionNode.Attributes["Database"].Value + ";");
			stringBuilder.Append("persist security info=True;Allow User Variables=True;Charset=utf8; ");
			return stringBuilder.ToString();
		}

		public string GetConnectionString(string ConnectionKey, out string Database)
		{
			Database = "";
			XmlNode xmlNode = this.CodeConfig.SelectSingleNode("//ConnectionStrings/ConnectionString[@Name='" + ConnectionKey + "']");
			string result;
			if (xmlNode == null)
			{
				result = "";
			}
			else
			{
				Database = xmlNode.ReadAttribute("Database", "");
				result = this.GetConnectionString(xmlNode);
			}
			return result;
		}

		public void Save()
		{
			this._XmlDocument.Save(this._CodeConfigPath);
		}

		public void InitConnectionStrings(ComboBox objComboBox)
		{
			objComboBox.Items.Clear();
			XmlNodeList xmlNodeList = this.CodeConfig.SelectNodes("//ConnectionStrings/ConnectionString");
			foreach (XmlNode objXmlNode in xmlNodeList)
			{
				string text = objXmlNode.ReadAttribute("Name", "");
				objComboBox.Items.Add(text);
				objComboBox.SelectedItem = text;
			}
		}

		public List<BusinessNodeInfo> GetBusiness()
		{
			List<BusinessNodeInfo> list = new List<BusinessNodeInfo>();
			foreach (XmlNode objXmlNode in this._XmlDocument.SelectNodes("//CodeConfig/BusinessConfig/Business"))
			{
				BusinessNodeInfo item = new BusinessNodeInfo
				{
					TableName = objXmlNode.ReadAttribute("TableName", ""),
					ConnectionKeyOrConnectionString = objXmlNode.ReadAttribute("ConnectionKeyOrConnectionString", ""),
					LogModuleType = objXmlNode.ReadAttribute("LogModuleType", ""),
					IsMongoDB = bool.Parse(objXmlNode.ReadAttribute("IsMongoDB", "false")),
					IsCheckFieldLength = bool.Parse(objXmlNode.ReadAttribute("IsCheckFieldLength", "true")),
					EntityName = objXmlNode.ReadAttribute("EntityName", ""),
					ConnectionKey = objXmlNode.ReadAttribute("ConnectionKey", ""),
					UIProjectName = objXmlNode.ReadAttribute("UIProjectName", ""),
					UIProjectPath = objXmlNode.ReadAttribute("UIProjectPath", ""),
					ModuleID = objXmlNode.ReadAttribute("ModuleID", ""),
					EditUrl = objXmlNode.ReadAttribute("EditUrl", ""),
					ListUrl = objXmlNode.ReadAttribute("ListUrl", ""),
					Description = objXmlNode.ReadAttribute("Description", "")
				};
				list.Add(item);
			}
			return list;
		}

		public void GetBusinessNodeInfoUIPath(BusinessNodeInfo objBusinessNodeInfo, out string UIProjectPath, out string UIProjectName)
		{
			UIProjectPath = "Gao7.ManaeWeb\\Manage\\XXXX";
			UIProjectName = "Gao7.ManaeWeb";
			if (!string.IsNullOrWhiteSpace(objBusinessNodeInfo.UIProjectPath) || !string.IsNullOrWhiteSpace(objBusinessNodeInfo.UIProjectName))
			{
				UIProjectPath = objBusinessNodeInfo.UIProjectPath;
				UIProjectName = objBusinessNodeInfo.UIProjectName;
			}
			else
			{
				BusinessNodeInfo businessNodeInfo = (from s in this.GetBusiness()
				where s.UIProjectPath != "" && s.UIProjectName != ""
				select s).FirstOrDefault<BusinessNodeInfo>();
				if (businessNodeInfo != null)
				{
					UIProjectPath = businessNodeInfo.UIProjectPath;
					UIProjectName = businessNodeInfo.UIProjectName;
				}
			}
		}

		public string GetBusinessNodeInfoExistsConnectionString()
		{
			BusinessNodeInfo businessNodeInfo = (from s in this.GetBusiness()
			where s.ConnectionKeyOrConnectionString != ""
			select s).FirstOrDefault<BusinessNodeInfo>();
			string result;
			if (businessNodeInfo != null)
			{
				result = businessNodeInfo.ConnectionKeyOrConnectionString;
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string GetBusinessNodeInfoExistsLogModuleType()
		{
			BusinessNodeInfo businessNodeInfo = (from s in this.GetBusiness()
			where s.LogModuleType != ""
			select s).FirstOrDefault<BusinessNodeInfo>();
			string result;
			if (businessNodeInfo != null)
			{
				result = businessNodeInfo.LogModuleType;
			}
			else
			{
				result = "";
			}
			return result;
		}

		public BusinessNodeInfo GetBusiness(string EntityName)
		{
			XmlNode xmlNode = this.CodeConfig.SelectSingleNode("//BusinessConfig/Business[@EntityName='" + EntityName + "']");
			BusinessNodeInfo result;
			if (xmlNode == null)
			{
				result = null;
			}
			else
			{
				BusinessNodeInfo businessNodeInfo = new BusinessNodeInfo
				{
					TableName = xmlNode.ReadAttribute("TableName", ""),
					ConnectionKeyOrConnectionString = xmlNode.ReadAttribute("ConnectionKeyOrConnectionString", ""),
					LogModuleType = xmlNode.ReadAttribute("LogModuleType", ""),
					IsMongoDB = bool.Parse(xmlNode.ReadAttribute("IsMongoDB", "false")),
					IsCheckFieldLength = bool.Parse(xmlNode.ReadAttribute("IsCheckFieldLength", "true")),
					EntityName = xmlNode.ReadAttribute("EntityName", ""),
					ConnectionKey = xmlNode.ReadAttribute("ConnectionKey", ""),
					UIProjectName = xmlNode.ReadAttribute("UIProjectName", ""),
					UIProjectPath = xmlNode.ReadAttribute("UIProjectPath", ""),
					ModuleID = xmlNode.ReadAttribute("ModuleID", ""),
					EditUrl = xmlNode.ReadAttribute("EditUrl", ""),
					ListUrl = xmlNode.ReadAttribute("ListUrl", ""),
					Description = xmlNode.ReadAttribute("Description", "")
				};
				result = businessNodeInfo;
			}
			return result;
		}

		public static void BusinessNodeToTableRuleSchema(BusinessNodeInfo objBusinessNodeInfo, TableRuleSchema objTableRuleSchema)
		{
			if (objBusinessNodeInfo != null && objTableRuleSchema != null)
			{
				objTableRuleSchema.LogModuleType = objBusinessNodeInfo.LogModuleType;
				objTableRuleSchema.ConnectionKeyOrConnectionString = objBusinessNodeInfo.ConnectionKeyOrConnectionString;
				objTableRuleSchema.EntityName = objBusinessNodeInfo.EntityName;
				objTableRuleSchema.ConnectionKey = objBusinessNodeInfo.ConnectionKey;
				objTableRuleSchema.IsMongoDB = objBusinessNodeInfo.IsMongoDB;
				objTableRuleSchema.IsCheckFieldLength = objBusinessNodeInfo.IsCheckFieldLength;
				objTableRuleSchema.UIProjectPath = objBusinessNodeInfo.UIProjectPath;
				objTableRuleSchema.UIProjectName = objBusinessNodeInfo.UIProjectName;
				objTableRuleSchema.EditUrl = objBusinessNodeInfo.EditUrl;
				objTableRuleSchema.ListUrl = objBusinessNodeInfo.ListUrl;
				objTableRuleSchema.ModuleID = objBusinessNodeInfo.ModuleID;
				if (string.IsNullOrWhiteSpace(objTableRuleSchema.Description))
				{
					objTableRuleSchema.Description = objBusinessNodeInfo.Description;
				}
			}
		}

		public CodeConfigHelper(string path)
		{
			this._CodeConfigPath = path;
		}

		public XmlNode GetConnectionStringNode(string ConnectionStringName, bool NoExistsAdd = false)
		{
			XmlNode xmlNode = this._XmlDocument.SelectSingleNode("//CodeConfig");
			if (xmlNode == null)
			{
				xmlNode = this._XmlDocument.CreateElement("CodeConfig");
				this._XmlDocument.AppendChild(xmlNode);
			}
			XmlNode xmlNode2 = this._XmlDocument.SelectSingleNode("//CodeConfig/ConnectionStrings");
			if (xmlNode2 == null)
			{
				xmlNode2 = this._XmlDocument.CreateElement("ConnectionStrings");
				xmlNode.AppendChild(xmlNode2);
			}
			XmlNode xmlNode3 = this._XmlDocument.SelectSingleNode("//ConnectionStrings/ConnectionString[@Name='" + ConnectionStringName + "']");
			if (xmlNode3 == null && NoExistsAdd)
			{
				xmlNode3 = this._XmlDocument.CreateElement("ConnectionString");
				xmlNode2.AppendChild(xmlNode3);
			}
			return xmlNode3;
		}

		public XmlNode GetBusinessNode(string TableName)
		{
			XmlNode xmlNode = this.CodeConfig.SelectSingleNode("//BusinessConfig/Business[@TableName='" + TableName + "']");
			XmlNode result;
			if (xmlNode != null)
			{
				result = xmlNode;
			}
			else
			{
				XmlNode xmlNode2 = this._XmlDocument.SelectSingleNode("//CodeConfig");
				if (xmlNode2 == null)
				{
					xmlNode2 = this._XmlDocument.CreateElement("CodeConfig");
					this._XmlDocument.AppendChild(xmlNode2);
				}
				XmlNode xmlNode3 = this._XmlDocument.SelectSingleNode("//CodeConfig/BusinessConfig");
				if (xmlNode3 == null)
				{
					xmlNode3 = this._XmlDocument.CreateElement("BusinessConfig");
					xmlNode2.AppendChild(xmlNode3);
				}
				result = null;
			}
			return result;
		}

		public XmlNode GetBusinessConfigNode()
		{
			XmlNode xmlNode = this.CodeConfig.SelectSingleNode("//CodeConfig/BusinessConfig");
			XmlNode result;
			if (xmlNode != null)
			{
				result = xmlNode;
			}
			else
			{
				XmlNode xmlNode2 = this._XmlDocument.SelectSingleNode("//CodeConfig");
				if (xmlNode2 == null)
				{
					xmlNode2 = this._XmlDocument.CreateElement("CodeConfig");
					this._XmlDocument.AppendChild(xmlNode2);
				}
				XmlNode xmlNode3 = this._XmlDocument.SelectSingleNode("//CodeConfig/BusinessConfig");
				if (xmlNode3 == null)
				{
					xmlNode3 = this._XmlDocument.CreateElement("BusinessConfig");
					xmlNode2.AppendChild(xmlNode3);
				}
				result = xmlNode3;
			}
			return result;
		}

		public bool LoadCodeConfigXml()
		{
			bool result;
			if (File.Exists(this._CodeConfigPath))
			{
				this._XmlDocument = new XmlDocument();
				try
				{
					this._XmlDocument.Load(this._CodeConfigPath);
				}
				catch (Exception ex)
				{
					MessageBox.Show("不好意思，你的CodeConfig.xml加载异常，请检查配置文件" + ex.ToString());
					result = false;
					return result;
				}
				result = true;
			}
			else
			{
				this._XmlDocument = new XmlDocument();
				result = true;
			}
			return result;
		}

		public static BusinessNodeInfo GetReadFileBusinessNodeInfo(string entityName, string projectPath)
		{
			string path = projectPath + "\\DataEntity\\" + entityName + ".cs";
			string path2 = projectPath + "\\Business\\Biz" + entityName + ".cs";
			string path3 = projectPath + "\\DataAccess\\Da" + entityName + ".cs";
			BusinessNodeInfo result;
			if (!File.Exists(path) || !File.Exists(path2) || !File.Exists(path3))
			{
				result = null;
			}
			else
			{
				Dictionary<string, BusinessNodeInfo> dictionary = new Dictionary<string, BusinessNodeInfo>();
				string input = File.ReadAllText(path);
				Regex regex = new Regex("public[\\w ]+_id", RegexOptions.IgnoreCase);
				MatchCollection matchCollection = regex.Matches(input);
				bool isMongoDB = matchCollection.Count > 0;
				string connectionKeyOrConnectionString = string.Empty;
				string logModuleType = "";
				string input2 = File.ReadAllText(path2);
				regex = new Regex("this\\(\\\"(?<connectionKey>[\\w\\.]*)\\\",\\s*dataObjectParam\\)", RegexOptions.IgnoreCase);
				matchCollection = regex.Matches(input2);
				if (matchCollection.Count > 0)
				{
					connectionKeyOrConnectionString = matchCollection[0].Groups["connectionKey"].Value;
				}
				regex = new Regex("Log.LogModuleType\\s*=\\s*\\\"(?<logModuleType>[\\w]*)\\\"", RegexOptions.IgnoreCase);
				matchCollection = regex.Matches(input2);
				if (matchCollection.Count > 0)
				{
					logModuleType = matchCollection[0].Groups["logModuleType"].Value;
				}
				string input3 = File.ReadAllText(path3);
				regex = new Regex(":\\s*base\\(\\\"(?<tableName>\\w*)\\\",", RegexOptions.IgnoreCase);
				string tableName = "";
				matchCollection = regex.Matches(input3);
				if (matchCollection.Count > 0)
				{
					tableName = matchCollection[0].Groups["tableName"].Value;
				}
				BusinessNodeInfo businessNodeInfo = new BusinessNodeInfo
				{
					EntityName = entityName,
					ConnectionKeyOrConnectionString = connectionKeyOrConnectionString,
					LogModuleType = logModuleType,
					IsMongoDB = isMongoDB,
					IsCheckFieldLength = true,
					TableName = tableName,
					EditUrl = "",
					ListUrl = "",
					ModuleID = ""
				};
				result = businessNodeInfo;
			}
			return result;
		}

		public void UpdateXml(List<TableRuleSchema> tableRuleSchemaList, string ConnectionKey)
		{
			XmlNode businessConfigNode = this.GetBusinessConfigNode();
			foreach (TableRuleSchema current in tableRuleSchemaList)
			{
				XmlNode businessNode = this.GetBusinessNode(current.TableName);
				if (businessNode == null)
				{
					XmlElement xmlElement = this.CodeConfig.CreateElement("Business");
					xmlElement.SetAttribute("TableName", current.TableName);
					xmlElement.SetAttribute("EntityName", current.EntityName);
					xmlElement.SetAttribute("Description", current.Description);
					xmlElement.SetAttribute("IsMongoDB", current.IsMongoDB.ToString());
					xmlElement.SetAttribute("IsCheckFieldLength", current.IsCheckFieldLength.ToString());
					xmlElement.SetAttribute("ConnectionKey", ConnectionKey);
					xmlElement.SetAttribute("ConnectionKeyOrConnectionString", current.ConnectionKeyOrConnectionString);
					xmlElement.SetAttribute("LogModuleType", current.LogModuleType);
					xmlElement.SetAttribute("UIProjectName", current.UIProjectName);
					xmlElement.SetAttribute("UIProjectPath", current.UIProjectPath);
					xmlElement.SetAttribute("EditUrl", current.EditUrl);
					xmlElement.SetAttribute("ListUrl", current.ListUrl);
					xmlElement.SetAttribute("ModuleID", current.ModuleID);
					foreach (ColumnRuleSchema current2 in from s in current.Columns
					where s.IsXmlField
					select s)
					{
						XmlElement xmlElement2 = this.CodeConfig.CreateElement("Field");
						xmlElement2.SetAttribute("FieldName", current2.FieldName);
						xmlElement2.SetAttribute("FieldTitle", current2.FieldTitle);
						xmlElement2.SetAttribute("IsCheck", current2.IsCheck.ToString());
						xmlElement2.SetAttribute("ErrorMessage", current2.ErrorMessage);
						xmlElement.AppendChild(xmlElement2);
					}
					businessConfigNode.AppendChild(xmlElement);
				}
				else
				{
					XmlElement xmlElement = (XmlElement)businessNode;
					xmlElement.SetAttribute("EntityName", current.EntityName);
					xmlElement.SetAttribute("Description", current.Description);
					xmlElement.SetAttribute("IsMongoDB", current.IsMongoDB.ToString());
					xmlElement.SetAttribute("IsCheckFieldLength", current.IsCheckFieldLength.ToString());
					xmlElement.SetAttribute("ConnectionKey", ConnectionKey);
					xmlElement.SetAttribute("ConnectionKeyOrConnectionString", current.ConnectionKeyOrConnectionString);
					xmlElement.SetAttribute("LogModuleType", current.LogModuleType);
					xmlElement.SetAttribute("UIProjectName", current.UIProjectName);
					xmlElement.SetAttribute("UIProjectPath", current.UIProjectPath);
					xmlElement.SetAttribute("EditUrl", current.EditUrl);
					xmlElement.SetAttribute("ListUrl", current.ListUrl);
					xmlElement.SetAttribute("ModuleID", current.ModuleID);
					foreach (ColumnRuleSchema current2 in from s in current.Columns
					where s.IsXmlField
					select s)
					{
						XmlNode xmlNode = this.CodeConfig.SelectSingleNode(string.Concat(new string[]
						{
							"//BusinessConfig/Business[@TableName='",
							current.TableName,
							"']/Field[@FieldName='",
							current2.FieldName,
							"']"
						}));
						if (xmlNode == null)
						{
							XmlElement xmlElement2 = this.CodeConfig.CreateElement("Field");
							xmlElement2.SetAttribute("FieldName", current2.FieldName);
							xmlElement2.SetAttribute("FieldTitle", current2.FieldTitle);
							xmlElement2.SetAttribute("IsCheck", current2.IsCheck.ToString());
							xmlElement2.SetAttribute("ErrorMessage", current2.ErrorMessage);
							xmlElement.AppendChild(xmlElement2);
						}
						else
						{
							XmlElement xmlElement2 = (XmlElement)xmlNode;
							xmlElement2.SetAttribute("FieldTitle", current2.FieldTitle);
							xmlElement2.SetAttribute("IsCheck", current2.IsCheck.ToString());
							xmlElement2.SetAttribute("ErrorMessage", current2.ErrorMessage);
						}
					}
				}
			}
			this.Save();
		}
	}
}
