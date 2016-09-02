namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    public static class TypeHelper
    {
        public static bool IsNoNull(this Guid value)
        {
            return (value != Guid.Empty);
        }

        public static bool IsNull(this Guid value)
        {
            return (value == Guid.Empty);
        }

        public static string RenderFileSize(this int pSize)
        {
            float num = pSize;
            if (pSize < 0x400)
            {
                return (pSize.ToString() + "字节");
            }
            if ((pSize >= 0x400) && (pSize <= 0x100000))
            {
                num /= 1024f;
                return (string.Format("{0:F2}", num) + "KB");
            }
            if ((pSize >= 0x100000) && (pSize <= 0x40000000))
            {
                num /= 1048576f;
                return (string.Format("{0:F2}", num) + "MB");
            }
            if (pSize >= 0x40000000)
            {
                num /= 1.073742E+09f;
                return (string.Format("{0:F2}", num) + "GB");
            }
            return "";
        }

        public static string RenderFileSize(this long pSize)
        {
            float num = pSize;
            if (pSize < 0x400L)
            {
                return (pSize.ToString() + "字节");
            }
            if ((pSize >= 0x400L) && (pSize <= 0x100000L))
            {
                num /= 1024f;
                return (string.Format("{0:F2}", num) + "KB");
            }
            if ((pSize >= 0x100000L) && (pSize <= 0x40000000L))
            {
                num /= 1048576f;
                return (string.Format("{0:F2}", num) + "MB");
            }
            if (pSize >= 0x40000000L)
            {
                num /= 1.073742E+09f;
                return (string.Format("{0:F2}", num) + "GB");
            }
            return "";
        }

        public static string RenderSecond(this double second)
        {
            if (second > 0.0)
            {
                if (second <= 60.0)
                {
                    return (string.Format("{0:F2}", second) + "秒");
                }
                if (second <= 3600.0)
                {
                    return (string.Format("{0:F2}", second / 60.0) + "分");
                }
                if (second <= 86400.0)
                {
                    return (string.Format("{0:F2}", second / 3600.0) + "小时");
                }
                return (string.Format("{0:F2}", second / 86400.0) + "天");
            }
            second = 0.0 - second;
            if (second <= 60.0)
            {
                return ("-" + string.Format("{0:F2}", second) + "秒");
            }
            if (second <= 3600.0)
            {
                return ("-" + string.Format("{0:F2}", second / 60.0) + "分");
            }
            if (second <= 86400.0)
            {
                return ("-" + string.Format("{0:F2}", second / 3600.0) + "小时");
            }
            return ("-" + string.Format("{0:F2}", second / 86400.0) + "天");
        }

        public static string RenderSecond(this int second)
        {
            if (second > 0)
            {
                if (second <= 60)
                {
                    return (string.Format("{0:F2}", second) + "秒");
                }
                if (second <= 0xe10)
                {
                    return (string.Format("{0:F2}", second / 60) + "分");
                }
                if (second <= 0x15180)
                {
                    return (string.Format("{0:F2}", second / 0xe10) + "小时");
                }
                return (string.Format("{0:F2}", second / 0x15180) + "天");
            }
            second = -second;
            if (second <= 60)
            {
                return ("-" + string.Format("{0:F2}", second) + "秒");
            }
            if (second <= 0xe10)
            {
                return ("-" + string.Format("{0:F2}", second / 60) + "分");
            }
            if (second <= 0x15180)
            {
                return ("-" + string.Format("{0:F2}", second / 0xe10) + "小时");
            }
            return ("-" + string.Format("{0:F2}", second / 0x15180) + "天");
        }
    }
}

