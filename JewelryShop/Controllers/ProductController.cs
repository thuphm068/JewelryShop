using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JewelryShop.Controllers
{
    [Route("san-pham")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
        
            _productService = productService;

        }

        [Route("Sort")]
        [HttpGet]
        public async Task<IActionResult> Sort(string sortOrder, string min = "000", string max = "1000000000", int pageIndex = 1)
        {
            ViewData["CurrentSort"] = sortOrder;
            var productHomePageDtos = await _productService.GetAllAvailableProducts();

            var realmin = Int32.Parse(new string(min.Substring(0, min.Length - 2).Where(x => x != ('.')).ToArray()));
            var realmax = Int32.Parse(new string(max.Substring(0, max.Length - 2).Where(x => x != ('.')).ToArray()));
            ViewData["CurrentMin"] = realmin.ToString();
            ViewData["CurrentMax"] = realmax.ToString();

            if (productHomePageDtos != null)
            {

                switch (sortOrder)
                {
                    case "price_desc":
                        productHomePageDtos = productHomePageDtos.OrderByDescending(s => s.Price).ToList();
                        break;
    
                    case "price_asc":
                        productHomePageDtos = productHomePageDtos.OrderBy(s => s.Price).ToList();
                        break;
                    default:
                        break;
                }

                if (realmax < realmin)
                {
                    return NotFound(); 
                }
                productHomePageDtos = productHomePageDtos.Where(x => x.Price < realmax && x.Price > realmin).ToList();

                int pageSize = 6;

                var objs = PaginatedList<ProductHomePageDto>.CreateAsync(productHomePageDtos, pageIndex, pageSize);

                return View("Index", objs);
            }
            return NotFound();
        }

        [HttpGet("{cate?}/{sub?}")]

        public async Task<IActionResult> IndexAsync(string? cate, string? sub, string sortOrder, string min = "000", string max = "1000000000", int pageIndex = 1)
        {
            ViewData["CurrentSort"] = sortOrder;

            var realmin = Int32.Parse(new string(min.Substring(0, min.Length - 2).Where(x => x != ('.')).ToArray()));
            var realmax = Int32.Parse(new string(max.Substring(0, max.Length - 2).Where(x => x != ('.')).ToArray()));
            ViewData["CurrentMin"] = realmin.ToString();
            ViewData["CurrentMax"] = realmax.ToString();

            List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
            if (cate != null)
            {
                productHomePageDtos = await _productService.GetProductsByCategoryName(cate);
                ViewData["CurrentCate"] = cate;
            }
            else
            {
                if (sub != null)
                {
                    productHomePageDtos = await _productService.GetProductsBySubCategoryName(sub);
                    ViewData["CurrentSub"] = sub;
                }
                else
                {
                    productHomePageDtos = await _productService.GetAllAvailableProducts();
                }
            }            
            if (productHomePageDtos != null)
            {

                switch (sortOrder)
                {
                    case "price_desc":
                        productHomePageDtos = productHomePageDtos.OrderByDescending(s => s.Price).ToList();
                        break;

                    case "price_asc":
                        productHomePageDtos = productHomePageDtos.OrderBy(s => s.Price).ToList();
                        break;
                    default:
                        break;
                }

                productHomePageDtos = productHomePageDtos.Where(x => x.Price < realmax && x.Price > realmin).ToList();

                int pageSize = 6;

                var objs = PaginatedList<ProductHomePageDto>.CreateAsync(productHomePageDtos, pageIndex, pageSize);

                return View("Index", objs);
            }

            return View(PaginatedList<ProductHomePageDto>.CreateAsync(productHomePageDtos, 1, 6));

        }



//[HttpGet("nhan-bac/{id?}")]  
//public async Task<IActionResult> IndexAsyncNhanBac(string? sub)
//{
//    List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
//    if (sub == null)
//    {
//        productHomePageDtos = await _productService.GetProductsByCategoryName("Nhẫn bạc");

//    }
//    else
//    {
//        productHomePageDtos = await _productService.GetProductsBySubCategoryName(sub);
//    }
//    return View("Index", productHomePageDtos);

//}        
//[HttpGet("khuyen-bac/{id?}")]  
//public async Task<IActionResult> IndexAsyncKhuyenBac(string? sub)
//{
//    List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
//    if (sub == null)
//    {
//        productHomePageDtos = await _productService.GetProductsByCategoryName("Khuyên bạc");
//    }
//    else
//    {
//        productHomePageDtos = await _productService.GetProductsBySubCategoryName(sub);
//    }
//    return View("Index", productHomePageDtos);
//}   


//[HttpGet("vong-bac/{id?}")]  
//public async Task<IActionResult> IndexAsyncVongBac(string? sub)
//{
//    List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
//    if (sub == null)
//    {
//        productHomePageDtos = await _productService.GetProductsByCategoryName("Vòng bạc");
//    }
//    else
//    {
//        productHomePageDtos = await _productService.GetProductsBySubCategoryName(sub);
//    }
//    return View("Index", productHomePageDtos);
//}


//[HttpGet("day-chuyen")]  
//public async Task<IActionResult> IndexAsyncKhuyenTai()
//{
//    var productdtos = await _productService.GetProductsByCategoryName("Dây chuyền");
//    return View("Index", productdtos);
//}


//[HttpGet("")]

//public async Task<IActionResult> Index()
//{
//    var productdtos = await _productService.GetAllAvailableProducts();
//    return View(PaginatedList<ProductHomePageDto>.CreateAsync(productdtos,1,6));
//}             


[HttpGet("detail/{id?}")]  
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
