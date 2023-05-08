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

        [HttpGet("detail/{id?}")]  
        public async Task<IActionResult> Detail(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productdto = await _productService.GetProductDetails(id);
            var productdtos = await _productService.GetProductsBySubCategoryName(productdto.SubCategoryName);
            ViewData["RelatedProducts"] = productdtos;
            return View(productdto);


        }
    }
}
