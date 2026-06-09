using System.Text;
using System.Text.Json;

namespace Apps.Smartsheet;

public static class WebhookLogger
{
    private static readonly HttpClient Client = new();
    private const string Url = "https://webhook.site/bc0f9563-d993-4c80-9f84-a18d988f4438";
    
    public static void Log(object body)
    {
        try
        {
            var json = JsonSerializer.Serialize(body);
            using var request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = Client.Send(request);
        }
        catch (Exception ex)
        {
            var json = JsonSerializer.Serialize(ex.Message);
            using var request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = Client.Send(request);
        }
    }
}