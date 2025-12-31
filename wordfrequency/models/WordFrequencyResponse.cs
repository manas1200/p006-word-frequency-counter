namespace wordfrequency.models;

public class WordFrequencyItem
{
    public string Word { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class WordFrequencyResponse
{
    public List<WordFrequencyItem> WordFrequencies { get; set; } = new();
    public int TotalWords { get; set; }
    public string? Error { get; set; }
}
