using System;
using System.Collections;
using System.Runtime.Serialization;
using WTF.Framework;

namespace WTF.Project.DataEntity
{
    #region Dict
    /// <summary>
    /// 微信精选字典 实体层
    /// </summary>
    [Serializable]
    public class Dict
    {
        #region Member Variables
        private System.Int32 _iD;
        private System.String _gUID;
        private System.String _tableName;
        private System.String _fieldName;
        private System.String _dictDesc;
        private System.Int32 _dictValue;
        private System.String _remark;
        private System.Int32 _flag;
        private System.Int32 _sortIndex;
        private System.Int32 _createUserID;
        private System.DateTime _createDate;
        private System.Int32 _modifyUserID;
        private System.DateTime _modifyDate;
        #endregion

        #region  Constructors
        public Dict()
        {
            _iD = 0;
            _gUID = System.Guid.NewGuid().ToString();
            _tableName = String.Empty;
            _fieldName = String.Empty;
            _dictDesc = String.Empty;
            _dictValue = 0;
            _remark = String.Empty;
            _flag = 0;
            _sortIndex = 0;
            _createUserID = 0;
            _createDate = System.DateTime.Now;
            _modifyUserID = 0;
            _modifyDate = System.DateTime.Now;
        }
        #endregion

        #region  Public Properties
        /// <summary>
        /// ID
        /// 主键
        /// </summary>
        [PrimaryKey]
        public Int32 ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
        /// <summary>
        /// GUID
        /// GUID
        /// </summary>
        public String GUID
        {
            get { return _gUID; }
            set
            {
                if (value.Length > 36)

                    throw new Exception("GUID请小于36位");
                else
                    _gUID = value;
            }
        }
        /// <summary>
        /// TableName
        /// 表名
        /// </summary>
        public String TableName
        {
            get { return _tableName; }
            set
            {
                if (value.Length > 128)

                    throw new Exception("TableName请小于128位");
                else
                    _tableName = value;
            }
        }
        /// <summary>
        /// FieldName
        /// 字段名
        /// </summary>
        public String FieldName
        {
            get { return _fieldName; }
            set
            {
                if (value.Length > 128)

                    throw new Exception("FieldName请小于128位");
                else
                    _fieldName = value;
            }
        }
        /// <summary>
        /// DictDesc
        /// 字典值描述
        /// </summary>
        public String DictDesc
        {
            get { return _dictDesc; }
            set
            {
                if (value.Length > 128)

                    throw new Exception("DictDesc请小于128位");
                else
                    _dictDesc = value;
            }
        }
        /// <summary>
        /// DictValue
        /// 字典值
        /// </summary>
        public Int32 DictValue
        {
            get { return _dictValue; }
            set { _dictValue = value; }
        }
        /// <summary>
        /// Remark
        /// 备注
        /// </summary>
        public String Remark
        {
            get { return _remark; }
            set
            {
                if (value.Length > 128)

                    throw new Exception("Remark请小于128位");
                else
                    _remark = value;
            }
        }
        /// <summary>
        /// Flag
        /// 状态 1：启用0：禁用
        /// </summary>
        public Int32 Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        /// <summary>
        /// SortIndex
        /// 排序
        /// </summary>
        public Int32 SortIndex
        {
            get { return _sortIndex; }
            set { _sortIndex = value; }
        }
        /// <summary>
        /// CreateUserID
        /// 创建用户标识
        /// </summary>
        public Int32 CreateUserID
        {
            get { return _createUserID; }
            set { _createUserID = value; }
        }
        /// <summary>
        /// CreateDate
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }
        /// <summary>
        /// ModifyUserID
        /// 修改用户标识
        /// </summary>
        public Int32 ModifyUserID
        {
            get { return _modifyUserID; }
            set { _modifyUserID = value; }
        }
        /// <summary>
        /// ModifyDate
        /// 修改时间
        /// </summary>
        public DateTime ModifyDate
        {
            get { return _modifyDate; }
            set { _modifyDate = value; }
        }
        #endregion
    }
    #endregion




}