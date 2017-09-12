namespace LongestSubsequence
{
    using System;
    using System.Linq;

    public class Program
    {
         public static void Main()
        {
            var numbersInput = Console.ReadLine().Split().Select(int.Parse).ToList();

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
                    if (i == numbersInput.Count - 1)
                    {
                        if (currentLength > maxLength)
                        {
                            maxLength = currentLength;
                            lastIndex = i;
                        }

                    }
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
                prevNum = currNum;
            }

            Console.WriteLine(string.Join(" ", numbersInput.Skip(lastIndex - maxLength + 1).Take(maxLength)));
        }
    }
}
