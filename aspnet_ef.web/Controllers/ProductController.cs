using aspnet_ef.services;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_ef.web
{
  public class ProductController : Controller
  {
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
      _productService = productService;
    }
    
    public IActionResult Index()
    {
      var products = _productService.GetProducts();

      return View(products);
      
    }
    
  }
  
}