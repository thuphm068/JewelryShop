using JewelryShop.Application.Contracts;

namespace JewelryShop.Models
{
    public class ProfileViewModel
    {
        public CustomerDto Customer { get; set; }
        public List<OrderDto> OrderDtos { get; set; }

        public ChangePasswordViewModel ChangePassword { get; set; }
    }
}
