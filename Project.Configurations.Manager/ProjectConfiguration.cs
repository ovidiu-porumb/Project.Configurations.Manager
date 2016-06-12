using System.IO;
using System.Text;
using Project.Configurations.Manager.Exceptions;
using Project.Configurations.Manager.Model;
using Project.Configurations.Preconditions;

// ReSharper disable StaticMemberInGenericType

namespace Project.Configurations.Manager
{
    public static class ProjectConfiguration<T> where T : ConfigurationModelBase
    {
        private static ConfigurationSource _configurationSource;

        public static T Data { get; private set; }
        public static string EnvironmentName { get; private set; }

        public static void Load(string configurationFilePath, string environmentFilePath)
        {
            SetupConfigurationSource(configurationFilePath, environmentFilePath);

            LoadEnvironmentName();
            LoadConfiguration();
        }

        private static void SetupConfigurationSource(string configurationFilePath, string environmentFilePath)
        {
            var preconditionsHandler = new PreconditionsHandler();
            preconditionsHandler.AssesThatIsMet(ConfigurationIsLoadedForTheFirstTime, _configurationSource, new ConfigurationHasBeenLoadedAlready());

            _configurationSource = new ConfigurationSource
            {
                ConfigurationFilePath = configurationFilePath,
                EnvironmentFilePath = environmentFilePath
            };
        }

        private static bool ConfigurationIsLoadedForTheFirstTime(ConfigurationSource configurationSource)
        {
            return configurationSource == null;
        }

        private static void LoadEnvironmentName()
        {
            var preconditionsHandler = new PreconditionsHandler();
            preconditionsHandler.AssesThatIsMet(File.Exists, _configurationSource.EnvironmentFilePath, new EnvironmentFileNotFoundException());

            var environment = new Environment();
            EnvironmentName = environment.Load(GetFileContent(_configurationSource.EnvironmentFilePath));
        }

        private static void LoadConfiguration()
        {
            var preconditionsHandler = new PreconditionsHandler();
            preconditionsHandler.AssesThatIsMet(File.Exists, _configurationSource.ConfigurationFilePath, new ConfigurationFileNotFoundException());

            Configuration<T> configuration = new Configuration<T>();
            Data = configuration.Load(GetFileContent(_configurationSource.ConfigurationFilePath), EnvironmentName);
        }

        private static string GetFileContent(string filePath)
        {
            string fileContent = string.Empty;

            using (var stream = new StreamReader(filePath, Encoding.UTF8))
            {
                fileContent = stream.ReadToEnd();
                stream.Close();
            }

            return fileContent;
        }
    }
}
