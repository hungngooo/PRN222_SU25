using System;
using System.Threading.Tasks;
namespace Asynchronous_Demonstration___02
{
    class Program
    {
        public static async Task<int> Method1()
        {
            int count = 0;
            await Task.Run(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("Method 1");
                    count += 1;
                }
            });
            return count;
        }

        public static void Method2()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Method 2");
            }
        }

        public static void Method3(int count)
        {
            Console.WriteLine("Method 3 is called.");
            Console.WriteLine($"Total count is {count}");
        }

        public static async Task callMethod()
        {
            Method2();                        // In ra "Method 2" 5 lần
            var count = await Method1();      // Đợi Method1() chạy xong, lấy count = 10
            Method3(count);                   // Gọi Method3 và in kết quả
        }

        static async Task Main(string[] args)
        {
            await callMethod();               // Gọi phương thức bất đồng bộ chính
            Console.ReadKey();                // Đợi người dùng nhấn phím
        }
    }
}
