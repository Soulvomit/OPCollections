namespace RBT
{
    public class OPRedBlackTree<T> where T : IComparable<T>
    {
        //property to store and access the root node of the tree
        public OPRBTNode<T> Root { get; private set; }

        //inserts a new node with the specified data into the tree
        public void Insert(T data)
        {
            //if the tree is empty, create a new node as the root and set its color to black
            if (Root == null)
            {
                Root = new OPRBTNode<T>(data);
                Root.Red = false;
            }
            //if the tree is not empty, call the InsertNode method to insert the new node
            else InsertNode(Root, data);
        }
        //recursively inserts a new node with the specified data into the tree
        private void InsertNode(OPRBTNode<T> node, T data)
        {
            //compare the new data with the current node's data
            int comparisonResult = data.CompareTo(node.Data);

            //if the new data is less than the current node's data
            if (comparisonResult < 0)
            {
                //if there is no left child, insert the new node as the left child and fix the tree
                if (node.LeftChild == null)
                {
                    node.LeftChild = new OPRBTNode<T>(data, node);
                    FixTreeAfterInsertion(node.LeftChild);
                }
                //if there is a left child, continue the insertion process with the left child
                else InsertNode(node.LeftChild, data);
            }
            //if the new data is greater than the current node's data
            else if (comparisonResult > 0)
            {
                //if there is no right child, insert the new node as the right child and fix the tree
                if (node.RightChild == null)
                {
                    node.RightChild = new OPRBTNode<T>(data, node);
                    FixTreeAfterInsertion(node.RightChild);
                }
                //if there is a right child, continue the insertion process with the right child
                else InsertNode(node.RightChild, data);
            }
            //if the new data is equal to the current node's data, do nothing (duplicate data is not inserted)
        }

        //fixes the Red-Black Tree properties after inserting a new node
        private void FixTreeAfterInsertion(OPRBTNode<T> node)
        {
            //loop while the current node is not null, not the root, and its parent is red
            while (node != null && node != Root && node.Parent.Red)
            {
                //if the parent of the current node is the left child of its grandparent
                if (node.Parent == node.Parent.Parent.LeftChild)
                {
                    //define the uncle as the right child of the grandparent
                    OPRBTNode<T> uncle = node.Parent.Parent.RightChild;
                    //if the uncle exists and is red
                    if (uncle != null && uncle.Red)
                    {
                        //recolor the parent, uncle, and grandparent nodes
                        node.Parent.Red = false;
                        uncle.Red = false;
                        node.Parent.Parent.Red = true;
                        //move the current node to the grandparent node
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        //if the current node is the right child of its parent, rotate left around the parent
                        if (node == node.Parent.RightChild)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }
                        //recolor the parent and grandparent nodes, and rotate right around the grandparent
                        node.Parent.Red = false;
                        node.Parent.Parent.Red = true;
                        RotateRight(node.Parent.Parent);
                    }
                }
                //if the parent of the current node is the right child of its grandparent
                else
                {
                    //define the uncle as the left child of the grandparent
                    OPRBTNode<T> uncle = node.Parent.Parent.LeftChild;
                    //if the uncle exists and is red
                    if (uncle != null && uncle.Red)
                    {
                        //recolor the parent, uncle, and grandparent nodes
                        node.Parent.Red = false;
                        uncle.Red = false;
                        node.Parent.Parent.Red = true;
                        //move the current node to the grandparent node
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        //if the current node is the left child of its parent, rotate right around the parent
                        if (node == node.Parent.LeftChild)
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }
                        //recolor the parent and grandparent nodes, and rotate left around the grandparent
                        node.Parent.Red = false;
                        node.Parent.Parent.Red = true;
                        RotateLeft(node.Parent.Parent);
                    }
                }
            }
            //ensure the root node is always black
            Root.Red = false;
        }

        //performs a left rotation around the specified node
        private void RotateLeft(OPRBTNode<T> node)
        {
            //store the right child of the current node
            OPRBTNode<T> rightChild = node.RightChild;
            //set the right child of the current node to the left child of the stored right child
            node.RightChild = rightChild.LeftChild;

            //if the left child of the stored right child exists, set its parent to the current node
            if (rightChild.LeftChild != null)
                rightChild.LeftChild.Parent = node;

            //set the parent of the stored right child to the parent of the current node
            rightChild.Parent = node.Parent;

            //if the current node is the root, update the root to the stored right child
            if (node.Parent == null)
                Root = rightChild;
            //if the current node is the left child of its parent, set the left child of the parent to the stored right child
            else if (node == node.Parent.LeftChild)
                node.Parent.LeftChild = rightChild;
            //if the current node is the right child of its parent, set the right child of the parent to the stored right child
            else node.Parent.RightChild = rightChild;

            //set the left child of the stored right child to the current node
            rightChild.LeftChild = node;
            //set the parent of the current node to the stored right child
            node.Parent = rightChild;
        }

        //performs a right rotation around the specified node
        private void RotateRight(OPRBTNode<T> node)
        {
            //store the left child of the current node
            OPRBTNode<T> leftChild = node.LeftChild;
            //set the left child of the current node to the right child of the stored left child
            node.LeftChild = leftChild.RightChild;

            //if the right child of the stored left child exists, set its parent to the current node
            if (leftChild.RightChild != null)
                leftChild.RightChild.Parent = node;

            //set the parent of the stored left child to the parent of the current node
            leftChild.Parent = node.Parent;

            //if the current node is the root, update the root to the stored left child
            if (node.Parent == null)
                Root = leftChild;
            //if the current node is the right child of its parent, set the right child of the parent to the stored left child
            else if (node == node.Parent.RightChild)
                node.Parent.RightChild = leftChild;
            //if the current node is the left child of its parent, set the left child of the parent to the stored left child
            else node.Parent.LeftChild = leftChild;

            //set the right child of the stored left child to the current node
            leftChild.RightChild = node;
            //set the parent of the current node to the stored left child
            node.Parent = leftChild;
        }

    }
}

