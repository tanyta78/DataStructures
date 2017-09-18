namespace SequenceNM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {

            int[] numbersInput = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int n = numbersInput[0];
            int m = numbersInput[1];

            Queue<SequenceItem> items = new Queue<SequenceItem>();

            items.Enqueue(new SequenceItem(n, null));

            while (items.Count > 0)
            {
                SequenceItem currentItem = items.Dequeue();

                if (currentItem.Value < m)
                {
                    items.Enqueue(new SequenceItem(currentItem.Value + 1, currentItem));
                    items.Enqueue(new SequenceItem(currentItem.Value + 2, currentItem));
                    items.Enqueue(new SequenceItem(currentItem.Value * 2, currentItem));
                }

                if (currentItem.Value == m)
                {
                    PrintSolution(currentItem);
                    break;
                }
            }
        }

        private static void PrintSolution(SequenceItem item)
        {
            Stack<int> output = new Stack<int>();

            while (item != null)
            {
                output.Push(item.Value);
                item = item.PreviousItem;
            }

            Console.WriteLine(string.Join(" -> ", output));
        }

        class SequenceItem
        {
            public SequenceItem(int value, SequenceItem previousItem)
            {
                this.Value = value;
                this.PreviousItem = previousItem;
            }

            public int Value { get; }

            public SequenceItem PreviousItem { get; }
        }
    }
}
