﻿using JewelryShop.Application.Contracts;
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
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/thong-tin-tai-khoan")]
        public async Task<IActionResult> AccountInfo(string phone)
        {
            var cusInfo = await _userService.ManageAccount(new CustomerDto
            {
                Phone = phone
            });
            return View(cusInfo);
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
            if(result)
            {
                SetUp(customerDto.Name, customerDto.Phone);
                return RedirectToAction("Index", "Home");
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

    }
}