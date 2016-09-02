namespace WTF.DAL
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;

    public class Dal<T, Key> : DalBase<T, Key> where T: class, new()
    {
        public Dal(string tableName, string primaryKeyName, string connectionKeyOrConnectionString) : base(tableName, "", primaryKeyName, connectionKeyOrConnectionString)
        {
        }

        public override IList<T> GetList(MySqlDataReader reader)
        {
            return base.GetList(reader, "*");
        }
    }
}

