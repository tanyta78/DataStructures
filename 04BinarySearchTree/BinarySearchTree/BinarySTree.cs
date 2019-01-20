namespace BinarySearchTree
{
    using System;
    using System.Collections.Generic;

    //all methods with iterative implementation

    public class BinarySTree<T> where T : IComparable
    {
        private Node root;

        public BinarySTree()
        {

        }

        private BinarySTree(Node node)
        {
            this.Copy(node);
        }

        private void Copy(Node node)
        {
            if (node == null) { return; }

            this.Insert(node.Value);
            this.Copy(node.Left);
            this.Copy(node.Right);
        }

        public void Insert(T element)
        {
            if (this.root == null)
            {
                this.root = new Node(element);
                return;
            }

            Node current = this.root;
            Node parent = current;
            while (current != null)
            {
                parent = current;
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)

                {
                    current = current.Right;
                }
                else
                {
                    return;
                }

            }

            current = new Node(element);

            if (parent.Value.CompareTo(element) < 0)
            {
                parent.Right = current;
            }
            else
            {
                parent.Left = current;
            }
        }

        public bool Contains(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current != null;
        }

        public BinarySTree<T> Search(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
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

            return new BinarySTree<T>(current);

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
                this.root = this.root.Right;
            }
            else
            {
                parent.Left = current.Right;
            }

        }

        //==============Recursive ==========================


        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var result = new List<T>();

            this.Range(this.root, result, startRange, endRange);

            return result ;
        }

        private void Range(Node node, List<T> result, T startRange, T endRange)
        {
            if (node == null)
            {
                return;
            }

            var compareLow = node.Value.CompareTo(startRange);
            var compareHigh = node.Value.CompareTo(endRange);

            if (compareLow > 0)
            {
                this.Range(node.Left, result, startRange, endRange);
            }
            if (compareLow >= 0 && compareHigh <= 0)
            {
                result.Add(node.Value);
            }
            if (compareHigh < 0)
            {
                this.Range(node.Right, result, startRange, endRange);
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

        private class Node
        {
            public Node(T value)
            {
                if (value == null)
                {
                    throw new ArgumentException("Cannot insert null value!");
                }
                this.Value = value;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
    }
}
