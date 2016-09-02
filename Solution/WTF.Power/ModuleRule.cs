namespace WTF.Power
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Power.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Xml;

    public class ModuleRule
    {
        private ModuleEntities objCurrentEntities;

        public bool CheckIsPowerData(string moduleTypeID, string pageName)
        {
            return this.CurrentEntities.sys_module.Any<WTF.Power.Entity.Sys_Module>(s => (((s.ModuleTypeID == moduleTypeID) && (s.ModuleCode == pageName)) && s.IsCheckPowerData));
        }

        private void CreateChildModeuleManageXmlElement(XmlDocument xmlDocSource, string ModuleID, XmlElement objXmlElement, List<WTF.Power.Entity.Sys_Module> objSys_ModuleList)
        {
            foreach (WTF.Power.Entity.Sys_Module module in from p in objSys_ModuleList
                where p.ParentModuleID == ModuleID
                orderby p.SortIndex
                select p)
            {
                XmlElement newChild = xmlDocSource.CreateElement("Module");
                newChild.SetAttribute("ModuleID", module.ModuleID.ToString());
                newChild.SetAttribute("ModuleName", module.ModuleName);
                if (module.IsMvc)
                {
                    newChild.SetAttribute("NavigateUrl", string.Format("~/ServiceLayer/Module/ModuleMvcInfo.aspx?ModuleID={0}", module.ModuleID.ToString()).EncryptModuleQuery());
                }
                else
                {
                    newChild.SetAttribute("NavigateUrl", string.Format("~/ServiceLayer/Module/ModuleInfo.aspx?ModuleID={0}", module.ModuleID.ToString()).EncryptModuleQuery());
                }
                objXmlElement.AppendChild(newChild);
                this.CreateChildModeuleManageXmlElement(xmlDocSource, module.ModuleID, newChild, objSys_ModuleList);
            }
        }

        private void CreateChildModeuleUrlXmlElement(XmlDocument xmlDocSource, string ModuleID, XmlElement objXmlElement, List<WTF.Power.Entity.Sys_Module> objSys_ModuleList, List<WTF.Power.Entity.Sys_Module> objDataSys_ModuleList, string urlFormant)
        {
            using (IEnumerator<WTF.Power.Entity.Sys_Module> enumerator = (from p in objSys_ModuleList
                where p.ParentModuleID == ModuleID
                orderby p.SortIndex
                select p).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Func<WTF.Power.Entity.Sys_Module, bool> predicate = null;
                    WTF.Power.Entity.Sys_Module objModule = enumerator.Current;
                    XmlElement newChild = xmlDocSource.CreateElement("Module");
                    newChild.SetAttribute("ModuleID", objModule.ModuleID.ToString());
                    newChild.SetAttribute("ModuleName", objModule.ModuleName);
                    if (predicate == null)
                    {
                        predicate = s => s.ModuleID == objModule.ModuleID;
                    }
                    newChild.SetAttribute("NavigateUrl", objDataSys_ModuleList.Any<WTF.Power.Entity.Sys_Module>(predicate) ? string.Format(urlFormant, objModule.ModuleID.ToString()).EncryptModuleQuery() : "javascript:void();");
                    objXmlElement.AppendChild(newChild);
                    this.CreateChildModeuleUrlXmlElement(xmlDocSource, objModule.ModuleID, newChild, objSys_ModuleList, objDataSys_ModuleList, urlFormant);
                }
            }
        }

        private void CreateChildModeuleXmlElement(XmlDocument xmlDocSource, string ModuleID, XmlElement objXmlElement, List<WTF.Power.Entity.Sys_Module> objSys_ModuleList)
        {
            foreach (WTF.Power.Entity.Sys_Module module in from p in objSys_ModuleList
                where p.ParentModuleID == ModuleID
                orderby p.SortIndex
                select p)
            {
                XmlElement newChild = xmlDocSource.CreateElement("Module");
                newChild.SetAttribute("ModuleID", module.ModuleID.ToString());
                newChild.SetAttribute("ModuleName", module.ModuleName);
                newChild.SetAttribute("NavigateUrl", module.ModuleID.ToString());
                objXmlElement.AppendChild(newChild);
                this.CreateChildModeuleXmlElement(xmlDocSource, module.ModuleID, newChild, objSys_ModuleList);
            }
        }

        public void DeleteModule(string moduleID)
        {
            this.CurrentEntities.DeleteModule(moduleID);
        }

        public void DeleteModuleCote(string condition)
        {
            this.CurrentEntities.sys_modulecote.DeleteData<WTF.Power.Entity.Sys_ModuleCote>(condition, new ObjectParameter[0]);
        }

        public void DeleteModuleCoteByKey(string primaryKey)
        {
            this.CurrentEntities.sys_modulecote.DeleteDataPrimaryKey<WTF.Power.Entity.Sys_ModuleCote>(primaryKey);
        }

        public void DeleteModuleDataByKey(string primaryKey)
        {
            if (!primaryKey.IsNull())
            {
                this.CurrentEntities.sys_moduledata.DeleteDataPrimaryKey<WTF.Power.Entity.Sys_ModuleData>(primaryKey);
                new UserRule().DeleteRoleData("it.ModuleDataID in {" + primaryKey.ConvertStringID() + "}");
                this.CurrentEntities.sys_modulecheckdata.DeleteData<WTF.Power.Entity.Sys_ModuleCheckData>("it.ModuleDataID in {" + primaryKey.ConvertStringID() + "}", new ObjectParameter[0]);
            }
        }

        public void DeleteModuleType(string moduleTypeIDList)
        {
            foreach (WTF.Power.Entity.Sys_ModuleType type in this.CurrentEntities.sys_moduletype.Where("it.ModuleTypeID in {" + moduleTypeIDList.ConvertStringID() + "}", new ObjectParameter[0]))
            {
                this.CurrentEntities.DeleteObject(type);
            }
            this.CurrentEntities.SaveChanges();
            this.CurrentEntities.sys_moduledata.DeleteData<WTF.Power.Entity.Sys_ModuleData>("it.ModuleID in {" + moduleTypeIDList.ConvertStringID() + "}", new ObjectParameter[0]);
        }

        public List<WTF.Power.Entity.Sys_ModuleData> GetCheckPageFieldModuleData(string moduleTypeID, string pageName, string fieldName)
        {
            string format = "\r\n   set @ModuleID='';\r\nSELECT    Sys_Module.MODULEID into @ModuleID\r\n    from    Sys_Module\r\n     where   Sys_Module.ModuleTypeID='{0}' \r\n and Sys_Module.ModuleCode='{1}';\r\n\r\nselect  distinct  Sys_ModuleData.* from  Sys_ModuleData\r\n where  Sys_ModuleData.FieldName='{2}' and \r\n   (Sys_ModuleData.ModuleID=@ModuleID  or   Sys_ModuleData.ModuleDataID in (select  ModuleDataID from Sys_ModuleCheckData where  Sys_ModuleCheckData.ModuleID=@ModuleID));";
            return this.CurrentEntities.ExecuteStoreQuery<WTF.Power.Entity.Sys_ModuleData>(string.Format(format, moduleTypeID, pageName, fieldName), new object[0]).ToList<WTF.Power.Entity.Sys_ModuleData>();
        }

        public List<WTF.Power.Entity.Sys_ModuleData> GetCheckPageModuleData(string moduleTypeID, string pageName)
        {
            string format = " \r\n  set @ModuleID='';\r\nSELECT    Sys_Module.MODULEID into @ModuleID\r\n    from    Sys_Module\r\n     where   Sys_Module.ModuleTypeID='{0}' \r\n and Sys_Module.ModuleCode='{1}';\r\n\r\nselect   distinct Sys_ModuleData.* from  Sys_ModuleData\r\n where  \r\n   (Sys_ModuleData.ModuleID=@ModuleID\r\n     or   Sys_ModuleData.ModuleDataID in (select  ModuleDataID from Sys_ModuleCheckData where  Sys_ModuleCheckData.ModuleID=@ModuleID));";
            return this.CurrentEntities.ExecuteStoreQuery<WTF.Power.Entity.Sys_ModuleData>(string.Format(format, moduleTypeID, pageName), new object[0]).ToList<WTF.Power.Entity.Sys_ModuleData>();
        }

        public string GetModeuleCopyTreexXml(string ModuleID)
        {
            Func<WTF.Power.Entity.Sys_Module, bool> predicate = null;
            WTF.Power.Entity.Sys_Module objSys_Module = this.CurrentEntities.sys_module.First<WTF.Power.Entity.Sys_Module>(s => s.ModuleID == ModuleID);
            XmlDocument xmlDocSource = new XmlDocument();
            List<WTF.Power.Entity.Sys_ModuleType> list = this.Sys_ModuleType.Include("Sys_Module").ToList<WTF.Power.Entity.Sys_ModuleType>();
            XmlElement newChild = xmlDocSource.CreateElement("Module");
            newChild.SetAttribute("ModuleID", Guid.Empty.ToString());
            newChild.SetAttribute("ModuleName", "平台复制管理");
            newChild.SetAttribute("NavigateUrl", "");
            xmlDocSource.AppendChild(newChild);
            foreach (WTF.Power.Entity.Sys_ModuleType type in list)
            {
                XmlElement element2 = xmlDocSource.CreateElement("Module");
                element2.SetAttribute("ModuleID", type.ModuleTypeID.ToString());
                element2.SetAttribute("ModuleName", type.ModuleTypeName);
                element2.SetAttribute("NavigateUrl", "ModuleType");
                newChild.AppendChild(element2);
                if (predicate == null)
                {
                    predicate = s => !s.ModuleIDPath.StartsWith(objSys_Module.ModuleIDPath);
                }
                this.CreateChildModeuleXmlElement(xmlDocSource, type.ModuleTypeID, element2, type.Sys_Module.Where<WTF.Power.Entity.Sys_Module>(predicate).ToList<WTF.Power.Entity.Sys_Module>());
            }
            return xmlDocSource.InnerXml;
        }

        public string GetModeuleManageTreeXmlText()
        {
            XmlDocument xmlDocSource = new XmlDocument();
            List<WTF.Power.Entity.Sys_ModuleType> list = (from s in this.Sys_ModuleType.Include("Sys_Module")
                orderby s.ModuleTypeName descending
                select s).ToList<WTF.Power.Entity.Sys_ModuleType>();
            XmlElement newChild = xmlDocSource.CreateElement("Module");
            newChild.SetAttribute("ModuleID", Guid.Empty.ToString());
            newChild.SetAttribute("ModuleName", "平台模块分类管理");
            newChild.SetAttribute("NavigateUrl", string.Format("~/ServiceLayer/Module/ModuleTypeList.aspx", new object[0]));
            xmlDocSource.AppendChild(newChild);
            foreach (WTF.Power.Entity.Sys_ModuleType type in list)
            {
                XmlElement element2 = xmlDocSource.CreateElement("Module");
                element2.SetAttribute("ModuleID", type.ModuleTypeID.ToString());
                element2.SetAttribute("ModuleName", type.ModuleTypeName);
                element2.SetAttribute("NavigateUrl", string.Format("~/ServiceLayer/Module/ModuleTypeInfo.aspx?ModuleID={0}", type.ModuleTypeID.ToString()).EncryptModuleQuery());
                newChild.AppendChild(element2);
                this.CreateChildModeuleManageXmlElement(xmlDocSource, type.ModuleTypeID, element2, type.Sys_Module.ToList<WTF.Power.Entity.Sys_Module>());
            }
            return xmlDocSource.InnerXml;
        }

        public string GetModeuleMoveTreexXml(string ModuleID)
        {
            XmlDocument xmlDocSource = new XmlDocument();
            WTF.Power.Entity.Sys_Module objSys_Module = this.CurrentEntities.sys_module.First<WTF.Power.Entity.Sys_Module>(s => s.ModuleID == ModuleID);
            string str = objSys_Module.ModuleTypeID.ToString();
            WTF.Power.Entity.Sys_ModuleType type = this.CurrentEntities.sys_moduletype.Where("it.ModuleTypeID =='" + str + "'", new ObjectParameter[0]).Include("Sys_Module").First<WTF.Power.Entity.Sys_ModuleType>();
            XmlElement newChild = xmlDocSource.CreateElement("Module");
            newChild.SetAttribute("ModuleID", type.ModuleTypeID.ToString());
            newChild.SetAttribute("ModuleName", type.ModuleTypeName);
            newChild.SetAttribute("NavigateUrl", type.ModuleTypeID.ToString());
            xmlDocSource.AppendChild(newChild);
            List<WTF.Power.Entity.Sys_Module> list = (from s in type.Sys_Module
                where !s.ModuleIDPath.StartsWith(objSys_Module.ModuleIDPath)
                select s).ToList<WTF.Power.Entity.Sys_Module>();
            this.CreateChildModeuleXmlElement(xmlDocSource, type.ModuleTypeID, newChild, list);
            return xmlDocSource.InnerXml;
        }

        public string GetModeuleTypeDataTreexXmlText(string moduleTypeID, string RoleID)
        {
            string format = "RoleDataPower.aspx?ModuleID={0}&RoleID=" + RoleID.ToString();
            new UserRule();
            XmlDocument xmlDocSource = new XmlDocument();
            WTF.Power.Entity.Sys_ModuleType type = this.CurrentEntities.sys_moduletype.FirstOrDefault<WTF.Power.Entity.Sys_ModuleType>(s => s.ModuleTypeID == moduleTypeID);
            XmlElement newChild = xmlDocSource.CreateElement("Module");
            newChild.SetAttribute("ModuleID", type.ModuleTypeID.ToString());
            newChild.SetAttribute("ModuleName", type.ModuleTypeName);
            newChild.SetAttribute("NavigateUrl", string.Format(format, type.ModuleTypeID.ToString()).EncryptModuleQuery());
            xmlDocSource.AppendChild(newChild);
            string str2 = "";
            string commandText = string.Format("SELECT   Sys_Module.* FROM   Sys_ModuleData INNER JOIN  Sys_Module ON  Sys_ModuleData.ModuleID =Sys_Module.ModuleID INNER JOIN  Sys_RolePower on  Sys_Module.ModuleID=Sys_RolePower.ModuleID where  Sys_Module.IsCheckPowerData=1 \r\nand Sys_Module.ModuleTypeID='{0}' AND  Sys_RolePower.RoleID='{1}'", moduleTypeID.ToString(), RoleID.ToString());
            List<WTF.Power.Entity.Sys_Module> list = this.CurrentEntities.ExecuteStoreQuery<WTF.Power.Entity.Sys_Module>(commandText, new object[0]).ToList<WTF.Power.Entity.Sys_Module>();
            foreach (WTF.Power.Entity.Sys_Module module in list)
            {
                str2 = str2 + module.ModuleIDPath + ",";
            }
            string str4 = str2.TrimEndComma().ConvertListString().Distinct<string>().ToList<string>().ConvertListToString<string>();
            if (str4.IsNoNull())
            {
                this.CreateChildModeuleUrlXmlElement(xmlDocSource, type.ModuleTypeID, newChild, this.CurrentEntities.sys_module.WhereKey<WTF.Power.Entity.Sys_Module>(str4).ToList<WTF.Power.Entity.Sys_Module>(), list, format);
            }
            return xmlDocSource.InnerXml;
        }

        public IEnumerable<WTF.Power.Entity.Sys_Module> GetPowerFunctionModule(string moduleTypeID, string moduleCode, bool containChild)
        {
            UserRule rule = new UserRule();
            return this.CurrentEntities.GetPowerFunctionModuleByID(moduleTypeID, moduleCode, rule.CurrentUser.UserID, new bool?(containChild)).OfType<WTF.Power.Entity.Sys_Module>();
        }

        public bool GetPowerOperateButton(string moduleTypeID, string moduleCode, string userID, string commandName)
        {
            return (this.CurrentEntities.GetPowerOperateCommandByID(moduleTypeID, moduleCode, userID, commandName).First<int?>() > 0);
        }

        public bool GetPowerOperateButton(string moduleTypeID, string moduleCode, string userID, string coteModuleID, string coteID, string commandName)
        {
            return (this.CurrentEntities.GetPowerCoteOperateCommandByID(moduleTypeID, moduleCode, userID, coteModuleID, coteID, commandName).First<int?>() > 0);
        }

        public List<WTF.Power.Entity.Sys_Module> GetPowerOperateModule(string moduleTypeID, string moduleCode, string userID)
        {
            return this.CurrentEntities.GetPowerOperateModuleByID(moduleTypeID, moduleCode, userID, "").ToList<WTF.Power.Entity.Sys_Module>();
        }

        public List<WTF.Power.Entity.Sys_Module> GetPowerOperateModule(string moduleTypeID, string moduleCode, string userID, OperatePlaceType operatePlaceType)
        {
            int num = (int) operatePlaceType;
            return this.CurrentEntities.GetPowerOperateModuleByID(moduleTypeID, moduleCode, userID, num.ToString()).ToList<WTF.Power.Entity.Sys_Module>();
        }

        public List<WTF.Power.Entity.Sys_Module> GetPowerOperateModule(string moduleTypeID, string moduleCode, string userID, string coteModuleID, string coteID)
        {
            return this.CurrentEntities.GetPowerCoteOperateModuleByID(moduleTypeID, moduleCode, userID, "", coteModuleID, coteID).ToList<WTF.Power.Entity.Sys_Module>();
        }

        public List<WTF.Power.Entity.Sys_Module> GetPowerOperateModule(string moduleTypeID, string moduleCode, string userID, OperatePlaceType operatePlaceType, string coteModuleID, string coteID)
        {
            int num = (int) operatePlaceType;
            return this.CurrentEntities.GetPowerCoteOperateModuleByID(moduleTypeID, moduleCode, userID, num.ToString(), coteModuleID, coteID).ToList<WTF.Power.Entity.Sys_Module>();
        }

        public string GetQuickModuleTreexXml()
        {
            XmlDocument xmlDocSource = new XmlDocument();
            WTF.Power.Entity.Sys_ModuleType type = this.CurrentEntities.sys_moduletype.Where("it.ModuleTypeCode == 'ModuleTemplate'", new ObjectParameter[0]).Include("Sys_Module").First<WTF.Power.Entity.Sys_ModuleType>();
            XmlElement newChild = xmlDocSource.CreateElement("Module");
            newChild.SetAttribute("ModuleID", type.ModuleTypeID.ToString());
            newChild.SetAttribute("ModuleName", "请选择以下结点");
            newChild.SetAttribute("NavigateUrl", type.ModuleTypeID.ToString());
            xmlDocSource.AppendChild(newChild);
            this.CreateChildModeuleXmlElement(xmlDocSource, type.ModuleTypeID, newChild, (from s in type.Sys_Module
                orderby s.SortIndex
                select s).ToList<WTF.Power.Entity.Sys_Module>());
            return xmlDocSource.InnerXml;
        }

        public List<WTF.Power.Entity.Sys_ModuleData> GetRolePowerModuleData(string ModuleID, string Rolid)
        {
            if (this.CurrentEntities.sys_moduletype.Any<WTF.Power.Entity.Sys_ModuleType>(s => s.ModuleTypeID == ModuleID))
            {
                string commandText = string.Format("select distinct Sys_ModuleData.*   from Sys_ModuleData  ,Sys_RolePower,\r\n\r\nSys_ModuleCheckData where \r\n Sys_RolePower.RoleID='{0}'  and Sys_ModuleData.Moduleid='{1}' and \r\n  Sys_ModuleCheckData.Moduleid=Sys_RolePower.Moduleid\r\nand   Sys_ModuleData.ModuleDataID=Sys_ModuleCheckData.ModuleDataID", Rolid.ToString(), ModuleID.ToString());
                return this.CurrentEntities.ExecuteStoreQuery<WTF.Power.Entity.Sys_ModuleData>(commandText, new object[0]).ToList<WTF.Power.Entity.Sys_ModuleData>();
            }
            return (from s in this.CurrentEntities.sys_moduledata
                where s.ModuleID == ModuleID
                select s).ToList<WTF.Power.Entity.Sys_ModuleData>();
        }

        public ObjectQuery<WTF.Power.Entity.Sys_Module> GetUpdateSqlModuleInfo(string moduleId)
        {
            WTF.Power.Entity.Sys_Module module = this.CurrentEntities.sys_module.First<WTF.Power.Entity.Sys_Module>(s => s.ModuleID == moduleId);
            return this.CurrentEntities.sys_module.Where("it.ModuleIDPath like '" + module.ModuleIDPath + "%'", new ObjectParameter[0]);
        }

        public List<WTF.Power.Entity.Sys_ModuleData> GetUserRolePowerModuleData(string ModuleID, string userID)
        {
            if (this.CurrentEntities.sys_moduletype.Any<WTF.Power.Entity.Sys_ModuleType>(s => s.ModuleTypeID == ModuleID))
            {
                string commandText = string.Format("select distinct Sys_ModuleData.*   from Sys_ModuleData  ,Sys_RolePower,\r\n\r\nSys_ModuleCheckData,Sys_RoleUser where Sys_RoleUser.UserID='{0}' and \r\n Sys_RolePower.RoleID=Sys_RoleUser.RoleID   and Sys_ModuleData.Moduleid='{1}' and \r\n  Sys_ModuleCheckData.Moduleid=Sys_RolePower.Moduleid\r\nand   Sys_ModuleData.ModuleDataID=Sys_ModuleCheckData.ModuleDataID", userID, ModuleID.ToString());
                return this.CurrentEntities.ExecuteStoreQuery<WTF.Power.Entity.Sys_ModuleData>(commandText, new object[0]).ToList<WTF.Power.Entity.Sys_ModuleData>();
            }
            string format = " select distinct Sys_ModuleData.* from Sys_ModuleData,Sys_RoleData,Sys_RoleUser\r\n where Sys_ModuleData.ModuleDataID=Sys_RoleData.ModuleDataID\r\nand Sys_RoleData.RoleID=Sys_RoleUser.RoleID AND Sys_RoleUser.UserID='{0}'\r\nAND Sys_ModuleData.ModuleID='{1}'";
            return this.CurrentEntities.ExecuteStoreQuery<WTF.Power.Entity.Sys_ModuleData>(string.Format(format, userID, ModuleID), new object[0]).ToList<WTF.Power.Entity.Sys_ModuleData>();
        }

        public void InsertModule(WTF.Power.Entity.Sys_Module objModule)
        {
            objModule.ModuleCode.CheckIsNull("请输入模块编码", LogModuleType.ModuleLog);
            if (this.CurrentEntities.sys_module.Any<WTF.Power.Entity.Sys_Module>(p => p.ModuleID == objModule.ParentModuleID))
            {
                WTF.Power.Entity.Sys_Module module = this.Sys_Module.FirstOrDefault<WTF.Power.Entity.Sys_Module>(p => p.ModuleID == objModule.ParentModuleID);
                module.Sys_ModuleTypeReference.Load();
                objModule.SortIndex = (from p in this.Sys_Module
                    where p.ParentModuleID == objModule.ParentModuleID
                    select p).Count<WTF.Power.Entity.Sys_Module>() + 1;
                objModule.ModuleIDPath = module.ModuleIDPath + "," + objModule.ModuleID.ToString();
                objModule.ModuleLevel = module.ModuleLevel + 1;
                module.Sys_ModuleType.Sys_Module.Add(objModule);
            }
            else
            {
                WTF.Power.Entity.Sys_ModuleType objModuleType = this.Sys_ModuleType.FirstOrDefault<WTF.Power.Entity.Sys_ModuleType>(p => p.ModuleTypeID == objModule.ParentModuleID);
                objModule.ModuleIDPath = objModule.ModuleID.ToString();
                objModule.SortIndex = (from p in this.Sys_Module
                    where p.ParentModuleID == objModuleType.ModuleTypeID
                    select p).Count<WTF.Power.Entity.Sys_Module>() + 1;
                objModule.ModuleLevel = 1;
                objModuleType.Sys_Module.Add(objModule);
            }
            this.CurrentEntities.SaveChanges();
        }

        public void InsertModuleCote(WTF.Power.Entity.Sys_ModuleCote objSys_ModuleCote)
        {
            objSys_ModuleCote.CoteTitle.CheckIsNull<string>("请输入栏目名称", "ModuleLog");
            this.CurrentEntities.AddTosys_modulecote(objSys_ModuleCote);
            this.CurrentEntities.SaveChanges();
        }

        public void InsertModuleData(WTF.Power.Entity.Sys_ModuleData objSys_ModuleData)
        {
            objSys_ModuleData.DataName.CheckIsNull<string>("请输入数据名称", "ModuleLog");
            objSys_ModuleData.FieldName.CheckIsNull<string>("请输入字段名", "ModuleLog");
            objSys_ModuleData.DataSelect.CheckIsNull<string>("请输入数据查询", "ModuleLog");
            this.CurrentEntities.AddTosys_moduledata(objSys_ModuleData);
            this.CurrentEntities.SaveChanges();
        }

        public void InsertModuleHelp(string moduleID, string helpTitle, string helpContent, string fileTextResourceID, string fileResourceID)
        {
            if ((from p in this.Sys_ModuleHelp
                where p.ModuleID == moduleID
                select p).Count<WTF.Power.Entity.Sys_ModuleHelp>() > 0)
            {
                WTF.Power.Entity.Sys_ModuleHelp help = this.Sys_ModuleHelp.Single<WTF.Power.Entity.Sys_ModuleHelp>(p => p.ModuleID == moduleID);
                help.HelpContent = helpContent;
                help.HelpTitle = helpTitle;
                help.FileResourceID = fileResourceID;
                help.FileTextResourceID = fileTextResourceID;
            }
            else
            {
                WTF.Power.Entity.Sys_ModuleHelp help2 = new WTF.Power.Entity.Sys_ModuleHelp {
                    ModuleID = moduleID,
                    HelpContent = helpContent,
                    HelpTitle = helpTitle,
                    CreateDate = DateTime.Now,
                    FileResourceID = fileResourceID,
                    FileTextResourceID = fileTextResourceID
                };
                this.CurrentEntities.AddTosys_modulehelp(help2);
            }
            this.CurrentEntities.AcceptAllChanges();
        }

        public void InsertModuleType(WTF.Power.Entity.Sys_ModuleType objModuleType)
        {
            this.CurrentEntities.AddTosys_moduletype(objModuleType);
            this.CurrentEntities.SaveChanges();
        }

        private void ModeuleChildCopy(string ParmentModuleID, string moduleID, List<WTF.Power.Entity.Sys_Module> objSys_ModuleList)
        {
            foreach (WTF.Power.Entity.Sys_Module module in from p in objSys_ModuleList
                where p.ParentModuleID == moduleID
                orderby p.SortIndex
                select p)
            {
                WTF.Power.Entity.Sys_Module objModule = new WTF.Power.Entity.Sys_Module {
                    ModuleCode = module.ModuleCode,
                    ModuleName = module.ModuleName,
                    ModuleShow = module.ModuleShow,
                    ModuleFunID = module.ModuleFunID,
                    OperateTypeID = module.OperateTypeID,
                    ParentModuleID = ParmentModuleID,
                    LogCategoryID = module.LogCategoryID,
                    IsDispose = module.IsDispose,
                    PlaceType = module.PlaceType,
                    Remark = module.Remark,
                    ToolTip = module.ToolTip,
                    ValGroupName = module.ValGroupName,
                    ClickScriptFun = module.ClickScriptFun,
                    CommandArgument = module.CommandArgument,
                    CommandName = module.CommandName,
                    IsMvc = module.IsMvc,
                    IsController = module.IsController,
                    ImageUrl = module.ImageUrl,
                    IsEdit = module.IsEdit,
                    ModuleID = Guid.NewGuid().ToString(),
                    MenuField = module.MenuField,
                    MenuCal = module.MenuCal,
                    MenuValue = module.MenuValue,
                    IsCheckPowerData = false,
                    ModuleCoteID = module.ModuleCoteID,
                    TargetUrl = module.TargetUrl,
                    ShareModuleID = module.ShareModuleID,
                    IsPower = module.IsPower,
                    IsSupperPower = module.IsSupperPower,
                    CoteKeyID = module.CoteKeyID
                };
                if (objModule.CoteKeyID > 0)
                {
                    objModule.CoteKeyID = this.Sys_Module.Max<WTF.Power.Entity.Sys_Module, int>(s => s.CoteKeyID) + 1;
                }
                this.InsertModule(objModule);
                this.ModeuleChildCopy(objModule.ModuleID, module.ModuleID, objSys_ModuleList);
            }
        }

        public string ModeuleCopy(string ModuleID, string tagModuleID)
        {
            string moduleIDPath = this.CurrentEntities.sys_module.First<WTF.Power.Entity.Sys_Module>(s => (s.ModuleID == ModuleID)).ModuleIDPath;
            List<WTF.Power.Entity.Sys_Module> source = this.CurrentEntities.sys_module.Where("it.ModuleIDPath like '" + moduleIDPath + "%'", new ObjectParameter[0]).ToList<WTF.Power.Entity.Sys_Module>();
            WTF.Power.Entity.Sys_Module module2 = source.First<WTF.Power.Entity.Sys_Module>(s => s.ModuleID == ModuleID);
            WTF.Power.Entity.Sys_Module objModule = new WTF.Power.Entity.Sys_Module {
                ModuleCode = module2.ModuleCode,
                ModuleName = module2.ModuleName + ((module2.ModuleFunID != 3) ? "-新复制的节点" : ""),
                ModuleShow = module2.ModuleShow,
                ModuleFunID = module2.ModuleFunID,
                OperateTypeID = module2.OperateTypeID,
                ParentModuleID = tagModuleID,
                LogCategoryID = module2.LogCategoryID,
                IsDispose = module2.IsDispose,
                PlaceType = module2.PlaceType,
                Remark = module2.Remark,
                ToolTip = module2.ToolTip,
                ValGroupName = module2.ValGroupName,
                ClickScriptFun = module2.ClickScriptFun,
                CommandArgument = module2.CommandArgument,
                CommandName = module2.CommandName,
                IsMvc = module2.IsMvc,
                IsController = module2.IsController,
                ImageUrl = module2.ImageUrl,
                ModuleID = Guid.NewGuid().ToString(),
                MenuField = module2.MenuField,
                MenuCal = module2.MenuCal,
                MenuValue = module2.MenuValue,
                IsCheckPowerData = false,
                ModuleCoteID = module2.ModuleCoteID,
                TargetUrl = module2.TargetUrl,
                IsEdit = module2.IsEdit,
                ShareModuleID = module2.ShareModuleID,
                IsPower = module2.IsPower,
                IsSupperPower = module2.IsSupperPower,
                CoteKeyID = module2.CoteKeyID
            };
            if (objModule.CoteKeyID > 0)
            {
                objModule.CoteKeyID = this.Sys_Module.Max<WTF.Power.Entity.Sys_Module, int>(s => s.CoteKeyID) + 1;
            }
            this.InsertModule(objModule);
            this.ModeuleChildCopy(objModule.ModuleID, ModuleID, source);
            return objModule.ModuleID;
        }

        public void ModeuleMove(string ModuleID, string tagModuleID)
        {
            WTF.Power.Entity.Sys_Module module = this.CurrentEntities.sys_module.First<WTF.Power.Entity.Sys_Module>(s => s.ModuleID == ModuleID);
            string moduleIDPath = module.ModuleIDPath;
            int moduleLevel = module.ModuleLevel;
            ObjectQuery<WTF.Power.Entity.Sys_Module> query = this.CurrentEntities.sys_module.Where("it.ModuleIDPath like '" + moduleIDPath + "%'", new ObjectParameter[0]);
            WTF.Power.Entity.Sys_Module module2 = (from s in this.CurrentEntities.sys_module
                where s.ModuleID == tagModuleID
                select s).FirstOrDefault<WTF.Power.Entity.Sys_Module>();
            string newValue = (module2 == null) ? ModuleID.ToString() : (module2.ModuleIDPath + "," + ModuleID.ToString());
            int num2 = (module2 == null) ? 0 : module2.ModuleLevel;
            foreach (WTF.Power.Entity.Sys_Module module3 in query)
            {
                module3.ModuleIDPath = module3.ModuleIDPath.Replace(moduleIDPath, newValue);
                module3.ModuleLevel = ((module3.ModuleLevel - moduleLevel) + 1) + num2;
                if (module3.ModuleID == ModuleID)
                {
                    module3.ParentModuleID = tagModuleID;
                }
            }
            this.SaveChanges();
        }

        public void ModeuleQuickCopy(string quickModulename, string moduleCode, int logModuleID, string moduleID, List<string> quickModuleIDList)
        {
            Func<WTF.Power.Entity.Sys_Module, bool> predicate = null;
            string moduleTypeID;
            List<string> objAddModuleIDList;
            List<WTF.Power.Entity.Sys_Module> source = (from s in this.CurrentEntities.sys_module
                where quickModuleIDList.Contains(s.ModuleID)
                select s).ToList<WTF.Power.Entity.Sys_Module>();
            if (source.Count != 0)
            {
                moduleTypeID = source.First<WTF.Power.Entity.Sys_Module>().ModuleTypeID;
                objAddModuleIDList = (from S in source select S.ModuleID).ToList<string>();
                foreach (WTF.Power.Entity.Sys_Module module in from s in source
                    where s.ParentModuleID == moduleTypeID
                    orderby s.SortIndex
                    select s)
                {
                    WTF.Power.Entity.Sys_Module objModule = new WTF.Power.Entity.Sys_Module {
                        ModuleID = Guid.NewGuid().ToString()
                    };
                    if ("新增".IndexOf(module.ModuleName) >= 0)
                    {
                        objModule.LogCategoryID = logModuleID;
                    }
                    else
                    {
                        objModule.LogCategoryID = module.LogCategoryID;
                    }
                    if ("新增,修改".IndexOf(module.ModuleName) >= 0)
                    {
                        objModule.ModuleCode = string.IsNullOrWhiteSpace(moduleCode) ? module.ModuleCode : moduleCode;
                    }
                    else
                    {
                        objModule.ModuleCode = module.ModuleCode;
                    }
                    if ("新增,修改,删除".IndexOf(module.ModuleName) >= 0)
                    {
                        objModule.ModuleName = module.ModuleName + quickModulename;
                        objModule.ToolTip = module.ToolTip + quickModulename;
                    }
                    else
                    {
                        objModule.ModuleName = module.ModuleName;
                        objModule.ToolTip = module.ToolTip;
                    }
                    objModule.ModuleShow = module.ModuleShow;
                    objModule.ModuleFunID = module.ModuleFunID;
                    objModule.OperateTypeID = module.OperateTypeID;
                    objModule.ParentModuleID = moduleID;
                    objModule.IsDispose = module.IsDispose;
                    objModule.PlaceType = module.PlaceType;
                    objModule.Remark = module.Remark;
                    objModule.ValGroupName = module.ValGroupName;
                    objModule.ClickScriptFun = module.ClickScriptFun;
                    objModule.CommandArgument = module.CommandArgument;
                    objModule.CommandName = module.CommandName;
                    objModule.IsMvc = false;
                    objModule.IsController = false;
                    objModule.ImageUrl = module.ImageUrl;
                    objModule.MenuField = module.MenuField;
                    objModule.MenuCal = module.MenuCal;
                    objModule.MenuValue = module.MenuValue;
                    objModule.IsCheckPowerData = false;
                    objModule.TargetUrl = module.TargetUrl;
                    objModule.ModuleCoteID = module.ModuleCoteID;
                    objModule.IsEdit = module.IsEdit;
                    objModule.ShareModuleID = module.ShareModuleID;
                    objModule.IsPower = module.IsPower;
                    objModule.CoteKeyID = module.CoteKeyID;
                    objModule.IsSupperPower = module.IsSupperPower;
                    this.InsertModule(objModule);
                    objAddModuleIDList.Remove(module.ModuleID);
                    this.ModuleQuickChildCopy(quickModulename, moduleCode, logModuleID, objModule.ModuleID, module.ModuleID, source, objAddModuleIDList);
                }
                if (objAddModuleIDList.Count > 0)
                {
                    if (predicate == null)
                    {
                        predicate = s => objAddModuleIDList.Contains(s.ModuleID);
                    }
                    foreach (WTF.Power.Entity.Sys_Module module3 in from s in source.Where<WTF.Power.Entity.Sys_Module>(predicate)
                        orderby s.SortIndex
                        select s)
                    {
                        WTF.Power.Entity.Sys_Module module4 = new WTF.Power.Entity.Sys_Module {
                            ModuleID = Guid.NewGuid().ToString()
                        };
                        if ("新增".IndexOf(module3.ModuleName) >= 0)
                        {
                            module4.LogCategoryID = logModuleID;
                        }
                        else
                        {
                            module4.LogCategoryID = module3.LogCategoryID;
                        }
                        if ("新增,修改".IndexOf(module3.ModuleName) >= 0)
                        {
                            module4.ModuleCode = string.IsNullOrWhiteSpace(moduleCode) ? module3.ModuleCode : moduleCode;
                        }
                        else
                        {
                            module4.ModuleCode = module3.ModuleCode;
                        }
                        if ("新增,修改,删除".IndexOf(module3.ModuleName) >= 0)
                        {
                            module4.ModuleName = module3.ModuleName + quickModulename;
                            module4.ToolTip = module3.ToolTip + quickModulename;
                        }
                        else
                        {
                            module4.ModuleName = module3.ModuleName;
                            module4.ToolTip = module3.ToolTip;
                        }
                        module4.ModuleShow = module3.ModuleShow;
                        module4.ModuleFunID = module3.ModuleFunID;
                        module4.OperateTypeID = module3.OperateTypeID;
                        module4.ParentModuleID = moduleID;
                        module4.IsDispose = module3.IsDispose;
                        module4.PlaceType = module3.PlaceType;
                        module4.Remark = module3.Remark;
                        module4.ValGroupName = module3.ValGroupName;
                        module4.ClickScriptFun = module3.ClickScriptFun;
                        module4.CommandArgument = module3.CommandArgument;
                        module4.CommandName = module3.CommandName;
                        module4.IsMvc = false;
                        module4.IsController = false;
                        module4.ImageUrl = module3.ImageUrl;
                        module4.MenuField = module3.MenuField;
                        module4.MenuCal = module3.MenuCal;
                        module4.MenuValue = module3.MenuValue;
                        module4.IsCheckPowerData = false;
                        module4.TargetUrl = module3.TargetUrl;
                        module4.ModuleCoteID = module3.ModuleCoteID;
                        module4.IsEdit = module3.IsEdit;
                        module4.ShareModuleID = module3.ShareModuleID;
                        module4.IsPower = module3.IsPower;
                        module4.CoteKeyID = module3.CoteKeyID;
                        module4.IsSupperPower = module3.IsSupperPower;
                        this.InsertModule(module4);
                    }
                }
            }
        }

        private void ModuleQuickChildCopy(string quickModulename, string moduleCode, int logModuleID, string parmentModuleID, string moduleID, List<WTF.Power.Entity.Sys_Module> objModuleList, List<string> objAddModuleIDList)
        {
            foreach (WTF.Power.Entity.Sys_Module module in from s in objModuleList
                where s.ParentModuleID == moduleID
                orderby s.SortIndex
                select s)
            {
                WTF.Power.Entity.Sys_Module objModule = new WTF.Power.Entity.Sys_Module {
                    ModuleID = Guid.NewGuid().ToString()
                };
                if ("新增".IndexOf(module.ModuleName) >= 0)
                {
                    objModule.LogCategoryID = logModuleID;
                }
                else
                {
                    objModule.LogCategoryID = module.LogCategoryID;
                }
                if ("新增,修改".IndexOf(module.ModuleName) >= 0)
                {
                    objModule.ModuleCode = string.IsNullOrWhiteSpace(moduleCode) ? module.ModuleCode : moduleCode;
                }
                else
                {
                    objModule.ModuleCode = module.ModuleCode;
                }
                if ("新增,修改,删除".IndexOf(module.ModuleName) >= 0)
                {
                    objModule.ModuleName = module.ModuleName + quickModulename;
                    objModule.ToolTip = module.ToolTip + quickModulename;
                }
                else
                {
                    objModule.ModuleName = module.ModuleName;
                    objModule.ToolTip = module.ToolTip;
                }
                objModule.ModuleShow = module.ModuleShow;
                objModule.ModuleFunID = module.ModuleFunID;
                objModule.OperateTypeID = module.OperateTypeID;
                objModule.ParentModuleID = parmentModuleID;
                objModule.LogCategoryID = module.LogCategoryID;
                objModule.IsDispose = module.IsDispose;
                objModule.PlaceType = module.PlaceType;
                objModule.Remark = module.Remark;
                objModule.ValGroupName = module.ValGroupName;
                objModule.ClickScriptFun = module.ClickScriptFun;
                objModule.CommandArgument = module.CommandArgument;
                objModule.CommandName = module.CommandName;
                objModule.IsMvc = false;
                objModule.IsController = false;
                objModule.ImageUrl = module.ImageUrl;
                objModule.MenuField = module.MenuField;
                objModule.MenuCal = module.MenuCal;
                objModule.MenuValue = module.MenuValue;
                objModule.IsCheckPowerData = false;
                objModule.TargetUrl = module.TargetUrl;
                objModule.ModuleCoteID = module.ModuleCoteID;
                objModule.IsEdit = module.IsEdit;
                objModule.ShareModuleID = module.ShareModuleID;
                objModule.IsPower = module.IsPower;
                objModule.CoteKeyID = module.CoteKeyID;
                objModule.IsSupperPower = module.IsSupperPower;
                this.InsertModule(objModule);
                objAddModuleIDList.Remove(module.ModuleID);
                this.ModuleQuickChildCopy(quickModulename, moduleCode, logModuleID, objModule.ModuleID, module.ModuleID, objModuleList, objAddModuleIDList);
            }
        }

        public void SaveChanges()
        {
            this.objCurrentEntities.SaveChanges();
        }

        public void UpdateModule(WTF.Power.Entity.Sys_Module objModule)
        {
            this.SaveChanges();
        }

        public void UpdateModuleCheckData(string moduleID, string moduleDataIDstring)
        {
            List<string> list = moduleDataIDstring.ConvertListString();
            string str = "";
            foreach (WTF.Power.Entity.Sys_ModuleCheckData data in from s in this.CurrentEntities.sys_modulecheckdata
                where s.ModuleID == moduleID
                select s)
            {
                if (list.Contains(data.ModuleDataID))
                {
                    list.Remove(data.ModuleDataID);
                }
                else
                {
                    str = str + data.ModuleDataID.ToString() + ",";
                    this.CurrentEntities.DeleteObject(data);
                }
            }
            this.CurrentEntities.SaveChanges();
            if (list.Count > 0)
            {
                foreach (string str2 in list)
                {
                    WTF.Power.Entity.Sys_ModuleCheckData data2 = new WTF.Power.Entity.Sys_ModuleCheckData {
                        ModuleCheckDataID = Guid.NewGuid().ToString(),
                        ModuleID = moduleID,
                        ModuleDataID = str2
                    };
                    this.CurrentEntities.AddTosys_modulecheckdata(data2);
                }
                this.CurrentEntities.SaveChanges();
            }
            str = str.TrimEndComma();
            if (str.IsNoNull())
            {
                new UserRule().DeleteRoleData("it.ModuleDataID in {" + str.ConvertStringID() + "}");
            }
        }

        public void UpdateModuleCote(WTF.Power.Entity.Sys_ModuleCote objSys_ModuleCote)
        {
            objSys_ModuleCote.CoteTitle.CheckIsNull<string>("请输入栏目名称", "ModuleLog");
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateModuleData(WTF.Power.Entity.Sys_ModuleData objSys_ModuleData)
        {
            objSys_ModuleData.DataName.CheckIsNull<string>("请输入数据名称", "ModuleLog");
            objSys_ModuleData.FieldName.CheckIsNull<string>("请输入字段名", "ModuleLog");
            objSys_ModuleData.DataSelect.CheckIsNull<string>("请输入数据查询", "ModuleLog");
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateModuleIco(int operateTypeID, string icoPath)
        {
            foreach (WTF.Power.Entity.Sys_Module module in from p in this.CurrentEntities.sys_module
                where p.OperateTypeID == operateTypeID
                select p)
            {
                module.ImageUrl = icoPath;
            }
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateModuleSort(string moduleIDstring)
        {
            moduleIDstring.CheckIsNull("请选择要排序的模块", LogModuleType.ModuleLog);
            List<string> list = moduleIDstring.ConvertListString();
            foreach (WTF.Power.Entity.Sys_Module module in this.Sys_Module.Where("it.ModuleID in {" + moduleIDstring.ConvertStringID() + "}", new ObjectParameter[0]))
            {
                module.SortIndex = list.IndexOf(module.ModuleID) + 1;
            }
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateoduleType(WTF.Power.Entity.Sys_ModuleType objModuleType)
        {
            this.CurrentEntities.SaveChanges();
        }

        public ModuleEntities CurrentEntities
        {
            get
            {
                if (this.objCurrentEntities == null)
                {
                    this.objCurrentEntities = new ModuleEntities(EntitiesHelper.GetConnectionString<ModuleEntities>("WTF.Power.ConnectionString"));
                }
                return this.objCurrentEntities;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_Module> Sys_Module
        {
            get
            {
                return this.CurrentEntities.sys_module;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_ModuleCheckData> Sys_ModuleCheckData
        {
            get
            {
                return this.CurrentEntities.sys_modulecheckdata;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_ModuleCote> Sys_ModuleCote
        {
            get
            {
                return this.CurrentEntities.sys_modulecote;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_ModuleData> Sys_ModuleData
        {
            get
            {
                return this.CurrentEntities.sys_moduledata;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_ModuleHelp> Sys_ModuleHelp
        {
            get
            {
                return this.CurrentEntities.sys_modulehelp;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_ModuleType> Sys_ModuleType
        {
            get
            {
                return this.CurrentEntities.sys_moduletype;
            }
        }
    }
}

