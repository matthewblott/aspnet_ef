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
    public DbSet<Product> Products { get; private set; }
    public DbSet<Price> Prices { get; private set; }
    public DbContext Instance => this;

    private IDbContextTransaction _transaction;
    
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

    public Context(DbContextOptions options) : base(options)
    {
      _transaction = Instance.Database.BeginTransaction();

      // if logging ...
      // this.ConfigureLogging(Console.WriteLine, LoggingCategories.Sql);
      
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