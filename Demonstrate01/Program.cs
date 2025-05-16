using System;
using System.Threading;
using System.Threading.Tasks;
namespace Demonstrate01
{
    class Program
    {
        static void PrintNumber(string message)
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"{message}:{i}");
                Thread.Sleep(1000); // Sleep 1 giây giữa mỗi lần in
            }
        } // end PrintNumber

        static void Main()
        {
            Thread.CurrentThread.Name = "Main";

            // Tạo task bằng lambda expression
            Task task01 = new Task(() => PrintNumber("Task 01"));
            task01.Start();

            // Tạo task bằng delegate và thực thi luôn
            Task task02 = Task.Run(delegate
            {
                PrintNumber("Task 02");
            });

            // Tạo task bằng Action delegate
            Task task03 = new Task(new Action(() =>
            {
                PrintNumber("Task 03");
            }));
            task03.Start();

            Console.WriteLine($"Thread '{Thread.CurrentThread.Name}'");
            Console.ReadKey();
        } // end Main
    } // end Program
}
