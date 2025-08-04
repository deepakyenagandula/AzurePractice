using System;
using System.Threading.Tasks;

namespace ConsoleApp1.MultiThreading;

public class AsyncAndAwait
{




    static void Main()
    {
        Task t = DoHeavyTask();
        
    }

    static async Task DoHeavyTask()
    {
        await Task.Delay(5000);
        Console.WriteLine("heavy task completed");
    }
}
