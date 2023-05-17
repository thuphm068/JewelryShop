using Microsoft.AspNetCore.Mvc;

namespace JewelryShop.Controllers
{
    [Route("")]

    public class CartController : Controller
    {
        [HttpGet("gio-hang")]
        public IActionResult Index()
        {
            return View("ShopCart");
        }
    }
}
