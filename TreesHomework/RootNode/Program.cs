namespace RootNode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;

    public class Program
    {
        static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        static void Main()
        {

            ReadTree();
            Tree<int> root = GetRootNode();

            //problem1 print root node 
            //PrintRoot(root);

            //problem2 print tree
            //root.Print();

            //problem3 leafNodes
            //PrintLeafesNodes();

            //problem4 middleNodes
            //PrintMiddleNodes();

            //problem5 DeepestNode(leftmost)
            //Console.WriteLine("Deepest node: " + GetDeepestNode(root).Value);

            // problem7 PrintAllPathsWithGivenSum
            //foreach (var leaf in GetPathsWithSum(root))
            //{
            //    PrintPath(leaf);
            //}

            //problem8 AllSubTreeWithGivenSum

            //foreach (var node in GetSubtreeWithSum(root))
            //{
            //    PrintPreOrder(node);
            //    Console.WriteLine();
            //}

            //problem6 LongestPath
            var deepestLeftMost = GetDeepestNode(GetDeepestNodeWithBFS(root));
            var longestPath= new List<int>();
            var node = deepestLeftMost;
            while (node.Parent!=null)
            {
                longestPath.Add(node.Value);
                node = node.Parent;
            }
            longestPath.Add(root.Value);
            longestPath.Reverse();
            Console.WriteLine("Longest path: "+string.Join(" ",longestPath));

        }

        private static Tree<int> GetDeepestNodeWithBFS(Tree<int> root)
        {
            var q = new Queue<Tree<int>>();
            q.Enqueue(root);
            Tree<int> current = null;
            
            while (q.Count > 0)
            {
                current = q.Dequeue();
                for (int i = current.Children.Count - 1; i >= 0; i--)
                {
                    q.Enqueue(current.Children[i]);
                }
            }

            return current;
        }

        private static void PrintPreOrder(Tree<int> node)
        {
            Console.Write(node.Value + " ");
            foreach (var child in node.Children)
            {
                PrintPreOrder(child);
            }
           
        }


        private static void PrintPath(Tree<int> leaf)
        {
            var stack = new Stack<int>();
            var current = leaf;
            
            while (current !=null)
            {
                stack.Push(current.Value);
                current = current.Parent;
            }

          
            Console.WriteLine(string.Join(" ",stack.ToArray()));
        }

        private static void PrintLeafesNodes()
        {
            var leafNodes = new List<int>();

            foreach (var tree in nodeByValue)
            {
                if (tree.Value.Children.Count == 0)
                {
                    leafNodes.Add(tree.Value.Value);
                }
            }
            leafNodes.Sort();

            Console.WriteLine("Leaf nodes: " + string.Join(" ", leafNodes));
        }

        private static void PrintRoot(Tree<int> root)
        {
            Console.WriteLine("Root node:" + root.Value);
        }

        private static Tree<int> GetRootNode()
        {
            return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
        }

        static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        public static void AddEdge(int parent, int child)
        {
            Tree<int> parentNode = GetTreeNodeByValue(parent);
            Tree<int> childNode = GetTreeNodeByValue(child);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }

        static void ReadTree()
        {
            int nodeCount = int.Parse(Console.ReadLine());

            for (int i = 1; i < nodeCount; i++)
            {
                int[] edge = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                AddEdge(edge[0], edge[1]);
            }

        }

        static void PrintMiddleNodes()
        {
            var nodes = nodeByValue.Values
                .Where(x => x.Parent != null && x.Children.Count != 0)
                .Select(x => x.Value)
                .OrderBy(x => x)
                .ToList();
            Console.WriteLine("Middle nodes: "+ string.Join(" ",nodes));
        }

        //private static Tree<int> GetDeepestNodeWithBFS(Tree<int> root)
        //{
        //    var q=new Queue<Tree<int>>();
        //    q.Enqueue(root);
        //    Tree<int> current = null;

        //    while (q.Count>0)
        //    {
        //        current = q.Dequeue();
        //        for (int i = current.Children.Count - 1; i >= 0; i--)
        //        {
        //            q.Enqueue(current.Children[i]);
        //        }
        //    }

        //    return current;
        //}

        private static Tree<int> GetDeepestNode(Tree<int> root)
        {
            int maxLevel = 0;
            Tree<int> deepest = null;
            DFS(root,1,ref maxLevel,ref deepest);

            return deepest;
        }

        private static void DFS(Tree<int> node,int level,ref int maxLevel,ref Tree<int> deepest)
        {
            if (node==null)
            {
                return;
            }

            foreach (var child in node.Children)
            {
                DFS(child,level+1,ref maxLevel,ref deepest);
            }

            if (level>maxLevel)
            {
                deepest = node;
                maxLevel = level;
            }
        }

        private static List<Tree<int>> GetPathsWithSum(Tree<int> root)
        {
            var leafes = new List<Tree<int>>();

            var target = int.Parse(Console.ReadLine());
            Console.WriteLine($"Paths of sum {target}:");

            Dfs(root,target,0,leafes);

            return leafes;
        }

        private static void Dfs(Tree<int> x,int target,int current,List<Tree<int>> leafes)
        {
            if (x==null)
            {
                return;
            }

            current += x.Value;

            foreach (var xChild in x.Children)
            {
                Dfs(xChild,target,current,leafes);
            }

            if (current==target && x.Children.Count==0)
            {
               leafes.Add(x);
            }
        }

        private static List<Tree<int>> GetSubtreeWithSum(Tree<int> root)
        {
            var target = int.Parse(Console.ReadLine());
            Console.WriteLine($"Subtrees of sum {target}:");

            var roots =new List<Tree<int>>();

            var sum =  DfsTrees(root, target, 0, roots);

            return roots;
        }

        private static int DfsTrees(Tree<int> node,int target,int current,List<Tree<int>> roots)
        {

            if (node==null)
            {
               return 0; 
            }

            current = node.Value;
           
            foreach (var child in node.Children)
            {
                current += DfsTrees(child,target,current,roots);
            }

            if (current==target)
            {
                roots.Add(node);
            }

            return current;
        }
    }
}
