﻿namespace WTF.Logging
{
    using WTF.Framework;
    using System;

    public enum LogCategory
    {
        [Enum("参数输入错误")]
        ArgumentInputError = 1,
        [Enum("攻击记录")]
        AttackInfo = 7,
        [Enum("ESearch语句")]
        ESearchInfo = 8,
        [Enum("异常错误")]
        ExceptionError = 2,
        [Enum("记录信息")]
        RecordInfo = 4,
        [Enum("Solr语句")]
        SolrInfo = 6,
        [Enum("Sql信息")]
        SqlInfo = 5,
        [Enum("跟踪信息")]
        TrackInfo = 3
    }
}

