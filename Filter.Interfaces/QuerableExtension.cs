namespace Filter.Interfaces
{
    public static class QuerableExtension
    {
        public static IQueryable<T> Filter<T>(
            this IQueryable<T> query, 
            IFilter<T> filter, 
            FilterCondition condition = FilterCondition.Or)
                => filter.Filter(query, condition);
    }
}
