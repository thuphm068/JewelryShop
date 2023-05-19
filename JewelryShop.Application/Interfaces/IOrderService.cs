using JewelryShop.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<bool> AddOrder(OrderDto orderdto);


    }
}
