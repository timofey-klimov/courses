using System.Linq.Expressions;

namespace Filter.Interfaces.FilterConvensions
{
    public abstract class FilterConventionBase : IFilterConvention
    {
        public abstract Expression<Func<T, bool>> CreateConvention<T>(MemberExpression property, ParameterExpression parameter, object value);

        protected Expression<Func<T, bool>> DoNothing<T>()
            => x => true;
    }
}
