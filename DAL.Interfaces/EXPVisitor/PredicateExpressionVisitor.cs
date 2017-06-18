using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.EXPVisitor
{
    public class PredicateExpressionVisitor<TSource, TDestination> : ExpressionVisitor
    {
        public ParameterExpression ReplacementParameter { get; private set; }

        public PredicateExpressionVisitor(ParameterExpression replacementParameter)
        {
            ReplacementParameter = replacementParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return ReplacementParameter;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TSource))
                return Expression.MakeMemberAccess(this.Visit(node.Expression),
                   typeof(TDestination).GetMember(node.Member.Name).FirstOrDefault());
            return base.VisitMember(node);
        }
    }
}
