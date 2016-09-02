namespace WTF.Power.Entity
{
    using System;
    using System.Data.EntityClient;
    using System.Data.Objects;

    public class ModuleEntities : ObjectContext
    {
        private ObjectSet<Sys_Module> _sys_module;
        private ObjectSet<Sys_ModuleCheckData> _sys_modulecheckdata;
        private ObjectSet<Sys_ModuleCote> _sys_modulecote;
        private ObjectSet<Sys_ModuleData> _sys_moduledata;
        private ObjectSet<Sys_ModuleHelp> _sys_modulehelp;
        private ObjectSet<Sys_ModuleType> _sys_moduletype;

        public ModuleEntities() : base("name=ModuleEntities", "ModuleEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public ModuleEntities(EntityConnection connection) : base(connection, "ModuleEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public ModuleEntities(string connectionString) : base(connectionString, "ModuleEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }

        public void AddTosys_module(Sys_Module sys_Module)
        {
            base.AddObject("sys_module", sys_Module);
        }

        public void AddTosys_modulecheckdata(Sys_ModuleCheckData sys_ModuleCheckData)
        {
            base.AddObject("sys_modulecheckdata", sys_ModuleCheckData);
        }

        public void AddTosys_modulecote(Sys_ModuleCote sys_ModuleCote)
        {
            base.AddObject("sys_modulecote", sys_ModuleCote);
        }

        public void AddTosys_moduledata(Sys_ModuleData sys_ModuleData)
        {
            base.AddObject("sys_moduledata", sys_ModuleData);
        }

        public void AddTosys_modulehelp(Sys_ModuleHelp sys_ModuleHelp)
        {
            base.AddObject("sys_modulehelp", sys_ModuleHelp);
        }

        public void AddTosys_moduletype(Sys_ModuleType sys_ModuleType)
        {
            base.AddObject("sys_moduletype", sys_ModuleType);
        }

        public int DeleteModule(string p_ModuleID)
        {
            ObjectParameter parameter;
            if (p_ModuleID != null)
            {
                parameter = new ObjectParameter("p_ModuleID", p_ModuleID);
            }
            else
            {
                parameter = new ObjectParameter("p_ModuleID", typeof(string));
            }
            return base.ExecuteFunction("DeleteModule", new ObjectParameter[] { parameter });
        }

        public ObjectResult<Sys_Module> GetAuthorizeGroupPowerModule(string p_AuthorizeGroupID, string p_ModuleTypeID)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            if (p_AuthorizeGroupID != null)
            {
                parameter = new ObjectParameter("p_AuthorizeGroupID", p_AuthorizeGroupID);
            }
            else
            {
                parameter = new ObjectParameter("p_AuthorizeGroupID", typeof(string));
            }
            if (p_ModuleTypeID != null)
            {
                parameter2 = new ObjectParameter("p_ModuleTypeID", p_ModuleTypeID);
            }
            else
            {
                parameter2 = new ObjectParameter("p_ModuleTypeID", typeof(string));
            }
            return base.ExecuteFunction<Sys_Module>("GetAuthorizeGroupPowerModule", new ObjectParameter[] { parameter, parameter2 });
        }

        public ObjectResult<Sys_Module> GetAuthorizeGroupPowerModule(string p_AuthorizeGroupID, string p_ModuleTypeID, MergeOption mergeOption)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            if (p_AuthorizeGroupID != null)
            {
                parameter = new ObjectParameter("p_AuthorizeGroupID", p_AuthorizeGroupID);
            }
            else
            {
                parameter = new ObjectParameter("p_AuthorizeGroupID", typeof(string));
            }
            if (p_ModuleTypeID != null)
            {
                parameter2 = new ObjectParameter("p_ModuleTypeID", p_ModuleTypeID);
            }
            else
            {
                parameter2 = new ObjectParameter("p_ModuleTypeID", typeof(string));
            }
            return base.ExecuteFunction<Sys_Module>("GetAuthorizeGroupPowerModule", mergeOption, new ObjectParameter[] { parameter, parameter2 });
        }

        public ObjectResult<int?> GetPowerCoteOperateCommandByID(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, string p_CoteModuleID, string p_CoteID, string p_CommandName)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
            ObjectParameter parameter5;
            ObjectParameter parameter6;
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
            if (p_CoteModuleID != null)
            {
                parameter4 = new ObjectParameter("p_CoteModuleID", p_CoteModuleID);
            }
            else
            {
                parameter4 = new ObjectParameter("p_CoteModuleID", typeof(string));
            }
            if (p_CoteID != null)
            {
                parameter5 = new ObjectParameter("p_CoteID", p_CoteID);
            }
            else
            {
                parameter5 = new ObjectParameter("p_CoteID", typeof(string));
            }
            if (p_CommandName != null)
            {
                parameter6 = new ObjectParameter("p_CommandName", p_CommandName);
            }
            else
            {
                parameter6 = new ObjectParameter("p_CommandName", typeof(string));
            }
            return base.ExecuteFunction<int?>("GetPowerCoteOperateCommandByID", new ObjectParameter[] { parameter, parameter2, parameter3, parameter4, parameter5, parameter6 });
        }

        public ObjectResult<Sys_Module> GetPowerCoteOperateModuleByID(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, string p_PlaceType, string p_CoteModuleID, string p_CoteID)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
            ObjectParameter parameter5;
            ObjectParameter parameter6;
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
            if (p_PlaceType != null)
            {
                parameter4 = new ObjectParameter("p_PlaceType", p_PlaceType);
            }
            else
            {
                parameter4 = new ObjectParameter("p_PlaceType", typeof(string));
            }
            if (p_CoteModuleID != null)
            {
                parameter5 = new ObjectParameter("p_CoteModuleID", p_CoteModuleID);
            }
            else
            {
                parameter5 = new ObjectParameter("p_CoteModuleID", typeof(string));
            }
            if (p_CoteID != null)
            {
                parameter6 = new ObjectParameter("p_CoteID", p_CoteID);
            }
            else
            {
                parameter6 = new ObjectParameter("p_CoteID", typeof(string));
            }
            return base.ExecuteFunction<Sys_Module>("GetPowerCoteOperateModuleByID", new ObjectParameter[] { parameter, parameter2, parameter3, parameter4, parameter5, parameter6 });
        }

        public ObjectResult<Sys_Module> GetPowerCoteOperateModuleByID(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, string p_PlaceType, string p_CoteModuleID, string p_CoteID, MergeOption mergeOption)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
            ObjectParameter parameter5;
            ObjectParameter parameter6;
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
            if (p_PlaceType != null)
            {
                parameter4 = new ObjectParameter("p_PlaceType", p_PlaceType);
            }
            else
            {
                parameter4 = new ObjectParameter("p_PlaceType", typeof(string));
            }
            if (p_CoteModuleID != null)
            {
                parameter5 = new ObjectParameter("p_CoteModuleID", p_CoteModuleID);
            }
            else
            {
                parameter5 = new ObjectParameter("p_CoteModuleID", typeof(string));
            }
            if (p_CoteID != null)
            {
                parameter6 = new ObjectParameter("p_CoteID", p_CoteID);
            }
            else
            {
                parameter6 = new ObjectParameter("p_CoteID", typeof(string));
            }
            return base.ExecuteFunction<Sys_Module>("GetPowerCoteOperateModuleByID", mergeOption, new ObjectParameter[] { parameter, parameter2, parameter3, parameter4, parameter5, parameter6 });
        }

        public ObjectResult<Sys_Module> GetPowerFunctionModuleByID(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, bool? p_ContainChild)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
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
            if (p_ContainChild.HasValue)
            {
                parameter4 = new ObjectParameter("p_ContainChild", p_ContainChild);
            }
            else
            {
                parameter4 = new ObjectParameter("p_ContainChild", typeof(bool));
            }
            return base.ExecuteFunction<Sys_Module>("GetPowerFunctionModuleByID", new ObjectParameter[] { parameter, parameter2, parameter3, parameter4 });
        }

        public ObjectResult<Sys_Module> GetPowerFunctionModuleByID(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, bool? p_ContainChild, MergeOption mergeOption)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
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
            if (p_ContainChild.HasValue)
            {
                parameter4 = new ObjectParameter("p_ContainChild", p_ContainChild);
            }
            else
            {
                parameter4 = new ObjectParameter("p_ContainChild", typeof(bool));
            }
            return base.ExecuteFunction<Sys_Module>("GetPowerFunctionModuleByID", mergeOption, new ObjectParameter[] { parameter, parameter2, parameter3, parameter4 });
        }

        public ObjectResult<int?> GetPowerOperateCommandByID(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, string p_CommandName)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
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
            if (p_CommandName != null)
            {
                parameter4 = new ObjectParameter("p_CommandName", p_CommandName);
            }
            else
            {
                parameter4 = new ObjectParameter("p_CommandName", typeof(string));
            }
            return base.ExecuteFunction<int?>("GetPowerOperateCommandByID", new ObjectParameter[] { parameter, parameter2, parameter3, parameter4 });
        }

        public ObjectResult<Sys_Module> GetPowerOperateModuleByID(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, string p_PlaceType)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
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
            if (p_PlaceType != null)
            {
                parameter4 = new ObjectParameter("p_PlaceType", p_PlaceType);
            }
            else
            {
                parameter4 = new ObjectParameter("p_PlaceType", typeof(string));
            }
            return base.ExecuteFunction<Sys_Module>("GetPowerOperateModuleByID", new ObjectParameter[] { parameter, parameter2, parameter3, parameter4 });
        }

        public ObjectResult<Sys_Module> GetPowerOperateModuleByID(string p_ModuleTypeID, string p_ModuleCode, string p_UserID, string p_PlaceType, MergeOption mergeOption)
        {
            ObjectParameter parameter;
            ObjectParameter parameter2;
            ObjectParameter parameter3;
            ObjectParameter parameter4;
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
            if (p_PlaceType != null)
            {
                parameter4 = new ObjectParameter("p_PlaceType", p_PlaceType);
            }
            else
            {
                parameter4 = new ObjectParameter("p_PlaceType", typeof(string));
            }
            return base.ExecuteFunction<Sys_Module>("GetPowerOperateModuleByID", mergeOption, new ObjectParameter[] { parameter, parameter2, parameter3, parameter4 });
        }

        public ObjectSet<Sys_Module> sys_module
        {
            get
            {
                if (this._sys_module == null)
                {
                    this._sys_module = base.CreateObjectSet<Sys_Module>("sys_module");
                }
                return this._sys_module;
            }
        }

        public ObjectSet<Sys_ModuleCheckData> sys_modulecheckdata
        {
            get
            {
                if (this._sys_modulecheckdata == null)
                {
                    this._sys_modulecheckdata = base.CreateObjectSet<Sys_ModuleCheckData>("sys_modulecheckdata");
                }
                return this._sys_modulecheckdata;
            }
        }

        public ObjectSet<Sys_ModuleCote> sys_modulecote
        {
            get
            {
                if (this._sys_modulecote == null)
                {
                    this._sys_modulecote = base.CreateObjectSet<Sys_ModuleCote>("sys_modulecote");
                }
                return this._sys_modulecote;
            }
        }

        public ObjectSet<Sys_ModuleData> sys_moduledata
        {
            get
            {
                if (this._sys_moduledata == null)
                {
                    this._sys_moduledata = base.CreateObjectSet<Sys_ModuleData>("sys_moduledata");
                }
                return this._sys_moduledata;
            }
        }

        public ObjectSet<Sys_ModuleHelp> sys_modulehelp
        {
            get
            {
                if (this._sys_modulehelp == null)
                {
                    this._sys_modulehelp = base.CreateObjectSet<Sys_ModuleHelp>("sys_modulehelp");
                }
                return this._sys_modulehelp;
            }
        }

        public ObjectSet<Sys_ModuleType> sys_moduletype
        {
            get
            {
                if (this._sys_moduletype == null)
                {
                    this._sys_moduletype = base.CreateObjectSet<Sys_ModuleType>("sys_moduletype");
                }
                return this._sys_moduletype;
            }
        }
    }
}

