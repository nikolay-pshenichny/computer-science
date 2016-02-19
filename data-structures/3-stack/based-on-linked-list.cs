#define DEBUG
namespace Stacks
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Stack (LinkedList based). Begin Tests.");

            // Test 1
            var stack1 = new Stack<string>();
            System.Diagnostics.Debug.Assert(stack1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", stack1.GetEnumerable()) == string.Empty);

            // Test 1
            stack1.Push("item-1");
            System.Diagnostics.Debug.Assert(stack1.Count == 1);
            System.Diagnostics.Debug.Assert(stack1.Peek() == "item-1");
            System.Diagnostics.Debug.Assert(string.Join("; ", stack1.GetEnumerable()) == "item-1");

            // Test 2
            stack1.Push("item-2");
            System.Diagnostics.Debug.Assert(stack1.Count == 2);
            System.Diagnostics.Debug.Assert(stack1.Peek() == "item-2");
            System.Diagnostics.Debug.Assert(string.Join("; ", stack1.GetEnumerable()) == "item-2; item-1");

            // Test 3
            stack1.Push("item-3");
            System.Diagnostics.Debug.Assert(stack1.Count == 3);
            System.Diagnostics.Debug.Assert(stack1.Peek() == "item-3");
            System.Diagnostics.Debug.Assert(string.Join("; ", stack1.GetEnumerable()) == "item-3; item-2; item-1");

            // Test 4
            var poppedItem1 = stack1.Pop();
            System.Diagnostics.Debug.Assert(poppedItem1 == "item-3");
            System.Diagnostics.Debug.Assert(stack1.Count == 2);
            System.Diagnostics.Debug.Assert(stack1.Peek() == "item-2");
            System.Diagnostics.Debug.Assert(string.Join("; ", stack1.GetEnumerable()) == "item-2; item-1");

            // Test 5
            stack1.Clear();
            System.Diagnostics.Debug.Assert(stack1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", stack1.GetEnumerable()) == string.Empty);

            System.Console.WriteLine("Stack (LinkedList based). End Tests.");
        }
    }

    public class Stack<TValueType>
    {
        private readonly System.Collections.Generic.LinkedList<TValueType> _internalLinkedList;

        public Stack()
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
                throw new System.InvalidOperationException("Stack is empty.");
            }

            return _internalLinkedList.First.Value;
        }

        public TValueType Pop()
        {
            if (_internalLinkedList.Count == 0)
            {
                throw new System.InvalidOperationException("Stack is empty.");
            }

            var value = _internalLinkedList.First.Value;
            _internalLinkedList.RemoveFirst();
            return value;
        }

        public void Push(TValueType value)
        {
            _internalLinkedList.AddFirst(value);
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