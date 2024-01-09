using System.Linq.Expressions;

namespace Nadam.DataServices.QueryExpression
{
    public class QueryOrder
    {
        public static IQueryable<TEntity> Ordering<TEntity>(IQueryable<TEntity> query, IEnumerable<OrderDefinition> orderings)
        {
            foreach (var order in orderings)
            {
                query = OrderQuery(query, CreateOrderByExpr<TEntity>(order.PropertyName), order.Param);
            }

            return query;
        }

        public static IOrderedQueryable<TEntity> OrderQuery<TEntity>(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, object>> expr,
            OrderParam direction)
        {
            var visitor = new OrederByVisitor();
            visitor.Visit(query.Expression);
            if (direction == OrderParam.Asc)
            {
                if (visitor.HasOrderBy)
                {
                    return ((IOrderedQueryable<TEntity>)query).ThenByDescending(expr);
                }
                return query.OrderByDescending(expr);
            }
            else
            {
                if (visitor.HasOrderBy)
                {
                    return ((IOrderedQueryable<TEntity>)query).ThenBy(expr);
                }
                return query.OrderBy(expr);
            }
        }

        public static Expression<Func<TEntity, object>> CreateOrderByExpr<TEntity>(string propertyName)
        {
            var parExpr = Expression.Parameter(typeof(TEntity));
            Expression conversion = Expression.Convert(Expression.Property(parExpr, propertyName), typeof(object));
            return Expression.Lambda<Func<TEntity, object>>(conversion, parExpr);
        }
    }
}
