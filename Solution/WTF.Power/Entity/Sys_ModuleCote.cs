namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="ModuleModel", Name="Sys_ModuleCote")]
    public class Sys_ModuleCote : EntityObject
    {
        private string _Condtion;
        private string _ConnectionStringName;
        private string _CoteTableName;
        private string _CoteTitle;
        private int _IDDataType;
        private string _IDName;
        private string _IDPathName;
        private bool _IsParentUrl;
        private int _ModuleCoteID;
        private string _Name;
        private string _ParentIDName;
        private string _RootIDValue;
        private string _SortExpression;

        public static Sys_ModuleCote CreateSys_ModuleCote(int moduleCoteID, string coteTitle, string coteTableName, string iDName, string name, string parentIDName, string iDPathName, string connectionStringName, string rootIDValue, int iDDataType, bool isParentUrl, string sortExpression, string condtion)
        {
            return new Sys_ModuleCote { ModuleCoteID = moduleCoteID, CoteTitle = coteTitle, CoteTableName = coteTableName, IDName = iDName, Name = name, ParentIDName = parentIDName, IDPathName = iDPathName, ConnectionStringName = connectionStringName, RootIDValue = rootIDValue, IDDataType = iDDataType, IsParentUrl = isParentUrl, SortExpression = sortExpression, Condtion = condtion };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string Condtion
        {
            get
            {
                return this._Condtion;
            }
            set
            {
                this.ReportPropertyChanging("Condtion");
                this._Condtion = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Condtion");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string ConnectionStringName
        {
            get
            {
                return this._ConnectionStringName;
            }
            set
            {
                this.ReportPropertyChanging("ConnectionStringName");
                this._ConnectionStringName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ConnectionStringName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string CoteTableName
        {
            get
            {
                return this._CoteTableName;
            }
            set
            {
                this.ReportPropertyChanging("CoteTableName");
                this._CoteTableName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CoteTableName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string CoteTitle
        {
            get
            {
                return this._CoteTitle;
            }
            set
            {
                this.ReportPropertyChanging("CoteTitle");
                this._CoteTitle = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("CoteTitle");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int IDDataType
        {
            get
            {
                return this._IDDataType;
            }
            set
            {
                this.ReportPropertyChanging("IDDataType");
                this._IDDataType = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IDDataType");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string IDName
        {
            get
            {
                return this._IDName;
            }
            set
            {
                this.ReportPropertyChanging("IDName");
                this._IDName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("IDName");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string IDPathName
        {
            get
            {
                return this._IDPathName;
            }
            set
            {
                this.ReportPropertyChanging("IDPathName");
                this._IDPathName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("IDPathName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsParentUrl
        {
            get
            {
                return this._IsParentUrl;
            }
            set
            {
                this.ReportPropertyChanging("IsParentUrl");
                this._IsParentUrl = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsParentUrl");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public int ModuleCoteID
        {
            get
            {
                return this._ModuleCoteID;
            }
            set
            {
                if (this._ModuleCoteID != value)
                {
                    this.ReportPropertyChanging("ModuleCoteID");
                    this._ModuleCoteID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("ModuleCoteID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.ReportPropertyChanging("Name");
                this._Name = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Name");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string ParentIDName
        {
            get
            {
                return this._ParentIDName;
            }
            set
            {
                this.ReportPropertyChanging("ParentIDName");
                this._ParentIDName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("ParentIDName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string RootIDValue
        {
            get
            {
                return this._RootIDValue;
            }
            set
            {
                this.ReportPropertyChanging("RootIDValue");
                this._RootIDValue = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("RootIDValue");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string SortExpression
        {
            get
            {
                return this._SortExpression;
            }
            set
            {
                this.ReportPropertyChanging("SortExpression");
                this._SortExpression = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("SortExpression");
            }
        }
    }
}

