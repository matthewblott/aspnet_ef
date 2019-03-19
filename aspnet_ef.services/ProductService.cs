using System.Collections.Generic;
using System.Linq;
using aspnet_ef.data;
using aspnet_ef.data.models;
using Microsoft.EntityFrameworkCore;

// https://stackoverflow.com/questions/36583439/inject-dbcontext-in-asp-net-core-concrete-type-or-interface
// https://medium.com/agilix/entity-framework-core-one-transaction-per-server-roundtrip-de807bacd1d5

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
    
    public Product GetProduct(int id)
    {
      return _db.Products.Find(id);
    }

    public Product GetProductWithPrices(int id)
    {
      // _db.Products.Include(x => x.Prices).FirstOrDefault(x => x.Id == 1);
      return _db.Products.Where(x => x.Id == 1).Include(x => x.Prices).FirstOrDefault();
    }

    private void Commit()
    {
      _db.Commit();
    }
    
  }
  
}