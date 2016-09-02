namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class DictionaryHelper
    {
        public static Dictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool updateExisted = true)
        {
            foreach (KeyValuePair<TKey, TValue> pair in values)
            {
                if (!(dict.ContainsKey(pair.Key) && !updateExisted))
                {
                    dict[pair.Key] = pair.Value;
                }
            }
            return dict;
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            return (dict.ContainsKey(key) ? dict[key] : defaultValue);
        }

        public static Dictionary<TKey, TValue> MergeDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictOne, IEnumerable<KeyValuePair<TKey, TValue>> dictTwo, bool updateExisted = true)
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> pair in dictOne)
            {
                dictionary[pair.Key] = pair.Value;
            }
            foreach (KeyValuePair<TKey, TValue> pair in dictTwo)
            {
                if (!(dictionary.ContainsKey(pair.Key) && !updateExisted))
                {
                    dictionary[pair.Key] = pair.Value;
                }
            }
            return dictionary;
        }

        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }
            return dict;
        }
    }
}

