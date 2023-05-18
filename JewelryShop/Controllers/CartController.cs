using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelryShop.Controllers
{
    [Route("")]

    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {

            _productService = productService;

        }


        [HttpGet("gio-hang")]
        public async Task<IActionResult> Index()
        {
            var serializedString = HttpContext.Session.GetString("P_ID");
            var listofproduct = new List<CartViewModel>();

            if (serializedString != null)
            {
                var listofid = JsonConvert.DeserializeObject<List<string>>(serializedString);
                var reallist = 
                listofid.GroupBy(x => x)
                    .Select(x => new
                    {
                        id = x.Key,
                        count = x.Count()
                    }).ToList();

                if (listofid != null)
                {
                    foreach (var id in reallist) 
                    {
                        var product = await _productService.GetProductDetails(new Guid(id.id));
                        listofproduct.Add(
                            new CartViewModel
                            {
                                count = id.count,
                                Product = product
                            }
                            );

                    }
                }
            }
            return View("ShopCart", listofproduct);
        } 
        



        [HttpGet("dat-hang")]
        public IActionResult CheckOut()
        {
            return View("CheckOut");
        }
    }
}
