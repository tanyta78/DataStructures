namespace HackerRankChallenge
{
    using System;

    public class Program
    {
        static int redJohn(int n)
        {
            // Create the solution table
            //Step 1 : Calculate permutations
            var f = new int[41];
            for (int i = 0; i < 4; i++)
            {
                f[i] = 1;
            }
            for (int i = 4; i < 41; i++)
            {
                f[i] = f[i - 1] + f[i - 4];
            }

            //Step 2: Use sieve of Erathosthenes to determine primes up to dt[40]
            var isPrime = new bool[299915];
            for (int i = 0; i <= f[40]; i++)
            {
                if (i>=4 && i%2==0)
                {
                    isPrime[i] = false;
                }
                else { isPrime[i] = true; }
            }
            for (int i = 3; i <= Math.Sqrt(f[40]); i += 2)
            {
                for (int j = i; j * i <= f[40]; j++)
                    isPrime[i * j] = false;
            }
            
            //Step 3: Calculate count of primes
            var count = new int[299915];
            for (int i = 0; i < 3; i++)
            {
                count[i] = 0;
            }
            for (int i = 2; i <= f[40]; i++)
            {
                count[i] = count[i - 1];
                if (isPrime[i])
                {
                    count[i] += 1;
                }
            }

            return count[f[n]];

        }

       static void Main(String[] args)
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int a0 = 0; a0 < t; a0++)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                int result = redJohn(n);
                Console.WriteLine(result);
            }
        }
    }
}
