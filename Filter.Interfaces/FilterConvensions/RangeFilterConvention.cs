using Filter.Interfaces.FilterTypes;
using System.Linq.Expressions;

namespace Filter.Interfaces.FilterConvensions
{
    public class RangeFilterConvention : FilterConventionBase
    {
        public override Expression<Func<T, bool>> CreateConvention<T>(MemberExpression property, ParameterExpression parameter, object value)
        {
            var rangeFilter = value as IRangeFilter;

            var startConstant = Expression.Constant(rangeFilter.StartValue);

            var endConstant = Expression.Constant(rangeFilter.EndValue);

            var greaterThen = Expression.GreaterThan(property, startConstant);

            var lessThen = Expression.LessThan(property, endConstant);

            var body = Expression.AndAlso(greaterThen, lessThen);

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
