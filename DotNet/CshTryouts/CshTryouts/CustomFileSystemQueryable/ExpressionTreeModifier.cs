using System.Linq;
using System.Linq.Expressions;

namespace CshTryouts.CustomQueryable
{
    class ExpressionTreeModifier : ExpressionVisitor
    {
        private IQueryable<FileSystemElement> queryablePlaces;

        internal ExpressionTreeModifier(IQueryable<FileSystemElement> places)
        {
            this.queryablePlaces = places;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // Replace the constant QueryableTerraServerData arg with the queryable Place collection. 
            if (c.Type == typeof(QueryableTerraServerData<Place>))
                return Expression.Constant(this.queryablePlaces);
            else
                return c;
        }
    }
}
