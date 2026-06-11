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
    // https://developers.smartsheet.com/api/smartsheet/openapi/webhooks/updatewebhook
    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
    {
        var payloadUrl = values["payloadUrl"];
        string sheetId = sheetIdentifier.SheetId;
        
        var createRequest = new SmartsheetRequest("webhooks", Method.Post)
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
        var createResponse = await Client.ExecuteWithErrorHandling<Result<WebhookEntity>>(createRequest);
        var webhookId = createResponse.Value.Id;

        _ = Task.Run(async () =>
        {
            await Task.Delay(3000);
            
            var enableRequest = new SmartsheetRequest($"webhooks/{webhookId}", Method.Put)
                .WithJsonBody(new { enabled = true });
            await Client.ExecuteWithErrorHandling(enableRequest);
        });
    }

    // https://developers.smartsheet.com/api/smartsheet/openapi/webhooks/deletewebhook
    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
    {
        var payloadUrl = values["payloadUrl"];
        
        var listRequest = new SmartsheetRequest("webhooks");
        var webhooks = await Client.PaginateOffset<WebhookEntity>(listRequest).ToListAsync();
        
        var targetWebhooks = webhooks.Where(x => x.CallbackUrl == payloadUrl).ToList();
        if (targetWebhooks.Count == 0)
            return;

        var deleteTasks = targetWebhooks.Select(async x =>
        {
            var deleteRequest = new SmartsheetRequest($"webhooks/{x.Id}", Method.Delete);
            await Client.ExecuteWithErrorHandling(deleteRequest);
        });
        await Task.WhenAll(deleteTasks);
    }
}