using System;
using aspnet_ef.data.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace aspnet_ef.data
{
  public interface IDbContext : IDisposable
  {
    DbContext Instance { get; }
  }

  public interface IContext : IDbContext
  {
    DbSet<Product> Products { get; }
    DbSet<Price> Prices { get; }

    void Commit();
  }

  public class Context : DbContext, IContext
  {
    private IDbContextTransaction _transaction;

    public Context(DbContextOptions options) : base(options)
    {
//      var loggerFactory = this.GetService<ILoggerFactory>();
//
//      if (loggerFactory != null)
//      {
//        var logger = loggerFactory.CreateLogger(DbLoggerCategory.Database.Connection.Name);
//        
//        logger.Log(LogLevel.Information, "Hello from Db!");
//       
//      }
      
      _transaction = Instance.Database.BeginTransaction();
      // if logging ...
//      this.ConfigureLogging(Console.WriteLine, LoggingCategories.Sql);
    }
    
    public DbSet<Product> Products { get; private set; }
    public DbSet<Price> Prices { get; private set; }
    public DbContext Instance => this;

    public void Commit()
    {
      try
      {
        _transaction.Commit();
      }
      catch (Exception)
      {
        _transaction.Rollback();
      }
      finally
      {
        _transaction.Dispose();
        _transaction = Instance.Database.BeginTransaction();
      }
    }

    ~Context()
    {
      if (_transaction == null) return;

      _transaction.Dispose();
      _transaction = null;
      
    }
    
  }
  
}