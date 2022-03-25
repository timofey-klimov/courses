using Entities.Base;
using System;
using System.Linq.Expressions;

namespace DataAccess.Interfaces.Specifications.Base
{
    public class AndSpecification<T> : BaseSpecification<T>
        where T : BaseEntity
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;
        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> CreateCriteria()
        {
            Expression<Func<T, bool>> leftExpression = _left.CreateCriteria();
            Expression<Func<T, bool>> rightExpression = _right.CreateCriteria();
            var paramExpr = Expression.Parameter(typeof(T));

            BinaryExpression andExpression = Expression.AndAlso(
                leftExpression.Body, rightExpression.Body);

            andExpression = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(andExpression);

            return Expression.Lambda<Func<T, bool>>(
                andExpression, paramExpr);
        }
    }
}
