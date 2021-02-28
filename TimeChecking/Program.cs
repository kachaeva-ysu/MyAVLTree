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
            var tree = new AVLTree<int, string>();
            tree.Add(30,"t");
            tree.Add(14, "re");
            tree.Add(40, "re");
            tree.Add(35, "re");
            tree.Add(50, "re");
            tree.Add(60,"rwe");
            tree.Add(45, "sf");
            tree.Remove(40);
            var traversedTree = tree.Traverse();
            foreach (var item in traversedTree)
                Console.WriteLine(item);
            Console.ReadLine();
        }
    }
}