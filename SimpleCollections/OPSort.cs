namespace SimpleCollections
{
    internal class OPSort<T>
    {
        #region MergeSort
        internal static OPNode<T> MergeSort(OPNode<T> front)
        {
            //if front or its next node is null we're done sorting
            if (front == null)
                return default;
            if (front.Next == null)
                return front;

            //get the split node
            OPNode<T> split = Split(front);

            //recursively mergesort the two split link collections
            OPNode<T> left = MergeSort(front);
            OPNode<T> right = MergeSort(split);

            //merge and get new front node
            OPNode<T> sorted = Merge(left, right);

            //return sorted 
            return sorted;
        }
        private static OPNode<T>? Split(OPNode<T> front)
        {
            //tortoise and hare aproach to finding the middle node, where we split; O(log(n))
            OPNode<T> slow = front;
            OPNode<T> fast = front;
            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next!;
                fast = fast.Next.Next!;
            }

            //get the node next to split point
            OPNode<T> split = slow.Next;

            //remove the link between them
            slow.Next = null;

            //return split point
            return split;
        }
        private static OPNode<T> Merge(OPNode<T> left, OPNode<T> right)
        {
            //if any input node is null we know we're done merging
            if (left == null)
                return right;
            if (right == null)
                return left;

            //result builder
            OPNode<T> result;

            //if left is bigger then right/left is smaller then right; build final result; O(n)
            if (Comparer<T>.Default.Compare(left.Data, right.Data) < 0)
            {
                //result is left
                result = left;
                //build next result from lefts next node
                result.Next = Merge(left.Next, right);
            }
            else
            {
                //result is right
                result = right;
                //build next result from rights next node
                result.Next = Merge(left, right.Next);
            }

            //return the result (or sub-result)
            return result;
        }
        #endregion

        #region BubbleSort
        internal static OPNode<T> BubbleSort(OPNode<T> front, int size)
        {
            //current node to check
            OPNode<T> current;

            //foreach element in link collection; O(n)
            for (int j = 0; j < size - 1; j++)
            {
                //set current to the start of the collection
                current = front;
                //foreach element in link collection; O(n^2)
                for (int i = 0; i < size - 1; i++)
                {
                    //if current data is smaller then next data then swap
                    if (Comparer<T>.Default.Compare(current.Data, current.Next.Data) > 0)
                    {
                        //swap data
                        T nextData = current.Next.Data;
                        T currentData = current.Data;
                        current.Data = nextData;
                        current.Next.Data = currentData;                
                    }
                    //set current to next node
                    current = current.Next;
                }
            }
            //return front
            return front;
        }
        internal static OPNode<T> BubbleSortOptimized(OPNode<T> front, int size)
        {
            //current node to check
            OPNode<T> current;
            //flag if a swap has occured
            bool swap = true;
            //counts iterations of while loop
            int counter = 0;

            //while we are stil swapping; O(n)
            while (swap) 
            {
                //set swap flag to false
                swap = false;
                //set current to the start of the collection
                current = front;
                //foreach element in link collection - iteration counter; O(n^2)
                for (int i = 0; i < size - 1 - counter; i++)
                {
                    //if current data is smaller then next data then swap
                    if (Comparer<T>.Default.Compare(current.Data, current.Next.Data) > 0)
                    {
                        //swap data
                        swap = true;
                        T nextData = current.Next.Data;
                        T currentData = current.Data;
                        current.Data = nextData;
                        current.Next.Data = currentData;
                    }
                    //set current to next node
                    current = current.Next;
                }
                //increment iteration counter
                counter++;
            }
            //return front
            return front;
        }
        #endregion

        #region QuickSort
        internal static OPArrayList<T> QuickSort(OPArrayList<T> list)
        {
            
            if (list.Count <= 1)
                return list;

            int pivotPosition = list.Count / 2;
            T pivotValue = list.Get(pivotPosition);

            list.RemoveAt(pivotPosition);

            OPArrayList<T> smaller = new OPArrayList<T>();
            OPArrayList<T> greater = new OPArrayList<T>();

            foreach (T item in list)
            {
                if (Comparer<T>.Default.Compare(pivotValue, item) < 0)
                    smaller.Add(item);
                else
                    greater.Add(item);
            }

            OPArrayList<T> sorted = QuickSort(smaller);
            sorted.Add(pivotValue);
            sorted.AddRange(QuickSort(greater));
            return sorted;
        }
        #endregion
    }
}
