#define DEBUG
namespace Stacks
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Stack (Array based). Begin Tests.");

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

            System.Console.WriteLine("Stack (Array based). End Tests.");
        }
    }

    public class Stack<TValueType>
    {
        private const int InternalArrayExpansionRate = 32;
        private TValueType[] _internalArray;
        private int _size;

        public Stack()
        {
            _internalArray = new TValueType[0]; // Empty array
            _size = 0;
        }

        public int Count { get { return _size; } }

        public void Clear()
        {
            _internalArray = new TValueType[0];
            _size = 0;
        }

        public TValueType Peek()
        {
            if (_size == 0)
            {
                throw new System.InvalidOperationException("Stack is empty.");
            }

            return _internalArray[_size - 1];
        }

        public TValueType Pop()
        {
            if (_size == 0)
            {
                throw new System.InvalidOperationException("Stack is empty.");
            }

            var value = _internalArray[_size - 1];
            _internalArray[_size - 1] = default(TValueType);
            _size--;
            return value;
        }

        public void Push(TValueType value)
        {
            // Expand the internal array, if we can't fit elements anymore
            if (_internalArray.Length == _size)
            {
                // Expand the internal array in order to be able to fit more elements
                // Note: This can be optimized in order to avoid frequent array copying.
                var expandedArray = new TValueType[_internalArray.Length + InternalArrayExpansionRate];
                _internalArray.CopyTo(expandedArray, 0);
                _internalArray = expandedArray;
            }

            _internalArray[_size] = value;
            _size++;
        }

        public System.Collections.Generic.IEnumerable<TValueType> GetEnumerable()
        {
            for (int index = _size - 1; index >= 0; index--)
            {
                yield return _internalArray[index];
            }
        }
    }
}