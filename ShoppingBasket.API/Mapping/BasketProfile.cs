using AutoMapper;
using ShoppingBasket.API.Domain.Entities;
using ShoppingBasket.API.DTOs;

namespace ShoppingBasket.API.Mapping;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<AddItemRequestDto, Product>();

        CreateMap<BasketItem, BasketItemDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Product.Name))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Product.Price))
            .ForMember(d => d.IsDiscounted, o => o.MapFrom(s => s.Product.IsDiscounted));
    }
}
