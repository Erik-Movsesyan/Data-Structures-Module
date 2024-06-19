using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks.DoublyLinkedList
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        public DoublyLinkedListNode<T>? First { get; private set; }
        public DoublyLinkedListNode<T>? Last { get; private set; }
        public int Length { get; private set; }

        public DoublyLinkedList() { }

        public DoublyLinkedList(IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);

            foreach (T item in collection)
            {
                Add(item);
            }
        }

        public void Add(T value)
        {
            var newNode = new DoublyLinkedListNode<T>(this, value);
            AddLast(newNode);
        }

        public void AddAt(int index, T value)
        {
            var newNode = new DoublyLinkedListNode<T>(this, value);
            ValidateIndexWithinInsertionRange(index);

            if (index == 0)
            {
                AddFirst(newNode);
            }
            else if (index == Last!.Index + 1)
            {
                AddLast(newNode);
            }
            else
            {
                var nodeToInsert = NodeAt(index);
                AddAfter(nodeToInsert?.Previous, newNode);
            }
        }

        public T? ElementAt(int index)
        {
            ValidateIndexWithinLengthRange(index);
            var resultNode = NodeAt(index);

            return resultNode!.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(this);
        }

        public void Remove(T item)
        {
            var nodeToRemove = NodeWithValue(item);
            if (nodeToRemove == null)
                return;

            RemoveNodeInternal(nodeToRemove);
        }

        public T RemoveAt(int index)
        {
            ValidateIndexWithinLengthRange(index);
            var nodeToRemove = NodeAt(index);

            RemoveNodeInternal(nodeToRemove);
            return nodeToRemove!.Value!;
        }

        private DoublyLinkedListNode<T>? NodeAt(int index)
        {
            var node = First;
            DoublyLinkedListNode<T>? resultNode = null;

            do
            {
                if (node!.Index == index)
                {
                    resultNode = node;
                    break;
                }

                node = node.Next;

            } while (node != null);

            return resultNode;
        }

        private DoublyLinkedListNode<T>? NodeWithValue(T value)
        {
            var node = First;
            var comparer = EqualityComparer<T>.Default;

            if (node != null)
            {
                if (value != null)
                {
                    DoublyLinkedListNode<T>? nextNode;
                    do
                    {
                        if (comparer.Equals(node!.Value, value))
                        {
                            return node;
                        }
                        nextNode = node.Next;
                        node = nextNode;

                    } while (nextNode != null);
                }
                else
                {
                    do
                    {
                        if (node.Value == null)
                        {
                            return node;
                        }
                        node = node.Next;

                    } while (node != null);
                }
            }
            return null;
        }

        private void AddAfter(DoublyLinkedListNode<T>? node, DoublyLinkedListNode<T>? newNode)
        {
            ValidateNode(node);
            ValidateNode(newNode);

            newNode!.Next = node!.Next;
            newNode.Previous = node;
            node.Next!.Previous = newNode;
            node.Next = newNode;
            Length++;
            newNode.Index = node.Index + 1;
            AdjustNodesIndices(newNode.Next!, true);
        }

        private void AddFirst(DoublyLinkedListNode<T> newNode)
        {
            ValidateNode(newNode);

            if (First == null)
            {
                AddToEmptyList(newNode);
            }
            else
            {
                newNode.Next = First;
                First.Previous = newNode;
                First = newNode;
                Length++;
                newNode.Index = 0;
                AdjustNodesIndices(First.Next, true);
            }
        }

        private void AddLast(DoublyLinkedListNode<T> newNode)
        {
            ValidateNode(newNode);

            if (First == null)
            {
                AddToEmptyList(newNode);
            }
            else
            {
                newNode.Previous = Last;
                Last!.Next = newNode;
                Last = newNode;
                Length++;
                newNode.Index = Length - 1;
            }
        }

        private void AddToEmptyList(DoublyLinkedListNode<T> newNode)
        {
            First = newNode;
            Last = newNode;
            newNode.Index = 0;
            Length++;
        }

        private void RemoveNodeInternal(DoublyLinkedListNode<T>? nodeToRemove)
        {
            if (nodeToRemove!.Previous != null)
            {
                nodeToRemove.Previous.Next = nodeToRemove.Next;

                if (nodeToRemove == Last)
                {
                    Last = nodeToRemove.Previous;
                }
                else
                {
                    nodeToRemove.Next!.Previous = nodeToRemove.Previous;
                    AdjustNodesIndices(nodeToRemove.Next, increment: false);
                }
            }
            else
            {
                if (nodeToRemove.Next != null)
                {
                    nodeToRemove.Next.Previous = null;
                    First = nodeToRemove.Next;
                    AdjustNodesIndices(nodeToRemove.Next, increment: false);
                }
                else
                {
                    First = null;
                    Last = null;
                }
            }

            nodeToRemove.Invalidate();
            Length--;
        }

        private void ValidateIndexWithinInsertionRange(int index)
        {
            if (index < 0 || index > Length)
            {
                var message = $"{(index < 0
                    ? "The insertion index must be more than or equal to 0"
                    : $"Node can not be inserted at index {index} since the length of the list is {Length}")}";

                throw new IndexOutOfRangeException($"{message}");
            }
        }

        private void ValidateIndexWithinLengthRange(int index)
        {
            if (index < 0 || index >= Length)
            {
                var message = $"{(index < 0
                    ? "The index has to be more than or equal to 0"
                        : Length == 0 ? "The linked list is empty is empty"
                    : $"value of the node can not be accessed at node index {index} since the length of the list is {Length}")}";

                throw new IndexOutOfRangeException($"{message}");
            }
        }

        private void ValidateNode(DoublyLinkedListNode<T>? node)
        {
            ArgumentNullException.ThrowIfNull(node);

            if (node.List != this)
                throw new InvalidOperationException("The node does not belong to the current DoublyLinkedList");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static void AdjustNodesIndices(DoublyLinkedListNode<T> startingNode, bool increment)
        {
            var currentNode = startingNode;
            do
            {
                if (increment)
                    currentNode.Index++;
                else
                    currentNode.Index--;

                currentNode = currentNode.Next;

            } while (currentNode != null);
        }
    }
}
