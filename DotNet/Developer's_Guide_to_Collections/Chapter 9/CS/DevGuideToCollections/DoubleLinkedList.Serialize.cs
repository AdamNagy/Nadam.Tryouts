using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DevGuideToCollections
{
    [Serializable()]
    public partial class DoubleLinkedList<T>: ISerializable
    {
        private DoubleLinkedList(SerializationInfo info, StreamingContext context)
        {
            int count = info.GetInt32("count");

            if (count > 0)
            {
                T[] tmp = (T[])info.GetValue("nodes", typeof(T[]));
                for (int i = 0; i < count; ++i)
                {
                    AddToEnd(tmp[i]);
                }
            }
        }


        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("count", Count);

            if (Count > 0)
            {
                T[] tmp = ToArray();

                info.AddValue("nodes", tmp);
            }
        }

        #endregion
    }
}
