using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets.Abstractions
{
    public interface ISecretsStore
    {
        public Task<Dictionary<string, string>> GetSecretAsync(string key);
    }
}
