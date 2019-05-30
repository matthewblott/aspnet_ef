using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace aspnet_ef.data
{
  public enum LoggingCategories
  {
    All = 0,
    Sql = 1
  }

  public static class DbContextLoggingExtensions
  {
    private static ILoggerFactory GetLoggerFactory(DbContext db)
    {
      var loggerFactory = db.GetService<ILoggerFactory>();

//      return loggerFactory;

      var serviceProvider = db.GetInfrastructure();
      var loggerFactory2 = serviceProvider.GetRequiredService<ILoggerFactory>();

      return loggerFactory2;

//      var serviceProvider = db.GetInfrastructure();
//      return (LoggerFactory) serviceProvider.GetService(typeof(ILoggerFactory));

    }
    
    public static void ConfigureLogging(this DbContext db, Action<string> logger, Func<string, LogLevel, bool> filter)
    {
      var loggerFactory = GetLoggerFactory(db);

      
      LogProvider.CreateOrModifyLoggerForDbContext(db.GetType(), loggerFactory, logger, filter);
      
    }

    public static void ConfigureLogging(this DbContext db, Action<string> logger,
      LoggingCategories categories = LoggingCategories.All)
    {
      var loggerFactory = GetLoggerFactory(db);

      if (categories == LoggingCategories.Sql)
      {
        var sqlCategories = new List<string>
        {
          DbLoggerCategory.Database.Command.Name,
          DbLoggerCategory.Query.Name,
          DbLoggerCategory.Update.Name
        };
        LogProvider.CreateOrModifyLoggerForDbContext(db.GetType(),
          loggerFactory,
          logger,
          (c, l) => sqlCategories.Contains(c));
      }
      else if (categories == LoggingCategories.All)
      {
        LogProvider.CreateOrModifyLoggerForDbContext(db.GetType(),
          loggerFactory, logger,
          (c, l) => true);
      }
    }
  }

  internal class LogProvider : ILoggerProvider
  {
    private static readonly ConcurrentDictionary<Type, LogProvider> Providers =
      new ConcurrentDictionary<Type, LogProvider>();

    //volatile to allow the configuration to be switched without locking
    private volatile LoggingConfiguration _configuration;


    private LogProvider(Action<string> logger, Func<string, LogLevel, bool> filter)
    {
      _configuration = new LoggingConfiguration(logger, filter);
    }

    public ILogger CreateLogger(string categoryName)
    {
      return new Logger(categoryName, this);
    }

    public void Dispose()
    {
    }

    private static bool DefaultFilter(string categoryName, LogLevel level)
    {
      return true;
    }

    public static void CreateOrModifyLoggerForDbContext(Type dbContextType,
      ILoggerFactory loggerFactory,
      Action<string> logger,
      Func<string, LogLevel, bool> filter = null)
    {
      var isNew = false;
      var provider = Providers.GetOrAdd(dbContextType, t =>
      {
        var p = new LogProvider(logger, filter ?? DefaultFilter);
        loggerFactory.AddProvider(p);
        isNew = true;
        return p;
      });

      if (!isNew) provider._configuration = new LoggingConfiguration(logger, filter ?? DefaultFilter);
    }

    private class LoggingConfiguration
    {
      public readonly Func<string, LogLevel, bool> Filter;
      public readonly Action<string> Logger;

      public LoggingConfiguration(Action<string> logger, Func<string, LogLevel, bool> filter)
      {
        Logger = logger;
        Filter = filter;
      }
    }

    private class Logger : ILogger
    {
      private readonly string _categoryName;
      private readonly LogProvider _provider;

      public Logger(string categoryName, LogProvider provider)
      {
        _provider = provider;
        _categoryName = categoryName;
      }

      public bool IsEnabled(LogLevel logLevel)
      {
        return true;
      }

      public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception exception, Func<TState, Exception, string> formatter)
      {
        //grab a reference to the current logger settings for consistency, 
        //and to eliminate the need to block a thread reconfiguring the logger
        var config = _provider._configuration;
        if (config.Filter(_categoryName, logLevel)) config.Logger(formatter(state, exception));
      }

      public IDisposable BeginScope<TState>(TState state)
      {
        return null;
      }
    }
  }
}