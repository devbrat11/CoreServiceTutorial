using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace TAM.Service.Helpers
{
    public class ConfigurationReader
    {
        private readonly IConfiguration _configuration;
        private string _fileName = "config.json";
        private LocalConfiguration _localConfiguration;

        public ConfigurationReader(IConfiguration configuration)
        {
            _configuration = configuration;
            JObject data = JObject.Parse(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),_fileName)));
            _localConfiguration = JsonConvert.DeserializeObject<LocalConfiguration>(data.ToString());
        }

        public string GetConnectionString(string dbType)
        {
            var server = _configuration.GetSection("DbConfiguration").GetSection("Server").Value;
            string connectionString = _localConfiguration.ConnectionStrings[dbType][server];
            return connectionString;
        }
    }

    public class LocalConfiguration
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Dictionary<string,Dictionary<string,string>> ConnectionStrings { get; set; }
    }

}
