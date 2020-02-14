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

        int Contains(T val);
        bool Contains(int idx, out T val);
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
                capacity = size;
                backbone = InitBackbone(size);// new List<(bool, T)>(size);
            }
            else
            {
                capacity = 30;
                backbone = InitBackbone(30); // new List<(bool, T)>(30);
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
                    if (item.Item1 && item.Item2.Equals(val))
                    {
                        lookingFor = item;
                        return idx;
                    }

                    idx++;
                }

                throw new IndexOutOfRangeException($"Value {val} does not exist in list");
            }
        }

        public int Count { get => backbone.Where(p => p.Item1).Count(); }

        public int Add(T val)
        {
            nextFreeSlot = GetNextFreeSlotIndex();
            if (nextFreeSlot == -1)
            {
                IncriseCapacity();
                nextFreeSlot = GetNextFreeSlotIndex();
            }

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

        public int Contains(T val)
        {
            var idx = 0;
            foreach (var item in backbone)
            {
                if (item.Item1 && item.Item2.Equals(val))
                    return idx;

                idx++;
            }

            return -1;
        }

        public bool Contains(int idx, out T val)
        {
            val = default(T);
            if (idx >= capacity)
                return false;

            if (backbone[idx].Item1)
            {
                val = backbone[idx].Item2;
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in backbone.Where(p => p.Item1))
                yield return item.Item2;
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

            return -1;
        }

        private void IncriseCapacity()
        {
            capacity *= 2;

            var newBackbone = new List<(bool, T)>(capacity);
            for (int i = 0; i < backbone.Count(); i++)
                newBackbone.Add(backbone[i]);

            for (int i = backbone.Count(); i < capacity; i++)
                newBackbone.Add((false, default(T)));

            backbone = newBackbone;
        }

        private List<(bool, T)> InitBackbone(int _capacity)
        {
            var newBackbone = new List<(bool, T)>(_capacity);

            for (int i = 0; i < capacity; i++)
                newBackbone.Add((false, default(T)));

            return newBackbone;
        }
    }
}
