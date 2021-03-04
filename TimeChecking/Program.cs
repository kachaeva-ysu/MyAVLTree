using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            int n = 10000;
            int[] array = Enumerable.Range(1, n).ToArray();
            Random r = new Random();
            for (int i = n - 1; i > 0; i--)
            {
                int j = r.Next(i + 1);
                int temp = array[j];
                array[j] = array[i];
                array[i] = temp;
            }
            StringBuilder addResult = new StringBuilder("Add: ");
            StringBuilder removeResult = new StringBuilder("Remove: ");
            StringBuilder containsResult = new StringBuilder("Contains: ");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var tree = new AVLTree<int, string>();
            for (int i = 0; i < n; i++)
                tree.Add(array[i], "a");
            stopwatch.Stop();
            addResult.Append("AVLTree "+stopwatch.Elapsed);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 5000; i < 7000; i++)
                tree.Remove(array[i]);
            stopwatch.Stop();
            removeResult.Append("AVLTree " + stopwatch.Elapsed);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < n; i++)
                tree.ContainsKey(array[i]);
            stopwatch.Stop();
            containsResult.Append("AVLTree " + stopwatch.Elapsed);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            var dict = new SortedDictionary<int, string>();
            for (int i = 0; i < n; i++)
                dict.Add(array[i], "a");
            stopwatch.Stop();
            addResult.Append(" SortedDictionary "+stopwatch.Elapsed);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 5000; i < 7000; i++)
                dict.Remove(array[i]);
            stopwatch.Stop();
            removeResult.Append(" SortedDictionary " + stopwatch.Elapsed);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < n; i++)
                dict.ContainsKey(array[i]);
            stopwatch.Stop();
            containsResult.Append(" SortedDictionary " + stopwatch.Elapsed);

            Console.WriteLine(addResult);
            Console.WriteLine(removeResult);
            Console.WriteLine(containsResult);


            AVLTree<int, string> tree2 = new AVLTree<int, string>();
            int[] array2 = new int[] { 7, 13, 27, 3, 5, 1, 4, 8, 17, 22, 14, 41, 25 };
            foreach (var a in array2)
            {
                tree2.Add(a, "a");
            }
            tree2.Remove(27);
            tree2.Remove(7);
            tree2.Remove(13);
            tree2.Remove(8);
            array2 = new int[] { 3, 5, 1, 4, 17, 22, 14, 41, 25 };
            Array.Sort(array2);
            Console.WriteLine(tree.Traverse().ToString());
            Console.ReadLine();
        }
    }
}