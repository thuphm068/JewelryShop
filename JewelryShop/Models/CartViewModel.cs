using JewelryShop.Application.Contracts;

namespace JewelryShop.Models
{
    public class CartViewModel
    {
        public List<CartModel> CartModels { get; set; }

        public CustomerDto Customer { get; set; } = new CustomerDto();
    }
}
