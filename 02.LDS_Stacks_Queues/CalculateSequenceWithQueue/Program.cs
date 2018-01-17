namespace CalculateSequenceWithQueue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            //	    S1 = N
            //      S2 = S1 + 1
            //    •	S3 = 2 * S1 + 1
            //    •	S4 = S1 + 2
            //    •	S5 = S2 + 1
            //    •	S6 = 2 * S2 + 1
            //    •	S7 = S2 + 2

            int n = int.Parse(Console.ReadLine());
            var result = new Queue<int>();

            var sequance = new List<int>{n};

            result.Enqueue(n);

            while (sequance.Count < 50)
            {
                var firstItem = result.Peek() + 1;
                result.Enqueue(firstItem);
                var secondItem = (result.Peek() * 2) + 1;
                result.Enqueue(secondItem);
                var thirdItem = firstItem + 1;
                result.Enqueue(thirdItem);

                result.Dequeue();
                sequance.Add(firstItem);
                sequance.Add(secondItem);
                sequance.Add(thirdItem);
            }

            Console.WriteLine(string.Join(", ", sequance.Take(50)));
        }
    }
}

