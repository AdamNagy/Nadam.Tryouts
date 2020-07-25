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
    internal class AssociativeArrayDebugView
    {
        object m_obj;

        public struct KeyValuePair
        {
            public KeyValuePair(object key, object value)
            {
                Key = key;
                Value = value;
            }

            public object Key;
            public object Value;

            public override string ToString()
            {
                return string.Format("[{0},{1}]",Key,Value);
            }
        }

        public AssociativeArrayDebugView(object obj)
        {
            m_obj = obj;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair[] Items
        {
            get
            {
                if (m_obj == null)
                {
                    return new KeyValuePair[0];
                }

                // You can use reflection to get the elements until the chapter of enumerations.
                var keys = m_obj.GetType().GetProperty("Keys");
                var values = m_obj.GetType().GetProperty("Values");

                if (keys == null || values == null)
                {
                    return new KeyValuePair[0];
                }

                Array rKeys = keys.GetValue(m_obj, null) as Array;
                Array rValues = values.GetValue(m_obj, null) as Array;



                KeyValuePair[] retval = new KeyValuePair[rKeys.Length];

                for (int i = 0; i < rKeys.Length && i < rValues.Length; ++i)
                {
                    retval[i] = new KeyValuePair(rKeys.GetValue(i), rValues.GetValue(i));
                }

                return  retval;
            }
        }
   }
}
