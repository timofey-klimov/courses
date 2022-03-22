using Entities.Base;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IFilterProvider
    {
        IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> spec)
            where TEntity : BaseEntity;

        Task<int> GetCountQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> spec)
             where TEntity : BaseEntity;
    }
}
