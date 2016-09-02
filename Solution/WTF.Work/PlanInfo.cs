using System;
using System.Collections.Generic;

namespace WTF.Work
{
	public class PlanInfo
	{
		public string WorkLogID
		{
			get;
			set;
		}

		public int PlanRunID
		{
			get;
			set;
		}

		public int PlanID
		{
			get;
			set;
		}

		public string WorkInfoName
		{
			get;
			set;
		}

		public string PlanName
		{
			get;
			set;
		}

		public string PlanConfigInfo
		{
			get;
			set;
		}

		public string FullName
		{
			get
			{
				return "作业名称:" + this.WorkInfoName + " 计划名称:" + this.PlanName;
			}
		}

		public int WorkInfoID
		{
			get;
			set;
		}

		public DateTime RunDateTime
		{
			get;
			set;
		}

		public List<PlanStepInfo> ProcessInfoList
		{
			get;
			set;
		}

		public PlanInfo()
		{
			this.WorkLogID = Guid.NewGuid().ToString();
		}
	}
}
