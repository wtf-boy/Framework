using WTF.DataConfig.Entity;
using WTF.Framework;
using WTF.Logging;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace WTF.DataConfig
{
    public class ParameterRule
    {
        private ParameterEntities objCurrentEntities;

        public ParameterEntities CurrentEntities
        {
            get
            {
                if (this.objCurrentEntities == null)
                {
                    this.objCurrentEntities = new ParameterEntities(EntitiesHelper.GetConnectionString<ParameterEntities>());
                }
                return this.objCurrentEntities;
            }
        }

        public ObjectQuery<Sys_ParameterType> Sys_ParameterType
        {
            get
            {
                return this.CurrentEntities.sys_parametertype;
            }
        }

        public ObjectQuery<Sys_Parameter> Sys_Parameter
        {
            get
            {
                return this.CurrentEntities.sys_parameter;
            }
        }

        public void SaveChanges()
        {
            this.CurrentEntities.SaveChanges();
        }

        public void InsertParameterType(Sys_ParameterType objParameterType)
        {
            objParameterType.ParameterTypeName.CheckIsNull("请输入参数类型名称", LogModuleType.ParameterLog);
            objParameterType.ParameterTypeCode.CheckIsNull("请输入参数类型代码", LogModuleType.ParameterLog);
            SysAssert.CheckCondition(this.CurrentEntities.sys_parametertype.Any((Sys_ParameterType p) => p.ParameterTypeCode == objParameterType.ParameterTypeCode), "输入的参数类型代码已经存在", LogModuleType.ParameterLog);
            this.CurrentEntities.AddTosys_parametertype(objParameterType);
            this.CurrentEntities.SaveChanges();
        }

        public void DeleteParameterType(string parameterTypeIDSiring)
        {
            //ToDo:<Sys_ParameterType> 泛型参数待确认
            this.CurrentEntities.DeleteDataPrimaryKey<Sys_ParameterType>(parameterTypeIDSiring);
        }

        public void InsertParameter(Sys_Parameter objParameter)
        {
            objParameter.ParameterCode.CheckIsNull("请输入参数代码", LogModuleType.ParameterLog);
            if (objParameter.ParameterCodeID == 0)
            {
                Sys_Parameter sys_Parameter = (from s in this.CurrentEntities.sys_parameter
                                               where s.ParameterTypeID == objParameter.ParameterTypeID
                                               orderby s.ParameterCodeID descending
                                               select s).FirstOrDefault<Sys_Parameter>();
                if (sys_Parameter.IsNull())
                {
                    objParameter.ParameterCodeID = 1;
                }
                else
                {
                    objParameter.ParameterCodeID = sys_Parameter.ParameterCodeID + 1;
                }
            }
            objParameter.ParameterName.CheckIsNull("请输入参数名称", LogModuleType.ParameterLog);
            this.CurrentEntities.AddTosys_parameter(objParameter);
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateParameter(int parameterTypeID)
        {
            Sys_ParameterType sys_ParameterType = this.CurrentEntities.sys_parametertype.First((Sys_ParameterType p) => p.ParameterTypeID == parameterTypeID);
            sys_ParameterType.TypeName.CheckIsNull("请输入类名才能更新", LogModuleType.ParameterLog);
            IList<EnumParameter> enumMembers = sys_ParameterType.TypeName.GetEnumMembers(sys_ParameterType.AssemblyName);
            this.CurrentEntities.sys_parameter.DeleteData((Sys_Parameter s) => s.ParameterTypeID == parameterTypeID);
            int num = 0;
            foreach (EnumParameter current in enumMembers)
            {
                this.CurrentEntities.AddTosys_parameter(new Sys_Parameter
                {
                    ParameterTypeID = parameterTypeID,
                    ParameterName = current.Description,
                    ParameterCode = current.Key,
                    ParameterCodeID = current.Value,
                    Remark = current.Description,
                    SortIndex = num++,
                    IsEnable = true
                });
            }
            this.CurrentEntities.SaveChanges();
        }

        public void DeleteParameter(string parameterIDString)
        {
            this.CurrentEntities.sys_parameter.DeleteDataPrimaryKey(parameterIDString);
        }

        public List<Sys_Parameter> GetParameter(string enumTypeCode, bool isCache = true)
        {
            if (!isCache)
            {
                return (from s in this.CurrentEntities.sys_parameter
                        where s.IsEnable == true && s.Sys_ParameterType.ParameterTypeCode == enumTypeCode
                        orderby s.SortIndex
                        select s).ToList<Sys_Parameter>();
            }
            List<Sys_Parameter> list = (List<Sys_Parameter>)CacheHelper.GetFromCache(CacheType.Parameter.ToString(), enumTypeCode);
            if (list.IsNull<Sys_Parameter>())
            {
                list = (from s in this.CurrentEntities.sys_parameter
                        where s.IsEnable == true && s.Sys_ParameterType.ParameterTypeCode == enumTypeCode
                        orderby s.SortIndex
                        select s).ToList<Sys_Parameter>();
                list.AddToCache(CacheType.Parameter.ToString(), enumTypeCode, CacheFactor.Hour, 1);
            }
            return list;
        }

        public void ParameterSort(string ParameterIDstring)
        {
            ParameterIDstring.CheckIsNull("请选择要排序的", LogModuleType.ParameterLog);
            List<int> objList = ParameterIDstring.ConvertListInt();
            foreach (Sys_Parameter current in from s in this.Sys_Parameter
                                              where objList.Contains(s.ParameterID)
                                              select s)
            {
                current.SortIndex = objList.IndexOf(current.ParameterID) + 1;
            }
            this.CurrentEntities.SaveChanges();
        }

        public Sys_Parameter GetParameter(string parameterTypeCode, int parameterCodeID)
        {
            Sys_Parameter sys_Parameter = this.CurrentEntities.sys_parameter.FirstOrDefault((Sys_Parameter s) => s.Sys_ParameterType.ParameterTypeCode == parameterTypeCode && s.ParameterCodeID == parameterCodeID);
            if (sys_Parameter.IsNull())
            {
                SysAssert.ArgumentAssert<LogModuleType>(string.Concat(new object[]
				{
					"未设置枚举参数",
					parameterTypeCode,
					"-",
					parameterCodeID,
					"值"
				}), LogModuleType.Framework);
            }
            return sys_Parameter;
        }
    }
}
