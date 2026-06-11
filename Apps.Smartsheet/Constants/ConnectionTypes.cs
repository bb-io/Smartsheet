namespace Apps.Smartsheet.Constants;

public static class ConnectionTypes
{
    public const string ApiKey = "API Key";
    public const string OAuth = "OAuth";

    public static readonly string[] SupportedConnectionTypes = [ApiKey, OAuth];
}