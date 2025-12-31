namespace wordfrequency.models;

public class WordFrequencyRequest
{
    public string? Text { get; set; }
    public int TopN { get; set; }
}
