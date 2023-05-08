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

        public async Task<List<ProductHomePageDto>> GetProductsBySubCategoryName(string subCategoryName)
        {
            var products = await _productRepository.GetAllList();
            var categorys = await _categoryRepository.GetAllList();
            var subcategorys = await _subcategoryRepository.GetAllList();

            var subcategory = subcategorys.FirstOrDefault(x => x.Name == subCategoryName);
           

            products = products.Where(x => x.SubCategoryId.Equals(subcategory.Id)).ToList();

            var result = _mapper.Map<List<ProductHomePageDto>>(products);

            return result;
        }

        public async Task<ProductDto> GetProductDetails(Guid id)
        {
            var product = await _productRepository.GetById(id);
            var warranty = await _warrantyRepository.GetById(product.WarrantyId);

            var productdto = _mapper.Map<ProductDto>(product);
            var subcategory = await _subcategoryRepository.GetById(product.SubCategoryId);


            productdto.SubCategoryName = subcategory.Name;
            productdto.SubCategoryId = subcategory.Id;

            var warrantydto = _mapper.Map<WarrantyDto>(warranty);
            productdto.WarrantyDto = warrantydto;

            return productdto;
        }

    }
}