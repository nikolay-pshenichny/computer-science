#define DEBUG
namespace Queues
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Queue (Array based). Begin Tests.");

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
            dequeuedItem1 = queue1.Dequeue();
            System.Diagnostics.Debug.Assert(dequeuedItem1 == "item-2");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-3");

            // Test 6
            queue1.Enqueue("item-4");
            queue1.Enqueue("item-5-(will-be-wraped)");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-3; item-4; item-5-(will-be-wraped)");

            // Test 7
            queue1.Enqueue("item-6 (another-wrapped-item)");
            queue1.Enqueue("item-7-(will-trigger-expansion)");
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == "item-3; item-4; item-5-(will-be-wraped); item-6 (another-wrapped-item); item-7-(will-trigger-expansion)");

            // Test 8
            queue1.Clear();
            System.Diagnostics.Debug.Assert(queue1.Count == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", queue1.GetEnumerable()) == string.Empty);

            System.Console.WriteLine("Queue (Array based). End Tests.");
        }
    }

    public class Queue<TValueType>
    {
        private TValueType[] _internalArray;

        private int _size;
        private int _headIndex;
        private int _tailIndex;

        public Queue()
        {
            _internalArray = new TValueType[0];
            _size = 0;
            _headIndex = 0;
            _tailIndex = -1;
        }

        public int Count { get { return _size; } }

        public void Clear()
        {
            _internalArray = new TValueType[0];
            _size = 0;
            _headIndex = 0;
            _tailIndex = -1;
        }

        public TValueType Peek()
        {
            if (_size == 0)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }

            return _internalArray[_headIndex];
        }

        public TValueType Dequeue()
        {
            if (_size == 0)
            {
                throw new System.InvalidOperationException("Queue is empty.");
            }

            var value = _internalArray[_headIndex];

            _internalArray[_headIndex] = default(TValueType);

            _headIndex++; // Move to the next element

            if (_headIndex == _internalArray.Length)
            {
                // If headIndex passed the end of the internalArray, set it to the beginning
                _headIndex = 0;
            }

            _size--;

            return value;
        }

        public void Enqueue(TValueType value)
        {
            // Check if we need to expand the internal storage
            if (_size == _internalArray.Length)
            {
                var newInternalArraySize = _size == 0 ? 2 : (_size * 2);
                var newInternalArray = new TValueType[newInternalArraySize];

                // Check if we need to copy content from the old internalArray
                if (_size > 0)
                {
                    int countOfElementsFromHeadToTheEndOfStorage = IsTailInFrontOfHead()
                        ? _internalArray.Length - _headIndex
                        : _tailIndex - _headIndex + 1;

                    // Copy elements that are located between Head and the end of the internal storage
                    System.Array.ConstrainedCopy(_internalArray, _headIndex, newInternalArray, 0, countOfElementsFromHeadToTheEndOfStorage);

                    // Coppy "wrapped" elements to the new array, if needed.
                    // ie. new elements that are located in the beginning of the internal storage if Tail was moved in front of the Head
                    if (IsTailInFrontOfHead())
                    {
                        System.Array.ConstrainedCopy(_internalArray, 0, newInternalArray, countOfElementsFromHeadToTheEndOfStorage, _tailIndex + 1);
                    }

                    _headIndex = 0;
                    _tailIndex = _size - 1;
                }
                else
                {
                    _headIndex = 0;
                    _tailIndex = -1;
                }

                _internalArray = newInternalArray;
            }


            _tailIndex++;
            if (_tailIndex == _internalArray.Length)
            {
                // if tail index passed the end of the array, lets wrap around 
                // and continue writing elements from the beginning of the array
                _tailIndex = 0;
            }

            _internalArray[_tailIndex] = value;

            _size++;
        }

        public System.Collections.Generic.IEnumerable<TValueType> GetEnumerable()
        {
            var indexFrom = _headIndex;
            var indexTo = IsTailInFrontOfHead()
                ? _internalArray.Length - 1
                : _tailIndex;

            // Output elements that are in the end of the internal array
            for (int index = indexFrom; index <= indexTo; index++)
            {
                yield return _internalArray[index];
            }

            if (IsTailInFrontOfHead())
            {
                // Output elements that are in the beginning of the internal array, if it was "wrapped"
                for (int index = 0; index <= _tailIndex; index++)
                {
                    yield return _internalArray[index];
                }
            }
        }


        private bool IsTailInFrontOfHead()
        {
            // Check if array was "wrapped" and we started adding new elements from the begining, but still reading from the end
            return _tailIndex < _headIndex;
        }
    }
}