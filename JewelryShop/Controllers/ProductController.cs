using JewelryShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JewelryShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
        
            _productService = productService;

        }

        [HttpGet("Index")]  
        public IActionResult Index()
        {
            var productdtos = _productService.GetAllAvailableProducts();
            return View(productdtos);
        }
    }
}
