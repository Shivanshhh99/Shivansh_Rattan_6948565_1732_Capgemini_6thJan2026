using AutoMapper;
using ECommerce.API.DTOs;
using ECommerce.API.Models;

namespace ECommerce.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}