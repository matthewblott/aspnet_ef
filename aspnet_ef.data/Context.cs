using System;
using System.IO;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using aspnet_ef.data.models;
using Microsoft.EntityFrameworkCore.Storage;

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
    private readonly string _connectionString;
    public DbSet<Product> Products { get; private set; }
    public DbSet<Price> Prices { get; private set; }
    public DbContext Instance => this;

    private IDbContextTransaction _transaction;
    
    public Context(string connectionString)
    {
      _connectionString = connectionString;
      _transaction = Instance.Database.BeginTransaction();

      // if logging ...
      //this.ConfigureLogging(Console.WriteLine, LoggingCategories.Sql);
      
    }
    
    public void Commit()
    {
      try
      {
        _transaction.Commit();
      }
      catch (Exception e)
      {
        _transaction.Rollback();
      }
      finally
      {
        _transaction.Dispose();
        _transaction = Instance.Database.BeginTransaction();
        
      }
      
    }
    
//    public Context(DbContextOptions options): base(options)
//    {
//    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
//      var path = Directory.GetCurrentDirectory();
//      var info = Directory.GetParent(path);
//      var connectionString = Path.Combine(info.FullName, "db", "aspnet_ef.sqlite");

      builder.UseSqlite("Data Source=" + _connectionString);
      
//      base.OnConfiguring(builder);
    }

    ~Context()
    {
      if (_transaction == null)
      {
        return;
      }
      
      _transaction.Dispose();
      _transaction = null;

    }
    
  }
  
}