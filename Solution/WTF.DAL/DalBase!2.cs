namespace WTF.DAL
{
    using MySql.Data.MySqlClient;
    using WTF.Framework;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public abstract class DalBase<T, Key> : DalBase where T: class, new()
    {
        public string _DataObjectParam;
        public string _PrimaryKeyName;
        public string _TableName;
        public string _TableViewName;

        public DalBase(string tableName, string tableViewName, string primaryKeyName) : this(tableName, tableViewName, primaryKeyName, "")
        {
        }

        public DalBase(string tableName, string tableViewName, string primaryKeyName, string connectionKeyOrConnectionString) : base(connectionKeyOrConnectionString)
        {
            this._DataObjectParam = string.Empty;
            this.IsPrimaryKeyInt = true;
            if (typeof(Key) == typeof(string))
            {
                this.IsPrimaryKeyInt = false;
            }
            this._TableName = tableName;
            this._TableViewName = tableViewName;
            this._PrimaryKeyName = primaryKeyName;
        }

        public virtual int AddData(IEnumerable<T> valueList)
        {
            return this.AddData(valueList, false);
        }

        public virtual int AddData(T value)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO " + this.TableName + " ");
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
                    if (info3.Name.ToLower() != this.PrimaryKeyName.ToLower())
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
                return base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            }
            int num = Convert.ToInt32(info.GetValue(value, null));
            if (!primaryKeyAttribute.Identity)
            {
                base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
                return num;
            }
            object obj2 = null;
            if (num >= 0)
            {
                if (info2 != null)
                {
                    builder.Append(string.Concat(new object[] { " SELECT ", info.Name, " FROM ", this.TableName, " WHERE GUID='", info2.GetValue(value, null), "' LIMIT 1;" }));
                }
                else
                {
                    builder.Append(" SELECT LAST_INSERT_ID();");
                }
                obj2 = base.ExecuteScalar(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
                if (obj2 == null)
                {
                    return 0;
                }
                info.SetValue(value, Convert.ToInt32(obj2), null);
            }
            else
            {
                obj2 = base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            }
            return Convert.ToInt32(obj2);
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
            builder.Append("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO " + this.TableName + " ");
            string str = "";
            List<MySqlParameter> objList = new List<MySqlParameter>();
            PrimaryKeyAttribute primaryKeyAttribute = null;
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.ToLower() != "_id")
                {
                    if (info.Name.ToLower() != this.PrimaryKeyName.ToLower())
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
                        if (info.Name.ToLower() != this.PrimaryKeyName.ToLower())
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
            obj2 = base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
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
            builder.Append("INSERT INTO " + this.TableName + " ");
            string str = "";
            List<MySqlParameter> objList = new List<MySqlParameter>();
            PrimaryKeyAttribute primaryKeyAttribute = null;
            foreach (PropertyInfo info in properties)
            {
                if (info.Name.ToLower() != "_id")
                {
                    if (info.Name.ToLower() != this.PrimaryKeyName.ToLower())
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
                        if (info.Name.ToLower() != this.PrimaryKeyName.ToLower())
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
            obj2 = base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            return Convert.ToInt32(obj2);
        }

        public bool Any(string condition)
        {
            return (this.GetTotal(condition) >= 1);
        }

        public bool Any(string condition, MySqlParameter[] parms)
        {
            return (this.GetTotal(condition, parms) >= 1);
        }

        public bool AnyView(string condition)
        {
            return (this.GetTotalView(condition) >= 1);
        }

        public bool AnyView(string condition, MySqlParameter[] parms)
        {
            return (this.GetTotalView(condition, parms) >= 1);
        }

        public virtual int ChangeMoveRecord(Key SelectID, Key TargetID, string topFieldName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("set @SelectSortID=0;set @TargetSortID=0;");
            builder.Append("select " + topFieldName + " into @SelectSortID from " + this._TableName + " where " + this.GetKeyIDCondition(SelectID) + ";");
            builder.Append("select " + topFieldName + " into @TargetSortID from " + this._TableName + " where " + this.GetKeyIDCondition(TargetID) + ";");
            builder.Append("update " + this._TableName + " set " + topFieldName + "=@TargetSortID  where " + this.GetKeyIDCondition(SelectID) + ";");
            builder.Append("update " + this._TableName + " set " + topFieldName + "=@SelectSortID  where " + this.GetKeyIDCondition(TargetID) + ";");
            return base.ExecuteNonQuery(builder.ToString(), new MySqlParameter[0]);
        }

        public virtual int Delete(IList<Key> IDList)
        {
            if (IDList.Count == 0)
            {
                return 0;
            }
            return this.Delete(this.GetKeyCondition(IDList));
        }

        public virtual int Delete(string condition)
        {
            return base.ExecuteDelete(this._TableName, condition);
        }

        public virtual int Delete(Key ID)
        {
            string condition = "";
            if (this.IsPrimaryKeyInt)
            {
                condition = this.PrimaryKeyName + "=" + ID.ToString();
            }
            else
            {
                condition = this.PrimaryKeyName + "='" + ID.ToString() + "'";
            }
            return this.Delete(condition);
        }

        public virtual int Delete(string condition, MySqlParameter[] parms)
        {
            return base.ExecuteDelete(this._TableName, condition, parms);
        }

        public virtual int DeleteIDString(string IDString)
        {
            if (string.IsNullOrWhiteSpace(IDString))
            {
                return 0;
            }
            return this.Delete(this.GetKeyCondition(IDString));
        }

        public virtual MySqlDataReader GetDataReader(string condition)
        {
            return this.GetDataReader(condition, "", "*");
        }

        public virtual MySqlDataReader GetDataReader(string condition, MySqlParameter[] parms)
        {
            return this.GetDataReader(condition, parms, "", "*");
        }

        public virtual MySqlDataReader GetDataReader(string condition, string sortExpression, string fields = "*")
        {
            return base.ExecuteReader(this._TableName, condition, sortExpression, fields);
        }

        public virtual MySqlDataReader GetDataReader(string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return base.ExecuteReader(this._TableName, condition, parms, sortExpression, fields);
        }

        public virtual MySqlDataReader GetDataReaderID(IList<Key> IDList, string sortExpression = "")
        {
            return this.GetDataReader(this.GetKeyCondition(IDList), sortExpression, "*");
        }

        public virtual MySqlDataReader GetDataReaderID(string IDString, string sortExpression = "")
        {
            return this.GetDataReader(this.GetKeyCondition(IDString), sortExpression, "*");
        }

        public MySqlDataReader GetDataReaderLimit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return base.ExecuteReaderLimit(this._TableName, condition, sortExpression, offset, limit, fields);
        }

        public MySqlDataReader GetDataReaderLimit(string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return base.ExecuteReaderLimit(this._TableName, condition, parms, sortExpression, offset, limit, fields);
        }

        public virtual IList<T> GetDataReaderListLimit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            MySqlDataReader reader = this.GetDataReaderLimit(condition, sortExpression, offset, limit, "*");
            return this.GetList(reader);
        }

        public virtual IList<T> GetDataReaderListLimit(string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            MySqlDataReader reader = this.GetDataReaderLimit(condition, parms, sortExpression, offset, limit, "*");
            return this.GetList(reader);
        }

        public virtual MySqlDataReader GetDataReaderView(string condition)
        {
            return this.GetDataReaderView(condition, "", "*");
        }

        public virtual MySqlDataReader GetDataReaderView(string condition, MySqlParameter[] parms)
        {
            return this.GetDataReaderView(condition, parms, "", "*");
        }

        public virtual MySqlDataReader GetDataReaderView(string condition, string sortExpression, string fields = "*")
        {
            return base.ExecuteReader(this._TableViewName, condition, sortExpression, fields);
        }

        public virtual MySqlDataReader GetDataReaderView(string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return base.ExecuteReader(this._TableViewName, condition, parms, sortExpression, fields);
        }

        public virtual MySqlDataReader GetDataReaderViewID(IList<Key> IDList, string sortExpression = "")
        {
            return this.GetDataReaderView(this.GetKeyCondition(IDList), sortExpression, "*");
        }

        public virtual MySqlDataReader GetDataReaderViewID(string IDString, string sortExpression = "")
        {
            return this.GetDataReaderView(this.GetKeyCondition(IDString), sortExpression, "*");
        }

        public MySqlDataReader GetDataReaderViewLimit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return base.ExecuteReaderLimit(this._TableViewName, condition, sortExpression, offset, limit, fields);
        }

        public MySqlDataReader GetDataReaderViewLimit(string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return base.ExecuteReaderLimit(this._TableViewName, condition, parms, sortExpression, offset, limit, fields);
        }

        public virtual DataSet GetDataSet(string condition)
        {
            return this.GetDataSet(condition, "", "*");
        }

        public virtual DataSet GetDataSet(string condition, MySqlParameter[] parms)
        {
            return this.GetDataSet(condition, parms, "", "*");
        }

        public virtual DataSet GetDataSet(string condition, string sortExpression, string fields = "*")
        {
            return base.ExecuteDataSet(this._TableName, condition, sortExpression, fields);
        }

        public virtual DataSet GetDataSet(string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return base.ExecuteDataSet(this._TableName, condition, parms, sortExpression, fields);
        }

        public virtual DataSet GetDataSetID(IList<Key> IDList, string sortExpression = "")
        {
            return this.GetDataSet(this.GetKeyCondition(IDList), sortExpression, "*");
        }

        public virtual DataSet GetDataSetID(string IDString, string sortExpression = "")
        {
            return this.GetDataSet(this.GetKeyCondition(IDString), sortExpression, "*");
        }

        public DataSet GetDataSetLimit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return base.ExecuteDataSetLimit(this._TableName, condition, sortExpression, offset, limit, fields);
        }

        public DataSet GetDataSetLimit(string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return base.ExecuteDataSetLimit(this._TableName, condition, parms, sortExpression, offset, limit, fields);
        }

        public virtual DataSet GetDataSetView(string condition)
        {
            return this.GetDataSetView(condition, "", "*");
        }

        public virtual DataSet GetDataSetView(string condition, MySqlParameter[] parms)
        {
            return this.GetDataSetView(condition, parms, "", "*");
        }

        public virtual DataSet GetDataSetView(string condition, string sortExpression, string fields = "*")
        {
            return base.ExecuteDataSet(this._TableViewName, condition, sortExpression, fields);
        }

        public virtual DataSet GetDataSetView(string condition, MySqlParameter[] parms, string sortExpression, string fields = "*")
        {
            return base.ExecuteDataSet(this._TableViewName, condition, parms, sortExpression, fields);
        }

        public virtual DataSet GetDataSetViewID(string IDString, string sortExpression = "")
        {
            return this.GetDataSetView(this.GetKeyCondition(IDString), sortExpression, "*");
        }

        public virtual DataSet GetDataSetViewID(IList<Key> IDList, string sortExpression = "")
        {
            return this.GetDataSetView(this.GetKeyCondition(IDList), sortExpression, "*");
        }

        public DataSet GetDataSetViewLimit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return base.ExecuteDataSetLimit(this._TableViewName, condition, sortExpression, offset, limit, fields);
        }

        public DataSet GetDataSetViewLimit(string condition, MySqlParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return base.ExecuteDataSetLimit(this._TableViewName, condition, parms, sortExpression, offset, limit, fields);
        }

        public List<FieldKey> GetFieldList<FieldKey>(string FieldName, string condition, MySqlParameter[] parms)
        {
            string commandText = "SELECT " + FieldName + " FROM " + this._TableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            DataTable table = base.ExecuteDataSet(commandText, parms).Tables[0];
            List<FieldKey> list = new List<FieldKey>();
            foreach (DataRow row in table.Rows)
            {
                list.Add((FieldKey) row[FieldName]);
            }
            return list;
        }

        public FieldType GetFieldValue<FieldType>(string FieldName, string condition, MySqlParameter[] parms)
        {
            string commandText = "SELECT " + FieldName + " FROM " + this._TableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            object obj2 = base.ExecuteScalar(commandText, parms);
            if ((obj2 == null) || (obj2 is DBNull))
            {
                return default(FieldType);
            }
            return (FieldType) obj2;
        }

        public object GetID(string condition)
        {
            return this.GetID(condition, null);
        }

        public object GetID(string condition, MySqlParameter[] parms)
        {
            string commandText = "SELECT " + this.PrimaryKeyName + " FROM " + this._TableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            return base.ExecuteScalar(commandText, parms);
        }

        public int GetIDInt(string condition)
        {
            object iD = this.GetID(condition);
            if (iD != null)
            {
                return Convert.ToInt32(iD);
            }
            return 0;
        }

        public int GetIDInt(string condition, MySqlParameter[] parms)
        {
            object iD = this.GetID(condition, parms);
            if (iD != null)
            {
                return Convert.ToInt32(iD);
            }
            return 0;
        }

        public List<Key> GetIDList(string condition, MySqlParameter[] parms)
        {
            string commandText = "SELECT " + this.PrimaryKeyName + " FROM " + this._TableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            DataTable table = base.ExecuteDataSet(commandText, parms).Tables[0];
            List<Key> list = new List<Key>();
            foreach (DataRow row in table.Rows)
            {
                list.Add((Key) row[this.PrimaryKeyName]);
            }
            return list;
        }

        public string GetIDString(string condition)
        {
            object iD = this.GetID(condition);
            if (iD != null)
            {
                return Convert.ToString(iD);
            }
            return string.Empty;
        }

        public string GetIDString(string condition, MySqlParameter[] parms)
        {
            object iD = this.GetID(condition, parms);
            if (iD != null)
            {
                return Convert.ToString(iD);
            }
            return string.Empty;
        }

        public string GetKeyCondition(string IDString)
        {
            string[] list = IDString.Split(new char[] { ',' });
            if (list.Length == 1)
            {
                if (this.IsPrimaryKeyInt)
                {
                    return (this.PrimaryKeyName + "=" + IDString);
                }
                return (this.PrimaryKeyName + "='" + IDString + "'");
            }
            if (this.IsPrimaryKeyInt)
            {
                return (this.PrimaryKeyName + " in (" + IDString + ")");
            }
            return (this.PrimaryKeyName + " in (" + list.ConvertStringID<string>() + ")");
        }

        public string GetKeyCondition(IList<Key> IDList)
        {
            if (this.IsPrimaryKeyInt)
            {
                return (this.PrimaryKeyName + " in (" + IDList.ConvertListToString<Key>() + ")");
            }
            return (this.PrimaryKeyName + " in (" + IDList.ConvertStringID<Key>() + ")");
        }

        public string GetKeyIDCondition(Key ID)
        {
            if (this.IsPrimaryKeyInt)
            {
                return (this.PrimaryKeyName + "=" + ID.ToString());
            }
            return (this.PrimaryKeyName + "='" + ID.ToString() + "'");
        }

        public abstract IList<T> GetList(MySqlDataReader reader);
        public virtual IList<T> GetList(string condition)
        {
            return this.GetList(condition, "");
        }

        public IList<T> GetList(MySqlDataReader reader, string fields)
        {
            Func<PropertyInfo, bool> predicate = null;
            IList<T> list = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> fieldsList = fields.ToLower().ConvertListString();
            try
            {
                Func<PropertyInfo, bool> func = null;
                int fieldCount = reader.FieldCount;
                string fieldName = string.Empty;
                while (reader.Read())
                {
                    T local = Activator.CreateInstance<T>();
                    if (string.IsNullOrWhiteSpace(fields) || (fields == "*"))
                    {
                        for (int i = 0; i < fieldCount; i++)
                        {
                            fieldName = reader.GetName(i);
                            if (func == null)
                            {
                                func = s => s.Name == fieldName;
                            }
                            PropertyInfo info = properties.FirstOrDefault<PropertyInfo>(func);
                            if (!((info == null) || reader.IsDBNull(i)))
                            {
                                info.SetValue(local, reader[info.Name], null);
                            }
                        }
                    }
                    else
                    {
                        if (predicate == null)
                        {
                            predicate = s => fieldsList.Contains(s.Name.ToLower());
                        }
                        foreach (PropertyInfo info in properties.Where<PropertyInfo>(predicate))
                        {
                            if ((info.Name.ToLower() != "_id") && (reader[info.Name] != DBNull.Value))
                            {
                                info.SetValue(local, reader[info.Name], null);
                            }
                        }
                    }
                    list.Add(local);
                }
            }
            finally
            {
                reader.Close();
            }
            return list;
        }

        public virtual IList<T> GetList(string condition, MySqlParameter[] parms)
        {
            return this.GetList(condition, parms, "");
        }

        public virtual IList<T> GetList(string condition, string sortExpression)
        {
            MySqlDataReader reader = this.GetDataReader(condition, sortExpression, "*");
            return this.GetList(reader);
        }

        public virtual IList<T> GetList(string condition, MySqlParameter[] parms, string sortExpression)
        {
            MySqlDataReader reader = this.GetDataReader(condition, parms, sortExpression, "*");
            return this.GetList(reader);
        }

        public virtual IList<T> GetListFields(string condition, MySqlParameter[] parms, string sortExpression, string fields)
        {
            MySqlDataReader reader = this.GetDataReader(condition, parms, sortExpression, fields);
            return this.GetList(reader, fields);
        }

        public virtual DataSet GetPage(string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.GetPage(this._TableName, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.GetPage(this._TableName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string tableName, string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return base.ExecutePage(tableName, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPage(string tableName, string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return base.ExecutePage(tableName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual IList<T> GetPageList(string condition, string sortExpression, int pageSize, int pageIndex)
        {
            MySqlDataReader reader = base.ExecutePageReaderList(this._TableName, condition, sortExpression, pageSize, pageIndex, "*");
            return this.GetList(reader);
        }

        public virtual IList<T> GetPageList(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex)
        {
            MySqlDataReader reader = base.ExecutePageReaderList(this._TableName, condition, parms, sortExpression, pageSize, pageIndex, "*");
            return this.GetList(reader);
        }

        public virtual IList<T> GetPageList(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount)
        {
            recordCount = 0;
            MySqlDataReader reader = base.ExecutePageReaderList(this._TableName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, "*");
            return this.GetList(reader);
        }

        public virtual IList<T> GetPageListFields(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields)
        {
            MySqlDataReader reader = base.ExecutePageReaderList(this._TableName, condition, parms, sortExpression, pageSize, pageIndex, fields);
            return this.GetList(reader, fields);
        }

        public virtual IList<T> GetPageListFields(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields)
        {
            MySqlDataReader reader = base.ExecutePageReaderList(this._TableName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
            return this.GetList(reader, fields);
        }

        public virtual DataSet GetPageView(string condition, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.GetPage(this._TableViewName, condition, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual DataSet GetPageView(string condition, MySqlParameter[] parms, string sortExpression, int pageSize, int pageIndex, out int recordCount, string fields = "*")
        {
            return this.GetPage(this._TableViewName, condition, parms, sortExpression, pageSize, pageIndex, out recordCount, fields);
        }

        public virtual T GetRecord(Key ID)
        {
            return this.GetRecord(this.GetKeyIDCondition(ID), "");
        }

        public virtual T GetRecord(string condition, string sortExpression = "")
        {
            return this.GetRecord(condition, null, sortExpression);
        }

        public virtual IList<T> GetRecord(IList<Key> IDList, string sortExpression = "")
        {
            MySqlDataReader reader = this.GetDataReader(this.GetKeyCondition(IDList), sortExpression, "*");
            return this.GetList(reader);
        }

        public virtual T GetRecord(string condition, MySqlParameter[] parms, string sortExpression = "")
        {
            string commandText = "SELECT * FROM " + this._TableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                commandText = commandText + " where " + condition;
            }
            if (!string.IsNullOrEmpty(sortExpression))
            {
                commandText = commandText + " order by  " + sortExpression;
            }
            commandText = commandText + "  LIMIT 1 ";
            MySqlDataReader reader = base.ExecuteReader(commandText, parms);
            IList<T> list = this.GetList(reader);
            if ((list == null) || (list.Count == 0))
            {
                return default(T);
            }
            return list[0];
        }

        public virtual IList<T> GetRecordIDString(string IDString, string sortExpression = "")
        {
            MySqlDataReader reader = this.GetDataReader(this.GetKeyCondition(IDString), sortExpression, "*");
            return this.GetList(reader);
        }

        public object GetScalar(string fieldName, string condition)
        {
            return this.GetScalar(fieldName, condition, null);
        }

        public object GetScalar(string fieldName, string condition, MySqlParameter[] parms)
        {
            string commandText = "SELECT  " + fieldName + " FROM " + this._TableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            return base.ExecuteScalar(commandText, parms);
        }

        private string GetSortKeyIDCondition(object ID)
        {
            if (this.IsPrimaryKeyInt)
            {
                return (this.PrimaryKeyName + "=" + ID.ToString());
            }
            return (this.PrimaryKeyName + "='" + ID.ToString() + "'");
        }

        private List<TableSortInfo> GetSortList(MySqlDataReader reader, string topFieldName)
        {
            List<TableSortInfo> list = new List<TableSortInfo>();
            topFieldName = topFieldName.Replace("`", "");
            try
            {
                while (reader.Read())
                {
                    TableSortInfo item = new TableSortInfo();
                    if (reader[this.PrimaryKeyName] != DBNull.Value)
                    {
                        item.ID = reader[this.PrimaryKeyName];
                    }
                    if (reader[topFieldName] != DBNull.Value)
                    {
                        item.SortValue = Convert.ToInt32(reader[topFieldName]);
                    }
                    list.Add(item);
                }
            }
            finally
            {
                reader.Close();
            }
            return list;
        }

        public int GetTotal(string condition)
        {
            return this.GetTotal(condition, null);
        }

        public int GetTotal(string condition, MySqlParameter[] parms)
        {
            int num = 0;
            string commandText = "SELECT Count(0) AS Total FROM " + this._TableName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            object obj2 = base.ExecuteScalar(commandText, parms);
            if (obj2 != null)
            {
                num = Convert.ToInt32(obj2);
            }
            return num;
        }

        public int GetTotalView(string condition)
        {
            return this.GetTotalView(condition, null);
        }

        public int GetTotalView(string condition, MySqlParameter[] parms)
        {
            int num = 0;
            string commandText = "SELECT Count(0) AS Total FROM " + this._TableViewName + " ";
            if (!string.IsNullOrEmpty(condition))
            {
                condition = " where " + condition;
                commandText = commandText + condition;
            }
            object obj2 = base.ExecuteScalar(commandText, parms);
            if (obj2 != null)
            {
                num = Convert.ToInt32(obj2);
            }
            return num;
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
            builder.Append("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO " + this.TableName + " ");
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
            obj2 = base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            return Convert.ToInt32(obj2);
        }

        public virtual int MoveRecord(Key SelectID, Key TargetID, string topFieldName, string condition = "")
        {
            MySqlDataReader reader;
            int sortValue;
            int num = Convert.ToInt32(this.GetScalar(topFieldName, this.GetKeyIDCondition(SelectID)));
            int num2 = Convert.ToInt32(this.GetScalar(topFieldName, this.GetKeyIDCondition(TargetID)));
            List<TableSortInfo> sortList = new List<TableSortInfo>();
            StringBuilder builder = new StringBuilder();
            if (num < num2)
            {
                reader = this.GetDataReader(string.Concat(new object[] { condition.IsNull() ? "" : (condition + " and "), topFieldName, ">", num, " and ", topFieldName, "<=", num2 }), topFieldName, "*");
                sortList = this.GetSortList(reader, topFieldName);
                builder.Append(string.Concat(new object[] { "update ", this._TableName, " set ", topFieldName, "=", num2, " where ", this.GetKeyIDCondition(SelectID), ";" }));
                sortValue = num;
                foreach (TableSortInfo info in from s in sortList
                    orderby s.SortValue
                    select s)
                {
                    builder.Append(string.Concat(new object[] { "update ", this._TableName, " set ", topFieldName, "=", sortValue, " where ", this.GetSortKeyIDCondition(info.ID), ";" }));
                    sortValue = info.SortValue;
                }
            }
            else
            {
                reader = this.GetDataReader(string.Concat(new object[] { condition.IsNull() ? "" : (condition + " and "), topFieldName, ">=", num2, " and ", topFieldName, "<", num }), topFieldName, "*");
                sortList = this.GetSortList(reader, topFieldName);
                builder.Append(string.Concat(new object[] { "update ", this._TableName, " set ", topFieldName, "=", num2, " where ", this.GetKeyIDCondition(SelectID), ";" }));
                sortValue = num;
                foreach (TableSortInfo info in from s in sortList
                    orderby s.SortValue descending
                    select s)
                {
                    builder.Append(string.Concat(new object[] { "update ", this._TableName, " set ", topFieldName, "=", sortValue, " where ", this.GetSortKeyIDCondition(info.ID), ";" }));
                    sortValue = info.SortValue;
                }
            }
            return base.ExecuteNonQuery(builder.ToString(), new MySqlParameter[0]);
        }

        public virtual int ReplaceAddData(T value)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("REPLACE INTO " + this.TableName + " ");
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
            return base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
        }

        public virtual int ReplaceAddData(IEnumerable<T> valueList)
        {
            if ((valueList == null) || (valueList.Count<T>() == 0))
            {
                return 0;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            builder.Append("REPLACE INSERT INTO " + this.TableName + " ");
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
            obj2 = base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
            return Convert.ToInt32(obj2);
        }

        public int TopCancel(Key ID, string topFieldName)
        {
            string commandText = "update " + this._TableName + " set " + topFieldName + "=" + this._PrimaryKeyName + " where " + this.GetKeyIDCondition(ID);
            return base.ExecuteNonQuery(commandText, new MySqlParameter[0]);
        }

        public int TopCancelIDString(string IDString, string topFieldName)
        {
            string commandText = "update " + this._TableName + " set " + topFieldName + "=" + this._PrimaryKeyName + " where " + this.GetKeyCondition(IDString);
            return base.ExecuteNonQuery(commandText, new MySqlParameter[0]);
        }

        public virtual int TopRecord(Key ID, string topFieldName, int topCount = 200)
        {
            string commandText = " set @SortCount=0;";
            string str2 = commandText;
            object obj2 = str2 + "SELECT " + topFieldName + " into  @SortCount from " + this._TableName + "  ORDER BY " + topFieldName + " DESC  LIMIT 1;";
            commandText = string.Concat(new object[] { obj2, "update ", this._TableName, " set ", topFieldName, "=@SortCount+", topCount, " where ", this.GetKeyIDCondition(ID), "; " });
            return base.ExecuteNonQuery(commandText, new MySqlParameter[0]);
        }

        public virtual int TopRecordIDString(string IDString, string topFieldName, int topCount = 200)
        {
            string commandText = " set @SortCount=0;";
            string str2 = commandText;
            object obj2 = str2 + "SELECT " + topFieldName + " into  @SortCount from " + this._TableName + "  ORDER BY " + topFieldName + " DESC  LIMIT 1;";
            commandText = string.Concat(new object[] { obj2, "Update ", this._TableName, " set ", topFieldName, "=@SortCount+", topCount, " where ", this.GetKeyCondition(IDString), "; " });
            return base.ExecuteNonQuery(commandText, new MySqlParameter[0]);
        }

        public virtual int Update(string updateFields, string condition)
        {
            return base.ExecuteUpdate(this._TableName, updateFields, condition);
        }

        public virtual int Update(string updateFields, string condition, MySqlParameter[] parms)
        {
            return base.ExecuteUpdate(this._TableName, updateFields, condition, parms);
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
                builder.Append("UPDATE " + this.TableName + " SET ");
                string str = "";
                PropertyInfo info = null;
                foreach (PropertyInfo info2 in properties)
                {
                    if (info2.Name.ToLower() != this.PrimaryKeyName.ToLower())
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
                builder.AppendFormat(" WHERE {0}=?{1};", this.PrimaryKeyName, this.PrimaryKeyName + num);
                objList.Add(new MySqlParameter("?" + this.PrimaryKeyName + num, info.GetValue(local, null)));
                num++;
            }
            return base.ExecuteNonQuery(builder.ToString(), objList.ConvertListToArray<MySqlParameter>());
        }

        public string DataObjectParam
        {
            get
            {
                return this._DataObjectParam;
            }
            set
            {
                this._DataObjectParam = value;
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

        public bool IsPrimaryKeyInt { get; set; }

        public string PrimaryKeyName
        {
            get
            {
                return this._PrimaryKeyName;
            }
        }

        public string TableName
        {
            get
            {
                return this._TableName;
            }
        }

        public string TableViewName
        {
            get
            {
                return this._TableViewName;
            }
        }
    }
}

