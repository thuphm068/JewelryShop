using JewelryShop.Application.Contracts;

namespace JewelryShop.Models
{
    public class OrderViewModel
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public List<string>  id { get; set; }
        public List<int>  count { get; set; }
        public float  total { get; set; }
    }
}
