namespace WTF.Logging
{
    using WTF.Framework;
    using System;

    public enum LogModuleType
    {
        [Enum("程序模块")]
        Application = 0,
        [Enum("论坛模块")]
        BBS = 11,
        [Enum("内容模块")]
        ContentLog = 10,
        [Enum("类库日志")]
        Framework = 2,
        [Enum("日志模块")]
        LogManager = 1,
        [Enum("邮件日志")]
        MailLog = 13,
        [Enum("模块日志")]
        ModuleLog = 3,
        [Enum("页面基类")]
        PageLog = 9,
        [Enum("参数模块")]
        ParameterLog = 7,
        [Enum("资源模块")]
        ResourceLog = 6,
        [Enum("平台日志")]
        SupportLog = 4,
        [Enum("Url模块")]
        UrlRewriter = 8,
        [Enum("用户模块")]
        UserLog = 5,
        [Enum("作业日志")]
        WorkLog = 12
    }
}

