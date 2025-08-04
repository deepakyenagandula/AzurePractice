using Azure.Storage.Blobs.Models;

namespace ConsoleApp1.MultiThreading;

public class ThreadLIfeCyle
{
    public static void KnowThreadLifeCycle()
    {
        /* Hi Here is the sample task cycle
         Different stages of Threads
         1. Created                  => Just created the task.  Example below */
        Task task = Task.Run(() =>
        {
            Printing();
        });
        /* 2. WaitingForActivation     =>  This mean when a task is created using ContinueWith method it has to wait for the task 
                                        completion of the task on which this current task is created. This stage is called WaitingForActivation. */
        task.ContinueWith(x =>  Console.WriteLine(""));
        /*3. WaitingToRun             =>  Until Scheduler schedule this task and assigning a thread resource the status will be WaitingRun.*/
        /*4. Running                  => Currently the thread is processing it execution.*/
        /* 5. WaitingForChildClass     => when a thread execution has anothr task created inside it so its execution completed and wait for
                                       the nested child task for its compleetion is WaitingForChildClass.*/

        // Final Status of Threads
        // 1. Faulted                   => This means while executing any exception or error occured.
        // 2. Cancelled                 => This means thread was cancelled by user using cancellationToken or whatever the sources are the to cancel in c#
        // 3. RanToCompletion           => This meanns thread completed its execution without any issue. 


    }

    public static void Printing()
    {
        System.Console.WriteLine("Printing");
    }
}
