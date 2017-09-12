namespace SortWords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
       public static void Main()
        {
            List<string> input = Console.ReadLine().Split().ToList();
            input.Sort();
            Console.WriteLine(String.Join(" ", input));
        }
    }
}
