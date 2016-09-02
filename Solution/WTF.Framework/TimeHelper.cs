namespace WTF.Framework
{
    using System;
    using System.Runtime.CompilerServices;

    public static class TimeHelper
    {
        public static DateTime ConvertSecondStampToDateTime(this int SecondStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
            TimeSpan span = new TimeSpan(SecondStamp * 0x989680L);
            return (time + span);
        }

        public static DateTime ConvertStampToDateTime(this long Stamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
            TimeSpan span = new TimeSpan(Stamp * 0x2710L);
            return (time + span);
        }

        public static int ConvertToSecondStamp(this DateTime date)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
            TimeSpan span = (TimeSpan) (date - time);
            return Convert.ToInt32((long) (span.Ticks / 0x989680L));
        }

        public static int ConvertToSecondStamp(this string datetime)
        {
            return DateTime.Parse(datetime).ConvertToSecondStamp();
        }

        public static long ConvertToStamp(this DateTime date)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
            TimeSpan span = (TimeSpan) (date - time);
            return Convert.ToInt64((long) (span.Ticks / 0x2710L));
        }

        public static long ConvertToTimeStamp(this string datetime)
        {
            return DateTime.Parse(datetime).ConvertToStamp();
        }

        public static string RegularDateTime
        {
            get
            {
                string str = string.Empty;
                int hour = DateTime.Now.Hour;
                int minute = DateTime.Now.Minute;
                int second = DateTime.Now.Second;
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;
                string str2 = DateTime.Now.TimeOfDay.ToString();
                string str3 = DateTime.Now.Date.ToString();
                str = (year < 10) ? ("0" + year.ToString()) : year.ToString();
                return (((((str + "-" + ((month < 10) ? ("0" + month.ToString()) : month.ToString())) + "-" + ((day < 10) ? ("0" + day.ToString()) : day.ToString())) + " " + ((hour < 10) ? ("0" + hour.ToString()) : hour.ToString())) + ":" + ((minute < 10) ? ("0" + minute.ToString()) : minute.ToString())) + ":" + ((second < 10) ? ("0" + second.ToString()) : second.ToString()));
            }
        }

        public static long Stamp
        {
            get
            {
                DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
                TimeSpan span = (TimeSpan) (DateTime.Now - time);
                return Convert.ToInt64((long) (span.Ticks / 0x2710L));
            }
        }

        public static int StampSecond
        {
            get
            {
                DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
                TimeSpan span = (TimeSpan) (DateTime.Now - time);
                return Convert.ToInt32((long) (span.Ticks / 0x989680L));
            }
        }

        public static string TimeStamp
        {
            get
            {
                DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
                TimeSpan span = (TimeSpan) (DateTime.Now - time);
                return Convert.ToString((long) (span.Ticks / 0x2710L));
            }
        }

        public static string TimeStampSecond
        {
            get
            {
                DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
                TimeSpan span = (TimeSpan) (DateTime.Now - time);
                return Convert.ToString((long) (span.Ticks / 0x989680L));
            }
        }
    }
}

