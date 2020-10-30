using System;
using System.Collections.Generic;

namespace BinTree
{
    class BinTree<T> where T : IComparable
    {
        private Node<T> root = null;

        private void Add(Node<T> root, Node<T> node)
        {
            if (root.Value.CompareTo(node.Value) == 0)
                return;

            if (root > node)
            {
                if (root.Left == null)
                    root.Left = node;
                else
                    this.Add(root.Left, node);
            }
            else
            {
                if (root.Right == null)
                    root.Right = node;
                else
                    this.Add(root.Right, node);
            }
        }

        public void Add(T value)
        {
            Node<T> node = new Node<T>(value);

            if (this.root == null)
            {
                this.root = node;
                return;
            }

            this.Add(this.root, node);
        }

        public List<Token<T>> Tokenization()
        {
            int Compare(Token<T> left, Token<T> right) => left.Root.CompareTo(right.Root);

            List<Token<T>> tree = new List<Token<T>>();

            if (this.root == null)
                return tree;

            int count = 0;
            Stack<(int, int, Node<T>)> stack = new Stack<(int, int, Node<T>)>();
            stack.Push((++count, 0, this.root));
            while (stack.Count != 0)
            {
                (int id, int root, Node<T> node) = stack.Pop();

                if (node == null)
                {
                    tree.Add(new Token<T>(id, root));
                    continue;
                }

                tree.Add(new Token<T>(id, root, node.Value));

                stack.Push((++count, id, node.Right));
                stack.Push((++count, id, node.Left));
            }

            tree.Sort((left, right) => Compare(left, right));

            return tree;
        }

        public void Output(Action<string> output)
        {
            Token<T> Pop(List<Token<T>> tokens)
            {
                int index = tokens.Count - 1;
                var value = tokens[index];
                tokens.RemoveAt(index);
                return value;
            }
            Token<T> Find(List<Token<T>> tokens, int id)
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].Id == id)
                        return tokens[i];
                }

                return null; // Никогда не произойдет
            }

            if (this.root == null)
                return;

            List<Token<T>> tree = this.Tokenization();

            while (tree.Count > 1)
            {
                Token<T> left = Pop(tree);
                Token<T> right = Pop(tree);
                Token<T> root = Find(tree, left.Root);

                int widthRoot = root.Merge[0].Length;
                int widthLeft = left.Merge[0].Length;
                int widthRight = right.Merge[0].Length;

                if (widthLeft == 0 && widthRight == 0)
                    continue;

                string rootEmpty = new string(' ', widthRoot);
                string leftEmpty = new string(' ', widthLeft);
                string rightEmpty = new string(' ', widthRight);
                int count = Math.Max(left.Merge.Count, right.Merge.Count);
                root.Merge[0] = leftEmpty + root.Merge[0] + rightEmpty;
                for (int i = 0; i < count; i++)
                {
                    string value = left.Merge.Count > i ? left.Merge[i] : leftEmpty;
                    value += rootEmpty;
                    value += right.Merge.Count > i ? right.Merge[i] : rightEmpty;
                    root.Merge.Add(value);
                }
            }

            foreach (string value in tree[0].Merge)
                output(value);
        }
    }
}
