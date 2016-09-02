namespace WTF.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class ListHelper
    {
        public static bool Contains<T>(this IEnumerable array, T obj)
        {
            foreach (T local in array)
            {
                if (obj.Equals(local))
                {
                    return true;
                }
            }
            return false;
        }

        public static ArrayList FilterRepeatArrayItem(this ArrayList arr)
        {
            ArrayList list = new ArrayList();
            if (arr.Count > 0)
            {
                list.Add(arr[0]);
            }
            for (int i = 0; i < arr.Count; i++)
            {
                if (!list.Contains(arr[i]))
                {
                    list.Add(arr[i]);
                }
            }
            return list;
        }

        public static bool IsArrayListNull(this ArrayList canShu, int biaoZhi)
        {
            try
            {
                if ((canShu == null) || (0 == canShu.Count))
                {
                    return true;
                }
                if (2 != biaoZhi)
                {
                    if (0 == biaoZhi)
                    {
                        foreach (object obj2 in canShu)
                        {
                            if (null == obj2)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    foreach (object obj2 in canShu)
                    {
                        if (obj2.IsNull())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static bool IsIListNull(this IList canShu, int biaoZhi)
        {
            try
            {
                if ((canShu == null) || (0 == canShu.Count))
                {
                    return true;
                }
                if (2 != biaoZhi)
                {
                    if (0 == biaoZhi)
                    {
                        foreach (object obj2 in (ArrayList)canShu)
                        {
                            if (null == obj2)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    foreach (object obj2 in (ArrayList)canShu)
                    {
                        if (obj2.IsNull())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static bool IsNoNull<T>(this IEnumerable<T> list)
        {
            return !list.IsNull<T>();
        }

        public static bool IsNoNull<T>(this IList<T> list)
        {
            return !list.IsNull<T>();
        }

        public static bool IsNull<T>(this IEnumerable<T> list)
        {
            return ((list == null) || (list.Count<T>() == 0));
        }

        public static bool IsNull<T>(this IList<T> list)
        {
            return ((list == null) || (list.Count == 0));
        }

        public static bool IsNullOrEmpty<T>(this List<T> array)
        {
            if (array != null)
            {
                return (array.Count == 0);
            }
            return true;
        }

        public static bool IsTheRightType(IList list, string typeName)
        {
            if ((list == null) || (list.Count == 0))
            {
                return false;
            }
            foreach (object obj2 in list)
            {
                if (obj2.GetType() != Type.GetType(typeName))
                {
                    return false;
                }
            }
            return true;
        }

        public static DataTable ToDataTable(IEnumerable list)
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type elementType = list.AsQueryable().ElementType;
            DataTable dt = new DataTable();
            Array.ForEach<PropertyInfo>(elementType.GetProperties(), delegate(PropertyInfo p)
            {
                pList.Add(p);
                dt.Columns.Add(p.Name, p.PropertyType);
            });
            IEnumerator enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object item = enumerator.Current;
                DataRow row = dt.NewRow();
                pList.ForEach(delegate(PropertyInfo p)
                {
                    row[p.Name] = p.GetValue(item, null);
                });
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static DataTable ToDataTable<TResult>(IEnumerable<TResult> value) where TResult : class
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type type = typeof(TResult);
            DataTable dt = new DataTable();
            Array.ForEach<PropertyInfo>(type.GetProperties(), delegate(PropertyInfo p)
            {
                pList.Add(p);
                dt.Columns.Add(p.Name, p.PropertyType);
            });
            using (IEnumerator<TResult> enumerator = value.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    TResult item = enumerator.Current;
                    DataRow row = dt.NewRow();
                    pList.ForEach(delegate(PropertyInfo p)
                    {
                        row[p.Name] = p.GetValue(item, null);
                    });
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static List<TResult> ToList<TResult>(this DataTable dt) where TResult : class, new()
        {
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            Array.ForEach<PropertyInfo>(typeof(TResult).GetProperties(), delegate(PropertyInfo p)
            {
                if (dt.Columns.IndexOf(p.Name) != -1)
                {
                    prlist.Add(p);
                }
            });
            List<TResult> list = new List<TResult>();
            IEnumerator enumerator = dt.Rows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                DataRow row = (DataRow)enumerator.Current;
                TResult ob = Activator.CreateInstance<TResult>();
                prlist.ForEach(delegate(PropertyInfo p)
                {
                    if (row[p.Name] != DBNull.Value)
                    {
                        p.SetValue(ob, row[p.Name], null);
                    }
                });
                list.Add(ob);
            }

            return list;
        }
    }
}

