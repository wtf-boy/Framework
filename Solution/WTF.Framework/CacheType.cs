namespace WTF.Framework
{
    using System;

    public enum CacheType
    {
        [Enum("所有缓存")]
        AllCache = 0,
        [Enum("论坛缓存")]
        BBS = 7,
        [Enum("内容缓存")]
        CMS = 6,
        [Enum("框架缓存")]
        Framework = 1,
        [Enum("日志缓存")]
        Logging = 4,
        [Enum("相遇网缓存")]
        Meet = 8,
        [Enum("模块缓存")]
        Module = 3,
        [Enum("参数缓存")]
        Parameter = 2,
        [Enum("网址缓存")]
        UrlRewriter = 5
    }
}

