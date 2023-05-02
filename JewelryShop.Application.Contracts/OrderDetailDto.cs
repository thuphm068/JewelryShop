

namespace JewelryShop.Application.Contracts
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; } = 0;
        public float TotalPrice { get; set; } = 0;
        public ProductOrderDto ProductOrderDto { get; set; }
      
    }
}
