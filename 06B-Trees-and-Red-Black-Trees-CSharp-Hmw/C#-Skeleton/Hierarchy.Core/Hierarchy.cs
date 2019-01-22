
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private Node root;
    private Dictionary<T, Node> nodes;

    public Hierarchy(T root)
    {
        this.root = new Node(root);
        this.nodes = new Dictionary<T, Node>
            {
                { root, this.root }
            };
    }

    public int Count => this.nodes.Count;

    public void Add(T element, T child)
    {
        //if (element == null || child == null)
        //{
        //    throw new ArgumentException();
        //}

        if (!this.Contains(element) || this.Contains(child))
        {
            throw new ArgumentException();
        }

        var childNode = new Node(child, this.nodes[element]);

        this.nodes[element].Children.Add(childNode);
        this.nodes.Add(child, childNode);
    }

    public void Remove(T element)
    {
        if (!this.Contains(element))
        {
            throw new ArgumentException();
        }

        var nodeElement = this.nodes[element];

        if (nodeElement.Parent == null)
        {
            throw new InvalidOperationException();
        }

        var parentNode = nodeElement.Parent;
        var nodeChildren = nodeElement.Children;
        foreach (var child in nodeChildren)
        {
            child.Parent = parentNode;
            parentNode.Children.Add(child);

        }

        parentNode.Children.Remove(nodeElement);
        this.nodes.Remove(element);
    }

    public IEnumerable<T> GetChildren(T item)
    {
        if (!this.Contains(item))
        {
            throw new ArgumentException();
        }

        var parent = this.nodes[item];

        return parent.Children.Select(x => x.Value);
    }

    public T GetParent(T item)
    {
        if (!this.Contains(item))
        {
            throw new ArgumentException();
        }

        var parent = this.nodes[item].Parent;

        return parent != null ? parent.Value : default(T);
    }

    public bool Contains(T value)
    {
        return this.nodes.ContainsKey(value);
    }

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        var result = this.nodes.Keys.Intersect(other.nodes.Keys);
        return result;

        //var list = new List<T>();

        //foreach (var key in this.nodes.Keys)
        //{
        //    if (other.nodes.ContainsKey(key))
        //    {
        //        list.Add(key);
        //    }
        //}

        //return list;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var queue = new Queue<T>();

        queue.Enqueue(this.root.Value);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var child in this.nodes[current].Children)
            {
                queue.Enqueue(child.Value);
            }
            yield return current;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class Node
    {
        public Node(T value, Node parent = null)
        {
            this.Value = value;
            this.Children = new List<Node>();
            this.Parent = parent;
        }

        public T Value { get; set; }
        public List<Node> Children { get; set; }
        public Node Parent { get; set; }

    }
}
