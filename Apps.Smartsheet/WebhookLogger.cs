using System.Text;
using System.Text.Json;

namespace Apps.Smartsheet;

public static class WebhookLogger
{
    private static readonly HttpClient Client = new();
    private const string Url = "https://webhook.site/84019abf-000c-4f13-a42b-2386b990d88b";

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