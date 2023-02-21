using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace Secrets
{
    public static class ProgramExtensions
    {
        public static void AddSecretStoreConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddDaprSecretStore(
               Constants.SECRET_STORE_NAME,
               new DaprClientBuilder().Build(), TimeSpan.FromSeconds(10));
        }
    }
}
