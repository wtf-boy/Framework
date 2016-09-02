namespace WTF.Framework
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;

    public abstract class MyExpressionVisitor<T>
    {
        protected MyExpressionVisitor()
        {
        }

        protected virtual T Visit(Expression node)
        {
            if (node == null)
            {
                return default(T);
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

        protected T Visit(ReadOnlyCollection<Expression> nodes)
        {
            int num = 0;
            int count = nodes.Count;
            while (num < count)
            {
                this.Visit(nodes[num]);
                num++;
            }
            return default(T);
        }

        protected virtual T VisitBinary(BinaryExpression node)
        {
            this.Visit(node.Left);
            this.Visit(node.Right);
            this.Visit(node.Conversion);
            return default(T);
        }

        protected virtual T VisitConditional(ConditionalExpression node)
        {
            this.Visit(node.Test);
            this.Visit(node.IfTrue);
            this.Visit(node.IfFalse);
            return default(T);
        }

        protected virtual T VisitConstant(ConstantExpression node)
        {
            return default(T);
        }

        protected virtual T VisitElementInit(ElementInit node)
        {
            this.Visit(node.Arguments);
            return default(T);
        }

        protected T VisitElementInitList(ReadOnlyCollection<ElementInit> nodes)
        {
            int num = 0;
            int count = nodes.Count;
            while (num < count)
            {
                this.VisitElementInit(nodes[num]);
                num++;
            }
            return default(T);
        }

        protected virtual T VisitInvocation(InvocationExpression node)
        {
            this.Visit(node.Arguments);
            this.Visit(node.Expression);
            return default(T);
        }

        protected virtual T VisitLambda(LambdaExpression node)
        {
            this.Visit(node.Body);
            return default(T);
        }

        protected virtual T VisitListInit(ListInitExpression node)
        {
            this.VisitNew(node.NewExpression);
            this.VisitElementInitList(node.Initializers);
            return default(T);
        }

        protected virtual T VisitMember(MemberExpression node)
        {
            this.Visit(node.Expression);
            return default(T);
        }

        protected virtual T VisitMemberAssignment(MemberAssignment node)
        {
            this.Visit(node.Expression);
            return default(T);
        }

        protected virtual T VisitMemberBinding(MemberBinding node)
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

        protected virtual T VisitMemberBindingList(ReadOnlyCollection<MemberBinding> nodes)
        {
            int num = 0;
            int count = nodes.Count;
            while (num < count)
            {
                this.VisitMemberBinding(nodes[num]);
                num++;
            }
            return default(T);
        }

        protected virtual T VisitMemberInit(MemberInitExpression node)
        {
            this.VisitNew(node.NewExpression);
            this.VisitMemberBindingList(node.Bindings);
            return default(T);
        }

        protected virtual T VisitMemberListBinding(MemberListBinding node)
        {
            this.VisitElementInitList(node.Initializers);
            return default(T);
        }

        protected virtual T VisitMemberMemberBinding(MemberMemberBinding node)
        {
            this.VisitMemberBindingList(node.Bindings);
            return default(T);
        }

        protected virtual T VisitMethodCall(MethodCallExpression node)
        {
            this.Visit(node.Object);
            this.Visit(node.Arguments);
            return default(T);
        }

        protected virtual T VisitNew(NewExpression node)
        {
            this.Visit(node.Arguments);
            return default(T);
        }

        protected virtual T VisitNewArray(NewArrayExpression node)
        {
            this.Visit(node.Expressions);
            return default(T);
        }

        protected virtual T VisitParameter(ParameterExpression node)
        {
            return default(T);
        }

        protected virtual T VisitTypeBinary(TypeBinaryExpression node)
        {
            this.Visit(node.Expression);
            return default(T);
        }

        protected virtual T VisitUnary(UnaryExpression node)
        {
            this.Visit(node.Operand);
            return default(T);
        }
    }
}

