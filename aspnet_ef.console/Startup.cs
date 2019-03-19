using System;
using System.IO;
using aspnet_ef.data;
using aspnet_ef.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspnet_ef.console
{
  public class Startup
  {
    public static IServiceCollection ConfigureServices()
    {
      var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true);
        
      configuration.AddUserSecrets<Program>();
      
      var configurationRoot = configuration.Build();
      var connStr = configurationRoot.GetConnectionString("default");
      var services = new ServiceCollection();
      var path = Directory.GetCurrentDirectory();
      var info = Directory.GetParent(path);
      var fullPath = Path.Combine(info.FullName, "db");
      var connectionString = string.Format(connStr, fullPath);
      
      services.AddSingleton(configurationRoot);
      services.AddDbContextPool<IContext, Context>(x => x.UseSqlite(connectionString));
      services.AddScoped<IProductService, ProductService>();

      return services;

    }
    
  }
  
}