using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    public class AVLTree<T>
    {
        // Node class with properties for value, height, left and right child nodes.
        public class Node
        {
            public T Value { get; set; } = default;
            public int Height;
            public Node Left;
            public Node Right;

            public Node(T value)
            {
                Value = value;
                Height = 1;
            }
        }
        // Root node of the AVL tree
        public Node root;
        // Comparison delegate used to compare values
        public Comparison<T> compare;

        // Constructor
        public AVLTree(Comparison<T> comparison)
        {
            compare = comparison;
        }

        // Helper method to get the height of a node
        private int GetHeight(Node node)
        {
            return node?.Height ?? 0;
        }

        // Helper method to get the balance factor of a node
        private int GetBalanceFactor(Node node)
        {
            return GetHeight(node.Right) - GetHeight(node.Left);
        }

        // Helper method to perform a right rotation on a node
        private Node RightRotate(Node node)
        {
            Node newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            newRoot.Height = Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;
            return newRoot;
        }

        // Helper method to perform a left rotation on a node
        private Node LeftRotate(Node node)
        {
            Node newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            newRoot.Height = Math.Max(GetHeight(newRoot.Left), GetHeight(newRoot.Right)) + 1;
            return newRoot;
        }

        // Helper method to balance a node
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

        // Public method to add a value
        public void Add(T value)
        {
            root = Add(root, value);
        }

        // Private method to add a value
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
        // Public method to check if a node contains a value
        public bool Contains(T value)
        {
            return Contains(root, value);
        }

   
        // Private method to check if a node contains a value
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

        // Public method to remove a value
        public void Remove(T value)
        {
            root = Remove(root, value);
        }

        // Private method to remove a value
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
                if (node.Left == null || node.Right == null)
                {
                    node = (node.Left == null) ? node.Right : node.Left;
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

        // Public method to find a value
        public Node Find(T value)
        {
            return FindHelper(root, value);
        }

        // Private method to help the public method to find a value
        private Node FindHelper(Node node, T value)
        {
            if (node == null)
                return null;

            int compareResult = Comparer<T>.Default.Compare(value, node.Value);
            if (compareResult < 0)
                return FindHelper(node.Left, value);
            else if (compareResult > 0)
                return FindHelper(node.Right, value);
            else
                return node;
        }

        // Private method to find min value of the tree
        private Node FindMin(Node node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        // Private method to find the max value of the tree
        private Node FindMax(Node node)
        {
            while (node.Right != null)
            {
                node = node.Right;
            }
            return node;
        }

        // Public method to give the max depth of the tree
        public int MaxDepth()
        {
            return MaxDepth(root);
        }

        // Private helper method to find max depth
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
