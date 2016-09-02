namespace WTF.Framework
{
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;

    public static class ValidateHelper
    {
        public static bool IsDataRowNullOrEmpty(this DataRow canShu)
        {
            try
            {
                return ((canShu == null) || (canShu.ItemArray.Length == 0));
            }
            catch
            {
                return true;
            }
        }

        public static bool IsDataTableNullOrEmpty(this DataTable canShu)
        {
            try
            {
                return ((null == canShu) || (canShu.Rows.Count == 0));
            }
            catch
            {
                return true;
            }
        }

        public static bool IsDecimalLessThanZero(this decimal canShu)
        {
            try
            {
                return (0M > canShu);
            }
            catch
            {
                return true;
            }
        }

        public static bool IsIntLessThanZero(this int canShu)
        {
            try
            {
                return (0 > canShu);
            }
            catch
            {
                return true;
            }
        }
    }
}

