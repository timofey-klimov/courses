using DataAccess.Interfaces;
using DataAccess.Interfaces.Specifications.Base;
using Entities.Base;
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
            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);

            return finalExpr;
        }
    }
}
