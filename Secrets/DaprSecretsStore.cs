using Dapr.Client;
using Microsoft.Extensions.Logging;
using Secrets.Abstractions;

namespace Secrets
{
    public class DaprSecretsStore : ISecretsStore
    {
        private readonly DaprClient _dapr;
        private readonly ILogger _logger;

        public DaprSecretsStore(DaprClient dapr, ILogger<DaprSecretsStore> logger)
        {
            _dapr = dapr;
            _logger = logger;
        }
        public async Task<Dictionary<string, string>> GetSecretAsync(string key)
        {
            return await _dapr.GetSecretAsync(Constants.SECRET_STORE_NAME, key);
        }
    }
}
