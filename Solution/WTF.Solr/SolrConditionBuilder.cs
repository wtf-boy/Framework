using WTF.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace WTF.Solr
{
	public class SolrConditionBuilder : MyExpressionVisitor
	{
		private List<object> m_arguments;

		private Stack<object> m_conditionParts;

		public string Condition
		{
			get;
			private set;
		}

		public object[] Arguments
		{
			get;
			private set;
		}

		public Dictionary<string, object> ArgumentsList
		{
			get;
			private set;
		}

		public void Build(Expression expression)
		{
			this.Condition = "";
			if (expression != null)
			{
				PartialEvaluator partialEvaluator = new PartialEvaluator();
				Expression expression2 = partialEvaluator.Eval(expression);
				this.m_arguments = new List<object>();
				this.m_conditionParts = new Stack<object>();
				this.Visit(expression2);
				this.Arguments = this.m_arguments.ToArray();
				this.Condition = ((this.m_conditionParts.Count > 0) ? (this.m_conditionParts.Pop() as string) : "");
			}
		}

		protected override Expression VisitBinary(BinaryExpression b)
		{
			Expression result;
			if (b == null)
			{
				result = b;
			}
			else
			{
				ExpressionType nodeType = b.NodeType;
				string text;
				if (nodeType != ExpressionType.AndAlso)
				{
					switch (nodeType)
					{
					case ExpressionType.Equal:
						text = ":{value}";
						goto IL_C4;
					case ExpressionType.ExclusiveOr:
					case ExpressionType.Invoke:
					case ExpressionType.Lambda:
					case ExpressionType.LeftShift:
						break;
					case ExpressionType.GreaterThan:
						text = ":{{value} TO *}";
						goto IL_C4;
					case ExpressionType.GreaterThanOrEqual:
						text = ":[{value} TO *}";
						goto IL_C4;
					case ExpressionType.LessThan:
						text = ":{ * TO {value}}";
						goto IL_C4;
					case ExpressionType.LessThanOrEqual:
						text = ":{ * TO {value}]";
						goto IL_C4;
					default:
						switch (nodeType)
						{
						case ExpressionType.NotEqual:
							text = ":{value}";
							goto IL_C4;
						case ExpressionType.OrElse:
							text = "OR";
							goto IL_C4;
						}
						break;
					}
					throw new NotSupportedException(b.NodeType + " is not supported.");
				}
				text = "AND";
				IL_C4:
				this.Visit(b.Left);
				this.Visit(b.Right);
				object obj = this.m_conditionParts.Pop();
				string text2;
				if (obj.GetType() == typeof(string))
				{
					text2 = (obj as string);
				}
				else if (obj.GetType() == typeof(DateTime))
				{
					text2 = "\"" + ((DateTime)obj).ToString("yyyy-MM-ddTHH:mm:ssZ") + "\"";
				}
				else if (obj.GetType() == typeof(bool))
				{
					text2 = (((bool)obj) ? "1" : "0");
				}
				else
				{
					text2 = obj.ToString();
				}
				string text3 = this.m_conditionParts.Pop() as string;
				if (b.NodeType == ExpressionType.NotEqual)
				{
					text3 = "-" + text3;
				}
				string item;
				if (b.NodeType == ExpressionType.AndAlso)
				{
					item = string.Format("{0} {1} {2}", text3, text, text2);
				}
				else if (b.NodeType == ExpressionType.OrElse)
				{
					if (text3.Split(new char[]
					{
						':'
					}).Length >= 4)
					{
						text3 = "(" + text3 + ")";
					}
					if (text2.Split(new char[]
					{
						':'
					}).Length >= 4)
					{
						text2 = "(" + text2 + ")";
					}
					item = string.Format("({0} {1} {2})", text3, text, text2);
				}
				else
				{
					item = string.Format("{0}{1}", text3, text.Replace("{value}", text2));
				}
				this.m_conditionParts.Push(item);
				result = b;
			}
			return result;
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			Expression result;
			if (c == null)
			{
				result = c;
			}
			else
			{
				this.m_arguments.Add(c.Value);
				this.m_conditionParts.Push(c.Value);
				result = c;
			}
			return result;
		}

		protected override Expression VisitMemberAccess(MemberExpression m)
		{
			Expression result;
			if (m == null)
			{
				result = m;
			}
			else
			{
				PropertyInfo propertyInfo = m.Member as PropertyInfo;
				if (propertyInfo == null)
				{
					result = m;
				}
				else
				{
					this.m_conditionParts.Push(string.Format("{0}", propertyInfo.Name));
					result = m;
				}
			}
			return result;
		}

		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression result;
			if (m != null)
			{
				string name = m.Method.Name;
				if (name != null)
				{
					string format;
					if (!(name == "StartsWith"))
					{
						if (!(name == "Contains"))
						{
							if (!(name == "EndsWith"))
							{
								goto IL_6B;
							}
							format = "{0}:{1}*";
						}
						else
						{
							format = "{0}:*{1}*";
						}
					}
					else
					{
						format = "{0}:*{1}";
					}
					if (m.Object != null)
					{
						this.Visit(m.Object);
						this.Visit(m.Arguments[0]);
					}
					else
					{
						foreach (Expression current in m.Arguments)
						{
							this.Visit(current);
						}
					}
					object obj = this.m_conditionParts.Pop();
					object obj2 = this.m_conditionParts.Pop();
					string text;
					object obj3;
					if (obj2 is string)
					{
						text = (obj2 as string);
						obj3 = obj;
					}
					else
					{
						text = (obj as string);
						obj3 = obj2;
					}
					object arg;
					if (obj3.GetType() == typeof(string))
					{
						arg = (obj3 as string);
					}
					else if (obj3.GetType() == typeof(DateTime))
					{
						arg = ((DateTime)obj3).ToString("yyyy-MM-ddTHH:mm:ssZ");
					}
					else
					{
						if (obj3 is ICollection || obj3 is IEnumerable)
						{
							string text2 = "";
							foreach (object current2 in (obj3 as IEnumerable))
							{
								string text3 = (current2.GetType() == typeof(string)) ? "\"" : "";
								object obj4 = text2;
								text2 = string.Concat(new object[]
								{
									obj4,
									" ",
									text,
									":",
									text3,
									current2,
									text3,
									" OR"
								});
							}
							text2 = StringHelper.TrimEnd(text2, "OR");
							text2 = "( " + text2 + " )";
							this.m_conditionParts.Push(text2);
							result = m;
							return result;
						}
						arg = obj3.ToString();
					}
					this.m_conditionParts.Push(string.Format(format, text, arg));
					result = m;
					return result;
				}
				IL_6B:
				throw new NotSupportedException(m.NodeType + " is not supported!");
			}
			result = m;
			return result;
		}
	}
}
