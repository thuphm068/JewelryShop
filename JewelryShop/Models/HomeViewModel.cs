using JewelryShop.Application.Contracts;

namespace JewelryShop.Models
{
    public class HomeViewModel
    {
        public List<ProductHomePageDto> NhanBacs { get; set; }
        public List<ProductHomePageDto> DayChuyens { get; set; }
    }
}
