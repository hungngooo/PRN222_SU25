﻿using System;
using System.Linq;
using System.Collections.Generic;
namespace PLINQ_Demonstration___01
{
    class Program
    {
        public static void Main()
        {
            var range = Enumerable.Range(1, 1_000_000);

            // Here is Sequential version
            var resultList = range.Where(i => i % 3 == 0).ToList();
            Console.WriteLine($"Sequential: Total items are {resultList.Count}");

            // Here is Parallel version using .AsParallel method
            resultList = range.AsParallel().Where(i => i % 3 == 0).ToList();
            Console.WriteLine($"Parallel: Total items are {resultList.Count}");

            resultList = (from i in range.AsParallel()
                          where i % 3 == 0
                          select i).ToList();
            Console.WriteLine($"Parallel: Total items are {resultList.Count}");

            Console.ReadLine();
        } // end Main
    } // end Program
}
