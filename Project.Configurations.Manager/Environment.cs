using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Project.Configurations.Manager.Exceptions;
using Project.Configurations.Manager.Model;

// ReSharper disable InconsistentNaming
// ReSharper disable ParameterHidesMember

namespace Project.Configurations.Manager
{
    public class Environment
    {
        private string environmentFileContent;

        public string Load(string environmentFileContent)
        {
            this.environmentFileContent = environmentFileContent;
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
            return JsonConvert.DeserializeObject<List<ProjectEnvironments>>(environmentFileContent);
        }
    }
}
