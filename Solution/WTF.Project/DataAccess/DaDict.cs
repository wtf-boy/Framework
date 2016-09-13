using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using WTF.DAL;
using WTF.Framework;
using WTF.Project.DataEntity;
namespace WTF.Project.DataAccess
{
    /// <summary>
    /// 微信精选字典topwx_dict_tb数据层
    /// </summary>
    public partial class DaDict : DalBase<Dict, Int32>
    {

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionKeyOrConnectionString">数据库连接串</param>
        /// <param name="dataObjectParam">表扩展名，如：us、jp</param>
        public DaDict(string connectionKeyOrConnectionString, string dataObjectParam)
            : base("topwx_dict_tb", "topwx_dict_vw", "ID", connectionKeyOrConnectionString)
        {
            if (!string.IsNullOrWhiteSpace(dataObjectParam))
            {
                _DataObjectParam = dataObjectParam;
                _TableName = "topwx_dict_" + dataObjectParam + "_tb";
                _TableViewName = "topwx_dict_" + dataObjectParam + "_vw";
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataObjectParam">表扩展名，如：us、jp</param>
        public DaDict(string dataObjectParam)
            : this("", dataObjectParam)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DaDict()
            : this("")
        {

        }

        #endregion

        #region 对象转换

        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="reader">reader</param>
        /// <returns></returns>
        public override IList<Dict> GetList(MySqlDataReader reader)
        {
            IList<Dict> objList = new List<Dict>();
            try
            {
                while (reader.Read())
                {
                    Dict dict = new Dict();
                    if (reader["ID"] != DBNull.Value)
                        dict.ID = Convert.ToInt32(reader["ID"]);
                    if (reader["GUID"] != DBNull.Value)
                        dict.GUID = Convert.ToString(reader["GUID"]);
                    if (reader["TableName"] != DBNull.Value)
                        dict.TableName = Convert.ToString(reader["TableName"]);
                    if (reader["FieldName"] != DBNull.Value)
                        dict.FieldName = Convert.ToString(reader["FieldName"]);
                    if (reader["DictDesc"] != DBNull.Value)
                        dict.DictDesc = Convert.ToString(reader["DictDesc"]);
                    if (reader["DictValue"] != DBNull.Value)
                        dict.DictValue = Convert.ToInt32(reader["DictValue"]);
                    if (reader["Remark"] != DBNull.Value)
                        dict.Remark = Convert.ToString(reader["Remark"]);
                    if (reader["Flag"] != DBNull.Value)
                        dict.Flag = Convert.ToInt32(reader["Flag"]);
                    if (reader["SortIndex"] != DBNull.Value)
                        dict.SortIndex = Convert.ToInt32(reader["SortIndex"]);
                    if (reader["CreateUserID"] != DBNull.Value)
                        dict.CreateUserID = Convert.ToInt32(reader["CreateUserID"]);
                    if (reader["CreateDate"] != DBNull.Value)
                        dict.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                    if (reader["ModifyUserID"] != DBNull.Value)
                        dict.ModifyUserID = Convert.ToInt32(reader["ModifyUserID"]);
                    if (reader["ModifyDate"] != DBNull.Value)
                        dict.ModifyDate = Convert.ToDateTime(reader["ModifyDate"]);

                    objList.Add(dict);
                }
            }
            finally
            {
                reader.Close();
            }

            return objList;

        }


        #endregion



        #region 新增

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="dict">对象</param>
        /// <returns></returns>
        public Int32 Add(Dict dict)
        {


            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO " + _TableName + " ");
            sb.Append("(");
            sb.Append("`GUID`,");
            sb.Append("`TableName`,");
            sb.Append("`FieldName`,");
            sb.Append("`DictDesc`,");
            sb.Append("`DictValue`,");
            sb.Append("`Remark`,");
            sb.Append("`Flag`,");
            sb.Append("`SortIndex`,");
            sb.Append("`CreateUserID`,");
            sb.Append("`CreateDate`,");
            sb.Append("`ModifyUserID`,");
            sb.Append("`ModifyDate`");

            sb.Append(")");
            sb.Append(" Values(");
            sb.Append("?GUID,");
            sb.Append("?TableName,");
            sb.Append("?FieldName,");
            sb.Append("?DictDesc,");
            sb.Append("?DictValue,");
            sb.Append("?Remark,");
            sb.Append("?Flag,");
            sb.Append("?SortIndex,");
            sb.Append("?CreateUserID,");
            sb.Append("?CreateDate,");
            sb.Append("?ModifyUserID,");
            sb.Append("?ModifyDate");

            sb.Append(");");

            MySqlParameter[] param = new MySqlParameter[12];
            param[0] = new MySqlParameter("?GUID", dict.GUID);
            param[1] = new MySqlParameter("?TableName", dict.TableName);
            param[2] = new MySqlParameter("?FieldName", dict.FieldName);
            param[3] = new MySqlParameter("?DictDesc", dict.DictDesc);
            param[4] = new MySqlParameter("?DictValue", dict.DictValue);
            param[5] = new MySqlParameter("?Remark", dict.Remark);
            param[6] = new MySqlParameter("?Flag", dict.Flag);
            param[7] = new MySqlParameter("?SortIndex", dict.SortIndex);
            param[8] = new MySqlParameter("?CreateUserID", dict.CreateUserID);
            param[9] = new MySqlParameter("?CreateDate", dict.CreateDate);
            param[10] = new MySqlParameter("?ModifyUserID", dict.ModifyUserID);
            param[11] = new MySqlParameter("?ModifyDate", dict.ModifyDate);


            object objValue = null;
            if (dict.ID >= 0)
            {
                sb.Append("SELECT ID FROM " + _TableName + " WHERE GUID='" + dict.GUID + "' LIMIT 1;");
                objValue = ExecuteScalar(sb.ToString(), param);
                if (objValue == null)
                {
                    return 0;
                }
                else
                {
                    dict.ID = Convert.ToInt32(objValue);
                }
            }
            else
            {
                objValue = ExecuteNonQuery(sb.ToString(), param);
            }

            return Convert.ToInt32(objValue);


        }


        #endregion


        #region 更新
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="Dict">对象</param>
        /// <returns></returns>
        public int Update(Dict dict)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + _TableName + " SET ");
            sb.Append("`GUID`=?GUID,");
            sb.Append("`TableName`=?TableName,");
            sb.Append("`FieldName`=?FieldName,");
            sb.Append("`DictDesc`=?DictDesc,");
            sb.Append("`DictValue`=?DictValue,");
            sb.Append("`Remark`=?Remark,");
            sb.Append("`Flag`=?Flag,");
            sb.Append("`SortIndex`=?SortIndex,");
            sb.Append("`CreateUserID`=?CreateUserID,");
            sb.Append("`CreateDate`=?CreateDate,");
            sb.Append("`ModifyUserID`=?ModifyUserID,");
            sb.Append("`ModifyDate`=?ModifyDate");
            sb.Append(" WHERE ID=?ID");



            MySqlParameter[] param = new MySqlParameter[13];
            param[0] = new MySqlParameter("?GUID", dict.GUID);
            param[1] = new MySqlParameter("?TableName", dict.TableName);
            param[2] = new MySqlParameter("?FieldName", dict.FieldName);
            param[3] = new MySqlParameter("?DictDesc", dict.DictDesc);
            param[4] = new MySqlParameter("?DictValue", dict.DictValue);
            param[5] = new MySqlParameter("?Remark", dict.Remark);
            param[6] = new MySqlParameter("?Flag", dict.Flag);
            param[7] = new MySqlParameter("?SortIndex", dict.SortIndex);
            param[8] = new MySqlParameter("?CreateUserID", dict.CreateUserID);
            param[9] = new MySqlParameter("?CreateDate", dict.CreateDate);
            param[10] = new MySqlParameter("?ModifyUserID", dict.ModifyUserID);
            param[11] = new MySqlParameter("?ModifyDate", dict.ModifyDate);
            param[12] = new MySqlParameter("?ID", dict.ID);


            return ExecuteNonQuery(sb.ToString(), param);

        }

        #endregion

    }
}