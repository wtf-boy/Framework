namespace WTF.Power.Entity
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_UserType")]
    public class Sys_UserType : EntityObject
    {
        private string _Remark;
        private int _UserTypeID;
        private string _UserTypeName;

        public static Sys_UserType CreateSys_UserType(int userTypeID, string userTypeName, string remark)
        {
            return new Sys_UserType { UserTypeID = userTypeID, UserTypeName = userTypeName, Remark = remark };
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
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

        [EdmRelationshipNavigationProperty("UserModel", "FK_Sys_User_Sys_UserType", "sys_user"), XmlIgnore, SoapIgnore, DataMember]
        public EntityCollection<WTF.Power.Entity.Sys_User> Sys_User
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.Sys_User>("UserModel.FK_Sys_User_Sys_UserType", "sys_user");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.Sys_User>("UserModel.FK_Sys_User_Sys_UserType", "sys_user", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
        public int UserTypeID
        {
            get
            {
                return this._UserTypeID;
            }
            set
            {
                if (this._UserTypeID != value)
                {
                    this.ReportPropertyChanging("UserTypeID");
                    this._UserTypeID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("UserTypeID");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string UserTypeName
        {
            get
            {
                return this._UserTypeName;
            }
            set
            {
                this.ReportPropertyChanging("UserTypeName");
                this._UserTypeName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("UserTypeName");
            }
        }
    }
}

