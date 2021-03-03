﻿using System;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var tree = new AVLTree<int, string>();
            for (int i = 0; i < n; i++)
                tree.Add(array[i], "a");
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 5000; i < 7000; i++)
                tree.Remove(array[i]);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < n; i++)
                tree.ContainsKey(array[i]);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            stopwatch = new Stopwatch();
            stopwatch.Start();
            var dict = new SortedDictionary<int, string>();
            for (int i = 0; i < n; i++)
                dict.Add(array[i], "a");
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 5000; i < 7000; i++)
                dict.Remove(array[i]);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < n; i++)
                dict.ContainsKey(array[i]);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            Console.ReadLine();
        }
    }
}