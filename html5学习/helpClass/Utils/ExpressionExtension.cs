using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace helpClass.Utils
{
    /// <summary>
    /// 表达式扩展
    /// </summary>
    public static class ExpressionExtension
    {
        private static Expression<T> Compose<T>(this Expression<T> exp1, Expression<T> exp2, Func<Expression, Expression, Expression> merge)
        {
            var map = exp1.Parameters.Select((f, i) => new { f, s = exp2.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var exp2Body = ParameterRebinder.ReplaceParameters(map, exp2.Body);
            return Expression.Lambda<T>(merge(exp1.Body, exp2Body), exp1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            return exp1.Compose(exp2, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            return exp1.Compose(exp2, Expression.Or);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
        {
            return exp1.Compose(exp2, Expression.NotEqual);
        }
    }

    public class ParameterRebinder : ExpressionVisitor
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
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }

    }
}

