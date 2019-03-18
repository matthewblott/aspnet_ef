using System;
using System.IO;
using aspnet_ef.data;
using aspnet_ef.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.UserSecrets;

// https://www.entityframeworktutorial.net/entityframework6/transaction-in-entity-framework.aspx
// https://stackoverflow.com/questions/36583439/inject-dbcontext-in-asp-net-core-concrete-type-or-interface
// https://medium.com/agilix/entity-framework-core-one-transaction-per-server-roundtrip-de807bacd1d5

namespace aspnet_ef.console
{
  public class Startup
  {
    public static IServiceCollection ConfigureServices()
    {
      var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true);
        
//      configuration.AddUserSecrets<Program>();
      
      var configurationRoot = configuration.Build();

      var connStr = configurationRoot.GetConnectionString("default");

      var services = new ServiceCollection();

      var path = Directory.GetCurrentDirectory();
      var info = Directory.GetParent(path);
      var connectionString = Path.Combine(info.FullName, "db", "aspnet_ef.sqlite");
      
      services.AddSingleton<IConfigurationRoot>(configurationRoot);
      services.AddScoped<IContext>(x => new Context(connectionString));
      services.AddScoped<IProductService, ProductService>();

      return services;

    }
    
  }
}