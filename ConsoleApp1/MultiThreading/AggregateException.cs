namespace ConsoleApp1.MultiThreading;

public class AggregateException
{
    static int _exceptionNo;

    public static void HandleAggregateException()
    {
        try
        {
            Task task = GetTaskOfMultipleExceptions().ContinueWith(aggregateExceptionTask =>
            {
                if (aggregateExceptionTask.Exception.HasData()) {
                    aggregateExceptionTask.Exception!.Handle(eachException =>
                    {
                        if (eachException is InvalidOperationException)
                            return true;
                        if (eachException is InvalidDataException)
                            return true;
                        return false;
                    });
                }
            });
            task.Wait();
        }
        catch
        {
            Console.WriteLine("hi ra chintu");
        }
    }

    static void CallingMainOutput()
    {
        HandleAggregateException();
    }

    public static void OccurInvalidDataException()
    {
        _exceptionNo++;
        throw new InvalidDataException($"{_exceptionNo} exception occured");
    }

    public static Task GetTaskOfMultipleExceptions()
    {
        return Task.Run(() =>
         {
             Task.Run(OccurInvalidDataException);
         }).ContinueWith(resultOfExceptionTask =>
         {
             Task.Run(OccurInvalidOperationException);
         });
    }


    public static void OccurInvalidOperationException() {
        _exceptionNo++;
        throw new InvalidOperationException($"{_exceptionNo} exception occured");
    }
    }