using EnvDTE;
using EnvDTE80;
using System;
using System.IO;
using System.Windows.Forms;

namespace WTFCode
{
	public class SelectFileInfo
	{
		public string ProjectName
		{
			get;
			set;
		}

		public string ProjectPath
		{
			get;
			set;
		}

		public string ProjectItemPath
		{
			get;
			set;
		}

		public string ItemFileName
		{
			get;
			set;
		}

		public string CodeConfigPath
		{
			get;
			set;
		}

		public string WTFConfigPath
		{
			get;
			set;
		}

		public DTE2 ApplicationObject
		{
			get;
			set;
		}

		public Project Project
		{
			get;
			set;
		}

		public ProjectItem ProjectItem
		{
			get;
			set;
		}

		public bool IsExistsCodeConfig()
		{
			bool result;
			if (!File.Exists(this.CodeConfigPath))
			{
				DialogResult dialogResult = MessageBox.Show("CodeConfig.xml不存在，请先配置连接，是否马上配置?", "", MessageBoxButtons.OKCancel);
				if (dialogResult == DialogResult.OK)
				{
					ConnectConfigFrom connectConfigFrom = new ConnectConfigFrom(this);
					connectConfigFrom.Show();
				}
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		public bool IsExistsWTFConfig()
		{
			bool result;
			if (!File.Exists(this.WTFConfigPath))
			{
                MessageBox.Show("插件根目录WTFCode.config不存在,配置此文件");
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		public string GetItemPathEntityName()
		{
			string result = string.Empty;
			if (this.ProjectItemPath.IndexOf("DataEntity") > 0)
			{
				result = this.ItemFileName;
			}
			else if (this.ProjectItemPath.IndexOf("DataAccess") > 0)
			{
				result = this.ItemFileName.Substring(2);
			}
			else if (this.ProjectItemPath.IndexOf("Business") > 0)
			{
				result = this.ItemFileName.Substring(3);
			}
			return result;
		}
	}
}
