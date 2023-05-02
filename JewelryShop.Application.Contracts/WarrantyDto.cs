using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.Application.Contracts
{
    public class WarrantyDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Period { get; set; } = 0; //tháng
        public string Description { get; set; } = string.Empty;

    }
}
