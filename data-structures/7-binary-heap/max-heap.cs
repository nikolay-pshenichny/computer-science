#define DEBUG
namespace BinaryHeap
{
    public class Program
    {
        public static void Main()
        {
            System.Console.WriteLine("Max Heap. Begin Tests.");

            // Test 1
            var heap1 = new MaxHeap<int>();
            System.Diagnostics.Debug.Assert(heap1.Size == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", heap1.GetEnumerable()) == string.Empty);

            // Test 2
            heap1.Insert(33);
            heap1.Insert(10);
            heap1.Insert(5);
            heap1.Insert(22);
            heap1.Insert(44);
            System.Diagnostics.Debug.Assert(heap1.Size == 5);
            System.Diagnostics.Debug.Assert(string.Join("; ", heap1.GetEnumerable()) == "44; 33; 22; 10; 5");

            // Test 3
            var head = heap1.Peek();
            System.Diagnostics.Debug.Assert(head == 44);
            System.Diagnostics.Debug.Assert(heap1.Size == 5);
            System.Diagnostics.Debug.Assert(string.Join("; ", heap1.GetEnumerable()) == "44; 33; 22; 10; 5");

            // Test 4
            head = heap1.ExtractMax();
            System.Diagnostics.Debug.Assert(head == 44);
            System.Diagnostics.Debug.Assert(heap1.Size == 4);
            System.Diagnostics.Debug.Assert(string.Join("; ", heap1.GetEnumerable()) == "33; 10; 22; 5");

            // Test 5
            heap1.Clear();
            System.Diagnostics.Debug.Assert(heap1.Size == 0);
            System.Diagnostics.Debug.Assert(string.Join("; ", heap1.GetEnumerable()) == "");

            // Test 6
            heap1 = new MaxHeap<int>(new int[] { 1, 20, 11, 33, 4, 9, 55 });
            System.Diagnostics.Debug.Assert(heap1.Size == 7);
            System.Diagnostics.Debug.Assert(string.Join("; ", heap1.GetEnumerable()) == "55; 33; 11; 20; 4; 9; 1");

            // Test 7
            head = heap1.ExtractMax();
            System.Diagnostics.Debug.Assert(head == 55);
            System.Diagnostics.Debug.Assert(heap1.Size == 6);
            System.Diagnostics.Debug.Assert(string.Join("; ", heap1.GetEnumerable()) == "33; 20; 11; 1; 4; 9");

            System.Console.WriteLine("Max Heap. End Tests.");
        }
    }


    public class MaxHeap<TValueType> where TValueType : System.IComparable<TValueType>
    {
        private const int DefaultSize = 4;
        private TValueType[] _internalStorage;
        private int _size;

        public MaxHeap()
        {
            _internalStorage = null;
            _size = 0;
        }

        public MaxHeap(TValueType[] arrayForMaxheapifycation)
        {
            if (arrayForMaxheapifycation == null)
            {
                throw new System.ArgumentNullException("arrayForMaxheapifycation");
            }

            // Create Max Heap from the provided data
            BuildMaxHeap(arrayForMaxheapifycation);
        }

        public int Size { get { return _size; } }

        public void Clear()
        {
            _internalStorage = null;
            _size = 0;
        }

        public void Insert(TValueType value)
        {
            if ((_internalStorage == null) || (_size == _internalStorage.Length))
            {
                ResizeInternalStorage();
            }

            // Insert the new element to the end of the heap
            _internalStorage[_size] = value;

            // Bubble it up to a proper position
            BubbleUp(_size);

            _size++;
        }

        public TValueType Peek()
        {
            if (_size == 0)
            {
                throw new System.InvalidOperationException("Heap is empty");
            }

            // Head of the max heap
            return _internalStorage[0];
        }


        public TValueType ExtractMax()
        {
            if (_size == 0)
            {
                throw new System.InvalidOperationException("Heap is empty");
            }

            // We will need to return the head element of the max heap, so lets put it in temp variable
            var result = _internalStorage[0];

            _size--;

            // Replace the head of the heap with the last element of the heap
            _internalStorage[0] = _internalStorage[_size];

            // BubbleDown the element of the head to a proper position.
            // We need to do this because we moved a smaller element from the end of the heap to the head
            BubbleDown(0);

            return result;
        }

        public System.Collections.Generic.IEnumerable<TValueType> GetEnumerable()
        {
            for (int idx = 0; idx < _size; idx++)
            {
                yield return _internalStorage[idx];
            }
        }

        /// <summary>
        /// Resize the internal storage if we are out of space
        /// </summary>
        private void ResizeInternalStorage()
        {
            int newSize = _size == 0 ? DefaultSize : _size * 2;
            var newStorage = new TValueType[newSize];
            if (_size > 0)
            {
                System.Array.Copy(_internalStorage, newStorage, _size);
            }
            _internalStorage = newStorage;
        }

        /// <summary>
        /// Aka: up-heap, bubble-up, percolate-up, sift-up, trickle-up, heapify-up, or cascade-up
        /// </summary>
        /// <param name="elementIndex"></param>
        private void BubbleUp(int elementIndex)
        {
            if (elementIndex > 0)
            {
                int parentElementIndex = elementIndex / 2;
                if (_internalStorage[elementIndex].CompareTo(_internalStorage[parentElementIndex]) > 0)
                {
                    // If value at 'elementIndex' is bigger than the value of the parent,
                    // then we need to swap them and MaxHeapify the 'parentElementIndex' to be sure that it is in the right place
                    SwapElements(elementIndex, parentElementIndex);
                    BubbleUp(parentElementIndex);
                }
            }
        }

        /// <summary>
        /// Aka: down-heap, bubble-down, percolate-down, sift-down, trickle down, heapify-down, cascade-down, and extract-min/max
        /// </summary>
        /// <param name="parentElementIndex"></param>
        private void BubbleDown(int parentElementIndex)
        {
            int leftChildIndex = (parentElementIndex * 2) + 1;
            int rightChildIndex = leftChildIndex + 1;
            int largestChildIndex = parentElementIndex;

            if ((leftChildIndex < _size) && (_internalStorage[leftChildIndex].CompareTo(_internalStorage[largestChildIndex]) > 0))
            {
                largestChildIndex = leftChildIndex;
            }

            if ((rightChildIndex < _size) && (_internalStorage[rightChildIndex].CompareTo(_internalStorage[largestChildIndex]) > 0))
            {
                largestChildIndex = rightChildIndex;
            }

            if (parentElementIndex != largestChildIndex)
            {
                SwapElements(parentElementIndex, largestChildIndex);
                BubbleDown(largestChildIndex);
            }
        }

        private void BuildMaxHeap(TValueType[] sourceArray)
        {
            _size = sourceArray.Length;
            _internalStorage = new TValueType[_size];
            System.Array.Copy(sourceArray, _internalStorage, _size);

            // In heaps, last parent is located at the (_size/2) index; after that point there are only leafs
            int midPoint = (_size / 2) - 1;
            for (int idx = midPoint; idx >= 0; idx--)
            {
                BubbleDown(idx);
            }
        }

    private void SwapElements(int idxA, int idxB)
        {
            TValueType temp = _internalStorage[idxA];
            _internalStorage[idxA] = _internalStorage[idxB];
            _internalStorage[idxB] = temp;
        }
    }

}