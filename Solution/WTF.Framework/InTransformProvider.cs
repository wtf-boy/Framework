namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;

    public class InTransformProvider : ITransformProvider
    {
        public bool Match(ConditionItem item, Type type)
        {
            return (item.Method == QueryMethod.In);
        }

        public IEnumerable<ConditionItem> Transform(ConditionItem item, Type type)
        {
            Array val = item.Value as Array;
            if (val == null)
            {
                string str = item.Value.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    val = str.Split(new char[] { ',' });
                }
            }
            return new ConditionItem[] { new ConditionItem(item.Field, QueryMethod.StdIn, val) };
        }
    }
}

