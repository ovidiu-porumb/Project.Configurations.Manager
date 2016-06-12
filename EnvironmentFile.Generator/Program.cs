using System;
using System.Collections.Generic;
using FileGenerator.Serializer;
using Project.Configurations.Manager.Model;

namespace EnvironmentFile.Generator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Application started...");

            var environmentsToBeSerialized = new List<ProjectEnvironments>
            {
                new ProjectEnvironments {MachineName = "SB070044LT", EnvironmentName = "Development"}
            };

            Console.WriteLine("Serializing data:");
            foreach (var item in environmentsToBeSerialized)
            {
                Console.WriteLine("{{ MachineName: {0}, EnvironmentName: {1}}}", item.MachineName, item.EnvironmentName);
            }

            FileJsonSerializer<List<ProjectEnvironments>> serializer = new FileJsonSerializer<List<ProjectEnvironments>>();
            serializer.Serialize(environmentsToBeSerialized, "Environments.json");

            Console.WriteLine("Data serialized in file Environments.json");
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadKey();
        }
    }
}
