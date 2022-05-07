using Filter.Interfaces.FilterConvensions;
using Shared;
using System.Linq.Expressions;
using System.Reflection;

namespace Filter.Interfaces
{
    public static class FilterBuilder
    {
        public static Expression<Func<T, bool>> CreateExpression<T>(IFilter<T> filter, FilterCondition condition)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            var parameter = Expression.Parameter(typeof(T));

            var filterProps = filter.GetType().GetProperties();

            var props = typeof(T).GetProperties()
                .Where(x => filterProps.Any(y => y.Name == x.Name))
                .Select(x => new PropertyInfoAndValue()
                {
                    PropertyInfo = x,
                    Value = CheckDefaultValueOfType(filter, x, filterProps)
                })
                .Where(x => x.Value != null)
                .ToArray();

            var expressions = props
                .Select(x => Create<T>(parameter, x))
                .ToArray();

            var expr = condition == FilterCondition.And 
                ? expressions.Aggregate((c, n) => c.And(n)) 
                : expressions.Aggregate((c, n) => c.Or(n));

            return expr;
        }

        private static Expression<Func<T, bool>> Create<T>(ParameterExpression parameter, PropertyInfoAndValue info)
        {
            var property = Expression.Property(parameter, info.PropertyInfo);

            var conventionFabric = new FilterConventionFabric();
            var convention = conventionFabric.CreateConvention(info.Value.GetType());

            return convention.CreateConvention<T>(property, parameter, info.Value);
        }

        #region helper
        private static object CheckDefaultValueOfType<T>(IFilter<T> filter, PropertyInfo entityProprty, PropertyInfo[] filterProps)
        {
            var property = filterProps.FirstOrDefault(x => x.Name == entityProprty.Name);

            if (property is null)
                return null;

            var value = property.GetValue(filter);

            switch (value)
            {
                case int intValue when intValue == default:
                    value = null;
                    break;
                case long longValue when longValue == default:
                    value = null;
                    break;
                case short shortValue when shortValue == default:
                    value = null;
                    break;
                case byte byteValue when byteValue == default:
                    value = null;
                    break;
            }

            return value;
        }
        #endregion
    }
}
