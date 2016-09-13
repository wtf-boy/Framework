using System;

namespace WTFCode
{
	public class BusinessNodeInfo
	{
		public string TableName
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

		public string LogModuleType
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

		public BusinessNodeInfo()
		{
			this.UIProjectName = "";
			this.UIProjectPath = "";
			this.IsMongoDB = false;
			this.IsCheckFieldLength = true;
			this.ConnectionKeyOrConnectionString = "";
		}
	}
}
