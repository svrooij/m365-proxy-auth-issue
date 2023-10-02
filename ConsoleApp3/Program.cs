using Azure.Core;
using Azure.Identity;
using System.Runtime.CompilerServices;

namespace ConsoleApp3
{
    internal class Program
    {
        // CHANGE THIS TO YOUR TENANT ID
        private const string Tenant = "4523da32-a387-41b8-a85e-d6a051147706";
        const string scope = "https://vault.azure.net/.default";
        static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource(10000); // 10 seconds timeout
            var cred = new ChainedTokenCredential(
                new AzureCliCredential(new AzureCliCredentialOptions
                {
                    TenantId = Tenant,

                }),
                new VisualStudioCredential(new VisualStudioCredentialOptions
                {
                    TenantId = Tenant
                })
            );
            var token = await cred.GetTokenAsync(new Azure.Core.TokenRequestContext(new[] { scope }), cancellationTokenSource.Token);
            Console.WriteLine(token.Token);
        }
    }
}