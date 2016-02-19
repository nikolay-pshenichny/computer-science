namespace MergeSort
{
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            int[] array = new int[10] { 3, 1, 7, 8, 2, 4, 9, 5, 6, 0 };
            PrintArray(array, "Before:");

            // Merge sort
            array = MergeSort(array);
    
            PrintArray(array, "After:");
        }

        private static T[] MergeSort<T>(T[] array) where T : System.IComparable<T>
        {
            if (array.Length <= 1)
            {
                return array;
            }

            // Split the original array in half and recursively sort left and right parts
            int middle = array.Length / 2;
            var left = MergeSort(new System.ArraySegment<T>(array, 0, middle).ToArray());
            var right = MergeSort(new System.ArraySegment<T>(array, middle, (array.Length - middle)).ToArray());

            // Merge sorted left and right arrays into the original array
            int leftArrayIndex = 0, rightArrayIndex = 0, targetArrayIndex = 0;

            while ((leftArrayIndex < left.Length) && (rightArrayIndex < right.Length))
            {
                if (left[leftArrayIndex].CompareTo(right[rightArrayIndex]) <= 0)
                {
                    array[targetArrayIndex] = left[leftArrayIndex++];
                }
                else
                {
                    array[targetArrayIndex] = right[rightArrayIndex++];
                }

                targetArrayIndex++;
            }

            // Copy the remaining elements from the Left array (if there are any)
            while (leftArrayIndex < left.Length)
            {
                array[targetArrayIndex++] = left[leftArrayIndex++];
            }

            // Copy the remaining elements from the Right array (if there are any)
            while (rightArrayIndex < right.Length)
            {
                array[targetArrayIndex++] = right[rightArrayIndex++];
            }

            return array;
        }

        public static void PrintArray<T>(T[] arrayToPrint, string message)
        {
            System.Console.WriteLine(message);
            System.Console.WriteLine(string.Join(" ", arrayToPrint));
        }
    }
}