namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web.UI.WebControls;

    public static class DateTimeHelper
    {
        public static string AgeToBirthday(this string age)
        {
            int num = Convert.ToInt32(DateTime.Now.Year.ToString()) - Convert.ToInt32(age);
            return (num.ToString() + "0101");
        }

        public static void BindAmFm(DropDownList list)
        {
            list.Items.Clear();
            list.Items.Add("AM(上午)");
            list.Items.Add("FM(下午)");
        }

        public static void BindDate(DropDownList lc1, DropDownList lc2, DropDownList lc3)
        {
            lc1.BindYear(0x7c6, 0x24);
            BindMonth(lc2);
            BindDays(lc3, Convert.ToInt16(lc1.SelectedItem.Value), Convert.ToInt16(lc2.SelectedItem.Value));
        }

        public static void BindDateListControl(ListControl drpYear, ListControl drpMonth, ListControl drpDay, DateTime tmpTime)
        {
            if (!tmpTime.IsNullDate())
            {
                int year;
                foreach (ListItem item in drpYear.Items)
                {
                    item.Selected = false;
                    year = tmpTime.Year;
                    if (item.Value.ToString() == year.ToString())
                    {
                        item.Selected = true;
                    }
                }
                foreach (ListItem item2 in drpMonth.Items)
                {
                    item2.Selected = false;
                    year = tmpTime.Month;
                    if (item2.Value.ToString() == year.ToString())
                    {
                        item2.Selected = true;
                    }
                }
                BindDays((DropDownList) drpDay, tmpTime.Year, tmpTime.Month);
                foreach (ListItem item3 in drpDay.Items)
                {
                    item3.Selected = false;
                    year = tmpTime.Day;
                    if (item3.Value.ToString() == year.ToString())
                    {
                        item3.Selected = true;
                    }
                }
            }
        }

        public static void BindDays(DropDownList list, int year, int month)
        {
            list.Items.Clear();
            int num = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= num; i++)
            {
                list.Items.Add(i.ToString());
            }
        }

        public static void BindMonth(DropDownList list)
        {
            list.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                list.Items.Add(i.ToString());
            }
        }

        public static void BindYear(this DropDownList list, int startYear, int count)
        {
            list.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                list.Items.Add(new ListItem(startYear.ToString(), startYear.ToString()));
                startYear++;
            }
        }

        public static object ConvertNullDbTime(this string dataTime)
        {
            if (dataTime.IsNull())
            {
                return DBNull.Value;
            }
            return dataTime;
        }

        public static List<int> DateDiff(this DateTime DateTime1, DateTime DateTime2)
        {
            List<int> list2;
            try
            {
                List<int> list = new List<int>();
                TimeSpan span = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts = new TimeSpan(DateTime2.Ticks);
                TimeSpan span3 = span.Subtract(ts).Duration();
                list.Add(span3.Days);
                list.Add(span3.Hours);
                list.Add(span3.Seconds);
                list.Add(span3.Milliseconds);
                list2 = list;
            }
            catch (Exception exception)
            {
                throw new ArgumentException("公共层DateTimeHelper类DateDiff方法参数错误", exception);
            }
            return list2;
        }

        public static string[] DateInCapitalCN(this string numbericDate)
        {
            string[] strArray = new string[] { "", "", "" };
            string str = numbericDate;
            for (int i = 0; i <= 3; i++)
            {
                strArray[0] = strArray[0] + str.Substring(i, 1).DigitToCapitalCN("");
            }
            string str2 = str.Substring(4, 2);
            if (str2 != null)
            {
                if (!(str2 == "01"))
                {
                    if (str2 == "02")
                    {
                        strArray[1] = "零贰";
                        goto Label_00FC;
                    }
                    if (str2 == "10")
                    {
                        strArray[1] = "零壹拾";
                        goto Label_00FC;
                    }
                    if (str2 == "11")
                    {
                        strArray[1] = "壹拾壹";
                        goto Label_00FC;
                    }
                    if (str2 == "12")
                    {
                        strArray[1] = "壹拾贰";
                        goto Label_00FC;
                    }
                }
                else
                {
                    strArray[1] = "零壹";
                    goto Label_00FC;
                }
            }
            strArray[1] = str.Substring(5, 1).DigitToCapitalCN("");
        Label_00FC:
            str2 = str.Substring(6, 2);
            if ((str2 != null) && (((str2 == "10") || (str2 == "20")) || (str2 == "30")))
            {
                strArray[2] = "零" + str.Substring(6, 1).DigitToCapitalCN("拾");
                return strArray;
            }
            strArray[2] = str.Substring(6, 1).DigitToCapitalCN("拾") + str.Substring(7, 1).DigitToCapitalCN("");
            return strArray;
        }

        public static string DigitToCapitalCN(this int digit, string cur)
        {
            switch (digit)
            {
                case 0:
                    return "零";

                case 1:
                    return ("壹" + cur);

                case 2:
                    return ("贰" + cur);

                case 3:
                    return ("叁" + cur);

                case 4:
                    return ("肆" + cur);

                case 5:
                    return ("伍" + cur);

                case 6:
                    return ("陆" + cur);

                case 7:
                    return ("柒" + cur);

                case 8:
                    return ("捌" + cur);

                case 9:
                    return ("玖" + cur);
            }
            return "";
        }

        public static string DigitToCapitalCN(this string digit, string cur)
        {
            return Convert.ToInt32(digit).DigitToCapitalCN(cur);
        }

        public static string DisplayDateTimeCountByMillisecond(this DateTime objDateTime, bool isSingleUnit = false)
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - objDateTime);
            long pMillisecond = Convert.ToInt64(span.TotalMilliseconds);
            if (pMillisecond == 0L)
            {
                return "刚刚";
            }
            return pMillisecond.DisplayDateTimeCountByMillisecond(isSingleUnit);
        }

        public static string DisplayDateTimeCountByMillisecond(this long pMillisecond, bool isSingleUnit = false)
        {
            StringBuilder builder = new StringBuilder();
            pMillisecond = int.Parse(string.Format("{0:F0}", pMillisecond / 0x3e8L));
            if (pMillisecond >= 0x15180L)
            {
                builder.Append(string.Format("{0:F0}", pMillisecond / 0x15180L));
                builder.Append("天");
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
                pMillisecond = pMillisecond % 0x15180L;
            }
            if ((pMillisecond > 0xe10L) || (pMillisecond == 0L))
            {
                builder.Append(string.Format("{0:F0}", pMillisecond / 0xe10L));
                builder.Append("小时");
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
                pMillisecond = pMillisecond % 0xe10L;
            }
            if ((pMillisecond > 60L) || (pMillisecond == 0L))
            {
                builder.Append(string.Format("{0:F0}", pMillisecond / 60L));
                builder.Append("分");
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
                pMillisecond = pMillisecond % 60L;
            }
            if (pMillisecond < 60L)
            {
                builder.Append(pMillisecond.ToString());
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
                builder.Append("秒");
            }
            return builder.ToString();
        }

        public static string DisplayDateTimeCountBySecond(this DateTime objDateTime, bool isSingleUnit = false)
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - objDateTime);
            long pSecond = Convert.ToInt64(span.TotalSeconds);
            if (pSecond == 0L)
            {
                return "刚刚";
            }
            return pSecond.DisplayDateTimeCountBySecond(isSingleUnit);
        }

        public static string DisplayDateTimeCountBySecond(this long pSecond, bool isSingleUnit = false)
        {
            StringBuilder builder = new StringBuilder();
            if (pSecond >= 0x15180L)
            {
                builder.Append(string.Format("{0:F0}", pSecond / 0x15180L));
                builder.Append("天");
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
                pSecond = pSecond % 0x15180L;
            }
            if ((pSecond > 0xe10L) || (pSecond == 0L))
            {
                builder.Append(string.Format("{0:F0}", pSecond / 0xe10L));
                builder.Append("小时");
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
                pSecond = pSecond % 0xe10L;
            }
            if ((pSecond > 60L) || (pSecond == 0L))
            {
                builder.Append(string.Format("{0:F0}", pSecond / 60L));
                builder.Append("分");
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
                pSecond = pSecond % 60L;
            }
            if (pSecond < 60L)
            {
                builder.Append(pSecond.ToString());
                builder.Append("秒");
                if (isSingleUnit)
                {
                    return builder.ToString();
                }
            }
            return builder.ToString();
        }

        public static bool FormatDate(ref string dateTime)
        {
            dateTime = dateTime.Trim();
            try
            {
                Convert.ToDateTime((string) dateTime);
            }
            catch
            {
                return false;
            }
            if (dateTime.Length > 10)
            {
                dateTime = dateTime.Substring(0, 10);
            }
            if (dateTime.Length != 10)
            {
                string str = dateTime;
                string str2 = str.Substring(0, str.IndexOf("-"));
                str = str.Substring(str.IndexOf("-") + 1, (str.Length - str.IndexOf("-")) - 1);
                string str3 = str.Substring(0, str.IndexOf("-"));
                string str4 = str.Substring(str.IndexOf("-") + 1, (str.Length - str.IndexOf("-")) - 1);
                dateTime = str2.PadLeft(4, '0') + "-" + str3.PadLeft(2, '0') + "-" + str4.PadLeft(2, '0');
            }
            return true;
        }

        public static DateTime GetApplicationTime(DropDownList lc1, DropDownList lc2, DropDownList lc3)
        {
            return new DateTime(int.Parse(lc1.SelectedItem.Value), int.Parse(lc2.SelectedItem.Value), int.Parse(lc3.SelectedItem.Value));
        }

        public static string GetChineseDayOfWeek()
        {
            return DateTime.Now.GetChineseDayOfWeek();
        }

        public static string GetChineseDayOfWeek(this DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "星期日";

                case DayOfWeek.Monday:
                    return "星期一";

                case DayOfWeek.Tuesday:
                    return "星期二";

                case DayOfWeek.Wednesday:
                    return "星期三";

                case DayOfWeek.Thursday:
                    return "星期四";

                case DayOfWeek.Friday:
                    return "星期五";

                case DayOfWeek.Saturday:
                    return "星期六";
            }
            return "星期几";
        }

        public static string GetChineseDayOfWeek(this string dateValue, string format = "yyyyMMdd", bool isOnlyWeekend = false)
        {
            DateTime dateTime = DateTime.ParseExact(dateValue, format, CultureInfo.CurrentCulture);
            if (isOnlyWeekend)
            {
                if (dateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    return "星期日";
                }
                if (dateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    return "星期六";
                }
                return "";
            }
            return dateTime.GetChineseDayOfWeek();
        }

        public static string GetDatePath(this DateTime date)
        {
            string str = "";
            string str2 = "";
            str = str + DateTime.Now.Year + "/";
            str2 = "00" + DateTime.Now.Month;
            str = str + str2.Substring(str2.Length - 2, 2) + "/";
            str2 = "00" + DateTime.Now.Day;
            return (str + str2.Substring(str2.Length - 2, 2));
        }

        public static string GetDateString(this string splitString)
        {
            DateTime now = DateTime.Now;
            StringBuilder builder = new StringBuilder();
            builder.Append(now.Year.ToString("0000"));
            builder.Append(splitString);
            builder.Append(now.Month.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Day.ToString("00"));
            return builder.ToString();
        }

        public static List<string> GetDays(this DateTime time1, DateTime time2)
        {
            ArgumentException exception;
            List<string> list = new List<string>();
            try
            {
                TimeSpan span = (TimeSpan) (time2 - time1);
                for (int i = 0; i <= span.Days; i++)
                {
                    try
                    {
                        string dateTime = time1.AddDays((double) i).ToShortDateString();
                        if (FormatDate(ref dateTime))
                        {
                            list.Add(dateTime);
                        }
                    }
                    catch (ArgumentException exception1)
                    {
                        exception = exception1;
                        throw new ArgumentException("公共层TimeParser类GetDays方法参数错误", exception);
                    }
                }
            }
            catch (ArgumentException exception3)
            {
                exception = exception3;
                throw new ArgumentException("公共层TimeParser类GetDays方法参数错误", exception);
            }
            catch (ArithmeticException exception2)
            {
                throw new ArithmeticException("公共层TimeParser类GetDays方法类型转换错误", exception2);
            }
            return list;
        }

        public static int GetDiffDays(this DateTime DateTime1, DateTime DateTime2)
        {
            int num2;
            try
            {
                TimeSpan span = (TimeSpan) (DateTime1.Date - DateTime2.Date);
                num2 = Convert.ToInt32(span.TotalDays);
            }
            catch (Exception exception)
            {
                throw new ArgumentException("公共层DateTimeHelper类GetDiffDays方法参数错误", exception);
            }
            return num2;
        }

        public static int GetMonthDays(this string year_month)
        {
            string year = year_month.Substring(0, 4);
            string month = year_month.Substring(5, 2);
            return year.GetMonthDays(month);
        }

        public static int GetMonthDays(this string year, string month)
        {
            switch (month)
            {
                case "1":
                case "3":
                case "5":
                case "7":
                case "8":
                case "01":
                case "03":
                case "05":
                case "07":
                case "08":
                case "10":
                case "12":
                    return 0x1f;

                case "4":
                case "6":
                case "9":
                case "04":
                case "06":
                case "09":
                case "11":
                    return 30;

                case "2":
                case "02":
                    if ((((Convert.ToInt32(year) % 100) == 0) || ((Convert.ToInt32(year) % 4) != 0)) && (((Convert.ToInt32(year) % 100) != 0) || ((Convert.ToInt32(year) % 4) != 0)))
                    {
                        return 0x1c;
                    }
                    return 0x1d;
            }
            return 0;
        }

        public static string GetMonthQuarter()
        {
            return DateTime.Now.GetMonthQuarter();
        }

        public static string GetMonthQuarter(this DateTime dateTime)
        {
            return dateTime.Month.GetMonthQuarter();
        }

        public static string GetMonthQuarter(this int month)
        {
            switch (month)
            {
                case 1:
                case 2:
                case 3:
                    return "第一季度";

                case 4:
                case 5:
                case 6:
                    return "第二季度";

                case 7:
                case 8:
                case 9:
                    return "第三季度";

                case 10:
                case 11:
                case 12:
                    return "第四季度";
            }
            return "";
        }

        public static string GetMonthQuarter(this string month)
        {
            return Convert.ToInt32(month).GetMonthQuarter();
        }

        public static string GetSolarTerm(this DateTime day)
        {
            string[] strArray = new string[] { 
                "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", 
                "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"
             };
            int[] numArray = new int[] { 
                0, 0x52d8, 0xa5e3, 0xf95c, 0x14d59, 0x1a206, 0x1f763, 0x24d89, 0x2a45d, 0x2fbdf, 0x353d8, 0x3ac35, 0x404af, 0x45d25, 0x4b553, 0x50d19, 
                0x56446, 0x5bac6, 0x61087, 0x6658a, 0x6b9db, 0x70d90, 0x760cc, 0x7b3b6
             };
            double num = 31556925974.7;
            DateTime time2 = DateTime.Parse("1900-01-06 02:05:00").AddMilliseconds(num * (day.Year - 0x76c));
            TimeSpan span = (TimeSpan) (day - time2);
            double totalMinutes = span.TotalMinutes;
            double num3 = totalMinutes + 1440.0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((totalMinutes <= numArray[i]) && (num3 > numArray[i]))
                {
                    return strArray[i];
                }
            }
            return "";
        }

        public static string GetSolarTerm(int year, int month, int day)
        {
            return DateTime.Parse(string.Concat(new object[] { year, "-", month, "-", day, " 00:00:00" })).GetSolarTerm();
        }

        public static string GetTimeString(this string splitString)
        {
            DateTime now = DateTime.Now;
            StringBuilder builder = new StringBuilder();
            builder.Append(now.Year.ToString("0000"));
            builder.Append(splitString);
            builder.Append(now.Month.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Day.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Hour.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Minute.ToString("00"));
            builder.Append(splitString);
            builder.Append(now.Second.ToString("00"));
            return builder.ToString();
        }

        public static int GetWeekAmount(this int year)
        {
            DateTime time = new DateTime(year, 12, 0x1f);
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static void GetWeekBetween(this DateTime dt, out DateTime startDate, out DateTime endDate)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    startDate = dt;
                    endDate = dt.AddDays(6.0);
                    return;

                case DayOfWeek.Tuesday:
                    startDate = dt.AddDays(-1.0);
                    endDate = dt.AddDays(5.0);
                    return;

                case DayOfWeek.Wednesday:
                    startDate = dt.AddDays(-2.0);
                    endDate = dt.AddDays(4.0);
                    return;

                case DayOfWeek.Thursday:
                    startDate = dt.AddDays(-3.0);
                    endDate = dt.AddDays(3.0);
                    return;

                case DayOfWeek.Friday:
                    startDate = dt.AddDays(-4.0);
                    endDate = dt.AddDays(2.0);
                    return;

                case DayOfWeek.Saturday:
                    startDate = dt.AddDays(-5.0);
                    endDate = dt.AddDays(1.0);
                    return;
            }
            startDate = dt.AddDays(-6.0);
            endDate = dt;
        }

        public static void GetWeekBetween(this string yearWeek, out DateTime startDate, out DateTime endDate)
        {
            if (!yearWeek.IsNumber(6, true))
            {
                throw new Exception("无效的年周，正确格式为:200801");
            }
            DateTime time = Convert.ToDateTime(yearWeek.Substring(0, 4) + "-01-01");
            int num = int.Parse(yearWeek.Substring(4)) - 1;
            time.AddDays((double) (num * 7)).GetWeekBetween(out startDate, out endDate);
        }

        public static int GetWeekOfYear(this DateTime date)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static int GetWeekOfYear(this string date)
        {
            return Convert.ToDateTime(date).GetWeekOfYear();
        }

        public static List<string> GetYears(this string beginYear, string endYear)
        {
            List<string> list = new List<string>();
            try
            {
                int num = Convert.ToInt32(endYear) - Convert.ToInt32(beginYear);
                for (int i = 0; i <= num; i++)
                {
                    list.Add((Convert.ToInt32(beginYear) + i).ToString());
                }
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException("公共层TimeParser方法GetYears参数错误", exception);
            }
            catch (ArithmeticException exception2)
            {
                throw new ArithmeticException("公共层TimeParser方法GetYears类型转换错误", exception2);
            }
            return list;
        }

        public static bool IsDate(string year, string month, string day)
        {
            if (!((year.IsYear() && month.IsMonth()) && day.IsDay()))
            {
                return false;
            }
            try
            {
                Convert.ToDateTime(year + "-" + month + "-" + day);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsDay(this string day)
        {
            if (string.IsNullOrWhiteSpace(day))
            {
                return false;
            }
            return day.IsMatch(@"^(0?[1-9]|(1|2)\d|3[0-1])$");
        }

        public static bool IsMonth(this string month)
        {
            if (string.IsNullOrWhiteSpace(month))
            {
                return false;
            }
            return month.IsMatch("^(0?[1-9]|1[0-2])$");
        }

        public static bool IsNullDate(this DateTime date)
        {
            DateTime time = new DateTime(0x76c, 1, 1);
            TimeSpan span = date.Subtract(time);
            TimeSpan span2 = date.Subtract(DateTime.MinValue);
            return ((span.TotalDays == 0.0) || (span2.TotalDays == 0.0));
        }

        public static string IsSpanYear(this string checkDate, int checkDays)
        {
            string str = checkDate.Substring(0, 4) + "-" + checkDate.Substring(4, 2) + "-" + checkDate.Substring(6, 2);
            string str2 = checkDate.Substring(0, 4) + "-01-01";
            if (Convert.ToDateTime(str).Subtract(Convert.ToDateTime(str2)).Days < checkDays)
            {
                return Convert.ToInt32((int) (Convert.ToInt32(checkDate.Substring(0, 4)) - 1)).ToString();
            }
            return string.Empty;
        }

        public static bool IsYear(this string year)
        {
            if (string.IsNullOrWhiteSpace(year))
            {
                return false;
            }
            return year.IsMatch(@"^\d{1,4}$");
        }

        public static bool IsYearMonth(this string day)
        {
            if (string.IsNullOrWhiteSpace(day))
            {
                return false;
            }
            return day.IsMatch(@"^\d{4}((0[1-9]{1})|(1[0-2]{1}))$");
        }

        public static string MillisecondToString(this long ms, bool bIncludeMs)
        {
            long num4 = ms % 0x3e8L;
            ms /= 0x3e8L;
            long num = ms / 0xe10L;
            ms = ms % 0xe10L;
            long num2 = ms / 60L;
            ms = ms % 60L;
            long num3 = ms;
            string str = ((num > 9L) ? num.ToString() : ("0" + num.ToString())) + ":" + ((num2 > 9L) ? num2.ToString() : ("0" + num2.ToString())) + ":" + ((num3 > 9L) ? num3.ToString() : ("0" + num3.ToString()));
            if (bIncludeMs)
            {
                str = str + "." + ((num4 > 9L) ? ((num4 > 0x63L) ? num4.ToString() : ("0" + num4.ToString())) : ("00" + num4.ToString()));
            }
            return str;
        }
    }
}

