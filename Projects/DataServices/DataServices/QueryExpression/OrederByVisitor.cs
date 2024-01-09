using System.Linq.Expressions;

namespace Nadam.DataServices.QueryExpression
{
    public class OrederByVisitor : ExpressionVisitor
    {
        public bool HasOrderBy { get; private set; }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable) &&
                (node.Method.Name == nameof(Queryable.OrderBy) || node.Method.Name == nameof(Queryable.OrderByDescending)) &&
                !node.Method.ReturnType.GenericTypeArguments[0].Name.Contains("AnonymousType"))
                HasOrderBy = true;

            return base.VisitMethodCall(node);
        }
    }
}
