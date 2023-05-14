using JewelryShop.Application.Interfaces;
using JewelryShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JewelryShop.Controllers
{

    public class HomeController : Controller
    {

        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {

            _productService = productService;
        }


        [HttpGet("")]
        //public async Task<IActionResult> IndexAsync(string CategoryName)
        //{
        //    var productdtos = await _productService.GetProductsByCategoryName(CategoryName);
        //    return View(productdtos);
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}