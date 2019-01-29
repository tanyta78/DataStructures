namespace MassEffectGalaxyMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
       public static void Main(string[] args)
       {
            var galaxy = new KdTree();

           var starsClusters = int.Parse(Console.ReadLine());
           var reports = int.Parse(Console.ReadLine());
           var size = int.Parse(Console.ReadLine());

           var space = new Rectangle(0,size,0,size);
           var clusters = new List<Point2D>();
           for (int i = 0; i < starsClusters; i++)
           {
               var tokens = Console.ReadLine().Split();

               var point = new Point2D(double.Parse(tokens[1]), double.Parse(tokens[2]));

               if (point.IsInRectangle(space))
               {
                   clusters.Add(point);
               }
           }

           clusters.ForEach(galaxy.Insert);
           clusters.Clear();

           for (int i = 0; i < reports; i++)
           {
               var tokens = Console.ReadLine().Split().Skip(1).Select(double.Parse).ToArray();
               Rectangle rect = new Rectangle(tokens[0], tokens[0] + tokens[2], tokens[1], tokens[1] + tokens[3]);

               galaxy.GetPoints(clusters.Add, rect, space);

               Console.WriteLine(clusters.Count);
               clusters.Clear();
           }
       }
    }
}
