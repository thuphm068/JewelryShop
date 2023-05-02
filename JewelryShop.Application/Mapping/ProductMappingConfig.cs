
using JewelryShop.Application.Contracts;
using JewelryShop.Domain.Entities;
using Mapster;

namespace JewelryShop.Application.Mapping;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Product, ProductDto>();
        config.NewConfig<Product, ProductOrderDto>();
        config.NewConfig<Warranty, WarrantyDto>();


    }
}

