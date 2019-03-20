using Microsoft.AspNetCore.Mvc;

namespace aspnet_ef.web.controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}