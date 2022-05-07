namespace Filter.Interfaces.FilterTypes
{
    public class StringFilter : IStringFilter
    {
        public string Value { get; }

        public bool IsNull { get; }

        public StringCondition Condition { get; }

        public StringFilter(string value, StringCondition condition = StringCondition.StartsWith)
        {
            Value = value;
            Condition = condition;
            IsNull = string.IsNullOrEmpty(value);
        }

    }
}
