namespace WTF.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public sealed class ParameterRebinder : WTF.Framework.ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression expression;
            if (this.map.TryGetValue(p, out expression))
            {
                p = expression;
            }
            return base.VisitParameter(p);
        }
    }
}

