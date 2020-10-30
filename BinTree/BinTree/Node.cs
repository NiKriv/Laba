using System;

namespace BinTree
{
    class Node<T> where T : IComparable
    {
        public T Value { get; set; }

        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node(T value) => this.Value = value;

        public static bool operator <(Node<T> left, Node<T> right) => left.Value.CompareTo(right.Value) < 0;
        public static bool operator >(Node<T> left, Node<T> right) => left.Value.CompareTo(right.Value) > 0;
    }
}
