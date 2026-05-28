using Apps.Smartsheet.Api;
using Apps.Smartsheet.Api.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet.Connections;

public class ConnectionValidator(InvocationContext invocationContext) : BaseInvocable(invocationContext), IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        CancellationToken cancellationToken)
    {
        try
        {
            var client = new SmartsheetClient(creds);
            var request = new SmartsheetRequest("users/me");

            var response = await client.ExecuteAsync(request, cancellationToken);

            var isValid = response.StatusCode != System.Net.HttpStatusCode.Unauthorized;
            return new ConnectionValidationResponse
            {
                IsValid = isValid,
                Message = isValid ? "Success" : (response.Content ?? response.ErrorMessage ?? response.StatusCode.ToString()),
            };
        } 
        catch(Exception ex)
        {
            InvocationContext.Logger?.LogError($"Connection validation failed: {ex.Message}", []);

            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }
    }
}