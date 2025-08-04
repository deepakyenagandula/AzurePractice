using System;
using Renci.SshNet;

namespace ConsoleApp1.MultiThreading;

public class ChildTask
{

    public class Student
    {
        internal int RollNo { get; set; }
        internal string? Name { get; set; }
    }

    public void CreatingAChildTask()
    {
        Task task = Task.Run(() =>
        {
            Console.WriteLine("Printing from parent task");
            // this below is child task
            Task childTask = Task.Run(() =>
            {
                Console.WriteLine("Printing from child task");
            });
        });
    }


    public static void Calling()
    {
        Example();
    }
    public static void Example()
    {
        try
        {
         Task t = Task.Run(() =>
        {
            Task t1 = Task.Run(() => { ThrowEx(); });
            Task t2 = Task.Run(() => { ThrowEx(); });
        });
        t.Wait();
        }
        catch
        {
            
        }
        
    }

    public static void ThrowEx()
    {
        throw new NotImplementedException();
    }
}
