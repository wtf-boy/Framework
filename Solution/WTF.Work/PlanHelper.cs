using WTF.Framework;
using System;
using System.Linq;
using System.Xml;

namespace WTF.Work
{
	public static class PlanHelper
	{
		private static DateTime GetDateTimeBetwwenTime(DateTime objDateTime, int Number, string startTime, string endTime)
		{
			DateTime dateTime = DateTime.Parse(objDateTime.ToString("yyyy-MM-dd") + " " + startTime);
			DateTime result;
			if (dateTime >= DateTime.Now)
			{
				result = dateTime;
			}
			else
			{
				DateTime t = DateTime.Parse(objDateTime.ToString("yyyy-MM-dd") + " " + endTime);
				while (dateTime.AddMinutes((double)Number) <= t)
				{
					dateTime = dateTime.AddMinutes((double)Number);
					if (dateTime >= DateTime.Now)
					{
						break;
					}
				}
				if (dateTime >= DateTime.Now)
				{
					result = dateTime;
				}
				else
				{
					result = DateTime.MinValue;
				}
			}
			return result;
		}

		private static DateTime GetMonthNumberWeekDate(DateTime objDateTime, int week, int Number)
		{
			DateTime dateTime = objDateTime.AddDays((double)(-(double)objDateTime.Day));
			DateTime result = DateTime.MinValue;
			DateTime t = objDateTime.AddMonths(1);
			t = t.AddDays((double)(-(double)t.Day));
			int num = 0;
			while (dateTime < t)
			{
				dateTime = dateTime.AddDays(1.0);
				if (dateTime.DayOfWeek == (DayOfWeek)week)
				{
					num++;
					if (Number > 0)
					{
						if (num == Number)
						{
							result = dateTime;
							break;
						}
					}
					else
					{
						result = dateTime;
					}
				}
			}
			return result;
		}

