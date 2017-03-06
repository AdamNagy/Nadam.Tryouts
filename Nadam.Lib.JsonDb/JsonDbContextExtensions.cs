using System;
using System.Collections.Generic;
using System.Linq;

namespace Nadam.Lib.JsonDb
{
    public static class JsonDbContextExtensions
    {
        public static void SelectForUpdate<T>(this JsonDbEngineContext context, Func<T, bool> pred, out T reference)
        {
            reference = (T)context.Set<T>()
                                  .SingleOrDefault(p => pred((T)p));
        }

        public static IList<T> Set<T>(this JsonDbEngineContext context)
        {
            var tableName = typeof(T).Name;
            tableName = tableName.PluralizeString();
            return (List<T>)context.GetValueFor<JsonDbEngineContext>(tableName);
        }

        public static void SetIdsFor(this IEnumerable<object> table)
        {
            int lastId = (int)table.Max(p => p.GetValueFor("Id"));
            //table.Where(p => (int)p.GetValueFor("Id") == 0)
            //     .Foreach(q => q.SetValueFor("Id", lastId));

            foreach (var item in table.Where(p => (int)p.GetValueFor("Id") == 0))
            {
                item.SetValueFor("Id", ++lastId);
            }
        }
    }
}
