namespace SweepAndPrune
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            var items = new List<GameObject>();
            var byId = new Dictionary<string, GameObject>();
            var counter = 0;

            string command;
            while ((command = Console.ReadLine()) != "start")
            {
                if (command == null)
                {
                    throw new ArgumentException();
                }

                var tokens = command.Split();

                switch (tokens[0])
                {
                    case "add":
                        Add(tokens, items, byId);
                        break;
                }

            }

            while ((command = Console.ReadLine()) != "end")
            {
                if (command == null)
                {
                    throw new ArgumentException();
                }

                var tokens = command.Split();
                counter++;
                if (tokens[0].Equals("move"))
                {
                    var id = tokens[1];
                    var x = int.Parse(tokens[2]);
                    var y = int.Parse(tokens[3]);
                    byId[id].X1 = x;
                    byId[id].Y1 = y;
                }

                SweepAndPrune(items, counter);

            }
        }

        private static void SweepAndPrune(List<GameObject> items, int counter)
        {
            InsertionSort(items);

            for (int i = 0; i < items.Count; i++)
            {
                var current = items[i];
                for (int j = i + 1; j < items.Count; j++)
                {
                    var candidate = items[j];

                    if (candidate.X1 > current.X2)
                    {
                        break;
                    }

                    if (current.Intersect(candidate))
                    {
                        Console.WriteLine($"({counter}) {current.Id} collides with {candidate.Id}");
                    }
                }
            }
        }

        private static void InsertionSort(List<GameObject> items)
        {
            for (int i = 1; i < items.Count; i++)
            {
                var j = i;
                while (j > 0 && items[j - 1].X1 > items[j].X1)
                {
                    Swap(j - 1, j, items);
                    j--;
                }
            }
        }

        private static void Swap(int i, int j, List<GameObject> items)
        {
            var temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }

        private static void Add(string[] tokens, List<GameObject> items, Dictionary<string, GameObject> byId)
        {
            var id = tokens[1];
            var x = int.Parse(tokens[2]);
            var y = int.Parse(tokens[3]);
            var item = new GameObject(id, x, y);
            items.Add(item);
            byId.Add(id, item);
        }
    }
}
