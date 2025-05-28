    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
namespace PLINQ_Demonstration___02
{

    class Program
    {
        // IsPrime returns true if number is Prime, else false.
        private static bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            for (var divisor = 2; divisor <= Math.Sqrt(number); divisor++)
            {
                if (number % divisor == 0)
                    return false;
            }

            return true;
        }

        // GetPrimeList returns Prime numbers by using sequential ForEach
        private static IList<int> GetPrimeList(IList<int> numbers) =>
            numbers.Where(IsPrime).ToList();

        // GetPrimeListWithParallel returns Prime numbers by using Parallel.ForEach
        private static IList<int> GetPrimeListWithParallel(IList<int> numbers)
        {
            var primeNumbers = new ConcurrentBag<int>();

            Parallel.ForEach(numbers, number =>
            {
                if (IsPrime(number))
                    primeNumbers.Add(number);
            });

            return primeNumbers.ToList();
        }

        static void Main()
        {
            // 2 million
            var limit = 2_000_000;
            var numbers = Enumerable.Range(0, limit).ToList();

            var watch = Stopwatch.StartNew();
            var primeNumbersFromForeach = GetPrimeList(numbers);
            watch.Stop();
            Console.WriteLine($"Classical foreach loop | Total prime numbers : {primeNumbersFromForeach.Count} | Time Taken : {watch.ElapsedMilliseconds} ms.");

            var watchForParallel = Stopwatch.StartNew();
            var primeNumbersFromParallelForeach = GetPrimeListWithParallel(numbers);
            watchForParallel.Stop();
            Console.WriteLine($"Parallel.ForEach loop  | Total prime numbers : {primeNumbersFromParallelForeach.Count} | Time Taken : {watchForParallel.ElapsedMilliseconds} ms.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}
