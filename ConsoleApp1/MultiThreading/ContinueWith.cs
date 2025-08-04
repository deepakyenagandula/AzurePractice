namespace ConsoleApp1.MultiThreading;

public class ContinueWith
{
    static void PrintingTaskResultHere<T>(Task<T> tTask)
    {
        System.Console.WriteLine(tTask.Result);
    }


    static int ThrowException()
    {
        Thread.Sleep(1000);
        throw new Exception("here is the exception");
    }


    // when you make a task to wait or to get result from the task for generic tasks then that exception is thrown outside which is the thread which 
    static void CallingTask()
    {
        Task t = Task.Run(ThrowException).ContinueWith(x => PrintingTaskResultHere(x));
        t.Wait();
    }

    static void CallingWithFilters()
    {
        Task t = Task.Run(() =>
        {
            ThrowException();
        }).ContinueWith(x => Console.WriteLine(x.Exception!.Message), TaskContinuationOptions.OnlyOnFaulted);
    }
}
