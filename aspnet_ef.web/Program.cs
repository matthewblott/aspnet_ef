using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace aspnet_ef.web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebHost
        .CreateDefaultBuilder(args)
        .ConfigureLogging(ConfigLogging)
//        .ConfigureLogging(logging =>
//        {
//          logging.ClearProviders();
//          logging.AddConsole();
//          logging.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
//        })
        .UseStartup<Startup>();
      
      var runner = builder.Build();

      runner.Run();
      
    }

    private static void ConfigLogging(ILoggingBuilder builder)
    {
      builder.ClearProviders();
      builder.AddConsole();
      builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
      builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
      builder.AddFilter(DbLoggerCategory.Update.Name, LogLevel.Information);
      
    }
    
  }
  
}