﻿namespace JewelryShop.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WarrantyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public float Price { get; set; } = 0;
        public Guid SubCategoryId { get; set; }
        public string Image { get; set; } = string.Empty;


    }

  
}