using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAVLTree;

namespace TimeChecking
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new AVLTree<int, string>();
            tree.Add(20, "e");
            tree.Add(15, "e");
            tree.Add(17, "e");
            tree.Add(3, "e");
            tree.Add(18, "e");
            tree.Add(14, "e");
            Console.WriteLine(tree.FindNode(3).Parent.Parent.Right.Left.Key.ToString());
            Console.WriteLine("Nastya made a commit");
            Console.ReadLine();
        }
    }
}
