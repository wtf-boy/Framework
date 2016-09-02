namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;

    public interface ITransformProvider
    {
        bool Match(ConditionItem item, Type type);
        IEnumerable<ConditionItem> Transform(ConditionItem item, Type type);
    }
}

