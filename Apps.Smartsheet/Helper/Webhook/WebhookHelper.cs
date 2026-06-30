using System.Net;
using System.Text;
using Apps.Smartsheet.Webhooks.Models.Entity;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.Smartsheet.Helper.Webhook;

public static class WebhookHelper
{
    public static ProcessedEvent ProcessEvent(WebhookRequest request, string targetObjectType, string targetEventType)
    {
        var payload = Deserialize(request);
        if (TryHandshake(payload, out var handshake))
            return new(handshake!, null);

        var matchingEventIds = payload?.Events
            .Where(e => e.ObjectType == targetObjectType && e.EventType == targetEventType)
            .Select(e => e.Id)
            .ToList() ?? [];

        return new(new HttpResponseMessage(HttpStatusCode.OK), matchingEventIds);
    }

    public static ProcessedCellEvent ProcessCellEvents(WebhookRequest request, string targetEventType)
    {
        var payload = Deserialize(request);
        if (TryHandshake(payload, out var handshake))
            return new(handshake!, null);

        var changes = payload?.Events
            .Where(e => e.ObjectType == "cell" && e.EventType == targetEventType)
            .Select(e => new CellChange(e.ColumnId, e.RowId))
            .ToList() ?? [];

        return new(new HttpResponseMessage(HttpStatusCode.OK), changes);
    }

    public static WebhookResponse<T> Preflight<T>(HttpResponseMessage responseMessage) 
        where T : class
    {
        return new WebhookResponse<T>
        {
            HttpResponseMessage = responseMessage,
            ReceivedWebhookRequestType = WebhookRequestType.Preflight,
            Result = null
        };
    }

    private static WebhookPayloadEntity? Deserialize(WebhookRequest request)
    {
        var payloadString = request.Body.ToString()
                            ?? throw new PluginApplicationException("Payload string was empty");
        return JsonConvert.DeserializeObject<WebhookPayloadEntity>(payloadString);
    }

    private static bool TryHandshake(WebhookPayloadEntity? payload, out HttpResponseMessage? response)
    {
        if (string.IsNullOrEmpty(payload?.Challenge))
        {
            response = null;
            return false;
        }

        var handshakeResponse = new { smartsheetHookResponse = payload.Challenge };
        var handshakeJson = JsonConvert.SerializeObject(handshakeResponse);

        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(handshakeJson, Encoding.UTF8, "application/json")
        };
        httpResponse.Headers.Add("Smartsheet-Hook-Response", payload.Challenge);

        response = httpResponse;
        return true;
    }
}