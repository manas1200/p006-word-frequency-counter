using CLOOPS.NATS.Attributes;
using CLOOPS.NATS.Meta;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using System.Text.RegularExpressions;
using wordfrequency.models;

namespace wordfrequency.controllers;

public class WordFrequencyController
{
    private readonly ILogger<WordFrequencyController> _logger;

    public WordFrequencyController(ILogger<WordFrequencyController> logger)
    {
        _logger = logger;
    }

    [NatsConsumer(_subject: "word.frequency")]
    public Task<NatsAck> HandleWordFrequency(
        NatsMsg<WordFrequencyRequest> msg,
        CancellationToken ct = default)
    {
        var request = msg.Data;

        // Empty or null text
        if (request == null || string.IsNullOrWhiteSpace(request.Text))
        {
            return Task.FromResult(
                new NatsAck(true, new WordFrequencyResponse
                {
                    WordFrequencies = new(),
                    TotalWords = 0
                })
            );
        }

        // Invalid topN
        if (request.TopN <= 0)
        {
            return Task.FromResult(
                new NatsAck(true, new WordFrequencyResponse
                {
                    Error = "Invalid topN value. It must be a positive integer."
                })
            );
        }

        // Normalize + split
        var words = Regex
            .Split(request.Text.ToLower(), @"[^a-z0-9]+")
            .Where(w => !string.IsNullOrWhiteSpace(w))
            .ToList();

        var totalWords = words.Count;

        // Count frequencies
        var frequencyMap = new Dictionary<string, int>();
        foreach (var word in words)
        {
            frequencyMap[word] = frequencyMap.GetValueOrDefault(word, 0) + 1;
        }

        // Sort & take top N
        var result = frequencyMap
            .OrderByDescending(kv => kv.Value)
            .ThenBy(kv => kv.Key)
            .Take(request.TopN)
            .Select(kv => new WordFrequencyItem
            {
                Word = kv.Key,
                Count = kv.Value
            })
            .ToList();

        var response = new WordFrequencyResponse
        {
            WordFrequencies = result,
            TotalWords = totalWords
        };

        _logger.LogInformation("Handled word.frequency request");

        return Task.FromResult(new NatsAck(true, response));
    }
}
