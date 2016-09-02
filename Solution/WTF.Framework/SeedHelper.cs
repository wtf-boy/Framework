namespace WTF.Framework
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;

    public static class SeedHelper
    {
        public static long GetSeedNextValue(string seedCode)
        {
            MySqlParameter parameter = new MySqlParameter("p_seedCode", seedCode);
            return Convert.ToInt64(WTF.Framework.MySqlHelper.ExecuteScalar("SevenConnectionString", "Sys_GetSeedNextValue", CommandType.StoredProcedure, new MySqlParameter[] { parameter }));
        }
    }
}

