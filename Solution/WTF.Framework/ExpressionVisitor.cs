namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;

    public abstract class ExpressionVisitor
    {
        protected ExpressionVisitor()
        {
        }

        protected ReadOnlyCollection<Expression> Visit(ReadOnlyCollection<Expression> nodes)
        {
            List<Expression> list = null;
            int num = 0;
            int count = nodes.Count;
            while (num < count)
            {
                Expression item = this.Visit(nodes[num]);
                if (list != null)
                {
                    list.Add(item);
                }
                else if (item != nodes[num])
                {
                    list = new List<Expression>(count);
                    for (int i = 0; i < num; i++)
                    {
                        list.Add(nodes[i]);
                    }
                    list.Add(item);
                }
                num++;
            }
            if (list != null)
            {
                return list.AsReadOnly();
            }
            return nodes;
        }

        protected virtual Expression Visit(Expression node)
        {
            if (node == null)
            {
                return node;
            }
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.ArrayIndex:
                case ExpressionType.Coalesce:
                case ExpressionType.Divide:
                case ExpressionType.Equal:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LeftShift:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.NotEqual:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.RightShift:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return this.VisitBinary((BinaryExpression) node);

                case ExpressionType.ArrayLength:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    return this.VisitUnary((UnaryExpression) node);

                case ExpressionType.Call:
                    return this.VisitMethodCall((MethodCallExpression) node);

                case ExpressionType.Conditional:
                    return this.VisitConditional((ConditionalExpression) node);

                case ExpressionType.Constant:
                    return this.VisitConstant((ConstantExpression) node);

                case ExpressionType.Invoke:
                    return this.VisitInvocation((InvocationExpression) node);

                case ExpressionType.Lambda:
                    return this.VisitLambda((LambdaExpression) node);

                case ExpressionType.ListInit:
                    return this.VisitListInit((ListInitExpression) node);

                case ExpressionType.MemberAccess:
                    return this.VisitMember((MemberExpression) node);

                case ExpressionType.MemberInit:
                    return this.VisitMemberInit((MemberInitExpression) node);

                case ExpressionType.New:
                    return this.VisitNew((NewExpression) node);

                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    return this.VisitNewArray((NewArrayExpression) node);

                case ExpressionType.Parameter:
                    return this.VisitParameter((ParameterExpression) node);

                case ExpressionType.TypeIs:
                    return this.VisitTypeBinary((TypeBinaryExpression) node);
            }
            throw new Exception(string.Format("Unhandled expression type: '{0}'", node.NodeType));
        }

        protected virtual Expression VisitBinary(BinaryExpression node)
        {
            Expression left = this.Visit(node.Left);
            Expression right = this.Visit(node.Right);
            Expression expression3 = this.Visit(node.Conversion);
            if (((left != node.Left) || (right != node.Right)) || (expression3 != node.Conversion))
            {
                if ((node.NodeType == ExpressionType.Coalesce) && (node.Conversion != null))
                {
                    return Expression.Coalesce(left, right, expression3 as LambdaExpression);
                }
                return Expression.MakeBinary(node.NodeType, left, right, node.IsLiftedToNull, node.Method);
            }
            return node;
        }

        protected virtual Expression VisitConditional(ConditionalExpression node)
        {
            Expression test = this.Visit(node.Test);
            Expression ifTrue = this.Visit(node.IfTrue);
            Expression ifFalse = this.Visit(node.IfFalse);
            if (((test != node.Test) || (ifTrue != node.IfTrue)) || (ifFalse != node.IfFalse))
            {
                return Expression.Condition(test, ifTrue, ifFalse);
            }
            return node;
        }

        protected virtual Expression VisitConstant(ConstantExpression node)
        {
            return node;
        }

        protected virtual ElementInit VisitElementInit(ElementInit node)
        {
            ReadOnlyCollection<Expression> arguments = this.Visit(node.Arguments);
            if (arguments != node.Arguments)
            {
                return Expression.ElementInit(node.AddMethod, arguments);
            }
            return node;
        }

        protected virtual IEnumerable<ElementInit> VisitElementInitList(ReadOnlyCollection<ElementInit> nodes)
        {
            List<ElementInit> list = null;
            int num = 0;
            int count = nodes.Count;
            while (num < count)
            {
                ElementInit item = this.VisitElementInit(nodes[num]);
                if (list != null)
                {
                    list.Add(item);
                }
                else if (item != nodes[num])
                {
                    list = new List<ElementInit>(count);
                    for (int i = 0; i < num; i++)
                    {
                        list.Add(nodes[i]);
                    }
                    list.Add(item);
                }
                num++;
            }
            if (list != null)
            {
                return list;
            }
            return nodes;
        }

        protected virtual Expression VisitInvocation(InvocationExpression node)
        {
            IEnumerable<Expression> arguments = this.Visit(node.Arguments);
            Expression expression = this.Visit(node.Expression);
            if ((arguments != node.Arguments) || (expression != node.Expression))
            {
                return Expression.Invoke(expression, arguments);
            }
            return node;
        }

        protected virtual Expression VisitLambda(LambdaExpression node)
        {
            Expression body = this.Visit(node.Body);
            if (body != node.Body)
            {
                return Expression.Lambda(node.Type, body, node.Parameters);
            }
            return node;
        }

        protected virtual Expression VisitListInit(ListInitExpression node)
        {
            NewExpression newExpression = this.VisitNew(node.NewExpression);
            IEnumerable<ElementInit> initializers = this.VisitElementInitList(node.Initializers);
            if ((newExpression != node.NewExpression) || (initializers != node.Initializers))
            {
                return Expression.ListInit(newExpression, initializers);
            }
            return node;
        }

        protected virtual Expression VisitMember(MemberExpression node)
        {
            Expression expression = this.Visit(node.Expression);
            if (expression != node.Expression)
            {
                return Expression.MakeMemberAccess(expression, node.Member);
            }
            return node;
        }

        protected virtual MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            Expression expression = this.Visit(node.Expression);
            if (expression != node.Expression)
            {
                return Expression.Bind(node.Member, expression);
            }
            return node;
        }

        protected virtual MemberBinding VisitMemberBinding(MemberBinding node)
        {
            switch (node.BindingType)
            {
                case MemberBindingType.Assignment:
                    return this.VisitMemberAssignment((MemberAssignment) node);

                case MemberBindingType.MemberBinding:
                    return this.VisitMemberMemberBinding((MemberMemberBinding) node);

                case MemberBindingType.ListBinding:
                    return this.VisitMemberListBinding((MemberListBinding) node);
            }
            throw new Exception(string.Format("Unhandled binding type '{0}'", node.BindingType));
        }

        protected virtual IEnumerable<MemberBinding> VisitMemberBindingList(ReadOnlyCollection<MemberBinding> nodes)
        {
            List<MemberBinding> list = null;
            int num = 0;
            int count = nodes.Count;
            while (num < count)
            {
                MemberBinding item = this.VisitMemberBinding(nodes[num]);
                if (list != null)
                {
                    list.Add(item);
                }
                else if (item != nodes[num])
                {
                    list = new List<MemberBinding>(count);
                    for (int i = 0; i < num; i++)
                    {
                        list.Add(nodes[i]);
                    }
                    list.Add(item);
                }
                num++;
            }
            if (list != null)
            {
                return list;
            }
            return nodes;
        }

        protected virtual Expression VisitMemberInit(MemberInitExpression node)
        {
            NewExpression newExpression = this.VisitNew(node.NewExpression);
            IEnumerable<MemberBinding> bindings = this.VisitMemberBindingList(node.Bindings);
            if ((newExpression != node.NewExpression) || (bindings != node.Bindings))
            {
                return Expression.MemberInit(newExpression, bindings);
            }
            return node;
        }

        protected virtual MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            IEnumerable<ElementInit> initializers = this.VisitElementInitList(node.Initializers);
            if (initializers != node.Initializers)
            {
                return Expression.ListBind(node.Member, initializers);
            }
            return node;
        }

        protected virtual MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            IEnumerable<MemberBinding> bindings = this.VisitMemberBindingList(node.Bindings);
            if (bindings != node.Bindings)
            {
                return Expression.MemberBind(node.Member, bindings);
            }
            return node;
        }

        protected virtual Expression VisitMethodCall(MethodCallExpression node)
        {
            Expression instance = this.Visit(node.Object);
            IEnumerable<Expression> arguments = this.Visit(node.Arguments);
            if ((instance != node.Object) || (arguments != node.Arguments))
            {
                return Expression.Call(instance, node.Method, arguments);
            }
            return node;
        }

        protected virtual NewExpression VisitNew(NewExpression node)
        {
            IEnumerable<Expression> arguments = this.Visit(node.Arguments);
            if (arguments != node.Arguments)
            {
                if (node.Members != null)
                {
                    return Expression.New(node.Constructor, arguments, node.Members);
                }
                return Expression.New(node.Constructor, arguments);
            }
            return node;
        }

        protected virtual Expression VisitNewArray(NewArrayExpression node)
        {
            IEnumerable<Expression> initializers = this.Visit(node.Expressions);
            if (initializers != node.Expressions)
            {
                if (node.NodeType == ExpressionType.NewArrayInit)
                {
                    return Expression.NewArrayInit(node.Type.GetElementType(), initializers);
                }
                return Expression.NewArrayBounds(node.Type.GetElementType(), initializers);
            }
            return node;
        }

        protected virtual Expression VisitParameter(ParameterExpression node)
        {
            return node;
        }

        protected virtual Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            Expression expression = this.Visit(node.Expression);
            if (expression != node.Expression)
            {
                return Expression.TypeIs(expression, node.TypeOperand);
            }
            return node;
        }

        protected virtual Expression VisitUnary(UnaryExpression node)
        {
            Expression operand = this.Visit(node.Operand);
            if (operand != node.Operand)
            {
                return Expression.MakeUnary(node.NodeType, operand, node.Type, node.Method);
            }
            return node;
        }
    }
}

