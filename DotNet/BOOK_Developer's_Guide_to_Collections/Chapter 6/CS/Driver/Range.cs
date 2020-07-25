using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Driver
{
    /// <summary>
    /// A simple class for interating over a range of integers.
    /// </summary>
    public class Range : IEnumerable<int>
    {
        int m_start;
        int m_end;

        #region Constructors

        /// <summary>
        /// Creates a new instance of the Range class that goes from 1 to end.
        /// </summary>
        /// <param name="end">The last number in the range. </param>
        public Range(int end)
        {
            m_start = 1;
            m_end = end;
        }

        /// <summary>
        /// Creates a new instance of the Range class that goes from start to end.
        /// </summary>
        /// <param name="start">The first number in the range.</param>
        /// <param name="end">The last number in the range.</param>
        public Range(int start, int end)
        {
            m_start = start;
            m_end = end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the start number.
        /// </summary>
        public int Start
        {
            get { return m_start; }
            set { m_start = value; }
        }

        /// <summary>
        /// Gets or sets the end number.
        /// </summary>
        public int End
        {
            get { return m_end; }
            set { m_end = value; }
        }

        #endregion

        /// <summary>
        /// Represents the enumerator for this class.
        /// </summary>
        public struct Enumerator : IEnumerator<int>
        {
            Range m_range;
            int m_index;

            internal Enumerator(Range range)
            {
                m_index = 0;
                m_range = range;
            }

            #region IEnumerator<int> Members

            public int Current
            {
                get
                {
                    Console.WriteLine("\tIEnumerator.Current");
                    return m_index;
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    Console.WriteLine("\tIEnumerator.Current");
                    return m_index;
                }
            }

            public bool MoveNext()
            {
                if (m_index >= m_range.End)
                {
                    Console.WriteLine("\tIEnumerator.MoveNext=false");
                    return false;
                }
                Console.WriteLine("\tIEnumerator.MoveNext");
                ++m_index;
                return true;
            }

            public void Reset()
            {
                Console.WriteLine("IEnumerator.Reset");
                m_index = m_range.Start - 1;
            }

            #endregion
        }

        #region IEnumerable<int> Members

        /// <summary>
        /// Gets the enumerator for this class.
        /// </summary>
        /// <returns>A enumerator that can be used to iterate the class.</returns>
        public IEnumerator<int> GetEnumerator()
        {
            Console.WriteLine("IEnumerable.GetEnumerator");
            return new Enumerator(this);
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// IEnumerable(T) is derived from IEnumerable. We also have to implement the non typesafe GetEnumerator as well.
        /// </summary>
        /// <returns>A enumerator that can be used to iterate the class.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            Console.WriteLine("IEnumerable.GetEnumerator");
            return new Enumerator(this);
        }

        #endregion
    }
}
