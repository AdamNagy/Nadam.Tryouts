using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace SQLiteDemo.QueryRepository
{
    public static class QueryFilter
    {
        public static IQueryable<TEntity> Filter<TEntity>(IQueryable<TEntity> query, IEnumerable<FilterDefinition> filters)
        {
            var parExpr = Expression.Parameter(typeof(TEntity));
            var props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var filter in filters)
            {
                var prop = props.SingleOrDefault(e => e.Name.Equals(filter.PropertyName, StringComparison.OrdinalIgnoreCase));
                if (prop != null)
                {
                    var predicate = CreateExpression(filter, f => CreateWhere<TEntity>(prop, filter), parExpr);
                    if (predicate != null)
                    {
                        query = query.Where(predicate);
                    }
                }
            }

            return query;
        }

        public static IQueryable<TEntity> Filter<TEntity>(IQueryable<TEntity> query, FilterDefinition filter)
        {
            var parExpr = Expression.Parameter(typeof(TEntity));
            var props = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var prop = props.SingleOrDefault(e => e.Name.Equals(filter.PropertyName, StringComparison.OrdinalIgnoreCase));
            if (prop != null)
            {
                var predicate = CreateExpression(filter, f => CreateWhere<TEntity>(prop, filter), parExpr);
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }
            }

            return query;
        }

        #region private methods
        private static Expression<Func<TDestination, bool>> CreateExpression<TDestination>(
            FilterDefinition filter,
            Func<FilterDefinition, Expression<Func<TDestination, bool>>> columnExpr,
            ParameterExpression parExpr = null)
        {
            if (parExpr == null)
            {
                parExpr = Expression.Parameter(typeof(TDestination));
            }

            Expression<Func<TDestination, bool>> predicate = null;


            //try
            //{
            //    //bool dateOperator = fiexpr.Operation.ToString().StartsWith("Date");

            //    if ((fiexpr.ReferenceValue as JsonElement?)?.ValueKind == JsonValueKind.String && dateOperator)
            //    {
            //        fiexpr.ReferenceValue = Convert.ToDateTime(fiexpr.Value.ToString().Split('T')[0]).AddDays(1);
            //    }
            //}
            //catch (FormatException exp)
            //{
            //    throw new FormatException(exp.Message);
            //}

            var expr = columnExpr.Invoke(filter);

            if (expr != null)
            {
                if (predicate != null)
                {
                    //if (fiexpr.Operation.Equals("or", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    predicate = (Expression<Func<TDestination, bool>>)Expression.Lambda(Expression.Or(
                    //        Expression.Invoke(predicate, parExpr),
                    //        Expression.Invoke(expr, parExpr)),
                    //        parExpr);
                    //}
                    //else
                    //{
                    predicate = (Expression<Func<TDestination, bool>>)Expression.Lambda(Expression.And(
                        Expression.Invoke(predicate, parExpr),
                        Expression.Invoke(expr, parExpr)),
                        parExpr);
                    // }
                }
                else
                {
                    predicate = expr;
                }
            }


            return predicate;
        }

        private static Expression<Func<TEntity, bool>> CreateWhere<TEntity>(PropertyInfo propertyInfo, FilterDefinition filter)
        {

            object value = null;
            if (filter.ReferenceValue == null) return CreateWhere<TEntity>(propertyInfo, filter.Operation, value);

            var type = propertyInfo.PropertyType;
            var converter = TypeDescriptor.GetConverter(type);

            if (!converter.CanConvertFrom(typeof(string)))
            {
                throw new NotSupportedException();
            }

            value = converter.ConvertFromInvariantString(filter.ReferenceValue.ToString().ToLower());

            return CreateWhere<TEntity>(propertyInfo, filter.Operation, value);
        }

        private static Expression<Func<TEntity, bool>> CreateWhere<TEntity>(
            PropertyInfo propertyInfo,
            FilterComparer filterOperation,
            object value)
        {
            var parExpr = Expression.Parameter(typeof(TEntity));
            var memberExpr = Expression.MakeMemberAccess(parExpr, propertyInfo);

            return CreateWhere<TEntity>(parExpr, memberExpr, filterOperation, value);
        }

        private static Expression<Func<TEntity, bool>> CreateWhere<TEntity, TColumn>(
            FilterDefinition filter,
            Expression<Func<TEntity, object>> column)
        {
            var parExpr = Expression.Parameter(typeof(TEntity));
            var val = TypeDescriptor.GetConverter(typeof(TColumn)).ConvertFromInvariantString(filter.ReferenceValue.ToString());
            var columnExpr = Expression.Lambda<Func<TEntity, TColumn>>(Expression.Convert(column.Body, typeof(TColumn)), column.Parameters);

            return CreateWhere<TEntity>(parExpr, Expression.Invoke(columnExpr, parExpr), filter.Operation, val);
        }

        private static Expression<Func<TEntity, bool>> CreateWhere<TEntity>(
            ParameterExpression parExpr,
            Expression columnExpr,
            FilterComparer op,
            object value)
        {
            Expression expr;
            Expression exp2 = null;

            var val = Expression.Constant(value);

            switch (op)
            {
                case FilterComparer.StartsWith:
                    if (value == null)
                    {
                        expr = Expression.Call(columnExpr, typeof(Nullable<bool>).GetMethods().Where(x => x.Name == "Equals").FirstOrDefault(), Expression.Constant(value, typeof(object)));
                    }
                    else
                    {
                        exp2 = Expression.Call(columnExpr, typeof(string).GetMethods().Where(x => x.Name == "ToLower" && x.GetParameters().Length == 0).FirstOrDefault());
                        expr = Expression.Call(exp2, typeof(string).GetMethod(nameof(String.StartsWith), new Type[] { typeof(string) }), val);
                    }
                    break;

                case FilterComparer.Contains:
                    exp2 = Expression.Call(columnExpr, typeof(string).GetMethods().Where(x => x.Name == "ToLower" && x.GetParameters().Length == 0).FirstOrDefault());
                    expr = Expression.Call(exp2, typeof(string).GetMethod(nameof(String.Contains), new Type[] { typeof(string) }), val);
                    break;

                //case OperatorEnum.NotContains:
                //    exp2 = Expression.Call(columnExpr, typeof(string).GetMethods().Where(x => x.Name == "ToLower" && x.GetParameters().Length == 0).FirstOrDefault());
                //    expr = Expression.Not(Expression.Call(exp2, typeof(string).GetMethod(nameof(String.Contains), new Type[] { typeof(string) }), val));
                //    break;

                case FilterComparer.EndsWith:
                    exp2 = Expression.Call(columnExpr, typeof(string).GetMethods().Where(x => x.Name == "ToLower" && x.GetParameters().Length == 0).FirstOrDefault());
                    expr = Expression.Call(exp2, typeof(string).GetMethod(nameof(String.EndsWith), new Type[] { typeof(string) }), val);
                    break;

                case FilterComparer.Equals:
                    if (columnExpr.Type == typeof(string))
                    {
                        exp2 = Expression.Call(columnExpr, typeof(string).GetMethods().Where(x => x.Name == "ToLower" && x.GetParameters().Length == 0).FirstOrDefault());
                        expr = Expression.Equal(exp2, Expression.Convert(val, columnExpr.Type));
                    }
                    else
                    {
                        expr = Expression.Equal(columnExpr, Expression.Convert(val, columnExpr.Type));//convert -> Working with nullable types in Expression Trees
                    }
                    break;

                //case OperatorEnum.NotEquals:
                //case OperatorEnum.IsNot:
                //case OperatorEnum.DateIsNot:
                //    if (columnExpr.Type == typeof(string))
                //    {
                //        exp2 = Expression.Call(columnExpr, typeof(string).GetMethods().Where(x => x.Name == "ToLower" && x.GetParameters().Length == 0).FirstOrDefault());
                //        expr = Expression.NotEqual(exp2, Expression.Convert(val, columnExpr.Type));
                //    }
                //    else
                //    {
                //        expr = Expression.NotEqual(columnExpr, Expression.Convert(val, columnExpr.Type));//convert -> Working with nullable types in Expression Trees
                //    }
                //    break;

                //case OperatorEnum.In:
                //    {
                //        var arr = (value as IEnumerable)?.GetEnumerator();
                //        if (arr?.MoveNext() != true)
                //        {
                //            return null;
                //        }
                //        var type = arr.Current?.GetType();
                //        if (columnExpr.Type.IsGenericType && columnExpr.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
                //        {
                //            type = typeof(Nullable<>).MakeGenericType(type);
                //        }
                //        expr = Expression.Call(val, typeof(ICollection<>).MakeGenericType(type).GetMethod("Contains", new Type[] { type }), columnExpr);
                //    }
                //    break;

                case FilterComparer.LessThan:
                    expr = Expression.LessThan(columnExpr, Expression.Convert(val, columnExpr.Type));
                    break;

                //case OperatorEnum.Lte:
                //    expr = Expression.LessThanOrEqual(columnExpr, Expression.Convert(val, columnExpr.Type));
                //    break;

                case FilterComparer.GreaterThan:
                    expr = Expression.GreaterThan(columnExpr, Expression.Convert(val, columnExpr.Type));
                    break;

                //case OperatorEnum.Gte:
                //    expr = Expression.GreaterThanOrEqual(columnExpr, Expression.Convert(val, columnExpr.Type));
                //    break;

                default:
                    throw new NotImplementedException();
            }

            return Expression.Lambda<Func<TEntity, bool>>(expr, parExpr);
        }
        #endregion
    }
}
