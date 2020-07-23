using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class NotificationList<T> : IEnumerable<T>
    {
        #region IEnumerable<T> Members

        /// <summary>
        /// Gets the enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator for this collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Gets the enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator for this collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        #endregion
    }
}
