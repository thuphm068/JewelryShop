using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Application.Services;
using JewelryShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace JewelryShop.Controllers
{
    [Route("san-pham")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {

            _productService = productService;

        }

        public async Task<IActionResult> Index()
        {
            var productdtos = await _productService.GetAllAvailableProducts();
            return View(productdtos);
        }



        [HttpGet("nhan-bac/{id?}")]
        public async Task<IActionResult> IndexAsyncNhanBac(string? sub, int page)
        {
            List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
            var pageResults = 6f;
            var pageCount = Math.Ceiling(productHomePageDtos.Count() / pageResults);

            
            if (sub == null)
            {
                productHomePageDtos = await _productService.GetProductsByCategoryName("Nhẫn bạc");
            }
            else
            {
                productHomePageDtos = await _productService.GetProductsBySubCategoryName(sub);
            }
            return View("Index", productHomePageDtos);

        }
        [HttpGet("khuyen-bac/{id?}")]
        public async Task<IActionResult> IndexAsyncKhuyenBac(string? sub)
        {
            List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
            if (sub == null)
            {
                productHomePageDtos = await _productService.GetProductsByCategoryName("Khuyên bạc");
            }
            else
            {
                productHomePageDtos = await _productService.GetProductsBySubCategoryName(sub);
            }
            return View("Index", productHomePageDtos);
        }


        [HttpGet("vong-bac/{id?}")]
        public async Task<IActionResult> IndexAsyncVongBac(string? sub)
        {
            List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
            if (sub == null)
            {
                productHomePageDtos = await _productService.GetProductsByCategoryName("Vòng bạc");
            }
            else
            {
                productHomePageDtos = await _productService.GetProductsBySubCategoryName(sub);
            }
            return View("Index", productHomePageDtos);
        }


        [HttpGet("day-chuyen")]
        public async Task<IActionResult> IndexAsyncKhuyenTai()
        {
            var productdtos = await _productService.GetProductsByCategoryName("Dây chuyền");
            return View("Index", productdtos);
        }


        [HttpGet("chi-tiet/{id?}")]
        public async Task<IActionResult> Detail(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productdto = await _productService.GetProductDetails((Guid)id);
            var productdtos = await _productService.GetProductsBySubCategoryName(productdto.SubCategoryName);
            ViewData["RelatedProducts"] = productdtos;
            return View(productdto);


        }


    }



}
