using AVLTree;
using System;

class Program
{
    static void Main(string[] args)
    {
        AVLTree<int> tree = new AVLTree<int>(new Comparison<int>((a, b) => a.CompareTo(b)));
        //tree.Add(1);
        //tree.Add(3);
        //tree.Add(2);

        tree.Add(42);
        tree.Add(153);
        tree.Add(43);

        //tree.Add(20);

        //tree.Add(10);
        //tree.Add(30);

        //tree.Add(5);
        //tree.Add(15);
        //tree.Add(35);
        //tree.Add(25);

        tree.Remove(20);

        bool containsTwo = tree.Contains(2);
        tree.Remove(2);
        int maxDepth = tree.MaxDepth();
        Console.WriteLine("Contains 2: " + containsTwo);
        Console.WriteLine("Max Depth: " + maxDepth);
        Console.ReadLine();

        // FindMax()
        // Find()
        // Comments
    }
}
