
using JewelryShop.Application.Contracts;
using JewelryShop.Domain.Entities;
using Mapster;

namespace JewelryShop.Application.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderDetail, OrderDetailDto>()
            .Map(dest => dest.ProductOrderDto.Id, src => src.ProductId)
            .Map(dest => dest, src => src);

        config.NewConfig<Order, OrderDto>();

    }
}

