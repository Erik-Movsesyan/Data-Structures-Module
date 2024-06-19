namespace Tasks.DoublyLinkedList
{
    public sealed class DoublyLinkedListNode<T>
    {
        public DoublyLinkedList<T>? List { get; internal set; }
        public DoublyLinkedListNode<T>? Next { get; internal set; }
        public DoublyLinkedListNode<T>? Previous { get; internal set; }
        public int? Index { get; internal set; }
        public T? Value { get; }

        internal DoublyLinkedListNode(DoublyLinkedList<T> list, T? value)
        {
            List = list;
            Value = value;
        }

        public DoublyLinkedListNode(T? value)
        {
            Value = value;
        }

        public void Invalidate()
        {
            List = null;
            Next = null;
            Previous = null;
            Index = null;
        }
    }
}
