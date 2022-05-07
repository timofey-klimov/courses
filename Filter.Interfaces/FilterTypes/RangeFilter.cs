namespace Filter.Interfaces.FilterTypes
{
    public class RangeFilter<TValue> : IRangeFilter
        where TValue: struct
    {
        public object StartValue { get; }

        public object EndValue { get; }

        public RangeFilter(TValue startValue, TValue endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
        }
    }
}
