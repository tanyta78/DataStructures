using System;
using System.Collections.Generic;

public class IntervalTree
{
    private class Node
    {
        internal Interval interval;
        internal double max;
        internal Node right;
        internal Node left;

        public Node(Interval interval)
        {
            this.interval = interval;
            this.max = interval.Hi;
        }
    }

    private Node root;

    public void Insert(double lo, double hi)
    {
        this.root = this.Insert(this.root, lo, hi);
    }

    public void EachInOrder(Action<Interval> action)
    {
        this.EachInOrder(this.root, action);
    }

    public Interval SearchAny(double startPoint, double endPoint)
    {
        var current = this.root;

        while (current != null && !current.interval.Intersects(startPoint, endPoint))
        {
            if (current.left != null && current.left.max > startPoint)
            {
                current = current.left;
            }
            else
            {
                current = current.right;
            }

        }
        return current?.interval;
    }

    public IEnumerable<Interval> SearchAll(double startPoint, double endPoint)
    {
        var result = new List<Interval>();
        this.SearchAll(this.root, startPoint, endPoint, result);
        return result;
    }

    private void SearchAll(Node node, double startPoint, double endPoint, List<Interval> result)
    {
        if (node == null)
        {
            return;
        }

       var goLeft = node.left != null && node.left.max > startPoint;
        var goRight = node.right != null && node.right.interval.Lo < endPoint;

        if (goLeft)
        {
            this.SearchAll(node.left, startPoint, endPoint, result);
        }

        if (node.interval.Intersects(startPoint, endPoint))
        {
            result.Add(node.interval);
        }

        if (goRight)
        {
            this.SearchAll(node.right, startPoint, endPoint, result);
        }
        
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.left, action);
        action(node.interval);
        this.EachInOrder(node.right, action);
    }

    private Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.interval.Lo);
        if (cmp < 0)
        {
            node.left = this.Insert(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = this.Insert(node.right, lo, hi);
        }

        this.UpdateMax(node);

        return node;
    }

    private void UpdateMax(Node node)
    {
        var maxChild = this.GetMax(node.left, node.right);
        var maxNode = this.GetMax(node, maxChild);
        node.max = maxNode.max;
    }

    private Node GetMax(Node a, Node b)
    {
        if (a == null)
        {
            return b;
        }

        if (b == null)
        {
            return a;
        }

        return a.max.CompareTo(b.max) > 0 ? a : b;
    }
}
