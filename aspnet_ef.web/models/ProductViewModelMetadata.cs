using System.ComponentModel.DataAnnotations;

namespace aspnet_ef.web.models
{
  public class ProductViewModelMetadata
  {
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
  }
}