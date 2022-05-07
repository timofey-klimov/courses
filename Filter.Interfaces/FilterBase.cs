namespace Filter.Interfaces
{
    public abstract class FilterBase<T> : IFilter<T>
    {
        public IQueryable<T> Filter(IQueryable<T> query, FilterCondition condition)
        {
            var expr = FilterBuilder.CreateExpression(this, condition);

            return query.Where(expr);
        }
    }
}
