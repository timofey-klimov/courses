using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Shared
{
    public class ParameterReplacer : ExpressionVisitor
    {

        private readonly ParameterExpression[] _first;
        private readonly ParameterExpression[] _second;

        public ParameterReplacer(IEnumerable<ParameterExpression> first, IEnumerable<ParameterExpression> second)
        {
            _first = first.ToArray();
            _second = second.ToArray();
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            for (int i = 0; i < _second.Count(); i++)
            {
                if (node == _second[i])
                {
                    return base.VisitParameter(_first[i]);
                }
            }

            return base.VisitParameter(node);
        }
    }
}
