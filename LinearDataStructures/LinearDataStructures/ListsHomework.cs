namespace LinearDataStructures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ListsHomework
    {
        private static int startRow = 0;
        private static int startCol = 0;
        private static string[,] labyrinth = new string[0, 0];

        public static void Main()
        {

            int numberOfRows = int.Parse(Console.ReadLine());
            labyrinth = new string[numberOfRows, numberOfRows];

            ReadLab(numberOfRows);

            var queue = new Queue<Cell>();
            queue.Enqueue(new Cell { Row = startRow, Col = startCol, Step = 1 });

            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();

                if (currentCell.Row - 1 >= 0 && labyrinth[currentCell.Row - 1, currentCell.Col] == "0")
                {
                    labyrinth[currentCell.Row - 1, currentCell.Col] = currentCell.Step.ToString();
                    queue.Enqueue(new Cell { Row = currentCell.Row - 1, Col = currentCell.Col, Step = currentCell.Step + 1 });
                }

                if (currentCell.Col + 1 < labyrinth.GetLength(1) && labyrinth[currentCell.Row, currentCell.Col + 1] == "0")
                {
                    labyrinth[currentCell.Row, currentCell.Col + 1] = currentCell.Step.ToString();
                    queue.Enqueue(new Cell { Row = currentCell.Row, Col = currentCell.Col + 1, Step = currentCell.Step + 1 });
                }

                if (currentCell.Row + 1 < labyrinth.GetLength(0) && labyrinth[currentCell.Row + 1, currentCell.Col] == "0")
                {
                    labyrinth[currentCell.Row + 1, currentCell.Col] = currentCell.Step.ToString();
                    queue.Enqueue(new Cell { Row = currentCell.Row + 1, Col = currentCell.Col, Step = currentCell.Step + 1 });
                }

                if (currentCell.Col - 1 >= 0 && labyrinth[currentCell.Row, currentCell.Col - 1] == "0")
                {
                    labyrinth[currentCell.Row, currentCell.Col - 1] = currentCell.Step.ToString();
                    queue.Enqueue(new Cell { Row = currentCell.Row, Col = currentCell.Col - 1, Step = currentCell.Step + 1 });
                }
            }

            PrintResultLabyrinth();
        }
        
        //DistanceInLabirinth
        private static void PrintResultLabyrinth()
        {
            for (int row = 0; row < labyrinth.GetLength(0); row++)
            {
                for (int col = 0; col < labyrinth.GetLength(1); col++)
                {
                    if (labyrinth[row, col] == "0")
                    {
                        Console.Write("u");
                    }
                    else
                    {
                        Console.Write(labyrinth[row, col]);
                    }
                }
                Console.WriteLine();
            }
        }

        public class Cell
        {
            public int Step { get; set; }
            public int Row { get; set; }
            public int Col { get; set; }
        }

        private static void ReadLab(int rows)
        {
          
            for (int row = 0; row < rows; row++)
            {
                char[] currentRow = Console.ReadLine().ToCharArray();
                for (int col = 0; col < rows; col++)
                {
                    labyrinth[row, col] = currentRow[col].ToString();
                    
                    if (labyrinth[row,col]=="*")
                    {
                        startRow = row;
                        startCol = col;
                    }
                }
            }

        
        }



        //
        

public static void CountOccureance()
        {
            List<int> numbers = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();

            var numOccure = new SortedDictionary<int, int>();

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


            foreach (var pair in numOccure)
            {
                //2 -> 2 times
                Console.WriteLine($"{pair.Key} -> {pair.Value} times");
            }
        }

        public static void RemoveOddOccurences()
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

        public static void LongestSubsequence()
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

        public static void SortWords()
        {
            List<string> input = Console.ReadLine().Split().ToList();
            input.Sort();
            Console.WriteLine(String.Join(" ", input));
        }

        public static void SumAndAverage()
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
