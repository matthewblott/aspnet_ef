using System.Collections.Generic;
using aspnet_ef.data.models;

namespace aspnet_ef.services
{
  public interface IProductService : IAddUpdate<Product>, IDelete
  {
    IEnumerable<Product> GetProducts();
    Product GetProduct(int id);
    Product GetProductWithPrices(int id);
      
  }
  
}