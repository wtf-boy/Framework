namespace WTF.Framework
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ThreadDirectory<Tkey, TValue> : Hashtable
    {
        public void Add(Tkey key, TValue value)
        {
            base.Add(key, value);
        }

        public bool ContainsKey(Tkey key)
        {
            return base.ContainsKey(key);
        }

        public TValue GetValue(Tkey key)
        {
            return this[key];
        }

        public void Remove(Tkey key)
        {
            base.Remove(key);
        }

        public static ThreadDirectory<Tkey, TValue> Synchronized(ThreadDirectory<Tkey, TValue> table)
        {
            return (ThreadDirectory<Tkey, TValue>) Hashtable.Synchronized(table);
        }

        public TValue this[Tkey key]
        {
            get
            {
                return (TValue) base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}

