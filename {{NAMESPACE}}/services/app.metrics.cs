using System.Diagnostics.Metrics;

namespace {{NAMESPACE}}.services;

public class AppMetricsService
{
    private readonly Meter _meter;

    // define application specific metrics here

    /// <summary>
    /// Count of jobs cleaned up
    /// </summary>
    public Counter<long> someCount { get; }

    public AppMetricsService()
    {
        _meter = new Meter("AppMetrics");

        someCount = _meter.CreateCounter<long>(
            "{{NAMESPACE}}_some_count",
            "count",
            "Counts the number of some events"
        );
    }

    public void RecordSomeCount(long count, string label = "")
    {
        KeyValuePair<string, object?>[] tags =
        [
            new("label", label),
            // others as needed
        ];

        someCount.Add(count, tags.AsSpan());
    }
}