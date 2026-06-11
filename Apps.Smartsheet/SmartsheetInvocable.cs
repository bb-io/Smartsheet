using Apps.Smartsheet.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Smartsheet;

public class SmartsheetInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds => 
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected SmartsheetClient Client { get; }

    protected SmartsheetInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
    }
}