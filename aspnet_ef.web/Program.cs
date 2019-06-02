using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace aspnet_ef.web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

      try
      {
        logger.Debug("init main");
        
        var builder = WebHost
          .CreateDefaultBuilder(args)
          .UseStartup<Startup>()
          .ConfigureLogging(ConfigLogging)
          .UseNLog();
      
        var runner = builder.Build();

        runner.Run();

      }
      catch (Exception e)
      {
        logger.Error(e, e.Message);
      }
      finally
      {
        LogManager.Shutdown();
      }      
      
    }

    private static void ConfigLogging(ILoggingBuilder builder)
    {
      builder.ClearProviders();
      // builder.AddConsole();
      // builder.SetMinimumLevel(LogLevel.Information); 
      // builder.AddNLog();

      //      builder.AddFilter(DbLoggerCategory.Name, LogLevel.Debug);
      //      builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Debug);
      //      builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Debug);
      //      builder.AddFilter(DbLoggerCategory.Update.Name, LogLevel.Debug);

    }
    
  }
  
}