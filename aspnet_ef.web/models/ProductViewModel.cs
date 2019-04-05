using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_ef.web.models
{
  [ModelMetadataType(typeof(ProductViewModelMetadata))]

  public class ProductViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<PriceViewModel> Prices { get; set; }
  }
}