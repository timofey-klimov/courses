using Entities.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.Base
{
    public abstract class BaseSpecification<T> : ISpecification<T>
        where T : BaseEntity
    {
        public abstract Expression<Func<T, bool>> CreateCriteria();

        public ISpecification<T> And(ISpecification<T> spec)
        {
            return new AndSpecification<T>(this, spec);
        }
        public ISpecification<T> Or(ISpecification<T> spec)
        {
            return new OrSpecification<T>(this, spec);
        }

        protected Expression<Func<T, bool>> AlwaysTrue() =>
            Expression.Lambda<Func<T, bool>>(Expression.Constant(true), Expression.Parameter(typeof(T), "_"));
    }
}
