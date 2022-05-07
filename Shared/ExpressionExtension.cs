using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class ExpressionExtension
    {
        public static Expression<T> Compose<T>(
           this Expression<T> first,
           Expression<T> second,
           Func<Expression, Expression, Expression> merge)
        {
            var secondBody = new ParameterReplacer(first.Parameters, second.Parameters).Visit(second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second) =>
                first.Compose(second, Expression.AndAlso);


        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second) =>
                first.Compose(second, Expression.OrElse);
    }
}
