using System.Collections;
using System.Collections.Generic;

namespace CshTryouts.EnumerablePattern
{
    public class MyEnumerable : IEnumerable<int>, IEnumerable
    {
        private List<int> backbone = new List<int>(10);

        public MyEnumerable()
        {
            for (int i = 1; i <= 10; i++)
            {
                backbone.Add(i);
            }
        }

        public IEnumerable<int> SelfEnumerator()
        {
            foreach (var number in backbone)
            {
                yield return number;
            }
        }

        public IEnumerable<int> SelfEnumerator(bool useYield)
        {
            return backbone;
        }

        public int this[int idx]
        {
            get => backbone[idx];
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new MyEnumerableEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MyEnumerableEnumerator : IEnumerator<int>
    {
        public int Current => backbone[index];
        object IEnumerator.Current => Current;

        private MyEnumerable backbone;
        private int index;

        public MyEnumerableEnumerator()
        {
            backbone = new MyEnumerable();
            index = -1;
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (index < 9)
            {
                ++index;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
