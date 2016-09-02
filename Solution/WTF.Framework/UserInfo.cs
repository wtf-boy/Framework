namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class UserInfo
    {
        private string _Account;
        private string _AccountTypeID;
        private int _ID;
        private string _IP;
        private bool _IsSuper;
        private string _JobNo;
        private DateTime _LoginDateTime;
        private string _NickName;
        private List<string> _RoleIDList;
        private string _UserID;
        private string _UserName;
        private int _UserTypeCID;

        public UserInfo()
        {
            this._RoleIDList = new List<string>();
        }

        public UserInfo(string userID, string account, string userName, string nickName, int id, string jobNo, int userTypeCID, string accountTypeID, string ip, bool isSuper, DateTime loginDateTime)
        {
            this._RoleIDList = new List<string>();
            this._AccountTypeID = accountTypeID;
            this._UserID = userID;
            this._Account = account;
            this._UserName = userName;
            this._UserTypeCID = userTypeCID;
            this._IP = ip;
            this._IsSuper = isSuper;
            this._LoginDateTime = loginDateTime;
            this._NickName = nickName;
            this._ID = id;
            this._JobNo = jobNo;
        }

        public string Account
        {
            get
            {
                return this._Account;
            }
            set
            {
                this._Account = value;
            }
        }

        public string AccountTypeID
        {
            get
            {
                return this._AccountTypeID;
            }
            set
            {
                this._AccountTypeID = value;
            }
        }

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public string IP
        {
            get
            {
                return this._IP;
            }
            set
            {
                this._IP = value;
            }
        }

        public bool IsSuper
        {
            get
            {
                return this._IsSuper;
            }
            set
            {
                this._IsSuper = value;
            }
        }

        public string JobNo
        {
            get
            {
                return this._JobNo;
            }
            set
            {
                this._JobNo = value;
            }
        }

        public DateTime LoginDataTime
        {
            get
            {
                return this._LoginDateTime;
            }
            set
            {
                this._LoginDateTime = value;
            }
        }

        public string NickName
        {
            get
            {
                return this._NickName;
            }
            set
            {
                this._NickName = value;
            }
        }

        public string Role
        {
            get
            {
                return this._RoleIDList.ConvertListToString<string>();
            }
        }

        public List<string> RoleID
        {
            get
            {
                return this._RoleIDList;
            }
            set
            {
                this._RoleIDList = value;
            }
        }

        public string UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        public int UserTypeCID
        {
            get
            {
                return this._UserTypeCID;
            }
            set
            {
                this._UserTypeCID = value;
            }
        }
    }
}

