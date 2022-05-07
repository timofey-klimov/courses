using Filter.Interfaces.FilterTypes;
using System.Linq.Expressions;
using System.Reflection;

namespace Filter.Interfaces.FilterConvensions
{
    public class StringFilterConvention : FilterConventionBase
    {
        private static Dictionary<StringCondition, MethodInfo> _dict;

        static StringFilterConvention()
        {
            _dict = new Dictionary<StringCondition, MethodInfo>()
            {
                { StringCondition.StartsWith, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }) },
                { StringCondition.Contains, typeof(string).GetMethod("Contains", new[] { typeof(string) }) },
                { StringCondition.Equals, typeof(string).GetMethod("Equals", new[] {typeof(string)}) }
            };
        }

        public override Expression<Func<T, bool>> CreateConvention<T>(MemberExpression member, ParameterExpression parameter, object value)
        {
            var stringFilter = value as StringFilter;

            if (stringFilter.IsNull)
                return DoNothing<T>();

            var constant = Expression.Constant(stringFilter.Value);

            return Expression.Lambda<Func<T, bool>>(CreateMethod(constant, member, stringFilter.Condition), parameter);
        }

        private MethodCallExpression CreateMethod(ConstantExpression constant, MemberExpression member, StringCondition stringMethodType)
        {
            return Expression.Call(member, _dict[stringMethodType], constant);
        }
    }
}
