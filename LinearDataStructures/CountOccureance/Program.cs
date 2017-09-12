namespace CountOccureance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
      public static void Main()
        {
            List<int> numbers = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();

            var numOccure = new SortedDictionary<int, int>();

            foreach (var number in numbers)
            {
                if (!numOccure.ContainsKey(number))
                {
                    numOccure.Add(number, 1);
                }
                else
                {
                    numOccure[number] += 1;
                }
            }


            foreach (var pair in numOccure)
            {
                //2 -> 2 times
                Console.WriteLine($"{pair.Key} -> {pair.Value} times");
            }
        }
    }
}
