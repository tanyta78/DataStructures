using System;
using System.Collections.Generic;

public class Tree<T>
{
    public T Value { get; set; }

    public IList<Tree<T>> Children { get; set; }

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
       // this.Children = new List<Tree<T>>(children);

        this.Children = new List<Tree<T>>();

        foreach (var child in children)
        {
            this.Children.Add(child);
        }
    }

    public void Print(int indent = 0)
    {
        Console.Write(new string(' ', 2 * indent));
        Console.WriteLine(this.Value);
        foreach (var child in this.Children)
        {
            child.Print(indent + 1);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);

        foreach (var child in this.Children)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        var result = new List<T>();
        this.DFS(this, result);
        return result;
    }

    public IEnumerable<T> OrderDfsWithStack()
    {
        var result = new Stack<T>();
       
        var stack = new Stack<Tree<T>>();
        stack.Push(this);

        while (stack.Count>0)
        {
            var current = stack.Pop();

            foreach (var child in current.Children)
            {
                stack.Push(child);
            }

            result.Push(current.Value);
        }
        
        return result.ToArray();
    }

    private void DFS(Tree<T> tree, List<T> result)
    {
        foreach (var child in tree.Children)
        {
            this.DFS(child, result);
        }

        result.Add(tree.Value);
    }

    public IEnumerable<T> OrderBFS()
    {
        var result = new List<T>();
        var queue = new Queue<Tree<T>>();

        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result.Add(node.Value);

            foreach (var nodeChild in node.Children)
            {
                queue.Enqueue(nodeChild);
            }
        }


        return result;
    }
}
