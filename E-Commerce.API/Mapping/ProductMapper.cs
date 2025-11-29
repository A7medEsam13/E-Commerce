using AutoMapper;
using E_Commerce.Core.Entities.Product;
using E_Commerce.Infrastructure.Data.DTOs;

namespace E_Commerce.API.Mapping
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, AddProductDto>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
