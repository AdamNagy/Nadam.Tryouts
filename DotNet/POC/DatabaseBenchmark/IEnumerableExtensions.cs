using System.Collections.Generic;
using System.Linq;

namespace DatabaseBenchmark.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> domain, int count)
        {

            var srcCount = domain.Count();
            if( srcCount < count )
            {
                yield return domain;
                yield break;
            }

            var splitSize = srcCount / count;

            var split = new List<T>();
            foreach (var item in domain)
            {
                split.Add(item);
                if( split.Count == splitSize)
                {
                    yield return split;
                    split = new List<T>();
                }
            }
        }
    }
}
