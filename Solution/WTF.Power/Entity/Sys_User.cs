namespace WTF.Power.Entity
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="UserModel", Name="Sys_User")]
    public class Sys_User : EntityObject
    {
        private string _Account;
        private string _AccountTypeID;
        private DateTime? _ActivationDate;
        private DateTime? _ApprovedDate;
        private DateTime _CreateDate;
        private string _Email;
        private int _FaildAnswerAttemptCount;
        private DateTime? _FaildAnswerAttemptStart;
        private int _FaildPasswordAttemptCount;
        private DateTime _FaildPasswordAttemptStart;
        private int _ID;
        private bool _IsActivation;
        private bool _IsAdmin;
        private bool _IsApproved;
        private bool _IsLockOut;
        private bool _IsSuper;
        private string _JobNo;
        private DateTime _LastLockOutDate;
        private DateTime _LastPasswordChangeDate;
        private string _LoginIP;
        private string _NickName;
        private string _Password;
        private string _PasswordAnswer;
        private string _PasswordQuestion;
        private string _UserID;
        private string _UserName;
        private int _UserTypeCID;

        public static Sys_User CreateSys_User(string userID, int userTypeCID, bool isSuper, bool isAdmin, string accountTypeID, string account, string password, string email, string loginIP, string passwordQuestion, string passwordAnswer, bool isApproved, bool isActivation, bool isLockOut, DateTime createDate, DateTime lastPasswordChangeDate, DateTime lastLockOutDate, int faildPasswordAttemptCount, DateTime faildPasswordAttemptStart, int faildAnswerAttemptCount, string userName, int id, string nickName)
        {
            return new Sys_User { 
                UserID = userID, UserTypeCID = userTypeCID, IsSuper = isSuper, IsAdmin = isAdmin, AccountTypeID = accountTypeID, Account = account, Password = password, Email = email, LoginIP = loginIP, PasswordQuestion = passwordQuestion, PasswordAnswer = passwordAnswer, IsApproved = isApproved, IsActivation = isActivation, IsLockOut = isLockOut, CreateDate = createDate, LastPasswordChangeDate = lastPasswordChangeDate, 
                LastLockOutDate = lastLockOutDate, FaildPasswordAttemptCount = faildPasswordAttemptCount, FaildPasswordAttemptStart = faildPasswordAttemptStart, FaildAnswerAttemptCount = faildAnswerAttemptCount, UserName = userName, ID = id, NickName = nickName
             };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string Account
        {
            get
            {
                return this._Account;
            }
            set
            {
                this.ReportPropertyChanging("Account");
                this._Account = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Account");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public string AccountTypeID
        {
            get
            {
                return this._AccountTypeID;
            }
            set
            {
                this.ReportPropertyChanging("AccountTypeID");
                this._AccountTypeID = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("AccountTypeID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? ActivationDate
        {
            get
            {
                return this._ActivationDate;
            }
            set
            {
                this.ReportPropertyChanging("ActivationDate");
                this._ActivationDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ActivationDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? ApprovedDate
        {
            get
            {
                return this._ApprovedDate;
            }
            set
            {
                this.ReportPropertyChanging("ApprovedDate");
                this._ApprovedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ApprovedDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
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
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this.ReportPropertyChanging("Email");
                this._Email = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Email");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int FaildAnswerAttemptCount
        {
            get
            {
                return this._FaildAnswerAttemptCount;
            }
            set
            {
                this.ReportPropertyChanging("FaildAnswerAttemptCount");
                this._FaildAnswerAttemptCount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FaildAnswerAttemptCount");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? FaildAnswerAttemptStart
        {
            get
            {
                return this._FaildAnswerAttemptStart;
            }
            set
            {
                this.ReportPropertyChanging("FaildAnswerAttemptStart");
                this._FaildAnswerAttemptStart = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FaildAnswerAttemptStart");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int FaildPasswordAttemptCount
        {
            get
            {
                return this._FaildPasswordAttemptCount;
            }
            set
            {
                this.ReportPropertyChanging("FaildPasswordAttemptCount");
                this._FaildPasswordAttemptCount = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FaildPasswordAttemptCount");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public DateTime FaildPasswordAttemptStart
        {
            get
            {
                return this._FaildPasswordAttemptStart;
            }
            set
            {
                this.ReportPropertyChanging("FaildPasswordAttemptStart");
                this._FaildPasswordAttemptStart = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FaildPasswordAttemptStart");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.ReportPropertyChanging("ID");
                this._ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsActivation
        {
            get
            {
                return this._IsActivation;
            }
            set
            {
                this.ReportPropertyChanging("IsActivation");
                this._IsActivation = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsActivation");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsAdmin
        {
            get
            {
                return this._IsAdmin;
            }
            set
            {
                this.ReportPropertyChanging("IsAdmin");
                this._IsAdmin = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsAdmin");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsApproved
        {
            get
            {
                return this._IsApproved;
            }
            set
            {
                this.ReportPropertyChanging("IsApproved");
                this._IsApproved = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsApproved");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public bool IsLockOut
        {
            get
            {
                return this._IsLockOut;
            }
            set
            {
                this.ReportPropertyChanging("IsLockOut");
                this._IsLockOut = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsLockOut");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsSuper
        {
            get
            {
                return this._IsSuper;
            }
            set
            {
                this.ReportPropertyChanging("IsSuper");
                this._IsSuper = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsSuper");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string JobNo
        {
            get
            {
                return this._JobNo;
            }
            set
            {
                this.ReportPropertyChanging("JobNo");
                this._JobNo = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("JobNo");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public DateTime LastLockOutDate
        {
            get
            {
                return this._LastLockOutDate;
            }
            set
            {
                this.ReportPropertyChanging("LastLockOutDate");
                this._LastLockOutDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("LastLockOutDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=false), DataMember]
        public DateTime LastPasswordChangeDate
        {
            get
            {
                return this._LastPasswordChangeDate;
            }
            set
            {
                this.ReportPropertyChanging("LastPasswordChangeDate");
                this._LastPasswordChangeDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("LastPasswordChangeDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string LoginIP
        {
            get
            {
                return this._LoginIP;
            }
            set
            {
                this.ReportPropertyChanging("LoginIP");
                this._LoginIP = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("LoginIP");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this.ReportPropertyChanging("NickName");
                this._NickName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("NickName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this.ReportPropertyChanging("Password");
                this._Password = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("Password");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string PasswordAnswer
        {
            get
            {
                return this._PasswordAnswer;
            }
            set
            {
                this.ReportPropertyChanging("PasswordAnswer");
                this._PasswordAnswer = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PasswordAnswer");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string PasswordQuestion
        {
            get
            {
                return this._PasswordQuestion;
            }
            set
            {
                this.ReportPropertyChanging("PasswordQuestion");
                this._PasswordQuestion = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PasswordQuestion");
            }
        }

        [XmlIgnore, SoapIgnore, DataMember, EdmRelationshipNavigationProperty("UserModel", "FK_User_Ref_RoleUser", "sys_roleuser")]
        public EntityCollection<WTF.Power.Entity.Sys_RoleUser> Sys_RoleUser
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<WTF.Power.Entity.Sys_RoleUser>("UserModel.FK_User_Ref_RoleUser", "sys_roleuser");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<WTF.Power.Entity.Sys_RoleUser>("UserModel.FK_User_Ref_RoleUser", "sys_roleuser", value);
                }
            }
        }

        [XmlIgnore, SoapIgnore, DataMember, EdmRelationshipNavigationProperty("UserModel", "FK_Sys_User_Sys_UserType", "sys_usertype")]
        public WTF.Power.Entity.Sys_UserType Sys_UserType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_UserType>("UserModel.FK_Sys_User_Sys_UserType", "sys_usertype").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_UserType>("UserModel.FK_Sys_User_Sys_UserType", "sys_usertype").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<WTF.Power.Entity.Sys_UserType> Sys_UserTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<WTF.Power.Entity.Sys_UserType>("UserModel.FK_Sys_User_Sys_UserType", "sys_usertype");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<WTF.Power.Entity.Sys_UserType>("UserModel.FK_Sys_User_Sys_UserType", "sys_usertype", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                if (this._UserID != value)
                {
                    this.ReportPropertyChanging("UserID");
                    this._UserID = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("UserID");
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this.ReportPropertyChanging("UserName");
                this._UserName = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("UserName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public int UserTypeCID
        {
            get
            {
                return this._UserTypeCID;
            }
            set
            {
                this.ReportPropertyChanging("UserTypeCID");
                this._UserTypeCID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("UserTypeCID");
            }
        }
    }
}

