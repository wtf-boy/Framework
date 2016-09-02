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

    public class PowerCoteTreePower
    {
        private string CurrentAuthorizeGroupID = string.Empty;
        private bool CurrentIsSuper;

        public PowerCoteTreePower(Sys_ModuleCote objSys_ModuleCote, string moduleTypeID)
        {
            this.CoteTableName = objSys_ModuleCote.CoteTableName;
            this.IDName = objSys_ModuleCote.IDName;
            this.ParentIDName = objSys_ModuleCote.ParentIDName;
            this.IDPathName = objSys_ModuleCote.IDPathName;
            this.Name = objSys_ModuleCote.Name;
            this.ConnectionStringName = objSys_ModuleCote.ConnectionStringName;
            this.RootIDValue = objSys_ModuleCote.RootIDValue.ToLower();
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
                    CoteInfo item = new CoteInfo();
                    if (reader[this.IDName] != DBNull.Value)
                    {
                        item.ID = Convert.ToString(reader[this.IDName]);
                    }
                    if (reader[this.ParentIDName] != DBNull.Value)
                    {
                        item.ParentID = Convert.ToString(reader[this.ParentIDName]);
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

        private void CreateCoteChildMenuPowerXmlElement(XmlDocument xmlDocSource, string ParentID, Sys_Module objCoteModule, XmlElement objXmlElement, List<CoteInfo> objCoteInfoList, List<Sys_Module> objSys_ModuleList, List<sys_authorizegrouppower> objCoteRolePowerList, List<Sys_Module> AddSys_ModuleList)
        {
            using (IEnumerator<CoteInfo> enumerator = (from s in objCoteInfoList
                where s.ParentID == ParentID
                select s).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Func<CoteInfo, bool> predicate = null;
                    CoteInfo objCoteInfo = enumerator.Current;
                    XmlElement newChild = xmlDocSource.CreateElement("Module");
                    newChild.SetAttribute("ModuleID", RolePowerKey.Create(objCoteModule.ModuleID, objCoteInfo.ID, objCoteModule.ModuleID, false).ToKey);
                    newChild.SetAttribute("ModuleName", objCoteInfo.Name);
                    objXmlElement.AppendChild(newChild);
                    bool isCoteSupper = this.ParentIsCoteSupper(objCoteInfo, objCoteInfoList, objCoteRolePowerList);
                    if (predicate == null)
                    {
                        predicate = s => s.ParentID == objCoteInfo.ID;
                    }
                    if (objCoteInfoList.Any<CoteInfo>(predicate))
                    {
                        if (this.CurrentAuthorizeGroupID.IsNull() || isCoteSupper)
                        {
                            XmlElement element2 = xmlDocSource.CreateElement("Module");
                            element2.SetAttribute("ModuleID", RolePowerKey.Create(objCoteModule.ModuleID, objCoteInfo.ID, objCoteModule.ModuleID, false, true).ToKey);
                            element2.SetAttribute("ModuleName", "***" + objCoteInfo.Name + "***所有权限");
                            newChild.AppendChild(element2);
                        }
                        if (this.IsParentUrl)
                        {
                            this.CreateEachModeuleXmlElement(xmlDocSource, objCoteInfo.ID, objCoteModule.ModuleID, isCoteSupper, objCoteModule, newChild, objSys_ModuleList, objCoteRolePowerList, AddSys_ModuleList);
                        }
                    }
                    else
                    {
                        this.CreateEachModeuleXmlElement(xmlDocSource, objCoteInfo.ID, objCoteModule.ModuleID, isCoteSupper, objCoteModule, newChild, objSys_ModuleList, objCoteRolePowerList, AddSys_ModuleList);
                    }
                    this.CreateCoteChildMenuPowerXmlElement(xmlDocSource, objCoteInfo.ID, objCoteModule, newChild, objCoteInfoList, objSys_ModuleList, objCoteRolePowerList, AddSys_ModuleList);
                }
            }
        }

        private void CreateCoteChildMenuPowerXmlElement(XmlDocument xmlDocSource, bool isCoteSuper, List<Sys_RolePower> objCotePowerList, string ParentID, XmlElement objXmlElement, List<CoteInfo> objCoteInfoList, string coteUrl, int ModuleLevel, Sys_Module objModule)
        {
            Func<Sys_RolePower, bool> predicate = null;
            bool flag = true;
            string format = "javascript:opentreemodule('{0}','{1}','{2}','{3}');";
            using (IEnumerator<CoteInfo> enumerator = (from s in objCoteInfoList
                where s.ParentID == ParentID
                select s).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Func<CoteInfo, bool> func = null;
                    CoteInfo objCoteInfo = enumerator.Current;
                    if (flag)
                    {
                        if (!this.IsParentUrl)
                        {
                            objXmlElement.SetNodeAttribute("NavigateUrl", "");
                        }
                        else if (!isCoteSuper)
                        {
                            if (predicate == null)
                            {
                                predicate = s => s.CoteID == ParentID;
                            }
                            if (objCotePowerList.Where<Sys_RolePower>(predicate).Count<Sys_RolePower>() < 2)
                            {
                                objXmlElement.SetNodeAttribute("NavigateUrl", "");
                            }
                        }
                        ModuleLevel++;
                        flag = false;
                    }
                    XmlElement newChild = xmlDocSource.CreateElement("Module");
                    newChild.SetAttribute("ModuleID", objCoteInfo.ID.ToString());
                    newChild.SetAttribute("ModuleName", objCoteInfo.Name);
                    newChild.SetAttribute("ToolTip", objCoteInfo.Name);
                    newChild.SetAttribute("ImageUrl", objModule.ImageUrl);
                    newChild.SetAttribute("ModuleLevel", ModuleLevel.ToString());
                    object[] args = new object[4];
                    args[0] = objModule.ModuleID.ToString() + objCoteInfo.ID;
                    args[1] = objModule.ImageUrl;
                    object[] objArray2 = new object[4];
                    objArray2[0] = objCoteInfo.ID;
                    objArray2[1] = objCoteInfo.ParentID;
                    if (func == null)
                    {
                        func = s => s.ParentID == objCoteInfo.ID;
                    }
                    objArray2[2] = objCoteInfoList.Any<CoteInfo>(func) ? "1" : "0";
                    objArray2[3] = objCoteInfo.Name;
                    args[2] = string.Format(coteUrl, objArray2).EncryptModuleQuery();
                    args[3] = objCoteInfo.Name;
                    newChild.SetAttribute("NavigateUrl", string.Format(format, args));
                    objXmlElement.AppendChild(newChild);
                    isCoteSuper = this.ParentIsCoteSupper(objCoteInfo, objCoteInfoList, objCotePowerList);
                    this.CreateCoteChildMenuPowerXmlElement(xmlDocSource, isCoteSuper, objCotePowerList, objCoteInfo.ID, newChild, objCoteInfoList, coteUrl, ModuleLevel, objModule);
                }
            }
        }

        private void CreateEachModeuleXmlElement(XmlDocument xmlDocSource, string coteID, string CoteModuleID, bool IsCoteSupper, Sys_Module objSys_Module, XmlElement objXmlElement, List<Sys_Module> objSys_ModuleList, List<sys_authorizegrouppower> objCoteRolePowerList, List<Sys_Module> AddSys_ModuleList)
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
            if ((objCoteRolePowerList != null) && !IsCoteSupper)
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
                    this.CreateEachModeuleXmlElement(xmlDocSource, coteID, CoteModuleID, IsCoteSupper, module2, newChild, objSys_ModuleList, objCoteRolePowerList, AddSys_ModuleList);
                }
            }
        }

        public List<CoteInfo> GetAllCoteInfoList()
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(this.IDPathName))
            {
                builder.Append(this.GetTabeleSql);
                if (!string.IsNullOrWhiteSpace(this.DefalutCondition))
                {
                    builder.Append(" where " + this.DefalutCondition + " ");
                }
                if (!string.IsNullOrWhiteSpace(this.SortExpression))
                {
                    builder.Append(" order by " + this.SortExpression);
                }
            }
            else
            {
                builder.AppendLine("SET @IDPath='';");
                string str = "";
                if (this.IDDataType == 1)
                {
                    str = this.IDName + "=" + this.RootIDValue;
                }
                else
                {
                    str = this.IDName + "='" + this.RootIDValue.Trim() + "'";
                }
                builder.AppendLine("SELECT " + this.IDPathName + " INTO @IDPath  FROM " + this.CoteTableName + " WHERE " + str + ";");
                builder.AppendLine(this.GetTabeleSql + " WHERE   " + this.IDPathName + " LIKE   CONCAT( @IDPath,'%') ");
                if (!string.IsNullOrWhiteSpace(this.DefalutCondition))
                {
                    builder.Append(" AND  " + this.DefalutCondition + " ");
                }
                if (!string.IsNullOrWhiteSpace(this.SortExpression))
                {
                    builder.Append(" order by " + this.SortExpression);
                }
            }
            MySqlDataReader reader = new WTF.Framework.MySqlHelper(this.ConnectionStringName).ExecuteReader(builder.ToString(), new MySqlParameter[0]);
            return this.ConvertCoteInfo(reader);
        }

        public List<CoteInfo> GetAllCoteInfoList(string RootIDValue)
        {
            if ((this.IDDataType == 1) && !RootIDValue.IsMatch(@"\d+"))
            {
                return new List<CoteInfo>();
            }
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(this.IDPathName))
            {
                builder.Append(this.GetTabeleSql);
                if (!string.IsNullOrWhiteSpace(this.DefalutCondition))
                {
                    builder.Append(" where " + this.DefalutCondition + " ");
                }
                if (!string.IsNullOrWhiteSpace(this.SortExpression))
                {
                    builder.Append(" order by " + this.SortExpression);
                }
            }
            else
            {
                builder.AppendLine("SET @IDPath='';");
                string str = "";
                if (this.IDDataType == 1)
                {
                    str = this.IDName + "=" + RootIDValue;
                }
                else
                {
                    str = this.IDName + "='" + RootIDValue.Trim() + "'";
                }
                builder.AppendLine("SELECT " + this.IDPathName + " INTO @IDPath  FROM " + this.CoteTableName + " WHERE " + str + ";");
                builder.AppendLine(this.GetTabeleSql + " WHERE   " + this.IDPathName + " LIKE   CONCAT( @IDPath,'%') ");
                if (!string.IsNullOrWhiteSpace(this.DefalutCondition))
                {
                    builder.Append(" AND  " + this.DefalutCondition + " ");
                }
                if (!string.IsNullOrWhiteSpace(this.SortExpression))
                {
                    builder.Append(" order by " + this.SortExpression);
                }
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
            if (string.IsNullOrWhiteSpace(key))
            {
                return new List<CoteInfo>();
            }
            string str = "";
            if (this.IDDataType == 1)
            {
                string str2 = "";
                foreach (string str3 in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (str3.IsMatch(@"\d+"))
                    {
                        str2 = str2 + str3 + ",";
                    }
                }
                str = this.IDName + " in (" + str2.TrimEndComma() + ")";
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
            List<CoteInfo> source = null;
            if (objUserInfo.IsSuper)
            {
                source = this.GetAllCoteInfoList();
            }
            else
            {
                List<Sys_RolePower> list2 = this.GetUserCoteRolePower(objUserInfo.UserID, CoteModuleID).ToList<Sys_RolePower>();
                if (list2.Count == 0)
                {
                    return null;
                }
                if (list2.Any<Sys_RolePower>(s => s.IsCoteSupper))
                {
                    source = new List<CoteInfo>();
                    foreach (string str in (from s in list2
                        where s.IsCoteSupper
                        select s.CoteID).Distinct<string>())
                    {
                        source.AddRange(this.GetAllCoteInfoList(str));
                    }
                    source.AddRange(this.GetCoteInfoKeyList((from s in list2
                        where !s.IsCoteSupper
                        select s.CoteID).Distinct<string>().ToList<string>().ConvertListToString<string>()));
                }
                else
                {
                    source = this.GetCoteInfoKeyList((from s in list2 select s.CoteID).ToList<string>().ConvertListToString<string>());
                }
            }
            return source.Distinct<CoteInfo>(new CoteInfoComparer()).ToList<CoteInfo>();
        }

        public XmlElement GetCotePowerMenuXmlElement(XmlDocument xmlDocSource, UserInfo objUserInfo, Sys_Module objModule)
        {
            List<CoteInfo> source = null;
            List<Sys_RolePower> list2 = null;
            this.CurrentIsSuper = objUserInfo.IsSuper;
            if (objUserInfo.IsSuper)
            {
                source = this.GetAllCoteInfoList();
            }
            else
            {
                list2 = this.GetUserCoteRolePower(objUserInfo.UserID, objModule.ModuleID).ToList<Sys_RolePower>();
                if (list2.Count == 0)
                {
                    return null;
                }
                if (list2.Any<Sys_RolePower>(s => s.IsCoteSupper))
                {
                    source = new List<CoteInfo>();
                    foreach (string str in (from s in list2
                        where s.IsCoteSupper
                        select s.CoteID).Distinct<string>())
                    {
                        source.AddRange(this.GetAllCoteInfoList(str));
                    }
                    source.AddRange(this.GetCoteInfoKeyList((from s in list2
                        where !s.IsCoteSupper
                        select s.CoteID).Distinct<string>().ToList<string>().ConvertListToString<string>()));
                }
                else
                {
                    source = this.GetCoteInfoKeyList((from s in list2 select s.CoteID).ToList<string>().ConvertListToString<string>());
                }
            }
            source = source.Distinct<CoteInfo>(new CoteInfoComparer()).ToList<CoteInfo>();
            CoteInfo objCoteInfo = source.FirstOrDefault<CoteInfo>(s => s.ID == this.RootIDValue);
            if (objCoteInfo == null)
            {
                return null;
            }
            string format = "javascript:opentreemodule('{0}','{1}','{2}','{3}');";
            string str3 = objModule.CommandArgument + ((objModule.CommandArgument.IndexOf("?") >= 0) ? "&" : "?") + this.IDName + "={0}&" + this.ParentIDName + "={1}&CoteIsParent={2}&CoteID={0}&CoteModuleID=" + objModule.ModuleID + "&PowerName={3}";
            XmlElement objXmlElement = xmlDocSource.CreateElement("Module");
            objXmlElement.SetAttribute("ModuleID", objCoteInfo.ID.ToString());
            objXmlElement.SetAttribute("ModuleName", objModule.ModuleName);
            objXmlElement.SetAttribute("ToolTip", objModule.ToolTip);
            objXmlElement.SetAttribute("ImageUrl", objModule.ImageUrl);
            objXmlElement.SetAttribute("ModuleLevel", objModule.ModuleLevel.ToString());
            objXmlElement.SetAttribute("NavigateUrl", string.Format(format, new object[] { objModule.ModuleID.ToString() + objCoteInfo.ID, objModule.ImageUrl, string.Format(str3, new object[] { objCoteInfo.ID, objCoteInfo.ParentID, source.Any<CoteInfo>(s => (s.ParentID == objCoteInfo.ID)) ? "1" : "0", objCoteInfo.Name }).EncryptModuleQuery(), objModule.ModuleName }));
            bool isCoteSuper = this.ParentIsCoteSupper(objCoteInfo, source, list2);
            this.CreateCoteChildMenuPowerXmlElement(xmlDocSource, isCoteSuper, list2, objCoteInfo.ID, objXmlElement, source, str3, objModule.ModuleLevel, objModule);
            return objXmlElement;
        }

        public XmlElement GetCotePowerXmlElement(XmlDocument xmlDocSource, string AuthorizeGroupID, Sys_Module objCoteModule, List<Sys_Module> objSys_ModuleList, List<Sys_Module> AddSys_ModuleList)
        {
            this.CurrentAuthorizeGroupID = AuthorizeGroupID;
            List<CoteInfo> source = null;
            List<sys_authorizegrouppower> authorizeGroupCotePower = null;
            if (AuthorizeGroupID.IsNull())
            {
                source = this.GetAllCoteInfoList();
            }
            else
            {
                authorizeGroupCotePower = this.GetAuthorizeGroupCotePower(AuthorizeGroupID, objCoteModule.ModuleID);
                if (authorizeGroupCotePower.Count == 0)
                {
                    return null;
                }
                if (authorizeGroupCotePower.Any<sys_authorizegrouppower>(s => s.IsCoteSupper))
                {
                    source = new List<CoteInfo>();
                    foreach (string str in (from s in authorizeGroupCotePower
                        where s.IsCoteSupper
                        select s.CoteID).Distinct<string>())
                    {
                        source.AddRange(this.GetAllCoteInfoList(str));
                    }
                    source.AddRange(this.GetCoteInfoKeyList((from s in authorizeGroupCotePower
                        where !s.IsCoteSupper
                        select s.CoteID).Distinct<string>().ToList<string>().ConvertListToString<string>()));
                    ModuleRule rule = new ModuleRule();
                    string ModuleIDPath = rule.Sys_Module.FirstOrDefault<Sys_Module>(s => (s.ModuleID == objCoteModule.ModuleID)).ModuleIDPath;
                    objSys_ModuleList.RemoveAll(s => s.ModuleIDPath.StartsWith(ModuleIDPath));
                    objSys_ModuleList.AddRange(from s in rule.Sys_Module
                        where (s.ModuleIDPath.StartsWith(ModuleIDPath) && s.IsPower) && !s.IsSupperPower
                        select s);
                }
                else
                {
                    source = this.GetCoteInfoKeyList((from s in authorizeGroupCotePower select s.CoteID).Distinct<string>().ToList<string>().ConvertListToString<string>());
                }
            }
            source = source.Distinct<CoteInfo>(new CoteInfoComparer()).ToList<CoteInfo>();
            CoteInfo objCoteInfo = source.FirstOrDefault<CoteInfo>(s => s.ID == this.RootIDValue);
            if (objCoteInfo == null)
            {
                return null;
            }
            XmlElement objXmlElement = xmlDocSource.CreateElement("Module");
            objXmlElement.SetAttribute("ModuleID", RolePowerKey.Create(objCoteModule.ModuleID, objCoteInfo.ID, objCoteModule.ModuleID, false).ToKey);
            objXmlElement.SetAttribute("ModuleName", objCoteModule.ModuleName);
            bool isCoteSupper = false;
            if (AuthorizeGroupID.IsNull() || authorizeGroupCotePower.Any<sys_authorizegrouppower>(s => ((s.CoteID == objCoteInfo.ID) && s.IsCoteSupper)))
            {
                isCoteSupper = true;
                XmlElement newChild = xmlDocSource.CreateElement("Module");
                newChild.SetAttribute("ModuleID", RolePowerKey.Create(objCoteModule.ModuleID, objCoteInfo.ID, objCoteModule.ModuleID, false, true).ToKey);
                newChild.SetAttribute("ModuleName", "***" + objCoteModule.ModuleName + "***所有权限");
                objXmlElement.AppendChild(newChild);
            }
            if (source.Any<CoteInfo>(s => s.ParentID == objCoteInfo.ID))
            {
                if (this.IsParentUrl)
                {
                    this.CreateEachModeuleXmlElement(xmlDocSource, objCoteInfo.ID, objCoteModule.ModuleID, isCoteSupper, objCoteModule, objXmlElement, objSys_ModuleList, authorizeGroupCotePower, AddSys_ModuleList);
                }
            }
            else
            {
                this.CreateEachModeuleXmlElement(xmlDocSource, objCoteInfo.ID, objCoteModule.ModuleID, isCoteSupper, objCoteModule, objXmlElement, objSys_ModuleList, authorizeGroupCotePower, AddSys_ModuleList);
            }
            this.CreateCoteChildMenuPowerXmlElement(xmlDocSource, objCoteInfo.ID, objCoteModule, objXmlElement, source, objSys_ModuleList, authorizeGroupCotePower, AddSys_ModuleList);
            return objXmlElement;
        }

        protected List<Sys_RolePower> GetUserCoteRolePower(string userID, string moduleID)
        {
            string format = " SELECT  Sys_RolePower.*\r\n                              FROM  Sys_RoleUser \r\n                             ,   Sys_RolePower WHERE Sys_RoleUser.UserID='{0}'   \r\n                             AND Sys_RolePower.CoteModuleID='{1}' and Sys_RoleUser.RoleID=Sys_RolePower.RoleID";
            UserRule rule = new UserRule();
            return rule.CurrentEntities.ExecuteStoreQuery<Sys_RolePower>(string.Format(format, userID, moduleID), new object[0]).ToList<Sys_RolePower>();
        }

        public bool ParentIsCoteSupper(CoteInfo objCoteInfo, List<CoteInfo> objCoteInfoList, List<sys_authorizegrouppower> objCoteRolePowerList)
        {
            if (this.CurrentAuthorizeGroupID.IsNull())
            {
                return true;
            }
            if (objCoteRolePowerList.Any<sys_authorizegrouppower>(s => (s.CoteID == objCoteInfo.ID) && s.IsCoteSupper))
            {
                return true;
            }
            CoteInfo info = objCoteInfoList.FirstOrDefault<CoteInfo>(s => s.ID == objCoteInfo.ParentID);
            if (info == null)
            {
                return false;
            }
            return this.ParentIsCoteSupper(info, objCoteInfoList, objCoteRolePowerList);
        }

        public bool ParentIsCoteSupper(CoteInfo objCoteInfo, List<CoteInfo> objCoteInfoList, List<Sys_RolePower> objCoteRolePowerList)
        {
            if (this.CurrentIsSuper)
            {
                return true;
            }
            if (objCoteRolePowerList.Any<Sys_RolePower>(s => (s.CoteID == objCoteInfo.ID) && s.IsCoteSupper))
            {
                return true;
            }
            CoteInfo info = objCoteInfoList.FirstOrDefault<CoteInfo>(s => s.ID == objCoteInfo.ParentID);
            if (info == null)
            {
                return false;
            }
            return this.ParentIsCoteSupper(info, objCoteInfoList, objCoteRolePowerList);
        }

        public string ConnectionStringName { get; set; }

        public string CoteTableName { get; set; }

        public string DefalutCondition { get; set; }

        public string GetTabeleSql
        {
            get
            {
                return (" select  DISTINCT " + this.IDName + "," + this.ParentIDName + "," + this.Name + " from " + this.CoteTableName + " ");
            }
        }

        public int IDDataType { get; set; }

        public string IDName { get; set; }

        public string IDPathName { get; set; }

        public bool IsParentUrl { get; set; }

        public string ModuleTypeID { get; set; }

        public string Name { get; set; }

        public string ParentIDName { get; set; }

        public string RootIDValue { get; set; }

        public string SortExpression { get; set; }
    }
}

