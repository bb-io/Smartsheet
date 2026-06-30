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

public abstract class SmartsheetBaseEventHandler(
    InvocationContext context,
    SheetIdentifier sheetIdentifier) 
    : SmartsheetInvocable(context), IWebhookEventHandler
{
    protected string SheetId => sheetIdentifier.SheetId;
    
    protected static string[] Events => ["*.*"];
    protected abstract string WebhookName { get; }
    protected virtual object? Subscope => null;

    // https://developers.smartsheet.com/api/smartsheet/openapi/webhooks/createwebhook
    // https://developers.smartsheet.com/api/smartsheet/openapi/webhooks/updatewebhook
    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> creds, Dictionary<string, string> values)
    {
        var payloadUrl = values["payloadUrl"];
        var body = new Dictionary<string, object?>
        {
            ["name"] = WebhookName,
            ["callbackUrl"] = payloadUrl,
            ["scope"] = "sheet",
            ["scopeObjectId"] = SheetId,
            ["events"] = Events,
            ["version"] = 1
        };
        if (Subscope is not null)
            body["subscope"] = Subscope;
        
        var createRequest = new SmartsheetRequest("webhooks", Method.Post).WithJsonBody(body);
        var createResponse = await Client.ExecuteWithErrorHandling<Result<WebhookEntity>>(createRequest);
        var webhookId = createResponse.Value.Id;

        /*
         * Spins off a new background thread that validates the webhook.
         * Because if we fire that POST request above, Smartsheet will pause it
         * and will immediately send a new handshake request to us.
         * And it'll expect us to send the response during 5 seconds
         *
         * So if we wait for the POST request to finish, it'll just fail the entire subscription flow
         * and timeout the handshake. So we need this Task.Run hack
         *
         * The delay is needed to ensure that the POST request actually delivers to the server
         * so we don't get a 404 error when trying to access the webhook by its ID
         */
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
        
        var targetWebhooks = webhooks.Where(x => x.CallbackUrl == payloadUrl && x.Name == WebhookName).ToList();
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