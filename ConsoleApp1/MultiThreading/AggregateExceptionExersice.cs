using System;

namespace Coding.Exercise
{
    public class AggregateExceptionExercise
    {
        public static Task Test(string? input)
        {
            var task = Task.Run(() => ParseToIntAndPrint(input)).ContinueWith(y => y.Exception!.Handle(eachException =>
            {
                if (eachException is ArgumentNullException)
                {
                    Console.WriteLine("The input is null.");
                    return true;
                }
                else if (eachException is FormatException)
                {
                    Console.WriteLine("The input is not in a correct format.");
                    return true;
                }
                else if (eachException is ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Unexpected exception type.");
                    throw eachException;
                }
                return true;
            }), TaskContinuationOptions.OnlyOnFaulted);
                 
            return task;
        }
        
        private static void ParseToIntAndPrint(string? input)
        {
            if(input is null)
            {
                throw new ArgumentNullException();
            }
            
            if(long.TryParse(input, out long result))
            {
                if(result > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException("The number is too big for an int.");
                }
                Console.WriteLine("Parsing successful, the result is: " + result);
            }
            else
            {
                throw new FormatException();
            }
        }
    }
}
