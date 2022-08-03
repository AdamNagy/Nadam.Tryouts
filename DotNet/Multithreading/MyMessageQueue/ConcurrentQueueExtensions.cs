using System.Collections.Concurrent;

namespace MyMessageQueue
{
    internal static class ConcurrentQueueExtensions
    {
        public static IEnumerable<T> TryDequeueBatched<T>(this ConcurrentQueue<T> queue, int batchSize)
        {
            var batch = new List<T>(batchSize);

            try
            {
                Monitor.Enter(queue);
                {
                    if (!queue.Any())
                        return batch;

                    for (int i = 0; i < batchSize && queue.Any(); i++)
                    {
                        var dequeuedSucess = queue.TryDequeue(out var dequeued);
                        if(dequeuedSucess)
                            batch.Add(dequeued);
                    }
                }
            }
            finally
            {
                Monitor.Exit(queue);
            }

            return batch;
        }

        public static T TryDequeue<T>(this ConcurrentQueue<T> queue, int tryTimes = 3, bool sleepBetweenTrings = true)
        {
            var trings = 0;
            var success = queue.TryDequeue(out var dequeued);

            while(!success && trings < tryTimes)
            {
                if(sleepBetweenTrings)
                    Task.Delay(50).Wait();

                success = queue.TryDequeue(out dequeued);
                trings++;
            }

            return dequeued;
        }
    }
}
