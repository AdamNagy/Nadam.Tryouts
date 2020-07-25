using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    class LambdaComparer<T> : IComparer<T>
    {
        Func<T, T, int> m_compare;

        public LambdaComparer(Func<T, T, int> compareFunction)
        {
            if (compareFunction == null)
            {
                throw new ArgumentNullException("compareFunction");
            }

            m_compare = compareFunction;
        }

        public int Compare(T x, T y)
        {
            return m_compare(x,y);
        }
    }
}
