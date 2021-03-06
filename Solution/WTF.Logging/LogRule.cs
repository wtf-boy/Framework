﻿namespace WTF.Logging
{
    using WTF.Framework;
    using WTF.Logging.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using System.Xml;

    public class LogRule
    {
        private LogEntities objCurrentEntities = null;

        public void ApplicationMove(int ApplicationID, int tagApplicationID)
        {
            string iDPath = this.CurrentEntities.loger_application.First<WTF.Logging.Entity.loger_application>(s => (s.ApplicationID == ApplicationID)).IDPath;
            ObjectQuery<WTF.Logging.Entity.loger_application> query = this.CurrentEntities.loger_application.Where("it.IDPath like '" + iDPath + "%'", new ObjectParameter[0]);
            WTF.Logging.Entity.loger_application _application2 = this.CurrentEntities.loger_application.FirstOrDefault<WTF.Logging.Entity.loger_application>(s => s.ApplicationID == tagApplicationID);
            string newValue = (_application2 == null) ? ("," + iDPath.ToString() + ",") : (_application2.IDPath + ApplicationID.ToString() + ",");
            foreach (WTF.Logging.Entity.loger_application _application3 in query)
            {
                _application3.IDPath = _application3.IDPath.Replace(iDPath, newValue);
                if (_application3.ApplicationID == ApplicationID)
                {
                    _application3.ParentID = tagApplicationID;
                }
            }
            this.SaveChanges();
        }

        private void CreateChildApplicationXmlElement(XmlDocument xmlDocSource, int ApplicationID, XmlElement objXmlElement, List<WTF.Logging.Entity.loger_application> objloger_applicationList, string url)
        {
            foreach (WTF.Logging.Entity.loger_application _application in from s in objloger_applicationList
                where s.ParentID == ApplicationID
                orderby s.SortIndex
                select s)
            {
                XmlElement newChild = xmlDocSource.CreateElement("TreeNodeMember");
                newChild.SetAttribute("TreeNodeID", _application.ApplicationID.ToString());
                newChild.SetAttribute("TreeNodeName", _application.ApplicationName);
                if (!string.IsNullOrWhiteSpace(url))
                {
                    newChild.SetAttribute("NavigateUrl", string.Format(url + "ApplicationID={0}", _application.ApplicationID.ToString()).EncryptModuleQuery());
                }
                objXmlElement.AppendChild(newChild);
                this.CreateChildApplicationXmlElement(xmlDocSource, _application.ApplicationID, newChild, objloger_applicationList, url);
            }
        }

        public int DeleteApplication(int applicationID)
        {
            WTF.Logging.Entity.loger_application _application = this.loger_application.FirstOrDefault<WTF.Logging.Entity.loger_application>(s => s.ApplicationID == applicationID);
            string iDPath = _application.IDPath;
            List<int> list = (from s in this.loger_application.Where("it.IDPath like '" + iDPath + "%'", new ObjectParameter[0]) select s.ApplicationID).ToList<int>();
            this.loger_loging.DeleteDataSql<WTF.Logging.Entity.loger_loging>("ApplicationID in (" + list.ConvertListToString<int>() + ")", new object[0]);
            this.loger_application.DeleteDataSql<WTF.Logging.Entity.loger_application>("IDPath like '" + iDPath + "%'", new object[0]);
            return _application.ParentID;
        }

        public string GetApplicationMoveTreexXml(int ApplicationID)
        {
            List<WTF.Logging.Entity.loger_application> source = this.CurrentEntities.loger_application.ToList<WTF.Logging.Entity.loger_application>();
            WTF.Logging.Entity.loger_application _application = source.FirstOrDefault<WTF.Logging.Entity.loger_application>(s => s.ParentID == 0);
            XmlDocument xmlDocSource = new XmlDocument();
            XmlElement newChild = xmlDocSource.CreateElement("TreeNodeMember");
            newChild.SetAttribute("TreeNodeID", _application.ApplicationID.ToString());
            newChild.SetAttribute("TreeNodeName", _application.ApplicationName);
            xmlDocSource.AppendChild(newChild);
            WTF.Logging.Entity.loger_application objMoveloger_application = source.FirstOrDefault<WTF.Logging.Entity.loger_application>(s => s.ApplicationID == ApplicationID);
            source = (from s in source
                where !s.IDPath.StartsWith(objMoveloger_application.IDPath)
                select s).ToList<WTF.Logging.Entity.loger_application>();
            this.CreateChildApplicationXmlElement(xmlDocSource, _application.ApplicationID, newChild, source, "");
            return xmlDocSource.InnerXml;
        }

        public string GetApplicationXmlText(string url)
        {
            if (url.IndexOf('?') < 0)
            {
                url = url + "?";
            }
            else
            {
                url = url + "&";
            }
            List<WTF.Logging.Entity.loger_application> source = this.CurrentEntities.loger_application.ToList<WTF.Logging.Entity.loger_application>();
            WTF.Logging.Entity.loger_application _application = source.FirstOrDefault<WTF.Logging.Entity.loger_application>(s => s.ParentID == 0);
            XmlDocument xmlDocSource = new XmlDocument();
            XmlElement newChild = xmlDocSource.CreateElement("TreeNodeMember");
            newChild.SetAttribute("TreeNodeID", _application.ApplicationID.ToString());
            newChild.SetAttribute("TreeNodeName", _application.ApplicationName);
            newChild.SetAttribute("NavigateUrl", string.Format(url + "ApplicationID={0}", _application.ApplicationID.ToString()).EncryptModuleQuery());
            xmlDocSource.AppendChild(newChild);
            this.CreateChildApplicationXmlElement(xmlDocSource, _application.ApplicationID, newChild, source, url);
            return xmlDocSource.InnerXml;
        }

        public WTF.Logging.Entity.loger_application GetCacheApplication(string ApplicationCode)
        {
            string childKey = "Log" + ApplicationCode;
            WTF.Logging.Entity.loger_application fromCache = CacheHelper.GetFromCache<WTF.Logging.Entity.loger_application>(WTF.Framework.CacheType.Logging.ToString(), childKey);
            if (fromCache == null)
            {
                this.CurrentEntities.loger_application.MergeOption = MergeOption.NoTracking;
                fromCache = this.loger_application.Where("it.ApplicationCode='" + ApplicationCode + "'", new ObjectParameter[0]).Include("loger_category").FirstOrDefault<WTF.Logging.Entity.loger_application>();
                if (fromCache == null)
                {
                    return null;
                }
                fromCache.AddToCache(WTF.Framework.CacheType.Logging.ToString(), childKey, ConfigHelper.GetIntValue("LogCacheTime", 10));
                return fromCache;
            }
            return fromCache;
        }

        public string GetCategoryTreeXmlText()
        {
            XmlDocument document = new XmlDocument();
            XmlElement newChild = document.CreateElement("LogModule");
            newChild.SetAttribute("TreeNodeID", Guid.NewGuid().ToString());
            newChild.SetAttribute("TreeNodeName", "日志程序管理");
            newChild.SetAttribute("NavigateUrl", "../../ServiceLayer/Loging/ApplicationList.aspx");
            newChild.SetAttribute("TreeLevel", "Module");
            document.AppendChild(newChild);
            XmlElement element2 = document.CreateElement("LogModule");
            element2.SetAttribute("TreeNodeID", Guid.NewGuid().ToString());
            element2.SetAttribute("TreeNodeName", "日志模块管理");
            element2.SetAttribute("NavigateUrl", "../../ServiceLayer/Loging/ModuleTypeList.aspx");
            element2.SetAttribute("TreeLevel", "Module");
            newChild.AppendChild(element2);
            XmlElement element3 = document.CreateElement("LogModule");
            element3.SetAttribute("TreeNodeID", Guid.NewGuid().ToString());
            element3.SetAttribute("TreeNodeName", "全部程序日志");
            element3.SetAttribute("NavigateUrl", "../../ServiceLayer/Loging/TrackLogingList.aspx");
            element3.SetAttribute("TreeLevel", "Module");
            newChild.AppendChild(element3);
            foreach (WTF.Logging.Entity.loger_application _application in this.CurrentEntities.loger_application.Include("loger_category"))
            {
                XmlElement element4 = document.CreateElement("LogModule");
                element4.SetAttribute("TreeNodeID", _application.ApplicationID.ToString());
                element4.SetAttribute("TreeNodeName", _application.ApplicationName);
                element4.SetAttribute("NavigateUrl", ("../../ServiceLayer/Loging/LogList.aspx?ApplicationID=" + _application.ApplicationID.ToString()).EncryptModuleQuery());
                element4.SetAttribute("TreeLevel", "ModuleType");
                newChild.AppendChild(element4);
                foreach (WTF.Logging.Entity.loger_category _category in from s in _application.loger_category
                    orderby s.CategoryName
                    select s)
                {
                    XmlElement element5 = document.CreateElement("LogModule");
                    element5.SetAttribute("TreeNodeID", _category.CategoryID.ToString());
                    element5.SetAttribute("TreeNodeName", _category.CategoryName);
                    element5.SetAttribute("NavigateUrl", ("../../ServiceLayer/Loging/LogList.aspx?ApplicationID=" + _application.ApplicationID.ToString() + "&CategoryID=" + _category.CategoryID.ToString() + "&CategoryTypeCode=" + _category.CategoryTypeCode.ToString()).EncryptModuleQuery());
                    element5.SetAttribute("TreeLevel", "Category");
                    element4.AppendChild(element5);
                }
            }
            return document.InnerXml;
        }

        public void InsertApplication(WTF.Logging.Entity.loger_application objloger_application)
        {
            objloger_application.ApplicationName.CheckIsNull("请输入程序名称", LogModuleType.LogManager);
            objloger_application.ApplicationCode.CheckIsNull("请输入程序代码", LogModuleType.LogManager);
            if (this.loger_application.Any<WTF.Logging.Entity.loger_application>(s => s.ApplicationCode == objloger_application.ApplicationCode))
            {
                SysAssert.InfoHintAssert("输入的程序代码已经存在");
            }
            objloger_application.SortIndex = 0;
            objloger_application.IDPath = "";
            objloger_application.CreateDate = DateTime.Now;
            this.CurrentEntities.AddTologer_application(objloger_application);
            this.CurrentEntities.SaveChanges();
            WTF.Logging.Entity.loger_application _application = this.loger_application.FirstOrDefault<WTF.Logging.Entity.loger_application>(s => s.ApplicationID == objloger_application.ParentID);
            objloger_application.SortIndex = objloger_application.ApplicationID;
            objloger_application.IDPath = _application.IDPath + objloger_application.ApplicationID + ",";
            List<EnumParameter> enumMembers = typeof(LogCategory).GetEnumMembers();
            foreach (EnumParameter parameter in enumMembers)
            {
                WTF.Logging.Entity.loger_category _category = new WTF.Logging.Entity.loger_category {
                    ApplicationID = objloger_application.ApplicationID,
                    CategoryTypeCode = parameter.Key,
                    CategoryName = parameter.Description,
                    LogWriteType = "1"
                };
                this.CurrentEntities.AddTologer_category(_category);
            }
            this.CurrentEntities.SaveChanges();
        }

        public void InsertCategory(WTF.Logging.Entity.loger_category objloger_category)
        {
            objloger_category.CategoryTypeCode.CheckIsNull("请输入日志类型代码", LogModuleType.LogManager);
            objloger_category.CategoryName.CheckIsNull("请输入日志类型名称", LogModuleType.LogManager);
            if (this.loger_category.Any<WTF.Logging.Entity.loger_category>(s => (s.ApplicationID == objloger_category.ApplicationID) && (s.CategoryTypeCode == objloger_category.CategoryTypeCode)))
            {
                SysAssert.ArgumentAssert<LogModuleType>("输入的日志类型代码已经存在", LogModuleType.LogManager);
            }
            this.CurrentEntities.AddTologer_category(objloger_category);
            this.CurrentEntities.SaveChanges();
        }

        public void Insertmoduletype(WTF.Logging.Entity.loger_moduletype objloger_moduletype)
        {
            objloger_moduletype.ModuleTypeCode.CheckIsNull<string>("请输入模块代码", "LogManager");
            objloger_moduletype.ModuleTypeName.CheckIsNull<string>("请输入模块名称", "LogManager");
            if (this.loger_moduletype.Any<WTF.Logging.Entity.loger_moduletype>(s => s.ModuleTypeCode == objloger_moduletype.ModuleTypeCode))
            {
                SysAssert.ArgumentAssert<LogModuleType>("输入的模块代码已经存在", LogModuleType.LogManager);
            }
            this.CurrentEntities.AddTologer_moduletype(objloger_moduletype);
            this.CurrentEntities.SaveChanges();
        }

        public void SaveChanges()
        {
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateApplication(WTF.Logging.Entity.loger_application objloger_application)
        {
            objloger_application.ApplicationName.CheckIsNull("请输入程序名称", LogModuleType.LogManager);
            objloger_application.ApplicationCode.CheckIsNull("请输入程序代码", LogModuleType.LogManager);
            if (this.loger_application.Any<WTF.Logging.Entity.loger_application>(s => (s.ApplicationID != objloger_application.ApplicationID) && (s.ApplicationCode == objloger_application.ApplicationCode)))
            {
                SysAssert.ArgumentAssert<LogModuleType>("输入的程序代码已经存在", LogModuleType.LogManager);
            }
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateApplicationCategory(int applicationID)
        {
            List<EnumParameter> enumMembers = typeof(LogCategory).GetEnumMembers();
            string iDPath = this.CurrentEntities.loger_application.FirstOrDefault<WTF.Logging.Entity.loger_application>(s => (s.ApplicationID == applicationID)).IDPath;
            using (List<int>.Enumerator enumerator = (from s in this.CurrentEntities.loger_application.Where("it.IDPath like '" + iDPath + "%'", new ObjectParameter[0]) select s.ApplicationID).ToList<int>().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int ApplicationIDItem = enumerator.Current;
                    List<string> list2 = (from s in this.CurrentEntities.loger_category
                        where s.ApplicationID == ApplicationIDItem
                        select s.CategoryTypeCode).ToList<string>();
                    foreach (EnumParameter parameter in enumMembers)
                    {
                        if (!list2.Contains(parameter.Key))
                        {
                            WTF.Logging.Entity.loger_category _category = new WTF.Logging.Entity.loger_category {
                                CategoryTypeCode = parameter.Key,
                                CategoryName = parameter.Description,
                                ApplicationID = ApplicationIDItem,
                                LogWriteType = "1"
                            };
                            this.CurrentEntities.AddTologer_category(_category);
                        }
                    }
                    this.CurrentEntities.SaveChanges();
                }
            }
        }

        public void Updatemoduletype(WTF.Logging.Entity.loger_moduletype objloger_moduletype)
        {
            objloger_moduletype.ModuleTypeCode.CheckIsNull<string>("请输入模块代码", "LogManager");
            objloger_moduletype.ModuleTypeName.CheckIsNull<string>("请输入模块名称", "LogManager");
            if (this.loger_moduletype.Any<WTF.Logging.Entity.loger_moduletype>(s => (s.ModuleTypeID != objloger_moduletype.ModuleTypeID) && (s.ModuleTypeCode == objloger_moduletype.ModuleTypeCode)))
            {
                SysAssert.ArgumentAssert<LogModuleType>("输入的模块代码已经存在", LogModuleType.LogManager);
            }
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateModuleType()
        {
            List<EnumParameter> enumMembers = typeof(LogModuleType).GetEnumMembers();
            List<string> list2 = (from s in this.CurrentEntities.loger_moduletype select s.ModuleTypeCode).ToList<string>();
            foreach (EnumParameter parameter in enumMembers)
            {
                if (!list2.Contains(parameter.Key))
                {
                    WTF.Logging.Entity.loger_moduletype _moduletype = new WTF.Logging.Entity.loger_moduletype {
                        ModuleTypeCode = parameter.Key,
                        ModuleTypeName = parameter.Description
                    };
                    this.CurrentEntities.AddTologer_moduletype(_moduletype);
                }
            }
            this.CurrentEntities.SaveChanges();
        }

        public LogEntities CurrentEntities
        {
            get
            {
                if (this.objCurrentEntities == null)
                {
                    this.objCurrentEntities = new LogEntities(EntitiesHelper.GetConnectionString<LogEntities>());
                }
                return this.objCurrentEntities;
            }
        }

        public ObjectQuery<WTF.Logging.Entity.loger_application> loger_application
        {
            get
            {
                return this.CurrentEntities.loger_application;
            }
        }

        public ObjectQuery<WTF.Logging.Entity.loger_category> loger_category
        {
            get
            {
                return this.CurrentEntities.loger_category;
            }
        }

        public ObjectQuery<WTF.Logging.Entity.loger_loging> loger_loging
        {
            get
            {
                return this.CurrentEntities.loger_loging;
            }
        }

        public ObjectQuery<WTF.Logging.Entity.loger_moduletype> loger_moduletype
        {
            get
            {
                return this.CurrentEntities.loger_moduletype;
            }
        }

        public ObjectQuery<WTF.Logging.Entity.loger_operationhistory> loger_operationhistory
        {
            get
            {
                return this.CurrentEntities.loger_operationhistory;
            }
        }

        public ObjectQuery<WTF.Logging.Entity.loger_operationloging> loger_operationloging
        {
            get
            {
                return this.CurrentEntities.loger_operationloging;
            }
        }
    }
}

