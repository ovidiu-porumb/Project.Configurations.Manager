using My.Project.Configuration;
using Project.Configurations.Manager;

namespace My.Project.B
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectConfiguration<MyProjectConfigurationModel>.Load("DemoConfiguration.json", "DemoEnvironments.json");
        }
    }
}
