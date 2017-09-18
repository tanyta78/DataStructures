namespace DistInLabAuthor
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            string[,] lab = ReadLab(n);
           

            bool[,] visited = new bool[lab.GetLength(0),lab.GetLength(1)];

            int row = 0;
            int col = 0;

            bool found = false;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (lab[i,j]=="*")
                    {
                        row = i;
                        col = j;
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            var queue = new Queue<Cell>();
            queue.Enqueue(new Cell(row,col,true,0));

            while (queue.Count!=0)
            {
                Cell current = queue.Dequeue();
                visited[current.Row, current.Col] = true;

                if (lab[current.Row, current.Col]!="*")
                {
                    lab[current.Row, current.Col] = current.Moves.ToString();
                }
                

                //up
                if (current.Row -1 >= 0 && lab[current.Row - 1, current.Col] != "x" && !visited[current.Row-1,current.Col])
                {
                    
                    queue.Enqueue(new Cell(current.Row-1,current.Col,false,current.Moves+1));
                }
                //right
                if (current.Col+1<lab.GetLength(1) && lab[current.Row,current.Col+1]!="x" && !visited[current.Row, current.Col + 1])
                {
                    queue.Enqueue(new Cell(current.Row, current.Col + 1, false, current.Moves + 1));
                }
                //down
                if (current.Row + 1 < lab.GetLength(0) && lab[current.Row + 1, current.Col] != "x" && !visited[current.Row + 1, current.Col])
                {
                    queue.Enqueue(new Cell(current.Row + 1, current.Col, false, current.Moves + 1));
                }

                //left
                if (current.Col - 1 >=0 && lab[current.Row, current.Col - 1] != "x" && !visited[current.Row, current.Col - 1])
                {
                    queue.Enqueue(new Cell(current.Row, current.Col - 1, false, current.Moves + 1));
                }
            }

            PrintLab(lab);

        }

        private static string[,] ReadLab(int n)
        {
            string[,] lab = new string[n, n];
            for (int i = 0; i < n; i++)
            {
                char[] line = Console.ReadLine().ToCharArray();
                for (int j = 0; j < n; j++)
                {
                    lab[i, j] = line[j].ToString();
                }
            }

            return lab;
        }

        private static void PrintLab(string[,] lab)
        {
            for (int i = 0; i < lab.GetLength(0); i++)
            {
                for (int j = 0; j < lab.GetLength(1); j++)
                {
                    if (lab[i,j]=="*")
                    {
                        Console.Write("*");
                    }
                    else if (lab[i, j] == "0")
                    {
                        Console.Write("u");
                    }
                    else
                    {
                        Console.Write(lab[i, j]);
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
}

class Cell
{
    public Cell(int row, int col, bool visited, int moves)
    {
        this.Row = row;
        this.Col = col;
        this.Visited = visited;
        this.Moves = moves;
    }

    public int Row { get; set; }
    public int Col { get; set; }
    public bool Visited { get; set; }
    public int Moves { get; set; }
}
