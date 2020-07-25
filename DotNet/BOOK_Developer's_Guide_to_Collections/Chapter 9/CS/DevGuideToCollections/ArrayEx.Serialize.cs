//#define ALWAYS_SERIALIZE_INTERNAL_ARRAY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace DevGuideToCollections
{
    [Serializable()]
    public partial class ArrayEx<T> : ISerializable
    {
        private ArrayEx(SerializationInfo info, StreamingContext context)
        {
            m_count = info.GetInt32("count");
            m_data = (T[])info.GetValue("data", typeof(T[]));
        }


        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("count", m_count);

#if ALWAYS_SERIALIZE_INTERNAL_ARRAY
            info.AddValue("data", m_data);
#else
            // Always serialize multiples of GROW_BY
            int sizeToSerialize = (m_count % GROW_BY == 0) ? m_count : m_count + (GROW_BY - m_count % GROW_BY);

            if (m_count >= m_data.Length - GROW_BY)
            {
                info.AddValue("data", m_data);
            }
            else
            {
                T[] tmp = new T[sizeToSerialize];
                Array.Copy(m_data, tmp, sizeToSerialize);
                info.AddValue("data", tmp);
            }
#endif
        }

        #endregion
    }
}
