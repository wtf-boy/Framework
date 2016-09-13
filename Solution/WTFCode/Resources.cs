using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WTFCode.CodeRule
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
                    ResourceManager resourceManager = new ResourceManager("WTFCode.CodeRule.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static string EditTableHeader
		{
			get
			{
				return Resources.ResourceManager.GetString("EditTableHeader", Resources.resourceCulture);
			}
		}

		internal static string GetBackCommand
		{
			get
			{
				return Resources.ResourceManager.GetString("GetBackCommand", Resources.resourceCulture);
			}
		}

		internal static string GetBizRule
		{
			get
			{
				return Resources.ResourceManager.GetString("GetBizRule", Resources.resourceCulture);
			}
		}

		internal static string GetBoundField
		{
			get
			{
				return Resources.ResourceManager.GetString("GetBoundField", Resources.resourceCulture);
			}
		}

		internal static string GetCommand
		{
			get
			{
				return Resources.ResourceManager.GetString("GetCommand", Resources.resourceCulture);
			}
		}

		internal static string GetCommandRnderPage
		{
			get
			{
				return Resources.ResourceManager.GetString("GetCommandRnderPage", Resources.resourceCulture);
			}
		}

		internal static string GetCondition
		{
			get
			{
				return Resources.ResourceManager.GetString("GetCondition", Resources.resourceCulture);
			}
		}

		internal static string GetConditionT
		{
			get
			{
				return Resources.ResourceManager.GetString("GetConditionT", Resources.resourceCulture);
			}
		}

		internal static string GetCreateCommand
		{
			get
			{
				return Resources.ResourceManager.GetString("GetCreateCommand", Resources.resourceCulture);
			}
		}

		internal static string GetDalAccess
		{
			get
			{
				return Resources.ResourceManager.GetString("GetDalAccess", Resources.resourceCulture);
			}
		}

		internal static string GetDbSet
		{
			get
			{
				return Resources.ResourceManager.GetString("GetDbSet", Resources.resourceCulture);
			}
		}

		internal static string GetDeleteCondition
		{
			get
			{
				return Resources.ResourceManager.GetString("GetDeleteCondition", Resources.resourceCulture);
			}
		}

		internal static string GetDeleteKey
		{
			get
			{
				return Resources.ResourceManager.GetString("GetDeleteKey", Resources.resourceCulture);
			}
		}

		internal static string GetDeleteT
		{
			get
			{
				return Resources.ResourceManager.GetString("GetDeleteT", Resources.resourceCulture);
			}
		}

		internal static string GetDeleteTEmpty
		{
			get
			{
				return Resources.ResourceManager.GetString("GetDeleteTEmpty", Resources.resourceCulture);
			}
		}

		internal static string GetEntitys
		{
			get
			{
				return Resources.ResourceManager.GetString("GetEntitys", Resources.resourceCulture);
			}
		}

		internal static string GetInsertDbSet
		{
			get
			{
				return Resources.ResourceManager.GetString("GetInsertDbSet", Resources.resourceCulture);
			}
		}

		internal static string GetInsertIdentity
		{
			get
			{
				return Resources.ResourceManager.GetString("GetInsertIdentity", Resources.resourceCulture);
			}
		}

		internal static string GetInsertNoIdentity
		{
			get
			{
				return Resources.ResourceManager.GetString("GetInsertNoIdentity", Resources.resourceCulture);
			}
		}

		internal static string GetInsertObject
		{
			get
			{
				return Resources.ResourceManager.GetString("GetInsertObject", Resources.resourceCulture);
			}
		}

		internal static string GetItemCommand
		{
			get
			{
				return Resources.ResourceManager.GetString("GetItemCommand", Resources.resourceCulture);
			}
		}

		internal static string GetItemRedirect
		{
			get
			{
				return Resources.ResourceManager.GetString("GetItemRedirect", Resources.resourceCulture);
			}
		}

		internal static string GetItemRedirectState
		{
			get
			{
				return Resources.ResourceManager.GetString("GetItemRedirectState", Resources.resourceCulture);
			}
		}

		internal static string GetJavaDal
		{
			get
			{
				return Resources.ResourceManager.GetString("GetJavaDal", Resources.resourceCulture);
			}
		}

		internal static string GetMyGridView
		{
			get
			{
				return Resources.ResourceManager.GetString("GetMyGridView", Resources.resourceCulture);
			}
		}

		internal static string GetObjectQuery
		{
			get
			{
				return Resources.ResourceManager.GetString("GetObjectQuery", Resources.resourceCulture);
			}
		}

		internal static string GetOperateField
		{
			get
			{
				return Resources.ResourceManager.GetString("GetOperateField", Resources.resourceCulture);
			}
		}

		internal static string GetQueryDate
		{
			get
			{
				return Resources.ResourceManager.GetString("GetQueryDate", Resources.resourceCulture);
			}
		}

		internal static string GetQueryDrop
		{
			get
			{
				return Resources.ResourceManager.GetString("GetQueryDrop", Resources.resourceCulture);
			}
		}

		internal static string GetQueryText
		{
			get
			{
				return Resources.ResourceManager.GetString("GetQueryText", Resources.resourceCulture);
			}
		}

		internal static string GetRenderPage
		{
			get
			{
				return Resources.ResourceManager.GetString("GetRenderPage", Resources.resourceCulture);
			}
		}

		internal static string GetRenderPageSql
		{
			get
			{
				return Resources.ResourceManager.GetString("GetRenderPageSql", Resources.resourceCulture);
			}
		}

		internal static string GetRenderPageT
		{
			get
			{
				return Resources.ResourceManager.GetString("GetRenderPageT", Resources.resourceCulture);
			}
		}

		internal static string GetRowCommand
		{
			get
			{
				return Resources.ResourceManager.GetString("GetRowCommand", Resources.resourceCulture);
			}
		}

		internal static string GetRowRedirect
		{
			get
			{
				return Resources.ResourceManager.GetString("GetRowRedirect", Resources.resourceCulture);
			}
		}

		internal static string GetRowRedirectState
		{
			get
			{
				return Resources.ResourceManager.GetString("GetRowRedirectState", Resources.resourceCulture);
			}
		}

		internal static string GetSearchCondition
		{
			get
			{
				return Resources.ResourceManager.GetString("GetSearchCondition", Resources.resourceCulture);
			}
		}

		internal static string GetSortExpression
		{
			get
			{
				return Resources.ResourceManager.GetString("GetSortExpression", Resources.resourceCulture);
			}
		}

		internal static string GetSummary
		{
			get
			{
				return Resources.ResourceManager.GetString("GetSummary", Resources.resourceCulture);
			}
		}

		internal static string GetTableColumns
		{
			get
			{
				return Resources.ResourceManager.GetString("GetTableColumns", Resources.resourceCulture);
			}
		}

		internal static string GetTableForeign
		{
			get
			{
				return Resources.ResourceManager.GetString("GetTableForeign", Resources.resourceCulture);
			}
		}

		internal static string GetTables
		{
			get
			{
				return Resources.ResourceManager.GetString("GetTables", Resources.resourceCulture);
			}
		}

		internal static string GetTemplateField
		{
			get
			{
				return Resources.ResourceManager.GetString("GetTemplateField", Resources.resourceCulture);
			}
		}

		internal static string GetToEntityValue
		{
			get
			{
				return Resources.ResourceManager.GetString("GetToEntityValue", Resources.resourceCulture);
			}
		}

		internal static string GetUpdateObject
		{
			get
			{
				return Resources.ResourceManager.GetString("GetUpdateObject", Resources.resourceCulture);
			}
		}

		internal static string GetWidth
		{
			get
			{
				return Resources.ResourceManager.GetString("GetWidth", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}
