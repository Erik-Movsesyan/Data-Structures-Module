using System;
using Tasks.DoNotChange;
using Tasks.DoublyLinkedList;

namespace Tasks.HybridFlowProcessor
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> _storage = [];

        public T Dequeue()
        {
            ValidateStorageNotEmpty();

            return _storage.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            _storage.AddAt(_storage.Length, item);
        }

        public T Pop()
        {
            ValidateStorageNotEmpty();
            
            return _storage.RemoveAt(_storage.Length - 1);
        }

        public void Push(T item)
        {
            _storage.Add(item);
        }

        private void ValidateStorageNotEmpty()
        {
            if (_storage.Length == 0)
            {
                throw new InvalidOperationException("The HybridFlowProcessor storage is empty");
            }
        }
    }
}
