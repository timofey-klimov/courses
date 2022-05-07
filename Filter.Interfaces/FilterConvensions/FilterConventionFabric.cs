using Filter.Interfaces.FilterTypes;

namespace Filter.Interfaces.FilterConvensions
{
    public class FilterConventionFabric
    {
        public IFilterConvention CreateConvention(Type type)
        {
            if (typeof(IStringFilter).IsAssignableFrom(type))
            {
                return new StringFilterConvention();
            }

            if (typeof(IRangeFilter).IsAssignableFrom(type))
            {
                return new RangeFilterConvention();
            }
            return new PrimitiveConvention();
        }
    }
}
