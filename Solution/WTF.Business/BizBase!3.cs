namespace WTF.Business
{
    using MySql.Data.MySqlClient;
    using WTF.DAL;
    using WTF.Framework;
    using WTF.Logging;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public abstract class BizBase<D, T, Key> where D: DalBase<T, Key> where T: class, new()
    {
        protected BizBase()
        {
        }

        public virtual int AddData(T value)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO " + this.Dal.TableName + " ");
            string str = "";
            string str2 = "";
            List<MySqlParameter> objList = new List<MySqlParameter>();
            PropertyInfo info = null;
            PrimaryKeyAttribute primaryKeyAttribute = null;
            PropertyInfo info2 = null;
            foreach (PropertyInfo info3 in properties)
            {
                if (info3.Name.ToLower() != "_id")
                {
                    if (info3.Name.ToLower() != this.Dal.PrimaryKeyName.ToLower())
                    {
                        if (info3.Name.ToLower() == "guid")
                        {
                            info2 = info3;
                        }
                        str = str + info3.Name + ",";
                        str2 = str2 + "?" + info3.Name + ",";
                        objList.Add(new MySqlParameter("?" + info3.Name, info3.GetValue(value, null)));
                    }
                    else
                    {
                        info = info3;
                        primaryKeyAttribute = info3.GetPrimaryKeyAttribute();
                        if (!primaryKeyAttribute.Identity)
                        {
                            str = str + info3.Name + ",";
                            str2 = str2 + "?" + info3.Name + ",";
                            objList.Add(new MySqlParameter("?" + info3.Name, info3.GetValue(value, null)));
                        }
                    }
                }
            }
            builder.Append("(");
            builder.Append(str.TrimEndComma());
            builder.Append(")");
            builder.Append(" Values(");
            builder.Append(str2.TrimEndComma());
            builder.Append("); ");
            if (info.PropertyType == typeof(string))
            {
                return this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            }
            int num = Convert.ToInt32(info.GetValue(value, null));
            if (!primaryKeyAttribute.Identity)
            {
                this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
                return num;
            }
            object obj2 = null;
            if (num >= 0)
            {
                if (info2 != null)
                {
                    builder.Append(string.Concat(new object[] { " SELECT ", info.Name, " FROM ", this.Dal.TableName, " WHERE GUID='", info2.GetValue(value, null), "' LIMIT 1;" }));
                }
                else
                {
                    builder.Append(" SELECT LAST_INSERT_ID();");
                }
                obj2 = this.Dal.ExecuteScalar(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
                if (obj2 == null)
                {
                    return 0;
                }
                info.SetValue(value, Convert.ToInt32(obj2), null);
            }
            else
            {
                obj2 = this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            }
            return Convert.ToInt32(obj2);
        }

        public virtual int AddData(IEnumerable<T> valueList)
        {
            return this.AddData(valueList, false);
        }

        public virtual int AddData(T value, bool isIgnore)
        {
            List<T> valueList = new List<T> {
                value
            };
            return this.AddData(valueList, isIgnore);
        }

        public virtual int AddData(IEnumerable<T> valueList, bool isIgnore)
        {
            if ((valueList == null) || (valueList.Count<T>() == 0))
            {
                return 0;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO " + this.Dal.TableName + " ");
            string str = "";
            List<MySqlParameter> objList = new List<MySqlParameter>();
            PrimaryKeyAttribute primaryKeyAttribute = null;
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.ToLower() != "_id")
                {
                    if (info.Name.ToLower() != this.Dal.PrimaryKeyName.ToLower())
                    {
                        str = str + info.Name + ",";
                    }
                    else
                    {
                        primaryKeyAttribute = info.GetPrimaryKeyAttribute();
                        if (!primaryKeyAttribute.Identity)
                        {
                            str = str + info.Name + ",";
                        }
                    }
                }
            }
            List<string> list2 = new List<string>();
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" Values ");
            int num = 0;
            foreach (T local in valueList)
            {
                builder2.Append("( ");
                string str2 = "";
                foreach (PropertyInfo info in properties)
                {
                    if (info.Name.ToLower() != "_id")
                    {
                        object obj3;
                        if (info.Name.ToLower() != this.Dal.PrimaryKeyName.ToLower())
                        {
                            obj3 = str2;
                            str2 = string.Concat(new object[] { obj3, "?", info.Name, num, "," });
                            objList.Add(new MySqlParameter("?" + info.Name + num, info.GetValue(local, null)));
                            num++;
                        }
                        else if (!primaryKeyAttribute.Identity)
                        {
                            obj3 = str2;
                            str2 = string.Concat(new object[] { obj3, "?", info.Name, num, "," });
                            objList.Add(new MySqlParameter("?" + info.Name + num, info.GetValue(local, null)));
                            num++;
                        }
                    }
                }
                str2 = str2.TrimEndComma();
                builder2.Append(str2);
                builder2.Append("),");
            }
            builder.Append("(");
            builder.Append(str.TrimEndComma());
            builder.Append(")");
            builder.Append(builder2.ToString().TrimEndComma());
            builder.Append(";");
            object obj2 = null;
            obj2 = this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            return Convert.ToInt32(obj2);
        }

        public virtual int AddDataKey(T value, params string[] updateField)
        {
            List<T> valueList = new List<T> {
                value
            };
            return this.AddDataKey(valueList, updateField);
        }

        public virtual int AddDataKey(IEnumerable<T> valueList, params string[] updateField)
        {
            string str = "";
            foreach (string str2 in updateField)
            {
                str = str + string.Format("{0}=VALUES({0}),", str2);
            }
            str = str.TrimEndComma();
            return this.AddDataKeyUpdate(valueList, str);
        }

        public virtual int AddDataKeyUpdate(T value, string updateSql)
        {
            List<T> valueList = new List<T> {
                value
            };
            return this.AddDataKeyUpdate(valueList, updateSql);
        }

        public virtual int AddDataKeyUpdate(IEnumerable<T> valueList, string updateSql)
        {
            if ((valueList == null) || (valueList.Count<T>() == 0))
            {
                return 0;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO " + this.Dal.TableName + " ");
            string str = "";
            List<MySqlParameter> objList = new List<MySqlParameter>();
            PrimaryKeyAttribute primaryKeyAttribute = null;
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.ToLower() != "_id")
                {
                    if (info.Name.ToLower() != this.Dal.PrimaryKeyName.ToLower())
                    {
                        str = str + info.Name + ",";
                    }
                    else
                    {
                        primaryKeyAttribute = info.GetPrimaryKeyAttribute();
                        if (!primaryKeyAttribute.Identity)
                        {
                            str = str + info.Name + ",";
                        }
                    }
                }
            }
            List<string> list2 = new List<string>();
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" Values ");
            int num = 0;
            foreach (T local in valueList)
            {
                builder2.Append("( ");
                string str2 = "";
                foreach (PropertyInfo info in properties)
                {
                    if (info.Name.ToLower() != "_id")
                    {
                        object obj3;
                        if (info.Name.ToLower() != this.Dal.PrimaryKeyName.ToLower())
                        {
                            obj3 = str2;
                            str2 = string.Concat(new object[] { obj3, "?", info.Name, num, "," });
                            objList.Add(new MySqlParameter("?" + info.Name + num, info.GetValue(local, null)));
                            num++;
                        }
                        else if (!primaryKeyAttribute.Identity)
                        {
                            obj3 = str2;
                            str2 = string.Concat(new object[] { obj3, "?", info.Name, num, "," });
                            objList.Add(new MySqlParameter("?" + info.Name + num, info.GetValue(local, null)));
                            num++;
                        }
                    }
                }
                str2 = str2.TrimEndComma();
                builder2.Append(str2);
                builder2.Append("),");
            }
            builder.Append("(");
            builder.Append(str.TrimEndComma());
            builder.Append(")");
            builder.Append(builder2.ToString().TrimEndComma());
            if (!string.IsNullOrWhiteSpace(updateSql))
            {
                builder.Append(" on duplicate key update ");
                builder.Append(updateSql);
            }
            builder.Append(";");
            object obj2 = null;
            obj2 = this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            return Convert.ToInt32(obj2);
        }

        public bool Any(string condition)
        {
            return this.Dal.Any(condition);
        }

        public bool Any(string condition, MySqlParameter[] parms)
        {
            return this.Dal.Any(condition, parms);
        }

        public bool AnyView(string condition)
        {
            return this.Dal.AnyView(condition);
        }

        public bool AnyView(string condition, MySqlParameter[] parms)
        {
            return this.Dal.AnyView(condition, parms);
        }

        public virtual int ChangeMoveRecord(Key SelectID, Key TargetID, string topFieldName)
        {
            return this.Dal.ChangeMoveRecord(SelectID, TargetID, topFieldName);
        }

        public MySqlParameter[] CreateSqlParameter(string parameterNames, params object[] parmsValue)
        {
            return this.Dal.CreateSqlParameter(parameterNames, parmsValue);
        }

        public virtual int Delete(string condition)
        {
            return this.Dal.Delete(condition);
        }

        public virtual int Delete(IList<Key> IDList)
        {
            return this.Dal.Delete(IDList);
        }

        public virtual int Delete(Key ID)
        {
            return this.Dal.Delete(ID);
        }

        public virtual int Delete(string condition, MySqlParameter[] parms)
        {
            return this.Dal.Delete(condition, parms);
        }

        public virtual int DeleteIDString(string IDString)
        {
            return this.Dal.DeleteIDString(IDString);
        }

        public virtual IDataReader GetDataReader(string condition)
        {
            return this.Dal.GetDataReader(condition);
        }

        public virtual IDataReader GetDataReader(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetDataReader(condition, parms);
        }

        public virtual IDataReader GetDataReader(string condition, string sortExpression, string fields = "*")
        {
            return this.Dal.GetDataReader(condition, sortExpression, fields);
        }

        public virtual IDataReader GetDataReader(string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return this.Dal.GetDataReader(condition, parms, sortExpression, fields);
        }

        public virtual MySqlDataReader GetDataReaderID(IList<Key> IDList, string sortExpression = "")
        {
            return this.Dal.GetDataReaderID(IDList, sortExpression);
        }

        public virtual MySqlDataReader GetDataReaderID(string IDString, string sortExpression = "")
        {
            return this.Dal.GetDataReaderID(IDString, sortExpression);
        }

        public virtual IList<T> GetDataReaderListLimit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.Dal.GetDataReaderListLimit(condition, sortExpression, offset, limit, fields);
        }

        public virtual IList<T> GetDataReaderListLimit(string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.Dal.GetDataReaderListLimit(condition, parms, sortExpression, offset, limit, fields);
        }

        public virtual IDataReader GetDataReaderView(string condition)
        {
            return this.Dal.GetDataReaderView(condition);
        }

        public virtual IDataReader GetDataReaderView(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetDataReaderView(condition, parms);
        }

        public virtual IDataReader GetDataReaderView(string condition, string sortExpression, string fields = "*")
        {
            return this.Dal.GetDataReaderView(condition, sortExpression, fields);
        }

        public virtual IDataReader GetDataReaderView(string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return this.Dal.GetDataReaderView(condition, parms, sortExpression, fields);
        }

        public virtual MySqlDataReader GetDataReaderViewID(IList<Key> IDList, string sortExpression = "")
        {
            return this.Dal.GetDataReaderViewID(IDList, sortExpression);
        }

        public virtual MySqlDataReader GetDataReaderViewID(string IDString, string sortExpression = "")
        {
            return this.Dal.GetDataReaderViewID(IDString, sortExpression);
        }

        public virtual DataSet GetDataSet(string condition)
        {
            return this.Dal.GetDataSet(condition);
        }

        public virtual DataSet GetDataSet(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetDataSet(condition, parms);
        }

        public virtual DataSet GetDataSet(string condition, string sortExpression, string fields = "*")
        {
            return this.Dal.GetDataSet(condition, sortExpression, fields);
        }

        public virtual DataSet GetDataSet(string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return this.Dal.GetDataSet(condition, parms, sortExpression, fields);
        }

        public virtual DataSet GetDataSetID(IList<Key> IDList, string sortExpression = "")
        {
            return this.Dal.GetDataSetID(IDList, sortExpression);
        }

        public virtual DataSet GetDataSetID(string IDString, string sortExpression = "")
        {
            return this.Dal.GetDataSetID(IDString, sortExpression);
        }

        public DataSet GetDataSetLimit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.Dal.GetDataSetLimit(condition, sortExpression, offset, limit, fields);
        }

        public DataSet GetDataSetLimit(string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.Dal.GetDataSetLimit(condition, parms, sortExpression, offset, limit, fields);
        }

        public virtual DataSet GetDataSetView(string condition)
        {
            return this.Dal.GetDataSetView(condition);
        }

        public virtual DataSet GetDataSetView(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetDataSetView(condition, parms);
        }

        public virtual DataSet GetDataSetView(string condition, string sortExpression, string fields = "*")
        {
            return this.Dal.GetDataSetView(condition, sortExpression, fields);
        }

        public virtual DataSet GetDataSetView(string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return this.Dal.GetDataSetView(condition, parms, sortExpression, fields);
        }

        public virtual DataSet GetDataSetViewID(string IDString, string sortExpression = "")
        {
            return this.Dal.GetDataSetViewID(IDString, sortExpression);
        }

        public virtual DataSet GetDataSetViewID(IList<Key> IDList, string sortExpression = "")
        {
            return this.Dal.GetDataSetViewID(IDList, sortExpression);
        }

        public DataSet GetDataSetViewLimit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.Dal.GetDataSetViewLimit(condition, sortExpression, offset, limit, fields);
        }

        public DataSet GetDataSetViewLimit(string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return this.Dal.GetDataSetViewLimit(condition, parms, sortExpression, offset, limit, fields);
        }

        public string GetFieldCondition<FieldKey>(string FieldName, IList<FieldKey> IDList)
        {
            return this.GetFieldCondition<FieldKey>(FieldName, IDList.ConvertListToString<FieldKey>());
        }

        public string GetFieldCondition<FieldKey>(string FieldName, string IdString)
        {
            bool flag = typeof(FieldKey) == typeof(string);
            if (flag)
            {
                return (FieldName + " in (" + IdString.ConvertStringID() + ")");
            }
            return (FieldName + " in (" + IdString + ")");
        }

        public List<FieldKey> GetFieldList<FieldKey>(string FieldName, string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetFieldList<FieldKey>(FieldName, condition, parms);
        }

        public FieldType GetFieldValue<FieldType>(string FieldName, string condition, MySqlParameter[] parms = null)
        {
            return this.Dal.GetFieldValue<FieldType>(FieldName, condition, parms);
        }

        public object GetID(string condition)
        {
            return this.Dal.GetID(condition);
        }

        public object GetID(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetID(condition, parms);
        }

        public int GetIDInt(string condition)
        {
            return this.Dal.GetIDInt(condition);
        }

        public int GetIDInt(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetIDInt(condition, parms);
        }

        public List<Key> GetIDList(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetIDList(condition, parms);
        }

        public string GetIDString(string condition)
        {
            return this.Dal.GetIDString(condition);
        }

        public string GetIDString(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetIDString(condition, parms);
        }

        public string GetKeyCondition(string IDString)
        {
            return this.Dal.GetKeyCondition(IDString);
        }

        public string GetKeyCondition(IList<Key> IDList)
        {
            return this.Dal.GetKeyCondition(IDList);
        }

        public string GetKeyIDCondition(Key ID)
        {
            return this.Dal.GetKeyIDCondition(ID);
        }

        public virtual IList<T> GetList(string condition)
        {
            return this.Dal.GetList(condition);
        }

        public virtual IList<T> GetList(string condition, string sortExpression)
        {
            return this.Dal.GetList(condition, sortExpression);
        }

        public virtual IList<T> GetList(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetList(condition, parms);
        }

        public virtual IList<T> GetList(string condition, MySqlParameter[] parms, string sortExpression)
        {
            return this.Dal.GetList(condition, parms, sortExpression);
        }

        public virtual IList<T> GetListFields(string condition, string fields)
        {
            return this.GetListFields(condition, "", fields);
        }

        public virtual IList<T> GetListFields(string condition, string sortExpression, string fields)
        {
            return this.GetListFields(condition, null, sortExpression, fields);
        }

        public virtual IList<T> GetListFields(string condition, MySqlParameter[] parms, string sortExpression, string fields)
        {
            return this.Dal.GetListFields(condition, parms, sortExpression, fields);
        }

        public virtual DataSet GetPage(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            int recordCount = 0;
            return this.Dal.GetPage(condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.Dal.GetPage(condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string tableName, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            int recordCount = 0;
            return this.Dal.GetPage(tableName, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            int recordCount = 0;
            return this.Dal.GetPage(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string tableName, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.Dal.GetPage(tableName, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.Dal.GetPage(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            int recordCount = 0;
            return this.Dal.GetPage(tableName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.Dal.GetPage(tableName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public DataSet GetPageCommand(string commandText, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.GetPageCommand(commandText, condition, null, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public DataSet GetPageCommand(string commandText, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.Dal.ExecutePageCommand(commandText, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public DataSet GetPageCommand(string connectionKeyOrConnectionString, string commandText, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.Dal.ExecutePageCommand(connectionKeyOrConnectionString, commandText, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual IList<T> GetPageList(string condition, string sortExpression, int pageSize, int pageIndex)
        {
            return this.Dal.GetPageList(condition, sortExpression, pageSize, pageIndex);
        }

        public virtual IList<T> GetPageList(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex)
        {
            return this.Dal.GetPageList(condition, parms, sortExpression, pageSize, pageIndex);
        }

        public virtual IList<T> GetPageList(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount)
        {
            return this.Dal.GetPageList(condition, parms, sortExpression, pageSize, pageIndex, out recordCount);
        }

        public virtual IList<T> GetPageListFields(string condition, string sortExpression, int pageSize, int pageIndex, string fields)
        {
            return this.GetPageListFields(condition, null, sortExpression, pageSize, pageIndex, fields);
        }

        public virtual IList<T> GetPageListFields(string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields)
        {
            return this.GetPageListFields(condition, null, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual IList<T> GetPageListFields(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields)
        {
            return this.Dal.GetPageListFields(condition, parms, sortExpression, pageSize, pageIndex, fields);
        }

        public virtual IList<T> GetPageListFields(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields)
        {
            return this.Dal.GetPageListFields(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPageView(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            int recordCount = 0;
            return this.Dal.GetPageView(condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPageView(string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.Dal.GetPageView(condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPageView(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            int recordCount = 0;
            return this.Dal.GetPageView(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPageView(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.Dal.GetPageView(condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public T GetRecord(Key ID)
        {
            return this.Dal.GetRecord(ID);
        }

        public T GetRecord(Guid guid)
        {
            return this.Dal.GetRecord("Guid='" + guid + "'", "");
        }

        public virtual T GetRecord(string condition, string sortExpression = "")
        {
            return this.Dal.GetRecord(condition, sortExpression);
        }

        public IList<T> GetRecord(IList<Key> IDList, string sortExpression = "")
        {
            return this.Dal.GetRecord(IDList, sortExpression);
        }

        public virtual T GetRecord(string condition, MySqlParameter[] parms, string sortExpression = "")
        {
            return this.Dal.GetRecord(condition, parms, sortExpression);
        }

        public IList<T> GetRecordIDString(string IDString, string sortExpression = "")
        {
            return this.Dal.GetRecordIDString(IDString, sortExpression);
        }

        public object GetScalar(string fieldName, string condition)
        {
            return this.Dal.GetScalar(fieldName, condition);
        }

        public object GetScalar(string fieldName, string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetScalar(fieldName, condition, parms);
        }

        public virtual int GetTotal(string condition)
        {
            return this.Dal.GetTotal(condition);
        }

        public virtual int GetTotal(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetTotal(condition, parms);
        }

        public virtual int GetTotalView(string condition)
        {
            return this.Dal.GetTotalView(condition);
        }

        public virtual int GetTotalView(string condition, MySqlParameter[] parms)
        {
            return this.Dal.GetTotalView(condition, parms);
        }

        public virtual int ImportAddData(T value)
        {
            return this.ImportAddData(value, false);
        }

        public virtual int ImportAddData(IEnumerable<T> valueList)
        {
            return this.ImportAddData(valueList, false);
        }

        public virtual int ImportAddData(T value, bool isIgnore)
        {
            List<T> valueList = new List<T> {
                value
            };
            return this.ImportAddData(valueList, isIgnore);
        }

        public virtual int ImportAddData(IEnumerable<T> valueList, bool isIgnore)
        {
            if ((valueList == null) || (valueList.Count<T>() == 0))
            {
                return 0;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO " + this.Dal.TableName + " ");
            string str = "";
            List<MySqlParameter> objList = new List<MySqlParameter>();
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.ToLower() != "_id")
                {
                    str = str + info.Name + ",";
                }
            }
            List<string> list2 = new List<string>();
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" Values ");
            int num = 0;
            foreach (T local in valueList)
            {
                builder2.Append("( ");
                string str2 = "";
                foreach (PropertyInfo info in properties)
                {
                    if (info.Name.ToLower() != "_id")
                    {
                        object obj3 = str2;
                        str2 = string.Concat(new object[] { obj3, "?", info.Name, num, "," });
                        objList.Add(new MySqlParameter("?" + info.Name + num, info.GetValue(local, null)));
                        num++;
                    }
                }
                str2 = str2.TrimEndComma();
                builder2.Append(str2);
                builder2.Append("),");
            }
            builder.Append("(");
            builder.Append(str.TrimEndComma());
            builder.Append(")");
            builder.Append(builder2.ToString().TrimEndComma());
            builder.Append(";");
            object obj2 = null;
            obj2 = this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            return Convert.ToInt32(obj2);
        }

        public virtual int MoveRecord(Key SelectID, Key TargetID, string topFieldName, string condition = "")
        {
            return this.Dal.MoveRecord(SelectID, TargetID, topFieldName, condition);
        }

        public virtual int ReplaceAddData(T value)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("REPLACE INTO " + this.Dal.TableName + " ");
            string str = "";
            string str2 = "";
            List<MySqlParameter> objList = new List<MySqlParameter>();
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.ToLower() != "_id")
                {
                    str = str + info.Name + ",";
                    str2 = str2 + "?" + info.Name + ",";
                    objList.Add(new MySqlParameter("?" + info.Name, info.GetValue(value, null)));
                }
            }
            builder.Append("(");
            builder.Append(str.TrimEndComma());
            builder.Append(")");
            builder.Append(" Values(");
            builder.Append(str2.TrimEndComma());
            builder.Append(");");
            return this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
        }

        public virtual int ReplaceAddData(IEnumerable<T> valueList)
        {
            if ((valueList == null) || (valueList.Count<T>() == 0))
            {
                return 0;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("REPLACE INTO " + this.Dal.TableName + " ");
            string str = "";
            List<MySqlParameter> objList = new List<MySqlParameter>();
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.ToLower() != "_id")
                {
                    str = str + info.Name + ",";
                }
            }
            List<string> list2 = new List<string>();
            StringBuilder builder2 = new StringBuilder();
            builder2.Append(" Values ");
            int num = 0;
            foreach (T local in valueList)
            {
                builder2.Append("( ");
                string str2 = "";
                foreach (PropertyInfo info in properties)
                {
                    if (info.Name.ToLower() != "_id")
                    {
                        object obj3 = str2;
                        str2 = string.Concat(new object[] { obj3, "?", info.Name, num, "," });
                        objList.Add(new MySqlParameter("?" + info.Name + num, info.GetValue(local, null)));
                        num++;
                    }
                }
                str2 = str2.TrimEndComma();
                builder2.Append(str2);
                builder2.Append("),");
            }
            builder.Append("(");
            builder.Append(str.TrimEndComma());
            builder.Append(")");
            builder.Append(builder2.ToString().TrimEndComma());
            builder.Append(";");
            object obj2 = null;
            obj2 = this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            return Convert.ToInt32(obj2);
        }

        public int TopCancel(Key ID, string topFieldName)
        {
            return this.Dal.TopCancel(ID, topFieldName);
        }

        public int TopCancelIDString(string IDString, string topFieldName)
        {
            return this.Dal.TopCancelIDString(IDString, topFieldName);
        }

        public virtual int TopRecord(Key ID, string topFieldName, int topCount = 200)
        {
            return this.Dal.TopRecord(ID, topFieldName, topCount);
        }

        public virtual int TopRecordIDString(string IDString, string topFieldName, int topCount = 200)
        {
            return this.Dal.TopRecordIDString(IDString, topFieldName, topCount);
        }

        public virtual int Update(string updateFields, string condition)
        {
            return this.Dal.Update(updateFields, condition);
        }

        public virtual int Update(string updateFields, string condition, MySqlParameter[] parms)
        {
            return this.Dal.Update(updateFields, condition, parms);
        }

        public virtual int UpdateData(T value, params string[] FieldName)
        {
            List<T> valueList = new List<T> {
                value
            };
            return this.UpdateData(valueList, FieldName);
        }

        public virtual int UpdateData(IEnumerable<T> valueList, params string[] FieldName)
        {
            if (valueList.Count<T>() == 0)
            {
                return 0;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            int num = 0;
            List<MySqlParameter> objList = new List<MySqlParameter>();
            foreach (T local in valueList)
            {
                builder.Append("UPDATE " + this.Dal.TableName + " SET ");
                string str = "";
                PropertyInfo info = null;
                foreach (PropertyInfo info2 in properties)
                {
                    if (info2.Name.ToLower() != this.Dal.PrimaryKeyName.ToLower())
                    {
                        if ((info2.Name.ToLower() != "_id") && ((FieldName.Length == 0) || FieldName.Contains<string>(info2.Name)))
                        {
                            str = str + string.Format("{0}=?{1},", info2.Name, info2.Name + num);
                            objList.Add(new MySqlParameter("?" + info2.Name + num, info2.GetValue(local, null)));
                            num++;
                        }
                    }
                    else
                    {
                        info = info2;
                    }
                }
                builder.Append(str.TrimEndComma());
                builder.AppendFormat(" WHERE {0}=?{1};", this.Dal.PrimaryKeyName, this.Dal.PrimaryKeyName + num);
                objList.Add(new MySqlParameter("?" + this.Dal.PrimaryKeyName + num, info.GetValue(local, null)));
                num++;
            }
            return this.Dal.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
        }

        public abstract D Dal { get; }

        public Type GetDalType
        {
            get
            {
                return typeof(D);
            }
        }

        public Type GetEntityType
        {
            get
            {
                return typeof(T);
            }
        }

        public Type GetKeyType
        {
            get
            {
                return typeof(Key);
            }
        }

        public Logger Log
        {
            get
            {
                return this.Dal.Log;
            }
        }

        public virtual string LogModuleType
        {
            get
            {
                return this.Dal.Log.LogModuleType;
            }
            set
            {
                this.Dal.Log.LogModuleType = value;
            }
        }
    }
}

