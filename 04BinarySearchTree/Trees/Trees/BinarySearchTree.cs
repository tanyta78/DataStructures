using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private class Node
    {
        public Node(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            this.Value = value;
        }

        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    private Node root;
    private Node current;

    public BinarySearchTree()
    {

    }

    private BinarySearchTree(Node root)
    {
        this.Copy(root);
    }

    private void Copy(Node node)
    {
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
        //recurcive insert

        this.root = this.Insert(this.root, value);

        //iterative insert
/*
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }

        Node current = this.root;
        Node parent = null;

        while (current != null)
        {
            if (value.CompareTo(current.Value) > 0)
            {
                parent = current;
                current = current.Right;

            }
            else if (value.CompareTo(current.Value) < 0)
            {
                parent = current;
                current = current.Left;
            }
            else
            {
                break;
            }
        }

        Node newNode = new Node(value);
        if (value.CompareTo(parent.Value) < 0)
        {
            parent.Left = newNode;
        }
        else
        {
            parent.Right = newNode;
        }
        */
    }

    public bool Contains(T value) 
    {
        Node current = this.root;

        while (current != null)
        {
            if (value.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else if (value.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node parent = null;
        Node minNode = this.root;

        while (minNode.Left != null)
        {
            parent = minNode;
            minNode = parent.Left;
        }

        if (parent == null)
        {
            this.root = minNode.Right;
        }
        else
        {
            parent.Left = minNode.Right;
        }
    }

    public BinarySearchTree<T> Search(T item)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(item) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(item) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        if (current == null)
        {
            return null;
        }

        return new BinarySearchTree<T>(current);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }
        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
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

    //recursive insert
    private Node Insert(Node node, T value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        int compare = node.Value.CompareTo(value);

        if (compare > 0)
        {
            node.Left = this.Insert(node.Left, value);
        }
        else if (compare < 0)
        {
            node.Right = this.Insert(node.Right, value);
        }

        return node;
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {

    }
}
