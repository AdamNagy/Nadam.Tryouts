using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace CustomQueryable
{
    internal class ObjectReader<T> : IEnumerable<T>, IEnumerable where T : class, new()
    {
        Enumerator enumerator;

        internal ObjectReader(DbDataReader reader)
        {
            enumerator = new Enumerator(reader);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Enumerator e = this.enumerator;

            if (e == null)
                throw new InvalidOperationException("Cannot enumerate more than once");

            enumerator = null;

            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            DbDataReader reader;
            FieldInfo[] fields;
            int[] fieldLookup;
            T current;

            internal Enumerator(DbDataReader reader)
            {
                this.reader = reader;
                fields = typeof(T).GetFields();
            }

            public T Current
            {
                get { return current; }
            }

            object IEnumerator.Current
            {

                get { return current; }

            }

            public bool MoveNext()
            {
                if (reader.Read())
                {
                    if (fieldLookup == null)
                    {
                        this.InitFieldLookup();
                    }

                    T instance = new T();

                    for (int i = 0, n = fields.Length; i < n; i++)
                    {
                        int index = fieldLookup[i];

                        if (index >= 0)
                        {
                            FieldInfo fi = fields[i];

                            if (this.reader.IsDBNull(index))
                                fi.SetValue(instance, null);
                            else
                                fi.SetValue(instance, this.reader.GetValue(index));
                        }
                    }

                    current = instance;
                    return true;
                }
                return false;
            }

            public void Reset() {}

            public void Dispose()
            {
                this.reader.Dispose();
            }

            private void InitFieldLookup()
            {
                Dictionary<string, int> map = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

                for (int i = 0, n = reader.FieldCount; i < n; i++)
                {
                    map.Add(reader.GetName(i), i);
                }

                fieldLookup = new int[fields.Length];

                for (int i = 0, n = fields.Length; i < n; i++)
                {
                    int index;

                    if (map.TryGetValue(this.fields[i].Name, out index))
                        this.fieldLookup[i] = index;
                    else
                        this.fieldLookup[i] = -1;
                }
            }
        }
    }
}
