using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspnet_ef.data.models
{
  public class Product
  {
    public int Id { get; set; }
    
    [Required, Column("ProductName")]
    public string Name { get; set; }
    
    public ICollection<Price> Prices { get; set; }
    
  }
  
}