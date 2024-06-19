using System.Collections.Generic;

namespace Tasks.DoNotChange
{
    public interface IDoublyLinkedList<T> : IEnumerable<T>
    {
        public int Length { get; }

        void Add(T value);

        void AddAt(int index, T value);

        void Remove(T item);

        T RemoveAt(int index);

        T? ElementAt(int index);
    }
}
