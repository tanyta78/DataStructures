namespace SumAndAvg
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
       public static void Main()
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
