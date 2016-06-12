using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Project.Configurations.Manager.Exceptions;
using Project.Configurations.Manager.Model;

namespace Project.Configurations.Manager
{
    public class Environment
    {
        private string _environmentFileContent;

        public string Load(string environmentFileContent)
        {
            _environmentFileContent = environmentFileContent;
            string machineName = System.Environment.MachineName;
            List<ProjectEnvironments> environments = DeserializeEnvironments();

            ProjectEnvironments mappedEnvironment = environments.SingleOrDefault(e => e.MachineName == machineName);
            if (mappedEnvironment == null)
            {
                throw new MachineNameNotFoundInEnvironmentFile();
            }

            return mappedEnvironment.EnvironmentName;
        }

        private List<ProjectEnvironments> DeserializeEnvironments()
        {
            return JsonConvert.DeserializeObject<List<ProjectEnvironments>>(_environmentFileContent);
        }
    }
}
