using JewelryShop.Application.Contracts;
namespace JewelryShop.Application.Interfaces;

public interface IOrderService
{
    public Task<bool> AddOrder(OrderDto orderdto);
    public Task<List<OrderDto>> GetAllCurrentOrder(string id);

    public Task<List<OrderDetailDto>> GetDetailsInOrder(Guid Id);

}
