using JewelryShop.Domain.Entities;

namespace JewelryShop.Domain.Repository;

public interface ICustomerRepository : IRepository<Customer>
{
    public Task<Customer> GetCustomerByPhone(string phone);
}

