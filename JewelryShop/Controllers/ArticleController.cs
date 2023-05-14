using Microsoft.AspNetCore.Mvc;

namespace JewelryShop.Controllers
{
    [Route("")]
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("lien-he")]        
        public IActionResult ContactUs()
        {
            return View("ContactUs");
        }
        [HttpGet("ve-cua-hang")]
        public IActionResult ABoutUs()
        {
            return View("AboutUs");
        }
    }
}
