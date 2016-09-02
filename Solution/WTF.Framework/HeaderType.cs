namespace WTF.Framework
{
    using System;

    public enum HeaderType
    {
        [Enum("--全部--")]
        All = 2,
        None = 0,
        [Enum("无")]
        NoneItem = 3,
        [Enum("--请选择--")]
        Select = 1
    }
}

