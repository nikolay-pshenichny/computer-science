namespace BubbleSort
{
    public class Program
    {
        public static void Main()
        {
            int[] array = new int[10] { 3, 1, 7, 8, 2, 4, 9, 5, 6, 0 };
            PrintArray(array, "Before:");

            // Bubble sort
            int n = array.Length;
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 1; i < n; i++)
                {
                    if (array[i - 1] > array[i])
                    {
                        // Swap elements
                        var temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        swapped = true;
                    }
                }
                n--;
            }
            while (swapped);

            PrintArray(array, "After:");
        }

        public static void PrintArray<T>(T[] arrayToPrint, string message)
        {
            System.Console.WriteLine(message);
            System.Console.WriteLine(string.Join(" ", arrayToPrint));
        }
    }
}