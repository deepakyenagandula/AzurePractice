namespace ConsoleApp1.MultiThreading;

public class RaceCondition
{
    static int _a;

    static void Calling()
    {
        ProcessIncrementAndDecrementParallely();
        System.Console.WriteLine("value of _a is " + _a);
        System.Console.WriteLine(_a);
    }

    public static void IncrementByOne()
    {
        _a++;
    }

    public static void DrecrementByOne()
    {
        _a--;
    }


    // here the value of _a is consumed by both of tasks parallelly 
    // suppose two threads are running for increasing the value of _a so 
    // Thread1                          Thread2
    // a = 0                            a = 0
    // temp becomes ONE                 lets say this another thread doing the operation at same time of thread 1
    // temp = a + 1;                    so the it took the value of _a which is ZERO.
    // a becomes value ONE              now it m increases the value of _a
    //                                  Thread2 Taking the value _a which was not updated by the thread1 at the time it is taken so there is a chance of getting the same value of _a for both the threads
    // a = temp                         temp = _a +1                            
    //                                  _a = temp                              
    //                                  value becomes one.

    static void ProcessIncrementAndDecrementParallely()
    {
        List<Task> increDecreTasks = new(40);
        for (int i = 0; i < 20; i++)
        {
            increDecreTasks.Add(Task.Run(IncrementByOne));
        }

        for (int i = 0; i < 20; i++)
        {
            increDecreTasks.Add(Task.Run(DrecrementByOne));
        }
        Task.WaitAll(increDecreTasks.ToArray());
    } 
    
    //Critical Section - Means a method or code wherenit access share resources Example above Increment and Decrement Methods
    

}
