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



        [Route("Sort")]
        [HttpGet]
        public async Task<IActionResult> Sort(string sortOrder, string min = "000", string max = "1500000000", int pageIndex = 1)
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

        [HttpGet]
        [Route("")]

        public async Task<IActionResult> Index(string? searchString, string? cate, string? sub, string sortOrder, string min = "000", string max = "1500000000", int pageIndex = 1)
        {
            ViewData["CurrentSort"] = sortOrder;

            var realmin = Int32.Parse(new string(min.Substring(0, min.Length - 2).Where(x => x != ('.')).ToArray()));
            var realmax = Int32.Parse(new string(max.Substring(0, max.Length - 2).Where(x => x != ('.')).ToArray()));
            ViewData["CurrentMin"] = realmin.ToString();
            ViewData["CurrentMax"] = realmax.ToString();
            ViewData["CurrentSearch"] = searchString;

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

            //return View(PaginatedList<ProductHomePageDto>.CreateAsync(productHomePageDtos, 1, 6));

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
