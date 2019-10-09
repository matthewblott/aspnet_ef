using System;
using aspnet_ef.services;
using aspnet_ef.web;
using Microsoft.Extensions.DependencyInjection;

// https://www.entityframeworktutorial.net/entityframework6/transaction-in-entity-framework.aspx
// https://stackoverflow.com/questions/36583439/inject-dbcontext-in-asp-net-core-concrete-type-or-interface
// https://medium.com/agilix/entity-framework-core-one-transaction-per-server-roundtrip-de807bacd1d5

namespace aspnet_ef.console
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var host = new HostEnvironment();
      var services = new ServiceCollection();
      var startup = new Startup(host);

      startup.ConfigureServices(services);

      var serviceProvider = services.BuildServiceProvider();
      var service = serviceProvider.GetService<IProductService>();
      var product = service.GetProductWithPrices(1);

      Console.WriteLine(product?.Name);

      var prices = product?.Prices;

      if (prices == null)
        return;

      foreach (var price in prices)
        Console.WriteLine(price.Amount);
      
    }
    
  }
  
}