using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuideToCollections
{
    public partial class ArrayEx<T> : IList<T>, System.Collections.IList
    {
        #region IList Members

        bool CanCastToT(object value)
        {
            if (!(value is T) && ((value != null) || typeof(T).IsValueType))
            {
                return false;
            }
            return true;
        }

 


        int System.Collections.IList.Add(object value)
        {
            if (!CanCastToT(value))
            {
                throw new ArgumentException("Value is not of type T", "value");
            }
            
            Add((T)value);

            return Count - 1;
        }

        bool System.Collections.IList.Contains(object value)
        {
            if (!CanCastToT(value))
            {
                throw new ArgumentException("Value is not of type T", "value");
            }
            return Contains((T)value);
        }

        int System.Collections.IList.IndexOf(object value)
        {
            if (!CanCastToT(value))
            {
                throw new ArgumentException("Value is not of type T", "value");
            }
            return IndexOf((T)value);
        }

        void System.Collections.IList.Insert(int index, object value)
        {
            if (!CanCastToT(value))
            {
                throw new ArgumentException("Value is not of type T", "value");
            }
            Insert(index, (T)value);
        }

        void System.Collections.IList.Remove(object value)
        {
            if (!CanCastToT(value))
            {
                throw new ArgumentException("Value is not of type T", "value");
            }
            Remove((T)value);
        }

        object System.Collections.IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                if (!CanCastToT(value))
                {
                    throw new ArgumentException("Value is not of type T", "value");
                }
                this[index] = (T)value;
            }
        }

        bool System.Collections.IList.IsFixedSize
        {
            get { return false; }
        }

        #endregion
    }
}
