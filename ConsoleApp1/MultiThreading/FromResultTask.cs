using System;

namespace ConsoleApp1.MultiThreading;

public class FromResultTask
{

    //Here we if in case we need a task of any value but we donot want to use thread
    //but want to get a Task type result we can use Task.FromResult method.
    public static Task<T> ExampleForFromResult<T>(T t)
    {
        Task<T> tResultWithOutAnyExecution = Task.FromResult<T>(t);
        return tResultWithOutAnyExecution;
    }
}
