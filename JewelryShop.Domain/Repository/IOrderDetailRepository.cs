using JewelryShop.Domain.Entities;

namespace JewelryShop.Domain.Repository;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    public Task<List<OrderDetail>> GetDetailsInOrder(Guid Id);
}

