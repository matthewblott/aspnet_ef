using aspnet_ef.data.models;

namespace aspnet_ef.services
{
  public interface IProductService
  {
    Product GetProduct(int id);
    Product GetProductWithPrices(int id);
    
  }
  
}