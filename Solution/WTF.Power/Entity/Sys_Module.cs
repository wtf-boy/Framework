namespace WTF.Power.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ModuleModel", Name="Sys_Module")]
    public class Sys_Module : EntityObject
    {
        private string _ClickScriptFun;
        private string _CommandArgument;
        private string _CommandName;
        private int _CoteKeyID;
        private string _ImageUrl;
        private bool _IsCheckPowerData;
        private bool _IsController;
        private bool _IsDispose;
        private bool _IsEdit;
        private bool _IsMvc;
        private bool _IsPower;
        private bool _IsSupperPower;
        private int _LogCategoryID;
        private string _MenuCal;
        private string _MenuField;
        private string _MenuValue;
        private string _ModuleCode;
        private int _ModuleCoteID;
        private int _ModuleFunID;
        private string _ModuleID;
        private string _ModuleIDPath;
        private int _ModuleLevel;
        private string _ModuleName;
        private bool _ModuleShow;
        private string _ModuleTypeID;
        private int _OperateTypeID;
        private string _ParentModuleID;
        private string _PlaceType;
        private string _Remark;
        private string _ShareModuleID;
        private int _SortIndex;
        private string _TargetUrl;
        private string _ToolTip;
        private string _ValGroupName;

        public static Sys_Module CreateSys_Module(string moduleID, string moduleName, string moduleCode, string moduleTypeID, int moduleFunID, bool isDispose, int logCategoryID, string placeType, int operateTypeID, bool moduleShow, bool isEdit, string imageUrl, string commandName, string commandArgument, string clickScriptFun, string toolTip, string parentModuleID, int moduleLevel, string moduleIDPath, int sortIndex, string valGroupName, string menuField, string menuCal, string menuValue, string remark, bool isMvc, bool isController, bool isCheckPowerData, int moduleCoteID, string targetUrl, string shareModuleID, int coteKeyID, bool isPower, bool isSupperPower)
        {
            return new Sys_Module { 
                ModuleID = moduleID, ModuleName = moduleName, ModuleCode = moduleCode, ModuleTypeID = moduleTypeID, ModuleFunID = moduleFunID, IsDispose = isDispose, LogCategoryID = logCategoryID, PlaceType = placeType, OperateTypeID = operateTypeID, ModuleShow = moduleShow, IsEdit = isEdit, ImageUrl = imageUrl, CommandName = commandName, CommandArgument = commandArgument, ClickScriptFun = clickScriptFun, ToolTip = toolTip, 
                ParentModuleID = parentModuleID, ModuleLevel = moduleLevel, ModuleIDPath = moduleIDPath, SortIndex = sortIndex, ValGroupName = valGroupName, MenuField = menuField, MenuCal = menuCal, MenuValue = menuValue, Remark = remark, IsMvc = isMvc, IsController = isController, IsCheckPowerData = isCheckPowerData, ModuleCoteID = moduleCoteID, TargetUrl = targetUrl, ShareModuleID = shareModuleID, CoteKeyID = coteKeyID, 
                IsPower = isPower, IsSupperPower = isSupperPower
             };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ClickScriptFun
        {
            get
            {
                return this._ClickScriptFun;
            }
            set
            {
                this.ReportPropertyChanging("ClickScriptFun");
                this._ClickScriptFun = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ClickScriptFun");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string CommandArgument
        {
            get
            {
                return this._CommandArgument;
            }
            set
            {
                this.ReportPropertyChanging("CommandArgument");
                this._CommandArgument = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CommandArgument");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string CommandName
        {
            get
            {
                return this._CommandName;
            }
            set
            {
                this.ReportPropertyChanging("CommandName");
                this._CommandName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CommandName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int CoteKeyID
        {
            get
            {
                return this._CoteKeyID;
            }
            set
            {
                this.ReportPropertyChanging("CoteKeyID");
                this._CoteKeyID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CoteKeyID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ImageUrl
        {
            get
            {
                return this._ImageUrl;
            }
            set
            {
                this.ReportPropertyChanging("ImageUrl");
                this._ImageUrl = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ImageUrl");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsCheckPowerData
        {
            get
            {
                return this._IsCheckPowerData;
            }
            set
            {
                this.ReportPropertyChanging("IsCheckPowerData");
                this._IsCheckPowerData = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsCheckPowerData");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsController
        {
            get
            {
                return this._IsController;
            }
            set
            {
                this.ReportPropertyChanging("IsController");
                this._IsController = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsController");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsDispose
        {
            get
            {
                return this._IsDispose;
            }
            set
            {
                this.ReportPropertyChanging("IsDispose");
                this._IsDispose = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsDispose");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsEdit
        {
            get
            {
                return this._IsEdit;
            }
            set
            {
                this.ReportPropertyChanging("IsEdit");
                this._IsEdit = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsEdit");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsMvc
        {
            get
            {
                return this._IsMvc;
            }
            set
            {
                this.ReportPropertyChanging("IsMvc");
                this._IsMvc = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsMvc");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsPower
        {
            get
            {
                return this._IsPower;
            }
            set
            {
                this.ReportPropertyChanging("IsPower");
                this._IsPower = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsPower");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsSupperPower
        {
            get
            {
                return this._IsSupperPower;
            }
            set
            {
                this.ReportPropertyChanging("IsSupperPower");
                this._IsSupperPower = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsSupperPower");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int LogCategoryID
        {
            get
            {
                return this._LogCategoryID;
            }
            set
            {
                this.ReportPropertyChanging("LogCategoryID");
                this._LogCategoryID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("LogCategoryID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string MenuCal
        {
            get
            {
                return this._MenuCal;
            }
            set
            {
                this.ReportPropertyChanging("MenuCal");
                this._MenuCal = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("MenuCal");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string MenuField
        {
            get
            {
                return this._MenuField;
            }
            set
            {
                this.ReportPropertyChanging("MenuField");
                this._MenuField = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("MenuField");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string MenuValue
        {
            get
            {
                return this._MenuValue;
            }
            set
            {
                this.ReportPropertyChanging("MenuValue");
                this._MenuValue = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("MenuValue");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ModuleCode
        {
            get
            {
                return this._ModuleCode;
            }
            set
            {
                this.ReportPropertyChanging("ModuleCode");
                this._ModuleCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int ModuleCoteID
        {
            get
            {
                return this._ModuleCoteID;
            }
            set
            {
                this.ReportPropertyChanging("ModuleCoteID");
                this._ModuleCoteID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ModuleCoteID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int ModuleFunID
        {
            get
            {
                return this._ModuleFunID;
            }
            set
            {
                this.ReportPropertyChanging("ModuleFunID");
                this._ModuleFunID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ModuleFunID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string ModuleID
        {
            get
            {
                return this._ModuleID;
            }
            set
            {
                if (this._ModuleID != value)
                {
                    this.ReportPropertyChanging("ModuleID");
                    this._ModuleID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("ModuleID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ModuleIDPath
        {
            get
            {
                return this._ModuleIDPath;
            }
            set
            {
                this.ReportPropertyChanging("ModuleIDPath");
                this._ModuleIDPath = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleIDPath");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int ModuleLevel
        {
            get
            {
                return this._ModuleLevel;
            }
            set
            {
                this.ReportPropertyChanging("ModuleLevel");
                this._ModuleLevel = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ModuleLevel");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ModuleName
        {
            get
            {
                return this._ModuleName;
            }
            set
            {
                this.ReportPropertyChanging("ModuleName");
                this._ModuleName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool ModuleShow
        {
            get
            {
                return this._ModuleShow;
            }
            set
            {
                this.ReportPropertyChanging("ModuleShow");
                this._ModuleShow = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ModuleShow");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ModuleTypeID
        {
            get
            {
                return this._ModuleTypeID;
            }
            set
            {
                this.ReportPropertyChanging("ModuleTypeID");
                this._ModuleTypeID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ModuleTypeID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int OperateTypeID
        {
            get
            {
                return this._OperateTypeID;
            }
            set
            {
                this.ReportPropertyChanging("OperateTypeID");
                this._OperateTypeID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OperateTypeID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ParentModuleID
        {
            get
            {
                return this._ParentModuleID;
            }
            set
            {
                this.ReportPropertyChanging("ParentModuleID");
                this._ParentModuleID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ParentModuleID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string PlaceType
        {
            get
            {
                return this._PlaceType;
            }
            set
            {
                this.ReportPropertyChanging("PlaceType");
                this._PlaceType = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PlaceType");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this.ReportPropertyChanging("Remark");
                this._Remark = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Remark");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ShareModuleID
        {
            get
            {
                return this._ShareModuleID;
            }
            set
            {
                this.ReportPropertyChanging("ShareModuleID");
                this._ShareModuleID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ShareModuleID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int SortIndex
        {
            get
            {
                return this._SortIndex;
            }
            set
            {
                this.ReportPropertyChanging("SortIndex");
                this._SortIndex = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SortIndex");
            }
        }

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("ModuleModel", "FK_module_moduletype", "Sys_ModuleType"), SoapIgnore]
        public WTF.Power.Entity.Sys_ModuleType Sys_ModuleType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_ModuleType>("ModuleModel.FK_module_moduletype", "Sys_ModuleType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_ModuleType>("ModuleModel.FK_module_moduletype", "Sys_ModuleType").Value = value;
            }
        }

        [Browsable(false), DataMember]
        public EntityReference<WTF.Power.Entity.Sys_ModuleType> Sys_ModuleTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_ModuleType>("ModuleModel.FK_module_moduletype", "Sys_ModuleType");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.Sys_ModuleType>("ModuleModel.FK_module_moduletype", "Sys_ModuleType", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string TargetUrl
        {
            get
            {
                return this._TargetUrl;
            }
            set
            {
                this.ReportPropertyChanging("TargetUrl");
                this._TargetUrl = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("TargetUrl");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ToolTip
        {
            get
            {
                return this._ToolTip;
            }
            set
            {
                this.ReportPropertyChanging("ToolTip");
                this._ToolTip = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ToolTip");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ValGroupName
        {
            get
            {
                return this._ValGroupName;
            }
            set
            {
                this.ReportPropertyChanging("ValGroupName");
                this._ValGroupName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ValGroupName");
            }
        }
    }
}

