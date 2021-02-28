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
                { 20, "e" },
                { 35, "e" },
                { 40, "e" }
            };
            //Console.WriteLine(tree._root.Key);
            var traversedTree = tree.Traverse();
            foreach (var item in traversedTree)
                Console.WriteLine(item);
            Console.ReadLine();
        }
    }
}