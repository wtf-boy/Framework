namespace WTF.Framework
{
    using System;

    [Flags]
    public enum FilterFlag
    {
        MultiLine = 1,
        NoMarkup = 2,
        NoScripting = 4,
        NoSQL = 8
    }
}

