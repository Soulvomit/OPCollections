namespace BST
{
    public class OPBSTNode<T>
    {
        public T? Data { get; set; }
        public OPBSTNode<T>? LeftChild { get; set; }
        public OPBSTNode<T>? RightChild { get; set; }

        public OPBSTNode(T? data, OPBSTNode<T>? leftChild = null, 
            OPBSTNode<T>? rightChild = null)
        {
            this.Data = data;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }
    }
}
