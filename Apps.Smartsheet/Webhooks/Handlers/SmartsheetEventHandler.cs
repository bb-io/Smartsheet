using Apps.Smartsheet.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Smartsheet.Webhooks.Handlers;

public class SmartsheetEventHandler(
    InvocationContext context,
    [WebhookParameter(true)] SheetIdentifier sheetIdentifier)
    : SmartsheetBaseEventHandler(context, sheetIdentifier)
{
    protected override string WebhookName => $"Blackbird Webhook - Sheet {SheetId}";
}