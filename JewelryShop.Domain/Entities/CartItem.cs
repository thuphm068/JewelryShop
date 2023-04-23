using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CartId { get; set; } 
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        public float TotalPrice { get; set; } = 0;
    }
}
