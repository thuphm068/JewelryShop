using JewelryShop.Application.Contracts;
using JewelryShop.Application.Interfaces;
using JewelryShop.Application.Services;
using JewelryShop.Domain.Shared.Const;
using JewelryShop.Helper;
using JewelryShop.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace JewelryShop.Controllers
{
    [Route("")]

    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/thong-tin-tai-khoan")]
        public async Task<IActionResult> AccountInfo()
        {
            string? phone = HttpContext.Session.GetString("phone");
            if (phone is null) { return Redirect("/dang-nhap"); }
            var cusInfo = await _userService.ManageAccount(phone);
            if (cusInfo is null) { return Redirect("/dang-nhap"); }
            var orderDtos = await _orderService.GetAllCurrentOrder(cusInfo.Phone);

            return View("Setting", new ProfileViewModel
            {
                OrderDtos = orderDtos,
                Customer = cusInfo
            });
        }

        [HttpPost]
        [Route("/EditAccount")]
        public async Task<IActionResult> EditAccount(ProfileViewModel profileViewModel, string gender)
        {
            if (gender == "Nam")
            {
                profileViewModel.Customer.Gender = Gender.Male;
            }
            else
            {
                profileViewModel.Customer.Gender = Gender.Female;

            }
            var result = await _userService.EditAccount(profileViewModel.Customer);
            if (result == false)
            {
                ViewBag.isFail = true;
            }
            ViewBag.isSuccess = true;
            var orderDtos = await _orderService.GetAllCurrentOrder(profileViewModel.Customer.Phone);
            profileViewModel.OrderDtos = orderDtos;
            return View("Setting", profileViewModel);
        }



        [HttpGet("/dang-nhap")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("/AuthenticateToLogin")]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {

            var customerDto = new CustomerDto
            {
                Password = vm.password,
                Phone = vm.userPhone
            };

            var result = await _userService.Login(customerDto);
            if (result)
            {
                SetUp(customerDto.Name, customerDto.Phone);
                if (TempData["ReturnUrl"] is null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(TempData["ReturnUrl"] as string ?? "/");
                }
            }
            else
            {
                ViewBag.isFail = true;

                return View();
            }
        }

        private void SetUp(string name, string phone)
        {
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("phone", phone);
        }

        [HttpGet("/dang-ky")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("/chinh-sua")]
        public IActionResult Setting()
        {
            return View("Setting");
        }

        [HttpPost("/Register")]
        public async Task<IActionResult> Register(RegisterViewModel vm, string gender)
        {
            var customerDto = new CustomerDto
            {
                Password = vm.password,
                Phone = vm.userPhone,
                Name = vm.name,
                Birthday = vm.birth,

                Address = vm.address,
                Email = vm.email,
            };
            if (gender == "Female")
            {
                customerDto.Gender = Gender.Female;
            }
            else
            {
                customerDto.Gender = Gender.Male;

            }
            var result = await _userService.Register(customerDto);
            if (result)
            {
                SetUp(customerDto.Name, customerDto.Phone);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.CommitAsync();
            return Redirect("/dang-nhap");
        }

    }
}
