using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace API.Data.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public string ConnectionString { get; set; }  
        public DbContextFactory()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            IConfigurationRoot root = configurationBuilder.Build();
            IConfigurationSection applicationConnectionString = root.GetSection("ConnectionStrings:ApplcationConnectionString");
            ConnectionString = applicationConnectionString.Value;
        }
        public ApplicationContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseSqlServer(ConnectionString);
            return new ApplicationContext(builder.Options);
        }
    }
}
