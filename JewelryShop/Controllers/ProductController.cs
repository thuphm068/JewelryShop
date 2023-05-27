using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Domain.Entities;
using JewelryShop.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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



        [HttpGet]
        [Route("")]

        public async Task<IActionResult> Index(string? searchString, string? cate, string? sub, string sortOrder, string range = "", int pageIndex = 1)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentRange"] = range;
            ViewData["CurrentSearch"] = searchString;

            List<ProductHomePageDto> productHomePageDtos = new List<ProductHomePageDto>();
            if (cate != null)
            {
                productHomePageDtos = await _productService.GetProductsByCategoryName(cate);
                ViewData["CurrentCate"] = cate;
                HttpContext.Session.SetString("currentPage", cate);
            }
            else
            {
                if (sub != null)
                {
                    productHomePageDtos = await _productService.GetProductsBySubCategoryName(sub);
                    ViewData["CurrentSub"] = sub;
                    HttpContext.Session.SetString("currentPage", sub);
                }
                else
                {
                    productHomePageDtos = await _productService.GetAllAvailableProducts();
                    HttpContext.Session.SetString("currentPage", "Product");
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
                switch (range)
                {
                    case "< 500.000đ":
                        productHomePageDtos = productHomePageDtos.Where(x => x.Price < 500000).ToList();
                        break;

                    case "500.000đ - 1.000.000đ":
                        productHomePageDtos = productHomePageDtos.Where(x => x.Price < 1000000 && x.Price > 500000).ToList();
                        break;

                    case "> 1.000.000đ":

                        break;
                }
            }
            if (searchString != null)
            {
                productHomePageDtos = productHomePageDtos.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            int pageSize = 6;
            var objs = PaginatedList<ProductHomePageDto>.CreateAsync(productHomePageDtos, pageIndex, pageSize);

            foreach (var product in objs)
            {
                product.FPrice = PriceFormatter.FormatPrice(product.Price);
            }
            return View("Index", objs);
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
            productdto.FPrice = PriceFormatter.FormatPrice(productdto.Price);
            foreach (var p in productdtos)
            {
                p.FPrice = PriceFormatter.FormatPrice(p.Price);
            }
            productdtos = productdtos.Take(4).ToList();
            ViewData["RelatedProducts"] = productdtos;
            return View(productdto);
        }


        [HttpGet("AddtoCart/{id?}")]
        public async Task<IActionResult> AddtoCart(Guid? id, int count = 1)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productdto = await _productService.GetProductDetails((Guid)id);
            if (productdto == null) { return NotFound(); }

            string? pro = HttpContext.Session.GetString("P_ID");
            if (pro != null)
            {
                var listofP = JsonConvert.DeserializeObject<List<string>>(pro);
                if (listofP != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        listofP.Add(((Guid)id).ToString());

                    }
                    HttpContext.Session.SetString("P_ID", JsonConvert.SerializeObject(listofP
                ).ToString());
                }
            }
            else
            {
                var temp = new List<string>();
                for (int i = 0; i < count; i++)
                {
                    temp.Add(((Guid)id).ToString());

                }
                this.HttpContext.Session.SetString(
               "P_ID",
               JsonConvert.SerializeObject(temp).ToString()
               );
            }
            await this.HttpContext.Session.CommitAsync();
            return RedirectToAction("Index", "Cart");


        }


    }
}
