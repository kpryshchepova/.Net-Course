using Microsoft.Extensions.Configuration;

namespace Employees
{
    public class Configuration
    {

        public string GetConfigurationString()
        {
            IConfiguration builder = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json")
                .AddEnvironmentVariables()
                .Build();
            
            return builder.GetRequiredSection("AppConfig:DbConnection").Value.ToString();

        }
    }
}
