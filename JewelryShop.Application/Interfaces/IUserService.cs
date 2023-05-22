﻿

using JewelryShop.Application.Contracts;

namespace JewelryShop.Application.Services;

public interface IUserService
{
    Task<bool> Login(CustomerDto customerDto);
    Task<bool> Register(CustomerDto customerDto);
    Task<CustomerDto?> ManageAccount(CustomerDto customerDto);
}