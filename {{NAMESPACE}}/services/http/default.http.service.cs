using Microsoft.Extensions.Logging;

namespace {{NAMESPACE}}.services.http;

public class DefaultHttpService
{
    private readonly ILogger<DefaultHttpService> _logger;
    private readonly AppSettings _appSettings;
    private readonly HttpClient _httpClient;
    public DefaultHttpService(ILogger<DefaultHttpService> logger, HttpClient httpClient, AppSettings appSettings)
    {
        _logger = logger;
        _appSettings = appSettings;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(appSettings.DefaultHttpUrl);
        // optional: configure base client configuration here
        // _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        // _httpClient.DefaultRequestHeaders.Add("User-Agent", "Cloops.Microservice.Template");
        // _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _appSettings.ApiKey);
    }

    public async Task<string> GetVersionAsync()
    {
        _logger.LogDebug("Getting version from default HTTP URL");
        var response = await _httpClient.GetAsync("/api/version").ConfigureAwait(false);
        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }
}