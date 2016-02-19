namespace InsertionSort
{
    public class Program
    {
        public static void Main()
        {
            int[] array = new int[10] { 3, 1, 7, 8, 2, 4, 9, 5, 6, 0 };
            PrintArray(array, "Before:");

            // Insertion sort
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                // Remember the item
                var temp = array[i];

                // "Shift" sorted items in order to free up a spot for the the item that we are working on
                int j = i - 1;
                while ((j >= 0) && (array[j] > temp))
                {
                    array[j + 1] = array[j];
                    j--;
                }

                // "Insert" the item into its spot
                array[j + 1] = temp;
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