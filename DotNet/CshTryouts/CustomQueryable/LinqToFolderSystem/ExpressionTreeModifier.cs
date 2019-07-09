using System.Linq;
using System.Linq.Expressions;

namespace CustomQueryable
{
    internal class ExpressionTreeModifier : ExpressionVisitor
    {
        private IQueryable<FileSystemElement> queryableFileSystemElements;

        internal ExpressionTreeModifier(IQueryable<FileSystemElement> fileSystemElements)
        {
            queryableFileSystemElements = fileSystemElements;
        }

        // public override Expression Visit(Expression exp) => Visit(exp);

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // Replace the constant FileSystemContext arg with the queryable fileSystemElements.
            if (c.Type == typeof(FileSystemContext))
            {
                return Expression.Constant(queryableFileSystemElements);
            }
            else
            {
                return c;
            }
        }
    }
}
