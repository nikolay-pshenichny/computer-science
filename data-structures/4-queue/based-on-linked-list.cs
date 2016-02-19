#define DEBUG
namespace Queues
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Queue (LinkedList based). Begin Tests.");

            // Test 1
            var queue1 = new Queue<string>();
            System.Diagnostics.Debug.Assert(queue1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == string.Empty);

            // Test 1
            queue1.Enqueue("item-1");
            System.Diagnostics.Debug.Assert(queue1.Count == 1);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-1");

            // Test 2
            queue1.Enqueue("item-2");
            System.Diagnostics.Debug.Assert(queue1.Count == 2);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-1; item-2");

            // Test 3
            queue1.Enqueue("item-3");
            System.Diagnostics.Debug.Assert(queue1.Count == 3);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-1; item-2; item-3");

            // Test 4
            var dequeuedItem1 = queue1.Dequeue();
            System.Diagnostics.Debug.Assert(dequeuedItem1 == "item-1");
            System.Diagnostics.Debug.Assert(queue1.Count == 2);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-2");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-2; item-3");

            // Test 5
            queue1.Clear();
            System.Diagnostics.Debug.Assert(queue1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == string.Empty);

            System.Console.WriteLine("Queue (LinkedList based). End Tests.");
        }
    }

    public class Queue<TValueType>
    {
        private readonly System.Collections.Generic.LinkedList<TValueType> _internalLinkedList;

        public Queue()
        {
            _internalLinkedList = new System.Collections.Generic.LinkedList<TValueType>();
        }

        public int Count { get { return _internalLinkedList.Count; } }

        public void Clear()
        {
            _internalLinkedList.Clear();
        }

        public TValueType Peek()
        {
            if (_internalLinkedList.Count == 0)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }

            return _internalLinkedList.First.Value;
        }

        public TValueType Dequeue()
        {
            if (_internalLinkedList.Count == 0)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }

            var value = _internalLinkedList.First.Value;
            _internalLinkedList.RemoveFirst();
            return value;
        }

        public void Enqueue(TValueType value)
        {
            _internalLinkedList.AddLast(value);
        }

        public System.Collections.Generic.IEnumerable<TValueType> GetEnumerable()
        {
            foreach (var item in _internalLinkedList)
            {
                yield return item;
            }
        }
    }
}