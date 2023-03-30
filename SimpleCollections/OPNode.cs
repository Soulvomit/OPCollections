namespace SimpleCollections
{
    public class OPNode<T>
    {

        public T? Data { get; set; }
        public OPNode<T>? Next { get; set; }

        public OPNode(T? data, OPNode<T>? next = null) 
        { 
            this.Data = data;
            this.Next = next;
        }
    }
}