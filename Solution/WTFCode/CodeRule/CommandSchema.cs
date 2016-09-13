using WTFCode.CodeRule;
using System;

namespace WTF.CodeRule
{
	public class CommandSchema
	{
		public string ModuleName
		{
			get;
			set;
		}

		public string CommandName
		{
			get;
			set;
		}

		public string ProcessType
		{
			get;
			set;
		}

		public string RedirectUrl
		{
			get;
			set;
		}

		public bool IsTop
		{
			get;
			set;
		}

		public bool IsList
		{
			get;
			set;
		}

		public bool IsBotom
		{
			get;
			set;
		}

		public int SortIndex
		{
			get;
			set;
		}

		public string ToRowItemCode(string simpTableName, string ruleName, string PrimaryKey, string ForeignKey)
		{
			string result;
			if (this.CommandName == "Create")
			{
				result = string.Format(Resources.GetCreateCommand, this.CommandName, this.RedirectUrl, string.IsNullOrEmpty(ForeignKey) ? "\"" : ("?" + ForeignKey + "=\"+" + ForeignKey));
			}
			else if (this.CommandName == "Remove")
			{
				result = string.Format(Resources.GetCommandRnderPage, this.CommandName, string.Format("obj{0}.Delete{1}ByKey({2});", ruleName, simpTableName, "e.CommandArgument.ToString()"));
			}
			else if (this.CommandName == "Back")
			{
				result = string.Format(Resources.GetBackCommand, this.CommandName, this.RedirectUrl, string.IsNullOrEmpty(ForeignKey) ? "\"" : ("?" + ForeignKey + "=\"+" + ForeignKey));
			}
			else if (this.ProcessType == "Redirect")
			{
				result = string.Format(Resources.GetRowRedirect, new object[]
				{
					this.CommandName,
					this.RedirectUrl,
					PrimaryKey,
					string.IsNullOrEmpty(ForeignKey) ? "" : (ForeignKey + "=\"+" + ForeignKey + "+\"&")
				});
			}
			else if (this.ProcessType == "RedirectState")
			{
				result = string.Format(Resources.GetRowRedirectState, new object[]
				{
					this.CommandName,
					this.RedirectUrl,
					PrimaryKey,
					string.IsNullOrEmpty(ForeignKey) ? "" : (ForeignKey + "=\"+" + ForeignKey + "+\"&")
				});
			}
			else
			{
				result = string.Format(Resources.GetCommandRnderPage, this.CommandName, "");
			}
			return result;
		}

		public string ToRowItemCodeSql(string ruleName, string PrimaryKey, string ForeignKey)
		{
			string result;
			if (this.CommandName == "Create")
			{
				result = string.Format(Resources.GetCreateCommand, this.CommandName, this.RedirectUrl, string.IsNullOrEmpty(ForeignKey) ? "\"" : ("?" + ForeignKey + "=\"+" + ForeignKey));
			}
			else if (this.CommandName == "Remove")
			{
				result = string.Format(Resources.GetCommandRnderPage, this.CommandName, string.Format("obj{0}.DeleteIDString({1});", ruleName, "e.CommandArgument.ToString()"));
			}
			else if (this.CommandName == "Back")
			{
				result = string.Format(Resources.GetBackCommand, this.CommandName, this.RedirectUrl, string.IsNullOrEmpty(ForeignKey) ? "\"" : ("?" + ForeignKey + "=\"+" + ForeignKey));
			}
			else if (this.ProcessType == "Redirect")
			{
				result = string.Format(Resources.GetRowRedirect, new object[]
				{
					this.CommandName,
					this.RedirectUrl,
					PrimaryKey,
					string.IsNullOrEmpty(ForeignKey) ? "" : (ForeignKey + "=\"+" + ForeignKey + "+\"&")
				});
			}
			else if (this.ProcessType == "RedirectState")
			{
				result = string.Format(Resources.GetRowRedirectState, new object[]
				{
					this.CommandName,
					this.RedirectUrl,
					PrimaryKey,
					string.IsNullOrEmpty(ForeignKey) ? "" : (ForeignKey + "=\"+" + ForeignKey + "+\"&")
				});
			}
			else
			{
				result = string.Format(Resources.GetCommandRnderPage, this.CommandName, "");
			}
			return result;
		}

