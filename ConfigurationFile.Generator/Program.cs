using System;
using System.Collections.Generic;
using FileGenerator.Serializer;
using My.Project.Configuration;

namespace ConfigurationFile.Generator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Application started...");

            var configurationToBeSerialized = new List<MyProjectConfigurationModel>
            {
                new MyProjectConfigurationModel
                {
                    EnvironmentName = "Development",
                        Common = new CommonConfigurations
                        {
                            DatabaseServer = "localhost\\sql2012",
                            LogFolder = "C:\\MyApplication\\Logs"
                        },
                        MyProjectA = new MyProjectAConfiguration
                        {
                            LogFileName = "LogFileForMyProjectA"
                        },
                        MyProjectB = new MyProjectBConfiguration
                        {
                            LogFileName = "LogFileForMyProjectB"
                        }
                },
                new MyProjectConfigurationModel
                {
                    EnvironmentName = "Staging",
                    Common = new CommonConfigurations
                    {
                        DatabaseServer = "staging_machine_name\\sql2012",
                        LogFolder = "D:\\StagingAppFolder\\MyApplication\\Logs"
                    },
                    MyProjectA = new MyProjectAConfiguration
                    {
                        LogFileName = "LogFileForMyProjectA"
                    },
                    MyProjectB = new MyProjectBConfiguration
                    {
                        LogFileName = "LogFileForMyProjectB"
                    }
                },
                new MyProjectConfigurationModel
                {
                    EnvironmentName = "Production",
                    Common = new CommonConfigurations
                    {
                        DatabaseServer = "production_machine_name\\sql2012",
                        LogFolder = "E:\\ProductionAppFolder\\MyApplication\\Logs"
                    },
                    MyProjectA = new MyProjectAConfiguration
                    {
                        LogFileName = "LogFileForMyProjectA"
                    },
                    MyProjectB = new MyProjectBConfiguration
                    {
                        LogFileName = "LogFileForMyProjectB"
                    }
                }
            };

            Console.WriteLine("Serializing data...");


            var serializer = new FileJsonSerializer<List<MyProjectConfigurationModel>>();
            serializer.Serialize(configurationToBeSerialized, "Configuration.json");

            Console.WriteLine("Data serialized in file Configuration.json");
            Console.WriteLine("Press SPACE to exit...");
            Console.ReadKey();
        }
    }
}
