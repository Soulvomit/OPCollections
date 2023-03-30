using System.Collections;

namespace SimpleCollections
{
    public class OPQueue<T> : IReadOnlyCollection<T>, ICollection
    {
        public int Count { get; private set; }
        public bool IsSynchronized { get; set; } 
        public object SyncRoot { get; private set; }
        public OPNode<T>? Front { get; private set; }
        public OPNode<T>? Rear { get; private set; }

        public OPQueue()
        {
            Count = 0;
            Front = null;
            Rear = null;
            SyncRoot = new();
            IsSynchronized = false;
        }

        public void Enqueue(T data)
        {
            OPNode<T> newNode = new(data);
            //if front is not null
            if (Front != null)
            {
                //move new node to buttom of queue
                Rear!.Next = newNode;
                //set buttom reference to point to the new node
                Rear = Rear.Next;
            }
            else
            {
                //move the new node to front and rear
                Front = Rear = newNode;
            }
            Count++;
        }
        public T? Dequeue()
        {
            //if front is null
            if (Front == null)
            {
                //return default data
                return default;
            }
            //store dequeued data for return
            T? dequeuedData = Front.Data;
            //move top pointer to next node in queue
            Front = Front.Next;
            //decrement counter by 1
            Count--;
            //return dequeued data
            return dequeuedData;
        }
        public T? Peek()
        {
            //if front is null
            if (Front == null)
            {
                //return default data
                return default;
            }
            //return peeked data
            return Front.Data;
        }

        public void Clear()
        {
            //clear front, rear and count; let the grabage collector do the rest
            Front = null;
            Rear = null;
            Count = 0;
        }
        public void Sort()
        {
            if(Count > 200)
                Front = OPSort<T>.MergeSort(Front);
            else
                Front = OPSort<T>.BubbleSortOptimized(Front, Count);

            //find rear node
            OPNode<T> current = Front;
            while(current.Next != null)
            {
                current = current.Next;
            }
            Rear = current;
        }

        public void CopyTo(Array array, int index)
        {
            //if front is null
            if (Front == null)
            {
                //return
                return;
            }
            OPNode<T> current = Front;
            for(int i = 0; i < Count; i++)
            {
                array.SetValue(current!.Data, index + i);
                current = current.Next;
            }
        }

        public IEnumerator GetEnumerator()
        {
            //gets enumerator for foreach iteration usability
            if (Front == null) yield break;

            OPNode<T>? current = Front;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            //gets enumerator for foreach iteration usability
            if (Front == null) yield break;

            OPNode<T>? current = Front;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}