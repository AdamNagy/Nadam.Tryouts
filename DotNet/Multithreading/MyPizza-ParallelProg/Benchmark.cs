using System.Collections.Concurrent;
using System.Diagnostics;

namespace MyPizza_ParallelProg
{
    internal static class Benchmark
    {
        public static TimeSpan Run(Action action)
        {
            var sw = new Stopwatch();
            sw.Start();
            action();
            sw.Stop();

            Console.WriteLine($"Time passed: {sw.ElapsedMilliseconds}");
            return sw.Elapsed;
        }

        public static ConcurrentQueue<T> ToConcurrentQueue<T>(this IEnumerable<T> enumerable)
            => new ConcurrentQueue<T>(enumerable);
    }
}
