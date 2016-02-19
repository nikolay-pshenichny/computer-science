#define DEBUG
namespace Queues
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("PriorityQueue (LinkedList based). Begin Tests.");

            // Test 1
            var queue1 = new PriorityQueue<string>();
            System.Diagnostics.Debug.Assert(queue1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == string.Empty);

            // Test 1
            queue1.Enqueue("item-1", Priority.Normal);
            System.Diagnostics.Debug.Assert(queue1.Count == 1);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-1");

            // Test 2
            queue1.Enqueue("item-2", Priority.Normal);
            System.Diagnostics.Debug.Assert(queue1.Count == 2);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-1; item-2");

            // Test 3
            queue1.Enqueue("item-1-high", Priority.High);
            System.Diagnostics.Debug.Assert(queue1.Count == 3);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-1-high");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-1-high; item-1; item-2");

            // Test 4
            queue1.Enqueue("item-2-high", Priority.High);
            System.Diagnostics.Debug.Assert(queue1.Count == 4);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-1-high");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-1-high; item-2-high; item-1; item-2");
            
            // Test 4
            queue1.Enqueue("item-3", Priority.Normal);
            System.Diagnostics.Debug.Assert(queue1.Count == 5);
            System.Diagnostics.Debug.Assert(queue1.Peek() == "item-1-high");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-1-high; item-2-high; item-1; item-2; item-3");

            System.Console.WriteLine("PriorityQueue (LinkedList based). End Tests.");
        }
    }

    public class PriorityQueue<TValueType>
    {
        private readonly System.Collections.Generic.LinkedList<QueueElement<TValueType>> _internalLinkedList;

        public PriorityQueue()
        {
            _internalLinkedList = new System.Collections.Generic.LinkedList<QueueElement<TValueType>>();
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

            return _internalLinkedList.First.Value.Value;
        }

        public TValueType Dequeue()
        {
            if (_internalLinkedList.Count == 0)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }

            var value = _internalLinkedList.First.Value.Value;
            _internalLinkedList.RemoveFirst();
            return value;
        }

        public void Enqueue(TValueType value, Priority priority)
        {
            var item = new QueueElement<TValueType> { Value = value, Priority = priority };

            if (_internalLinkedList.Count == 0)
            {
                _internalLinkedList.AddFirst(item);
            }
            else
            {
                // Enqueue, grouped by priority.
                var current = _internalLinkedList.Last;
                while((current != null) && (current.Value.Priority < priority))
                {
                    current = current.Previous;
                }

                if (current == null)
                {
                    // If we reached the beginning of the queue, that means, that there are no elements
                    // with the requested priority. ie. our item has a higher priority. let's add it first
                    _internalLinkedList.AddFirst(item);
                }
                else
                {
                    // Otherwise, we found an element with the same priority as we have in our new Item.
                    // Therefore, we need to add our item right after it
                    _internalLinkedList.AddAfter(current, item);
                }
            }
        }

        public System.Collections.Generic.IEnumerable<TValueType> GetEnumerable()
        {
            foreach (var item in _internalLinkedList)
            {
                yield return item.Value;
            }
        }
    }

    public class QueueElement<TValueType>
    {
        public TValueType Value { get; set; }
        public Priority Priority { get; set; }
    }

    public enum Priority
    {
        Normal = 0,
        High = 1
    }
}