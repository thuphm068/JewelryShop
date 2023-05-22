using JewelryShop.Domain.Shared.Const;

namespace JewelryShop.Models
{
    public class RegisterViewModel
    {
        public string userPhone { get; set; }
        public string password { get; set; }
        public string conformPassword { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public Gender gender { get; set; }
        public DateTime birth { get; set; } = DateTime.Now;

      
    }
}
