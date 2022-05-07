using System.Linq.Expressions;

namespace Filter.Interfaces.FilterConvensions
{
    public class PrimitiveConvention : FilterConventionBase
    {
        public override Expression<Func<T, bool>> CreateConvention<T>(MemberExpression property, ParameterExpression parameter, object value)
        {
            if (value == null)
                return DoNothing<T>();

            var constant = Expression.Constant(value);

            var equals = Expression.Equal(property, constant);

            return Expression.Lambda<Func<T, bool>>(equals, parameter);
        }
    }
}
