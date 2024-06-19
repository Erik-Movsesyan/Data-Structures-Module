using Tasks.DoublyLinkedList;

namespace ListDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list1 = new DoublyLinkedList<string>();
            list1.AddAt(0, "a0");
            list1.Add("a");
            list1.Add("b");
            list1.Add("a0");
            list1.Add("c");


            var list2 = new DoublyLinkedList<string>();
            list2.Add("a");
            list2.Add("b");
            list2.Add("c");
            //list2.Remove("a");
            list2.RemoveAt(3);


        }
    }
}
