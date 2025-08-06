using System;
using Newtonsoft.Json;

namespace ConsoleApp1.QuoteFinderTask;

public class QuoteFinderAPIResponseType
{
    public long Count { get; set; }
    public long TotalCount { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public long lastItemIndex { get; set; }
    public List<QuoteFinderResultType> Results { get; set; } = [];
}

public class QuoteFinderResultType
{
    [JsonProperty("_id")]
    public string Id { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string AuthorSlug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; }
    public DateTime DateModified { get; set; }
    public long Length { get; set; }
    public List<string> Tags { get; set; } = [];

}