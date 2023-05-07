using JewelryShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JewelryShop.Controllers
{
    [Route("nhan-bac")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
        
            _productService = productService;

        }

        [HttpGet("")]  
        public async Task<IActionResult> IndexAsync()
        {
            var productdtos = await _productService.GetAllAvailableProducts();
            return View(productdtos);
        }
    }
}
