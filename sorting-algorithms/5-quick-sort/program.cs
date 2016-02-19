namespace QuickSort
{
    public class Program
    {
        public static void Main()
        {
            int[] array = new int[10] { 3, 1, 7, 8, 2, 4, 9, 5, 6, 0 };
            PrintArray(array, "Before:");

            QuickSort(array, 0, array.Length - 1);

            PrintArray(array, "After:");
        }

        private static void QuickSort<T>(T[] array, int loIndex, int hiIndex) where T : System.IComparable<T>
        {
            if (loIndex < hiIndex)
            {
                var p = Partition(array, loIndex, hiIndex);
                QuickSort(array, loIndex, p - 1);
                QuickSort(array, p + 1, hiIndex);
            }
        }

        private static int Partition<T>(T[] array, int loIndex, int hiIndex) where T : System.IComparable<T>
        {
            int left = loIndex;
            int pivot = loIndex;
            int right = hiIndex;
            var pivotItem = array[pivot];

            while (left < right)
            {
                // Advance left index to the first item that is greater than pivot_item
                while (array[left].CompareTo(pivotItem) <= 0) { left++; }

                // Retreat right index to the first item that is less than or equal to pivot_item
                while (array[right].CompareTo(pivotItem) > 0) { right--; }

                // Swap left and right items, because they are in incorrect order
                // Ie. item that is less_than/equal_to pivot_item is on the right; and item that is greater than pivot_item is on the left.
                //     but should be the opposite
                if (left < right) { Swap(array, left, right); }
            }

            // Move pivot_item to "center"
            array[loIndex] = array[right];
            array[right] = pivotItem;
            return right;
        }

        private static void Swap<T>(T[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }


        public static void PrintArray<T>(T[] arrayToPrint, string message)
        {
            System.Console.WriteLine(message);
            System.Console.WriteLine(string.Join(" ", arrayToPrint));
        }
    }
}