using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WTFCode
{
	public static class Common
	{
		public static void ShowCodeForm(string code)
		{
			ShowCodeForm showCodeForm = new ShowCodeForm(code);
			showCodeForm.Show();
		}

		public static string ReadAttribute(this XmlNode objXmlNode, string AttributeName, string defaultValue = "")
		{
			XmlAttribute xmlAttribute = objXmlNode.Attributes[AttributeName];
			string result;
			if (xmlAttribute != null)
			{
				result = xmlAttribute.Value;
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		public static string ReadCell(this DataGridViewRow objDataGridViewRow, string CellName, string defaultValue = "")
		{
			DataGridViewCell dataGridViewCell = objDataGridViewRow.Cells[CellName];
			string result;
			if (dataGridViewCell == null)
			{
				result = defaultValue;
			}
			else
			{
				object value = dataGridViewCell.Value;
				if (value == null)
				{
					result = defaultValue;
				}
				else
				{
					string text = value.ToString();
					if (string.IsNullOrEmpty(text))
					{
						result = defaultValue;
					}
					else
					{
						result = text;
					}
				}
			}
			return result;
		}

		public static void WriteCode(string filePath, string fileName, string ruleCode, bool isPreviewCode = false)
		{
			if (isPreviewCode)
			{
				Common.ShowCodeForm(ruleCode);
			}
			else if (File.Exists(filePath))
			{
				if (MessageBox.Show("确定要覆盖" + fileName + "文件吗?此操作不可恢复！", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					File.WriteAllText(filePath, ruleCode, Encoding.UTF8);
				}
				else
				{
					Common.ShowCodeForm(ruleCode);
				}
			}
			else
			{
				FileStream fileStream = File.Create(filePath);
				fileStream.Close();
				File.WriteAllText(filePath, ruleCode, Encoding.UTF8);
				MessageBox.Show(fileName + "生成成功");
			}
		}

		public static string GetCSCode(string CodeCode, string ProjectName, string Inherits)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("using System;");
			stringBuilder.AppendLine("using System.Collections.Generic;");
			stringBuilder.AppendLine("using System.Linq;");
			stringBuilder.AppendLine("using System.Web;");
			stringBuilder.AppendLine("using System.Web.UI;");
			stringBuilder.AppendLine("using System.Web.UI.WebControls;");
			stringBuilder.AppendLine("using WTF.Framework;");
			stringBuilder.AppendLine("using " + ProjectName + ".Business;");
			stringBuilder.AppendLine("using " + ProjectName + ".DataEntity;");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine(string.Format("public partial class {0}:SupportPageBaseSql", Inherits));
			stringBuilder.AppendLine("{");
			stringBuilder.AppendLine(CodeCode);
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		public static string GetUiPath(string UIProjectPath, SelectFileInfo objSelectFileInfo)
		{
			string text;
			if (File.Exists(UIProjectPath))
			{
				text = UIProjectPath;
			}
			else
			{
				text = Path.Combine(objSelectFileInfo.ProjectPath.Replace(objSelectFileInfo.ProjectName, ""), UIProjectPath);
			}
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		public static string GetConfigConnectionString(string ConfigPath)
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
			{
				ExeConfigFilename = ConfigPath
			}, ConfigurationUserLevel.None);
			return configuration.ConnectionStrings.ConnectionStrings["WTFConnectionString"].ToString();
		}
	}
}
