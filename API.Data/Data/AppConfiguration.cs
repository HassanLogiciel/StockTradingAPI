using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace API.Data.Data
{
    public class AppConfiguration
    {
        public string IdentityConnectionString { get; set; }
        public string ApplicationConnectionString { get; set; }
        public AppConfiguration()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            IConfigurationRoot root = configurationBuilder.Build();
            IConfigurationSection identityConnectionString = root.GetSection("ConnectionStrings:IdentityConnectionString");
            IConfigurationSection applicationConnectionString = root.GetSection("ConnectionStrings:ApplcationConnectionString");
            IdentityConnectionString = identityConnectionString.Value;
            ApplicationConnectionString = applicationConnectionString.Value;
        }
    }
}
