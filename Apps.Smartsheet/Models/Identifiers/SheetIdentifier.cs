using Apps.Smartsheet.Handlers;
using Apps.Smartsheet.Handlers.FileFolder;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.SDK.Extensions.FileManagement.Models.FileDataSourceItems;

namespace Apps.Smartsheet.Models.Identifiers;

public class SheetIdentifier
{
    [Display("Sheet ID"), FileDataSource(typeof(SheetPickerDataHandler))]
    public string SheetId { get; set; } = string.Empty;

    [Display("Workspace ID"), DataSource(typeof(WorkspaceDataHandler))]
    public string? WorkspaceId { get; set; }
}