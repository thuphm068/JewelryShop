using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Helper;
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
        public async Task<IActionResult> Index(List<ProductHomePageDto> NhanBacs, List<ProductHomePageDto> DayChuyens, string? cate = "Nhẫn bạc")
        {
            HttpContext.Session.SetString("currentPage", "Home");

            List<ProductHomePageDto> productHomePageDtos1 = new List<ProductHomePageDto>();
            List<ProductHomePageDto> productHomePageDtos2 = new List<ProductHomePageDto>();

            productHomePageDtos1 = await _productService.GetProductsByCategoryName("Nhẫn bạc");
            productHomePageDtos2 = await _productService.GetProductsByCategoryName("Dây chuyền");

            HomeViewModel viewModel = new HomeViewModel
            {
                NhanBacs = productHomePageDtos1,
                DayChuyens = productHomePageDtos2,
            };
            foreach (var product in productHomePageDtos1)
            {
                product.FPrice = PriceFormatter.FormatPrice(product.Price);
            }  
            foreach (var product in productHomePageDtos2)
            {
                product.FPrice = PriceFormatter.FormatPrice(product.Price);
            } 
            return View("Index", viewModel);                
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