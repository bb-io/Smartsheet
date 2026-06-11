using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Microsoft.Extensions.Configuration;

namespace Tests.Smartsheet.Base;

public class TestBase
{
    public static List<IEnumerable<AuthenticationCredentialsProvider>> CredentialGroups { get; private set; }
    
    public static List<InvocationContext> InvocationContexts { get; private set; }

    protected TestContext? TestContext { get; set; }

    protected FileManager FileManager { get; set; }

    static TestBase()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        CredentialGroups = config.GetSection("ConnectionDefinition")
            .GetChildren()
            .Select(section =>
                section.GetChildren()
                    .Select(child => new AuthenticationCredentialsProvider(child.Key, child.Value ?? string.Empty))
            )
            .ToList();

        InvocationContexts = CredentialGroups.Select(group => new InvocationContext
        {
            AuthenticationCredentialsProviders = group
        }).ToList();
    }

    protected TestBase()
    {
        FileManager = new FileManager();
    }
}
