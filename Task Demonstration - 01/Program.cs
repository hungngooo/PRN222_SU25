﻿namespace Task_Demonstration___01
{
    internal class Program
    {
        static void PrintNumber(string message)
        {
            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine($"{message} {i}");
                Thread.Sleep(1000); // Simulate some work
            }
        }
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";
            Task task01 = new Task(() => PrintNumber("Task 01"));
            task01.Start();
            Task task02 = Task.Run(delegate
            {
                PrintNumber("Task 02");
            });
            Task task03 = new Task(new Action(() => PrintNumber("Task 03")));
            task03.Start();
            Console.WriteLine($"Thread '{Thread.CurrentThread.Name}'");
            Console.ReadKey();
        }
    }
}
