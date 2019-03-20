using aspnet_ef.data.models;
using aspnet_ef.services;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_ef.web.controllers
{
  public class ProductsController : Controller
  {
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
      _productService = productService;
    }

    public IActionResult Index()
    {
      var products = _productService.GetProducts();

      return View(products);
    }

    public IActionResult New()
    {
      var product = new Product();

      return View(product);
    }

    public IActionResult Edit(int id)
    {
      var product = _productService.GetProduct(id);

      return View(product);
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
      _productService.Add(product);

      return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public IActionResult Update(Product product)
    {
      _productService.Update(product);

      return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
      _productService.Delete(id);

      return RedirectToAction(nameof(Index));
    }
  }
}