using JewelryShop.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.Application.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductHomePageDto>> GetAllAvailableProducts();
        public Task<List<ProductHomePageDto>> GetProductsBySubCategoryName(string categoryName);
        public Task<ProductDto> GetProductDetails(Guid id);
        public Task<List<ProductHomePageDto>> GetProductsByCategoryName(string CategoryName);


    }
}
