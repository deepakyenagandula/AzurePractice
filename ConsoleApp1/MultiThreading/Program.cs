using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace New
{
    public class Temporary
    {
        static void MultiThradingPractice()
        {
            string input = "";
            Console.WriteLine("App Started");
            while (!(input == "exit"))
            {
                Console.WriteLine("Please enter here to know your string length");
                string? userInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userInput))
                {
                    Task taskRes = Task.Run(() => PrintStrLength(userInput));
                    taskRes.Wait();
                    if (!taskRes.IsCompletedSuccessfully)
                    {
                        Console.WriteLine("the length is being processing you can do another length checks meanwhile ");
                    }
                    Task.Run(() => GetStrLength(""));
                }
            }
        }

        static void PrintStrLength(string input)
        {
            Thread.Sleep(5000);
            Console.WriteLine($"length of {input} : {input.Length}");
        }

        static int GetStrLength(string input)
        {
            Thread.Sleep(5000);
            return input.Length;
        }
    }
}