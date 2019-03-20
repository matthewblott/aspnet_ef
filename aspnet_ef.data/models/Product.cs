using System.Collections.Generic;

namespace aspnet_ef.data.models
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Price> Prices { get; set; }
  }
}