namespace BST
{
    public class OPBinarySearchTree<T>
    {
        public OPBSTNode<T>? Root { get; private set; }
        public int Count { get; private set; }
        public OPBinarySearchTree(T? rootValue = default) 
        {
            if (rootValue == null)
            {
                Root = null;
                Count = 0;
            }
            else
            {
                Root = new OPBSTNode<T>(rootValue);
                Count = 1;
            }
        }

        #region Insert
        //inserts a new value into the binary search tree
        public void Insert(T value)
        {
            //if the tree is empty
            if (Root == null)
                //set the root to a new node with the given value
                Root = new OPBSTNode<T>(value);
            else
                //call the private Insert method with the root node and the value to insert
                Insert(Root, value);
            //increment the count of nodes in the tree
            Count++;
        }

        //inserts a new value into the binary search tree, starting from a given node
        private void Insert(OPBSTNode<T> current, T value)
        {
            //if the value is smaller than the current node's data
            if (Comparer<T>.Default.Compare(value, current.Data) < 0)
            {
                //if the left child does not exist
                if (current.LeftChild == null)
                    //insert the value as the left child
                    current.LeftChild = new OPBSTNode<T>(value);
                //if the left child exists
                else
                    //recursively call insert method with the left child and the value
                    Insert(current.LeftChild, value);
            }
            //if the value is greater than or equal to the current node's data
            else
            {
                //if the right child does not exist
                if (current.RightChild == null)
                    //insert the value as the right child
                    current.RightChild = new OPBSTNode<T>(value);
                //if the right child exists
                else
                    //recursively call insert method with the right child and the value
                    Insert(current.RightChild, value);
            }
        }
        #endregion

        #region Find
        public bool Find(T value)
        {
            return Find(Root, value);
        }
        private bool Find(OPBSTNode<T> current, T value)
        {
            if (current == null)
                return false;
            else
            {
                if (Comparer<T>.Default.Compare(value, current.Data) == 0)
                        return true;
                else
                {
                    if (Comparer<T>.Default.Compare(value, current.Data) < 0)
                        return Find(current.LeftChild, value);
                    else
                        return Find(current.RightChild, value);
                }
            }
        }
        public OPBSTNode<T> FindNode(T value)
        {
            return FindNode(Root, value);
        }
        private OPBSTNode<T> FindNode(OPBSTNode<T> current, T value)
        {
            if (current == null)
                return null;
            else
            {
                if (Comparer<T>.Default.Compare(value, current.Data) == 0)
                    return current;
                else
                {
                    if (Comparer<T>.Default.Compare(value, current.Data) < 0)
                        return FindNode(current.LeftChild, value);
                    else
                        return FindNode(current.RightChild, value);
                }
            }
        }
        public OPBSTNode<T> FindParent(OPBSTNode<T> child)
        {
            return FindParent(Root, null, child);
        }
        private OPBSTNode<T> FindParent(OPBSTNode<T> current, OPBSTNode<T> parent, OPBSTNode<T> child)
        {
            if (current == null)
                return null;
            else
            {
                if (Comparer<T>.Default.Compare(child.Data, current.Data) == 0)
                    return parent;
                else
                {
                    if (Comparer<T>.Default.Compare(child.Data, current.Data) < 0)
                        return FindParent(current.LeftChild, current, child);
                    else
                        return FindParent(current.RightChild, current, child);
                }
            }
        }
        #endregion

        #region Delete
        public int Delete(T value)
        {
            //case 0 - no root
            if(Root == null) return 0;
            //find deletion node
            OPBSTNode<T> delNode = FindNode(value);
            //case 1 - node does not exist
            if(delNode == null) return 1;
            //find the parent node
            OPBSTNode<T> parent = FindParent(delNode);
            //case 2 - no children (leaf)
            if (delNode.LeftChild == null && delNode.RightChild == null)
            {
                //find out if deletion node is left or right of parent
                if (parent.LeftChild == delNode)
                    parent.LeftChild = null;
                else
                    parent.RightChild = null;
                //return
                return 2;
            }
            //case 3 - one child
            if ((delNode.LeftChild != null && delNode.RightChild == null) ||
                (delNode.RightChild != null && delNode.LeftChild == null))
            {
                //find out if deletion node is left or right of parent
                if (parent.LeftChild == delNode)
                {
                    //if deletion node left child is null
                    if (delNode.LeftChild == null)
                        //set parent left child to deletion nodes right child
                        parent.LeftChild = delNode.RightChild;
                    else
                        //set parent left child to deletion nodes left child
                        parent.LeftChild = delNode.LeftChild;
                }
                else
                {
                    //if deletion node left child is null
                    if (delNode.LeftChild == null)
                        //set parent right child to deletion nodes right child
                        parent.RightChild = delNode.RightChild;
                    else
                        //set parent right child to deletion nodes left child
                        parent.RightChild = delNode.LeftChild;
                }
                //return
                return 3;
            }
            //case 4 - two children
            if (delNode.LeftChild != null && delNode.RightChild != null)
            {
                //find the in order successor
                OPBSTNode<T> inOrderSuccessor = Min(delNode);
                //find out if deletion node is left or right of parent
                if (parent.LeftChild == delNode)
                    //set parent left to in order successor
                    parent.LeftChild = inOrderSuccessor;
                else
                    //set parent right to in order successor
                    parent.RightChild = inOrderSuccessor;
                //set in order successor children to deletion node children 
                inOrderSuccessor.LeftChild = delNode.LeftChild;
                inOrderSuccessor.RightChild = delNode.RightChild;
                //return
                return 4;
            }
            //return; something is wrong!
            return -1;
        }
        #endregion

        #region Min/Max
        public OPBSTNode<T> Min(OPBSTNode<T> current)
        {
            if (current.LeftChild != null)
                return Min(current.LeftChild);

            return current;
        }
        public OPBSTNode<T> Max(OPBSTNode<T> current)
        {
            if (current.RightChild != null)
                return Max(current.RightChild);

            return current;
        }
        #endregion

        #region Traversal
        public void PreOrder(OPBSTNode<T> current, ref List<T> preOrderList)
        {
            preOrderList.Add(current.Data);

            if(current.LeftChild != null)
                PreOrder(current.LeftChild, ref preOrderList);

            if(current.RightChild != null)
                PreOrder(current.RightChild, ref preOrderList);
        }
        public void InOrder(OPBSTNode<T> current, ref List<T> inOrderList)
        {
            if(current.LeftChild != null)
                InOrder(current.LeftChild, ref inOrderList);

            inOrderList.Add(current.Data);

            if(current.RightChild != null)
                InOrder(current.RightChild, ref inOrderList);
        }
        public void InOrderDescending(OPBSTNode<T> current, ref List<T> inOrderList)
        {
            if(current.RightChild != null)
                InOrderDescending(current.RightChild, ref inOrderList);

            inOrderList.Add(current.Data);

            if(current.LeftChild != null)
                InOrderDescending(current.LeftChild, ref inOrderList);
        }
        public void PostOrder(OPBSTNode<T> current, ref List<T> postOrderList)
        {
            if(current.LeftChild != null)
                PostOrder(current.LeftChild, ref postOrderList);

            if(current.RightChild != null)
                PostOrder(current.RightChild, ref postOrderList);

            postOrderList.Add(current.Data);
        }
        #endregion
    }
}