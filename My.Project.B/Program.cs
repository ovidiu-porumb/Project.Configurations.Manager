using System;
using System.IO;
using My.Project.Configuration;
using Project.Configurations.Manager;

namespace My.Project.B
{
    class Program
    {
        static void Main(string[] args)
        {
            string configurationFilePath =
                Path.Combine(
                    @"C:\Users\ovidiu.porumb\Documents\visual studio 2013\Projects\Project.Configurations.Manager",
                    "DemoConfiguration.json");
            string environmentFilePath =
                Path.Combine(
                    @"C:\Users\ovidiu.porumb\Documents\visual studio 2013\Projects\Project.Configurations.Manager",
                    "DemoEnvironments.json");

            ProjectConfiguration<MyProjectConfigurationModel>.Load(configurationFilePath, environmentFilePath);
            MyProjectConfigurationModel myConfig = ProjectConfiguration<MyProjectConfigurationModel>.Data;

            Console.WriteLine("Showing the configurations...");
            Console.WriteLine();

            Console.WriteLine("EnvironmentName: {0}", myConfig.EnvironmentName);
            Console.WriteLine("DatabaseServer: {0}", myConfig.Common.DatabaseServer);
            Console.WriteLine("LogFolder: {0}", myConfig.Common.LogFolder);
            Console.WriteLine("LogFileName: {0}", myConfig.MyProjectB.LogFileName);

            Console.WriteLine();
            Console.WriteLine("Press SPACE to exit...");
            Console.ReadKey();
        }
    }
}
