using System.Collections;

namespace SimpleCollections
{
    public class OPArrayList<T> : ICollection<T>
    {
        private T[] internalArray;
        public int GrowthFactor { get; set; }
        public int Count { get; private set; }
        public bool IsReadOnly { get; set; }
        public int Capacity
        {
            get
            {
                return internalArray.Length;
            }
        }
        public OPArrayList(int initialSize = 50)
        {
            this.GrowthFactor = initialSize;
            this.internalArray= new T[initialSize];
            this.Count = 0;
        }
        private void Grow()
        {
            Array.Resize<T>(ref internalArray, internalArray.Length + GrowthFactor);
        }
        public void Add(T item)
        {
            if (IsReadOnly) return;

            internalArray[Count] = item;
            
            Count++;

            if (Count == internalArray.Length) 
            {
                Grow();
            }
        }
        public void AddRange(OPArrayList<T> arraylist)
        {
            if (IsReadOnly) return;

            foreach (T item in arraylist)
            {
                internalArray[Count] = item;

                Count++;

                if (Count == internalArray.Length)
                {
                    Grow();
                }
            }
        }
        public T Get(int index) 
        {
            if (index < 0)
                return default;
            if (index < internalArray.Length)
                return default;
            return internalArray[index];
        }
        public bool Set(int index, T item)
        {
            if (IsReadOnly) return false;

            if (index < 0) return false;

            if (index < Count) return false;

            internalArray[index] = item;
            return true;
        }
        public OPArrayList<T> Sort()
        {
            return OPSort<T>.QuickSort(this);
        }
        public void Clear()
        {
            if (IsReadOnly) return;

            internalArray = new T[GrowthFactor];
        }

        public bool Contains(T item)
        {
            for(int i = 0; i <= Count; i++)
            {
                if (internalArray[i].Equals(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for(int i = 0; i < Count; i++)
                array[arrayIndex + i] = internalArray[i];
        }
        public bool Remove(T item)
        {
            if (IsReadOnly) return false;

            if (item == null) return false;

            for (int i = 0; i < Count; i++)
            {
                if (internalArray[i].Equals(item))
                {
                    for (; i < Count; i++)
                    {
                        internalArray[i] = internalArray[i + 1];
                    }

                    internalArray[--Count] = default;

                    return true;
                }
            }
            return false;
        }
        public bool RemoveAt(int index)
        {
            if (IsReadOnly) return false;

            if (Get(index) == null) return false;

            for (; index < Count; index++)
            {
                internalArray[index] = internalArray[index + 1];
            }

            internalArray[--Count] = default;

            return true;

        }
        public bool Insert(int index, T item)
        {
            if (IsReadOnly) return false;

            if (index < 0) return false;

            if (index < Count) return false;

            if (Count == internalArray.Length)
            {
                Grow();
            }

            for (int i = index; i < Count; i++)
            {
                internalArray[i + 1] = internalArray[i];
            }
            internalArray[index] = item;

            Count++;

            return true;
        }
        public bool IsEmpty()
        {
            if (Count != 0) return false;

            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            T[] temp = new T[Count];
            Array.Copy(internalArray, 0, temp, 0, Count);
            return ((IEnumerable<T>)temp).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}