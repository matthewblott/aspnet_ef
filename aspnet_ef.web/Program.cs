using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace aspnet_ef.web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

      try
      {
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

      // Default loggers 
//      builder.AddConsole();
//      builder.AddDebug();
//      builder.AddEventSourceLogger();
//      builder.AddEventLog(); // Windows only
      
      //builder.SetMinimumLevel(LogLevel.Trace); 
      //builder.AddNLog(); // required ?

//      builder.AddFilter(DbLoggerCategory.Name, LogLevel.Debug);
//      builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
//      builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Debug);
//      builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Debug);
//      builder.AddFilter(DbLoggerCategory.Update.Name, LogLevel.Debug);

    }
    
  }
  
}