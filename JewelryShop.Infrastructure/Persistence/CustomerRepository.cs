using JewelryShop.Domain.Entities;
using JewelryShop.Domain.Repository;

using JewelryShop.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace JewelryShop.Infrastructure.Persistence
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    { 
        public CustomerRepository(JewelryShopDBContext context): base(context) 
        {

        }

        public async Task<Customer?> GetCustomerByPhone(string phone)
        {
            return await _context.Set<Customer>().SingleOrDefaultAsync(x => x.Phone == phone);
        }

   
          
    }
}
