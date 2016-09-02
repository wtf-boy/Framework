namespace WTF.Power
{
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Power.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class PowerRule
    {
        private ModuleEntities objCurrentEntities;

        private void CreateChildShareModeuleXmlElement(XmlDocument xmlDocSource, string ShareModuleID, string CoteID, string ModuleID, string ParentModuleID, bool IsEdit, string ModuleCode, XmlElement objXmlElement, List<WTF.Power.Entity.Sys_Module> objSys_ModuleList, List<WTF.Power.Entity.Sys_Module> AddSys_ModuleList)
        {
            Func<WTF.Power.Entity.Sys_Module, bool> predicate = null;
            Func<WTF.Power.Entity.Sys_Module, bool> func2 = null;
            string SearchModuleID = ModuleID;
            if (IsEdit)
            {
                if (predicate == null)
                {
                    predicate = s => (!s.IsEdit && (s.ParentModuleID == ParentModuleID)) && (s.ModuleCode == ModuleCode);
                }
                WTF.Power.Entity.Sys_Module module = objSys_ModuleList.FirstOrDefault<WTF.Power.Entity.Sys_Module>(predicate);
                if (module != null)
                {
                    SearchModuleID = module.ModuleID;
                }
                else if (AddSys_ModuleList != null)
                {
                    if (func2 == null)
                    {
                        func2 = s => (!s.IsEdit && (s.ParentModuleID == ParentModuleID)) && (s.ModuleCode == ModuleCode);
                    }
                    module = AddSys_ModuleList.FirstOrDefault<WTF.Power.Entity.Sys_Module>(func2);
                    if (module != null)
                    {
                        SearchModuleID = module.ModuleID;
                    }
                }
            }
            foreach (WTF.Power.Entity.Sys_Module module2 in from p in objSys_ModuleList
                where p.ParentModuleID == SearchModuleID
                orderby p.SortIndex
                select p)
            {
                XmlElement newChild = xmlDocSource.CreateElement("Module");
                newChild.SetAttribute("ModuleID", RolePowerKey.Create(ShareModuleID, CoteID, module2.ModuleID, true).ToKey);
                newChild.SetAttribute("ModuleName", module2.ModuleName);
                objXmlElement.AppendChild(newChild);
                this.CreateChildShareModeuleXmlElement(xmlDocSource, ShareModuleID, CoteID, module2.ModuleID, module2.ParentModuleID, module2.IsEdit, module2.ModuleCode, newChild, objSys_ModuleList, AddSys_ModuleList);
            }
        }

        private XmlElement CreateCoteModeule(XmlDocument xmlDocSource, string AuthorizeGroupID, WTF.Power.Entity.Sys_Module objSys_Module, List<WTF.Power.Entity.Sys_Module> objSys_ModuleList, List<WTF.Power.Entity.Sys_Module> AddSys_ModuleList)
        {
            try
            {
                WTF.Power.Entity.Sys_ModuleCote cote = this.Sys_ModuleCote.FirstOrDefault<WTF.Power.Entity.Sys_ModuleCote>(s => s.ModuleCoteID == objSys_Module.ModuleCoteID);
                if (cote == null)
                {
                    return null;
                }
                if (cote.CoteTableName.IsNull())
                {
                    return null;
                }
                if ((cote.ParentIDName.IsNull() && cote.RootIDValue.IsNull()) && cote.IDPathName.IsNull())
                {
                    PowerCotePower power = new PowerCotePower(cote, objSys_Module.ModuleTypeID);
                    return power.GetCotePowerXmlElement(xmlDocSource, AuthorizeGroupID, objSys_Module, objSys_ModuleList, AddSys_ModuleList);
                }
                PowerCoteTreePower power2 = new PowerCoteTreePower(cote, objSys_Module.ModuleTypeID);
                return power2.GetCotePowerXmlElement(xmlDocSource, AuthorizeGroupID, objSys_Module, objSys_ModuleList, AddSys_ModuleList);
            }
            catch (Exception exception)
            {
                LogHelper<LogModuleType>.Write(LogModuleType.ModuleLog, exception, "");
                return null;
            }
        }

        private void CreatePowerChildElement(XmlDocument xmlDocSource, string AuthorizeGroupID, string ModuleID, string ParentModuleID, bool IsEdit, string ModuleCode, XmlElement objXmlElement, List<WTF.Power.Entity.Sys_Module> objSys_ModuleList, List<WTF.Power.Entity.Sys_Module> AddSys_ModuleList)
        {
            Func<WTF.Power.Entity.Sys_Module, bool> predicate = null;
            Func<WTF.Power.Entity.Sys_Module, bool> func3 = null;
            string SearchModuleID = ModuleID;
            if (IsEdit)
            {
                if (predicate == null)
                {
                    predicate = s => (!s.IsEdit && (s.ParentModuleID == ParentModuleID)) && (s.ModuleCode == ModuleCode);
                }
                WTF.Power.Entity.Sys_Module module = objSys_ModuleList.FirstOrDefault<WTF.Power.Entity.Sys_Module>(predicate);
                if (module != null)
                {
                    SearchModuleID = module.ModuleID;
                }
                else if (AddSys_ModuleList != null)
                {
                    if (func3 == null)
                    {
                        func3 = s => (!s.IsEdit && (s.ParentModuleID == ParentModuleID)) && (s.ModuleCode == ModuleCode);
                    }
                    module = AddSys_ModuleList.FirstOrDefault<WTF.Power.Entity.Sys_Module>(func3);
                    if (module != null)
                    {
                        SearchModuleID = module.ModuleID;
                    }
                }
            }
            using (IEnumerator<WTF.Power.Entity.Sys_Module> enumerator = (from p in objSys_ModuleList
                where p.ParentModuleID == SearchModuleID
                orderby p.SortIndex
                select p).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Func<WTF.Power.Entity.Sys_Module, bool> func = null;
                    WTF.Power.Entity.Sys_Module objModule = enumerator.Current;
                    if (objModule.ModuleCoteID > 0)
                    {
                        if (func == null)
                        {
                            func = s => s.ModuleIDPath.StartsWith(objModule.ModuleIDPath);
                        }
                        XmlElement newChild = this.CreateCoteModeule(xmlDocSource, AuthorizeGroupID, objModule, objSys_ModuleList.Where<WTF.Power.Entity.Sys_Module>(func).ToList<WTF.Power.Entity.Sys_Module>(), AddSys_ModuleList);
                        if (newChild != null)
                        {
                            objXmlElement.AppendChild(newChild);
                        }
                    }
                    else
                    {
                        XmlElement element2 = xmlDocSource.CreateElement("Module");
                        element2.SetAttribute("ModuleID", RolePowerKey.Create(objModule.ModuleID).ToKey);
                        element2.SetAttribute("ModuleName", objModule.ModuleName);
                        objXmlElement.AppendChild(element2);
                        if (objModule.ShareModuleID.IsNoNull())
                        {
                            this.CreateChildShareModeuleXmlElement(xmlDocSource, objModule.ShareModuleID, objModule.ModuleID, objModule.ShareModuleID, objModule.ShareModuleID, objModule.IsEdit, objModule.ModuleCode, element2, objSys_ModuleList, AddSys_ModuleList);
                        }
                        else
                        {
                            this.CreatePowerChildElement(xmlDocSource, AuthorizeGroupID, objModule.ModuleID, objModule.ParentModuleID, objModule.IsEdit, objModule.ModuleCode, element2, objSys_ModuleList, AddSys_ModuleList);
                        }
                    }
                }
            }
        }

        private void CreatePowerTreeXmlMenuElement(XmlDocument xmlDocSource, string ModuleID, XmlElement objXmlElement, List<WTF.Power.Entity.Sys_Module> objSys_ModuleList)
        {
            string format = "javascript:opentreemodule('{0}','{1}','{2}','{3}');";
            foreach (WTF.Power.Entity.Sys_Module module in from p in objSys_ModuleList
                where p.ParentModuleID == ModuleID
                orderby p.SortIndex
                select p)
            {
                if (module.ModuleCoteID > 0)
                {
                    UserRule rule = new UserRule();
                    XmlElement newChild = this.GetCotePowerXmlElement(xmlDocSource, module, rule.CurrentUser);
                    if (newChild != null)
                    {
                        objXmlElement.AppendChild(newChild);
                    }
                }
                else
                {
                    XmlElement element2 = xmlDocSource.CreateElement("Module");
                    element2.SetAttribute("ModuleID", module.ModuleID.ToString());
                    element2.SetAttribute("ModuleName", module.ModuleName);
                    element2.SetAttribute("ToolTip", module.ToolTip);
                    element2.SetAttribute("ImageUrl", module.ImageUrl);
                    element2.SetAttribute("ModuleLevel", module.ModuleLevel.ToString());
                    if (module.ShareModuleID.IsNull())
                    {
                        string url = module.CommandArgument + ((module.CommandArgument.IndexOf("?") >= 0) ? "&" : "?") + "PowerID=" + module.ModuleID + "&PowerName=" + module.ModuleName + ((module.CoteKeyID > 0) ? ("&CoteKeyID=" + module.CoteKeyID) : "");
                        element2.SetAttribute("NavigateUrl", module.CommandArgument.IsNull() ? "" : string.Format(format, new object[] { module.ModuleID, module.ImageUrl, url.EncryptModuleQuery(), module.ModuleName }));
                    }
                    else
                    {
                        string str3 = module.CommandArgument + ((module.CommandArgument.IndexOf("?") >= 0) ? "&" : "?") + "CoteID=" + module.ModuleID + "&CoteModuleID=" + module.ShareModuleID + "&PowerName=" + module.ModuleName + ((module.CoteKeyID > 0) ? ("&CoteKeyID=" + module.CoteKeyID) : "");
                        element2.SetAttribute("NavigateUrl", module.CommandArgument.IsNull() ? "" : string.Format(format, new object[] { module.ModuleID, module.ImageUrl, str3.EncryptModuleQuery(), module.ModuleName }));
                    }
                    objXmlElement.AppendChild(element2);
                    this.CreatePowerTreeXmlMenuElement(xmlDocSource, module.ModuleID, element2, objSys_ModuleList);
                }
            }
        }

        public XmlElement GetCotePowerXmlElement(XmlDocument xmlDocSource, WTF.Power.Entity.Sys_Module objSys_Module, UserInfo objUserInfo)
        {
            try
            {
                WTF.Power.Entity.Sys_ModuleCote cote = this.Sys_ModuleCote.FirstOrDefault<WTF.Power.Entity.Sys_ModuleCote>(s => s.ModuleCoteID == objSys_Module.ModuleCoteID);
                if (cote == null)
                {
                    return null;
                }
                if (cote.CoteTableName.IsNull())
                {
                    return null;
                }
                if ((cote.ParentIDName.IsNull() && cote.RootIDValue.IsNull()) && cote.IDPathName.IsNull())
                {
                    PowerCotePower power = new PowerCotePower(cote, objSys_Module.ModuleTypeID);
                    return power.GetCotePowerMenuXmlElement(xmlDocSource, objUserInfo, objSys_Module);
                }
                PowerCoteTreePower power2 = new PowerCoteTreePower(cote, objSys_Module.ModuleTypeID);
                return power2.GetCotePowerMenuXmlElement(xmlDocSource, objUserInfo, objSys_Module);
            }
            catch (Exception exception)
            {
                LogHelper<LogModuleType>.Write(LogModuleType.ModuleLog, exception, "");
                return null;
            }
        }

        public IEnumerable<ModuleCoteInfo> GetPowerCoteTreeFunctionModule(string moduleTypeID)
        {
            UserRule rule = new UserRule();
            List<ModuleCoteInfo> list = new List<ModuleCoteInfo>();
            List<WTF.Power.Entity.Sys_Module> list2 = this.CurrentEntities.GetPowerFunctionModuleByID(moduleTypeID, "", rule.CurrentUser.UserID, true).ToList<WTF.Power.Entity.Sys_Module>();
            foreach (WTF.Power.Entity.Sys_Module module in from s in list2
                where s.ParentModuleID == moduleTypeID
                orderby s.SortIndex
                select s)
            {
                ModuleCoteInfo item = new ModuleCoteInfo {
                    ModuleID = module.ModuleID,
                    ImageUrl = module.ImageUrl,
                    ModuleName = module.ModuleName,
                    PowXml = "",
                    SortIndex = module.SortIndex,
                    ToolTip = module.ToolTip
                };
                XmlDocument xmlDocSource = new XmlDocument();
                XmlElement newChild = xmlDocSource.CreateElement("Module");
                newChild.SetAttribute("ModuleID", item.ModuleID.ToString());
                newChild.SetAttribute("ModuleName", item.ModuleName);
                newChild.SetAttribute("ToolTip", item.ToolTip);
                newChild.SetAttribute("ImageUrl", item.ImageUrl);
                newChild.SetAttribute("ModuleLevel", module.ModuleLevel.ToString());
                newChild.SetAttribute("NavigateUrl", "");
                xmlDocSource.AppendChild(newChild);
                if (module.ModuleCoteID > 0)
                {
                    XmlElement element2 = this.GetCotePowerXmlElement(xmlDocSource, module, rule.CurrentUser);
                    if (element2 != null)
                    {
                        newChild.AppendChild(element2);
                    }
                }
                else
                {
                    this.CreatePowerTreeXmlMenuElement(xmlDocSource, item.ModuleID, newChild, list2);
                }
                item.PowXml = xmlDocSource.InnerXml;
                list.Add(item);
            }
            return list;
        }

        public string GetPowerCoteTreeFunctionModuleXml(string moduleTypeID, string ModuleCode)
        {
            UserRule rule = new UserRule();
            new List<ModuleCoteInfo>();
            List<WTF.Power.Entity.Sys_Module> list = this.CurrentEntities.GetPowerFunctionModuleByID(moduleTypeID, ModuleCode, rule.CurrentUser.UserID, true).ToList<WTF.Power.Entity.Sys_Module>();
            WTF.Power.Entity.Sys_Module module = this.CurrentEntities.sys_module.FirstOrDefault<WTF.Power.Entity.Sys_Module>(s => (s.ModuleTypeID == moduleTypeID) && (s.ModuleCode == ModuleCode));
            XmlDocument xmlDocSource = new XmlDocument();
            if (module.ModuleCoteID > 0)
            {
                XmlElement newChild = this.GetCotePowerXmlElement(xmlDocSource, module, rule.CurrentUser);
                if (newChild != null)
                {
                    xmlDocSource.AppendChild(newChild);
                }
                else
                {
                    XmlElement element2 = xmlDocSource.CreateElement("Module");
                    element2.SetAttribute("ModuleID", module.ModuleID.ToString());
                    element2.SetAttribute("ModuleName", module.ModuleName);
                    element2.SetAttribute("ToolTip", module.ToolTip);
                    element2.SetAttribute("ImageUrl", module.ImageUrl);
                    element2.SetAttribute("ModuleLevel", module.ModuleLevel.ToString());
                    element2.SetAttribute("NavigateUrl", "");
                    xmlDocSource.AppendChild(newChild);
                }
            }
            else
            {
                XmlElement element3 = xmlDocSource.CreateElement("Module");
                element3.SetAttribute("ModuleID", module.ModuleID.ToString());
                element3.SetAttribute("ModuleName", module.ModuleName);
                element3.SetAttribute("ToolTip", module.ToolTip);
                element3.SetAttribute("ImageUrl", module.ImageUrl);
                element3.SetAttribute("ModuleLevel", module.ModuleLevel.ToString());
                element3.SetAttribute("NavigateUrl", "");
                xmlDocSource.AppendChild(element3);
                this.CreatePowerTreeXmlMenuElement(xmlDocSource, module.ModuleID, element3, list);
            }
            return xmlDocSource.InnerXml;
        }

        public string GetPowerTreexXmlText(string moduleTypeID, bool isSupper = false)
        {
            new UserRule();
            XmlDocument xmlDocSource = new XmlDocument();
            WTF.Power.Entity.Sys_ModuleType type = this.CurrentEntities.sys_moduletype.FirstOrDefault<WTF.Power.Entity.Sys_ModuleType>(s => s.ModuleTypeID == moduleTypeID);
            List<WTF.Power.Entity.Sys_Module> list = null;
            if (isSupper)
            {
                list = (from s in this.CurrentEntities.sys_module
                    where (s.ModuleTypeID == moduleTypeID) && s.IsPower
                    select s).ToList<WTF.Power.Entity.Sys_Module>();
            }
            else
            {
                list = (from s in this.CurrentEntities.sys_module
                    where ((s.ModuleTypeID == moduleTypeID) && s.IsPower) && !s.IsSupperPower
                    select s).ToList<WTF.Power.Entity.Sys_Module>();
            }
            XmlElement newChild = xmlDocSource.CreateElement("Module");
            newChild.SetAttribute("ModuleID", RolePowerKey.Create(type.ModuleTypeID).ToKey);
            newChild.SetAttribute("ModuleName", type.ModuleTypeName);
            xmlDocSource.AppendChild(newChild);
            this.CreatePowerChildElement(xmlDocSource, "", type.ModuleTypeID, Guid.Empty.ToString(), false, "", newChild, list, null);
            return xmlDocSource.InnerXml;
        }

        public string GetPowerTreexXmlText(string moduleTypeID, string AuthorizeGroupID)
        {
            new UserRule();
            XmlDocument xmlDocSource = new XmlDocument();
            WTF.Power.Entity.Sys_ModuleType type = this.CurrentEntities.sys_moduletype.First<WTF.Power.Entity.Sys_ModuleType>(s => s.ModuleTypeID == moduleTypeID);
            XmlElement newChild = xmlDocSource.CreateElement("Module");
            newChild.SetAttribute("ModuleID", RolePowerKey.Create(type.ModuleTypeID).ToKey);
            newChild.SetAttribute("ModuleName", type.ModuleTypeName);
            xmlDocSource.AppendChild(newChild);
            List<WTF.Power.Entity.Sys_Module> list = this.CurrentEntities.GetAuthorizeGroupPowerModule(AuthorizeGroupID, moduleTypeID).ToList<WTF.Power.Entity.Sys_Module>();
            List<string> editModuleModuleList = (from s in list
                where s.IsEdit
                select s.ModuleCode).ToList<string>();
            List<WTF.Power.Entity.Sys_Module> list2 = new List<WTF.Power.Entity.Sys_Module>();
            if (editModuleModuleList.Count > 0)
            {
                list2 = (from s in this.CurrentEntities.sys_module
                    where editModuleModuleList.Contains(s.ModuleCode) && !s.IsEdit
                    select s).ToList<WTF.Power.Entity.Sys_Module>();
            }
            this.CreatePowerChildElement(xmlDocSource, AuthorizeGroupID, type.ModuleTypeID, Guid.Empty.ToString(), false, "", newChild, list, list2);
            return xmlDocSource.InnerXml;
        }

        public void SaveChanges()
        {
            this.objCurrentEntities.SaveChanges();
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

