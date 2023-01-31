using AVLTree;
using System;

class Program
{
    static void Main(string[] args)
    {
        AVLTree<int> tree = new AVLTree<int>(new Comparison<int>((a, b) => a.CompareTo(b)));
        tree.Add(1);
        tree.Add(2);
        tree.Add(3);
        bool containsTwo = tree.Contains(2);
        tree.Remove(2);
        int maxDepth = tree.MaxDepth();
        Console.WriteLine("Contains 2: " + containsTwo);
        Console.WriteLine("Max Depth: " + maxDepth);
        Console.ReadLine();
    }
}
