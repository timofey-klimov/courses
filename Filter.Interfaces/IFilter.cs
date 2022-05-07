namespace Filter.Interfaces
{
    public interface IFilter<T>
    {
        IQueryable<T> Filter(IQueryable<T> query, FilterCondition condition);
    }
}
