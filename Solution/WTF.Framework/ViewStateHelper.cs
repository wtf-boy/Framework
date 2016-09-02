namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI;

    public static class ViewStateHelper
    {
        public static bool GetBool(this StateBag objStateBag, string key, bool defaultValue)
        {
            object obj2 = objStateBag[key];
            if (obj2 != null)
            {
                return (bool) obj2;
            }
            return defaultValue;
        }

        public static double GetDouble(this StateBag objStateBag, string key, double defaultValue)
        {
            object obj2 = objStateBag[key];
            if (obj2 != null)
            {
                return (double) obj2;
            }
            return defaultValue;
        }

        public static float GetFloat(this StateBag objStateBag, string key, float defaultValue)
        {
            object obj2 = objStateBag[key];
            if (obj2 != null)
            {
                return (float) obj2;
            }
            return defaultValue;
        }

        public static Guid GetGuid(this StateBag objStateBag, string key)
        {
            return objStateBag.GetGuid(key, Guid.Empty);
        }

        public static Guid GetGuid(this StateBag objStateBag, string key, Guid defaultValue)
        {
            object obj2 = objStateBag[key];
            if (obj2 != null)
            {
                return (Guid) obj2;
            }
            return defaultValue;
        }

        public static int GetInt(this StateBag objStateBag, string key)
        {
            return objStateBag.GetInt(key, -2147483648);
        }

        public static int GetInt(this StateBag objStateBag, string key, int defaultValue)
        {
            object obj2 = objStateBag[key];
            if (obj2 != null)
            {
                return (int) obj2;
            }
            return defaultValue;
        }

        public static string GetString(this StateBag objStateBag, string key)
        {
            return objStateBag.GetString(key, string.Empty);
        }

        public static string GetString(this StateBag objStateBag, string key, string defaultValue)
        {
            object obj2 = objStateBag[key];
            if (obj2 != null)
            {
                return (string) obj2;
            }
            return defaultValue;
        }

        public static T GetT<T>(this StateBag objStateBag, string key, T defaultValue)
        {
            object obj2 = objStateBag[key];
            if (obj2 != null)
            {
                return (T) obj2;
            }
            return defaultValue;
        }
    }
}

