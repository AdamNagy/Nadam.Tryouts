using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DevGuideToCollections
{
    [Serializable()]
    public partial class AssociativeArrayHT<TKey, TValue> : ISerializable
    {
        private AssociativeArrayHT(SerializationInfo info, StreamingContext context)
        {
            int count = info.GetInt32("count");

            if (count > 0)
            {
                List<KeyValuePair<TKey, TValue>> tmp = (List<KeyValuePair<TKey, TValue>>)info.GetValue("kvps", typeof(List<KeyValuePair<TKey, TValue>>));
                foreach (KeyValuePair<TKey, TValue> kvp in tmp)
                {
                    Add(kvp.Key, kvp.Value);
                }
            }
        }


        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("count", Count);

            if (Count > 0)
            {
                List<KeyValuePair<TKey, TValue>> kvps = new List<KeyValuePair<TKey, TValue>>();

                foreach (KeyValuePair<TKey, TValue> kvp in this)
                {
                    kvps.Add(kvp);
                }

                info.AddValue("kvps", kvps);
            }
        }

        #endregion
    }
}
