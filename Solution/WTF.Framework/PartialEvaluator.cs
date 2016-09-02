namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class PartialEvaluator : System.Linq.Expressions.ExpressionVisitor
    {
        private HashSet<Expression> m_candidates;
        private Func<Expression, bool> m_fnCanBeEvaluated;

        public PartialEvaluator() : this(new Func<Expression, bool>(PartialEvaluator.CanBeEvaluatedLocally))
        {
        }

        public PartialEvaluator(Func<Expression, bool> fnCanBeEvaluated)
        {
            this.m_fnCanBeEvaluated = fnCanBeEvaluated;
        }

        private static bool CanBeEvaluatedLocally(Expression exp)
        {
            return (exp.NodeType != ExpressionType.Parameter);
        }

        public Expression Eval(Expression exp)
        {
            this.m_candidates = new Nominator(this.m_fnCanBeEvaluated).Nominate(exp);
            return this.Visit(exp);
        }

        private Expression Evaluate(Expression e)
        {
            if (e.NodeType == ExpressionType.Constant)
            {
                return e;
            }
            return Expression.Constant(Expression.Lambda(e, new ParameterExpression[0]).Compile().DynamicInvoke(null), e.Type);
        }

        public override Expression Visit(Expression exp)
        {
            if (exp == null)
            {
                return null;
            }
            if (this.m_candidates.Contains(exp))
            {
                return this.Evaluate(exp);
            }
            return base.Visit(exp);
        }

        private class Nominator : System.Linq.Expressions.ExpressionVisitor
        {
            private HashSet<Expression> m_candidates;
            private bool m_cannotBeEvaluated;
            private Func<Expression, bool> m_fnCanBeEvaluated;

            internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                this.m_fnCanBeEvaluated = fnCanBeEvaluated;
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                this.m_candidates = new HashSet<Expression>();
                this.Visit(expression);
                return this.m_candidates;
            }

            public override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    bool cannotBeEvaluated = this.m_cannotBeEvaluated;
                    this.m_cannotBeEvaluated = false;
                    base.Visit(expression);
                    if (!this.m_cannotBeEvaluated)
                    {
                        if (this.m_fnCanBeEvaluated(expression))
                        {
                            this.m_candidates.Add(expression);
                        }
                        else
                        {
                            this.m_cannotBeEvaluated = true;
                        }
                    }
                    this.m_cannotBeEvaluated |= cannotBeEvaluated;
                }
                return expression;
            }
        }
    }
}