		public static DateTime CreateNextPlanDate(string config, DateTime lastRunDate)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(config);
			DateTime dateTime = DateTime.MaxValue;
			XmlNode xmlNode = xmlDocument.SelectSingleNode("Plan");
			string value = xmlNode.Attributes["Type"].Value;
			DateTime result;
			if (value == "1")
			{
				result = DateTime.Parse(xmlNode.SelectSingleNode("Execute").InnerText);
			}
			else
			{
				DateTime dateTime2 = (lastRunDate.ToString("yyyy-MM-dd") == DateTime.MaxValue.ToString("yyyy-MM-dd")) ? DateTime.Parse(xmlNode.Attributes["StartDate"].Value) : DateTime.Parse(lastRunDate.ToString("yyyy-MM-dd"));
				DateTime dateTime3 = DateTime.Parse(xmlNode.Attributes["EndDate"].Value);
				DateTime t = (dateTime3.ToString("yyyy-MM-dd") == DateTime.MaxValue.ToString("yyyy-MM-dd")) ? dateTime3 : DateTime.Parse(dateTime3.ToString("yyyy-MM-dd") + " 23:59:59");
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Frequency");
				XmlNode xmlNode3 = xmlNode.SelectSingleNode("DayFrequency");
				string value2 = xmlNode3.Attributes["Type"].Value;
				string value3 = xmlNode2.Attributes["Type"].Value;
				string text = "";
				string endTime = "";
				string a = "";
				int num = 0;
				string str = "";
				if (value2 == "2")
				{
					XmlNode xmlNode4 = xmlNode3.SelectSingleNode("Interval");
					text = xmlNode4.Attributes["StartTime"].Value;
					endTime = xmlNode4.Attributes["EndTime"].Value;
					a = xmlNode4.Attributes["Type"].Value;
					num = int.Parse(xmlNode4.Attributes["Number"].Value);
				}
				else
				{
					str = xmlNode3.SelectSingleNode("ExecuteTime").InnerText;
				}
				int num2 = 0;
				if (value3 == "1")
				{
					int num3 = int.Parse(xmlNode2.SelectSingleNode("Day").Attributes["Interval"].Value);
					if (value2 == "1")
					{
						dateTime = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd") + " " + str);
						while (dateTime < DateTime.Now)
						{
							dateTime = dateTime.AddDays((double)num3);
						}
					}
					else
					{
						dateTime = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd") + " " + text);
						while (dateTime < DateTime.Now)
						{
							dateTime = dateTime.AddDays((double)(num2 * num3));
							DateTime dateTimeBetwwenTime = PlanHelper.GetDateTimeBetwwenTime(dateTime, (a == "1") ? num : (num * 60), text, endTime);
							if (dateTimeBetwwenTime != DateTime.MinValue)
							{
								dateTime = dateTimeBetwwenTime;
								break;
							}
							num2 = 1;
						}
					}
				}
				else if (value3 == "2")
				{
					XmlNode xmlNode5 = xmlNode2.SelectSingleNode("Week");
					int num3 = int.Parse(xmlNode5.Attributes["Interval"].Value);
					if (value2 == "1")
					{
						DateTime t2 = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
						dateTime = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd") + " " + str);
						dateTime = dateTime.AddDays((double)(-(double)dateTime.DayOfWeek));
						while (dateTime < DateTime.Now)
						{
							dateTime = dateTime.AddDays((double)(num2 * 7 * num3));
							foreach (int current in from s in xmlNode5.InnerText.ConvertListInt()
							orderby s
							select s)
							{
								if (dateTime.AddDays((double)current) > DateTime.Now && dateTime.AddDays((double)current) >= t2)
								{
									dateTime = dateTime.AddDays((double)current);
									break;
								}
							}
							num2 = 1;
						}
					}
					else
					{
						DateTime t2 = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
						dateTime = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
						dateTime = dateTime.AddDays((double)(-(double)dateTime.DayOfWeek));
						while (dateTime < DateTime.Now)
						{
							dateTime = dateTime.AddDays((double)(num2 * 7 * num3));
							foreach (int current in from s in xmlNode5.InnerText.ConvertListInt()
							orderby s
							select s)
							{
								DateTime dateTime4 = dateTime.AddDays((double)current);
								if (dateTime4 >= t2)
								{
									DateTime dateTimeBetwwenTime = PlanHelper.GetDateTimeBetwwenTime(dateTime4, (a == "1") ? num : (num * 60), text, endTime);
									if (dateTimeBetwwenTime != DateTime.MinValue)
									{
										dateTime = dateTimeBetwwenTime;
										break;
									}
								}
							}
							num2 = 1;
						}
					}
				}
				else
				{
					XmlNode xmlNode6 = xmlNode2.SelectSingleNode("Month");
					string value4 = xmlNode6.Attributes["MonthType"].Value;
					int num3 = int.Parse(xmlNode6.Attributes["Interval"].Value);
					if (value4 == "1")
					{
						int num4 = int.Parse(xmlNode6.SelectSingleNode("Day").InnerText) - 1;
						if (value2 == "1")
						{
							DateTime t2 = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
							dateTime = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd") + " " + str);
							dateTime = dateTime.AddDays((double)(-(double)dateTime.Day + 1));
							while (dateTime < DateTime.Now)
							{
								dateTime = dateTime.AddMonths(num2 * num3);
								if (dateTime.AddDays((double)num4) > DateTime.Now && dateTime.AddDays((double)num4) >= t2 && dateTime.AddDays((double)num4).Month == dateTime.Month)
								{
									dateTime = dateTime.AddDays((double)num4);
									break;
								}
								num2 = 1;
							}
						}
						else
						{
							DateTime t2 = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
							dateTime = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
							dateTime = dateTime.AddDays((double)(-(double)dateTime.Day + 1));
							while (dateTime < DateTime.Now)
							{
								dateTime = dateTime.AddMonths(num2 * num3);
								if (dateTime.AddDays((double)num4) >= t2 && dateTime.AddDays((double)num4).Month == dateTime.Month)
								{
									DateTime dateTimeBetwwenTime = PlanHelper.GetDateTimeBetwwenTime(dateTime.AddDays((double)num4), (a == "1") ? num : (num * 60), text, endTime);
									if (dateTimeBetwwenTime != DateTime.MinValue)
									{
										dateTime = dateTimeBetwwenTime;
										break;
									}
								}
								num2 = 1;
							}
						}
					}
					else
					{
						XmlNode xmlNode7 = xmlNode6.SelectSingleNode("Week");
						int number = int.Parse(xmlNode7.Attributes["WeekNumber"].Value);
						int week = int.Parse(xmlNode7.InnerText);
						if (value2 == "1")
						{
							DateTime t2 = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
							dateTime = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd") + " " + xmlNode3.SelectSingleNode("ExecuteTime").InnerText);
							dateTime = dateTime.AddDays((double)(-(double)dateTime.Day + 1));
							while (dateTime < DateTime.Now)
							{
								dateTime = dateTime.AddMonths(num2 * num3);
								DateTime monthNumberWeekDate = PlanHelper.GetMonthNumberWeekDate(dateTime, week, number);
								if (monthNumberWeekDate != DateTime.MinValue && monthNumberWeekDate > DateTime.Now && monthNumberWeekDate >= t2)
								{
									dateTime = monthNumberWeekDate;
									break;
								}
								num2 = 1;
							}
						}
						else
						{
							DateTime t2 = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
							dateTime = DateTime.Parse(dateTime2.ToString("yyyy-MM-dd"));
							dateTime = dateTime.AddDays((double)(-(double)dateTime.Day + 1));
							while (dateTime < DateTime.Now)
							{
								dateTime = dateTime.AddMonths(num2 * num3);
								DateTime monthNumberWeekDate = PlanHelper.GetMonthNumberWeekDate(dateTime, week, number);
								if (monthNumberWeekDate != DateTime.MinValue && monthNumberWeekDate >= t2)
								{
									DateTime dateTimeBetwwenTime = PlanHelper.GetDateTimeBetwwenTime(monthNumberWeekDate, (a == "1") ? num : (num * 60), text, endTime);
									if (dateTimeBetwwenTime != DateTime.MinValue)
									{
										dateTime = dateTimeBetwwenTime;
										break;
									}
								}
								num2 = 1;
							}
						}
					}
				}
				if (dateTime <= t)
				{
					result = dateTime;
				}
				else
				{
					result = DateTime.MaxValue;
				}
			}
			return result;
		}
	}
}
