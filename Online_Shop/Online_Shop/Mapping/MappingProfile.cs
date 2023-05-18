using AutoMapper;
using Online_Shop.Dto;
using Online_Shop.Models;

namespace Online_Shop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<OrderProduct, OrderProductDto>().ReverseMap();
            CreateMap<List<Product>, List<ProductDto>>().ReverseMap();
            CreateMap<List<Order>, List<OrderDto>>().ReverseMap();
        }
    }
}
