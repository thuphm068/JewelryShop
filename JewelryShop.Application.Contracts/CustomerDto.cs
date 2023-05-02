using JewelryShop.Domain.Shared.Const;


namespace JewelryShop.Application.Contracts
{
    public class CustomerDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Male; //enum
        public string Address { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string Password { get; set; } = string.Empty;
    }
}
