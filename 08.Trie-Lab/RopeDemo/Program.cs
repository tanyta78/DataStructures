namespace RopeDemo
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Wintellect.PowerCollections;

    public class Program
    {
        public static void Main(string[] args)
        {
            int OperationsCount = 100_000;

            BigList<string> rope = new BigList<string>();
            string str = String.Empty;
            var sb = new StringBuilder();
            Stopwatch stopwatch = Stopwatch.StartNew();

            #region insertAtStart

            for (int i = 0; i < OperationsCount; i++)
            {
                rope.Insert(0, "a");
            }

            stopwatch.Stop();
            Console.WriteLine($"Rope insert at start: {stopwatch.ElapsedMilliseconds}ms");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < OperationsCount; i++)
            {
                str = str.Insert(0, "a");
            }

            stopwatch.Stop();
            Console.WriteLine($"String insert at start:: {stopwatch.ElapsedMilliseconds}ms");

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < OperationsCount; i++)
            {
                sb.Insert(0, "a");
            }

            stopwatch.Stop();
            Console.WriteLine($"StringBuilder insert at start:: {stopwatch.ElapsedMilliseconds}ms");

            #endregion

            #region insertMiddle

            rope = new BigList<string>();
            for (int i = 0; i < OperationsCount; i++)
            {
                rope.Insert(rope.Count / 2, "a");
            }

            stopwatch.Stop();
            Console.WriteLine($"Rope insert at middle:: {stopwatch.ElapsedMilliseconds}ms");

            str = String.Empty;
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < OperationsCount; i++)
            {
                str = str.Insert(str.Length / 2, "a");
            }

            stopwatch.Stop();
            Console.WriteLine($"String insert at middle:: {stopwatch.ElapsedMilliseconds}ms");

            sb = new StringBuilder();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < OperationsCount; i++)
            {
                sb.Insert(sb.Length / 2, "a");
            }

            stopwatch.Stop();
            Console.WriteLine($"StringBuilder insert at middle:: {stopwatch.ElapsedMilliseconds}ms");

            #endregion

            #region insertEnd

            rope = new BigList<string>();
            for (int i = 0; i < OperationsCount; i++)
            {
                rope.Add("a");
            }

            stopwatch.Stop();
            Console.WriteLine($"Rope insert at end:: {stopwatch.ElapsedMilliseconds}ms");

            str = String.Empty;
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < OperationsCount; i++)
            {
                str += "a";
            }

            stopwatch.Stop();
            Console.WriteLine($"String insert at end: {stopwatch.ElapsedMilliseconds}ms");

            sb = new StringBuilder();
            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < OperationsCount; i++)
            {
                sb.Append("a");
            }

            stopwatch.Stop();
            Console.WriteLine($"StringBuilder insert at end: {stopwatch.ElapsedMilliseconds}ms");

            #endregion
        }
    }
}
