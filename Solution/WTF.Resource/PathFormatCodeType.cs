namespace WTF.Resource
{
    using WTF.Framework;
    using System;

    public enum PathFormatCodeType
    {
        [Enum("按创建日期组织")]
        ByDate = 0,
        [Enum("按创建日期加小时组织")]
        ByDateAndHour = 1,
        [Enum("按唯一标识分组组织")]
        ByID = 2,
        [Enum("不分组")]
        ByNone = 3
    }
}

