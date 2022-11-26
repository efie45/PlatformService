using System.Text;
using System.Text.Json;
using PlatformService.DTOs;

namespace PlatformService.SyncDataService.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task SendPlatformToCommand(PlatformReadDTO platform)
    {
        var httpContent = new StringContent(JsonSerializer.Serialize(platform), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_config["CommandService"]}/api/commands/platforms", httpContent);

        Console.WriteLine(
            response.IsSuccessStatusCode
                ? "--> Sync POST to command service was OK!"
                : "--> Sync POST to command service was NOT OK!");
    }
}





