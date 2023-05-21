using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Helper;
using JewelryShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelryShop.Controllers
{
    [Route("")]

    public class UserController: Controller
    {


        [HttpGet("/dang-nhap")]
        public IActionResult Login(LoginViewModel vm)
        {

                return View("Login");

        }



        [HttpGet("/dang-ky")]
        public IActionResult Register()
        {
            return View("Register");
        }


    }
}
