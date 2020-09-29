using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    public class AbpConfigurationiBuilderOptions
    {
        public Assembly UserSecretsAssembly { get; set; }
        public string UserSecretsId { get; set; }
        public string FullName { get; set; } = "appsettings";
        public string EnvironmentName { get; set; }
        public string BasePath { get; set; }
        public string EnvironmentVariablesPrefix { get; set; }
        public string[] CommandLineArgs { get; set; }
    }
}
