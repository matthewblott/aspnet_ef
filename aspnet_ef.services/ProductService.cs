using System;
using System.Collections.Generic;
using System.Linq;
using aspnet_ef.data;
using aspnet_ef.data.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace aspnet_ef.services
{
  public class ProductService : IProductService
  {
    private readonly IContext _db;

    public ProductService(IContext db)
    {
      _db = db;
    }

    public IEnumerable<Product> GetProducts()
    {
      return _db.Products;
    }

    public IEnumerable<Product> GetProductsFromSearch(string searchTerm)
    {
      var isIdSearch = int.TryParse(searchTerm, out var id);

      var q =
        from x in _db.Products
        select x;

      q =
        from x in q
        where x.Name.Contains(searchTerm)
        select x;
      
      if (isIdSearch)
      {
        q =
          from x in q
          where x.Id == id
          select x;
      }

      var items = q.ToList();

      var sql = q.ToSql();
      
      Console.WriteLine(sql);
      
      return items;
    }

    
    public Product GetProduct(int id)
    {
      return _db.Products.Find(id);
    }

    public void Add(Product product)
    {
      _db.Products.Add(product);
      _db.Instance.SaveChanges();

      _db.Commit();
    }

    public void Update(Product product)
    {
      var existingProduct = _db.Products.Find(product.Id);

      existingProduct.Name = product.Name;

      _db.Instance.SaveChanges();

      _db.Commit();
    }

    public void Delete(object id)
    {
      var product = _db.Products.Find(id);

      _db.Products.Remove(product);
      _db.Instance.SaveChanges();
      _db.Commit();
    }

    public Product GetProductWithPrices(int id)
    {
      return _db.Products.Where(x => x.Id == 1).Include(x => x.Prices).FirstOrDefault();
    }
    
  }
  
}