using JewelryShop.Application.Contracts;

namespace JewelryShop.Models
{
    public class ProductViewModel
    {
        public ProductDto Product { get; set; }
        List<ProductHomePageDto> khuyentai { get; set; }
        List<ProductHomePageDto> nhanbac { get; set; }

    }
}
