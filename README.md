# Project.Configurations.Manager

I created this small library to help the .NET developers to keep in check their configurations and to avoid the App.config nightmare accross the projects in a solution.


## Solution explorer view
![alt tag] (https://github.com/ovidiu-porumb/Project.Configurations.Manager/blob/master/DocumentationResources/SolutionExplorer.png)

## Installation

Clone this repo. Build it. Add reference to Project.Configurations.Manager in your projects.
A NuGet package will come after some tests are added.

## Usage

1. I suggest to create a configuration project (like the one presented in the demo: My.Project.Configuration). Here, you will create your strongly typed configuration model. 
2. Derive your configuration model from class ***ConfigurationModelBase***. The demo presented looks like this:
```cs
public class MyProjectConfigurationModel : ConfigurationModelBase
{
        public CommonConfigurations Common { get; set; }
        public MyProjectAConfiguration MyProjectA { get; set; }
        public MyProjectBConfiguration MyProjectB { get; set; }
}
```

You need to inherit from ConfigurationModelBase for the EnvironmentName property which will help structure the configuration.
In the demo, I created a small logical structure for:
1. common configurations which you use accross the projects and, normally, you would duplicate these values inside each project's App.config file. Example:
```cs
public class CommonConfigurations
{
        public string DatabaseServer { get; set; }
        public string LogFolder { get; set; }
}
```
2. specific configurations for each project, which you need only in the project's scope, like a particular name for the log file. Example:
```cs
public class MyProjectAConfiguration
{
        public string LogFileName { get; set; }
}
```
```cs
public class MyProjectBConfiguration
{
        public string LogFileName { get; set; }
}
```
Of course, you can have whatever properties you want inside these classes.


The configuration file will look something like this:
```json
[
    {
        "Common": {
            "DatabaseServer": "localhost\\sql2012",
            "LogFolder": "C:\\MyApplication\\Logs"
        },
        "MyProjectA": {
            "LogFileName": "LogFileForMyProjectA"
        },
        "MyProjectB": {
            "LogFileName": "LogFileForMyProjectB"
        },
        "EnvironmentName": "Development"
    },
    {
        "Common": {
            "DatabaseServer": "staging_machine_name\\sql2012",
            "LogFolder": "D:\\StagingAppFolder\\MyApplication\\Logs"
        },
        "MyProjectA": {
            "LogFileName": "LogFileForMyProjectA"
        },
        "MyProjectB": {
            "LogFileName": "LogFileForMyProjectB"
        },
        "EnvironmentName": "Staging"
    },
    {
        "Common": {
            "DatabaseServer": "production_machine_name\\sql2012",
            "LogFolder": "E:\\ProductionAppFolder\\MyApplication\\Logs"
        },
        "MyProjectA": {
            "LogFileName": "LogFileForMyProjectA"
        },
        "MyProjectB": {
            "LogFileName": "LogFileForMyProjectB"
        },
        "EnvironmentName": "Production"
    }
]
```

Yep. All the configurations accross your environments are in one place. No more scattered configs.

### Resolving which configurations should be used for the current environment

So, normally, you would add a config transform tool on your App.config and Web.config files like [SlowCheetah](https://visualstudiogallery.msdn.microsoft.com/69023d00-a4f9-4a34-a6cd-7e854ba318b5) which gets the job done, but it also adds overhead on the build configurations and transform files. Also, you could use a post build command to xcopy your desired config files besides your binaries which is worse than the config transform option.

The way that this library solves this predicament is:
1. map your machine name to the environment name in a json file. Example:
```json
[
    {
        "MachineName": "SB070044LT",
        "EnvironmentName": "Development"
    },
    {
        "MachineName": "StagingMachineName",
        "EnvironmentName": "Staging"
    },
    { 
        "MachineName": "ProductionMachineName",
        "EnvironmentName": "Production"
    }
]
```
2.* and that's it! Use the mapped environment name in the configuration file*

Project.Configurations.Manager will check the machine name where it runs and then, based on that info, will choose the appropriate configuration set from the json file. 
For example, if I ran the application on the machine **SB070044LT** which is maped in the demo project to the **Development** environment, the following configurations were selected at runtime:
```json
 {
        "Common": {
            "DatabaseServer": "localhost\\sql2012",
            "LogFolder": "C:\\MyApplication\\Logs"
        },
        "MyProjectA": {
            "LogFileName": "LogFileForMyProjectA"
        },
        "MyProjectB": {
            "LogFileName": "LogFileForMyProjectB"
        },
        "EnvironmentName": "Development"
    }
```

### Accessing configurations from code

```cs
ProjectConfiguration<MyProjectConfigurationModel>.Load(configurationFilePath, environmentFilePath);
MyProjectConfigurationModel myConfig = ProjectConfiguration<MyProjectConfigurationModel>.Data;
```

where **myConfig** is a wrapper over **ProjectConfiguration<MyProjectConfigurationModel>.Data** to make it easier to access your configurations like this:
```cs 
Console.WriteLine("EnvironmentName: {0}", myConfig.EnvironmentName);
Console.WriteLine("DatabaseServer: {0}", myConfig.Common.DatabaseServer);
Console.WriteLine("LogFolder: {0}", myConfig.Common.LogFolder);
Console.WriteLine("LogFileName: {0}", myConfig.MyProjectA.LogFileName);
```