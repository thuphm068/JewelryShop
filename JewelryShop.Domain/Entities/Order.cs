using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryShop.Domain.Shared.Const;


namespace JewelryShop.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending; //enum
        public DateTime Date { get; set; } = DateTime.Now;
        public float Total { get; set; } = 0;
    }
}
