using System.Collections.Generic;
using aspnet_ef.data.models;
using aspnet_ef.services;
using aspnet_ef.web.models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace aspnet_ef.web.controllers
{
  [ServiceFilter(typeof(LogFilter))]
  public class ProductsController : Controller
  {
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(IProductService productService, IMapper mapper, ILogger<ProductsController> logger)
    {
      _productService = productService;
      _mapper = mapper;
      _logger = logger;
      _logger.LogInformation(message: "XXXXXXXXXXXXXXXXXXX ProductsController constructor called");
    }

    public IActionResult Index()
    {
      _logger.LogInformation(message: "ProductsController Index called");
      
      var products = _productService.GetProducts();
      var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
      
      return View(productViewModels);
    }

    public IActionResult New()
    {
      var product = new ProductViewModel();

      return View(product);
    }

    public IActionResult Edit(int id)
    {
      var product = _productService.GetProduct(id);
      var productViewModel = _mapper.Map<ProductViewModel>(product);
      
      return View(productViewModel);
      
    }

    [HttpPost]
    public IActionResult Create(ProductViewModel productViewModel)
    {
      var product = _mapper.Map<Product>(productViewModel);
      
      _productService.Add(product);

      return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public IActionResult Update(ProductViewModel productViewModel)
    {
      var product = _mapper.Map<Product>(productViewModel);

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