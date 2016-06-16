using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Project.Configurations.Manager.Exceptions;
using Project.Configurations.Manager.Model;
using Project.Configurations.Preconditions;

// ReSharper disable InconsistentNaming
// ReSharper disable ParameterHidesMember

namespace Project.Configurations.Manager
{
    public class Configuration<T> where T : ConfigurationModelBase
    {
        private string configurationFileContent;
        
        public T Load(string configurationFileContent, string environmentName)
        {
            this.configurationFileContent = configurationFileContent;

            var preconditionsHandler = new PreconditionsHandler();
            preconditionsHandler.AssesThatIsMet(EnvironmentIsConfigured, environmentName, new EnvironmentConfigurationNotFoundException());

            List<T> configuration = DeserializeConfiguration();

            return configuration.Single(c => c.EnvironmentName == environmentName);

        }

        public bool EnvironmentIsConfigured(string environmentName)
        {
            return configurationFileContent.Contains(environmentName);
        }

        private List<T> DeserializeConfiguration()
        {
            return JsonConvert.DeserializeObject<List<T>>(configurationFileContent);
        }
    }
}
