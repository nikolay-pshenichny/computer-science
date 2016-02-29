namespace StringSearch
{
    public class Program
    {
        private const char DefaultShiftIndicator = (char)0;

        public static void Main()
        {
            string text = "In computer science, string searching algorithms, sometimes called string matching algorithms, " 
                + "are an important class of string algorithms that try to find a place where one or several strings (also "
                + "called patterns) are found within a larger string or text.";

            string pattern = "string";


            // Boyer-Moore-Horspool string search algorithm

            var batMatchTable = GenerateBadMatchTable(pattern);

            int textIdx = 0;
            int textLen = text.Length;
            int patternLen = pattern.Length;
            int comparisonsMade = 0;

            while (textIdx <= (textLen - patternLen))
            {
                int charactersLeftToMatch = patternLen - 1;
                // Compare from the end of the pattern
                while ((charactersLeftToMatch >= 0) && (AreEqual(text[textIdx + charactersLeftToMatch], pattern[charactersLeftToMatch], ref comparisonsMade)))
                {
                    charactersLeftToMatch--;
                }

                if (charactersLeftToMatch < 0)
                {
                    // We found a match
                    System.Console.WriteLine("Match found at position {0}", textIdx);

                    textIdx = textIdx + patternLen;
                }
                else
                {
                    // Calculate on how many chars we need to move the textIdx
                    char badMatchCharacter = text[textIdx + patternLen - 1];
                    int charactersToSkip;
                    if (batMatchTable.ContainsKey(badMatchCharacter))
                    {
                        // If the character in the orignal text is also in the Pattern string, let's 
                        // try to align them in such a way that they match.
                        // (note: that is why we have our BadMatch table)
                        charactersToSkip = batMatchTable[badMatchCharacter];
                    }
                    else
                    {
                        // If the character in original text is not in the Pattern string, then we need
                        // to move textIdx forward on patternLen characters
                        charactersToSkip = batMatchTable[DefaultShiftIndicator];
                    }

                    // Move the textIdx forward
                    textIdx = textIdx + charactersToSkip;
                }
            }

            System.Console.WriteLine("Comparisons made: {0}", comparisonsMade);
        }

        private static bool AreEqual(char a, char b, ref int comparisonsMade)
        {
            comparisonsMade++;
            return a == b;
        }

        /// <summary>
        /// Builds a "bad match" table
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static System.Collections.Generic.Dictionary<char, int> GenerateBadMatchTable(string pattern)
        {
            var result = new System.Collections.Generic.Dictionary<char, int>();

            result.Add(DefaultShiftIndicator, pattern.Length);

            for (int i = 0; i < pattern.Length - 1; i++)
            {
                result[pattern[i]] = pattern.Length - i - 1;
            }

            return result;
        }

    }
}