using JewelryShop.Domain.Shared.Const;

namespace JewelryShop.Application.Contracts
{
    public class OrderDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public OrderStatus Status { get; set; } = OrderStatus.Pending; //enum
        public DateTime Date { get; set; } = DateTime.Now;
        public float Total { get; set; } = 0;
        public List<OrderDetailDto> orderDetailDtos { get; set; }

    }
}