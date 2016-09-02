namespace WTF.Pages
{
    using WTF.Framework;
    using System;
    using System.Data.Objects;
    using System.Runtime.CompilerServices;

    public static class PageFieldData
    {
        public static ObjectQuery<T> WhereFieldData<T>(this ObjectQuery<T> objObjectQuery, string fieldName)
        {
            ModulePage currentHandler = (ModulePage) SysVariable.CurrentHandler;
            string str = "";
            if (!currentHandler.IsPowerDataCheck)
            {
            }
            if (currentHandler.CheckIsPowerData(currentHandler.ModuleTypeID, currentHandler.PowerPageCode))
            {
                str = currentHandler.CheckPowerFieldData(currentHandler.ModuleTypeID, currentHandler.PowerPageCode, fieldName, fieldName);
            }
            if (str.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(str, new ObjectParameter[0]);
            }
            return objObjectQuery;
        }

        public static ObjectQuery<T> WhereFieldData<T>(this ObjectQuery<T> objObjectQuery, string fieldSourceName, string fieldName)
        {
            ModulePage currentHandler = (ModulePage) SysVariable.CurrentHandler;
            string str = "";
            if (currentHandler.IsPowerDataCheck)
            {
                if (currentHandler.CheckIsPowerData(currentHandler.ModuleTypeID, currentHandler.PowerPageCode))
                {
                    str = currentHandler.CheckPowerFieldData(currentHandler.ModuleTypeID, currentHandler.PowerPageCode, fieldSourceName, fieldName);
                }
                if (str.IsNoNull())
                {
                    objObjectQuery = objObjectQuery.Where(str, new ObjectParameter[0]);
                }
            }
            return objObjectQuery;
        }

        public static ObjectQuery<T> WhereFieldData<T>(this ObjectQuery<T> objObjectQuery, string pageCode, string fieldSourceName, string fieldName)
        {
            ModulePage currentHandler = (ModulePage) SysVariable.CurrentHandler;
            string str = "";
            if (!currentHandler.IsPowerDataCheck)
            {
            }
            if (currentHandler.CheckIsPowerData(currentHandler.ModuleTypeID, pageCode))
            {
                str = currentHandler.CheckPowerFieldData(currentHandler.ModuleTypeID, pageCode, fieldSourceName, fieldName);
            }
            if (str.IsNoNull())
            {
                objObjectQuery = objObjectQuery.Where(str, new ObjectParameter[0]);
            }
            return objObjectQuery;
        }
    }
}

