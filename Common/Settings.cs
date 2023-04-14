using Microsoft.Extensions.Configuration;

namespace Common
{
    public class Settings
    {
        private readonly IConfiguration Configuration;

        public Settings(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GetStorageConnectionString()
        {
            return Configuration.GetConnectionString("Storage");
        }
    }
}