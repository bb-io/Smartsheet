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
        var payloadString = request.Body.ToString() ?? 
                            throw new PluginApplicationException("Payload string was empty");
        
        var payload = JsonConvert.DeserializeObject<WebhookPayloadEntity>(payloadString);
        if (!string.IsNullOrEmpty(payload?.Challenge))
        {
            var handshakeResponse = new { smartsheetHookResponse = payload.Challenge };
            var handshakeJson = JsonConvert.SerializeObject(handshakeResponse);
            
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(handshakeJson, Encoding.UTF8, "application/json")
            };
            httpResponse.Headers.Add("Smartsheet-Hook-Response", payload.Challenge);
            
            return new(httpResponse, null); 
        }

        var matchingEventIds = payload?.Events
            .Where(e => e.ObjectType == targetObjectType && e.EventType == targetEventType)
            .Select(e => e.Id)
            .ToList() ?? [];

        return new(new HttpResponseMessage(HttpStatusCode.OK), matchingEventIds);
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
}