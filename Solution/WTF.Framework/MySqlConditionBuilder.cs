namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class MySqlConditionBuilder : MyExpressionVisitor
    {
        private List<object> m_arguments;
        private Stack<string> m_conditionParts;

        public void Build(Expression expression)
        {
            Expression exp = new PartialEvaluator().Eval(expression);
            this.m_arguments = new List<object>();
            this.m_conditionParts = new Stack<string>();
            this.Visit(exp);
            this.Arguments = this.m_arguments.ToArray();
            this.Condition = (this.m_conditionParts.Count > 0) ? this.m_conditionParts.Pop() : null;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b != null)
            {
                string str;
                switch (b.NodeType)
                {
                    case ExpressionType.Divide:
                        str = "/";
                        break;

                    case ExpressionType.Equal:
                        str = "=";
                        break;

                    case ExpressionType.GreaterThan:
                        str = ">";
                        break;

                    case ExpressionType.GreaterThanOrEqual:
                        str = ">=";
                        break;

                    case ExpressionType.LessThan:
                        str = "<";
                        break;

                    case ExpressionType.LessThanOrEqual:
                        str = "<=";
                        break;

                    case ExpressionType.AndAlso:
                        str = "AND";
                        break;

                    case ExpressionType.Add:
                        str = "+";
                        break;

                    case ExpressionType.NotEqual:
                        str = "<>";
                        break;

                    case ExpressionType.OrElse:
                        str = "OR";
                        break;

                    case ExpressionType.Subtract:
                        str = "-";
                        break;

                    case ExpressionType.Multiply:
                        str = "*";
                        break;

                    default:
                        throw new NotSupportedException(b.NodeType + " is not supported.");
                }
                this.Visit(b.Left);
                this.Visit(b.Right);
                string format = this.m_conditionParts.Pop();
                string str3 = this.m_conditionParts.Pop();
                string item = string.Format("({0} {1} {2})", str3, str, string.Format(format, "?" + str3));
                this.m_conditionParts.Push(item);
            }
            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c != null)
            {
                this.m_arguments.Add(c.Value);
                this.m_conditionParts.Push(string.Format("{{{0}}}", 0));
            }
            return c;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m != null)
            {
                PropertyInfo member = m.Member as PropertyInfo;
                if (member == null)
                {
                    return m;
                }
                this.m_conditionParts.Push(string.Format("{0}", member.Name));
            }
            return m;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            string str;
            if (m == null)
            {
                return m;
            }
            string name = m.Method.Name;
            if (name != null)
            {
                if (!(name == "StartsWith"))
                {
                    if (name == "Contains")
                    {
                        str = "{0} LIKE ?{1}";
                        goto Label_0085;
                    }
                    if (name == "EndsWith")
                    {
                        str = "{0} LIKE ?{1}";
                        goto Label_0085;
                    }
                }
                else
                {
                    str = "{0} LIKE ?{1}";
                    goto Label_0085;
                }
            }
            throw new NotSupportedException(m.NodeType + " is not supported!");
        Label_0085:
            this.Visit(m.Object);
            this.Visit(m.Arguments[0]);
            string str2 = this.m_conditionParts.Pop();
            string str3 = this.m_conditionParts.Pop();
            this.m_conditionParts.Push(string.Format(str, str3, str3));
            return m;
        }

        public object[] Arguments { get; private set; }

        public Dictionary<string, object> ArgumentsList { get; private set; }

        public string Condition { get; private set; }
    }
}

