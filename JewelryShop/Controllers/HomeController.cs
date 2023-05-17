using JewelryShop.Application.Contracts;
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
        public async Task<IActionResult> Index(string? cate = "Nhẫn bạc")
        {
            List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
            productHomePageDtos = await _productService.GetProductsByCategoryName(cate);

            return View("Index", productHomePageDtos);
        }

        //public async Task<IActionResult> Index()
        //{
        //    List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
        //    productHomePageDtos = await _productService.GetAllAvailableProducts();
        //    return View(productHomePageDtos);
        //}

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