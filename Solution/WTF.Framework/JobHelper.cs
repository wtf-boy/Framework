namespace WTF.Framework
{
    using System;
    using System.Xml;
    using System.Linq;

    public static class JobHelper
    {
        public static DateTime CreateNextPlanDate(string config, DateTime lastRunDate)
        {
            int num3;
            DateTime time5;
            DateTime time6;
            XmlDocument document = new XmlDocument();
            document.LoadXml(config);
            DateTime maxValue = DateTime.MaxValue;
            XmlNode node = document.SelectSingleNode("Plan");
            if (node.Attributes["Type"].Value == "1")
            {
                return DateTime.Parse(node.SelectSingleNode("Execute").InnerText);
            }
            DateTime time2 = (lastRunDate.ToString("yyyy-MM-dd") == DateTime.MaxValue.ToString("yyyy-MM-dd")) ? DateTime.Parse(node.Attributes["StartDate"].Value) : DateTime.Parse(lastRunDate.ToString("yyyy-MM-dd"));
            DateTime time3 = DateTime.Parse(node.Attributes["EndDate"].Value);
            DateTime time4 = (time3.ToString("yyyy-MM-dd") == DateTime.MaxValue.ToString("yyyy-MM-dd")) ? time3 : DateTime.Parse(time3.ToString("yyyy-MM-dd") + " 23:59:59");
            XmlNode node2 = node.SelectSingleNode("Frequency");
            XmlNode node3 = node.SelectSingleNode("DayFrequency");
            string str2 = node3.Attributes["Type"].Value;
            string str3 = node2.Attributes["Type"].Value;
            string startTime = "";
            string endTime = "";
            string str6 = "";
            int num = 0;
            string innerText = "";
            if (str2 == "2")
            {
                XmlNode node4 = node3.SelectSingleNode("Interval");
                startTime = node4.Attributes["StartTime"].Value;
                endTime = node4.Attributes["EndTime"].Value;
                str6 = node4.Attributes["Type"].Value;
                num = int.Parse(node4.Attributes["Number"].Value);
            }
            else
            {
                innerText = node3.SelectSingleNode("ExecuteTime").InnerText;
            }
            int num2 = 0;
            switch (str3)
            {
                case "1":
                    num3 = int.Parse(node2.SelectSingleNode("Day").Attributes["Interval"].Value);
                    if (str2 == "1")
                    {
                        for (maxValue = DateTime.Parse(time2.ToString("yyyy-MM-dd") + " " + innerText); maxValue < DateTime.Now; maxValue = maxValue.AddDays((double)num3))
                        {
                        }
                    }
                    else
                    {
                        maxValue = DateTime.Parse(time2.ToString("yyyy-MM-dd") + " " + startTime);
                        while (maxValue < DateTime.Now)
                        {
                            maxValue = maxValue.AddDays((double)(num2 * num3));
                            time5 = GetDateTimeBetwwenTime(maxValue, (str6 == "1") ? num : (num * 60), startTime, endTime);
                            if (time5 != DateTime.MinValue)
                            {
                                maxValue = time5;
                                break;
                            }
                            num2 = 1;
                        }
                    }
                    break;

                case "2":
                    {
                        XmlNode node5 = node2.SelectSingleNode("Week");
                        num3 = int.Parse(node5.Attributes["Interval"].Value);
                        if (str2 == "1")
                        {
                            time6 = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                            maxValue = DateTime.Parse(time2.ToString("yyyy-MM-dd") + " " + innerText);
                            maxValue = maxValue.AddDays(0 - maxValue.DayOfWeek);
                            while (maxValue < DateTime.Now)
                            {
                                maxValue = maxValue.AddDays((double)((num2 * 7) * num3));
                                foreach (int num4 in from s in node5.InnerText.ConvertListInt()
                                                     orderby s
                                                     select s)
                                {
                                    if ((maxValue.AddDays((double)num4) > DateTime.Now) && (maxValue.AddDays((double)num4) >= time6))
                                    {
                                        maxValue = maxValue.AddDays((double)num4);
                                        break;
                                    }
                                }
                                num2 = 1;
                            }
                        }
                        else
                        {
                            time6 = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                            maxValue = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                            maxValue = maxValue.AddDays(0 - maxValue.DayOfWeek);
                            while (maxValue < DateTime.Now)
                            {
                                maxValue = maxValue.AddDays((double)((num2 * 7) * num3));
                                foreach (int num4 in from s in node5.InnerText.ConvertListInt()
                                                     orderby s
                                                     select s)
                                {
                                    DateTime objDateTime = maxValue.AddDays((double)num4);
                                    if (objDateTime >= time6)
                                    {
                                        time5 = GetDateTimeBetwwenTime(objDateTime, (str6 == "1") ? num : (num * 60), startTime, endTime);
                                        if (time5 != DateTime.MinValue)
                                        {
                                            maxValue = time5;
                                            break;
                                        }
                                    }
                                }
                                num2 = 1;
                            }
                        }
                        break;
                    }
                default:
                    {
                        XmlNode node6 = node2.SelectSingleNode("Month");
                        string str8 = node6.Attributes["MonthType"].Value;
                        num3 = int.Parse(node6.Attributes["Interval"].Value);
                        if (str8 == "1")
                        {
                            int num5 = int.Parse(node6.SelectSingleNode("Day").InnerText) - 1;
                            if (str2 == "1")
                            {
                                time6 = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                                maxValue = DateTime.Parse(time2.ToString("yyyy-MM-dd") + " " + innerText);
                                maxValue = maxValue.AddDays((double)(-maxValue.Day + 1));
                                while (maxValue < DateTime.Now)
                                {
                                    maxValue = maxValue.AddMonths(num2 * num3);
                                    if (((maxValue.AddDays((double)num5) > DateTime.Now) && (maxValue.AddDays((double)num5) >= time6)) && (maxValue.AddDays((double)num5).Month == maxValue.Month))
                                    {
                                        maxValue = maxValue.AddDays((double)num5);
                                        break;
                                    }
                                    num2 = 1;
                                }
                            }
                            else
                            {
                                time6 = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                                maxValue = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                                maxValue = maxValue.AddDays((double)(-maxValue.Day + 1));
                                while (maxValue < DateTime.Now)
                                {
                                    maxValue = maxValue.AddMonths(num2 * num3);
                                    if ((maxValue.AddDays((double)num5) >= time6) && (maxValue.AddDays((double)num5).Month == maxValue.Month))
                                    {
                                        time5 = GetDateTimeBetwwenTime(maxValue.AddDays((double)num5), (str6 == "1") ? num : (num * 60), startTime, endTime);
                                        if (time5 != DateTime.MinValue)
                                        {
                                            maxValue = time5;
                                            break;
                                        }
                                    }
                                    num2 = 1;
                                }
                            }
                        }
                        else
                        {
                            DateTime time8;
                            XmlNode node7 = node6.SelectSingleNode("Week");
                            int number = int.Parse(node7.Attributes["WeekNumber"].Value);
                            int week = int.Parse(node7.InnerText);
                            if (str2 == "1")
                            {
                                time6 = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                                maxValue = DateTime.Parse(time2.ToString("yyyy-MM-dd") + " " + node3.SelectSingleNode("ExecuteTime").InnerText);
                                maxValue = maxValue.AddDays((double)(-maxValue.Day + 1));
                                while (maxValue < DateTime.Now)
                                {
                                    maxValue = maxValue.AddMonths(num2 * num3);
                                    time8 = GetMonthNumberWeekDate(maxValue, week, number);
                                    if (((time8 != DateTime.MinValue) && (time8 > DateTime.Now)) && (time8 >= time6))
                                    {
                                        maxValue = time8;
                                        break;
                                    }
                                    num2 = 1;
                                }
                            }
                            else
                            {
                                time6 = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                                maxValue = DateTime.Parse(time2.ToString("yyyy-MM-dd"));
                                maxValue = maxValue.AddDays((double)(-maxValue.Day + 1));
                                while (maxValue < DateTime.Now)
                                {
                                    maxValue = maxValue.AddMonths(num2 * num3);
                                    time8 = GetMonthNumberWeekDate(maxValue, week, number);
                                    if ((time8 != DateTime.MinValue) && (time8 >= time6))
                                    {
                                        time5 = GetDateTimeBetwwenTime(time8, (str6 == "1") ? num : (num * 60), startTime, endTime);
                                        if (time5 != DateTime.MinValue)
                                        {
                                            maxValue = time5;
                                            break;
                                        }
                                    }
                                    num2 = 1;
                                }
                            }
                        }
                        break;
                    }
            }
            if (maxValue <= time4)
            {
                return maxValue;
            }
            return DateTime.MaxValue;
        }

        private static DateTime GetDateTimeBetwwenTime(DateTime objDateTime, int Number, string startTime, string endTime)
        {
            DateTime time = DateTime.Parse(objDateTime.ToString("yyyy-MM-dd") + " " + startTime);
            if (time >= DateTime.Now)
            {
                return time;
            }
            DateTime time2 = DateTime.Parse(objDateTime.ToString("yyyy-MM-dd") + " " + endTime);
            while (time.AddMinutes((double)Number) <= time2)
            {
                time = time.AddMinutes((double)Number);
                if (time >= DateTime.Now)
                {
                    break;
                }
            }
            if (time >= DateTime.Now)
            {
                return time;
            }
            return DateTime.MinValue;
        }

        private static DateTime GetMonthNumberWeekDate(DateTime objDateTime, int week, int Number)
        {
            DateTime time = objDateTime.AddDays((double)-objDateTime.Day);
            DateTime minValue = DateTime.MinValue;
            DateTime time3 = objDateTime.AddMonths(1);
            time3 = time3.AddDays((double)-time3.Day);
            int num = 0;
            while (time < time3)
            {
                time = time.AddDays(1.0);
                if ((int)time.DayOfWeek == week)
                {
                    num++;
                    if (Number > 0)
                    {
                        if (num == Number)
                        {
                            return time;
                        }
                    }
                    else
                    {
                        minValue = time;
                    }
                }
            }
            return minValue;
        }
    }
}

