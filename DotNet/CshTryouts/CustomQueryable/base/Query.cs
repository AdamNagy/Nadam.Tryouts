using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CustomQueryable
{
    public class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
    {
        QueryProvider provider;
        Expression expression;

        Expression IQueryable.Expression
        {
            get { return expression; }
        }

        Type IQueryable.ElementType
        {
            get { return typeof(T); }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return provider; }
        }

        public Query(QueryProvider _provider)
        {
            provider = _provider ?? throw new ArgumentNullException("provider");

            expression = Expression.Constant(this);
        }

        public Query(QueryProvider _provider, Expression _expression)
        {
            if (_provider == null)
                throw new ArgumentNullException("provider");

            if (_expression == null)
                throw new ArgumentNullException("expression");

            if (!typeof(IQueryable<T>).IsAssignableFrom(_expression.Type))
                throw new ArgumentOutOfRangeException("expression");

            provider = _provider;
            expression = _expression;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)provider.Execute(expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)provider.Execute(expression)).GetEnumerator();
        }

        public override string ToString()
        {
            return provider.GetQueryText(expression);
        }
    }
}
