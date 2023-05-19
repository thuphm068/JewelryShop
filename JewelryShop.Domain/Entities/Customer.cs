using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryShop.Domain.Shared.Const;

namespace JewelryShop.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Male; //enum
        public string Address { get; set; } = string.Empty;
        public DateTime Birthday{ get; set; } = DateTime.Now;
        public string Password { get; set; } = string.Empty;
        

        public void UpdateCustomerInfo(Customer customer)
        {
            Name = customer.Name;
            Phone = customer.Phone;
            Address = customer.Address;
        }
    }
}
