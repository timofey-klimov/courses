using System.Linq.Expressions;

namespace Filter.Interfaces.FilterConvensions
{
    public interface IFilterConvention
    {
        public Expression<Func<T, bool>> CreateConvention<T>(MemberExpression property, ParameterExpression parameter, object value);
    }
}
