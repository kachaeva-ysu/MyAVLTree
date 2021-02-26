using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAVLTree;

namespace TimeChecking
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var tree = new AVLTree<int, string>
            {
                { 16, "e" },
                { 4, "e" },
                { 900, "e" },
                { 81, "e" },
                { 11, "e" },
                { 13, "e" },
                { 8, "e" },
                { 17, "e" },
                { 70, "e" },
                { 73, "e" },
                { 111, "e" },
                { 135, "e" }
            };
            Console.WriteLine(tree._root.Key);
            tree.Remove(16);
            var traversedTree = tree.Traverse();
            foreach (var item in traversedTree)
                Console.WriteLine(item);
            Console.ReadLine();
        }
    }
}