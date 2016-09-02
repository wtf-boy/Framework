using WTF.DataConfig.Entity;
using WTF.Framework;
using WTF.Logging;
using System;
using System.Data.Objects;
using System.Linq;

namespace WTF.DataConfig
{
	public class DataTemplateRule
	{
		private DataTemplateEntities objCurrentEntities;

		public DataTemplateEntities CurrentEntities
		{
			get
			{
				if (this.objCurrentEntities == null)
				{
					this.objCurrentEntities = new DataTemplateEntities(EntitiesHelper.GetConnectionString<DataTemplateEntities>());
				}
				return this.objCurrentEntities;
			}
		}

		public ObjectQuery<Sys_DataTemplateType> Sys_DataTemplateType
		{
			get
			{
				return this.CurrentEntities.sys_datatemplatetype;
			}
		}

		public ObjectQuery<Sys_DataTemplate> Sys_DataTemplate
		{
			get
			{
				return this.CurrentEntities.sys_datatemplate;
			}
		}

		public void SaveChanges()
		{
			this.CurrentEntities.SaveChanges();
		}

		public void InsertDataTemplateType(Sys_DataTemplateType objDataTemplateType)
		{
			objDataTemplateType.DataTemplateTypeCode.CheckIsNull("请输入数据模板类型代码", LogModuleType.ParameterLog);
			objDataTemplateType.DataTemplateTypeName.CheckIsNull("请输入数据模板类型名称", LogModuleType.ParameterLog);
			SysAssert.CheckCondition(this.Sys_DataTemplateType.Any((Sys_DataTemplateType s) => s.DataTemplateTypeCode == objDataTemplateType.DataTemplateTypeCode), "输入的代码已经存在", LogModuleType.ParameterLog);
			this.CurrentEntities.AddTosys_datatemplatetype(objDataTemplateType);
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteDataTemplateType(string dataTemplateTypeIDString)
		{
			this.CurrentEntities.sys_datatemplatetype.DeleteDataPrimaryKey(dataTemplateTypeIDString);
		}

		public void InsertDataTemplate(Sys_DataTemplate objDataTemplate)
		{
			objDataTemplate.TemplateCode.CheckIsNull("请输入数据模板代码", LogModuleType.ParameterLog);
			objDataTemplate.TemplateName.CheckIsNull("请输入数据模板名称", LogModuleType.ParameterLog);
			objDataTemplate.TemplateContent.CheckIsNull("请输入数据模板内容", LogModuleType.ParameterLog);
			this.CurrentEntities.AddTosys_datatemplate(objDataTemplate);
			this.CurrentEntities.SaveChanges();
		}

		public void DeleteDataTemplate(string dataTemplateIDString)
		{
			this.CurrentEntities.sys_datatemplate.DeleteDataPrimaryKey(dataTemplateIDString);
		}

		public string GetDataTemplate(string dataTemplateTypeCode, string templateCode)
		{
			Sys_DataTemplate sys_DataTemplate = this.CurrentEntities.sys_datatemplate.FirstOrDefault((Sys_DataTemplate s) => s.TemplateCode == templateCode && s.Sys_DataTemplateType.DataTemplateTypeCode == dataTemplateTypeCode);
			if (sys_DataTemplate.IsNull())
			{
				SysAssert.ArgumentAssert<LogModuleType>(string.Concat(new string[]
				{
					"未设置数据模板",
					dataTemplateTypeCode,
					"-",
					templateCode,
					"值"
				}), LogModuleType.Framework);
			}
			return sys_DataTemplate.TemplateContent;
		}
	}
}
