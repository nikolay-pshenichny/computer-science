namespace StringSearch
{
    public class Program
    {
        public static void Main()
        {
            string text = "In computer science, string searching algorithms, sometimes called string matching algorithms, " 
                + "are an important class of string algorithms that try to find a place where one or several strings (also "
                + "called patterns) are found within a larger string or text.";

            string pattern = "string";


            // Naive string search algorithm

            int textIdx = 0;
            int textLen = text.Length;
            int patternLen = pattern.Length;
            int comparisonsMade = 0;

            while (textIdx <= (textLen - patternLen))
            {
                int patternIdx = 0;

                while (AreEqual(text[textIdx + patternIdx], pattern[patternIdx], ref comparisonsMade))
                {
                    patternIdx++;

                    if (patternIdx == patternLen)
                    {
                        // We found a match
                        System.Console.WriteLine("Match found at position {0}", textIdx);

                        // Lets move the textIdx on patternLen characters forward to skip processing the same characters
                        textIdx = textIdx + patternLen - 1;

                        break;
                    }
                }

                textIdx++;
            }

            System.Console.WriteLine("Comparisons made: {0}", comparisonsMade);
        }

        private static bool AreEqual(char a, char b, ref int comparisonsMade)
        {
            comparisonsMade++;
            return a == b;
        }

    }
}