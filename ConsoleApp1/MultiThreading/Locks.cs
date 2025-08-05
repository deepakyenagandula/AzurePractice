using System;

namespace ConsoleApp1.MultiThreading;

public class Locks
{

    object _lockObj = new object();

    int _a;

    void LockingAndIncreasingByOne()
    {
        // here locking means when a thread is running the piece of code and another thread wants to access the same code that
        // thread needs the obj which is placed inside lock to access it while trying to access. If the object is already locked the another thread should wait 
        // until the current thread which is using this object as unlock th object releases the lock obj. That object will only be released when 
        // the current thread task is finsihed means the code which is inside the flower braces of lock is completed or when the exception occured inside the same code
        lock (_lockObj)
        {
            _a++;
        }
    }

    void LockingAndDecreasingByOne()
    {
        lock (_lockObj)
        {
            _a--;
        }
    }

    static List<Task> LoopNTimesAndGetLisOfTasks(int n, Action a)
    {
        List<Task> tasks = new(n);
        for (int i = 0; i < n; i++)
        {
            tasks.Add(Task.Run(() => a()));
        }
        return tasks;
    }

    static void Process()
    {
        Locks locks = new();
        var incrementingTasks = LoopNTimesAndGetLisOfTasks(20, locks.LockingAndIncreasingByOne);
        var decreasingTasksTasks = LoopNTimesAndGetLisOfTasks(20, locks.LockingAndDecreasingByOne);
        incrementingTasks.AddRange(decreasingTasksTasks);
        Task.WaitAll(incrementingTasks.ToArray());
        Console.WriteLine("printing the value of _a below : ");
        Console.WriteLine(locks._a);
    }

}
