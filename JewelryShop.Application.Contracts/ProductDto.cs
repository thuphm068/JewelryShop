﻿namespace JewelryShop.Application.Contracts
{
    public class ProductDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public WarrantyDto WarrantyDto { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public float Price { get; set; } = 0;
        public string FPrice { get; set; } = "";
        public Guid SubCategoryId { get; set; }
        public Guid CategoryId { get; set; }

        public string SubCategoryName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;


    }


}