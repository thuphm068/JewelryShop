using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.Domain.Entities
{
    public class Warranty
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Period { get; set; } = 0; //tháng
        public string Description { get; set; } = string.Empty;

    }
}
