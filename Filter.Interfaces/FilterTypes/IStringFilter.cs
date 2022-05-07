namespace Filter.Interfaces.FilterTypes
{
    public interface IStringFilter
    {
        public string Value { get; }

        public bool IsNull { get; }

        public StringCondition Condition { get; }
    }

    public enum StringCondition
    {
        Equals,
        StartsWith,
        Contains
    }
}
