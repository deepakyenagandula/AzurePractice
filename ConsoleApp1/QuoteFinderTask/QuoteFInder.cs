using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp1.MultiThreading;

namespace ConsoleApp1.QuoteFinderTask;

public class QuoteFInder
{
    static string url = "https://api.quotable.io/quotes?page={0}&limit={1}";
    static async Task<List<QuoteFinderResultType>> GetQuoteFinderResultsAsync(int pageNumber, int noOfRecordsPerPage)
    {
        string actualUrl = string.Format(url, pageNumber, noOfRecordsPerPage);
        System.Console.WriteLine(actualUrl);
        HttpClient client = new();
        HttpResponseMessage responseMsg = await client.GetAsync(actualUrl);
        System.Console.WriteLine(responseMsg.StatusCode);
        responseMsg.EnsureSuccessStatusCode();
        string responseAsJosn = await responseMsg.Content.ReadAsStringAsync();
        if (!string.IsNullOrEmpty(responseAsJosn))
        {
            var result = JsonSerializer.Deserialize<QuoteFinderAPIResponseType>(responseAsJosn);
            if (result.HasData() && result!.Results.HasData())
            {
                return result.Results;
            }
        }
        throw new Exception("Something went wrong");
    }

    static async Task<string?> FindAndGetMatchedShortestQuoteAsync(Task<List<QuoteFinderResultType>> quotes, string quoteWord)
    {
        var res = await quotes;
        return res.Where(w => w.Content.Contains(quoteWord, StringComparison.OrdinalIgnoreCase)).Select(z => z.Content).MinBy(y => y.Length);
    }

    static async Task Main()
    {
        int noOfPages = TakeNumericInput("Enter the number of pages to search : "),
        quotesPerPage = TakeNumericInput("Enter the number quotes to search in page (quotes per page) : ");
        List<Task<List<QuoteFinderResultType>>> quoteFinderTasks = new(noOfPages);
        for (int i = 1; i <= noOfPages; i++)
        {
            quoteFinderTasks.Add(GetQuoteFinderResultsAsync(i, quotesPerPage));
        }
        string word = TakeAValidWord("Enter the matching word : ");
        List<Task<string?>> tasks = new(noOfPages);
        foreach (Task<List<QuoteFinderResultType>> d in quoteFinderTasks)
        {
            if (IsParallel())
            {
                tasks.Add(Task.Run(() => FindAndGetMatchedShortestQuoteAsync(d, word)));
            }
            else
            {
                tasks.Add(FindAndGetMatchedShortestQuoteAsync(d, word));
            }
        }
        string?[] awaitedQuotes = await Task.WhenAll(tasks);
        int a = 0;
        foreach (string? eachQuote in awaitedQuotes)
        {
            a++;
            if (string.IsNullOrEmpty(eachQuote))
            {
                Console.WriteLine($"no quotes for page {a}");
            }
            else
            {
                Console.WriteLine($"found this quote in page {a} : {eachQuote}");
            }
        }
    }

    static bool CheckWord(string? word)
    {
        if (!string.IsNullOrEmpty(word) && word.Split([' ', ',', '.', '!', '?', ':', ';'], StringSplitOptions.RemoveEmptyEntries).Count() == 1 && word.All(w => char.IsLetter(w)))
        {
            return true;
        }
        return false;
    }

    static int TakeNumericInput(string contentToDisplay)
    {
        while (true)
        {
            string? userNumericInput = TakeInput(contentToDisplay);

            if (!string.IsNullOrEmpty(userNumericInput) && int.TryParse(userNumericInput, out int result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
    }

    static string? TakeInput(string contentTOisplay)
    {
        Console.Write(contentTOisplay);
        string? userInput = Console.ReadLine();
        return userInput;
    }

    static string TakeAValidWord(string contentTOisplay)
    {
        while (true)
        {
            string? userInputWord = TakeInput(contentTOisplay);

            if (CheckWord(userInputWord))
            {
                return $" {userInputWord} ";
            }
            Console.WriteLine("Please enter a valid input word.");
        }
    }
    static bool IsParallel() => true;
        
}

    




