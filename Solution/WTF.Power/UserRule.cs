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
    using System.Text;
    using System.Web.Security;

    public class UserRule
    {
        private UserEntities objCurrentEntities;

        public void AddRoleUser(string roleID, string userIDString)
        {
            if (userIDString.IsNoNull())
            {
                List<string> list = userIDString.ConvertListString();
                foreach (WTF.Power.Entity.Sys_RoleUser user in this.CurrentEntities.sys_roleuser.Where(" it.RoleID=='" + roleID.ToString() + "' AND  it.UserID in{" + userIDString.ConvertStringID() + "}", new ObjectParameter[0]))
                {
                    list.Remove(user.UserID);
                }
                if (list.Count > 0)
                {
                    foreach (string str in list)
                    {
                        WTF.Power.Entity.Sys_RoleUser user2 = new WTF.Power.Entity.Sys_RoleUser {
                            RoleID = roleID,
                            UserID = str,
                            RoleUserID = Guid.NewGuid().ToString()
                        };
                        this.CurrentEntities.AddTosys_roleuser(user2);
                    }
                    this.CurrentEntities.SaveChanges();
                }
            }
        }

        public void AddUserRole(string userID, string roleIDString)
        {
            if (roleIDString.IsNoNull())
            {
                List<string> list = roleIDString.ConvertListString();
                foreach (WTF.Power.Entity.Sys_RoleUser user in this.CurrentEntities.sys_roleuser.Where(" it.UserID =='" + userID.ToString() + "' AND it.RoleID in{" + roleIDString.ConvertStringID() + "}", new ObjectParameter[0]))
                {
                    list.Remove(user.RoleID);
                }
                if (list.Count > 0)
                {
                    foreach (string str in list)
                    {
                        WTF.Power.Entity.Sys_RoleUser user2 = new WTF.Power.Entity.Sys_RoleUser {
                            RoleID = str,
                            UserID = userID,
                            RoleUserID = Guid.NewGuid().ToString()
                        };
                        this.CurrentEntities.AddTosys_roleuser(user2);
                    }
                    this.CurrentEntities.SaveChanges();
                }
            }
        }

        public bool CheckControllerAction(string moduleCodeType, string controllerName, string actionName)
        {
            if (this.CurrentUser.IsSuper)
            {
                return true;
            }
            new ModuleRule();
            return false;
        }

        public bool CheckIsHaveAccount(string account)
        {
            account.CheckIsNull("请输入要查询的帐号", LogModuleType.UserLog);
            return ((ConfigHelper.GetValue("ObligateAccount", "").ToLower().IndexOf(account.ToLower()) != -1) || ((from p in this.CurrentEntities.sys_user
                where p.Account == account
                select p).Count<WTF.Power.Entity.Sys_User>() > 0));
        }

        public bool CheckIsHaveEmail(string email)
        {
            return ((from p in this.CurrentEntities.sys_user
                where p.Email == email
                select p).Count<WTF.Power.Entity.Sys_User>() > 0);
        }

        public string CheckPowerDataCondition(string moduleTypeID, string pageName, string userID)
        {
            List<Sys_ModuleData> checkPageModuleData = new ModuleRule().GetCheckPageModuleData(moduleTypeID, pageName);
            if (checkPageModuleData.Count == 0)
            {
                return "";
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (Sys_ModuleData data in checkPageModuleData)
            {
                if (!dictionary.ContainsKey(data.FieldName))
                {
                    dictionary.Add(data.FieldName, this.ValueDefaultFormant(data.FieldType));
                }
            }
            List<Sys_RoleData_Info> list2 = this.CurrentEntities.GetPowerDataModule(moduleTypeID, pageName, userID).ToList<Sys_RoleData_Info>();
            if (list2.Count > 0)
            {
                foreach (Sys_RoleData_Info info in list2)
                {
                    dictionary.Remove(info.FieldName);
                }
                foreach (Sys_RoleData_Info info2 in list2)
                {
                    if (dictionary.ContainsKey(info2.FieldName))
                    {
                        dictionary[info2.FieldName] = dictionary[info2.FieldName] + "," + this.ValueFormant(info2.DataSelect, info2.FieldType);
                    }
                    else
                    {
                        dictionary.Add(info2.FieldName, this.ValueFormant(info2.DataSelect, info2.FieldType));
                    }
                }
            }
            string str = "";
            foreach (string str2 in dictionary.Keys)
            {
                string str3 = str;
                str = str3 + "and it." + str2 + " in {" + dictionary[str2] + "} ";
            }
            return str.Substring(3);
        }

        public string CheckPowerFieldData(string moduleTypeID, string pageName, string fieldNameSource, string fieldName, string userID)
        {
            List<Sys_ModuleData> list = new ModuleRule().GetCheckPageFieldModuleData(moduleTypeID, pageName, fieldName);
            if (list.Count == 0)
            {
                return "";
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (Sys_ModuleData data in list)
            {
                if (!dictionary.ContainsKey(data.FieldName))
                {
                    dictionary.Add(data.FieldName, this.ValueDefaultFormant(data.FieldType));
                }
            }
            List<Sys_RoleData_Info> list2 = (from s in this.CurrentEntities.GetPowerDataModule(moduleTypeID, pageName, userID).ToList<Sys_RoleData_Info>()
                where s.FieldName == fieldName
                select s).ToList<Sys_RoleData_Info>();
            if (list2.Count > 0)
            {
                foreach (Sys_RoleData_Info info in list2)
                {
                    dictionary.Remove(info.FieldName);
                }
                foreach (Sys_RoleData_Info info2 in list2)
                {
                    if (dictionary.ContainsKey(info2.FieldName))
                    {
                        dictionary[info2.FieldName] = dictionary[info2.FieldName] + "," + this.ValueFormant(info2.DataSelect, info2.FieldType);
                    }
                    else
                    {
                        dictionary.Add(info2.FieldName, this.ValueFormant(info2.DataSelect, info2.FieldType));
                    }
                }
            }
            string str = "";
            foreach (string str2 in dictionary.Keys)
            {
                string str3 = str;
                str = str3 + "and it." + fieldNameSource + " in {" + dictionary[str2] + "} ";
            }
            return str.Substring(3);
        }

        public bool CheckPowerFrame(string moduleTypeID, string UserID)
        {
            return (this.CurrentEntities.CheckPowerFrameByID(UserID, moduleTypeID).First<int?>().Value > 0);
        }

        public bool CheckPowerPage(string moduleTypeID, string pageName, string userID)
        {
            return (this.CurrentEntities.CheckPowerPageByID(userID, pageName, moduleTypeID).First<int?>().Value > 0);
        }

        public bool CheckPowerPage(string moduleTypeID, string coteModuleID, string coteID, string pageName, string userID)
        {
            return (this.CurrentEntities.CheckPowerCotePageByID(userID, coteModuleID, coteID, pageName, moduleTypeID).First<int?>().Value > 0);
        }

        public bool CheckUserAnswer(string account, string answer)
        {
            account.CheckIsNull("请输入用户帐号", LogModuleType.UserLog);
            answer.CheckIsNull("请输入提示问题的答案", LogModuleType.UserLog);
            answer = answer.MD5Encrypt();
            WTF.Power.Entity.Sys_User user = this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(p => p.Account == account);
            string passwordAnswer = user.PasswordAnswer;
            if (passwordAnswer != answer)
            {
                user.FaildAnswerAttemptStart = new DateTime?(DateTime.Now);
                user.FaildAnswerAttemptCount++;
                this.CurrentEntities.SaveChanges();
            }
            return (passwordAnswer == answer);
        }

        public void CopyUserPower(string SourceuserID, string targetUserID, string AuthorizeGroupID)
        {
            List<string> copyRoleIDList = (from s in this.Sys_RoleUser
                where s.UserID == targetUserID
                select s.RoleID).ToList<string>();
            IQueryable<WTF.Power.Entity.Sys_Role> source = from s in this.Sys_Role
                where copyRoleIDList.Contains(s.RoleID)
                select s;
            if (!string.IsNullOrWhiteSpace(AuthorizeGroupID))
            {
                source = from s in source
                    where s.AuthorizeGroupID == AuthorizeGroupID
                    select s;
            }
            List<WTF.Power.Entity.Sys_Role> list = source.ToList<WTF.Power.Entity.Sys_Role>();
            this.UpdateUserRole(SourceuserID, (from s in list
                where !s.IsUserRole && !s.IsSystem
                select s.RoleID).ToList<string>().ConvertListToString<string>(), "");
            using (IEnumerator<WTF.Power.Entity.Sys_Role> enumerator = (from s in list
                where (s.IsUserRole && (s.AuthorizeGroupID != Guid.Empty.ToString())) && !s.IsSystem
                select s).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    WTF.Power.Entity.Sys_Role objSys_Role = enumerator.Current;
                    WTF.Power.Entity.Sys_Role objSelfRole = this.GetUserSelfAuthorizeGroupRole(objSys_Role.AuthorizeGroupID, SourceuserID, objSys_Role.ModuleTypeID);
                    List<WTF.Power.Entity.Sys_RolePower> list2 = (from s in this.Sys_RolePower
                        where s.RoleID == objSelfRole.RoleID
                        select s).ToList<WTF.Power.Entity.Sys_RolePower>();
                    using (IEnumerator<WTF.Power.Entity.Sys_RolePower> enumerator2 = (from s in this.Sys_RolePower
                        where s.RoleID == objSys_Role.RoleID
                        select s).GetEnumerator())
                    {
                        while (enumerator2.MoveNext())
                        {
                            Func<WTF.Power.Entity.Sys_RolePower, bool> predicate = null;
                            WTF.Power.Entity.Sys_RolePower copyPower = enumerator2.Current;
                            if (predicate == null)
                            {
                                predicate = s => (((s.ModuleID == copyPower.ModuleID) && (s.CoteID == copyPower.CoteID)) && ((s.CoteModuleID == copyPower.CoteModuleID) && (s.IsShare == copyPower.IsShare))) && (s.IsCoteSupper == copyPower.IsCoteSupper);
                            }
                            if (!list2.Any<WTF.Power.Entity.Sys_RolePower>(predicate))
                            {
                                WTF.Power.Entity.Sys_RolePower power = new WTF.Power.Entity.Sys_RolePower {
                                    RolePowerID = Guid.NewGuid().ToString(),
                                    IsShare = copyPower.IsShare,
                                    CoteModuleID = copyPower.CoteModuleID,
                                    CoteID = copyPower.CoteID,
                                    ModuleID = copyPower.ModuleID,
                                    IsCoteSupper = copyPower.IsCoteSupper,
                                    RoleID = objSelfRole.RoleID
                                };
                                this.CurrentEntities.AddTosys_rolepower(power);
                            }
                        }
                        continue;
                    }
                }
            }
            this.CurrentEntities.SaveChanges();
        }

        public void DeleteauthorizegroupByKey(string primaryKey)
        {
            this.CurrentEntities.sys_authorizegroup.DeleteDataPrimaryKey<WTF.Power.Entity.sys_authorizegroup>(primaryKey);
        }

        public void DeleteauthorizegrouppowerByKey(string primaryKey)
        {
            this.CurrentEntities.sys_authorizegrouppower.DeleteDataPrimaryKey<WTF.Power.Entity.sys_authorizegrouppower>(primaryKey);
        }

        public void DeleteRole(string roleIDString)
        {
            foreach (WTF.Power.Entity.Sys_Role role in this.Sys_Role.Where("it.RoleID IN {" + roleIDString.ConvertStringID() + "}", new ObjectParameter[0]))
            {
                this.CurrentEntities.DeleteObject(role);
            }
            this.CurrentEntities.SaveChanges();
        }

        public void DeleteRoleData(string condition)
        {
            this.CurrentEntities.sys_roledata.DeleteData<WTF.Power.Entity.Sys_RoleData>(condition, new ObjectParameter[0]);
        }

        public void DeleteRoleDataByKey(string primaryKey)
        {
            this.CurrentEntities.sys_roledata.DeleteDataPrimaryKey<WTF.Power.Entity.Sys_RoleData>(primaryKey);
        }

        public void DeleteUser(string UserIDString)
        {
            foreach (WTF.Power.Entity.Sys_User user in this.CurrentEntities.sys_user.Where("it.UserID in { " + UserIDString.ConvertStringID() + "}", new ObjectParameter[0]))
            {
                this.CurrentEntities.DeleteObject(user);
            }
            this.CurrentEntities.SaveChanges();
            foreach (WTF.Power.Entity.Sys_Role role in this.CurrentEntities.sys_role.Where("it.RefUserID in { " + UserIDString.ConvertStringID() + "} and it.IsUserRole=true", new ObjectParameter[0]))
            {
                this.CurrentEntities.DeleteObject(role);
            }
            this.CurrentEntities.SaveChanges();
        }

        public void DeleteUserType(string condition)
        {
            this.CurrentEntities.sys_usertype.DeleteData<WTF.Power.Entity.Sys_UserType>(condition, new ObjectParameter[0]);
        }

        public void DeleteUserTypeByKey(string primaryKey)
        {
            this.CurrentEntities.sys_usertype.DeleteDataPrimaryKey<WTF.Power.Entity.Sys_UserType>(primaryKey);
        }

        public List<RolePowerKey> GetAuthorizeGroupPower(string AuthorizeGroupID)
        {
            new ModuleRule();
            return (from s in this.CurrentEntities.sys_authorizegrouppower
                where s.AuthorizeGroupID == AuthorizeGroupID
                select new RolePowerKey { CoteID = s.CoteID, CoteModuleID = s.CoteModuleID, ModuleID = s.ModuleID, IsShare = s.IsShare, IsCoteSupper = s.IsCoteSupper }).Distinct<RolePowerKey>().ToList<RolePowerKey>();
        }

        public List<string> GetCoteOperateModule(string userID, string voteID, string moduleID)
        {
            string format = "SELECT  Sys_RoleCotePower.ModuleID\r\n  FROM  Sys_RoleUser \r\n , Sys_RoleCote,Sys_RoleCotePower WHERE Sys_RoleUser.UserID='{0}' and  Sys_RoleUser.RoleID=Sys_RoleCote.RoleID\r\n AND Sys_RoleCote.ModuleID='{1}' and   Sys_RoleCote.CoteID='{2}' AND Sys_RoleCote.RoleCoteID=Sys_RoleCotePower.RoleCoteID";
            return this.CurrentEntities.ExecuteStoreQuery<string>(string.Format(format, userID, moduleID, voteID), new object[0]).ToList<string>();
        }

        public UserInfo GetCurrentUser(string ModuleTypeID)
        {
            if (SysVariable.CurrentContext.Request.IsAuthenticated)
            {
                if ((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Session["CurrentUser"] == null))
                {
                    this.LoginRule(SysVariable.CurrentContext.User.Identity.Name);
                    return (UserInfo) SysVariable.CurrentContext.Session["CurrentUser"];
                }
                if ((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Session["CurrentUser"] != null))
                {
                    if (((UserInfo) SysVariable.CurrentContext.Session["CurrentUser"]).Account != SysVariable.CurrentContext.User.Identity.Name)
                    {
                        SysVariable.CurrentContext.Session.Clear();
                        this.LoginRule(SysVariable.CurrentContext.User.Identity.Name);
                    }
                    return (UserInfo) SysVariable.CurrentContext.Session["CurrentUser"];
                }
            }
            UserInfo info = new UserInfo();
            if (SysVariable.CurrentContext.Session["CurrentUser"] != null)
            {
                info = (UserInfo) SysVariable.CurrentContext.Session["CurrentUser"];
                if (info.UserID == Guid.Empty.ToString())
                {
                    return info;
                }
            }
            info = new UserInfo(Guid.Empty.ToString(), "Anonymous", "旅客", "旅客", 0, "", -1, "", RequestHelper.UserHostAddress, false, DateTime.Now);
            string GuidEmpty = Guid.Empty.ToString();
            info.RoleID = (from s in this.Sys_RoleUser
                where ((s.UserID == GuidEmpty) && !s.Sys_Role.IsLockOut) && (s.Sys_Role.ModuleTypeID == ModuleTypeID)
                orderby s.RoleID
                select s.RoleID).ToList<string>();
            SysVariable.CurrentContext.Session["CurrentUser"] = info;
            return info;
        }

        public List<RolePowerKey> GetRoleKeyPower(string roleID)
        {
            return (from s in this.CurrentEntities.sys_rolepower
                where s.RoleID == roleID
                select new RolePowerKey { CoteID = s.CoteID, CoteModuleID = s.CoteModuleID, ModuleID = s.ModuleID, IsShare = s.IsShare, IsCoteSupper = s.IsCoteSupper }).Distinct<RolePowerKey>().ToList<RolePowerKey>();
        }

        public string GetRoleModuleTypeUserType(string roleID)
        {
            return this.CurrentEntities.sys_modulerole.FirstOrDefault<WTF.Power.Entity.Sys_ModuleRole>(s => (s.RoleID == roleID)).UserType;
        }

        public List<string> GetRolePower(string roleID)
        {
            return (from s in this.CurrentEntities.sys_rolepower
                where s.RoleID == roleID
                select s.ModuleID).ToList<string>();
        }

        public string GetRoleUser(string roleID)
        {
            roleID.CheckIsNull("请输入角色标识", LogModuleType.UserLog);
            return (from s in this.CurrentEntities.sys_roleuser
                where s.RoleID == roleID
                select s.UserID).ConvertListToString<string>();
        }

        public string GetRoleUser(string roleID, string createUserID)
        {
            roleID.CheckIsNull("请输入角色标识", LogModuleType.UserLog);
            string str = "";
            foreach (string str2 in from s in this.CurrentEntities.sys_roleuser
                where (s.RoleID == roleID) && (s.Sys_Role.UserID == createUserID)
                select s.UserID)
            {
                str = str + str2.ToString() + ",";
            }
            if (!str.IsNoNull())
            {
                return "";
            }
            return str.TrimEndComma();
        }

        public List<RolePowerKey> GetUserAllKeyPower(string userid)
        {
            List<string> userRoleList = (from s in this.CurrentEntities.sys_roleuser
                where s.UserID == userid
                select s.RoleID).ToList<string>();
            if (userRoleList.Count == 0)
            {
                return new List<RolePowerKey>();
            }
            return (from s in this.CurrentEntities.sys_rolepower
                where userRoleList.Contains(s.RoleID)
                select new RolePowerKey { CoteID = s.CoteID, CoteModuleID = s.CoteModuleID, ModuleID = s.ModuleID, IsShare = s.IsShare, IsCoteSupper = s.IsCoteSupper }).ToList<RolePowerKey>();
        }

        public List<RolePowerKey> GetUserKeyPower(string userid, string ModuleTypeID, string AuthorizeGroupID)
        {
            WTF.Power.Entity.Sys_Role objSys_Role = this.CurrentEntities.sys_role.FirstOrDefault<WTF.Power.Entity.Sys_Role>(s => (((s.RefUserID == userid) && (s.ModuleTypeID == ModuleTypeID)) && (s.AuthorizeGroupID == AuthorizeGroupID)) && s.IsUserRole);
            if (objSys_Role == null)
            {
                return new List<RolePowerKey>();
            }
            return (from s in this.CurrentEntities.sys_rolepower
                where s.RoleID == objSys_Role.RoleID
                select new RolePowerKey { CoteID = s.CoteID, CoteModuleID = s.CoteModuleID, ModuleID = s.ModuleID, IsShare = s.IsShare, IsCoteSupper = s.IsCoteSupper }).ToList<RolePowerKey>();
        }

        public IQueryable<Sys_ModuleType> GetUserModuleType(string UserID)
        {
            string UserTypeCID = this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(s => (s.UserID == UserID)).UserTypeCID.ToString();
            ModuleRule rule = new ModuleRule();
            return (from s in rule.Sys_ModuleType
                where s.UserType.Contains(UserTypeCID)
                select s);
        }

        public List<string> GetUserModuleTypeID(string UserID)
        {
            string UserTypeCID = this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(s => (s.UserID == UserID)).UserTypeCID.ToString();
            ModuleRule rule = new ModuleRule();
            return (from s in rule.Sys_ModuleType
                where s.UserType.Contains(UserTypeCID)
                select s.ModuleTypeID).ToList<string>();
        }

        public ObjectQuery<WTF.Power.Entity.Sys_Role> GetUserModuleTypeRole(string UserID)
        {
            List<string> userModuleTypeID = this.GetUserModuleTypeID(UserID);
            if (userModuleTypeID.Count<string>() == 0)
            {
                return this.CurrentEntities.sys_role.Where("it.ModuleTypeID='" + Guid.Empty.ToString() + "'", new ObjectParameter[0]);
            }
            return this.CurrentEntities.sys_role.Where("it.ModuleTypeID IN {" + userModuleTypeID.ConvertStringID<string>() + "}", new ObjectParameter[0]);
        }

        public string GetUserRole(string userID)
        {
            userID.CheckIsNull("请输入用户标识", LogModuleType.UserLog);
            return (from s in this.CurrentEntities.sys_roleuser
                where s.UserID == userID
                select s.RoleID).ConvertListToString<string>();
        }

        public WTF.Power.Entity.Sys_Role GetUserSelfAuthorizeGroupRole(string AuthorizeGroupID, string UserID, string ModuleTypeID)
        {
            WTF.Power.Entity.Sys_Role objRule = this.Sys_Role.FirstOrDefault<WTF.Power.Entity.Sys_Role>(s => (((s.AuthorizeGroupID == AuthorizeGroupID) && (s.ModuleTypeID == ModuleTypeID)) && (s.RefUserID == UserID)) && s.IsUserRole);
            if (objRule == null)
            {
                WTF.Power.Entity.Sys_User user = this.Sys_User.FirstOrDefault<WTF.Power.Entity.Sys_User>(s => s.UserID == UserID);
                if (user == null)
                {
                    SysAssert.InfoHintAssert("对不起，找不到此用户");
                }
                string authorizeGroupName = "平台虚拟组权限";
                bool flag = false;
                if (AuthorizeGroupID != Guid.Empty.ToString())
                {
                    WTF.Power.Entity.sys_authorizegroup _authorizegroup = this.sys_authorizegroup.FirstOrDefault<WTF.Power.Entity.sys_authorizegroup>(s => s.AuthorizeGroupID == AuthorizeGroupID);
                    if (_authorizegroup == null)
                    {
                        SysAssert.InfoHintAssert("对不起，找不到此授权组");
                    }
                    flag = false;
                    authorizeGroupName = _authorizegroup.AuthorizeGroupName;
                }
                else
                {
                    flag = true;
                }
                if (this.CurrentUser.UserTypeCID == -1)
                {
                    SysAssert.InfoHintAssert("对不起，你未登录无法设置");
                }
                objRule = new WTF.Power.Entity.Sys_Role {
                    RoleID = Guid.NewGuid().ToString(),
                    AuthorizeGroupID = AuthorizeGroupID,
                    RefUserID = UserID,
                    IsUserRole = true,
                    UserID = this.CurrentUser.UserID,
                    RoleName = (user.Account + "|角色私有").CutWord(20),
                    RoleCode = objRule.RoleName.ConvertChineseSpell(false, ' '),
                    Remark = (user.Account + "|" + authorizeGroupName).CutWord(90),
                    IsLockOut = false,
                    ModuleTypeID = ModuleTypeID,
                    RoleGroupID = "",
                    IsSystem = flag,
                    AccountTypeID = this.CurrentUser.AccountTypeID
                };
                this.InsertRole(objRule);
                this.AddRoleUser(objRule.RoleID, UserID);
            }
            return objRule;
        }

        public ObjectQuery<WTF.Power.Entity.Sys_ModuleRole> GetUserTypeRole(string UserID)
        {
            string str = this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(s => (s.UserID == UserID)).UserTypeCID.ToString();
            return this.CurrentEntities.sys_modulerole.Where("it.UserType like '%" + str + "%'", new ObjectParameter[0]);
        }

        public ObjectQuery<WTF.Power.Entity.Sys_User> GetUserTypeUser(string UserTypeCID)
        {
            return this.CurrentEntities.sys_user.Where("it.UserTypeCID in {" + UserTypeCID + "}", new ObjectParameter[0]);
        }

        public void Insertauthorizegroup(WTF.Power.Entity.sys_authorizegroup objsys_authorizegroup)
        {
            objsys_authorizegroup.AuthorizeGroupName.CheckIsNull<string>("请输入授权组名", "ModuleLog");
            this.CurrentEntities.AddTosys_authorizegroup(objsys_authorizegroup);
            this.CurrentEntities.SaveChanges();
        }

        public void Insertauthorizegrouppower(WTF.Power.Entity.sys_authorizegrouppower objsys_authorizegrouppower)
        {
            this.CurrentEntities.AddTosys_authorizegrouppower(objsys_authorizegrouppower);
            this.CurrentEntities.SaveChanges();
        }

        public string InsertClientUser(int userTypeCID, string accountTypeID, string nickName, string jobNo, string userName, string account, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, bool isActivation, bool isAdmin = false)
        {
            return this.InsertUser(userTypeCID, accountTypeID, false, nickName, jobNo, userName, account, password, email, passwordQuestion, passwordQuestion, isApproved, isActivation, isAdmin);
        }

        public void InsertRole(WTF.Power.Entity.Sys_Role objRule)
        {
            objRule.UserID = this.CurrentUser.UserID.ToString();
            objRule.CreateDate = DateTime.Now;
            objRule.RoleName.CheckIsNull("请输入角色名称", LogModuleType.UserLog);
            this.CurrentEntities.AddTosys_role(objRule);
            this.CurrentEntities.SaveChanges();
        }

        public void InsertRoleCote(string roleID, string moduleID, string InsertID, string rightID)
        {
            WTF.Power.Entity.Sys_RoleCote cote = new WTF.Power.Entity.Sys_RoleCote {
                RoleID = roleID,
                CoteID = InsertID,
                ModuleID = moduleID,
                RoleCoteID = Guid.NewGuid().ToString()
            };
            this.CurrentEntities.AddTosys_rolecote(cote);
            if (rightID.IsNoNull())
            {
                foreach (string str in from s in this.CurrentEntities.sys_rolecoteinfo
                    where ((s.RoleID == roleID) && (s.CoteModuleID == moduleID)) && (s.CoteID == rightID)
                    select s.ModuleID)
                {
                    WTF.Power.Entity.Sys_RoleCotePower entity = new WTF.Power.Entity.Sys_RoleCotePower {
                        ModuleID = str,
                        RoleCotePowerID = Guid.NewGuid().ToString(),
                        RoleCoteID = cote.RoleCoteID
                    };
                    cote.Sys_RoleCotePower.Add(entity);
                }
            }
            this.CurrentEntities.SaveChanges();
        }

        public void InsertRoleData(WTF.Power.Entity.Sys_RoleData objSys_RoleData)
        {
            this.CurrentEntities.AddTosys_roledata(objSys_RoleData);
            this.CurrentEntities.SaveChanges();
        }

        public string InsertUser(int userTypeCID, string accountTypeID, bool isSuper, string nickName, string jobNo, string userName, string account, string password, string email, bool isAdmin = false)
        {
            return this.InsertUser(userTypeCID, accountTypeID, isSuper, nickName, jobNo, userName, account, password, email, "", "", true, true, isAdmin);
        }

        private string InsertUser(int userTypeCID, string accountTypeID, bool isSuper, string nickName, string jobNo, string userName, string account, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, bool isActivation, bool isAdmin = false)
        {
            account = account.Trim();
            password = password.Trim();
            account.CheckIsNull("请输入用户帐号", LogModuleType.UserLog);
            password.CheckIsNull("请输入用户密码", LogModuleType.UserLog);
            accountTypeID.CheckIsNull("请输入帐户归属", LogModuleType.UserLog);
            SysAssert.InfoHintAssert(this.CheckIsHaveAccount(account), "输入的帐号系统已经存在");
            if (!email.IsNull())
            {
                SysAssert.InfoHintAssert(this.CheckIsHaveEmail(email), "输入的Email系统已经存在");
            }
            if (!string.IsNullOrEmpty(passwordQuestion))
            {
                SysAssert.CheckCondition(string.IsNullOrEmpty(passwordAnswer), "请输入提示答案", LogModuleType.UserLog);
                passwordAnswer = passwordAnswer.MD5Encrypt();
            }
            string userHostAddress = RequestHelper.UserHostAddress;
            Guid.NewGuid();
            password = password.MD5Encrypt();
            WTF.Power.Entity.Sys_User user = new WTF.Power.Entity.Sys_User {
                UserID = Guid.NewGuid().ToString(),
                UserTypeCID = userTypeCID,
                IsSuper = isSuper,
                AccountTypeID = accountTypeID,
                Account = account,
                Password = password,
                Email = email,
                LoginIP = userHostAddress,
                PasswordQuestion = passwordQuestion,
                PasswordAnswer = passwordAnswer,
                IsApproved = isApproved,
                IsActivation = isActivation,
                IsAdmin = isAdmin,
                UserName = userName,
                JobNo = jobNo,
                NickName = nickName
            };
            if (user.IsApproved)
            {
                user.ApprovedDate = new DateTime?(DateTime.Now);
            }
            if (user.IsActivation)
            {
                user.ActivationDate = new DateTime?(DateTime.Now);
            }
            user.IsLockOut = false;
            user.CreateDate = DateTime.Now;
            user.FaildAnswerAttemptCount = 0;
            user.FaildPasswordAttemptStart = "1753-01-01".ConvertDateTime();
            user.LastPasswordChangeDate = "1753-01-01".ConvertDateTime();
            user.LastLockOutDate = "1753-01-01".ConvertDateTime();
            user.FaildAnswerAttemptStart = new DateTime?("1753-01-01".ConvertDateTime());
            user.FaildPasswordAttemptCount = 0;
            this.CurrentEntities.AddTosys_user(user);
            this.CurrentEntities.SaveChanges();
            return user.UserID;
        }

        public void InsertUserType(WTF.Power.Entity.Sys_UserType objSys_UserType)
        {
            objSys_UserType.UserTypeName.CheckIsNull<string>("请输入用户类型名称", "UserLog");
            objSys_UserType.Remark.CheckIsNull<string>("请输入备注", "UserLog");
            this.CurrentEntities.AddTosys_usertype(objSys_UserType);
            this.CurrentEntities.SaveChanges();
        }

        public LoginInfo LoginRule(string account)
        {
            return this.LoginRule(account, "", true, false, null);
        }

        public LoginInfo LoginRule(string account, string password, bool isSetAuthCookie, List<int> objUserTypeList)
        {
            return this.LoginRule(account, password, false, isSetAuthCookie, objUserTypeList);
        }

        public LoginInfo LoginRule(string account, string password, bool isAuthenticated, bool isSetAuthCookie, List<int> objUserTypeList)
        {
            account.CheckIsNull("请输入登录帐号", LogModuleType.UserLog);
            if (!isAuthenticated)
            {
                password.CheckIsNull("请输入登录密码", LogModuleType.UserLog);
            }
            WTF.Power.Entity.Sys_User objUser = this.CurrentEntities.sys_user.FirstOrDefault<WTF.Power.Entity.Sys_User>(p => p.Account == account);
            SysAssert.InfoHintAssert(objUser.IsNull(), "输入的用户帐号系统不存在");
            if (!isAuthenticated && (objUser.Password != password.MD5Encrypt()))
            {
                objUser.FaildPasswordAttemptCount = objUser.FaildPasswordAttemptCount;
                objUser.FaildPasswordAttemptStart = DateTime.Now;
                this.CurrentEntities.SaveChanges();
                SysAssert.InfoHintAssert("请输入正确的密码");
            }
            if ((objUserTypeList != null) && (objUserTypeList.Count<int>() > 0))
            {
                SysAssert.InfoHintAssert(!objUserTypeList.Contains(objUser.UserTypeCID), "输入的用户帐号系统不存在");
            }
            if (isAuthenticated && ((!objUser.IsApproved || objUser.IsLockOut) || !objUser.IsActivation))
            {
                FormsAuthentication.SignOut();
            }
            SysAssert.InfoHintAssert(!objUser.IsApproved, "对不起你的用户帐号未审核通过");
            SysAssert.InfoHintAssert(objUser.IsLockOut, "对不起你的用户帐号已经被系统锁住");
            SysAssert.InfoHintAssert(!objUser.IsActivation, "对不起你的用户帐号未激活");
            UserInfo info = new UserInfo(objUser.UserID, objUser.Account, objUser.UserName, objUser.NickName, objUser.ID, objUser.JobNo, objUser.UserTypeCID, objUser.AccountTypeID, RequestHelper.UserHostAddress, objUser.IsSuper, DateTime.Now) {
                RoleID = (from s in this.Sys_RoleUser
                    where (s.UserID == objUser.UserID) && !s.Sys_Role.IsLockOut
                    orderby s.RoleID
                    select s.RoleID).ToList<string>()
            };
            SysVariable.CurrentContext.Session["CurrentUser"] = info;
            SysVariable.CurrentContext.Session["SignatureID"] = info.UserID;
            if (!isAuthenticated)
            {
                FormsAuthentication.SetAuthCookie(account, isSetAuthCookie);
            }
            return new LoginInfo { UserTypeCID = objUser.UserTypeCID, UserID = objUser.UserID };
        }

        public void RemoveRoleUser(string roleID, string userIDString)
        {
            if (userIDString.IsNoNull())
            {
                foreach (WTF.Power.Entity.Sys_RoleUser user in this.CurrentEntities.sys_roleuser.Where(" it.RoleID=='" + roleID.ToString() + "' AND  it.UserID in{" + userIDString.ConvertStringID() + "}", new ObjectParameter[0]))
                {
                    this.CurrentEntities.DeleteObject(user);
                }
                this.CurrentEntities.SaveChanges();
            }
        }

        public void RemoveUserRole(string userID, string roleIDString)
        {
            if (roleIDString.IsNoNull())
            {
                foreach (WTF.Power.Entity.Sys_RoleUser user in this.CurrentEntities.sys_roleuser.Where(" it.UserID =='" + userID.ToString() + "' AND it.RoleID in{" + roleIDString.ConvertStringID() + "}", new ObjectParameter[0]))
                {
                    this.CurrentEntities.DeleteObject(user);
                }
                this.CurrentEntities.SaveChanges();
            }
        }

        public bool ReSetPassword(string userID, string newPassword)
        {
            if (userID.IsNull())
            {
                SysAssert.ArgumentAssert<LogModuleType>("请选择需要修改的用户", LogModuleType.UserLog);
            }
            if (string.IsNullOrEmpty(newPassword))
            {
                SysAssert.ArgumentAssert<LogModuleType>("请输入新密码", LogModuleType.UserLog);
            }
            newPassword = newPassword.MD5Encrypt();
            WTF.Power.Entity.Sys_User user = this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(p => p.UserID == userID);
            user.Password = newPassword;
            user.LastPasswordChangeDate = DateTime.Now;
            this.CurrentEntities.SaveChanges();
            return true;
        }

        public bool ReSetPassword(string userID, string oldPassword, string newPassword)
        {
            if (userID == Guid.Empty.ToString())
            {
                SysAssert.ArgumentAssert<LogModuleType>("请选择需要修改的用户", LogModuleType.UserLog);
            }
            oldPassword = oldPassword.MD5Encrypt();
            if (this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(p => (p.UserID == userID)).Password != oldPassword)
            {
                SysAssert.ArgumentAssert<LogModuleType>("输入的旧密码不正确请重新输入", LogModuleType.UserLog);
            }
            this.ReSetPassword(userID, newPassword);
            return true;
        }

        public bool ReSetPasswordByAcction(string account, string newPassword)
        {
            account.CheckIsNull("请输入要修改密码的用户帐号", LogModuleType.UserLog);
            newPassword.CheckIsNull("请输入新密码", LogModuleType.UserLog);
            newPassword = newPassword.MD5Encrypt();
            WTF.Power.Entity.Sys_User user = this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(p => p.Account == account);
            user.Password = newPassword;
            user.LastPasswordChangeDate = DateTime.Now;
            this.CurrentEntities.SaveChanges();
            return true;
        }

        public void RevertAuthorizeGroupPower(string AuthorizeGroupID)
        {
            string commandText = "  delete from sys_role where AuthorizeGroupID='" + AuthorizeGroupID + "'";
            this.CurrentEntities.ExecuteStoreCommand(commandText, new object[0]);
        }

        public void RevertUserPower(string userID, string AuthorizeGroupIDString)
        {
            string commandText = "";
            if (string.IsNullOrWhiteSpace(AuthorizeGroupIDString))
            {
                commandText = " delete from sys_roleuser where  UserID='" + userID + "'; delete from sys_role where IsUserRole=1 and  RefUserID='" + userID + "';";
            }
            else
            {
                commandText = " delete from sys_roleuser where  sys_roleuser.UserID='" + userID + "' and  sys_roleuser.RoleID in (select sys_role.RoleID from  sys_role  where sys_role.AuthorizeGroupID in (" + AuthorizeGroupIDString.ConvertStringID() + ") ); delete from sys_role where IsUserRole=1 and AuthorizeGroupID in (" + AuthorizeGroupIDString.ConvertStringID() + ") and  RefUserID='" + userID + "';";
            }
            this.CurrentEntities.ExecuteStoreCommand(commandText, new object[0]);
        }

        public void SaveChanges()
        {
            this.CurrentEntities.SaveChanges();
        }

        public bool SetQuestionAnswer(string userID, string question, string answer)
        {
            if (userID == Guid.Empty.ToString())
            {
                SysAssert.ArgumentAssert<LogModuleType>("请选择需要修改的用户", LogModuleType.UserLog);
            }
            question.CheckIsNull("提示问题不为空", LogModuleType.UserLog);
            answer.CheckIsNull("答案不为空", LogModuleType.UserLog);
            answer = answer.MD5Encrypt();
            WTF.Power.Entity.Sys_User user = this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(p => p.UserID == userID);
            user.PasswordQuestion = question;
            user.PasswordAnswer = answer;
            this.CurrentEntities.SaveChanges();
            return true;
        }

        public bool SetUserActivation(string userIDString, bool isActivation)
        {
            foreach (WTF.Power.Entity.Sys_User user in this.CurrentEntities.sys_user.Where("it.UserID in {" + userIDString.ConvertStringID() + "}", new ObjectParameter[0]))
            {
                user.IsActivation = isActivation;
            }
            this.CurrentEntities.SaveChanges();
            return true;
        }

        public bool SetUserApprove(string[] userID, bool isApprove)
        {
            foreach (WTF.Power.Entity.Sys_User user in from p in this.CurrentEntities.sys_user
                where userID.Contains<string>(p.UserID)
                select p)
            {
                user.IsApproved = isApprove;
            }
            this.CurrentEntities.SaveChanges();
            return true;
        }

        public bool SetUserLock(string userIDString, bool isLockOut)
        {
            foreach (WTF.Power.Entity.Sys_User user in this.CurrentEntities.sys_user.Where("it.UserID in {" + userIDString.ConvertStringID() + "}", new ObjectParameter[0]))
            {
                user.IsLockOut = isLockOut;
                user.LastLockOutDate = DateTime.Now;
            }
            this.CurrentEntities.SaveChanges();
            return true;
        }

        public void SystemExitRule()
        {
            FormsAuthentication.SignOut();
            SysVariable.CurrentContext.Session.Clear();
        }

        public void UpdateAccount(string userID, string account)
        {
            account.CheckIsNull("请输入帐号", LogModuleType.UserLog);
            SysAssert.CheckCondition((from p in this.CurrentEntities.sys_user
                where (p.UserID != userID) && (p.Account == account)
                select p).Count<WTF.Power.Entity.Sys_User>() > 0, "输入的帐号系统已经存在", LogModuleType.UserLog);
            this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(p => (p.UserID == userID)).Account = account;
            this.CurrentEntities.SaveChanges();
        }

        public void Updateauthorizegroup(WTF.Power.Entity.sys_authorizegroup objsys_authorizegroup)
        {
            objsys_authorizegroup.AuthorizeGroupName.CheckIsNull<string>("请输入授权组名", "ModuleLog");
            this.CurrentEntities.SaveChanges();
        }

        public void Updateauthorizegrouppower(WTF.Power.Entity.sys_authorizegrouppower objsys_authorizegrouppower)
        {
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateAuthorizeGroupPower(string AuthorizeGroupID, List<RolePowerKey> objRolePowerKeyList)
        {
            using (List<string>.Enumerator enumerator = (from s in objRolePowerKeyList
                where (s.CoteModuleID != "") && !s.IsShare
                select s.CoteModuleID).Distinct<string>().ToList<string>().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Func<RolePowerKey, bool> predicate = null;
                    string coteModuleID = enumerator.Current;
                    if (predicate == null)
                    {
                        predicate = s => (s.ModuleID == coteModuleID) && (s.CoteModuleID == "");
                    }
                    if (!objRolePowerKeyList.Any<RolePowerKey>(predicate))
                    {
                        objRolePowerKeyList.Add(RolePowerKey.Create(coteModuleID));
                    }
                }
            }
            List<RolePowerKey> list2 = new List<RolePowerKey>();
            using (IEnumerator<WTF.Power.Entity.sys_authorizegrouppower> enumerator2 = (from s in this.CurrentEntities.sys_authorizegrouppower
                where s.AuthorizeGroupID == AuthorizeGroupID
                select s).GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    Func<RolePowerKey, bool> func2 = null;
                    WTF.Power.Entity.sys_authorizegrouppower objgrouppower = enumerator2.Current;
                    if (func2 == null)
                    {
                        func2 = s => (((s.ModuleID == objgrouppower.ModuleID) && (s.CoteModuleID == objgrouppower.CoteModuleID)) && ((s.CoteID == objgrouppower.CoteID) && (s.IsShare == objgrouppower.IsShare))) && (s.IsCoteSupper == objgrouppower.IsCoteSupper);
                    }
                    RolePowerKey item = objRolePowerKeyList.FirstOrDefault<RolePowerKey>(func2);
                    if (item != null)
                    {
                        objRolePowerKeyList.Remove(item);
                    }
                    else
                    {
                        this.CurrentEntities.DeleteObject(objgrouppower);
                        list2.Add(RolePowerKey.Create(objgrouppower.CoteModuleID, objgrouppower.CoteID, objgrouppower.ModuleID, objgrouppower.IsShare, objgrouppower.IsCoteSupper));
                    }
                }
            }
            this.CurrentEntities.SaveChanges();
            foreach (RolePowerKey key2 in objRolePowerKeyList)
            {
                WTF.Power.Entity.sys_authorizegrouppower _authorizegrouppower = new WTF.Power.Entity.sys_authorizegrouppower {
                    AuthorizeGroupID = AuthorizeGroupID,
                    ModuleID = key2.ModuleID,
                    CoteID = key2.CoteID,
                    CoteModuleID = key2.CoteModuleID,
                    IsShare = key2.IsShare,
                    IsCoteSupper = key2.IsCoteSupper,
                    AuthorizeGroupPowerID = Guid.NewGuid().ToString()
                };
                this.CurrentEntities.AddTosys_authorizegrouppower(_authorizegrouppower);
            }
            this.CurrentEntities.SaveChanges();
            if (this.sys_authorizegroup.First<WTF.Power.Entity.sys_authorizegroup>(s => (s.AuthorizeGroupID == AuthorizeGroupID)).IsRevertPower)
            {
                StringBuilder builder = new StringBuilder();
                if (list2.Count > 0)
                {
                    int num = 0;
                    List<string> list3 = (from s in list2
                        where (s.CoteModuleID != "") && s.IsCoteSupper
                        select s.CoteModuleID).Distinct<string>().ToList<string>();
                    (from s in list2
                        where (s.CoteModuleID != "") && !s.IsShare
                        select s.CoteModuleID).Distinct<string>().ToList<string>();
                    List<string> CoteModuleIDSupperList = (from s in (from s in this.CurrentEntities.sys_authorizegrouppower
                        where (s.CoteModuleID != "") && s.IsCoteSupper
                        select s).ToList<WTF.Power.Entity.sys_authorizegrouppower>() select s.CoteModuleID).Distinct<string>().ToList<string>();
                    list2.RemoveAll(s => CoteModuleIDSupperList.Contains(s.CoteModuleID));
                    list3.RemoveAll(s => CoteModuleIDSupperList.Contains(s));
                    foreach (RolePowerKey key3 in from s in list2
                        where s.CoteModuleID != ""
                        select s)
                    {
                        num++;
                        builder.AppendLine(" delete from sys_rolepower where ModuleID='" + key3.ModuleID + "' and  CoteID='" + key3.CoteID + "' and   CoteModuleID='" + key3.CoteModuleID + "' and IsShare=" + (key3.IsShare ? "1" : "0") + " and IsCoteSupper=" + (key3.IsCoteSupper ? "1" : "0") + " and  Sys_RolePower.Roleid in (select Roleid from  Sys_Role where  Sys_Role.AuthorizeGroupID='" + AuthorizeGroupID + "');");
                        if (num == 50)
                        {
                            this.CurrentEntities.ExecuteStoreCommand(builder.ToString(), new object[0]);
                            builder.Clear();
                            num = 0;
                        }
                    }
                    if (num > 0)
                    {
                        this.CurrentEntities.ExecuteStoreCommand(builder.ToString(), new object[0]);
                    }
                    if (list3.Count > 0)
                    {
                        num = 0;
                        using (List<string>.Enumerator enumerator5 = list3.GetEnumerator())
                        {
                            while (enumerator5.MoveNext())
                            {
                                string CoteModuleID = enumerator5.Current;
                                string commandText = "select sys_rolepower.*  from sys_rolepower where  sys_rolepower.CoteModuleID='" + CoteModuleID + "' and  sys_rolepower.RoleID IN ( select sys_role.RoleID  from sys_role where  sys_role.AuthorizeGroupID='" + AuthorizeGroupID + "')";
                                List<WTF.Power.Entity.sys_authorizegrouppower> source = (from s in this.CurrentEntities.sys_authorizegrouppower
                                    where (s.AuthorizeGroupID == AuthorizeGroupID) && (s.CoteModuleID == CoteModuleID)
                                    select s).ToList<WTF.Power.Entity.sys_authorizegrouppower>();
                                string str2 = "";
                                using (IEnumerator<WTF.Power.Entity.Sys_RolePower> enumerator6 = this.CurrentEntities.ExecuteStoreQuery<WTF.Power.Entity.Sys_RolePower>(commandText, new object[0]).GetEnumerator())
                                {
                                    while (enumerator6.MoveNext())
                                    {
                                        Func<WTF.Power.Entity.sys_authorizegrouppower, bool> func3 = null;
                                        WTF.Power.Entity.Sys_RolePower objSys_RolePower = enumerator6.Current;
                                        if (func3 == null)
                                        {
                                            func3 = s => (((s.CoteModuleID == objSys_RolePower.CoteModuleID) && (s.CoteID == objSys_RolePower.CoteID)) && ((s.ModuleID == objSys_RolePower.ModuleID) && (s.IsShare == objSys_RolePower.IsShare))) && (s.IsCoteSupper == objSys_RolePower.IsCoteSupper);
                                        }
                                        if (!source.Any<WTF.Power.Entity.sys_authorizegrouppower>(func3))
                                        {
                                            str2 = str2 + objSys_RolePower.RolePowerID.ToString() + ",";
                                            num++;
                                            if (num == 50)
                                            {
                                                str2 = str2.TrimEndComma();
                                                this.CurrentEntities.ExecuteStoreCommand("delete from sys_rolepower where RolePowerID in (" + str2.ConvertStringID() + ")", new object[0]);
                                                str2 = "";
                                                num = 0;
                                            }
                                        }
                                    }
                                }
                                if ((num > 0) && str2.IsNoNull())
                                {
                                    this.CurrentEntities.ExecuteStoreCommand("delete from sys_rolepower where RolePowerID in (" + str2.ConvertStringID() + ")", new object[0]);
                                    str2 = "";
                                }
                            }
                        }
                    }
                }
                List<string> list5 = (from s in list2
                    where s.CoteModuleID == ""
                    select s.ModuleID).ToList<string>();
                if (list5.Count > 0)
                {
                    int num2 = 0;
                    foreach (string str3 in list5)
                    {
                        num2++;
                        builder.AppendLine(" delete from sys_rolepower where ModuleID='" + str3 + "' and    CoteModuleID=''   and  Sys_RolePower.Roleid in (select Roleid from  Sys_Role where  Sys_Role.AuthorizeGroupID='" + AuthorizeGroupID + "');");
                        if (num2 == 50)
                        {
                            this.CurrentEntities.ExecuteStoreCommand(builder.ToString(), new object[0]);
                            builder.Clear();
                            num2 = 0;
                        }
                    }
                    if (num2 > 0)
                    {
                        this.CurrentEntities.ExecuteStoreCommand(builder.ToString(), new object[0]);
                    }
                }
            }
        }

        public void UpdateRoleCote(string roleID, string moduleID, List<string> coteIDString)
        {
            string str = "";
            foreach (WTF.Power.Entity.Sys_RoleCote cote in from s in this.CurrentEntities.sys_rolecote
                where (s.RoleID == roleID) && (s.ModuleID == moduleID)
                select s)
            {
                if (!coteIDString.Contains(cote.CoteID))
                {
                    this.CurrentEntities.DeleteObject(cote);
                    str = str + cote.CoteID + ",";
                }
                else
                {
                    coteIDString.Remove(cote.CoteID);
                }
            }
            this.CurrentEntities.SaveChanges();
            foreach (string str2 in coteIDString)
            {
                WTF.Power.Entity.Sys_RoleCote cote2 = new WTF.Power.Entity.Sys_RoleCote {
                    RoleID = roleID,
                    CoteID = str2,
                    ModuleID = moduleID,
                    RoleCoteID = Guid.NewGuid().ToString()
                };
                this.CurrentEntities.AddTosys_rolecote(cote2);
            }
            this.CurrentEntities.SaveChanges();
            str = str.TrimEndComma();
            WTF.Power.Entity.Sys_Role role = this.CurrentEntities.sys_role.First<WTF.Power.Entity.Sys_Role>(s => s.RoleID == roleID);
            if (str.IsNoNull() && role.IsSystem)
            {
                string format = "DELETE FROM  Sys_RoleCote where  \r\nModuleID='{0}' and Sys_RoleCote.CoteID IN ({1}) and \r\n Sys_RoleCote.Roleid in (select Roleid from \r\n Sys_Role where Sys_Role.IsSystem=0 and\r\n Sys_Role.ModuleTypeID='{2}')";
                this.CurrentEntities.ExecuteStoreCommand(string.Format(format, moduleID, str.ConvertStringID(), role.ModuleTypeID), new object[0]);
            }
        }

        public void UpdateRoleCote(string ModuleID, string CoteID, string RoleID, string moduleIDString)
        {
            List<string> list = moduleIDString.ConvertListString();
            string roleCoteID = this.CurrentEntities.sys_rolecote.First<WTF.Power.Entity.Sys_RoleCote>(s => (((s.ModuleID == ModuleID) && (s.RoleID == RoleID)) && (s.CoteID == CoteID))).RoleCoteID;
            string str = "";
            foreach (WTF.Power.Entity.Sys_RoleCotePower power in from s in this.CurrentEntities.sys_rolecotepower
                where s.RoleCoteID == roleCoteID
                select s)
            {
                if (!list.Contains(power.ModuleID))
                {
                    this.CurrentEntities.DeleteObject(power);
                    str = str + power.ModuleID + ",";
                }
                else
                {
                    list.Remove(power.ModuleID);
                }
            }
            this.CurrentEntities.SaveChanges();
            foreach (string str2 in list)
            {
                WTF.Power.Entity.Sys_RoleCotePower power2 = new WTF.Power.Entity.Sys_RoleCotePower {
                    ModuleID = str2,
                    RoleCoteID = roleCoteID,
                    RoleCotePowerID = Guid.NewGuid().ToString()
                };
                this.CurrentEntities.AddTosys_rolecotepower(power2);
            }
            this.CurrentEntities.SaveChanges();
            str = str.TrimEndComma();
            WTF.Power.Entity.Sys_Role role = this.CurrentEntities.sys_role.First<WTF.Power.Entity.Sys_Role>(s => s.RoleID == RoleID);
            if (str.IsNoNull() && role.IsSystem)
            {
                string format = "delete  from Sys_RoleCotePower where \r\nSys_RoleCotePower.ModuleID in ({1}) and \r\nSys_RoleCotePower.RoleCoteID in \r\n(\r\nselect  RoleCoteID from  Sys_RoleCote,Sys_Role\r\n where Sys_RoleCote.ModuleID='{0}'\r\n AND Sys_RoleCote.CoteID='{2}' \r\n AND  Sys_RoleCote.RoleID=Sys_Role.RoleID\r\n AND Sys_Role.IsSystem=0 AND  Sys_Role.ModuleTypeID='{3}'\r\n)";
                this.CurrentEntities.ExecuteStoreCommand(string.Format(format, new object[] { ModuleID, str.ConvertStringID(), CoteID, role.ModuleTypeID }), new object[0]);
            }
        }

        public void UpdateRoleData(WTF.Power.Entity.Sys_RoleData objSys_RoleData)
        {
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateRoleLock(string roleIDString, bool isLockOut)
        {
            foreach (WTF.Power.Entity.Sys_Role role in this.CurrentEntities.sys_role.Where("it.RoleID in {" + roleIDString.ConvertStringID() + "}", new ObjectParameter[0]))
            {
                role.IsLockOut = isLockOut;
            }
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateRolePower(string roleID, List<RolePowerKey> objRolePowerKeyList)
        {
            using (List<string>.Enumerator enumerator = (from s in objRolePowerKeyList
                where (s.CoteModuleID != "") && !s.IsShare
                select s.CoteModuleID).Distinct<string>().ToList<string>().GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Func<RolePowerKey, bool> predicate = null;
                    string coteModuleID = enumerator.Current;
                    if (predicate == null)
                    {
                        predicate = s => (s.ModuleID == coteModuleID) && (s.CoteModuleID == "");
                    }
                    if (!objRolePowerKeyList.Any<RolePowerKey>(predicate))
                    {
                        objRolePowerKeyList.Add(RolePowerKey.Create(coteModuleID));
                    }
                }
            }
            List<RolePowerKey> list2 = new List<RolePowerKey>();
            using (IEnumerator<WTF.Power.Entity.Sys_RolePower> enumerator2 = (from s in this.CurrentEntities.sys_rolepower
                where s.RoleID == roleID
                select s).GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    Func<RolePowerKey, bool> func2 = null;
                    WTF.Power.Entity.Sys_RolePower objRolePower = enumerator2.Current;
                    if (func2 == null)
                    {
                        func2 = s => (((s.ModuleID == objRolePower.ModuleID) && (s.CoteModuleID == objRolePower.CoteModuleID)) && ((s.CoteID == objRolePower.CoteID) && (s.IsShare == objRolePower.IsShare))) && (s.IsCoteSupper == objRolePower.IsCoteSupper);
                    }
                    RolePowerKey item = objRolePowerKeyList.FirstOrDefault<RolePowerKey>(func2);
                    if (item != null)
                    {
                        objRolePowerKeyList.Remove(item);
                    }
                    else
                    {
                        this.CurrentEntities.DeleteObject(objRolePower);
                        list2.Add(RolePowerKey.Create(objRolePower.CoteModuleID, objRolePower.CoteID, objRolePower.ModuleID, objRolePower.IsShare, objRolePower.IsCoteSupper));
                    }
                }
            }
            this.CurrentEntities.SaveChanges();
            this.CurrentEntities.sys_role.First<WTF.Power.Entity.Sys_Role>(s => s.RoleID == roleID);
            foreach (RolePowerKey key2 in objRolePowerKeyList)
            {
                WTF.Power.Entity.Sys_RolePower power = new WTF.Power.Entity.Sys_RolePower {
                    RoleID = roleID,
                    ModuleID = key2.ModuleID,
                    CoteID = key2.CoteID,
                    CoteModuleID = key2.CoteModuleID,
                    IsShare = key2.IsShare,
                    IsCoteSupper = key2.IsCoteSupper,
                    RolePowerID = Guid.NewGuid().ToString()
                };
                this.CurrentEntities.AddTosys_rolepower(power);
            }
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateRoleUser(string roleID, string userIDString)
        {
            List<string> list = userIDString.ConvertListString();
            foreach (WTF.Power.Entity.Sys_RoleUser user in from s in this.CurrentEntities.sys_roleuser
                where s.RoleID == roleID
                select s)
            {
                if (list.Contains(user.UserID))
                {
                    list.Remove(user.UserID);
                }
                else
                {
                    this.CurrentEntities.DeleteObject(user);
                }
            }
            this.CurrentEntities.SaveChanges();
            if (list.Count > 0)
            {
                foreach (string str in list)
                {
                    WTF.Power.Entity.Sys_RoleUser user2 = new WTF.Power.Entity.Sys_RoleUser {
                        UserID = str,
                        RoleID = roleID,
                        RoleUserID = Guid.NewGuid().ToString()
                    };
                    this.CurrentEntities.AddTosys_roleuser(user2);
                }
                this.CurrentEntities.SaveChanges();
            }
        }

        public void UpdateRoleUser(string roleID, string userIDString, string userIDNoString)
        {
            List<string> list = userIDString.ConvertListString();
            List<string> list2 = userIDNoString.ConvertListString();
            foreach (WTF.Power.Entity.Sys_RoleUser user in from s in this.CurrentEntities.sys_roleuser
                where s.RoleID == roleID
                select s)
            {
                if (list.Contains(user.UserID))
                {
                    list.Remove(user.UserID);
                }
                else if (list2.Contains(user.UserID))
                {
                    this.CurrentEntities.DeleteObject(user);
                }
            }
            this.CurrentEntities.SaveChanges();
            if (list.Count > 0)
            {
                foreach (string str in list)
                {
                    WTF.Power.Entity.Sys_RoleUser user2 = new WTF.Power.Entity.Sys_RoleUser {
                        UserID = str,
                        RoleID = roleID,
                        RoleUserID = Guid.NewGuid().ToString()
                    };
                    this.CurrentEntities.AddTosys_roleuser(user2);
                }
                this.CurrentEntities.SaveChanges();
            }
        }

        public void UpdateSuppportUser(string nickName, string jobNo, string userName, string account, bool isApprove, string email, string userID)
        {
            account.CheckIsNull("请输入帐号", LogModuleType.UserLog);
            SysAssert.CheckCondition((from p in this.CurrentEntities.sys_user
                where (p.UserID != userID) && (p.Account == account)
                select p).Count<WTF.Power.Entity.Sys_User>() > 0, "输入的帐号系统已经存在", LogModuleType.UserLog);
            if (!string.IsNullOrEmpty(email))
            {
                SysAssert.CheckCondition((from p in this.CurrentEntities.sys_user
                    where (p.UserID != userID) && (p.Email == email.Trim())
                    select p).Count<WTF.Power.Entity.Sys_User>() > 0, "输入的Email系统已经存在", LogModuleType.UserLog);
            }
            WTF.Power.Entity.Sys_User user = this.CurrentEntities.sys_user.First<WTF.Power.Entity.Sys_User>(p => p.UserID == userID);
            user.Account = account;
            user.Email = email;
            user.UserName = userName;
            user.IsApproved = isApprove;
            user.NickName = nickName;
            user.JobNo = jobNo;
            this.CurrentEntities.SaveChanges();
        }

        public void UpdateUserRole(string userID, string roleIDString)
        {
            List<string> list = roleIDString.ConvertListString();
            foreach (WTF.Power.Entity.Sys_RoleUser user in from s in this.CurrentEntities.sys_roleuser
                where s.UserID == userID
                select s)
            {
                if (list.Contains(user.RoleID))
                {
                    list.Remove(user.RoleID);
                }
                else
                {
                    this.CurrentEntities.DeleteObject(user);
                }
            }
            this.CurrentEntities.SaveChanges();
            if (list.Count > 0)
            {
                foreach (string str in list)
                {
                    WTF.Power.Entity.Sys_RoleUser user2 = new WTF.Power.Entity.Sys_RoleUser {
                        UserID = userID,
                        RoleID = str,
                        RoleUserID = Guid.NewGuid().ToString()
                    };
                    this.CurrentEntities.AddTosys_roleuser(user2);
                }
                this.CurrentEntities.SaveChanges();
            }
        }

        public void UpdateUserRole(string userID, string roleIDString, string roleIDNoString)
        {
            List<string> list = roleIDString.ConvertListString();
            List<string> list2 = roleIDNoString.ConvertListString();
            foreach (WTF.Power.Entity.Sys_RoleUser user in from s in this.CurrentEntities.sys_roleuser
                where s.UserID == userID
                select s)
            {
                if (list.Contains(user.RoleID))
                {
                    list.Remove(user.RoleID);
                }
                else if (list2.Contains(user.RoleID))
                {
                    this.CurrentEntities.DeleteObject(user);
                }
            }
            this.CurrentEntities.SaveChanges();
            if (list.Count > 0)
            {
                foreach (string str in list)
                {
                    WTF.Power.Entity.Sys_RoleUser user2 = new WTF.Power.Entity.Sys_RoleUser {
                        UserID = userID,
                        RoleID = str,
                        RoleUserID = Guid.NewGuid().ToString()
                    };
                    this.CurrentEntities.AddTosys_roleuser(user2);
                }
                this.CurrentEntities.SaveChanges();
            }
        }

        public void UpdateUserType(WTF.Power.Entity.Sys_UserType objSys_UserType)
        {
            objSys_UserType.UserTypeName.CheckIsNull<string>("请输入用户类型名称", "UserLog");
            objSys_UserType.Remark.CheckIsNull<string>("请输入备注", "UserLog");
            this.CurrentEntities.SaveChanges();
        }

        private string ValueDefaultFormant(int FileDataType)
        {
            if (FileDataType == 1)
            {
                int num = -2147483648;
                return num.ToString();
            }
            if (FileDataType == 2)
            {
                return ("'" + Guid.NewGuid().ToString() + "'");
            }
            if (FileDataType == 3)
            {
                return "False";
            }
            if (FileDataType == 4)
            {
                return ("Guid'" + Guid.NewGuid().ToString() + "'");
            }
            return "";
        }

        private string ValueFormant(string value, int FileDataType)
        {
            if (FileDataType != 1)
            {
                if (FileDataType == 2)
                {
                    value = value.ConvertStringID();
                    return value;
                }
                if ((FileDataType != 3) && (FileDataType == 4))
                {
                    value = value.ConvertGuidID();
                }
            }
            return value;
        }

        public UserEntities CurrentEntities
        {
            get
            {
                if (this.objCurrentEntities == null)
                {
                    this.objCurrentEntities = new UserEntities(EntitiesHelper.GetConnectionString<UserEntities>("WTF.Power.ConnectionString"));
                }
                return this.objCurrentEntities;
            }
        }

        public UserInfo CurrentUser
        {
            get
            {
                if (SysVariable.CurrentContext.Request.IsAuthenticated)
                {
                    if ((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Session["CurrentUser"] == null))
                    {
                        this.LoginRule(SysVariable.CurrentContext.User.Identity.Name);
                        return (UserInfo) SysVariable.CurrentContext.Session["CurrentUser"];
                    }
                    if ((SysVariable.CurrentContext != null) && (SysVariable.CurrentContext.Session["CurrentUser"] != null))
                    {
                        if (((UserInfo) SysVariable.CurrentContext.Session["CurrentUser"]).Account.ToLower() != SysVariable.CurrentContext.User.Identity.Name.ToLower())
                        {
                            SysVariable.CurrentContext.Session.Clear();
                            this.LoginRule(SysVariable.CurrentContext.User.Identity.Name);
                        }
                        return (UserInfo) SysVariable.CurrentContext.Session["CurrentUser"];
                    }
                }
                UserInfo info = new UserInfo();
                if (SysVariable.CurrentContext.Session["CurrentUser"] != null)
                {
                    info = (UserInfo) SysVariable.CurrentContext.Session["CurrentUser"];
                    if (info.UserID == Guid.Empty.ToString())
                    {
                        return info;
                    }
                }
                info = new UserInfo(Guid.Empty.ToString(), "Anonymous", "游客", "游客", 0, "", -1, "", RequestHelper.UserHostAddress, false, DateTime.Now);
                string ModuleTypeID = ((ModulePage) SysVariable.CurrentPage).ModuleTypeID.ToString();
                string GuidEmpty = Guid.Empty.ToString();
                info.RoleID = (from s in this.Sys_RoleUser
                    where ((s.UserID == GuidEmpty) && !s.Sys_Role.IsLockOut) && (s.Sys_Role.ModuleTypeID == ModuleTypeID)
                    orderby s.RoleID
                    select s.RoleID).ToList<string>();
                SysVariable.CurrentContext.Session["CurrentUser"] = info;
                SysVariable.CurrentContext.Session["SignatureID"] = info.UserID;
                return info;
            }
        }

        public ObjectQuery<WTF.Power.Entity.sys_authorizegroup> sys_authorizegroup
        {
            get
            {
                return this.CurrentEntities.sys_authorizegroup;
            }
        }

        public ObjectQuery<WTF.Power.Entity.sys_authorizegrouppower> sys_authorizegrouppower
        {
            get
            {
                return this.CurrentEntities.sys_authorizegrouppower;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_ModuleRole> Sys_ModuleRole
        {
            get
            {
                return this.CurrentEntities.sys_modulerole;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_Role> Sys_Role
        {
            get
            {
                return this.CurrentEntities.sys_role;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_RoleCote> Sys_RoleCote
        {
            get
            {
                return this.CurrentEntities.sys_rolecote;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_RoleCotePower> Sys_RoleCotePower
        {
            get
            {
                return this.CurrentEntities.sys_rolecotepower;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_RoleData> Sys_RoleData
        {
            get
            {
                return this.CurrentEntities.sys_roledata;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_RolePower> Sys_RolePower
        {
            get
            {
                return this.CurrentEntities.sys_rolepower;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_RoleUser> Sys_RoleUser
        {
            get
            {
                return this.CurrentEntities.sys_roleuser;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_User> Sys_User
        {
            get
            {
                return this.CurrentEntities.sys_user;
            }
        }

        public ObjectQuery<WTF.Power.Entity.Sys_UserType> Sys_UserType
        {
            get
            {
                return this.CurrentEntities.sys_usertype;
            }
        }
    }
}

