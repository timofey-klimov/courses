using Entities.Base;
using Shared;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.Base
{
    public class OrSpecification<T> : BaseSpecification<T>
        where T : BaseEntity
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;
        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> CreateCriteria()
        {
            var leftExpression = _left.CreateCriteria();
            var rightExpression = _right.CreateCriteria();

            return leftExpression.Or(rightExpression);
        }
    }
}
