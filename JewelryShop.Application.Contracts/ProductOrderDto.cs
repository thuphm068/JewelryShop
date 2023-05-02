namespace JewelryShop.Application.Contracts
{
    public class ProductOrderDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; } = 0;


    }
    public class ProductHomePageDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; } = 0;
    }

  
}