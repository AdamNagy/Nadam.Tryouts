using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        private object locker = new object();
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            // todo: feel free to add your code here
            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);
            var tasks = new List<Task<(long i, long j, long sum)>>((int)(m1.RowCount * m2.ColCount)); // long to int

            // Parallel.For(0, tasks.Count, i => { });
            var sw = new Stopwatch();
            // sw.StartNew();
            for (long i = 0; i < m1.RowCount; i++)
            {
                for (byte j = 0; j < m2.ColCount; j++)
                {
                    long a = i, b = j;
                    tasks.Add(Task.Run(() => CalcMatrixItem(ref m1, ref m2, a, b)));
                }
            };
            
            var taskArr = tasks.ToArray();
            Task.WaitAll(taskArr);

            foreach (var multiplyTask in taskArr)
            {
                var multipliedItem = multiplyTask.Result;
                resultMatrix.SetElement(multipliedItem.i, multipliedItem.j, multipliedItem.sum);
            }

            return resultMatrix;
        }

        private (long i, long j, long sum) CalcMatrixItem(ref IMatrix a, ref IMatrix b, long i, long j)
        {
            // not needed
            lock(locker)
            {
                long sum = 0;
                for (byte k = 0; k < a.ColCount; k++)
                    sum += a.GetElement(i, k) * b.GetElement(k, j);

                return (i, j, sum);
            }
        }
    }
}
