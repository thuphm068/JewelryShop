using JewelryShop.Domain.Entities;
using JewelryShop.Domain.Repository;
using System;
using JewelryShop.Infrastructure.Persistence;
using JewelryShop.Application.Contracts;
using MapsterMapper;
using System.Security.Cryptography.X509Certificates;
using JewelryShop.Application.Interfaces;

namespace JewelryShop.Application.Services
{
    public class ProductService: IProductService
    {
        //set up related repository using Dependency Injection
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<SubCategory> _subcategoryRepository;
        private readonly IRepository<Warranty> _warrantyRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository,
            IRepository<SubCategory> subcategoryRepository,
            IRepository<Warranty> warrantyRepository,
            IMapper mapper) { 
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _warrantyRepository = warrantyRepository;

        }

        public async Task<List<ProductHomePageDto>> GetAllAvailableProducts()
        {
            var products = await _productRepository.GetAllList();
            var result = _mapper.Map<List<ProductHomePageDto>>(products);

            return result;
        }

        public async Task<List<ProductHomePageDto>> GetProductsByCategoryName(string categoryName)
        {
            var products = await _productRepository.GetAllList();
            var categorys = await _categoryRepository.GetAllList();
            var subcategorys = await _subcategoryRepository.GetAllList();

            var id = categorys.FirstOrDefault(x => x.Name == categoryName);
            subcategorys = subcategorys.Where(x => x.CategoryId.Equals(id)).ToList();
            var listSubCateIds = subcategorys.Select(x=> x.Id).ToList();

            products = products.Where(x => listSubCateIds.Contains(x.SubCategoryId)).ToList();

            var result = _mapper.Map<List<ProductHomePageDto>>(products);

            return result;
        }

        public async Task<ProductDto> GetProductDetails(Guid id)
        {
            var product = await _productRepository.GetById(id);
            var warranty = await _warrantyRepository.GetById(product.WarrantyId);

            var productdto = _mapper.Map<ProductDto>(product);
            var warrantydto = _mapper.Map<WarrantyDto>(warranty);
            productdto.WarrantyDto = warrantydto;

            return productdto;
        }





    }
}