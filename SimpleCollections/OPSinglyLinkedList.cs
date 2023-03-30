using System.Collections;

namespace SimpleCollections
{
    public class OPSinglyLinkedList<T> : ICollection<T>
    {
        public int Count { get; set; } = 0;
        public OPNode<T>? Head { get; private set; } = null;
        public bool IsReadOnly { get; set; } = false;

        public void Add(T item)
        {
            if (IsReadOnly) return;

            if (Head != null)
            {
                Head = new OPNode<T>(item, Head);
            }
            else
            {
                Head = new OPNode<T>(item);
            }
            Count++;
        }

        public bool Contains(T item)
        {
            if (Head == null) return false;

            OPNode<T>? next = Head;

            while (next != null)
            {
                if (next.Data == null)
                {
                    if (item == null)
                    {
                        return true;
                    }
                    continue;
                }
                if (!next.Data.Equals(item))
                {
                    next = next.Next;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public bool Remove(T item)
        {
            if (IsReadOnly) return false;

            if(Head == null) return false;

            OPNode<T>? next = Head;
            OPNode<T>? previous = Head;

            while (next != null)
            {
                if (next.Data == null) 
                {
                    if (item == null) 
                    {
                        previous.Next = next.Next;
                        Count--;
                        return true;
                    }
                    continue;
                }
                if (!next.Data.Equals(item))
                {
                    previous = next;
                    next = next.Next;
                }
                else
                {
                    previous.Next = next.Next;
                    Count--;
                    return true;
                }
            }
            return false;
        }
        public void Sort()
        {
            if (Count > 200)
                Head = OPSort<T>.MergeSort(Head);
            else
                Head = OPSort<T>.BubbleSortOptimized(Head, Count);
        }

        public void Clear()
        {
            if (IsReadOnly) return;

            Head = null;
            Count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            OPNode<T>? current = Head;
            int counter = 0;
            while (current != null)
            {
                array[arrayIndex + counter] = current.Data;
                current = current.Next;
                counter++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Head == null) yield break;

            OPNode<T>? current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
