using Apps.Smartsheet.Api.Requests;
using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Utility.Wrapper;
using Apps.Smartsheet.Webhooks.Models.Entity;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.Smartsheet.Webhooks.Handlers;

public class SmartsheetEventHandler(InvocationContext context, [WebhookParameter(true)] SheetIdentifier sheetIdentifier) 
    : SmartsheetInvocable(context), IWebhookEventHandler
{
    private static string[] Events => ["*.*"]; 

    // https://developers.smartsheet.com/api/smartsheet/openapi/webhooks/createwebhook
    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
    {
        var payloadUrl = values["payloadUrl"];
        string sheetId = sheetIdentifier.SheetId;
        
        var request = new SmartsheetRequest("webhooks", Method.Post)
            .WithJsonBody(
                new
                {
                    name = $"Blackbird Webhook - Sheet {sheetId}",
                    callbackUrl = payloadUrl,
                    scope = "sheet",
                    scopeObjectId = sheetId,
                    events = Events,
                    version = 1 
                });

        await Client.ExecuteWithErrorHandling<Result<WebhookEntity>>(request);
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/webhooks/deletewebhook
    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
    {
        var payloadUrl = values["payloadUrl"];
        
        var listRequest = new SmartsheetRequest("webhooks");
        var webhooks = await Client.PaginateOffset<WebhookEntity>(listRequest).ToListAsync();
        
        var targetWebhook = webhooks.FirstOrDefault(x => x.CallbackUrl == payloadUrl);
        if (targetWebhook != null)
        {
            var deleteRequest = new SmartsheetRequest($"webhooks/{targetWebhook.Id}", Method.Delete);
            await Client.ExecuteWithErrorHandling(deleteRequest);
        }
    }
}