namespace TreeHomework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        public static void Main()
        {
            ReadTree();
            var root = GetRootNode();

            //Problem 1.	Root Node
            /* Write a program to read the tree and find its root node:
                         
             Console.WriteLine($"Root node: {root.Value}");*/

            //Problem 2.	Print Tree
            /*
             * Write a program to read the tree from the console and print it in the following format (each level indented +2 spaces)
             
                root.Print();
            */

            //Problem 3.	Leaf Nodes
            /*
             * Write a program to read the tree and find all leaf nodes (in increasing order)
            
            PrintLeafesNodes();

            */

            //Problem 4.	Middle Nodes
            /*Write a program to read the tree and find all middle nodes (in increasing order)
             PrintMiddleNodes();
            */

            //Problem 5.	* Deepest Node
            /*Write a program to read the tree and find its deepest node (leftmost)
             * 
            var deepestNode = GetDeepestNode(root);
            Console.WriteLine($"Deepest node: {deepestNode.Value}");

            */

            //Problem 6.	Longest Path
            /*Find the longest path in the tree (the leftmost if several paths have the same longest length)
            
            var deepestNode = GetDeepestNode(root);
            Console.Write("Longest path: ");
            PrintPath(deepestNode);

              */

            //Problem 7.	All Paths With a Given Sum
            /*Find all paths in the tree with given sum of their nodes (from the leftmost to the rightmost)
             * 
              var targetSum = int.Parse(Console.ReadLine());
            Console.WriteLine($"Paths of sum {targetSum}:");
            var leaves = GetPathWithGivenSum(root, targetSum);
            foreach (var leaf in leaves)
            {
                PrintPath(leaf);
            }
             */
            var targetSum = int.Parse(Console.ReadLine());
            Console.WriteLine($"Subtrees of sum {targetSum}:");
            var subtreeRoots = GetSubtreeWithSum(root, targetSum);

            foreach (var node in subtreeRoots)
            {
                PrintPreOrder(node);
                Console.WriteLine();
            }

        }

        private static void PrintPreOrder(Tree<int> node)
        {
            Console.Write(node.Value + " ");
            foreach (var child in node.Children)
            {
                PrintPreOrder(child);
            }

        }

        private static List<Tree<int>> GetSubtreeWithSum(Tree<int> root,int targetSum)
        {
           var roots = new List<Tree<int>>();

            var sum = DfsTrees(root, targetSum, 0, roots);

            return roots;
        }

        private static int DfsTrees(Tree<int> node, int targetSum, int currentSum, List<Tree<int>> roots)
        {

            if (node == null)
            {
                return 0;
            }

            currentSum = node.Value;

            foreach (var child in node.Children)
            {
                currentSum += DfsTrees(child, targetSum, currentSum, roots);
            }

            if (currentSum == targetSum)
            {
                roots.Add(node);
            }

            return currentSum;
        }

        private static List<Tree<int>> GetPathWithGivenSum(Tree<int> root, int targetSum)
        {
           var leaves = new List<Tree<int>>();

            //find with dfs leaves and check sum
            FindLeavesAndCheckSumWithDfs(root, targetSum, 0,leaves);

            return leaves;
        }

        private static void FindLeavesAndCheckSumWithDfs(Tree<int> currentNode, int targetSum, int currentSum, List<Tree<int>> leaves)
        {
            if (currentNode==null)
            {
                return;
            }

            currentSum += currentNode.Value;

            foreach (var currentNodeChild in currentNode.Children)
            {
                FindLeavesAndCheckSumWithDfs(currentNodeChild, targetSum, currentSum,leaves);
            }

            if (currentSum== targetSum && currentNode.Children.Count==0)
            {
                leaves.Add(currentNode);
            }
        }

        private static void PrintPath(Tree<int> leaf)
        {
            var stack = new Stack<int>();
            var current = leaf;

            while (current != null)
            {
                stack.Push(current.Value);
                current = current.Parent;
            }


            Console.WriteLine(string.Join(" ", stack.ToArray()));
        }

        private static Tree<int> GetDeepestNode(Tree<int> root)
        {
            int maxLevel = 0;
            Tree<int> deepest = null;
            DFS(root, 1, ref maxLevel, ref deepest);

            return deepest;
        }

        private static void DFS(Tree<int> node, int level, ref int maxLevel, ref Tree<int> deepest)
        {
            if (node == null)
            {
                return;
            }

            foreach (var child in node.Children)
            {
                DFS(child, level + 1, ref maxLevel, ref deepest);
            }

            if (level > maxLevel)
            {
                deepest = node;
                maxLevel = level;
            }
        }

        private static void PrintMiddleNodes()
        {
            var middleNodes = nodeByValue.Values
                                         .Where(x => x.Parent != null && x.Children.Count != 0)
                                         .Select(x => x.Value)
                                         .OrderBy(x => x)
                                         .ToList();
            
            Console.WriteLine("Middle nodes: " + string.Join(" ", middleNodes));
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
                string[] edge = Console.ReadLine().Split(' ');
                AddEdge(int.Parse(edge[0]), int.Parse(edge[1]));
            }
        }

        static Tree<int> GetRootNode()
        {
            return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
        }
    }
}
