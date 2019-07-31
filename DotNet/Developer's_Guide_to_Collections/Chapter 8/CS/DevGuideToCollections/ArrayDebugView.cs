using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DevGuideToCollections
{
      /// <summary>
    /// Proxy for displaying the class in the debugger.
    /// </summary>  
    class ArrayDebugView
    {
        System.Collections.IEnumerable m_obj;

        public ArrayDebugView(System.Collections.IEnumerable obj)
        {
            m_obj = obj;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object[] Items
        {
            get
            {
                if (m_obj == null)
                {
                    return new object[0];
                }

                List<Object> retval = new List<object>();
                foreach (object item in m_obj)
                {
                    retval.Add(item);
                }

                return retval.ToArray();
            }
        }
    }
}
