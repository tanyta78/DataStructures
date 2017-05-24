namespace LinearDataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ListsHomework
    {
        public static void Main()
        {
            var numbersInput = Console.ReadLine().Split().Select(int.Parse).ToList();
          //  int startIndex = 0;
            int lastIndex = 0;
            int maxLength = 1;
            int prevNum = numbersInput[0];
            int currentLength = 1;
            for (int i = 1; i < numbersInput.Count; i++)
            {
                int currNum = numbersInput[i];
                if (prevNum == currNum)
                {
                    currentLength++;
                    continue;
                }
                else
                {
                    if (currentLength > maxLength)
                    {
                        maxLength = currentLength;
                        lastIndex = i - 1;
                    }
                    currentLength = 1;
                }
               
            }


        }

        public static void LongestSubsequence()
        {
            
        }

        public static void SortWords()
        {
            List<string> input = Console.ReadLine().Split().ToList();
            input.Sort();
            Console.WriteLine(String.Join(" ", input));
        }

        public static void SumAndAverage()
        {
        string[] input = Console.ReadLine().Split();
        List<int> numbers = new List<int>();
            if (input.Length == 0)
        {
            Console.WriteLine("Sum=0; Average=0.00");
        }
        else
        {
            foreach (var item in input)
            {
                numbers.Add(int.Parse(item));
            }

            Console.WriteLine($"Sum={numbers.Sum()}; Average={numbers.Average():f2}");
        }
        }

    }
}
