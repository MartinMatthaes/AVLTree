using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    public class AVLTree<T>
    {
        private class Node
        {
            public T Value;
            public int Height;
            public Node Left;
            public Node Right;

            public Node(T value)
            {
                Value = value;
                Height = 1;
            }
        }

        private Node root;
        private Comparison<T> compare;

        public AVLTree(Comparison<T> comparison)
        {
            compare = comparison;
        }

        private int GetHeight(Node node)
        {
            return node?.Height ?? 0;
        }

        private int GetBalanceFactor(Node node)
        {
            return GetHeight(node.Right) - GetHeight(node.Left);
        }

        private Node RightRotate(Node node)
        {
            Node newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            newRoot.Height = Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;
            return newRoot;
        }

        private Node LeftRotate(Node node)
        {
            Node newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            newRoot.Height = Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;
            return newRoot;
        }

        private Node Balance(Node node)
        {
            int balanceFactor = GetBalanceFactor(node);
            if (balanceFactor < -1)
            {
                if (GetBalanceFactor(node.Left) > 0)
                {
                    node.Left = LeftRotate(node.Left);
                }
                node = RightRotate(node);
            }
            else if (balanceFactor > 1)
            {
                if (GetBalanceFactor(node.Right) < 0)
                {
                    node.Right = RightRotate(node.Right);
                }
                node = LeftRotate(node);
            }
            else
            {
                node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            }
            return node;
        }

        public void Add(T value)
        {
            root = Add(root, value);
        }

        private Node Add(Node node, T value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            int comparisonResult = compare(value, node.Value);
            if (comparisonResult < 0)
            {
                node.Left = Add(node.Left, value);
            }
            else if (comparisonResult > 0)
            {
                node.Right = Add(node.Right, value);
            }
            else
            {
                node.Value = value;
            }
            return Balance(node);
        }
        public bool Contains(T value)
        {
            return Contains(root, value);
        }

        private bool Contains(Node node, T value)
        {
            if (node == null)
            {
                return false;
            }

            int comparisonResult = compare(value, node.Value);
            if (comparisonResult < 0)
            {
                return Contains(node.Left, value);
            }
            else if (comparisonResult > 0)
            {
                return Contains(node.Right, value);
            }
            else
            {
                return true;
            }
        }

        public void Remove(T value)
        {
            root = Remove(root, value);
        }

        private Node Remove(Node node, T value)
        {
            if (node == null)
            {
                return null;
            }

            int comparisonResult = compare(value, node.Value);
            if (comparisonResult < 0)
            {
                node.Left = Remove(node.Left, value);
            }
            else if (comparisonResult > 0)
            {
                node.Right = Remove(node.Right, value);
            }
            else
            {
                if (node.Left == null)
                {
                    node = node.Right;
                }
                else if (node.Right == null)
                {
                    node = node.Left;
                }
                else
                {
                    Node minNode = FindMin(node.Right);
                    node.Value = minNode.Value;
                    node.Right = Remove(node.Right, minNode.Value);
                }
            }

            if (node == null)
            {
                return null;
            }

            return Balance(node);
        }

        private Node FindMin(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public int MaxDepth()
        {
            return MaxDepth(root);
        }

        private int MaxDepth(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            else
            {
                int leftDepth = MaxDepth(node.Left);
                int rightDepth = MaxDepth(node.Right);
                return Math.Max(leftDepth, rightDepth) + 1;
            }
        }
    }
}
