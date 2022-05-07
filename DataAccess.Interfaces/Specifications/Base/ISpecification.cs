using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.Base
{
    public interface ISpecification<T>
        where T : BaseEntity
    {
        abstract Expression<Func<T, bool>> CreateCriteria();

        ISpecification<T> And(ISpecification<T> spec);

        ISpecification<T> Or(ISpecification<T> spec);
    }
}
