using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace CustomQueryable
{
    public class DbQueryProvider : QueryProvider
    {
        DbConnection connection;

        public DbQueryProvider(DbConnection connection)
        {
            this.connection = connection;
        }

        public override string GetQueryText(Expression expression)
        {
            return Translate(expression);
        }

        public override object Execute(Expression expression)
        {
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = Translate(expression);
            DbDataReader reader = cmd.ExecuteReader();
            Type elementType = TypeSystem.GetElementType(expression.Type);

            return Activator.CreateInstance(
                typeof(ObjectReader<>).MakeGenericType(elementType),
                BindingFlags.Instance | BindingFlags.NonPublic, null,
                new object[] { reader },
                null);
        }
        private string Translate(Expression expression)
        {
            return new QueryTranslator().Translate(expression);
        }
    }
}
