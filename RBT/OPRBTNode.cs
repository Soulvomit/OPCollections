namespace RBT
{
    public class OPRBTNode<T>
    {
        public T? Data { get; set; }
        public bool Red { get; set; }
        public OPRBTNode<T> Parent { get; set; }
        public OPRBTNode<T> LeftChild { get; set; }
        public OPRBTNode<T> RightChild { get; set; }

        public OPRBTNode(T? data, OPRBTNode<T>? parent = null, 
            OPRBTNode<T>? leftChild = null, OPRBTNode<T>? rightChild = null)
        {
            this.Data = data;
            this.Parent = parent;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            this.Red = true;
        }
    }
}