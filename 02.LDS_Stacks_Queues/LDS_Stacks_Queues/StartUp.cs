namespace LDS_Stacks_Queues
{
    using System;
    using System.Collections;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            Stack numbers = new Stack();

            var inputString = Console.ReadLine();

            if (inputString != String.Empty)
            {
                var inputNumbers = inputString.Split().Select(int.Parse).ToArray();

                foreach (int num in inputNumbers)
                {
                    numbers.Push(num);
                }
            }

            while (numbers.Count > 0)
            {
                Console.Write(numbers.Pop());
                Console.Write(' ');
            }

            Console.WriteLine();
        }

    }
}
