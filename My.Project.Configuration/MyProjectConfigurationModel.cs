using Project.Configurations.Manager.Model;

namespace My.Project.Configuration
{
    public class MyProjectConfigurationModel : ConfigurationModelBase
    {
        public CommonConfigurations Common { get; set; }
        public MyProjectAConfiguration MyProjectA { get; set; }
        public MyProjectBConfiguration MyProjectB { get; set; }
    }
}
