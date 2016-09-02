namespace WTF.Power
{
    using MySql.Data.MySqlClient;
    using WTF.Framework;
    using WTF.Logging;
    using WTF.Power.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;

    public class PowerCotePower
    {
        public PowerCotePower(Sys_ModuleCote objSys_ModuleCote, string moduleTypeID)
        {
            this.CoteTableName = objSys_ModuleCote.CoteTableName;
            this.IDName = objSys_ModuleCote.IDName;
            this.Name = objSys_ModuleCote.Name;
            this.ConnectionStringName = objSys_ModuleCote.ConnectionStringName;
            this.IDDataType = objSys_ModuleCote.IDDataType;
            this.IsParentUrl = objSys_ModuleCote.IsParentUrl;
            this.ModuleTypeID = moduleTypeID;
            this.DefalutCondition = objSys_ModuleCote.Condtion;
            this.DefalutCondition = this.DefalutCondition.Replace("{ModuleTypeID}", this.ModuleTypeID);
            this.SortExpression = objSys_ModuleCote.SortExpression;
        }

        private List<CoteInfo> ConvertCoteInfo(MySqlDataReader reader)
        {
            List<CoteInfo> list = new List<CoteInfo>();
            try
            {
                while (reader.Read())
                {
                    CoteInfo item = new CoteInfo {
                        ParentID = ""
                    };
                    if (reader[this.IDName] != DBNull.Value)
                    {
                        item.ID = Convert.ToString(reader[this.IDName]);
                    }
                    if (reader[this.Name] != DBNull.Value)
                    {
                        item.Name = Convert.ToString(reader[this.Name]);
                    }
                    list.Add(item);
                }
            }
            catch (Exception exception)
            {
                LogHelper<LogModuleType>.Write(LogModuleType.ModuleLog, exception, "");
                return list;
            }
            finally
            {
                reader.Close();
            }
            return list;
        }

        private void CreateEachModeuleXmlElement(XmlDocument xmlDocSource, string coteID, string CoteModuleID, Sys_Module objSys_Module, XmlElement objXmlElement, List<Sys_Module> objSys_ModuleList, List<sys_authorizegrouppower> objCoteRolePowerList, List<Sys_Module> AddSys_ModuleList)
        {
            Func<Sys_Module, bool> predicate = null;
            Func<Sys_Module, bool> func2 = null;
            Func<sys_authorizegrouppower, bool> func3 = null;
            string SearchModuleID = objSys_Module.ModuleID;
            if (objSys_Module.IsEdit)
            {
                if (predicate == null)
                {
                    predicate = s => (!s.IsEdit && (s.ParentModuleID == objSys_Module.ParentModuleID)) && (s.ModuleCode == objSys_Module.ModuleCode);
                }
                Sys_Module module = objSys_ModuleList.FirstOrDefault<Sys_Module>(predicate);
                if (module != null)
                {
                    SearchModuleID = module.ModuleID;
                }
                else if (AddSys_ModuleList != null)
                {
                    if (func2 == null)
                    {
                        func2 = s => (!s.IsEdit && (s.ParentModuleID == objSys_Module.ParentModuleID)) && (s.ModuleCode == objSys_Module.ModuleCode);
                    }
                    module = AddSys_ModuleList.FirstOrDefault<Sys_Module>(func2);
                    if (module != null)
                    {
                        SearchModuleID = module.ModuleID;
                    }
                }
            }
            List<string> list = null;
            if (objCoteRolePowerList != null)
            {
                if (func3 == null)
                {
                    func3 = s => s.CoteID == coteID;
                }
                list = (from s in objCoteRolePowerList.Where<sys_authorizegrouppower>(func3) select s.ModuleID).ToList<string>();
            }
            foreach (Sys_Module module2 in from p in objSys_ModuleList
                where p.ParentModuleID == SearchModuleID
                orderby p.SortIndex
                select p)
            {
                if ((list == null) || list.Contains(module2.ModuleID))
                {
                    XmlElement newChild = xmlDocSource.CreateElement("Module");
                    newChild.SetAttribute("ModuleID", RolePowerKey.Create(CoteModuleID, coteID, module2.ModuleID, false).ToKey);
                    newChild.SetAttribute("ModuleName", module2.ModuleName);
                    objXmlElement.AppendChild(newChild);
                    this.CreateEachModeuleXmlElement(xmlDocSource, coteID, CoteModuleID, module2, newChild, objSys_ModuleList, objCoteRolePowerList, AddSys_ModuleList);
                }
            }
        }

        public List<CoteInfo> GetAllCoteInfoList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetTabeleSql);
            if (!string.IsNullOrWhiteSpace(this.DefalutCondition))
            {
                builder.Append(" where " + this.DefalutCondition + " ");
            }
            if (!string.IsNullOrWhiteSpace(this.SortExpression))
            {
                builder.Append(" order by " + this.SortExpression);
            }
            MySqlDataReader reader = new WTF.Framework.MySqlHelper(this.ConnectionStringName).ExecuteReader(builder.ToString(), new MySqlParameter[0]);
            return this.ConvertCoteInfo(reader);
        }

        protected List<sys_authorizegrouppower> GetAuthorizeGroupCotePower(string AuthorizeGroupID, string moduleID)
        {
            string format = " SELECT *  FROM  sys_authorizegrouppower  WHERE AuthorizeGroupID='{0}'  AND CoteModuleID='{1}'";
            UserRule rule = new UserRule();
            return rule.CurrentEntities.ExecuteStoreQuery<sys_authorizegrouppower>(string.Format(format, AuthorizeGroupID, moduleID), new object[0]).ToList<sys_authorizegrouppower>();
        }

        public CoteInfo GetCoteInfoKeyID(string key)
        {
            return this.GetCoteInfoKeyList(key)[0];
        }

        public List<CoteInfo> GetCoteInfoKeyList(string key)
        {
            string str = "";
            if (this.IDDataType == 1)
            {
                str = this.IDName + " in (" + key + ")";
            }
            else
            {
                str = this.IDName + " in (" + key.ConvertStringID() + ")";
            }
            if (!string.IsNullOrWhiteSpace(this.DefalutCondition))
            {
                str = str + " and " + this.DefalutCondition;
            }
            WTF.Framework.MySqlHelper helper = new WTF.Framework.MySqlHelper(this.ConnectionStringName);
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetTabeleSql);
            if (!string.IsNullOrWhiteSpace(str))
            {
                builder.Append(" where " + str + " ");
            }
            if ((key.Split(new char[] { ',' }).Length > 1) && !string.IsNullOrWhiteSpace(this.SortExpression))
            {
                builder.Append(" order by " + this.SortExpression);
            }
            MySqlDataReader reader = helper.ExecuteReader(builder.ToString(), new MySqlParameter[0]);
            return this.ConvertCoteInfo(reader);
        }

        public List<CoteInfo> GetCotePowerList(UserInfo objUserInfo, string CoteModuleID)
        {
            if (objUserInfo.IsSuper)
            {
                return this.GetAllCoteInfoList();
            }
            List<Sys_RolePower> source = this.GetUserCoteRolePower(objUserInfo.UserID, CoteModuleID).ToList<Sys_RolePower>();
            if (source.Count == 0)
            {
                return null;
            }
            if (source.Any<Sys_RolePower>(s => s.IsCoteSupper))
            {
                return this.GetAllCoteInfoList();
            }
            return this.GetCoteInfoKeyList((from s in source select s.CoteID).ToList<string>().ConvertListToString<string>());
        }

        public XmlElement GetCotePowerMenuXmlElement(XmlDocument xmlDocSource, UserInfo objUserInfo, Sys_Module objModule)
        {
            List<CoteInfo> allCoteInfoList = null;
            if (objUserInfo.IsSuper)
            {
                allCoteInfoList = this.GetAllCoteInfoList();
            }
            else
            {
                List<Sys_RolePower> source = this.GetUserCoteRolePower(objUserInfo.UserID, objModule.ModuleID).ToList<Sys_RolePower>();
                if (source.Count == 0)
                {
                    return null;
                }
                if (source.Any<Sys_RolePower>(s => s.IsCoteSupper))
                {
                    allCoteInfoList = this.GetAllCoteInfoList();
                }
                else
                {
                    allCoteInfoList = this.GetCoteInfoKeyList((from s in source select s.CoteID).ToList<string>().ConvertListToString<string>());
                }
            }
            string format = "javascript:opentreemodule('{0}','{1}','{2}','{3}');";
            string str2 = objModule.CommandArgument + ((objModule.CommandArgument.IndexOf("?") >= 0) ? "&" : "?") + this.IDName + "={0}&CoteID={0}&CoteModuleID=" + objModule.ModuleID + "&PowerName={1}";
            XmlElement element = xmlDocSource.CreateElement("Module");
            element.SetAttribute("ModuleID", objModule.ModuleID.ToString());
            element.SetAttribute("ModuleName", objModule.ModuleName);
            element.SetAttribute("ToolTip", objModule.ToolTip);
            element.SetAttribute("ImageUrl", objModule.ImageUrl);
            element.SetAttribute("ModuleLevel", objModule.ModuleLevel.ToString());
            element.SetAttribute("NavigateUrl", "");
            foreach (CoteInfo info in allCoteInfoList)
            {
                XmlElement newChild = xmlDocSource.CreateElement("Module");
                newChild.SetAttribute("ModuleID", info.ID.ToString());
                newChild.SetAttribute("ModuleName", info.Name);
                newChild.SetAttribute("ToolTip", info.Name);
                newChild.SetAttribute("ImageUrl", objModule.ImageUrl);
                newChild.SetAttribute("ModuleLevel", (objModule.ModuleLevel + 1).ToString());
                newChild.SetAttribute("NavigateUrl", string.Format(format, new object[] { objModule.ModuleID.ToString() + info.ID, objModule.ImageUrl, string.Format(str2, info.ID, info.Name).EncryptModuleQuery(), info.Name }));
                element.AppendChild(newChild);
            }
            return element;
        }

        public XmlElement GetCotePowerXmlElement(XmlDocument xmlDocSource, string AuthorizeGroupID, Sys_Module objCoteModule, List<Sys_Module> objSys_ModuleList, List<Sys_Module> AddSys_ModuleList)
        {
            List<CoteInfo> allCoteInfoList = null;
            List<sys_authorizegrouppower> source = null;
            allCoteInfoList = this.GetAllCoteInfoList();
            if (AuthorizeGroupID.IsNull())
            {
                allCoteInfoList = this.GetAllCoteInfoList();
            }
            else
            {
                source = this.GetAuthorizeGroupCotePower(AuthorizeGroupID, objCoteModule.ModuleID);
                if (source.Count == 0)
                {
                    return null;
                }
                if (source.Any<sys_authorizegrouppower>(s => s.IsCoteSupper))
                {
                    allCoteInfoList = this.GetAllCoteInfoList();
                    source = null;
                    ModuleRule rule = new ModuleRule();
                    string ModuleIDPath = rule.Sys_Module.FirstOrDefault<Sys_Module>(s => (s.ModuleID == objCoteModule.ModuleID)).ModuleIDPath;
                    objSys_ModuleList.RemoveAll(s => s.ModuleIDPath.StartsWith(ModuleIDPath));
                    objSys_ModuleList.AddRange(from s in rule.Sys_Module
                        where (s.ModuleIDPath.StartsWith(ModuleIDPath) && s.IsPower) && !s.IsSupperPower
                        select s);
                }
                else
                {
                    allCoteInfoList = this.GetCoteInfoKeyList((from s in source select s.CoteID).Distinct<string>().ToList<string>().ConvertListToString<string>());
                }
            }
            XmlElement element = xmlDocSource.CreateElement("Module");
            element.SetAttribute("ModuleID", RolePowerKey.Create(objCoteModule.ModuleID).ToKey);
            element.SetAttribute("ModuleName", objCoteModule.ModuleName);
            if (AuthorizeGroupID.IsNull() || (source == null))
            {
                XmlElement newChild = xmlDocSource.CreateElement("Module");
                newChild.SetAttribute("ModuleID", RolePowerKey.Create(objCoteModule.ModuleID, objCoteModule.ModuleID, objCoteModule.ModuleID, false, true).ToKey);
                newChild.SetAttribute("ModuleName", "***" + objCoteModule.ModuleName + "***所有权限");
                element.AppendChild(newChild);
            }
            foreach (CoteInfo info in allCoteInfoList)
            {
                XmlElement element3 = xmlDocSource.CreateElement("Module");
                element3.SetAttribute("ModuleID", RolePowerKey.Create(objCoteModule.ModuleID, info.ID, objCoteModule.ModuleID, false).ToKey);
                element3.SetAttribute("ModuleName", info.Name);
                element.AppendChild(element3);
                this.CreateEachModeuleXmlElement(xmlDocSource, info.ID, objCoteModule.ModuleID, objCoteModule, element3, objSys_ModuleList, source, AddSys_ModuleList);
            }
            return element;
        }

        protected List<Sys_RolePower> GetUserCoteRolePower(string userID, string moduleID)
        {
            string format = " SELECT  Sys_RolePower.*\r\n                              FROM  Sys_RoleUser \r\n                             ,   Sys_RolePower WHERE Sys_RoleUser.UserID='{0}'   \r\n                             AND Sys_RolePower.CoteModuleID='{1}' and Sys_RoleUser.RoleID=Sys_RolePower.RoleID";
            UserRule rule = new UserRule();
            return rule.CurrentEntities.ExecuteStoreQuery<Sys_RolePower>(string.Format(format, userID, moduleID), new object[0]).ToList<Sys_RolePower>();
        }

        public string ConnectionStringName { get; set; }

        public string CoteTableName { get; set; }

        public string DefalutCondition { get; set; }

        public string GetTabeleSql
        {
            get
            {
                return (" select DISTINCT " + this.IDName + "," + this.Name + " from " + this.CoteTableName + " ");
            }
        }

        public int IDDataType { get; set; }

        public string IDName { get; set; }

        public bool IsParentUrl { get; set; }

        public string ModuleTypeID { get; set; }

        public string Name { get; set; }

        public string SortExpression { get; set; }
    }
}

