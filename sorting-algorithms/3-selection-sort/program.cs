namespace SelectionSort
{
    public class Program
    {
        public static void Main()
        {
            int[] array = new int[10] { 3, 1, 7, 8, 2, 4, 9, 5, 6, 0 };
            PrintArray(array, "Before:");

            // Selection sort
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                // Assume that the item that we see has minimal value
                int iMin = i;

                // Compare it to the rest of the array 
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] < array[iMin])
                    {
                        iMin = j;
                    }
                }

                if (iMin != i)
                {
                    // Swap items
                    var temp = array[i];
                    array[i] = array[iMin];
                    array[iMin] = temp;
                }
            }

            PrintArray(array, "After:");
        }

        public static void PrintArray<T>(T[] arrayToPrint, string message)
        {
            System.Console.WriteLine(message);
            System.Console.WriteLine(string.Join(" ", arrayToPrint));
        }
    }
}