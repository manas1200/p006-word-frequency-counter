using Microsoft.Extensions.Logging;

namespace wordfrequency.services;

public class EchoService
{
    private readonly ILogger<EchoService> _logger;

    public EchoService(ILogger<EchoService> logger)
    {
        _logger = logger;
    }

    public string Echo(string message)
    {
        _logger.LogDebug("Echoing message: {Message}", message);
        return message;
    }
}