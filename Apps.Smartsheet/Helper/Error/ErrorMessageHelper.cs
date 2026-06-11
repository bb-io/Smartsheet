namespace Apps.Smartsheet.Helper.Error;

public static class ErrorMessageHelper
{
    public static string GenerateFailedToDeleteMessage(string entityType)
    {
        return $"Failed to delete the {entityType}. No additional information received from Smartsheet";
    }
}