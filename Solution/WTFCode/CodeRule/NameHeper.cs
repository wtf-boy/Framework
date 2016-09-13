using System;
using System.Text.RegularExpressions;

namespace WTF.CodeRule
{
	public static class NameHeper
	{
		private static Regex cleanRegEx = new Regex("\\s+|_|-|\\.", RegexOptions.Compiled);

		public static string CleanName(this string name)
		{
			return name;
		}

		public static string PascalCase(this string name)
		{
			string text = name.CleanName();
			return char.ToUpper(text[0]) + text.Substring(1);
		}

		public static string PropertyName(this string column)
		{
			return column.PascalCase();
		}

		public static string MemberName(this string column)
		{
			return "_" + column.CamelCase();
		}

		public static string CamelCase(this string name)
		{
			string text = name.CleanName();
			return char.ToLower(text[0]) + text.Substring(1);
		}
	}
}
