namespace RemoveOddOccurences
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

            var numOccure = new Dictionary<int, int>();

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

            List<int> result = new List<int>();

            foreach (var pair in numOccure)
            {
                if (pair.Value % 2 == 1)
                {
                    result.Add(pair.Key);
                }
            }

            foreach (var num in result)
            {
                numbers.RemoveAll(x => x == num);
            }

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}
