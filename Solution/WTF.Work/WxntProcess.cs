using WTF.Framework;
using System;

namespace WTF.Work
{
	public class WxntProcess : JobProcess
	{
		public override InvokeResult Execute(string config, DateTime runDateTime, InvokeResult previousStepResult)
		{
			Console.WriteLine("五心内天开始执行");
			base.LogWrite("五心内天开始执行");
			return ConfigHelper.GetBoolTrueValue("IsRunSucess") ? base.CreateSuccessResult() : base.CreateFailResult();
		}
	}
}
