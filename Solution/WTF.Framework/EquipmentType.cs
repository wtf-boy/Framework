namespace WTF.Framework
{
    using System;

    public enum EquipmentType
    {
        [Enum("Android")]
        Android = 2,
        [Enum("Ipad")]
        Ipad = 3,
        [Enum("Iphone")]
        Iphone = 4,
        [Enum("PC")]
        PC = 1,
        [Enum("未知类型")]
        Unknown = 0
    }
}

