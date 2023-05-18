using JewelryShop.Application.Contracts;

namespace JewelryShop.Models
{
    public class CartViewModel
    {
        public ProductDto Product { get; set; }
        public int count { get; set; }
    }
}
