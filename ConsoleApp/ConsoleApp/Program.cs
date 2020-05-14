using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await PrintAsync();
            PrintSync();

            Console.WriteLine("Check Async!");

        }

        public static async Task PrintAsync()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    Console.WriteLine($"Async = {i}");
                    // Console.WriteLine(Thread.CurrentThread.Name);
                }
            });
        }

        public static void PrintSync()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"Sync = {i}");
            }
        }
    }
}
