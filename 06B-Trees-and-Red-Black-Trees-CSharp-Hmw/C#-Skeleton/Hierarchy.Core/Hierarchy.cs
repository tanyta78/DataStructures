namespace Hierarchy.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node<T> root;
        private Dictionary<T, Node<T>> nodes;

        public Hierarchy(T root)
        {
            this.root = new Node<T>(root);
            this.nodes = new Dictionary<T, Node<T>>
            {
                { root, this.root }
            };
        }

        public int Count => this.nodes.Count;

        public void Add(T element, T child)
        {
            if (element == null || child == null)
            {
                throw new ArgumentException();
            }

            if (!this.Contains(element) || this.Contains(child))
            {
                throw new ArgumentException();
            }

            var parentNode = this.nodes[element];
            var childNode = new Node<T>(child)
            {
                Parent = parentNode
            };

            parentNode.Children.Add(childNode);
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
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node<T>>();

            var current = this.root;
            queue.Enqueue(current);

            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                yield return current.Value;
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node<T>
        {
            public Node(T value)
            {
                this.Value = value;
                this.Children = new List<Node<T>>();
                this.Parent = null;
            }

            public T Value { get; set; }
            public List<Node<T>> Children { get; set; }
            public Node<T> Parent { get; set; }

        }
    }
}