namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="ModuleModel", Name="Sys_ModuleHelp"), DataContract(IsReference=true)]
    public class Sys_ModuleHelp : EntityObject
    {
        private DateTime _CreateDate;
        private string _FileResourceID;
        private string _FileTextResourceID;
        private string _HelpContent;
        private string _HelpTitle;
        private string _ModuleID;

        public static Sys_ModuleHelp CreateSys_ModuleHelp(string moduleID, string helpTitle, DateTime createDate, string fileResourceID, string fileTextResourceID, string helpContent)
        {
            return new Sys_ModuleHelp { ModuleID = moduleID, HelpTitle = helpTitle, CreateDate = createDate, FileResourceID = fileResourceID, FileTextResourceID = fileTextResourceID, HelpContent = helpContent };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public DateTime CreateDate
        {
            get
            {
                return this._CreateDate;
            }
            set
            {
                this.ReportPropertyChanging("CreateDate");
                this._CreateDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CreateDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string FileResourceID
        {
            get
            {
                return this._FileResourceID;
            }
            set
            {
                this.ReportPropertyChanging("FileResourceID");
                this._FileResourceID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FileResourceID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string FileTextResourceID
        {
            get
            {
                return this._FileTextResourceID;
            }
            set
            {
                this.ReportPropertyChanging("FileTextResourceID");
                this._FileTextResourceID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("FileTextResourceID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string HelpContent
        {
            get
            {
                return this._HelpContent;
            }
            set
            {
                this.ReportPropertyChanging("HelpContent");
                this._HelpContent = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("HelpContent");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string HelpTitle
        {
            get
            {
                return this._HelpTitle;
            }
            set
            {
                this.ReportPropertyChanging("HelpTitle");
                this._HelpTitle = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("HelpTitle");
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
    }
}

