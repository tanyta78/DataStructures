namespace LinearDataStructures
{
    using System;
    using System.Collections.Generic;
   
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
    }
}
