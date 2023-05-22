using JewelryShop.Application.Contracts;

namespace JewelryShop.Models
{
    public class CartModel
    {
        public ProductDto Product { get; set; }
        public int count { get; set; }
        public string  totalprice { get; set; }
    }
}
