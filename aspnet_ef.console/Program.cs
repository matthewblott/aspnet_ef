using System;
using aspnet_ef.services;
using Microsoft.Extensions.DependencyInjection;

namespace aspnet_ef.console
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var services = Startup.ConfigureServices();
      var serviceProvider = services.BuildServiceProvider();
      var service = serviceProvider.GetService<IProductService>();

      var product = service.GetProductWithPrices(1);

      Console.WriteLine(product?.Name);

      var prices = product?.Prices;

      if (prices == null)
      {
        return;
      }

      foreach (var price in prices)
      {
        Console.WriteLine(price.Amount);
      }
      
    }
    
  }
  
}