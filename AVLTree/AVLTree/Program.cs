using AVLTree;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Create AVLTree<int> class using a Comparison<int> delegate
        AVLTree<int> tree = new AVLTree<int>(new Comparison<int>((a, b) => a.CompareTo(b)));

        // Add some nodes to the tree.
        tree.Add(1);
        tree.Add(3);
        tree.Add(2);

        tree.Add(74);
        tree.Add(153);
        tree.Add(43);

        // Call the Find method to search for a node with key 74.
        AVLTree<int>.Node node = tree.Find(74);
        if (node != null)
        {
            Console.WriteLine("Node with key 42 found: " + node.Value);
        }
        else
        {
            Console.WriteLine("Node with key 42 not found");
        }

        // Add some more nodes to the tree.
        tree.Add(20);
        tree.Add(10);
        tree.Add(30);
        tree.Add(5);
        tree.Add(15);
        tree.Add(35);
        tree.Add(25);

        // Remove a node from the tree.
        tree.Remove(20);

        // Check if the tree contains a node with key 2.
        bool containsTwo = tree.Contains(2);
        tree.Remove(2);

        // Get the maximum depth of the tree.
        int maxDepth = tree.MaxDepth();

        // Display the results.
        Console.WriteLine("Contains 2: " + containsTwo);
        Console.WriteLine("Max Depth: " + maxDepth);
        Console.ReadLine();
    }

}
