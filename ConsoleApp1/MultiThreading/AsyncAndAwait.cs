using System;
using System.Threading.Tasks;

namespace ConsoleApp1.MultiThreading;

public class AsyncAndAwait
{




    static async Task MainMethod()
    {
        Console.WriteLine("Main thread started execution...");
        // Here main thread will comeback and prints Main thread execution completed... immediately  because the task is not awaited so it runs in background
        // If we awaits the task like this while calling await DoHeavyTask(); then the main thread will not move forward until the DoHeavyTask is completed.
        // or else we can start and we can await it whenever we want the value or completion of task like await t;
        Task t = DoHeavyTask();
        Console.WriteLine("Main thread execution completed...");
    }

    // When main thread calls this method and reaches await keyword then main thread will return to Main method continues its execution without waiting for this DoHeavyTask Method
    // this method will be runned in background and returns a Task obj to main method
    static async Task DoHeavyTask()
    {
        await RunCPUBoundWork();
        Console.WriteLine("heavy task completed");
    }


    static async Task RunCPUBoundWork()
    {
        for (int i = 0; i < 5000; i++)
        {
            Console.WriteLine(i);
        }
    }
}