		public string ToItemCode(string simpTableName, string ruleName, string PrimaryKey, string ForeignKey)
		{
			string result;
			if (this.CommandName == "Create")
			{
				result = string.Format(Resources.GetCreateCommand, this.CommandName, this.RedirectUrl, string.IsNullOrEmpty(ForeignKey) ? "\"" : ("?" + ForeignKey + "=\"+" + ForeignKey));
			}
			else if (this.CommandName == "Remove")
			{
				result = string.Format(Resources.GetCommandRnderPage, this.CommandName, string.Format("obj{0}.Delete{1}ByKey({2});", ruleName, simpTableName, "gdvContent.SelectedRowDataKeys"));
			}
			else if (this.CommandName == "Back")
			{
				result = string.Format(Resources.GetBackCommand, this.CommandName, this.RedirectUrl, string.IsNullOrEmpty(ForeignKey) ? "\"" : ("?" + ForeignKey + "=\"+" + ForeignKey));
			}
			else if (this.CommandName == "Search")
			{
				result = Resources.GetSearchCondition;
			}
			else if (this.ProcessType == "Redirect")
			{
				result = string.Format(Resources.GetItemRedirect, new object[]
				{
					this.CommandName,
					this.RedirectUrl,
					PrimaryKey,
					string.IsNullOrEmpty(ForeignKey) ? "" : (ForeignKey + "=\"+" + ForeignKey + "+\"&")
				});
			}
			else if (this.ProcessType == "RedirectState")
			{
				result = string.Format(Resources.GetItemRedirectState, new object[]
				{
					this.CommandName,
					this.RedirectUrl,
					PrimaryKey,
					string.IsNullOrEmpty(ForeignKey) ? "" : (ForeignKey + "=\"+" + ForeignKey + "+\"&")
				});
			}
			else
			{
				result = string.Format(Resources.GetCommandRnderPage, this.CommandName, "");
			}
			return result;
		}

		public string ToItemCodeSql(string ruleName, string PrimaryKey, string ForeignKey)
		{
			string result;
			if (this.CommandName == "Create")
			{
				result = string.Format(Resources.GetCreateCommand, this.CommandName, this.RedirectUrl, string.IsNullOrEmpty(ForeignKey) ? "\"" : ("?" + ForeignKey + "=\"+" + ForeignKey));
			}
			else if (this.CommandName == "Remove")
			{
				result = string.Format(Resources.GetCommandRnderPage, this.CommandName, string.Format("obj{0}.DeleteIDString({1});", ruleName, "gdvContent.SelectedRowDataKeys"));
			}
			else if (this.CommandName == "Back")
			{
				result = string.Format(Resources.GetBackCommand, this.CommandName, this.RedirectUrl, string.IsNullOrEmpty(ForeignKey) ? "\"" : ("?" + ForeignKey + "=\"+" + ForeignKey));
			}
			else if (this.CommandName == "Search")
			{
				result = Resources.GetSearchCondition;
			}
			else if (this.ProcessType == "Redirect")
			{
				result = string.Format(Resources.GetItemRedirect, new object[]
				{
					this.CommandName,
					this.RedirectUrl,
					PrimaryKey,
					string.IsNullOrEmpty(ForeignKey) ? "" : (ForeignKey + "=\"+" + ForeignKey + "+\"&")
				});
			}
			else if (this.ProcessType == "RedirectState")
			{
				result = string.Format(Resources.GetItemRedirectState, new object[]
				{
					this.CommandName,
					this.RedirectUrl,
					PrimaryKey,
					string.IsNullOrEmpty(ForeignKey) ? "" : (ForeignKey + "=\"+" + ForeignKey + "+\"&")
				});
			}
			else
			{
				result = string.Format(Resources.GetCommandRnderPage, this.CommandName, "");
			}
			return result;
		}
	}
}
