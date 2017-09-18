using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;

        }

        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public BinarySearchTree()
    {

    }

    private BinarySearchTree(Node node)
    {
        this.Copy(node);
    }

    private void Copy(Node node)
    {
        //pre order

        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public void Insert(T value)
    {
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }
        Node parent = null;
        Node current = this.root;
        while (current != null)
        {
            parent = current;
            if (current.Value.CompareTo(value) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(value) < 0)
            {
                current = current.Right;
            }
            else
            {
                return;
            }
        }

        current = new Node(value);
        if (parent.Value.CompareTo(value) < 0)
        {
            parent.Right = current;
        }
        else
        {
            parent.Left = current;
        }
    }

    public bool Contains(T value)
    {
        Node current = FindElement(value);
        return current != null;
    }

    private Node FindElement(T value)
    {
        Node current = this.root;

        while (current != null)
        {

            if (current.Value.CompareTo(value) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(value) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node current = this.root;
        Node parent = null;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = current.Right;
        }
        else
        {
            parent.Left = current.Right;
        }

    }

    public BinarySearchTree<T> Search(T item)
    {
        Node current = FindElement(item);


        return new BinarySearchTree<T>(current);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> result = new Queue<T>();

        this.Range(startRange, endRange, result, this.root);


        return result;
    }

    private void Range(T startRange, T endRange, Queue<T> result, Node node)
    {
        if (node == null)
        {
            return;
        }

        int compareLow = startRange.CompareTo(node.Value);
        int compareHigh = endRange.CompareTo(node.Value);

        if (compareLow < 0)
        {
            this.Range(startRange, endRange, result, node.Left);
        }

        if (compareLow <= 0 && compareHigh >= 0)
        {
            result.Enqueue(node.Value);
        }

        if (compareHigh > 0)
        {
            this.Range(startRange, endRange, result, node.Right);
        }
    }

    private IEnumerable<T> Range(Node node, T startRange, T endRange)
    {
        if (node == null)
        {
            yield break;
        }

        int compareLow = startRange.CompareTo(node.Value);
        int compareHigh = endRange.CompareTo(node.Value);

        if (compareLow < 0)
        {
            foreach (var item in this.Range(node.Left, startRange, endRange))
            {
                yield return item;
            }

        }

        if (compareLow <= 0 && compareHigh >= 0)
        {
            yield return node.Value;
        }

        if (compareHigh > 0)
        {
            foreach (var item in this.Range(node.Right, startRange, endRange))
            {
                yield return item;
            }
            
        }


    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);

    }
}


public class Launcher
{
    public static void Main(string[] args)
    {

    }
}
