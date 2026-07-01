using Apps.Smartsheet.Models.Identifiers;
using Apps.Smartsheet.Models.Identifiers.Optional;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;

namespace Apps.Smartsheet.Webhooks.Handlers;

public class SmartsheetSheetEventHandler(
    InvocationContext context,
    [WebhookParameter(true)] SheetIdentifier sheetIdentifier,
    [WebhookParameter] OptionalColumnIdentifier? columnIdentifier)
    : SmartsheetBaseEventHandler(context, sheetIdentifier)
{
    protected override string WebhookName => HasColumn
        ? $"Blackbird Webhook - Sheet {SheetId} - Col {columnIdentifier!.ColumnId}"
        : $"Blackbird Webhook - Sheet {SheetId}";

    protected override object? Subscope => HasColumn
        ? new { columnIds = new[] { columnIdentifier!.ColumnId } }
        : null;
    
    private bool HasColumn => !string.IsNullOrEmpty(columnIdentifier?.ColumnId);
}