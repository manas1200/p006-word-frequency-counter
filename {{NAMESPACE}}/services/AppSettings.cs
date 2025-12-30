namespace {{NAMESPACE}}.services;

public class AppSettings : BaseAppSettings
{
    public string DefaultHttpUrl { get; init; } = Environment.GetEnvironmentVariable("DEFAULT_HTTP_URL") ?? "https://google.com";
    public bool EnableAliveNudge { get; init; } = Convert.ToBoolean(Environment.GetEnvironmentVariable("ENABLE_ALIVE_NUDGE") ?? "True");
    public string AliveNudgeCron { get; init; } = Environment.GetEnvironmentVariable("ALIVE_NUDGE_CRON") ?? "*/30 * * * * *"; // every 30 seconds
}