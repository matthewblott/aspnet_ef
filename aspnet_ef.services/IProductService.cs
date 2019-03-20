using System.Collections.Generic;
using aspnet_ef.data.models;

namespace aspnet_ef.services
{
  public interface IProductService
  {
    IEnumerable<Product> GetProducts();
    Product GetProduct(int id);
    Product GetProductWithPrices(int id);

    void Add(Product product);

    void Update(Product product);

    void Delete(int id);

  }
  
}