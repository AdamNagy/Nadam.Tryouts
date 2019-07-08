using System.Collections.Generic;

namespace CshTryouts.EnumerablePattern
{
    public static class YieldUseage
    {
        public static IEnumerable<double> Fibonacci(double nth)
        {
            double n1 = 1, n2 = 1;
            yield return n1;
            yield return n2;

            for (int i = 0; i < nth - 2; i++)
            {
                var temp = n2;
                n2 = n1 + temp;
                n1 = temp;
                yield return n2;
            }
        }
    }
}
