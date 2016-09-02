namespace WTF.Logging
{
    using WTF.Framework;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class SysAssert
    {
        public static void ArgumentAssert(string errorMessage, string logCategoryTypeCode = "Application")
        {
            throw new ArgumentInputNullException(errorMessage, logCategoryTypeCode);
        }

        public static void ArgumentAssert<T>(string errorMessage, T objLogCategoryType)
        {
            throw new ArgumentInputNullException(errorMessage, objLogCategoryType.ToString());
        }

        public static void CheckCondition(bool condition, string errorMessage, LogModuleType objLogModuleType)
        {
            if (condition)
            {
                ArgumentAssert<LogModuleType>(errorMessage, objLogModuleType);
            }
        }

        public static void CheckCondition<T>(bool condition, string errorMessage, T objLogModuleType)
        {
            if (condition)
            {
                ArgumentAssert<T>(errorMessage, objLogModuleType);
            }
        }

        public static void CheckIsNull(this Guid value, string errorMessage, LogModuleType objLogCategoryType)
        {
            CheckCondition(value.IsNull(), errorMessage, objLogCategoryType);
        }

        public static void CheckIsNull<T>(this Guid value, string errorMessage, T objLogCategoryTyp)
        {
            CheckCondition<T>(value.IsNull(), errorMessage, objLogCategoryTyp);
        }

        public static void CheckIsNull(this object value, string errorMessage, LogModuleType objLogModuleType)
        {
            CheckCondition(value.IsNull(), errorMessage, objLogModuleType);
        }

        public static void CheckIsNull<T>(this object value, string errorMessage, T objLogModuleType)
        {
            CheckCondition<T>(value.IsNull(), errorMessage, objLogModuleType);
        }

        public static void CheckIsNull(this string value, string errorMessage, LogModuleType objLogCategoryType)
        {
            CheckCondition(value.IsNull(), errorMessage, objLogCategoryType);
        }

        public static void CheckIsNull<T>(this string value, string errorMessage, T objLogCategoryTyp)
        {
            CheckCondition<T>(value.IsNull(), errorMessage, objLogCategoryTyp);
        }

        public static void Http301Assert(string redirectUrl)
        {
            Http301Assert(true, redirectUrl);
        }

        public static void Http301Assert(bool condition, string redirectUrl)
        {
            if (condition)
            {
                throw new Http301Exception(redirectUrl);
            }
        }

        public static void Http302Assert(string redirectUrl, string message = "临时移动,页面进行跳转")
        {
            Http302Assert(true, redirectUrl, message);
        }

        public static void Http302Assert(bool condition, string redirectUrl, string message = "临时移动,页面进行跳转")
        {
            if (condition)
            {
                throw new Http302Exception(redirectUrl, message);
            }
        }

        public static void Http404Assert(string message = "对不起，此页面不存在")
        {
            Http404Assert(true, message);
        }

        public static void Http404Assert(bool condition, string message = "对不起，此页面不存在")
        {
            if (condition)
            {
                throw new Http404Exception(message);
            }
        }

        public static void Http500Assert(string message = "")
        {
            Http500Assert(true, message);
        }

        public static void Http500Assert(bool condition, string message = "")
        {
            if (condition)
            {
                throw new Http500Exception(message);
            }
        }

        public static void InfoHintAssert(string hintMessage)
        {
            throw new InfoHintException(hintMessage);
        }

        public static void InfoHintAssert(bool condition, string hintMessage)
        {
            if (condition)
            {
                throw new InfoHintException(hintMessage);
            }
        }
    }
}

