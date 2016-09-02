namespace WTF.Power.Entity
{
    using System;
    using System.Data.EntityClient;
    using System.Data.Objects;

    public class UserEntities : ObjectContext
    {
        private ObjectSet<WTF.Power.Entity.sys_authorizegroup> _sys_authorizegroup;
        private ObjectSet<WTF.Power.Entity.sys_authorizegrouppower> _sys_authorizegrouppower;
        private ObjectSet<Sys_ModuleRole> _sys_modulerole;
        private ObjectSet<Sys_Role> _sys_role;
        private ObjectSet<Sys_RoleCote> _sys_rolecote;
        private ObjectSet<Sys_RoleCoteInfo> _sys_rolecoteinfo;
        private ObjectSet<Sys_RoleCotePower> _sys_rolecotepower;
        private ObjectSet<Sys_RoleData> _sys_roledata;
        private ObjectSet<Sys_RoleData_Info> _sys_roledata_info;
        private ObjectSet<Sys_RolePower> _sys_rolepower;
        private ObjectSet<Sys_RoleUser> _sys_roleuser;
        private ObjectSet<Sys_User> _sys_user;
        private ObjectSet<Sys_UserType> _sys_usertype;

        public UserEntities() : base("name=UserEntities", "UserEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public UserEntities(EntityConnection connection) : base(connection, "UserEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public UserEntities(string connectionString) : base(connectionString, "UserEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public void AddTosys_authorizegroup(WTF.Power.Entity.sys_authorizegroup sys_authorizegroup)
        {
            base.AddObject("sys_authorizegroup", sys_authorizegroup);
        }

        public void AddTosys_authorizegrouppower(WTF.Power.Entity.sys_authorizegrouppower sys_authorizegrouppower)
        {
            base.AddObject("sys_authorizegrouppower", sys_authorizegrouppower);
        }

        public void AddTosys_modulerole(Sys_ModuleRole sys_ModuleRole)
        {
            base.AddObject("sys_modulerole", sys_ModuleRole);
        }

        public void AddTosys_role(Sys_Role sys_Role)
        {
            base.AddObject("sys_role", sys_Role);
        }

        public void AddTosys_rolecote(Sys_RoleCote sys_RoleCote)
        {
            base.AddObject("sys_rolecote", sys_RoleCote);
        }

        public void AddTosys_rolecoteinfo(Sys_RoleCoteInfo sys_RoleCoteInfo)
        {
            base.AddObject("sys_rolecoteinfo", sys_RoleCoteInfo);
        }

        public void AddTosys_rolecotepower(Sys_RoleCotePower sys_RoleCotePower)
        {
            base.AddObject("sys_rolecotepower", sys_RoleCotePower);
        }

        public void AddTosys_roledata(Sys_RoleData sys_RoleData)
        {
            base.AddObject("sys_roledata", sys_RoleData);
        }

        public void AddTosys_roledata_info(Sys_RoleData_Info sys_RoleData_Info)
        {
            base.AddObject("sys_roledata_info", sys_RoleData_Info);
        }

        public void AddTosys_rolepower(Sys_RolePower sys_RolePower)
        {
            base.AddObject("sys_rolepower", sys_RolePower);
        }

        public void AddTosys_roleuser(Sys_RoleUser sys_RoleUser)
        {
            base.AddObject("sys_roleuser", sys_RoleUser);
        }

        public void AddTosys_user(Sys_User sys_User)
        {
            base.AddObject("sys_user", sys_User);
        }

        public void AddTosys_usertype(Sys_UserType sys_UserType)
        {
            base.AddObject("sys_usertype", sys_UserType);
        }

        public ObjectResult<int?> CheckPowerCotePageByID(string p_UserID, string p_CoteModuleID, string p_CoteID, string p_ModuleCode, string p_ModuleTypeID)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
            ObjectParameter parameter5;
            if (p_UserID != null)
            {
                parameter = new ObjectParameter("p_UserID", p_UserID);
            }
            else
            {
                parameter = new ObjectParameter("p_UserID", typeof(string));
            }
            if (p_CoteModuleID != null)
            {
                parameter2 = new ObjectParameter("p_CoteModuleID", p_CoteModuleID);
            }
            else
            {
                parameter2 = new ObjectParameter("p_CoteModuleID", typeof(string));
            }
            if (p_CoteID != null)
            {
                parameter3 = new ObjectParameter("p_CoteID", p_CoteID);
            }
            else
            {
                parameter3 = new ObjectParameter("p_CoteID", typeof(string));
            }
            if (p_ModuleCode != null)
            {
                parameter4 = new ObjectParameter("p_ModuleCode", p_ModuleCode);
            }
            else
            {
                parameter4 = new ObjectParameter("p_ModuleCode", typeof(string));
            }
            if (p_ModuleTypeID != null)
            {
                parameter5 = new ObjectParameter("p_ModuleTypeID", p_ModuleTypeID);
            }
            else
            {
                parameter5 = new ObjectParameter("p_ModuleTypeID", typeof(string));
            }
            return base.ExecuteFunction<int?>("CheckPowerCotePageByID", new ObjectParameter[] { parameter, parameter2, parameter3, parameter4, parameter5 });
        }

        public ObjectResult<int?> CheckPowerFrameByID(string p_UserID, string p_ModuleTypeID)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            if (p_UserID != null)
            {
                parameter = new ObjectParameter("p_UserID", p_UserID);
            }
            else
            {
                parameter = new ObjectParameter("p_UserID", typeof(string));
            }
            if (p_ModuleTypeID != null)
            {
                parameter2 = new ObjectParameter("p_ModuleTypeID", p_ModuleTypeID);
            }
            else
            {
                parameter2 = new ObjectParameter("p_ModuleTypeID", typeof(string));
            }
            return base.ExecuteFunction<int?>("CheckPowerFrameByID", new ObjectParameter[] { parameter, parameter2 });
        }

        public ObjectResult<int?> CheckPowerPageByID(string p_UserID, string p_ModuleCode, string p_ModuleTypeID)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            if (p_UserID != null)
            {
                parameter = new ObjectParameter("p_UserID", p_UserID);
            }
            else
            {
                parameter = new ObjectParameter("p_UserID", typeof(string));
            }
            if (p_ModuleCode != null)
            {
                parameter2 = new ObjectParameter("p_ModuleCode", p_ModuleCode);
            }
            else
            {
                parameter2 = new ObjectParameter("p_ModuleCode", typeof(string));
            }
            if (p_ModuleTypeID != null)
            {
                parameter3 = new ObjectParameter("p_ModuleTypeID", p_ModuleTypeID);
            }
            else
            {
                parameter3 = new ObjectParameter("p_ModuleTypeID", typeof(string));
            }
            return base.ExecuteFunction<int?>("CheckPowerPageByID", new ObjectParameter[] { parameter, parameter2, parameter3 });
        }

        public ObjectResult<Sys_RoleData_Info> GetPowerDataModule(string p_ModuleTypeID, string p_ModuleCode, string p_UserID)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            if (p_ModuleTypeID != null)
            {
                parameter = new ObjectParameter("p_ModuleTypeID", p_ModuleTypeID);
            }
            else
            {
                parameter = new ObjectParameter("p_ModuleTypeID", typeof(string));
            }
            if (p_ModuleCode != null)
            {
                parameter2 = new ObjectParameter("p_ModuleCode", p_ModuleCode);
            }
            else
            {
                parameter2 = new ObjectParameter("p_ModuleCode", typeof(string));
            }
            if (p_UserID != null)
            {
                parameter3 = new ObjectParameter("p_UserID", p_UserID);
            }
            else
            {
                parameter3 = new ObjectParameter("p_UserID", typeof(string));
            }
            return base.ExecuteFunction<Sys_RoleData_Info>("GetPowerDataModule", new ObjectParameter[] { parameter, parameter2, parameter3 });
        }

        public ObjectResult<Sys_RoleData_Info> GetPowerDataModule(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, MergeOption mergeOption)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            if (p_ModuleTypeID != null)
            {
                parameter = new ObjectParameter("p_ModuleTypeID", p_ModuleTypeID);
            }
            else
            {
                parameter = new ObjectParameter("p_ModuleTypeID", typeof(string));
            }
            if (p_ModuleCode != null)
            {
                parameter2 = new ObjectParameter("p_ModuleCode", p_ModuleCode);
            }
            else
            {
                parameter2 = new ObjectParameter("p_ModuleCode", typeof(string));
            }
            if (p_UserID != null)
            {
                parameter3 = new ObjectParameter("p_UserID", p_UserID);
            }
            else
            {
                parameter3 = new ObjectParameter("p_UserID", typeof(string));
            }
            return base.ExecuteFunction<Sys_RoleData_Info>("GetPowerDataModule", mergeOption, new ObjectParameter[] { parameter, parameter2, parameter3 });
        }

        public ObjectSet<WTF.Power.Entity.sys_authorizegroup> sys_authorizegroup
        {
            get
            {
                if (this._sys_authorizegroup == null)
                {
                    this._sys_authorizegroup = base.CreateObjectSet<WTF.Power.Entity.sys_authorizegroup>("sys_authorizegroup");
                }
                return this._sys_authorizegroup;
            }
        }

        public ObjectSet<WTF.Power.Entity.sys_authorizegrouppower> sys_authorizegrouppower
        {
            get
            {
                if (this._sys_authorizegrouppower == null)
                {
                    this._sys_authorizegrouppower = base.CreateObjectSet<WTF.Power.Entity.sys_authorizegrouppower>("sys_authorizegrouppower");
                }
                return this._sys_authorizegrouppower;
            }
        }

        public ObjectSet<Sys_ModuleRole> sys_modulerole
        {
            get
            {
                if (this._sys_modulerole == null)
                {
                    this._sys_modulerole = base.CreateObjectSet<Sys_ModuleRole>("sys_modulerole");
                }
                return this._sys_modulerole;
            }
        }

        public ObjectSet<Sys_Role> sys_role
        {
            get
            {
                if (this._sys_role == null)
                {
                    this._sys_role = base.CreateObjectSet<Sys_Role>("sys_role");
                }
                return this._sys_role;
            }
        }

        public ObjectSet<Sys_RoleCote> sys_rolecote
        {
            get
            {
                if (this._sys_rolecote == null)
                {
                    this._sys_rolecote = base.CreateObjectSet<Sys_RoleCote>("sys_rolecote");
                }
                return this._sys_rolecote;
            }
        }

        public ObjectSet<Sys_RoleCoteInfo> sys_rolecoteinfo
        {
            get
            {
                if (this._sys_rolecoteinfo == null)
                {
                    this._sys_rolecoteinfo = base.CreateObjectSet<Sys_RoleCoteInfo>("sys_rolecoteinfo");
                }
                return this._sys_rolecoteinfo;
            }
        }

        public ObjectSet<Sys_RoleCotePower> sys_rolecotepower
        {
            get
            {
                if (this._sys_rolecotepower == null)
                {
                    this._sys_rolecotepower = base.CreateObjectSet<Sys_RoleCotePower>("sys_rolecotepower");
                }
                return this._sys_rolecotepower;
            }
        }

        public ObjectSet<Sys_RoleData> sys_roledata
        {
            get
            {
                if (this._sys_roledata == null)
                {
                    this._sys_roledata = base.CreateObjectSet<Sys_RoleData>("sys_roledata");
                }
                return this._sys_roledata;
            }
        }

        public ObjectSet<Sys_RoleData_Info> sys_roledata_info
        {
            get
            {
                if (this._sys_roledata_info == null)
                {
                    this._sys_roledata_info = base.CreateObjectSet<Sys_RoleData_Info>("sys_roledata_info");
                }
                return this._sys_roledata_info;
            }
        }

        public ObjectSet<Sys_RolePower> sys_rolepower
        {
            get
            {
                if (this._sys_rolepower == null)
                {
                    this._sys_rolepower = base.CreateObjectSet<Sys_RolePower>("sys_rolepower");
                }
                return this._sys_rolepower;
            }
        }

        public ObjectSet<Sys_RoleUser> sys_roleuser
        {
            get
            {
                if (this._sys_roleuser == null)
                {
                    this._sys_roleuser = base.CreateObjectSet<Sys_RoleUser>("sys_roleuser");
                }
                return this._sys_roleuser;
            }
        }

        public ObjectSet<Sys_User> sys_user
        {
            get
            {
                if (this._sys_user == null)
                {
                    this._sys_user = base.CreateObjectSet<Sys_User>("sys_user");
                }
                return this._sys_user;
            }
        }

        public ObjectSet<Sys_UserType> sys_usertype
        {
            get
            {
                if (this._sys_usertype == null)
                {
                    this._sys_usertype = base.CreateObjectSet<Sys_UserType>("sys_usertype");
                }
                return this._sys_usertype;
            }
        }
    }
}

