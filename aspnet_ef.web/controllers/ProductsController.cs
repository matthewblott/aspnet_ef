using System.Collections.Generic;
using aspnet_ef.data.models;
using aspnet_ef.services;
using aspnet_ef.web.models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_ef.web.controllers
{
  public class ProductsController : Controller
  {
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
      _productService = productService;
      _mapper = mapper;
    }

    public IActionResult Index()
    {
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