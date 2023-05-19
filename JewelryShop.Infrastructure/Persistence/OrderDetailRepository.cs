using JewelryShop.Domain.Entities;
using JewelryShop.Domain.Repository;

using JewelryShop.Infrastructure.Entity_Framework_Core;
using Microsoft.EntityFrameworkCore;

namespace JewelryShop.Infrastructure.Persistence
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    { 
        public OrderDetailRepository(JewelryShopDBContext context): base(context) 
        {
        }

        public async Task<List<OrderDetail>> GetDetailsInOrder(Guid Id)
        {
            try
            {
                return await _context.Set<OrderDetail>().Where(x => x.OrderId == Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
