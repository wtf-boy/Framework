﻿namespace WTF.Logging
{
    using WTF.Framework;
    using System;

    public enum OperationType
    {
        [Enum("审核")]
        Audit = 6,
        [Enum("取消置顶")]
        CancelTop = 11,
        [Enum("交换记录")]
        ChanageMove = 13,
        [Enum("删除")]
        Delete = 3,
        [Enum("新增")]
        Insert = 1,
        [Enum("移动记录")]
        MoveRow = 12,
        [Enum("其它操作")]
        Other = 0x3e8,
        [Enum("发布")]
        Release = 4,
        [Enum("置顶")]
        Top = 10,
        [Enum("取消审核")]
        UnAudit = 7,
        [Enum("取消发布")]
        UnRelease = 5,
        [Enum("取消推荐")]
        UnVouch = 9,
        [Enum("修改")]
        Update = 2,
        [Enum("推荐")]
        Vouch = 8
    }
}

