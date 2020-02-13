using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyCollection
{
    public interface IIndexList<T> : IEnumerable<T>
    {
        int Add(T val);
        void Remove(T val);

        T this[int index] { get; }
        int this[T index] { get; }

        int Count { get; }
    }

    public class IndexList<T> : IIndexList<T>
    {
        private List<(bool, T)> backbone;
        private int nextFreeSlot = -1;
        private int capacity;

        public IndexList(int size = 0)
        {
            if (size > 0)
            {
                backbone = new List<(bool, T)>(size);
                capacity = size;
            }
            else
            {
                backbone = new List<(bool, T)>(30);
                capacity = 30;
            }

            for (int i = 0; i < capacity; i++)
            {
                backbone.Add((false, default(T)));
            }
        }

        public T this[int index]
        {
            get
            {
                if(index >= capacity)
                    throw new IndexOutOfRangeException();

                var toLookFor = backbone[index];
                if( !toLookFor.Item1 )
                    throw new IndexOutOfRangeException($"Slot is empty at index {index}");

                return toLookFor.Item2;
            }
        }

        public int this[T val]
        {
            get
            {
                (bool, T) lookingFor = (false, default(T));
                int idx = 0;
                foreach (var item in backbone)
                {
                    if (item.Item2.Equals(val))
                        lookingFor = item;
                    idx++;
                }

                if (lookingFor.Item1)
                    return idx;

                throw new IndexOutOfRangeException($"Value {val} does not exist in list");
            }
        }

        public int Count { get => backbone.Where(p => p.Item1).Count(); }

        public int Add(T val)
        {
            nextFreeSlot = GetNextFreeSlotIndex();
            backbone[nextFreeSlot] = (true, val);
            return nextFreeSlot;
        }

        public void Remove(T val)
        {
            var toRemove = backbone.FirstOrDefault(p => p.Item2.Equals(val));
            if (!toRemove.Item1)
                return;

            var removedAtIndex = backbone.IndexOf(toRemove);
            if (removedAtIndex > -1)
            {
                backbone[removedAtIndex] = (false, default(T));
                nextFreeSlot = GetNextFreeSlotIndex();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in backbone)
            {
                if (item.Item1)
                    yield return item.Item2;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetNextFreeSlotIndex()
        {
            for(var idx = 0; idx < backbone.Capacity; ++idx)
            {
                if (!backbone[idx].Item1)
                    return idx;
            }

            return nextFreeSlot;
        }
    }
}
