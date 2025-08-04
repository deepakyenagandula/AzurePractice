using System;

namespace ConsoleApp1.MultiThreading;

public class CancellationTokenExercise
{


    static void CancellingATask()
    {
        Console.WriteLine($"Cuurent Thread Id : {Thread.CurrentThread.ManagedThreadId}");

        // cancellation token helps in cancelling a token
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        Task t = Task.Run(() => UnlimitedLoopingMethod(cancellationTokenSource.Token));
        cancellationTokenSource.Cancel();
        Console.WriteLine("Main method finished");
    }

    static void UnlimitedLoopingMethod(CancellationToken token)
    {
        int i = 0;
        while (true)
        {
            token.ThrowIfCancellationRequested();
            Console.WriteLine($"value of i is : {i}");
            i++;
        }
    }
}
